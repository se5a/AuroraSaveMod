// See https://aka.ms/new-console-template for more information

using System.Linq.Expressions;
using System.Text.RegularExpressions;



Rejigger.DoRegex();
Console.WriteLine("RegexRejigger!");

public struct BlockParts
{
    public string StructRawName;
    public string StructName
    {
        get { return StructRawName + "Data"; }
    }
    public string StructArrayName
    {
        get { return StructRawName + "Store"; }
    }
    public string CollectionName;
    public string XinYList;
    public string XinY;
    public string Body;
    public string[] PreLoopSQLLines;
    public string[] TopInnerSQLLines;
    public string[] KeyVarLines;
    public string[] BottomSLQLines;
    //public int Depth;
    //public bool HasNested;
    //public ForeachParts[] Children;
}

public static class Rejigger
{
    
    static string varNamePtn = "\"@(?<varName>.*)\", ";
    
    static string varTypePtn = @".*\.(?<varType>.*)_\d+\);";
    private static string varTypeNamePtn = @".*\.(?<varType>.*_\d+)\);";
    
    static string varEnumNamePtn = @"genum(?<enum>\d+_\d+)";
    static string varEnumTypePtn = @"genum(?<enum>\d+)_\d+";
    static string methodNamePtn = "public void (?<methName>method_\\d+)\\(";
    static string ClassNamePtn = @"FCT_(?<className>\w+)(?= \(.*)";
    static string ClassNamePtn2 = @"DIM_(?<className>\w+)(?= \(.*)";
    static string keyVarLinesPtn = @"\s*sqliteCommand.Parameters.AddWithValue.*;";
    
    private static string origMethodName;
    private static string className;
    //private static string structName;
    //private static string structArrayName;
    
    //private static string collectionName;

    private static BlockParts TopLevelBlock;
    private static BlockParts[] ForeachBlocks;
    
    public static int indentIndex = 1;
    public static string indent = "\t";
    public static void DoRegex()
    {
        string input = System.IO.File.ReadAllText(@"AuroraMethods.txt");
        Setup(input);
        
        string classstr = BuildClassStart();
        //classstr = BuildStruct(classstr, TopLevelBlock);
        foreach (var block in ForeachBlocks)
        {
            classstr = BuildStruct(classstr, block);
        }
        
        
        classstr += "\n";
        classstr = BuildConstructor(classstr, input);
        classstr += "\n";
        classstr = BuildSaveToSqlMethod(classstr, input);
        while (indentIndex > 1)
        {
            indentIndex--;
            classstr = classstr.DoIndent("}\n");
        }

        
        System.IO.File.WriteAllText(@"Output.txt", classstr);

    }

    static void Setup(string input)
    {
        origMethodName = Regex.Match(input, methodNamePtn).Groups["methName"].Value;
        var match = Regex.Match(input, ClassNamePtn);
        if(!match.Success)
            match = Regex.Match(input, ClassNamePtn2);
        
        var groups = match.Groups;
        className = "Save"+groups["className"].Value;
        var structName = groups["className"].Value + "Data";
        //structArrayName = structName + "Store";
        


        var foreachLinesAray = Regex.Matches(input, @"(?<=\s*)foreach.*");
        ForeachBlocks = new BlockParts[foreachLinesAray.Count];
        var foreachSplit = Regex.Split(input, "foreach");

        //this chunk needs to check only blocks not inside a foreach loop.
        var matchs = Regex.Matches(foreachSplit[0], keyVarLinesPtn);
        var tlkvl = new string[matchs.Count];
        for (int i = 0; i < matchs.Count; i++)
        {
            tlkvl[i] = matchs[i].Value;
        }
        
        //string clctnPtn = "foreach .* this.(?<colName>.+_\\d+)\\.";
        //match = Regex.Match(foreachLinesAray[i].Value, clctnPtn);
        var collectionName = className; //match.Groups["colName"].Value;

        TopLevelBlock = new BlockParts()
        {
            StructRawName = structName,
            CollectionName = collectionName,
            KeyVarLines = tlkvl,
            Body = foreachSplit[0]
        };
        
        
        for (int i = 0; i < ForeachBlocks.Length; i++)
        {
            var strPreBlock = foreachSplit[i];
            var opening = Regex.Matches(strPreBlock, "{.*", RegexOptions.Singleline);
            
            strPreBlock = opening[opening.Count-1].Value;

            var sqlMatches = Regex.Matches(strPreBlock, "new SQLiteCommand.*");
            string[] sqlpreloopstrings = new string[sqlMatches.Count];
            for (int j = 0; j < sqlMatches.Count; j++)
            {
                sqlpreloopstrings[j] = sqlMatches[j].Value;
            }
            
            string listStr = "";
            var list = Regex.Match(strPreBlock, @"List<\w+\d*> list =.*");
            if (list.Success)
                listStr = list.Value;
            
            //this is imperfect, it wont handle nested stuff 
            var strBlock = foreachSplit[i + 1];
            //remove everything before the openbrace
            strBlock = Regex.Match(strBlock, @"\{.*", RegexOptions.Singleline).Value;
            //get everything to the matching closebrace
            var matchBrace = MatchingBrace(strBlock);

            //if MatchingBrace returns an empty string, it was unable to find the closing brace
            //this is normaly caused by the following foreachblock being nested inside this one.
            //todo: handle nesting
            if (matchBrace != "") 
                strBlock = matchBrace;
            
            sqlMatches = Regex.Matches(strBlock, "sqliteCommand.CommandText.*");
            string[] sqltopInner = new string[sqlMatches.Count];
            for (int j = 0; j < sqlMatches.Count; j++)
            {
                sqltopInner[j] = sqlMatches[j].Value;
            }
            
            sqlMatches = Regex.Matches(strBlock, "sqliteCommand.ExecuteNonQuery.*");
            string[] sqlBotInner = new string[sqlMatches.Count];
            for (int j = 0; j < sqlMatches.Count; j++)
            {
                sqlBotInner[j] = sqlMatches[j].Value;
            }
            
            var clctnPtn = "foreach .* this.(?<colName>.+_\\d+)\\.";
            match = Regex.Match(foreachLinesAray[i].Value, clctnPtn);
            collectionName = match.Groups["colName"].Value;

            var kvlMatches = Regex.Matches(strBlock, keyVarLinesPtn);
            var kvl = new string[kvlMatches.Count];
            for (int j = 0; j < kvlMatches.Count; j++)
            {
                kvl[j] = kvlMatches[j].Value;
            }

            //set struct name
            var input2 = strBlock;
            match = Regex.Match(input2, ClassNamePtn);
            if(!match.Success)
                match = Regex.Match(input2, ClassNamePtn2);
            groups = match.Groups;
            structName = groups["className"].Value;
            
            var parts = new BlockParts()
            {
                StructRawName = structName,
                CollectionName = collectionName,
                XinYList = listStr,
                XinY = foreachLinesAray[i].Value,
                Body = strBlock,
                KeyVarLines = kvl,
                PreLoopSQLLines = sqlpreloopstrings,
                TopInnerSQLLines = sqltopInner,
                BottomSLQLines = sqlBotInner,
                //HasNested = false,
            };
            ForeachBlocks[i] = parts;
        }
        
        //var foreachBlockMatches Regex.Matches(input, "foreach.*", RegexOptions.Singleline);
        


    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static string BuildClassStart()
    {

        string mthd = DoIndent("",@"/// <summary>" + "\n");
        mthd = mthd.DoIndent(@"/// " + origMethodName + "\n");
        mthd = mthd.DoIndent(@"/// </summary>" + "\n");
        mthd = mthd.DoIndent("public class " + className + "\n"); 
        mthd = mthd.DoIndent("{\n");
        indentIndex++;
        return mthd;
    }

    static string BuildStruct(string classstr, BlockParts parts)
    {

        var keyVarLines = parts.KeyVarLines;
        var structName = parts.StructName;
        var structArrayName = parts.StructArrayName;
        
        string structString = "";
        structString = structString.DoIndent();
        structString += "public struct " + structName + "\n";
        structString = structString.DoIndent();
        structString += "{\n";
        indentIndex++;
        
        foreach (string varline in keyVarLines)
        {
            var matchVarName = Regex.Match(varline, varNamePtn);
            
            var matchVarType = Regex.Match(varline, varTypePtn);
            
            var matchVarEnum = Regex.Match(varline, varEnumTypePtn);
            var matchTypeName = Regex.Match(varline, varTypeNamePtn);
            if (matchVarName.Success)
            {
                string varName = matchVarName.Groups["varName"].Value;
                if(varName == "GameID")
                    continue;
                string varType = matchVarType.Groups["varType"].Value;

                if(varName == "RaceID" && varType == "gclass21")
                    continue;

                if (matchVarEnum.Success)
                {
                    varType = "GEnum" + matchVarEnum.Groups["enum"].Value;
                }

                if (Regex.IsMatch(varline, "Name"))
                    varType = "string";

                if (Regex.IsMatch(varline, "aurora")) //many unobfuscated enums start with Aurora 
                    varType = Regex.Replace(varType, "aurora", "Aurora");
                
                if (varType == "") //if we can't figure out the type, we'll guess at an int. 
                    varType = "int";
                
                
                structString = structString.DoIndent();
                structString += "public " + varType + " " + varName + ";\n";
            }
        }

        indentIndex--;
        structString = structString.DoIndent("}\n");
        
        classstr += structString;
        if(parts.XinY is null)
            classstr = classstr.DoIndent(structName + " " + structArrayName + ";\n\n");
        else
            classstr = classstr.DoIndent(structName + "[] " + structArrayName + ";\n\n");

        
        return classstr;
    }

    static string BuildConstructor(string classstr, string input)
    {
        string methStr = DoIndent("", "public " + className + "(GClass0 game)\n");
        methStr = methStr.DoIndent("{\n");
        indentIndex++;

        if(TopLevelBlock.KeyVarLines.Length > 0)
        {
            methStr = BuildStructInit(methStr, TopLevelBlock);
            methStr = BuildForeachBody(methStr, TopLevelBlock);
        }
/*
        var nums = Regex.Match(input, @"\w+ num = 0.*;.*(?=\s+foreach)", RegexOptions.Singleline);
        if (nums.Success)
        {
            var val = nums.Value;
            var lines = Regex.Split(val, @"\n\t+");
            
            foreach (string line in lines)
            {
                methStr = DoIndent(methStr, line + "\n");
            }   
        }
        */
        
            

        
        methStr = methStr.DoIndent("int i = 0;\n");
        int i = 0;
        foreach (var block in ForeachBlocks)
        {
            methStr = BuildForeachStart(methStr, block, i);
            methStr = BuildForeachBody(methStr, block);
            i++;
        }
        
        indentIndex--;
        methStr = methStr.DoIndent("}\n");

        classstr += methStr;
        return classstr;
    }

    static string BuildStructInit(string methStr, BlockParts blockParts)
    {
        var structName = blockParts.StructName;
        var structArrayName = blockParts.StructArrayName;
        var collectionName = blockParts.CollectionName;
        string initStructArray = "";
        if(blockParts.XinY is null)
        {
            initStructArray = structArrayName + " = new " + structName + ";\n";
        }
        else
        {
            initStructArray = structArrayName + " = new " + structName + "[game." + collectionName + ".Count()];\n";
        }
        methStr = methStr.DoIndent(initStructArray);
        return methStr;
    }

    static string BuildXinYList(string methStr, BlockParts blockBlock, int num)
    {
        string badStr = "";
        if (Regex.IsMatch(blockBlock.XinY, @"SelectMany\(new Func"))
        {
            badStr = blockBlock.XinY;
        }
        else if (blockBlock.XinYList != "")
        {
            if (Regex.IsMatch(blockBlock.XinYList, @"SelectMany\(new Func"))
            {
                badStr = blockBlock.XinYList;
            }
        }

        if (badStr != "")
        {
            //we need to rebuild this whole line, since the decompiler can't handle it.
            //foreach (GClass142 gclass in this.dictionary_20.Values.SelectMany(new Func<GClass132, IEnumerable<GClass142>>(GClass0.<>c.<>9.method_157)).ToList<GClass142>())

            //this.PopulationList.Values.SelectMany<Population, PopulationInstallation>((Func<Population, IEnumerable<PopulationInstallation>>) (x => (IEnumerable<PopulationInstallation>) x.PopInstallations.Values)).ToList<PopulationInstallation>())
            //this = game
            //PopulationList.Values = [Collection1]
            //Population = [Type1]
            //PopulationInstallation = [Type2]
            //x.PopInstallations.Values is tricky since the decompiler looses this data. we have to find it manualy.
            // first we will create a seperate list for this
            string liststr = "var list"+num+" = game.";

            string collection1 = Regex.Match(badStr, @"this.(?<collection1>\w+_\d+)\.Values").Groups["collection1"].Value;
            var types = Regex.Match(badStr, @"new Func<(?<type1>\w+\d+), IEnumerable<(?<type2>\w+\d+)>>");
            string type1 = types.Groups["type1"].Value;
            string type2 = types.Groups["type2"].Value;
          
            liststr += collection1; //add the collectionName
            liststr += ".Values.SelectMany";
            liststr += "<" + type1 + ", " + type2 +">";
            liststr += "((Func<" + type1 + ", IEnumerable<" + type2 + ">>)";
            liststr += "(x => (IEnumerable<" + type2 + ">) ";
            liststr += "x.???????.???))";
            liststr += ".ToList<" + type2 + ">();";

            methStr = methStr.DoIndent(liststr + "\n");
            methStr = methStr.DoIndent("foreach(" + type2 + " gclass in list)\n");
            methStr = methStr.DoIndent("{\n");
            indentIndex++;
        }
        else
        {
            var foreachstr = Regex.Replace(blockBlock.XinY, "this", "game");
            methStr = methStr.DoIndent(foreachstr + "\n");
            methStr = methStr.DoIndent("{\n");
            indentIndex++;
        }

        return methStr;

    }

    /// <summary>
    /// create the foreach line. 
    /// </summary>
    /// <param name="methStr"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    static string BuildForeachStart(string methStr, BlockParts blockBlock, int num)
    {
        methStr = methStr.DoIndent("i = 0;\n");//reset i to 0
        string input = blockBlock.XinY;
        string foreachstr = Regex.Match(input, "foreach .*").Value;
        methStr = BuildXinYList(methStr, blockBlock, num);


        return methStr;
    }

    static string BuildForeachBody(string methStr, BlockParts blockParts)
    {
        var input = blockParts.Body;
        var structName = blockParts.StructName;
        var structArrayName = blockParts.StructArrayName;
        var nums = Regex.Match(input, @"(?<nums>\w+ num = 0;.*;\n\s+})\n(?=\s+sqlite)", RegexOptions.Singleline);
        if(nums.Success)
        {
            var val = nums.Groups["nums"].Value;
            var lines = Regex.Split(val, @"\n\t+");
            foreach (string line in lines)
            {
                if (Regex.IsMatch(line, @"\}"))
                    indentIndex--;
                methStr = DoIndent(methStr, line + "\n");
                if (Regex.IsMatch(line, @"\{"))
                    indentIndex++;

            }
        }

        nums = Regex.Match(input, @"\s+(?<nums>if.*})(?=\s+sqlite)", RegexOptions.Singleline);
        if(nums.Success)
        {
            var val = nums.Groups["nums"].Value;
            FixIndentingInBlock(ref methStr, val);
            /*
            var lines = Regex.Split(val, @"\n\t+");
            foreach (string line in lines)
            {
                if (Regex.IsMatch(line, @"\}"))
                    indentIndex--;
                methStr = DoIndent(methStr, line + "\n");
                if (Regex.IsMatch(line, @"\{"))
                    indentIndex++;

            }
            */
        }


        string newdataObj = "var " + "dataObj = new " + structName + "()\n";
        methStr = methStr.DoIndent(newdataObj);
        methStr = methStr.DoIndent("{\n");
        indentIndex++;
        
        string ptrn = @"\s*sqliteCommand.Parameters.AddWithValue.*;";
        var matchs = Regex.Matches(input, ptrn);

        
        foreach (Match match in matchs)
        {
            string varline = match.Value;
            var matchVarName = Regex.Match(varline, varNamePtn);
            var matchVarTypeName = Regex.Match(varline, varTypeNamePtn);
            var matchVarEnum = Regex.Match(varline, varEnumNamePtn);
            if (matchVarName.Success)
            {
                string varName = matchVarName.Groups["varName"].Value;
                if(varName == "GameID") //we skip storing gameID since we store it elsewhere
                    continue;
                string varSourceName = matchVarTypeName.Groups["varType"].Value;
                
                if (matchVarEnum.Success)
                {
                    varSourceName = "genum" + matchVarEnum.Groups["enum"].Value;
                }

                string varstr = Regex.Replace(varline, ".*\", ",varName + " = ");
                varstr = Regex.Replace(varstr, @"\n", "");
                varstr = Regex.Replace(varstr, @"\);", ",\n");
                //string varstr = varName + " = gclass." + varSourceName + ",\n";
                methStr = methStr.DoIndent(varstr);
                
            }
        }

        indentIndex--;
        methStr = methStr.DoIndent("};\n");

        if(blockParts.XinY is null)
            methStr = methStr.DoIndent(structArrayName + " = dataObj();\n");
        else
        {
            methStr = methStr.DoIndent(structArrayName + "[i] = dataObj;\n");
            methStr = methStr.DoIndent("i++;\n");
        }
        
        indentIndex--;
        methStr = methStr.DoIndent("}\n");

        return methStr;
    }

    static string BuildSaveToSqlMethod(string classstr, string input)
    {
        string methStr = DoIndent("", "public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID, int raceID)\n");
        methStr = methStr.DoIndent("{\n");
        indentIndex++;
        methStr = methStr.DoIndent("try\n");
        methStr = methStr.DoIndent("{\n");
        indentIndex++;
        var matches = Regex.Matches(input, "(?<!sqliteCommand = )new SQLiteCommand.*");
        foreach (Match cmdMatch in matches)
        {
            var cmdStr = Regex.Replace(cmdMatch.Value, "this.int_57", "gameID");
            methStr = methStr.DoIndent(cmdStr+"\n");
        }

        var str = Regex.Match(input, @"using \(SQLiteCommand.*").Value;
        methStr = methStr.DoIndent(str+"\n");
        methStr = methStr.DoIndent("{\n");
        indentIndex++;

        //if(TopLevelBlock.KeyVarLines.Length > 0)
        
        /*
            var structName = TopLevelBlock.StructName;
            var structArrayName = structName + "Data";
            bool foreachbool = Regex.Match(input, "foreach .*").Success;
            //foreachstr = Regex.Replace(foreachstr, "this", "game");
            if (foreachbool)
            {
                methStr = methStr.DoIndent("foreach(var " + structName + " in " + structArrayName + ")\n");
                methStr = methStr.DoIndent("{\n");
                indentIndex++;
            }
        */
        if(TopLevelBlock.KeyVarLines.Length > 0)
            methStr = BuildSaveToSQLForeachBlock(methStr, TopLevelBlock);
        
        foreach (var block in ForeachBlocks)
        {
            methStr = BuildSaveToSQLForeachBlock(methStr, block);
        }
        
        
        indentIndex--;
        methStr = methStr.DoIndent("}\n");
        
        indentIndex--;
        methStr = methStr.DoIndent("}\n");

        //indentIndex--;
        //methStr = methStr.DoIndent("}\n");
        //catches
        var match = Regex.Match(input, @"(catch).*(\);\n\s+})", RegexOptions.Singleline);
        FixIndentingInBlock(ref methStr, match.Value);

        indentIndex--;
        methStr = methStr.DoIndent("}\n");

        classstr += methStr;
        return classstr;
    }

    static string BuildSaveToSQLForeachBlock(string methStr, BlockParts block)
    {
        var structName = block.StructName;
        var datastore = block.StructArrayName;

        string foreachline = "foreach (var dataObj in " + datastore + " )\n";
        methStr = methStr.DoIndent(foreachline);
        methStr = methStr.DoIndent("{\n");
        indentIndex++;
        
        
        foreach (var line in block.TopInnerSQLLines)
        {
            methStr = methStr.DoIndent(line + "\n");
        }
        
        foreach (string varline in block.KeyVarLines)
        {
            var matchVarName = Regex.Match(varline, varNamePtn);
            var matchVarType = Regex.Match(varline, varTypePtn);

            if (matchVarName.Success)
            {
                string lnpatern = @"(?<line>sqliteCommand.Parameters.AddWithValue\(\""@\w+.*""), ";
                var rmatch = Regex.Match(varline, lnpatern);
                string varstr = rmatch.Groups["line"].Value;
                string varName = matchVarName.Groups["varName"].Value;
                string varType = matchVarType.Groups["varType"].Value;
                
                if(varName == "GameID")
                {
                    if(Regex.IsMatch(varline, "this.int_57"))
                    {
                        varstr += ", gameID);\n";
                    }
                }
                else if(varName == "RaceID" && varType == "gclass21")
                {
                    string patern = "gclass.gclass21_0.RaceID";
                    //Regex.Match(varline, patern);
                    if(Regex.IsMatch(varline, patern))
                    {
                        varstr += ", raceID);\n";
                    }
                }
                else
                {
                    varstr += ", dataObj." + varName + ");\n";
                }
                methStr = methStr.DoIndent(varstr);
            }
        }
        foreach (var line in block.BottomSLQLines)
        {
            if(Regex.Match(line,"sqliteCommand.ExecuteNonQuery").Success)
                methStr = methStr.DoIndent("sqliteCommand.ExecuteNonQuery();\n");
            
        }
        indentIndex--;
        methStr = methStr.DoIndent("}\n");

        return methStr;
    }

    static string DoIndent(this string input, string str = "")
    {
        for (int i = 0; i < indentIndex; i++)
        {
            input += indent;
        }

        return input + str;
    }

    static void FixIndentingInBlock(ref string str, string input)
    {
        
        var lines = Regex.Split(input, @"\n\t+");
        foreach (string line in lines)
        {
            if (Regex.IsMatch(line, @"\}"))
                indentIndex--;
            str = DoIndent(str, line + "\n");
            if (Regex.IsMatch(line, @"\{"))
                indentIndex++;

        }
    }

    static string MatchingBrace(string input, int matchIndex = 0)
    {
        string returnstring = "";
        int depth = 0;
        if (matchIndex == -1)
        {
            depth = 1;
            matchIndex = 0;
        }

        for (int i = matchIndex; i < input.Length; i++)
        {
            var charAtIndex_i = input[i];
            if (charAtIndex_i == '{')
                depth++;
            else if (charAtIndex_i == '}')
                depth--;

            if (depth == 0)
                return input.Substring(matchIndex, i+1);


        }

        return returnstring;
    }

}
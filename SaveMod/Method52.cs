public void method_52()
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();
    try
    {
        Cursor.Current = Cursors.WaitCursor;
        using (SQLiteConnection sqliteConnection = new SQLiteConnection(GClass202.string_1))
        {
            sqliteConnection.Open();
            using (SQLiteTransaction sqliteTransaction = sqliteConnection.BeginTransaction())
            {
                this.method_53();
                this.method_54(sqliteConnection);
                this.method_55(sqliteConnection);
                this.method_56(sqliteConnection);
                this.method_65(sqliteConnection);
                this.method_66(sqliteConnection);
                this.method_67(sqliteConnection);
                this.method_72(sqliteConnection);
                this.method_73(sqliteConnection);
                this.method_74(sqliteConnection);
                this.method_128(sqliteConnection);
                this.method_75(sqliteConnection);
                this.method_76(sqliteConnection);
                this.method_77(sqliteConnection);
                this.method_68(sqliteConnection);
                this.method_78(sqliteConnection);
                this.method_80(sqliteConnection);
                this.method_79(sqliteConnection);
                this.method_81(sqliteConnection);
                this.method_82(sqliteConnection);
                this.method_93(sqliteConnection);
                this.method_84(sqliteConnection);
                this.method_85(sqliteConnection);
                this.method_86(sqliteConnection);
                this.method_87(sqliteConnection);
                this.method_88(sqliteConnection);
                this.method_89(sqliteConnection);
                this.method_90(sqliteConnection);
                this.method_91(sqliteConnection);
                this.method_92(sqliteConnection);
                this.method_94(sqliteConnection);
                this.method_95(sqliteConnection);
                this.method_69(sqliteConnection);
                this.method_96(sqliteConnection);
                this.method_97(sqliteConnection);
                this.method_98(sqliteConnection);
                this.method_99(sqliteConnection);
                this.method_100(sqliteConnection);
                this.method_101(sqliteConnection);
                this.method_103(sqliteConnection);
                this.method_104(sqliteConnection);
                this.method_105(sqliteConnection);
                this.method_106(sqliteConnection);
                this.method_107(sqliteConnection);
                this.method_108(sqliteConnection);
                this.method_109(sqliteConnection);
                this.method_110(sqliteConnection);
                this.method_83(sqliteConnection);
                this.method_111(sqliteConnection);
                this.method_112(sqliteConnection);
                this.method_113(sqliteConnection);
                this.method_114(sqliteConnection);
                this.method_115(sqliteConnection);
                this.method_116(sqliteConnection);
                this.method_121(sqliteConnection);
                this.method_117(sqliteConnection);
                this.method_118(sqliteConnection);
                this.method_119(sqliteConnection);
                this.method_120(sqliteConnection);
                this.method_122(sqliteConnection);
                this.method_123(sqliteConnection);
                this.method_124(sqliteConnection);
                this.method_125(sqliteConnection);
                this.method_126(sqliteConnection);
                this.method_127(sqliteConnection);
                this.method_130(sqliteConnection);
                this.method_131(sqliteConnection);
                this.method_132(sqliteConnection);
                this.method_129(sqliteConnection);
                this.method_64(sqliteConnection);
                this.method_70(sqliteConnection);
                this.method_63(sqliteConnection);
                this.method_62(sqliteConnection);
                this.method_61(sqliteConnection);
                this.method_60(sqliteConnection);
                this.method_59(sqliteConnection);
                this.method_71(sqliteConnection);
                this.method_102(sqliteConnection);
                this.method_57(sqliteConnection);
                this.method_58(sqliteConnection);
                sqliteTransaction.Commit();
            }
            sqliteConnection.Close();
        }
        Cursor.Current = Cursors.Default;
    }
    catch (OleDbException oleDbException_)
    {
        GClass202.smethod_72(oleDbException_, 1425);
    }
    catch (Exception exception_)
    {
        GClass202.smethod_68(exception_, 1426);
    }

    sw.Stop();
    string text = "SaveTime = " + sw.Elapsed.TotalSeconds;
    GClass21 gclass = this.gclass21_0;
    this.gclass85_0.method_2(GEnum123.const_0, text, gclass, null, 0.0, 0.0, AuroraEventCategory.All);
    SaveData save = SaveGameMethods.SaveToMemory(this);
    SaveGameMethods.SaveToSQLDatabase(this, save);
}
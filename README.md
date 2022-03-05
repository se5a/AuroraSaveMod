# AuroraSaveMod
Mod *Attempts* to vastly improves save times by threading the IO portion of the save. (currently not finished, not fully working, don't use unless you're wanting to develop this mod) 

Note: this will likely *never* be an "approved" mod, since it's saving to the database and potntialy creating issues that are hard to find. if you use this do not report bugs to steve. 

## What it does:  
- Saves the game normaly (this is to allow us to compare the two save methods while developing).
- Saves the game to in memory structs. 
- Saves the in memory structs to the on disk SQL database in another thread using Task.Factory.StartNew
    during this time the ui is responsive and the game is playable by the player. 
- Posts SaveTimes to the EventLog.
- Uses some fancy thread marshaling to allow the thread doing the SQL task to update the log without locking. 
    this decision was made to allow modifying origional game functions and methods to a minimum. 
    Currently the only method modified is Game.SaveCurrentGame() (Gclass0.method_52)
    the rest is Added classes. 

![alt text](https://github.com/se5a/AuroraSaveMod/blob/main/SaveMod/savetimes.PNG?raw=true)

Included is the modded AuroraSaveMod.exe, you'll still need to copy AuroraDB.db to AuroraQSDB.db 

but if you want to do it the long way yourself:
- Open Aurora.exe in dnspy
- in the Assembly Explorer right click Aurora and select "Add Class"
- in the Add Class dialog box, down the bottom left click the green C# icon ("Add Source Code")
- Select all three source files "SaveData.cs", "SaveData2.cs" "SaveData3.cs"
(nothing will show in the window, but if you click the dropdown which defailts to main.cs, you'll see the three files)
- Click Compile.
- Expand Aurora -> Aurora.exe -> {} - 
find and select "GClass0" (this is the obfusicated "Game" class)
- find and right click "method_52()" and click "edit method" (don't edit class here, it's way too big).
- Edit the method to be the same as the provided method_52 (you can remove the stopwatch and event log stuff, or make it so it only calls the below two functions:
SaveData save = SaveGameMethods.SaveToMemory(this);
SaveGameMethods.SaveToSQLDatabase(this, save);
- In dnspy click File -> Save All dialog change the file name to "AuroraSaveMod.exe" or whatever, maybe don't overwrite your deobfusicated one incase you mess up.
- *Copy* your AuroraDB.db and name the copy "AuroraQSDB.db" (save mod needs a copy of the database to save into) 
- You can then close dnSpy and run AuroraSaveMod.exe as you would normal aurora. 
- or you can debug while in dnSpy. if doing the later, ensure you've opened the version you saved in the previous step (dnSpy is a bit dumb and still runs the origional) maybe right click the origional aurora.exe and click "Remove" then add the new one in so you don't get confused. 

## Issues:
AuroraQSDB.db is larger than the "normal" save and also has problems loading. I've messed something up with the sql bit. 
Still need to implement "Save every ingame %timespan%" or whatever. 


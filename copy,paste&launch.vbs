Const SourceFile = "C:\Users\..."
Const DestinationFile = "C:\Users\..."
Const GamePath = "steam://rungameid/XXXXXX"
Const LogFolder = "\Log\"
Const DoLog = false

Set fso = CreateObject("Scripting.FileSystemObject")

if DoLog then
	if not fso.FolderExists(LogFolder) then
		FSO.CreateFolder(LogFolder)
	end if

	if fso.FileExists(LogFolder+"log.txt") then
		set objLog = fso.OpenTextFile(LogFolder+"log.txt", 8, true, 0)
	else
		set objLog = fso.CreateTextFile(LogFolder+"log.txt",True)
	end	if
end if

if DoLog then objLog.WriteLine "" end if
if DoLog then objLog.WriteLine FormatDateTime(Date,1) end if

fso.CopyFile SourceFile, DestinationFile, True
if DoLog then objLog.WriteLine FormatDateTime(Time,3) + ": Copy file" + SourceFile + " to " + DestinationFile end if

Set oShell = CreateObject ("Wscript.Shell") 
oShell.Run GamePath
if DoLog then 
	objLog.WriteLine FormatDateTime(Time,3) + ": Launch" + GamePath 
	objLog.close
end if
#Requires AutoHotkey v2.0
#SingleInstance Force

; Check if a command-line argument was provided
if (A_Args.Length < 1) {
    MsgBox "Error: No application path provided."
    ExitApp
}

; Get the full path from the first command-line argument
appPath := A_Args[1]

; Extract the executable name from the path
exeName := ""
loop Parse, appPath, "\" {
    exeName := A_LoopField
}

wins := WinGetList("ahk_exe " exeName)

if (wins.Length = 0) {
    Run appPath
    ExitApp 1
}

if WinActive("ahk_exe" exeName) {
    WinMinimize "A"
    ExitApp 2
}

if WinExist("ahk_exe" exeName) {
    WinActivate 
    ExitApp 3
}

ExitApp 0

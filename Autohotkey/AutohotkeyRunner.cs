using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SuchByte.MacroDeck.Logging;
using SuchByte.WindowsUtils;
using System.Reflection;

public enum ToggleAppResult
{
    // Output by AHK
    NothingHappened = 0,
    ApplicationStarted = 1,
    // Output by AHK
    ApplicationMinimized = 2,
    // Output by AHK
    ApplicationFocused = 3,
    // Either AHK or script not found or failed to start or other error
    Error = -1
}

public class AutoHotkeyRunner
{
    public static ToggleAppResult ToggleApp(string targetExePath)
    {
        string pluginDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string ahkScriptPath = Path.Combine(pluginDirectory, "Autohotkey\\ToggleApp.ahk");

        return RunAhkScript(ahkScriptPath, targetExePath);
    }

    private static ToggleAppResult RunAhkScript(string ahkScriptPath, string targetExePath, string ahkExePath = "C:\\Program Files\\AutoHotkey\\v2\\AutoHotkey.exe")
    {
        if (!File.Exists(ahkExePath))
        {
            MacroDeckLogger.Error(PluginInstance.Main, $"AutoHotkey executable not found. Please ensure it is installed.");
            return ToggleAppResult.Error;
        }

        if (!File.Exists(ahkScriptPath))
        {
            MacroDeckLogger.Error(PluginInstance.Main, $"AHK script not found at {ahkScriptPath}");
            return ToggleAppResult.Error;
        }

        string arguments = $"\"{ahkScriptPath}\" \"{targetExePath}\"";
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = ahkExePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processStartInfo))
            {
                process.WaitForExit();
                return (ToggleAppResult)process.ExitCode;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to start AHK script: {ex.Message}");
            MacroDeckLogger.Error(PluginInstance.Main, $"Failed to start AHK script: {ex.Message}");
            return ToggleAppResult.Error;
        }
    }
}

using SuchByte.MacroDeck.Logging;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Shlop.WindowsUtilsRevamped.Actions.StartApplication;


public class ApplicationLauncher
{
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(nint hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(nint hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool IsIconic(nint hWnd);

    [DllImport("kernel32.dll")]
    public static extern nint OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("psapi.dll")]
    static extern uint GetModuleFileNameEx(nint hProcess, nint hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseHandle(nint hObject);

    [DllImport("user32.dll")]
    private static extern nint GetForegroundWindow();



    private const int SW_MINIMIZE = 0;
    private const int SW_RESTORE = 9;


    public static void StartApplication(string path, string arguments, bool asAdmin)
    {
        var p = new Process
        {
            StartInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path),
                Arguments = arguments,
                Verb = asAdmin ? "runas" : ""
            }
        };
        p.Start();
    }

    public static bool IsRunning(string path)
    {
        path = WindowsShortcut.GetShortcutTarget(path);
        bool isRunning = GetProcessByPath(path) != null;
        return isRunning;
    }

    public static bool IsFocused(string path)
    {
        var process = GetProcessByPath(path);
        if (process == null)
        {
            return false;
        }

        nint targetHwnd = process.MainWindowHandle;
        if (targetHwnd == nint.Zero)
        {
            return false;
        }

        nint foregroundHwnd = GetForegroundWindow();
        return targetHwnd == foregroundHwnd;
    }

    public static void KillApplication(string path)
    {
        path = WindowsShortcut.GetShortcutTarget(path);
        if (!IsRunning(path)) return;
        var p = GetProcessByPath(path);
        if (p == null) return;
        Process.GetProcessesByName(p.ProcessName).ToList().ForEach(p =>
        {
            MacroDeckLogger.Trace(Main.Instance, $"Killing process: {p.ProcessName} PID: {p.Id}");
            p.Kill();
        });
    }

    public static void BringToForeground(string path)
    {
        path = WindowsShortcut.GetShortcutTarget(path);
        if (!IsRunning(path)) return;
        var p = GetProcessByPath(path);
        if (p == null) return;

        nint handle = p.MainWindowHandle;

        ShowWindow(handle, 5);
        ShowWindow(handle, 10);

        if (!IsIconic(handle))
        {
            return;
        }
        MinimizeAndRestoreWindow(handle); // Fallback function
    }

    public static Process GetProcessByPath(string path)
    {
        path = WindowsShortcut.GetShortcutTarget(path);
        return Process.GetProcesses().ToArray().Where(p => GetProcessFileName(p.Id).Equals(path, StringComparison.OrdinalIgnoreCase)).OrderByDescending(p => p.Id).FirstOrDefault();
    }


    private static void MinimizeAndRestoreWindow(nint hWnd)
    {
        ShowWindow(hWnd, SW_MINIMIZE);
        ShowWindow(hWnd, SW_RESTORE);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetProcessFileName(int pid)
    {
        var processHandle = OpenProcess(0x0400 | 0x0010, false, pid);

        if (processHandle == nint.Zero)
        {
            return "";
        }

        const int lengthSb = 4000;

        var sb = new StringBuilder(lengthSb);

        string result = null;

        if (GetModuleFileNameEx(processHandle, nint.Zero, sb, lengthSb) > 0)
        {
            result = sb.ToString();
        }

        CloseHandle(processHandle);

        return result;
    }
}

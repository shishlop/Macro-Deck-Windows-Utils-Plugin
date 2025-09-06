using SuchByte.MacroDeck.Logging;
using System.Runtime.InteropServices;
using System.Text;
using Shlop.WindowsUtilsRevamped;

// Monitors window focus changes and raises an event when the focused window changes (globally)
// Example: button states which depend on the currently focused application
public class WindowFocusMonitor
{
    // Delegate to define the signature of the hook callback function
    private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    // Event type for foreground window changes
    private const uint EVENT_SYSTEM_FOREGROUND = 0x0003;
    private const uint WINEVENT_OUTOFCONTEXT = 0x0000;

    private static IntPtr _winEventHook;
    private static WinEventDelegate _winEventDelegate;
    private static Thread _monitorThread;

    public static event Action<string, uint> WindowFocusChanged;

    public static void StartMonitoring()
    {
        // Check if the monitor thread is already running.
        if (_monitorThread != null && _monitorThread.IsAlive)
        {
            MacroDeckLogger.Info(Main.Instance, $"Global window focus changes listener was already started earlier. Hook: {_winEventHook}");
            return;
        }

        _monitorThread = new Thread(MonitorThreadRun);
        _monitorThread.IsBackground = true;
        _monitorThread.SetApartmentState(ApartmentState.STA); // Essential for UI-related APIs
        _monitorThread.Start();

        MacroDeckLogger.Info(Main.Instance, "Started monitoring global window focus changes on a new thread.");
    }

    public static void StopMonitoring()
    {
        if (_monitorThread != null && _monitorThread.IsAlive)
        {
            // You can implement a clean shutdown signal here if needed.
            UnhookWinEvent(_winEventHook);
            _winEventHook = IntPtr.Zero;
            MacroDeckLogger.Info(Main.Instance, "Stopped monitoring global window focus changes.");
        }
    }

    private static void MonitorThreadRun()
    {
        // The delegate must be stored in a static field to prevent garbage collection.
        _winEventDelegate = new WinEventDelegate(WinEventProc);

        _winEventHook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _winEventDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);

        if (_winEventHook == IntPtr.Zero)
        {
            MacroDeckLogger.Error(Main.Instance, $"Failed to start monitoring global window focus changes. Hook: {_winEventHook}");
            return;
        }

        MacroDeckLogger.Info(Main.Instance, $"Monitoring started on a dedicated thread. Hook: {_winEventHook}");

        // This is the message loop required for the hook to receive events.
        Application.Run();

        // This code runs when the message loop exits.
        UnhookWinEvent(_winEventHook);
        _winEventHook = IntPtr.Zero;
    }

    private static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
    {
        if (eventType == EVENT_SYSTEM_FOREGROUND)
        {
            StringBuilder sb = new StringBuilder(256);
            GetWindowText(hwnd, sb, 256);
            string windowTitle = sb.ToString();

            uint processId;
            GetWindowThreadProcessId(hwnd, out processId);

            WindowFocusChanged?.Invoke(windowTitle, processId);
        }
    }
}
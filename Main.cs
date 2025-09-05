using SuchByte.MacroDeck.Plugins;
using SuchByte.WindowsUtils.Actions;
using SuchByte.WindowsUtils.Language;
using WindowsInput;

namespace SuchByte.WindowsUtils;

public static class PluginInstance
{
    public static Main Main;
}

public class Main : MacroDeckPlugin
{
    public static Main Instance;

    public InputSimulator InputSimulator = new();

    public Main()
    {
        Instance = this;
        PluginInstance.Main = this;
    }

    public override void Enable()
    {
        PluginLanguageManager.Initialize();
        this.Actions = new List<PluginAction>
        {
            new WriteTextAction(),
            new CommandlineAction(),
            new OpenFileAction(),
            new OpenFolderAction(),
            new StartApplicationAction(),
            new IncreaseVolumeAction(),
            new DecreaseVolumeAction(),
            new MuteVolumeAction(),
            new WindowsExplorerControlAction(),
            new HotkeyAction(),
        };

        // note(shlop): Start monitoring for window focus changes globally.
        // This should only be called once when the plugin loads.
        // Ideally MacroDeckPlugin should define OnLoad and OnUnload methods for such initialization and cleanup tasks.
        // Right now, I just use a boolean flag in WindowFocusMonitor to prevent multiple initializations.
        WindowFocusMonitor.StartMonitoring();
    }
}

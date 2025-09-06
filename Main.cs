using Shlop.WindowsUtilsRevamped.Language;
using SuchByte.MacroDeck.Plugins;
using Shlop.WindowsUtilsRevamped.Actions.CommandLine;
using Shlop.WindowsUtilsRevamped.Actions.DecreaseVolume;
using Shlop.WindowsUtilsRevamped.Actions.Hotkey;
using Shlop.WindowsUtilsRevamped.Actions.IncreaseVolume;
//note(shlop): idk why multihotkey is not used
//using Shlop.WindowsUtilsRevamped.Actions.MultiHotkey;
using Shlop.WindowsUtilsRevamped.Actions.MuteVolume;
using Shlop.WindowsUtilsRevamped.Actions.OpenFileFolder;
using Shlop.WindowsUtilsRevamped.Actions.StartApplication;
using Shlop.WindowsUtilsRevamped.Actions.WindowsExplorerControl;
using Shlop.WindowsUtilsRevamped.Actions.WriteText;
using WindowsInput;

namespace Shlop.WindowsUtilsRevamped;

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

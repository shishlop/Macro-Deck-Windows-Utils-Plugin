using Shlop.WindowsUtilsRevamped.Language;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using WindowsInput;

namespace Shlop.WindowsUtilsRevamped.Actions.IncreaseVolume;

public class IncreaseVolumeAction : PluginAction
{
    public override string Name => PluginLanguageManager.PluginStrings.ActionIncreaseVolume;

    public override string Description => PluginLanguageManager.PluginStrings.ActionIncreaseVolumeDescription;

    public override void Trigger(string clientId, ActionButton actionButton)
    {
        PluginInstance.Main.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
    }
}

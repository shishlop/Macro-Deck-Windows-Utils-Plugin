using Shlop.WindowsUtilsRevamped.Language;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using WindowsInput;

namespace Shlop.WindowsUtilsRevamped.Actions.MuteVolume;

public class MuteVolumeAction : PluginAction
{
    public override string Name => PluginLanguageManager.PluginStrings.ActionMuteVolume;

    public override string Description => PluginLanguageManager.PluginStrings.ActionMuteVolumeDescription;

    public override void Trigger(string clientId, ActionButton actionButton)
    {
        PluginInstance.Main.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_MUTE);
    }
}

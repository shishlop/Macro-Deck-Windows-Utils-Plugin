using Newtonsoft.Json.Linq;
using Shlop.WindowsUtilsRevamped.Language;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System.Diagnostics;

namespace Shlop.WindowsUtilsRevamped.Actions.OpenFileFolder;

public class OpenFileAction : PluginAction
{
    public override string Name => PluginLanguageManager.PluginStrings.ActionOpenFile;

    public override string Description => PluginLanguageManager.PluginStrings.ActionOpenFile;

    public override bool CanConfigure => true;

    public override void Trigger(string clientId, ActionButton actionButton)
    {
        if (!string.IsNullOrWhiteSpace(Configuration))
        {
            try
            {
                JObject configurationObject = JObject.Parse(Configuration);
                var path = configurationObject["path"].ToString();

                var p = new Process
                {
                    StartInfo = new ProcessStartInfo(path)
                    {
                        UseShellExecute = true,
                    }
                };
                p.Start();
            }
            catch { }
        }
    }

    public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
    {
        return new FileFolderSelector(this, actionConfigurator, SelectType.FILE);
    }
}

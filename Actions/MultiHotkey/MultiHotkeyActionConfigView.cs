using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

namespace Shlop.WindowsUtilsRevamped.Actions.MultiHotkey;

public partial class MultiHotkeyActionConfigView : ActionConfigControl
{
    private readonly MultiHotkeyActionConfigViewModel _viewModel;

    public MultiHotkeyActionConfigView(PluginAction action)
    {
        InitializeComponent();
        this._viewModel = new MultiHotkeyActionConfigViewModel(action);
    }

    private void MultiHotkeyActionConfigView_Load(object sender, EventArgs e)
    {

    }

    public override bool OnActionSave()
    {
        return this._viewModel.SaveConfig();
    }
}

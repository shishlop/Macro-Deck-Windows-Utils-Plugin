using SuchByte.MacroDeck.GUI.CustomControls;

namespace Shlop.WindowsUtilsRevamped.Actions.MultiHotkey;

partial class MultiHotkeyActionConfigView
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // MultiHotkeyActionConfigView
        // 
        this.Name = "MultiHotkeyActionConfigView";
        this.Load += new System.EventHandler(this.MultiHotkeyActionConfigView_Load);
        this.ResumeLayout(false);

    }
}

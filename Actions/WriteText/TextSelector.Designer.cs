using SuchByte.MacroDeck.GUI.CustomControls;

namespace Shlop.WindowsUtilsRevamped.Actions.WriteText;

partial class TextSelector
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

    private RoundedTextBox textBox;
    private ButtonPrimary btnAddVariable;
    private System.Windows.Forms.ContextMenuStrip variablesContextMenu;
}

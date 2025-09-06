using SuchByte.MacroDeck.GUI.CustomControls;

namespace Shlop.WindowsUtilsRevamped.Actions.OpenFileFolder;

partial class FileFolderSelector
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

    private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary btnBrowse;
    private System.Windows.Forms.Label lblPath;
    private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox path;
    private System.Windows.Forms.Label lblChoose;
}

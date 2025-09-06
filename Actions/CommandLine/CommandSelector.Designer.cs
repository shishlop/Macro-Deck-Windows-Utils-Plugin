
using SuchByte.MacroDeck.GUI.CustomControls;

namespace Shlop.WindowsUtilsRevamped.Actions.CommandLine;

partial class CommandSelector
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

    private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox workingDirectory;
    private System.Windows.Forms.Label lblWorkingDirectory;
    private System.Windows.Forms.Label lblCommand;
    private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox command;
    private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary btnBrowse;
    private System.Windows.Forms.CheckBox checkSaveVariable;
    private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox variableName;
    private RoundedComboBox variableType;
}

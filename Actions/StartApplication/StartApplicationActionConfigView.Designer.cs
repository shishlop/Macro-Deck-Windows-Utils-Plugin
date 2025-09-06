
using SuchByte.MacroDeck.GUI.CustomControls;

namespace Shlop.WindowsUtilsRevamped.Actions.StartApplication;

partial class StartApplicationActionConfigView
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
        path = new RoundedTextBox();
        lblPath = new Label();
        btnBrowse = new ButtonPrimary();
        lblArguments = new Label();
        arguments = new RoundedTextBox();
        checkRunAsAdmin = new CheckBox();
        label1 = new Label();
        method = new RoundedComboBox();
        checkSyncButtonState = new CheckBox();
        deriveAppIcon = new CheckBox();
        SuspendLayout();
        // 
        // path
        // 
        path.BackColor = Color.FromArgb(65, 65, 65);
        path.Cursor = Cursors.Hand;
        path.Font = new Font("Tahoma", 11.25F);
        path.Location = new Point(216, 145);
        path.Name = "path";
        path.Padding = new Padding(8, 5, 8, 5);
        path.Size = new Size(486, 29);
        path.TabIndex = 1;
        path.TabStop = false;
        // 
        // lblPath
        // 
        lblPath.Font = new Font("Tahoma", 11.25F);
        lblPath.Location = new Point(97, 145);
        lblPath.Name = "lblPath";
        lblPath.Size = new Size(83, 29);
        lblPath.TabIndex = 1;
        lblPath.Text = "Path:";
        lblPath.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnBrowse
        // 
        btnBrowse.BorderRadius = 8;
        btnBrowse.Cursor = Cursors.Hand;
        btnBrowse.FlatStyle = FlatStyle.Flat;
        btnBrowse.Font = new Font("Tahoma", 9.75F);
        btnBrowse.ForeColor = Color.White;
        btnBrowse.Location = new Point(708, 145);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Progress = 0;
        btnBrowse.Size = new Size(38, 29);
        btnBrowse.TabIndex = 2;
        btnBrowse.Text = "...";
        btnBrowse.UseVisualStyleBackColor = true;
        btnBrowse.Click += BtnBrowse_Click;
        // 
        // lblArguments
        // 
        lblArguments.Font = new Font("Tahoma", 11.25F);
        lblArguments.Location = new Point(97, 180);
        lblArguments.Name = "lblArguments";
        lblArguments.Size = new Size(113, 29);
        lblArguments.TabIndex = 3;
        lblArguments.Text = "Arguments:";
        lblArguments.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // arguments
        // 
        arguments.BackColor = Color.FromArgb(65, 65, 65);
        arguments.Cursor = Cursors.Hand;
        arguments.Font = new Font("Tahoma", 11.25F);
        arguments.Location = new Point(216, 180);
        arguments.Name = "arguments";
        arguments.Padding = new Padding(8, 5, 8, 5);
        arguments.Size = new Size(486, 29);
        arguments.TabIndex = 4;
        // 
        // checkRunAsAdmin
        // 
        checkRunAsAdmin.Font = new Font("Tahoma", 11.25F);
        checkRunAsAdmin.Location = new Point(497, 215);
        checkRunAsAdmin.Name = "checkRunAsAdmin";
        checkRunAsAdmin.Size = new Size(249, 29);
        checkRunAsAdmin.TabIndex = 8;
        checkRunAsAdmin.Text = "As Administrator";
        checkRunAsAdmin.UseVisualStyleBackColor = true;
        // 
        // label1
        // 
        label1.Font = new Font("Tahoma", 11.25F);
        label1.Location = new Point(97, 215);
        label1.Name = "label1";
        label1.Size = new Size(113, 29);
        label1.TabIndex = 9;
        label1.Text = "Method:";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // method
        // 
        method.BackColor = Color.FromArgb(65, 65, 65);
        method.Cursor = Cursors.Hand;
        method.Font = new Font("Tahoma", 9.75F);
        method.Location = new Point(216, 214);
        method.Name = "method";
        method.Padding = new Padding(8, 2, 8, 2);
        method.Size = new Size(159, 28);
        method.TabIndex = 10;
        // 
        // checkSyncButtonState
        // 
        checkSyncButtonState.Font = new Font("Tahoma", 11.25F);
        checkSyncButtonState.Location = new Point(97, 250);
        checkSyncButtonState.Name = "checkSyncButtonState";
        checkSyncButtonState.Size = new Size(249, 29);
        checkSyncButtonState.TabIndex = 11;
        checkSyncButtonState.Text = "Sync button state";
        checkSyncButtonState.UseVisualStyleBackColor = true;
        // 
        // deriveAppIcon
        // 
        deriveAppIcon.Font = new Font("Tahoma", 11.25F);
        deriveAppIcon.Location = new Point(97, 275);
        deriveAppIcon.Name = "deriveAppIcon";
        deriveAppIcon.Size = new Size(249, 29);
        deriveAppIcon.TabIndex = 11;
        deriveAppIcon.Text = "Derive app icon";
        deriveAppIcon.UseVisualStyleBackColor = true;
        // 
        // StartApplicationActionConfigView
        // 
        AutoScaleDimensions = new SizeF(10F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(lblPath);
        Controls.Add(path);
        Controls.Add(btnBrowse);
        Controls.Add(checkSyncButtonState);
        Controls.Add(deriveAppIcon);
        Controls.Add(lblArguments);
        Controls.Add(method);
        Controls.Add(arguments);
        Controls.Add(label1);
        Controls.Add(checkRunAsAdmin);
        Name = "StartApplicationActionConfigView";
        Load += StartApplicationActionConfigView_Load;
        ResumeLayout(false);

    }

    private RoundedTextBox path;
    private System.Windows.Forms.Label lblPath;
    private ButtonPrimary btnBrowse;
    private System.Windows.Forms.Label lblArguments;
    private RoundedTextBox arguments;
    private System.Windows.Forms.CheckBox checkRunAsAdmin;
    private System.Windows.Forms.Label label1;
    private RoundedComboBox method;
    private System.Windows.Forms.CheckBox checkSyncButtonState;
    private System.Windows.Forms.CheckBox deriveAppIcon;
}

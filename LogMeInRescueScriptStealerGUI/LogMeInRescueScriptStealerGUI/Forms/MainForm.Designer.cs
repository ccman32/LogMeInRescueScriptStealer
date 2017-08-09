namespace LogMeInRescueScriptStealerGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.installationStatusLabel = new System.Windows.Forms.Label();
            this.installUninstallButton = new System.Windows.Forms.Button();
            this.currentInstallationStatusLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.outputPathTextBox = new System.Windows.Forms.TextBox();
            this.outputPathLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // installationStatusLabel
            // 
            this.installationStatusLabel.AutoSize = true;
            this.installationStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installationStatusLabel.Location = new System.Drawing.Point(12, 9);
            this.installationStatusLabel.Name = "installationStatusLabel";
            this.installationStatusLabel.Size = new System.Drawing.Size(156, 24);
            this.installationStatusLabel.TabIndex = 6;
            this.installationStatusLabel.Text = "Installation Status:";
            // 
            // installUninstallButton
            // 
            this.installUninstallButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installUninstallButton.Location = new System.Drawing.Point(16, 84);
            this.installUninstallButton.Name = "installUninstallButton";
            this.installUninstallButton.Size = new System.Drawing.Size(288, 32);
            this.installUninstallButton.TabIndex = 8;
            this.installUninstallButton.UseVisualStyleBackColor = true;
            this.installUninstallButton.Click += new System.EventHandler(this.installUninstallButton_Click);
            // 
            // currentInstallationStatusLabel
            // 
            this.currentInstallationStatusLabel.AutoSize = true;
            this.currentInstallationStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentInstallationStatusLabel.Location = new System.Drawing.Point(168, 9);
            this.currentInstallationStatusLabel.Name = "currentInstallationStatusLabel";
            this.currentInstallationStatusLabel.Size = new System.Drawing.Size(0, 24);
            this.currentInstallationStatusLabel.TabIndex = 7;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(310, 56);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 9;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // outputPathTextBox
            // 
            this.outputPathTextBox.Location = new System.Drawing.Point(16, 58);
            this.outputPathTextBox.Name = "outputPathTextBox";
            this.outputPathTextBox.ReadOnly = true;
            this.outputPathTextBox.Size = new System.Drawing.Size(288, 20);
            this.outputPathTextBox.TabIndex = 10;
            // 
            // outputPathLabel
            // 
            this.outputPathLabel.AutoSize = true;
            this.outputPathLabel.Location = new System.Drawing.Point(13, 42);
            this.outputPathLabel.Name = "outputPathLabel";
            this.outputPathLabel.Size = new System.Drawing.Size(67, 13);
            this.outputPathLabel.TabIndex = 11;
            this.outputPathLabel.Text = "Output Path:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 128);
            this.Controls.Add(this.outputPathLabel);
            this.Controls.Add(this.outputPathTextBox);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.installationStatusLabel);
            this.Controls.Add(this.installUninstallButton);
            this.Controls.Add(this.currentInstallationStatusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogMeIn Rescue Script Stealer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label installationStatusLabel;
        private System.Windows.Forms.Button installUninstallButton;
        private System.Windows.Forms.Label currentInstallationStatusLabel;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox outputPathTextBox;
        private System.Windows.Forms.Label outputPathLabel;
    }
}


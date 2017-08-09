using LogMeInRescueScriptStealerGUI.Model;
using System;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace LogMeInRescueScriptStealerGUI
{
    public partial class MainForm : Form
    {
        private bool isInstalled = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void updateInstallationStatus()
        {
            isInstalled = Installer.IsInstalled();
            currentInstallationStatusLabel.ForeColor = isInstalled ? Color.Lime : Color.Red;
            currentInstallationStatusLabel.Text = isInstalled ? "Installed" : "Not installed";
            installUninstallButton.Text = isInstalled ? "Uninstall" : "Install";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!(new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show("Please start this program as administrator!", "Missing administrator privileges", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();

                return;
            }

            updateInstallationStatus();
            outputPathTextBox.Text = IniInterface.ReadValue("Settings", "OutputPath", Installer.GetInstallationIniFileName());
        }

        private void installUninstallButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isInstalled)
                {
                    Installer.Uninstall();
                }
                else
                {
                    if (outputPathTextBox.TextLength > 0
                        && Directory.Exists(outputPathTextBox.Text))
                    {
                        Installer.Install();
                        IniInterface.WriteValue("Settings", "OutputPath", outputPathTextBox.Text, Installer.GetInstallationIniFileName());
                    }
                    else
                    {
                        MessageBox.Show("Please specify a valid Output Path!", "LogMeIn Rescue Script Stealer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                updateInstallationStatus();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("An error occurred while {0} LogMeIn Rescue Script Stealer: {1}", isInstalled ? "uninstalling" : "installing", ex.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                outputPathTextBox.Text = openFolderDialog.SelectedPath;
            }
        }
    }
}

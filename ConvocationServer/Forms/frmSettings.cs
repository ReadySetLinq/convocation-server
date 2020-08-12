using ConvocationServer.Storage;
using System;
using System.Net;
using System.Windows.Forms;

namespace ConvocationServer.Forms
{
    public partial class FrmSettings : Form
    {
        public Settings StorageSettings;

        public FrmSettings(Settings settings)
        {
            InitializeComponent();

            StorageSettings = settings;
            txtIP.Text = StorageSettings.IPAddress;
            numPort.Value = StorageSettings.Port;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            StorageSettings.IPAddress = txtIP.Text.Trim();
            StorageSettings.Port = (int)numPort.Value;
            StorageSettings.Save();
            Hide();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}

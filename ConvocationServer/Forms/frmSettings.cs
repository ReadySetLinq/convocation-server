using ConvocationServer.Storage;
using System;
using System.Net;
using System.Windows.Forms;

namespace ConvocationServer.Forms
{
    public partial class FrmSettings : Form
    {
        private readonly FrmServer _parent;

        public FrmSettings(FrmServer parent)
        {
            InitializeComponent();
            _parent = parent;

            Settings storageSettings = _parent.StorageSettings;
            txtIP.Text = storageSettings.IPAddress;
            numPort.Value = storageSettings.Port;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Settings storageSettings = _parent.StorageSettings;
            storageSettings.IPAddress = txtIP.Text.Trim();
            storageSettings.Port = (int)numPort.Value;
            storageSettings.Save();
            Hide();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}

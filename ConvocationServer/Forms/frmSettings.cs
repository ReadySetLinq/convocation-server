using ConvocationServer.Storage;
using System;
using System.Net;
using System.Net.Sockets;
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

            // Generate the list of available IP addresses to pick
            cmbIP.Items.Clear();
            IPAddress[] ipv4Addresses = Array.FindAll(Dns.GetHostEntry(string.Empty).AddressList,
                                        addr => addr.AddressFamily == AddressFamily.InterNetwork);
            for (int i = 0; i < ipv4Addresses.Length; i++)
            {
                cmbIP.Items.Add(ipv4Addresses[i].ToString());
            }

            Settings storageSettings = _parent.StorageSettings;
            numPort.Value = storageSettings.Port;

            if (cmbIP.Items.Count > 0)
            {
                int index = cmbIP.Items.IndexOf(storageSettings.IPAddress);
                if (index > -1)
                    cmbIP.SelectedIndex = index;
                else
                    cmbIP.SelectedIndex = 0;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Settings storageSettings = _parent.StorageSettings;
            storageSettings.IPAddress = cmbIP.Text.Trim();
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

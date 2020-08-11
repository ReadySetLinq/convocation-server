using System;
using System.Net;
using System.Windows.Forms;

namespace ConvocationServer.Forms
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();

            this.LoadSettings();
        }

        private void LoadSettings()
        {
            txtIP.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            numPort.Value = 8181;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

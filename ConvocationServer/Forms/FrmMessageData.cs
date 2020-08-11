using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvocationServer.Forms
{
    public partial class FrmMessageData : Form
    {
        public FrmMessageData()
        {
            InitializeComponent();
        }

        public void SetData(string message = "", string direction = "Unknown", string timestamp = "Unknown")
        {
            this.txtMessage.Text = message;
            this.txtDirection.Text = direction;
            this.txtTimestamp.Text = timestamp;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

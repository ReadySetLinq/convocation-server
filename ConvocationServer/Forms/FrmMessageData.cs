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

        public void SetData(string message = "", string title = "", string direction = "Unknown", string timestamp = "Unknown")
        {
            this.Text = $"Message Data - {title}";
            txtMessage.Text = message;
            txtDirection.Text = direction;
            txtTimestamp.Text = timestamp;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}

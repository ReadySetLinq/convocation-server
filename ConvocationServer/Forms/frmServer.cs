using ConvocationServer.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConvocationServer
{
    public partial class FrmServer : Form
    {
        private readonly List<Form> LstForms = new List<Form>
        {
            // Settings = Index 0
           new FrmSettings(),
           // MessageData = Index 1
           new FrmMessageData()
        };

        public FrmServer()
        {
            InitializeComponent();

            notifyIcon.BalloonTipTitle = "RSL - Server";
            notifyIcon.BalloonTipText = "Double click to open!";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;

            // this.WindowState = FormWindowState.Minimized;
            // this.MinimizeToTray();
        }

        private void MinimizeToTray()
        {
            // Hide all other open forms first
            for (int i = 0; i < LstForms.Count; i++)
            {
                Form frm = LstForms[i];
                if (frm.Visible)
                    frm.Hide();
            }

            Hide();
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000);
        }


        private void FrmServer_Resize(object sender, EventArgs e)
        {
            //if the form is minimized hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.MinimizeToTray();
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // When the notification icon is double clicked, hide it
            // ad show the window
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        // File ToolStrip Buttons
        private void SettingsStripMenuItem_Click(object sender, EventArgs e)
        {
            LstForms[0].Show();
        }

        private void StatusStripMenuItem_Click(object sender, EventArgs e)
        {
            bool connected = true;
            if (connected)
            {
                this.statusStripMenuItem.Text = "Disconnect";
            } else
            {
                this.statusStripMenuItem.Text = "Connect";
            }
        }

        private void HideWindowStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.MinimizeToTray();
        }

        private void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ManageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // User ToolStrip Buttons
        private void AddNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Help ToolStrip Buttons
        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

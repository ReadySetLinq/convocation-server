using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ConvocationServer.Forms;
using ConvocationServer.Sockets;

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
        private DataTable TblMessages = new DataTable();
        private readonly SocketServer Server = new SocketServer();
        private readonly System.Timers.Timer tmrFailedToConnect = new System.Timers.Timer
        {
            Interval = 5000,
            AutoReset = false
        };
        

        public FrmServer()
        {
            InitializeComponent();

            this.Text += " v" + Application.ProductVersion;
            this.lblStatus.Text = "Stopped";
            this.tmrFailedToConnect.Elapsed += this.OnFailedToOpenEvent;
            this.TblMessages.Columns.Add("Data", typeof(string));
            this.TblMessages.Columns.Add("Direction", typeof(string));
            this.TblMessages.Columns.Add("Timestamp", typeof(string));
            this.dataGridViewMessages.DataSource = this.TblMessages;

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

        private void LblStatus_TextChanged(object sender, EventArgs e)
        {
            Label lbl = this.lblStatus;
            ToolStripMenuItem statusItem = this.statusStripMenuItem;

            if (lbl.Text == "Started")
            {
                lbl.ForeColor = System.Drawing.Color.YellowGreen;
                statusItem.Text = "Stop";
            } else
            {
                lbl.ForeColor = System.Drawing.Color.OrangeRed;
                statusItem.Text = "Start";
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
            if (this.Server.IsConnected)
            {
                this.Server.Stop();
                this.lblStatus.Text = "Stopped";
            } else
            {
                if (this.Server.Start())
                {
                    this.lblStatus.Text = "Started";
                    this.tmrFailedToConnect.Stop();
                }
                else
                {
                    if (!this.tmrFailedToConnect.Enabled)
                    {
                        this.tmrFailedToConnect.Start();
                    }
                    else
                    {
                        this.tmrFailedToConnect.Stop();
                        this.tmrFailedToConnect.Start();
                    }

                    this.lblStatus.Text = "Failed to Start!";
                }
            }
        }

        private void OnFailedToOpenEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Reset to status to Stopped
            this.lblStatus.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                this.lblStatus.Text = "Stopped";
            });

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

        // DataGridViewMessages Buttons
        private void DataGridViewMessages_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridViewMessages.Rows[e.RowIndex];
            FrmMessageData frm = (FrmMessageData)LstForms[1];
            frm.SetData(
                row.Cells[0].Value.ToString(), 
                row.Cells[1].Value.ToString(), 
                row.Cells[2].Value.ToString()
            );
            frm.Show();
        }

        private void CmbDataView_SelectedIndexChanged(object sender, EventArgs e)
        {
           switch(this.cmbDataView.SelectedIndex)
            {
                case 0: // All
                    this.DataViewFilter(this.dataGridViewMessages, "Direction", "");
                    break;
                case 1: // Incoming
                    this.DataViewFilter(this.dataGridViewMessages, "Direction", "Incoming");
                    break;
                case 2: // Outgoing
                    this.DataViewFilter(this.dataGridViewMessages, "Direction", "Outgoing");
                    break;
                default: // All
                    this.DataViewFilter(this.dataGridViewMessages, "Direction", "");
                    break;
            }
        }

        private DataGridView DataViewFilter(DataGridView dgv, string cell, string filter)
        {
            // Turn the filter lowercase and trim it once to use later
            string key = filter.ToLower().Trim();

            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgv.DataSource];
            currencyManager1.SuspendBinding();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                // Skip null rows
                if (row == null) continue;

                // No filter was given, so all rows should be made visible
                if (filter == "")
                    row.Visible = true;
                else
                {
                    // If the value does not match, hide the layer
                    // otherwise show it
                    if (row.Cells[cell].Value.ToString().ToLower().Trim() != key)
                        row.Visible = false;
                    else
                        row.Visible = true;
                }
            }
            currencyManager1.ResumeBinding();

            return dgv;
        }
    }
}

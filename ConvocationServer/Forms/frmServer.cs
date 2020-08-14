using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ConvocationServer.Forms;
using ConvocationServer.Sockets;
using ConvocationServer.Storage;

namespace ConvocationServer
{
    public partial class FrmServer : Form
    {
        private readonly List<Form> LstForms;
        private readonly SocketServer Server;
        private readonly System.Timers.Timer tmrFailedToConnect = new System.Timers.Timer
        {
            Interval = 5000,
            AutoReset = false
        };

        public DataTable TblMessages = new DataTable();
        public Settings StorageSettings = new Settings();


        public FrmServer()
        {
            InitializeComponent();

            Text += " v" + Application.ProductVersion;
            lblStatus.Text = "Stopped";
            tmrFailedToConnect.Elapsed += OnFailedToOpenEvent;
            TblMessages.Columns.Add("Message", typeof(string));
            TblMessages.Columns.Add("Title", typeof(string));
            TblMessages.Columns.Add("Direction", typeof(string));
            TblMessages.Columns.Add("Timestamp", typeof(string));
            dgvMessages.DataSource = TblMessages;
            dgvMessages.Columns[0].Visible = false;
            
            // Load settings
            StorageSettings.Load();

            LstForms = new List<Form>
            {
                // Settings = Index 0
               new FrmSettings(this),
               // MessageData = Index 1
               new FrmMessageData(),
               // Users Manager = Index 2
               new FrmUsers(this),
            };
            Server = new SocketServer(this);

            notifyIcon.BalloonTipTitle = "RSL - Server";
            notifyIcon.BalloonTipText = "Double click to open!";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.Visible = true;

            // WindowState = FormWindowState.Minimized;
            // MinimizeToTray();
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
            showToolStripMenuItem.Text = "Show";
            notifyIcon.ShowBalloonTip(1000);
        }

        private void FrmServer_Resize(object sender, EventArgs e)
        {
            //if the form is minimized hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (WindowState == FormWindowState.Minimized)
            {
                MinimizeToTray();
            }
        }

        private void LblStatus_TextChanged(object sender, EventArgs e)
        {
            Label lbl = lblStatus;
            ToolStripMenuItem statusItem = statusStripMenuItem;

            if (lbl.Text == "Started")
            {
                lbl.ForeColor = System.Drawing.Color.YellowGreen;
                statusItem.Text = "Stop";
                connectToolStripMenuItem.Text = "Stop";
            } else
            {
                lbl.ForeColor = System.Drawing.Color.OrangeRed;
                statusItem.Text = "Start";
                connectToolStripMenuItem.Text = "Start";
            }
        }

        // ctxMenuStripNotify Buttons
        private void ConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleServer();
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showToolStripMenuItem.Text == "Hide")
            {
                WindowState = FormWindowState.Minimized;
                MinimizeToTray();
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                showToolStripMenuItem.Text = "Hide";
                ctxMenuStripNotify.Hide();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        // File ToolStrip Buttons
        private void SettingsStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LstForms[0].IsDisposed)
            {
                LstForms[0] = new FrmSettings(this);
            }
            FrmSettings frm = (FrmSettings)LstForms[0];

            if (frm == null) return;

            frm.Show();
            frm.BringToFront();
        }

        private void StatusStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleServer();
        }

        private void OnFailedToOpenEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Reset to status to Stopped
            lblStatus.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                if (Server.IsConnected)
                    lblStatus.Text = "Stop";
                else
                    lblStatus.Text = "Start";
            });

        }

        private void HideWindowStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            MinimizeToTray();
        }

        private void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        // User ToolStrip Buttons
        private void ManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LstForms[2].IsDisposed)
            {
                LstForms[2] = new FrmUsers(this);
            }
            FrmUsers frm = (FrmUsers)LstForms[2];

            if (frm == null) return;

            frm.Show();
            frm.BringToFront();
        }

        // Help ToolStrip Buttons
        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
        private void FrmServer_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CmbDataView_SelectedIndexChanged(object sender, EventArgs e)
        {
           switch(cmbDataView.SelectedIndex)
            {
                case 0: // All
                    DataViewFilter(dgvMessages, "Direction", "");
                    break;
                case 1: // Incoming
                    DataViewFilter(dgvMessages, "Direction", "Incoming");
                    break;
                case 2: // Outgoing
                    DataViewFilter(dgvMessages, "Direction", "Outgoing");
                    break;
                default: // All
                    DataViewFilter(dgvMessages, "Direction", "");
                    break;
            }
        }

        private void DgvMessages_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            dgvMessages.ClearSelection();
            int rowSelected = e.RowIndex;
            if (e.RowIndex != -1)
            {
                dgvMessages.Rows[rowSelected].Selected = true;
            }
            e.ContextMenuStrip = ctxMenuStripMessageData;
        }

        private void ToolStripMenuItemDetailedView_Click(object sender, EventArgs e)
        {
            if (dgvMessages.SelectedRows.Count == 0) return;

            if (LstForms[1].IsDisposed)
            {
                LstForms[1] = new FrmMessageData();
            }
            FrmMessageData frm = (FrmMessageData)LstForms[1];
            DataGridViewRow row = dgvMessages.SelectedRows[0];

            if (frm == null || row.Cells.Count != 3) return;

            object message = row.Cells[0].Value;
            object direction = row.Cells[2].Value;
            object timestamp = row.Cells[3].Value;
            frm.SetData(
                message?.ToString(),
                direction?.ToString(),
                timestamp?.ToString()
            );
            frm.Show();
            frm.BringToFront();
        }

        private void ExitApp()
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void ToggleServer()
        {
            if (Server.IsConnected)
            {
                Server.Stop();
                lblStatus.Text = "Stopped";
            }
            else
            {
                Server.Start();
                if (!tmrFailedToConnect.Enabled)
                    tmrFailedToConnect.Start();
                else
                {
                    tmrFailedToConnect.Stop();
                    tmrFailedToConnect.Start();
                }

                lblStatus.Text = "Starting...";
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

        public void AddMessage(string message, string title, string direction, string timestamp = null)
        {
            if (timestamp == null)
                timestamp = DateTime.Now.ToString();

            TblMessages.Rows.Add(message, title, direction, timestamp);
        }
    }
}

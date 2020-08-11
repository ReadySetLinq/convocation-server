namespace ConvocationServer
{
    partial class FrmServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmServer));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStripServer = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideWindowStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStaticStatus = new System.Windows.Forms.Label();
            this.dataGridViewMessages = new System.Windows.Forms.DataGridView();
            this.lblStaticView = new System.Windows.Forms.Label();
            this.cmbDataView = new System.Windows.Forms.ComboBox();
            this.columnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripServer.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Double Click to re-open";
            this.notifyIcon.BalloonTipTitle = "RSL";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "RSL - Server";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // menuStripServer
            // 
            this.menuStripServer.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.usersStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripServer.Location = new System.Drawing.Point(0, 0);
            this.menuStripServer.Name = "menuStripServer";
            this.menuStripServer.Size = new System.Drawing.Size(622, 28);
            this.menuStripServer.TabIndex = 0;
            this.menuStripServer.Text = "RSL";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsStripMenuItem,
            this.statusStripMenuItem,
            this.hideWindowStripMenuItem,
            this.exitStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.ToolTipText = "FIle";
            // 
            // settingsStripMenuItem
            // 
            this.settingsStripMenuItem.Name = "settingsStripMenuItem";
            this.settingsStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.settingsStripMenuItem.Text = "Settings";
            this.settingsStripMenuItem.Click += new System.EventHandler(this.SettingsStripMenuItem_Click);
            // 
            // statusStripMenuItem
            // 
            this.statusStripMenuItem.Name = "statusStripMenuItem";
            this.statusStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.statusStripMenuItem.Text = "Connect";
            this.statusStripMenuItem.Click += new System.EventHandler(this.StatusStripMenuItem_Click);
            // 
            // hideWindowStripMenuItem
            // 
            this.hideWindowStripMenuItem.Name = "hideWindowStripMenuItem";
            this.hideWindowStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.hideWindowStripMenuItem.Text = "Hide";
            this.hideWindowStripMenuItem.ToolTipText = "Hide Window";
            this.hideWindowStripMenuItem.Click += new System.EventHandler(this.HideWindowStripMenuItem_Click);
            // 
            // exitStripMenuItem
            // 
            this.exitStripMenuItem.Name = "exitStripMenuItem";
            this.exitStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.exitStripMenuItem.Text = "Exit";
            this.exitStripMenuItem.Click += new System.EventHandler(this.ExitStripMenuItem_Click);
            // 
            // usersStripMenuItem
            // 
            this.usersStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.manageToolStripMenuItem});
            this.usersStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.usersStripMenuItem.Name = "usersStripMenuItem";
            this.usersStripMenuItem.Size = new System.Drawing.Size(58, 26);
            this.usersStripMenuItem.Text = "Users";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.addNewToolStripMenuItem.Text = "Add New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.AddNewToolStripMenuItem_Click);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.manageToolStripMenuItem.Text = "Manage";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.ManageToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.InfoToolStripMenuItem_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.cmbDataView);
            this.panelBottom.Controls.Add(this.lblStaticView);
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Controls.Add(this.lblStaticStatus);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 401);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(622, 40);
            this.panelBottom.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblStatus.Location = new System.Drawing.Point(139, 6);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(98, 20);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Connected";
            // 
            // lblStaticStatus
            // 
            this.lblStaticStatus.AutoSize = true;
            this.lblStaticStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaticStatus.Location = new System.Drawing.Point(12, 6);
            this.lblStaticStatus.Name = "lblStaticStatus";
            this.lblStaticStatus.Size = new System.Drawing.Size(130, 20);
            this.lblStaticStatus.TabIndex = 0;
            this.lblStaticStatus.Text = "Server Status:";
            // 
            // dataGridViewMessages
            // 
            this.dataGridViewMessages.AllowUserToAddRows = false;
            this.dataGridViewMessages.AllowUserToDeleteRows = false;
            this.dataGridViewMessages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMessages.BackgroundColor = System.Drawing.Color.DimGray;
            this.dataGridViewMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnData,
            this.columnDirection,
            this.columnTimestamp});
            this.dataGridViewMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMessages.Location = new System.Drawing.Point(0, 28);
            this.dataGridViewMessages.MultiSelect = false;
            this.dataGridViewMessages.Name = "dataGridViewMessages";
            this.dataGridViewMessages.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewMessages.RowHeadersWidth = 51;
            this.dataGridViewMessages.RowTemplate.Height = 24;
            this.dataGridViewMessages.Size = new System.Drawing.Size(622, 373);
            this.dataGridViewMessages.TabIndex = 2;
            // 
            // lblStaticView
            // 
            this.lblStaticView.AutoSize = true;
            this.lblStaticView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaticView.Location = new System.Drawing.Point(428, 8);
            this.lblStaticView.Name = "lblStaticView";
            this.lblStaticView.Size = new System.Drawing.Size(55, 20);
            this.lblStaticView.TabIndex = 2;
            this.lblStaticView.Text = "View:";
            // 
            // cmbDataView
            // 
            this.cmbDataView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDataView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDataView.FormattingEnabled = true;
            this.cmbDataView.ItemHeight = 18;
            this.cmbDataView.Items.AddRange(new object[] {
            "All",
            "Incoming",
            "Outgoing"});
            this.cmbDataView.Location = new System.Drawing.Point(489, 6);
            this.cmbDataView.MaxDropDownItems = 3;
            this.cmbDataView.Name = "cmbDataView";
            this.cmbDataView.Size = new System.Drawing.Size(121, 26);
            this.cmbDataView.TabIndex = 99;
            this.cmbDataView.TabStop = false;
            this.cmbDataView.Text = "All";
            // 
            // columnData
            // 
            this.columnData.FillWeight = 152.1073F;
            this.columnData.HeaderText = "Data";
            this.columnData.MinimumWidth = 6;
            this.columnData.Name = "columnData";
            this.columnData.ReadOnly = true;
            this.columnData.ToolTipText = "Message Data";
            // 
            // columnDirection
            // 
            this.columnDirection.FillWeight = 56.14973F;
            this.columnDirection.HeaderText = "Direction";
            this.columnDirection.MinimumWidth = 6;
            this.columnDirection.Name = "columnDirection";
            this.columnDirection.ReadOnly = true;
            this.columnDirection.ToolTipText = "Incoming/Outgoing";
            // 
            // columnTimestamp
            // 
            this.columnTimestamp.FillWeight = 91.74297F;
            this.columnTimestamp.HeaderText = "Timestamp";
            this.columnTimestamp.MinimumWidth = 6;
            this.columnTimestamp.Name = "columnTimestamp";
            this.columnTimestamp.ReadOnly = true;
            this.columnTimestamp.ToolTipText = "When the message happened";
            // 
            // FrmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(622, 441);
            this.Controls.Add(this.dataGridViewMessages);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.menuStripServer);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripServer;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(4096, 2160);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FrmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSL - Server";
            this.Resize += new System.EventHandler(this.FrmServer_Resize);
            this.menuStripServer.ResumeLayout(false);
            this.menuStripServer.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.MenuStrip menuStripServer;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideWindowStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStaticStatus;
        private System.Windows.Forms.ToolStripMenuItem settingsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewMessages;
        private System.Windows.Forms.ComboBox cmbDataView;
        private System.Windows.Forms.Label lblStaticView;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTimestamp;
    }
}


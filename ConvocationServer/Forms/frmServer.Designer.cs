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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxMenuStripNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripServer = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideWindowStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panViewDock = new System.Windows.Forms.Panel();
            this.cmbDataView = new System.Windows.Forms.ComboBox();
            this.lblStaticView = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStaticStatus = new System.Windows.Forms.Label();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.ctxMenuStripMessageData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDetailedView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuStripNotify.SuspendLayout();
            this.menuStripServer.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panViewDock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            this.ctxMenuStripMessageData.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Double Click to re-open";
            this.notifyIcon.BalloonTipTitle = "RSL";
            this.notifyIcon.ContextMenuStrip = this.ctxMenuStripNotify;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "RSL - Server";
            // 
            // ctxMenuStripNotify
            // 
            this.ctxMenuStripNotify.ForeColor = System.Drawing.Color.Black;
            this.ctxMenuStripNotify.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenuStripNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.ctxMenuStripNotify.Name = "ctxMenuStripNotify";
            this.ctxMenuStripNotify.Size = new System.Drawing.Size(133, 76);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.ConnectToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.showToolStripMenuItem.Text = "Hide";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.ToolTipText = "File";
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
            this.manageToolStripMenuItem});
            this.usersStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.usersStripMenuItem.Name = "usersStripMenuItem";
            this.usersStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.usersStripMenuItem.Text = "Users";
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.manageToolStripMenuItem.Text = "Manage";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.ManageToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
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
            this.panelBottom.Controls.Add(this.panViewDock);
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Controls.Add(this.lblStaticStatus);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 401);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(622, 40);
            this.panelBottom.TabIndex = 1;
            // 
            // panViewDock
            // 
            this.panViewDock.BackColor = System.Drawing.Color.Transparent;
            this.panViewDock.Controls.Add(this.cmbDataView);
            this.panViewDock.Controls.Add(this.lblStaticView);
            this.panViewDock.Dock = System.Windows.Forms.DockStyle.Right;
            this.panViewDock.Location = new System.Drawing.Point(418, 0);
            this.panViewDock.Name = "panViewDock";
            this.panViewDock.Size = new System.Drawing.Size(204, 40);
            this.panViewDock.TabIndex = 2;
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
            this.cmbDataView.Location = new System.Drawing.Point(64, 4);
            this.cmbDataView.MaxDropDownItems = 3;
            this.cmbDataView.Name = "cmbDataView";
            this.cmbDataView.Size = new System.Drawing.Size(121, 26);
            this.cmbDataView.TabIndex = 101;
            this.cmbDataView.TabStop = false;
            this.cmbDataView.Text = "All";
            this.cmbDataView.SelectedIndexChanged += new System.EventHandler(this.CmbDataView_SelectedIndexChanged);
            // 
            // lblStaticView
            // 
            this.lblStaticView.AutoSize = true;
            this.lblStaticView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaticView.Location = new System.Drawing.Point(3, 6);
            this.lblStaticView.Name = "lblStaticView";
            this.lblStaticView.Size = new System.Drawing.Size(55, 20);
            this.lblStaticView.TabIndex = 100;
            this.lblStaticView.Text = "View:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Silver;
            this.lblStatus.Location = new System.Drawing.Point(148, 6);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 20);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "...";
            this.lblStatus.TextChanged += new System.EventHandler(this.LblStatus_TextChanged);
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
            // dgvMessages
            // 
            this.dgvMessages.AllowUserToAddRows = false;
            this.dgvMessages.AllowUserToDeleteRows = false;
            this.dgvMessages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMessages.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMessages.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMessages.Location = new System.Drawing.Point(0, 28);
            this.dgvMessages.MultiSelect = false;
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvMessages.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMessages.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMessages.RowTemplate.ContextMenuStrip = this.ctxMenuStripMessageData;
            this.dgvMessages.RowTemplate.Height = 24;
            this.dgvMessages.Size = new System.Drawing.Size(622, 373);
            this.dgvMessages.TabIndex = 2;
            this.dgvMessages.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.DgvMessages_CellContextMenuStripNeeded);
            // 
            // ctxMenuStripMessageData
            // 
            this.ctxMenuStripMessageData.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenuStripMessageData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDetailedView});
            this.ctxMenuStripMessageData.Name = "ctxMenuStripMessageData";
            this.ctxMenuStripMessageData.Size = new System.Drawing.Size(172, 28);
            // 
            // toolStripMenuItemDetailedView
            // 
            this.toolStripMenuItemDetailedView.Name = "toolStripMenuItemDetailedView";
            this.toolStripMenuItemDetailedView.Size = new System.Drawing.Size(171, 24);
            this.toolStripMenuItemDetailedView.Text = "Detailed View";
            this.toolStripMenuItemDetailedView.Click += new System.EventHandler(this.ToolStripMenuItemDetailedView_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // FrmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(622, 441);
            this.Controls.Add(this.dgvMessages);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.menuStripServer);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripServer;
            this.MaximumSize = new System.Drawing.Size(4096, 2160);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FrmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSL - Server";
            this.Resize += new System.EventHandler(this.FrmServer_Resize);
            this.ctxMenuStripNotify.ResumeLayout(false);
            this.menuStripServer.ResumeLayout(false);
            this.menuStripServer.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panViewDock.ResumeLayout(false);
            this.panViewDock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            this.ctxMenuStripMessageData.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStaticStatus;
        private System.Windows.Forms.ToolStripMenuItem settingsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.ContextMenuStrip ctxMenuStripNotify;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panViewDock;
        private System.Windows.Forms.ComboBox cmbDataView;
        private System.Windows.Forms.Label lblStaticView;
        private System.Windows.Forms.ContextMenuStrip ctxMenuStripMessageData;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDetailedView;
    }
}


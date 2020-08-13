namespace ConvocationServer.Forms
{
    partial class FrmUsers
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
            this.groupSelectedUser = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupUsers = new System.Windows.Forms.GroupBox();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.ctxMenuStripUser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.groupSelectedUser.SuspendLayout();
            this.groupUsers.SuspendLayout();
            this.ctxMenuStripUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupSelectedUser
            // 
            this.groupSelectedUser.Controls.Add(this.btnReset);
            this.groupSelectedUser.Controls.Add(this.btnSave);
            this.groupSelectedUser.Controls.Add(this.txtPassword);
            this.groupSelectedUser.Controls.Add(this.lblPassword);
            this.groupSelectedUser.Controls.Add(this.txtUserName);
            this.groupSelectedUser.Controls.Add(this.lblUserName);
            this.groupSelectedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupSelectedUser.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupSelectedUser.Location = new System.Drawing.Point(135, 0);
            this.groupSelectedUser.Name = "groupSelectedUser";
            this.groupSelectedUser.Size = new System.Drawing.Size(376, 235);
            this.groupSelectedUser.TabIndex = 7;
            this.groupSelectedUser.TabStop = false;
            this.groupSelectedUser.Text = "Selected User";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(250, 153);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(109, 35);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(12, 153);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 35);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(121, 100);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(238, 27);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Tag = "";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(8, 102);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(97, 20);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password:";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(122, 39);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(237, 27);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Tag = "";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(8, 41);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(103, 20);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Tag = "";
            this.lblUserName.Text = "UserName:";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(147, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(347, 31);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupUsers
            // 
            this.groupUsers.Controls.Add(this.lstUsers);
            this.groupUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupUsers.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupUsers.Location = new System.Drawing.Point(4, 0);
            this.groupUsers.Name = "groupUsers";
            this.groupUsers.Size = new System.Drawing.Size(127, 235);
            this.groupUsers.TabIndex = 9;
            this.groupUsers.TabStop = false;
            this.groupUsers.Text = "Users";
            // 
            // lstUsers
            // 
            this.lstUsers.BackColor = System.Drawing.Color.DimGray;
            this.lstUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstUsers.ContextMenuStrip = this.ctxMenuStripUser;
            this.lstUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUsers.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.ItemHeight = 20;
            this.lstUsers.Location = new System.Drawing.Point(3, 23);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(121, 209);
            this.lstUsers.TabIndex = 0;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.LstUsers_SelectedIndexChanged);
            // 
            // ctxMenuStripUser
            // 
            this.ctxMenuStripUser.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenuStripUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.ctxMenuStripUser.Name = "ctxMenuStripUser";
            this.ctxMenuStripUser.Size = new System.Drawing.Size(133, 28);
            this.ctxMenuStripUser.Opening += new System.ComponentModel.CancelEventHandler(this.CtxMenuStripUser_Opening);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.ForeColor = System.Drawing.Color.Black;
            this.btnAddNew.Location = new System.Drawing.Point(7, 235);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(117, 31);
            this.btnAddNew.TabIndex = 10;
            this.btnAddNew.TabStop = false;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // FrmUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(532, 307);
            this.ControlBox = false;
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.groupUsers);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupSelectedUser);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmUsers";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Manager";
            this.Shown += new System.EventHandler(this.FrmUsers_Shown);
            this.groupSelectedUser.ResumeLayout(false);
            this.groupSelectedUser.PerformLayout();
            this.groupUsers.ResumeLayout(false);
            this.ctxMenuStripUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupSelectedUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupUsers;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.ContextMenuStrip ctxMenuStripUser;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}
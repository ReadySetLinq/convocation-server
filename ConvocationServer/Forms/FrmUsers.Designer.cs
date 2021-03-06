﻿namespace ConvocationServer.Forms
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
            this.groupSelectedUser.Location = new System.Drawing.Point(124, 0);
            this.groupSelectedUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupSelectedUser.Name = "groupSelectedUser";
            this.groupSelectedUser.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupSelectedUser.Size = new System.Drawing.Size(260, 191);
            this.groupSelectedUser.TabIndex = 7;
            this.groupSelectedUser.TabStop = false;
            this.groupSelectedUser.Text = "Selected User";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.Control;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReset.Location = new System.Drawing.Point(163, 124);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 28);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSave.Location = new System.Drawing.Point(16, 124);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 28);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(91, 81);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(154, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Tag = "";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 83);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(82, 17);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password:";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Location = new System.Drawing.Point(92, 32);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(154, 23);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Tag = "";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(6, 33);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(88, 17);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Tag = "";
            this.lblUserName.Text = "UserName:";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(132, 195);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(238, 25);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupUsers
            // 
            this.groupUsers.Controls.Add(this.lstUsers);
            this.groupUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupUsers.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupUsers.Location = new System.Drawing.Point(3, 0);
            this.groupUsers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupUsers.Name = "groupUsers";
            this.groupUsers.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupUsers.Size = new System.Drawing.Size(116, 191);
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
            this.lstUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUsers.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.ItemHeight = 17;
            this.lstUsers.Location = new System.Drawing.Point(2, 18);
            this.lstUsers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(112, 171);
            this.lstUsers.TabIndex = 0;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.LstUsers_SelectedIndexChanged);
            // 
            // ctxMenuStripUser
            // 
            this.ctxMenuStripUser.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenuStripUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.ctxMenuStripUser.Name = "ctxMenuStripUser";
            this.ctxMenuStripUser.Size = new System.Drawing.Size(114, 26);
            this.ctxMenuStripUser.Opening += new System.ComponentModel.CancelEventHandler(this.CtxMenuStripUser_Opening);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddNew.Location = new System.Drawing.Point(11, 195);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(100, 25);
            this.btnAddNew.TabIndex = 10;
            this.btnAddNew.TabStop = false;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = false;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // FrmUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(387, 225);
            this.ControlBox = false;
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.groupUsers);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupSelectedUser);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmUsers";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
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
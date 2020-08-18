namespace ConvocationServer.Forms
{
    partial class FrmMessageData
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
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDirection = new System.Windows.Forms.TextBox();
            this.txtTimestamp = new System.Windows.Forms.TextBox();
            this.lblStaticDirection = new System.Windows.Forms.Label();
            this.lblStaticTimestamp = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.Location = new System.Drawing.Point(250, 216);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 28);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDirection);
            this.groupBox1.Controls.Add(this.txtTimestamp);
            this.groupBox1.Controls.Add(this.lblStaticDirection);
            this.groupBox1.Controls.Add(this.lblStaticTimestamp);
            this.groupBox1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Location = new System.Drawing.Point(10, 191);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(227, 74);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // txtDirection
            // 
            this.txtDirection.BackColor = System.Drawing.SystemColors.Window;
            this.txtDirection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirection.Location = new System.Drawing.Point(146, 17);
            this.txtDirection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDirection.Name = "txtDirection";
            this.txtDirection.ReadOnly = true;
            this.txtDirection.Size = new System.Drawing.Size(66, 22);
            this.txtDirection.TabIndex = 6;
            this.txtDirection.Tag = "";
            // 
            // txtTimestamp
            // 
            this.txtTimestamp.BackColor = System.Drawing.SystemColors.Window;
            this.txtTimestamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimestamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimestamp.Location = new System.Drawing.Point(4, 41);
            this.txtTimestamp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTimestamp.Name = "txtTimestamp";
            this.txtTimestamp.ReadOnly = true;
            this.txtTimestamp.Size = new System.Drawing.Size(208, 22);
            this.txtTimestamp.TabIndex = 5;
            this.txtTimestamp.Tag = "";
            // 
            // lblStaticDirection
            // 
            this.lblStaticDirection.AutoSize = true;
            this.lblStaticDirection.Location = new System.Drawing.Point(90, 19);
            this.lblStaticDirection.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStaticDirection.Name = "lblStaticDirection";
            this.lblStaticDirection.Size = new System.Drawing.Size(52, 13);
            this.lblStaticDirection.TabIndex = 4;
            this.lblStaticDirection.Text = "Direction:";
            // 
            // lblStaticTimestamp
            // 
            this.lblStaticTimestamp.AutoSize = true;
            this.lblStaticTimestamp.Location = new System.Drawing.Point(4, 25);
            this.lblStaticTimestamp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStaticTimestamp.Name = "lblStaticTimestamp";
            this.lblStaticTimestamp.Size = new System.Drawing.Size(61, 13);
            this.lblStaticTimestamp.TabIndex = 3;
            this.lblStaticTimestamp.Tag = "";
            this.lblStaticTimestamp.Text = "Timestamp:";
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.Black;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMessage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(346, 187);
            this.txtMessage.TabIndex = 7;
            this.txtMessage.Text = "";
            // 
            // FrmMessageData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(346, 274);
            this.ControlBox = false;
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMessageData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Data";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.Label lblStaticDirection;
        private System.Windows.Forms.Label lblStaticTimestamp;
        private System.Windows.Forms.TextBox txtDirection;
        private System.Windows.Forms.TextBox txtTimestamp;
    }
}
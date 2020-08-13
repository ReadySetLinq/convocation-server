namespace ConvocationServer.Forms
{
    partial class FrmSettings
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupNetwork = new System.Windows.Forms.GroupBox();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIP = new System.Windows.Forms.ComboBox();
            this.groupNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(240, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(12, 122);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 35);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupNetwork
            // 
            this.groupNetwork.Controls.Add(this.cmbIP);
            this.groupNetwork.Controls.Add(this.numPort);
            this.groupNetwork.Controls.Add(this.label2);
            this.groupNetwork.Controls.Add(this.label1);
            this.groupNetwork.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupNetwork.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupNetwork.Location = new System.Drawing.Point(0, 0);
            this.groupNetwork.Name = "groupNetwork";
            this.groupNetwork.Size = new System.Drawing.Size(370, 116);
            this.groupNetwork.TabIndex = 6;
            this.groupNetwork.TabStop = false;
            this.groupNetwork.Text = "Network";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(70, 75);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(80, 27);
            this.numPort.TabIndex = 3;
            this.numPort.Value = new decimal(new int[] {
            8181,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 0;
            this.label1.Tag = "This is for external devices connecting. Do NOT change this unless you have a spe" +
    "cfic reason!";
            this.label1.Text = "IP:";
            // 
            // cmbIP
            // 
            this.cmbIP.FormattingEnabled = true;
            this.cmbIP.Location = new System.Drawing.Point(51, 26);
            this.cmbIP.Name = "cmbIP";
            this.cmbIP.Size = new System.Drawing.Size(298, 28);
            this.cmbIP.TabIndex = 4;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(370, 205);
            this.ControlBox = false;
            this.Controls.Add(this.groupNetwork);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.groupNetwork.ResumeLayout(false);
            this.groupNetwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupNetwork;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbIP;
    }
}
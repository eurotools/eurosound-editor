
namespace sb_editor.Forms
{
    partial class SetMaxBankSizeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetMaxBankSizeForm));
            this.lblBankName = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.lblMaxPlayStation = new System.Windows.Forms.Label();
            this.nudPlayStation = new System.Windows.Forms.NumericUpDown();
            this.lblPlayStationK = new System.Windows.Forms.Label();
            this.lblPcK = new System.Windows.Forms.Label();
            this.nudPC = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.lblGameCubeK = new System.Windows.Forms.Label();
            this.nudGameCube = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.lblXboxK = new System.Windows.Forms.Label();
            this.nudXbox = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayStation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameCube)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXbox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(12, 15);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(66, 13);
            this.lblBankName.TabIndex = 0;
            this.lblBankName.Text = "Bank Name:";
            // 
            // txtBankName
            // 
            this.txtBankName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBankName.BackColor = System.Drawing.SystemColors.Window;
            this.txtBankName.Location = new System.Drawing.Point(84, 12);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.ReadOnly = true;
            this.txtBankName.Size = new System.Drawing.Size(216, 20);
            this.txtBankName.TabIndex = 1;
            // 
            // lblMaxPlayStation
            // 
            this.lblMaxPlayStation.AutoSize = true;
            this.lblMaxPlayStation.Location = new System.Drawing.Point(12, 49);
            this.lblMaxPlayStation.Name = "lblMaxPlayStation";
            this.lblMaxPlayStation.Size = new System.Drawing.Size(160, 13);
            this.lblMaxPlayStation.TabIndex = 2;
            this.lblMaxPlayStation.Text = "SoundBank Max on PlayStation:";
            // 
            // nudPlayStation
            // 
            this.nudPlayStation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPlayStation.Location = new System.Drawing.Point(211, 47);
            this.nudPlayStation.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudPlayStation.Name = "nudPlayStation";
            this.nudPlayStation.Size = new System.Drawing.Size(69, 20);
            this.nudPlayStation.TabIndex = 3;
            // 
            // lblPlayStationK
            // 
            this.lblPlayStationK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayStationK.AutoSize = true;
            this.lblPlayStationK.Location = new System.Drawing.Point(286, 49);
            this.lblPlayStationK.Name = "lblPlayStationK";
            this.lblPlayStationK.Size = new System.Drawing.Size(14, 13);
            this.lblPlayStationK.TabIndex = 4;
            this.lblPlayStationK.Text = "K";
            // 
            // lblPcK
            // 
            this.lblPcK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPcK.AutoSize = true;
            this.lblPcK.Location = new System.Drawing.Point(286, 69);
            this.lblPcK.Name = "lblPcK";
            this.lblPcK.Size = new System.Drawing.Size(14, 13);
            this.lblPcK.TabIndex = 7;
            this.lblPcK.Text = "K";
            // 
            // nudPC
            // 
            this.nudPC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPC.Location = new System.Drawing.Point(211, 67);
            this.nudPC.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudPC.Name = "nudPC";
            this.nudPC.Size = new System.Drawing.Size(69, 20);
            this.nudPC.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "SoundBank Max on PC:";
            // 
            // lblGameCubeK
            // 
            this.lblGameCubeK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGameCubeK.AutoSize = true;
            this.lblGameCubeK.Location = new System.Drawing.Point(286, 89);
            this.lblGameCubeK.Name = "lblGameCubeK";
            this.lblGameCubeK.Size = new System.Drawing.Size(14, 13);
            this.lblGameCubeK.TabIndex = 10;
            this.lblGameCubeK.Text = "K";
            // 
            // nudGameCube
            // 
            this.nudGameCube.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudGameCube.Location = new System.Drawing.Point(211, 87);
            this.nudGameCube.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudGameCube.Name = "nudGameCube";
            this.nudGameCube.Size = new System.Drawing.Size(69, 20);
            this.nudGameCube.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "SoundBank Max on GameCube:";
            // 
            // lblXboxK
            // 
            this.lblXboxK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblXboxK.AutoSize = true;
            this.lblXboxK.Location = new System.Drawing.Point(286, 109);
            this.lblXboxK.Name = "lblXboxK";
            this.lblXboxK.Size = new System.Drawing.Size(14, 13);
            this.lblXboxK.TabIndex = 13;
            this.lblXboxK.Text = "K";
            // 
            // nudXbox
            // 
            this.nudXbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudXbox.Location = new System.Drawing.Point(211, 107);
            this.nudXbox.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudXbox.Name = "nudXbox";
            this.nudXbox.Size = new System.Drawing.Size(69, 20);
            this.nudXbox.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "SoundBank Max on XBox:";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(119, 140);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // SetMaxBankSizeForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 175);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblXboxK);
            this.Controls.Add(this.nudXbox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblGameCubeK);
            this.Controls.Add(this.nudGameCube);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblPcK);
            this.Controls.Add(this.nudPC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblPlayStationK);
            this.Controls.Add(this.nudPlayStation);
            this.Controls.Add(this.lblMaxPlayStation);
            this.Controls.Add(this.txtBankName);
            this.Controls.Add(this.lblBankName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetMaxBankSizeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Max Bank Size";
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayStation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameCube)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label lblMaxPlayStation;
        private System.Windows.Forms.NumericUpDown nudPlayStation;
        private System.Windows.Forms.Label lblPlayStationK;
        private System.Windows.Forms.Label lblPcK;
        private System.Windows.Forms.NumericUpDown nudPC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGameCubeK;
        private System.Windows.Forms.NumericUpDown nudGameCube;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblXboxK;
        private System.Windows.Forms.NumericUpDown nudXbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnOk;
    }
}
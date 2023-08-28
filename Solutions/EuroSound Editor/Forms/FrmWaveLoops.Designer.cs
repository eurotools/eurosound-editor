
namespace sb_editor.Forms
{
    partial class FrmWaveLoops
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkToLastSample = new System.Windows.Forms.CheckBox();
            this.chkLoopSettings = new System.Windows.Forms.CheckBox();
            this.nudEndLoop = new System.Windows.Forms.NumericUpDown();
            this.lblEndLoop = new System.Windows.Forms.Label();
            this.nudStartLoop = new System.Windows.Forms.NumericUpDown();
            this.lblLoopStart = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNote = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndLoop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartLoop)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkToLastSample);
            this.groupBox1.Controls.Add(this.chkLoopSettings);
            this.groupBox1.Controls.Add(this.nudEndLoop);
            this.groupBox1.Controls.Add(this.lblEndLoop);
            this.groupBox1.Controls.Add(this.nudStartLoop);
            this.groupBox1.Controls.Add(this.lblLoopStart);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // chkToLastSample
            // 
            this.chkToLastSample.AutoSize = true;
            this.chkToLastSample.Location = new System.Drawing.Point(189, 45);
            this.chkToLastSample.Name = "chkToLastSample";
            this.chkToLastSample.Size = new System.Drawing.Size(100, 17);
            this.chkToLastSample.TabIndex = 4;
            this.chkToLastSample.Text = "To Last Sample";
            this.chkToLastSample.UseVisualStyleBackColor = true;
            this.chkToLastSample.CheckedChanged += new System.EventHandler(this.ChkToLastSample_CheckedChanged);
            // 
            // chkLoopSettings
            // 
            this.chkLoopSettings.AutoSize = true;
            this.chkLoopSettings.Checked = true;
            this.chkLoopSettings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoopSettings.Location = new System.Drawing.Point(9, 0);
            this.chkLoopSettings.Name = "chkLoopSettings";
            this.chkLoopSettings.Size = new System.Drawing.Size(100, 17);
            this.chkLoopSettings.TabIndex = 0;
            this.chkLoopSettings.Text = "Enable Looping";
            this.chkLoopSettings.UseVisualStyleBackColor = true;
            this.chkLoopSettings.CheckedChanged += new System.EventHandler(this.ChkLoopSettings_CheckedChanged);
            // 
            // nudEndLoop
            // 
            this.nudEndLoop.Location = new System.Drawing.Point(185, 19);
            this.nudEndLoop.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudEndLoop.Name = "nudEndLoop";
            this.nudEndLoop.Size = new System.Drawing.Size(100, 20);
            this.nudEndLoop.TabIndex = 3;
            // 
            // lblEndLoop
            // 
            this.lblEndLoop.AutoSize = true;
            this.lblEndLoop.Location = new System.Drawing.Point(150, 21);
            this.lblEndLoop.Name = "lblEndLoop";
            this.lblEndLoop.Size = new System.Drawing.Size(29, 13);
            this.lblEndLoop.TabIndex = 2;
            this.lblEndLoop.Text = "End:";
            // 
            // nudStartLoop
            // 
            this.nudStartLoop.Location = new System.Drawing.Point(44, 19);
            this.nudStartLoop.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudStartLoop.Name = "nudStartLoop";
            this.nudStartLoop.Size = new System.Drawing.Size(100, 20);
            this.nudStartLoop.TabIndex = 1;
            // 
            // lblLoopStart
            // 
            this.lblLoopStart.AutoSize = true;
            this.lblLoopStart.Location = new System.Drawing.Point(6, 21);
            this.lblLoopStart.Name = "lblLoopStart";
            this.lblLoopStart.Size = new System.Drawing.Size(32, 13);
            this.lblLoopStart.TabIndex = 0;
            this.lblLoopStart.Text = "Start:";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(233, 100);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(152, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(9, 84);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(255, 13);
            this.lblNote.TabIndex = 2;
            this.lblNote.Text = "Note: The start and end loop must be set in samples.";
            // 
            // FrmWaveLoops
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(320, 135);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWaveLoops";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmWaveLoops";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWaveLoops_FormClosing);
            this.Load += new System.EventHandler(this.FrmWaveLoops_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEndLoop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartLoop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkToLastSample;
        private System.Windows.Forms.CheckBox chkLoopSettings;
        private System.Windows.Forms.NumericUpDown nudEndLoop;
        private System.Windows.Forms.Label lblEndLoop;
        private System.Windows.Forms.NumericUpDown nudStartLoop;
        private System.Windows.Forms.Label lblLoopStart;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNote;
    }
}
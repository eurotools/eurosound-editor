
namespace sb_editor.Forms
{
    partial class MultiCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiCreate));
            this.grbSamplesToUse = new System.Windows.Forms.GroupBox();
            this.lstSampleFiles = new System.Windows.Forms.ListBox();
            this.grbSFXs = new System.Windows.Forms.GroupBox();
            this.lstSfxNames = new System.Windows.Forms.ListBox();
            this.chkForceUpperCase = new System.Windows.Forms.CheckBox();
            this.chkRandomSeq = new System.Windows.Forms.CheckBox();
            this.txtHashCode_Prefix = new System.Windows.Forms.TextBox();
            this.lblPrefix_SFXs = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.grbSamplesToUse.SuspendLayout();
            this.grbSFXs.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSamplesToUse
            // 
            this.grbSamplesToUse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSamplesToUse.Controls.Add(this.lstSampleFiles);
            this.grbSamplesToUse.Location = new System.Drawing.Point(12, 5);
            this.grbSamplesToUse.Name = "grbSamplesToUse";
            this.grbSamplesToUse.Size = new System.Drawing.Size(450, 242);
            this.grbSamplesToUse.TabIndex = 0;
            this.grbSamplesToUse.TabStop = false;
            this.grbSamplesToUse.Text = "Sample Files To Use";
            // 
            // lstSampleFiles
            // 
            this.lstSampleFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSampleFiles.FormattingEnabled = true;
            this.lstSampleFiles.HorizontalScrollbar = true;
            this.lstSampleFiles.Location = new System.Drawing.Point(3, 16);
            this.lstSampleFiles.Name = "lstSampleFiles";
            this.lstSampleFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSampleFiles.Size = new System.Drawing.Size(444, 223);
            this.lstSampleFiles.TabIndex = 0;
            // 
            // grbSFXs
            // 
            this.grbSFXs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSFXs.Controls.Add(this.lstSfxNames);
            this.grbSFXs.Location = new System.Drawing.Point(12, 253);
            this.grbSFXs.Name = "grbSFXs";
            this.grbSFXs.Size = new System.Drawing.Size(450, 259);
            this.grbSFXs.TabIndex = 1;
            this.grbSFXs.TabStop = false;
            this.grbSFXs.Text = "SFX Names To Be Created";
            // 
            // lstSfxNames
            // 
            this.lstSfxNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSfxNames.FormattingEnabled = true;
            this.lstSfxNames.HorizontalScrollbar = true;
            this.lstSfxNames.Location = new System.Drawing.Point(3, 16);
            this.lstSfxNames.Name = "lstSfxNames";
            this.lstSfxNames.Size = new System.Drawing.Size(444, 240);
            this.lstSfxNames.TabIndex = 0;
            // 
            // chkForceUpperCase
            // 
            this.chkForceUpperCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkForceUpperCase.AutoSize = true;
            this.chkForceUpperCase.Location = new System.Drawing.Point(12, 518);
            this.chkForceUpperCase.Name = "chkForceUpperCase";
            this.chkForceUpperCase.Size = new System.Drawing.Size(112, 17);
            this.chkForceUpperCase.TabIndex = 2;
            this.chkForceUpperCase.Text = "Force Upper Case";
            this.chkForceUpperCase.UseVisualStyleBackColor = true;
            this.chkForceUpperCase.CheckStateChanged += new System.EventHandler(this.ChkForceUpperCase_CheckStateChanged);
            // 
            // chkRandomSeq
            // 
            this.chkRandomSeq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRandomSeq.AutoSize = true;
            this.chkRandomSeq.Location = new System.Drawing.Point(12, 541);
            this.chkRandomSeq.Name = "chkRandomSeq";
            this.chkRandomSeq.Size = new System.Drawing.Size(152, 17);
            this.chkRandomSeq.TabIndex = 3;
            this.chkRandomSeq.Text = "Create Random Sequence";
            this.chkRandomSeq.UseVisualStyleBackColor = true;
            this.chkRandomSeq.CheckedChanged += new System.EventHandler(this.ChkRandomSeq_CheckedChanged);
            // 
            // txtHashCode_Prefix
            // 
            this.txtHashCode_Prefix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHashCode_Prefix.Location = new System.Drawing.Point(315, 564);
            this.txtHashCode_Prefix.Name = "txtHashCode_Prefix";
            this.txtHashCode_Prefix.Size = new System.Drawing.Size(147, 20);
            this.txtHashCode_Prefix.TabIndex = 4;
            this.txtHashCode_Prefix.TextChanged += new System.EventHandler(this.TxtHashCode_Prefix_TextChanged);
            // 
            // lblPrefix_SFXs
            // 
            this.lblPrefix_SFXs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPrefix_SFXs.AutoSize = true;
            this.lblPrefix_SFXs.Location = new System.Drawing.Point(12, 567);
            this.lblPrefix_SFXs.Name = "lblPrefix_SFXs";
            this.lblPrefix_SFXs.Size = new System.Drawing.Size(115, 13);
            this.lblPrefix_SFXs.TabIndex = 5;
            this.lblPrefix_SFXs.Text = "Prefix SFX Labels with:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(12, 590);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(126, 590);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(108, 30);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Location = new System.Drawing.Point(240, 590);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(108, 30);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(354, 590);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(108, 30);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Wav Files (*.wav)|*.wav";
            this.openFileDialog.Multiselect = true;
            // 
            // MultiCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 632);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblPrefix_SFXs);
            this.Controls.Add(this.txtHashCode_Prefix);
            this.Controls.Add(this.chkRandomSeq);
            this.Controls.Add(this.chkForceUpperCase);
            this.Controls.Add(this.grbSFXs);
            this.Controls.Add(this.grbSamplesToUse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiCreate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create SFXs From Sample Files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_NewMultipleSfx_FormClosing);
            this.Load += new System.EventHandler(this.Frm_NewMultipleSfx_Load);
            this.grbSamplesToUse.ResumeLayout(false);
            this.grbSFXs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSamplesToUse;
        private System.Windows.Forms.ListBox lstSampleFiles;
        private System.Windows.Forms.GroupBox grbSFXs;
        private System.Windows.Forms.ListBox lstSfxNames;
        private System.Windows.Forms.CheckBox chkForceUpperCase;
        private System.Windows.Forms.CheckBox chkRandomSeq;
        private System.Windows.Forms.TextBox txtHashCode_Prefix;
        private System.Windows.Forms.Label lblPrefix_SFXs;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
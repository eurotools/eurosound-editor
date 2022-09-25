
namespace EuroSound_Editor.Forms
{
    partial class MusicApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicApp));
            this.grbAvailableMusicFiles = new System.Windows.Forms.GroupBox();
            this.nudUserValue = new System.Windows.Forms.NumericUpDown();
            this.btnVerifyHashCodes = new System.Windows.Forms.Button();
            this.btnForceOutput = new System.Windows.Forms.Button();
            this.btnForceSelected = new System.Windows.Forms.Button();
            this.btnViewErrorFile = new System.Windows.Forms.Button();
            this.nudVolume = new System.Windows.Forms.NumericUpDown();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnUpdateFiles = new System.Windows.Forms.Button();
            this.lvwMusicFiles = new EuroSound_Editor.Panels.ListView_ColumnSortingClick();
            this.Col_Musics_FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Musics_Volume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Musics_ErrorStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Musics_HashCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Musics_Marker = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Musics_Wav = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Musics_OutputFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Musics_UserValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grbOutputFile = new System.Windows.Forms.GroupBox();
            this.cboOutputFormat = new System.Windows.Forms.ComboBox();
            this.chkOutputOnlyMarkerFile = new System.Windows.Forms.CheckBox();
            this.txtOutputTime = new System.Windows.Forms.TextBox();
            this.btnRemapHashCodes = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grbAvailableMusicFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume)).BeginInit();
            this.grbOutputFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbAvailableMusicFiles
            // 
            this.grbAvailableMusicFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbAvailableMusicFiles.Controls.Add(this.nudUserValue);
            this.grbAvailableMusicFiles.Controls.Add(this.btnVerifyHashCodes);
            this.grbAvailableMusicFiles.Controls.Add(this.btnForceOutput);
            this.grbAvailableMusicFiles.Controls.Add(this.btnForceSelected);
            this.grbAvailableMusicFiles.Controls.Add(this.btnViewErrorFile);
            this.grbAvailableMusicFiles.Controls.Add(this.nudVolume);
            this.grbAvailableMusicFiles.Controls.Add(this.btnOutput);
            this.grbAvailableMusicFiles.Controls.Add(this.btnUpdateFiles);
            this.grbAvailableMusicFiles.Controls.Add(this.lvwMusicFiles);
            this.grbAvailableMusicFiles.Location = new System.Drawing.Point(12, 8);
            this.grbAvailableMusicFiles.Name = "grbAvailableMusicFiles";
            this.grbAvailableMusicFiles.Size = new System.Drawing.Size(766, 278);
            this.grbAvailableMusicFiles.TabIndex = 0;
            this.grbAvailableMusicFiles.TabStop = false;
            this.grbAvailableMusicFiles.Text = "Available Music Files";
            // 
            // nudUserValue
            // 
            this.nudUserValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudUserValue.Location = new System.Drawing.Point(666, 252);
            this.nudUserValue.Name = "nudUserValue";
            this.nudUserValue.Size = new System.Drawing.Size(75, 20);
            this.nudUserValue.TabIndex = 8;
            this.nudUserValue.ValueChanged += new System.EventHandler(this.NudUserValue_ValueChanged);
            // 
            // btnVerifyHashCodes
            // 
            this.btnVerifyHashCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVerifyHashCodes.Location = new System.Drawing.Point(574, 249);
            this.btnVerifyHashCodes.Name = "btnVerifyHashCodes";
            this.btnVerifyHashCodes.Size = new System.Drawing.Size(86, 23);
            this.btnVerifyHashCodes.TabIndex = 7;
            this.btnVerifyHashCodes.Text = "Verify MFX FIle";
            this.btnVerifyHashCodes.UseVisualStyleBackColor = true;
            this.btnVerifyHashCodes.Click += new System.EventHandler(this.BtnVerifyHashCodes_Click);
            // 
            // btnForceOutput
            // 
            this.btnForceOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnForceOutput.Location = new System.Drawing.Point(480, 249);
            this.btnForceOutput.Name = "btnForceOutput";
            this.btnForceOutput.Size = new System.Drawing.Size(88, 23);
            this.btnForceOutput.TabIndex = 6;
            this.btnForceOutput.Text = "Force Output";
            this.btnForceOutput.UseVisualStyleBackColor = true;
            this.btnForceOutput.Click += new System.EventHandler(this.BtnForceOutput_Click);
            // 
            // btnForceSelected
            // 
            this.btnForceSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnForceSelected.Location = new System.Drawing.Point(386, 249);
            this.btnForceSelected.Name = "btnForceSelected";
            this.btnForceSelected.Size = new System.Drawing.Size(88, 23);
            this.btnForceSelected.TabIndex = 5;
            this.btnForceSelected.Text = "Force Selected";
            this.btnForceSelected.UseVisualStyleBackColor = true;
            this.btnForceSelected.Click += new System.EventHandler(this.BtnForceSelected_Click);
            // 
            // btnViewErrorFile
            // 
            this.btnViewErrorFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnViewErrorFile.Location = new System.Drawing.Point(240, 249);
            this.btnViewErrorFile.Name = "btnViewErrorFile";
            this.btnViewErrorFile.Size = new System.Drawing.Size(89, 23);
            this.btnViewErrorFile.TabIndex = 4;
            this.btnViewErrorFile.Text = "View Error File";
            this.btnViewErrorFile.UseVisualStyleBackColor = true;
            this.btnViewErrorFile.Click += new System.EventHandler(this.BtnViewErrorFile_Click);
            // 
            // nudVolume
            // 
            this.nudVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudVolume.Location = new System.Drawing.Point(169, 252);
            this.nudVolume.Name = "nudVolume";
            this.nudVolume.Size = new System.Drawing.Size(65, 20);
            this.nudVolume.TabIndex = 3;
            this.nudVolume.ValueChanged += new System.EventHandler(this.NudVolume_ValueChanged);
            // 
            // btnOutput
            // 
            this.btnOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOutput.Location = new System.Drawing.Point(87, 249);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 2;
            this.btnOutput.Text = "Output";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.BtnOutput_Click);
            // 
            // btnUpdateFiles
            // 
            this.btnUpdateFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateFiles.Location = new System.Drawing.Point(6, 249);
            this.btnUpdateFiles.Name = "btnUpdateFiles";
            this.btnUpdateFiles.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateFiles.TabIndex = 1;
            this.btnUpdateFiles.Text = "Update";
            this.btnUpdateFiles.UseVisualStyleBackColor = true;
            this.btnUpdateFiles.Click += new System.EventHandler(this.BtnUpdateFiles_Click);
            // 
            // lvwMusicFiles
            // 
            this.lvwMusicFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMusicFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Musics_FileName,
            this.Col_Musics_Volume,
            this.Col_Musics_ErrorStatus,
            this.Col_Musics_HashCode,
            this.Col_Musics_Marker,
            this.Col_Musics_Wav,
            this.Col_Musics_OutputFileName,
            this.Col_Musics_UserValue});
            this.lvwMusicFiles.FullRowSelect = true;
            this.lvwMusicFiles.GridLines = true;
            this.lvwMusicFiles.HideSelection = false;
            this.lvwMusicFiles.Location = new System.Drawing.Point(6, 19);
            this.lvwMusicFiles.Name = "lvwMusicFiles";
            this.lvwMusicFiles.Size = new System.Drawing.Size(754, 224);
            this.lvwMusicFiles.TabIndex = 0;
            this.lvwMusicFiles.UseCompatibleStateImageBehavior = false;
            this.lvwMusicFiles.View = System.Windows.Forms.View.Details;
            this.lvwMusicFiles.SelectedIndexChanged += new System.EventHandler(this.LvwMusicFiles_SelectedIndexChanged);
            // 
            // Col_Musics_FileName
            // 
            this.Col_Musics_FileName.Text = "File Name";
            this.Col_Musics_FileName.Width = 160;
            // 
            // Col_Musics_Volume
            // 
            this.Col_Musics_Volume.Text = "Volume";
            this.Col_Musics_Volume.Width = 55;
            // 
            // Col_Musics_ErrorStatus
            // 
            this.Col_Musics_ErrorStatus.Text = "Error Status";
            this.Col_Musics_ErrorStatus.Width = 105;
            // 
            // Col_Musics_HashCode
            // 
            this.Col_Musics_HashCode.Text = "HashCode";
            this.Col_Musics_HashCode.Width = 70;
            // 
            // Col_Musics_Marker
            // 
            this.Col_Musics_Marker.Text = "Marker";
            this.Col_Musics_Marker.Width = 80;
            // 
            // Col_Musics_Wav
            // 
            this.Col_Musics_Wav.Text = "Wav";
            this.Col_Musics_Wav.Width = 80;
            // 
            // Col_Musics_OutputFileName
            // 
            this.Col_Musics_OutputFileName.Text = "Output Filename";
            this.Col_Musics_OutputFileName.Width = 100;
            // 
            // Col_Musics_UserValue
            // 
            this.Col_Musics_UserValue.Text = "User Value";
            this.Col_Musics_UserValue.Width = 80;
            // 
            // grbOutputFile
            // 
            this.grbOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbOutputFile.Controls.Add(this.cboOutputFormat);
            this.grbOutputFile.Location = new System.Drawing.Point(12, 293);
            this.grbOutputFile.Name = "grbOutputFile";
            this.grbOutputFile.Size = new System.Drawing.Size(234, 48);
            this.grbOutputFile.TabIndex = 1;
            this.grbOutputFile.TabStop = false;
            this.grbOutputFile.Text = "Output This Format Only";
            // 
            // cboOutputFormat
            // 
            this.cboOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutputFormat.FormattingEnabled = true;
            this.cboOutputFormat.Items.AddRange(new object[] {
            "PlayStation2",
            "GameCube",
            "PC",
            "X Box",
            "All"});
            this.cboOutputFormat.Location = new System.Drawing.Point(6, 16);
            this.cboOutputFormat.Name = "cboOutputFormat";
            this.cboOutputFormat.Size = new System.Drawing.Size(222, 21);
            this.cboOutputFormat.TabIndex = 0;
            // 
            // chkOutputOnlyMarkerFile
            // 
            this.chkOutputOnlyMarkerFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkOutputOnlyMarkerFile.AutoSize = true;
            this.chkOutputOnlyMarkerFile.Location = new System.Drawing.Point(252, 298);
            this.chkOutputOnlyMarkerFile.Name = "chkOutputOnlyMarkerFile";
            this.chkOutputOnlyMarkerFile.Size = new System.Drawing.Size(102, 17);
            this.chkOutputOnlyMarkerFile.TabIndex = 2;
            this.chkOutputOnlyMarkerFile.Text = "Marker File Only";
            this.chkOutputOnlyMarkerFile.UseVisualStyleBackColor = true;
            // 
            // txtOutputTime
            // 
            this.txtOutputTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOutputTime.Location = new System.Drawing.Point(252, 321);
            this.txtOutputTime.Name = "txtOutputTime";
            this.txtOutputTime.Size = new System.Drawing.Size(248, 20);
            this.txtOutputTime.TabIndex = 3;
            // 
            // btnRemapHashCodes
            // 
            this.btnRemapHashCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemapHashCodes.Location = new System.Drawing.Point(360, 292);
            this.btnRemapHashCodes.Name = "btnRemapHashCodes";
            this.btnRemapHashCodes.Size = new System.Drawing.Size(140, 23);
            this.btnRemapHashCodes.TabIndex = 4;
            this.btnRemapHashCodes.Text = "ReMap HashCodes";
            this.btnRemapHashCodes.UseVisualStyleBackColor = true;
            this.btnRemapHashCodes.Click += new System.EventHandler(this.BtnRemapHashCodes_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(703, 318);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // MusicApp
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 353);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRemapHashCodes);
            this.Controls.Add(this.txtOutputTime);
            this.Controls.Add(this.chkOutputOnlyMarkerFile);
            this.Controls.Add(this.grbOutputFile);
            this.Controls.Add(this.grbAvailableMusicFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MusicApp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Music Maker";
            this.Load += new System.EventHandler(this.Frm_MusicMaker_Load);
            this.grbAvailableMusicFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudUserValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume)).EndInit();
            this.grbOutputFile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbAvailableMusicFiles;
        private System.Windows.Forms.NumericUpDown nudUserValue;
        private System.Windows.Forms.Button btnVerifyHashCodes;
        private System.Windows.Forms.Button btnForceOutput;
        private System.Windows.Forms.Button btnForceSelected;
        private System.Windows.Forms.Button btnViewErrorFile;
        private System.Windows.Forms.NumericUpDown nudVolume;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnUpdateFiles;
        private System.Windows.Forms.ColumnHeader Col_Musics_FileName;
        private System.Windows.Forms.ColumnHeader Col_Musics_Volume;
        private System.Windows.Forms.ColumnHeader Col_Musics_ErrorStatus;
        private System.Windows.Forms.ColumnHeader Col_Musics_HashCode;
        private System.Windows.Forms.ColumnHeader Col_Musics_Marker;
        private System.Windows.Forms.ColumnHeader Col_Musics_Wav;
        private System.Windows.Forms.ColumnHeader Col_Musics_OutputFileName;
        private System.Windows.Forms.ColumnHeader Col_Musics_UserValue;
        private System.Windows.Forms.GroupBox grbOutputFile;
        private System.Windows.Forms.ComboBox cboOutputFormat;
        private System.Windows.Forms.Button btnRemapHashCodes;
        private System.Windows.Forms.Button btnOK;
        protected internal System.Windows.Forms.CheckBox chkOutputOnlyMarkerFile;
        protected internal Panels.ListView_ColumnSortingClick lvwMusicFiles;
        protected internal System.Windows.Forms.TextBox txtOutputTime;
    }
}
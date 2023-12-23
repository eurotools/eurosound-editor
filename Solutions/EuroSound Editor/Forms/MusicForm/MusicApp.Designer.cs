
namespace sb_editor.Forms
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
            this.lvwMusicFiles = new sb_editor.Panels.ListView_ColumnSortingClick();
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
            this.grbxJumpMarkers = new System.Windows.Forms.GroupBox();
            this.lstbx_JumpMakers = new System.Windows.Forms.ListBox();
            this.grbxConsoleControl = new System.Windows.Forms.GroupBox();
            this.btnRunTarget = new System.Windows.Forms.Button();
            this.btnResetTarget = new System.Windows.Forms.Button();
            this.grbx_TestOptions = new System.Windows.Forms.GroupBox();
            this.grbxVolume = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnJump = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.grbAvailableMusicFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume)).BeginInit();
            this.grbOutputFile.SuspendLayout();
            this.grbxJumpMarkers.SuspendLayout();
            this.grbxConsoleControl.SuspendLayout();
            this.grbx_TestOptions.SuspendLayout();
            this.grbxVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // grbAvailableMusicFiles
            // 
            this.grbAvailableMusicFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
            this.cboOutputFormat.Location = new System.Drawing.Point(6, 16);
            this.cboOutputFormat.Name = "cboOutputFormat";
            this.cboOutputFormat.Size = new System.Drawing.Size(222, 21);
            this.cboOutputFormat.TabIndex = 0;
            // 
            // chkOutputOnlyMarkerFile
            // 
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
            this.txtOutputTime.Location = new System.Drawing.Point(252, 321);
            this.txtOutputTime.Name = "txtOutputTime";
            this.txtOutputTime.Size = new System.Drawing.Size(248, 20);
            this.txtOutputTime.TabIndex = 3;
            // 
            // btnRemapHashCodes
            // 
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
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(703, 318);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // grbxJumpMarkers
            // 
            this.grbxJumpMarkers.Controls.Add(this.lstbx_JumpMakers);
            this.grbxJumpMarkers.Location = new System.Drawing.Point(12, 364);
            this.grbxJumpMarkers.Name = "grbxJumpMarkers";
            this.grbxJumpMarkers.Size = new System.Drawing.Size(261, 264);
            this.grbxJumpMarkers.TabIndex = 6;
            this.grbxJumpMarkers.TabStop = false;
            this.grbxJumpMarkers.Text = "Jump Markers";
            // 
            // lstbx_JumpMakers
            // 
            this.lstbx_JumpMakers.DisplayMember = "Name";
            this.lstbx_JumpMakers.FormattingEnabled = true;
            this.lstbx_JumpMakers.Location = new System.Drawing.Point(6, 19);
            this.lstbx_JumpMakers.Name = "lstbx_JumpMakers";
            this.lstbx_JumpMakers.Size = new System.Drawing.Size(249, 238);
            this.lstbx_JumpMakers.TabIndex = 0;
            // 
            // grbxConsoleControl
            // 
            this.grbxConsoleControl.Controls.Add(this.btnRunTarget);
            this.grbxConsoleControl.Controls.Add(this.btnResetTarget);
            this.grbxConsoleControl.Location = new System.Drawing.Point(279, 364);
            this.grbxConsoleControl.Name = "grbxConsoleControl";
            this.grbxConsoleControl.Size = new System.Drawing.Size(188, 53);
            this.grbxConsoleControl.TabIndex = 7;
            this.grbxConsoleControl.TabStop = false;
            this.grbxConsoleControl.Text = "Console Control";
            // 
            // btnRunTarget
            // 
            this.btnRunTarget.Location = new System.Drawing.Point(97, 19);
            this.btnRunTarget.Name = "btnRunTarget";
            this.btnRunTarget.Size = new System.Drawing.Size(85, 23);
            this.btnRunTarget.TabIndex = 1;
            this.btnRunTarget.Text = "Run Target";
            this.btnRunTarget.UseVisualStyleBackColor = true;
            this.btnRunTarget.Click += new System.EventHandler(this.BtnRunTarget_Click);
            // 
            // btnResetTarget
            // 
            this.btnResetTarget.Location = new System.Drawing.Point(6, 19);
            this.btnResetTarget.Name = "btnResetTarget";
            this.btnResetTarget.Size = new System.Drawing.Size(85, 23);
            this.btnResetTarget.TabIndex = 0;
            this.btnResetTarget.Text = "Reset Target";
            this.btnResetTarget.UseVisualStyleBackColor = true;
            // 
            // grbx_TestOptions
            // 
            this.grbx_TestOptions.Controls.Add(this.grbxVolume);
            this.grbx_TestOptions.Controls.Add(this.btnStop);
            this.grbx_TestOptions.Controls.Add(this.btnJump);
            this.grbx_TestOptions.Controls.Add(this.btnPause);
            this.grbx_TestOptions.Controls.Add(this.btnPlay);
            this.grbx_TestOptions.Location = new System.Drawing.Point(279, 423);
            this.grbx_TestOptions.Name = "grbx_TestOptions";
            this.grbx_TestOptions.Size = new System.Drawing.Size(200, 152);
            this.grbx_TestOptions.TabIndex = 8;
            this.grbx_TestOptions.TabStop = false;
            this.grbx_TestOptions.Text = "Test Options";
            // 
            // grbxVolume
            // 
            this.grbxVolume.Controls.Add(this.trackBar1);
            this.grbxVolume.Location = new System.Drawing.Point(6, 77);
            this.grbxVolume.Name = "grbxVolume";
            this.grbxVolume.Size = new System.Drawing.Size(188, 67);
            this.grbxVolume.TabIndex = 6;
            this.grbxVolume.TabStop = false;
            this.grbxVolume.Text = "Volume";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 16);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(176, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickFrequency = 10;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(87, 48);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnJump
            // 
            this.btnJump.Location = new System.Drawing.Point(6, 48);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(75, 23);
            this.btnJump.TabIndex = 2;
            this.btnJump.Text = "Jump";
            this.btnJump.UseVisualStyleBackColor = true;
            this.btnJump.Click += new System.EventHandler(this.BtnJump_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(87, 19);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.BtnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(6, 19);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // MusicApp
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 640);
            this.Controls.Add(this.grbx_TestOptions);
            this.Controls.Add(this.grbxConsoleControl);
            this.Controls.Add(this.grbxJumpMarkers);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MusicApp_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MusicMaker_Load);
            this.grbAvailableMusicFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudUserValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume)).EndInit();
            this.grbOutputFile.ResumeLayout(false);
            this.grbxJumpMarkers.ResumeLayout(false);
            this.grbxConsoleControl.ResumeLayout(false);
            this.grbx_TestOptions.ResumeLayout(false);
            this.grbxVolume.ResumeLayout(false);
            this.grbxVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
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
        private System.Windows.Forms.GroupBox grbxJumpMarkers;
        private System.Windows.Forms.ListBox lstbx_JumpMakers;
        private System.Windows.Forms.GroupBox grbxConsoleControl;
        private System.Windows.Forms.Button btnRunTarget;
        private System.Windows.Forms.Button btnResetTarget;
        private System.Windows.Forms.GroupBox grbx_TestOptions;
        private System.Windows.Forms.GroupBox grbxVolume;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnJump;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
    }
}
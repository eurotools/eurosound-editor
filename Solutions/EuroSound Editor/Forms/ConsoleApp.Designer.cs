﻿
namespace sb_editor.Forms
{
    partial class ConsoleApp
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartSFX = new System.Windows.Forms.Button();
            this.btnStopSFX = new System.Windows.Forms.Button();
            this.nudHashCode = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudZ = new System.Windows.Forms.NumericUpDown();
            this.labelListZ = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.labelListY = new System.Windows.Forms.Label();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.labelListX = new System.Windows.Forms.Label();
            this.btnStart3dSound = new System.Windows.Forms.Button();
            this.grbxSfxPlay = new System.Windows.Forms.GroupBox();
            this.nudHashCodePlaying = new System.Windows.Forms.NumericUpDown();
            this.btnPlaySample = new System.Windows.Forms.Button();
            this.grbxSamplePlay = new System.Windows.Forms.GroupBox();
            this.chkStreamingTest = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nudOuterRadius = new System.Windows.Forms.NumericUpDown();
            this.nudInnerRadius = new System.Windows.Forms.NumericUpDown();
            this.lstbSamples = new System.Windows.Forms.ListBox();
            this.grbxRandomTest = new System.Windows.Forms.GroupBox();
            this.btnStopRandomTest = new System.Windows.Forms.Button();
            this.btnStartRandomTest = new System.Windows.Forms.Button();
            this.lstBox_SFXs = new System.Windows.Forms.ListBox();
            this.grbxAvailableSoundBanks = new System.Windows.Forms.GroupBox();
            this.btnLoadSoundbanks = new System.Windows.Forms.Button();
            this.lstbAvailableSoundBanks = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeLoadSoundBanks = new System.Windows.Forms.Button();
            this.lstbLoadedSoundBanks = new System.Windows.Forms.ListBox();
            this.grbxTestPostVel = new System.Windows.Forms.GroupBox();
            this.nudScale = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chxDrawCircle = new System.Windows.Forms.CheckBox();
            this.chxTestPan = new System.Windows.Forms.CheckBox();
            this.btnTestRight = new System.Windows.Forms.Button();
            this.btnTestLeft = new System.Windows.Forms.Button();
            this.btnResetPos = new System.Windows.Forms.Button();
            this.picBox_XY = new System.Windows.Forms.PictureBox();
            this.picBox_ZX = new System.Windows.Forms.PictureBox();
            this.btnOkey = new System.Windows.Forms.Button();
            this.grbxSFXsFolderPath = new System.Windows.Forms.GroupBox();
            this.grbxSoundDetails = new System.Windows.Forms.GroupBox();
            this.btnSearchSoundDetails = new System.Windows.Forms.Button();
            this.txtSoundDetails = new System.Windows.Forms.TextBox();
            this.btnSearchOutPath = new System.Windows.Forms.Button();
            this.txtSoundBankFile = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grbxTestingGroup = new System.Windows.Forms.GroupBox();
            this.rbtn_Xbox = new System.Windows.Forms.RadioButton();
            this.rbtn_GameCube = new System.Windows.Forms.RadioButton();
            this.rbtn_PS2 = new System.Windows.Forms.RadioButton();
            this.rbtn_PC = new System.Windows.Forms.RadioButton();
            this.btnSfxReset = new System.Windows.Forms.Button();
            this.btnStopAllSFXs = new System.Windows.Forms.Button();
            this.trckBarMasterVolume = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_musicApp = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nudHashCode)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            this.grbxSfxPlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHashCodePlaying)).BeginInit();
            this.grbxSamplePlay.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOuterRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInnerRadius)).BeginInit();
            this.grbxRandomTest.SuspendLayout();
            this.grbxAvailableSoundBanks.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grbxTestPostVel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_XY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_ZX)).BeginInit();
            this.grbxSFXsFolderPath.SuspendLayout();
            this.grbxSoundDetails.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grbxTestingGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckBarMasterVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartSFX
            // 
            this.btnStartSFX.Location = new System.Drawing.Point(92, 19);
            this.btnStartSFX.Name = "btnStartSFX";
            this.btnStartSFX.Size = new System.Drawing.Size(75, 23);
            this.btnStartSFX.TabIndex = 1;
            this.btnStartSFX.Text = "SFX";
            this.btnStartSFX.UseVisualStyleBackColor = true;
            this.btnStartSFX.Click += new System.EventHandler(this.BtnStartSFX_Click);
            // 
            // btnStopSFX
            // 
            this.btnStopSFX.Location = new System.Drawing.Point(173, 19);
            this.btnStopSFX.Name = "btnStopSFX";
            this.btnStopSFX.Size = new System.Drawing.Size(75, 23);
            this.btnStopSFX.TabIndex = 2;
            this.btnStopSFX.Text = "Stop SFX";
            this.btnStopSFX.UseVisualStyleBackColor = true;
            this.btnStopSFX.Click += new System.EventHandler(this.BtnStopSFX_Click);
            // 
            // nudHashCode
            // 
            this.nudHashCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudHashCode.Hexadecimal = true;
            this.nudHashCode.Location = new System.Drawing.Point(57, 423);
            this.nudHashCode.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudHashCode.Name = "nudHashCode";
            this.nudHashCode.Size = new System.Drawing.Size(95, 20);
            this.nudHashCode.TabIndex = 0;
            this.nudHashCode.Value = new decimal(new int[] {
            436207636,
            0,
            0,
            0});
            this.nudHashCode.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.nudZ);
            this.groupBox1.Controls.Add(this.labelListZ);
            this.groupBox1.Controls.Add(this.nudY);
            this.groupBox1.Controls.Add(this.labelListY);
            this.groupBox1.Controls.Add(this.nudX);
            this.groupBox1.Controls.Add(this.labelListX);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 48);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
            // 
            // nudZ
            // 
            this.nudZ.DecimalPlaces = 6;
            this.nudZ.Location = new System.Drawing.Point(325, 19);
            this.nudZ.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nudZ.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nudZ.Name = "nudZ";
            this.nudZ.Size = new System.Drawing.Size(120, 20);
            this.nudZ.TabIndex = 5;
            this.nudZ.ValueChanged += new System.EventHandler(this.UpdatePictureBoxes);
            // 
            // labelListZ
            // 
            this.labelListZ.AutoSize = true;
            this.labelListZ.Location = new System.Drawing.Point(302, 21);
            this.labelListZ.Name = "labelListZ";
            this.labelListZ.Size = new System.Drawing.Size(17, 13);
            this.labelListZ.TabIndex = 4;
            this.labelListZ.Text = "Z:";
            // 
            // nudY
            // 
            this.nudY.DecimalPlaces = 6;
            this.nudY.Location = new System.Drawing.Point(176, 19);
            this.nudY.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nudY.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(120, 20);
            this.nudY.TabIndex = 3;
            this.nudY.ValueChanged += new System.EventHandler(this.UpdatePictureBoxes);
            // 
            // labelListY
            // 
            this.labelListY.AutoSize = true;
            this.labelListY.Location = new System.Drawing.Point(155, 21);
            this.labelListY.Name = "labelListY";
            this.labelListY.Size = new System.Drawing.Size(17, 13);
            this.labelListY.TabIndex = 2;
            this.labelListY.Text = "Y:";
            // 
            // nudX
            // 
            this.nudX.DecimalPlaces = 6;
            this.nudX.Location = new System.Drawing.Point(29, 19);
            this.nudX.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nudX.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(120, 20);
            this.nudX.TabIndex = 1;
            this.nudX.ValueChanged += new System.EventHandler(this.UpdatePictureBoxes);
            // 
            // labelListX
            // 
            this.labelListX.AutoSize = true;
            this.labelListX.Location = new System.Drawing.Point(6, 21);
            this.labelListX.Name = "labelListX";
            this.labelListX.Size = new System.Drawing.Size(17, 13);
            this.labelListX.TabIndex = 0;
            this.labelListX.Text = "X:";
            // 
            // btnStart3dSound
            // 
            this.btnStart3dSound.Location = new System.Drawing.Point(11, 19);
            this.btnStart3dSound.Name = "btnStart3dSound";
            this.btnStart3dSound.Size = new System.Drawing.Size(75, 23);
            this.btnStart3dSound.TabIndex = 4;
            this.btnStart3dSound.Text = "SFX 3d";
            this.btnStart3dSound.UseVisualStyleBackColor = true;
            this.btnStart3dSound.Click += new System.EventHandler(this.BtnStart3dSound_Click);
            // 
            // grbxSfxPlay
            // 
            this.grbxSfxPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbxSfxPlay.Controls.Add(this.nudHashCodePlaying);
            this.grbxSfxPlay.Controls.Add(this.btnStart3dSound);
            this.grbxSfxPlay.Controls.Add(this.btnStartSFX);
            this.grbxSfxPlay.Controls.Add(this.btnStopSFX);
            this.grbxSfxPlay.Location = new System.Drawing.Point(6, 479);
            this.grbxSfxPlay.Name = "grbxSfxPlay";
            this.grbxSfxPlay.Size = new System.Drawing.Size(266, 61);
            this.grbxSfxPlay.TabIndex = 5;
            this.grbxSfxPlay.TabStop = false;
            this.grbxSfxPlay.Text = "SFX Play";
            // 
            // nudHashCodePlaying
            // 
            this.nudHashCodePlaying.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudHashCodePlaying.Hexadecimal = true;
            this.nudHashCodePlaying.Location = new System.Drawing.Point(185, 41);
            this.nudHashCodePlaying.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudHashCodePlaying.Name = "nudHashCodePlaying";
            this.nudHashCodePlaying.Size = new System.Drawing.Size(95, 20);
            this.nudHashCodePlaying.TabIndex = 16;
            this.nudHashCodePlaying.Value = new decimal(new int[] {
            436207636,
            0,
            0,
            0});
            this.nudHashCodePlaying.Visible = false;
            // 
            // btnPlaySample
            // 
            this.btnPlaySample.Location = new System.Drawing.Point(6, 19);
            this.btnPlaySample.Name = "btnPlaySample";
            this.btnPlaySample.Size = new System.Drawing.Size(75, 23);
            this.btnPlaySample.TabIndex = 6;
            this.btnPlaySample.Text = "Play Sample";
            this.btnPlaySample.UseVisualStyleBackColor = true;
            this.btnPlaySample.Click += new System.EventHandler(this.BtnPlaySample_Click);
            // 
            // grbxSamplePlay
            // 
            this.grbxSamplePlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbxSamplePlay.Controls.Add(this.btnPlaySample);
            this.grbxSamplePlay.Location = new System.Drawing.Point(278, 479);
            this.grbxSamplePlay.Name = "grbxSamplePlay";
            this.grbxSamplePlay.Size = new System.Drawing.Size(106, 61);
            this.grbxSamplePlay.TabIndex = 7;
            this.grbxSamplePlay.TabStop = false;
            this.grbxSamplePlay.Text = "Sample Play";
            // 
            // chkStreamingTest
            // 
            this.chkStreamingTest.AutoSize = true;
            this.chkStreamingTest.Location = new System.Drawing.Point(12, 12);
            this.chkStreamingTest.Name = "chkStreamingTest";
            this.chkStreamingTest.Size = new System.Drawing.Size(102, 17);
            this.chkStreamingTest.TabIndex = 0;
            this.chkStreamingTest.Text = "Streaming Tests";
            this.chkStreamingTest.UseVisualStyleBackColor = true;
            this.chkStreamingTest.CheckedChanged += new System.EventHandler(this.ChkStreamingTest_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.nudOuterRadius);
            this.groupBox4.Controls.Add(this.nudInnerRadius);
            this.groupBox4.Controls.Add(this.lstbSamples);
            this.groupBox4.Controls.Add(this.grbxRandomTest);
            this.groupBox4.Controls.Add(this.lstBox_SFXs);
            this.groupBox4.Controls.Add(this.grbxSfxPlay);
            this.groupBox4.Controls.Add(this.nudHashCode);
            this.groupBox4.Controls.Add(this.grbxSamplePlay);
            this.groupBox4.Location = new System.Drawing.Point(12, 35);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(390, 546);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Play SFX or Sample For Selected Bank";
            // 
            // nudOuterRadius
            // 
            this.nudOuterRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudOuterRadius.Location = new System.Drawing.Point(6, 442);
            this.nudOuterRadius.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudOuterRadius.Name = "nudOuterRadius";
            this.nudOuterRadius.Size = new System.Drawing.Size(45, 20);
            this.nudOuterRadius.TabIndex = 15;
            this.nudOuterRadius.Visible = false;
            this.nudOuterRadius.ValueChanged += new System.EventHandler(this.UpdatePictureBoxes);
            // 
            // nudInnerRadius
            // 
            this.nudInnerRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudInnerRadius.Location = new System.Drawing.Point(6, 423);
            this.nudInnerRadius.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudInnerRadius.Name = "nudInnerRadius";
            this.nudInnerRadius.Size = new System.Drawing.Size(45, 20);
            this.nudInnerRadius.TabIndex = 14;
            this.nudInnerRadius.Visible = false;
            this.nudInnerRadius.ValueChanged += new System.EventHandler(this.UpdatePictureBoxes);
            // 
            // lstbSamples
            // 
            this.lstbSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbSamples.FormattingEnabled = true;
            this.lstbSamples.HorizontalScrollbar = true;
            this.lstbSamples.Location = new System.Drawing.Point(278, 19);
            this.lstbSamples.Name = "lstbSamples";
            this.lstbSamples.Size = new System.Drawing.Size(106, 381);
            this.lstbSamples.TabIndex = 9;
            // 
            // grbxRandomTest
            // 
            this.grbxRandomTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbxRandomTest.Controls.Add(this.btnStopRandomTest);
            this.grbxRandomTest.Controls.Add(this.btnStartRandomTest);
            this.grbxRandomTest.Location = new System.Drawing.Point(152, 423);
            this.grbxRandomTest.Name = "grbxRandomTest";
            this.grbxRandomTest.Size = new System.Drawing.Size(120, 50);
            this.grbxRandomTest.TabIndex = 8;
            this.grbxRandomTest.TabStop = false;
            this.grbxRandomTest.Text = "Random Tests";
            // 
            // btnStopRandomTest
            // 
            this.btnStopRandomTest.Location = new System.Drawing.Point(62, 19);
            this.btnStopRandomTest.Name = "btnStopRandomTest";
            this.btnStopRandomTest.Size = new System.Drawing.Size(50, 23);
            this.btnStopRandomTest.TabIndex = 11;
            this.btnStopRandomTest.Text = "Stop";
            this.btnStopRandomTest.UseVisualStyleBackColor = true;
            this.btnStopRandomTest.Click += new System.EventHandler(this.BtnStopRandomTest_Click);
            // 
            // btnStartRandomTest
            // 
            this.btnStartRandomTest.Location = new System.Drawing.Point(6, 19);
            this.btnStartRandomTest.Name = "btnStartRandomTest";
            this.btnStartRandomTest.Size = new System.Drawing.Size(50, 23);
            this.btnStartRandomTest.TabIndex = 10;
            this.btnStartRandomTest.Text = "Start";
            this.btnStartRandomTest.UseVisualStyleBackColor = true;
            this.btnStartRandomTest.Click += new System.EventHandler(this.BtnStartRandomTest_Click);
            // 
            // lstBox_SFXs
            // 
            this.lstBox_SFXs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstBox_SFXs.FormattingEnabled = true;
            this.lstBox_SFXs.HorizontalScrollbar = true;
            this.lstBox_SFXs.Location = new System.Drawing.Point(6, 19);
            this.lstBox_SFXs.Name = "lstBox_SFXs";
            this.lstBox_SFXs.Size = new System.Drawing.Size(266, 381);
            this.lstBox_SFXs.Sorted = true;
            this.lstBox_SFXs.TabIndex = 0;
            this.lstBox_SFXs.SelectedIndexChanged += new System.EventHandler(this.LstBox_SFXs_SelectedIndexChanged);
            // 
            // grbxAvailableSoundBanks
            // 
            this.grbxAvailableSoundBanks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbxAvailableSoundBanks.Controls.Add(this.btnLoadSoundbanks);
            this.grbxAvailableSoundBanks.Controls.Add(this.lstbAvailableSoundBanks);
            this.grbxAvailableSoundBanks.Location = new System.Drawing.Point(408, 12);
            this.grbxAvailableSoundBanks.Name = "grbxAvailableSoundBanks";
            this.grbxAvailableSoundBanks.Size = new System.Drawing.Size(253, 269);
            this.grbxAvailableSoundBanks.TabIndex = 4;
            this.grbxAvailableSoundBanks.TabStop = false;
            this.grbxAvailableSoundBanks.Text = "All Available SoundBanks";
            // 
            // btnLoadSoundbanks
            // 
            this.btnLoadSoundbanks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadSoundbanks.Location = new System.Drawing.Point(6, 240);
            this.btnLoadSoundbanks.Name = "btnLoadSoundbanks";
            this.btnLoadSoundbanks.Size = new System.Drawing.Size(241, 23);
            this.btnLoadSoundbanks.TabIndex = 11;
            this.btnLoadSoundbanks.Text = "Load SoundBanks >>>";
            this.btnLoadSoundbanks.UseVisualStyleBackColor = true;
            this.btnLoadSoundbanks.Click += new System.EventHandler(this.BtnLoadSoundbanks_Click);
            // 
            // lstbAvailableSoundBanks
            // 
            this.lstbAvailableSoundBanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbAvailableSoundBanks.FormattingEnabled = true;
            this.lstbAvailableSoundBanks.HorizontalScrollbar = true;
            this.lstbAvailableSoundBanks.Location = new System.Drawing.Point(6, 19);
            this.lstbAvailableSoundBanks.Name = "lstbAvailableSoundBanks";
            this.lstbAvailableSoundBanks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstbAvailableSoundBanks.Size = new System.Drawing.Size(241, 212);
            this.lstbAvailableSoundBanks.Sorted = true;
            this.lstbAvailableSoundBanks.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnDeLoadSoundBanks);
            this.groupBox2.Controls.Add(this.lstbLoadedSoundBanks);
            this.groupBox2.Location = new System.Drawing.Point(667, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 269);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Currently Loaded SoundBanks";
            // 
            // btnDeLoadSoundBanks
            // 
            this.btnDeLoadSoundBanks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeLoadSoundBanks.Location = new System.Drawing.Point(6, 240);
            this.btnDeLoadSoundBanks.Name = "btnDeLoadSoundBanks";
            this.btnDeLoadSoundBanks.Size = new System.Drawing.Size(239, 23);
            this.btnDeLoadSoundBanks.TabIndex = 12;
            this.btnDeLoadSoundBanks.Text = "<<< De-Load SoundBank";
            this.btnDeLoadSoundBanks.UseVisualStyleBackColor = true;
            this.btnDeLoadSoundBanks.Click += new System.EventHandler(this.BtnDeLoadSoundBanks_Click);
            // 
            // lstbLoadedSoundBanks
            // 
            this.lstbLoadedSoundBanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbLoadedSoundBanks.FormattingEnabled = true;
            this.lstbLoadedSoundBanks.HorizontalScrollbar = true;
            this.lstbLoadedSoundBanks.Location = new System.Drawing.Point(6, 19);
            this.lstbLoadedSoundBanks.Name = "lstbLoadedSoundBanks";
            this.lstbLoadedSoundBanks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstbLoadedSoundBanks.Size = new System.Drawing.Size(239, 212);
            this.lstbLoadedSoundBanks.TabIndex = 11;
            // 
            // grbxTestPostVel
            // 
            this.grbxTestPostVel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbxTestPostVel.Controls.Add(this.nudScale);
            this.grbxTestPostVel.Controls.Add(this.label1);
            this.grbxTestPostVel.Controls.Add(this.chxDrawCircle);
            this.grbxTestPostVel.Controls.Add(this.chxTestPan);
            this.grbxTestPostVel.Controls.Add(this.btnTestRight);
            this.grbxTestPostVel.Controls.Add(this.btnTestLeft);
            this.grbxTestPostVel.Controls.Add(this.btnResetPos);
            this.grbxTestPostVel.Controls.Add(this.picBox_XY);
            this.grbxTestPostVel.Controls.Add(this.picBox_ZX);
            this.grbxTestPostVel.Controls.Add(this.groupBox1);
            this.grbxTestPostVel.Location = new System.Drawing.Point(408, 287);
            this.grbxTestPostVel.Name = "grbxTestPostVel";
            this.grbxTestPostVel.Size = new System.Drawing.Size(510, 472);
            this.grbxTestPostVel.TabIndex = 6;
            this.grbxTestPostVel.TabStop = false;
            this.grbxTestPostVel.Text = "Test Position and Velocity";
            // 
            // nudScale
            // 
            this.nudScale.DecimalPlaces = 2;
            this.nudScale.Location = new System.Drawing.Point(432, 446);
            this.nudScale.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudScale.Name = "nudScale";
            this.nudScale.Size = new System.Drawing.Size(72, 20);
            this.nudScale.TabIndex = 16;
            this.nudScale.Value = new decimal(new int[] {
            79,
            0,
            0,
            65536});
            this.nudScale.ValueChanged += new System.EventHandler(this.UpdatePictureBoxes);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 448);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Zoom:";
            // 
            // chxDrawCircle
            // 
            this.chxDrawCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chxDrawCircle.AutoSize = true;
            this.chxDrawCircle.Location = new System.Drawing.Point(331, 447);
            this.chxDrawCircle.Name = "chxDrawCircle";
            this.chxDrawCircle.Size = new System.Drawing.Size(52, 17);
            this.chxDrawCircle.TabIndex = 10;
            this.chxDrawCircle.Text = "Circle";
            this.chxDrawCircle.UseVisualStyleBackColor = true;
            this.chxDrawCircle.CheckedChanged += new System.EventHandler(this.ChxDrawCircle_CheckedChanged);
            // 
            // chxTestPan
            // 
            this.chxTestPan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chxTestPan.AutoSize = true;
            this.chxTestPan.Location = new System.Drawing.Point(249, 447);
            this.chxTestPan.Name = "chxTestPan";
            this.chxTestPan.Size = new System.Drawing.Size(45, 17);
            this.chxTestPan.TabIndex = 9;
            this.chxTestPan.Text = "Pan";
            this.chxTestPan.UseVisualStyleBackColor = true;
            // 
            // btnTestRight
            // 
            this.btnTestRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTestRight.Location = new System.Drawing.Point(168, 443);
            this.btnTestRight.Name = "btnTestRight";
            this.btnTestRight.Size = new System.Drawing.Size(75, 23);
            this.btnTestRight.TabIndex = 8;
            this.btnTestRight.Text = "Right";
            this.btnTestRight.UseVisualStyleBackColor = true;
            this.btnTestRight.Click += new System.EventHandler(this.BtnTestRight_Click);
            // 
            // btnTestLeft
            // 
            this.btnTestLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTestLeft.Location = new System.Drawing.Point(87, 443);
            this.btnTestLeft.Name = "btnTestLeft";
            this.btnTestLeft.Size = new System.Drawing.Size(75, 23);
            this.btnTestLeft.TabIndex = 7;
            this.btnTestLeft.Text = "Left";
            this.btnTestLeft.UseVisualStyleBackColor = true;
            this.btnTestLeft.Click += new System.EventHandler(this.BtnTestLeft_Click);
            // 
            // btnResetPos
            // 
            this.btnResetPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetPos.Location = new System.Drawing.Point(6, 443);
            this.btnResetPos.Name = "btnResetPos";
            this.btnResetPos.Size = new System.Drawing.Size(75, 23);
            this.btnResetPos.TabIndex = 6;
            this.btnResetPos.Text = "Reset";
            this.btnResetPos.UseVisualStyleBackColor = true;
            this.btnResetPos.Click += new System.EventHandler(this.BtnResetPos_Click);
            // 
            // picBox_XY
            // 
            this.picBox_XY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picBox_XY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(255)))), ((int)(((byte)(224)))));
            this.picBox_XY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_XY.Location = new System.Drawing.Point(332, 73);
            this.picBox_XY.Name = "picBox_XY";
            this.picBox_XY.Size = new System.Drawing.Size(160, 368);
            this.picBox_XY.TabIndex = 5;
            this.picBox_XY.TabStop = false;
            // 
            // picBox_ZX
            // 
            this.picBox_ZX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picBox_ZX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(255)))), ((int)(((byte)(224)))));
            this.picBox_ZX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_ZX.Location = new System.Drawing.Point(6, 73);
            this.picBox_ZX.Name = "picBox_ZX";
            this.picBox_ZX.Size = new System.Drawing.Size(320, 368);
            this.picBox_ZX.TabIndex = 4;
            this.picBox_ZX.TabStop = false;
            // 
            // btnOkey
            // 
            this.btnOkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkey.Location = new System.Drawing.Point(843, 765);
            this.btnOkey.Name = "btnOkey";
            this.btnOkey.Size = new System.Drawing.Size(75, 23);
            this.btnOkey.TabIndex = 8;
            this.btnOkey.Text = "OK";
            this.btnOkey.UseVisualStyleBackColor = true;
            this.btnOkey.Click += new System.EventHandler(this.BtnOkey_Click);
            // 
            // grbxSFXsFolderPath
            // 
            this.grbxSFXsFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbxSFXsFolderPath.Controls.Add(this.grbxSoundDetails);
            this.grbxSFXsFolderPath.Controls.Add(this.btnSearchOutPath);
            this.grbxSFXsFolderPath.Controls.Add(this.txtSoundBankFile);
            this.grbxSFXsFolderPath.Location = new System.Drawing.Point(12, 694);
            this.grbxSFXsFolderPath.Name = "grbxSFXsFolderPath";
            this.grbxSFXsFolderPath.Size = new System.Drawing.Size(390, 101);
            this.grbxSFXsFolderPath.TabIndex = 3;
            this.grbxSFXsFolderPath.TabStop = false;
            this.grbxSFXsFolderPath.Text = "Platform Output Folder Path";
            // 
            // grbxSoundDetails
            // 
            this.grbxSoundDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbxSoundDetails.Controls.Add(this.btnSearchSoundDetails);
            this.grbxSoundDetails.Controls.Add(this.txtSoundDetails);
            this.grbxSoundDetails.Location = new System.Drawing.Point(6, 45);
            this.grbxSoundDetails.Name = "grbxSoundDetails";
            this.grbxSoundDetails.Size = new System.Drawing.Size(378, 50);
            this.grbxSoundDetails.TabIndex = 2;
            this.grbxSoundDetails.TabStop = false;
            this.grbxSoundDetails.Text = "Sound Details File";
            // 
            // btnSearchSoundDetails
            // 
            this.btnSearchSoundDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchSoundDetails.Location = new System.Drawing.Point(346, 18);
            this.btnSearchSoundDetails.Name = "btnSearchSoundDetails";
            this.btnSearchSoundDetails.Size = new System.Drawing.Size(26, 20);
            this.btnSearchSoundDetails.TabIndex = 3;
            this.btnSearchSoundDetails.Text = "...";
            this.btnSearchSoundDetails.UseVisualStyleBackColor = true;
            this.btnSearchSoundDetails.Click += new System.EventHandler(this.BtnSearchSoundDetails_Click);
            // 
            // txtSoundDetails
            // 
            this.txtSoundDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoundDetails.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoundDetails.Location = new System.Drawing.Point(6, 19);
            this.txtSoundDetails.Name = "txtSoundDetails";
            this.txtSoundDetails.ReadOnly = true;
            this.txtSoundDetails.Size = new System.Drawing.Size(334, 20);
            this.txtSoundDetails.TabIndex = 2;
            this.txtSoundDetails.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TxtSoundDetails_MouseDoubleClick);
            // 
            // btnSearchOutPath
            // 
            this.btnSearchOutPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchOutPath.Location = new System.Drawing.Point(358, 18);
            this.btnSearchOutPath.Name = "btnSearchOutPath";
            this.btnSearchOutPath.Size = new System.Drawing.Size(26, 20);
            this.btnSearchOutPath.TabIndex = 1;
            this.btnSearchOutPath.Text = "...";
            this.btnSearchOutPath.UseVisualStyleBackColor = true;
            this.btnSearchOutPath.Click += new System.EventHandler(this.BtnSearchOutPath_Click);
            // 
            // txtSoundBankFile
            // 
            this.txtSoundBankFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoundBankFile.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoundBankFile.Location = new System.Drawing.Point(6, 19);
            this.txtSoundBankFile.Name = "txtSoundBankFile";
            this.txtSoundBankFile.ReadOnly = true;
            this.txtSoundBankFile.Size = new System.Drawing.Size(346, 20);
            this.txtSoundBankFile.TabIndex = 0;
            this.txtSoundBankFile.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TxtSoundBankFile_MouseDoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grbxTestingGroup);
            this.groupBox3.Controls.Add(this.btnSfxReset);
            this.groupBox3.Controls.Add(this.btnStopAllSFXs);
            this.groupBox3.Controls.Add(this.trckBarMasterVolume);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(12, 587);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 101);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Misc SFX Module Commands";
            // 
            // grbxTestingGroup
            // 
            this.grbxTestingGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbxTestingGroup.Controls.Add(this.rbtn_Xbox);
            this.grbxTestingGroup.Controls.Add(this.rbtn_GameCube);
            this.grbxTestingGroup.Controls.Add(this.rbtn_PS2);
            this.grbxTestingGroup.Controls.Add(this.rbtn_PC);
            this.grbxTestingGroup.Location = new System.Drawing.Point(6, 48);
            this.grbxTestingGroup.Name = "grbxTestingGroup";
            this.grbxTestingGroup.Size = new System.Drawing.Size(292, 47);
            this.grbxTestingGroup.TabIndex = 4;
            this.grbxTestingGroup.TabStop = false;
            this.grbxTestingGroup.Text = "Platform Type:";
            // 
            // rbtn_Xbox
            // 
            this.rbtn_Xbox.AutoSize = true;
            this.rbtn_Xbox.Location = new System.Drawing.Point(231, 22);
            this.rbtn_Xbox.Name = "rbtn_Xbox";
            this.rbtn_Xbox.Size = new System.Drawing.Size(49, 17);
            this.rbtn_Xbox.TabIndex = 3;
            this.rbtn_Xbox.Text = "Xbox";
            this.rbtn_Xbox.UseVisualStyleBackColor = true;
            this.rbtn_Xbox.CheckedChanged += new System.EventHandler(this.Rbtn_Xbox_CheckedChanged);
            this.rbtn_Xbox.Click += new System.EventHandler(this.Rbtn_platform_Click);
            // 
            // rbtn_GameCube
            // 
            this.rbtn_GameCube.AutoSize = true;
            this.rbtn_GameCube.Location = new System.Drawing.Point(144, 22);
            this.rbtn_GameCube.Name = "rbtn_GameCube";
            this.rbtn_GameCube.Size = new System.Drawing.Size(81, 17);
            this.rbtn_GameCube.TabIndex = 2;
            this.rbtn_GameCube.Text = "Game Cube";
            this.rbtn_GameCube.UseVisualStyleBackColor = true;
            this.rbtn_GameCube.CheckedChanged += new System.EventHandler(this.Rbtn_GameCube_CheckedChanged);
            this.rbtn_GameCube.Click += new System.EventHandler(this.Rbtn_platform_Click);
            // 
            // rbtn_PS2
            // 
            this.rbtn_PS2.AutoSize = true;
            this.rbtn_PS2.Location = new System.Drawing.Point(51, 22);
            this.rbtn_PS2.Name = "rbtn_PS2";
            this.rbtn_PS2.Size = new System.Drawing.Size(87, 17);
            this.rbtn_PS2.TabIndex = 1;
            this.rbtn_PS2.Text = "PlayStation 2";
            this.rbtn_PS2.UseVisualStyleBackColor = true;
            this.rbtn_PS2.CheckedChanged += new System.EventHandler(this.Rbtn_PS2_CheckedChanged);
            this.rbtn_PS2.Click += new System.EventHandler(this.Rbtn_platform_Click);
            // 
            // rbtn_PC
            // 
            this.rbtn_PC.AutoSize = true;
            this.rbtn_PC.Checked = true;
            this.rbtn_PC.Location = new System.Drawing.Point(6, 22);
            this.rbtn_PC.Name = "rbtn_PC";
            this.rbtn_PC.Size = new System.Drawing.Size(39, 17);
            this.rbtn_PC.TabIndex = 0;
            this.rbtn_PC.TabStop = true;
            this.rbtn_PC.Text = "PC";
            this.rbtn_PC.UseVisualStyleBackColor = true;
            this.rbtn_PC.CheckedChanged += new System.EventHandler(this.Rbtn_PC_CheckedChanged);
            this.rbtn_PC.Click += new System.EventHandler(this.Rbtn_platform_Click);
            // 
            // btnSfxReset
            // 
            this.btnSfxReset.Location = new System.Drawing.Point(304, 48);
            this.btnSfxReset.Name = "btnSfxReset";
            this.btnSfxReset.Size = new System.Drawing.Size(80, 23);
            this.btnSfxReset.TabIndex = 3;
            this.btnSfxReset.Text = "SFX Reset";
            this.btnSfxReset.UseVisualStyleBackColor = true;
            this.btnSfxReset.Click += new System.EventHandler(this.BtnSfxReset_Click);
            // 
            // btnStopAllSFXs
            // 
            this.btnStopAllSFXs.Location = new System.Drawing.Point(304, 19);
            this.btnStopAllSFXs.Name = "btnStopAllSFXs";
            this.btnStopAllSFXs.Size = new System.Drawing.Size(80, 23);
            this.btnStopAllSFXs.TabIndex = 2;
            this.btnStopAllSFXs.Text = "Stop All SFXs";
            this.btnStopAllSFXs.UseVisualStyleBackColor = true;
            this.btnStopAllSFXs.Click += new System.EventHandler(this.BtnStopAllSFXs_Click);
            // 
            // trckBarMasterVolume
            // 
            this.trckBarMasterVolume.Location = new System.Drawing.Point(57, 19);
            this.trckBarMasterVolume.Maximum = 100;
            this.trckBarMasterVolume.Name = "trckBarMasterVolume";
            this.trckBarMasterVolume.Size = new System.Drawing.Size(197, 45);
            this.trckBarMasterVolume.TabIndex = 1;
            this.trckBarMasterVolume.TickFrequency = 10;
            this.trckBarMasterVolume.Value = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Volume:";
            // 
            // btn_musicApp
            // 
            this.btn_musicApp.Location = new System.Drawing.Point(408, 765);
            this.btn_musicApp.Name = "btn_musicApp";
            this.btn_musicApp.Size = new System.Drawing.Size(243, 30);
            this.btn_musicApp.TabIndex = 7;
            this.btn_musicApp.Text = "Interactive Music App";
            this.btn_musicApp.UseVisualStyleBackColor = true;
            this.btn_musicApp.Click += new System.EventHandler(this.Btn_musicApp_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "SFX File (*.sfx)|*.sfx";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // ConsoleApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 807);
            this.Controls.Add(this.btn_musicApp);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grbxSFXsFolderPath);
            this.Controls.Add(this.btnOkey);
            this.Controls.Add(this.grbxTestPostVel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grbxAvailableSoundBanks);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.chkStreamingTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsoleApp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound Tester App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleApp_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nudHashCode)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            this.grbxSfxPlay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudHashCodePlaying)).EndInit();
            this.grbxSamplePlay.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudOuterRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInnerRadius)).EndInit();
            this.grbxRandomTest.ResumeLayout(false);
            this.grbxAvailableSoundBanks.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.grbxTestPostVel.ResumeLayout(false);
            this.grbxTestPostVel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_XY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_ZX)).EndInit();
            this.grbxSFXsFolderPath.ResumeLayout(false);
            this.grbxSFXsFolderPath.PerformLayout();
            this.grbxSoundDetails.ResumeLayout(false);
            this.grbxSoundDetails.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grbxTestingGroup.ResumeLayout(false);
            this.grbxTestingGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckBarMasterVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartSFX;
        private System.Windows.Forms.Button btnStopSFX;
        private System.Windows.Forms.NumericUpDown nudHashCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudZ;
        private System.Windows.Forms.Label labelListZ;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.Label labelListY;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.Label labelListX;
        private System.Windows.Forms.Button btnStart3dSound;
        private System.Windows.Forms.GroupBox grbxSfxPlay;
        private System.Windows.Forms.Button btnPlaySample;
        private System.Windows.Forms.GroupBox grbxSamplePlay;
        private System.Windows.Forms.CheckBox chkStreamingTest;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lstbSamples;
        private System.Windows.Forms.GroupBox grbxRandomTest;
        private System.Windows.Forms.Button btnStopRandomTest;
        private System.Windows.Forms.Button btnStartRandomTest;
        private System.Windows.Forms.ListBox lstBox_SFXs;
        private System.Windows.Forms.GroupBox grbxAvailableSoundBanks;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadSoundbanks;
        private System.Windows.Forms.ListBox lstbAvailableSoundBanks;
        private System.Windows.Forms.Button btnDeLoadSoundBanks;
        private System.Windows.Forms.ListBox lstbLoadedSoundBanks;
        private System.Windows.Forms.GroupBox grbxTestPostVel;
        private System.Windows.Forms.PictureBox picBox_XY;
        private System.Windows.Forms.PictureBox picBox_ZX;
        private System.Windows.Forms.CheckBox chxDrawCircle;
        private System.Windows.Forms.CheckBox chxTestPan;
        private System.Windows.Forms.Button btnTestRight;
        private System.Windows.Forms.Button btnTestLeft;
        private System.Windows.Forms.Button btnResetPos;
        private System.Windows.Forms.Button btnOkey;
        private System.Windows.Forms.NumericUpDown nudOuterRadius;
        private System.Windows.Forms.NumericUpDown nudInnerRadius;
        private System.Windows.Forms.NumericUpDown nudScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbxSFXsFolderPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSfxReset;
        private System.Windows.Forms.Button btnStopAllSFXs;
        private System.Windows.Forms.TrackBar trckBarMasterVolume;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.GroupBox grbxTestingGroup;
        private System.Windows.Forms.RadioButton rbtn_Xbox;
        private System.Windows.Forms.RadioButton rbtn_GameCube;
        private System.Windows.Forms.RadioButton rbtn_PS2;
        private System.Windows.Forms.RadioButton rbtn_PC;
        private System.Windows.Forms.Button btn_musicApp;
        private System.Windows.Forms.Button btnSearchOutPath;
        private System.Windows.Forms.TextBox txtSoundBankFile;
        private System.Windows.Forms.GroupBox grbxSoundDetails;
        private System.Windows.Forms.Button btnSearchSoundDetails;
        private System.Windows.Forms.TextBox txtSoundDetails;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NumericUpDown nudHashCodePlaying;
    }
}
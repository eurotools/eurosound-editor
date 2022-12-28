
namespace sb_editor.Panels
{
    partial class UserControl_SamplePool
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_SamplePool));
            this.grbSamplePool = new System.Windows.Forms.GroupBox();
            this.grbSampleProperties = new System.Windows.Forms.GroupBox();
            this.lblRandomPan = new System.Windows.Forms.Label();
            this.lblPan = new System.Windows.Forms.Label();
            this.lblRandomVolume = new System.Windows.Forms.Label();
            this.lblBaseVolume = new System.Windows.Forms.Label();
            this.lblRandomPitch = new System.Windows.Forms.Label();
            this.lblPitch = new System.Windows.Forms.Label();
            this.nudRandomPan = new System.Windows.Forms.NumericUpDown();
            this.nudPan = new System.Windows.Forms.NumericUpDown();
            this.nudRandomVolume = new System.Windows.Forms.NumericUpDown();
            this.nudBaseVolume = new System.Windows.Forms.NumericUpDown();
            this.nudRandomPitchOffset = new System.Windows.Forms.NumericUpDown();
            this.nudPitchOffset = new System.Windows.Forms.NumericUpDown();
            this.chkEnableStereo = new System.Windows.Forms.CheckBox();
            this.grbSampleInformation = new System.Windows.Forms.GroupBox();
            this.lblSampleInfo_StreamedValue = new System.Windows.Forms.Label();
            this.lblSampleInfo_Streamed = new System.Windows.Forms.Label();
            this.lblSampleInfo_LoopValue = new System.Windows.Forms.Label();
            this.lblSampleInfo_Loop = new System.Windows.Forms.Label();
            this.lblSampleInfo_LengthValue = new System.Windows.Forms.Label();
            this.lblSampleInfo_Length = new System.Windows.Forms.Label();
            this.lblSampleInfo_SizeValue = new System.Windows.Forms.Label();
            this.lblSampleInfo_Size = new System.Windows.Forms.Label();
            this.lblSampleInfo_FrequencyValue = new System.Windows.Forms.Label();
            this.lblSampleInfo_Frequency = new System.Windows.Forms.Label();
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.lblMove = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstSamples = new System.Windows.Forms.ListBox();
            this.ContextMenu_Listbox = new System.Windows.Forms.ContextMenu();
            this.mnuAdd = new System.Windows.Forms.MenuItem();
            this.mnuRemove = new System.Windows.Forms.MenuItem();
            this.mnuOpen = new System.Windows.Forms.MenuItem();
            this.mnuCopy = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuPlay = new System.Windows.Forms.MenuItem();
            this.mnuStop = new System.Windows.Forms.MenuItem();
            this.chkEnableSubSFX = new System.Windows.Forms.CheckBox();
            this.OpenFileDiag_Samples = new System.Windows.Forms.OpenFileDialog();
            this.grbSamplePool.SuspendLayout();
            this.grbSampleProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomPitchOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPitchOffset)).BeginInit();
            this.grbSampleInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSamplePool
            // 
            this.grbSamplePool.Controls.Add(this.grbSampleProperties);
            this.grbSamplePool.Controls.Add(this.chkEnableStereo);
            this.grbSamplePool.Controls.Add(this.grbSampleInformation);
            this.grbSamplePool.Controls.Add(this.cboFormat);
            this.grbSamplePool.Controls.Add(this.lblFormat);
            this.grbSamplePool.Controls.Add(this.btnMoveDown);
            this.grbSamplePool.Controls.Add(this.btnMoveUp);
            this.grbSamplePool.Controls.Add(this.lblMove);
            this.grbSamplePool.Controls.Add(this.btnStop);
            this.grbSamplePool.Controls.Add(this.btnPlay);
            this.grbSamplePool.Controls.Add(this.btnEdit);
            this.grbSamplePool.Controls.Add(this.btnOpen);
            this.grbSamplePool.Controls.Add(this.btnCopy);
            this.grbSamplePool.Controls.Add(this.btnRemove);
            this.grbSamplePool.Controls.Add(this.btnAdd);
            this.grbSamplePool.Controls.Add(this.lstSamples);
            this.grbSamplePool.Controls.Add(this.chkEnableSubSFX);
            this.grbSamplePool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbSamplePool.Location = new System.Drawing.Point(0, 0);
            this.grbSamplePool.Name = "grbSamplePool";
            this.grbSamplePool.Size = new System.Drawing.Size(777, 349);
            this.grbSamplePool.TabIndex = 0;
            this.grbSamplePool.TabStop = false;
            this.grbSamplePool.Text = "Sample Pool";
            // 
            // grbSampleProperties
            // 
            this.grbSampleProperties.Controls.Add(this.lblRandomPan);
            this.grbSampleProperties.Controls.Add(this.lblPan);
            this.grbSampleProperties.Controls.Add(this.lblRandomVolume);
            this.grbSampleProperties.Controls.Add(this.lblBaseVolume);
            this.grbSampleProperties.Controls.Add(this.lblRandomPitch);
            this.grbSampleProperties.Controls.Add(this.lblPitch);
            this.grbSampleProperties.Controls.Add(this.nudRandomPan);
            this.grbSampleProperties.Controls.Add(this.nudPan);
            this.grbSampleProperties.Controls.Add(this.nudRandomVolume);
            this.grbSampleProperties.Controls.Add(this.nudBaseVolume);
            this.grbSampleProperties.Controls.Add(this.nudRandomPitchOffset);
            this.grbSampleProperties.Controls.Add(this.nudPitchOffset);
            this.grbSampleProperties.Location = new System.Drawing.Point(520, 137);
            this.grbSampleProperties.Name = "grbSampleProperties";
            this.grbSampleProperties.Size = new System.Drawing.Size(251, 166);
            this.grbSampleProperties.TabIndex = 16;
            this.grbSampleProperties.TabStop = false;
            this.grbSampleProperties.Text = "Sample Properties";
            // 
            // lblRandomPan
            // 
            this.lblRandomPan.Location = new System.Drawing.Point(6, 139);
            this.lblRandomPan.Name = "lblRandomPan";
            this.lblRandomPan.Size = new System.Drawing.Size(132, 13);
            this.lblRandomPan.TabIndex = 10;
            this.lblRandomPan.Text = "Random Pan Offset";
            // 
            // lblPan
            // 
            this.lblPan.Location = new System.Drawing.Point(6, 115);
            this.lblPan.Name = "lblPan";
            this.lblPan.Size = new System.Drawing.Size(132, 13);
            this.lblPan.TabIndex = 8;
            this.lblPan.Text = "Pan (-100 to 100)";
            // 
            // lblRandomVolume
            // 
            this.lblRandomVolume.Location = new System.Drawing.Point(6, 91);
            this.lblRandomVolume.Name = "lblRandomVolume";
            this.lblRandomVolume.Size = new System.Drawing.Size(142, 13);
            this.lblRandomVolume.TabIndex = 6;
            this.lblRandomVolume.Text = "Random Volume Offset: (-/+)";
            // 
            // lblBaseVolume
            // 
            this.lblBaseVolume.Location = new System.Drawing.Point(6, 67);
            this.lblBaseVolume.Name = "lblBaseVolume";
            this.lblBaseVolume.Size = new System.Drawing.Size(142, 13);
            this.lblBaseVolume.TabIndex = 4;
            this.lblBaseVolume.Text = "Base Volume: (0-100)";
            // 
            // lblRandomPitch
            // 
            this.lblRandomPitch.Location = new System.Drawing.Point(6, 43);
            this.lblRandomPitch.Name = "lblRandomPitch";
            this.lblRandomPitch.Size = new System.Drawing.Size(142, 13);
            this.lblRandomPitch.TabIndex = 2;
            this.lblRandomPitch.Text = "Random Pitch Offset: (-/+)";
            // 
            // lblPitch
            // 
            this.lblPitch.Location = new System.Drawing.Point(6, 19);
            this.lblPitch.Name = "lblPitch";
            this.lblPitch.Size = new System.Drawing.Size(142, 13);
            this.lblPitch.TabIndex = 0;
            this.lblPitch.Text = "Pitch Offset: (semitones)";
            // 
            // nudRandomPan
            // 
            this.nudRandomPan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRandomPan.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRandomPan.Location = new System.Drawing.Point(155, 137);
            this.nudRandomPan.Name = "nudRandomPan";
            this.nudRandomPan.Size = new System.Drawing.Size(90, 20);
            this.nudRandomPan.TabIndex = 11;
            this.nudRandomPan.ValueChanged += new System.EventHandler(this.NudRandomPan_ValueChanged);
            // 
            // nudPan
            // 
            this.nudPan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPan.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPan.Location = new System.Drawing.Point(155, 113);
            this.nudPan.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudPan.Name = "nudPan";
            this.nudPan.Size = new System.Drawing.Size(90, 20);
            this.nudPan.TabIndex = 9;
            this.nudPan.ValueChanged += new System.EventHandler(this.NudPan_ValueChanged);
            // 
            // nudRandomVolume
            // 
            this.nudRandomVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRandomVolume.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRandomVolume.Location = new System.Drawing.Point(155, 89);
            this.nudRandomVolume.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudRandomVolume.Name = "nudRandomVolume";
            this.nudRandomVolume.Size = new System.Drawing.Size(90, 20);
            this.nudRandomVolume.TabIndex = 7;
            this.nudRandomVolume.ValueChanged += new System.EventHandler(this.NudRandomVolume_ValueChanged);
            // 
            // nudBaseVolume
            // 
            this.nudBaseVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudBaseVolume.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudBaseVolume.Location = new System.Drawing.Point(155, 65);
            this.nudBaseVolume.Name = "nudBaseVolume";
            this.nudBaseVolume.Size = new System.Drawing.Size(90, 20);
            this.nudBaseVolume.TabIndex = 5;
            this.nudBaseVolume.ValueChanged += new System.EventHandler(this.NudBaseVolume_ValueChanged);
            // 
            // nudRandomPitchOffset
            // 
            this.nudRandomPitchOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRandomPitchOffset.DecimalPlaces = 1;
            this.nudRandomPitchOffset.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudRandomPitchOffset.Location = new System.Drawing.Point(155, 41);
            this.nudRandomPitchOffset.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nudRandomPitchOffset.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            -2147483648});
            this.nudRandomPitchOffset.Name = "nudRandomPitchOffset";
            this.nudRandomPitchOffset.Size = new System.Drawing.Size(90, 20);
            this.nudRandomPitchOffset.TabIndex = 3;
            this.nudRandomPitchOffset.ValueChanged += new System.EventHandler(this.NudRandomPitchOffset_ValueChanged);
            // 
            // nudPitchOffset
            // 
            this.nudPitchOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPitchOffset.DecimalPlaces = 1;
            this.nudPitchOffset.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudPitchOffset.Location = new System.Drawing.Point(155, 17);
            this.nudPitchOffset.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nudPitchOffset.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            -2147483648});
            this.nudPitchOffset.Name = "nudPitchOffset";
            this.nudPitchOffset.Size = new System.Drawing.Size(90, 20);
            this.nudPitchOffset.TabIndex = 1;
            this.nudPitchOffset.ValueChanged += new System.EventHandler(this.NudPitchOffset_ValueChanged);
            // 
            // chkEnableStereo
            // 
            this.chkEnableStereo.AutoSize = true;
            this.chkEnableStereo.Location = new System.Drawing.Point(211, 43);
            this.chkEnableStereo.Name = "chkEnableStereo";
            this.chkEnableStereo.Size = new System.Drawing.Size(124, 17);
            this.chkEnableStereo.TabIndex = 1;
            this.chkEnableStereo.Text = "Enable Music Labels";
            this.chkEnableStereo.UseVisualStyleBackColor = true;
            this.chkEnableStereo.Visible = false;
            this.chkEnableStereo.CheckedChanged += new System.EventHandler(this.ChkEnableStereo_CheckedChanged);
            this.chkEnableStereo.CheckStateChanged += new System.EventHandler(this.ChkEnableStereo_CheckStateChanged);
            // 
            // grbSampleInformation
            // 
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_StreamedValue);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_Streamed);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_LoopValue);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_Loop);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_LengthValue);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_Length);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_SizeValue);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_Size);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_FrequencyValue);
            this.grbSampleInformation.Controls.Add(this.lblSampleInfo_Frequency);
            this.grbSampleInformation.Location = new System.Drawing.Point(545, 36);
            this.grbSampleInformation.Name = "grbSampleInformation";
            this.grbSampleInformation.Size = new System.Drawing.Size(209, 95);
            this.grbSampleInformation.TabIndex = 15;
            this.grbSampleInformation.TabStop = false;
            this.grbSampleInformation.Text = "Sample Information";
            // 
            // lblSampleInfo_StreamedValue
            // 
            this.lblSampleInfo_StreamedValue.AutoSize = true;
            this.lblSampleInfo_StreamedValue.Location = new System.Drawing.Point(95, 76);
            this.lblSampleInfo_StreamedValue.Name = "lblSampleInfo_StreamedValue";
            this.lblSampleInfo_StreamedValue.Size = new System.Drawing.Size(0, 13);
            this.lblSampleInfo_StreamedValue.TabIndex = 9;
            // 
            // lblSampleInfo_Streamed
            // 
            this.lblSampleInfo_Streamed.AutoSize = true;
            this.lblSampleInfo_Streamed.Location = new System.Drawing.Point(6, 76);
            this.lblSampleInfo_Streamed.Name = "lblSampleInfo_Streamed";
            this.lblSampleInfo_Streamed.Size = new System.Drawing.Size(55, 13);
            this.lblSampleInfo_Streamed.TabIndex = 8;
            this.lblSampleInfo_Streamed.Text = "Streamed:";
            // 
            // lblSampleInfo_LoopValue
            // 
            this.lblSampleInfo_LoopValue.AutoSize = true;
            this.lblSampleInfo_LoopValue.Location = new System.Drawing.Point(95, 62);
            this.lblSampleInfo_LoopValue.Name = "lblSampleInfo_LoopValue";
            this.lblSampleInfo_LoopValue.Size = new System.Drawing.Size(0, 13);
            this.lblSampleInfo_LoopValue.TabIndex = 7;
            // 
            // lblSampleInfo_Loop
            // 
            this.lblSampleInfo_Loop.AutoSize = true;
            this.lblSampleInfo_Loop.Location = new System.Drawing.Point(6, 62);
            this.lblSampleInfo_Loop.Name = "lblSampleInfo_Loop";
            this.lblSampleInfo_Loop.Size = new System.Drawing.Size(34, 13);
            this.lblSampleInfo_Loop.TabIndex = 6;
            this.lblSampleInfo_Loop.Text = "Loop:";
            // 
            // lblSampleInfo_LengthValue
            // 
            this.lblSampleInfo_LengthValue.AutoSize = true;
            this.lblSampleInfo_LengthValue.Location = new System.Drawing.Point(95, 47);
            this.lblSampleInfo_LengthValue.Name = "lblSampleInfo_LengthValue";
            this.lblSampleInfo_LengthValue.Size = new System.Drawing.Size(0, 13);
            this.lblSampleInfo_LengthValue.TabIndex = 5;
            // 
            // lblSampleInfo_Length
            // 
            this.lblSampleInfo_Length.AutoSize = true;
            this.lblSampleInfo_Length.Location = new System.Drawing.Point(6, 47);
            this.lblSampleInfo_Length.Name = "lblSampleInfo_Length";
            this.lblSampleInfo_Length.Size = new System.Drawing.Size(43, 13);
            this.lblSampleInfo_Length.TabIndex = 4;
            this.lblSampleInfo_Length.Text = "Length:";
            // 
            // lblSampleInfo_SizeValue
            // 
            this.lblSampleInfo_SizeValue.AutoSize = true;
            this.lblSampleInfo_SizeValue.Location = new System.Drawing.Point(95, 31);
            this.lblSampleInfo_SizeValue.Name = "lblSampleInfo_SizeValue";
            this.lblSampleInfo_SizeValue.Size = new System.Drawing.Size(0, 13);
            this.lblSampleInfo_SizeValue.TabIndex = 3;
            // 
            // lblSampleInfo_Size
            // 
            this.lblSampleInfo_Size.AutoSize = true;
            this.lblSampleInfo_Size.Location = new System.Drawing.Point(6, 31);
            this.lblSampleInfo_Size.Name = "lblSampleInfo_Size";
            this.lblSampleInfo_Size.Size = new System.Drawing.Size(30, 13);
            this.lblSampleInfo_Size.TabIndex = 2;
            this.lblSampleInfo_Size.Text = "Size:";
            // 
            // lblSampleInfo_FrequencyValue
            // 
            this.lblSampleInfo_FrequencyValue.AutoSize = true;
            this.lblSampleInfo_FrequencyValue.Location = new System.Drawing.Point(95, 16);
            this.lblSampleInfo_FrequencyValue.Name = "lblSampleInfo_FrequencyValue";
            this.lblSampleInfo_FrequencyValue.Size = new System.Drawing.Size(0, 13);
            this.lblSampleInfo_FrequencyValue.TabIndex = 1;
            // 
            // lblSampleInfo_Frequency
            // 
            this.lblSampleInfo_Frequency.AutoSize = true;
            this.lblSampleInfo_Frequency.Location = new System.Drawing.Point(6, 16);
            this.lblSampleInfo_Frequency.Name = "lblSampleInfo_Frequency";
            this.lblSampleInfo_Frequency.Size = new System.Drawing.Size(60, 13);
            this.lblSampleInfo_Frequency.TabIndex = 0;
            this.lblSampleInfo_Frequency.Text = "Frequency:";
            // 
            // cboFormat
            // 
            this.cboFormat.FormattingEnabled = true;
            this.cboFormat.Location = new System.Drawing.Point(611, 9);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Size = new System.Drawing.Size(143, 21);
            this.cboFormat.TabIndex = 14;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormat.Location = new System.Drawing.Point(541, 10);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(64, 20);
            this.lblFormat.TabIndex = 13;
            this.lblFormat.Text = "Format:";
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.Location = new System.Drawing.Point(483, 107);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(20, 20);
            this.btnMoveDown.TabIndex = 12;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.BtnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.Location = new System.Drawing.Point(483, 87);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(20, 20);
            this.btnMoveUp.TabIndex = 11;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.BtnMoveUp_Click);
            // 
            // lblMove
            // 
            this.lblMove.AutoSize = true;
            this.lblMove.Location = new System.Drawing.Point(480, 60);
            this.lblMove.Name = "lblMove";
            this.lblMove.Size = new System.Drawing.Size(34, 13);
            this.lblMove.TabIndex = 10;
            this.lblMove.Text = "Move";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(437, 320);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(40, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(398, 320);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(40, 23);
            this.btnPlay.TabIndex = 8;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(297, 320);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(55, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(199, 320);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(55, 23);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(138, 320);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(55, 23);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(77, 320);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(55, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(16, 320);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(55, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // lstSamples
            // 
            this.lstSamples.AllowDrop = true;
            this.lstSamples.ContextMenu = this.ContextMenu_Listbox;
            this.lstSamples.DisplayMember = "FilePath";
            this.lstSamples.FormattingEnabled = true;
            this.lstSamples.Location = new System.Drawing.Point(16, 76);
            this.lstSamples.Name = "lstSamples";
            this.lstSamples.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSamples.Size = new System.Drawing.Size(461, 238);
            this.lstSamples.TabIndex = 2;
            this.lstSamples.SelectedIndexChanged += new System.EventHandler(this.LstSamples_SelectedIndexChanged);
            this.lstSamples.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstSamples_DragDrop);
            this.lstSamples.DragOver += new System.Windows.Forms.DragEventHandler(this.LstSamples_DragOver);
            this.lstSamples.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstSamples_MouseDoubleClick);
            // 
            // ContextMenu_Listbox
            // 
            this.ContextMenu_Listbox.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAdd,
            this.mnuRemove,
            this.mnuOpen,
            this.mnuCopy,
            this.mnuEdit,
            this.mnuPlay,
            this.mnuStop});
            // 
            // mnuAdd
            // 
            this.mnuAdd.Index = 0;
            this.mnuAdd.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuAdd.Text = "Add";
            this.mnuAdd.Click += new System.EventHandler(this.MnuAdd_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Index = 1;
            this.mnuRemove.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.mnuRemove.Text = "Remove";
            this.mnuRemove.Click += new System.EventHandler(this.MnuRemove_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Index = 2;
            this.mnuOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.MnuOpen_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Index = 3;
            this.mnuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.MnuCopy_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 4;
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Click += new System.EventHandler(this.MnuEdit_Click);
            // 
            // mnuPlay
            // 
            this.mnuPlay.Index = 5;
            this.mnuPlay.Text = "Play";
            this.mnuPlay.Click += new System.EventHandler(this.MnuPlay_Click);
            // 
            // mnuStop
            // 
            this.mnuStop.Index = 6;
            this.mnuStop.Text = "Stop";
            this.mnuStop.Click += new System.EventHandler(this.MnuStop_Click);
            // 
            // chkEnableSubSFX
            // 
            this.chkEnableSubSFX.AutoSize = true;
            this.chkEnableSubSFX.Location = new System.Drawing.Point(16, 43);
            this.chkEnableSubSFX.Name = "chkEnableSubSFX";
            this.chkEnableSubSFX.Size = new System.Drawing.Size(109, 17);
            this.chkEnableSubSFX.TabIndex = 0;
            this.chkEnableSubSFX.Text = "Enable Sub SFXs";
            this.chkEnableSubSFX.UseVisualStyleBackColor = true;
            this.chkEnableSubSFX.CheckedChanged += new System.EventHandler(this.ChkEnableSubSFX_CheckedChanged);
            this.chkEnableSubSFX.Click += new System.EventHandler(this.ChkEnableSubSFX_Click);
            // 
            // OpenFileDiag_Samples
            // 
            this.OpenFileDiag_Samples.Filter = "WAV Files (*.wav)|*.wav";
            this.OpenFileDiag_Samples.FilterIndex = 0;
            this.OpenFileDiag_Samples.Multiselect = true;
            // 
            // UserControl_SamplePool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbSamplePool);
            this.Name = "UserControl_SamplePool";
            this.Size = new System.Drawing.Size(777, 349);
            this.grbSamplePool.ResumeLayout(false);
            this.grbSamplePool.PerformLayout();
            this.grbSampleProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomPitchOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPitchOffset)).EndInit();
            this.grbSampleInformation.ResumeLayout(false);
            this.grbSampleInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSamplePool;
        private System.Windows.Forms.GroupBox grbSampleInformation;
        private System.Windows.Forms.Label lblSampleInfo_StreamedValue;
        private System.Windows.Forms.Label lblSampleInfo_Streamed;
        private System.Windows.Forms.Label lblSampleInfo_LoopValue;
        private System.Windows.Forms.Label lblSampleInfo_Loop;
        private System.Windows.Forms.Label lblSampleInfo_LengthValue;
        private System.Windows.Forms.Label lblSampleInfo_Length;
        private System.Windows.Forms.Label lblSampleInfo_SizeValue;
        private System.Windows.Forms.Label lblSampleInfo_Size;
        private System.Windows.Forms.Label lblSampleInfo_FrequencyValue;
        private System.Windows.Forms.Label lblSampleInfo_Frequency;
        private System.Windows.Forms.ComboBox cboFormat;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Label lblMove;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenu ContextMenu_Listbox;
        private System.Windows.Forms.MenuItem mnuAdd;
        private System.Windows.Forms.MenuItem mnuRemove;
        private System.Windows.Forms.MenuItem mnuOpen;
        private System.Windows.Forms.MenuItem mnuCopy;
        private System.Windows.Forms.MenuItem mnuEdit;
        private System.Windows.Forms.MenuItem mnuPlay;
        private System.Windows.Forms.MenuItem mnuStop;
        protected internal System.Windows.Forms.ListBox lstSamples;
        private System.Windows.Forms.OpenFileDialog OpenFileDiag_Samples;
        private System.Windows.Forms.GroupBox grbSampleProperties;
        protected internal System.Windows.Forms.Label lblRandomPan;
        protected internal System.Windows.Forms.Label lblPan;
        protected internal System.Windows.Forms.Label lblRandomVolume;
        protected internal System.Windows.Forms.Label lblBaseVolume;
        protected internal System.Windows.Forms.Label lblRandomPitch;
        protected internal System.Windows.Forms.Label lblPitch;
        protected internal System.Windows.Forms.NumericUpDown nudRandomPan;
        protected internal System.Windows.Forms.NumericUpDown nudPan;
        protected internal System.Windows.Forms.NumericUpDown nudRandomVolume;
        protected internal System.Windows.Forms.NumericUpDown nudBaseVolume;
        protected internal System.Windows.Forms.NumericUpDown nudRandomPitchOffset;
        protected internal System.Windows.Forms.NumericUpDown nudPitchOffset;
        protected internal System.Windows.Forms.CheckBox chkEnableSubSFX;
        protected internal System.Windows.Forms.CheckBox chkEnableStereo;
    }
}

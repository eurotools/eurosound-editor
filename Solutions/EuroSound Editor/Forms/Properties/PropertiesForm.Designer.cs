
namespace sb_editor
{
    partial class PropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesForm));
            this.grbMasterDirectory = new System.Windows.Forms.GroupBox();
            this.lblMaster = new System.Windows.Forms.Label();
            this.btnSetMasterFolder = new System.Windows.Forms.Button();
            this.txtMasterDirectory = new System.Windows.Forms.TextBox();
            this.grbHashCodeDirectory = new System.Windows.Forms.GroupBox();
            this.btnSetHashCodesDir = new System.Windows.Forms.Button();
            this.txtHashCodeFile = new System.Windows.Forms.TextBox();
            this.grbEngineXPath = new System.Windows.Forms.GroupBox();
            this.btnSetEngineXPath = new System.Windows.Forms.Button();
            this.txtEngineXProject = new System.Windows.Forms.TextBox();
            this.grbEuroLandPath = new System.Windows.Forms.GroupBox();
            this.btnSetEuroLandServer = new System.Windows.Forms.Button();
            this.txtEuroLandServer = new System.Windows.Forms.TextBox();
            this.grbAvailableFormat = new System.Windows.Forms.GroupBox();
            this.btnAutoReSampleOff = new System.Windows.Forms.Button();
            this.btnAutoReSampleOn = new System.Windows.Forms.Button();
            this.btnSearchOutputFolder = new System.Windows.Forms.Button();
            this.btnAddFormat = new System.Windows.Forms.Button();
            this.cboAvailableFormats = new System.Windows.Forms.ComboBox();
            this.lvwAvailableFormats = new System.Windows.Forms.ListView();
            this.colFormatProps_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFormatProps_OutFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFormatProps_AutoReSample = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.LineShape_Divider2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.LineShape_Divider1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.grbAvailableReSample = new System.Windows.Forms.GroupBox();
            this.pnlListView = new System.Windows.Forms.Panel();
            this.lstAvailableSampleRates = new System.Windows.Forms.ListBox();
            this.btnAddSampleRate = new System.Windows.Forms.Button();
            this.grbReSampleRatePerFormat = new System.Windows.Forms.GroupBox();
            this.lvwReSampleFormats = new System.Windows.Forms.ListView();
            this.colFormat_Label = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFormat_Rate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.grbMisc = new System.Windows.Forms.GroupBox();
            this.lblXbox_Unit = new System.Windows.Forms.Label();
            this.lblGameCube_Unit = new System.Windows.Forms.Label();
            this.lblPC_Unit = new System.Windows.Forms.Label();
            this.lblPlaySatitonlblUnit = new System.Windows.Forms.Label();
            this.chkPreviewCommands = new System.Windows.Forms.CheckBox();
            this.chkPrefixHashCodes = new System.Windows.Forms.CheckBox();
            this.nudXboxSzie = new System.Windows.Forms.NumericUpDown();
            this.lblXboxSize = new System.Windows.Forms.Label();
            this.nudGameCubeSize = new System.Windows.Forms.NumericUpDown();
            this.nudPCSize = new System.Windows.Forms.NumericUpDown();
            this.nudPlayStationSize = new System.Windows.Forms.NumericUpDown();
            this.lblGameCubeSize = new System.Windows.Forms.Label();
            this.lblPCSize = new System.Windows.Forms.Label();
            this.lblPlayStationSize = new System.Windows.Forms.Label();
            this.txtTextEditor = new System.Windows.Forms.TextBox();
            this.lblTextEditor = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtSoundForge = new System.Windows.Forms.TextBox();
            this.lblSoundForge = new System.Windows.Forms.Label();
            this.txtEditWavsTool = new System.Windows.Forms.TextBox();
            this.cboDefaultRate = new System.Windows.Forms.ComboBox();
            this.lblEditWavs = new System.Windows.Forms.Label();
            this.lblDefaultSampleRate = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.LineShape_Divider4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.LineShape_Divider3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.OpenFileDiag = new System.Windows.Forms.OpenFileDialog();
            this.grbMasterDirectory.SuspendLayout();
            this.grbHashCodeDirectory.SuspendLayout();
            this.grbEngineXPath.SuspendLayout();
            this.grbEuroLandPath.SuspendLayout();
            this.grbAvailableFormat.SuspendLayout();
            this.grbAvailableReSample.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.grbReSampleRatePerFormat.SuspendLayout();
            this.grbMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudXboxSzie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameCubeSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPCSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayStationSize)).BeginInit();
            this.SuspendLayout();
            // 
            // grbMasterDirectory
            // 
            this.grbMasterDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMasterDirectory.Controls.Add(this.lblMaster);
            this.grbMasterDirectory.Controls.Add(this.btnSetMasterFolder);
            this.grbMasterDirectory.Controls.Add(this.txtMasterDirectory);
            this.grbMasterDirectory.Location = new System.Drawing.Point(12, 10);
            this.grbMasterDirectory.Name = "grbMasterDirectory";
            this.grbMasterDirectory.Size = new System.Drawing.Size(644, 42);
            this.grbMasterDirectory.TabIndex = 0;
            this.grbMasterDirectory.TabStop = false;
            this.grbMasterDirectory.Text = "Master Directory";
            // 
            // lblMaster
            // 
            this.lblMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaster.AutoSize = true;
            this.lblMaster.Location = new System.Drawing.Point(472, 18);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(58, 13);
            this.lblMaster.TabIndex = 2;
            this.lblMaster.Text = "+ \\Master\\";
            // 
            // btnSetMasterFolder
            // 
            this.btnSetMasterFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetMasterFolder.Location = new System.Drawing.Point(557, 15);
            this.btnSetMasterFolder.Name = "btnSetMasterFolder";
            this.btnSetMasterFolder.Size = new System.Drawing.Size(75, 20);
            this.btnSetMasterFolder.TabIndex = 1;
            this.btnSetMasterFolder.Text = "Set Folder";
            this.btnSetMasterFolder.UseVisualStyleBackColor = true;
            this.btnSetMasterFolder.Click += new System.EventHandler(this.BtnSetMasterFolder_Click);
            // 
            // txtMasterDirectory
            // 
            this.txtMasterDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterDirectory.BackColor = System.Drawing.SystemColors.Window;
            this.txtMasterDirectory.Location = new System.Drawing.Point(6, 15);
            this.txtMasterDirectory.Name = "txtMasterDirectory";
            this.txtMasterDirectory.ReadOnly = true;
            this.txtMasterDirectory.Size = new System.Drawing.Size(460, 20);
            this.txtMasterDirectory.TabIndex = 0;
            // 
            // grbHashCodeDirectory
            // 
            this.grbHashCodeDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbHashCodeDirectory.Controls.Add(this.btnSetHashCodesDir);
            this.grbHashCodeDirectory.Controls.Add(this.txtHashCodeFile);
            this.grbHashCodeDirectory.Location = new System.Drawing.Point(12, 53);
            this.grbHashCodeDirectory.Name = "grbHashCodeDirectory";
            this.grbHashCodeDirectory.Size = new System.Drawing.Size(644, 42);
            this.grbHashCodeDirectory.TabIndex = 3;
            this.grbHashCodeDirectory.TabStop = false;
            this.grbHashCodeDirectory.Text = "HashCode File Directory";
            // 
            // btnSetHashCodesDir
            // 
            this.btnSetHashCodesDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetHashCodesDir.Location = new System.Drawing.Point(557, 15);
            this.btnSetHashCodesDir.Name = "btnSetHashCodesDir";
            this.btnSetHashCodesDir.Size = new System.Drawing.Size(75, 20);
            this.btnSetHashCodesDir.TabIndex = 1;
            this.btnSetHashCodesDir.Text = "Set Folder";
            this.btnSetHashCodesDir.UseVisualStyleBackColor = true;
            this.btnSetHashCodesDir.Click += new System.EventHandler(this.BtnSetHashCodesDir_Click);
            // 
            // txtHashCodeFile
            // 
            this.txtHashCodeFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHashCodeFile.BackColor = System.Drawing.SystemColors.Window;
            this.txtHashCodeFile.Location = new System.Drawing.Point(6, 15);
            this.txtHashCodeFile.Name = "txtHashCodeFile";
            this.txtHashCodeFile.ReadOnly = true;
            this.txtHashCodeFile.Size = new System.Drawing.Size(460, 20);
            this.txtHashCodeFile.TabIndex = 0;
            // 
            // grbEngineXPath
            // 
            this.grbEngineXPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbEngineXPath.Controls.Add(this.btnSetEngineXPath);
            this.grbEngineXPath.Controls.Add(this.txtEngineXProject);
            this.grbEngineXPath.Location = new System.Drawing.Point(12, 98);
            this.grbEngineXPath.Name = "grbEngineXPath";
            this.grbEngineXPath.Size = new System.Drawing.Size(644, 42);
            this.grbEngineXPath.TabIndex = 4;
            this.grbEngineXPath.TabStop = false;
            this.grbEngineXPath.Text = "EngineX project path";
            // 
            // btnSetEngineXPath
            // 
            this.btnSetEngineXPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetEngineXPath.Location = new System.Drawing.Point(557, 15);
            this.btnSetEngineXPath.Name = "btnSetEngineXPath";
            this.btnSetEngineXPath.Size = new System.Drawing.Size(75, 20);
            this.btnSetEngineXPath.TabIndex = 1;
            this.btnSetEngineXPath.Text = "Set Folder";
            this.btnSetEngineXPath.UseVisualStyleBackColor = true;
            this.btnSetEngineXPath.Click += new System.EventHandler(this.BtnSetEngineXPath_Click);
            // 
            // txtEngineXProject
            // 
            this.txtEngineXProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEngineXProject.BackColor = System.Drawing.SystemColors.Window;
            this.txtEngineXProject.Location = new System.Drawing.Point(6, 15);
            this.txtEngineXProject.Name = "txtEngineXProject";
            this.txtEngineXProject.ReadOnly = true;
            this.txtEngineXProject.Size = new System.Drawing.Size(460, 20);
            this.txtEngineXProject.TabIndex = 0;
            // 
            // grbEuroLandPath
            // 
            this.grbEuroLandPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbEuroLandPath.Controls.Add(this.btnSetEuroLandServer);
            this.grbEuroLandPath.Controls.Add(this.txtEuroLandServer);
            this.grbEuroLandPath.Location = new System.Drawing.Point(12, 143);
            this.grbEuroLandPath.Name = "grbEuroLandPath";
            this.grbEuroLandPath.Size = new System.Drawing.Size(644, 42);
            this.grbEuroLandPath.TabIndex = 5;
            this.grbEuroLandPath.TabStop = false;
            this.grbEuroLandPath.Text = "EuroLand HashCodes Server Path";
            // 
            // btnSetEuroLandServer
            // 
            this.btnSetEuroLandServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetEuroLandServer.Location = new System.Drawing.Point(557, 15);
            this.btnSetEuroLandServer.Name = "btnSetEuroLandServer";
            this.btnSetEuroLandServer.Size = new System.Drawing.Size(75, 20);
            this.btnSetEuroLandServer.TabIndex = 1;
            this.btnSetEuroLandServer.Text = "Set Folder";
            this.btnSetEuroLandServer.UseVisualStyleBackColor = true;
            this.btnSetEuroLandServer.Click += new System.EventHandler(this.BtnSetEuroLandServer_Click);
            // 
            // txtEuroLandServer
            // 
            this.txtEuroLandServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEuroLandServer.BackColor = System.Drawing.SystemColors.Window;
            this.txtEuroLandServer.Location = new System.Drawing.Point(6, 15);
            this.txtEuroLandServer.Name = "txtEuroLandServer";
            this.txtEuroLandServer.ReadOnly = true;
            this.txtEuroLandServer.Size = new System.Drawing.Size(460, 20);
            this.txtEuroLandServer.TabIndex = 0;
            // 
            // grbAvailableFormat
            // 
            this.grbAvailableFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbAvailableFormat.Controls.Add(this.btnAutoReSampleOff);
            this.grbAvailableFormat.Controls.Add(this.btnAutoReSampleOn);
            this.grbAvailableFormat.Controls.Add(this.btnSearchOutputFolder);
            this.grbAvailableFormat.Controls.Add(this.btnAddFormat);
            this.grbAvailableFormat.Controls.Add(this.cboAvailableFormats);
            this.grbAvailableFormat.Controls.Add(this.lvwAvailableFormats);
            this.grbAvailableFormat.Controls.Add(this.shapeContainer2);
            this.grbAvailableFormat.Location = new System.Drawing.Point(12, 191);
            this.grbAvailableFormat.Name = "grbAvailableFormat";
            this.grbAvailableFormat.Size = new System.Drawing.Size(644, 164);
            this.grbAvailableFormat.TabIndex = 6;
            this.grbAvailableFormat.TabStop = false;
            this.grbAvailableFormat.Text = "Available Format Properties";
            // 
            // btnAutoReSampleOff
            // 
            this.btnAutoReSampleOff.Location = new System.Drawing.Point(569, 135);
            this.btnAutoReSampleOff.Name = "btnAutoReSampleOff";
            this.btnAutoReSampleOff.Size = new System.Drawing.Size(50, 23);
            this.btnAutoReSampleOff.TabIndex = 5;
            this.btnAutoReSampleOff.Text = "Off";
            this.btnAutoReSampleOff.UseVisualStyleBackColor = true;
            this.btnAutoReSampleOff.Click += new System.EventHandler(this.BtnAutoReSampleOff_Click);
            // 
            // btnAutoReSampleOn
            // 
            this.btnAutoReSampleOn.Location = new System.Drawing.Point(513, 135);
            this.btnAutoReSampleOn.Name = "btnAutoReSampleOn";
            this.btnAutoReSampleOn.Size = new System.Drawing.Size(50, 23);
            this.btnAutoReSampleOn.TabIndex = 4;
            this.btnAutoReSampleOn.Text = "On";
            this.btnAutoReSampleOn.UseVisualStyleBackColor = true;
            this.btnAutoReSampleOn.Click += new System.EventHandler(this.BtnAutoReSampleOn_Click);
            // 
            // btnSearchOutputFolder
            // 
            this.btnSearchOutputFolder.Location = new System.Drawing.Point(285, 135);
            this.btnSearchOutputFolder.Name = "btnSearchOutputFolder";
            this.btnSearchOutputFolder.Size = new System.Drawing.Size(147, 23);
            this.btnSearchOutputFolder.TabIndex = 3;
            this.btnSearchOutputFolder.Text = "Browse for output folder";
            this.btnSearchOutputFolder.UseVisualStyleBackColor = true;
            this.btnSearchOutputFolder.Click += new System.EventHandler(this.BtnSearchOutputFolder_Click);
            // 
            // btnAddFormat
            // 
            this.btnAddFormat.Location = new System.Drawing.Point(6, 135);
            this.btnAddFormat.Name = "btnAddFormat";
            this.btnAddFormat.Size = new System.Drawing.Size(75, 23);
            this.btnAddFormat.TabIndex = 2;
            this.btnAddFormat.Text = "Add";
            this.btnAddFormat.UseVisualStyleBackColor = true;
            this.btnAddFormat.Click += new System.EventHandler(this.BtnAddFormat_Click);
            // 
            // cboAvailableFormats
            // 
            this.cboAvailableFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAvailableFormats.FormattingEnabled = true;
            this.cboAvailableFormats.Items.AddRange(new object[] {
            "PlayStation2",
            "X Box",
            "GameCube",
            "PC"});
            this.cboAvailableFormats.Location = new System.Drawing.Point(87, 137);
            this.cboAvailableFormats.Name = "cboAvailableFormats";
            this.cboAvailableFormats.Size = new System.Drawing.Size(140, 21);
            this.cboAvailableFormats.TabIndex = 1;
            // 
            // lvwAvailableFormats
            // 
            this.lvwAvailableFormats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFormatProps_Name,
            this.colFormatProps_OutFolder,
            this.colFormatProps_AutoReSample});
            this.lvwAvailableFormats.FullRowSelect = true;
            this.lvwAvailableFormats.GridLines = true;
            this.lvwAvailableFormats.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwAvailableFormats.HideSelection = false;
            this.lvwAvailableFormats.Location = new System.Drawing.Point(6, 19);
            this.lvwAvailableFormats.Name = "lvwAvailableFormats";
            this.lvwAvailableFormats.Size = new System.Drawing.Size(632, 110);
            this.lvwAvailableFormats.TabIndex = 0;
            this.lvwAvailableFormats.UseCompatibleStateImageBehavior = false;
            this.lvwAvailableFormats.View = System.Windows.Forms.View.Details;
            // 
            // colFormatProps_Name
            // 
            this.colFormatProps_Name.Text = "Available Formats";
            this.colFormatProps_Name.Width = 230;
            // 
            // colFormatProps_OutFolder
            // 
            this.colFormatProps_OutFolder.Text = "Output Folder";
            this.colFormatProps_OutFolder.Width = 250;
            // 
            // colFormatProps_AutoReSample
            // 
            this.colFormatProps_AutoReSample.Text = "Auto Re-Sample On/Off?";
            this.colFormatProps_AutoReSample.Width = 140;
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.LineShape_Divider2,
            this.LineShape_Divider1});
            this.shapeContainer2.Size = new System.Drawing.Size(638, 145);
            this.shapeContainer2.TabIndex = 6;
            this.shapeContainer2.TabStop = false;
            // 
            // LineShape_Divider2
            // 
            this.LineShape_Divider2.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.LineShape_Divider2.Name = "LineShape_Divider2";
            this.LineShape_Divider2.SelectionColor = System.Drawing.SystemColors.Control;
            this.LineShape_Divider2.X1 = 486;
            this.LineShape_Divider2.X2 = 486;
            this.LineShape_Divider2.Y1 = 112;
            this.LineShape_Divider2.Y2 = 145;
            // 
            // LineShape_Divider1
            // 
            this.LineShape_Divider1.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.LineShape_Divider1.Name = "LineShape_Divider1";
            this.LineShape_Divider1.SelectionColor = System.Drawing.SystemColors.Control;
            this.LineShape_Divider1.X1 = 235;
            this.LineShape_Divider1.X2 = 235;
            this.LineShape_Divider1.Y1 = 112;
            this.LineShape_Divider1.Y2 = 145;
            // 
            // grbAvailableReSample
            // 
            this.grbAvailableReSample.Controls.Add(this.pnlListView);
            this.grbAvailableReSample.Controls.Add(this.btnAddSampleRate);
            this.grbAvailableReSample.Location = new System.Drawing.Point(12, 361);
            this.grbAvailableReSample.Name = "grbAvailableReSample";
            this.grbAvailableReSample.Size = new System.Drawing.Size(222, 242);
            this.grbAvailableReSample.TabIndex = 7;
            this.grbAvailableReSample.TabStop = false;
            this.grbAvailableReSample.Text = "Available Re-Sample Rates";
            // 
            // pnlListView
            // 
            this.pnlListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListView.Controls.Add(this.lstAvailableSampleRates);
            this.pnlListView.Location = new System.Drawing.Point(6, 19);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.Size = new System.Drawing.Size(210, 188);
            this.pnlListView.TabIndex = 9;
            // 
            // lstAvailableSampleRates
            // 
            this.lstAvailableSampleRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAvailableSampleRates.FormattingEnabled = true;
            this.lstAvailableSampleRates.Location = new System.Drawing.Point(0, 0);
            this.lstAvailableSampleRates.Name = "lstAvailableSampleRates";
            this.lstAvailableSampleRates.Size = new System.Drawing.Size(210, 188);
            this.lstAvailableSampleRates.TabIndex = 0;
            // 
            // btnAddSampleRate
            // 
            this.btnAddSampleRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddSampleRate.Location = new System.Drawing.Point(6, 213);
            this.btnAddSampleRate.Name = "btnAddSampleRate";
            this.btnAddSampleRate.Size = new System.Drawing.Size(75, 23);
            this.btnAddSampleRate.TabIndex = 8;
            this.btnAddSampleRate.Text = "Add";
            this.btnAddSampleRate.UseVisualStyleBackColor = true;
            this.btnAddSampleRate.Click += new System.EventHandler(this.BtnAddSampleRate_Click);
            // 
            // grbReSampleRatePerFormat
            // 
            this.grbReSampleRatePerFormat.Controls.Add(this.lvwReSampleFormats);
            this.grbReSampleRatePerFormat.Controls.Add(this.cboFormat);
            this.grbReSampleRatePerFormat.Location = new System.Drawing.Point(323, 361);
            this.grbReSampleRatePerFormat.Name = "grbReSampleRatePerFormat";
            this.grbReSampleRatePerFormat.Size = new System.Drawing.Size(333, 242);
            this.grbReSampleRatePerFormat.TabIndex = 8;
            this.grbReSampleRatePerFormat.TabStop = false;
            this.grbReSampleRatePerFormat.Text = "Re-Sample Rate Values per Format";
            // 
            // lvwReSampleFormats
            // 
            this.lvwReSampleFormats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwReSampleFormats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFormat_Label,
            this.colFormat_Rate});
            this.lvwReSampleFormats.FullRowSelect = true;
            this.lvwReSampleFormats.GridLines = true;
            this.lvwReSampleFormats.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwReSampleFormats.HideSelection = false;
            this.lvwReSampleFormats.Location = new System.Drawing.Point(6, 47);
            this.lvwReSampleFormats.Name = "lvwReSampleFormats";
            this.lvwReSampleFormats.Size = new System.Drawing.Size(321, 190);
            this.lvwReSampleFormats.TabIndex = 1;
            this.lvwReSampleFormats.UseCompatibleStateImageBehavior = false;
            this.lvwReSampleFormats.View = System.Windows.Forms.View.Details;
            this.lvwReSampleFormats.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvwReSampleFormats_MouseDoubleClick);
            // 
            // colFormat_Label
            // 
            this.colFormat_Label.Text = "Label";
            this.colFormat_Label.Width = 155;
            // 
            // colFormat_Rate
            // 
            this.colFormat_Rate.Text = "Re-Sample Rate";
            this.colFormat_Rate.Width = 155;
            // 
            // cboFormat
            // 
            this.cboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormat.FormattingEnabled = true;
            this.cboFormat.Location = new System.Drawing.Point(6, 19);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Size = new System.Drawing.Size(214, 21);
            this.cboFormat.TabIndex = 0;
            this.cboFormat.SelectedIndexChanged += new System.EventHandler(this.CboFormat_SelectedIndexChanged);
            // 
            // grbMisc
            // 
            this.grbMisc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbMisc.Controls.Add(this.lblXbox_Unit);
            this.grbMisc.Controls.Add(this.lblGameCube_Unit);
            this.grbMisc.Controls.Add(this.lblPC_Unit);
            this.grbMisc.Controls.Add(this.lblPlaySatitonlblUnit);
            this.grbMisc.Controls.Add(this.chkPreviewCommands);
            this.grbMisc.Controls.Add(this.chkPrefixHashCodes);
            this.grbMisc.Controls.Add(this.nudXboxSzie);
            this.grbMisc.Controls.Add(this.lblXboxSize);
            this.grbMisc.Controls.Add(this.nudGameCubeSize);
            this.grbMisc.Controls.Add(this.nudPCSize);
            this.grbMisc.Controls.Add(this.nudPlayStationSize);
            this.grbMisc.Controls.Add(this.lblGameCubeSize);
            this.grbMisc.Controls.Add(this.lblPCSize);
            this.grbMisc.Controls.Add(this.lblPlayStationSize);
            this.grbMisc.Controls.Add(this.txtTextEditor);
            this.grbMisc.Controls.Add(this.lblTextEditor);
            this.grbMisc.Controls.Add(this.txtUserName);
            this.grbMisc.Controls.Add(this.lblUserName);
            this.grbMisc.Controls.Add(this.txtSoundForge);
            this.grbMisc.Controls.Add(this.lblSoundForge);
            this.grbMisc.Controls.Add(this.txtEditWavsTool);
            this.grbMisc.Controls.Add(this.cboDefaultRate);
            this.grbMisc.Controls.Add(this.lblEditWavs);
            this.grbMisc.Controls.Add(this.lblDefaultSampleRate);
            this.grbMisc.Location = new System.Drawing.Point(12, 609);
            this.grbMisc.Name = "grbMisc";
            this.grbMisc.Size = new System.Drawing.Size(563, 218);
            this.grbMisc.TabIndex = 9;
            this.grbMisc.TabStop = false;
            this.grbMisc.Text = "Misc";
            // 
            // lblXbox_Unit
            // 
            this.lblXbox_Unit.AutoSize = true;
            this.lblXbox_Unit.Location = new System.Drawing.Point(298, 194);
            this.lblXbox_Unit.Name = "lblXbox_Unit";
            this.lblXbox_Unit.Size = new System.Drawing.Size(14, 13);
            this.lblXbox_Unit.TabIndex = 25;
            this.lblXbox_Unit.Text = "K";
            // 
            // lblGameCube_Unit
            // 
            this.lblGameCube_Unit.AutoSize = true;
            this.lblGameCube_Unit.Location = new System.Drawing.Point(298, 174);
            this.lblGameCube_Unit.Name = "lblGameCube_Unit";
            this.lblGameCube_Unit.Size = new System.Drawing.Size(14, 13);
            this.lblGameCube_Unit.TabIndex = 24;
            this.lblGameCube_Unit.Text = "K";
            // 
            // lblPC_Unit
            // 
            this.lblPC_Unit.AutoSize = true;
            this.lblPC_Unit.Location = new System.Drawing.Point(298, 154);
            this.lblPC_Unit.Name = "lblPC_Unit";
            this.lblPC_Unit.Size = new System.Drawing.Size(14, 13);
            this.lblPC_Unit.TabIndex = 23;
            this.lblPC_Unit.Text = "K";
            // 
            // lblPlaySatitonlblUnit
            // 
            this.lblPlaySatitonlblUnit.AutoSize = true;
            this.lblPlaySatitonlblUnit.Location = new System.Drawing.Point(298, 134);
            this.lblPlaySatitonlblUnit.Name = "lblPlaySatitonlblUnit";
            this.lblPlaySatitonlblUnit.Size = new System.Drawing.Size(14, 13);
            this.lblPlaySatitonlblUnit.TabIndex = 22;
            this.lblPlaySatitonlblUnit.Text = "K";
            // 
            // chkPreviewCommands
            // 
            this.chkPreviewCommands.AutoSize = true;
            this.chkPreviewCommands.Location = new System.Drawing.Point(329, 150);
            this.chkPreviewCommands.Name = "chkPreviewCommands";
            this.chkPreviewCommands.Size = new System.Drawing.Size(206, 17);
            this.chkPreviewCommands.TabIndex = 21;
            this.chkPreviewCommands.Text = "View Pre/Post Output Dos Commands";
            this.chkPreviewCommands.UseVisualStyleBackColor = true;
            // 
            // chkPrefixHashCodes
            // 
            this.chkPrefixHashCodes.AutoSize = true;
            this.chkPrefixHashCodes.Location = new System.Drawing.Point(329, 133);
            this.chkPrefixHashCodes.Name = "chkPrefixHashCodes";
            this.chkPrefixHashCodes.Size = new System.Drawing.Size(207, 17);
            this.chkPrefixHashCodes.TabIndex = 20;
            this.chkPrefixHashCodes.Text = "Prefix All HashCodes with HT_Sound_";
            this.chkPrefixHashCodes.UseVisualStyleBackColor = true;
            // 
            // nudXboxSzie
            // 
            this.nudXboxSzie.Location = new System.Drawing.Point(172, 192);
            this.nudXboxSzie.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudXboxSzie.Name = "nudXboxSzie";
            this.nudXboxSzie.Size = new System.Drawing.Size(120, 20);
            this.nudXboxSzie.TabIndex = 19;
            // 
            // lblXboxSize
            // 
            this.lblXboxSize.AutoSize = true;
            this.lblXboxSize.Location = new System.Drawing.Point(6, 194);
            this.lblXboxSize.Name = "lblXboxSize";
            this.lblXboxSize.Size = new System.Drawing.Size(131, 13);
            this.lblXboxSize.TabIndex = 18;
            this.lblXboxSize.Text = "SoundBank Max on Xbox:";
            // 
            // nudGameCubeSize
            // 
            this.nudGameCubeSize.Location = new System.Drawing.Point(172, 172);
            this.nudGameCubeSize.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudGameCubeSize.Name = "nudGameCubeSize";
            this.nudGameCubeSize.Size = new System.Drawing.Size(120, 20);
            this.nudGameCubeSize.TabIndex = 17;
            // 
            // nudPCSize
            // 
            this.nudPCSize.Location = new System.Drawing.Point(172, 152);
            this.nudPCSize.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudPCSize.Name = "nudPCSize";
            this.nudPCSize.Size = new System.Drawing.Size(120, 20);
            this.nudPCSize.TabIndex = 16;
            // 
            // nudPlayStationSize
            // 
            this.nudPlayStationSize.Location = new System.Drawing.Point(172, 132);
            this.nudPlayStationSize.Maximum = new decimal(new int[] {
            1868,
            0,
            0,
            0});
            this.nudPlayStationSize.Name = "nudPlayStationSize";
            this.nudPlayStationSize.Size = new System.Drawing.Size(120, 20);
            this.nudPlayStationSize.TabIndex = 15;
            // 
            // lblGameCubeSize
            // 
            this.lblGameCubeSize.AutoSize = true;
            this.lblGameCubeSize.Location = new System.Drawing.Point(6, 174);
            this.lblGameCubeSize.Name = "lblGameCubeSize";
            this.lblGameCubeSize.Size = new System.Drawing.Size(160, 13);
            this.lblGameCubeSize.TabIndex = 14;
            this.lblGameCubeSize.Text = "SoundBank Max on GameCube:";
            // 
            // lblPCSize
            // 
            this.lblPCSize.AutoSize = true;
            this.lblPCSize.Location = new System.Drawing.Point(6, 154);
            this.lblPCSize.Name = "lblPCSize";
            this.lblPCSize.Size = new System.Drawing.Size(121, 13);
            this.lblPCSize.TabIndex = 12;
            this.lblPCSize.Text = "SoundBank Max on PC:";
            // 
            // lblPlayStationSize
            // 
            this.lblPlayStationSize.AutoSize = true;
            this.lblPlayStationSize.Location = new System.Drawing.Point(6, 134);
            this.lblPlayStationSize.Name = "lblPlayStationSize";
            this.lblPlayStationSize.Size = new System.Drawing.Size(160, 13);
            this.lblPlayStationSize.TabIndex = 10;
            this.lblPlayStationSize.Text = "SoundBank Max on PlayStation:";
            // 
            // txtTextEditor
            // 
            this.txtTextEditor.BackColor = System.Drawing.SystemColors.Window;
            this.txtTextEditor.Location = new System.Drawing.Point(100, 102);
            this.txtTextEditor.Name = "txtTextEditor";
            this.txtTextEditor.ReadOnly = true;
            this.txtTextEditor.Size = new System.Drawing.Size(457, 20);
            this.txtTextEditor.TabIndex = 9;
            this.txtTextEditor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TxtTextEditor_MouseDoubleClick);
            // 
            // lblTextEditor
            // 
            this.lblTextEditor.AutoSize = true;
            this.lblTextEditor.Location = new System.Drawing.Point(5, 105);
            this.lblTextEditor.Name = "lblTextEditor";
            this.lblTextEditor.Size = new System.Drawing.Size(61, 13);
            this.lblTextEditor.TabIndex = 8;
            this.lblTextEditor.Text = "Text Editor:";
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserName.Location = new System.Drawing.Point(100, 80);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(457, 20);
            this.txtUserName.TabIndex = 7;
            this.txtUserName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TxtUserName_DoubleClick);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(6, 83);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 13);
            this.lblUserName.TabIndex = 6;
            this.lblUserName.Text = "UserName:";
            // 
            // txtSoundForge
            // 
            this.txtSoundForge.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoundForge.Location = new System.Drawing.Point(100, 58);
            this.txtSoundForge.Name = "txtSoundForge";
            this.txtSoundForge.ReadOnly = true;
            this.txtSoundForge.Size = new System.Drawing.Size(457, 20);
            this.txtSoundForge.TabIndex = 5;
            this.txtSoundForge.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TxtSoundForge_MouseDoubleClick);
            // 
            // lblSoundForge
            // 
            this.lblSoundForge.AutoSize = true;
            this.lblSoundForge.Location = new System.Drawing.Point(6, 61);
            this.lblSoundForge.Name = "lblSoundForge";
            this.lblSoundForge.Size = new System.Drawing.Size(88, 13);
            this.lblSoundForge.TabIndex = 4;
            this.lblSoundForge.Text = "SoundForge.exe:";
            // 
            // txtEditWavsTool
            // 
            this.txtEditWavsTool.BackColor = System.Drawing.SystemColors.Window;
            this.txtEditWavsTool.Location = new System.Drawing.Point(100, 36);
            this.txtEditWavsTool.Name = "txtEditWavsTool";
            this.txtEditWavsTool.ReadOnly = true;
            this.txtEditWavsTool.Size = new System.Drawing.Size(457, 20);
            this.txtEditWavsTool.TabIndex = 3;
            this.txtEditWavsTool.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TxtEditWavsTool_MouseDoubleClick);
            // 
            // cboDefaultRate
            // 
            this.cboDefaultRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDefaultRate.FormattingEnabled = true;
            this.cboDefaultRate.Location = new System.Drawing.Point(119, 14);
            this.cboDefaultRate.Name = "cboDefaultRate";
            this.cboDefaultRate.Size = new System.Drawing.Size(200, 21);
            this.cboDefaultRate.TabIndex = 2;
            this.cboDefaultRate.SelectedIndexChanged += new System.EventHandler(this.CboDefaultRate_SelectedIndexChanged);
            // 
            // lblEditWavs
            // 
            this.lblEditWavs.AutoSize = true;
            this.lblEditWavs.Location = new System.Drawing.Point(6, 39);
            this.lblEditWavs.Name = "lblEditWavs";
            this.lblEditWavs.Size = new System.Drawing.Size(81, 13);
            this.lblEditWavs.TabIndex = 1;
            this.lblEditWavs.Text = "Edit Wavs with:";
            // 
            // lblDefaultSampleRate
            // 
            this.lblDefaultSampleRate.AutoSize = true;
            this.lblDefaultSampleRate.Location = new System.Drawing.Point(5, 17);
            this.lblDefaultSampleRate.Name = "lblDefaultSampleRate";
            this.lblDefaultSampleRate.Size = new System.Drawing.Size(108, 13);
            this.lblDefaultSampleRate.TabIndex = 0;
            this.lblDefaultSampleRate.Text = "Default Sample Rate:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(581, 775);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(581, 804);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.LineShape_Divider4,
            this.LineShape_Divider3});
            this.shapeContainer1.Size = new System.Drawing.Size(668, 834);
            this.shapeContainer1.TabIndex = 12;
            this.shapeContainer1.TabStop = false;
            // 
            // LineShape_Divider4
            // 
            this.LineShape_Divider4.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.LineShape_Divider4.Name = "LineShape_Divider4";
            this.LineShape_Divider4.SelectionColor = System.Drawing.SystemColors.Control;
            this.LineShape_Divider4.X1 = 279;
            this.LineShape_Divider4.X2 = 279;
            this.LineShape_Divider4.Y1 = 357;
            this.LineShape_Divider4.Y2 = 602;
            // 
            // LineShape_Divider3
            // 
            this.LineShape_Divider3.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.LineShape_Divider3.Name = "LineShape_Divider3";
            this.LineShape_Divider3.SelectionColor = System.Drawing.SystemColors.Control;
            this.LineShape_Divider3.X1 = 11;
            this.LineShape_Divider3.X2 = 655;
            this.LineShape_Divider3.Y1 = 356;
            this.LineShape_Divider3.Y2 = 356;
            // 
            // OpenFileDiag
            // 
            this.OpenFileDiag.Filter = "EXE Files (*.exe)|*.exe";
            // 
            // PropertiesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(668, 834);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grbMisc);
            this.Controls.Add(this.grbReSampleRatePerFormat);
            this.Controls.Add(this.grbAvailableReSample);
            this.Controls.Add(this.grbAvailableFormat);
            this.Controls.Add(this.grbEuroLandPath);
            this.Controls.Add(this.grbEngineXPath);
            this.Controls.Add(this.grbHashCodeDirectory);
            this.Controls.Add(this.grbMasterDirectory);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_ProjectProperties_FormClosing);
            this.Load += new System.EventHandler(this.Frm_ProjectProperties_Load);
            this.grbMasterDirectory.ResumeLayout(false);
            this.grbMasterDirectory.PerformLayout();
            this.grbHashCodeDirectory.ResumeLayout(false);
            this.grbHashCodeDirectory.PerformLayout();
            this.grbEngineXPath.ResumeLayout(false);
            this.grbEngineXPath.PerformLayout();
            this.grbEuroLandPath.ResumeLayout(false);
            this.grbEuroLandPath.PerformLayout();
            this.grbAvailableFormat.ResumeLayout(false);
            this.grbAvailableReSample.ResumeLayout(false);
            this.pnlListView.ResumeLayout(false);
            this.grbReSampleRatePerFormat.ResumeLayout(false);
            this.grbMisc.ResumeLayout(false);
            this.grbMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudXboxSzie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameCubeSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPCSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayStationSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMasterDirectory;
        private System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.Button btnSetMasterFolder;
        private System.Windows.Forms.TextBox txtMasterDirectory;
        private System.Windows.Forms.GroupBox grbHashCodeDirectory;
        private System.Windows.Forms.Button btnSetHashCodesDir;
        private System.Windows.Forms.TextBox txtHashCodeFile;
        private System.Windows.Forms.GroupBox grbEngineXPath;
        private System.Windows.Forms.Button btnSetEngineXPath;
        private System.Windows.Forms.TextBox txtEngineXProject;
        private System.Windows.Forms.GroupBox grbEuroLandPath;
        private System.Windows.Forms.Button btnSetEuroLandServer;
        private System.Windows.Forms.TextBox txtEuroLandServer;
        private System.Windows.Forms.GroupBox grbAvailableFormat;
        private System.Windows.Forms.ListView lvwAvailableFormats;
        private System.Windows.Forms.ColumnHeader colFormatProps_Name;
        private System.Windows.Forms.ColumnHeader colFormatProps_OutFolder;
        private System.Windows.Forms.ColumnHeader colFormatProps_AutoReSample;
        private System.Windows.Forms.Button btnAutoReSampleOff;
        private System.Windows.Forms.Button btnAutoReSampleOn;
        private System.Windows.Forms.Button btnSearchOutputFolder;
        private System.Windows.Forms.Button btnAddFormat;
        private System.Windows.Forms.ComboBox cboAvailableFormats;
        private System.Windows.Forms.GroupBox grbAvailableReSample;
        private System.Windows.Forms.Panel pnlListView;
        private System.Windows.Forms.ListBox lstAvailableSampleRates;
        private System.Windows.Forms.Button btnAddSampleRate;
        private System.Windows.Forms.GroupBox grbReSampleRatePerFormat;
        private System.Windows.Forms.ListView lvwReSampleFormats;
        private System.Windows.Forms.ColumnHeader colFormat_Label;
        private System.Windows.Forms.ColumnHeader colFormat_Rate;
        private System.Windows.Forms.ComboBox cboFormat;
        private System.Windows.Forms.GroupBox grbMisc;
        private System.Windows.Forms.NumericUpDown nudXboxSzie;
        private System.Windows.Forms.Label lblXboxSize;
        private System.Windows.Forms.NumericUpDown nudGameCubeSize;
        private System.Windows.Forms.NumericUpDown nudPCSize;
        private System.Windows.Forms.NumericUpDown nudPlayStationSize;
        private System.Windows.Forms.Label lblGameCubeSize;
        private System.Windows.Forms.Label lblPCSize;
        private System.Windows.Forms.Label lblPlayStationSize;
        private System.Windows.Forms.TextBox txtTextEditor;
        private System.Windows.Forms.Label lblTextEditor;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtSoundForge;
        private System.Windows.Forms.Label lblSoundForge;
        private System.Windows.Forms.TextBox txtEditWavsTool;
        private System.Windows.Forms.ComboBox cboDefaultRate;
        private System.Windows.Forms.Label lblEditWavs;
        private System.Windows.Forms.Label lblDefaultSampleRate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkPreviewCommands;
        private System.Windows.Forms.CheckBox chkPrefixHashCodes;
        private System.Windows.Forms.Label lblXbox_Unit;
        private System.Windows.Forms.Label lblGameCube_Unit;
        private System.Windows.Forms.Label lblPC_Unit;
        private System.Windows.Forms.Label lblPlaySatitonlblUnit;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape LineShape_Divider2;
        private Microsoft.VisualBasic.PowerPacks.LineShape LineShape_Divider1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape LineShape_Divider4;
        private Microsoft.VisualBasic.PowerPacks.LineShape LineShape_Divider3;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.OpenFileDialog OpenFileDiag;
    }
}
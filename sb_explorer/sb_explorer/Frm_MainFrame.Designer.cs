
namespace sb_explorer
{
    partial class Frm_MainFrame
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MainFrame));
            this.Textbox_GroupHashcode = new System.Windows.Forms.TextBox();
            this.Label_GroupHashCode = new System.Windows.Forms.Label();
            this.vOuterRadiusReal = new System.Windows.Forms.TextBox();
            this.Label_OuterRadiusReal = new System.Windows.Forms.Label();
            this.vInnerRadiusReal = new System.Windows.Forms.TextBox();
            this.Label_InnerRadiusReal = new System.Windows.Forms.Label();
            this.vDuckerLength = new System.Windows.Forms.TextBox();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ListView_HashCodes = new System.Windows.Forms.ListView();
            this.HashcodesList_HashCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HashcodesList_Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HashcodesList_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu_CopyHashCodeInfo = new System.Windows.Forms.ContextMenu();
            this.ContextMenuItem_CopyName = new System.Windows.Forms.MenuItem();
            this.ContextMenuItem_CopyHashCode = new System.Windows.Forms.MenuItem();
            this.Panel_DetailsAndStreamData = new System.Windows.Forms.Panel();
            this.splitContainer_details = new System.Windows.Forms.SplitContainer();
            this.Label_SoundBank_Name = new System.Windows.Forms.Label();
            this.SoundbankFileName = new System.Windows.Forms.TextBox();
            this.Label_HashCodeName = new System.Windows.Forms.Label();
            this.HashcodeName = new System.Windows.Forms.TextBox();
            this.Panel_ObjectInfo = new System.Windows.Forms.Panel();
            this.CheckedListBox_SampleFlags = new System.Windows.Forms.CheckedListBox();
            this.CheckedListBox_UserFlags = new System.Windows.Forms.CheckedListBox();
            this.Label_SampleCount = new System.Windows.Forms.Label();
            this.vSampleCount = new System.Windows.Forms.TextBox();
            this.Label_SamplePool = new System.Windows.Forms.Label();
            this.ListView_SamplePool = new System.Windows.Forms.ListView();
            this.SamplePool_Hashcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SamplePool_Volume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SamplePool_VolOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SamplePool_Pitch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SamplePool_PitchOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SamplePool_Pan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SamplePool_PanOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Textbox_GroupMaxChannels = new System.Windows.Forms.TextBox();
            this.Label_GroupMaxChannels = new System.Windows.Forms.Label();
            this.Label_DuckerLength = new System.Windows.Forms.Label();
            this.vMasterVolume = new System.Windows.Forms.TextBox();
            this.Label_MasterVolume = new System.Windows.Forms.Label();
            this.vDucker = new System.Windows.Forms.TextBox();
            this.Label_Ducker = new System.Windows.Forms.Label();
            this.vPriority = new System.Windows.Forms.TextBox();
            this.Label_Priority = new System.Windows.Forms.Label();
            this.vMaxVoices = new System.Windows.Forms.TextBox();
            this.Label_MaxVoices = new System.Windows.Forms.Label();
            this.vReverbSend = new System.Windows.Forms.TextBox();
            this.Label_ReverbSend = new System.Windows.Forms.Label();
            this.vMaxDelay = new System.Windows.Forms.TextBox();
            this.Label_MaxDelay = new System.Windows.Forms.Label();
            this.vMinDelay = new System.Windows.Forms.TextBox();
            this.Label_MinDelay = new System.Windows.Forms.Label();
            this.Textbox_TrackingType = new System.Windows.Forms.TextBox();
            this.Label_TrackingType = new System.Windows.Forms.Label();
            this.Button_ReloadFile = new System.Windows.Forms.Button();
            this.Tab_Options = new System.Windows.Forms.TabControl();
            this.Tab_HexView = new System.Windows.Forms.TabPage();
            this.Label_HexViewer_Key = new System.Windows.Forms.Label();
            this.Label_HexViewer_SamplePool = new System.Windows.Forms.Label();
            this.Button_HexViewer_SamplePool = new System.Windows.Forms.Button();
            this.Label_HexViewer_UserFlags = new System.Windows.Forms.Label();
            this.Button_HexViewer_UserFlags = new System.Windows.Forms.Button();
            this.Label_HexViewer_Flags = new System.Windows.Forms.Label();
            this.Button_HexViewer_Flags = new System.Windows.Forms.Button();
            this.Label_HexViewer_HeaderData = new System.Windows.Forms.Label();
            this.Button_HexViewer_HeaderData = new System.Windows.Forms.Button();
            this.ListView_HexEditor = new System.Windows.Forms.ListView();
            this.HexViewer_Offset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_Byte1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_Byte2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_Byte3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_Byte4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_Byte5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_Byte6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_Byte7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_Byte8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexViewer_ASCII = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu_HexView_ChangeFont = new System.Windows.Forms.ContextMenu();
            this.MenuItem_HexContextMenu_ChangeFont = new System.Windows.Forms.MenuItem();
            this.Tab_Wav_Head_Data = new System.Windows.Forms.TabPage();
            this.ListView_WavData = new sb_explorer.CustomControls.ListView_ColumnSortingClick();
            this.WavHeaderData_Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WavHeaderData_Flags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WavHeaderData_Address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WavHeaderData_MemorySize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WavHeaderData_SampleSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WavHeaderData_Frequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WavHeaderData_LoopStartOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WavHeaderData_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu_ListView_WavData = new System.Windows.Forms.ContextMenu();
            this.ListViewWavData_Options = new System.Windows.Forms.MenuItem();
            this.Tab_Stream_Data = new System.Windows.Forms.TabPage();
            this.Button_ValidateADPCM = new System.Windows.Forms.Button();
            this.Textbox_StreamFilePath = new System.Windows.Forms.TextBox();
            this.ListView_StreamData = new System.Windows.Forms.ListView();
            this.StreamData_Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_AdpcmStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_MarkerOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_MarkerSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_AudioOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_AudioLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_BaseVolume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu_ExporStreamtMarkers = new System.Windows.Forms.ContextMenu();
            this.MenuItem_ExportStreamMarkers = new System.Windows.Forms.MenuItem();
            this.ListViewStreamData_Options = new System.Windows.Forms.MenuItem();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ListView_StreamData_StartMarkers = new System.Windows.Forms.ListView();
            this.StreamData_StartMarkers_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_StartMarkers_Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_StartMarkers_Position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_StartMarkers_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_StartMarkers_LoopStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_StartMarkers_LoopMarkerIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_StartMarkers_MarkerPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label_StreamData_StartMarkerCount = new System.Windows.Forms.Label();
            this.textStartMarkerCount = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.ListView_StreamData_Markers = new System.Windows.Forms.ListView();
            this.StreamData_Markers_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_Markers_StartMarkerIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_Markers_Position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_Markers_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_Markers_LoopStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StreamData_Markers_LoopMarkerIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label_StreamData_MarkerCount = new System.Windows.Forms.Label();
            this.textMarkerCount = new System.Windows.Forms.TextBox();
            this.Button_LoadStreamData = new System.Windows.Forms.Button();
            this.MainStatusBar = new System.Windows.Forms.StatusBar();
            this.StatusLabel_HashCode = new System.Windows.Forms.StatusBarPanel();
            this.StatusLabel_Version = new System.Windows.Forms.StatusBarPanel();
            this.StatusLabel_Platform = new System.Windows.Forms.StatusBarPanel();
            this.StatusLabel_SoundhDir = new System.Windows.Forms.StatusBarPanel();
            this.MenuItem_File = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_OpenSoundbank = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_ViewMusic = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_Separator1 = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_SoundhDir = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_Separator2 = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_RecentFiles = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_Separator3 = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_Exit = new System.Windows.Forms.MenuItem();
            this.MenuItem_View = new System.Windows.Forms.MenuItem();
            this.MenuItemView_FindHashCode = new System.Windows.Forms.MenuItem();
            this.MenuItemView_FindNextHashCode = new System.Windows.Forms.MenuItem();
            this.MenuItem_Help = new System.Windows.Forms.MenuItem();
            this.MenuItemHelp_About = new System.Windows.Forms.MenuItem();
            this.Form_MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.openFileDialog_Soundbanks = new System.Windows.Forms.OpenFileDialog();
            this.HexViewfontDialog = new System.Windows.Forms.FontDialog();
            this.openFileDialog_MusicFile = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog_StreamFile = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDlg_SaveFile = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Panel_DetailsAndStreamData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_details)).BeginInit();
            this.splitContainer_details.Panel1.SuspendLayout();
            this.splitContainer_details.Panel2.SuspendLayout();
            this.splitContainer_details.SuspendLayout();
            this.Panel_ObjectInfo.SuspendLayout();
            this.Tab_Options.SuspendLayout();
            this.Tab_HexView.SuspendLayout();
            this.Tab_Wav_Head_Data.SuspendLayout();
            this.Tab_Stream_Data.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel_HashCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel_Version)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel_Platform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel_SoundhDir)).BeginInit();
            this.SuspendLayout();
            // 
            // Textbox_GroupHashcode
            // 
            this.Textbox_GroupHashcode.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_GroupHashcode.Location = new System.Drawing.Point(424, 24);
            this.Textbox_GroupHashcode.Name = "Textbox_GroupHashcode";
            this.Textbox_GroupHashcode.ReadOnly = true;
            this.Textbox_GroupHashcode.Size = new System.Drawing.Size(96, 20);
            this.Textbox_GroupHashcode.TabIndex = 23;
            this.Textbox_GroupHashcode.Text = "0";
            // 
            // Label_GroupHashCode
            // 
            this.Label_GroupHashCode.AutoSize = true;
            this.Label_GroupHashCode.ForeColor = System.Drawing.Color.Blue;
            this.Label_GroupHashCode.Location = new System.Drawing.Point(424, 8);
            this.Label_GroupHashCode.Name = "Label_GroupHashCode";
            this.Label_GroupHashCode.Size = new System.Drawing.Size(88, 13);
            this.Label_GroupHashCode.TabIndex = 22;
            this.Label_GroupHashCode.Text = "Group Hashcode";
            // 
            // vOuterRadiusReal
            // 
            this.vOuterRadiusReal.BackColor = System.Drawing.SystemColors.Control;
            this.vOuterRadiusReal.Location = new System.Drawing.Point(112, 120);
            this.vOuterRadiusReal.Name = "vOuterRadiusReal";
            this.vOuterRadiusReal.ReadOnly = true;
            this.vOuterRadiusReal.Size = new System.Drawing.Size(96, 20);
            this.vOuterRadiusReal.TabIndex = 11;
            this.vOuterRadiusReal.Text = "0";
            // 
            // Label_OuterRadiusReal
            // 
            this.Label_OuterRadiusReal.AutoSize = true;
            this.Label_OuterRadiusReal.Location = new System.Drawing.Point(112, 104);
            this.Label_OuterRadiusReal.Name = "Label_OuterRadiusReal";
            this.Label_OuterRadiusReal.Size = new System.Drawing.Size(69, 13);
            this.Label_OuterRadiusReal.TabIndex = 10;
            this.Label_OuterRadiusReal.Text = "Outer Radius";
            // 
            // vInnerRadiusReal
            // 
            this.vInnerRadiusReal.BackColor = System.Drawing.SystemColors.Control;
            this.vInnerRadiusReal.Location = new System.Drawing.Point(8, 120);
            this.vInnerRadiusReal.Name = "vInnerRadiusReal";
            this.vInnerRadiusReal.ReadOnly = true;
            this.vInnerRadiusReal.Size = new System.Drawing.Size(96, 20);
            this.vInnerRadiusReal.TabIndex = 5;
            this.vInnerRadiusReal.Text = "0";
            // 
            // Label_InnerRadiusReal
            // 
            this.Label_InnerRadiusReal.AutoSize = true;
            this.Label_InnerRadiusReal.Location = new System.Drawing.Point(8, 104);
            this.Label_InnerRadiusReal.Name = "Label_InnerRadiusReal";
            this.Label_InnerRadiusReal.Size = new System.Drawing.Size(67, 13);
            this.Label_InnerRadiusReal.TabIndex = 4;
            this.Label_InnerRadiusReal.Text = "Inner Radius";
            // 
            // vDuckerLength
            // 
            this.vDuckerLength.BackColor = System.Drawing.SystemColors.Control;
            this.vDuckerLength.Location = new System.Drawing.Point(216, 120);
            this.vDuckerLength.Name = "vDuckerLength";
            this.vDuckerLength.ReadOnly = true;
            this.vDuckerLength.Size = new System.Drawing.Size(96, 20);
            this.vDuckerLength.TabIndex = 17;
            this.vDuckerLength.Text = "0";
            // 
            // FolderBrowserDialog
            // 
            this.FolderBrowserDialog.Description = "Select Sound.h Folder";
            this.FolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.FolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ListView_HashCodes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Panel_DetailsAndStreamData);
            this.splitContainer1.Panel2.Controls.Add(this.MainStatusBar);
            this.splitContainer1.Size = new System.Drawing.Size(1192, 889);
            this.splitContainer1.SplitterDistance = 216;
            this.splitContainer1.TabIndex = 0;
            // 
            // ListView_HashCodes
            // 
            this.ListView_HashCodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HashcodesList_HashCode,
            this.HashcodesList_Status,
            this.HashcodesList_Name});
            this.ListView_HashCodes.ContextMenu = this.ContextMenu_CopyHashCodeInfo;
            this.ListView_HashCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_HashCodes.FullRowSelect = true;
            this.ListView_HashCodes.HideSelection = false;
            this.ListView_HashCodes.Location = new System.Drawing.Point(0, 0);
            this.ListView_HashCodes.Name = "ListView_HashCodes";
            this.ListView_HashCodes.Size = new System.Drawing.Size(216, 889);
            this.ListView_HashCodes.TabIndex = 0;
            this.ListView_HashCodes.UseCompatibleStateImageBehavior = false;
            this.ListView_HashCodes.View = System.Windows.Forms.View.Details;
            this.ListView_HashCodes.SelectedIndexChanged += new System.EventHandler(this.ListView_HashCodes_SelectedIndexChanged);
            // 
            // HashcodesList_HashCode
            // 
            this.HashcodesList_HashCode.Text = "HashCode";
            this.HashcodesList_HashCode.Width = 75;
            // 
            // HashcodesList_Status
            // 
            this.HashcodesList_Status.Text = "OK";
            this.HashcodesList_Status.Width = 30;
            // 
            // HashcodesList_Name
            // 
            this.HashcodesList_Name.Text = "Name";
            this.HashcodesList_Name.Width = 100;
            // 
            // ContextMenu_CopyHashCodeInfo
            // 
            this.ContextMenu_CopyHashCodeInfo.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ContextMenuItem_CopyName,
            this.ContextMenuItem_CopyHashCode});
            // 
            // ContextMenuItem_CopyName
            // 
            this.ContextMenuItem_CopyName.Index = 0;
            this.ContextMenuItem_CopyName.Text = "Copy Name";
            this.ContextMenuItem_CopyName.Click += new System.EventHandler(this.ContextMenuItem_CopyName_Click);
            // 
            // ContextMenuItem_CopyHashCode
            // 
            this.ContextMenuItem_CopyHashCode.Index = 1;
            this.ContextMenuItem_CopyHashCode.Text = "Copy HashCode";
            this.ContextMenuItem_CopyHashCode.Click += new System.EventHandler(this.ContextMenuItem_CopyHashCode_Click);
            // 
            // Panel_DetailsAndStreamData
            // 
            this.Panel_DetailsAndStreamData.AutoScroll = true;
            this.Panel_DetailsAndStreamData.Controls.Add(this.splitContainer_details);
            this.Panel_DetailsAndStreamData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_DetailsAndStreamData.Location = new System.Drawing.Point(0, 0);
            this.Panel_DetailsAndStreamData.Name = "Panel_DetailsAndStreamData";
            this.Panel_DetailsAndStreamData.Size = new System.Drawing.Size(972, 867);
            this.Panel_DetailsAndStreamData.TabIndex = 3;
            // 
            // splitContainer_details
            // 
            this.splitContainer_details.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_details.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_details.Name = "splitContainer_details";
            // 
            // splitContainer_details.Panel1
            // 
            this.splitContainer_details.Panel1.Controls.Add(this.Label_SoundBank_Name);
            this.splitContainer_details.Panel1.Controls.Add(this.SoundbankFileName);
            this.splitContainer_details.Panel1.Controls.Add(this.Label_HashCodeName);
            this.splitContainer_details.Panel1.Controls.Add(this.HashcodeName);
            this.splitContainer_details.Panel1.Controls.Add(this.Panel_ObjectInfo);
            this.splitContainer_details.Panel1.Controls.Add(this.Button_ReloadFile);
            // 
            // splitContainer_details.Panel2
            // 
            this.splitContainer_details.Panel2.Controls.Add(this.Tab_Options);
            this.splitContainer_details.Size = new System.Drawing.Size(972, 867);
            this.splitContainer_details.SplitterDistance = 552;
            this.splitContainer_details.TabIndex = 1;
            // 
            // Label_SoundBank_Name
            // 
            this.Label_SoundBank_Name.Location = new System.Drawing.Point(8, 0);
            this.Label_SoundBank_Name.Name = "Label_SoundBank_Name";
            this.Label_SoundBank_Name.Size = new System.Drawing.Size(112, 16);
            this.Label_SoundBank_Name.TabIndex = 0;
            this.Label_SoundBank_Name.Text = "Soundbank filename";
            // 
            // SoundbankFileName
            // 
            this.SoundbankFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SoundbankFileName.BackColor = System.Drawing.SystemColors.Window;
            this.SoundbankFileName.HideSelection = false;
            this.SoundbankFileName.Location = new System.Drawing.Point(8, 16);
            this.SoundbankFileName.Name = "SoundbankFileName";
            this.SoundbankFileName.ReadOnly = true;
            this.SoundbankFileName.Size = new System.Drawing.Size(544, 20);
            this.SoundbankFileName.TabIndex = 1;
            // 
            // Label_HashCodeName
            // 
            this.Label_HashCodeName.Location = new System.Drawing.Point(8, 40);
            this.Label_HashCodeName.Name = "Label_HashCodeName";
            this.Label_HashCodeName.Size = new System.Drawing.Size(96, 16);
            this.Label_HashCodeName.TabIndex = 2;
            this.Label_HashCodeName.Text = "Hashcode Name";
            // 
            // HashcodeName
            // 
            this.HashcodeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HashcodeName.BackColor = System.Drawing.SystemColors.Window;
            this.HashcodeName.HideSelection = false;
            this.HashcodeName.Location = new System.Drawing.Point(8, 56);
            this.HashcodeName.Name = "HashcodeName";
            this.HashcodeName.ReadOnly = true;
            this.HashcodeName.Size = new System.Drawing.Size(544, 20);
            this.HashcodeName.TabIndex = 3;
            // 
            // Panel_ObjectInfo
            // 
            this.Panel_ObjectInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_ObjectInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel_ObjectInfo.Controls.Add(this.CheckedListBox_SampleFlags);
            this.Panel_ObjectInfo.Controls.Add(this.CheckedListBox_UserFlags);
            this.Panel_ObjectInfo.Controls.Add(this.Label_SampleCount);
            this.Panel_ObjectInfo.Controls.Add(this.vSampleCount);
            this.Panel_ObjectInfo.Controls.Add(this.Label_SamplePool);
            this.Panel_ObjectInfo.Controls.Add(this.ListView_SamplePool);
            this.Panel_ObjectInfo.Controls.Add(this.Textbox_GroupMaxChannels);
            this.Panel_ObjectInfo.Controls.Add(this.Label_GroupMaxChannels);
            this.Panel_ObjectInfo.Controls.Add(this.Textbox_GroupHashcode);
            this.Panel_ObjectInfo.Controls.Add(this.Label_GroupHashCode);
            this.Panel_ObjectInfo.Controls.Add(this.vOuterRadiusReal);
            this.Panel_ObjectInfo.Controls.Add(this.Label_OuterRadiusReal);
            this.Panel_ObjectInfo.Controls.Add(this.vInnerRadiusReal);
            this.Panel_ObjectInfo.Controls.Add(this.Label_InnerRadiusReal);
            this.Panel_ObjectInfo.Controls.Add(this.vDuckerLength);
            this.Panel_ObjectInfo.Controls.Add(this.Label_DuckerLength);
            this.Panel_ObjectInfo.Controls.Add(this.vMasterVolume);
            this.Panel_ObjectInfo.Controls.Add(this.Label_MasterVolume);
            this.Panel_ObjectInfo.Controls.Add(this.vDucker);
            this.Panel_ObjectInfo.Controls.Add(this.Label_Ducker);
            this.Panel_ObjectInfo.Controls.Add(this.vPriority);
            this.Panel_ObjectInfo.Controls.Add(this.Label_Priority);
            this.Panel_ObjectInfo.Controls.Add(this.vMaxVoices);
            this.Panel_ObjectInfo.Controls.Add(this.Label_MaxVoices);
            this.Panel_ObjectInfo.Controls.Add(this.vReverbSend);
            this.Panel_ObjectInfo.Controls.Add(this.Label_ReverbSend);
            this.Panel_ObjectInfo.Controls.Add(this.vMaxDelay);
            this.Panel_ObjectInfo.Controls.Add(this.Label_MaxDelay);
            this.Panel_ObjectInfo.Controls.Add(this.vMinDelay);
            this.Panel_ObjectInfo.Controls.Add(this.Label_MinDelay);
            this.Panel_ObjectInfo.Controls.Add(this.Textbox_TrackingType);
            this.Panel_ObjectInfo.Controls.Add(this.Label_TrackingType);
            this.Panel_ObjectInfo.Location = new System.Drawing.Point(0, 80);
            this.Panel_ObjectInfo.Name = "Panel_ObjectInfo";
            this.Panel_ObjectInfo.Size = new System.Drawing.Size(552, 632);
            this.Panel_ObjectInfo.TabIndex = 4;
            // 
            // CheckedListBox_SampleFlags
            // 
            this.CheckedListBox_SampleFlags.ForeColor = System.Drawing.Color.Blue;
            this.CheckedListBox_SampleFlags.Items.AddRange(new object[] {
            "MaxReject",
            "NextFreeOneToUse",
            "IgnoreAge",
            "MultiSample",
            "RandomPick",
            "Shuffled",
            "Loop",
            "Polyphonic",
            "UnderWater",
            "PauseInNis",
            "HasSubSFX",
            "StealOnLouder",
            "TreatLikeMusic",
            "UserFlags14",
            "UserFlags15",
            "UserFlags16"});
            this.CheckedListBox_SampleFlags.Location = new System.Drawing.Point(8, 160);
            this.CheckedListBox_SampleFlags.Name = "CheckedListBox_SampleFlags";
            this.CheckedListBox_SampleFlags.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.CheckedListBox_SampleFlags.Size = new System.Drawing.Size(168, 244);
            this.CheckedListBox_SampleFlags.TabIndex = 26;
            // 
            // CheckedListBox_UserFlags
            // 
            this.CheckedListBox_UserFlags.ForeColor = System.Drawing.Color.Purple;
            this.CheckedListBox_UserFlags.Items.AddRange(new object[] {
            "UserFlags1",
            "UserFlags2",
            "UserFlags3",
            "UserFlags4",
            "UserFlags5",
            "UserFlags6",
            "UserFlags7",
            "UserFlags8",
            "UserFlags9",
            "UserFlags10",
            "UserFlags11",
            "UserFlags12",
            "UserFlags13",
            "UserFlags14",
            "UserFlags15",
            "UserFlags16"});
            this.CheckedListBox_UserFlags.Location = new System.Drawing.Point(184, 160);
            this.CheckedListBox_UserFlags.Name = "CheckedListBox_UserFlags";
            this.CheckedListBox_UserFlags.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.CheckedListBox_UserFlags.Size = new System.Drawing.Size(168, 244);
            this.CheckedListBox_UserFlags.TabIndex = 27;
            // 
            // Label_SampleCount
            // 
            this.Label_SampleCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_SampleCount.AutoSize = true;
            this.Label_SampleCount.ForeColor = System.Drawing.Color.Green;
            this.Label_SampleCount.Location = new System.Drawing.Point(338, 416);
            this.Label_SampleCount.Name = "Label_SampleCount";
            this.Label_SampleCount.Size = new System.Drawing.Size(73, 13);
            this.Label_SampleCount.TabIndex = 29;
            this.Label_SampleCount.Text = "Sample Count";
            // 
            // vSampleCount
            // 
            this.vSampleCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vSampleCount.Location = new System.Drawing.Point(417, 413);
            this.vSampleCount.Name = "vSampleCount";
            this.vSampleCount.Size = new System.Drawing.Size(128, 20);
            this.vSampleCount.TabIndex = 30;
            this.vSampleCount.Text = "0";
            // 
            // Label_SamplePool
            // 
            this.Label_SamplePool.AutoSize = true;
            this.Label_SamplePool.ForeColor = System.Drawing.Color.Green;
            this.Label_SamplePool.Location = new System.Drawing.Point(8, 416);
            this.Label_SamplePool.Name = "Label_SamplePool";
            this.Label_SamplePool.Size = new System.Drawing.Size(66, 13);
            this.Label_SamplePool.TabIndex = 28;
            this.Label_SamplePool.Text = "Sample Pool";
            // 
            // ListView_SamplePool
            // 
            this.ListView_SamplePool.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_SamplePool.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SamplePool_Hashcode,
            this.SamplePool_Volume,
            this.SamplePool_VolOffset,
            this.SamplePool_Pitch,
            this.SamplePool_PitchOffset,
            this.SamplePool_Pan,
            this.SamplePool_PanOffset});
            this.ListView_SamplePool.FullRowSelect = true;
            this.ListView_SamplePool.GridLines = true;
            this.ListView_SamplePool.HideSelection = false;
            this.ListView_SamplePool.Location = new System.Drawing.Point(8, 440);
            this.ListView_SamplePool.Name = "ListView_SamplePool";
            this.ListView_SamplePool.Size = new System.Drawing.Size(537, 168);
            this.ListView_SamplePool.TabIndex = 31;
            this.ListView_SamplePool.UseCompatibleStateImageBehavior = false;
            this.ListView_SamplePool.View = System.Windows.Forms.View.Details;
            this.ListView_SamplePool.SelectedIndexChanged += new System.EventHandler(this.ListView_SamplePool_SelectedIndexChanged);
            // 
            // SamplePool_Hashcode
            // 
            this.SamplePool_Hashcode.Text = "Hashcode";
            this.SamplePool_Hashcode.Width = 80;
            // 
            // SamplePool_Volume
            // 
            this.SamplePool_Volume.Text = "Vol";
            this.SamplePool_Volume.Width = 36;
            // 
            // SamplePool_VolOffset
            // 
            this.SamplePool_VolOffset.Text = "Vol +/-";
            this.SamplePool_VolOffset.Width = 47;
            // 
            // SamplePool_Pitch
            // 
            this.SamplePool_Pitch.Text = "Pitch";
            this.SamplePool_Pitch.Width = 42;
            // 
            // SamplePool_PitchOffset
            // 
            this.SamplePool_PitchOffset.Text = "Pitch +/-";
            // 
            // SamplePool_Pan
            // 
            this.SamplePool_Pan.Text = "Pan";
            this.SamplePool_Pan.Width = 47;
            // 
            // SamplePool_PanOffset
            // 
            this.SamplePool_PanOffset.Text = "Pan +/-";
            // 
            // Textbox_GroupMaxChannels
            // 
            this.Textbox_GroupMaxChannels.Location = new System.Drawing.Point(424, 72);
            this.Textbox_GroupMaxChannels.Name = "Textbox_GroupMaxChannels";
            this.Textbox_GroupMaxChannels.ReadOnly = true;
            this.Textbox_GroupMaxChannels.Size = new System.Drawing.Size(96, 20);
            this.Textbox_GroupMaxChannels.TabIndex = 25;
            this.Textbox_GroupMaxChannels.Text = "0";
            // 
            // Label_GroupMaxChannels
            // 
            this.Label_GroupMaxChannels.AutoSize = true;
            this.Label_GroupMaxChannels.ForeColor = System.Drawing.Color.Blue;
            this.Label_GroupMaxChannels.Location = new System.Drawing.Point(424, 56);
            this.Label_GroupMaxChannels.Name = "Label_GroupMaxChannels";
            this.Label_GroupMaxChannels.Size = new System.Drawing.Size(106, 13);
            this.Label_GroupMaxChannels.TabIndex = 24;
            this.Label_GroupMaxChannels.Text = "Group Max Channels";
            // 
            // Label_DuckerLength
            // 
            this.Label_DuckerLength.AutoSize = true;
            this.Label_DuckerLength.Location = new System.Drawing.Point(216, 104);
            this.Label_DuckerLength.Name = "Label_DuckerLength";
            this.Label_DuckerLength.Size = new System.Drawing.Size(78, 13);
            this.Label_DuckerLength.TabIndex = 16;
            this.Label_DuckerLength.Text = "Ducker Length";
            // 
            // vMasterVolume
            // 
            this.vMasterVolume.BackColor = System.Drawing.SystemColors.Control;
            this.vMasterVolume.Location = new System.Drawing.Point(320, 72);
            this.vMasterVolume.Name = "vMasterVolume";
            this.vMasterVolume.ReadOnly = true;
            this.vMasterVolume.Size = new System.Drawing.Size(96, 20);
            this.vMasterVolume.TabIndex = 21;
            this.vMasterVolume.Text = "0";
            // 
            // Label_MasterVolume
            // 
            this.Label_MasterVolume.AutoSize = true;
            this.Label_MasterVolume.Location = new System.Drawing.Point(320, 56);
            this.Label_MasterVolume.Name = "Label_MasterVolume";
            this.Label_MasterVolume.Size = new System.Drawing.Size(77, 13);
            this.Label_MasterVolume.TabIndex = 20;
            this.Label_MasterVolume.Text = "Master Volume";
            // 
            // vDucker
            // 
            this.vDucker.BackColor = System.Drawing.SystemColors.Control;
            this.vDucker.Location = new System.Drawing.Point(216, 72);
            this.vDucker.Name = "vDucker";
            this.vDucker.ReadOnly = true;
            this.vDucker.Size = new System.Drawing.Size(96, 20);
            this.vDucker.TabIndex = 15;
            this.vDucker.Text = "0";
            // 
            // Label_Ducker
            // 
            this.Label_Ducker.AutoSize = true;
            this.Label_Ducker.Location = new System.Drawing.Point(216, 56);
            this.Label_Ducker.Name = "Label_Ducker";
            this.Label_Ducker.Size = new System.Drawing.Size(42, 13);
            this.Label_Ducker.TabIndex = 14;
            this.Label_Ducker.Text = "Ducker";
            // 
            // vPriority
            // 
            this.vPriority.BackColor = System.Drawing.SystemColors.Control;
            this.vPriority.Location = new System.Drawing.Point(112, 72);
            this.vPriority.Name = "vPriority";
            this.vPriority.ReadOnly = true;
            this.vPriority.Size = new System.Drawing.Size(96, 20);
            this.vPriority.TabIndex = 9;
            this.vPriority.Text = "0";
            // 
            // Label_Priority
            // 
            this.Label_Priority.AutoSize = true;
            this.Label_Priority.Location = new System.Drawing.Point(112, 56);
            this.Label_Priority.Name = "Label_Priority";
            this.Label_Priority.Size = new System.Drawing.Size(38, 13);
            this.Label_Priority.TabIndex = 8;
            this.Label_Priority.Text = "Priority";
            // 
            // vMaxVoices
            // 
            this.vMaxVoices.BackColor = System.Drawing.SystemColors.Control;
            this.vMaxVoices.Location = new System.Drawing.Point(8, 72);
            this.vMaxVoices.Name = "vMaxVoices";
            this.vMaxVoices.ReadOnly = true;
            this.vMaxVoices.Size = new System.Drawing.Size(96, 20);
            this.vMaxVoices.TabIndex = 3;
            this.vMaxVoices.Text = "0";
            // 
            // Label_MaxVoices
            // 
            this.Label_MaxVoices.AutoSize = true;
            this.Label_MaxVoices.Location = new System.Drawing.Point(8, 56);
            this.Label_MaxVoices.Name = "Label_MaxVoices";
            this.Label_MaxVoices.Size = new System.Drawing.Size(62, 13);
            this.Label_MaxVoices.TabIndex = 2;
            this.Label_MaxVoices.Text = "Max Voices";
            // 
            // vReverbSend
            // 
            this.vReverbSend.BackColor = System.Drawing.SystemColors.Control;
            this.vReverbSend.Location = new System.Drawing.Point(320, 24);
            this.vReverbSend.Name = "vReverbSend";
            this.vReverbSend.ReadOnly = true;
            this.vReverbSend.Size = new System.Drawing.Size(96, 20);
            this.vReverbSend.TabIndex = 19;
            this.vReverbSend.Text = "0";
            // 
            // Label_ReverbSend
            // 
            this.Label_ReverbSend.AutoSize = true;
            this.Label_ReverbSend.Location = new System.Drawing.Point(320, 8);
            this.Label_ReverbSend.Name = "Label_ReverbSend";
            this.Label_ReverbSend.Size = new System.Drawing.Size(70, 13);
            this.Label_ReverbSend.TabIndex = 18;
            this.Label_ReverbSend.Text = "Reverb Send";
            // 
            // vMaxDelay
            // 
            this.vMaxDelay.BackColor = System.Drawing.SystemColors.Control;
            this.vMaxDelay.Location = new System.Drawing.Point(216, 24);
            this.vMaxDelay.Name = "vMaxDelay";
            this.vMaxDelay.ReadOnly = true;
            this.vMaxDelay.Size = new System.Drawing.Size(96, 20);
            this.vMaxDelay.TabIndex = 13;
            this.vMaxDelay.Text = "0";
            // 
            // Label_MaxDelay
            // 
            this.Label_MaxDelay.AutoSize = true;
            this.Label_MaxDelay.Location = new System.Drawing.Point(216, 8);
            this.Label_MaxDelay.Name = "Label_MaxDelay";
            this.Label_MaxDelay.Size = new System.Drawing.Size(57, 13);
            this.Label_MaxDelay.TabIndex = 12;
            this.Label_MaxDelay.Text = "Max Delay";
            // 
            // vMinDelay
            // 
            this.vMinDelay.BackColor = System.Drawing.SystemColors.Control;
            this.vMinDelay.Location = new System.Drawing.Point(112, 24);
            this.vMinDelay.Name = "vMinDelay";
            this.vMinDelay.ReadOnly = true;
            this.vMinDelay.Size = new System.Drawing.Size(96, 20);
            this.vMinDelay.TabIndex = 7;
            this.vMinDelay.Text = "0";
            // 
            // Label_MinDelay
            // 
            this.Label_MinDelay.AutoSize = true;
            this.Label_MinDelay.Location = new System.Drawing.Point(112, 8);
            this.Label_MinDelay.Name = "Label_MinDelay";
            this.Label_MinDelay.Size = new System.Drawing.Size(54, 13);
            this.Label_MinDelay.TabIndex = 6;
            this.Label_MinDelay.Text = "Min Delay";
            // 
            // Textbox_TrackingType
            // 
            this.Textbox_TrackingType.BackColor = System.Drawing.SystemColors.Control;
            this.Textbox_TrackingType.Location = new System.Drawing.Point(6, 23);
            this.Textbox_TrackingType.Name = "Textbox_TrackingType";
            this.Textbox_TrackingType.ReadOnly = true;
            this.Textbox_TrackingType.Size = new System.Drawing.Size(96, 20);
            this.Textbox_TrackingType.TabIndex = 1;
            this.Textbox_TrackingType.Text = "0";
            // 
            // Label_TrackingType
            // 
            this.Label_TrackingType.AutoSize = true;
            this.Label_TrackingType.Location = new System.Drawing.Point(8, 8);
            this.Label_TrackingType.Name = "Label_TrackingType";
            this.Label_TrackingType.Size = new System.Drawing.Size(76, 13);
            this.Label_TrackingType.TabIndex = 0;
            this.Label_TrackingType.Text = "Tracking Type";
            // 
            // Button_ReloadFile
            // 
            this.Button_ReloadFile.Enabled = false;
            this.Button_ReloadFile.Location = new System.Drawing.Point(8, 720);
            this.Button_ReloadFile.Name = "Button_ReloadFile";
            this.Button_ReloadFile.Size = new System.Drawing.Size(112, 32);
            this.Button_ReloadFile.TabIndex = 5;
            this.Button_ReloadFile.Text = "Reload";
            this.Button_ReloadFile.UseVisualStyleBackColor = true;
            this.Button_ReloadFile.Click += new System.EventHandler(this.Button_ReloadFile_Click);
            // 
            // Tab_Options
            // 
            this.Tab_Options.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tab_Options.Controls.Add(this.Tab_HexView);
            this.Tab_Options.Controls.Add(this.Tab_Wav_Head_Data);
            this.Tab_Options.Controls.Add(this.Tab_Stream_Data);
            this.Tab_Options.Location = new System.Drawing.Point(3, 8);
            this.Tab_Options.Name = "Tab_Options";
            this.Tab_Options.SelectedIndex = 0;
            this.Tab_Options.Size = new System.Drawing.Size(408, 760);
            this.Tab_Options.TabIndex = 31;
            // 
            // Tab_HexView
            // 
            this.Tab_HexView.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_HexView.Controls.Add(this.Label_HexViewer_Key);
            this.Tab_HexView.Controls.Add(this.Label_HexViewer_SamplePool);
            this.Tab_HexView.Controls.Add(this.Button_HexViewer_SamplePool);
            this.Tab_HexView.Controls.Add(this.Label_HexViewer_UserFlags);
            this.Tab_HexView.Controls.Add(this.Button_HexViewer_UserFlags);
            this.Tab_HexView.Controls.Add(this.Label_HexViewer_Flags);
            this.Tab_HexView.Controls.Add(this.Button_HexViewer_Flags);
            this.Tab_HexView.Controls.Add(this.Label_HexViewer_HeaderData);
            this.Tab_HexView.Controls.Add(this.Button_HexViewer_HeaderData);
            this.Tab_HexView.Controls.Add(this.ListView_HexEditor);
            this.Tab_HexView.Location = new System.Drawing.Point(4, 22);
            this.Tab_HexView.Name = "Tab_HexView";
            this.Tab_HexView.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_HexView.Size = new System.Drawing.Size(400, 734);
            this.Tab_HexView.TabIndex = 0;
            this.Tab_HexView.Text = "Hex View";
            // 
            // Label_HexViewer_Key
            // 
            this.Label_HexViewer_Key.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_HexViewer_Key.AutoSize = true;
            this.Label_HexViewer_Key.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_HexViewer_Key.Location = new System.Drawing.Point(8, 496);
            this.Label_HexViewer_Key.Name = "Label_HexViewer_Key";
            this.Label_HexViewer_Key.Size = new System.Drawing.Size(32, 13);
            this.Label_HexViewer_Key.TabIndex = 1;
            this.Label_HexViewer_Key.Text = "Key:";
            // 
            // Label_HexViewer_SamplePool
            // 
            this.Label_HexViewer_SamplePool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_HexViewer_SamplePool.AutoSize = true;
            this.Label_HexViewer_SamplePool.Location = new System.Drawing.Point(320, 496);
            this.Label_HexViewer_SamplePool.Name = "Label_HexViewer_SamplePool";
            this.Label_HexViewer_SamplePool.Size = new System.Drawing.Size(66, 13);
            this.Label_HexViewer_SamplePool.TabIndex = 9;
            this.Label_HexViewer_SamplePool.Text = "Sample Pool";
            // 
            // Button_HexViewer_SamplePool
            // 
            this.Button_HexViewer_SamplePool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_HexViewer_SamplePool.BackColor = System.Drawing.Color.Green;
            this.Button_HexViewer_SamplePool.Enabled = false;
            this.Button_HexViewer_SamplePool.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Button_HexViewer_SamplePool.Location = new System.Drawing.Point(304, 496);
            this.Button_HexViewer_SamplePool.Name = "Button_HexViewer_SamplePool";
            this.Button_HexViewer_SamplePool.Size = new System.Drawing.Size(16, 16);
            this.Button_HexViewer_SamplePool.TabIndex = 8;
            this.Button_HexViewer_SamplePool.UseVisualStyleBackColor = false;
            // 
            // Label_HexViewer_UserFlags
            // 
            this.Label_HexViewer_UserFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_HexViewer_UserFlags.AutoSize = true;
            this.Label_HexViewer_UserFlags.Location = new System.Drawing.Point(232, 496);
            this.Label_HexViewer_UserFlags.Name = "Label_HexViewer_UserFlags";
            this.Label_HexViewer_UserFlags.Size = new System.Drawing.Size(57, 13);
            this.Label_HexViewer_UserFlags.TabIndex = 7;
            this.Label_HexViewer_UserFlags.Text = "User Flags";
            // 
            // Button_HexViewer_UserFlags
            // 
            this.Button_HexViewer_UserFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_HexViewer_UserFlags.BackColor = System.Drawing.Color.Purple;
            this.Button_HexViewer_UserFlags.Enabled = false;
            this.Button_HexViewer_UserFlags.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Button_HexViewer_UserFlags.Location = new System.Drawing.Point(216, 496);
            this.Button_HexViewer_UserFlags.Name = "Button_HexViewer_UserFlags";
            this.Button_HexViewer_UserFlags.Size = new System.Drawing.Size(16, 16);
            this.Button_HexViewer_UserFlags.TabIndex = 6;
            this.Button_HexViewer_UserFlags.UseVisualStyleBackColor = false;
            // 
            // Label_HexViewer_Flags
            // 
            this.Label_HexViewer_Flags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_HexViewer_Flags.AutoSize = true;
            this.Label_HexViewer_Flags.Location = new System.Drawing.Point(168, 496);
            this.Label_HexViewer_Flags.Name = "Label_HexViewer_Flags";
            this.Label_HexViewer_Flags.Size = new System.Drawing.Size(32, 13);
            this.Label_HexViewer_Flags.TabIndex = 5;
            this.Label_HexViewer_Flags.Text = "Flags";
            // 
            // Button_HexViewer_Flags
            // 
            this.Button_HexViewer_Flags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_HexViewer_Flags.BackColor = System.Drawing.Color.Blue;
            this.Button_HexViewer_Flags.Enabled = false;
            this.Button_HexViewer_Flags.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Button_HexViewer_Flags.Location = new System.Drawing.Point(152, 496);
            this.Button_HexViewer_Flags.Name = "Button_HexViewer_Flags";
            this.Button_HexViewer_Flags.Size = new System.Drawing.Size(16, 16);
            this.Button_HexViewer_Flags.TabIndex = 4;
            this.Button_HexViewer_Flags.UseVisualStyleBackColor = false;
            // 
            // Label_HexViewer_HeaderData
            // 
            this.Label_HexViewer_HeaderData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_HexViewer_HeaderData.AutoSize = true;
            this.Label_HexViewer_HeaderData.Location = new System.Drawing.Point(72, 496);
            this.Label_HexViewer_HeaderData.Name = "Label_HexViewer_HeaderData";
            this.Label_HexViewer_HeaderData.Size = new System.Drawing.Size(68, 13);
            this.Label_HexViewer_HeaderData.TabIndex = 3;
            this.Label_HexViewer_HeaderData.Text = "Header Data";
            // 
            // Button_HexViewer_HeaderData
            // 
            this.Button_HexViewer_HeaderData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_HexViewer_HeaderData.BackColor = System.Drawing.Color.Black;
            this.Button_HexViewer_HeaderData.Enabled = false;
            this.Button_HexViewer_HeaderData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Button_HexViewer_HeaderData.Location = new System.Drawing.Point(56, 496);
            this.Button_HexViewer_HeaderData.Name = "Button_HexViewer_HeaderData";
            this.Button_HexViewer_HeaderData.Size = new System.Drawing.Size(16, 16);
            this.Button_HexViewer_HeaderData.TabIndex = 2;
            this.Button_HexViewer_HeaderData.UseVisualStyleBackColor = false;
            // 
            // ListView_HexEditor
            // 
            this.ListView_HexEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_HexEditor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HexViewer_Offset,
            this.HexViewer_Byte1,
            this.HexViewer_Byte2,
            this.HexViewer_Byte3,
            this.HexViewer_Byte4,
            this.HexViewer_Byte5,
            this.HexViewer_Byte6,
            this.HexViewer_Byte7,
            this.HexViewer_Byte8,
            this.HexViewer_ASCII});
            this.ListView_HexEditor.ContextMenu = this.ContextMenu_HexView_ChangeFont;
            this.ListView_HexEditor.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListView_HexEditor.FullRowSelect = true;
            this.ListView_HexEditor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView_HexEditor.HideSelection = false;
            this.ListView_HexEditor.Location = new System.Drawing.Point(16, 8);
            this.ListView_HexEditor.MultiSelect = false;
            this.ListView_HexEditor.Name = "ListView_HexEditor";
            this.ListView_HexEditor.Size = new System.Drawing.Size(368, 464);
            this.ListView_HexEditor.TabIndex = 0;
            this.ListView_HexEditor.UseCompatibleStateImageBehavior = false;
            this.ListView_HexEditor.View = System.Windows.Forms.View.Details;
            // 
            // HexViewer_Offset
            // 
            this.HexViewer_Offset.Text = "Offset";
            this.HexViewer_Offset.Width = 66;
            // 
            // HexViewer_Byte1
            // 
            this.HexViewer_Byte1.Text = "";
            this.HexViewer_Byte1.Width = 28;
            // 
            // HexViewer_Byte2
            // 
            this.HexViewer_Byte2.Text = "";
            this.HexViewer_Byte2.Width = 28;
            // 
            // HexViewer_Byte3
            // 
            this.HexViewer_Byte3.Text = "";
            this.HexViewer_Byte3.Width = 28;
            // 
            // HexViewer_Byte4
            // 
            this.HexViewer_Byte4.Text = "";
            this.HexViewer_Byte4.Width = 28;
            // 
            // HexViewer_Byte5
            // 
            this.HexViewer_Byte5.Text = "";
            this.HexViewer_Byte5.Width = 28;
            // 
            // HexViewer_Byte6
            // 
            this.HexViewer_Byte6.Text = "";
            this.HexViewer_Byte6.Width = 28;
            // 
            // HexViewer_Byte7
            // 
            this.HexViewer_Byte7.Text = "";
            this.HexViewer_Byte7.Width = 28;
            // 
            // HexViewer_Byte8
            // 
            this.HexViewer_Byte8.Text = "";
            this.HexViewer_Byte8.Width = 28;
            // 
            // HexViewer_ASCII
            // 
            this.HexViewer_ASCII.Text = "ASCII";
            this.HexViewer_ASCII.Width = 77;
            // 
            // ContextMenu_HexView_ChangeFont
            // 
            this.ContextMenu_HexView_ChangeFont.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_HexContextMenu_ChangeFont});
            // 
            // MenuItem_HexContextMenu_ChangeFont
            // 
            this.MenuItem_HexContextMenu_ChangeFont.Index = 0;
            this.MenuItem_HexContextMenu_ChangeFont.Text = "Change Font";
            this.MenuItem_HexContextMenu_ChangeFont.Click += new System.EventHandler(this.MenuItem_HexContextMenu_ChangeFont_Click);
            // 
            // Tab_Wav_Head_Data
            // 
            this.Tab_Wav_Head_Data.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_Wav_Head_Data.Controls.Add(this.ListView_WavData);
            this.Tab_Wav_Head_Data.Location = new System.Drawing.Point(4, 22);
            this.Tab_Wav_Head_Data.Name = "Tab_Wav_Head_Data";
            this.Tab_Wav_Head_Data.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Wav_Head_Data.Size = new System.Drawing.Size(400, 734);
            this.Tab_Wav_Head_Data.TabIndex = 1;
            this.Tab_Wav_Head_Data.Text = "Wav Header Data";
            // 
            // ListView_WavData
            // 
            this.ListView_WavData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_WavData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WavHeaderData_Number,
            this.WavHeaderData_Flags,
            this.WavHeaderData_Address,
            this.WavHeaderData_MemorySize,
            this.WavHeaderData_SampleSize,
            this.WavHeaderData_Frequency,
            this.WavHeaderData_LoopStartOffset,
            this.WavHeaderData_Duration});
            this.ListView_WavData.ContextMenu = this.ContextMenu_ListView_WavData;
            this.ListView_WavData.FullRowSelect = true;
            this.ListView_WavData.GridLines = true;
            this.ListView_WavData.HideSelection = false;
            this.ListView_WavData.Location = new System.Drawing.Point(8, 8);
            this.ListView_WavData.Name = "ListView_WavData";
            this.ListView_WavData.Size = new System.Drawing.Size(384, 640);
            this.ListView_WavData.TabIndex = 0;
            this.ListView_WavData.UseCompatibleStateImageBehavior = false;
            this.ListView_WavData.View = System.Windows.Forms.View.Details;
            this.ListView_WavData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_WavData_MouseDoubleClick);
            // 
            // WavHeaderData_Number
            // 
            this.WavHeaderData_Number.Text = "No.";
            this.WavHeaderData_Number.Width = 34;
            // 
            // WavHeaderData_Flags
            // 
            this.WavHeaderData_Flags.Text = "Flags";
            this.WavHeaderData_Flags.Width = 40;
            // 
            // WavHeaderData_Address
            // 
            this.WavHeaderData_Address.Text = "Address";
            // 
            // WavHeaderData_MemorySize
            // 
            this.WavHeaderData_MemorySize.Text = "Memory Size";
            this.WavHeaderData_MemorySize.Width = 75;
            // 
            // WavHeaderData_SampleSize
            // 
            this.WavHeaderData_SampleSize.Text = "Sample Size";
            this.WavHeaderData_SampleSize.Width = 73;
            // 
            // WavHeaderData_Frequency
            // 
            this.WavHeaderData_Frequency.Text = "Freq.";
            this.WavHeaderData_Frequency.Width = 42;
            // 
            // WavHeaderData_LoopStartOffset
            // 
            this.WavHeaderData_LoopStartOffset.Text = "LoopStartOffset";
            this.WavHeaderData_LoopStartOffset.Width = 89;
            // 
            // WavHeaderData_Duration
            // 
            this.WavHeaderData_Duration.Text = "Duration (ms)";
            this.WavHeaderData_Duration.Width = 80;
            // 
            // ContextMenu_ListView_WavData
            // 
            this.ContextMenu_ListView_WavData.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ListViewWavData_Options});
            // 
            // ListViewWavData_Options
            // 
            this.ListViewWavData_Options.Index = 0;
            this.ListViewWavData_Options.Text = "Options";
            this.ListViewWavData_Options.Click += new System.EventHandler(this.ListViewWavData_Options_Click);
            // 
            // Tab_Stream_Data
            // 
            this.Tab_Stream_Data.BackColor = System.Drawing.SystemColors.Control;
            this.Tab_Stream_Data.Controls.Add(this.Button_ValidateADPCM);
            this.Tab_Stream_Data.Controls.Add(this.Textbox_StreamFilePath);
            this.Tab_Stream_Data.Controls.Add(this.ListView_StreamData);
            this.Tab_Stream_Data.Controls.Add(this.tabControl2);
            this.Tab_Stream_Data.Controls.Add(this.Button_LoadStreamData);
            this.Tab_Stream_Data.Location = new System.Drawing.Point(4, 22);
            this.Tab_Stream_Data.Name = "Tab_Stream_Data";
            this.Tab_Stream_Data.Size = new System.Drawing.Size(400, 734);
            this.Tab_Stream_Data.TabIndex = 2;
            this.Tab_Stream_Data.Text = "Stream Data";
            // 
            // Button_ValidateADPCM
            // 
            this.Button_ValidateADPCM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_ValidateADPCM.Enabled = false;
            this.Button_ValidateADPCM.Location = new System.Drawing.Point(136, 672);
            this.Button_ValidateADPCM.Name = "Button_ValidateADPCM";
            this.Button_ValidateADPCM.Size = new System.Drawing.Size(144, 23);
            this.Button_ValidateADPCM.TabIndex = 4;
            this.Button_ValidateADPCM.Text = "Validate All Streams";
            this.Button_ValidateADPCM.Click += new System.EventHandler(this.Button_ValidateADPCM_Click);
            // 
            // Textbox_StreamFilePath
            // 
            this.Textbox_StreamFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_StreamFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_StreamFilePath.Location = new System.Drawing.Point(8, 704);
            this.Textbox_StreamFilePath.Name = "Textbox_StreamFilePath";
            this.Textbox_StreamFilePath.ReadOnly = true;
            this.Textbox_StreamFilePath.Size = new System.Drawing.Size(384, 20);
            this.Textbox_StreamFilePath.TabIndex = 6;
            // 
            // ListView_StreamData
            // 
            this.ListView_StreamData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_StreamData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StreamData_Index,
            this.StreamData_AdpcmStatus,
            this.StreamData_MarkerOffset,
            this.StreamData_MarkerSize,
            this.StreamData_AudioOffset,
            this.StreamData_AudioLength,
            this.StreamData_BaseVolume});
            this.ListView_StreamData.ContextMenu = this.ContextMenu_ExporStreamtMarkers;
            this.ListView_StreamData.FullRowSelect = true;
            this.ListView_StreamData.GridLines = true;
            this.ListView_StreamData.HideSelection = false;
            this.ListView_StreamData.Location = new System.Drawing.Point(8, 8);
            this.ListView_StreamData.MultiSelect = false;
            this.ListView_StreamData.Name = "ListView_StreamData";
            this.ListView_StreamData.Size = new System.Drawing.Size(384, 344);
            this.ListView_StreamData.TabIndex = 0;
            this.ListView_StreamData.UseCompatibleStateImageBehavior = false;
            this.ListView_StreamData.View = System.Windows.Forms.View.Details;
            this.ListView_StreamData.SelectedIndexChanged += new System.EventHandler(this.ListView_StreamData_SelectedIndexChanged);
            this.ListView_StreamData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_StreamData_MouseDoubleClick);
            // 
            // StreamData_Index
            // 
            this.StreamData_Index.Text = "No";
            this.StreamData_Index.Width = 42;
            // 
            // StreamData_AdpcmStatus
            // 
            this.StreamData_AdpcmStatus.Text = "ADPCM";
            // 
            // StreamData_MarkerOffset
            // 
            this.StreamData_MarkerOffset.Text = "Marker Offset";
            this.StreamData_MarkerOffset.Width = 81;
            // 
            // StreamData_MarkerSize
            // 
            this.StreamData_MarkerSize.Text = "Marker Size";
            this.StreamData_MarkerSize.Width = 71;
            // 
            // StreamData_AudioOffset
            // 
            this.StreamData_AudioOffset.Text = "Audio Offset";
            this.StreamData_AudioOffset.Width = 80;
            // 
            // StreamData_AudioLength
            // 
            this.StreamData_AudioLength.Text = "Audio Length";
            this.StreamData_AudioLength.Width = 95;
            // 
            // StreamData_BaseVolume
            // 
            this.StreamData_BaseVolume.Text = "Base Volume";
            this.StreamData_BaseVolume.Width = 82;
            // 
            // ContextMenu_ExporStreamtMarkers
            // 
            this.ContextMenu_ExporStreamtMarkers.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_ExportStreamMarkers,
            this.ListViewStreamData_Options});
            // 
            // MenuItem_ExportStreamMarkers
            // 
            this.MenuItem_ExportStreamMarkers.Index = 0;
            this.MenuItem_ExportStreamMarkers.Text = "Export Stream Markers";
            this.MenuItem_ExportStreamMarkers.Click += new System.EventHandler(this.MenuItem_ExportStreamMarkers_Click);
            // 
            // ListViewStreamData_Options
            // 
            this.ListViewStreamData_Options.Index = 1;
            this.ListViewStreamData_Options.Text = "Options";
            this.ListViewStreamData_Options.Click += new System.EventHandler(this.ListViewStreamData_Options_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(8, 360);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(384, 304);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ListView_StreamData_StartMarkers);
            this.tabPage4.Controls.Add(this.Label_StreamData_StartMarkerCount);
            this.tabPage4.Controls.Add(this.textStartMarkerCount);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(376, 278);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Start Markers";
            // 
            // ListView_StreamData_StartMarkers
            // 
            this.ListView_StreamData_StartMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_StreamData_StartMarkers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StreamData_StartMarkers_No,
            this.StreamData_StartMarkers_Index,
            this.StreamData_StartMarkers_Position,
            this.StreamData_StartMarkers_Type,
            this.StreamData_StartMarkers_LoopStart,
            this.StreamData_StartMarkers_LoopMarkerIndex,
            this.StreamData_StartMarkers_MarkerPosition});
            this.ListView_StreamData_StartMarkers.FullRowSelect = true;
            this.ListView_StreamData_StartMarkers.GridLines = true;
            this.ListView_StreamData_StartMarkers.HideSelection = false;
            this.ListView_StreamData_StartMarkers.Location = new System.Drawing.Point(8, 32);
            this.ListView_StreamData_StartMarkers.Name = "ListView_StreamData_StartMarkers";
            this.ListView_StreamData_StartMarkers.Size = new System.Drawing.Size(360, 240);
            this.ListView_StreamData_StartMarkers.TabIndex = 2;
            this.ListView_StreamData_StartMarkers.UseCompatibleStateImageBehavior = false;
            this.ListView_StreamData_StartMarkers.View = System.Windows.Forms.View.Details;
            // 
            // StreamData_StartMarkers_No
            // 
            this.StreamData_StartMarkers_No.Text = "No.";
            this.StreamData_StartMarkers_No.Width = 30;
            // 
            // StreamData_StartMarkers_Index
            // 
            this.StreamData_StartMarkers_Index.Text = "Index";
            // 
            // StreamData_StartMarkers_Position
            // 
            this.StreamData_StartMarkers_Position.Text = "Position";
            // 
            // StreamData_StartMarkers_Type
            // 
            this.StreamData_StartMarkers_Type.Text = "Type";
            // 
            // StreamData_StartMarkers_LoopStart
            // 
            this.StreamData_StartMarkers_LoopStart.Text = "Loop Start";
            this.StreamData_StartMarkers_LoopStart.Width = 80;
            // 
            // StreamData_StartMarkers_LoopMarkerIndex
            // 
            this.StreamData_StartMarkers_LoopMarkerIndex.Text = "Loop Marker Index";
            this.StreamData_StartMarkers_LoopMarkerIndex.Width = 120;
            // 
            // StreamData_StartMarkers_MarkerPosition
            // 
            this.StreamData_StartMarkers_MarkerPosition.Text = "Marker Position";
            this.StreamData_StartMarkers_MarkerPosition.Width = 98;
            // 
            // Label_StreamData_StartMarkerCount
            // 
            this.Label_StreamData_StartMarkerCount.Location = new System.Drawing.Point(8, 12);
            this.Label_StreamData_StartMarkerCount.Name = "Label_StreamData_StartMarkerCount";
            this.Label_StreamData_StartMarkerCount.Size = new System.Drawing.Size(80, 12);
            this.Label_StreamData_StartMarkerCount.TabIndex = 0;
            this.Label_StreamData_StartMarkerCount.Text = "Marker Count";
            // 
            // textStartMarkerCount
            // 
            this.textStartMarkerCount.BackColor = System.Drawing.SystemColors.Window;
            this.textStartMarkerCount.Location = new System.Drawing.Point(88, 8);
            this.textStartMarkerCount.Name = "textStartMarkerCount";
            this.textStartMarkerCount.ReadOnly = true;
            this.textStartMarkerCount.Size = new System.Drawing.Size(100, 20);
            this.textStartMarkerCount.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.ListView_StreamData_Markers);
            this.tabPage5.Controls.Add(this.Label_StreamData_MarkerCount);
            this.tabPage5.Controls.Add(this.textMarkerCount);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(376, 278);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Markers";
            this.tabPage5.Visible = false;
            // 
            // ListView_StreamData_Markers
            // 
            this.ListView_StreamData_Markers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_StreamData_Markers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StreamData_Markers_No,
            this.StreamData_Markers_StartMarkerIndex,
            this.StreamData_Markers_Position,
            this.StreamData_Markers_Type,
            this.StreamData_Markers_LoopStart,
            this.StreamData_Markers_LoopMarkerIndex});
            this.ListView_StreamData_Markers.FullRowSelect = true;
            this.ListView_StreamData_Markers.GridLines = true;
            this.ListView_StreamData_Markers.HideSelection = false;
            this.ListView_StreamData_Markers.Location = new System.Drawing.Point(8, 32);
            this.ListView_StreamData_Markers.Name = "ListView_StreamData_Markers";
            this.ListView_StreamData_Markers.Size = new System.Drawing.Size(360, 240);
            this.ListView_StreamData_Markers.TabIndex = 2;
            this.ListView_StreamData_Markers.UseCompatibleStateImageBehavior = false;
            this.ListView_StreamData_Markers.View = System.Windows.Forms.View.Details;
            // 
            // StreamData_Markers_No
            // 
            this.StreamData_Markers_No.Text = "No.";
            this.StreamData_Markers_No.Width = 30;
            // 
            // StreamData_Markers_StartMarkerIndex
            // 
            this.StreamData_Markers_StartMarkerIndex.Text = "Start Marker Index";
            this.StreamData_Markers_StartMarkerIndex.Width = 103;
            // 
            // StreamData_Markers_Position
            // 
            this.StreamData_Markers_Position.Text = "Position";
            // 
            // StreamData_Markers_Type
            // 
            this.StreamData_Markers_Type.Text = "Type";
            // 
            // StreamData_Markers_LoopStart
            // 
            this.StreamData_Markers_LoopStart.Text = "Loop Start";
            this.StreamData_Markers_LoopStart.Width = 80;
            // 
            // StreamData_Markers_LoopMarkerIndex
            // 
            this.StreamData_Markers_LoopMarkerIndex.Text = "Loop Marker Index";
            this.StreamData_Markers_LoopMarkerIndex.Width = 120;
            // 
            // Label_StreamData_MarkerCount
            // 
            this.Label_StreamData_MarkerCount.Location = new System.Drawing.Point(8, 12);
            this.Label_StreamData_MarkerCount.Name = "Label_StreamData_MarkerCount";
            this.Label_StreamData_MarkerCount.Size = new System.Drawing.Size(80, 12);
            this.Label_StreamData_MarkerCount.TabIndex = 0;
            this.Label_StreamData_MarkerCount.Text = "Marker Count";
            // 
            // textMarkerCount
            // 
            this.textMarkerCount.BackColor = System.Drawing.SystemColors.Window;
            this.textMarkerCount.Location = new System.Drawing.Point(88, 8);
            this.textMarkerCount.Name = "textMarkerCount";
            this.textMarkerCount.ReadOnly = true;
            this.textMarkerCount.Size = new System.Drawing.Size(100, 20);
            this.textMarkerCount.TabIndex = 1;
            // 
            // Button_LoadStreamData
            // 
            this.Button_LoadStreamData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_LoadStreamData.Location = new System.Drawing.Point(8, 672);
            this.Button_LoadStreamData.Name = "Button_LoadStreamData";
            this.Button_LoadStreamData.Size = new System.Drawing.Size(75, 23);
            this.Button_LoadStreamData.TabIndex = 2;
            this.Button_LoadStreamData.Text = "Load...";
            this.Button_LoadStreamData.Click += new System.EventHandler(this.Button_LoadStreamData_Click);
            // 
            // MainStatusBar
            // 
            this.MainStatusBar.Location = new System.Drawing.Point(0, 867);
            this.MainStatusBar.Name = "MainStatusBar";
            this.MainStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.StatusLabel_HashCode,
            this.StatusLabel_Version,
            this.StatusLabel_Platform,
            this.StatusLabel_SoundhDir});
            this.MainStatusBar.ShowPanels = true;
            this.MainStatusBar.Size = new System.Drawing.Size(972, 22);
            this.MainStatusBar.TabIndex = 2;
            this.MainStatusBar.Text = "Main Status Bar";
            // 
            // StatusLabel_HashCode
            // 
            this.StatusLabel_HashCode.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
            this.StatusLabel_HashCode.Name = "StatusLabel_HashCode";
            this.StatusLabel_HashCode.Text = "HashCode:";
            this.StatusLabel_HashCode.Width = 200;
            // 
            // StatusLabel_Version
            // 
            this.StatusLabel_Version.Name = "StatusLabel_Version";
            this.StatusLabel_Version.Text = "Version: 10";
            this.StatusLabel_Version.Width = 99;
            // 
            // StatusLabel_Platform
            // 
            this.StatusLabel_Platform.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
            this.StatusLabel_Platform.Name = "StatusLabel_Platform";
            this.StatusLabel_Platform.Text = "Platform:";
            this.StatusLabel_Platform.Width = 99;
            // 
            // StatusLabel_SoundhDir
            // 
            this.StatusLabel_SoundhDir.Name = "StatusLabel_SoundhDir";
            this.StatusLabel_SoundhDir.Text = "Sound.h Dir:";
            this.StatusLabel_SoundhDir.Width = 250;
            // 
            // MenuItem_File
            // 
            this.MenuItem_File.Index = 0;
            this.MenuItem_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemFile_OpenSoundbank,
            this.MenuItemFile_ViewMusic,
            this.MenuItemFile_Separator1,
            this.MenuItemFile_SoundhDir,
            this.MenuItemFile_Separator2,
            this.MenuItemFile_RecentFiles,
            this.MenuItemFile_Separator3,
            this.MenuItemFile_Exit});
            this.MenuItem_File.Text = "File";
            // 
            // MenuItemFile_OpenSoundbank
            // 
            this.MenuItemFile_OpenSoundbank.Index = 0;
            this.MenuItemFile_OpenSoundbank.Text = "Load Soundbank *.sfx";
            this.MenuItemFile_OpenSoundbank.Click += new System.EventHandler(this.MenuItemFile_OpenSoundbank_Click);
            // 
            // MenuItemFile_ViewMusic
            // 
            this.MenuItemFile_ViewMusic.Index = 1;
            this.MenuItemFile_ViewMusic.Text = "View Music File *.sfx";
            this.MenuItemFile_ViewMusic.Click += new System.EventHandler(this.MenuItemFile_ViewMusic_Click);
            // 
            // MenuItemFile_Separator1
            // 
            this.MenuItemFile_Separator1.Index = 2;
            this.MenuItemFile_Separator1.Text = "-";
            // 
            // MenuItemFile_SoundhDir
            // 
            this.MenuItemFile_SoundhDir.Index = 3;
            this.MenuItemFile_SoundhDir.Text = "Set Sound.h Directory";
            this.MenuItemFile_SoundhDir.Click += new System.EventHandler(this.MenuItemFile_SoundhDir_Click);
            // 
            // MenuItemFile_Separator2
            // 
            this.MenuItemFile_Separator2.Index = 4;
            this.MenuItemFile_Separator2.Text = "-";
            // 
            // MenuItemFile_RecentFiles
            // 
            this.MenuItemFile_RecentFiles.Index = 5;
            this.MenuItemFile_RecentFiles.Text = "Recent Files";
            // 
            // MenuItemFile_Separator3
            // 
            this.MenuItemFile_Separator3.Index = 6;
            this.MenuItemFile_Separator3.Text = "-";
            // 
            // MenuItemFile_Exit
            // 
            this.MenuItemFile_Exit.Index = 7;
            this.MenuItemFile_Exit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.MenuItemFile_Exit.Text = "Exit";
            this.MenuItemFile_Exit.Click += new System.EventHandler(this.MenuItemFile_Exit_Click);
            // 
            // MenuItem_View
            // 
            this.MenuItem_View.Index = 1;
            this.MenuItem_View.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemView_FindHashCode,
            this.MenuItemView_FindNextHashCode});
            this.MenuItem_View.Text = "View";
            // 
            // MenuItemView_FindHashCode
            // 
            this.MenuItemView_FindHashCode.Enabled = false;
            this.MenuItemView_FindHashCode.Index = 0;
            this.MenuItemView_FindHashCode.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.MenuItemView_FindHashCode.Text = "Find Hashcode";
            this.MenuItemView_FindHashCode.Click += new System.EventHandler(this.MenuItemView_FindHashCode_Click);
            // 
            // MenuItemView_FindNextHashCode
            // 
            this.MenuItemView_FindNextHashCode.Enabled = false;
            this.MenuItemView_FindNextHashCode.Index = 1;
            this.MenuItemView_FindNextHashCode.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.MenuItemView_FindNextHashCode.Text = "Find Next";
            this.MenuItemView_FindNextHashCode.Click += new System.EventHandler(this.MenuItemView_FindNextHashCode_Click);
            // 
            // MenuItem_Help
            // 
            this.MenuItem_Help.Index = 2;
            this.MenuItem_Help.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemHelp_About});
            this.MenuItem_Help.Text = "Help";
            // 
            // MenuItemHelp_About
            // 
            this.MenuItemHelp_About.Index = 0;
            this.MenuItemHelp_About.Text = "About...";
            this.MenuItemHelp_About.Click += new System.EventHandler(this.MenuItemHelp_About_Click);
            // 
            // Form_MainMenu
            // 
            this.Form_MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_File,
            this.MenuItem_View,
            this.MenuItem_Help});
            // 
            // openFileDialog_Soundbanks
            // 
            this.openFileDialog_Soundbanks.DefaultExt = "sfx";
            this.openFileDialog_Soundbanks.Filter = "SFX Files|HC0?00??.sfx";
            this.openFileDialog_Soundbanks.Title = "Open Soundbank file";
            // 
            // HexViewfontDialog
            // 
            this.HexViewfontDialog.FixedPitchOnly = true;
            this.HexViewfontDialog.FontMustExist = true;
            this.HexViewfontDialog.MaxSize = 10;
            this.HexViewfontDialog.ShowEffects = false;
            // 
            // openFileDialog_MusicFile
            // 
            this.openFileDialog_MusicFile.DefaultExt = "sfx";
            this.openFileDialog_MusicFile.Filter = "SFX Files|HCE??0??.sfx";
            this.openFileDialog_MusicFile.Title = "Open Music file";
            // 
            // openFileDialog_StreamFile
            // 
            this.openFileDialog_StreamFile.DefaultExt = "sfx";
            this.openFileDialog_StreamFile.Filter = "SFX Files|HC0?FFFF.sfx";
            this.openFileDialog_StreamFile.Title = "Open Stream file";
            // 
            // Frm_MainFrame
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1192, 889);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.Form_MainMenu;
            this.Name = "Frm_MainFrame";
            this.Text = "Eurosound Soundbank Explorer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_MainFrame_FormClosed);
            this.Load += new System.EventHandler(this.Frm_MainFrame_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Frm_MainFrame_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Frm_MainFrame_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.Panel_DetailsAndStreamData.ResumeLayout(false);
            this.splitContainer_details.Panel1.ResumeLayout(false);
            this.splitContainer_details.Panel1.PerformLayout();
            this.splitContainer_details.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_details)).EndInit();
            this.splitContainer_details.ResumeLayout(false);
            this.Panel_ObjectInfo.ResumeLayout(false);
            this.Panel_ObjectInfo.PerformLayout();
            this.Tab_Options.ResumeLayout(false);
            this.Tab_HexView.ResumeLayout(false);
            this.Tab_HexView.PerformLayout();
            this.Tab_Wav_Head_Data.ResumeLayout(false);
            this.Tab_Stream_Data.ResumeLayout(false);
            this.Tab_Stream_Data.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel_HashCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel_Version)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel_Platform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel_SoundhDir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected internal System.Windows.Forms.TextBox Textbox_GroupHashcode;
        private System.Windows.Forms.Label Label_GroupHashCode;
        protected internal System.Windows.Forms.TextBox vOuterRadiusReal;
        private System.Windows.Forms.Label Label_OuterRadiusReal;
        protected internal System.Windows.Forms.TextBox vInnerRadiusReal;
        private System.Windows.Forms.Label Label_InnerRadiusReal;
        protected internal System.Windows.Forms.TextBox vDuckerLength;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        protected internal System.Windows.Forms.ListView ListView_HashCodes;
        private System.Windows.Forms.ColumnHeader HashcodesList_Status;
        private System.Windows.Forms.ColumnHeader HashcodesList_HashCode;
        private System.Windows.Forms.ColumnHeader HashcodesList_Name;
        private System.Windows.Forms.StatusBar MainStatusBar;
        protected internal System.Windows.Forms.StatusBarPanel StatusLabel_HashCode;
        protected internal System.Windows.Forms.StatusBarPanel StatusLabel_Version;
        protected internal System.Windows.Forms.StatusBarPanel StatusLabel_Platform;
        protected internal System.Windows.Forms.StatusBarPanel StatusLabel_SoundhDir;
        private System.Windows.Forms.SplitContainer splitContainer_details;
        private System.Windows.Forms.Panel Panel_ObjectInfo;
        protected internal System.Windows.Forms.TextBox Textbox_GroupMaxChannels;
        private System.Windows.Forms.Label Label_GroupMaxChannels;
        private System.Windows.Forms.Label Label_DuckerLength;
        protected internal System.Windows.Forms.TextBox vMasterVolume;
        private System.Windows.Forms.Label Label_MasterVolume;
        protected internal System.Windows.Forms.TextBox vDucker;
        private System.Windows.Forms.Label Label_Ducker;
        protected internal System.Windows.Forms.TextBox vPriority;
        private System.Windows.Forms.Label Label_Priority;
        protected internal System.Windows.Forms.TextBox vMaxVoices;
        private System.Windows.Forms.Label Label_MaxVoices;
        protected internal System.Windows.Forms.TextBox vReverbSend;
        private System.Windows.Forms.Label Label_ReverbSend;
        protected internal System.Windows.Forms.TextBox vMaxDelay;
        private System.Windows.Forms.Label Label_MaxDelay;
        protected internal System.Windows.Forms.TextBox vMinDelay;
        private System.Windows.Forms.Label Label_MinDelay;
        protected internal System.Windows.Forms.TextBox Textbox_TrackingType;
        private System.Windows.Forms.Label Label_TrackingType;
        private System.Windows.Forms.Button Button_ReloadFile;
        private System.Windows.Forms.TabControl Tab_Options;
        private System.Windows.Forms.TabPage Tab_HexView;
        protected internal System.Windows.Forms.ListView ListView_HexEditor;
        private System.Windows.Forms.ColumnHeader HexViewer_Offset;
        private System.Windows.Forms.ColumnHeader HexViewer_Byte1;
        private System.Windows.Forms.ColumnHeader HexViewer_Byte2;
        private System.Windows.Forms.ColumnHeader HexViewer_Byte3;
        private System.Windows.Forms.ColumnHeader HexViewer_Byte4;
        private System.Windows.Forms.ColumnHeader HexViewer_Byte5;
        private System.Windows.Forms.ColumnHeader HexViewer_Byte6;
        private System.Windows.Forms.ColumnHeader HexViewer_Byte7;
        private System.Windows.Forms.ColumnHeader HexViewer_Byte8;
        private System.Windows.Forms.ColumnHeader HexViewer_ASCII;
        private System.Windows.Forms.TabPage Tab_Wav_Head_Data;
        protected internal sb_explorer.CustomControls.ListView_ColumnSortingClick ListView_WavData;
        private System.Windows.Forms.ColumnHeader WavHeaderData_Number;
        private System.Windows.Forms.ColumnHeader WavHeaderData_Flags;
        private System.Windows.Forms.ColumnHeader WavHeaderData_Address;
        private System.Windows.Forms.ColumnHeader WavHeaderData_MemorySize;
        private System.Windows.Forms.ColumnHeader WavHeaderData_SampleSize;
        private System.Windows.Forms.ColumnHeader WavHeaderData_Frequency;
        private System.Windows.Forms.ColumnHeader WavHeaderData_LoopStartOffset;
        private System.Windows.Forms.ColumnHeader WavHeaderData_Duration;
        private System.Windows.Forms.TabPage Tab_Stream_Data;
        private System.Windows.Forms.MenuItem MenuItem_File;
        private System.Windows.Forms.MenuItem MenuItemFile_ViewMusic;
        private System.Windows.Forms.MenuItem MenuItemFile_Separator1;
        private System.Windows.Forms.MenuItem MenuItemFile_SoundhDir;
        private System.Windows.Forms.MenuItem MenuItemFile_Separator2;
        private System.Windows.Forms.MenuItem MenuItemFile_Exit;
        private System.Windows.Forms.MenuItem MenuItem_View;
        private System.Windows.Forms.MenuItem MenuItemView_FindHashCode;
        private System.Windows.Forms.MenuItem MenuItemView_FindNextHashCode;
        private System.Windows.Forms.MenuItem MenuItem_Help;
        private System.Windows.Forms.MenuItem MenuItemHelp_About;
        private System.Windows.Forms.ContextMenu ContextMenu_CopyHashCodeInfo;
        private System.Windows.Forms.MenuItem ContextMenuItem_CopyName;
        private System.Windows.Forms.MenuItem ContextMenuItem_CopyHashCode;
        private System.Windows.Forms.MenuItem MenuItem_HexContextMenu_ChangeFont;
        private System.Windows.Forms.MainMenu Form_MainMenu;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Soundbanks;
        private System.Windows.Forms.ContextMenu ContextMenu_HexView_ChangeFont;
        private System.Windows.Forms.Label Label_HexViewer_Key;
        private System.Windows.Forms.Label Label_HexViewer_SamplePool;
        private System.Windows.Forms.Button Button_HexViewer_SamplePool;
        private System.Windows.Forms.Label Label_HexViewer_UserFlags;
        private System.Windows.Forms.Button Button_HexViewer_UserFlags;
        private System.Windows.Forms.Label Label_HexViewer_Flags;
        private System.Windows.Forms.Button Button_HexViewer_Flags;
        private System.Windows.Forms.Label Label_HexViewer_HeaderData;
        private System.Windows.Forms.Button Button_HexViewer_HeaderData;
        private System.Windows.Forms.Label Label_SampleCount;
        private System.Windows.Forms.TextBox vSampleCount;
        private System.Windows.Forms.Label Label_SamplePool;
        private System.Windows.Forms.ListView ListView_SamplePool;
        private System.Windows.Forms.ColumnHeader SamplePool_Hashcode;
        private System.Windows.Forms.ColumnHeader SamplePool_Volume;
        private System.Windows.Forms.ColumnHeader SamplePool_VolOffset;
        private System.Windows.Forms.ColumnHeader SamplePool_Pitch;
        private System.Windows.Forms.ColumnHeader SamplePool_PitchOffset;
        private System.Windows.Forms.ColumnHeader SamplePool_Pan;
        private System.Windows.Forms.ColumnHeader SamplePool_PanOffset;
        private System.Windows.Forms.CheckedListBox CheckedListBox_SampleFlags;
        private System.Windows.Forms.CheckedListBox CheckedListBox_UserFlags;
        private System.Windows.Forms.Button Button_ValidateADPCM;
        private System.Windows.Forms.TextBox Textbox_StreamFilePath;
        private System.Windows.Forms.ColumnHeader StreamData_Index;
        private System.Windows.Forms.ColumnHeader StreamData_AdpcmStatus;
        private System.Windows.Forms.ColumnHeader StreamData_MarkerOffset;
        private System.Windows.Forms.ColumnHeader StreamData_MarkerSize;
        private System.Windows.Forms.ColumnHeader StreamData_AudioOffset;
        private System.Windows.Forms.ColumnHeader StreamData_AudioLength;
        private System.Windows.Forms.ColumnHeader StreamData_BaseVolume;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView ListView_StreamData_StartMarkers;
        private System.Windows.Forms.ColumnHeader StreamData_StartMarkers_No;
        private System.Windows.Forms.ColumnHeader StreamData_StartMarkers_Index;
        private System.Windows.Forms.ColumnHeader StreamData_StartMarkers_Position;
        private System.Windows.Forms.ColumnHeader StreamData_StartMarkers_Type;
        private System.Windows.Forms.ColumnHeader StreamData_StartMarkers_LoopStart;
        private System.Windows.Forms.ColumnHeader StreamData_StartMarkers_LoopMarkerIndex;
        private System.Windows.Forms.ColumnHeader StreamData_StartMarkers_MarkerPosition;
        private System.Windows.Forms.Label Label_StreamData_StartMarkerCount;
        private System.Windows.Forms.TextBox textStartMarkerCount;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView ListView_StreamData_Markers;
        private System.Windows.Forms.ColumnHeader StreamData_Markers_No;
        private System.Windows.Forms.ColumnHeader StreamData_Markers_StartMarkerIndex;
        private System.Windows.Forms.ColumnHeader StreamData_Markers_Position;
        private System.Windows.Forms.ColumnHeader StreamData_Markers_Type;
        private System.Windows.Forms.ColumnHeader StreamData_Markers_LoopStart;
        private System.Windows.Forms.ColumnHeader StreamData_Markers_LoopMarkerIndex;
        private System.Windows.Forms.Label Label_StreamData_MarkerCount;
        private System.Windows.Forms.TextBox textMarkerCount;
        private System.Windows.Forms.Button Button_LoadStreamData;
        private System.Windows.Forms.Label Label_SoundBank_Name;
        private System.Windows.Forms.TextBox SoundbankFileName;
        private System.Windows.Forms.Label Label_HashCodeName;
        private System.Windows.Forms.TextBox HashcodeName;
        private System.Windows.Forms.FontDialog HexViewfontDialog;
        private System.Windows.Forms.MenuItem MenuItemFile_OpenSoundbank;
        private System.Windows.Forms.MenuItem MenuItemFile_RecentFiles;
        private System.Windows.Forms.MenuItem MenuItemFile_Separator3;
        private System.Windows.Forms.OpenFileDialog openFileDialog_MusicFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog_StreamFile;
        protected internal System.Windows.Forms.ListView ListView_StreamData;
        private System.Windows.Forms.ContextMenu ContextMenu_ExporStreamtMarkers;
        private System.Windows.Forms.MenuItem MenuItem_ExportStreamMarkers;
        private System.Windows.Forms.SaveFileDialog SaveFileDlg_SaveFile;
        private System.Windows.Forms.ContextMenu ContextMenu_ListView_WavData;
        private System.Windows.Forms.MenuItem ListViewWavData_Options;
        private System.Windows.Forms.MenuItem ListViewStreamData_Options;
        private System.Windows.Forms.Panel Panel_DetailsAndStreamData;
    }
}
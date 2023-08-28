
namespace sb_editor
{
    partial class ReSampleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReSampleForm));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuSample = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSample_Play = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSample_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSample_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.grbProjectPath = new System.Windows.Forms.GroupBox();
            this.txtMasterFolder = new System.Windows.Forms.TextBox();
            this.grbPreviewSample = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnStopPreview = new System.Windows.Forms.Button();
            this.cboPreviewFormat = new System.Windows.Forms.ComboBox();
            this.ContextMenu_ListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.lblSampleCount = new System.Windows.Forms.Label();
            this.btnEditSample = new System.Windows.Forms.Button();
            this.txtBootupTime = new System.Windows.Forms.TextBox();
            this.grbMoveSamplesToFolder = new System.Windows.Forms.GroupBox();
            this.btnMoveSelection = new System.Windows.Forms.Button();
            this.txtSelectionFolder = new System.Windows.Forms.TextBox();
            this.btnMakePurgeList = new System.Windows.Forms.Button();
            this.btnViewPurgedLis = new System.Windows.Forms.Button();
            this.btnPurgeGo = new System.Windows.Forms.Button();
            this.lblResampleRate = new System.Windows.Forms.Label();
            this.cboSampleRate = new System.Windows.Forms.ComboBox();
            this.btnReSampleAll = new System.Windows.Forms.Button();
            this.btnStreamSel = new System.Windows.Forms.Button();
            this.btnSampleSel = new System.Windows.Forms.Button();
            this.btnDeReSampleAll = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lvwAllSamples = new sb_editor.Panels.ListView_ColumnSortingClick();
            this.Column_SampleFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_ResampleRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_ReSample = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_StreamMe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_ReSmp4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_ReSmp3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_ReSmp2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_ReSmp1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuLoopSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.grbProjectPath.SuspendLayout();
            this.grbPreviewSample.SuspendLayout();
            this.ContextMenu_ListView.SuspendLayout();
            this.grbMoveSamplesToFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSample});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainMenu.Size = new System.Drawing.Size(909, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "Main Menu";
            // 
            // mnuSample
            // 
            this.mnuSample.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSample_Play,
            this.mnuSample_Stop,
            this.mnuSample_Edit});
            this.mnuSample.Name = "mnuSample";
            this.mnuSample.Size = new System.Drawing.Size(58, 24);
            this.mnuSample.Text = "Sample";
            // 
            // mnuSample_Play
            // 
            this.mnuSample_Play.Name = "mnuSample_Play";
            this.mnuSample_Play.Size = new System.Drawing.Size(98, 22);
            this.mnuSample_Play.Text = "Play";
            this.mnuSample_Play.Click += new System.EventHandler(this.MnuSample_Play_Click);
            // 
            // mnuSample_Stop
            // 
            this.mnuSample_Stop.Name = "mnuSample_Stop";
            this.mnuSample_Stop.Size = new System.Drawing.Size(98, 22);
            this.mnuSample_Stop.Text = "Stop";
            this.mnuSample_Stop.Click += new System.EventHandler(this.MnuSample_Stop_Click);
            // 
            // mnuSample_Edit
            // 
            this.mnuSample_Edit.Name = "mnuSample_Edit";
            this.mnuSample_Edit.Size = new System.Drawing.Size(98, 22);
            this.mnuSample_Edit.Text = "Edit";
            this.mnuSample_Edit.Click += new System.EventHandler(this.MnuSample_Edit_Click);
            // 
            // grbProjectPath
            // 
            this.grbProjectPath.Controls.Add(this.txtMasterFolder);
            this.grbProjectPath.Location = new System.Drawing.Point(12, 27);
            this.grbProjectPath.Name = "grbProjectPath";
            this.grbProjectPath.Size = new System.Drawing.Size(463, 48);
            this.grbProjectPath.TabIndex = 0;
            this.grbProjectPath.TabStop = false;
            this.grbProjectPath.Text = "Project Path";
            // 
            // txtMasterFolder
            // 
            this.txtMasterFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterFolder.BackColor = System.Drawing.SystemColors.Window;
            this.txtMasterFolder.Location = new System.Drawing.Point(6, 19);
            this.txtMasterFolder.Name = "txtMasterFolder";
            this.txtMasterFolder.ReadOnly = true;
            this.txtMasterFolder.Size = new System.Drawing.Size(451, 20);
            this.txtMasterFolder.TabIndex = 0;
            // 
            // grbPreviewSample
            // 
            this.grbPreviewSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbPreviewSample.Controls.Add(this.btnPreview);
            this.grbPreviewSample.Controls.Add(this.btnStopPreview);
            this.grbPreviewSample.Controls.Add(this.cboPreviewFormat);
            this.grbPreviewSample.Location = new System.Drawing.Point(481, 27);
            this.grbPreviewSample.Name = "grbPreviewSample";
            this.grbPreviewSample.Size = new System.Drawing.Size(416, 48);
            this.grbPreviewSample.TabIndex = 2;
            this.grbPreviewSample.TabStop = false;
            this.grbPreviewSample.Text = "Preview Sample";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(254, 17);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 2;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // btnStopPreview
            // 
            this.btnStopPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopPreview.Location = new System.Drawing.Point(335, 17);
            this.btnStopPreview.Name = "btnStopPreview";
            this.btnStopPreview.Size = new System.Drawing.Size(75, 23);
            this.btnStopPreview.TabIndex = 1;
            this.btnStopPreview.Text = "Stop";
            this.btnStopPreview.UseVisualStyleBackColor = true;
            this.btnStopPreview.Click += new System.EventHandler(this.BtnStopPreview_Click);
            // 
            // cboPreviewFormat
            // 
            this.cboPreviewFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPreviewFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPreviewFormat.FormattingEnabled = true;
            this.cboPreviewFormat.Location = new System.Drawing.Point(6, 19);
            this.cboPreviewFormat.Name = "cboPreviewFormat";
            this.cboPreviewFormat.Size = new System.Drawing.Size(242, 21);
            this.cboPreviewFormat.TabIndex = 0;
            // 
            // ContextMenu_ListView
            // 
            this.ContextMenu_ListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPlay,
            this.mnuStop,
            this.mnuEdit,
            this.mnuLoopSettings});
            this.ContextMenu_ListView.Name = "ContextMenu_ListView";
            this.ContextMenu_ListView.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ContextMenu_ListView.Size = new System.Drawing.Size(188, 114);
            // 
            // mnuPlay
            // 
            this.mnuPlay.Name = "mnuPlay";
            this.mnuPlay.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.mnuPlay.Size = new System.Drawing.Size(187, 22);
            this.mnuPlay.Text = "Play";
            this.mnuPlay.Click += new System.EventHandler(this.MnuPlay_Click);
            // 
            // mnuStop
            // 
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Escape)));
            this.mnuStop.Size = new System.Drawing.Size(187, 22);
            this.mnuStop.Text = "Stop";
            this.mnuStop.Click += new System.EventHandler(this.MnuStop_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Back)));
            this.mnuEdit.Size = new System.Drawing.Size(187, 22);
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Click += new System.EventHandler(this.MnuEdit_Click);
            // 
            // lblSampleCount
            // 
            this.lblSampleCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSampleCount.AutoSize = true;
            this.lblSampleCount.Location = new System.Drawing.Point(91, 437);
            this.lblSampleCount.Name = "lblSampleCount";
            this.lblSampleCount.Size = new System.Drawing.Size(85, 13);
            this.lblSampleCount.TabIndex = 4;
            this.lblSampleCount.Text = "Sample Count: 0";
            // 
            // btnEditSample
            // 
            this.btnEditSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditSample.Location = new System.Drawing.Point(94, 466);
            this.btnEditSample.Name = "btnEditSample";
            this.btnEditSample.Size = new System.Drawing.Size(75, 23);
            this.btnEditSample.TabIndex = 5;
            this.btnEditSample.Text = "Edit";
            this.btnEditSample.UseVisualStyleBackColor = true;
            this.btnEditSample.Click += new System.EventHandler(this.BtnEditSample_Click);
            // 
            // txtBootupTime
            // 
            this.txtBootupTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBootupTime.BackColor = System.Drawing.SystemColors.Window;
            this.txtBootupTime.Location = new System.Drawing.Point(175, 468);
            this.txtBootupTime.Name = "txtBootupTime";
            this.txtBootupTime.ReadOnly = true;
            this.txtBootupTime.Size = new System.Drawing.Size(246, 20);
            this.txtBootupTime.TabIndex = 6;
            // 
            // grbMoveSamplesToFolder
            // 
            this.grbMoveSamplesToFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbMoveSamplesToFolder.Controls.Add(this.btnMoveSelection);
            this.grbMoveSamplesToFolder.Controls.Add(this.txtSelectionFolder);
            this.grbMoveSamplesToFolder.Location = new System.Drawing.Point(12, 495);
            this.grbMoveSamplesToFolder.Name = "grbMoveSamplesToFolder";
            this.grbMoveSamplesToFolder.Size = new System.Drawing.Size(784, 43);
            this.grbMoveSamplesToFolder.TabIndex = 7;
            this.grbMoveSamplesToFolder.TabStop = false;
            this.grbMoveSamplesToFolder.Text = "Move Selected Sample Folder:";
            // 
            // btnMoveSelection
            // 
            this.btnMoveSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveSelection.Location = new System.Drawing.Point(616, 15);
            this.btnMoveSelection.Name = "btnMoveSelection";
            this.btnMoveSelection.Size = new System.Drawing.Size(162, 23);
            this.btnMoveSelection.TabIndex = 1;
            this.btnMoveSelection.Text = "Move Selection to this Folder";
            this.btnMoveSelection.UseVisualStyleBackColor = true;
            this.btnMoveSelection.Click += new System.EventHandler(this.BtnMoveSelection_Click);
            // 
            // txtSelectionFolder
            // 
            this.txtSelectionFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelectionFolder.BackColor = System.Drawing.SystemColors.Window;
            this.txtSelectionFolder.Location = new System.Drawing.Point(6, 17);
            this.txtSelectionFolder.Name = "txtSelectionFolder";
            this.txtSelectionFolder.ReadOnly = true;
            this.txtSelectionFolder.Size = new System.Drawing.Size(604, 20);
            this.txtSelectionFolder.TabIndex = 0;
            this.txtSelectionFolder.Text = "Set Folder";
            this.txtSelectionFolder.DoubleClick += new System.EventHandler(this.TxtSelectionFolder_DoubleClick);
            // 
            // btnMakePurgeList
            // 
            this.btnMakePurgeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMakePurgeList.Location = new System.Drawing.Point(12, 544);
            this.btnMakePurgeList.Name = "btnMakePurgeList";
            this.btnMakePurgeList.Size = new System.Drawing.Size(94, 23);
            this.btnMakePurgeList.TabIndex = 8;
            this.btnMakePurgeList.Text = "Make Purge List";
            this.btnMakePurgeList.UseVisualStyleBackColor = true;
            this.btnMakePurgeList.Click += new System.EventHandler(this.BtnMakePurgeList_Click);
            // 
            // btnViewPurgedLis
            // 
            this.btnViewPurgedLis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnViewPurgedLis.Location = new System.Drawing.Point(112, 544);
            this.btnViewPurgedLis.Name = "btnViewPurgedLis";
            this.btnViewPurgedLis.Size = new System.Drawing.Size(94, 23);
            this.btnViewPurgedLis.TabIndex = 9;
            this.btnViewPurgedLis.Text = "View Purged List";
            this.btnViewPurgedLis.UseVisualStyleBackColor = true;
            this.btnViewPurgedLis.Click += new System.EventHandler(this.BtnViewPurgedLis_Click);
            // 
            // btnPurgeGo
            // 
            this.btnPurgeGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPurgeGo.Location = new System.Drawing.Point(212, 544);
            this.btnPurgeGo.Name = "btnPurgeGo";
            this.btnPurgeGo.Size = new System.Drawing.Size(94, 23);
            this.btnPurgeGo.TabIndex = 10;
            this.btnPurgeGo.Text = "Purge Go";
            this.btnPurgeGo.UseVisualStyleBackColor = true;
            this.btnPurgeGo.Click += new System.EventHandler(this.BtnPurgeGo_Click);
            // 
            // lblResampleRate
            // 
            this.lblResampleRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResampleRate.AutoSize = true;
            this.lblResampleRate.Location = new System.Drawing.Point(267, 437);
            this.lblResampleRate.Name = "lblResampleRate";
            this.lblResampleRate.Size = new System.Drawing.Size(185, 13);
            this.lblResampleRate.TabIndex = 11;
            this.lblResampleRate.Text = "Set Re-Sample Rate for Selection To:";
            // 
            // cboSampleRate
            // 
            this.cboSampleRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSampleRate.FormattingEnabled = true;
            this.cboSampleRate.Location = new System.Drawing.Point(458, 434);
            this.cboSampleRate.Name = "cboSampleRate";
            this.cboSampleRate.Size = new System.Drawing.Size(237, 21);
            this.cboSampleRate.TabIndex = 12;
            this.cboSampleRate.SelectionChangeCommitted += new System.EventHandler(this.CboSampleRate_SelectionChangeCommitted);
            // 
            // btnReSampleAll
            // 
            this.btnReSampleAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReSampleAll.Location = new System.Drawing.Point(701, 437);
            this.btnReSampleAll.Name = "btnReSampleAll";
            this.btnReSampleAll.Size = new System.Drawing.Size(95, 23);
            this.btnReSampleAll.TabIndex = 13;
            this.btnReSampleAll.Text = "ReSample All";
            this.btnReSampleAll.UseVisualStyleBackColor = true;
            this.btnReSampleAll.Click += new System.EventHandler(this.BtnReSampleAll_Click);
            // 
            // btnStreamSel
            // 
            this.btnStreamSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStreamSel.Location = new System.Drawing.Point(802, 437);
            this.btnStreamSel.Name = "btnStreamSel";
            this.btnStreamSel.Size = new System.Drawing.Size(95, 23);
            this.btnStreamSel.TabIndex = 14;
            this.btnStreamSel.Text = "Stream Sel.";
            this.btnStreamSel.UseVisualStyleBackColor = true;
            this.btnStreamSel.Click += new System.EventHandler(this.BtnStreamSel_Click);
            // 
            // btnSampleSel
            // 
            this.btnSampleSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSampleSel.Location = new System.Drawing.Point(802, 466);
            this.btnSampleSel.Name = "btnSampleSel";
            this.btnSampleSel.Size = new System.Drawing.Size(95, 23);
            this.btnSampleSel.TabIndex = 16;
            this.btnSampleSel.Text = "Sample Sel.";
            this.btnSampleSel.UseVisualStyleBackColor = true;
            this.btnSampleSel.Click += new System.EventHandler(this.BtnSampleSel_Click);
            // 
            // btnDeReSampleAll
            // 
            this.btnDeReSampleAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeReSampleAll.Location = new System.Drawing.Point(701, 466);
            this.btnDeReSampleAll.Name = "btnDeReSampleAll";
            this.btnDeReSampleAll.Size = new System.Drawing.Size(95, 23);
            this.btnDeReSampleAll.TabIndex = 15;
            this.btnDeReSampleAll.Text = "DeReSample All";
            this.btnDeReSampleAll.UseVisualStyleBackColor = true;
            this.btnDeReSampleAll.Click += new System.EventHandler(this.BtnDeReSampleAll_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(822, 544);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Set Folder For Sample Files.";
            // 
            // lvwAllSamples
            // 
            this.lvwAllSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwAllSamples.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column_SampleFileName,
            this.Column_ResampleRate,
            this.Column_Size,
            this.Column_Date,
            this.Column_ReSample,
            this.Column_StreamMe,
            this.Column_ReSmp4,
            this.Column_ReSmp3,
            this.Column_ReSmp2,
            this.Column_ReSmp1});
            this.lvwAllSamples.ContextMenuStrip = this.ContextMenu_ListView;
            this.lvwAllSamples.FullRowSelect = true;
            this.lvwAllSamples.GridLines = true;
            this.lvwAllSamples.HideSelection = false;
            this.lvwAllSamples.Location = new System.Drawing.Point(12, 81);
            this.lvwAllSamples.Name = "lvwAllSamples";
            this.lvwAllSamples.Size = new System.Drawing.Size(885, 347);
            this.lvwAllSamples.TabIndex = 3;
            this.lvwAllSamples.UseCompatibleStateImageBehavior = false;
            this.lvwAllSamples.View = System.Windows.Forms.View.Details;
            this.lvwAllSamples.SelectedIndexChanged += new System.EventHandler(this.LvwAllSamples_SelectedIndexChanged);
            this.lvwAllSamples.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvwAllSamples_MouseDoubleClick);
            // 
            // Column_SampleFileName
            // 
            this.Column_SampleFileName.Text = "Sample Filename";
            this.Column_SampleFileName.Width = 350;
            // 
            // Column_ResampleRate
            // 
            this.Column_ResampleRate.Text = "Re-Sample Rate";
            this.Column_ResampleRate.Width = 100;
            // 
            // Column_Size
            // 
            this.Column_Size.Text = "Size";
            this.Column_Size.Width = 90;
            // 
            // Column_Date
            // 
            this.Column_Date.Text = "Date";
            this.Column_Date.Width = 150;
            // 
            // Column_ReSample
            // 
            this.Column_ReSample.Text = "ReSample";
            this.Column_ReSample.Width = 80;
            // 
            // Column_StreamMe
            // 
            this.Column_StreamMe.Text = "Stream Me?";
            this.Column_StreamMe.Width = 100;
            // 
            // Column_ReSmp4
            // 
            this.Column_ReSmp4.Text = "ReSmp4";
            // 
            // Column_ReSmp3
            // 
            this.Column_ReSmp3.Text = "ReSmp2";
            // 
            // Column_ReSmp2
            // 
            this.Column_ReSmp2.Text = "ReSmp3";
            // 
            // Column_ReSmp1
            // 
            this.Column_ReSmp1.Text = "ReSmp4";
            // 
            // mnuLoopSettings
            // 
            this.mnuLoopSettings.Name = "mnuLoopSettings";
            this.mnuLoopSettings.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuLoopSettings.Size = new System.Drawing.Size(187, 22);
            this.mnuLoopSettings.Text = "Loop Settings";
            this.mnuLoopSettings.Click += new System.EventHandler(this.MnuLoopSettings_Click);
            // 
            // ReSampleForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 579);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnSampleSel);
            this.Controls.Add(this.btnDeReSampleAll);
            this.Controls.Add(this.btnStreamSel);
            this.Controls.Add(this.btnReSampleAll);
            this.Controls.Add(this.cboSampleRate);
            this.Controls.Add(this.lblResampleRate);
            this.Controls.Add(this.btnPurgeGo);
            this.Controls.Add(this.btnViewPurgedLis);
            this.Controls.Add(this.btnMakePurgeList);
            this.Controls.Add(this.grbMoveSamplesToFolder);
            this.Controls.Add(this.txtBootupTime);
            this.Controls.Add(this.btnEditSample);
            this.Controls.Add(this.lblSampleCount);
            this.Controls.Add(this.lvwAllSamples);
            this.Controls.Add(this.grbPreviewSample);
            this.Controls.Add(this.grbProjectPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReSampleForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Re-Sample Rates";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_ReSampleRates_FormClosing);
            this.Load += new System.EventHandler(this.Frm_ReSampleRates_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grbProjectPath.ResumeLayout(false);
            this.grbProjectPath.PerformLayout();
            this.grbPreviewSample.ResumeLayout(false);
            this.ContextMenu_ListView.ResumeLayout(false);
            this.grbMoveSamplesToFolder.ResumeLayout(false);
            this.grbMoveSamplesToFolder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuSample;
        private System.Windows.Forms.ToolStripMenuItem mnuSample_Play;
        private System.Windows.Forms.ToolStripMenuItem mnuSample_Stop;
        private System.Windows.Forms.ToolStripMenuItem mnuSample_Edit;
        private System.Windows.Forms.GroupBox grbProjectPath;
        private System.Windows.Forms.TextBox txtMasterFolder;
        private System.Windows.Forms.GroupBox grbPreviewSample;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnStopPreview;
        private System.Windows.Forms.ComboBox cboPreviewFormat;
        private System.Windows.Forms.ColumnHeader Column_SampleFileName;
        private System.Windows.Forms.ColumnHeader Column_ResampleRate;
        private System.Windows.Forms.ColumnHeader Column_Size;
        private System.Windows.Forms.ColumnHeader Column_Date;
        private System.Windows.Forms.ColumnHeader Column_ReSample;
        private System.Windows.Forms.ColumnHeader Column_StreamMe;
        private System.Windows.Forms.ColumnHeader Column_ReSmp4;
        private System.Windows.Forms.ColumnHeader Column_ReSmp3;
        private System.Windows.Forms.ColumnHeader Column_ReSmp2;
        private System.Windows.Forms.ColumnHeader Column_ReSmp1;
        private System.Windows.Forms.Label lblSampleCount;
        private System.Windows.Forms.Button btnEditSample;
        private System.Windows.Forms.GroupBox grbMoveSamplesToFolder;
        private System.Windows.Forms.Button btnMoveSelection;
        private System.Windows.Forms.TextBox txtSelectionFolder;
        private System.Windows.Forms.Button btnMakePurgeList;
        private System.Windows.Forms.Button btnViewPurgedLis;
        private System.Windows.Forms.Button btnPurgeGo;
        private System.Windows.Forms.Label lblResampleRate;
        private System.Windows.Forms.ComboBox cboSampleRate;
        private System.Windows.Forms.Button btnReSampleAll;
        private System.Windows.Forms.Button btnStreamSel;
        private System.Windows.Forms.Button btnSampleSel;
        private System.Windows.Forms.Button btnDeReSampleAll;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_ListView;
        private System.Windows.Forms.ToolStripMenuItem mnuPlay;
        private System.Windows.Forms.ToolStripMenuItem mnuStop;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        protected internal System.Windows.Forms.TextBox txtBootupTime;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        protected internal Panels.ListView_ColumnSortingClick lvwAllSamples;
        private System.Windows.Forms.ToolStripMenuItem mnuLoopSettings;
    }
}
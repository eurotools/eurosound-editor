
namespace sb_editor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuFile_Open = new System.Windows.Forms.MenuItem();
            this.mnuFile_New = new System.Windows.Forms.MenuItem();
            this.mnuFile_RecentProjects = new System.Windows.Forms.MenuItem();
            this.mnuFile_RecentFiles = new System.Windows.Forms.MenuItem();
            this.mnuFile_Exit = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelp_About = new System.Windows.Forms.MenuItem();
            this.SplitContainer_Lists = new System.Windows.Forms.SplitContainer();
            this.UserControl_Available_Databases = new sb_editor.Panels.UserControl_MainForm_AvailableDataBases();
            this.SplitContainers_Lists2 = new System.Windows.Forms.SplitContainer();
            this.UserControl_DataBaseSfx = new sb_editor.Panels.UserControl_MainForm_SfxInDataBase();
            this.UserControl_Available_SFXs = new sb_editor.Panels.UserControl_MainForm_AvailableSFX();
            this.UserControl_DataBasesInSoundBank = new sb_editor.Panels.UserControl_MainForm_DataBasesInSoundBank();
            this.UserControl_SoundBanks_CheckBox = new sb_editor.Panels.UserControl_MainForm_SoundBanks_CheckBox();
            this.UserControl_Misc = new sb_editor.Panels.UserControl_MainForm_Misc();
            this.UserControl_Output = new sb_editor.Panels.UserControl_MainForm_Output();
            this.UserControl_SoundBanks = new sb_editor.Panels.UserControl_Manform_SoundBanks();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Lists)).BeginInit();
            this.SplitContainer_Lists.Panel1.SuspendLayout();
            this.SplitContainer_Lists.Panel2.SuspendLayout();
            this.SplitContainer_Lists.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainers_Lists2)).BeginInit();
            this.SplitContainers_Lists2.Panel1.SuspendLayout();
            this.SplitContainers_Lists2.Panel2.SuspendLayout();
            this.SplitContainers_Lists2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuHelp});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile_Open,
            this.mnuFile_New,
            this.mnuFile_RecentProjects,
            this.mnuFile_Exit});
            this.mnuFile.Text = "File";
            // 
            // mnuFile_Open
            // 
            this.mnuFile_Open.Index = 0;
            this.mnuFile_Open.Text = "Open Project";
            this.mnuFile_Open.Click += new System.EventHandler(this.MnuFile_Open_Click);
            // 
            // mnuFile_New
            // 
            this.mnuFile_New.Index = 1;
            this.mnuFile_New.Text = "New Project";
            this.mnuFile_New.Click += new System.EventHandler(this.MnuFile_New_Click);
            // 
            // mnuFile_RecentProjects
            // 
            this.mnuFile_RecentProjects.Index = 2;
            this.mnuFile_RecentProjects.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile_RecentFiles});
            this.mnuFile_RecentProjects.Text = "Recent Projects";
            // 
            // mnuFile_RecentFiles
            // 
            this.mnuFile_RecentFiles.Index = 0;
            this.mnuFile_RecentFiles.Text = "";
            // 
            // mnuFile_Exit
            // 
            this.mnuFile_Exit.Index = 3;
            this.mnuFile_Exit.Text = "Exit";
            this.mnuFile_Exit.Click += new System.EventHandler(this.MnuFile_Exit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 1;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelp_About});
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelp_About
            // 
            this.mnuHelp_About.Index = 0;
            this.mnuHelp_About.Text = "About";
            this.mnuHelp_About.Click += new System.EventHandler(this.MnuHelp_About_Click);
            // 
            // SplitContainer_Lists
            // 
            this.SplitContainer_Lists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer_Lists.Location = new System.Drawing.Point(414, 2);
            this.SplitContainer_Lists.Name = "SplitContainer_Lists";
            // 
            // SplitContainer_Lists.Panel1
            // 
            this.SplitContainer_Lists.Panel1.Controls.Add(this.UserControl_Available_Databases);
            // 
            // SplitContainer_Lists.Panel2
            // 
            this.SplitContainer_Lists.Panel2.Controls.Add(this.SplitContainers_Lists2);
            this.SplitContainer_Lists.Size = new System.Drawing.Size(735, 703);
            this.SplitContainer_Lists.SplitterDistance = 208;
            this.SplitContainer_Lists.TabIndex = 3;
            // 
            // UserControl_Available_Databases
            // 
            this.UserControl_Available_Databases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserControl_Available_Databases.Location = new System.Drawing.Point(0, 0);
            this.UserControl_Available_Databases.Name = "UserControl_Available_Databases";
            this.UserControl_Available_Databases.Size = new System.Drawing.Size(208, 703);
            this.UserControl_Available_Databases.TabIndex = 0;
            // 
            // SplitContainers_Lists2
            // 
            this.SplitContainers_Lists2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainers_Lists2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainers_Lists2.Name = "SplitContainers_Lists2";
            // 
            // SplitContainers_Lists2.Panel1
            // 
            this.SplitContainers_Lists2.Panel1.Controls.Add(this.UserControl_DataBaseSfx);
            // 
            // SplitContainers_Lists2.Panel2
            // 
            this.SplitContainers_Lists2.Panel2.Controls.Add(this.UserControl_Available_SFXs);
            this.SplitContainers_Lists2.Size = new System.Drawing.Size(523, 703);
            this.SplitContainers_Lists2.SplitterDistance = 229;
            this.SplitContainers_Lists2.TabIndex = 1;
            // 
            // UserControl_DataBaseSfx
            // 
            this.UserControl_DataBaseSfx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserControl_DataBaseSfx.Location = new System.Drawing.Point(0, 0);
            this.UserControl_DataBaseSfx.Name = "UserControl_DataBaseSfx";
            this.UserControl_DataBaseSfx.Size = new System.Drawing.Size(229, 703);
            this.UserControl_DataBaseSfx.TabIndex = 0;
            // 
            // UserControl_Available_SFXs
            // 
            this.UserControl_Available_SFXs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserControl_Available_SFXs.EnableReadOnly = false;
            this.UserControl_Available_SFXs.Location = new System.Drawing.Point(0, 0);
            this.UserControl_Available_SFXs.Name = "UserControl_Available_SFXs";
            this.UserControl_Available_SFXs.Size = new System.Drawing.Size(290, 703);
            this.UserControl_Available_SFXs.TabIndex = 1;
            // 
            // UserControl_DataBasesInSoundBank
            // 
            this.UserControl_DataBasesInSoundBank.Location = new System.Drawing.Point(210, 82);
            this.UserControl_DataBasesInSoundBank.Name = "UserControl_DataBasesInSoundBank";
            this.UserControl_DataBasesInSoundBank.Size = new System.Drawing.Size(192, 179);
            this.UserControl_DataBasesInSoundBank.TabIndex = 5;
            this.UserControl_DataBasesInSoundBank.Visible = false;
            // 
            // UserControl_SoundBanks_CheckBox
            // 
            this.UserControl_SoundBanks_CheckBox.Location = new System.Drawing.Point(12, 82);
            this.UserControl_SoundBanks_CheckBox.Name = "UserControl_SoundBanks_CheckBox";
            this.UserControl_SoundBanks_CheckBox.Size = new System.Drawing.Size(192, 179);
            this.UserControl_SoundBanks_CheckBox.TabIndex = 4;
            this.UserControl_SoundBanks_CheckBox.Visible = false;
            // 
            // UserControl_Misc
            // 
            this.UserControl_Misc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UserControl_Misc.Location = new System.Drawing.Point(259, 484);
            this.UserControl_Misc.Name = "UserControl_Misc";
            this.UserControl_Misc.Size = new System.Drawing.Size(149, 221);
            this.UserControl_Misc.TabIndex = 2;
            // 
            // UserControl_Output
            // 
            this.UserControl_Output.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UserControl_Output.Location = new System.Drawing.Point(12, 484);
            this.UserControl_Output.Name = "UserControl_Output";
            this.UserControl_Output.Size = new System.Drawing.Size(241, 221);
            this.UserControl_Output.TabIndex = 1;
            // 
            // UserControl_SoundBanks
            // 
            this.UserControl_SoundBanks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UserControl_SoundBanks.Location = new System.Drawing.Point(12, 2);
            this.UserControl_SoundBanks.Name = "UserControl_SoundBanks";
            this.UserControl_SoundBanks.Size = new System.Drawing.Size(396, 476);
            this.UserControl_SoundBanks.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 717);
            this.Controls.Add(this.UserControl_DataBasesInSoundBank);
            this.Controls.Add(this.UserControl_SoundBanks_CheckBox);
            this.Controls.Add(this.SplitContainer_Lists);
            this.Controls.Add(this.UserControl_Misc);
            this.Controls.Add(this.UserControl_Output);
            this.Controls.Add(this.UserControl_SoundBanks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.MainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EuroSound";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.Frm_MainForm_Shown);
            this.SplitContainer_Lists.Panel1.ResumeLayout(false);
            this.SplitContainer_Lists.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Lists)).EndInit();
            this.SplitContainer_Lists.ResumeLayout(false);
            this.SplitContainers_Lists2.Panel1.ResumeLayout(false);
            this.SplitContainers_Lists2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainers_Lists2)).EndInit();
            this.SplitContainers_Lists2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.MenuItem mnuFile;
        private System.Windows.Forms.MenuItem mnuFile_Open;
        private System.Windows.Forms.MenuItem mnuFile_New;
        private System.Windows.Forms.MenuItem mnuFile_Exit;
        private System.Windows.Forms.MenuItem mnuHelp;
        private System.Windows.Forms.MenuItem mnuHelp_About;
        protected internal Panels.UserControl_Manform_SoundBanks UserControl_SoundBanks;
        protected internal Panels.UserControl_MainForm_Output UserControl_Output;
        protected internal Panels.UserControl_MainForm_Misc UserControl_Misc;
        protected internal Panels.UserControl_MainForm_AvailableDataBases UserControl_Available_Databases;
        protected internal Panels.UserControl_MainForm_SfxInDataBase UserControl_DataBaseSfx;
        protected internal Panels.UserControl_MainForm_AvailableSFX UserControl_Available_SFXs;
        protected internal System.Windows.Forms.MenuItem mnuFile_RecentProjects;
        protected internal System.Windows.Forms.MenuItem mnuFile_RecentFiles;
        protected internal Panels.UserControl_MainForm_SoundBanks_CheckBox UserControl_SoundBanks_CheckBox;
        protected internal Panels.UserControl_MainForm_DataBasesInSoundBank UserControl_DataBasesInSoundBank;
        protected internal System.Windows.Forms.SplitContainer SplitContainer_Lists;
        protected internal System.Windows.Forms.SplitContainer SplitContainers_Lists2;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}
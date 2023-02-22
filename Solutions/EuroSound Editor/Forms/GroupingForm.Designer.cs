
namespace sb_editor.Forms
{
    partial class GroupingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupingForm));
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbSfxInGroup = new System.Windows.Forms.GroupBox();
            this.nudMaxChannels_Group = new System.Windows.Forms.NumericUpDown();
            this.btnRemoveSFXsFromGroup = new System.Windows.Forms.Button();
            this.cboSFXsInGroup_Steal = new System.Windows.Forms.ComboBox();
            this.lblSFXsInGroup_Steal = new System.Windows.Forms.Label();
            this.lblMaxVoices_Group = new System.Windows.Forms.Label();
            this.lblSFXsInGroup_Count = new System.Windows.Forms.Label();
            this.lvwSFXsInGroup = new sb_editor.Panels.ListView_ColumnSortingClick();
            this.Col_SfxGroup_Hashcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_SfxGroup_MaxVoices = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_SfxGroup_Steal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_SfxGroup_Priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grbAvailableSfx = new System.Windows.Forms.GroupBox();
            this.lvwAvailable_SFXs = new sb_editor.Panels.ListView_ColumnSortingClick();
            this.Col_AvailableSFXs_HashCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_AvailableSFXs_MaxVoices = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_AvailableSFXs_Steal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nudMaxChannels_SFXs = new System.Windows.Forms.NumericUpDown();
            this.btnAddSFXsToGroup = new System.Windows.Forms.Button();
            this.cboAvailableSFXs_Steal = new System.Windows.Forms.ComboBox();
            this.lblAvailableSFXs_Steal = new System.Windows.Forms.Label();
            this.lblMaxVoices_SFX = new System.Windows.Forms.Label();
            this.lblTotalSFXs = new System.Windows.Forms.Label();
            this.txtBootupTime = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.grbGroups = new System.Windows.Forms.GroupBox();
            this.nudMaxVoices = new System.Windows.Forms.NumericUpDown();
            this.nudPriority = new System.Windows.Forms.NumericUpDown();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblMaxVoices = new System.Windows.Forms.Label();
            this.chkDistanceWhenTesting = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.grbAction = new System.Windows.Forms.GroupBox();
            this.RadiobtnAction_Reject = new System.Windows.Forms.RadioButton();
            this.RadiobtnAction_Steal = new System.Windows.Forms.RadioButton();
            this.lblGroupsCount = new System.Windows.Forms.Label();
            this.lvwGroups = new sb_editor.Panels.ListView_ColumnSortingClick();
            this.Col_Label = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Max = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRename = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lstAvailableGroups = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.grbSfxInGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxChannels_Group)).BeginInit();
            this.grbAvailableSfx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxChannels_SFXs)).BeginInit();
            this.grbGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxVoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).BeginInit();
            this.grbAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Location = new System.Drawing.Point(331, 12);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.grbSfxInGroup);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.grbAvailableSfx);
            this.SplitContainer1.Size = new System.Drawing.Size(700, 586);
            this.SplitContainer1.SplitterDistance = 347;
            this.SplitContainer1.TabIndex = 24;
            // 
            // grbSfxInGroup
            // 
            this.grbSfxInGroup.Controls.Add(this.nudMaxChannels_Group);
            this.grbSfxInGroup.Controls.Add(this.btnRemoveSFXsFromGroup);
            this.grbSfxInGroup.Controls.Add(this.cboSFXsInGroup_Steal);
            this.grbSfxInGroup.Controls.Add(this.lblSFXsInGroup_Steal);
            this.grbSfxInGroup.Controls.Add(this.lblMaxVoices_Group);
            this.grbSfxInGroup.Controls.Add(this.lblSFXsInGroup_Count);
            this.grbSfxInGroup.Controls.Add(this.lvwSFXsInGroup);
            this.grbSfxInGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbSfxInGroup.Location = new System.Drawing.Point(0, 0);
            this.grbSfxInGroup.Name = "grbSfxInGroup";
            this.grbSfxInGroup.Size = new System.Drawing.Size(347, 586);
            this.grbSfxInGroup.TabIndex = 1;
            this.grbSfxInGroup.TabStop = false;
            this.grbSfxInGroup.Text = "SFXs in Group";
            // 
            // nudMaxChannels_Group
            // 
            this.nudMaxChannels_Group.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaxChannels_Group.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMaxChannels_Group.Location = new System.Drawing.Point(163, 530);
            this.nudMaxChannels_Group.Name = "nudMaxChannels_Group";
            this.nudMaxChannels_Group.Size = new System.Drawing.Size(53, 20);
            this.nudMaxChannels_Group.TabIndex = 3;
            this.nudMaxChannels_Group.Click += new System.EventHandler(this.NudMaxChannels_Group_Click);
            // 
            // btnRemoveSFXsFromGroup
            // 
            this.btnRemoveSFXsFromGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveSFXsFromGroup.Location = new System.Drawing.Point(6, 557);
            this.btnRemoveSFXsFromGroup.Name = "btnRemoveSFXsFromGroup";
            this.btnRemoveSFXsFromGroup.Size = new System.Drawing.Size(335, 23);
            this.btnRemoveSFXsFromGroup.TabIndex = 6;
            this.btnRemoveSFXsFromGroup.Text = "Remove SFXs From Group >>>";
            this.btnRemoveSFXsFromGroup.UseVisualStyleBackColor = true;
            this.btnRemoveSFXsFromGroup.Click += new System.EventHandler(this.BtnRemoveSFXsFromGroup_Click);
            // 
            // cboSFXsInGroup_Steal
            // 
            this.cboSFXsInGroup_Steal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSFXsInGroup_Steal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSFXsInGroup_Steal.FormattingEnabled = true;
            this.cboSFXsInGroup_Steal.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSFXsInGroup_Steal.Location = new System.Drawing.Point(281, 529);
            this.cboSFXsInGroup_Steal.Name = "cboSFXsInGroup_Steal";
            this.cboSFXsInGroup_Steal.Size = new System.Drawing.Size(60, 21);
            this.cboSFXsInGroup_Steal.TabIndex = 5;
            this.cboSFXsInGroup_Steal.SelectionChangeCommitted += new System.EventHandler(this.CboSFXsInGroup_Steal_SelectionChangeCommitted);
            // 
            // lblSFXsInGroup_Steal
            // 
            this.lblSFXsInGroup_Steal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSFXsInGroup_Steal.AutoSize = true;
            this.lblSFXsInGroup_Steal.Location = new System.Drawing.Point(235, 533);
            this.lblSFXsInGroup_Steal.Name = "lblSFXsInGroup_Steal";
            this.lblSFXsInGroup_Steal.Size = new System.Drawing.Size(40, 13);
            this.lblSFXsInGroup_Steal.TabIndex = 4;
            this.lblSFXsInGroup_Steal.Text = "Steal?:";
            // 
            // lblMaxVoices_Group
            // 
            this.lblMaxVoices_Group.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxVoices_Group.AutoSize = true;
            this.lblMaxVoices_Group.Location = new System.Drawing.Point(127, 533);
            this.lblMaxVoices_Group.Name = "lblMaxVoices_Group";
            this.lblMaxVoices_Group.Size = new System.Drawing.Size(30, 13);
            this.lblMaxVoices_Group.TabIndex = 2;
            this.lblMaxVoices_Group.Text = "Max:";
            // 
            // lblSFXsInGroup_Count
            // 
            this.lblSFXsInGroup_Count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSFXsInGroup_Count.AutoSize = true;
            this.lblSFXsInGroup_Count.Location = new System.Drawing.Point(6, 533);
            this.lblSFXsInGroup_Count.Name = "lblSFXsInGroup_Count";
            this.lblSFXsInGroup_Count.Size = new System.Drawing.Size(43, 13);
            this.lblSFXsInGroup_Count.TabIndex = 1;
            this.lblSFXsInGroup_Count.Text = "Total: 0";
            // 
            // lvwSFXsInGroup
            // 
            this.lvwSFXsInGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwSFXsInGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_SfxGroup_Hashcode,
            this.Col_SfxGroup_MaxVoices,
            this.Col_SfxGroup_Steal,
            this.Col_SfxGroup_Priority});
            this.lvwSFXsInGroup.FullRowSelect = true;
            this.lvwSFXsInGroup.GridLines = true;
            this.lvwSFXsInGroup.HideSelection = false;
            this.lvwSFXsInGroup.Location = new System.Drawing.Point(6, 19);
            this.lvwSFXsInGroup.Name = "lvwSFXsInGroup";
            this.lvwSFXsInGroup.Size = new System.Drawing.Size(335, 505);
            this.lvwSFXsInGroup.TabIndex = 0;
            this.lvwSFXsInGroup.UseCompatibleStateImageBehavior = false;
            this.lvwSFXsInGroup.View = System.Windows.Forms.View.Details;
            this.lvwSFXsInGroup.SelectedIndexChanged += new System.EventHandler(this.LvwSFXsInGroup_SelectedIndexChanged);
            // 
            // Col_SfxGroup_Hashcode
            // 
            this.Col_SfxGroup_Hashcode.Text = "SFX";
            this.Col_SfxGroup_Hashcode.Width = 200;
            // 
            // Col_SfxGroup_MaxVoices
            // 
            this.Col_SfxGroup_MaxVoices.Text = "Max";
            this.Col_SfxGroup_MaxVoices.Width = 50;
            // 
            // Col_SfxGroup_Steal
            // 
            this.Col_SfxGroup_Steal.Text = "Steal?";
            this.Col_SfxGroup_Steal.Width = 50;
            // 
            // Col_SfxGroup_Priority
            // 
            this.Col_SfxGroup_Priority.Text = "Priority";
            // 
            // grbAvailableSfx
            // 
            this.grbAvailableSfx.Controls.Add(this.lvwAvailable_SFXs);
            this.grbAvailableSfx.Controls.Add(this.nudMaxChannels_SFXs);
            this.grbAvailableSfx.Controls.Add(this.btnAddSFXsToGroup);
            this.grbAvailableSfx.Controls.Add(this.cboAvailableSFXs_Steal);
            this.grbAvailableSfx.Controls.Add(this.lblAvailableSFXs_Steal);
            this.grbAvailableSfx.Controls.Add(this.lblMaxVoices_SFX);
            this.grbAvailableSfx.Controls.Add(this.lblTotalSFXs);
            this.grbAvailableSfx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbAvailableSfx.Location = new System.Drawing.Point(0, 0);
            this.grbAvailableSfx.Name = "grbAvailableSfx";
            this.grbAvailableSfx.Size = new System.Drawing.Size(349, 586);
            this.grbAvailableSfx.TabIndex = 19;
            this.grbAvailableSfx.TabStop = false;
            this.grbAvailableSfx.Text = "Available SFXs";
            // 
            // lvwAvailable_SFXs
            // 
            this.lvwAvailable_SFXs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwAvailable_SFXs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_AvailableSFXs_HashCode,
            this.Col_AvailableSFXs_MaxVoices,
            this.Col_AvailableSFXs_Steal});
            this.lvwAvailable_SFXs.FullRowSelect = true;
            this.lvwAvailable_SFXs.GridLines = true;
            this.lvwAvailable_SFXs.HideSelection = false;
            this.lvwAvailable_SFXs.Location = new System.Drawing.Point(6, 19);
            this.lvwAvailable_SFXs.Name = "lvwAvailable_SFXs";
            this.lvwAvailable_SFXs.Size = new System.Drawing.Size(337, 505);
            this.lvwAvailable_SFXs.TabIndex = 7;
            this.lvwAvailable_SFXs.UseCompatibleStateImageBehavior = false;
            this.lvwAvailable_SFXs.View = System.Windows.Forms.View.Details;
            this.lvwAvailable_SFXs.SelectedIndexChanged += new System.EventHandler(this.LvwAvailable_SFXs_SelectedIndexChanged);
            // 
            // Col_AvailableSFXs_HashCode
            // 
            this.Col_AvailableSFXs_HashCode.Text = "SFX";
            this.Col_AvailableSFXs_HashCode.Width = 200;
            // 
            // Col_AvailableSFXs_MaxVoices
            // 
            this.Col_AvailableSFXs_MaxVoices.Text = "Max";
            this.Col_AvailableSFXs_MaxVoices.Width = 50;
            // 
            // Col_AvailableSFXs_Steal
            // 
            this.Col_AvailableSFXs_Steal.Text = "Steal?";
            this.Col_AvailableSFXs_Steal.Width = 50;
            // 
            // nudMaxChannels_SFXs
            // 
            this.nudMaxChannels_SFXs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaxChannels_SFXs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMaxChannels_SFXs.Location = new System.Drawing.Point(165, 530);
            this.nudMaxChannels_SFXs.Name = "nudMaxChannels_SFXs";
            this.nudMaxChannels_SFXs.Size = new System.Drawing.Size(53, 20);
            this.nudMaxChannels_SFXs.TabIndex = 3;
            this.nudMaxChannels_SFXs.Click += new System.EventHandler(this.NudMaxChannels_SFXs_Click);
            // 
            // btnAddSFXsToGroup
            // 
            this.btnAddSFXsToGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSFXsToGroup.Location = new System.Drawing.Point(6, 557);
            this.btnAddSFXsToGroup.Name = "btnAddSFXsToGroup";
            this.btnAddSFXsToGroup.Size = new System.Drawing.Size(337, 23);
            this.btnAddSFXsToGroup.TabIndex = 6;
            this.btnAddSFXsToGroup.Text = "<<< Add SFXs to Group";
            this.btnAddSFXsToGroup.UseVisualStyleBackColor = true;
            this.btnAddSFXsToGroup.Click += new System.EventHandler(this.BtnAddSFXsToGroup_Click);
            // 
            // cboAvailableSFXs_Steal
            // 
            this.cboAvailableSFXs_Steal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAvailableSFXs_Steal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAvailableSFXs_Steal.FormattingEnabled = true;
            this.cboAvailableSFXs_Steal.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboAvailableSFXs_Steal.Location = new System.Drawing.Point(283, 530);
            this.cboAvailableSFXs_Steal.Name = "cboAvailableSFXs_Steal";
            this.cboAvailableSFXs_Steal.Size = new System.Drawing.Size(60, 21);
            this.cboAvailableSFXs_Steal.TabIndex = 5;
            this.cboAvailableSFXs_Steal.SelectionChangeCommitted += new System.EventHandler(this.CboAvailableSFXs_Steal_SelectionChangeCommitted);
            // 
            // lblAvailableSFXs_Steal
            // 
            this.lblAvailableSFXs_Steal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvailableSFXs_Steal.AutoSize = true;
            this.lblAvailableSFXs_Steal.Location = new System.Drawing.Point(237, 533);
            this.lblAvailableSFXs_Steal.Name = "lblAvailableSFXs_Steal";
            this.lblAvailableSFXs_Steal.Size = new System.Drawing.Size(40, 13);
            this.lblAvailableSFXs_Steal.TabIndex = 4;
            this.lblAvailableSFXs_Steal.Text = "Steal?:";
            // 
            // lblMaxVoices_SFX
            // 
            this.lblMaxVoices_SFX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxVoices_SFX.AutoSize = true;
            this.lblMaxVoices_SFX.Location = new System.Drawing.Point(129, 533);
            this.lblMaxVoices_SFX.Name = "lblMaxVoices_SFX";
            this.lblMaxVoices_SFX.Size = new System.Drawing.Size(30, 13);
            this.lblMaxVoices_SFX.TabIndex = 2;
            this.lblMaxVoices_SFX.Text = "Max:";
            // 
            // lblTotalSFXs
            // 
            this.lblTotalSFXs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalSFXs.AutoSize = true;
            this.lblTotalSFXs.Location = new System.Drawing.Point(6, 533);
            this.lblTotalSFXs.Name = "lblTotalSFXs";
            this.lblTotalSFXs.Size = new System.Drawing.Size(43, 13);
            this.lblTotalSFXs.TabIndex = 1;
            this.lblTotalSFXs.Text = "Total: 0";
            // 
            // txtBootupTime
            // 
            this.txtBootupTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBootupTime.Location = new System.Drawing.Point(12, 611);
            this.txtBootupTime.Name = "txtBootupTime";
            this.txtBootupTime.Size = new System.Drawing.Size(171, 20);
            this.txtBootupTime.TabIndex = 23;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(936, 604);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 33);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // grbGroups
            // 
            this.grbGroups.Controls.Add(this.nudMaxVoices);
            this.grbGroups.Controls.Add(this.nudPriority);
            this.grbGroups.Controls.Add(this.lblPriority);
            this.grbGroups.Controls.Add(this.lblMaxVoices);
            this.grbGroups.Controls.Add(this.chkDistanceWhenTesting);
            this.grbGroups.Controls.Add(this.btnDelete);
            this.grbGroups.Controls.Add(this.grbAction);
            this.grbGroups.Controls.Add(this.lblGroupsCount);
            this.grbGroups.Controls.Add(this.lvwGroups);
            this.grbGroups.Controls.Add(this.btnRename);
            this.grbGroups.Controls.Add(this.btnNew);
            this.grbGroups.Controls.Add(this.lstAvailableGroups);
            this.grbGroups.Location = new System.Drawing.Point(12, 12);
            this.grbGroups.Name = "grbGroups";
            this.grbGroups.Size = new System.Drawing.Size(313, 586);
            this.grbGroups.TabIndex = 21;
            this.grbGroups.TabStop = false;
            this.grbGroups.Text = "Groups";
            // 
            // nudMaxVoices
            // 
            this.nudMaxVoices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaxVoices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMaxVoices.Location = new System.Drawing.Point(221, 507);
            this.nudMaxVoices.Name = "nudMaxVoices";
            this.nudMaxVoices.Size = new System.Drawing.Size(86, 20);
            this.nudMaxVoices.TabIndex = 9;
            this.nudMaxVoices.Click += new System.EventHandler(this.NudMaxVoices_Click);
            // 
            // nudPriority
            // 
            this.nudPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPriority.Location = new System.Drawing.Point(221, 533);
            this.nudPriority.Name = "nudPriority";
            this.nudPriority.Size = new System.Drawing.Size(86, 20);
            this.nudPriority.TabIndex = 11;
            this.nudPriority.Click += new System.EventHandler(this.NudPriority_Click);
            // 
            // lblPriority
            // 
            this.lblPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(150, 535);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(41, 13);
            this.lblPriority.TabIndex = 10;
            this.lblPriority.Text = "Priority:";
            // 
            // lblMaxVoices
            // 
            this.lblMaxVoices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxVoices.AutoSize = true;
            this.lblMaxVoices.Location = new System.Drawing.Point(150, 509);
            this.lblMaxVoices.Name = "lblMaxVoices";
            this.lblMaxVoices.Size = new System.Drawing.Size(65, 13);
            this.lblMaxVoices.TabIndex = 8;
            this.lblMaxVoices.Text = "Max Voices:";
            // 
            // chkDistanceWhenTesting
            // 
            this.chkDistanceWhenTesting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDistanceWhenTesting.AutoSize = true;
            this.chkDistanceWhenTesting.Checked = true;
            this.chkDistanceWhenTesting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDistanceWhenTesting.Location = new System.Drawing.Point(6, 563);
            this.chkDistanceWhenTesting.Name = "chkDistanceWhenTesting";
            this.chkDistanceWhenTesting.Size = new System.Drawing.Size(187, 17);
            this.chkDistanceWhenTesting.TabIndex = 7;
            this.chkDistanceWhenTesting.Text = "Use Distance when testing Priority";
            this.chkDistanceWhenTesting.UseVisualStyleBackColor = true;
            this.chkDistanceWhenTesting.CheckedChanged += new System.EventHandler(this.ChkDistanceWhenTesting_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(191, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // grbAction
            // 
            this.grbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbAction.Controls.Add(this.RadiobtnAction_Reject);
            this.grbAction.Controls.Add(this.RadiobtnAction_Steal);
            this.grbAction.Location = new System.Drawing.Point(6, 502);
            this.grbAction.Name = "grbAction";
            this.grbAction.Size = new System.Drawing.Size(70, 55);
            this.grbAction.TabIndex = 6;
            this.grbAction.TabStop = false;
            this.grbAction.Text = "Action";
            // 
            // RadiobtnAction_Reject
            // 
            this.RadiobtnAction_Reject.AutoSize = true;
            this.RadiobtnAction_Reject.Location = new System.Drawing.Point(6, 33);
            this.RadiobtnAction_Reject.Name = "RadiobtnAction_Reject";
            this.RadiobtnAction_Reject.Size = new System.Drawing.Size(56, 17);
            this.RadiobtnAction_Reject.TabIndex = 1;
            this.RadiobtnAction_Reject.Text = "Reject";
            this.RadiobtnAction_Reject.UseVisualStyleBackColor = true;
            this.RadiobtnAction_Reject.CheckedChanged += new System.EventHandler(this.RadiobtnAction_Reject_CheckedChanged);
            // 
            // RadiobtnAction_Steal
            // 
            this.RadiobtnAction_Steal.AutoSize = true;
            this.RadiobtnAction_Steal.Checked = true;
            this.RadiobtnAction_Steal.Location = new System.Drawing.Point(6, 13);
            this.RadiobtnAction_Steal.Name = "RadiobtnAction_Steal";
            this.RadiobtnAction_Steal.Size = new System.Drawing.Size(49, 17);
            this.RadiobtnAction_Steal.TabIndex = 0;
            this.RadiobtnAction_Steal.TabStop = true;
            this.RadiobtnAction_Steal.Text = "Steal";
            this.RadiobtnAction_Steal.UseVisualStyleBackColor = true;
            this.RadiobtnAction_Steal.CheckedChanged += new System.EventHandler(this.RadiobtnAction_Steal_CheckedChanged);
            // 
            // lblGroupsCount
            // 
            this.lblGroupsCount.AutoSize = true;
            this.lblGroupsCount.Location = new System.Drawing.Point(6, 457);
            this.lblGroupsCount.Name = "lblGroupsCount";
            this.lblGroupsCount.Size = new System.Drawing.Size(43, 13);
            this.lblGroupsCount.TabIndex = 5;
            this.lblGroupsCount.Text = "Total: 0";
            // 
            // lvwGroups
            // 
            this.lvwGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Label,
            this.Col_Max,
            this.Col_Priority});
            this.lvwGroups.FullRowSelect = true;
            this.lvwGroups.GridLines = true;
            this.lvwGroups.HideSelection = false;
            this.lvwGroups.Location = new System.Drawing.Point(6, 48);
            this.lvwGroups.Name = "lvwGroups";
            this.lvwGroups.Size = new System.Drawing.Size(301, 240);
            this.lvwGroups.TabIndex = 3;
            this.lvwGroups.UseCompatibleStateImageBehavior = false;
            this.lvwGroups.View = System.Windows.Forms.View.Details;
            // 
            // Col_Label
            // 
            this.Col_Label.Text = "Label";
            this.Col_Label.Width = 150;
            // 
            // Col_Max
            // 
            this.Col_Max.Text = "Max";
            this.Col_Max.Width = 50;
            // 
            // Col_Priority
            // 
            this.Col_Priority.Text = "Priority";
            this.Col_Priority.Width = 50;
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(100, 19);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(80, 23);
            this.btnRename.TabIndex = 1;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.BtnRename_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(6, 19);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // lstAvailableGroups
            // 
            this.lstAvailableGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAvailableGroups.FormattingEnabled = true;
            this.lstAvailableGroups.Location = new System.Drawing.Point(6, 294);
            this.lstAvailableGroups.Name = "lstAvailableGroups";
            this.lstAvailableGroups.Size = new System.Drawing.Size(301, 160);
            this.lstAvailableGroups.Sorted = true;
            this.lstAvailableGroups.TabIndex = 4;
            this.lstAvailableGroups.SelectedIndexChanged += new System.EventHandler(this.LstAvailableGroups_SelectedIndexChanged);
            // 
            // GroupingForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 649);
            this.Controls.Add(this.grbGroups);
            this.Controls.Add(this.SplitContainer1);
            this.Controls.Add(this.txtBootupTime);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup SFX Groups";
            this.Load += new System.EventHandler(this.GroupingForm_Load);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.grbSfxInGroup.ResumeLayout(false);
            this.grbSfxInGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxChannels_Group)).EndInit();
            this.grbAvailableSfx.ResumeLayout(false);
            this.grbAvailableSfx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxChannels_SFXs)).EndInit();
            this.grbGroups.ResumeLayout(false);
            this.grbGroups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxVoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).EndInit();
            this.grbAction.ResumeLayout(false);
            this.grbAction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.GroupBox grbSfxInGroup;
        protected internal System.Windows.Forms.NumericUpDown nudMaxChannels_Group;
        internal System.Windows.Forms.Button btnRemoveSFXsFromGroup;
        internal System.Windows.Forms.ComboBox cboSFXsInGroup_Steal;
        internal System.Windows.Forms.Label lblSFXsInGroup_Steal;
        internal System.Windows.Forms.Label lblMaxVoices_Group;
        internal System.Windows.Forms.Label lblSFXsInGroup_Count;
        internal Panels.ListView_ColumnSortingClick lvwSFXsInGroup;
        internal System.Windows.Forms.ColumnHeader Col_SfxGroup_Hashcode;
        internal System.Windows.Forms.ColumnHeader Col_SfxGroup_MaxVoices;
        internal System.Windows.Forms.ColumnHeader Col_SfxGroup_Steal;
        internal System.Windows.Forms.GroupBox grbAvailableSfx;
        internal Panels.ListView_ColumnSortingClick lvwAvailable_SFXs;
        internal System.Windows.Forms.ColumnHeader Col_AvailableSFXs_HashCode;
        internal System.Windows.Forms.ColumnHeader Col_AvailableSFXs_MaxVoices;
        internal System.Windows.Forms.ColumnHeader Col_AvailableSFXs_Steal;
        protected internal System.Windows.Forms.NumericUpDown nudMaxChannels_SFXs;
        internal System.Windows.Forms.Button btnAddSFXsToGroup;
        internal System.Windows.Forms.ComboBox cboAvailableSFXs_Steal;
        internal System.Windows.Forms.Label lblAvailableSFXs_Steal;
        internal System.Windows.Forms.Label lblMaxVoices_SFX;
        internal System.Windows.Forms.Label lblTotalSFXs;
        internal System.Windows.Forms.TextBox txtBootupTime;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.GroupBox grbGroups;
        protected internal System.Windows.Forms.NumericUpDown nudMaxVoices;
        protected internal System.Windows.Forms.NumericUpDown nudPriority;
        internal System.Windows.Forms.Label lblPriority;
        internal System.Windows.Forms.Label lblMaxVoices;
        internal System.Windows.Forms.CheckBox chkDistanceWhenTesting;
        internal System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.GroupBox grbAction;
        internal System.Windows.Forms.RadioButton RadiobtnAction_Reject;
        internal System.Windows.Forms.RadioButton RadiobtnAction_Steal;
        internal System.Windows.Forms.Label lblGroupsCount;
        internal Panels.ListView_ColumnSortingClick lvwGroups;
        internal System.Windows.Forms.ColumnHeader Col_Label;
        internal System.Windows.Forms.ColumnHeader Col_Max;
        internal System.Windows.Forms.ColumnHeader Col_Priority;
        internal System.Windows.Forms.Button btnRename;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.ListBox lstAvailableGroups;
        private System.Windows.Forms.ColumnHeader Col_SfxGroup_Priority;
    }
}
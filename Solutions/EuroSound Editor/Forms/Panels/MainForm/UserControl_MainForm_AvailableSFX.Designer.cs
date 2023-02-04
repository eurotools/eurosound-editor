
namespace sb_editor.Panels
{
    partial class UserControl_MainForm_AvailableSFX
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_MainForm_AvailableSFX));
            this.grbAvailableSFXs = new System.Windows.Forms.GroupBox();
            this.chkIconView = new System.Windows.Forms.CheckBox();
            this.UserControl_RefineSFX = new sb_editor.Panels.UserControl_MainForm_RefineSFXList();
            this.pnlListView = new System.Windows.Forms.Panel();
            this.lblSFXsTutorial = new System.Windows.Forms.Label();
            this.ContextMenu_ListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddToDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewMultiple = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMultiEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.lstTempSorted = new System.Windows.Forms.ListBox();
            this.lstAvailableSFXs = new MultiSelListBox();
            this.DataGrid_SFXs = new System.Windows.Forms.DataGridView();
            this.Col_Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.Col_HashCodeLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotal_SFXs = new System.Windows.Forms.Label();
            this.btnAddSFXs = new System.Windows.Forms.Button();
            this.lvwImageList = new System.Windows.Forms.ImageList(this.components);
            this.ToolTip_Controls = new System.Windows.Forms.ToolTip(this.components);
            this.grbAvailableSFXs.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.ContextMenu_ListView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_SFXs)).BeginInit();
            this.SuspendLayout();
            // 
            // grbAvailableSFXs
            // 
            this.grbAvailableSFXs.Controls.Add(this.chkIconView);
            this.grbAvailableSFXs.Controls.Add(this.UserControl_RefineSFX);
            this.grbAvailableSFXs.Controls.Add(this.pnlListView);
            this.grbAvailableSFXs.Controls.Add(this.lblTotal_SFXs);
            this.grbAvailableSFXs.Controls.Add(this.btnAddSFXs);
            this.grbAvailableSFXs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbAvailableSFXs.Location = new System.Drawing.Point(0, 0);
            this.grbAvailableSFXs.Name = "grbAvailableSFXs";
            this.grbAvailableSFXs.Size = new System.Drawing.Size(304, 533);
            this.grbAvailableSFXs.TabIndex = 2;
            this.grbAvailableSFXs.TabStop = false;
            this.grbAvailableSFXs.Text = "Available SFXs";
            // 
            // chkIconView
            // 
            this.chkIconView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIconView.AutoSize = true;
            this.chkIconView.Location = new System.Drawing.Point(225, 430);
            this.chkIconView.Name = "chkIconView";
            this.chkIconView.Size = new System.Drawing.Size(73, 17);
            this.chkIconView.TabIndex = 5;
            this.chkIconView.Text = "Icon View";
            this.chkIconView.UseVisualStyleBackColor = true;
            this.chkIconView.CheckedChanged += new System.EventHandler(this.ChkIconView_CheckedChanged);
            // 
            // UserControl_RefineSFX
            // 
            this.UserControl_RefineSFX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserControl_RefineSFX.Location = new System.Drawing.Point(6, 453);
            this.UserControl_RefineSFX.Name = "UserControl_RefineSFX";
            this.UserControl_RefineSFX.Size = new System.Drawing.Size(292, 74);
            this.UserControl_RefineSFX.TabIndex = 4;
            // 
            // pnlListView
            // 
            this.pnlListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListView.Controls.Add(this.lblSFXsTutorial);
            this.pnlListView.Controls.Add(this.lstTempSorted);
            this.pnlListView.Controls.Add(this.lstAvailableSFXs);
            this.pnlListView.Controls.Add(this.DataGrid_SFXs);
            this.pnlListView.Location = new System.Drawing.Point(6, 48);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.Size = new System.Drawing.Size(292, 380);
            this.pnlListView.TabIndex = 3;
            // 
            // lblSFXsTutorial
            // 
            this.lblSFXsTutorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSFXsTutorial.BackColor = System.Drawing.SystemColors.Window;
            this.lblSFXsTutorial.ContextMenuStrip = this.ContextMenu_ListView;
            this.lblSFXsTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSFXsTutorial.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSFXsTutorial.Location = new System.Drawing.Point(4, 2);
            this.lblSFXsTutorial.Name = "lblSFXsTutorial";
            this.lblSFXsTutorial.Size = new System.Drawing.Size(286, 41);
            this.lblSFXsTutorial.TabIndex = 9;
            this.lblSFXsTutorial.Text = "Right-click here to create your first sound effect (SFX). ";
            // 
            // ContextMenu_ListView
            // 
            this.ContextMenu_ListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddToDB,
            this.mnuPlay,
            this.mnuStop,
            this.mnuProperties,
            this.mnuEdit,
            this.mnuNew,
            this.mnuCopy,
            this.mnuDelete,
            this.mnuRename,
            this.mnuNewMultiple,
            this.mnuMultiEditor});
            this.ContextMenu_ListView.Name = "ContextMenu_ListView";
            this.ContextMenu_ListView.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ContextMenu_ListView.Size = new System.Drawing.Size(169, 246);
            // 
            // mnuAddToDB
            // 
            this.mnuAddToDB.Name = "mnuAddToDB";
            this.mnuAddToDB.Size = new System.Drawing.Size(168, 22);
            this.mnuAddToDB.Text = "Add To DB";
            this.mnuAddToDB.Click += new System.EventHandler(this.MnuAddToDB_Click);
            // 
            // mnuPlay
            // 
            this.mnuPlay.Enabled = false;
            this.mnuPlay.Name = "mnuPlay";
            this.mnuPlay.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.mnuPlay.Size = new System.Drawing.Size(168, 22);
            this.mnuPlay.Text = "Play";
            // 
            // mnuStop
            // 
            this.mnuStop.Enabled = false;
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Escape)));
            this.mnuStop.Size = new System.Drawing.Size(168, 22);
            this.mnuStop.Text = "Stop";
            // 
            // mnuProperties
            // 
            this.mnuProperties.Name = "mnuProperties";
            this.mnuProperties.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuProperties.Size = new System.Drawing.Size(168, 22);
            this.mnuProperties.Text = "Properties";
            this.mnuProperties.Click += new System.EventHandler(this.MnuProperties_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Back)));
            this.mnuEdit.Size = new System.Drawing.Size(168, 22);
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Click += new System.EventHandler(this.MnuEdit_Click);
            // 
            // mnuNew
            // 
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNew.Size = new System.Drawing.Size(168, 22);
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.MnuNew_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuCopy.Size = new System.Drawing.Size(168, 22);
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.MnuCopy_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuDelete.Size = new System.Drawing.Size(168, 22);
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.MnuDelete_Click);
            // 
            // mnuRename
            // 
            this.mnuRename.Name = "mnuRename";
            this.mnuRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuRename.Size = new System.Drawing.Size(168, 22);
            this.mnuRename.Text = "Rename";
            this.mnuRename.Click += new System.EventHandler(this.MnuRename_Click);
            // 
            // mnuNewMultiple
            // 
            this.mnuNewMultiple.Name = "mnuNewMultiple";
            this.mnuNewMultiple.Size = new System.Drawing.Size(168, 22);
            this.mnuNewMultiple.Text = "New Multiple";
            this.mnuNewMultiple.Click += new System.EventHandler(this.MnuNewMultiple_Click);
            // 
            // mnuMultiEditor
            // 
            this.mnuMultiEditor.Name = "mnuMultiEditor";
            this.mnuMultiEditor.Size = new System.Drawing.Size(168, 22);
            this.mnuMultiEditor.Text = "Multi Editor";
            this.mnuMultiEditor.Click += new System.EventHandler(this.MnuMultiEditor_Click);
            // 
            // lstTempSorted
            // 
            this.lstTempSorted.DisplayMember = "Text";
            this.lstTempSorted.FormattingEnabled = true;
            this.lstTempSorted.Location = new System.Drawing.Point(53, 283);
            this.lstTempSorted.Name = "lstTempSorted";
            this.lstTempSorted.Size = new System.Drawing.Size(185, 69);
            this.lstTempSorted.Sorted = true;
            this.lstTempSorted.TabIndex = 6;
            this.lstTempSorted.ValueMember = "ItemData";
            this.lstTempSorted.Visible = false;
            // 
            // lstAvailableSFXs
            // 
            this.lstAvailableSFXs.AllowDrop = true;
            this.lstAvailableSFXs.ContextMenuStrip = this.ContextMenu_ListView;
            this.lstAvailableSFXs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAvailableSFXs.DragDropEffectVal = System.Windows.Forms.DragDropEffects.Copy;
            this.lstAvailableSFXs.FormattingEnabled = true;
            this.lstAvailableSFXs.HorizontalScrollbar = true;
            this.lstAvailableSFXs.Location = new System.Drawing.Point(0, 0);
            this.lstAvailableSFXs.Name = "lstAvailableSFXs";
            this.lstAvailableSFXs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAvailableSFXs.Size = new System.Drawing.Size(292, 380);
            this.lstAvailableSFXs.TabIndex = 0;
            this.ToolTip_Controls.SetToolTip(this.lstAvailableSFXs, "All Available SFXs");
            this.lstAvailableSFXs.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstAvailableSFXs_DragDrop);
            this.lstAvailableSFXs.DragOver += new System.Windows.Forms.DragEventHandler(this.LstAvailableSFXs_DragOver);
            this.lstAvailableSFXs.DoubleClick += new System.EventHandler(this.LstAvailableSFXs_DoubleClick);
            // 
            // DataGrid_SFXs
            // 
            this.DataGrid_SFXs.AllowDrop = true;
            this.DataGrid_SFXs.AllowUserToAddRows = false;
            this.DataGrid_SFXs.AllowUserToDeleteRows = false;
            this.DataGrid_SFXs.AllowUserToResizeColumns = false;
            this.DataGrid_SFXs.AllowUserToResizeRows = false;
            this.DataGrid_SFXs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid_SFXs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Image,
            this.Col_HashCodeLabel});
            this.DataGrid_SFXs.ContextMenuStrip = this.ContextMenu_ListView;
            this.DataGrid_SFXs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid_SFXs.Location = new System.Drawing.Point(0, 0);
            this.DataGrid_SFXs.Name = "DataGrid_SFXs";
            this.DataGrid_SFXs.ReadOnly = true;
            this.DataGrid_SFXs.RowHeadersVisible = false;
            this.DataGrid_SFXs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid_SFXs.Size = new System.Drawing.Size(292, 380);
            this.DataGrid_SFXs.TabIndex = 8;
            this.DataGrid_SFXs.Visible = false;
            this.DataGrid_SFXs.DragDrop += new System.Windows.Forms.DragEventHandler(this.DataGrid_SFXs_DragDrop);
            this.DataGrid_SFXs.DragOver += new System.Windows.Forms.DragEventHandler(this.DataGrid_SFXs_DragOver);
            this.DataGrid_SFXs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataGrid_SFXs_MouseDoubleClick);
            this.DataGrid_SFXs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGrid_SFXs_MouseDown);
            // 
            // Col_Image
            // 
            this.Col_Image.HeaderText = "";
            this.Col_Image.Name = "Col_Image";
            this.Col_Image.ReadOnly = true;
            this.Col_Image.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col_Image.Width = 50;
            // 
            // Col_HashCodeLabel
            // 
            this.Col_HashCodeLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col_HashCodeLabel.HeaderText = "SFX HashCode Label";
            this.Col_HashCodeLabel.Name = "Col_HashCodeLabel";
            this.Col_HashCodeLabel.ReadOnly = true;
            this.Col_HashCodeLabel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col_HashCodeLabel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblTotal_SFXs
            // 
            this.lblTotal_SFXs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal_SFXs.AutoSize = true;
            this.lblTotal_SFXs.Location = new System.Drawing.Point(6, 431);
            this.lblTotal_SFXs.Name = "lblTotal_SFXs";
            this.lblTotal_SFXs.Size = new System.Drawing.Size(43, 13);
            this.lblTotal_SFXs.TabIndex = 2;
            this.lblTotal_SFXs.Text = "Total: 0";
            // 
            // btnAddSFXs
            // 
            this.btnAddSFXs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSFXs.Enabled = false;
            this.btnAddSFXs.Location = new System.Drawing.Point(6, 19);
            this.btnAddSFXs.Name = "btnAddSFXs";
            this.btnAddSFXs.Size = new System.Drawing.Size(292, 23);
            this.btnAddSFXs.TabIndex = 0;
            this.btnAddSFXs.Text = "<<< Add SFXs";
            this.ToolTip_Controls.SetToolTip(this.btnAddSFXs, "Add Selected SFX(s) To Selected DataBase");
            this.btnAddSFXs.UseVisualStyleBackColor = true;
            this.btnAddSFXs.Click += new System.EventHandler(this.BtnAddSFXs_Click);
            // 
            // lvwImageList
            // 
            this.lvwImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lvwImageList.ImageStream")));
            this.lvwImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.lvwImageList.Images.SetKeyName(0, "Normal.png");
            this.lvwImageList.Images.SetKeyName(1, "SubSFX.png");
            this.lvwImageList.Images.SetKeyName(2, "SubSFX2.png");
            // 
            // UserControl_MainForm_AvailableSFX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbAvailableSFXs);
            this.Name = "UserControl_MainForm_AvailableSFX";
            this.Size = new System.Drawing.Size(304, 533);
            this.Load += new System.EventHandler(this.UserControl_MainForm_AvailableSFX_Load);
            this.grbAvailableSFXs.ResumeLayout(false);
            this.grbAvailableSFXs.PerformLayout();
            this.pnlListView.ResumeLayout(false);
            this.ContextMenu_ListView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_SFXs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbAvailableSFXs;
        private System.Windows.Forms.Panel pnlListView;
        private System.Windows.Forms.ToolTip ToolTip_Controls;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_ListView;
        private System.Windows.Forms.ToolStripMenuItem mnuAddToDB;
        private System.Windows.Forms.ToolStripMenuItem mnuPlay;
        private System.Windows.Forms.ToolStripMenuItem mnuStop;
        private System.Windows.Forms.ToolStripMenuItem mnuProperties;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
        private System.Windows.Forms.ToolStripMenuItem mnuNewMultiple;
        private System.Windows.Forms.ToolStripMenuItem mnuMultiEditor;
        protected internal MultiSelListBox lstAvailableSFXs;
        protected internal System.Windows.Forms.Label lblTotal_SFXs;
        protected internal System.Windows.Forms.Button btnAddSFXs;
        protected internal UserControl_MainForm_RefineSFXList UserControl_RefineSFX;
        private System.Windows.Forms.ImageList lvwImageList;
        private System.Windows.Forms.ListBox lstTempSorted;
        protected internal System.Windows.Forms.CheckBox chkIconView;
        private System.Windows.Forms.DataGridViewImageColumn Col_Image;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_HashCodeLabel;
        protected internal System.Windows.Forms.DataGridView DataGrid_SFXs;
        private System.Windows.Forms.Label lblSFXsTutorial;
    }
}

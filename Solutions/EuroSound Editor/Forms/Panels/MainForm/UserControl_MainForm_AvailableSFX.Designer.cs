
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
            this.lstTempSorted = new System.Windows.Forms.ListBox();
            this.lstAvailableSFXs = new MultiSelListBox();
            this.ContextMenu_ListView = new System.Windows.Forms.ContextMenu();
            this.mnuAddToDB = new System.Windows.Forms.MenuItem();
            this.mnuPlay = new System.Windows.Forms.MenuItem();
            this.mnuStop = new System.Windows.Forms.MenuItem();
            this.mnuProperties = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuNew = new System.Windows.Forms.MenuItem();
            this.mnuCopy = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.mnuRename = new System.Windows.Forms.MenuItem();
            this.mnuNewMultiple = new System.Windows.Forms.MenuItem();
            this.mnuMultiEditor = new System.Windows.Forms.MenuItem();
            this.DataGrid_SFXs = new System.Windows.Forms.DataGridView();
            this.Col_Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.Col_HashCodeLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotal_SFXs = new System.Windows.Forms.Label();
            this.btnAddSFXs = new System.Windows.Forms.Button();
            this.lvwImageList = new System.Windows.Forms.ImageList(this.components);
            this.ToolTip_Controls = new System.Windows.Forms.ToolTip(this.components);
            this.grbAvailableSFXs.SuspendLayout();
            this.pnlListView.SuspendLayout();
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
            this.grbAvailableSFXs.Size = new System.Drawing.Size(304, 575);
            this.grbAvailableSFXs.TabIndex = 2;
            this.grbAvailableSFXs.TabStop = false;
            this.grbAvailableSFXs.Text = "Available SFXs";
            // 
            // chkIconView
            // 
            this.chkIconView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIconView.AutoSize = true;
            this.chkIconView.Location = new System.Drawing.Point(225, 472);
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
            this.UserControl_RefineSFX.Location = new System.Drawing.Point(6, 495);
            this.UserControl_RefineSFX.Name = "UserControl_RefineSFX";
            this.UserControl_RefineSFX.Size = new System.Drawing.Size(292, 74);
            this.UserControl_RefineSFX.TabIndex = 4;
            // 
            // pnlListView
            // 
            this.pnlListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListView.Controls.Add(this.lstTempSorted);
            this.pnlListView.Controls.Add(this.lstAvailableSFXs);
            this.pnlListView.Controls.Add(this.DataGrid_SFXs);
            this.pnlListView.Location = new System.Drawing.Point(6, 48);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.Size = new System.Drawing.Size(292, 422);
            this.pnlListView.TabIndex = 3;
            // 
            // lstTempSorted
            // 
            this.lstTempSorted.DisplayMember = "Text";
            this.lstTempSorted.FormattingEnabled = true;
            this.lstTempSorted.Location = new System.Drawing.Point(53, 49);
            this.lstTempSorted.Name = "lstTempSorted";
            this.lstTempSorted.Size = new System.Drawing.Size(185, 303);
            this.lstTempSorted.Sorted = true;
            this.lstTempSorted.TabIndex = 6;
            this.lstTempSorted.ValueMember = "ItemData";
            this.lstTempSorted.Visible = false;
            // 
            // lstAvailableSFXs
            // 
            this.lstAvailableSFXs.AllowDrop = true;
            this.lstAvailableSFXs.ContextMenu = this.ContextMenu_ListView;
            this.lstAvailableSFXs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAvailableSFXs.DragDropEffectVal = System.Windows.Forms.DragDropEffects.Copy;
            this.lstAvailableSFXs.FormattingEnabled = true;
            this.lstAvailableSFXs.HorizontalScrollbar = true;
            this.lstAvailableSFXs.Location = new System.Drawing.Point(0, 0);
            this.lstAvailableSFXs.Name = "lstAvailableSFXs";
            this.lstAvailableSFXs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAvailableSFXs.Size = new System.Drawing.Size(292, 422);
            this.lstAvailableSFXs.TabIndex = 0;
            this.ToolTip_Controls.SetToolTip(this.lstAvailableSFXs, "All Available SFXs");
            this.lstAvailableSFXs.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstAvailableSFXs_DragDrop);
            this.lstAvailableSFXs.DragOver += new System.Windows.Forms.DragEventHandler(this.LstAvailableSFXs_DragOver);
            this.lstAvailableSFXs.DoubleClick += new System.EventHandler(this.LstAvailableSFXs_DoubleClick);
            // 
            // ContextMenu_ListView
            // 
            this.ContextMenu_ListView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
            // 
            // mnuAddToDB
            // 
            this.mnuAddToDB.Index = 0;
            this.mnuAddToDB.Shortcut = System.Windows.Forms.Shortcut.AltBksp;
            this.mnuAddToDB.Text = "Add To DB";
            this.mnuAddToDB.Click += new System.EventHandler(this.MnuAddToDB_Click);
            // 
            // mnuPlay
            // 
            this.mnuPlay.Index = 1;
            this.mnuPlay.Text = "Play";
            // 
            // mnuStop
            // 
            this.mnuStop.Index = 2;
            this.mnuStop.Text = "Stop";
            // 
            // mnuProperties
            // 
            this.mnuProperties.Index = 3;
            this.mnuProperties.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.mnuProperties.Text = "Properties";
            this.mnuProperties.Click += new System.EventHandler(this.MnuProperties_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 4;
            this.mnuEdit.Shortcut = System.Windows.Forms.Shortcut.AltBksp;
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Click += new System.EventHandler(this.MnuEdit_Click);
            // 
            // mnuNew
            // 
            this.mnuNew.Index = 5;
            this.mnuNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.MnuNew_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Index = 6;
            this.mnuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.MnuCopy_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 7;
            this.mnuDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.MnuDelete_Click);
            // 
            // mnuRename
            // 
            this.mnuRename.Index = 8;
            this.mnuRename.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.mnuRename.Text = "Rename";
            this.mnuRename.Click += new System.EventHandler(this.MnuRename_Click);
            // 
            // mnuNewMultiple
            // 
            this.mnuNewMultiple.Index = 9;
            this.mnuNewMultiple.Text = "New Multiple";
            this.mnuNewMultiple.Click += new System.EventHandler(this.MnuNewMultiple_Click);
            // 
            // mnuMultiEditor
            // 
            this.mnuMultiEditor.Index = 10;
            this.mnuMultiEditor.Text = "Multi Editor";
            this.mnuMultiEditor.Click += new System.EventHandler(this.MnuMultiEditor_Click);
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
            this.DataGrid_SFXs.ContextMenu = this.ContextMenu_ListView;
            this.DataGrid_SFXs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid_SFXs.Location = new System.Drawing.Point(0, 0);
            this.DataGrid_SFXs.Name = "DataGrid_SFXs";
            this.DataGrid_SFXs.ReadOnly = true;
            this.DataGrid_SFXs.RowHeadersVisible = false;
            this.DataGrid_SFXs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid_SFXs.Size = new System.Drawing.Size(292, 422);
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
            this.lblTotal_SFXs.Location = new System.Drawing.Point(6, 473);
            this.lblTotal_SFXs.Name = "lblTotal_SFXs";
            this.lblTotal_SFXs.Size = new System.Drawing.Size(43, 13);
            this.lblTotal_SFXs.TabIndex = 2;
            this.lblTotal_SFXs.Text = "Total: 0";
            // 
            // btnAddSFXs
            // 
            this.btnAddSFXs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.Size = new System.Drawing.Size(304, 575);
            this.Load += new System.EventHandler(this.UserControl_MainForm_AvailableSFX_Load);
            this.grbAvailableSFXs.ResumeLayout(false);
            this.grbAvailableSFXs.PerformLayout();
            this.pnlListView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_SFXs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbAvailableSFXs;
        private System.Windows.Forms.Panel pnlListView;
        private System.Windows.Forms.ToolTip ToolTip_Controls;
        private System.Windows.Forms.ContextMenu ContextMenu_ListView;
        private System.Windows.Forms.MenuItem mnuAddToDB;
        private System.Windows.Forms.MenuItem mnuPlay;
        private System.Windows.Forms.MenuItem mnuStop;
        private System.Windows.Forms.MenuItem mnuProperties;
        private System.Windows.Forms.MenuItem mnuEdit;
        private System.Windows.Forms.MenuItem mnuNew;
        private System.Windows.Forms.MenuItem mnuCopy;
        private System.Windows.Forms.MenuItem mnuDelete;
        private System.Windows.Forms.MenuItem mnuRename;
        private System.Windows.Forms.MenuItem mnuNewMultiple;
        private System.Windows.Forms.MenuItem mnuMultiEditor;
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
    }
}

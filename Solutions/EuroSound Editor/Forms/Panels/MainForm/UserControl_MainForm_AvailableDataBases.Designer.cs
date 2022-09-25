
namespace sb_editor.Panels
{
    partial class UserControl_MainForm_AvailableDataBases
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
            this.grbAvailableDataBases = new System.Windows.Forms.GroupBox();
            this.pnlListView = new System.Windows.Forms.Panel();
            this.lstDataBases = new System.Windows.Forms.ListBox();
            this.ContextMenu_DataBases = new System.Windows.Forms.ContextMenu();
            this.mnuAddDataBaseToSoundBank = new System.Windows.Forms.MenuItem();
            this.mnuNew = new System.Windows.Forms.MenuItem();
            this.mnuCopy = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.mnuRename = new System.Windows.Forms.MenuItem();
            this.mnuProperties = new System.Windows.Forms.MenuItem();
            this.lblDataBases_Count = new System.Windows.Forms.Label();
            this.btnAddDataBases = new System.Windows.Forms.Button();
            this.ToolTip_Controls = new System.Windows.Forms.ToolTip(this.components);
            this.grbAvailableDataBases.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbAvailableDataBases
            // 
            this.grbAvailableDataBases.Controls.Add(this.pnlListView);
            this.grbAvailableDataBases.Controls.Add(this.lblDataBases_Count);
            this.grbAvailableDataBases.Controls.Add(this.btnAddDataBases);
            this.grbAvailableDataBases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbAvailableDataBases.Location = new System.Drawing.Point(0, 0);
            this.grbAvailableDataBases.Name = "grbAvailableDataBases";
            this.grbAvailableDataBases.Size = new System.Drawing.Size(376, 595);
            this.grbAvailableDataBases.TabIndex = 0;
            this.grbAvailableDataBases.TabStop = false;
            this.grbAvailableDataBases.Text = "Available DataBases";
            // 
            // pnlListView
            // 
            this.pnlListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListView.Controls.Add(this.lstDataBases);
            this.pnlListView.Location = new System.Drawing.Point(6, 48);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.Size = new System.Drawing.Size(364, 522);
            this.pnlListView.TabIndex = 3;
            // 
            // lstDataBases
            // 
            this.lstDataBases.ContextMenu = this.ContextMenu_DataBases;
            this.lstDataBases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDataBases.FormattingEnabled = true;
            this.lstDataBases.HorizontalScrollbar = true;
            this.lstDataBases.Location = new System.Drawing.Point(0, 0);
            this.lstDataBases.Name = "lstDataBases";
            this.lstDataBases.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstDataBases.Size = new System.Drawing.Size(364, 522);
            this.lstDataBases.Sorted = true;
            this.lstDataBases.TabIndex = 1;
            this.ToolTip_Controls.SetToolTip(this.lstDataBases, "All Available DataBases");
            this.lstDataBases.SelectedIndexChanged += new System.EventHandler(this.LstDataBases_SelectedIndexChanged);
            this.lstDataBases.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstDataBases_MouseDoubleClick);
            // 
            // ContextMenu_DataBases
            // 
            this.ContextMenu_DataBases.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAddDataBaseToSoundBank,
            this.mnuNew,
            this.mnuCopy,
            this.mnuDelete,
            this.mnuRename,
            this.mnuProperties});
            // 
            // mnuAddDataBaseToSoundBank
            // 
            this.mnuAddDataBaseToSoundBank.Index = 0;
            this.mnuAddDataBaseToSoundBank.Shortcut = System.Windows.Forms.Shortcut.AltBksp;
            this.mnuAddDataBaseToSoundBank.Text = "Add DB to SB";
            this.mnuAddDataBaseToSoundBank.Click += new System.EventHandler(this.MnuAddDataBaseToSoundBank_Click);
            // 
            // mnuNew
            // 
            this.mnuNew.Index = 1;
            this.mnuNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.MnuNew_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Index = 2;
            this.mnuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.MnuCopy_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 3;
            this.mnuDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.MnuDelete_Click);
            // 
            // mnuRename
            // 
            this.mnuRename.Index = 4;
            this.mnuRename.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.mnuRename.Text = "Rename";
            this.mnuRename.Click += new System.EventHandler(this.MnuRename_Click);
            // 
            // mnuProperties
            // 
            this.mnuProperties.Index = 5;
            this.mnuProperties.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.mnuProperties.Text = "Properties";
            this.mnuProperties.Click += new System.EventHandler(this.MnuProperties_Click);
            // 
            // lblDataBases_Count
            // 
            this.lblDataBases_Count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataBases_Count.AutoSize = true;
            this.lblDataBases_Count.Location = new System.Drawing.Point(6, 573);
            this.lblDataBases_Count.Name = "lblDataBases_Count";
            this.lblDataBases_Count.Size = new System.Drawing.Size(43, 13);
            this.lblDataBases_Count.TabIndex = 2;
            this.lblDataBases_Count.Text = "Total: 0";
            // 
            // btnAddDataBases
            // 
            this.btnAddDataBases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDataBases.Location = new System.Drawing.Point(6, 19);
            this.btnAddDataBases.Name = "btnAddDataBases";
            this.btnAddDataBases.Size = new System.Drawing.Size(364, 23);
            this.btnAddDataBases.TabIndex = 0;
            this.btnAddDataBases.Text = "<<< Add DataBases";
            this.ToolTip_Controls.SetToolTip(this.btnAddDataBases, "Add Selected SFX(s) To Selected DataBase");
            this.btnAddDataBases.UseVisualStyleBackColor = true;
            this.btnAddDataBases.Click += new System.EventHandler(this.BtnAddDataBases_Click);
            // 
            // UserControl_MainForm_AvailableDataBases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbAvailableDataBases);
            this.Name = "UserControl_MainForm_AvailableDataBases";
            this.Size = new System.Drawing.Size(376, 595);
            this.grbAvailableDataBases.ResumeLayout(false);
            this.grbAvailableDataBases.PerformLayout();
            this.pnlListView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbAvailableDataBases;
        private System.Windows.Forms.Panel pnlListView;
        private System.Windows.Forms.ToolTip ToolTip_Controls;
        private System.Windows.Forms.ContextMenu ContextMenu_DataBases;
        private System.Windows.Forms.MenuItem mnuAddDataBaseToSoundBank;
        private System.Windows.Forms.MenuItem mnuNew;
        private System.Windows.Forms.MenuItem mnuCopy;
        private System.Windows.Forms.MenuItem mnuDelete;
        private System.Windows.Forms.MenuItem mnuRename;
        private System.Windows.Forms.MenuItem mnuProperties;
        protected internal System.Windows.Forms.Button btnAddDataBases;
        protected internal System.Windows.Forms.ListBox lstDataBases;
        protected internal System.Windows.Forms.Label lblDataBases_Count;
    }
}


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
            this.lblDataBaseTutorial = new System.Windows.Forms.Label();
            this.ContextMenu_DataBases = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddDataBaseToSoundBank = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.lstDataBases = new System.Windows.Forms.ListBox();
            this.lblDataBases_Count = new System.Windows.Forms.Label();
            this.btnAddDataBases = new System.Windows.Forms.Button();
            this.ToolTip_Controls = new System.Windows.Forms.ToolTip(this.components);
            this.grbAvailableDataBases.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.ContextMenu_DataBases.SuspendLayout();
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
            this.pnlListView.Controls.Add(this.lblDataBaseTutorial);
            this.pnlListView.Controls.Add(this.lstDataBases);
            this.pnlListView.Location = new System.Drawing.Point(6, 48);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.Size = new System.Drawing.Size(364, 522);
            this.pnlListView.TabIndex = 3;
            // 
            // lblDataBaseTutorial
            // 
            this.lblDataBaseTutorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataBaseTutorial.BackColor = System.Drawing.SystemColors.Window;
            this.lblDataBaseTutorial.ContextMenuStrip = this.ContextMenu_DataBases;
            this.lblDataBaseTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataBaseTutorial.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblDataBaseTutorial.Location = new System.Drawing.Point(3, 3);
            this.lblDataBaseTutorial.Name = "lblDataBaseTutorial";
            this.lblDataBaseTutorial.Size = new System.Drawing.Size(358, 58);
            this.lblDataBaseTutorial.TabIndex = 2;
            this.lblDataBaseTutorial.Text = "Right-click here to create your first DataBase (DB); a DataBase is a logical grou" +
    "p of SFX, which you can include into various SoundBanks.";
            // 
            // ContextMenu_DataBases
            // 
            this.ContextMenu_DataBases.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddDataBaseToSoundBank,
            this.mnuNew,
            this.mnuCopy,
            this.mnuDelete,
            this.mnuRename,
            this.mnuProperties});
            this.ContextMenu_DataBases.Name = "ContextMenu_DataBases";
            this.ContextMenu_DataBases.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ContextMenu_DataBases.Size = new System.Drawing.Size(206, 136);
            // 
            // mnuAddDataBaseToSoundBank
            // 
            this.mnuAddDataBaseToSoundBank.Name = "mnuAddDataBaseToSoundBank";
            this.mnuAddDataBaseToSoundBank.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Back)));
            this.mnuAddDataBaseToSoundBank.Size = new System.Drawing.Size(205, 22);
            this.mnuAddDataBaseToSoundBank.Text = "Add DB to SB";
            this.mnuAddDataBaseToSoundBank.Click += new System.EventHandler(this.MnuAddDataBaseToSoundBank_Click);
            // 
            // mnuNew
            // 
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNew.Size = new System.Drawing.Size(205, 22);
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.MnuNew_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuCopy.Size = new System.Drawing.Size(205, 22);
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.MnuCopy_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuDelete.Size = new System.Drawing.Size(205, 22);
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.MnuDelete_Click);
            // 
            // mnuRename
            // 
            this.mnuRename.Name = "mnuRename";
            this.mnuRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuRename.Size = new System.Drawing.Size(205, 22);
            this.mnuRename.Text = "Rename";
            this.mnuRename.Click += new System.EventHandler(this.MnuRename_Click);
            // 
            // mnuProperties
            // 
            this.mnuProperties.Name = "mnuProperties";
            this.mnuProperties.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuProperties.Size = new System.Drawing.Size(205, 22);
            this.mnuProperties.Text = "Properties";
            this.mnuProperties.Click += new System.EventHandler(this.MnuProperties_Click);
            // 
            // lstDataBases
            // 
            this.lstDataBases.ContextMenuStrip = this.ContextMenu_DataBases;
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
            this.btnAddDataBases.Enabled = false;
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
            this.ContextMenu_DataBases.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbAvailableDataBases;
        private System.Windows.Forms.Panel pnlListView;
        private System.Windows.Forms.ToolTip ToolTip_Controls;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_DataBases;
        private System.Windows.Forms.ToolStripMenuItem mnuAddDataBaseToSoundBank;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
        private System.Windows.Forms.ToolStripMenuItem mnuProperties;
        protected internal System.Windows.Forms.Button btnAddDataBases;
        protected internal System.Windows.Forms.ListBox lstDataBases;
        protected internal System.Windows.Forms.Label lblDataBases_Count;
        private System.Windows.Forms.Label lblDataBaseTutorial;
    }
}


namespace EuroSound_Editor.Panels
{
    partial class UserControl_MainForm_SfxInDataBase
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
            this.lstSfxInDataBase = new MultiSelListBox();
            this.ContextMenu_ListBox = new System.Windows.Forms.ContextMenu();
            this.mnuRemoveSFX = new System.Windows.Forms.MenuItem();
            this.mnuPlay = new System.Windows.Forms.MenuItem();
            this.mnuStop = new System.Windows.Forms.MenuItem();
            this.mnuProperties = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuSelectSFX = new System.Windows.Forms.MenuItem();
            this.mnuMultiEditor = new System.Windows.Forms.MenuItem();
            this.lblSfxCount = new System.Windows.Forms.Label();
            this.btnRemoveSfx = new System.Windows.Forms.Button();
            this.ToolTip_Controls = new System.Windows.Forms.ToolTip(this.components);
            this.grbAvailableDataBases.SuspendLayout();
            this.pnlListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbAvailableDataBases
            // 
            this.grbAvailableDataBases.Controls.Add(this.pnlListView);
            this.grbAvailableDataBases.Controls.Add(this.lblSfxCount);
            this.grbAvailableDataBases.Controls.Add(this.btnRemoveSfx);
            this.grbAvailableDataBases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbAvailableDataBases.Location = new System.Drawing.Point(0, 0);
            this.grbAvailableDataBases.Name = "grbAvailableDataBases";
            this.grbAvailableDataBases.Size = new System.Drawing.Size(376, 595);
            this.grbAvailableDataBases.TabIndex = 1;
            this.grbAvailableDataBases.TabStop = false;
            this.grbAvailableDataBases.Text = "SFXs In DataBase";
            // 
            // pnlListView
            // 
            this.pnlListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListView.Controls.Add(this.lstSfxInDataBase);
            this.pnlListView.Location = new System.Drawing.Point(6, 48);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.Size = new System.Drawing.Size(364, 522);
            this.pnlListView.TabIndex = 3;
            // 
            // lstSfxInDataBase
            // 
            this.lstSfxInDataBase.AllowDrop = true;
            this.lstSfxInDataBase.ContextMenu = this.ContextMenu_ListBox;
            this.lstSfxInDataBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSfxInDataBase.DragDropEffectVal = System.Windows.Forms.DragDropEffects.Move;
            this.lstSfxInDataBase.FormattingEnabled = true;
            this.lstSfxInDataBase.HorizontalScrollbar = true;
            this.lstSfxInDataBase.Location = new System.Drawing.Point(0, 0);
            this.lstSfxInDataBase.Name = "lstSfxInDataBase";
            this.lstSfxInDataBase.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSfxInDataBase.Size = new System.Drawing.Size(364, 522);
            this.lstSfxInDataBase.Sorted = true;
            this.lstSfxInDataBase.TabIndex = 0;
            this.ToolTip_Controls.SetToolTip(this.lstSfxInDataBase, "All SFXs in the Selected DataBase");
            this.lstSfxInDataBase.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstSfxInDataBase_DragDrop);
            this.lstSfxInDataBase.DragOver += new System.Windows.Forms.DragEventHandler(this.LstSfxInDataBase_DragOver);
            this.lstSfxInDataBase.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstSfxInDataBase_MouseDoubleClick);
            // 
            // ContextMenu_ListBox
            // 
            this.ContextMenu_ListBox.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuRemoveSFX,
            this.mnuPlay,
            this.mnuStop,
            this.mnuProperties,
            this.mnuEdit,
            this.mnuSelectSFX,
            this.mnuMultiEditor});
            // 
            // mnuRemoveSFX
            // 
            this.mnuRemoveSFX.Index = 0;
            this.mnuRemoveSFX.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.mnuRemoveSFX.Text = "Remove SFX";
            this.mnuRemoveSFX.Click += new System.EventHandler(this.MnuRemoveSFX_Click);
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
            // mnuSelectSFX
            // 
            this.mnuSelectSFX.Index = 5;
            this.mnuSelectSFX.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.mnuSelectSFX.Text = "Select SFX";
            this.mnuSelectSFX.Click += new System.EventHandler(this.MnuSelectSFX_Click);
            // 
            // mnuMultiEditor
            // 
            this.mnuMultiEditor.Index = 6;
            this.mnuMultiEditor.Text = "Multi Editor";
            // 
            // lblSfxCount
            // 
            this.lblSfxCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSfxCount.AutoSize = true;
            this.lblSfxCount.Location = new System.Drawing.Point(6, 573);
            this.lblSfxCount.Name = "lblSfxCount";
            this.lblSfxCount.Size = new System.Drawing.Size(43, 13);
            this.lblSfxCount.TabIndex = 2;
            this.lblSfxCount.Text = "Total: 0";
            // 
            // btnRemoveSfx
            // 
            this.btnRemoveSfx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveSfx.Location = new System.Drawing.Point(6, 19);
            this.btnRemoveSfx.Name = "btnRemoveSfx";
            this.btnRemoveSfx.Size = new System.Drawing.Size(364, 23);
            this.btnRemoveSfx.TabIndex = 0;
            this.btnRemoveSfx.Text = "Remove SFXs >>>";
            this.ToolTip_Controls.SetToolTip(this.btnRemoveSfx, "Remove Selected SFX(s) From Selected DataBase");
            this.btnRemoveSfx.UseVisualStyleBackColor = true;
            this.btnRemoveSfx.Click += new System.EventHandler(this.BtnRemoveSfx_Click);
            // 
            // UserControl_MainForm_SfxInDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbAvailableDataBases);
            this.Name = "UserControl_MainForm_SfxInDataBase";
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
        private System.Windows.Forms.ContextMenu ContextMenu_ListBox;
        private System.Windows.Forms.MenuItem mnuRemoveSFX;
        private System.Windows.Forms.MenuItem mnuPlay;
        private System.Windows.Forms.MenuItem mnuStop;
        private System.Windows.Forms.MenuItem mnuProperties;
        private System.Windows.Forms.MenuItem mnuEdit;
        private System.Windows.Forms.MenuItem mnuSelectSFX;
        private System.Windows.Forms.MenuItem mnuMultiEditor;
        protected internal MultiSelListBox lstSfxInDataBase;
        protected internal System.Windows.Forms.Label lblSfxCount;
        protected internal System.Windows.Forms.Button btnRemoveSfx;
    }
}

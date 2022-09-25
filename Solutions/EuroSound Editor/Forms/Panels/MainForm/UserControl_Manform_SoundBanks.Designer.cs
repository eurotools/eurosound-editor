
namespace sb_editor.Panels
{
    partial class UserControl_Manform_SoundBanks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_Manform_SoundBanks));
            this.grbSoundBanks = new System.Windows.Forms.GroupBox();
            this.lblDataBases_Total = new System.Windows.Forms.Label();
            this.lblSoundBanksTotal = new System.Windows.Forms.Label();
            this.tvwSoundBanks = new System.Windows.Forms.TreeView();
            this.ContextMenu_TreeView = new System.Windows.Forms.ContextMenu();
            this.mnuNew_SoundBank = new System.Windows.Forms.MenuItem();
            this.mnuCopy_SoundBank = new System.Windows.Forms.MenuItem();
            this.mnuDelete_SoundBank = new System.Windows.Forms.MenuItem();
            this.mnuRename_SoundBank = new System.Windows.Forms.MenuItem();
            this.mnuProperties_SoundBank = new System.Windows.Forms.MenuItem();
            this.mnuMaxOutSize_SoundBank = new System.Windows.Forms.MenuItem();
            this.imlTreeView = new System.Windows.Forms.ImageList(this.components);
            this.grbSoundBanks.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSoundBanks
            // 
            this.grbSoundBanks.Controls.Add(this.lblDataBases_Total);
            this.grbSoundBanks.Controls.Add(this.lblSoundBanksTotal);
            this.grbSoundBanks.Controls.Add(this.tvwSoundBanks);
            this.grbSoundBanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbSoundBanks.Location = new System.Drawing.Point(0, 0);
            this.grbSoundBanks.Name = "grbSoundBanks";
            this.grbSoundBanks.Size = new System.Drawing.Size(294, 274);
            this.grbSoundBanks.TabIndex = 0;
            this.grbSoundBanks.TabStop = false;
            this.grbSoundBanks.Text = "Sound Banks";
            // 
            // lblDataBases_Total
            // 
            this.lblDataBases_Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataBases_Total.AutoSize = true;
            this.lblDataBases_Total.Location = new System.Drawing.Point(213, 244);
            this.lblDataBases_Total.Name = "lblDataBases_Total";
            this.lblDataBases_Total.Size = new System.Drawing.Size(61, 13);
            this.lblDataBases_Total.TabIndex = 2;
            this.lblDataBases_Total.Text = "DB Total: 0";
            // 
            // lblSoundBanksTotal
            // 
            this.lblSoundBanksTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSoundBanksTotal.AutoSize = true;
            this.lblSoundBanksTotal.Location = new System.Drawing.Point(6, 244);
            this.lblSoundBanksTotal.Name = "lblSoundBanksTotal";
            this.lblSoundBanksTotal.Size = new System.Drawing.Size(60, 13);
            this.lblSoundBanksTotal.TabIndex = 1;
            this.lblSoundBanksTotal.Text = "SB Total: 0";
            // 
            // tvwSoundBanks
            // 
            this.tvwSoundBanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwSoundBanks.ContextMenu = this.ContextMenu_TreeView;
            this.tvwSoundBanks.ImageIndex = 0;
            this.tvwSoundBanks.ImageList = this.imlTreeView;
            this.tvwSoundBanks.Indent = 39;
            this.tvwSoundBanks.Location = new System.Drawing.Point(6, 19);
            this.tvwSoundBanks.Name = "tvwSoundBanks";
            this.tvwSoundBanks.SelectedImageIndex = 0;
            this.tvwSoundBanks.ShowNodeToolTips = true;
            this.tvwSoundBanks.ShowPlusMinus = false;
            this.tvwSoundBanks.ShowRootLines = false;
            this.tvwSoundBanks.Size = new System.Drawing.Size(282, 222);
            this.tvwSoundBanks.TabIndex = 0;
            this.tvwSoundBanks.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TvwSoundBanks_BeforeCollapse);
            this.tvwSoundBanks.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TvwSoundBanks_BeforeExpand);
            this.tvwSoundBanks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvwSoundBanks_AfterSelect);
            this.tvwSoundBanks.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvwSoundBanks_NodeMouseClick);
            this.tvwSoundBanks.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvwSoundBanks_NodeMouseDoubleClick);
            // 
            // ContextMenu_TreeView
            // 
            this.ContextMenu_TreeView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNew_SoundBank,
            this.mnuCopy_SoundBank,
            this.mnuDelete_SoundBank,
            this.mnuRename_SoundBank,
            this.mnuProperties_SoundBank,
            this.mnuMaxOutSize_SoundBank});
            // 
            // mnuNew_SoundBank
            // 
            this.mnuNew_SoundBank.Index = 0;
            this.mnuNew_SoundBank.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuNew_SoundBank.Text = "New";
            this.mnuNew_SoundBank.Click += new System.EventHandler(this.MnuNew_SoundBank_Click);
            // 
            // mnuCopy_SoundBank
            // 
            this.mnuCopy_SoundBank.Index = 1;
            this.mnuCopy_SoundBank.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuCopy_SoundBank.Text = "Copy";
            this.mnuCopy_SoundBank.Click += new System.EventHandler(this.MnuCopy_SoundBank_Click);
            // 
            // mnuDelete_SoundBank
            // 
            this.mnuDelete_SoundBank.Index = 2;
            this.mnuDelete_SoundBank.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.mnuDelete_SoundBank.Text = "Delete";
            this.mnuDelete_SoundBank.Click += new System.EventHandler(this.MnuDelete_SoundBank_Click);
            // 
            // mnuRename_SoundBank
            // 
            this.mnuRename_SoundBank.Index = 3;
            this.mnuRename_SoundBank.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.mnuRename_SoundBank.Text = "Rename";
            this.mnuRename_SoundBank.Click += new System.EventHandler(this.MnuRename_SoundBank_Click);
            // 
            // mnuProperties_SoundBank
            // 
            this.mnuProperties_SoundBank.Index = 4;
            this.mnuProperties_SoundBank.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.mnuProperties_SoundBank.Text = "Properties";
            this.mnuProperties_SoundBank.Click += new System.EventHandler(this.MnuProperties_SoundBank_Click);
            // 
            // mnuMaxOutSize_SoundBank
            // 
            this.mnuMaxOutSize_SoundBank.Index = 5;
            this.mnuMaxOutSize_SoundBank.Text = "Max Output Size?";
            this.mnuMaxOutSize_SoundBank.Click += new System.EventHandler(this.MnuMaxOutSize_SoundBank_Click);
            // 
            // imlTreeView
            // 
            this.imlTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeView.ImageStream")));
            this.imlTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imlTreeView.Images.SetKeyName(0, "directory_closed-1.png");
            this.imlTreeView.Images.SetKeyName(1, "directory_open_cool-1.png");
            this.imlTreeView.Images.SetKeyName(2, "soundbank_Database.png");
            this.imlTreeView.Images.SetKeyName(3, "Music-note-blue-icon.png");
            // 
            // UserControl_Manform_SoundBanks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbSoundBanks);
            this.Name = "UserControl_Manform_SoundBanks";
            this.Size = new System.Drawing.Size(294, 274);
            this.grbSoundBanks.ResumeLayout(false);
            this.grbSoundBanks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSoundBanks;
        private System.Windows.Forms.Label lblDataBases_Total;
        private System.Windows.Forms.Label lblSoundBanksTotal;
        private System.Windows.Forms.ImageList imlTreeView;
        private System.Windows.Forms.ContextMenu ContextMenu_TreeView;
        private System.Windows.Forms.MenuItem mnuNew_SoundBank;
        private System.Windows.Forms.MenuItem mnuCopy_SoundBank;
        private System.Windows.Forms.MenuItem mnuDelete_SoundBank;
        private System.Windows.Forms.MenuItem mnuRename_SoundBank;
        private System.Windows.Forms.MenuItem mnuProperties_SoundBank;
        private System.Windows.Forms.MenuItem mnuMaxOutSize_SoundBank;
        protected internal System.Windows.Forms.TreeView tvwSoundBanks;
    }
}

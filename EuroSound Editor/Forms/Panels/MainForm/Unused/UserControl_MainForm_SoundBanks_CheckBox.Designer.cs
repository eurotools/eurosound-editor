
namespace EuroSound_Editor.Panels
{
    partial class UserControl_MainForm_SoundBanks_CheckBox
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
            this.grbSoundBanks = new System.Windows.Forms.GroupBox();
            this.lblBankNumber = new System.Windows.Forms.Label();
            this.btnSelect_Clear = new System.Windows.Forms.Button();
            this.btnSelect_Invert = new System.Windows.Forms.Button();
            this.btnSelect_All = new System.Windows.Forms.Button();
            this.lblSizeLastOutput = new System.Windows.Forms.Label();
            this.cbllstSoundbanks = new System.Windows.Forms.CheckedListBox();
            this.ContextMenu_TreeView = new System.Windows.Forms.ContextMenu();
            this.mnuNew = new System.Windows.Forms.MenuItem();
            this.mnuCopy = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.mnuRename = new System.Windows.Forms.MenuItem();
            this.mnuProperties = new System.Windows.Forms.MenuItem();
            this.mnuMaxOutputSize = new System.Windows.Forms.MenuItem();
            this.ToolTip_Help = new System.Windows.Forms.ToolTip(this.components);
            this.grbSoundBanks.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSoundBanks
            // 
            this.grbSoundBanks.Controls.Add(this.lblBankNumber);
            this.grbSoundBanks.Controls.Add(this.btnSelect_Clear);
            this.grbSoundBanks.Controls.Add(this.btnSelect_Invert);
            this.grbSoundBanks.Controls.Add(this.btnSelect_All);
            this.grbSoundBanks.Controls.Add(this.lblSizeLastOutput);
            this.grbSoundBanks.Controls.Add(this.cbllstSoundbanks);
            this.grbSoundBanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbSoundBanks.Location = new System.Drawing.Point(0, 0);
            this.grbSoundBanks.Name = "grbSoundBanks";
            this.grbSoundBanks.Size = new System.Drawing.Size(196, 263);
            this.grbSoundBanks.TabIndex = 0;
            this.grbSoundBanks.TabStop = false;
            this.grbSoundBanks.Text = "Sound Banks";
            // 
            // lblBankNumber
            // 
            this.lblBankNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBankNumber.AutoSize = true;
            this.lblBankNumber.Location = new System.Drawing.Point(122, 218);
            this.lblBankNumber.Name = "lblBankNumber";
            this.lblBankNumber.Size = new System.Drawing.Size(61, 13);
            this.lblBankNumber.TabIndex = 5;
            this.lblBankNumber.Text = "Bank No: 0";
            // 
            // btnSelect_Clear
            // 
            this.btnSelect_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect_Clear.Location = new System.Drawing.Point(128, 234);
            this.btnSelect_Clear.Name = "btnSelect_Clear";
            this.btnSelect_Clear.Size = new System.Drawing.Size(55, 23);
            this.btnSelect_Clear.TabIndex = 4;
            this.btnSelect_Clear.Text = "Clear";
            this.ToolTip_Help.SetToolTip(this.btnSelect_Clear, "Clear All SoundBanks Checked For Output");
            this.btnSelect_Clear.UseVisualStyleBackColor = true;
            this.btnSelect_Clear.Click += new System.EventHandler(this.BtnSelect_Clear_Click);
            // 
            // btnSelect_Invert
            // 
            this.btnSelect_Invert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect_Invert.Location = new System.Drawing.Point(67, 234);
            this.btnSelect_Invert.Name = "btnSelect_Invert";
            this.btnSelect_Invert.Size = new System.Drawing.Size(55, 23);
            this.btnSelect_Invert.TabIndex = 3;
            this.btnSelect_Invert.Text = "Invert";
            this.ToolTip_Help.SetToolTip(this.btnSelect_Invert, "Flip SoundBanks Checked For Output");
            this.btnSelect_Invert.UseVisualStyleBackColor = true;
            this.btnSelect_Invert.Click += new System.EventHandler(this.BtnSelect_Invert_Click);
            // 
            // btnSelect_All
            // 
            this.btnSelect_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect_All.Location = new System.Drawing.Point(6, 234);
            this.btnSelect_All.Name = "btnSelect_All";
            this.btnSelect_All.Size = new System.Drawing.Size(55, 23);
            this.btnSelect_All.TabIndex = 2;
            this.btnSelect_All.Text = "All";
            this.ToolTip_Help.SetToolTip(this.btnSelect_All, "Check All SoundBanks For Output");
            this.btnSelect_All.UseVisualStyleBackColor = true;
            this.btnSelect_All.Click += new System.EventHandler(this.BtnSelect_All_Click);
            // 
            // lblSizeLastOutput
            // 
            this.lblSizeLastOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSizeLastOutput.AutoSize = true;
            this.lblSizeLastOutput.Location = new System.Drawing.Point(6, 200);
            this.lblSizeLastOutput.Name = "lblSizeLastOutput";
            this.lblSizeLastOutput.Size = new System.Drawing.Size(115, 13);
            this.lblSizeLastOutput.TabIndex = 1;
            this.lblSizeLastOutput.Text = "Size Last Output: ????";
            // 
            // cbllstSoundbanks
            // 
            this.cbllstSoundbanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbllstSoundbanks.ContextMenu = this.ContextMenu_TreeView;
            this.cbllstSoundbanks.FormattingEnabled = true;
            this.cbllstSoundbanks.Location = new System.Drawing.Point(6, 19);
            this.cbllstSoundbanks.Name = "cbllstSoundbanks";
            this.cbllstSoundbanks.Size = new System.Drawing.Size(184, 169);
            this.cbllstSoundbanks.Sorted = true;
            this.cbllstSoundbanks.TabIndex = 0;
            this.ToolTip_Help.SetToolTip(this.cbllstSoundbanks, "All Available SoundBanks");
            this.cbllstSoundbanks.SelectedIndexChanged += new System.EventHandler(this.Checked_lstSoundbanks_SelectedIndexChanged);
            // 
            // ContextMenu_TreeView
            // 
            this.ContextMenu_TreeView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNew,
            this.mnuCopy,
            this.mnuDelete,
            this.mnuRename,
            this.mnuProperties,
            this.mnuMaxOutputSize});
            // 
            // mnuNew
            // 
            this.mnuNew.Index = 0;
            this.mnuNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.MnuNew_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Index = 1;
            this.mnuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.MnuCopy_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 2;
            this.mnuDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.MnuDelete_Click);
            // 
            // mnuRename
            // 
            this.mnuRename.Index = 3;
            this.mnuRename.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.mnuRename.Text = "Rename";
            this.mnuRename.Click += new System.EventHandler(this.MnuRename_Click);
            // 
            // mnuProperties
            // 
            this.mnuProperties.Index = 4;
            this.mnuProperties.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.mnuProperties.Text = "Properties";
            this.mnuProperties.Click += new System.EventHandler(this.MnuProperties_Click);
            // 
            // mnuMaxOutputSize
            // 
            this.mnuMaxOutputSize.Index = 5;
            this.mnuMaxOutputSize.Text = "Max Output Size?";
            this.mnuMaxOutputSize.Click += new System.EventHandler(this.MnuMaxOutputSize_Click);
            // 
            // UserControl_ManForm_SoundBanks_CheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbSoundBanks);
            this.Name = "UserControl_ManForm_SoundBanks_CheckBox";
            this.Size = new System.Drawing.Size(196, 263);
            this.grbSoundBanks.ResumeLayout(false);
            this.grbSoundBanks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSoundBanks;
        private System.Windows.Forms.Label lblBankNumber;
        private System.Windows.Forms.Button btnSelect_Clear;
        private System.Windows.Forms.Button btnSelect_Invert;
        private System.Windows.Forms.Button btnSelect_All;
        private System.Windows.Forms.Label lblSizeLastOutput;
        protected internal System.Windows.Forms.CheckedListBox cbllstSoundbanks;
        private System.Windows.Forms.ToolTip ToolTip_Help;
        private System.Windows.Forms.ContextMenu ContextMenu_TreeView;
        private System.Windows.Forms.MenuItem mnuNew;
        private System.Windows.Forms.MenuItem mnuCopy;
        private System.Windows.Forms.MenuItem mnuDelete;
        private System.Windows.Forms.MenuItem mnuRename;
        private System.Windows.Forms.MenuItem mnuProperties;
        private System.Windows.Forms.MenuItem mnuMaxOutputSize;
    }
}

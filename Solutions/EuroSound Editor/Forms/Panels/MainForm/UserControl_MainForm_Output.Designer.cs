
namespace EuroSound_Editor.Panels
{
    partial class UserControl_MainForm_Output
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
            this.grbOutput = new System.Windows.Forms.GroupBox();
            this.chkOutputAllLanguages = new System.Windows.Forms.CheckBox();
            this.lblOutput_Language = new System.Windows.Forms.Label();
            this.cboOutputLanguage = new System.Windows.Forms.ComboBox();
            this.cboOutputFormat = new System.Windows.Forms.ComboBox();
            this.lblOutput_Format = new System.Windows.Forms.Label();
            this.chkFastReSample = new System.Windows.Forms.CheckBox();
            this.btnQuickOutput = new System.Windows.Forms.Button();
            this.btnFullOutput = new System.Windows.Forms.Button();
            this.rdoAllForAll = new System.Windows.Forms.RadioButton();
            this.rdoAllBanksSelectedFormat = new System.Windows.Forms.RadioButton();
            this.rdoOutput_Selected = new System.Windows.Forms.RadioButton();
            this.ToolTip_Controls = new System.Windows.Forms.ToolTip(this.components);
            this.grbOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbOutput
            // 
            this.grbOutput.Controls.Add(this.chkOutputAllLanguages);
            this.grbOutput.Controls.Add(this.lblOutput_Language);
            this.grbOutput.Controls.Add(this.cboOutputLanguage);
            this.grbOutput.Controls.Add(this.cboOutputFormat);
            this.grbOutput.Controls.Add(this.lblOutput_Format);
            this.grbOutput.Controls.Add(this.chkFastReSample);
            this.grbOutput.Controls.Add(this.btnQuickOutput);
            this.grbOutput.Controls.Add(this.btnFullOutput);
            this.grbOutput.Controls.Add(this.rdoAllForAll);
            this.grbOutput.Controls.Add(this.rdoAllBanksSelectedFormat);
            this.grbOutput.Controls.Add(this.rdoOutput_Selected);
            this.grbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbOutput.Location = new System.Drawing.Point(0, 0);
            this.grbOutput.Name = "grbOutput";
            this.grbOutput.Size = new System.Drawing.Size(241, 215);
            this.grbOutput.TabIndex = 0;
            this.grbOutput.TabStop = false;
            this.grbOutput.Text = "Output";
            // 
            // chkOutputAllLanguages
            // 
            this.chkOutputAllLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkOutputAllLanguages.AutoSize = true;
            this.chkOutputAllLanguages.Location = new System.Drawing.Point(6, 192);
            this.chkOutputAllLanguages.Name = "chkOutputAllLanguages";
            this.chkOutputAllLanguages.Size = new System.Drawing.Size(128, 17);
            this.chkOutputAllLanguages.TabIndex = 10;
            this.chkOutputAllLanguages.Text = "Output All Languages";
            this.chkOutputAllLanguages.UseVisualStyleBackColor = true;
            this.chkOutputAllLanguages.Click += new System.EventHandler(this.ChkOutputAllLanguages_Click);
            // 
            // lblOutput_Language
            // 
            this.lblOutput_Language.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOutput_Language.AutoSize = true;
            this.lblOutput_Language.Location = new System.Drawing.Point(6, 168);
            this.lblOutput_Language.Name = "lblOutput_Language";
            this.lblOutput_Language.Size = new System.Drawing.Size(58, 13);
            this.lblOutput_Language.TabIndex = 9;
            this.lblOutput_Language.Text = "Language:";
            // 
            // cboOutputLanguage
            // 
            this.cboOutputLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboOutputLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutputLanguage.FormattingEnabled = true;
            this.cboOutputLanguage.Location = new System.Drawing.Point(70, 165);
            this.cboOutputLanguage.Name = "cboOutputLanguage";
            this.cboOutputLanguage.Size = new System.Drawing.Size(162, 21);
            this.cboOutputLanguage.TabIndex = 8;
            this.cboOutputLanguage.SelectionChangeCommitted += new System.EventHandler(this.CboOutputLanguage_SelectionChangeCommitted);
            // 
            // cboOutputFormat
            // 
            this.cboOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutputFormat.FormattingEnabled = true;
            this.cboOutputFormat.Location = new System.Drawing.Point(70, 138);
            this.cboOutputFormat.Name = "cboOutputFormat";
            this.cboOutputFormat.Size = new System.Drawing.Size(162, 21);
            this.cboOutputFormat.TabIndex = 7;
            this.ToolTip_Controls.SetToolTip(this.cboOutputFormat, "Format To Output SoundBanks In");
            // 
            // lblOutput_Format
            // 
            this.lblOutput_Format.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOutput_Format.AutoSize = true;
            this.lblOutput_Format.Location = new System.Drawing.Point(6, 141);
            this.lblOutput_Format.Name = "lblOutput_Format";
            this.lblOutput_Format.Size = new System.Drawing.Size(42, 13);
            this.lblOutput_Format.TabIndex = 6;
            this.lblOutput_Format.Text = "Format:";
            // 
            // chkFastReSample
            // 
            this.chkFastReSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkFastReSample.AutoSize = true;
            this.chkFastReSample.Location = new System.Drawing.Point(6, 115);
            this.chkFastReSample.Name = "chkFastReSample";
            this.chkFastReSample.Size = new System.Drawing.Size(98, 17);
            this.chkFastReSample.TabIndex = 5;
            this.chkFastReSample.Text = "Fast ReSample";
            this.chkFastReSample.UseVisualStyleBackColor = true;
            this.chkFastReSample.CheckedChanged += new System.EventHandler(this.ChkFastReSample_CheckedChanged);
            // 
            // btnQuickOutput
            // 
            this.btnQuickOutput.Location = new System.Drawing.Point(122, 86);
            this.btnQuickOutput.Name = "btnQuickOutput";
            this.btnQuickOutput.Size = new System.Drawing.Size(110, 23);
            this.btnQuickOutput.TabIndex = 4;
            this.btnQuickOutput.Text = "Quick Output";
            this.ToolTip_Controls.SetToolTip(this.btnQuickOutput, "As Per Full Output But Excluding Re-Sampling And SFX_Define.h Build.");
            this.btnQuickOutput.UseVisualStyleBackColor = true;
            this.btnQuickOutput.Click += new System.EventHandler(this.BtnQuickOutput_Click);
            // 
            // btnFullOutput
            // 
            this.btnFullOutput.Location = new System.Drawing.Point(6, 86);
            this.btnFullOutput.Name = "btnFullOutput";
            this.btnFullOutput.Size = new System.Drawing.Size(110, 23);
            this.btnFullOutput.TabIndex = 3;
            this.btnFullOutput.Text = "Full Output";
            this.ToolTip_Controls.SetToolTip(this.btnFullOutput, "Output SoundBank(s) Based On Options Selected");
            this.btnFullOutput.UseVisualStyleBackColor = true;
            this.btnFullOutput.Click += new System.EventHandler(this.BtnFullOutput_Click);
            // 
            // rdoAllForAll
            // 
            this.rdoAllForAll.AutoSize = true;
            this.rdoAllForAll.Location = new System.Drawing.Point(6, 63);
            this.rdoAllForAll.Name = "rdoAllForAll";
            this.rdoAllForAll.Size = new System.Drawing.Size(141, 17);
            this.rdoAllForAll.TabIndex = 2;
            this.rdoAllForAll.Text = "All Banks For All Formats";
            this.rdoAllForAll.UseVisualStyleBackColor = true;
            // 
            // rdoAllBanksSelectedFormat
            // 
            this.rdoAllBanksSelectedFormat.AutoSize = true;
            this.rdoAllBanksSelectedFormat.Location = new System.Drawing.Point(6, 40);
            this.rdoAllBanksSelectedFormat.Name = "rdoAllBanksSelectedFormat";
            this.rdoAllBanksSelectedFormat.Size = new System.Drawing.Size(145, 17);
            this.rdoAllBanksSelectedFormat.TabIndex = 1;
            this.rdoAllBanksSelectedFormat.Text = "All Banks For This Format";
            this.rdoAllBanksSelectedFormat.UseVisualStyleBackColor = true;
            // 
            // rdoOutput_Selected
            // 
            this.rdoOutput_Selected.AutoSize = true;
            this.rdoOutput_Selected.Checked = true;
            this.rdoOutput_Selected.Location = new System.Drawing.Point(6, 17);
            this.rdoOutput_Selected.Name = "rdoOutput_Selected";
            this.rdoOutput_Selected.Size = new System.Drawing.Size(100, 17);
            this.rdoOutput_Selected.TabIndex = 0;
            this.rdoOutput_Selected.TabStop = true;
            this.rdoOutput_Selected.Text = "Selected Banks";
            this.rdoOutput_Selected.UseVisualStyleBackColor = true;
            // 
            // UserControl_MainForm_Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbOutput);
            this.Name = "UserControl_MainForm_Output";
            this.Size = new System.Drawing.Size(241, 215);
            this.grbOutput.ResumeLayout(false);
            this.grbOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbOutput;
        private System.Windows.Forms.Label lblOutput_Language;
        private System.Windows.Forms.Label lblOutput_Format;
        private System.Windows.Forms.ToolTip ToolTip_Controls;
        protected internal System.Windows.Forms.ComboBox cboOutputLanguage;
        protected internal System.Windows.Forms.CheckBox chkFastReSample;
        protected internal System.Windows.Forms.CheckBox chkOutputAllLanguages;
        protected internal System.Windows.Forms.RadioButton rdoAllForAll;
        protected internal System.Windows.Forms.RadioButton rdoAllBanksSelectedFormat;
        protected internal System.Windows.Forms.RadioButton rdoOutput_Selected;
        protected internal System.Windows.Forms.ComboBox cboOutputFormat;
        protected internal System.Windows.Forms.Button btnQuickOutput;
        protected internal System.Windows.Forms.Button btnFullOutput;
    }
}

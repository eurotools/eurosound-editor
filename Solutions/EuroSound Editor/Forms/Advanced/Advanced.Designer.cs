
namespace sb_editor.Forms
{
    partial class Advanced
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Advanced));
            this.grbAdvancedOptions = new System.Windows.Forms.GroupBox();
            this.btnValidatePlatforms = new System.Windows.Forms.Button();
            this.btnValidateSubSFXs = new System.Windows.Forms.Button();
            this.btnCheckStealOnLouder = new System.Windows.Forms.Button();
            this.btnLanguageFolderTool = new System.Windows.Forms.Button();
            this.btnValidateInterSample = new System.Windows.Forms.Button();
            this.btnReAllocateHashcodes = new System.Windows.Forms.Button();
            this.btnCheckHashCodes = new System.Windows.Forms.Button();
            this.btnMakeReport = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSetupSfxGroups = new System.Windows.Forms.Button();
            this.grbAdvancedOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbAdvancedOptions
            // 
            this.grbAdvancedOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbAdvancedOptions.Controls.Add(this.btnSetupSfxGroups);
            this.grbAdvancedOptions.Controls.Add(this.btnValidatePlatforms);
            this.grbAdvancedOptions.Controls.Add(this.btnValidateSubSFXs);
            this.grbAdvancedOptions.Controls.Add(this.btnCheckStealOnLouder);
            this.grbAdvancedOptions.Controls.Add(this.btnLanguageFolderTool);
            this.grbAdvancedOptions.Controls.Add(this.btnValidateInterSample);
            this.grbAdvancedOptions.Controls.Add(this.btnReAllocateHashcodes);
            this.grbAdvancedOptions.Controls.Add(this.btnCheckHashCodes);
            this.grbAdvancedOptions.Controls.Add(this.btnMakeReport);
            this.grbAdvancedOptions.Location = new System.Drawing.Point(12, 12);
            this.grbAdvancedOptions.Name = "grbAdvancedOptions";
            this.grbAdvancedOptions.Size = new System.Drawing.Size(201, 265);
            this.grbAdvancedOptions.TabIndex = 0;
            this.grbAdvancedOptions.TabStop = false;
            this.grbAdvancedOptions.Text = "Advanced Options";
            // 
            // btnValidatePlatforms
            // 
            this.btnValidatePlatforms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidatePlatforms.Location = new System.Drawing.Point(6, 208);
            this.btnValidatePlatforms.Name = "btnValidatePlatforms";
            this.btnValidatePlatforms.Size = new System.Drawing.Size(189, 21);
            this.btnValidatePlatforms.TabIndex = 7;
            this.btnValidatePlatforms.Text = "Validate Platform SFX Versions";
            this.btnValidatePlatforms.UseVisualStyleBackColor = true;
            this.btnValidatePlatforms.Click += new System.EventHandler(this.BtnValidatePlatforms_Click);
            // 
            // btnValidateSubSFXs
            // 
            this.btnValidateSubSFXs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidateSubSFXs.Location = new System.Drawing.Point(6, 181);
            this.btnValidateSubSFXs.Name = "btnValidateSubSFXs";
            this.btnValidateSubSFXs.Size = new System.Drawing.Size(189, 21);
            this.btnValidateSubSFXs.TabIndex = 6;
            this.btnValidateSubSFXs.Text = "Validate Sub SFS links";
            this.btnValidateSubSFXs.UseVisualStyleBackColor = true;
            this.btnValidateSubSFXs.Click += new System.EventHandler(this.BtnValidateSubSFXs_Click);
            // 
            // btnCheckStealOnLouder
            // 
            this.btnCheckStealOnLouder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckStealOnLouder.Location = new System.Drawing.Point(6, 154);
            this.btnCheckStealOnLouder.Name = "btnCheckStealOnLouder";
            this.btnCheckStealOnLouder.Size = new System.Drawing.Size(189, 21);
            this.btnCheckStealOnLouder.TabIndex = 5;
            this.btnCheckStealOnLouder.Text = "Steal On Louder Check";
            this.btnCheckStealOnLouder.UseVisualStyleBackColor = true;
            this.btnCheckStealOnLouder.Click += new System.EventHandler(this.BtnCheckStealOnLouder_Click);
            // 
            // btnLanguageFolderTool
            // 
            this.btnLanguageFolderTool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLanguageFolderTool.Location = new System.Drawing.Point(6, 127);
            this.btnLanguageFolderTool.Name = "btnLanguageFolderTool";
            this.btnLanguageFolderTool.Size = new System.Drawing.Size(189, 21);
            this.btnLanguageFolderTool.TabIndex = 4;
            this.btnLanguageFolderTool.Text = "Language Folder Compare";
            this.btnLanguageFolderTool.UseVisualStyleBackColor = true;
            this.btnLanguageFolderTool.Click += new System.EventHandler(this.BtnLanguageFolderTool_Click);
            // 
            // btnValidateInterSample
            // 
            this.btnValidateInterSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidateInterSample.Location = new System.Drawing.Point(6, 100);
            this.btnValidateInterSample.Name = "btnValidateInterSample";
            this.btnValidateInterSample.Size = new System.Drawing.Size(189, 21);
            this.btnValidateInterSample.TabIndex = 3;
            this.btnValidateInterSample.Text = "Validate Inter-Sample Delay";
            this.btnValidateInterSample.UseVisualStyleBackColor = true;
            this.btnValidateInterSample.Click += new System.EventHandler(this.BtnValidateInterSample_Click);
            // 
            // btnReAllocateHashcodes
            // 
            this.btnReAllocateHashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReAllocateHashcodes.Location = new System.Drawing.Point(6, 73);
            this.btnReAllocateHashcodes.Name = "btnReAllocateHashcodes";
            this.btnReAllocateHashcodes.Size = new System.Drawing.Size(189, 21);
            this.btnReAllocateHashcodes.TabIndex = 2;
            this.btnReAllocateHashcodes.Text = "Re-Allocate HashCodes";
            this.btnReAllocateHashcodes.UseVisualStyleBackColor = true;
            this.btnReAllocateHashcodes.Click += new System.EventHandler(this.BtnReAllocateHashcodes_Click);
            // 
            // btnCheckHashCodes
            // 
            this.btnCheckHashCodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckHashCodes.Location = new System.Drawing.Point(6, 46);
            this.btnCheckHashCodes.Name = "btnCheckHashCodes";
            this.btnCheckHashCodes.Size = new System.Drawing.Size(189, 21);
            this.btnCheckHashCodes.TabIndex = 1;
            this.btnCheckHashCodes.Text = "Check SFX HashCodes";
            this.btnCheckHashCodes.UseVisualStyleBackColor = true;
            this.btnCheckHashCodes.Click += new System.EventHandler(this.BtnCheckHashCodes_Click);
            // 
            // btnMakeReport
            // 
            this.btnMakeReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMakeReport.Location = new System.Drawing.Point(6, 19);
            this.btnMakeReport.Name = "btnMakeReport";
            this.btnMakeReport.Size = new System.Drawing.Size(189, 21);
            this.btnMakeReport.TabIndex = 0;
            this.btnMakeReport.Text = "Make Report";
            this.btnMakeReport.UseVisualStyleBackColor = true;
            this.btnMakeReport.Click += new System.EventHandler(this.BtnMakeReport_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(75, 283);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnSetupSfxGroups
            // 
            this.btnSetupSfxGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetupSfxGroups.Location = new System.Drawing.Point(6, 235);
            this.btnSetupSfxGroups.Name = "btnSetupSfxGroups";
            this.btnSetupSfxGroups.Size = new System.Drawing.Size(189, 21);
            this.btnSetupSfxGroups.TabIndex = 8;
            this.btnSetupSfxGroups.Text = "Setup SFX Groups";
            this.btnSetupSfxGroups.UseVisualStyleBackColor = true;
            this.btnSetupSfxGroups.Click += new System.EventHandler(this.BtnSetupSfxGroups_Click);
            // 
            // Advanced
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 318);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grbAdvancedOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Advanced";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced";
            this.grbAdvancedOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbAdvancedOptions;
        private System.Windows.Forms.Button btnValidatePlatforms;
        private System.Windows.Forms.Button btnValidateSubSFXs;
        private System.Windows.Forms.Button btnCheckStealOnLouder;
        private System.Windows.Forms.Button btnLanguageFolderTool;
        private System.Windows.Forms.Button btnValidateInterSample;
        private System.Windows.Forms.Button btnReAllocateHashcodes;
        private System.Windows.Forms.Button btnCheckHashCodes;
        private System.Windows.Forms.Button btnMakeReport;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSetupSfxGroups;
    }
}
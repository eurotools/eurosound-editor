
namespace sb_editor.Forms
{
    partial class LanguageFolderCompare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageFolderCompare));
            this.grbAdditionSecondary = new System.Windows.Forms.GroupBox();
            this.txtAdditionFilesSecondary = new System.Windows.Forms.TextBox();
            this.grbSecondaryMissing = new System.Windows.Forms.GroupBox();
            this.txtMissingFilesSecondary = new System.Windows.Forms.TextBox();
            this.btnDoCompare = new System.Windows.Forms.Button();
            this.grbSecondaryPath = new System.Windows.Forms.GroupBox();
            this.btnSetSecondaryFolder = new System.Windows.Forms.Button();
            this.txtSecondaryPath = new System.Windows.Forms.TextBox();
            this.grbPrimaryPath = new System.Windows.Forms.GroupBox();
            this.btnSetPrimaryFolder = new System.Windows.Forms.Button();
            this.txtPrimaryPath = new System.Windows.Forms.TextBox();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.grbAdditionSecondary.SuspendLayout();
            this.grbSecondaryMissing.SuspendLayout();
            this.grbSecondaryPath.SuspendLayout();
            this.grbPrimaryPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbAdditionSecondary
            // 
            this.grbAdditionSecondary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbAdditionSecondary.Controls.Add(this.txtAdditionFilesSecondary);
            this.grbAdditionSecondary.Location = new System.Drawing.Point(379, 153);
            this.grbAdditionSecondary.Name = "grbAdditionSecondary";
            this.grbAdditionSecondary.Size = new System.Drawing.Size(361, 358);
            this.grbAdditionSecondary.TabIndex = 4;
            this.grbAdditionSecondary.TabStop = false;
            this.grbAdditionSecondary.Text = "Addition Files in Secondary Path";
            // 
            // txtAdditionFilesSecondary
            // 
            this.txtAdditionFilesSecondary.BackColor = System.Drawing.SystemColors.Window;
            this.txtAdditionFilesSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAdditionFilesSecondary.Location = new System.Drawing.Point(3, 16);
            this.txtAdditionFilesSecondary.Multiline = true;
            this.txtAdditionFilesSecondary.Name = "txtAdditionFilesSecondary";
            this.txtAdditionFilesSecondary.ReadOnly = true;
            this.txtAdditionFilesSecondary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAdditionFilesSecondary.Size = new System.Drawing.Size(355, 339);
            this.txtAdditionFilesSecondary.TabIndex = 0;
            // 
            // grbSecondaryMissing
            // 
            this.grbSecondaryMissing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbSecondaryMissing.Controls.Add(this.txtMissingFilesSecondary);
            this.grbSecondaryMissing.Location = new System.Drawing.Point(12, 153);
            this.grbSecondaryMissing.Name = "grbSecondaryMissing";
            this.grbSecondaryMissing.Size = new System.Drawing.Size(361, 358);
            this.grbSecondaryMissing.TabIndex = 3;
            this.grbSecondaryMissing.TabStop = false;
            this.grbSecondaryMissing.Text = "Missing Files in Secondary Path";
            // 
            // txtMissingFilesSecondary
            // 
            this.txtMissingFilesSecondary.BackColor = System.Drawing.SystemColors.Window;
            this.txtMissingFilesSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMissingFilesSecondary.Location = new System.Drawing.Point(3, 16);
            this.txtMissingFilesSecondary.Multiline = true;
            this.txtMissingFilesSecondary.Name = "txtMissingFilesSecondary";
            this.txtMissingFilesSecondary.ReadOnly = true;
            this.txtMissingFilesSecondary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMissingFilesSecondary.Size = new System.Drawing.Size(355, 339);
            this.txtMissingFilesSecondary.TabIndex = 0;
            // 
            // btnDoCompare
            // 
            this.btnDoCompare.Location = new System.Drawing.Point(12, 114);
            this.btnDoCompare.Name = "btnDoCompare";
            this.btnDoCompare.Size = new System.Drawing.Size(85, 33);
            this.btnDoCompare.TabIndex = 2;
            this.btnDoCompare.Text = "Do Compare";
            this.btnDoCompare.UseVisualStyleBackColor = true;
            this.btnDoCompare.Click += new System.EventHandler(this.BtnDoCompare_Click);
            // 
            // grbSecondaryPath
            // 
            this.grbSecondaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSecondaryPath.Controls.Add(this.btnSetSecondaryFolder);
            this.grbSecondaryPath.Controls.Add(this.txtSecondaryPath);
            this.grbSecondaryPath.Location = new System.Drawing.Point(12, 63);
            this.grbSecondaryPath.Name = "grbSecondaryPath";
            this.grbSecondaryPath.Size = new System.Drawing.Size(728, 45);
            this.grbSecondaryPath.TabIndex = 1;
            this.grbSecondaryPath.TabStop = false;
            this.grbSecondaryPath.Text = "Secondary Path";
            // 
            // btnSetSecondaryFolder
            // 
            this.btnSetSecondaryFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetSecondaryFolder.Location = new System.Drawing.Point(647, 13);
            this.btnSetSecondaryFolder.Name = "btnSetSecondaryFolder";
            this.btnSetSecondaryFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSetSecondaryFolder.TabIndex = 1;
            this.btnSetSecondaryFolder.Text = "Set Folder";
            this.btnSetSecondaryFolder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSetSecondaryFolder.UseVisualStyleBackColor = true;
            this.btnSetSecondaryFolder.Click += new System.EventHandler(this.BtnSetSecondaryFolder_Click);
            // 
            // txtSecondaryPath
            // 
            this.txtSecondaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecondaryPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtSecondaryPath.Location = new System.Drawing.Point(6, 16);
            this.txtSecondaryPath.Name = "txtSecondaryPath";
            this.txtSecondaryPath.ReadOnly = true;
            this.txtSecondaryPath.Size = new System.Drawing.Size(635, 20);
            this.txtSecondaryPath.TabIndex = 0;
            // 
            // grbPrimaryPath
            // 
            this.grbPrimaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbPrimaryPath.Controls.Add(this.btnSetPrimaryFolder);
            this.grbPrimaryPath.Controls.Add(this.txtPrimaryPath);
            this.grbPrimaryPath.Location = new System.Drawing.Point(12, 12);
            this.grbPrimaryPath.Name = "grbPrimaryPath";
            this.grbPrimaryPath.Size = new System.Drawing.Size(728, 45);
            this.grbPrimaryPath.TabIndex = 0;
            this.grbPrimaryPath.TabStop = false;
            this.grbPrimaryPath.Text = "Primary Path";
            // 
            // btnSetPrimaryFolder
            // 
            this.btnSetPrimaryFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetPrimaryFolder.Location = new System.Drawing.Point(647, 13);
            this.btnSetPrimaryFolder.Name = "btnSetPrimaryFolder";
            this.btnSetPrimaryFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSetPrimaryFolder.TabIndex = 1;
            this.btnSetPrimaryFolder.Text = "Set Folder";
            this.btnSetPrimaryFolder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSetPrimaryFolder.UseVisualStyleBackColor = true;
            this.btnSetPrimaryFolder.Click += new System.EventHandler(this.BtnSetPrimaryFolder_Click);
            // 
            // txtPrimaryPath
            // 
            this.txtPrimaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrimaryPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrimaryPath.Location = new System.Drawing.Point(6, 16);
            this.txtPrimaryPath.Name = "txtPrimaryPath";
            this.txtPrimaryPath.ReadOnly = true;
            this.txtPrimaryPath.Size = new System.Drawing.Size(635, 20);
            this.txtPrimaryPath.TabIndex = 0;
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "Set Folder For Sample Files.";
            // 
            // LanguageFolderCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 523);
            this.Controls.Add(this.grbAdditionSecondary);
            this.Controls.Add(this.grbSecondaryMissing);
            this.Controls.Add(this.btnDoCompare);
            this.Controls.Add(this.grbSecondaryPath);
            this.Controls.Add(this.grbPrimaryPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LanguageFolderCompare";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Language Folder Compare Utility";
            this.grbAdditionSecondary.ResumeLayout(false);
            this.grbAdditionSecondary.PerformLayout();
            this.grbSecondaryMissing.ResumeLayout(false);
            this.grbSecondaryMissing.PerformLayout();
            this.grbSecondaryPath.ResumeLayout(false);
            this.grbSecondaryPath.PerformLayout();
            this.grbPrimaryPath.ResumeLayout(false);
            this.grbPrimaryPath.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grbAdditionSecondary;
        internal System.Windows.Forms.TextBox txtAdditionFilesSecondary;
        internal System.Windows.Forms.GroupBox grbSecondaryMissing;
        internal System.Windows.Forms.TextBox txtMissingFilesSecondary;
        internal System.Windows.Forms.Button btnDoCompare;
        internal System.Windows.Forms.GroupBox grbSecondaryPath;
        internal System.Windows.Forms.Button btnSetSecondaryFolder;
        internal System.Windows.Forms.TextBox txtSecondaryPath;
        internal System.Windows.Forms.GroupBox grbPrimaryPath;
        internal System.Windows.Forms.Button btnSetPrimaryFolder;
        internal System.Windows.Forms.TextBox txtPrimaryPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}
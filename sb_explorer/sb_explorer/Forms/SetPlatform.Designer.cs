
namespace sb_explorer
{
    partial class SetPlatform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetPlatform));
            this.Label_Explanation = new System.Windows.Forms.Label();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Combobox_Platform = new System.Windows.Forms.ComboBox();
            this.Label_Platform = new System.Windows.Forms.Label();
            this.Textbox_FilePath = new System.Windows.Forms.TextBox();
            this.Label_FilePath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label_Explanation
            // 
            this.Label_Explanation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Explanation.Location = new System.Drawing.Point(12, 35);
            this.Label_Explanation.Name = "Label_Explanation";
            this.Label_Explanation.Size = new System.Drawing.Size(354, 51);
            this.Label_Explanation.TabIndex = 13;
            this.Label_Explanation.Text = "The destination platform for this SFX file could not been found. Please specify t" +
    "he platform on the following combo box or cancel the loading. ";
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(291, 136);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 12;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(210, 136);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 11;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Combobox_Platform
            // 
            this.Combobox_Platform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_Platform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_Platform.FormattingEnabled = true;
            this.Combobox_Platform.Items.AddRange(new object[] {
            "PC",
            "PS2",
            "GC",
            "XB"});
            this.Combobox_Platform.Location = new System.Drawing.Point(69, 89);
            this.Combobox_Platform.Name = "Combobox_Platform";
            this.Combobox_Platform.Size = new System.Drawing.Size(297, 21);
            this.Combobox_Platform.TabIndex = 10;
            // 
            // Label_Platform
            // 
            this.Label_Platform.AutoSize = true;
            this.Label_Platform.Location = new System.Drawing.Point(12, 92);
            this.Label_Platform.Name = "Label_Platform";
            this.Label_Platform.Size = new System.Drawing.Size(48, 13);
            this.Label_Platform.TabIndex = 9;
            this.Label_Platform.Text = "Platform:";
            // 
            // Textbox_FilePath
            // 
            this.Textbox_FilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_FilePath.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_FilePath.Location = new System.Drawing.Point(69, 12);
            this.Textbox_FilePath.Name = "Textbox_FilePath";
            this.Textbox_FilePath.ReadOnly = true;
            this.Textbox_FilePath.Size = new System.Drawing.Size(297, 20);
            this.Textbox_FilePath.TabIndex = 8;
            // 
            // Label_FilePath
            // 
            this.Label_FilePath.AutoSize = true;
            this.Label_FilePath.Location = new System.Drawing.Point(12, 15);
            this.Label_FilePath.Name = "Label_FilePath";
            this.Label_FilePath.Size = new System.Drawing.Size(51, 13);
            this.Label_FilePath.TabIndex = 7;
            this.Label_FilePath.Text = "File Path:";
            // 
            // SetPlatform
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(378, 171);
            this.Controls.Add(this.Label_Explanation);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Combobox_Platform);
            this.Controls.Add(this.Label_Platform);
            this.Controls.Add(this.Textbox_FilePath);
            this.Controls.Add(this.Label_FilePath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetPlatform";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Specify File Platform";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Explanation;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Label Label_Platform;
        private System.Windows.Forms.TextBox Textbox_FilePath;
        private System.Windows.Forms.Label Label_FilePath;
        protected internal System.Windows.Forms.ComboBox Combobox_Platform;
    }
}
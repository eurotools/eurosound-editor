
namespace sb_explorer
{
    partial class StreambanksList_Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreambanksList_Options));
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.GroupBox_ColAudioLength = new System.Windows.Forms.GroupBox();
            this.RadioButton_ColAudioLength_Dec = new System.Windows.Forms.RadioButton();
            this.RadioButton_ColAudioLength_Hex = new System.Windows.Forms.RadioButton();
            this.GroupBox_ColAudioOffset = new System.Windows.Forms.GroupBox();
            this.RadioButton_ColAudioOffset_Dec = new System.Windows.Forms.RadioButton();
            this.RadioButton_ColAudioOffset_Hex = new System.Windows.Forms.RadioButton();
            this.GroupBox_ColMarkerSize = new System.Windows.Forms.GroupBox();
            this.RadioButton_ColMarkerSize_Dec = new System.Windows.Forms.RadioButton();
            this.RadioButton_ColMarkerSize_Hex = new System.Windows.Forms.RadioButton();
            this.GroupBox_ColMarkerOffset = new System.Windows.Forms.GroupBox();
            this.RadioButton_ColMarkerOffset_Dec = new System.Windows.Forms.RadioButton();
            this.RadioButton_ColMarkerOffset_Hex = new System.Windows.Forms.RadioButton();
            this.GroupBox_ColAudioLength.SuspendLayout();
            this.GroupBox_ColAudioOffset.SuspendLayout();
            this.GroupBox_ColMarkerSize.SuspendLayout();
            this.GroupBox_ColMarkerOffset.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(288, 183);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 29);
            this.Button_Cancel.TabIndex = 9;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(207, 183);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 29);
            this.Button_OK.TabIndex = 8;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // GroupBox_ColAudioLength
            // 
            this.GroupBox_ColAudioLength.Controls.Add(this.RadioButton_ColAudioLength_Dec);
            this.GroupBox_ColAudioLength.Controls.Add(this.RadioButton_ColAudioLength_Hex);
            this.GroupBox_ColAudioLength.Location = new System.Drawing.Point(12, 91);
            this.GroupBox_ColAudioLength.Name = "GroupBox_ColAudioLength";
            this.GroupBox_ColAudioLength.Size = new System.Drawing.Size(113, 73);
            this.GroupBox_ColAudioLength.TabIndex = 13;
            this.GroupBox_ColAudioLength.TabStop = false;
            this.GroupBox_ColAudioLength.Text = "Audio Length";
            // 
            // RadioButton_ColAudioLength_Dec
            // 
            this.RadioButton_ColAudioLength_Dec.AutoSize = true;
            this.RadioButton_ColAudioLength_Dec.Checked = true;
            this.RadioButton_ColAudioLength_Dec.Location = new System.Drawing.Point(6, 42);
            this.RadioButton_ColAudioLength_Dec.Name = "RadioButton_ColAudioLength_Dec";
            this.RadioButton_ColAudioLength_Dec.Size = new System.Drawing.Size(63, 17);
            this.RadioButton_ColAudioLength_Dec.TabIndex = 2;
            this.RadioButton_ColAudioLength_Dec.TabStop = true;
            this.RadioButton_ColAudioLength_Dec.Text = "Decimal";
            this.RadioButton_ColAudioLength_Dec.UseVisualStyleBackColor = true;
            // 
            // RadioButton_ColAudioLength_Hex
            // 
            this.RadioButton_ColAudioLength_Hex.AutoSize = true;
            this.RadioButton_ColAudioLength_Hex.Location = new System.Drawing.Point(6, 19);
            this.RadioButton_ColAudioLength_Hex.Name = "RadioButton_ColAudioLength_Hex";
            this.RadioButton_ColAudioLength_Hex.Size = new System.Drawing.Size(86, 17);
            this.RadioButton_ColAudioLength_Hex.TabIndex = 2;
            this.RadioButton_ColAudioLength_Hex.Text = "Hexadecimal";
            this.RadioButton_ColAudioLength_Hex.UseVisualStyleBackColor = true;
            // 
            // GroupBox_ColAudioOffset
            // 
            this.GroupBox_ColAudioOffset.Controls.Add(this.RadioButton_ColAudioOffset_Dec);
            this.GroupBox_ColAudioOffset.Controls.Add(this.RadioButton_ColAudioOffset_Hex);
            this.GroupBox_ColAudioOffset.Location = new System.Drawing.Point(250, 12);
            this.GroupBox_ColAudioOffset.Name = "GroupBox_ColAudioOffset";
            this.GroupBox_ColAudioOffset.Size = new System.Drawing.Size(113, 73);
            this.GroupBox_ColAudioOffset.TabIndex = 12;
            this.GroupBox_ColAudioOffset.TabStop = false;
            this.GroupBox_ColAudioOffset.Text = "Audio Offset";
            // 
            // RadioButton_ColAudioOffset_Dec
            // 
            this.RadioButton_ColAudioOffset_Dec.AutoSize = true;
            this.RadioButton_ColAudioOffset_Dec.Checked = true;
            this.RadioButton_ColAudioOffset_Dec.Location = new System.Drawing.Point(6, 42);
            this.RadioButton_ColAudioOffset_Dec.Name = "RadioButton_ColAudioOffset_Dec";
            this.RadioButton_ColAudioOffset_Dec.Size = new System.Drawing.Size(63, 17);
            this.RadioButton_ColAudioOffset_Dec.TabIndex = 2;
            this.RadioButton_ColAudioOffset_Dec.TabStop = true;
            this.RadioButton_ColAudioOffset_Dec.Text = "Decimal";
            this.RadioButton_ColAudioOffset_Dec.UseVisualStyleBackColor = true;
            // 
            // RadioButton_ColAudioOffset_Hex
            // 
            this.RadioButton_ColAudioOffset_Hex.AutoSize = true;
            this.RadioButton_ColAudioOffset_Hex.Location = new System.Drawing.Point(6, 19);
            this.RadioButton_ColAudioOffset_Hex.Name = "RadioButton_ColAudioOffset_Hex";
            this.RadioButton_ColAudioOffset_Hex.Size = new System.Drawing.Size(86, 17);
            this.RadioButton_ColAudioOffset_Hex.TabIndex = 2;
            this.RadioButton_ColAudioOffset_Hex.Text = "Hexadecimal";
            this.RadioButton_ColAudioOffset_Hex.UseVisualStyleBackColor = true;
            // 
            // GroupBox_ColMarkerSize
            // 
            this.GroupBox_ColMarkerSize.Controls.Add(this.RadioButton_ColMarkerSize_Dec);
            this.GroupBox_ColMarkerSize.Controls.Add(this.RadioButton_ColMarkerSize_Hex);
            this.GroupBox_ColMarkerSize.Location = new System.Drawing.Point(131, 12);
            this.GroupBox_ColMarkerSize.Name = "GroupBox_ColMarkerSize";
            this.GroupBox_ColMarkerSize.Size = new System.Drawing.Size(113, 73);
            this.GroupBox_ColMarkerSize.TabIndex = 11;
            this.GroupBox_ColMarkerSize.TabStop = false;
            this.GroupBox_ColMarkerSize.Text = "Marker Size";
            // 
            // RadioButton_ColMarkerSize_Dec
            // 
            this.RadioButton_ColMarkerSize_Dec.AutoSize = true;
            this.RadioButton_ColMarkerSize_Dec.Checked = true;
            this.RadioButton_ColMarkerSize_Dec.Location = new System.Drawing.Point(6, 42);
            this.RadioButton_ColMarkerSize_Dec.Name = "RadioButton_ColMarkerSize_Dec";
            this.RadioButton_ColMarkerSize_Dec.Size = new System.Drawing.Size(63, 17);
            this.RadioButton_ColMarkerSize_Dec.TabIndex = 2;
            this.RadioButton_ColMarkerSize_Dec.TabStop = true;
            this.RadioButton_ColMarkerSize_Dec.Text = "Decimal";
            this.RadioButton_ColMarkerSize_Dec.UseVisualStyleBackColor = true;
            // 
            // RadioButton_ColMarkerSize_Hex
            // 
            this.RadioButton_ColMarkerSize_Hex.AutoSize = true;
            this.RadioButton_ColMarkerSize_Hex.Location = new System.Drawing.Point(6, 19);
            this.RadioButton_ColMarkerSize_Hex.Name = "RadioButton_ColMarkerSize_Hex";
            this.RadioButton_ColMarkerSize_Hex.Size = new System.Drawing.Size(86, 17);
            this.RadioButton_ColMarkerSize_Hex.TabIndex = 2;
            this.RadioButton_ColMarkerSize_Hex.Text = "Hexadecimal";
            this.RadioButton_ColMarkerSize_Hex.UseVisualStyleBackColor = true;
            // 
            // GroupBox_ColMarkerOffset
            // 
            this.GroupBox_ColMarkerOffset.Controls.Add(this.RadioButton_ColMarkerOffset_Dec);
            this.GroupBox_ColMarkerOffset.Controls.Add(this.RadioButton_ColMarkerOffset_Hex);
            this.GroupBox_ColMarkerOffset.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_ColMarkerOffset.Name = "GroupBox_ColMarkerOffset";
            this.GroupBox_ColMarkerOffset.Size = new System.Drawing.Size(113, 73);
            this.GroupBox_ColMarkerOffset.TabIndex = 10;
            this.GroupBox_ColMarkerOffset.TabStop = false;
            this.GroupBox_ColMarkerOffset.Text = "Marker Offset";
            // 
            // RadioButton_ColMarkerOffset_Dec
            // 
            this.RadioButton_ColMarkerOffset_Dec.AutoSize = true;
            this.RadioButton_ColMarkerOffset_Dec.Checked = true;
            this.RadioButton_ColMarkerOffset_Dec.Location = new System.Drawing.Point(6, 42);
            this.RadioButton_ColMarkerOffset_Dec.Name = "RadioButton_ColMarkerOffset_Dec";
            this.RadioButton_ColMarkerOffset_Dec.Size = new System.Drawing.Size(63, 17);
            this.RadioButton_ColMarkerOffset_Dec.TabIndex = 2;
            this.RadioButton_ColMarkerOffset_Dec.TabStop = true;
            this.RadioButton_ColMarkerOffset_Dec.Text = "Decimal";
            this.RadioButton_ColMarkerOffset_Dec.UseVisualStyleBackColor = true;
            // 
            // RadioButton_ColMarkerOffset_Hex
            // 
            this.RadioButton_ColMarkerOffset_Hex.AutoSize = true;
            this.RadioButton_ColMarkerOffset_Hex.Location = new System.Drawing.Point(6, 19);
            this.RadioButton_ColMarkerOffset_Hex.Name = "RadioButton_ColMarkerOffset_Hex";
            this.RadioButton_ColMarkerOffset_Hex.Size = new System.Drawing.Size(86, 17);
            this.RadioButton_ColMarkerOffset_Hex.TabIndex = 2;
            this.RadioButton_ColMarkerOffset_Hex.Text = "Hexadecimal";
            this.RadioButton_ColMarkerOffset_Hex.UseVisualStyleBackColor = true;
            // 
            // StreambanksList_Options
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(375, 224);
            this.Controls.Add(this.GroupBox_ColAudioLength);
            this.Controls.Add(this.GroupBox_ColAudioOffset);
            this.Controls.Add(this.GroupBox_ColMarkerSize);
            this.Controls.Add(this.GroupBox_ColMarkerOffset);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StreambanksList_Options";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stream Data Listview Options";
            this.Load += new System.EventHandler(this.StreambanksList_Options_Load);
            this.GroupBox_ColAudioLength.ResumeLayout(false);
            this.GroupBox_ColAudioLength.PerformLayout();
            this.GroupBox_ColAudioOffset.ResumeLayout(false);
            this.GroupBox_ColAudioOffset.PerformLayout();
            this.GroupBox_ColMarkerSize.ResumeLayout(false);
            this.GroupBox_ColMarkerSize.PerformLayout();
            this.GroupBox_ColMarkerOffset.ResumeLayout(false);
            this.GroupBox_ColMarkerOffset.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.GroupBox GroupBox_ColAudioLength;
        private System.Windows.Forms.RadioButton RadioButton_ColAudioLength_Dec;
        private System.Windows.Forms.RadioButton RadioButton_ColAudioLength_Hex;
        private System.Windows.Forms.GroupBox GroupBox_ColAudioOffset;
        private System.Windows.Forms.RadioButton RadioButton_ColAudioOffset_Dec;
        private System.Windows.Forms.RadioButton RadioButton_ColAudioOffset_Hex;
        private System.Windows.Forms.GroupBox GroupBox_ColMarkerSize;
        private System.Windows.Forms.RadioButton RadioButton_ColMarkerSize_Dec;
        private System.Windows.Forms.RadioButton RadioButton_ColMarkerSize_Hex;
        private System.Windows.Forms.GroupBox GroupBox_ColMarkerOffset;
        private System.Windows.Forms.RadioButton RadioButton_ColMarkerOffset_Dec;
        private System.Windows.Forms.RadioButton RadioButton_ColMarkerOffset_Hex;
    }
}
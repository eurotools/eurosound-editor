
namespace sb_explorer
{
    partial class ADPCMValidator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADPCMValidator));
            this.ProgressBar_Validation = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // ProgressBar_Validation
            // 
            this.ProgressBar_Validation.Location = new System.Drawing.Point(24, 32);
            this.ProgressBar_Validation.Name = "ProgressBar_Validation";
            this.ProgressBar_Validation.Size = new System.Drawing.Size(328, 23);
            this.ProgressBar_Validation.TabIndex = 1;
            // 
            // ADPCMValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 93);
            this.Controls.Add(this.ProgressBar_Validation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ADPCMValidator";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Validating ADPCM data";
            this.Shown += new System.EventHandler(this.ADPCMValidator_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.ProgressBar ProgressBar_Validation;
    }
}
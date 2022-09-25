
namespace EuroSound_Editor.Forms
{
    partial class MissingSamplesFound
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MissingSamplesFound));
            this.grbSamples = new System.Windows.Forms.GroupBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.lstSamplesList = new System.Windows.Forms.ListBox();
            this.grbSamples.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSamples
            // 
            this.grbSamples.Controls.Add(this.Button_OK);
            this.grbSamples.Controls.Add(this.lstSamplesList);
            this.grbSamples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbSamples.Location = new System.Drawing.Point(0, 0);
            this.grbSamples.Name = "grbSamples";
            this.grbSamples.Size = new System.Drawing.Size(513, 519);
            this.grbSamples.TabIndex = 1;
            this.grbSamples.TabStop = false;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(422, 480);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(85, 33);
            this.Button_OK.TabIndex = 1;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            // 
            // lstSamplesList
            // 
            this.lstSamplesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSamplesList.FormattingEnabled = true;
            this.lstSamplesList.Location = new System.Drawing.Point(6, 12);
            this.lstSamplesList.Name = "lstSamplesList";
            this.lstSamplesList.Size = new System.Drawing.Size(501, 459);
            this.lstSamplesList.TabIndex = 0;
            // 
            // MissingSamplesFound
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 519);
            this.Controls.Add(this.grbSamples);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MissingSamplesFound";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Missing Samples Found";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MissingSamplesFound_FormClosing);
            this.Load += new System.EventHandler(this.MissingSamplesFound_Load);
            this.grbSamples.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSamples;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.ListBox lstSamplesList;
    }
}
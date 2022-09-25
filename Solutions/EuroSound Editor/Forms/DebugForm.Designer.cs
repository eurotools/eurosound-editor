
namespace sb_editor.Forms
{
    partial class DebugForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugForm));
            this.grbDebugData = new System.Windows.Forms.GroupBox();
            this.txtDebugData = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.grbDebugData.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDebugData
            // 
            this.grbDebugData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbDebugData.Controls.Add(this.txtDebugData);
            this.grbDebugData.Location = new System.Drawing.Point(12, 12);
            this.grbDebugData.Name = "grbDebugData";
            this.grbDebugData.Size = new System.Drawing.Size(367, 330);
            this.grbDebugData.TabIndex = 0;
            this.grbDebugData.TabStop = false;
            // 
            // txtDebugData
            // 
            this.txtDebugData.BackColor = System.Drawing.SystemColors.Window;
            this.txtDebugData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebugData.Location = new System.Drawing.Point(3, 16);
            this.txtDebugData.Multiline = true;
            this.txtDebugData.Name = "txtDebugData";
            this.txtDebugData.ReadOnly = true;
            this.txtDebugData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDebugData.Size = new System.Drawing.Size(361, 311);
            this.txtDebugData.TabIndex = 0;
            this.txtDebugData.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(267, 348);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 33);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // DebugForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 393);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grbDebugData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DebugForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Debug Data";
            this.grbDebugData.ResumeLayout(false);
            this.grbDebugData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDebugData;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtDebugData;
    }
}
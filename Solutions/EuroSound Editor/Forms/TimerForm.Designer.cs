
namespace EuroSound_Editor.Forms
{
    partial class TimerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimerForm));
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.grbTaskTime = new System.Windows.Forms.GroupBox();
            this.grbTaskTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar1.Location = new System.Drawing.Point(5, 29);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(691, 23);
            this.ProgressBar1.TabIndex = 0;
            // 
            // grbTaskTime
            // 
            this.grbTaskTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTaskTime.Controls.Add(this.ProgressBar1);
            this.grbTaskTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbTaskTime.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grbTaskTime.Location = new System.Drawing.Point(7, 6);
            this.grbTaskTime.Margin = new System.Windows.Forms.Padding(2);
            this.grbTaskTime.Name = "grbTaskTime";
            this.grbTaskTime.Size = new System.Drawing.Size(701, 60);
            this.grbTaskTime.TabIndex = 3;
            this.grbTaskTime.TabStop = false;
            this.grbTaskTime.Text = "EuroSound Task Time Remaining:";
            // 
            // TimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 72);
            this.Controls.Add(this.grbTaskTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound Timer Form";
            this.Load += new System.EventHandler(this.Frm_TimerForm_Load);
            this.grbTaskTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.ProgressBar ProgressBar1;
        protected internal System.Windows.Forms.GroupBox grbTaskTime;
    }
}
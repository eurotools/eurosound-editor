
namespace EuroSound_Editor.Forms
{
    partial class Frm_RefineList
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
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.grbTaskTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Size = new System.Drawing.Size(676, 23);
            // 
            // grbTaskTime
            // 
            this.grbTaskTime.Size = new System.Drawing.Size(687, 60);
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerReportsProgress = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // Frm_RefineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 67);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Frm_RefineList";
            this.ShowInTaskbar = false;
            this.Text = "Updating the SFX Refine List.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_RefineList_FormClosing);
            this.Shown += new System.EventHandler(this.Frm_RefineList_Shown);
            this.grbTaskTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.ComponentModel.BackgroundWorker BackgroundWorker;
    }
}
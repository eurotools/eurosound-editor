
namespace sb_editor.Forms
{
    partial class PCDllVoicesForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Played = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Playing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Looping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reverb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stopped = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Active,
            this.Played,
            this.Playing,
            this.Looping,
            this.Reverb,
            this.Stop,
            this.Stopped,
            this.Locked});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(515, 635);
            this.dataGridView1.TabIndex = 2;
            // 
            // Index
            // 
            this.Index.FillWeight = 91.37055F;
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 58;
            // 
            // Active
            // 
            this.Active.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Active.FillWeight = 106.549F;
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            // 
            // Played
            // 
            this.Played.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Played.FillWeight = 106.549F;
            this.Played.HeaderText = "Played";
            this.Played.Name = "Played";
            this.Played.ReadOnly = true;
            // 
            // Playing
            // 
            this.Playing.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Playing.FillWeight = 106.549F;
            this.Playing.HeaderText = "Playing";
            this.Playing.Name = "Playing";
            this.Playing.ReadOnly = true;
            // 
            // Looping
            // 
            this.Looping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Looping.FillWeight = 106.549F;
            this.Looping.HeaderText = "Looping";
            this.Looping.Name = "Looping";
            this.Looping.ReadOnly = true;
            // 
            // Reverb
            // 
            this.Reverb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Reverb.FillWeight = 106.549F;
            this.Reverb.HeaderText = "Reverb";
            this.Reverb.Name = "Reverb";
            this.Reverb.ReadOnly = true;
            // 
            // Stop
            // 
            this.Stop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Stop.FillWeight = 83.62036F;
            this.Stop.HeaderText = "Stop";
            this.Stop.Name = "Stop";
            this.Stop.ReadOnly = true;
            // 
            // Stopped
            // 
            this.Stopped.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Stopped.FillWeight = 85.71536F;
            this.Stopped.HeaderText = "Stopped";
            this.Stopped.Name = "Stopped";
            this.Stopped.ReadOnly = true;
            // 
            // Locked
            // 
            this.Locked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Locked.FillWeight = 106.549F;
            this.Locked.HeaderText = "Locked";
            this.Locked.Name = "Locked";
            this.Locked.ReadOnly = true;
            // 
            // PCDllVoicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 635);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PCDllVoicesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PC DLL Voice View";
            this.Load += new System.EventHandler(this.PCDllVoicesForm_Load);
            this.Shown += new System.EventHandler(this.PCDllVoicesForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn Played;
        private System.Windows.Forms.DataGridViewTextBoxColumn Playing;
        private System.Windows.Forms.DataGridViewTextBoxColumn Looping;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reverb;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stopped;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locked;
    }
}
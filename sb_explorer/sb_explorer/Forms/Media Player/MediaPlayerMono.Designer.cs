
namespace sb_explorer.MediaPlayer
{
    partial class MediaPlayerMono
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaPlayerMono));
            this.Button_Stop = new System.Windows.Forms.Button();
            this.Button_Play = new System.Windows.Forms.Button();
            this.Button_Close = new System.Windows.Forms.Button();
            this.WavesViewer = new sb_explorer.CustomControls.EuroSound_WaveViewer();
            this.Button_SaveWav = new System.Windows.Forms.Button();
            this.SaveFileDlg_SaveFile = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // Button_Stop
            // 
            this.Button_Stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Stop.Location = new System.Drawing.Point(368, 198);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(75, 23);
            this.Button_Stop.TabIndex = 3;
            this.Button_Stop.Text = "Stop";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // Button_Play
            // 
            this.Button_Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Play.Location = new System.Drawing.Point(287, 198);
            this.Button_Play.Name = "Button_Play";
            this.Button_Play.Size = new System.Drawing.Size(75, 23);
            this.Button_Play.TabIndex = 2;
            this.Button_Play.Text = "Play";
            this.Button_Play.UseVisualStyleBackColor = true;
            this.Button_Play.Click += new System.EventHandler(this.Button_Play_Click);
            // 
            // Button_Close
            // 
            this.Button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Close.Location = new System.Drawing.Point(449, 198);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(75, 23);
            this.Button_Close.TabIndex = 4;
            this.Button_Close.Text = "Close";
            this.Button_Close.UseVisualStyleBackColor = true;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // WavesViewer
            // 
            this.WavesViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WavesViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.WavesViewer.CurrentWaveImage = null;
            this.WavesViewer.Location = new System.Drawing.Point(12, 12);
            this.WavesViewer.Name = "WavesViewer";
            this.WavesViewer.PenWidth = 1F;
            this.WavesViewer.SamplesPerPixel = 128;
            this.WavesViewer.Size = new System.Drawing.Size(512, 180);
            this.WavesViewer.StartPosition = ((long)(0));
            this.WavesViewer.TabIndex = 0;
            this.WavesViewer.WaveStream = null;
            // 
            // Button_SaveWav
            // 
            this.Button_SaveWav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SaveWav.Location = new System.Drawing.Point(206, 198);
            this.Button_SaveWav.Name = "Button_SaveWav";
            this.Button_SaveWav.Size = new System.Drawing.Size(75, 23);
            this.Button_SaveWav.TabIndex = 1;
            this.Button_SaveWav.Text = "Save";
            this.Button_SaveWav.UseVisualStyleBackColor = true;
            this.Button_SaveWav.Click += new System.EventHandler(this.Button_SaveWav_Click);
            // 
            // MediaPlayerMono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Close;
            this.ClientSize = new System.Drawing.Size(536, 233);
            this.Controls.Add(this.Button_SaveWav);
            this.Controls.Add(this.Button_Close);
            this.Controls.Add(this.Button_Play);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.WavesViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MediaPlayerMono";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Media Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MediaPlayerMono_FormClosing);
            this.Shown += new System.EventHandler(this.MediaPlayerMono_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.EuroSound_WaveViewer WavesViewer;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.Button Button_Play;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.Button Button_SaveWav;
        private System.Windows.Forms.SaveFileDialog SaveFileDlg_SaveFile;
    }
}
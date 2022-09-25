
namespace EuroSound_Editor
{
    partial class Splash
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.picSplashImage = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tmrSplash = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picSplashImage)).BeginInit();
            this.SuspendLayout();
            // 
            // picSplashImage
            // 
            this.picSplashImage.Image = ((System.Drawing.Image)(resources.GetObject("picSplashImage.Image")));
            this.picSplashImage.Location = new System.Drawing.Point(0, 0);
            this.picSplashImage.Name = "picSplashImage";
            this.picSplashImage.Size = new System.Drawing.Size(680, 520);
            this.picSplashImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSplashImage.TabIndex = 0;
            this.picSplashImage.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.White;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(542, 461);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(86, 19);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Version: 0";
            // 
            // tmrSplash
            // 
            this.tmrSplash.Interval = 900;
            this.tmrSplash.Tick += new System.EventHandler(this.TimerSplash_Tick);
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 520);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.picSplashImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(680, 520);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(680, 520);
            this.Name = "Splash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EuroSound";
            this.Load += new System.EventHandler(this.Splash_Load);
            this.Shown += new System.EventHandler(this.Frm_Splash_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picSplashImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picSplashImage;
        private System.Windows.Forms.Label lblVersion;
        internal System.Windows.Forms.Timer tmrSplash;
    }
}


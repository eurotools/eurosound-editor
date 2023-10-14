
namespace sb_editor.Panels
{
    partial class UserControl_MainForm_Misc
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grbMisc = new System.Windows.Forms.GroupBox();
            this.txtMisc_Debug = new System.Windows.Forms.TextBox();
            this.btnConsoleApp = new System.Windows.Forms.Button();
            this.btnMisc_Advanced = new System.Windows.Forms.Button();
            this.Music_Misc_SfxDefault = new System.Windows.Forms.Button();
            this.btnMisc_MusicMaker = new System.Windows.Forms.Button();
            this.btnMisc_ReSampling = new System.Windows.Forms.Button();
            this.btnMisc_Properties = new System.Windows.Forms.Button();
            this.ToolTip_Controls = new System.Windows.Forms.ToolTip(this.components);
            this.grbMisc.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMisc
            // 
            this.grbMisc.Controls.Add(this.txtMisc_Debug);
            this.grbMisc.Controls.Add(this.btnConsoleApp);
            this.grbMisc.Controls.Add(this.btnMisc_Advanced);
            this.grbMisc.Controls.Add(this.Music_Misc_SfxDefault);
            this.grbMisc.Controls.Add(this.btnMisc_MusicMaker);
            this.grbMisc.Controls.Add(this.btnMisc_ReSampling);
            this.grbMisc.Controls.Add(this.btnMisc_Properties);
            this.grbMisc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbMisc.Location = new System.Drawing.Point(0, 0);
            this.grbMisc.Name = "grbMisc";
            this.grbMisc.Size = new System.Drawing.Size(172, 221);
            this.grbMisc.TabIndex = 0;
            this.grbMisc.TabStop = false;
            this.grbMisc.Text = "Misc";
            // 
            // txtMisc_Debug
            // 
            this.txtMisc_Debug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMisc_Debug.Location = new System.Drawing.Point(6, 193);
            this.txtMisc_Debug.Name = "txtMisc_Debug";
            this.txtMisc_Debug.Size = new System.Drawing.Size(160, 20);
            this.txtMisc_Debug.TabIndex = 6;
            this.txtMisc_Debug.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TxtMisc_Debug_MouseDoubleClick);
            // 
            // btnConsoleApp
            // 
            this.btnConsoleApp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsoleApp.Location = new System.Drawing.Point(6, 164);
            this.btnConsoleApp.Name = "btnConsoleApp";
            this.btnConsoleApp.Size = new System.Drawing.Size(160, 23);
            this.btnConsoleApp.TabIndex = 5;
            this.btnConsoleApp.Text = "Console App";
            this.btnConsoleApp.UseVisualStyleBackColor = true;
            this.btnConsoleApp.Click += new System.EventHandler(this.BtnMisc_MarkersEditor_Click);
            // 
            // btnMisc_Advanced
            // 
            this.btnMisc_Advanced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMisc_Advanced.Location = new System.Drawing.Point(6, 135);
            this.btnMisc_Advanced.Name = "btnMisc_Advanced";
            this.btnMisc_Advanced.Size = new System.Drawing.Size(160, 23);
            this.btnMisc_Advanced.TabIndex = 4;
            this.btnMisc_Advanced.Text = "Advanced";
            this.btnMisc_Advanced.UseVisualStyleBackColor = true;
            this.btnMisc_Advanced.Click += new System.EventHandler(this.BtnMisc_Advanced_Click);
            // 
            // Music_Misc_SfxDefault
            // 
            this.Music_Misc_SfxDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Music_Misc_SfxDefault.Location = new System.Drawing.Point(6, 106);
            this.Music_Misc_SfxDefault.Name = "Music_Misc_SfxDefault";
            this.Music_Misc_SfxDefault.Size = new System.Drawing.Size(160, 23);
            this.Music_Misc_SfxDefault.TabIndex = 3;
            this.Music_Misc_SfxDefault.Text = "Default SFX";
            this.Music_Misc_SfxDefault.UseVisualStyleBackColor = true;
            this.Music_Misc_SfxDefault.Click += new System.EventHandler(this.Music_Misc_SfxDefault_Click);
            // 
            // btnMisc_MusicMaker
            // 
            this.btnMisc_MusicMaker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMisc_MusicMaker.Location = new System.Drawing.Point(6, 77);
            this.btnMisc_MusicMaker.Name = "btnMisc_MusicMaker";
            this.btnMisc_MusicMaker.Size = new System.Drawing.Size(160, 23);
            this.btnMisc_MusicMaker.TabIndex = 2;
            this.btnMisc_MusicMaker.Text = "Music Maker";
            this.btnMisc_MusicMaker.UseVisualStyleBackColor = true;
            this.btnMisc_MusicMaker.Click += new System.EventHandler(this.BtnMisc_MusicMaker_Click);
            // 
            // btnMisc_ReSampling
            // 
            this.btnMisc_ReSampling.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMisc_ReSampling.Location = new System.Drawing.Point(6, 48);
            this.btnMisc_ReSampling.Name = "btnMisc_ReSampling";
            this.btnMisc_ReSampling.Size = new System.Drawing.Size(160, 23);
            this.btnMisc_ReSampling.TabIndex = 1;
            this.btnMisc_ReSampling.Text = "Re-Sampling";
            this.ToolTip_Controls.SetToolTip(this.btnMisc_ReSampling, "Open Sample Re-Sampling Window");
            this.btnMisc_ReSampling.UseVisualStyleBackColor = true;
            this.btnMisc_ReSampling.Click += new System.EventHandler(this.BtnMisc_ReSampling_Click);
            // 
            // btnMisc_Properties
            // 
            this.btnMisc_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMisc_Properties.Location = new System.Drawing.Point(6, 19);
            this.btnMisc_Properties.Name = "btnMisc_Properties";
            this.btnMisc_Properties.Size = new System.Drawing.Size(160, 23);
            this.btnMisc_Properties.TabIndex = 0;
            this.btnMisc_Properties.Text = "Properties";
            this.ToolTip_Controls.SetToolTip(this.btnMisc_Properties, "Open Project Properties Window");
            this.btnMisc_Properties.UseVisualStyleBackColor = true;
            this.btnMisc_Properties.Click += new System.EventHandler(this.BtnMisc_Properties_Click);
            // 
            // UserControl_MainForm_Misc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbMisc);
            this.Name = "UserControl_MainForm_Misc";
            this.Size = new System.Drawing.Size(172, 221);
            this.grbMisc.ResumeLayout(false);
            this.grbMisc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMisc;
        private System.Windows.Forms.Button btnConsoleApp;
        private System.Windows.Forms.Button btnMisc_Advanced;
        private System.Windows.Forms.Button Music_Misc_SfxDefault;
        private System.Windows.Forms.Button btnMisc_MusicMaker;
        private System.Windows.Forms.Button btnMisc_ReSampling;
        private System.Windows.Forms.Button btnMisc_Properties;
        private System.Windows.Forms.ToolTip ToolTip_Controls;
        protected internal System.Windows.Forms.TextBox txtMisc_Debug;
    }
}


namespace sb_editor
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.GroupBox_About = new System.Windows.Forms.GroupBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label_Credits = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_GetUpdate = new System.Windows.Forms.Button();
            this.Label_Title = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.GroupBox_About.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox_About
            // 
            this.GroupBox_About.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_About.Controls.Add(this.Panel1);
            this.GroupBox_About.Controls.Add(this.Button_OK);
            this.GroupBox_About.Controls.Add(this.Button_GetUpdate);
            this.GroupBox_About.Controls.Add(this.Label_Title);
            this.GroupBox_About.Controls.Add(this.lblCurrentVersion);
            this.GroupBox_About.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_About.Name = "GroupBox_About";
            this.GroupBox_About.Size = new System.Drawing.Size(355, 338);
            this.GroupBox_About.TabIndex = 1;
            this.GroupBox_About.TabStop = false;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel1.Controls.Add(this.Label_Credits);
            this.Panel1.Location = new System.Drawing.Point(6, 127);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(343, 176);
            this.Panel1.TabIndex = 3;
            // 
            // Label_Credits
            // 
            this.Label_Credits.Location = new System.Drawing.Point(-2, 0);
            this.Label_Credits.Name = "Label_Credits";
            this.Label_Credits.Size = new System.Drawing.Size(343, 172);
            this.Label_Credits.TabIndex = 4;
            this.Label_Credits.Text = "\r\nProgrammer:\r\nJordi Martínez (Jmarti856)\r\n\r\nOriginal Tool Developers:\r\nEurocom D" +
    "evelopments 2002\r\n\r\nGameCube dsp Tool:\r\nCopyright (c) 2017 Alex Barney\r\n\r\nSpecia" +
    "l Thanks:\r\nIsmael Ferreras (Swyter)";
            this.Label_Credits.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(140, 309);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 5;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            // 
            // Button_GetUpdate
            // 
            this.Button_GetUpdate.Location = new System.Drawing.Point(121, 98);
            this.Button_GetUpdate.Name = "Button_GetUpdate";
            this.Button_GetUpdate.Size = new System.Drawing.Size(107, 23);
            this.Button_GetUpdate.TabIndex = 2;
            this.Button_GetUpdate.Text = "Check Last Version";
            this.Button_GetUpdate.UseVisualStyleBackColor = true;
            this.Button_GetUpdate.Click += new System.EventHandler(this.Button_GetUpdate_Click);
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(56, 16);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(232, 20);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "Multi Format SFX creation Tool.";
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentVersion.Location = new System.Drawing.Point(6, 75);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(343, 20);
            this.lblCurrentVersion.TabIndex = 1;
            this.lblCurrentVersion.Text = "This Version: Pending...";
            this.lblCurrentVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // HelpForm
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 362);
            this.Controls.Add(this.GroupBox_About);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound Help Form";
            this.Load += new System.EventHandler(this.HelpForm_Load);
            this.GroupBox_About.ResumeLayout(false);
            this.GroupBox_About.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox_About;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label_Credits;
        internal System.Windows.Forms.Button Button_OK;
        internal System.Windows.Forms.Button Button_GetUpdate;
        internal System.Windows.Forms.Label Label_Title;
        internal System.Windows.Forms.Label lblCurrentVersion;
    }
}
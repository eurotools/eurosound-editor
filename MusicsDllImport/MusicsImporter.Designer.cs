
namespace MusicsDllImport
{
    partial class MusicsImporter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicsImporter));
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Import = new System.Windows.Forms.Button();
            this.TextBox_DestinationFolder = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button_DlcFolder = new System.Windows.Forms.Button();
            this.Textbox_DlcFolderPath = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnOutFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(457, 87);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 30);
            this.Button_OK.TabIndex = 15;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Import
            // 
            this.Button_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Import.Location = new System.Drawing.Point(376, 87);
            this.Button_Import.Name = "Button_Import";
            this.Button_Import.Size = new System.Drawing.Size(75, 30);
            this.Button_Import.TabIndex = 13;
            this.Button_Import.Text = "Import!";
            this.Button_Import.UseVisualStyleBackColor = true;
            this.Button_Import.Click += new System.EventHandler(this.Button_Import_Click);
            // 
            // TextBox_DestinationFolder
            // 
            this.TextBox_DestinationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_DestinationFolder.BackColor = System.Drawing.SystemColors.Window;
            this.TextBox_DestinationFolder.Location = new System.Drawing.Point(113, 32);
            this.TextBox_DestinationFolder.Name = "TextBox_DestinationFolder";
            this.TextBox_DestinationFolder.ReadOnly = true;
            this.TextBox_DestinationFolder.Size = new System.Drawing.Size(388, 20);
            this.TextBox_DestinationFolder.TabIndex = 12;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 35);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(95, 13);
            this.Label2.TabIndex = 11;
            this.Label2.Text = "Destination Folder:";
            // 
            // Button_DlcFolder
            // 
            this.Button_DlcFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_DlcFolder.Location = new System.Drawing.Point(507, 6);
            this.Button_DlcFolder.Name = "Button_DlcFolder";
            this.Button_DlcFolder.Size = new System.Drawing.Size(25, 20);
            this.Button_DlcFolder.TabIndex = 10;
            this.Button_DlcFolder.Text = "...";
            this.Button_DlcFolder.UseVisualStyleBackColor = true;
            this.Button_DlcFolder.Click += new System.EventHandler(this.Button_DlcFolder_Click);
            // 
            // Textbox_DlcFolderPath
            // 
            this.Textbox_DlcFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_DlcFolderPath.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_DlcFolderPath.Location = new System.Drawing.Point(81, 6);
            this.Textbox_DlcFolderPath.Name = "Textbox_DlcFolderPath";
            this.Textbox_DlcFolderPath.ReadOnly = true;
            this.Textbox_DlcFolderPath.Size = new System.Drawing.Size(420, 20);
            this.Textbox_DlcFolderPath.TabIndex = 9;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(63, 13);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "DLC Folder:";
            // 
            // btnOutFolder
            // 
            this.btnOutFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutFolder.Location = new System.Drawing.Point(507, 32);
            this.btnOutFolder.Name = "btnOutFolder";
            this.btnOutFolder.Size = new System.Drawing.Size(25, 20);
            this.btnOutFolder.TabIndex = 16;
            this.btnOutFolder.Text = "...";
            this.btnOutFolder.UseVisualStyleBackColor = true;
            this.btnOutFolder.Click += new System.EventHandler(this.BtnOutFolder_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 58);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(520, 23);
            this.progressBar1.TabIndex = 17;
            // 
            // MusicsImporter
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 125);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnOutFolder);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Button_Import);
            this.Controls.Add(this.TextBox_DestinationFolder);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Button_DlcFolder);
            this.Controls.Add(this.Textbox_DlcFolderPath);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MusicsImporter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Musics From DLC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MusicsImporter_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button_OK;
        internal System.Windows.Forms.Button Button_Import;
        internal System.Windows.Forms.TextBox TextBox_DestinationFolder;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button Button_DlcFolder;
        internal System.Windows.Forms.TextBox Textbox_DlcFolderPath;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnOutFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}


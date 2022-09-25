
namespace sb_editor.Forms
{
    partial class DataBasePropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataBasePropertiesForm));
            this.grbMargins = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDependenciesCount = new System.Windows.Forms.Label();
            this.lstDependencies = new System.Windows.Forms.ListBox();
            this.lblDataBase_Dependencies = new System.Windows.Forms.Label();
            this.lblSamplesCount = new System.Windows.Forms.Label();
            this.lblTotalSamples = new System.Windows.Forms.Label();
            this.lstTotalSamples = new System.Windows.Forms.ListBox();
            this.lblSFXsCount = new System.Windows.Forms.Label();
            this.lstSFXs = new System.Windows.Forms.ListBox();
            this.lblTotalSFXs = new System.Windows.Forms.Label();
            this.lblDataBaseName = new System.Windows.Forms.Label();
            this.lblTotalSampleSize_Value = new System.Windows.Forms.Label();
            this.lblDataBaseName_Value = new System.Windows.Forms.Label();
            this.lblTotalSampleSize = new System.Windows.Forms.Label();
            this.lblFileInfo1 = new System.Windows.Forms.Label();
            this.lblSampleCount_Value = new System.Windows.Forms.Label();
            this.lblFileInfo1_Value = new System.Windows.Forms.Label();
            this.lblSampleCount = new System.Windows.Forms.Label();
            this.lblFileInfo2 = new System.Windows.Forms.Label();
            this.lblSFXCount_Value = new System.Windows.Forms.Label();
            this.lblFileInfo2_Value = new System.Windows.Forms.Label();
            this.lblSFXCount = new System.Windows.Forms.Label();
            this.lblFileInfo3 = new System.Windows.Forms.Label();
            this.lblDatabaseCount_Value = new System.Windows.Forms.Label();
            this.lblFileInfo3_Value = new System.Windows.Forms.Label();
            this.lblDatabaseCount = new System.Windows.Forms.Label();
            this.lblFileInfo4 = new System.Windows.Forms.Label();
            this.lblFileInfo4_Value = new System.Windows.Forms.Label();
            this.grbMargins.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMargins
            // 
            this.grbMargins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMargins.Controls.Add(this.lblDataBaseName);
            this.grbMargins.Controls.Add(this.lblTotalSampleSize_Value);
            this.grbMargins.Controls.Add(this.btnOK);
            this.grbMargins.Controls.Add(this.lblDataBaseName_Value);
            this.grbMargins.Controls.Add(this.lblDependenciesCount);
            this.grbMargins.Controls.Add(this.lblTotalSampleSize);
            this.grbMargins.Controls.Add(this.lstDependencies);
            this.grbMargins.Controls.Add(this.lblFileInfo1);
            this.grbMargins.Controls.Add(this.lblDataBase_Dependencies);
            this.grbMargins.Controls.Add(this.lblSampleCount_Value);
            this.grbMargins.Controls.Add(this.lblSamplesCount);
            this.grbMargins.Controls.Add(this.lblFileInfo1_Value);
            this.grbMargins.Controls.Add(this.lblTotalSamples);
            this.grbMargins.Controls.Add(this.lblSampleCount);
            this.grbMargins.Controls.Add(this.lstTotalSamples);
            this.grbMargins.Controls.Add(this.lblFileInfo2);
            this.grbMargins.Controls.Add(this.lblSFXsCount);
            this.grbMargins.Controls.Add(this.lblSFXCount_Value);
            this.grbMargins.Controls.Add(this.lstSFXs);
            this.grbMargins.Controls.Add(this.lblFileInfo2_Value);
            this.grbMargins.Controls.Add(this.lblTotalSFXs);
            this.grbMargins.Controls.Add(this.lblSFXCount);
            this.grbMargins.Controls.Add(this.lblFileInfo4_Value);
            this.grbMargins.Controls.Add(this.lblFileInfo3);
            this.grbMargins.Controls.Add(this.lblFileInfo4);
            this.grbMargins.Controls.Add(this.lblDatabaseCount_Value);
            this.grbMargins.Controls.Add(this.lblDatabaseCount);
            this.grbMargins.Controls.Add(this.lblFileInfo3_Value);
            this.grbMargins.Location = new System.Drawing.Point(12, 8);
            this.grbMargins.Name = "grbMargins";
            this.grbMargins.Size = new System.Drawing.Size(613, 602);
            this.grbMargins.TabIndex = 0;
            this.grbMargins.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(281, 573);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 27;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblDependenciesCount
            // 
            this.lblDependenciesCount.AutoSize = true;
            this.lblDependenciesCount.Location = new System.Drawing.Point(307, 259);
            this.lblDependenciesCount.Name = "lblDependenciesCount";
            this.lblDependenciesCount.Size = new System.Drawing.Size(43, 13);
            this.lblDependenciesCount.TabIndex = 26;
            this.lblDependenciesCount.Text = "Total: 0";
            // 
            // lstDependencies
            // 
            this.lstDependencies.FormattingEnabled = true;
            this.lstDependencies.HorizontalScrollbar = true;
            this.lstDependencies.Location = new System.Drawing.Point(310, 31);
            this.lstDependencies.Name = "lstDependencies";
            this.lstDependencies.Size = new System.Drawing.Size(297, 225);
            this.lstDependencies.Sorted = true;
            this.lstDependencies.TabIndex = 25;
            // 
            // lblDataBase_Dependencies
            // 
            this.lblDataBase_Dependencies.AutoSize = true;
            this.lblDataBase_Dependencies.Location = new System.Drawing.Point(307, 15);
            this.lblDataBase_Dependencies.Name = "lblDataBase_Dependencies";
            this.lblDataBase_Dependencies.Size = new System.Drawing.Size(169, 13);
            this.lblDataBase_Dependencies.TabIndex = 24;
            this.lblDataBase_Dependencies.Text = "Sound Bank File Dependencies: 0";
            // 
            // lblSamplesCount
            // 
            this.lblSamplesCount.AutoSize = true;
            this.lblSamplesCount.Location = new System.Drawing.Point(307, 548);
            this.lblSamplesCount.Name = "lblSamplesCount";
            this.lblSamplesCount.Size = new System.Drawing.Size(43, 13);
            this.lblSamplesCount.TabIndex = 23;
            this.lblSamplesCount.Text = "Total: 0";
            // 
            // lblTotalSamples
            // 
            this.lblTotalSamples.AutoSize = true;
            this.lblTotalSamples.Location = new System.Drawing.Point(307, 304);
            this.lblTotalSamples.Name = "lblTotalSamples";
            this.lblTotalSamples.Size = new System.Drawing.Size(77, 13);
            this.lblTotalSamples.TabIndex = 22;
            this.lblTotalSamples.Text = "Total Samples:";
            // 
            // lstTotalSamples
            // 
            this.lstTotalSamples.FormattingEnabled = true;
            this.lstTotalSamples.HorizontalScrollbar = true;
            this.lstTotalSamples.Location = new System.Drawing.Point(310, 320);
            this.lstTotalSamples.Name = "lstTotalSamples";
            this.lstTotalSamples.Size = new System.Drawing.Size(297, 225);
            this.lstTotalSamples.Sorted = true;
            this.lstTotalSamples.TabIndex = 21;
            // 
            // lblSFXsCount
            // 
            this.lblSFXsCount.AutoSize = true;
            this.lblSFXsCount.Location = new System.Drawing.Point(12, 548);
            this.lblSFXsCount.Name = "lblSFXsCount";
            this.lblSFXsCount.Size = new System.Drawing.Size(43, 13);
            this.lblSFXsCount.TabIndex = 20;
            this.lblSFXsCount.Text = "Total: 0";
            // 
            // lstSFXs
            // 
            this.lstSFXs.FormattingEnabled = true;
            this.lstSFXs.HorizontalScrollbar = true;
            this.lstSFXs.Location = new System.Drawing.Point(9, 320);
            this.lstSFXs.Name = "lstSFXs";
            this.lstSFXs.Size = new System.Drawing.Size(295, 225);
            this.lstSFXs.Sorted = true;
            this.lstSFXs.TabIndex = 19;
            // 
            // lblTotalSFXs
            // 
            this.lblTotalSFXs.AutoSize = true;
            this.lblTotalSFXs.Location = new System.Drawing.Point(6, 304);
            this.lblTotalSFXs.Name = "lblTotalSFXs";
            this.lblTotalSFXs.Size = new System.Drawing.Size(62, 13);
            this.lblTotalSFXs.TabIndex = 18;
            this.lblTotalSFXs.Text = "Total SFXs:";
            // 
            // lblDataBaseName
            // 
            this.lblDataBaseName.AutoSize = true;
            this.lblDataBaseName.Location = new System.Drawing.Point(6, 16);
            this.lblDataBaseName.Name = "lblDataBaseName";
            this.lblDataBaseName.Size = new System.Drawing.Size(38, 13);
            this.lblDataBaseName.TabIndex = 36;
            this.lblDataBaseName.Text = "Name:";
            // 
            // lblTotalSampleSize_Value
            // 
            this.lblTotalSampleSize_Value.AutoSize = true;
            this.lblTotalSampleSize_Value.Location = new System.Drawing.Point(152, 194);
            this.lblTotalSampleSize_Value.Name = "lblTotalSampleSize_Value";
            this.lblTotalSampleSize_Value.Size = new System.Drawing.Size(41, 13);
            this.lblTotalSampleSize_Value.TabIndex = 53;
            this.lblTotalSampleSize_Value.Text = "label15";
            // 
            // lblDataBaseName_Value
            // 
            this.lblDataBaseName_Value.AutoSize = true;
            this.lblDataBaseName_Value.Location = new System.Drawing.Point(152, 16);
            this.lblDataBaseName_Value.Name = "lblDataBaseName_Value";
            this.lblDataBaseName_Value.Size = new System.Drawing.Size(35, 13);
            this.lblDataBaseName_Value.TabIndex = 37;
            this.lblDataBaseName_Value.Text = "label1";
            // 
            // lblTotalSampleSize
            // 
            this.lblTotalSampleSize.AutoSize = true;
            this.lblTotalSampleSize.Location = new System.Drawing.Point(6, 194);
            this.lblTotalSampleSize.Name = "lblTotalSampleSize";
            this.lblTotalSampleSize.Size = new System.Drawing.Size(95, 13);
            this.lblTotalSampleSize.TabIndex = 52;
            this.lblTotalSampleSize.Text = "Total Sample Size:";
            // 
            // lblFileInfo1
            // 
            this.lblFileInfo1.AutoSize = true;
            this.lblFileInfo1.Location = new System.Drawing.Point(6, 48);
            this.lblFileInfo1.Name = "lblFileInfo1";
            this.lblFileInfo1.Size = new System.Drawing.Size(60, 13);
            this.lblFileInfo1.TabIndex = 38;
            this.lblFileInfo1.Text = "File Info #1";
            // 
            // lblSampleCount_Value
            // 
            this.lblSampleCount_Value.AutoSize = true;
            this.lblSampleCount_Value.Location = new System.Drawing.Point(152, 161);
            this.lblSampleCount_Value.Name = "lblSampleCount_Value";
            this.lblSampleCount_Value.Size = new System.Drawing.Size(35, 13);
            this.lblSampleCount_Value.TabIndex = 51;
            this.lblSampleCount_Value.Text = "label9";
            // 
            // lblFileInfo1_Value
            // 
            this.lblFileInfo1_Value.AutoSize = true;
            this.lblFileInfo1_Value.Location = new System.Drawing.Point(152, 48);
            this.lblFileInfo1_Value.Name = "lblFileInfo1_Value";
            this.lblFileInfo1_Value.Size = new System.Drawing.Size(35, 13);
            this.lblFileInfo1_Value.TabIndex = 39;
            this.lblFileInfo1_Value.Text = "label1";
            // 
            // lblSampleCount
            // 
            this.lblSampleCount.AutoSize = true;
            this.lblSampleCount.Location = new System.Drawing.Point(6, 161);
            this.lblSampleCount.Name = "lblSampleCount";
            this.lblSampleCount.Size = new System.Drawing.Size(76, 13);
            this.lblSampleCount.TabIndex = 50;
            this.lblSampleCount.Text = "Sample Count:";
            // 
            // lblFileInfo2
            // 
            this.lblFileInfo2.AutoSize = true;
            this.lblFileInfo2.Location = new System.Drawing.Point(6, 63);
            this.lblFileInfo2.Name = "lblFileInfo2";
            this.lblFileInfo2.Size = new System.Drawing.Size(60, 13);
            this.lblFileInfo2.TabIndex = 40;
            this.lblFileInfo2.Text = "File Info #2";
            // 
            // lblSFXCount_Value
            // 
            this.lblSFXCount_Value.AutoSize = true;
            this.lblSFXCount_Value.Location = new System.Drawing.Point(152, 146);
            this.lblSFXCount_Value.Name = "lblSFXCount_Value";
            this.lblSFXCount_Value.Size = new System.Drawing.Size(41, 13);
            this.lblSFXCount_Value.TabIndex = 49;
            this.lblSFXCount_Value.Text = "label11";
            // 
            // lblFileInfo2_Value
            // 
            this.lblFileInfo2_Value.AutoSize = true;
            this.lblFileInfo2_Value.Location = new System.Drawing.Point(152, 63);
            this.lblFileInfo2_Value.Name = "lblFileInfo2_Value";
            this.lblFileInfo2_Value.Size = new System.Drawing.Size(35, 13);
            this.lblFileInfo2_Value.TabIndex = 41;
            this.lblFileInfo2_Value.Text = "label3";
            // 
            // lblSFXCount
            // 
            this.lblSFXCount.AutoSize = true;
            this.lblSFXCount.Location = new System.Drawing.Point(6, 146);
            this.lblSFXCount.Name = "lblSFXCount";
            this.lblSFXCount.Size = new System.Drawing.Size(61, 13);
            this.lblSFXCount.TabIndex = 48;
            this.lblSFXCount.Text = "SFX Count:";
            // 
            // lblFileInfo3
            // 
            this.lblFileInfo3.AutoSize = true;
            this.lblFileInfo3.Location = new System.Drawing.Point(6, 78);
            this.lblFileInfo3.Name = "lblFileInfo3";
            this.lblFileInfo3.Size = new System.Drawing.Size(60, 13);
            this.lblFileInfo3.TabIndex = 42;
            this.lblFileInfo3.Text = "File Info #3";
            // 
            // lblDatabaseCount_Value
            // 
            this.lblDatabaseCount_Value.AutoSize = true;
            this.lblDatabaseCount_Value.Location = new System.Drawing.Point(152, 131);
            this.lblDatabaseCount_Value.Name = "lblDatabaseCount_Value";
            this.lblDatabaseCount_Value.Size = new System.Drawing.Size(41, 13);
            this.lblDatabaseCount_Value.TabIndex = 47;
            this.lblDatabaseCount_Value.Text = "label13";
            // 
            // lblFileInfo3_Value
            // 
            this.lblFileInfo3_Value.AutoSize = true;
            this.lblFileInfo3_Value.Location = new System.Drawing.Point(152, 78);
            this.lblFileInfo3_Value.Name = "lblFileInfo3_Value";
            this.lblFileInfo3_Value.Size = new System.Drawing.Size(35, 13);
            this.lblFileInfo3_Value.TabIndex = 43;
            this.lblFileInfo3_Value.Text = "label5";
            // 
            // lblDatabaseCount
            // 
            this.lblDatabaseCount.AutoSize = true;
            this.lblDatabaseCount.Location = new System.Drawing.Point(6, 131);
            this.lblDatabaseCount.Name = "lblDatabaseCount";
            this.lblDatabaseCount.Size = new System.Drawing.Size(87, 13);
            this.lblDatabaseCount.TabIndex = 46;
            this.lblDatabaseCount.Text = "Database Count:";
            // 
            // lblFileInfo4
            // 
            this.lblFileInfo4.AutoSize = true;
            this.lblFileInfo4.Location = new System.Drawing.Point(6, 93);
            this.lblFileInfo4.Name = "lblFileInfo4";
            this.lblFileInfo4.Size = new System.Drawing.Size(60, 13);
            this.lblFileInfo4.TabIndex = 44;
            this.lblFileInfo4.Text = "File Info #4";
            // 
            // lblFileInfo4_Value
            // 
            this.lblFileInfo4_Value.AutoSize = true;
            this.lblFileInfo4_Value.Location = new System.Drawing.Point(152, 93);
            this.lblFileInfo4_Value.Name = "lblFileInfo4_Value";
            this.lblFileInfo4_Value.Size = new System.Drawing.Size(35, 13);
            this.lblFileInfo4_Value.TabIndex = 45;
            this.lblFileInfo4_Value.Text = "label7";
            // 
            // DataBasePropertiesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 622);
            this.Controls.Add(this.grbMargins);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataBasePropertiesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DataBase Properties";
            this.Load += new System.EventHandler(this.Frm_DataBaseProperties_Load);
            this.grbMargins.ResumeLayout(false);
            this.grbMargins.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMargins;
        private System.Windows.Forms.ListBox lstSFXs;
        private System.Windows.Forms.Label lblTotalSFXs;
        private System.Windows.Forms.Label lblSFXsCount;
        private System.Windows.Forms.Label lblSamplesCount;
        private System.Windows.Forms.Label lblTotalSamples;
        private System.Windows.Forms.ListBox lstTotalSamples;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDependenciesCount;
        private System.Windows.Forms.ListBox lstDependencies;
        private System.Windows.Forms.Label lblDataBase_Dependencies;
        private System.Windows.Forms.Label lblDataBaseName;
        private System.Windows.Forms.Label lblTotalSampleSize_Value;
        private System.Windows.Forms.Label lblDataBaseName_Value;
        private System.Windows.Forms.Label lblTotalSampleSize;
        private System.Windows.Forms.Label lblFileInfo1;
        private System.Windows.Forms.Label lblSampleCount_Value;
        private System.Windows.Forms.Label lblFileInfo1_Value;
        private System.Windows.Forms.Label lblSampleCount;
        private System.Windows.Forms.Label lblFileInfo2;
        private System.Windows.Forms.Label lblSFXCount_Value;
        private System.Windows.Forms.Label lblFileInfo2_Value;
        private System.Windows.Forms.Label lblSFXCount;
        private System.Windows.Forms.Label lblFileInfo4_Value;
        private System.Windows.Forms.Label lblFileInfo3;
        private System.Windows.Forms.Label lblFileInfo4;
        private System.Windows.Forms.Label lblDatabaseCount_Value;
        private System.Windows.Forms.Label lblDatabaseCount;
        private System.Windows.Forms.Label lblFileInfo3_Value;
    }
}

namespace EuroSound_Editor.Forms
{
    partial class MultiEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiEditor));
            this.lvwItems = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReverb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTracking = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInnerRad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOuterRad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSteal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPriority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAlertness = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDucker = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDuckLen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMasterVol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUnderWater = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStealLouder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nudReverb = new System.Windows.Forms.NumericUpDown();
            this.cboTrackingType = new System.Windows.Forms.ComboBox();
            this.nudInnerRad = new System.Windows.Forms.NumericUpDown();
            this.nudOuterRad = new System.Windows.Forms.NumericUpDown();
            this.nudMaxVoices = new System.Windows.Forms.NumericUpDown();
            this.cboSteal = new System.Windows.Forms.ComboBox();
            this.nudPriority = new System.Windows.Forms.NumericUpDown();
            this.nudAlertness = new System.Windows.Forms.NumericUpDown();
            this.nudDucker = new System.Windows.Forms.NumericUpDown();
            this.nudDuckerLength = new System.Windows.Forms.NumericUpDown();
            this.nudMasterVol = new System.Windows.Forms.NumericUpDown();
            this.cboUnderWater = new System.Windows.Forms.ComboBox();
            this.cboStealOnLouder = new System.Windows.Forms.ComboBox();
            this.grbLocked = new System.Windows.Forms.GroupBox();
            this.chkApplyAllFormats = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOpenAsExcel = new System.Windows.Forms.Button();
            this.cboCombo3 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudReverb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInnerRad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOuterRad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxVoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlertness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDucker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckerLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterVol)).BeginInit();
            this.grbLocked.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwItems
            // 
            this.lvwItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colReverb,
            this.colTracking,
            this.colInnerRad,
            this.colOuterRad,
            this.colMax,
            this.colSteal,
            this.colPriority,
            this.colAlertness,
            this.colDucker,
            this.colDuckLen,
            this.colMasterVol,
            this.colUnderWater,
            this.colStealLouder});
            this.lvwItems.FullRowSelect = true;
            this.lvwItems.GridLines = true;
            this.lvwItems.HideSelection = false;
            this.lvwItems.Location = new System.Drawing.Point(12, 32);
            this.lvwItems.Name = "lvwItems";
            this.lvwItems.Size = new System.Drawing.Size(1250, 415);
            this.lvwItems.TabIndex = 0;
            this.lvwItems.UseCompatibleStateImageBehavior = false;
            this.lvwItems.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 260;
            // 
            // colReverb
            // 
            this.colReverb.Text = "Reverb";
            this.colReverb.Width = 75;
            // 
            // colTracking
            // 
            this.colTracking.Text = "Tracking";
            this.colTracking.Width = 75;
            // 
            // colInnerRad
            // 
            this.colInnerRad.Text = "Inner Rad.";
            this.colInnerRad.Width = 75;
            // 
            // colOuterRad
            // 
            this.colOuterRad.Text = "Outer Rad.";
            this.colOuterRad.Width = 75;
            // 
            // colMax
            // 
            this.colMax.Text = "Max";
            this.colMax.Width = 75;
            // 
            // colSteal
            // 
            this.colSteal.Text = "Steal?";
            this.colSteal.Width = 75;
            // 
            // colPriority
            // 
            this.colPriority.Text = "Priority";
            this.colPriority.Width = 75;
            // 
            // colAlertness
            // 
            this.colAlertness.Text = "Alertness";
            this.colAlertness.Width = 75;
            // 
            // colDucker
            // 
            this.colDucker.Text = "Ducker";
            this.colDucker.Width = 75;
            // 
            // colDuckLen
            // 
            this.colDuckLen.Text = "Duck Len";
            this.colDuckLen.Width = 75;
            // 
            // colMasterVol
            // 
            this.colMasterVol.Text = "Master Vol";
            this.colMasterVol.Width = 75;
            // 
            // colUnderWater
            // 
            this.colUnderWater.Text = "UnderWater";
            this.colUnderWater.Width = 75;
            // 
            // colStealLouder
            // 
            this.colStealLouder.Text = "Steal Louder";
            this.colStealLouder.Width = 75;
            // 
            // nudReverb
            // 
            this.nudReverb.Location = new System.Drawing.Point(276, 453);
            this.nudReverb.Name = "nudReverb";
            this.nudReverb.Size = new System.Drawing.Size(70, 20);
            this.nudReverb.TabIndex = 2;
            this.nudReverb.ValueChanged += new System.EventHandler(this.NudReverb_ValueChanged);
            // 
            // cboTrackingType
            // 
            this.cboTrackingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrackingType.FormattingEnabled = true;
            this.cboTrackingType.Items.AddRange(new object[] {
            "2d",
            "Amb",
            "3d",
            "3d No:HRTF",
            "2d PL2"});
            this.cboTrackingType.Location = new System.Drawing.Point(352, 452);
            this.cboTrackingType.Name = "cboTrackingType";
            this.cboTrackingType.Size = new System.Drawing.Size(68, 21);
            this.cboTrackingType.TabIndex = 3;
            this.cboTrackingType.SelectedIndexChanged += new System.EventHandler(this.CboTrackingType_SelectedIndexChanged);
            // 
            // nudInnerRad
            // 
            this.nudInnerRad.Location = new System.Drawing.Point(426, 453);
            this.nudInnerRad.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudInnerRad.Name = "nudInnerRad";
            this.nudInnerRad.Size = new System.Drawing.Size(70, 20);
            this.nudInnerRad.TabIndex = 4;
            this.nudInnerRad.ValueChanged += new System.EventHandler(this.NudInnerRad_ValueChanged);
            // 
            // nudOuterRad
            // 
            this.nudOuterRad.Location = new System.Drawing.Point(502, 453);
            this.nudOuterRad.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudOuterRad.Name = "nudOuterRad";
            this.nudOuterRad.Size = new System.Drawing.Size(70, 20);
            this.nudOuterRad.TabIndex = 5;
            this.nudOuterRad.ValueChanged += new System.EventHandler(this.NudOuterRad_ValueChanged);
            // 
            // nudMaxVoices
            // 
            this.nudMaxVoices.Location = new System.Drawing.Point(578, 453);
            this.nudMaxVoices.Name = "nudMaxVoices";
            this.nudMaxVoices.Size = new System.Drawing.Size(70, 20);
            this.nudMaxVoices.TabIndex = 6;
            this.nudMaxVoices.ValueChanged += new System.EventHandler(this.NudMaxVoices_ValueChanged);
            // 
            // cboSteal
            // 
            this.cboSteal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSteal.FormattingEnabled = true;
            this.cboSteal.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSteal.Location = new System.Drawing.Point(654, 452);
            this.cboSteal.Name = "cboSteal";
            this.cboSteal.Size = new System.Drawing.Size(68, 21);
            this.cboSteal.TabIndex = 7;
            this.cboSteal.SelectedIndexChanged += new System.EventHandler(this.CboSteal_SelectedIndexChanged);
            // 
            // nudPriority
            // 
            this.nudPriority.Location = new System.Drawing.Point(728, 453);
            this.nudPriority.Name = "nudPriority";
            this.nudPriority.Size = new System.Drawing.Size(70, 20);
            this.nudPriority.TabIndex = 8;
            this.nudPriority.ValueChanged += new System.EventHandler(this.NudPriority_ValueChanged);
            // 
            // nudAlertness
            // 
            this.nudAlertness.Location = new System.Drawing.Point(804, 453);
            this.nudAlertness.Name = "nudAlertness";
            this.nudAlertness.Size = new System.Drawing.Size(70, 20);
            this.nudAlertness.TabIndex = 9;
            this.nudAlertness.ValueChanged += new System.EventHandler(this.NudAlertness_ValueChanged);
            // 
            // nudDucker
            // 
            this.nudDucker.Location = new System.Drawing.Point(880, 453);
            this.nudDucker.Name = "nudDucker";
            this.nudDucker.Size = new System.Drawing.Size(70, 20);
            this.nudDucker.TabIndex = 10;
            this.nudDucker.ValueChanged += new System.EventHandler(this.NudDucker_ValueChanged);
            // 
            // nudDuckerLength
            // 
            this.nudDuckerLength.Location = new System.Drawing.Point(956, 453);
            this.nudDuckerLength.Name = "nudDuckerLength";
            this.nudDuckerLength.Size = new System.Drawing.Size(70, 20);
            this.nudDuckerLength.TabIndex = 11;
            this.nudDuckerLength.ValueChanged += new System.EventHandler(this.NudDuckerLength_ValueChanged);
            // 
            // nudMasterVol
            // 
            this.nudMasterVol.Location = new System.Drawing.Point(1032, 453);
            this.nudMasterVol.Name = "nudMasterVol";
            this.nudMasterVol.Size = new System.Drawing.Size(70, 20);
            this.nudMasterVol.TabIndex = 12;
            this.nudMasterVol.ValueChanged += new System.EventHandler(this.NudMasterVol_ValueChanged);
            // 
            // cboUnderWater
            // 
            this.cboUnderWater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnderWater.FormattingEnabled = true;
            this.cboUnderWater.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cboUnderWater.Location = new System.Drawing.Point(1108, 452);
            this.cboUnderWater.Name = "cboUnderWater";
            this.cboUnderWater.Size = new System.Drawing.Size(68, 21);
            this.cboUnderWater.TabIndex = 13;
            this.cboUnderWater.SelectedIndexChanged += new System.EventHandler(this.CboUnderWater_SelectedIndexChanged);
            // 
            // cboStealOnLouder
            // 
            this.cboStealOnLouder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStealOnLouder.FormattingEnabled = true;
            this.cboStealOnLouder.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cboStealOnLouder.Location = new System.Drawing.Point(1182, 452);
            this.cboStealOnLouder.Name = "cboStealOnLouder";
            this.cboStealOnLouder.Size = new System.Drawing.Size(68, 21);
            this.cboStealOnLouder.TabIndex = 14;
            this.cboStealOnLouder.SelectedIndexChanged += new System.EventHandler(this.CboStealOnLouder_SelectedIndexChanged);
            // 
            // grbLocked
            // 
            this.grbLocked.Controls.Add(this.chkApplyAllFormats);
            this.grbLocked.Location = new System.Drawing.Point(276, 479);
            this.grbLocked.Name = "grbLocked";
            this.grbLocked.Size = new System.Drawing.Size(863, 45);
            this.grbLocked.TabIndex = 16;
            this.grbLocked.TabStop = false;
            this.grbLocked.Text = "Locked to all Formats";
            // 
            // chkApplyAllFormats
            // 
            this.chkApplyAllFormats.AutoSize = true;
            this.chkApplyAllFormats.Checked = true;
            this.chkApplyAllFormats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkApplyAllFormats.Enabled = false;
            this.chkApplyAllFormats.Location = new System.Drawing.Point(6, 19);
            this.chkApplyAllFormats.Name = "chkApplyAllFormats";
            this.chkApplyAllFormats.Size = new System.Drawing.Size(118, 17);
            this.chkApplyAllFormats.TabIndex = 0;
            this.chkApplyAllFormats.Text = "Apply to All Formats";
            this.chkApplyAllFormats.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(1157, 484);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(105, 40);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnOpenAsExcel
            // 
            this.btnOpenAsExcel.Location = new System.Drawing.Point(175, 453);
            this.btnOpenAsExcel.Name = "btnOpenAsExcel";
            this.btnOpenAsExcel.Size = new System.Drawing.Size(95, 30);
            this.btnOpenAsExcel.TabIndex = 1;
            this.btnOpenAsExcel.Text = "Open as Excel";
            this.btnOpenAsExcel.UseVisualStyleBackColor = true;
            this.btnOpenAsExcel.Click += new System.EventHandler(this.BtnOpenAsExcel_Click);
            // 
            // cboCombo3
            // 
            this.cboCombo3.FormattingEnabled = true;
            this.cboCombo3.Location = new System.Drawing.Point(12, 503);
            this.cboCombo3.Name = "cboCombo3";
            this.cboCombo3.Size = new System.Drawing.Size(141, 21);
            this.cboCombo3.TabIndex = 15;
            // 
            // MultiEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 536);
            this.Controls.Add(this.cboCombo3);
            this.Controls.Add(this.btnOpenAsExcel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grbLocked);
            this.Controls.Add(this.cboStealOnLouder);
            this.Controls.Add(this.cboUnderWater);
            this.Controls.Add(this.nudMasterVol);
            this.Controls.Add(this.nudDuckerLength);
            this.Controls.Add(this.nudDucker);
            this.Controls.Add(this.nudAlertness);
            this.Controls.Add(this.nudPriority);
            this.Controls.Add(this.cboSteal);
            this.Controls.Add(this.nudMaxVoices);
            this.Controls.Add(this.nudOuterRad);
            this.Controls.Add(this.nudInnerRad);
            this.Controls.Add(this.cboTrackingType);
            this.Controls.Add(this.nudReverb);
            this.Controls.Add(this.lvwItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Multi Editor";
            this.Load += new System.EventHandler(this.MultiEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudReverb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInnerRad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOuterRad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxVoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlertness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDucker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckerLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterVol)).EndInit();
            this.grbLocked.ResumeLayout(false);
            this.grbLocked.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwItems;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colReverb;
        private System.Windows.Forms.ColumnHeader colTracking;
        private System.Windows.Forms.ColumnHeader colInnerRad;
        private System.Windows.Forms.ColumnHeader colOuterRad;
        private System.Windows.Forms.ColumnHeader colMax;
        private System.Windows.Forms.ColumnHeader colSteal;
        private System.Windows.Forms.ColumnHeader colPriority;
        private System.Windows.Forms.ColumnHeader colAlertness;
        private System.Windows.Forms.ColumnHeader colDucker;
        private System.Windows.Forms.ColumnHeader colDuckLen;
        private System.Windows.Forms.ColumnHeader colMasterVol;
        private System.Windows.Forms.ColumnHeader colUnderWater;
        private System.Windows.Forms.ColumnHeader colStealLouder;
        private System.Windows.Forms.NumericUpDown nudReverb;
        private System.Windows.Forms.ComboBox cboTrackingType;
        private System.Windows.Forms.NumericUpDown nudInnerRad;
        private System.Windows.Forms.NumericUpDown nudOuterRad;
        private System.Windows.Forms.NumericUpDown nudMaxVoices;
        private System.Windows.Forms.ComboBox cboSteal;
        private System.Windows.Forms.NumericUpDown nudPriority;
        private System.Windows.Forms.NumericUpDown nudAlertness;
        private System.Windows.Forms.NumericUpDown nudDucker;
        private System.Windows.Forms.NumericUpDown nudDuckerLength;
        private System.Windows.Forms.NumericUpDown nudMasterVol;
        private System.Windows.Forms.ComboBox cboUnderWater;
        private System.Windows.Forms.ComboBox cboStealOnLouder;
        private System.Windows.Forms.GroupBox grbLocked;
        private System.Windows.Forms.CheckBox chkApplyAllFormats;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnOpenAsExcel;
        private System.Windows.Forms.ComboBox cboCombo3;
    }
}
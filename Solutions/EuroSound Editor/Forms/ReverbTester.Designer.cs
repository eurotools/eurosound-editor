
namespace sb_editor.Forms
{
    partial class ReverbTester
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReverbTester));
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabPagePC = new System.Windows.Forms.TabPage();
            this.tabPageXBox = new System.Windows.Forms.TabPage();
            this.tabPageGameCube = new System.Windows.Forms.TabPage();
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.btnPlayTest = new System.Windows.Forms.Button();
            this.lblFilter2 = new System.Windows.Forms.Label();
            this.trkBarFilter2 = new System.Windows.Forms.TrackBar();
            this.lblFilter1 = new System.Windows.Forms.Label();
            this.trkBarFilter1 = new System.Windows.Forms.TrackBar();
            this.lblLowPassFilter = new System.Windows.Forms.Label();
            this.trkBarLowPassFilter = new System.Windows.Forms.TrackBar();
            this.lblDamp = new System.Windows.Forms.Label();
            this.trkBarDamp = new System.Windows.Forms.TrackBar();
            this.lblWidth = new System.Windows.Forms.Label();
            this.trkBarWidth = new System.Windows.Forms.TrackBar();
            this.lblRoomSize = new System.Windows.Forms.Label();
            this.trkBarRoomSize = new System.Windows.Forms.TrackBar();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnCopySelected = new System.Windows.Forms.Button();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.btnReMapHashCodes = new System.Windows.Forms.Button();
            this.btnRenameSelected = new System.Windows.Forms.Button();
            this.lstbHashCodes = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblHashCode = new System.Windows.Forms.Label();
            this.toolTipTrackBars = new System.Windows.Forms.ToolTip(this.components);
            this.tabCtrl.SuspendLayout();
            this.grbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarFilter2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarFilter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarLowPassFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarDamp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarRoomSize)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrl
            // 
            this.tabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrl.Controls.Add(this.tabPagePC);
            this.tabCtrl.Controls.Add(this.tabPageXBox);
            this.tabCtrl.Controls.Add(this.tabPageGameCube);
            this.tabCtrl.ItemSize = new System.Drawing.Size(232, 18);
            this.tabCtrl.Location = new System.Drawing.Point(12, 4);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(703, 23);
            this.tabCtrl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabCtrl.TabIndex = 0;
            this.tabCtrl.SelectedIndexChanged += new System.EventHandler(this.TabCtrl_SelectedIndexChanged);
            // 
            // tabPagePC
            // 
            this.tabPagePC.Location = new System.Drawing.Point(4, 22);
            this.tabPagePC.Name = "tabPagePC";
            this.tabPagePC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePC.Size = new System.Drawing.Size(695, 0);
            this.tabPagePC.TabIndex = 0;
            this.tabPagePC.Text = "PC";
            this.tabPagePC.UseVisualStyleBackColor = true;
            // 
            // tabPageXBox
            // 
            this.tabPageXBox.Location = new System.Drawing.Point(4, 22);
            this.tabPageXBox.Name = "tabPageXBox";
            this.tabPageXBox.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageXBox.Size = new System.Drawing.Size(695, 0);
            this.tabPageXBox.TabIndex = 1;
            this.tabPageXBox.Text = "XBox";
            this.tabPageXBox.UseVisualStyleBackColor = true;
            // 
            // tabPageGameCube
            // 
            this.tabPageGameCube.Location = new System.Drawing.Point(4, 22);
            this.tabPageGameCube.Name = "tabPageGameCube";
            this.tabPageGameCube.Size = new System.Drawing.Size(695, 0);
            this.tabPageGameCube.TabIndex = 2;
            this.tabPageGameCube.Text = "GameCube";
            this.tabPageGameCube.UseVisualStyleBackColor = true;
            // 
            // grbMain
            // 
            this.grbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMain.Controls.Add(this.btnPlayTest);
            this.grbMain.Controls.Add(this.lblFilter2);
            this.grbMain.Controls.Add(this.trkBarFilter2);
            this.grbMain.Controls.Add(this.lblFilter1);
            this.grbMain.Controls.Add(this.trkBarFilter1);
            this.grbMain.Controls.Add(this.lblLowPassFilter);
            this.grbMain.Controls.Add(this.trkBarLowPassFilter);
            this.grbMain.Controls.Add(this.lblDamp);
            this.grbMain.Controls.Add(this.trkBarDamp);
            this.grbMain.Controls.Add(this.lblWidth);
            this.grbMain.Controls.Add(this.trkBarWidth);
            this.grbMain.Controls.Add(this.lblRoomSize);
            this.grbMain.Controls.Add(this.trkBarRoomSize);
            this.grbMain.Location = new System.Drawing.Point(3, 6);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(400, 272);
            this.grbMain.TabIndex = 1;
            this.grbMain.TabStop = false;
            // 
            // btnPlayTest
            // 
            this.btnPlayTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlayTest.Location = new System.Drawing.Point(6, 236);
            this.btnPlayTest.Name = "btnPlayTest";
            this.btnPlayTest.Size = new System.Drawing.Size(75, 30);
            this.btnPlayTest.TabIndex = 12;
            this.btnPlayTest.Text = "Play Test";
            this.btnPlayTest.UseVisualStyleBackColor = true;
            this.btnPlayTest.Click += new System.EventHandler(this.BtnPlayTest_Click);
            // 
            // lblFilter2
            // 
            this.lblFilter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilter2.AutoSize = true;
            this.lblFilter2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter2.Location = new System.Drawing.Point(240, 190);
            this.lblFilter2.Name = "lblFilter2";
            this.lblFilter2.Size = new System.Drawing.Size(66, 24);
            this.lblFilter2.TabIndex = 11;
            this.lblFilter2.Text = "Filter 2";
            // 
            // trkBarFilter2
            // 
            this.trkBarFilter2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkBarFilter2.Location = new System.Drawing.Point(6, 190);
            this.trkBarFilter2.Maximum = 1000;
            this.trkBarFilter2.Name = "trkBarFilter2";
            this.trkBarFilter2.Size = new System.Drawing.Size(228, 45);
            this.trkBarFilter2.TabIndex = 10;
            this.trkBarFilter2.TickFrequency = 100;
            this.trkBarFilter2.Scroll += new System.EventHandler(this.TrkBarFilter2_Scroll);
            // 
            // lblFilter1
            // 
            this.lblFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilter1.AutoSize = true;
            this.lblFilter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter1.Location = new System.Drawing.Point(240, 153);
            this.lblFilter1.Name = "lblFilter1";
            this.lblFilter1.Size = new System.Drawing.Size(66, 24);
            this.lblFilter1.TabIndex = 9;
            this.lblFilter1.Text = "Filter 1";
            // 
            // trkBarFilter1
            // 
            this.trkBarFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkBarFilter1.Location = new System.Drawing.Point(6, 153);
            this.trkBarFilter1.Maximum = 1000;
            this.trkBarFilter1.Name = "trkBarFilter1";
            this.trkBarFilter1.Size = new System.Drawing.Size(228, 45);
            this.trkBarFilter1.TabIndex = 8;
            this.trkBarFilter1.TickFrequency = 100;
            this.trkBarFilter1.Scroll += new System.EventHandler(this.TrkBarFilter1_Scroll);
            // 
            // lblLowPassFilter
            // 
            this.lblLowPassFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLowPassFilter.AutoSize = true;
            this.lblLowPassFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowPassFilter.Location = new System.Drawing.Point(240, 117);
            this.lblLowPassFilter.Name = "lblLowPassFilter";
            this.lblLowPassFilter.Size = new System.Drawing.Size(136, 24);
            this.lblLowPassFilter.TabIndex = 7;
            this.lblLowPassFilter.Text = "Low Pass Filter";
            // 
            // trkBarLowPassFilter
            // 
            this.trkBarLowPassFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkBarLowPassFilter.Location = new System.Drawing.Point(6, 117);
            this.trkBarLowPassFilter.Maximum = 1000;
            this.trkBarLowPassFilter.Name = "trkBarLowPassFilter";
            this.trkBarLowPassFilter.Size = new System.Drawing.Size(228, 45);
            this.trkBarLowPassFilter.TabIndex = 6;
            this.trkBarLowPassFilter.TickFrequency = 100;
            this.trkBarLowPassFilter.Scroll += new System.EventHandler(this.TrkBarLowPassFilter_Scroll);
            // 
            // lblDamp
            // 
            this.lblDamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDamp.AutoSize = true;
            this.lblDamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDamp.Location = new System.Drawing.Point(240, 81);
            this.lblDamp.Name = "lblDamp";
            this.lblDamp.Size = new System.Drawing.Size(60, 24);
            this.lblDamp.TabIndex = 5;
            this.lblDamp.Text = "Damp";
            // 
            // trkBarDamp
            // 
            this.trkBarDamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkBarDamp.Location = new System.Drawing.Point(6, 81);
            this.trkBarDamp.Maximum = 1000;
            this.trkBarDamp.Name = "trkBarDamp";
            this.trkBarDamp.Size = new System.Drawing.Size(228, 45);
            this.trkBarDamp.TabIndex = 4;
            this.trkBarDamp.TickFrequency = 100;
            this.trkBarDamp.Scroll += new System.EventHandler(this.TrkBarDamp_Scroll);
            // 
            // lblWidth
            // 
            this.lblWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWidth.AutoSize = true;
            this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidth.Location = new System.Drawing.Point(240, 45);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(58, 24);
            this.lblWidth.TabIndex = 3;
            this.lblWidth.Text = "Width";
            // 
            // trkBarWidth
            // 
            this.trkBarWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkBarWidth.Location = new System.Drawing.Point(6, 45);
            this.trkBarWidth.Maximum = 1000;
            this.trkBarWidth.Name = "trkBarWidth";
            this.trkBarWidth.Size = new System.Drawing.Size(228, 45);
            this.trkBarWidth.TabIndex = 2;
            this.trkBarWidth.TickFrequency = 100;
            this.trkBarWidth.Scroll += new System.EventHandler(this.TrkBarWidth_Scroll);
            // 
            // lblRoomSize
            // 
            this.lblRoomSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoomSize.AutoSize = true;
            this.lblRoomSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomSize.Location = new System.Drawing.Point(240, 14);
            this.lblRoomSize.Name = "lblRoomSize";
            this.lblRoomSize.Size = new System.Drawing.Size(102, 24);
            this.lblRoomSize.TabIndex = 1;
            this.lblRoomSize.Text = "Room Size";
            // 
            // trkBarRoomSize
            // 
            this.trkBarRoomSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkBarRoomSize.Location = new System.Drawing.Point(6, 14);
            this.trkBarRoomSize.Maximum = 1000;
            this.trkBarRoomSize.Name = "trkBarRoomSize";
            this.trkBarRoomSize.Size = new System.Drawing.Size(228, 45);
            this.trkBarRoomSize.TabIndex = 0;
            this.trkBarRoomSize.TickFrequency = 100;
            this.trkBarRoomSize.Scroll += new System.EventHandler(this.TrkBarRoomSize_Scroll);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.btnAddNew);
            this.pnlMain.Controls.Add(this.btnCopySelected);
            this.pnlMain.Controls.Add(this.btnDeleteSelected);
            this.pnlMain.Controls.Add(this.btnReMapHashCodes);
            this.pnlMain.Controls.Add(this.btnRenameSelected);
            this.pnlMain.Controls.Add(this.lstbHashCodes);
            this.pnlMain.Controls.Add(this.grbMain);
            this.pnlMain.Location = new System.Drawing.Point(12, 25);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(703, 285);
            this.pnlMain.TabIndex = 1;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(605, 248);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(92, 30);
            this.btnAddNew.TabIndex = 18;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // btnCopySelected
            // 
            this.btnCopySelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopySelected.Location = new System.Drawing.Point(507, 248);
            this.btnCopySelected.Name = "btnCopySelected";
            this.btnCopySelected.Size = new System.Drawing.Size(92, 30);
            this.btnCopySelected.TabIndex = 17;
            this.btnCopySelected.Text = "Copy Selected";
            this.btnCopySelected.UseVisualStyleBackColor = true;
            this.btnCopySelected.Click += new System.EventHandler(this.BtnCopySelected_Click);
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSelected.Location = new System.Drawing.Point(409, 248);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(92, 30);
            this.btnDeleteSelected.TabIndex = 16;
            this.btnDeleteSelected.Text = "Delete Selected";
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.BtnDeleteSelected_Click);
            // 
            // btnReMapHashCodes
            // 
            this.btnReMapHashCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReMapHashCodes.Location = new System.Drawing.Point(556, 212);
            this.btnReMapHashCodes.Name = "btnReMapHashCodes";
            this.btnReMapHashCodes.Size = new System.Drawing.Size(140, 30);
            this.btnReMapHashCodes.TabIndex = 15;
            this.btnReMapHashCodes.Text = "ReMap HashCodes";
            this.btnReMapHashCodes.UseVisualStyleBackColor = true;
            this.btnReMapHashCodes.Click += new System.EventHandler(this.BtnReMapHashCodes_Click);
            // 
            // btnRenameSelected
            // 
            this.btnRenameSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRenameSelected.Location = new System.Drawing.Point(409, 212);
            this.btnRenameSelected.Name = "btnRenameSelected";
            this.btnRenameSelected.Size = new System.Drawing.Size(140, 30);
            this.btnRenameSelected.TabIndex = 14;
            this.btnRenameSelected.Text = "Rename Selected";
            this.btnRenameSelected.UseVisualStyleBackColor = true;
            this.btnRenameSelected.Click += new System.EventHandler(this.BtnRenameSelected_Click);
            // 
            // lstbHashCodes
            // 
            this.lstbHashCodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbHashCodes.FormattingEnabled = true;
            this.lstbHashCodes.Location = new System.Drawing.Point(409, 5);
            this.lstbHashCodes.Name = "lstbHashCodes";
            this.lstbHashCodes.Size = new System.Drawing.Size(287, 199);
            this.lstbHashCodes.Sorted = true;
            this.lstbHashCodes.TabIndex = 2;
            this.lstbHashCodes.SelectedIndexChanged += new System.EventHandler(this.LstbHashCodes_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(615, 316);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // lblHashCode
            // 
            this.lblHashCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHashCode.AutoSize = true;
            this.lblHashCode.Location = new System.Drawing.Point(12, 325);
            this.lblHashCode.Name = "lblHashCode";
            this.lblHashCode.Size = new System.Drawing.Size(60, 13);
            this.lblHashCode.TabIndex = 2;
            this.lblHashCode.Text = "HashCode:";
            // 
            // ReverbTester
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 358);
            this.Controls.Add(this.lblHashCode);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tabCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReverbTester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Reverb Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReverbTester_FormClosing);
            this.Load += new System.EventHandler(this.ReverbTester_Load);
            this.tabCtrl.ResumeLayout(false);
            this.grbMain.ResumeLayout(false);
            this.grbMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarFilter2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarFilter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarLowPassFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarDamp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBarRoomSize)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabPagePC;
        private System.Windows.Forms.TabPage tabPageXBox;
        private System.Windows.Forms.GroupBox grbMain;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabPage tabPageGameCube;
        private System.Windows.Forms.Button btnPlayTest;
        private System.Windows.Forms.Label lblFilter2;
        private System.Windows.Forms.TrackBar trkBarFilter2;
        private System.Windows.Forms.Label lblFilter1;
        private System.Windows.Forms.TrackBar trkBarFilter1;
        private System.Windows.Forms.Label lblLowPassFilter;
        private System.Windows.Forms.TrackBar trkBarLowPassFilter;
        private System.Windows.Forms.Label lblDamp;
        private System.Windows.Forms.TrackBar trkBarDamp;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.TrackBar trkBarWidth;
        private System.Windows.Forms.Label lblRoomSize;
        private System.Windows.Forms.TrackBar trkBarRoomSize;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnCopySelected;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnReMapHashCodes;
        private System.Windows.Forms.Button btnRenameSelected;
        private System.Windows.Forms.ListBox lstbHashCodes;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblHashCode;
        private System.Windows.Forms.ToolTip toolTipTrackBars;
    }
}
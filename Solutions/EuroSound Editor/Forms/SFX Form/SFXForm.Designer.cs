
namespace sb_editor.Forms
{
    partial class SFXForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFXForm));
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.pnlSFXParameters = new System.Windows.Forms.Panel();
            this.ButtonDllVoices = new System.Windows.Forms.Button();
            this.btnSfxTestDebug = new System.Windows.Forms.Button();
            this.pnlAlert = new System.Windows.Forms.Panel();
            this.lblAlert = new System.Windows.Forms.Label();
            this.btnDefSettings_Cancel = new System.Windows.Forms.Button();
            this.btnDefSettings_Accept = new System.Windows.Forms.Button();
            this.lblHashCode = new System.Windows.Forms.Label();
            this.txtHashCode = new System.Windows.Forms.TextBox();
            this.btnReverbTester = new System.Windows.Forms.Button();
            this.btnStopSFX = new System.Windows.Forms.Button();
            this.btnTestSFX = new System.Windows.Forms.Button();
            this.txtDllTime = new System.Windows.Forms.TextBox();
            this.txtEsTime = new System.Windows.Forms.TextBox();
            this.UserControl_SamplePool = new sb_editor.Panels.UserControl_SamplePool();
            this.UserControl_SFX_Parameters = new sb_editor.Panels.UserControl_SFX_Parameters();
            this.UserControl_SamplePoolControl = new sb_editor.Panels.UserControl_SamplePoolControl();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.grbDeleteFormat = new System.Windows.Forms.GroupBox();
            this.btnRemoveFormat = new System.Windows.Forms.Button();
            this.grbClipboard = new System.Windows.Forms.GroupBox();
            this.btnClipboardPaste = new System.Windows.Forms.Button();
            this.btnClipboardCopy = new System.Windows.Forms.Button();
            this.grbCreateFormat = new System.Windows.Forms.GroupBox();
            this.btnPC = new System.Windows.Forms.Button();
            this.btnXBOX = new System.Windows.Forms.Button();
            this.btnGAMECUBE = new System.Windows.Forms.Button();
            this.btnPLAYSTATION2 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblCurrentSFX = new System.Windows.Forms.Label();
            this.tmrTabPageBlink = new System.Windows.Forms.Timer(this.components);
            this.pnlSFXParameters.SuspendLayout();
            this.pnlAlert.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.grbDeleteFormat.SuspendLayout();
            this.grbClipboard.SuspendLayout();
            this.grbCreateFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrl
            // 
            this.tabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCtrl.Location = new System.Drawing.Point(12, -1);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(881, 31);
            this.tabCtrl.TabIndex = 0;
            this.tabCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabCtrl_DrawItem);
            this.tabCtrl.SelectedIndexChanged += new System.EventHandler(this.TabCtrl_SelectedIndexChanged);
            this.tabCtrl.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabCtrl_Deselecting);
            // 
            // pnlSFXParameters
            // 
            this.pnlSFXParameters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSFXParameters.Controls.Add(this.ButtonDllVoices);
            this.pnlSFXParameters.Controls.Add(this.btnSfxTestDebug);
            this.pnlSFXParameters.Controls.Add(this.pnlAlert);
            this.pnlSFXParameters.Controls.Add(this.btnDefSettings_Cancel);
            this.pnlSFXParameters.Controls.Add(this.btnDefSettings_Accept);
            this.pnlSFXParameters.Controls.Add(this.lblHashCode);
            this.pnlSFXParameters.Controls.Add(this.txtHashCode);
            this.pnlSFXParameters.Controls.Add(this.btnReverbTester);
            this.pnlSFXParameters.Controls.Add(this.btnStopSFX);
            this.pnlSFXParameters.Controls.Add(this.btnTestSFX);
            this.pnlSFXParameters.Controls.Add(this.txtDllTime);
            this.pnlSFXParameters.Controls.Add(this.txtEsTime);
            this.pnlSFXParameters.Controls.Add(this.UserControl_SamplePool);
            this.pnlSFXParameters.Controls.Add(this.UserControl_SFX_Parameters);
            this.pnlSFXParameters.Controls.Add(this.UserControl_SamplePoolControl);
            this.pnlSFXParameters.Location = new System.Drawing.Point(12, 28);
            this.pnlSFXParameters.Name = "pnlSFXParameters";
            this.pnlSFXParameters.Size = new System.Drawing.Size(881, 680);
            this.pnlSFXParameters.TabIndex = 1;
            // 
            // ButtonDllVoices
            // 
            this.ButtonDllVoices.Location = new System.Drawing.Point(787, 510);
            this.ButtonDllVoices.Name = "ButtonDllVoices";
            this.ButtonDllVoices.Size = new System.Drawing.Size(88, 23);
            this.ButtonDllVoices.TabIndex = 11;
            this.ButtonDllVoices.Text = "PC DLL Voices";
            this.ButtonDllVoices.UseVisualStyleBackColor = true;
            this.ButtonDllVoices.Click += new System.EventHandler(this.ButtonDllVoices_Click);
            // 
            // btnSfxTestDebug
            // 
            this.btnSfxTestDebug.Location = new System.Drawing.Point(787, 452);
            this.btnSfxTestDebug.Name = "btnSfxTestDebug";
            this.btnSfxTestDebug.Size = new System.Drawing.Size(88, 23);
            this.btnSfxTestDebug.TabIndex = 9;
            this.btnSfxTestDebug.Text = "PC DLL Debug";
            this.btnSfxTestDebug.UseVisualStyleBackColor = true;
            this.btnSfxTestDebug.Click += new System.EventHandler(this.BtnSfxTestDebug_Click);
            // 
            // pnlAlert
            // 
            this.pnlAlert.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAlert.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAlert.Controls.Add(this.lblAlert);
            this.pnlAlert.Location = new System.Drawing.Point(140, 253);
            this.pnlAlert.Name = "pnlAlert";
            this.pnlAlert.Size = new System.Drawing.Size(615, 50);
            this.pnlAlert.TabIndex = 10;
            this.pnlAlert.Visible = false;
            // 
            // lblAlert
            // 
            this.lblAlert.BackColor = System.Drawing.SystemColors.Control;
            this.lblAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert.ForeColor = System.Drawing.Color.Red;
            this.lblAlert.Location = new System.Drawing.Point(0, 0);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(611, 46);
            this.lblAlert.TabIndex = 0;
            this.lblAlert.Text = "Common Is Not Used By This SFX!";
            this.lblAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDefSettings_Cancel
            // 
            this.btnDefSettings_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDefSettings_Cancel.Location = new System.Drawing.Point(693, 313);
            this.btnDefSettings_Cancel.Name = "btnDefSettings_Cancel";
            this.btnDefSettings_Cancel.Size = new System.Drawing.Size(88, 23);
            this.btnDefSettings_Cancel.TabIndex = 3;
            this.btnDefSettings_Cancel.Text = "Cancel";
            this.btnDefSettings_Cancel.UseVisualStyleBackColor = true;
            this.btnDefSettings_Cancel.Visible = false;
            this.btnDefSettings_Cancel.Click += new System.EventHandler(this.BtnDefSettings_Cancel_Click);
            // 
            // btnDefSettings_Accept
            // 
            this.btnDefSettings_Accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDefSettings_Accept.Location = new System.Drawing.Point(787, 313);
            this.btnDefSettings_Accept.Name = "btnDefSettings_Accept";
            this.btnDefSettings_Accept.Size = new System.Drawing.Size(88, 23);
            this.btnDefSettings_Accept.TabIndex = 4;
            this.btnDefSettings_Accept.Text = "OK";
            this.btnDefSettings_Accept.UseVisualStyleBackColor = true;
            this.btnDefSettings_Accept.Visible = false;
            this.btnDefSettings_Accept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // lblHashCode
            // 
            this.lblHashCode.AutoSize = true;
            this.lblHashCode.Location = new System.Drawing.Point(801, 536);
            this.lblHashCode.Name = "lblHashCode";
            this.lblHashCode.Size = new System.Drawing.Size(60, 13);
            this.lblHashCode.TabIndex = 12;
            this.lblHashCode.Text = "HashCode:";
            // 
            // txtHashCode
            // 
            this.txtHashCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtHashCode.Location = new System.Drawing.Point(787, 552);
            this.txtHashCode.Name = "txtHashCode";
            this.txtHashCode.ReadOnly = true;
            this.txtHashCode.Size = new System.Drawing.Size(88, 20);
            this.txtHashCode.TabIndex = 13;
            // 
            // btnReverbTester
            // 
            this.btnReverbTester.Location = new System.Drawing.Point(787, 481);
            this.btnReverbTester.Name = "btnReverbTester";
            this.btnReverbTester.Size = new System.Drawing.Size(88, 23);
            this.btnReverbTester.TabIndex = 10;
            this.btnReverbTester.Text = "Reverb Tester";
            this.btnReverbTester.UseVisualStyleBackColor = true;
            this.btnReverbTester.Click += new System.EventHandler(this.BtnReverbTester_Click);
            // 
            // btnStopSFX
            // 
            this.btnStopSFX.Location = new System.Drawing.Point(787, 423);
            this.btnStopSFX.Name = "btnStopSFX";
            this.btnStopSFX.Size = new System.Drawing.Size(88, 23);
            this.btnStopSFX.TabIndex = 8;
            this.btnStopSFX.Text = "Stop SFX";
            this.btnStopSFX.UseVisualStyleBackColor = true;
            this.btnStopSFX.Click += new System.EventHandler(this.BtnStopSFX_Click);
            // 
            // btnTestSFX
            // 
            this.btnTestSFX.Location = new System.Drawing.Point(787, 394);
            this.btnTestSFX.Name = "btnTestSFX";
            this.btnTestSFX.Size = new System.Drawing.Size(88, 23);
            this.btnTestSFX.TabIndex = 7;
            this.btnTestSFX.Text = "Test SFX";
            this.btnTestSFX.UseVisualStyleBackColor = true;
            this.btnTestSFX.Click += new System.EventHandler(this.BtnTestSFX_Click);
            // 
            // txtDllTime
            // 
            this.txtDllTime.BackColor = System.Drawing.SystemColors.Window;
            this.txtDllTime.Location = new System.Drawing.Point(787, 368);
            this.txtDllTime.Name = "txtDllTime";
            this.txtDllTime.ReadOnly = true;
            this.txtDllTime.Size = new System.Drawing.Size(88, 20);
            this.txtDllTime.TabIndex = 6;
            // 
            // txtEsTime
            // 
            this.txtEsTime.BackColor = System.Drawing.SystemColors.Window;
            this.txtEsTime.Location = new System.Drawing.Point(787, 342);
            this.txtEsTime.Name = "txtEsTime";
            this.txtEsTime.ReadOnly = true;
            this.txtEsTime.Size = new System.Drawing.Size(88, 20);
            this.txtEsTime.TabIndex = 5;
            // 
            // UserControl_SamplePool
            // 
            this.UserControl_SamplePool.Location = new System.Drawing.Point(3, 313);
            this.UserControl_SamplePool.Name = "UserControl_SamplePool";
            this.UserControl_SamplePool.Size = new System.Drawing.Size(777, 360);
            this.UserControl_SamplePool.TabIndex = 2;
            // 
            // UserControl_SFX_Parameters
            // 
            this.UserControl_SFX_Parameters.Location = new System.Drawing.Point(3, 5);
            this.UserControl_SFX_Parameters.Name = "UserControl_SFX_Parameters";
            this.UserControl_SFX_Parameters.Size = new System.Drawing.Size(492, 302);
            this.UserControl_SFX_Parameters.TabIndex = 0;
            // 
            // UserControl_SamplePoolControl
            // 
            this.UserControl_SamplePoolControl.Location = new System.Drawing.Point(501, 5);
            this.UserControl_SamplePoolControl.Name = "UserControl_SamplePoolControl";
            this.UserControl_SamplePoolControl.Size = new System.Drawing.Size(373, 302);
            this.UserControl_SamplePoolControl.TabIndex = 1;
            // 
            // pnlOptions
            // 
            this.pnlOptions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOptions.Controls.Add(this.grbDeleteFormat);
            this.pnlOptions.Controls.Add(this.grbClipboard);
            this.pnlOptions.Controls.Add(this.grbCreateFormat);
            this.pnlOptions.Location = new System.Drawing.Point(12, 714);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(881, 57);
            this.pnlOptions.TabIndex = 2;
            // 
            // grbDeleteFormat
            // 
            this.grbDeleteFormat.Controls.Add(this.btnRemoveFormat);
            this.grbDeleteFormat.Location = new System.Drawing.Point(665, 3);
            this.grbDeleteFormat.Name = "grbDeleteFormat";
            this.grbDeleteFormat.Size = new System.Drawing.Size(208, 47);
            this.grbDeleteFormat.TabIndex = 2;
            this.grbDeleteFormat.TabStop = false;
            this.grbDeleteFormat.Text = "Delete Specific Version";
            // 
            // btnRemoveFormat
            // 
            this.btnRemoveFormat.Location = new System.Drawing.Point(6, 18);
            this.btnRemoveFormat.Name = "btnRemoveFormat";
            this.btnRemoveFormat.Size = new System.Drawing.Size(92, 23);
            this.btnRemoveFormat.TabIndex = 0;
            this.btnRemoveFormat.Text = "Remove Format";
            this.btnRemoveFormat.UseVisualStyleBackColor = true;
            this.btnRemoveFormat.Click += new System.EventHandler(this.BtnRemoveFormat_Click);
            // 
            // grbClipboard
            // 
            this.grbClipboard.Controls.Add(this.btnClipboardPaste);
            this.grbClipboard.Controls.Add(this.btnClipboardCopy);
            this.grbClipboard.Location = new System.Drawing.Point(409, 3);
            this.grbClipboard.Name = "grbClipboard";
            this.grbClipboard.Size = new System.Drawing.Size(250, 47);
            this.grbClipboard.TabIndex = 1;
            this.grbClipboard.TabStop = false;
            this.grbClipboard.Text = "Copy and Paste Version Data";
            // 
            // btnClipboardPaste
            // 
            this.btnClipboardPaste.Location = new System.Drawing.Point(127, 18);
            this.btnClipboardPaste.Name = "btnClipboardPaste";
            this.btnClipboardPaste.Size = new System.Drawing.Size(115, 23);
            this.btnClipboardPaste.TabIndex = 1;
            this.btnClipboardPaste.Text = "Paste From Clipboard";
            this.btnClipboardPaste.UseVisualStyleBackColor = true;
            this.btnClipboardPaste.Click += new System.EventHandler(this.BtnClipboardPaste_Click);
            // 
            // btnClipboardCopy
            // 
            this.btnClipboardCopy.Location = new System.Drawing.Point(6, 18);
            this.btnClipboardCopy.Name = "btnClipboardCopy";
            this.btnClipboardCopy.Size = new System.Drawing.Size(115, 23);
            this.btnClipboardCopy.TabIndex = 0;
            this.btnClipboardCopy.Text = "Copy To Clipboard";
            this.btnClipboardCopy.UseVisualStyleBackColor = true;
            this.btnClipboardCopy.Click += new System.EventHandler(this.BtnClipboardCopy_Click);
            // 
            // grbCreateFormat
            // 
            this.grbCreateFormat.Controls.Add(this.btnPC);
            this.grbCreateFormat.Controls.Add(this.btnXBOX);
            this.grbCreateFormat.Controls.Add(this.btnGAMECUBE);
            this.grbCreateFormat.Controls.Add(this.btnPLAYSTATION2);
            this.grbCreateFormat.Location = new System.Drawing.Point(3, 3);
            this.grbCreateFormat.Name = "grbCreateFormat";
            this.grbCreateFormat.Size = new System.Drawing.Size(400, 47);
            this.grbCreateFormat.TabIndex = 0;
            this.grbCreateFormat.TabStop = false;
            this.grbCreateFormat.Text = "Create Format Specific Version For:";
            // 
            // btnPC
            // 
            this.btnPC.Enabled = false;
            this.btnPC.Location = new System.Drawing.Point(302, 18);
            this.btnPC.Name = "btnPC";
            this.btnPC.Size = new System.Drawing.Size(92, 23);
            this.btnPC.TabIndex = 3;
            this.btnPC.Text = "PC";
            this.btnPC.UseVisualStyleBackColor = true;
            this.btnPC.Click += new System.EventHandler(this.BtnPC_Click);
            // 
            // btnXBOX
            // 
            this.btnXBOX.Enabled = false;
            this.btnXBOX.Location = new System.Drawing.Point(204, 18);
            this.btnXBOX.Name = "btnXBOX";
            this.btnXBOX.Size = new System.Drawing.Size(92, 23);
            this.btnXBOX.TabIndex = 2;
            this.btnXBOX.Text = "Xbox";
            this.btnXBOX.UseVisualStyleBackColor = true;
            this.btnXBOX.Click += new System.EventHandler(this.BtnXBOX_Click);
            // 
            // btnGAMECUBE
            // 
            this.btnGAMECUBE.Enabled = false;
            this.btnGAMECUBE.Location = new System.Drawing.Point(106, 18);
            this.btnGAMECUBE.Name = "btnGAMECUBE";
            this.btnGAMECUBE.Size = new System.Drawing.Size(92, 23);
            this.btnGAMECUBE.TabIndex = 1;
            this.btnGAMECUBE.Text = "GameCube";
            this.btnGAMECUBE.UseVisualStyleBackColor = true;
            this.btnGAMECUBE.Click += new System.EventHandler(this.BtnGAMECUBE_Click);
            // 
            // btnPLAYSTATION2
            // 
            this.btnPLAYSTATION2.Enabled = false;
            this.btnPLAYSTATION2.Location = new System.Drawing.Point(8, 18);
            this.btnPLAYSTATION2.Name = "btnPLAYSTATION2";
            this.btnPLAYSTATION2.Size = new System.Drawing.Size(92, 23);
            this.btnPLAYSTATION2.TabIndex = 0;
            this.btnPLAYSTATION2.Text = "PlayStation2";
            this.btnPLAYSTATION2.UseVisualStyleBackColor = true;
            this.btnPLAYSTATION2.Click += new System.EventHandler(this.BtnPLAYSTATION2_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(737, 777);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(818, 777);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // lblCurrentSFX
            // 
            this.lblCurrentSFX.AutoSize = true;
            this.lblCurrentSFX.Location = new System.Drawing.Point(12, 783);
            this.lblCurrentSFX.Name = "lblCurrentSFX";
            this.lblCurrentSFX.Size = new System.Drawing.Size(47, 13);
            this.lblCurrentSFX.TabIndex = 3;
            this.lblCurrentSFX.Text = ">Name: ";
            // 
            // tmrTabPageBlink
            // 
            this.tmrTabPageBlink.Interval = 500;
            this.tmrTabPageBlink.Tick += new System.EventHandler(this.TmrTabPageBlink_Tick);
            // 
            // SFXForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(905, 814);
            this.Controls.Add(this.lblCurrentSFX);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.pnlSFXParameters);
            this.Controls.Add(this.tabCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SFXForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SFX Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SFX_Form_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SFX_Form_Load);
            this.Shown += new System.EventHandler(this.Frm_SFX_Form_Shown);
            this.pnlSFXParameters.ResumeLayout(false);
            this.pnlSFXParameters.PerformLayout();
            this.pnlAlert.ResumeLayout(false);
            this.pnlOptions.ResumeLayout(false);
            this.grbDeleteFormat.ResumeLayout(false);
            this.grbClipboard.ResumeLayout(false);
            this.grbCreateFormat.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.Panel pnlSFXParameters;
        private System.Windows.Forms.GroupBox grbDeleteFormat;
        private System.Windows.Forms.Button btnRemoveFormat;
        private System.Windows.Forms.GroupBox grbClipboard;
        private System.Windows.Forms.Button btnClipboardPaste;
        private System.Windows.Forms.Button btnClipboardCopy;
        private System.Windows.Forms.GroupBox grbCreateFormat;
        private System.Windows.Forms.Button btnPC;
        private System.Windows.Forms.Button btnXBOX;
        private System.Windows.Forms.Button btnGAMECUBE;
        private System.Windows.Forms.Button btnPLAYSTATION2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblCurrentSFX;
        private System.Windows.Forms.Button btnReverbTester;
        private System.Windows.Forms.Button btnStopSFX;
        private System.Windows.Forms.Button btnTestSFX;
        private System.Windows.Forms.TextBox txtDllTime;
        private System.Windows.Forms.TextBox txtEsTime;
        private System.Windows.Forms.TextBox txtHashCode;
        private System.Windows.Forms.Label lblHashCode;
        protected internal System.Windows.Forms.Panel pnlAlert;
        private System.Windows.Forms.Label lblAlert;
        protected internal System.Windows.Forms.Timer tmrTabPageBlink;
        protected internal Panels.UserControl_SFX_Parameters UserControl_SFX_Parameters;
        protected internal Panels.UserControl_SamplePoolControl UserControl_SamplePoolControl;
        protected internal Panels.UserControl_SamplePool UserControl_SamplePool;
        protected internal System.Windows.Forms.Button btnDefSettings_Accept;
        protected internal System.Windows.Forms.Button btnDefSettings_Cancel;
        protected internal System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Button btnSfxTestDebug;
        private System.Windows.Forms.Button ButtonDllVoices;
    }
}
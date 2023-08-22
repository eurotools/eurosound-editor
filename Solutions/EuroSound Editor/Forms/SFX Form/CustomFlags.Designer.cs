
namespace sb_editor.Forms.SFX_Form
{
    partial class CustomFlags
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
            this.chkListFlags = new System.Windows.Forms.CheckedListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblLabelsList = new System.Windows.Forms.Label();
            this.txtLabelsPath = new System.Windows.Forms.TextBox();
            this.btnSearchList = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // chkListFlags
            // 
            this.chkListFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListFlags.CheckOnClick = true;
            this.chkListFlags.FormattingEnabled = true;
            this.chkListFlags.Items.AddRange(new object[] {
            "UserFlag1",
            "UserFlag10",
            "UserFlag11",
            "UserFlag12",
            "UserFlag13",
            "UserFlag14",
            "UserFlag15",
            "UserFlag16",
            "UserFlag2",
            "UserFlag3",
            "UserFlag4",
            "UserFlag5",
            "UserFlag6",
            "UserFlag7",
            "UserFlag8",
            "UserFlag9"});
            this.chkListFlags.Location = new System.Drawing.Point(12, 41);
            this.chkListFlags.MultiColumn = true;
            this.chkListFlags.Name = "chkListFlags";
            this.chkListFlags.Size = new System.Drawing.Size(326, 124);
            this.chkListFlags.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(182, 177);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(263, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblLabelsList
            // 
            this.lblLabelsList.AutoSize = true;
            this.lblLabelsList.Location = new System.Drawing.Point(12, 17);
            this.lblLabelsList.Name = "lblLabelsList";
            this.lblLabelsList.Size = new System.Drawing.Size(54, 13);
            this.lblLabelsList.TabIndex = 0;
            this.lblLabelsList.Text = "Flags List:";
            // 
            // txtLabelsPath
            // 
            this.txtLabelsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabelsPath.Location = new System.Drawing.Point(72, 14);
            this.txtLabelsPath.Name = "txtLabelsPath";
            this.txtLabelsPath.Size = new System.Drawing.Size(235, 20);
            this.txtLabelsPath.TabIndex = 1;
            // 
            // btnSearchList
            // 
            this.btnSearchList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchList.Location = new System.Drawing.Point(313, 14);
            this.btnSearchList.Name = "btnSearchList";
            this.btnSearchList.Size = new System.Drawing.Size(25, 21);
            this.btnSearchList.TabIndex = 2;
            this.btnSearchList.Text = "...";
            this.btnSearchList.UseVisualStyleBackColor = true;
            this.btnSearchList.Click += new System.EventHandler(this.BtnSearchList_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            // 
            // CustomFlags
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(350, 212);
            this.Controls.Add(this.btnSearchList);
            this.Controls.Add(this.txtLabelsPath);
            this.Controls.Add(this.lblLabelsList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkListFlags);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomFlags";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom Flags";
            this.Load += new System.EventHandler(this.CustomFlags_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkListFlags;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblLabelsList;
        private System.Windows.Forms.TextBox txtLabelsPath;
        private System.Windows.Forms.Button btnSearchList;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
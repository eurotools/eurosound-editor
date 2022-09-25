
namespace MarkersEditor
{
    partial class Frm_InputBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_InputBox));
            this.Label_MarkerName = new System.Windows.Forms.Label();
            this.Textbox_MarkerName = new System.Windows.Forms.TextBox();
            this.Label_Milliseconds = new System.Windows.Forms.Label();
            this.Numeric_Milliseconds = new System.Windows.Forms.NumericUpDown();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_Milliseconds)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_MarkerName
            // 
            this.Label_MarkerName.AutoSize = true;
            this.Label_MarkerName.Location = new System.Drawing.Point(12, 15);
            this.Label_MarkerName.Name = "Label_MarkerName";
            this.Label_MarkerName.Size = new System.Drawing.Size(72, 13);
            this.Label_MarkerName.TabIndex = 0;
            this.Label_MarkerName.Text = "Marker Label:";
            // 
            // Textbox_MarkerName
            // 
            this.Textbox_MarkerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_MarkerName.Location = new System.Drawing.Point(90, 12);
            this.Textbox_MarkerName.Name = "Textbox_MarkerName";
            this.Textbox_MarkerName.Size = new System.Drawing.Size(238, 20);
            this.Textbox_MarkerName.TabIndex = 1;
            // 
            // Label_Milliseconds
            // 
            this.Label_Milliseconds.AutoSize = true;
            this.Label_Milliseconds.Location = new System.Drawing.Point(17, 40);
            this.Label_Milliseconds.Name = "Label_Milliseconds";
            this.Label_Milliseconds.Size = new System.Drawing.Size(67, 13);
            this.Label_Milliseconds.TabIndex = 2;
            this.Label_Milliseconds.Text = "Milliseconds:";
            // 
            // Numeric_Milliseconds
            // 
            this.Numeric_Milliseconds.Location = new System.Drawing.Point(90, 38);
            this.Numeric_Milliseconds.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.Numeric_Milliseconds.Name = "Numeric_Milliseconds";
            this.Numeric_Milliseconds.Size = new System.Drawing.Size(120, 20);
            this.Numeric_Milliseconds.TabIndex = 3;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(172, 84);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 4;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(253, 84);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 5;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Frm_InputBox
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(340, 119);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Numeric_Milliseconds);
            this.Controls.Add(this.Label_Milliseconds);
            this.Controls.Add(this.Textbox_MarkerName);
            this.Controls.Add(this.Label_MarkerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_InputBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Marker";
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_Milliseconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_MarkerName;
        private System.Windows.Forms.Label Label_Milliseconds;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        protected internal System.Windows.Forms.TextBox Textbox_MarkerName;
        protected internal System.Windows.Forms.NumericUpDown Numeric_Milliseconds;
    }
}

namespace sb_explorer
{
    partial class FindHashCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindHashCode));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RadioButton_MatchPartial = new System.Windows.Forms.RadioButton();
            this.RadioButton_MatchWhole = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_Find = new System.Windows.Forms.Button();
            this.Textbox_TextSearch = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadioButton_MatchPartial);
            this.groupBox1.Controls.Add(this.RadioButton_MatchWhole);
            this.groupBox1.Location = new System.Drawing.Point(21, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 96);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find";
            // 
            // RadioButton_MatchPartial
            // 
            this.RadioButton_MatchPartial.AutoSize = true;
            this.RadioButton_MatchPartial.Checked = true;
            this.RadioButton_MatchPartial.Location = new System.Drawing.Point(24, 56);
            this.RadioButton_MatchPartial.Name = "RadioButton_MatchPartial";
            this.RadioButton_MatchPartial.Size = new System.Drawing.Size(86, 17);
            this.RadioButton_MatchPartial.TabIndex = 1;
            this.RadioButton_MatchPartial.TabStop = true;
            this.RadioButton_MatchPartial.Text = "Match partial";
            // 
            // RadioButton_MatchWhole
            // 
            this.RadioButton_MatchWhole.AutoSize = true;
            this.RadioButton_MatchWhole.Enabled = false;
            this.RadioButton_MatchWhole.Location = new System.Drawing.Point(24, 24);
            this.RadioButton_MatchWhole.Name = "RadioButton_MatchWhole";
            this.RadioButton_MatchWhole.Size = new System.Drawing.Size(112, 17);
            this.RadioButton_MatchWhole.TabIndex = 0;
            this.RadioButton_MatchWhole.Text = "Match whole word";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Hashcode";
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(349, 147);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(72, 32);
            this.Button_Cancel.TabIndex = 12;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_Find
            // 
            this.Button_Find.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_Find.Location = new System.Drawing.Point(253, 147);
            this.Button_Find.Name = "Button_Find";
            this.Button_Find.Size = new System.Drawing.Size(72, 32);
            this.Button_Find.TabIndex = 11;
            this.Button_Find.Text = "Find";
            this.Button_Find.Click += new System.EventHandler(this.Button_Find_Click);
            // 
            // Textbox_TextSearch
            // 
            this.Textbox_TextSearch.Location = new System.Drawing.Point(21, 43);
            this.Textbox_TextSearch.Name = "Textbox_TextSearch";
            this.Textbox_TextSearch.Size = new System.Drawing.Size(384, 20);
            this.Textbox_TextSearch.TabIndex = 10;
            // 
            // FindHashCode
            // 
            this.AcceptButton = this.Button_Find;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(442, 207);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Find);
            this.Controls.Add(this.Textbox_TextSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindHashCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find Hashcode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FindHashCode_FormClosed);
            this.Load += new System.EventHandler(this.FindHashCode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RadioButton_MatchPartial;
        private System.Windows.Forms.RadioButton RadioButton_MatchWhole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_Find;
        protected internal System.Windows.Forms.TextBox Textbox_TextSearch;
    }
}
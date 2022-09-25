
namespace sb_editor.Panels
{
    partial class UserControl_MainForm_RefineSFXList
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
            this.grbRefine = new System.Windows.Forms.GroupBox();
            this.chkSortByDate = new System.Windows.Forms.CheckBox();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnUnUsed = new System.Windows.Forms.Button();
            this.btnUpdateList = new System.Windows.Forms.Button();
            this.cboWords = new System.Windows.Forms.ComboBox();
            this.grbRefine.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbRefine
            // 
            this.grbRefine.Controls.Add(this.chkSortByDate);
            this.grbRefine.Controls.Add(this.btnShowAll);
            this.grbRefine.Controls.Add(this.btnUnUsed);
            this.grbRefine.Controls.Add(this.btnUpdateList);
            this.grbRefine.Controls.Add(this.cboWords);
            this.grbRefine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbRefine.Location = new System.Drawing.Point(0, 0);
            this.grbRefine.Name = "grbRefine";
            this.grbRefine.Size = new System.Drawing.Size(323, 73);
            this.grbRefine.TabIndex = 0;
            this.grbRefine.TabStop = false;
            this.grbRefine.Text = "Refine SFX List";
            // 
            // chkSortByDate
            // 
            this.chkSortByDate.AutoSize = true;
            this.chkSortByDate.Location = new System.Drawing.Point(6, 49);
            this.chkSortByDate.Name = "chkSortByDate";
            this.chkSortByDate.Size = new System.Drawing.Size(92, 17);
            this.chkSortByDate.TabIndex = 4;
            this.chkSortByDate.Text = "Sort By Date?";
            this.chkSortByDate.UseVisualStyleBackColor = true;
            this.chkSortByDate.CheckedChanged += new System.EventHandler(this.ChkSortByDate_CheckedChanged);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(257, 46);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(60, 21);
            this.btnShowAll.TabIndex = 3;
            this.btnShowAll.Text = "All";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // btnUnUsed
            // 
            this.btnUnUsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnUsed.Location = new System.Drawing.Point(191, 46);
            this.btnUnUsed.Name = "btnUnUsed";
            this.btnUnUsed.Size = new System.Drawing.Size(60, 21);
            this.btnUnUsed.TabIndex = 2;
            this.btnUnUsed.Text = "Un-Used";
            this.btnUnUsed.UseVisualStyleBackColor = true;
            this.btnUnUsed.Click += new System.EventHandler(this.BtnUnUsed_Click);
            // 
            // btnUpdateList
            // 
            this.btnUpdateList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateList.Location = new System.Drawing.Point(257, 19);
            this.btnUpdateList.Name = "btnUpdateList";
            this.btnUpdateList.Size = new System.Drawing.Size(60, 21);
            this.btnUpdateList.TabIndex = 1;
            this.btnUpdateList.Text = "Update";
            this.btnUpdateList.UseVisualStyleBackColor = true;
            this.btnUpdateList.Click += new System.EventHandler(this.BtnUpdateList_Click);
            // 
            // cboWords
            // 
            this.cboWords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboWords.FormattingEnabled = true;
            this.cboWords.Location = new System.Drawing.Point(6, 19);
            this.cboWords.Name = "cboWords";
            this.cboWords.Size = new System.Drawing.Size(245, 21);
            this.cboWords.TabIndex = 0;
            this.cboWords.SelectedIndexChanged += new System.EventHandler(this.CboWords_SelectedIndexChanged);
            // 
            // UserControl_MainForm_RefineSFXList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbRefine);
            this.Name = "UserControl_MainForm_RefineSFXList";
            this.Size = new System.Drawing.Size(323, 73);
            this.grbRefine.ResumeLayout(false);
            this.grbRefine.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbRefine;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnUnUsed;
        private System.Windows.Forms.Button btnUpdateList;
        protected internal System.Windows.Forms.ComboBox cboWords;
        protected internal System.Windows.Forms.CheckBox chkSortByDate;
    }
}

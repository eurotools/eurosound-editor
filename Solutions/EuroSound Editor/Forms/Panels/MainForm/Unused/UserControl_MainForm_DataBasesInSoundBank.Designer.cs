
namespace sb_editor.Panels
{
    partial class UserControl_MainForm_DataBasesInSoundBank
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
            this.components = new System.ComponentModel.Container();
            this.grbDatabasesInSoundBank = new System.Windows.Forms.GroupBox();
            this.btnRemoveDataBase = new System.Windows.Forms.Button();
            this.lstDatabases = new System.Windows.Forms.ListBox();
            this.lstContextMenu = new System.Windows.Forms.ContextMenu();
            this.mnuRemoveDataBases = new System.Windows.Forms.MenuItem();
            this.mnuProperties = new System.Windows.Forms.MenuItem();
            this.mnuSelectDataBase = new System.Windows.Forms.MenuItem();
            this.mnuRemoveSelDataBase = new System.Windows.Forms.MenuItem();
            this.ToolTip_Help = new System.Windows.Forms.ToolTip(this.components);
            this.grbDatabasesInSoundBank.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDatabasesInSoundBank
            // 
            this.grbDatabasesInSoundBank.Controls.Add(this.btnRemoveDataBase);
            this.grbDatabasesInSoundBank.Controls.Add(this.lstDatabases);
            this.grbDatabasesInSoundBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbDatabasesInSoundBank.Location = new System.Drawing.Point(0, 0);
            this.grbDatabasesInSoundBank.Name = "grbDatabasesInSoundBank";
            this.grbDatabasesInSoundBank.Size = new System.Drawing.Size(223, 179);
            this.grbDatabasesInSoundBank.TabIndex = 0;
            this.grbDatabasesInSoundBank.TabStop = false;
            this.grbDatabasesInSoundBank.Text = "DataBases In SoundBank";
            // 
            // btnRemoveDataBase
            // 
            this.btnRemoveDataBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveDataBase.Location = new System.Drawing.Point(6, 150);
            this.btnRemoveDataBase.Name = "btnRemoveDataBase";
            this.btnRemoveDataBase.Size = new System.Drawing.Size(211, 23);
            this.btnRemoveDataBase.TabIndex = 1;
            this.btnRemoveDataBase.Text = "Remove Databases >>>";
            this.ToolTip_Help.SetToolTip(this.btnRemoveDataBase, "Remove Selected DataBase(s) From Selected SoundBank");
            this.btnRemoveDataBase.UseVisualStyleBackColor = true;
            this.btnRemoveDataBase.Click += new System.EventHandler(this.BtnRemoveDataBase_Click);
            // 
            // lstDatabases
            // 
            this.lstDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDatabases.ContextMenu = this.lstContextMenu;
            this.lstDatabases.FormattingEnabled = true;
            this.lstDatabases.Location = new System.Drawing.Point(6, 19);
            this.lstDatabases.Name = "lstDatabases";
            this.lstDatabases.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstDatabases.Size = new System.Drawing.Size(211, 121);
            this.lstDatabases.TabIndex = 0;
            this.ToolTip_Help.SetToolTip(this.lstDatabases, "All DataBases in the Selected SoundBank");
            // 
            // lstContextMenu
            // 
            this.lstContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuRemoveDataBases,
            this.mnuProperties,
            this.mnuSelectDataBase,
            this.mnuRemoveSelDataBase});
            // 
            // mnuRemoveDataBases
            // 
            this.mnuRemoveDataBases.Index = 0;
            this.mnuRemoveDataBases.Text = "Remove DB";
            this.mnuRemoveDataBases.Click += new System.EventHandler(this.MnuRemoveDataBases_Click);
            // 
            // mnuProperties
            // 
            this.mnuProperties.Index = 1;
            this.mnuProperties.Text = "Properties";
            this.mnuProperties.Click += new System.EventHandler(this.MnuProperties_Click);
            // 
            // mnuSelectDataBase
            // 
            this.mnuSelectDataBase.Index = 2;
            this.mnuSelectDataBase.Text = "Select DB";
            this.mnuSelectDataBase.Click += new System.EventHandler(this.MnuSelectDataBase_Click);
            // 
            // mnuRemoveSelDataBase
            // 
            this.mnuRemoveSelDataBase.Index = 3;
            this.mnuRemoveSelDataBase.Text = "Remove Selected DB";
            this.mnuRemoveSelDataBase.Click += new System.EventHandler(this.MnuRemoveSelDataBase_Click);
            // 
            // UserControl_MainForm_DataBasesInSoundBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbDatabasesInSoundBank);
            this.Name = "UserControl_MainForm_DataBasesInSoundBank";
            this.Size = new System.Drawing.Size(223, 179);
            this.grbDatabasesInSoundBank.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDatabasesInSoundBank;
        private System.Windows.Forms.Button btnRemoveDataBase;
        private System.Windows.Forms.MenuItem mnuRemoveDataBases;
        private System.Windows.Forms.MenuItem mnuProperties;
        private System.Windows.Forms.MenuItem mnuSelectDataBase;
        private System.Windows.Forms.MenuItem mnuRemoveSelDataBase;
        protected internal System.Windows.Forms.ContextMenu lstContextMenu;
        private System.Windows.Forms.ToolTip ToolTip_Help;
        protected internal System.Windows.Forms.ListBox lstDatabases;
    }
}


namespace MarkersEditor
{
    partial class Frm_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.lvwMarkers = new System.Windows.Forms.ListView();
            this.Col_Milliseconds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_MarkerType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_MarkerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grbMarkers = new System.Windows.Forms.GroupBox();
            this.btnAddMarker_Jump = new System.Windows.Forms.Button();
            this.btnAddMarker_Pause = new System.Windows.Forms.Button();
            this.btnAddMarker_End = new System.Windows.Forms.Button();
            this.btnAddMarker_Goto = new System.Windows.Forms.Button();
            this.btnAddMarker_Loop = new System.Windows.Forms.Button();
            this.btnAddMarker_Start = new System.Windows.Forms.Button();
            this.grbActions = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSaveMidiFile = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.grbMarkers.SuspendLayout();
            this.grbActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwMarkers
            // 
            this.lvwMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMarkers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Milliseconds,
            this.Col_MarkerType,
            this.Col_MarkerName});
            this.lvwMarkers.FullRowSelect = true;
            this.lvwMarkers.GridLines = true;
            this.lvwMarkers.HideSelection = false;
            this.lvwMarkers.Location = new System.Drawing.Point(12, 12);
            this.lvwMarkers.Name = "lvwMarkers";
            this.lvwMarkers.Size = new System.Drawing.Size(425, 293);
            this.lvwMarkers.TabIndex = 0;
            this.lvwMarkers.UseCompatibleStateImageBehavior = false;
            this.lvwMarkers.View = System.Windows.Forms.View.Details;
            // 
            // Col_Milliseconds
            // 
            this.Col_Milliseconds.Text = "Milliseconds";
            this.Col_Milliseconds.Width = 150;
            // 
            // Col_MarkerType
            // 
            this.Col_MarkerType.Text = "Marker Type";
            this.Col_MarkerType.Width = 80;
            // 
            // Col_MarkerName
            // 
            this.Col_MarkerName.Text = "Marker Name";
            this.Col_MarkerName.Width = 170;
            // 
            // grbMarkers
            // 
            this.grbMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMarkers.Controls.Add(this.btnAddMarker_Jump);
            this.grbMarkers.Controls.Add(this.btnAddMarker_Pause);
            this.grbMarkers.Controls.Add(this.btnAddMarker_End);
            this.grbMarkers.Controls.Add(this.btnAddMarker_Goto);
            this.grbMarkers.Controls.Add(this.btnAddMarker_Loop);
            this.grbMarkers.Controls.Add(this.btnAddMarker_Start);
            this.grbMarkers.Location = new System.Drawing.Point(12, 311);
            this.grbMarkers.Name = "grbMarkers";
            this.grbMarkers.Size = new System.Drawing.Size(537, 53);
            this.grbMarkers.TabIndex = 2;
            this.grbMarkers.TabStop = false;
            this.grbMarkers.Text = "Available Markers";
            // 
            // btnAddMarker_Jump
            // 
            this.btnAddMarker_Jump.Enabled = false;
            this.btnAddMarker_Jump.Location = new System.Drawing.Point(330, 19);
            this.btnAddMarker_Jump.Name = "btnAddMarker_Jump";
            this.btnAddMarker_Jump.Size = new System.Drawing.Size(75, 23);
            this.btnAddMarker_Jump.TabIndex = 4;
            this.btnAddMarker_Jump.Text = "Jump";
            this.btnAddMarker_Jump.UseVisualStyleBackColor = true;
            this.btnAddMarker_Jump.Click += new System.EventHandler(this.Button_AddMarker_Jump_Click);
            // 
            // btnAddMarker_Pause
            // 
            this.btnAddMarker_Pause.Enabled = false;
            this.btnAddMarker_Pause.Location = new System.Drawing.Point(249, 19);
            this.btnAddMarker_Pause.Name = "btnAddMarker_Pause";
            this.btnAddMarker_Pause.Size = new System.Drawing.Size(75, 23);
            this.btnAddMarker_Pause.TabIndex = 3;
            this.btnAddMarker_Pause.Text = "Pause";
            this.btnAddMarker_Pause.UseVisualStyleBackColor = true;
            this.btnAddMarker_Pause.Click += new System.EventHandler(this.Button_AddMarker_Pause_Click);
            // 
            // btnAddMarker_End
            // 
            this.btnAddMarker_End.Location = new System.Drawing.Point(411, 19);
            this.btnAddMarker_End.Name = "btnAddMarker_End";
            this.btnAddMarker_End.Size = new System.Drawing.Size(75, 23);
            this.btnAddMarker_End.TabIndex = 5;
            this.btnAddMarker_End.Text = "End";
            this.btnAddMarker_End.UseVisualStyleBackColor = true;
            this.btnAddMarker_End.Click += new System.EventHandler(this.Button_AddMarker_End_Click);
            // 
            // btnAddMarker_Goto
            // 
            this.btnAddMarker_Goto.Location = new System.Drawing.Point(168, 19);
            this.btnAddMarker_Goto.Name = "btnAddMarker_Goto";
            this.btnAddMarker_Goto.Size = new System.Drawing.Size(75, 23);
            this.btnAddMarker_Goto.TabIndex = 2;
            this.btnAddMarker_Goto.Text = "Goto";
            this.btnAddMarker_Goto.UseVisualStyleBackColor = true;
            this.btnAddMarker_Goto.Click += new System.EventHandler(this.Button_AddMarker_Goto_Click);
            // 
            // btnAddMarker_Loop
            // 
            this.btnAddMarker_Loop.Location = new System.Drawing.Point(87, 19);
            this.btnAddMarker_Loop.Name = "btnAddMarker_Loop";
            this.btnAddMarker_Loop.Size = new System.Drawing.Size(75, 23);
            this.btnAddMarker_Loop.TabIndex = 1;
            this.btnAddMarker_Loop.Text = "Loop";
            this.btnAddMarker_Loop.UseVisualStyleBackColor = true;
            this.btnAddMarker_Loop.Click += new System.EventHandler(this.Button_AddMarker_Loop_Click);
            // 
            // btnAddMarker_Start
            // 
            this.btnAddMarker_Start.Location = new System.Drawing.Point(6, 19);
            this.btnAddMarker_Start.Name = "btnAddMarker_Start";
            this.btnAddMarker_Start.Size = new System.Drawing.Size(75, 23);
            this.btnAddMarker_Start.TabIndex = 0;
            this.btnAddMarker_Start.Text = "Start";
            this.btnAddMarker_Start.UseVisualStyleBackColor = true;
            this.btnAddMarker_Start.Click += new System.EventHandler(this.Button_AddMarker_Start_Click);
            // 
            // grbActions
            // 
            this.grbActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbActions.Controls.Add(this.btnExit);
            this.grbActions.Controls.Add(this.btnRemove);
            this.grbActions.Controls.Add(this.btnSaveMidiFile);
            this.grbActions.Controls.Add(this.btnClearList);
            this.grbActions.Location = new System.Drawing.Point(443, 12);
            this.grbActions.Name = "grbActions";
            this.grbActions.Size = new System.Drawing.Size(106, 293);
            this.grbActions.TabIndex = 1;
            this.grbActions.TabStop = false;
            this.grbActions.Text = "Actions:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(6, 106);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(6, 19);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(94, 23);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.Button_Remove_Click);
            // 
            // btnSaveMidiFile
            // 
            this.btnSaveMidiFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveMidiFile.Location = new System.Drawing.Point(6, 77);
            this.btnSaveMidiFile.Name = "btnSaveMidiFile";
            this.btnSaveMidiFile.Size = new System.Drawing.Size(94, 23);
            this.btnSaveMidiFile.TabIndex = 2;
            this.btnSaveMidiFile.Text = "Save";
            this.btnSaveMidiFile.UseVisualStyleBackColor = true;
            this.btnSaveMidiFile.Click += new System.EventHandler(this.Button_SaveMidiFile_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearList.Location = new System.Drawing.Point(6, 48);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(94, 23);
            this.btnClearList.TabIndex = 1;
            this.btnClearList.Text = "Clear List";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.Button_ClearList_Click);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "MIDI (*.mid)|*.mid|All Files (*.*)|*.*";
            this.SaveFileDialog.FilterIndex = 0;
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 376);
            this.Controls.Add(this.grbActions);
            this.Controls.Add(this.grbMarkers);
            this.Controls.Add(this.lvwMarkers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Music Marker Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grbMarkers.ResumeLayout(false);
            this.grbActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvwMarkers;
        private System.Windows.Forms.GroupBox grbMarkers;
        private System.Windows.Forms.Button btnAddMarker_Loop;
        private System.Windows.Forms.Button btnAddMarker_Start;
        private System.Windows.Forms.Button btnAddMarker_End;
        private System.Windows.Forms.Button btnAddMarker_Goto;
        private System.Windows.Forms.GroupBox grbActions;
        private System.Windows.Forms.Button btnSaveMidiFile;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.ColumnHeader Col_Milliseconds;
        private System.Windows.Forms.ColumnHeader Col_MarkerType;
        private System.Windows.Forms.ColumnHeader Col_MarkerName;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAddMarker_Pause;
        private System.Windows.Forms.Button btnAddMarker_Jump;
    }
}


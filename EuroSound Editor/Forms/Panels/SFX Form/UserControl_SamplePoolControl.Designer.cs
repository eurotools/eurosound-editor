
namespace EuroSound_Editor.Panels
{
    partial class UserControl_SamplePoolControl
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
            this.grbSamplePool = new System.Windows.Forms.GroupBox();
            this.grbSampleProperties = new System.Windows.Forms.GroupBox();
            this.lblRandomPan = new System.Windows.Forms.Label();
            this.lblPan = new System.Windows.Forms.Label();
            this.lblRandomVolume = new System.Windows.Forms.Label();
            this.lblBaseVolume = new System.Windows.Forms.Label();
            this.lblRandomPitch = new System.Windows.Forms.Label();
            this.lblPitch = new System.Windows.Forms.Label();
            this.nudRandomPan = new System.Windows.Forms.NumericUpDown();
            this.nudPan = new System.Windows.Forms.NumericUpDown();
            this.nudRandomVolume = new System.Windows.Forms.NumericUpDown();
            this.nudBaseVolume = new System.Windows.Forms.NumericUpDown();
            this.nudRandomPitchOffset = new System.Windows.Forms.NumericUpDown();
            this.nudPitchOffset = new System.Windows.Forms.NumericUpDown();
            this.chkPolyphonic = new System.Windows.Forms.CheckBox();
            this.chkShuffled = new System.Windows.Forms.CheckBox();
            this.rdoMultiSample = new System.Windows.Forms.RadioButton();
            this.chkRandomPick = new System.Windows.Forms.CheckBox();
            this.rdoSingle = new System.Windows.Forms.RadioButton();
            this.lblInterSampleDelay = new System.Windows.Forms.Label();
            this.nudMaxDelay = new System.Windows.Forms.NumericUpDown();
            this.nudMinDelay = new System.Windows.Forms.NumericUpDown();
            this.lblMinDelay = new System.Windows.Forms.Label();
            this.lblMaxDelay = new System.Windows.Forms.Label();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.LineShape_Divider1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.grbSamplePool.SuspendLayout();
            this.grbSampleProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomPitchOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPitchOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // grbSamplePool
            // 
            this.grbSamplePool.Controls.Add(this.grbSampleProperties);
            this.grbSamplePool.Controls.Add(this.chkPolyphonic);
            this.grbSamplePool.Controls.Add(this.chkShuffled);
            this.grbSamplePool.Controls.Add(this.rdoMultiSample);
            this.grbSamplePool.Controls.Add(this.chkRandomPick);
            this.grbSamplePool.Controls.Add(this.rdoSingle);
            this.grbSamplePool.Controls.Add(this.lblInterSampleDelay);
            this.grbSamplePool.Controls.Add(this.nudMaxDelay);
            this.grbSamplePool.Controls.Add(this.nudMinDelay);
            this.grbSamplePool.Controls.Add(this.lblMinDelay);
            this.grbSamplePool.Controls.Add(this.lblMaxDelay);
            this.grbSamplePool.Controls.Add(this.chkLoop);
            this.grbSamplePool.Controls.Add(this.shapeContainer1);
            this.grbSamplePool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbSamplePool.Location = new System.Drawing.Point(0, 0);
            this.grbSamplePool.Name = "grbSamplePool";
            this.grbSamplePool.Size = new System.Drawing.Size(383, 272);
            this.grbSamplePool.TabIndex = 0;
            this.grbSamplePool.TabStop = false;
            this.grbSamplePool.Text = "Sample Pool Control";
            // 
            // grbSampleProperties
            // 
            this.grbSampleProperties.Controls.Add(this.lblRandomPan);
            this.grbSampleProperties.Controls.Add(this.lblPan);
            this.grbSampleProperties.Controls.Add(this.lblRandomVolume);
            this.grbSampleProperties.Controls.Add(this.lblBaseVolume);
            this.grbSampleProperties.Controls.Add(this.lblRandomPitch);
            this.grbSampleProperties.Controls.Add(this.lblPitch);
            this.grbSampleProperties.Controls.Add(this.nudRandomPan);
            this.grbSampleProperties.Controls.Add(this.nudPan);
            this.grbSampleProperties.Controls.Add(this.nudRandomVolume);
            this.grbSampleProperties.Controls.Add(this.nudBaseVolume);
            this.grbSampleProperties.Controls.Add(this.nudRandomPitchOffset);
            this.grbSampleProperties.Controls.Add(this.nudPitchOffset);
            this.grbSampleProperties.Location = new System.Drawing.Point(143, 104);
            this.grbSampleProperties.Name = "grbSampleProperties";
            this.grbSampleProperties.Size = new System.Drawing.Size(227, 166);
            this.grbSampleProperties.TabIndex = 19;
            this.grbSampleProperties.TabStop = false;
            this.grbSampleProperties.Text = "Default Sample Properties";
            this.grbSampleProperties.Visible = false;
            // 
            // lblRandomPan
            // 
            this.lblRandomPan.Location = new System.Drawing.Point(6, 139);
            this.lblRandomPan.Name = "lblRandomPan";
            this.lblRandomPan.Size = new System.Drawing.Size(132, 13);
            this.lblRandomPan.TabIndex = 10;
            this.lblRandomPan.Text = "Random Pan Offset";
            // 
            // lblPan
            // 
            this.lblPan.Location = new System.Drawing.Point(6, 115);
            this.lblPan.Name = "lblPan";
            this.lblPan.Size = new System.Drawing.Size(132, 13);
            this.lblPan.TabIndex = 8;
            this.lblPan.Text = "Pan (-100 to 100)";
            // 
            // lblRandomVolume
            // 
            this.lblRandomVolume.Location = new System.Drawing.Point(6, 91);
            this.lblRandomVolume.Name = "lblRandomVolume";
            this.lblRandomVolume.Size = new System.Drawing.Size(142, 13);
            this.lblRandomVolume.TabIndex = 6;
            this.lblRandomVolume.Text = "Random Volume Offset: (-/+)";
            // 
            // lblBaseVolume
            // 
            this.lblBaseVolume.Location = new System.Drawing.Point(6, 67);
            this.lblBaseVolume.Name = "lblBaseVolume";
            this.lblBaseVolume.Size = new System.Drawing.Size(142, 13);
            this.lblBaseVolume.TabIndex = 4;
            this.lblBaseVolume.Text = "Base Volume: (0-100)";
            // 
            // lblRandomPitch
            // 
            this.lblRandomPitch.Location = new System.Drawing.Point(6, 43);
            this.lblRandomPitch.Name = "lblRandomPitch";
            this.lblRandomPitch.Size = new System.Drawing.Size(142, 13);
            this.lblRandomPitch.TabIndex = 2;
            this.lblRandomPitch.Text = "Random Pitch Offset: (-/+)";
            // 
            // lblPitch
            // 
            this.lblPitch.Location = new System.Drawing.Point(6, 19);
            this.lblPitch.Name = "lblPitch";
            this.lblPitch.Size = new System.Drawing.Size(142, 13);
            this.lblPitch.TabIndex = 0;
            this.lblPitch.Text = "Pitch Offset: (semitones)";
            // 
            // nudRandomPan
            // 
            this.nudRandomPan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRandomPan.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRandomPan.Location = new System.Drawing.Point(155, 137);
            this.nudRandomPan.Name = "nudRandomPan";
            this.nudRandomPan.Size = new System.Drawing.Size(66, 20);
            this.nudRandomPan.TabIndex = 11;
            this.nudRandomPan.ValueChanged += new System.EventHandler(this.NudRandomPan_ValueChanged);
            // 
            // nudPan
            // 
            this.nudPan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPan.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPan.Location = new System.Drawing.Point(155, 113);
            this.nudPan.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudPan.Name = "nudPan";
            this.nudPan.Size = new System.Drawing.Size(66, 20);
            this.nudPan.TabIndex = 9;
            this.nudPan.ValueChanged += new System.EventHandler(this.NudPan_ValueChanged);
            // 
            // nudRandomVolume
            // 
            this.nudRandomVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRandomVolume.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRandomVolume.Location = new System.Drawing.Point(155, 89);
            this.nudRandomVolume.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147418112});
            this.nudRandomVolume.Name = "nudRandomVolume";
            this.nudRandomVolume.Size = new System.Drawing.Size(66, 20);
            this.nudRandomVolume.TabIndex = 7;
            this.nudRandomVolume.ValueChanged += new System.EventHandler(this.NudRandomVolume_ValueChanged);
            // 
            // nudBaseVolume
            // 
            this.nudBaseVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudBaseVolume.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudBaseVolume.Location = new System.Drawing.Point(155, 65);
            this.nudBaseVolume.Name = "nudBaseVolume";
            this.nudBaseVolume.Size = new System.Drawing.Size(66, 20);
            this.nudBaseVolume.TabIndex = 5;
            this.nudBaseVolume.ValueChanged += new System.EventHandler(this.NudBaseVolume_ValueChanged);
            // 
            // nudRandomPitchOffset
            // 
            this.nudRandomPitchOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRandomPitchOffset.DecimalPlaces = 1;
            this.nudRandomPitchOffset.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudRandomPitchOffset.Location = new System.Drawing.Point(155, 41);
            this.nudRandomPitchOffset.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nudRandomPitchOffset.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            -2147483648});
            this.nudRandomPitchOffset.Name = "nudRandomPitchOffset";
            this.nudRandomPitchOffset.Size = new System.Drawing.Size(66, 20);
            this.nudRandomPitchOffset.TabIndex = 3;
            this.nudRandomPitchOffset.ValueChanged += new System.EventHandler(this.NudRandomPitchOffset_ValueChanged);
            // 
            // nudPitchOffset
            // 
            this.nudPitchOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPitchOffset.DecimalPlaces = 1;
            this.nudPitchOffset.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudPitchOffset.Location = new System.Drawing.Point(155, 17);
            this.nudPitchOffset.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nudPitchOffset.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            -2147483648});
            this.nudPitchOffset.Name = "nudPitchOffset";
            this.nudPitchOffset.Size = new System.Drawing.Size(66, 20);
            this.nudPitchOffset.TabIndex = 1;
            this.nudPitchOffset.ValueChanged += new System.EventHandler(this.NudPitchOffset_ValueChanged);
            // 
            // chkPolyphonic
            // 
            this.chkPolyphonic.AutoSize = true;
            this.chkPolyphonic.Location = new System.Drawing.Point(58, 180);
            this.chkPolyphonic.Name = "chkPolyphonic";
            this.chkPolyphonic.Size = new System.Drawing.Size(78, 17);
            this.chkPolyphonic.TabIndex = 10;
            this.chkPolyphonic.Text = "Polyphonic";
            this.chkPolyphonic.UseVisualStyleBackColor = true;
            this.chkPolyphonic.CheckedChanged += new System.EventHandler(this.ChkPolyphonic_CheckedChanged);
            // 
            // chkShuffled
            // 
            this.chkShuffled.AutoSize = true;
            this.chkShuffled.Location = new System.Drawing.Point(58, 162);
            this.chkShuffled.Name = "chkShuffled";
            this.chkShuffled.Size = new System.Drawing.Size(65, 17);
            this.chkShuffled.TabIndex = 9;
            this.chkShuffled.Text = "Shuffled";
            this.chkShuffled.UseVisualStyleBackColor = true;
            this.chkShuffled.CheckedChanged += new System.EventHandler(this.ChkShuffled_CheckedChanged);
            // 
            // rdoMultiSample
            // 
            this.rdoMultiSample.AutoSize = true;
            this.rdoMultiSample.Location = new System.Drawing.Point(10, 143);
            this.rdoMultiSample.Name = "rdoMultiSample";
            this.rdoMultiSample.Size = new System.Drawing.Size(85, 17);
            this.rdoMultiSample.TabIndex = 8;
            this.rdoMultiSample.TabStop = true;
            this.rdoMultiSample.Text = "Multi-Sample";
            this.rdoMultiSample.UseVisualStyleBackColor = true;
            this.rdoMultiSample.CheckedChanged += new System.EventHandler(this.RdoMultiSample_CheckedChanged);
            // 
            // chkRandomPick
            // 
            this.chkRandomPick.AutoSize = true;
            this.chkRandomPick.Enabled = false;
            this.chkRandomPick.Location = new System.Drawing.Point(58, 125);
            this.chkRandomPick.Name = "chkRandomPick";
            this.chkRandomPick.Size = new System.Drawing.Size(90, 17);
            this.chkRandomPick.TabIndex = 7;
            this.chkRandomPick.Text = "Random Pick";
            this.chkRandomPick.UseVisualStyleBackColor = true;
            // 
            // rdoSingle
            // 
            this.rdoSingle.AutoSize = true;
            this.rdoSingle.Location = new System.Drawing.Point(10, 108);
            this.rdoSingle.Name = "rdoSingle";
            this.rdoSingle.Size = new System.Drawing.Size(54, 17);
            this.rdoSingle.TabIndex = 6;
            this.rdoSingle.TabStop = true;
            this.rdoSingle.Text = "Single";
            this.rdoSingle.UseVisualStyleBackColor = true;
            this.rdoSingle.CheckedChanged += new System.EventHandler(this.RdoSingle_CheckedChanged);
            // 
            // lblInterSampleDelay
            // 
            this.lblInterSampleDelay.AutoSize = true;
            this.lblInterSampleDelay.Location = new System.Drawing.Point(10, 50);
            this.lblInterSampleDelay.Name = "lblInterSampleDelay";
            this.lblInterSampleDelay.Size = new System.Drawing.Size(99, 13);
            this.lblInterSampleDelay.TabIndex = 1;
            this.lblInterSampleDelay.Text = "Inter-Sample Delay:";
            // 
            // nudMaxDelay
            // 
            this.nudMaxDelay.Location = new System.Drawing.Point(236, 73);
            this.nudMaxDelay.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this.nudMaxDelay.Minimum = new decimal(new int[] {
            32000,
            0,
            0,
            -2147483648});
            this.nudMaxDelay.Name = "nudMaxDelay";
            this.nudMaxDelay.Size = new System.Drawing.Size(70, 20);
            this.nudMaxDelay.TabIndex = 5;
            // 
            // nudMinDelay
            // 
            this.nudMinDelay.Location = new System.Drawing.Point(76, 73);
            this.nudMinDelay.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this.nudMinDelay.Minimum = new decimal(new int[] {
            32000,
            0,
            0,
            -2147483648});
            this.nudMinDelay.Name = "nudMinDelay";
            this.nudMinDelay.Size = new System.Drawing.Size(70, 20);
            this.nudMinDelay.TabIndex = 3;
            // 
            // lblMinDelay
            // 
            this.lblMinDelay.AutoSize = true;
            this.lblMinDelay.Location = new System.Drawing.Point(10, 75);
            this.lblMinDelay.Name = "lblMinDelay";
            this.lblMinDelay.Size = new System.Drawing.Size(60, 13);
            this.lblMinDelay.TabIndex = 2;
            this.lblMinDelay.Text = "Min. Delay:";
            // 
            // lblMaxDelay
            // 
            this.lblMaxDelay.AutoSize = true;
            this.lblMaxDelay.Location = new System.Drawing.Point(167, 75);
            this.lblMaxDelay.Name = "lblMaxDelay";
            this.lblMaxDelay.Size = new System.Drawing.Size(63, 13);
            this.lblMaxDelay.TabIndex = 4;
            this.lblMaxDelay.Text = "Max. Delay:";
            // 
            // chkLoop
            // 
            this.chkLoop.AutoSize = true;
            this.chkLoop.Location = new System.Drawing.Point(10, 25);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(50, 17);
            this.chkLoop.TabIndex = 0;
            this.chkLoop.Text = "Loop";
            this.chkLoop.UseVisualStyleBackColor = true;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.LineShape_Divider1});
            this.shapeContainer1.Size = new System.Drawing.Size(377, 253);
            this.shapeContainer1.TabIndex = 6;
            this.shapeContainer1.TabStop = false;
            // 
            // LineShape_Divider1
            // 
            this.LineShape_Divider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineShape_Divider1.Name = "LineShape_Divider1";
            this.LineShape_Divider1.X1 = 7;
            this.LineShape_Divider1.X2 = 366;
            this.LineShape_Divider1.Y1 = 83;
            this.LineShape_Divider1.Y2 = 83;
            // 
            // UserControl_SamplePoolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbSamplePool);
            this.Name = "UserControl_SamplePoolControl";
            this.Size = new System.Drawing.Size(383, 272);
            this.grbSamplePool.ResumeLayout(false);
            this.grbSamplePool.PerformLayout();
            this.grbSampleProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomPitchOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPitchOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSamplePool;
        private System.Windows.Forms.Label lblInterSampleDelay;
        private System.Windows.Forms.Label lblMinDelay;
        private System.Windows.Forms.Label lblMaxDelay;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape LineShape_Divider1;
        protected internal System.Windows.Forms.CheckBox chkRandomPick;
        protected internal System.Windows.Forms.CheckBox chkPolyphonic;
        protected internal System.Windows.Forms.CheckBox chkShuffled;
        protected internal System.Windows.Forms.RadioButton rdoMultiSample;
        protected internal System.Windows.Forms.RadioButton rdoSingle;
        protected internal System.Windows.Forms.NumericUpDown nudMaxDelay;
        protected internal System.Windows.Forms.NumericUpDown nudMinDelay;
        protected internal System.Windows.Forms.CheckBox chkLoop;
        protected internal System.Windows.Forms.GroupBox grbSampleProperties;
        protected internal System.Windows.Forms.Label lblRandomPan;
        protected internal System.Windows.Forms.Label lblPan;
        protected internal System.Windows.Forms.Label lblRandomVolume;
        protected internal System.Windows.Forms.Label lblBaseVolume;
        protected internal System.Windows.Forms.Label lblRandomPitch;
        protected internal System.Windows.Forms.Label lblPitch;
        protected internal System.Windows.Forms.NumericUpDown nudRandomPan;
        protected internal System.Windows.Forms.NumericUpDown nudPan;
        protected internal System.Windows.Forms.NumericUpDown nudRandomVolume;
        protected internal System.Windows.Forms.NumericUpDown nudBaseVolume;
        protected internal System.Windows.Forms.NumericUpDown nudRandomPitchOffset;
        protected internal System.Windows.Forms.NumericUpDown nudPitchOffset;
    }
}

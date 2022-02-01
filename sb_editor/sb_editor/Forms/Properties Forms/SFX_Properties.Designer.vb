<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SFX_Properties
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SFX_Properties))
        Me.Label_SFX_Name = New System.Windows.Forms.Label()
        Me.Textbox_SFX_Name = New System.Windows.Forms.TextBox()
        Me.GroupBox_FileInfo = New System.Windows.Forms.GroupBox()
        Me.Label_ModifiedBy_Value = New System.Windows.Forms.Label()
        Me.Label_ModifiedBy = New System.Windows.Forms.Label()
        Me.Label_CreatedBy_Value = New System.Windows.Forms.Label()
        Me.Label_CreatedBy = New System.Windows.Forms.Label()
        Me.Label_Value_LastModified = New System.Windows.Forms.Label()
        Me.Label_LastModified = New System.Windows.Forms.Label()
        Me.Label_Value_FirstCreated = New System.Windows.Forms.Label()
        Me.Label_FirstCreated = New System.Windows.Forms.Label()
        Me.Label_Value_Size = New System.Windows.Forms.Label()
        Me.Label_Size = New System.Windows.Forms.Label()
        Me.Label_DataBase_Dependencies = New System.Windows.Forms.Label()
        Me.Textbox_DataBase_Deps = New System.Windows.Forms.TextBox()
        Me.ListBox_DataBase_Dependencies = New System.Windows.Forms.ListBox()
        Me.ListBox_Samples = New System.Windows.Forms.ListBox()
        Me.Label_Samples = New System.Windows.Forms.Label()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.GroupBox_Recount = New System.Windows.Forms.GroupBox()
        Me.Label_SampleCount_Value = New System.Windows.Forms.Label()
        Me.Label_SampleCount = New System.Windows.Forms.Label()
        Me.Label_SfxCount_Value = New System.Windows.Forms.Label()
        Me.Label_DatabaseCount_Value = New System.Windows.Forms.Label()
        Me.Label_SFXCount = New System.Windows.Forms.Label()
        Me.Label_DataBaseCount = New System.Windows.Forms.Label()
        Me.GroupBox_FileInfo.SuspendLayout()
        Me.GroupBox_Recount.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_SFX_Name
        '
        Me.Label_SFX_Name.AutoSize = True
        Me.Label_SFX_Name.Location = New System.Drawing.Point(12, 15)
        Me.Label_SFX_Name.Name = "Label_SFX_Name"
        Me.Label_SFX_Name.Size = New System.Drawing.Size(38, 13)
        Me.Label_SFX_Name.TabIndex = 0
        Me.Label_SFX_Name.Text = "Name:"
        '
        'Textbox_SFX_Name
        '
        Me.Textbox_SFX_Name.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Textbox_SFX_Name.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_SFX_Name.Location = New System.Drawing.Point(56, 12)
        Me.Textbox_SFX_Name.Name = "Textbox_SFX_Name"
        Me.Textbox_SFX_Name.ReadOnly = True
        Me.Textbox_SFX_Name.Size = New System.Drawing.Size(323, 20)
        Me.Textbox_SFX_Name.TabIndex = 1
        '
        'GroupBox_FileInfo
        '
        Me.GroupBox_FileInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_ModifiedBy_Value)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_ModifiedBy)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_CreatedBy_Value)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_CreatedBy)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_Value_LastModified)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_LastModified)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_Value_FirstCreated)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_FirstCreated)
        Me.GroupBox_FileInfo.Location = New System.Drawing.Point(12, 38)
        Me.GroupBox_FileInfo.Name = "GroupBox_FileInfo"
        Me.GroupBox_FileInfo.Size = New System.Drawing.Size(367, 94)
        Me.GroupBox_FileInfo.TabIndex = 2
        Me.GroupBox_FileInfo.TabStop = False
        Me.GroupBox_FileInfo.Text = "File Info"
        '
        'Label_ModifiedBy_Value
        '
        Me.Label_ModifiedBy_Value.AutoSize = True
        Me.Label_ModifiedBy_Value.Location = New System.Drawing.Point(100, 68)
        Me.Label_ModifiedBy_Value.Name = "Label_ModifiedBy_Value"
        Me.Label_ModifiedBy_Value.Size = New System.Drawing.Size(35, 13)
        Me.Label_ModifiedBy_Value.TabIndex = 7
        Me.Label_ModifiedBy_Value.Text = "XXXX"
        '
        'Label_ModifiedBy
        '
        Me.Label_ModifiedBy.AutoSize = True
        Me.Label_ModifiedBy.Location = New System.Drawing.Point(6, 67)
        Me.Label_ModifiedBy.Name = "Label_ModifiedBy"
        Me.Label_ModifiedBy.Size = New System.Drawing.Size(88, 13)
        Me.Label_ModifiedBy.TabIndex = 6
        Me.Label_ModifiedBy.Text = "Last Modified By:"
        '
        'Label_CreatedBy_Value
        '
        Me.Label_CreatedBy_Value.AutoSize = True
        Me.Label_CreatedBy_Value.Location = New System.Drawing.Point(100, 34)
        Me.Label_CreatedBy_Value.Name = "Label_CreatedBy_Value"
        Me.Label_CreatedBy_Value.Size = New System.Drawing.Size(35, 13)
        Me.Label_CreatedBy_Value.TabIndex = 3
        Me.Label_CreatedBy_Value.Text = "XXXX"
        '
        'Label_CreatedBy
        '
        Me.Label_CreatedBy.AutoSize = True
        Me.Label_CreatedBy.Location = New System.Drawing.Point(32, 34)
        Me.Label_CreatedBy.Name = "Label_CreatedBy"
        Me.Label_CreatedBy.Size = New System.Drawing.Size(62, 13)
        Me.Label_CreatedBy.TabIndex = 2
        Me.Label_CreatedBy.Text = "Created By:"
        '
        'Label_Value_LastModified
        '
        Me.Label_Value_LastModified.AutoSize = True
        Me.Label_Value_LastModified.Location = New System.Drawing.Point(100, 51)
        Me.Label_Value_LastModified.Name = "Label_Value_LastModified"
        Me.Label_Value_LastModified.Size = New System.Drawing.Size(120, 13)
        Me.Label_Value_LastModified.TabIndex = 5
        Me.Label_Value_LastModified.Text = "XX-XX-XXXX XX:XX:XX"
        '
        'Label_LastModified
        '
        Me.Label_LastModified.AutoSize = True
        Me.Label_LastModified.Location = New System.Drawing.Point(21, 51)
        Me.Label_LastModified.Name = "Label_LastModified"
        Me.Label_LastModified.Size = New System.Drawing.Size(73, 13)
        Me.Label_LastModified.TabIndex = 4
        Me.Label_LastModified.Text = "Last Modified:"
        '
        'Label_Value_FirstCreated
        '
        Me.Label_Value_FirstCreated.AutoSize = True
        Me.Label_Value_FirstCreated.Location = New System.Drawing.Point(100, 17)
        Me.Label_Value_FirstCreated.Name = "Label_Value_FirstCreated"
        Me.Label_Value_FirstCreated.Size = New System.Drawing.Size(120, 13)
        Me.Label_Value_FirstCreated.TabIndex = 1
        Me.Label_Value_FirstCreated.Text = "XX-XX-XXXX XX:XX:XX"
        '
        'Label_FirstCreated
        '
        Me.Label_FirstCreated.AutoSize = True
        Me.Label_FirstCreated.Location = New System.Drawing.Point(25, 17)
        Me.Label_FirstCreated.Name = "Label_FirstCreated"
        Me.Label_FirstCreated.Size = New System.Drawing.Size(69, 13)
        Me.Label_FirstCreated.TabIndex = 0
        Me.Label_FirstCreated.Text = "First Created:"
        '
        'Label_Value_Size
        '
        Me.Label_Value_Size.AutoSize = True
        Me.Label_Value_Size.Location = New System.Drawing.Point(108, 73)
        Me.Label_Value_Size.Name = "Label_Value_Size"
        Me.Label_Value_Size.Size = New System.Drawing.Size(81, 13)
        Me.Label_Value_Size.TabIndex = 7
        Me.Label_Value_Size.Text = "0 (MB) (0 bytes)"
        '
        'Label_Size
        '
        Me.Label_Size.AutoSize = True
        Me.Label_Size.Location = New System.Drawing.Point(7, 73)
        Me.Label_Size.Name = "Label_Size"
        Me.Label_Size.Size = New System.Drawing.Size(95, 13)
        Me.Label_Size.TabIndex = 6
        Me.Label_Size.Text = "Total Sample Size:"
        '
        'Label_DataBase_Dependencies
        '
        Me.Label_DataBase_Dependencies.AutoSize = True
        Me.Label_DataBase_Dependencies.Location = New System.Drawing.Point(9, 242)
        Me.Label_DataBase_Dependencies.Name = "Label_DataBase_Dependencies"
        Me.Label_DataBase_Dependencies.Size = New System.Drawing.Size(148, 13)
        Me.Label_DataBase_Dependencies.TabIndex = 4
        Me.Label_DataBase_Dependencies.Text = "DataBase File Dependencies:"
        '
        'Textbox_DataBase_Deps
        '
        Me.Textbox_DataBase_Deps.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_DataBase_Deps.Location = New System.Drawing.Point(163, 239)
        Me.Textbox_DataBase_Deps.Name = "Textbox_DataBase_Deps"
        Me.Textbox_DataBase_Deps.ReadOnly = True
        Me.Textbox_DataBase_Deps.Size = New System.Drawing.Size(50, 20)
        Me.Textbox_DataBase_Deps.TabIndex = 5
        '
        'ListBox_DataBase_Dependencies
        '
        Me.ListBox_DataBase_Dependencies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_DataBase_Dependencies.FormattingEnabled = True
        Me.ListBox_DataBase_Dependencies.HorizontalScrollbar = True
        Me.ListBox_DataBase_Dependencies.Location = New System.Drawing.Point(12, 265)
        Me.ListBox_DataBase_Dependencies.Name = "ListBox_DataBase_Dependencies"
        Me.ListBox_DataBase_Dependencies.Size = New System.Drawing.Size(367, 121)
        Me.ListBox_DataBase_Dependencies.TabIndex = 6
        '
        'ListBox_Samples
        '
        Me.ListBox_Samples.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Samples.FormattingEnabled = True
        Me.ListBox_Samples.HorizontalScrollbar = True
        Me.ListBox_Samples.Location = New System.Drawing.Point(12, 405)
        Me.ListBox_Samples.Name = "ListBox_Samples"
        Me.ListBox_Samples.Size = New System.Drawing.Size(367, 108)
        Me.ListBox_Samples.TabIndex = 8
        '
        'Label_Samples
        '
        Me.Label_Samples.AutoSize = True
        Me.Label_Samples.Location = New System.Drawing.Point(9, 389)
        Me.Label_Samples.Name = "Label_Samples"
        Me.Label_Samples.Size = New System.Drawing.Size(50, 13)
        Me.Label_Samples.TabIndex = 7
        Me.Label_Samples.Text = "Samples:"
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button_OK.Location = New System.Drawing.Point(158, 524)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 9
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'GroupBox_Recount
        '
        Me.GroupBox_Recount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Recount.Controls.Add(Me.Label_Value_Size)
        Me.GroupBox_Recount.Controls.Add(Me.Label_Size)
        Me.GroupBox_Recount.Controls.Add(Me.Label_SampleCount_Value)
        Me.GroupBox_Recount.Controls.Add(Me.Label_SampleCount)
        Me.GroupBox_Recount.Controls.Add(Me.Label_SfxCount_Value)
        Me.GroupBox_Recount.Controls.Add(Me.Label_DatabaseCount_Value)
        Me.GroupBox_Recount.Controls.Add(Me.Label_SFXCount)
        Me.GroupBox_Recount.Controls.Add(Me.Label_DataBaseCount)
        Me.GroupBox_Recount.Location = New System.Drawing.Point(12, 138)
        Me.GroupBox_Recount.Name = "GroupBox_Recount"
        Me.GroupBox_Recount.Size = New System.Drawing.Size(367, 95)
        Me.GroupBox_Recount.TabIndex = 3
        Me.GroupBox_Recount.TabStop = False
        Me.GroupBox_Recount.Text = "Files Recount:"
        '
        'Label_SampleCount_Value
        '
        Me.Label_SampleCount_Value.AutoSize = True
        Me.Label_SampleCount_Value.Location = New System.Drawing.Point(108, 56)
        Me.Label_SampleCount_Value.Name = "Label_SampleCount_Value"
        Me.Label_SampleCount_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_SampleCount_Value.TabIndex = 5
        Me.Label_SampleCount_Value.Text = "0"
        '
        'Label_SampleCount
        '
        Me.Label_SampleCount.AutoSize = True
        Me.Label_SampleCount.Location = New System.Drawing.Point(26, 56)
        Me.Label_SampleCount.Name = "Label_SampleCount"
        Me.Label_SampleCount.Size = New System.Drawing.Size(76, 13)
        Me.Label_SampleCount.TabIndex = 4
        Me.Label_SampleCount.Text = "Sample Count:"
        '
        'Label_SfxCount_Value
        '
        Me.Label_SfxCount_Value.AutoSize = True
        Me.Label_SfxCount_Value.Location = New System.Drawing.Point(108, 37)
        Me.Label_SfxCount_Value.Name = "Label_SfxCount_Value"
        Me.Label_SfxCount_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_SfxCount_Value.TabIndex = 3
        Me.Label_SfxCount_Value.Text = "0"
        '
        'Label_DatabaseCount_Value
        '
        Me.Label_DatabaseCount_Value.AutoSize = True
        Me.Label_DatabaseCount_Value.Location = New System.Drawing.Point(108, 18)
        Me.Label_DatabaseCount_Value.Name = "Label_DatabaseCount_Value"
        Me.Label_DatabaseCount_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_DatabaseCount_Value.TabIndex = 1
        Me.Label_DatabaseCount_Value.Text = "0"
        '
        'Label_SFXCount
        '
        Me.Label_SFXCount.AutoSize = True
        Me.Label_SFXCount.Location = New System.Drawing.Point(41, 37)
        Me.Label_SFXCount.Name = "Label_SFXCount"
        Me.Label_SFXCount.Size = New System.Drawing.Size(61, 13)
        Me.Label_SFXCount.TabIndex = 2
        Me.Label_SFXCount.Text = "SFX Count:"
        '
        'Label_DataBaseCount
        '
        Me.Label_DataBaseCount.AutoSize = True
        Me.Label_DataBaseCount.Location = New System.Drawing.Point(15, 18)
        Me.Label_DataBaseCount.Name = "Label_DataBaseCount"
        Me.Label_DataBaseCount.Size = New System.Drawing.Size(87, 13)
        Me.Label_DataBaseCount.TabIndex = 0
        Me.Label_DataBaseCount.Text = "Database Count:"
        '
        'SFX_Properties
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 559)
        Me.Controls.Add(Me.GroupBox_Recount)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Label_Samples)
        Me.Controls.Add(Me.ListBox_Samples)
        Me.Controls.Add(Me.ListBox_DataBase_Dependencies)
        Me.Controls.Add(Me.Textbox_DataBase_Deps)
        Me.Controls.Add(Me.Label_DataBase_Dependencies)
        Me.Controls.Add(Me.GroupBox_FileInfo)
        Me.Controls.Add(Me.Textbox_SFX_Name)
        Me.Controls.Add(Me.Label_SFX_Name)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SFX_Properties"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SFX_Properties"
        Me.GroupBox_FileInfo.ResumeLayout(False)
        Me.GroupBox_FileInfo.PerformLayout()
        Me.GroupBox_Recount.ResumeLayout(False)
        Me.GroupBox_Recount.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_SFX_Name As Label
    Friend WithEvents Textbox_SFX_Name As TextBox
    Friend WithEvents GroupBox_FileInfo As GroupBox
    Friend WithEvents Label_Value_Size As Label
    Friend WithEvents Label_Size As Label
    Friend WithEvents Label_Value_LastModified As Label
    Friend WithEvents Label_LastModified As Label
    Friend WithEvents Label_Value_FirstCreated As Label
    Friend WithEvents Label_FirstCreated As Label
    Friend WithEvents Label_DataBase_Dependencies As Label
    Friend WithEvents Textbox_DataBase_Deps As TextBox
    Friend WithEvents ListBox_DataBase_Dependencies As ListBox
    Friend WithEvents ListBox_Samples As ListBox
    Friend WithEvents Label_Samples As Label
    Friend WithEvents Button_OK As Button
    Friend WithEvents GroupBox_Recount As GroupBox
    Friend WithEvents Label_SfxCount_Value As Label
    Friend WithEvents Label_DatabaseCount_Value As Label
    Friend WithEvents Label_SFXCount As Label
    Friend WithEvents Label_DataBaseCount As Label
    Friend WithEvents Label_SampleCount_Value As Label
    Friend WithEvents Label_SampleCount As Label
    Friend WithEvents Label_ModifiedBy As Label
    Friend WithEvents Label_CreatedBy_Value As Label
    Friend WithEvents Label_CreatedBy As Label
    Friend WithEvents Label_ModifiedBy_Value As Label
End Class

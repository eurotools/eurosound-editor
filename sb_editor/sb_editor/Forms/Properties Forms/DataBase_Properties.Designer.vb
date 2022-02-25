<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataBase_Properties
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataBase_Properties))
        Me.Label_DataBaseName = New System.Windows.Forms.Label()
        Me.TextBox_DataBaseName = New System.Windows.Forms.TextBox()
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
        Me.Label_SampleCount_Value = New System.Windows.Forms.Label()
        Me.Label_SampleCount = New System.Windows.Forms.Label()
        Me.Label_SfxCount_Value = New System.Windows.Forms.Label()
        Me.Label_DatabaseCount_Value = New System.Windows.Forms.Label()
        Me.Label_SFXCount = New System.Windows.Forms.Label()
        Me.Label_DataBaseCount = New System.Windows.Forms.Label()
        Me.Label_SoundBank_Dependencies = New System.Windows.Forms.Label()
        Me.Label_SbDependencies_Value = New System.Windows.Forms.Label()
        Me.ListBox_SoundBank_Dependencies = New System.Windows.Forms.ListBox()
        Me.Label_TotalDependencies_Count = New System.Windows.Forms.Label()
        Me.Label_TotalSfx = New System.Windows.Forms.Label()
        Me.ListBox_TotalSfx = New System.Windows.Forms.ListBox()
        Me.Label_TotalSfx_Count = New System.Windows.Forms.Label()
        Me.Label_TotalSamples = New System.Windows.Forms.Label()
        Me.ListBox_TotalSamples = New System.Windows.Forms.ListBox()
        Me.Label_TotalSamples_Count = New System.Windows.Forms.Label()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label_DataBaseName
        '
        Me.Label_DataBaseName.AutoSize = True
        Me.Label_DataBaseName.Location = New System.Drawing.Point(12, 15)
        Me.Label_DataBaseName.Name = "Label_DataBaseName"
        Me.Label_DataBaseName.Size = New System.Drawing.Size(38, 13)
        Me.Label_DataBaseName.TabIndex = 0
        Me.Label_DataBaseName.Text = "Name:"
        '
        'TextBox_DataBaseName
        '
        Me.TextBox_DataBaseName.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_DataBaseName.Location = New System.Drawing.Point(56, 12)
        Me.TextBox_DataBaseName.Name = "TextBox_DataBaseName"
        Me.TextBox_DataBaseName.ReadOnly = True
        Me.TextBox_DataBaseName.Size = New System.Drawing.Size(251, 20)
        Me.TextBox_DataBaseName.TabIndex = 1
        '
        'Label_ModifiedBy_Value
        '
        Me.Label_ModifiedBy_Value.AutoSize = True
        Me.Label_ModifiedBy_Value.Location = New System.Drawing.Point(135, 95)
        Me.Label_ModifiedBy_Value.Name = "Label_ModifiedBy_Value"
        Me.Label_ModifiedBy_Value.Size = New System.Drawing.Size(35, 13)
        Me.Label_ModifiedBy_Value.TabIndex = 7
        Me.Label_ModifiedBy_Value.Text = "XXXX"
        '
        'Label_ModifiedBy
        '
        Me.Label_ModifiedBy.AutoSize = True
        Me.Label_ModifiedBy.Location = New System.Drawing.Point(12, 94)
        Me.Label_ModifiedBy.Name = "Label_ModifiedBy"
        Me.Label_ModifiedBy.Size = New System.Drawing.Size(88, 13)
        Me.Label_ModifiedBy.TabIndex = 6
        Me.Label_ModifiedBy.Text = "Last Modified By:"
        '
        'Label_CreatedBy_Value
        '
        Me.Label_CreatedBy_Value.AutoSize = True
        Me.Label_CreatedBy_Value.Location = New System.Drawing.Point(135, 61)
        Me.Label_CreatedBy_Value.Name = "Label_CreatedBy_Value"
        Me.Label_CreatedBy_Value.Size = New System.Drawing.Size(35, 13)
        Me.Label_CreatedBy_Value.TabIndex = 3
        Me.Label_CreatedBy_Value.Text = "XXXX"
        '
        'Label_CreatedBy
        '
        Me.Label_CreatedBy.AutoSize = True
        Me.Label_CreatedBy.Location = New System.Drawing.Point(12, 61)
        Me.Label_CreatedBy.Name = "Label_CreatedBy"
        Me.Label_CreatedBy.Size = New System.Drawing.Size(62, 13)
        Me.Label_CreatedBy.TabIndex = 2
        Me.Label_CreatedBy.Text = "Created By:"
        '
        'Label_Value_LastModified
        '
        Me.Label_Value_LastModified.AutoSize = True
        Me.Label_Value_LastModified.Location = New System.Drawing.Point(135, 78)
        Me.Label_Value_LastModified.Name = "Label_Value_LastModified"
        Me.Label_Value_LastModified.Size = New System.Drawing.Size(120, 13)
        Me.Label_Value_LastModified.TabIndex = 5
        Me.Label_Value_LastModified.Text = "XX-XX-XXXX XX:XX:XX"
        '
        'Label_LastModified
        '
        Me.Label_LastModified.AutoSize = True
        Me.Label_LastModified.Location = New System.Drawing.Point(12, 78)
        Me.Label_LastModified.Name = "Label_LastModified"
        Me.Label_LastModified.Size = New System.Drawing.Size(73, 13)
        Me.Label_LastModified.TabIndex = 4
        Me.Label_LastModified.Text = "Last Modified:"
        '
        'Label_Value_FirstCreated
        '
        Me.Label_Value_FirstCreated.AutoSize = True
        Me.Label_Value_FirstCreated.Location = New System.Drawing.Point(135, 44)
        Me.Label_Value_FirstCreated.Name = "Label_Value_FirstCreated"
        Me.Label_Value_FirstCreated.Size = New System.Drawing.Size(120, 13)
        Me.Label_Value_FirstCreated.TabIndex = 1
        Me.Label_Value_FirstCreated.Text = "XX-XX-XXXX XX:XX:XX"
        '
        'Label_FirstCreated
        '
        Me.Label_FirstCreated.AutoSize = True
        Me.Label_FirstCreated.Location = New System.Drawing.Point(12, 44)
        Me.Label_FirstCreated.Name = "Label_FirstCreated"
        Me.Label_FirstCreated.Size = New System.Drawing.Size(69, 13)
        Me.Label_FirstCreated.TabIndex = 0
        Me.Label_FirstCreated.Text = "First Created:"
        '
        'Label_Value_Size
        '
        Me.Label_Value_Size.AutoSize = True
        Me.Label_Value_Size.Location = New System.Drawing.Point(135, 193)
        Me.Label_Value_Size.Name = "Label_Value_Size"
        Me.Label_Value_Size.Size = New System.Drawing.Size(81, 13)
        Me.Label_Value_Size.TabIndex = 7
        Me.Label_Value_Size.Text = "0 (MB) (0 bytes)"
        '
        'Label_Size
        '
        Me.Label_Size.AutoSize = True
        Me.Label_Size.Location = New System.Drawing.Point(12, 193)
        Me.Label_Size.Name = "Label_Size"
        Me.Label_Size.Size = New System.Drawing.Size(95, 13)
        Me.Label_Size.TabIndex = 6
        Me.Label_Size.Text = "Total Sample Size:"
        '
        'Label_SampleCount_Value
        '
        Me.Label_SampleCount_Value.AutoSize = True
        Me.Label_SampleCount_Value.Location = New System.Drawing.Point(135, 160)
        Me.Label_SampleCount_Value.Name = "Label_SampleCount_Value"
        Me.Label_SampleCount_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_SampleCount_Value.TabIndex = 5
        Me.Label_SampleCount_Value.Text = "0"
        '
        'Label_SampleCount
        '
        Me.Label_SampleCount.AutoSize = True
        Me.Label_SampleCount.Location = New System.Drawing.Point(12, 160)
        Me.Label_SampleCount.Name = "Label_SampleCount"
        Me.Label_SampleCount.Size = New System.Drawing.Size(76, 13)
        Me.Label_SampleCount.TabIndex = 4
        Me.Label_SampleCount.Text = "Sample Count:"
        '
        'Label_SfxCount_Value
        '
        Me.Label_SfxCount_Value.AutoSize = True
        Me.Label_SfxCount_Value.Location = New System.Drawing.Point(135, 141)
        Me.Label_SfxCount_Value.Name = "Label_SfxCount_Value"
        Me.Label_SfxCount_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_SfxCount_Value.TabIndex = 3
        Me.Label_SfxCount_Value.Text = "0"
        '
        'Label_DatabaseCount_Value
        '
        Me.Label_DatabaseCount_Value.AutoSize = True
        Me.Label_DatabaseCount_Value.Location = New System.Drawing.Point(135, 122)
        Me.Label_DatabaseCount_Value.Name = "Label_DatabaseCount_Value"
        Me.Label_DatabaseCount_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_DatabaseCount_Value.TabIndex = 1
        Me.Label_DatabaseCount_Value.Text = "0"
        '
        'Label_SFXCount
        '
        Me.Label_SFXCount.AutoSize = True
        Me.Label_SFXCount.Location = New System.Drawing.Point(12, 141)
        Me.Label_SFXCount.Name = "Label_SFXCount"
        Me.Label_SFXCount.Size = New System.Drawing.Size(61, 13)
        Me.Label_SFXCount.TabIndex = 2
        Me.Label_SFXCount.Text = "SFX Count:"
        '
        'Label_DataBaseCount
        '
        Me.Label_DataBaseCount.AutoSize = True
        Me.Label_DataBaseCount.Location = New System.Drawing.Point(12, 122)
        Me.Label_DataBaseCount.Name = "Label_DataBaseCount"
        Me.Label_DataBaseCount.Size = New System.Drawing.Size(87, 13)
        Me.Label_DataBaseCount.TabIndex = 0
        Me.Label_DataBaseCount.Text = "Database Count:"
        '
        'Label_SoundBank_Dependencies
        '
        Me.Label_SoundBank_Dependencies.AutoSize = True
        Me.Label_SoundBank_Dependencies.Location = New System.Drawing.Point(310, 15)
        Me.Label_SoundBank_Dependencies.Name = "Label_SoundBank_Dependencies"
        Me.Label_SoundBank_Dependencies.Size = New System.Drawing.Size(157, 13)
        Me.Label_SoundBank_Dependencies.TabIndex = 5
        Me.Label_SoundBank_Dependencies.Text = "SoundBank File Dependencies:"
        '
        'Label_SbDependencies_Value
        '
        Me.Label_SbDependencies_Value.AutoSize = True
        Me.Label_SbDependencies_Value.Location = New System.Drawing.Point(463, 15)
        Me.Label_SbDependencies_Value.Name = "Label_SbDependencies_Value"
        Me.Label_SbDependencies_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_SbDependencies_Value.TabIndex = 6
        Me.Label_SbDependencies_Value.Text = "0"
        '
        'ListBox_SoundBank_Dependencies
        '
        Me.ListBox_SoundBank_Dependencies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_SoundBank_Dependencies.FormattingEnabled = True
        Me.ListBox_SoundBank_Dependencies.HorizontalScrollbar = True
        Me.ListBox_SoundBank_Dependencies.Location = New System.Drawing.Point(313, 31)
        Me.ListBox_SoundBank_Dependencies.Name = "ListBox_SoundBank_Dependencies"
        Me.ListBox_SoundBank_Dependencies.Size = New System.Drawing.Size(326, 212)
        Me.ListBox_SoundBank_Dependencies.TabIndex = 7
        '
        'Label_TotalDependencies_Count
        '
        Me.Label_TotalDependencies_Count.AutoSize = True
        Me.Label_TotalDependencies_Count.Location = New System.Drawing.Point(310, 245)
        Me.Label_TotalDependencies_Count.Name = "Label_TotalDependencies_Count"
        Me.Label_TotalDependencies_Count.Size = New System.Drawing.Size(43, 13)
        Me.Label_TotalDependencies_Count.TabIndex = 8
        Me.Label_TotalDependencies_Count.Text = "Total: 0"
        '
        'Label_TotalSfx
        '
        Me.Label_TotalSfx.AutoSize = True
        Me.Label_TotalSfx.Location = New System.Drawing.Point(12, 275)
        Me.Label_TotalSfx.Name = "Label_TotalSfx"
        Me.Label_TotalSfx.Size = New System.Drawing.Size(62, 13)
        Me.Label_TotalSfx.TabIndex = 9
        Me.Label_TotalSfx.Text = "Total SFXs:"
        '
        'ListBox_TotalSfx
        '
        Me.ListBox_TotalSfx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox_TotalSfx.FormattingEnabled = True
        Me.ListBox_TotalSfx.HorizontalScrollbar = True
        Me.ListBox_TotalSfx.Location = New System.Drawing.Point(12, 291)
        Me.ListBox_TotalSfx.Name = "ListBox_TotalSfx"
        Me.ListBox_TotalSfx.Size = New System.Drawing.Size(295, 277)
        Me.ListBox_TotalSfx.TabIndex = 10
        '
        'Label_TotalSfx_Count
        '
        Me.Label_TotalSfx_Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_TotalSfx_Count.AutoSize = True
        Me.Label_TotalSfx_Count.Location = New System.Drawing.Point(12, 571)
        Me.Label_TotalSfx_Count.Name = "Label_TotalSfx_Count"
        Me.Label_TotalSfx_Count.Size = New System.Drawing.Size(43, 13)
        Me.Label_TotalSfx_Count.TabIndex = 11
        Me.Label_TotalSfx_Count.Text = "Total: 0"
        '
        'Label_TotalSamples
        '
        Me.Label_TotalSamples.AutoSize = True
        Me.Label_TotalSamples.Location = New System.Drawing.Point(310, 275)
        Me.Label_TotalSamples.Name = "Label_TotalSamples"
        Me.Label_TotalSamples.Size = New System.Drawing.Size(77, 13)
        Me.Label_TotalSamples.TabIndex = 12
        Me.Label_TotalSamples.Text = "Total Samples:"
        '
        'ListBox_TotalSamples
        '
        Me.ListBox_TotalSamples.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_TotalSamples.FormattingEnabled = True
        Me.ListBox_TotalSamples.HorizontalScrollbar = True
        Me.ListBox_TotalSamples.Location = New System.Drawing.Point(313, 291)
        Me.ListBox_TotalSamples.Name = "ListBox_TotalSamples"
        Me.ListBox_TotalSamples.Size = New System.Drawing.Size(326, 277)
        Me.ListBox_TotalSamples.Sorted = True
        Me.ListBox_TotalSamples.TabIndex = 13
        '
        'Label_TotalSamples_Count
        '
        Me.Label_TotalSamples_Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_TotalSamples_Count.AutoSize = True
        Me.Label_TotalSamples_Count.Location = New System.Drawing.Point(310, 571)
        Me.Label_TotalSamples_Count.Name = "Label_TotalSamples_Count"
        Me.Label_TotalSamples_Count.Size = New System.Drawing.Size(43, 13)
        Me.Label_TotalSamples_Count.TabIndex = 14
        Me.Label_TotalSamples_Count.Text = "Total: 0"
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(278, 603)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 15
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'DataBase_Properties
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(651, 638)
        Me.Controls.Add(Me.Label_ModifiedBy_Value)
        Me.Controls.Add(Me.Label_ModifiedBy)
        Me.Controls.Add(Me.Label_Value_Size)
        Me.Controls.Add(Me.Label_CreatedBy_Value)
        Me.Controls.Add(Me.Label_Size)
        Me.Controls.Add(Me.Label_CreatedBy)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Label_Value_LastModified)
        Me.Controls.Add(Me.Label_SampleCount_Value)
        Me.Controls.Add(Me.Label_LastModified)
        Me.Controls.Add(Me.Label_TotalSamples_Count)
        Me.Controls.Add(Me.Label_Value_FirstCreated)
        Me.Controls.Add(Me.Label_SampleCount)
        Me.Controls.Add(Me.Label_FirstCreated)
        Me.Controls.Add(Me.ListBox_TotalSamples)
        Me.Controls.Add(Me.Label_SfxCount_Value)
        Me.Controls.Add(Me.Label_TotalSamples)
        Me.Controls.Add(Me.Label_DatabaseCount_Value)
        Me.Controls.Add(Me.Label_TotalSfx_Count)
        Me.Controls.Add(Me.Label_SFXCount)
        Me.Controls.Add(Me.ListBox_TotalSfx)
        Me.Controls.Add(Me.Label_DataBaseCount)
        Me.Controls.Add(Me.Label_TotalSfx)
        Me.Controls.Add(Me.Label_TotalDependencies_Count)
        Me.Controls.Add(Me.ListBox_SoundBank_Dependencies)
        Me.Controls.Add(Me.Label_SbDependencies_Value)
        Me.Controls.Add(Me.Label_SoundBank_Dependencies)
        Me.Controls.Add(Me.TextBox_DataBaseName)
        Me.Controls.Add(Me.Label_DataBaseName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DataBase_Properties"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DataBase Properties"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_DataBaseName As Label
    Friend WithEvents TextBox_DataBaseName As TextBox
    Friend WithEvents Label_ModifiedBy_Value As Label
    Friend WithEvents Label_ModifiedBy As Label
    Friend WithEvents Label_CreatedBy_Value As Label
    Friend WithEvents Label_CreatedBy As Label
    Friend WithEvents Label_Value_LastModified As Label
    Friend WithEvents Label_LastModified As Label
    Friend WithEvents Label_Value_FirstCreated As Label
    Friend WithEvents Label_FirstCreated As Label
    Friend WithEvents Label_Value_Size As Label
    Friend WithEvents Label_Size As Label
    Friend WithEvents Label_SampleCount_Value As Label
    Friend WithEvents Label_SampleCount As Label
    Friend WithEvents Label_SfxCount_Value As Label
    Friend WithEvents Label_DatabaseCount_Value As Label
    Friend WithEvents Label_SFXCount As Label
    Friend WithEvents Label_DataBaseCount As Label
    Friend WithEvents Label_SoundBank_Dependencies As Label
    Friend WithEvents Label_SbDependencies_Value As Label
    Friend WithEvents ListBox_SoundBank_Dependencies As ListBox
    Friend WithEvents Label_TotalDependencies_Count As Label
    Friend WithEvents Label_TotalSfx As Label
    Friend WithEvents ListBox_TotalSfx As ListBox
    Friend WithEvents Label_TotalSfx_Count As Label
    Friend WithEvents Label_TotalSamples As Label
    Friend WithEvents ListBox_TotalSamples As ListBox
    Friend WithEvents Label_TotalSamples_Count As Label
    Friend WithEvents Button_OK As Button
End Class

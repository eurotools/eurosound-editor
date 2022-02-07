<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SfxNewMultiple
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SfxNewMultiple))
        Me.GroupBox_InputSamples = New System.Windows.Forms.GroupBox()
        Me.ListBox_SampleFiles = New System.Windows.Forms.ListBox()
        Me.Groupbox_SfxNames = New System.Windows.Forms.GroupBox()
        Me.ListBox_SfxNames = New System.Windows.Forms.ListBox()
        Me.CheckBox_ForceUpperCase = New System.Windows.Forms.CheckBox()
        Me.CheckBox_RandomSequence = New System.Windows.Forms.CheckBox()
        Me.Label_SfxPrefix = New System.Windows.Forms.Label()
        Me.TextBox_SfxPrefix = New System.Windows.Forms.TextBox()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.Button_Remove = New System.Windows.Forms.Button()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.OpenFile_WaveFiles = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox_InputSamples.SuspendLayout()
        Me.Groupbox_SfxNames.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_InputSamples
        '
        Me.GroupBox_InputSamples.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_InputSamples.Controls.Add(Me.ListBox_SampleFiles)
        Me.GroupBox_InputSamples.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_InputSamples.Name = "GroupBox_InputSamples"
        Me.GroupBox_InputSamples.Size = New System.Drawing.Size(446, 226)
        Me.GroupBox_InputSamples.TabIndex = 0
        Me.GroupBox_InputSamples.TabStop = False
        Me.GroupBox_InputSamples.Text = "Sample Files To Use"
        '
        'ListBox_SampleFiles
        '
        Me.ListBox_SampleFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_SampleFiles.FormattingEnabled = True
        Me.ListBox_SampleFiles.HorizontalScrollbar = True
        Me.ListBox_SampleFiles.Location = New System.Drawing.Point(6, 19)
        Me.ListBox_SampleFiles.Name = "ListBox_SampleFiles"
        Me.ListBox_SampleFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_SampleFiles.Size = New System.Drawing.Size(434, 199)
        Me.ListBox_SampleFiles.TabIndex = 0
        '
        'Groupbox_SfxNames
        '
        Me.Groupbox_SfxNames.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Groupbox_SfxNames.Controls.Add(Me.ListBox_SfxNames)
        Me.Groupbox_SfxNames.Location = New System.Drawing.Point(12, 244)
        Me.Groupbox_SfxNames.Name = "Groupbox_SfxNames"
        Me.Groupbox_SfxNames.Size = New System.Drawing.Size(446, 253)
        Me.Groupbox_SfxNames.TabIndex = 1
        Me.Groupbox_SfxNames.TabStop = False
        Me.Groupbox_SfxNames.Text = "SFX Names To Be Created"
        '
        'ListBox_SfxNames
        '
        Me.ListBox_SfxNames.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_SfxNames.FormattingEnabled = True
        Me.ListBox_SfxNames.HorizontalScrollbar = True
        Me.ListBox_SfxNames.Location = New System.Drawing.Point(6, 19)
        Me.ListBox_SfxNames.Name = "ListBox_SfxNames"
        Me.ListBox_SfxNames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_SfxNames.Size = New System.Drawing.Size(434, 225)
        Me.ListBox_SfxNames.TabIndex = 0
        '
        'CheckBox_ForceUpperCase
        '
        Me.CheckBox_ForceUpperCase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_ForceUpperCase.AutoSize = True
        Me.CheckBox_ForceUpperCase.Location = New System.Drawing.Point(12, 512)
        Me.CheckBox_ForceUpperCase.Name = "CheckBox_ForceUpperCase"
        Me.CheckBox_ForceUpperCase.Size = New System.Drawing.Size(112, 17)
        Me.CheckBox_ForceUpperCase.TabIndex = 2
        Me.CheckBox_ForceUpperCase.Text = "Force Upper Case"
        Me.CheckBox_ForceUpperCase.UseVisualStyleBackColor = True
        '
        'CheckBox_RandomSequence
        '
        Me.CheckBox_RandomSequence.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_RandomSequence.AutoSize = True
        Me.CheckBox_RandomSequence.Location = New System.Drawing.Point(12, 535)
        Me.CheckBox_RandomSequence.Name = "CheckBox_RandomSequence"
        Me.CheckBox_RandomSequence.Size = New System.Drawing.Size(152, 17)
        Me.CheckBox_RandomSequence.TabIndex = 3
        Me.CheckBox_RandomSequence.Text = "Create Random Sequence"
        Me.CheckBox_RandomSequence.UseVisualStyleBackColor = True
        '
        'Label_SfxPrefix
        '
        Me.Label_SfxPrefix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_SfxPrefix.AutoSize = True
        Me.Label_SfxPrefix.Location = New System.Drawing.Point(12, 561)
        Me.Label_SfxPrefix.Name = "Label_SfxPrefix"
        Me.Label_SfxPrefix.Size = New System.Drawing.Size(118, 13)
        Me.Label_SfxPrefix.TabIndex = 4
        Me.Label_SfxPrefix.Text = "Preifx SFX Labels With:"
        '
        'TextBox_SfxPrefix
        '
        Me.TextBox_SfxPrefix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_SfxPrefix.Location = New System.Drawing.Point(136, 558)
        Me.TextBox_SfxPrefix.Name = "TextBox_SfxPrefix"
        Me.TextBox_SfxPrefix.Size = New System.Drawing.Size(178, 20)
        Me.TextBox_SfxPrefix.TabIndex = 5
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.Location = New System.Drawing.Point(83, 584)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 6
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_Add
        '
        Me.Button_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_Add.Location = New System.Drawing.Point(164, 584)
        Me.Button_Add.Name = "Button_Add"
        Me.Button_Add.Size = New System.Drawing.Size(75, 23)
        Me.Button_Add.TabIndex = 7
        Me.Button_Add.Text = "Add"
        Me.Button_Add.UseVisualStyleBackColor = True
        '
        'Button_Remove
        '
        Me.Button_Remove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_Remove.Location = New System.Drawing.Point(245, 584)
        Me.Button_Remove.Name = "Button_Remove"
        Me.Button_Remove.Size = New System.Drawing.Size(75, 23)
        Me.Button_Remove.TabIndex = 8
        Me.Button_Remove.Text = "Remove"
        Me.Button_Remove.UseVisualStyleBackColor = True
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.Location = New System.Drawing.Point(326, 584)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 9
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'OpenFile_WaveFiles
        '
        Me.OpenFile_WaveFiles.Filter = "Wave Files (*.wav)|*.wav"
        Me.OpenFile_WaveFiles.Multiselect = True
        '
        'SfxNewMultiple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(470, 619)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.Button_Remove)
        Me.Controls.Add(Me.Button_Add)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.TextBox_SfxPrefix)
        Me.Controls.Add(Me.Label_SfxPrefix)
        Me.Controls.Add(Me.CheckBox_RandomSequence)
        Me.Controls.Add(Me.CheckBox_ForceUpperCase)
        Me.Controls.Add(Me.Groupbox_SfxNames)
        Me.Controls.Add(Me.GroupBox_InputSamples)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SfxNewMultiple"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create SFXs From Sample Files"
        Me.GroupBox_InputSamples.ResumeLayout(False)
        Me.Groupbox_SfxNames.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox_InputSamples As GroupBox
    Friend WithEvents ListBox_SampleFiles As ListBox
    Friend WithEvents Groupbox_SfxNames As GroupBox
    Friend WithEvents ListBox_SfxNames As ListBox
    Friend WithEvents CheckBox_ForceUpperCase As CheckBox
    Friend WithEvents CheckBox_RandomSequence As CheckBox
    Friend WithEvents Label_SfxPrefix As Label
    Friend WithEvents TextBox_SfxPrefix As TextBox
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents Button_Add As Button
    Friend WithEvents Button_Remove As Button
    Friend WithEvents Button_Ok As Button
    Friend WithEvents OpenFile_WaveFiles As OpenFileDialog
End Class

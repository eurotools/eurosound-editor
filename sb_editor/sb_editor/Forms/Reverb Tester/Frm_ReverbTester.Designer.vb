<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ReverbTester
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_ReverbTester))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPagePC = New System.Windows.Forms.TabPage()
        Me.TabPageXbox = New System.Windows.Forms.TabPage()
        Me.TabPageGameCube = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button_AddNewHashCode = New System.Windows.Forms.Button()
        Me.Button_CopySelection = New System.Windows.Forms.Button()
        Me.Button_DeleteSelection = New System.Windows.Forms.Button()
        Me.Button_RemapHashCodes = New System.Windows.Forms.Button()
        Me.Button_RenameSelected = New System.Windows.Forms.Button()
        Me.ListBox_HashCodes = New System.Windows.Forms.ListBox()
        Me.GroupBox_Options = New System.Windows.Forms.GroupBox()
        Me.Button_PlayTest = New System.Windows.Forms.Button()
        Me.Label_Filter2 = New System.Windows.Forms.Label()
        Me.TrackBar_Filter2 = New System.Windows.Forms.TrackBar()
        Me.TrackBar_Filter1 = New System.Windows.Forms.TrackBar()
        Me.TrackBar_LowPassFilter = New System.Windows.Forms.TrackBar()
        Me.Label_Filter1 = New System.Windows.Forms.Label()
        Me.TrackBar_Damp = New System.Windows.Forms.TrackBar()
        Me.TrackBar_Width = New System.Windows.Forms.TrackBar()
        Me.Label_RoomSize = New System.Windows.Forms.Label()
        Me.Label_LowPassFilter = New System.Windows.Forms.Label()
        Me.TrackBar_RoomSize = New System.Windows.Forms.TrackBar()
        Me.Label_Damp = New System.Windows.Forms.Label()
        Me.Label_Width = New System.Windows.Forms.Label()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.Label_HashCodeLabel = New System.Windows.Forms.Label()
        Me.Label_HashCode = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox_Options.SuspendLayout()
        CType(Me.TrackBar_Filter2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_Filter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_LowPassFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_Damp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_Width, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_RoomSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPagePC)
        Me.TabControl1.Controls.Add(Me.TabPageXbox)
        Me.TabControl1.Controls.Add(Me.TabPageGameCube)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(707, 22)
        Me.TabControl1.TabIndex = 0
        '
        'TabPagePC
        '
        Me.TabPagePC.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePC.Name = "TabPagePC"
        Me.TabPagePC.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePC.Size = New System.Drawing.Size(699, 0)
        Me.TabPagePC.TabIndex = 0
        Me.TabPagePC.Text = "PC"
        Me.TabPagePC.UseVisualStyleBackColor = True
        '
        'TabPageXbox
        '
        Me.TabPageXbox.Location = New System.Drawing.Point(4, 22)
        Me.TabPageXbox.Name = "TabPageXbox"
        Me.TabPageXbox.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageXbox.Size = New System.Drawing.Size(699, 0)
        Me.TabPageXbox.TabIndex = 1
        Me.TabPageXbox.Text = "XBox"
        Me.TabPageXbox.UseVisualStyleBackColor = True
        '
        'TabPageGameCube
        '
        Me.TabPageGameCube.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGameCube.Name = "TabPageGameCube"
        Me.TabPageGameCube.Size = New System.Drawing.Size(699, 0)
        Me.TabPageGameCube.TabIndex = 2
        Me.TabPageGameCube.Text = "GameCube"
        Me.TabPageGameCube.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Button_AddNewHashCode)
        Me.Panel1.Controls.Add(Me.Button_CopySelection)
        Me.Panel1.Controls.Add(Me.Button_DeleteSelection)
        Me.Panel1.Controls.Add(Me.Button_RemapHashCodes)
        Me.Panel1.Controls.Add(Me.Button_RenameSelected)
        Me.Panel1.Controls.Add(Me.ListBox_HashCodes)
        Me.Panel1.Controls.Add(Me.GroupBox_Options)
        Me.Panel1.Location = New System.Drawing.Point(12, 34)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(707, 302)
        Me.Panel1.TabIndex = 1
        '
        'Button_AddNewHashCode
        '
        Me.Button_AddNewHashCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_AddNewHashCode.Location = New System.Drawing.Point(616, 263)
        Me.Button_AddNewHashCode.Name = "Button_AddNewHashCode"
        Me.Button_AddNewHashCode.Size = New System.Drawing.Size(84, 23)
        Me.Button_AddNewHashCode.TabIndex = 6
        Me.Button_AddNewHashCode.Text = "Add New"
        Me.Button_AddNewHashCode.UseVisualStyleBackColor = True
        '
        'Button_CopySelection
        '
        Me.Button_CopySelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_CopySelection.Location = New System.Drawing.Point(519, 263)
        Me.Button_CopySelection.Name = "Button_CopySelection"
        Me.Button_CopySelection.Size = New System.Drawing.Size(91, 23)
        Me.Button_CopySelection.TabIndex = 5
        Me.Button_CopySelection.Text = "Copy Selected"
        Me.Button_CopySelection.UseVisualStyleBackColor = True
        '
        'Button_DeleteSelection
        '
        Me.Button_DeleteSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_DeleteSelection.Location = New System.Drawing.Point(422, 263)
        Me.Button_DeleteSelection.Name = "Button_DeleteSelection"
        Me.Button_DeleteSelection.Size = New System.Drawing.Size(91, 23)
        Me.Button_DeleteSelection.TabIndex = 4
        Me.Button_DeleteSelection.Text = "Delete Selected"
        Me.Button_DeleteSelection.UseVisualStyleBackColor = True
        '
        'Button_RemapHashCodes
        '
        Me.Button_RemapHashCodes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_RemapHashCodes.Location = New System.Drawing.Point(563, 234)
        Me.Button_RemapHashCodes.Name = "Button_RemapHashCodes"
        Me.Button_RemapHashCodes.Size = New System.Drawing.Size(137, 23)
        Me.Button_RemapHashCodes.TabIndex = 3
        Me.Button_RemapHashCodes.Text = "ReMap HashCodes"
        Me.Button_RemapHashCodes.UseVisualStyleBackColor = True
        '
        'Button_RenameSelected
        '
        Me.Button_RenameSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_RenameSelected.Location = New System.Drawing.Point(422, 234)
        Me.Button_RenameSelected.Name = "Button_RenameSelected"
        Me.Button_RenameSelected.Size = New System.Drawing.Size(135, 23)
        Me.Button_RenameSelected.TabIndex = 2
        Me.Button_RenameSelected.Text = "Rename Selected"
        Me.Button_RenameSelected.UseVisualStyleBackColor = True
        '
        'ListBox_HashCodes
        '
        Me.ListBox_HashCodes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_HashCodes.FormattingEnabled = True
        Me.ListBox_HashCodes.HorizontalScrollbar = True
        Me.ListBox_HashCodes.Location = New System.Drawing.Point(422, 16)
        Me.ListBox_HashCodes.Name = "ListBox_HashCodes"
        Me.ListBox_HashCodes.Size = New System.Drawing.Size(278, 212)
        Me.ListBox_HashCodes.Sorted = True
        Me.ListBox_HashCodes.TabIndex = 1
        '
        'GroupBox_Options
        '
        Me.GroupBox_Options.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Options.Controls.Add(Me.Button_PlayTest)
        Me.GroupBox_Options.Controls.Add(Me.Label_Filter2)
        Me.GroupBox_Options.Controls.Add(Me.TrackBar_Filter2)
        Me.GroupBox_Options.Controls.Add(Me.TrackBar_Filter1)
        Me.GroupBox_Options.Controls.Add(Me.TrackBar_LowPassFilter)
        Me.GroupBox_Options.Controls.Add(Me.Label_Filter1)
        Me.GroupBox_Options.Controls.Add(Me.TrackBar_Damp)
        Me.GroupBox_Options.Controls.Add(Me.TrackBar_Width)
        Me.GroupBox_Options.Controls.Add(Me.Label_RoomSize)
        Me.GroupBox_Options.Controls.Add(Me.Label_LowPassFilter)
        Me.GroupBox_Options.Controls.Add(Me.TrackBar_RoomSize)
        Me.GroupBox_Options.Controls.Add(Me.Label_Damp)
        Me.GroupBox_Options.Controls.Add(Me.Label_Width)
        Me.GroupBox_Options.Location = New System.Drawing.Point(3, 11)
        Me.GroupBox_Options.Name = "GroupBox_Options"
        Me.GroupBox_Options.Size = New System.Drawing.Size(413, 275)
        Me.GroupBox_Options.TabIndex = 0
        Me.GroupBox_Options.TabStop = False
        '
        'Button_PlayTest
        '
        Me.Button_PlayTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PlayTest.Location = New System.Drawing.Point(6, 246)
        Me.Button_PlayTest.Name = "Button_PlayTest"
        Me.Button_PlayTest.Size = New System.Drawing.Size(75, 23)
        Me.Button_PlayTest.TabIndex = 12
        Me.Button_PlayTest.Text = "Play Test"
        Me.Button_PlayTest.UseVisualStyleBackColor = True
        '
        'Label_Filter2
        '
        Me.Label_Filter2.AutoSize = True
        Me.Label_Filter2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Filter2.Location = New System.Drawing.Point(267, 209)
        Me.Label_Filter2.Name = "Label_Filter2"
        Me.Label_Filter2.Size = New System.Drawing.Size(70, 25)
        Me.Label_Filter2.TabIndex = 11
        Me.Label_Filter2.Text = "Filter 2"
        '
        'TrackBar_Filter2
        '
        Me.TrackBar_Filter2.Location = New System.Drawing.Point(6, 209)
        Me.TrackBar_Filter2.Maximum = 1000
        Me.TrackBar_Filter2.Name = "TrackBar_Filter2"
        Me.TrackBar_Filter2.Size = New System.Drawing.Size(255, 45)
        Me.TrackBar_Filter2.TabIndex = 10
        Me.TrackBar_Filter2.TickFrequency = 100
        '
        'TrackBar_Filter1
        '
        Me.TrackBar_Filter1.Location = New System.Drawing.Point(6, 169)
        Me.TrackBar_Filter1.Maximum = 1000
        Me.TrackBar_Filter1.Name = "TrackBar_Filter1"
        Me.TrackBar_Filter1.Size = New System.Drawing.Size(255, 45)
        Me.TrackBar_Filter1.TabIndex = 8
        Me.TrackBar_Filter1.TickFrequency = 100
        '
        'TrackBar_LowPassFilter
        '
        Me.TrackBar_LowPassFilter.Location = New System.Drawing.Point(6, 132)
        Me.TrackBar_LowPassFilter.Maximum = 1000
        Me.TrackBar_LowPassFilter.Name = "TrackBar_LowPassFilter"
        Me.TrackBar_LowPassFilter.Size = New System.Drawing.Size(255, 45)
        Me.TrackBar_LowPassFilter.TabIndex = 6
        Me.TrackBar_LowPassFilter.TickFrequency = 100
        '
        'Label_Filter1
        '
        Me.Label_Filter1.AutoSize = True
        Me.Label_Filter1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Filter1.Location = New System.Drawing.Point(267, 169)
        Me.Label_Filter1.Name = "Label_Filter1"
        Me.Label_Filter1.Size = New System.Drawing.Size(70, 25)
        Me.Label_Filter1.TabIndex = 9
        Me.Label_Filter1.Text = "Filter 1"
        '
        'TrackBar_Damp
        '
        Me.TrackBar_Damp.Location = New System.Drawing.Point(6, 93)
        Me.TrackBar_Damp.Maximum = 1000
        Me.TrackBar_Damp.Name = "TrackBar_Damp"
        Me.TrackBar_Damp.Size = New System.Drawing.Size(255, 45)
        Me.TrackBar_Damp.TabIndex = 4
        Me.TrackBar_Damp.TickFrequency = 100
        '
        'TrackBar_Width
        '
        Me.TrackBar_Width.Location = New System.Drawing.Point(6, 54)
        Me.TrackBar_Width.Maximum = 1000
        Me.TrackBar_Width.Name = "TrackBar_Width"
        Me.TrackBar_Width.Size = New System.Drawing.Size(255, 45)
        Me.TrackBar_Width.TabIndex = 2
        Me.TrackBar_Width.TickFrequency = 100
        '
        'Label_RoomSize
        '
        Me.Label_RoomSize.AutoSize = True
        Me.Label_RoomSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_RoomSize.Location = New System.Drawing.Point(267, 19)
        Me.Label_RoomSize.Name = "Label_RoomSize"
        Me.Label_RoomSize.Size = New System.Drawing.Size(107, 25)
        Me.Label_RoomSize.TabIndex = 1
        Me.Label_RoomSize.Text = "Room Size"
        '
        'Label_LowPassFilter
        '
        Me.Label_LowPassFilter.AutoSize = True
        Me.Label_LowPassFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_LowPassFilter.Location = New System.Drawing.Point(267, 132)
        Me.Label_LowPassFilter.Name = "Label_LowPassFilter"
        Me.Label_LowPassFilter.Size = New System.Drawing.Size(144, 25)
        Me.Label_LowPassFilter.TabIndex = 7
        Me.Label_LowPassFilter.Text = "Low Pass Filter"
        '
        'TrackBar_RoomSize
        '
        Me.TrackBar_RoomSize.Location = New System.Drawing.Point(6, 19)
        Me.TrackBar_RoomSize.Maximum = 1000
        Me.TrackBar_RoomSize.Name = "TrackBar_RoomSize"
        Me.TrackBar_RoomSize.Size = New System.Drawing.Size(255, 45)
        Me.TrackBar_RoomSize.TabIndex = 0
        Me.TrackBar_RoomSize.TickFrequency = 100
        '
        'Label_Damp
        '
        Me.Label_Damp.AutoSize = True
        Me.Label_Damp.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Damp.Location = New System.Drawing.Point(267, 93)
        Me.Label_Damp.Name = "Label_Damp"
        Me.Label_Damp.Size = New System.Drawing.Size(64, 25)
        Me.Label_Damp.TabIndex = 5
        Me.Label_Damp.Text = "Damp"
        '
        'Label_Width
        '
        Me.Label_Width.AutoSize = True
        Me.Label_Width.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Width.Location = New System.Drawing.Point(267, 54)
        Me.Label_Width.Name = "Label_Width"
        Me.Label_Width.Size = New System.Drawing.Size(63, 25)
        Me.Label_Width.TabIndex = 3
        Me.Label_Width.Text = "Width"
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.Location = New System.Drawing.Point(644, 342)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 4
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'Label_HashCodeLabel
        '
        Me.Label_HashCodeLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_HashCodeLabel.AutoSize = True
        Me.Label_HashCodeLabel.Location = New System.Drawing.Point(12, 347)
        Me.Label_HashCodeLabel.Name = "Label_HashCodeLabel"
        Me.Label_HashCodeLabel.Size = New System.Drawing.Size(60, 13)
        Me.Label_HashCodeLabel.TabIndex = 2
        Me.Label_HashCodeLabel.Text = "HashCode:"
        '
        'Label_HashCode
        '
        Me.Label_HashCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_HashCode.AutoSize = True
        Me.Label_HashCode.Location = New System.Drawing.Point(70, 347)
        Me.Label_HashCode.Name = "Label_HashCode"
        Me.Label_HashCode.Size = New System.Drawing.Size(66, 13)
        Me.Label_HashCode.TabIndex = 3
        Me.Label_HashCode.Text = "0x00000000"
        '
        'Frm_ReverbTester
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 377)
        Me.Controls.Add(Me.Label_HashCode)
        Me.Controls.Add(Me.Label_HashCodeLabel)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_ReverbTester"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reverb Tester"
        Me.TabControl1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox_Options.ResumeLayout(False)
        Me.GroupBox_Options.PerformLayout()
        CType(Me.TrackBar_Filter2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_Filter1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_LowPassFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_Damp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_Width, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_RoomSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPagePC As TabPage
    Friend WithEvents TabPageXbox As TabPage
    Friend WithEvents TabPageGameCube As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button_AddNewHashCode As Button
    Friend WithEvents Button_CopySelection As Button
    Friend WithEvents Button_DeleteSelection As Button
    Friend WithEvents Button_RemapHashCodes As Button
    Friend WithEvents Button_RenameSelected As Button
    Friend WithEvents ListBox_HashCodes As ListBox
    Friend WithEvents GroupBox_Options As GroupBox
    Friend WithEvents Label_Filter2 As Label
    Friend WithEvents TrackBar_Filter2 As TrackBar
    Friend WithEvents TrackBar_Filter1 As TrackBar
    Friend WithEvents TrackBar_LowPassFilter As TrackBar
    Friend WithEvents Label_Filter1 As Label
    Friend WithEvents TrackBar_Damp As TrackBar
    Friend WithEvents TrackBar_Width As TrackBar
    Friend WithEvents Label_RoomSize As Label
    Friend WithEvents Label_LowPassFilter As Label
    Friend WithEvents TrackBar_RoomSize As TrackBar
    Friend WithEvents Label_Damp As Label
    Friend WithEvents Label_Width As Label
    Friend WithEvents Button_Ok As Button
    Friend WithEvents Label_HashCodeLabel As Label
    Friend WithEvents Label_HashCode As Label
    Friend WithEvents Button_PlayTest As Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MusicMaker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MusicMaker))
        Me.GroupBox_MusicMaker = New System.Windows.Forms.GroupBox()
        Me.Button_ForceOutput = New System.Windows.Forms.Button()
        Me.Button_ForceSelected = New System.Windows.Forms.Button()
        Me.Numeric_Volume = New System.Windows.Forms.NumericUpDown()
        Me.TextBox_UserValue = New System.Windows.Forms.TextBox()
        Me.Button_ViewErrorFile = New System.Windows.Forms.Button()
        Me.Button_VeryfyMfx = New System.Windows.Forms.Button()
        Me.Button_Output = New System.Windows.Forms.Button()
        Me.Button_UpdateFiles = New System.Windows.Forms.Button()
        Me.ListView_MusicFiles = New ListView_ColumnSortingClick.ListView_ColumnSortingClick()
        Me.Col_FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Volume = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_ErrorStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_HashCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Marker = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Wav = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_OutputFileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_UserValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox_OutputFormat = New System.Windows.Forms.GroupBox()
        Me.ComboBox_OutputFormat = New System.Windows.Forms.ComboBox()
        Me.CheckBox_MarkerFileOnly = New System.Windows.Forms.CheckBox()
        Me.TextBox_OutputTime = New System.Windows.Forms.TextBox()
        Me.Button_RemapHashCodes = New System.Windows.Forms.Button()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.Button_ImportMusics = New System.Windows.Forms.Button()
        Me.GroupBox_MusicMaker.SuspendLayout()
        CType(Me.Numeric_Volume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_OutputFormat.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_MusicMaker
        '
        Me.GroupBox_MusicMaker.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_MusicMaker.Controls.Add(Me.Button_ForceOutput)
        Me.GroupBox_MusicMaker.Controls.Add(Me.Button_ForceSelected)
        Me.GroupBox_MusicMaker.Controls.Add(Me.Numeric_Volume)
        Me.GroupBox_MusicMaker.Controls.Add(Me.TextBox_UserValue)
        Me.GroupBox_MusicMaker.Controls.Add(Me.Button_ViewErrorFile)
        Me.GroupBox_MusicMaker.Controls.Add(Me.Button_VeryfyMfx)
        Me.GroupBox_MusicMaker.Controls.Add(Me.Button_Output)
        Me.GroupBox_MusicMaker.Controls.Add(Me.Button_UpdateFiles)
        Me.GroupBox_MusicMaker.Controls.Add(Me.ListView_MusicFiles)
        Me.GroupBox_MusicMaker.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_MusicMaker.Name = "GroupBox_MusicMaker"
        Me.GroupBox_MusicMaker.Size = New System.Drawing.Size(762, 386)
        Me.GroupBox_MusicMaker.TabIndex = 0
        Me.GroupBox_MusicMaker.TabStop = False
        Me.GroupBox_MusicMaker.Text = "Available Music Files"
        '
        'Button_ForceOutput
        '
        Me.Button_ForceOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_ForceOutput.Location = New System.Drawing.Point(424, 356)
        Me.Button_ForceOutput.Name = "Button_ForceOutput"
        Me.Button_ForceOutput.Size = New System.Drawing.Size(101, 23)
        Me.Button_ForceOutput.TabIndex = 6
        Me.Button_ForceOutput.Text = "Add All For Output"
        Me.Button_ForceOutput.UseVisualStyleBackColor = True
        '
        'Button_ForceSelected
        '
        Me.Button_ForceSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_ForceSelected.Location = New System.Drawing.Point(309, 357)
        Me.Button_ForceSelected.Name = "Button_ForceSelected"
        Me.Button_ForceSelected.Size = New System.Drawing.Size(109, 23)
        Me.Button_ForceSelected.TabIndex = 5
        Me.Button_ForceSelected.Text = "Add Sel. For Output"
        Me.Button_ForceSelected.UseVisualStyleBackColor = True
        '
        'Numeric_Volume
        '
        Me.Numeric_Volume.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_Volume.Location = New System.Drawing.Point(142, 360)
        Me.Numeric_Volume.Name = "Numeric_Volume"
        Me.Numeric_Volume.Size = New System.Drawing.Size(60, 20)
        Me.Numeric_Volume.TabIndex = 3
        '
        'TextBox_UserValue
        '
        Me.TextBox_UserValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_UserValue.Location = New System.Drawing.Point(637, 359)
        Me.TextBox_UserValue.Name = "TextBox_UserValue"
        Me.TextBox_UserValue.Size = New System.Drawing.Size(60, 20)
        Me.TextBox_UserValue.TabIndex = 8
        Me.TextBox_UserValue.Text = "0"
        '
        'Button_ViewErrorFile
        '
        Me.Button_ViewErrorFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_ViewErrorFile.Location = New System.Drawing.Point(208, 357)
        Me.Button_ViewErrorFile.Name = "Button_ViewErrorFile"
        Me.Button_ViewErrorFile.Size = New System.Drawing.Size(82, 23)
        Me.Button_ViewErrorFile.TabIndex = 4
        Me.Button_ViewErrorFile.Text = "View Error File"
        Me.Button_ViewErrorFile.UseVisualStyleBackColor = True
        '
        'Button_VeryfyMfx
        '
        Me.Button_VeryfyMfx.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_VeryfyMfx.Location = New System.Drawing.Point(546, 357)
        Me.Button_VeryfyMfx.Name = "Button_VeryfyMfx"
        Me.Button_VeryfyMfx.Size = New System.Drawing.Size(85, 23)
        Me.Button_VeryfyMfx.TabIndex = 7
        Me.Button_VeryfyMfx.Text = "Verify MFX File"
        Me.Button_VeryfyMfx.UseVisualStyleBackColor = True
        '
        'Button_Output
        '
        Me.Button_Output.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_Output.Location = New System.Drawing.Point(72, 357)
        Me.Button_Output.Name = "Button_Output"
        Me.Button_Output.Size = New System.Drawing.Size(60, 23)
        Me.Button_Output.TabIndex = 2
        Me.Button_Output.Text = "Output"
        Me.Button_Output.UseVisualStyleBackColor = True
        '
        'Button_UpdateFiles
        '
        Me.Button_UpdateFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_UpdateFiles.Location = New System.Drawing.Point(6, 357)
        Me.Button_UpdateFiles.Name = "Button_UpdateFiles"
        Me.Button_UpdateFiles.Size = New System.Drawing.Size(60, 23)
        Me.Button_UpdateFiles.TabIndex = 1
        Me.Button_UpdateFiles.Text = "Update"
        Me.Button_UpdateFiles.UseVisualStyleBackColor = True
        '
        'ListView_MusicFiles
        '
        Me.ListView_MusicFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_MusicFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Col_FileName, Me.Col_Volume, Me.Col_ErrorStatus, Me.Col_HashCode, Me.Col_Marker, Me.Col_Wav, Me.Col_OutputFileName, Me.Col_UserValue})
        Me.ListView_MusicFiles.FullRowSelect = True
        Me.ListView_MusicFiles.GridLines = True
        Me.ListView_MusicFiles.HideSelection = False
        Me.ListView_MusicFiles.Location = New System.Drawing.Point(6, 19)
        Me.ListView_MusicFiles.Name = "ListView_MusicFiles"
        Me.ListView_MusicFiles.Size = New System.Drawing.Size(750, 332)
        Me.ListView_MusicFiles.TabIndex = 0
        Me.ListView_MusicFiles.UseCompatibleStateImageBehavior = False
        Me.ListView_MusicFiles.View = System.Windows.Forms.View.Details
        '
        'Col_FileName
        '
        Me.Col_FileName.Text = "File Name"
        Me.Col_FileName.Width = 135
        '
        'Col_Volume
        '
        Me.Col_Volume.Text = "Volume"
        Me.Col_Volume.Width = 57
        '
        'Col_ErrorStatus
        '
        Me.Col_ErrorStatus.Text = "Error Status"
        Me.Col_ErrorStatus.Width = 100
        '
        'Col_HashCode
        '
        Me.Col_HashCode.Text = "HashCode"
        Me.Col_HashCode.Width = 75
        '
        'Col_Marker
        '
        Me.Col_Marker.Text = "Marker"
        '
        'Col_Wav
        '
        Me.Col_Wav.Text = "Wav"
        Me.Col_Wav.Width = 72
        '
        'Col_OutputFileName
        '
        Me.Col_OutputFileName.Text = "Output Filename"
        Me.Col_OutputFileName.Width = 130
        '
        'Col_UserValue
        '
        Me.Col_UserValue.Text = "User Value"
        Me.Col_UserValue.Width = 100
        '
        'GroupBox_OutputFormat
        '
        Me.GroupBox_OutputFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_OutputFormat.Controls.Add(Me.ComboBox_OutputFormat)
        Me.GroupBox_OutputFormat.Location = New System.Drawing.Point(12, 404)
        Me.GroupBox_OutputFormat.Name = "GroupBox_OutputFormat"
        Me.GroupBox_OutputFormat.Size = New System.Drawing.Size(205, 56)
        Me.GroupBox_OutputFormat.TabIndex = 1
        Me.GroupBox_OutputFormat.TabStop = False
        Me.GroupBox_OutputFormat.Text = "Output This Format Only"
        '
        'ComboBox_OutputFormat
        '
        Me.ComboBox_OutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_OutputFormat.FormattingEnabled = True
        Me.ComboBox_OutputFormat.Location = New System.Drawing.Point(6, 19)
        Me.ComboBox_OutputFormat.Name = "ComboBox_OutputFormat"
        Me.ComboBox_OutputFormat.Size = New System.Drawing.Size(193, 21)
        Me.ComboBox_OutputFormat.TabIndex = 0
        '
        'CheckBox_MarkerFileOnly
        '
        Me.CheckBox_MarkerFileOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_MarkerFileOnly.AutoSize = True
        Me.CheckBox_MarkerFileOnly.Location = New System.Drawing.Point(223, 416)
        Me.CheckBox_MarkerFileOnly.Name = "CheckBox_MarkerFileOnly"
        Me.CheckBox_MarkerFileOnly.Size = New System.Drawing.Size(102, 17)
        Me.CheckBox_MarkerFileOnly.TabIndex = 2
        Me.CheckBox_MarkerFileOnly.Text = "Marker File Only"
        Me.CheckBox_MarkerFileOnly.UseVisualStyleBackColor = True
        '
        'TextBox_OutputTime
        '
        Me.TextBox_OutputTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_OutputTime.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_OutputTime.Location = New System.Drawing.Point(223, 439)
        Me.TextBox_OutputTime.Name = "TextBox_OutputTime"
        Me.TextBox_OutputTime.ReadOnly = True
        Me.TextBox_OutputTime.Size = New System.Drawing.Size(217, 20)
        Me.TextBox_OutputTime.TabIndex = 3
        '
        'Button_RemapHashCodes
        '
        Me.Button_RemapHashCodes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_RemapHashCodes.Location = New System.Drawing.Point(331, 410)
        Me.Button_RemapHashCodes.Name = "Button_RemapHashCodes"
        Me.Button_RemapHashCodes.Size = New System.Drawing.Size(109, 23)
        Me.Button_RemapHashCodes.TabIndex = 4
        Me.Button_RemapHashCodes.Text = "ReMap HashCodes"
        Me.Button_RemapHashCodes.UseVisualStyleBackColor = True
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_Ok.Location = New System.Drawing.Point(699, 437)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 6
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'Button_ImportMusics
        '
        Me.Button_ImportMusics.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_ImportMusics.Location = New System.Drawing.Point(446, 410)
        Me.Button_ImportMusics.Name = "Button_ImportMusics"
        Me.Button_ImportMusics.Size = New System.Drawing.Size(130, 23)
        Me.Button_ImportMusics.TabIndex = 5
        Me.Button_ImportMusics.Text = "Import Musics From DLC"
        Me.Button_ImportMusics.UseVisualStyleBackColor = True
        '
        'MusicMaker
        '
        Me.AcceptButton = Me.Button_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(786, 472)
        Me.Controls.Add(Me.Button_ImportMusics)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.Button_RemapHashCodes)
        Me.Controls.Add(Me.TextBox_OutputTime)
        Me.Controls.Add(Me.CheckBox_MarkerFileOnly)
        Me.Controls.Add(Me.GroupBox_OutputFormat)
        Me.Controls.Add(Me.GroupBox_MusicMaker)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MusicMaker"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Music Maker"
        Me.GroupBox_MusicMaker.ResumeLayout(False)
        Me.GroupBox_MusicMaker.PerformLayout()
        CType(Me.Numeric_Volume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_OutputFormat.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox_MusicMaker As GroupBox
    Friend WithEvents Button_VeryfyMfx As Button
    Friend WithEvents Button_ForceOutput As Button
    Friend WithEvents Button_ForceSelected As Button
    Friend WithEvents Button_ViewErrorFile As Button
    Friend WithEvents Button_Output As Button
    Friend WithEvents Button_UpdateFiles As Button
    Friend WithEvents Col_FileName As ColumnHeader
    Friend WithEvents Col_Volume As ColumnHeader
    Friend WithEvents Col_ErrorStatus As ColumnHeader
    Friend WithEvents Col_HashCode As ColumnHeader
    Friend WithEvents Col_Marker As ColumnHeader
    Friend WithEvents Col_Wav As ColumnHeader
    Friend WithEvents Col_OutputFileName As ColumnHeader
    Friend WithEvents Col_UserValue As ColumnHeader
    Friend WithEvents GroupBox_OutputFormat As GroupBox
    Friend WithEvents CheckBox_MarkerFileOnly As CheckBox
    Friend WithEvents Button_RemapHashCodes As Button
    Friend WithEvents Button_Ok As Button
    Private WithEvents TextBox_UserValue As TextBox
    Protected Friend WithEvents TextBox_OutputTime As TextBox
    Private WithEvents Numeric_Volume As NumericUpDown
    Protected Friend WithEvents ListView_MusicFiles As ListView_ColumnSortingClick.ListView_ColumnSortingClick
    Protected Friend WithEvents ComboBox_OutputFormat As ComboBox
    Friend WithEvents Button_ImportMusics As Button
End Class

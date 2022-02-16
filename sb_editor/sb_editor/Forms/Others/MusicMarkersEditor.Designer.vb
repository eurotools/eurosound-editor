<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MusicMarkersEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MusicMarkersEditor))
        Me.GroupBox_WaveFile = New System.Windows.Forms.GroupBox()
        Me.Button_SetFile = New System.Windows.Forms.Button()
        Me.TextBox_MusicFilePath = New System.Windows.Forms.TextBox()
        Me.ListView_Markers = New System.Windows.Forms.ListView()
        Me.Col_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Position = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Goto = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox_MarkerTypes = New System.Windows.Forms.GroupBox()
        Me.Numeric_MarkerPosition = New System.Windows.Forms.NumericUpDown()
        Me.Label_Position = New System.Windows.Forms.Label()
        Me.Button_AddEndMarker = New System.Windows.Forms.Button()
        Me.Button_AddGotoMarker = New System.Windows.Forms.Button()
        Me.Button_AddLoopMarker = New System.Windows.Forms.Button()
        Me.Button_AddStartMarker = New System.Windows.Forms.Button()
        Me.Button_RemoveSelected = New System.Windows.Forms.Button()
        Me.Button_Close = New System.Windows.Forms.Button()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.OpenFileDialog_WaveFile = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox_Options = New System.Windows.Forms.GroupBox()
        Me.Button_RenameMarkers = New System.Windows.Forms.Button()
        Me.GroupBox_WaveFile.SuspendLayout()
        Me.GroupBox_MarkerTypes.SuspendLayout()
        CType(Me.Numeric_MarkerPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Options.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_WaveFile
        '
        Me.GroupBox_WaveFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_WaveFile.Controls.Add(Me.Button_SetFile)
        Me.GroupBox_WaveFile.Controls.Add(Me.TextBox_MusicFilePath)
        Me.GroupBox_WaveFile.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_WaveFile.Name = "GroupBox_WaveFile"
        Me.GroupBox_WaveFile.Size = New System.Drawing.Size(461, 49)
        Me.GroupBox_WaveFile.TabIndex = 0
        Me.GroupBox_WaveFile.TabStop = False
        Me.GroupBox_WaveFile.Text = "Wave File Path"
        '
        'Button_SetFile
        '
        Me.Button_SetFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SetFile.Location = New System.Drawing.Point(380, 17)
        Me.Button_SetFile.Name = "Button_SetFile"
        Me.Button_SetFile.Size = New System.Drawing.Size(75, 23)
        Me.Button_SetFile.TabIndex = 1
        Me.Button_SetFile.Text = "Set File"
        Me.Button_SetFile.UseVisualStyleBackColor = True
        '
        'TextBox_MusicFilePath
        '
        Me.TextBox_MusicFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_MusicFilePath.Location = New System.Drawing.Point(6, 19)
        Me.TextBox_MusicFilePath.Name = "TextBox_MusicFilePath"
        Me.TextBox_MusicFilePath.Size = New System.Drawing.Size(368, 20)
        Me.TextBox_MusicFilePath.TabIndex = 0
        '
        'ListView_Markers
        '
        Me.ListView_Markers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Markers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Col_Name, Me.Col_Position, Me.Col_Goto})
        Me.ListView_Markers.Enabled = False
        Me.ListView_Markers.FullRowSelect = True
        Me.ListView_Markers.GridLines = True
        Me.ListView_Markers.HideSelection = False
        Me.ListView_Markers.Location = New System.Drawing.Point(12, 67)
        Me.ListView_Markers.MultiSelect = False
        Me.ListView_Markers.Name = "ListView_Markers"
        Me.ListView_Markers.Size = New System.Drawing.Size(461, 313)
        Me.ListView_Markers.TabIndex = 1
        Me.ListView_Markers.UseCompatibleStateImageBehavior = False
        Me.ListView_Markers.View = System.Windows.Forms.View.Details
        '
        'Col_Name
        '
        Me.Col_Name.Text = "Name"
        Me.Col_Name.Width = 150
        '
        'Col_Position
        '
        Me.Col_Position.Text = "Position"
        Me.Col_Position.Width = 150
        '
        'Col_Goto
        '
        Me.Col_Goto.Text = "Goto"
        Me.Col_Goto.Width = 150
        '
        'GroupBox_MarkerTypes
        '
        Me.GroupBox_MarkerTypes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_MarkerTypes.Controls.Add(Me.Numeric_MarkerPosition)
        Me.GroupBox_MarkerTypes.Controls.Add(Me.Label_Position)
        Me.GroupBox_MarkerTypes.Controls.Add(Me.Button_AddEndMarker)
        Me.GroupBox_MarkerTypes.Controls.Add(Me.Button_AddGotoMarker)
        Me.GroupBox_MarkerTypes.Controls.Add(Me.Button_AddLoopMarker)
        Me.GroupBox_MarkerTypes.Controls.Add(Me.Button_AddStartMarker)
        Me.GroupBox_MarkerTypes.Enabled = False
        Me.GroupBox_MarkerTypes.Location = New System.Drawing.Point(12, 386)
        Me.GroupBox_MarkerTypes.Name = "GroupBox_MarkerTypes"
        Me.GroupBox_MarkerTypes.Size = New System.Drawing.Size(461, 53)
        Me.GroupBox_MarkerTypes.TabIndex = 2
        Me.GroupBox_MarkerTypes.TabStop = False
        Me.GroupBox_MarkerTypes.Text = "Marker Types"
        '
        'Numeric_MarkerPosition
        '
        Me.Numeric_MarkerPosition.Location = New System.Drawing.Point(243, 22)
        Me.Numeric_MarkerPosition.Maximum = New Decimal(New Integer() {-1, 0, 0, 0})
        Me.Numeric_MarkerPosition.Name = "Numeric_MarkerPosition"
        Me.Numeric_MarkerPosition.Size = New System.Drawing.Size(107, 20)
        Me.Numeric_MarkerPosition.TabIndex = 5
        '
        'Label_Position
        '
        Me.Label_Position.AutoSize = True
        Me.Label_Position.Location = New System.Drawing.Point(190, 24)
        Me.Label_Position.Name = "Label_Position"
        Me.Label_Position.Size = New System.Drawing.Size(47, 13)
        Me.Label_Position.TabIndex = 4
        Me.Label_Position.Text = "Position:"
        '
        'Button_AddEndMarker
        '
        Me.Button_AddEndMarker.Location = New System.Drawing.Point(144, 19)
        Me.Button_AddEndMarker.Name = "Button_AddEndMarker"
        Me.Button_AddEndMarker.Size = New System.Drawing.Size(40, 23)
        Me.Button_AddEndMarker.TabIndex = 3
        Me.Button_AddEndMarker.Text = "End"
        Me.Button_AddEndMarker.UseVisualStyleBackColor = True
        '
        'Button_AddGotoMarker
        '
        Me.Button_AddGotoMarker.Location = New System.Drawing.Point(98, 19)
        Me.Button_AddGotoMarker.Name = "Button_AddGotoMarker"
        Me.Button_AddGotoMarker.Size = New System.Drawing.Size(40, 23)
        Me.Button_AddGotoMarker.TabIndex = 2
        Me.Button_AddGotoMarker.Text = "Goto"
        Me.Button_AddGotoMarker.UseVisualStyleBackColor = True
        '
        'Button_AddLoopMarker
        '
        Me.Button_AddLoopMarker.Location = New System.Drawing.Point(52, 19)
        Me.Button_AddLoopMarker.Name = "Button_AddLoopMarker"
        Me.Button_AddLoopMarker.Size = New System.Drawing.Size(40, 23)
        Me.Button_AddLoopMarker.TabIndex = 1
        Me.Button_AddLoopMarker.Text = "Loop"
        Me.Button_AddLoopMarker.UseVisualStyleBackColor = True
        '
        'Button_AddStartMarker
        '
        Me.Button_AddStartMarker.Location = New System.Drawing.Point(6, 19)
        Me.Button_AddStartMarker.Name = "Button_AddStartMarker"
        Me.Button_AddStartMarker.Size = New System.Drawing.Size(40, 23)
        Me.Button_AddStartMarker.TabIndex = 0
        Me.Button_AddStartMarker.Text = "Start"
        Me.Button_AddStartMarker.UseVisualStyleBackColor = True
        '
        'Button_RemoveSelected
        '
        Me.Button_RemoveSelected.Location = New System.Drawing.Point(6, 19)
        Me.Button_RemoveSelected.Name = "Button_RemoveSelected"
        Me.Button_RemoveSelected.Size = New System.Drawing.Size(101, 23)
        Me.Button_RemoveSelected.TabIndex = 6
        Me.Button_RemoveSelected.Text = "Delete Selected"
        Me.Button_RemoveSelected.UseVisualStyleBackColor = True
        '
        'Button_Close
        '
        Me.Button_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Close.Location = New System.Drawing.Point(317, 504)
        Me.Button_Close.Name = "Button_Close"
        Me.Button_Close.Size = New System.Drawing.Size(75, 23)
        Me.Button_Close.TabIndex = 3
        Me.Button_Close.Text = "Close"
        Me.Button_Close.UseVisualStyleBackColor = True
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.Location = New System.Drawing.Point(398, 504)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 4
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'OpenFileDialog_WaveFile
        '
        Me.OpenFileDialog_WaveFile.DefaultExt = "*.wav"
        Me.OpenFileDialog_WaveFile.Filter = "Wave Files (*.wav)|*.wav"
        '
        'GroupBox_Options
        '
        Me.GroupBox_Options.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Options.Controls.Add(Me.Button_RenameMarkers)
        Me.GroupBox_Options.Controls.Add(Me.Button_RemoveSelected)
        Me.GroupBox_Options.Enabled = False
        Me.GroupBox_Options.Location = New System.Drawing.Point(12, 445)
        Me.GroupBox_Options.Name = "GroupBox_Options"
        Me.GroupBox_Options.Size = New System.Drawing.Size(461, 53)
        Me.GroupBox_Options.TabIndex = 5
        Me.GroupBox_Options.TabStop = False
        Me.GroupBox_Options.Text = "Options"
        '
        'Button_RenameMarkers
        '
        Me.Button_RenameMarkers.Location = New System.Drawing.Point(113, 19)
        Me.Button_RenameMarkers.Name = "Button_RenameMarkers"
        Me.Button_RenameMarkers.Size = New System.Drawing.Size(101, 23)
        Me.Button_RenameMarkers.TabIndex = 7
        Me.Button_RenameMarkers.Text = "Re-Name Markers"
        Me.Button_RenameMarkers.UseVisualStyleBackColor = True
        '
        'MusicMarkersEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 539)
        Me.Controls.Add(Me.GroupBox_Options)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.Button_Close)
        Me.Controls.Add(Me.GroupBox_MarkerTypes)
        Me.Controls.Add(Me.ListView_Markers)
        Me.Controls.Add(Me.GroupBox_WaveFile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MusicMarkersEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Music Markers Editor"
        Me.GroupBox_WaveFile.ResumeLayout(False)
        Me.GroupBox_WaveFile.PerformLayout()
        Me.GroupBox_MarkerTypes.ResumeLayout(False)
        Me.GroupBox_MarkerTypes.PerformLayout()
        CType(Me.Numeric_MarkerPosition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Options.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_WaveFile As GroupBox
    Friend WithEvents Button_SetFile As Button
    Friend WithEvents TextBox_MusicFilePath As TextBox
    Friend WithEvents ListView_Markers As ListView
    Friend WithEvents Col_Name As ColumnHeader
    Friend WithEvents Col_Position As ColumnHeader
    Friend WithEvents Col_Goto As ColumnHeader
    Friend WithEvents GroupBox_MarkerTypes As GroupBox
    Friend WithEvents Numeric_MarkerPosition As NumericUpDown
    Friend WithEvents Label_Position As Label
    Friend WithEvents Button_AddEndMarker As Button
    Friend WithEvents Button_AddGotoMarker As Button
    Friend WithEvents Button_AddLoopMarker As Button
    Friend WithEvents Button_AddStartMarker As Button
    Friend WithEvents Button_Close As Button
    Friend WithEvents Button_Ok As Button
    Friend WithEvents OpenFileDialog_WaveFile As OpenFileDialog
    Friend WithEvents Button_RemoveSelected As Button
    Friend WithEvents GroupBox_Options As GroupBox
    Friend WithEvents Button_RenameMarkers As Button
End Class

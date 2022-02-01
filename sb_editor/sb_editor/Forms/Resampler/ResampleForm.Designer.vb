<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ResampleForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResampleForm))
        Me.GroupBox_ProjectPath = New System.Windows.Forms.GroupBox()
        Me.TextBox_ProjectPath = New System.Windows.Forms.TextBox()
        Me.MainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem_Sample = New System.Windows.Forms.MenuItem()
        Me.MainMenu_SamplePlay = New System.Windows.Forms.MenuItem()
        Me.MainMenu_SampleStop = New System.Windows.Forms.MenuItem()
        Me.MainMenu_SampleEdit = New System.Windows.Forms.MenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button_Stop = New System.Windows.Forms.Button()
        Me.Button_Preview = New System.Windows.Forms.Button()
        Me.ComboBox_PreviewOptions = New System.Windows.Forms.ComboBox()
        Me.Label_SampleCount = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox_AvailableRates = New System.Windows.Forms.ComboBox()
        Me.Button_ReSampleAll = New System.Windows.Forms.Button()
        Me.Button_StreamSel = New System.Windows.Forms.Button()
        Me.Button_DeReSampleAll = New System.Windows.Forms.Button()
        Me.Button_UnStreamSel = New System.Windows.Forms.Button()
        Me.Button_EditSample = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button_MoveSelection = New System.Windows.Forms.Button()
        Me.TextBox_MoveSamplesTo = New System.Windows.Forms.TextBox()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ContextMenu_SampleOptions = New System.Windows.Forms.ContextMenu()
        Me.MenuContext_Play = New System.Windows.Forms.MenuItem()
        Me.MenuContext_Stop = New System.Windows.Forms.MenuItem()
        Me.MenuContext_Edit = New System.Windows.Forms.MenuItem()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox_BootupTime = New System.Windows.Forms.TextBox()
        Me.FolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.ListView_Samples = New ListView_ColumnSortingClick.ListView_ColumnSortingClick()
        Me.Col_SampleFileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_ReSampleRate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Size = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Date = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_ReSample = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Stream = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_ReSmp4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_ReSmp2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_ReSmp3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_ReSmp5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox_ProjectPath.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_ProjectPath
        '
        Me.GroupBox_ProjectPath.Controls.Add(Me.TextBox_ProjectPath)
        Me.GroupBox_ProjectPath.Location = New System.Drawing.Point(12, 4)
        Me.GroupBox_ProjectPath.Name = "GroupBox_ProjectPath"
        Me.GroupBox_ProjectPath.Size = New System.Drawing.Size(551, 48)
        Me.GroupBox_ProjectPath.TabIndex = 0
        Me.GroupBox_ProjectPath.TabStop = False
        Me.GroupBox_ProjectPath.Text = "Project Path"
        '
        'TextBox_ProjectPath
        '
        Me.TextBox_ProjectPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_ProjectPath.Location = New System.Drawing.Point(6, 19)
        Me.TextBox_ProjectPath.Name = "TextBox_ProjectPath"
        Me.TextBox_ProjectPath.Size = New System.Drawing.Size(539, 20)
        Me.TextBox_ProjectPath.TabIndex = 0
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem_Sample})
        '
        'MenuItem_Sample
        '
        Me.MenuItem_Sample.Index = 0
        Me.MenuItem_Sample.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MainMenu_SamplePlay, Me.MainMenu_SampleStop, Me.MainMenu_SampleEdit})
        Me.MenuItem_Sample.Text = "Sample"
        '
        'MainMenu_SamplePlay
        '
        Me.MainMenu_SamplePlay.Index = 0
        Me.MainMenu_SamplePlay.Text = "Play"
        '
        'MainMenu_SampleStop
        '
        Me.MainMenu_SampleStop.Index = 1
        Me.MainMenu_SampleStop.Text = "Stop"
        '
        'MainMenu_SampleEdit
        '
        Me.MainMenu_SampleEdit.Index = 2
        Me.MainMenu_SampleEdit.Text = "Edit"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button_Stop)
        Me.GroupBox1.Controls.Add(Me.Button_Preview)
        Me.GroupBox1.Controls.Add(Me.ComboBox_PreviewOptions)
        Me.GroupBox1.Location = New System.Drawing.Point(569, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(385, 48)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Preview Sample"
        '
        'Button_Stop
        '
        Me.Button_Stop.Location = New System.Drawing.Point(304, 17)
        Me.Button_Stop.Name = "Button_Stop"
        Me.Button_Stop.Size = New System.Drawing.Size(75, 23)
        Me.Button_Stop.TabIndex = 2
        Me.Button_Stop.Text = "Stop"
        Me.Button_Stop.UseVisualStyleBackColor = True
        '
        'Button_Preview
        '
        Me.Button_Preview.Location = New System.Drawing.Point(223, 17)
        Me.Button_Preview.Name = "Button_Preview"
        Me.Button_Preview.Size = New System.Drawing.Size(75, 23)
        Me.Button_Preview.TabIndex = 1
        Me.Button_Preview.Text = "Preview"
        Me.Button_Preview.UseVisualStyleBackColor = True
        '
        'ComboBox_PreviewOptions
        '
        Me.ComboBox_PreviewOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PreviewOptions.FormattingEnabled = True
        Me.ComboBox_PreviewOptions.Items.AddRange(New Object() {"Original (Not Re-Sampled)", "PlayStation2", "GameCube", "PC", "X Box"})
        Me.ComboBox_PreviewOptions.Location = New System.Drawing.Point(6, 18)
        Me.ComboBox_PreviewOptions.Name = "ComboBox_PreviewOptions"
        Me.ComboBox_PreviewOptions.Size = New System.Drawing.Size(211, 21)
        Me.ComboBox_PreviewOptions.TabIndex = 0
        '
        'Label_SampleCount
        '
        Me.Label_SampleCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_SampleCount.AutoSize = True
        Me.Label_SampleCount.Location = New System.Drawing.Point(15, 510)
        Me.Label_SampleCount.Name = "Label_SampleCount"
        Me.Label_SampleCount.Size = New System.Drawing.Size(85, 13)
        Me.Label_SampleCount.TabIndex = 3
        Me.Label_SampleCount.Text = "Sample Count: 0"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(339, 510)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(185, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Set Re-Sample Rate for Selection To:"
        '
        'ComboBox_AvailableRates
        '
        Me.ComboBox_AvailableRates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_AvailableRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_AvailableRates.FormattingEnabled = True
        Me.ComboBox_AvailableRates.Location = New System.Drawing.Point(530, 507)
        Me.ComboBox_AvailableRates.Name = "ComboBox_AvailableRates"
        Me.ComboBox_AvailableRates.Size = New System.Drawing.Size(226, 21)
        Me.ComboBox_AvailableRates.TabIndex = 5
        '
        'Button_ReSampleAll
        '
        Me.Button_ReSampleAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ReSampleAll.Location = New System.Drawing.Point(762, 507)
        Me.Button_ReSampleAll.Name = "Button_ReSampleAll"
        Me.Button_ReSampleAll.Size = New System.Drawing.Size(93, 23)
        Me.Button_ReSampleAll.TabIndex = 6
        Me.Button_ReSampleAll.Text = "ReSample All"
        Me.Button_ReSampleAll.UseVisualStyleBackColor = True
        '
        'Button_StreamSel
        '
        Me.Button_StreamSel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_StreamSel.Location = New System.Drawing.Point(861, 507)
        Me.Button_StreamSel.Name = "Button_StreamSel"
        Me.Button_StreamSel.Size = New System.Drawing.Size(93, 23)
        Me.Button_StreamSel.TabIndex = 7
        Me.Button_StreamSel.Text = "Stream Sel."
        Me.Button_StreamSel.UseVisualStyleBackColor = True
        '
        'Button_DeReSampleAll
        '
        Me.Button_DeReSampleAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_DeReSampleAll.Location = New System.Drawing.Point(762, 536)
        Me.Button_DeReSampleAll.Name = "Button_DeReSampleAll"
        Me.Button_DeReSampleAll.Size = New System.Drawing.Size(93, 23)
        Me.Button_DeReSampleAll.TabIndex = 10
        Me.Button_DeReSampleAll.Text = "DeReSample All"
        Me.Button_DeReSampleAll.UseVisualStyleBackColor = True
        '
        'Button_UnStreamSel
        '
        Me.Button_UnStreamSel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_UnStreamSel.Location = New System.Drawing.Point(861, 536)
        Me.Button_UnStreamSel.Name = "Button_UnStreamSel"
        Me.Button_UnStreamSel.Size = New System.Drawing.Size(93, 23)
        Me.Button_UnStreamSel.TabIndex = 11
        Me.Button_UnStreamSel.Text = "UnStream Sel."
        Me.Button_UnStreamSel.UseVisualStyleBackColor = True
        '
        'Button_EditSample
        '
        Me.Button_EditSample.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_EditSample.Location = New System.Drawing.Point(18, 536)
        Me.Button_EditSample.Name = "Button_EditSample"
        Me.Button_EditSample.Size = New System.Drawing.Size(75, 23)
        Me.Button_EditSample.TabIndex = 8
        Me.Button_EditSample.Text = "Edit"
        Me.Button_EditSample.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Button_MoveSelection)
        Me.GroupBox2.Controls.Add(Me.TextBox_MoveSamplesTo)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 565)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(720, 49)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Move Selected Sample File Folder:"
        '
        'Button_MoveSelection
        '
        Me.Button_MoveSelection.Location = New System.Drawing.Point(557, 17)
        Me.Button_MoveSelection.Name = "Button_MoveSelection"
        Me.Button_MoveSelection.Size = New System.Drawing.Size(152, 23)
        Me.Button_MoveSelection.TabIndex = 1
        Me.Button_MoveSelection.Text = "Move Selection to this Folder"
        Me.Button_MoveSelection.UseVisualStyleBackColor = True
        '
        'TextBox_MoveSamplesTo
        '
        Me.TextBox_MoveSamplesTo.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_MoveSamplesTo.Location = New System.Drawing.Point(6, 19)
        Me.TextBox_MoveSamplesTo.Name = "TextBox_MoveSamplesTo"
        Me.TextBox_MoveSamplesTo.ReadOnly = True
        Me.TextBox_MoveSamplesTo.Size = New System.Drawing.Size(545, 20)
        Me.TextBox_MoveSamplesTo.TabIndex = 0
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(798, 620)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 13
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(879, 620)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 14
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(532, 536)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Fix"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ContextMenu_SampleOptions
        '
        Me.ContextMenu_SampleOptions.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuContext_Play, Me.MenuContext_Stop, Me.MenuContext_Edit})
        '
        'MenuContext_Play
        '
        Me.MenuContext_Play.Index = 0
        Me.MenuContext_Play.Text = "Play"
        '
        'MenuContext_Stop
        '
        Me.MenuContext_Stop.Index = 1
        Me.MenuContext_Stop.Text = "Stop"
        '
        'MenuContext_Edit
        '
        Me.MenuContext_Edit.Index = 2
        Me.MenuContext_Edit.Text = "Edit"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(613, 536)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 15
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox_BootupTime
        '
        Me.TextBox_BootupTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_BootupTime.Location = New System.Drawing.Point(99, 538)
        Me.TextBox_BootupTime.Name = "TextBox_BootupTime"
        Me.TextBox_BootupTime.Size = New System.Drawing.Size(274, 20)
        Me.TextBox_BootupTime.TabIndex = 9
        '
        'FolderBrowser
        '
        Me.FolderBrowser.Description = "Set Folder For Sample Files."
        Me.FolderBrowser.ShowNewFolderButton = False
        '
        'ListView_Samples
        '
        Me.ListView_Samples.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Samples.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Col_SampleFileName, Me.Col_ReSampleRate, Me.Col_Size, Me.Col_Date, Me.Col_ReSample, Me.Col_Stream, Me.Col_ReSmp4, Me.Col_ReSmp2, Me.Col_ReSmp3, Me.Col_ReSmp5})
        Me.ListView_Samples.ContextMenu = Me.ContextMenu_SampleOptions
        Me.ListView_Samples.FullRowSelect = True
        Me.ListView_Samples.GridLines = True
        Me.ListView_Samples.HideSelection = False
        Me.ListView_Samples.Location = New System.Drawing.Point(12, 58)
        Me.ListView_Samples.Name = "ListView_Samples"
        Me.ListView_Samples.Size = New System.Drawing.Size(942, 443)
        Me.ListView_Samples.TabIndex = 2
        Me.ListView_Samples.UseCompatibleStateImageBehavior = False
        Me.ListView_Samples.View = System.Windows.Forms.View.Details
        '
        'Col_SampleFileName
        '
        Me.Col_SampleFileName.Text = "Sample Filename"
        Me.Col_SampleFileName.Width = 300
        '
        'Col_ReSampleRate
        '
        Me.Col_ReSampleRate.Text = "Re-Sample Rate"
        Me.Col_ReSampleRate.Width = 100
        '
        'Col_Size
        '
        Me.Col_Size.Text = "Size"
        Me.Col_Size.Width = 100
        '
        'Col_Date
        '
        Me.Col_Date.Text = "Date"
        Me.Col_Date.Width = 150
        '
        'Col_ReSample
        '
        Me.Col_ReSample.Text = "ReSample"
        Me.Col_ReSample.Width = 80
        '
        'Col_Stream
        '
        Me.Col_Stream.Text = "Stream Me?"
        Me.Col_Stream.Width = 100
        '
        'Col_ReSmp4
        '
        Me.Col_ReSmp4.Text = "ReSmp4"
        Me.Col_ReSmp4.Width = 80
        '
        'Col_ReSmp2
        '
        Me.Col_ReSmp2.Text = "ReSmp2"
        Me.Col_ReSmp2.Width = 80
        '
        'Col_ReSmp3
        '
        Me.Col_ReSmp3.Text = "ReSmp3"
        Me.Col_ReSmp3.Width = 80
        '
        'Col_ReSmp5
        '
        Me.Col_ReSmp5.Text = "ReSmp4"
        Me.Col_ReSmp5.Width = 80
        '
        'ResampleForm
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(966, 655)
        Me.Controls.Add(Me.TextBox_BootupTime)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Button_EditSample)
        Me.Controls.Add(Me.Button_UnStreamSel)
        Me.Controls.Add(Me.Button_DeReSampleAll)
        Me.Controls.Add(Me.Button_StreamSel)
        Me.Controls.Add(Me.Button_ReSampleAll)
        Me.Controls.Add(Me.ComboBox_AvailableRates)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label_SampleCount)
        Me.Controls.Add(Me.ListView_Samples)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox_ProjectPath)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu
        Me.MinimizeBox = False
        Me.Name = "ResampleForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Set Re-Sample Rates"
        Me.GroupBox_ProjectPath.ResumeLayout(False)
        Me.GroupBox_ProjectPath.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox_ProjectPath As GroupBox
    Friend WithEvents TextBox_ProjectPath As TextBox
    Friend WithEvents MainMenu As MainMenu
    Friend WithEvents MenuItem_Sample As MenuItem
    Friend WithEvents MainMenu_SamplePlay As MenuItem
    Friend WithEvents MainMenu_SampleStop As MenuItem
    Friend WithEvents MainMenu_SampleEdit As MenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button_Stop As Button
    Friend WithEvents Button_Preview As Button
    Friend WithEvents ComboBox_PreviewOptions As ComboBox
    Friend WithEvents ListView_Samples As ListView_ColumnSortingClick.ListView_ColumnSortingClick
    Friend WithEvents Col_SampleFileName As ColumnHeader
    Friend WithEvents Col_ReSampleRate As ColumnHeader
    Friend WithEvents Col_Size As ColumnHeader
    Friend WithEvents Col_Date As ColumnHeader
    Friend WithEvents Col_ReSample As ColumnHeader
    Friend WithEvents Col_Stream As ColumnHeader
    Friend WithEvents Col_ReSmp4 As ColumnHeader
    Friend WithEvents Col_ReSmp2 As ColumnHeader
    Friend WithEvents Col_ReSmp3 As ColumnHeader
    Friend WithEvents Col_ReSmp5 As ColumnHeader
    Friend WithEvents Label_SampleCount As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox_AvailableRates As ComboBox
    Friend WithEvents Button_ReSampleAll As Button
    Friend WithEvents Button_StreamSel As Button
    Friend WithEvents Button_DeReSampleAll As Button
    Friend WithEvents Button_UnStreamSel As Button
    Friend WithEvents Button_EditSample As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button_MoveSelection As Button
    Friend WithEvents TextBox_MoveSamplesTo As TextBox
    Friend WithEvents Button_OK As Button
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ContextMenu_SampleOptions As ContextMenu
    Friend WithEvents MenuContext_Play As MenuItem
    Friend WithEvents MenuContext_Stop As MenuItem
    Friend WithEvents MenuContext_Edit As MenuItem
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox_BootupTime As TextBox
    Friend WithEvents FolderBrowser As FolderBrowserDialog
End Class

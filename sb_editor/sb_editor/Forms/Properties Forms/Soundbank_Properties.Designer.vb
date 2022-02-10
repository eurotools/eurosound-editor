<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Soundbank_Properties
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Soundbank_Properties))
        Me.GroupBox_SoundbankData = New System.Windows.Forms.GroupBox()
        Me.GroupBox_FileSize = New System.Windows.Forms.GroupBox()
        Me.Label_SizeFile_Xbox = New System.Windows.Forms.Label()
        Me.Label_XBox = New System.Windows.Forms.Label()
        Me.Label_SizeFile_PC = New System.Windows.Forms.Label()
        Me.Label_Pc = New System.Windows.Forms.Label()
        Me.Label_SizeFile_GameCube = New System.Windows.Forms.Label()
        Me.Label_GameCube = New System.Windows.Forms.Label()
        Me.Label_SizeFile_PlayStation2 = New System.Windows.Forms.Label()
        Me.Label_PlayStation2 = New System.Windows.Forms.Label()
        Me.GroupBox_Recount = New System.Windows.Forms.GroupBox()
        Me.Label_Value_Size = New System.Windows.Forms.Label()
        Me.Label_Size = New System.Windows.Forms.Label()
        Me.Label_SampleCount_Value = New System.Windows.Forms.Label()
        Me.Label_SampleCount = New System.Windows.Forms.Label()
        Me.Label_SfxCount_Value = New System.Windows.Forms.Label()
        Me.Label_DatabaseCount_Value = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label_DataBaseCount = New System.Windows.Forms.Label()
        Me.GroupBox_FileInfo = New System.Windows.Forms.GroupBox()
        Me.Label_ModifiedBy_Value = New System.Windows.Forms.Label()
        Me.Label_ModifiedBy = New System.Windows.Forms.Label()
        Me.Label_CreatedBy_Value = New System.Windows.Forms.Label()
        Me.Label_CreatedBy = New System.Windows.Forms.Label()
        Me.Label_Value_LastModified = New System.Windows.Forms.Label()
        Me.Label_LastModified = New System.Windows.Forms.Label()
        Me.Label_Value_FirstCreated = New System.Windows.Forms.Label()
        Me.Label_FirstCreated = New System.Windows.Forms.Label()
        Me.Label_Value_OutFileName = New System.Windows.Forms.Label()
        Me.Label_OutFileName = New System.Windows.Forms.Label()
        Me.ListBox_SFXs = New System.Windows.Forms.ListBox()
        Me.Label_SfxCount = New System.Windows.Forms.Label()
        Me.Label_DataBasesCount = New System.Windows.Forms.Label()
        Me.Label_TotalSamples = New System.Windows.Forms.Label()
        Me.ListBox_SamplesList = New System.Windows.Forms.ListBox()
        Me.ListBox_Databases = New System.Windows.Forms.ListBox()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.GroupBox_SoundbankData.SuspendLayout()
        Me.GroupBox_FileSize.SuspendLayout()
        Me.GroupBox_Recount.SuspendLayout()
        Me.GroupBox_FileInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_SoundbankData
        '
        Me.GroupBox_SoundbankData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_SoundbankData.Controls.Add(Me.GroupBox_FileSize)
        Me.GroupBox_SoundbankData.Controls.Add(Me.GroupBox_Recount)
        Me.GroupBox_SoundbankData.Controls.Add(Me.GroupBox_FileInfo)
        Me.GroupBox_SoundbankData.Controls.Add(Me.Label_Value_OutFileName)
        Me.GroupBox_SoundbankData.Controls.Add(Me.Label_OutFileName)
        Me.GroupBox_SoundbankData.Controls.Add(Me.ListBox_SFXs)
        Me.GroupBox_SoundbankData.Controls.Add(Me.Label_SfxCount)
        Me.GroupBox_SoundbankData.Controls.Add(Me.Label_DataBasesCount)
        Me.GroupBox_SoundbankData.Controls.Add(Me.Label_TotalSamples)
        Me.GroupBox_SoundbankData.Controls.Add(Me.ListBox_SamplesList)
        Me.GroupBox_SoundbankData.Controls.Add(Me.ListBox_Databases)
        Me.GroupBox_SoundbankData.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_SoundbankData.Name = "GroupBox_SoundbankData"
        Me.GroupBox_SoundbankData.Size = New System.Drawing.Size(733, 632)
        Me.GroupBox_SoundbankData.TabIndex = 0
        Me.GroupBox_SoundbankData.TabStop = False
        Me.GroupBox_SoundbankData.Text = "Soundbank Name"
        '
        'GroupBox_FileSize
        '
        Me.GroupBox_FileSize.Controls.Add(Me.Label_SizeFile_Xbox)
        Me.GroupBox_FileSize.Controls.Add(Me.Label_XBox)
        Me.GroupBox_FileSize.Controls.Add(Me.Label_SizeFile_PC)
        Me.GroupBox_FileSize.Controls.Add(Me.Label_Pc)
        Me.GroupBox_FileSize.Controls.Add(Me.Label_SizeFile_GameCube)
        Me.GroupBox_FileSize.Controls.Add(Me.Label_GameCube)
        Me.GroupBox_FileSize.Controls.Add(Me.Label_SizeFile_PlayStation2)
        Me.GroupBox_FileSize.Controls.Add(Me.Label_PlayStation2)
        Me.GroupBox_FileSize.Location = New System.Drawing.Point(6, 218)
        Me.GroupBox_FileSize.Name = "GroupBox_FileSize"
        Me.GroupBox_FileSize.Size = New System.Drawing.Size(358, 91)
        Me.GroupBox_FileSize.TabIndex = 10
        Me.GroupBox_FileSize.TabStop = False
        Me.GroupBox_FileSize.Text = "Estimated File Size"
        '
        'Label_SizeFile_Xbox
        '
        Me.Label_SizeFile_Xbox.AutoSize = True
        Me.Label_SizeFile_Xbox.Location = New System.Drawing.Point(100, 65)
        Me.Label_SizeFile_Xbox.Name = "Label_SizeFile_Xbox"
        Me.Label_SizeFile_Xbox.Size = New System.Drawing.Size(179, 13)
        Me.Label_SizeFile_Xbox.TabIndex = 7
        Me.Label_SizeFile_Xbox.Text = "0 (MB) (00,000 bytes) - ESTIMATED"
        '
        'Label_XBox
        '
        Me.Label_XBox.AutoSize = True
        Me.Label_XBox.Location = New System.Drawing.Point(6, 65)
        Me.Label_XBox.Name = "Label_XBox"
        Me.Label_XBox.Size = New System.Drawing.Size(38, 13)
        Me.Label_XBox.TabIndex = 6
        Me.Label_XBox.Text = "X Box:"
        '
        'Label_SizeFile_PC
        '
        Me.Label_SizeFile_PC.AutoSize = True
        Me.Label_SizeFile_PC.Location = New System.Drawing.Point(100, 48)
        Me.Label_SizeFile_PC.Name = "Label_SizeFile_PC"
        Me.Label_SizeFile_PC.Size = New System.Drawing.Size(179, 13)
        Me.Label_SizeFile_PC.TabIndex = 5
        Me.Label_SizeFile_PC.Text = "0 (MB) (00,000 bytes) - ESTIMATED"
        '
        'Label_Pc
        '
        Me.Label_Pc.AutoSize = True
        Me.Label_Pc.Location = New System.Drawing.Point(6, 48)
        Me.Label_Pc.Name = "Label_Pc"
        Me.Label_Pc.Size = New System.Drawing.Size(24, 13)
        Me.Label_Pc.TabIndex = 4
        Me.Label_Pc.Text = "PC:"
        '
        'Label_SizeFile_GameCube
        '
        Me.Label_SizeFile_GameCube.AutoSize = True
        Me.Label_SizeFile_GameCube.Location = New System.Drawing.Point(100, 32)
        Me.Label_SizeFile_GameCube.Name = "Label_SizeFile_GameCube"
        Me.Label_SizeFile_GameCube.Size = New System.Drawing.Size(179, 13)
        Me.Label_SizeFile_GameCube.TabIndex = 3
        Me.Label_SizeFile_GameCube.Text = "0 (MB) (00,000 bytes) - ESTIMATED"
        '
        'Label_GameCube
        '
        Me.Label_GameCube.AutoSize = True
        Me.Label_GameCube.Location = New System.Drawing.Point(6, 32)
        Me.Label_GameCube.Name = "Label_GameCube"
        Me.Label_GameCube.Size = New System.Drawing.Size(63, 13)
        Me.Label_GameCube.TabIndex = 2
        Me.Label_GameCube.Text = "GameCube:"
        '
        'Label_SizeFile_PlayStation2
        '
        Me.Label_SizeFile_PlayStation2.AutoSize = True
        Me.Label_SizeFile_PlayStation2.Location = New System.Drawing.Point(100, 16)
        Me.Label_SizeFile_PlayStation2.Name = "Label_SizeFile_PlayStation2"
        Me.Label_SizeFile_PlayStation2.Size = New System.Drawing.Size(179, 13)
        Me.Label_SizeFile_PlayStation2.TabIndex = 1
        Me.Label_SizeFile_PlayStation2.Text = "0 (MB) (00,000 bytes) - ESTIMATED"
        '
        'Label_PlayStation2
        '
        Me.Label_PlayStation2.AutoSize = True
        Me.Label_PlayStation2.Location = New System.Drawing.Point(6, 16)
        Me.Label_PlayStation2.Name = "Label_PlayStation2"
        Me.Label_PlayStation2.Size = New System.Drawing.Size(69, 13)
        Me.Label_PlayStation2.TabIndex = 0
        Me.Label_PlayStation2.Text = "PlayStation2:"
        '
        'GroupBox_Recount
        '
        Me.GroupBox_Recount.Controls.Add(Me.Label_Value_Size)
        Me.GroupBox_Recount.Controls.Add(Me.Label_Size)
        Me.GroupBox_Recount.Controls.Add(Me.Label_SampleCount_Value)
        Me.GroupBox_Recount.Controls.Add(Me.Label_SampleCount)
        Me.GroupBox_Recount.Controls.Add(Me.Label_SfxCount_Value)
        Me.GroupBox_Recount.Controls.Add(Me.Label_DatabaseCount_Value)
        Me.GroupBox_Recount.Controls.Add(Me.Label1)
        Me.GroupBox_Recount.Controls.Add(Me.Label_DataBaseCount)
        Me.GroupBox_Recount.Location = New System.Drawing.Point(6, 106)
        Me.GroupBox_Recount.Name = "GroupBox_Recount"
        Me.GroupBox_Recount.Size = New System.Drawing.Size(358, 93)
        Me.GroupBox_Recount.TabIndex = 9
        Me.GroupBox_Recount.TabStop = False
        Me.GroupBox_Recount.Text = "Files Recount:"
        '
        'Label_Value_Size
        '
        Me.Label_Value_Size.AutoSize = True
        Me.Label_Value_Size.Location = New System.Drawing.Point(100, 73)
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
        'Label_SampleCount_Value
        '
        Me.Label_SampleCount_Value.AutoSize = True
        Me.Label_SampleCount_Value.Location = New System.Drawing.Point(100, 56)
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
        Me.Label_SfxCount_Value.Location = New System.Drawing.Point(100, 37)
        Me.Label_SfxCount_Value.Name = "Label_SfxCount_Value"
        Me.Label_SfxCount_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_SfxCount_Value.TabIndex = 3
        Me.Label_SfxCount_Value.Text = "0"
        '
        'Label_DatabaseCount_Value
        '
        Me.Label_DatabaseCount_Value.AutoSize = True
        Me.Label_DatabaseCount_Value.Location = New System.Drawing.Point(100, 18)
        Me.Label_DatabaseCount_Value.Name = "Label_DatabaseCount_Value"
        Me.Label_DatabaseCount_Value.Size = New System.Drawing.Size(13, 13)
        Me.Label_DatabaseCount_Value.TabIndex = 1
        Me.Label_DatabaseCount_Value.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "SFX Count:"
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
        'GroupBox_FileInfo
        '
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_ModifiedBy_Value)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_ModifiedBy)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_CreatedBy_Value)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_CreatedBy)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_Value_LastModified)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_LastModified)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_Value_FirstCreated)
        Me.GroupBox_FileInfo.Controls.Add(Me.Label_FirstCreated)
        Me.GroupBox_FileInfo.Location = New System.Drawing.Point(6, 16)
        Me.GroupBox_FileInfo.Name = "GroupBox_FileInfo"
        Me.GroupBox_FileInfo.Size = New System.Drawing.Size(358, 84)
        Me.GroupBox_FileInfo.TabIndex = 8
        Me.GroupBox_FileInfo.TabStop = False
        Me.GroupBox_FileInfo.Text = "File Info"
        '
        'Label_ModifiedBy_Value
        '
        Me.Label_ModifiedBy_Value.AutoSize = True
        Me.Label_ModifiedBy_Value.Location = New System.Drawing.Point(100, 64)
        Me.Label_ModifiedBy_Value.Name = "Label_ModifiedBy_Value"
        Me.Label_ModifiedBy_Value.Size = New System.Drawing.Size(35, 13)
        Me.Label_ModifiedBy_Value.TabIndex = 7
        Me.Label_ModifiedBy_Value.Text = "XXXX"
        '
        'Label_ModifiedBy
        '
        Me.Label_ModifiedBy.AutoSize = True
        Me.Label_ModifiedBy.Location = New System.Drawing.Point(6, 63)
        Me.Label_ModifiedBy.Name = "Label_ModifiedBy"
        Me.Label_ModifiedBy.Size = New System.Drawing.Size(88, 13)
        Me.Label_ModifiedBy.TabIndex = 6
        Me.Label_ModifiedBy.Text = "Last Modified By:"
        '
        'Label_CreatedBy_Value
        '
        Me.Label_CreatedBy_Value.AutoSize = True
        Me.Label_CreatedBy_Value.Location = New System.Drawing.Point(100, 30)
        Me.Label_CreatedBy_Value.Name = "Label_CreatedBy_Value"
        Me.Label_CreatedBy_Value.Size = New System.Drawing.Size(35, 13)
        Me.Label_CreatedBy_Value.TabIndex = 3
        Me.Label_CreatedBy_Value.Text = "XXXX"
        '
        'Label_CreatedBy
        '
        Me.Label_CreatedBy.AutoSize = True
        Me.Label_CreatedBy.Location = New System.Drawing.Point(32, 30)
        Me.Label_CreatedBy.Name = "Label_CreatedBy"
        Me.Label_CreatedBy.Size = New System.Drawing.Size(62, 13)
        Me.Label_CreatedBy.TabIndex = 2
        Me.Label_CreatedBy.Text = "Created By:"
        '
        'Label_Value_LastModified
        '
        Me.Label_Value_LastModified.AutoSize = True
        Me.Label_Value_LastModified.Location = New System.Drawing.Point(100, 47)
        Me.Label_Value_LastModified.Name = "Label_Value_LastModified"
        Me.Label_Value_LastModified.Size = New System.Drawing.Size(120, 13)
        Me.Label_Value_LastModified.TabIndex = 5
        Me.Label_Value_LastModified.Text = "XX-XX-XXXX XX:XX:XX"
        '
        'Label_LastModified
        '
        Me.Label_LastModified.AutoSize = True
        Me.Label_LastModified.Location = New System.Drawing.Point(21, 47)
        Me.Label_LastModified.Name = "Label_LastModified"
        Me.Label_LastModified.Size = New System.Drawing.Size(73, 13)
        Me.Label_LastModified.TabIndex = 4
        Me.Label_LastModified.Text = "Last Modified:"
        '
        'Label_Value_FirstCreated
        '
        Me.Label_Value_FirstCreated.AutoSize = True
        Me.Label_Value_FirstCreated.Location = New System.Drawing.Point(100, 13)
        Me.Label_Value_FirstCreated.Name = "Label_Value_FirstCreated"
        Me.Label_Value_FirstCreated.Size = New System.Drawing.Size(120, 13)
        Me.Label_Value_FirstCreated.TabIndex = 1
        Me.Label_Value_FirstCreated.Text = "XX-XX-XXXX XX:XX:XX"
        '
        'Label_FirstCreated
        '
        Me.Label_FirstCreated.AutoSize = True
        Me.Label_FirstCreated.Location = New System.Drawing.Point(25, 13)
        Me.Label_FirstCreated.Name = "Label_FirstCreated"
        Me.Label_FirstCreated.Size = New System.Drawing.Size(69, 13)
        Me.Label_FirstCreated.TabIndex = 0
        Me.Label_FirstCreated.Text = "First Created:"
        '
        'Label_Value_OutFileName
        '
        Me.Label_Value_OutFileName.AutoSize = True
        Me.Label_Value_OutFileName.Location = New System.Drawing.Point(106, 202)
        Me.Label_Value_OutFileName.Name = "Label_Value_OutFileName"
        Me.Label_Value_OutFileName.Size = New System.Drawing.Size(81, 13)
        Me.Label_Value_OutFileName.TabIndex = 7
        Me.Label_Value_OutFileName.Text = "HC000000.SFX"
        '
        'Label_OutFileName
        '
        Me.Label_OutFileName.AutoSize = True
        Me.Label_OutFileName.Location = New System.Drawing.Point(6, 202)
        Me.Label_OutFileName.Name = "Label_OutFileName"
        Me.Label_OutFileName.Size = New System.Drawing.Size(87, 13)
        Me.Label_OutFileName.TabIndex = 6
        Me.Label_OutFileName.Text = "Output Filename:"
        '
        'ListBox_SFXs
        '
        Me.ListBox_SFXs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox_SFXs.FormattingEnabled = True
        Me.ListBox_SFXs.HorizontalScrollbar = True
        Me.ListBox_SFXs.Location = New System.Drawing.Point(6, 455)
        Me.ListBox_SFXs.Name = "ListBox_SFXs"
        Me.ListBox_SFXs.Size = New System.Drawing.Size(358, 160)
        Me.ListBox_SFXs.Sorted = True
        Me.ListBox_SFXs.TabIndex = 5
        '
        'Label_SfxCount
        '
        Me.Label_SfxCount.AutoSize = True
        Me.Label_SfxCount.Location = New System.Drawing.Point(6, 439)
        Me.Label_SfxCount.Name = "Label_SfxCount"
        Me.Label_SfxCount.Size = New System.Drawing.Size(44, 13)
        Me.Label_SfxCount.TabIndex = 4
        Me.Label_SfxCount.Text = "SFXs: 0"
        '
        'Label_DataBasesCount
        '
        Me.Label_DataBasesCount.AutoSize = True
        Me.Label_DataBasesCount.Location = New System.Drawing.Point(6, 312)
        Me.Label_DataBasesCount.Name = "Label_DataBasesCount"
        Me.Label_DataBasesCount.Size = New System.Drawing.Size(71, 13)
        Me.Label_DataBasesCount.TabIndex = 3
        Me.Label_DataBasesCount.Text = "DataBases: 0"
        '
        'Label_TotalSamples
        '
        Me.Label_TotalSamples.AutoSize = True
        Me.Label_TotalSamples.Location = New System.Drawing.Point(367, 10)
        Me.Label_TotalSamples.Name = "Label_TotalSamples"
        Me.Label_TotalSamples.Size = New System.Drawing.Size(59, 13)
        Me.Label_TotalSamples.TabIndex = 2
        Me.Label_TotalSamples.Text = "Samples: 0"
        '
        'ListBox_SamplesList
        '
        Me.ListBox_SamplesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_SamplesList.FormattingEnabled = True
        Me.ListBox_SamplesList.HorizontalScrollbar = True
        Me.ListBox_SamplesList.Location = New System.Drawing.Point(370, 26)
        Me.ListBox_SamplesList.Name = "ListBox_SamplesList"
        Me.ListBox_SamplesList.Size = New System.Drawing.Size(357, 589)
        Me.ListBox_SamplesList.Sorted = True
        Me.ListBox_SamplesList.TabIndex = 1
        '
        'ListBox_Databases
        '
        Me.ListBox_Databases.FormattingEnabled = True
        Me.ListBox_Databases.HorizontalScrollbar = True
        Me.ListBox_Databases.Location = New System.Drawing.Point(6, 328)
        Me.ListBox_Databases.Name = "ListBox_Databases"
        Me.ListBox_Databases.Size = New System.Drawing.Size(358, 108)
        Me.ListBox_Databases.Sorted = True
        Me.ListBox_Databases.TabIndex = 0
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(346, 650)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 1
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Soundbank_Properties
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 685)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.GroupBox_SoundbankData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Soundbank_Properties"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sound Bank Properties"
        Me.GroupBox_SoundbankData.ResumeLayout(False)
        Me.GroupBox_SoundbankData.PerformLayout()
        Me.GroupBox_FileSize.ResumeLayout(False)
        Me.GroupBox_FileSize.PerformLayout()
        Me.GroupBox_Recount.ResumeLayout(False)
        Me.GroupBox_Recount.PerformLayout()
        Me.GroupBox_FileInfo.ResumeLayout(False)
        Me.GroupBox_FileInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_SoundbankData As GroupBox
    Friend WithEvents ListBox_SFXs As ListBox
    Friend WithEvents Label_SfxCount As Label
    Friend WithEvents Label_DataBasesCount As Label
    Friend WithEvents Label_TotalSamples As Label
    Friend WithEvents ListBox_SamplesList As ListBox
    Friend WithEvents ListBox_Databases As ListBox
    Friend WithEvents Label_Value_OutFileName As Label
    Friend WithEvents Label_OutFileName As Label
    Friend WithEvents Button_OK As Button
    Friend WithEvents GroupBox_Recount As GroupBox
    Friend WithEvents Label_Value_Size As Label
    Friend WithEvents Label_Size As Label
    Friend WithEvents Label_SampleCount_Value As Label
    Friend WithEvents Label_SampleCount As Label
    Friend WithEvents Label_SfxCount_Value As Label
    Friend WithEvents Label_DatabaseCount_Value As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label_DataBaseCount As Label
    Friend WithEvents GroupBox_FileInfo As GroupBox
    Friend WithEvents Label_ModifiedBy_Value As Label
    Friend WithEvents Label_ModifiedBy As Label
    Friend WithEvents Label_CreatedBy_Value As Label
    Friend WithEvents Label_CreatedBy As Label
    Friend WithEvents Label_Value_LastModified As Label
    Friend WithEvents Label_LastModified As Label
    Friend WithEvents Label_Value_FirstCreated As Label
    Friend WithEvents Label_FirstCreated As Label
    Friend WithEvents GroupBox_FileSize As GroupBox
    Friend WithEvents Label_SizeFile_Xbox As Label
    Friend WithEvents Label_XBox As Label
    Friend WithEvents Label_SizeFile_PC As Label
    Friend WithEvents Label_Pc As Label
    Friend WithEvents Label_SizeFile_GameCube As Label
    Friend WithEvents Label_GameCube As Label
    Friend WithEvents Label_SizeFile_PlayStation2 As Label
    Friend WithEvents Label_PlayStation2 As Label
End Class

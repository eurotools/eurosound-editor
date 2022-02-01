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
        Me.GroupBox_Recount.SuspendLayout()
        Me.GroupBox_FileInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_SoundbankData
        '
        Me.GroupBox_SoundbankData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.GroupBox_SoundbankData.Size = New System.Drawing.Size(691, 585)
        Me.GroupBox_SoundbankData.TabIndex = 0
        Me.GroupBox_SoundbankData.TabStop = False
        Me.GroupBox_SoundbankData.Text = "Soundbank Name"
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
        Me.Label_Value_OutFileName.Location = New System.Drawing.Point(99, 202)
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
        Me.ListBox_SFXs.Location = New System.Drawing.Point(9, 429)
        Me.ListBox_SFXs.Name = "ListBox_SFXs"
        Me.ListBox_SFXs.Size = New System.Drawing.Size(358, 147)
        Me.ListBox_SFXs.TabIndex = 5
        '
        'Label_SfxCount
        '
        Me.Label_SfxCount.AutoSize = True
        Me.Label_SfxCount.Location = New System.Drawing.Point(6, 413)
        Me.Label_SfxCount.Name = "Label_SfxCount"
        Me.Label_SfxCount.Size = New System.Drawing.Size(44, 13)
        Me.Label_SfxCount.TabIndex = 4
        Me.Label_SfxCount.Text = "SFXs: 0"
        '
        'Label_DataBasesCount
        '
        Me.Label_DataBasesCount.AutoSize = True
        Me.Label_DataBasesCount.Location = New System.Drawing.Point(6, 286)
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
        Me.ListBox_SamplesList.Size = New System.Drawing.Size(315, 550)
        Me.ListBox_SamplesList.TabIndex = 1
        '
        'ListBox_Databases
        '
        Me.ListBox_Databases.FormattingEnabled = True
        Me.ListBox_Databases.HorizontalScrollbar = True
        Me.ListBox_Databases.Location = New System.Drawing.Point(6, 302)
        Me.ListBox_Databases.Name = "ListBox_Databases"
        Me.ListBox_Databases.Size = New System.Drawing.Size(358, 108)
        Me.ListBox_Databases.TabIndex = 0
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(346, 603)
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
        Me.ClientSize = New System.Drawing.Size(715, 638)
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
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SfxMultiEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SfxMultiEditor))
        Me.ListView_SfxFiles = New ListView_ColumnSortingClick.ListView_ColumnSortingClick()
        Me.Col_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Reverb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Tracking = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_InnerRadius = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_OuterRadius = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Max = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Steal = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Priority = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Alertness = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Ducker = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_DuckerLength = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_MasterVolume = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_UnderWater = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_StealLouder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Numeric_Reverb = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox_TrackingType = New System.Windows.Forms.ComboBox()
        Me.Numeric_InnerRadius = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_OuterRadius = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_MaxVoices = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox_Steal = New System.Windows.Forms.ComboBox()
        Me.Numeric_Priority = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_Alertness = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_Ducker = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_DuckerLength = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_MasterVolume = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox_UnderWater = New System.Windows.Forms.ComboBox()
        Me.ComboBox_StealLouder = New System.Windows.Forms.ComboBox()
        Me.GroupBox_LockedToAllFormats = New System.Windows.Forms.GroupBox()
        Me.CheckBox_ApplyToAllFormats = New System.Windows.Forms.CheckBox()
        Me.Button_Ok = New System.Windows.Forms.Button()
        CType(Me.Numeric_Reverb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_InnerRadius, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_OuterRadius, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_MaxVoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_Priority, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_Alertness, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_Ducker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_DuckerLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_MasterVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_LockedToAllFormats.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView_SfxFiles
        '
        Me.ListView_SfxFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_SfxFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Col_Name, Me.Col_Reverb, Me.Col_Tracking, Me.Col_InnerRadius, Me.Col_OuterRadius, Me.Col_Max, Me.Col_Steal, Me.Col_Priority, Me.Col_Alertness, Me.Col_Ducker, Me.Col_DuckerLength, Me.Col_MasterVolume, Me.Col_UnderWater, Me.Col_StealLouder})
        Me.ListView_SfxFiles.FullRowSelect = True
        Me.ListView_SfxFiles.GridLines = True
        Me.ListView_SfxFiles.HideSelection = False
        Me.ListView_SfxFiles.Location = New System.Drawing.Point(12, 12)
        Me.ListView_SfxFiles.Name = "ListView_SfxFiles"
        Me.ListView_SfxFiles.Size = New System.Drawing.Size(1248, 464)
        Me.ListView_SfxFiles.TabIndex = 0
        Me.ListView_SfxFiles.UseCompatibleStateImageBehavior = False
        Me.ListView_SfxFiles.View = System.Windows.Forms.View.Details
        '
        'Col_Name
        '
        Me.Col_Name.Text = "Name"
        Me.Col_Name.Width = 200
        '
        'Col_Reverb
        '
        Me.Col_Reverb.Text = "Reverb"
        Me.Col_Reverb.Width = 80
        '
        'Col_Tracking
        '
        Me.Col_Tracking.Text = "Tracking"
        Me.Col_Tracking.Width = 80
        '
        'Col_InnerRadius
        '
        Me.Col_InnerRadius.Text = "Inner Rad."
        Me.Col_InnerRadius.Width = 80
        '
        'Col_OuterRadius
        '
        Me.Col_OuterRadius.Text = "Outer Rad."
        Me.Col_OuterRadius.Width = 80
        '
        'Col_Max
        '
        Me.Col_Max.Text = "Max"
        Me.Col_Max.Width = 80
        '
        'Col_Steal
        '
        Me.Col_Steal.Text = "Steal?"
        Me.Col_Steal.Width = 80
        '
        'Col_Priority
        '
        Me.Col_Priority.Text = "Priority"
        Me.Col_Priority.Width = 80
        '
        'Col_Alertness
        '
        Me.Col_Alertness.Text = "Alertness"
        Me.Col_Alertness.Width = 80
        '
        'Col_Ducker
        '
        Me.Col_Ducker.Text = "Ducker"
        Me.Col_Ducker.Width = 80
        '
        'Col_DuckerLength
        '
        Me.Col_DuckerLength.Text = "Ducker Len"
        Me.Col_DuckerLength.Width = 80
        '
        'Col_MasterVolume
        '
        Me.Col_MasterVolume.Text = "Master Vol"
        Me.Col_MasterVolume.Width = 80
        '
        'Col_UnderWater
        '
        Me.Col_UnderWater.Text = "UnderWater"
        Me.Col_UnderWater.Width = 80
        '
        'Col_StealLouder
        '
        Me.Col_StealLouder.Text = "Steal Louder"
        Me.Col_StealLouder.Width = 80
        '
        'Numeric_Reverb
        '
        Me.Numeric_Reverb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_Reverb.Location = New System.Drawing.Point(206, 483)
        Me.Numeric_Reverb.Name = "Numeric_Reverb"
        Me.Numeric_Reverb.Size = New System.Drawing.Size(77, 20)
        Me.Numeric_Reverb.TabIndex = 1
        '
        'ComboBox_TrackingType
        '
        Me.ComboBox_TrackingType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_TrackingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_TrackingType.FormattingEnabled = True
        Me.ComboBox_TrackingType.Items.AddRange(New Object() {"2d", "Amb", "3d", "3d No:HRTF", "2d PL2"})
        Me.ComboBox_TrackingType.Location = New System.Drawing.Point(289, 482)
        Me.ComboBox_TrackingType.Name = "ComboBox_TrackingType"
        Me.ComboBox_TrackingType.Size = New System.Drawing.Size(84, 21)
        Me.ComboBox_TrackingType.TabIndex = 2
        '
        'Numeric_InnerRadius
        '
        Me.Numeric_InnerRadius.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_InnerRadius.Location = New System.Drawing.Point(379, 483)
        Me.Numeric_InnerRadius.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.Numeric_InnerRadius.Name = "Numeric_InnerRadius"
        Me.Numeric_InnerRadius.Size = New System.Drawing.Size(74, 20)
        Me.Numeric_InnerRadius.TabIndex = 3
        '
        'Numeric_OuterRadius
        '
        Me.Numeric_OuterRadius.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_OuterRadius.Location = New System.Drawing.Point(459, 483)
        Me.Numeric_OuterRadius.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.Numeric_OuterRadius.Name = "Numeric_OuterRadius"
        Me.Numeric_OuterRadius.Size = New System.Drawing.Size(77, 20)
        Me.Numeric_OuterRadius.TabIndex = 4
        '
        'Numeric_MaxVoices
        '
        Me.Numeric_MaxVoices.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_MaxVoices.Location = New System.Drawing.Point(542, 483)
        Me.Numeric_MaxVoices.Name = "Numeric_MaxVoices"
        Me.Numeric_MaxVoices.Size = New System.Drawing.Size(72, 20)
        Me.Numeric_MaxVoices.TabIndex = 5
        '
        'ComboBox_Steal
        '
        Me.ComboBox_Steal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Steal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Steal.FormattingEnabled = True
        Me.ComboBox_Steal.Items.AddRange(New Object() {"True", "False"})
        Me.ComboBox_Steal.Location = New System.Drawing.Point(620, 482)
        Me.ComboBox_Steal.Name = "ComboBox_Steal"
        Me.ComboBox_Steal.Size = New System.Drawing.Size(74, 21)
        Me.ComboBox_Steal.TabIndex = 6
        '
        'Numeric_Priority
        '
        Me.Numeric_Priority.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_Priority.Location = New System.Drawing.Point(700, 483)
        Me.Numeric_Priority.Name = "Numeric_Priority"
        Me.Numeric_Priority.Size = New System.Drawing.Size(73, 20)
        Me.Numeric_Priority.TabIndex = 7
        '
        'Numeric_Alertness
        '
        Me.Numeric_Alertness.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_Alertness.Location = New System.Drawing.Point(779, 483)
        Me.Numeric_Alertness.Name = "Numeric_Alertness"
        Me.Numeric_Alertness.Size = New System.Drawing.Size(73, 20)
        Me.Numeric_Alertness.TabIndex = 8
        '
        'Numeric_Ducker
        '
        Me.Numeric_Ducker.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_Ducker.Location = New System.Drawing.Point(858, 483)
        Me.Numeric_Ducker.Name = "Numeric_Ducker"
        Me.Numeric_Ducker.Size = New System.Drawing.Size(75, 20)
        Me.Numeric_Ducker.TabIndex = 9
        '
        'Numeric_DuckerLength
        '
        Me.Numeric_DuckerLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_DuckerLength.Location = New System.Drawing.Point(939, 483)
        Me.Numeric_DuckerLength.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.Numeric_DuckerLength.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.Numeric_DuckerLength.Name = "Numeric_DuckerLength"
        Me.Numeric_DuckerLength.Size = New System.Drawing.Size(75, 20)
        Me.Numeric_DuckerLength.TabIndex = 10
        '
        'Numeric_MasterVolume
        '
        Me.Numeric_MasterVolume.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Numeric_MasterVolume.Location = New System.Drawing.Point(1020, 483)
        Me.Numeric_MasterVolume.Name = "Numeric_MasterVolume"
        Me.Numeric_MasterVolume.Size = New System.Drawing.Size(75, 20)
        Me.Numeric_MasterVolume.TabIndex = 11
        '
        'ComboBox_UnderWater
        '
        Me.ComboBox_UnderWater.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_UnderWater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_UnderWater.FormattingEnabled = True
        Me.ComboBox_UnderWater.Items.AddRange(New Object() {"False", "True"})
        Me.ComboBox_UnderWater.Location = New System.Drawing.Point(1101, 482)
        Me.ComboBox_UnderWater.Name = "ComboBox_UnderWater"
        Me.ComboBox_UnderWater.Size = New System.Drawing.Size(74, 21)
        Me.ComboBox_UnderWater.TabIndex = 12
        '
        'ComboBox_StealLouder
        '
        Me.ComboBox_StealLouder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_StealLouder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_StealLouder.FormattingEnabled = True
        Me.ComboBox_StealLouder.Items.AddRange(New Object() {"False", "True"})
        Me.ComboBox_StealLouder.Location = New System.Drawing.Point(1181, 482)
        Me.ComboBox_StealLouder.Name = "ComboBox_StealLouder"
        Me.ComboBox_StealLouder.Size = New System.Drawing.Size(74, 21)
        Me.ComboBox_StealLouder.TabIndex = 13
        '
        'GroupBox_LockedToAllFormats
        '
        Me.GroupBox_LockedToAllFormats.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_LockedToAllFormats.Controls.Add(Me.CheckBox_ApplyToAllFormats)
        Me.GroupBox_LockedToAllFormats.Location = New System.Drawing.Point(206, 509)
        Me.GroupBox_LockedToAllFormats.Name = "GroupBox_LockedToAllFormats"
        Me.GroupBox_LockedToAllFormats.Size = New System.Drawing.Size(973, 45)
        Me.GroupBox_LockedToAllFormats.TabIndex = 14
        Me.GroupBox_LockedToAllFormats.TabStop = False
        Me.GroupBox_LockedToAllFormats.Text = "Locked To All Formats"
        '
        'CheckBox_ApplyToAllFormats
        '
        Me.CheckBox_ApplyToAllFormats.AutoSize = True
        Me.CheckBox_ApplyToAllFormats.Checked = True
        Me.CheckBox_ApplyToAllFormats.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_ApplyToAllFormats.Enabled = False
        Me.CheckBox_ApplyToAllFormats.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_ApplyToAllFormats.Name = "CheckBox_ApplyToAllFormats"
        Me.CheckBox_ApplyToAllFormats.Size = New System.Drawing.Size(122, 17)
        Me.CheckBox_ApplyToAllFormats.TabIndex = 0
        Me.CheckBox_ApplyToAllFormats.Text = "Apply To All Formats"
        Me.CheckBox_ApplyToAllFormats.UseVisualStyleBackColor = True
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.Location = New System.Drawing.Point(1185, 524)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 15
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'SfxMultiEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1272, 558)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.GroupBox_LockedToAllFormats)
        Me.Controls.Add(Me.ComboBox_StealLouder)
        Me.Controls.Add(Me.ComboBox_UnderWater)
        Me.Controls.Add(Me.Numeric_MasterVolume)
        Me.Controls.Add(Me.Numeric_DuckerLength)
        Me.Controls.Add(Me.Numeric_Ducker)
        Me.Controls.Add(Me.Numeric_Alertness)
        Me.Controls.Add(Me.Numeric_Priority)
        Me.Controls.Add(Me.ComboBox_Steal)
        Me.Controls.Add(Me.Numeric_MaxVoices)
        Me.Controls.Add(Me.Numeric_OuterRadius)
        Me.Controls.Add(Me.Numeric_InnerRadius)
        Me.Controls.Add(Me.ComboBox_TrackingType)
        Me.Controls.Add(Me.Numeric_Reverb)
        Me.Controls.Add(Me.ListView_SfxFiles)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SfxMultiEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Multi Editor"
        CType(Me.Numeric_Reverb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_InnerRadius, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_OuterRadius, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_MaxVoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_Priority, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_Alertness, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_Ducker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_DuckerLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_MasterVolume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_LockedToAllFormats.ResumeLayout(False)
        Me.GroupBox_LockedToAllFormats.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListView_SfxFiles As ListView_ColumnSortingClick.ListView_ColumnSortingClick
    Friend WithEvents Col_Name As ColumnHeader
    Friend WithEvents Col_Reverb As ColumnHeader
    Friend WithEvents Col_Tracking As ColumnHeader
    Friend WithEvents Col_InnerRadius As ColumnHeader
    Friend WithEvents Col_OuterRadius As ColumnHeader
    Friend WithEvents Col_Max As ColumnHeader
    Friend WithEvents Col_Steal As ColumnHeader
    Friend WithEvents Col_Priority As ColumnHeader
    Friend WithEvents Col_Alertness As ColumnHeader
    Friend WithEvents Col_Ducker As ColumnHeader
    Friend WithEvents Col_DuckerLength As ColumnHeader
    Friend WithEvents Col_MasterVolume As ColumnHeader
    Friend WithEvents Col_UnderWater As ColumnHeader
    Friend WithEvents Col_StealLouder As ColumnHeader
    Friend WithEvents Numeric_Reverb As NumericUpDown
    Friend WithEvents ComboBox_TrackingType As ComboBox
    Friend WithEvents Numeric_InnerRadius As NumericUpDown
    Friend WithEvents Numeric_OuterRadius As NumericUpDown
    Friend WithEvents Numeric_MaxVoices As NumericUpDown
    Friend WithEvents ComboBox_Steal As ComboBox
    Friend WithEvents Numeric_Priority As NumericUpDown
    Friend WithEvents Numeric_Alertness As NumericUpDown
    Friend WithEvents Numeric_Ducker As NumericUpDown
    Friend WithEvents Numeric_DuckerLength As NumericUpDown
    Friend WithEvents Numeric_MasterVolume As NumericUpDown
    Friend WithEvents ComboBox_UnderWater As ComboBox
    Friend WithEvents ComboBox_StealLouder As ComboBox
    Friend WithEvents GroupBox_LockedToAllFormats As GroupBox
    Friend WithEvents CheckBox_ApplyToAllFormats As CheckBox
    Friend WithEvents Button_Ok As Button
End Class

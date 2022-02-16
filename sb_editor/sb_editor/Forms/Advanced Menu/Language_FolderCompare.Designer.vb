<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Language_FolderCompare
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Language_FolderCompare))
        Me.GroupBox_PrimaryPath = New System.Windows.Forms.GroupBox()
        Me.Button_SetPrimaryFolder = New System.Windows.Forms.Button()
        Me.TextBox_PrimaryPath = New System.Windows.Forms.TextBox()
        Me.GroupBox_SecondaryPath = New System.Windows.Forms.GroupBox()
        Me.Button_SetSecondaryFolder = New System.Windows.Forms.Button()
        Me.TextBox_SecondaryPath = New System.Windows.Forms.TextBox()
        Me.Button_DoCompare = New System.Windows.Forms.Button()
        Me.GroupBox_SecondaryMissing = New System.Windows.Forms.GroupBox()
        Me.TextBox_MissingFilesSecondary = New System.Windows.Forms.TextBox()
        Me.Groupbox_AdditionSecondary = New System.Windows.Forms.GroupBox()
        Me.TextBox_AdditionFilesSecondary = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox_PrimaryPath.SuspendLayout()
        Me.GroupBox_SecondaryPath.SuspendLayout()
        Me.GroupBox_SecondaryMissing.SuspendLayout()
        Me.Groupbox_AdditionSecondary.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_PrimaryPath
        '
        Me.GroupBox_PrimaryPath.Controls.Add(Me.Button_SetPrimaryFolder)
        Me.GroupBox_PrimaryPath.Controls.Add(Me.TextBox_PrimaryPath)
        Me.GroupBox_PrimaryPath.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_PrimaryPath.Name = "GroupBox_PrimaryPath"
        Me.GroupBox_PrimaryPath.Size = New System.Drawing.Size(706, 51)
        Me.GroupBox_PrimaryPath.TabIndex = 0
        Me.GroupBox_PrimaryPath.TabStop = False
        Me.GroupBox_PrimaryPath.Text = "Primary Path"
        '
        'Button_SetPrimaryFolder
        '
        Me.Button_SetPrimaryFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SetPrimaryFolder.Location = New System.Drawing.Point(625, 16)
        Me.Button_SetPrimaryFolder.Name = "Button_SetPrimaryFolder"
        Me.Button_SetPrimaryFolder.Size = New System.Drawing.Size(75, 23)
        Me.Button_SetPrimaryFolder.TabIndex = 1
        Me.Button_SetPrimaryFolder.Text = "Set Folder"
        Me.Button_SetPrimaryFolder.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button_SetPrimaryFolder.UseVisualStyleBackColor = True
        '
        'TextBox_PrimaryPath
        '
        Me.TextBox_PrimaryPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_PrimaryPath.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_PrimaryPath.Location = New System.Drawing.Point(6, 19)
        Me.TextBox_PrimaryPath.Name = "TextBox_PrimaryPath"
        Me.TextBox_PrimaryPath.ReadOnly = True
        Me.TextBox_PrimaryPath.Size = New System.Drawing.Size(613, 20)
        Me.TextBox_PrimaryPath.TabIndex = 0
        '
        'GroupBox_SecondaryPath
        '
        Me.GroupBox_SecondaryPath.Controls.Add(Me.Button_SetSecondaryFolder)
        Me.GroupBox_SecondaryPath.Controls.Add(Me.TextBox_SecondaryPath)
        Me.GroupBox_SecondaryPath.Location = New System.Drawing.Point(12, 69)
        Me.GroupBox_SecondaryPath.Name = "GroupBox_SecondaryPath"
        Me.GroupBox_SecondaryPath.Size = New System.Drawing.Size(706, 51)
        Me.GroupBox_SecondaryPath.TabIndex = 1
        Me.GroupBox_SecondaryPath.TabStop = False
        Me.GroupBox_SecondaryPath.Text = "Secondary Path"
        '
        'Button_SetSecondaryFolder
        '
        Me.Button_SetSecondaryFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SetSecondaryFolder.Location = New System.Drawing.Point(625, 16)
        Me.Button_SetSecondaryFolder.Name = "Button_SetSecondaryFolder"
        Me.Button_SetSecondaryFolder.Size = New System.Drawing.Size(75, 23)
        Me.Button_SetSecondaryFolder.TabIndex = 3
        Me.Button_SetSecondaryFolder.Text = "Set Folder"
        Me.Button_SetSecondaryFolder.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button_SetSecondaryFolder.UseVisualStyleBackColor = True
        '
        'TextBox_SecondaryPath
        '
        Me.TextBox_SecondaryPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_SecondaryPath.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_SecondaryPath.Location = New System.Drawing.Point(6, 19)
        Me.TextBox_SecondaryPath.Name = "TextBox_SecondaryPath"
        Me.TextBox_SecondaryPath.ReadOnly = True
        Me.TextBox_SecondaryPath.Size = New System.Drawing.Size(613, 20)
        Me.TextBox_SecondaryPath.TabIndex = 2
        '
        'Button_DoCompare
        '
        Me.Button_DoCompare.Location = New System.Drawing.Point(12, 126)
        Me.Button_DoCompare.Name = "Button_DoCompare"
        Me.Button_DoCompare.Size = New System.Drawing.Size(85, 33)
        Me.Button_DoCompare.TabIndex = 2
        Me.Button_DoCompare.Text = "Do Compare"
        Me.Button_DoCompare.UseVisualStyleBackColor = True
        '
        'GroupBox_SecondaryMissing
        '
        Me.GroupBox_SecondaryMissing.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_SecondaryMissing.Controls.Add(Me.TextBox_MissingFilesSecondary)
        Me.GroupBox_SecondaryMissing.Location = New System.Drawing.Point(12, 165)
        Me.GroupBox_SecondaryMissing.Name = "GroupBox_SecondaryMissing"
        Me.GroupBox_SecondaryMissing.Size = New System.Drawing.Size(350, 378)
        Me.GroupBox_SecondaryMissing.TabIndex = 3
        Me.GroupBox_SecondaryMissing.TabStop = False
        Me.GroupBox_SecondaryMissing.Text = "Missing Files in Secondary Path"
        '
        'TextBox_MissingFilesSecondary
        '
        Me.TextBox_MissingFilesSecondary.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_MissingFilesSecondary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_MissingFilesSecondary.Location = New System.Drawing.Point(3, 16)
        Me.TextBox_MissingFilesSecondary.Multiline = True
        Me.TextBox_MissingFilesSecondary.Name = "TextBox_MissingFilesSecondary"
        Me.TextBox_MissingFilesSecondary.ReadOnly = True
        Me.TextBox_MissingFilesSecondary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox_MissingFilesSecondary.Size = New System.Drawing.Size(344, 359)
        Me.TextBox_MissingFilesSecondary.TabIndex = 1
        '
        'Groupbox_AdditionSecondary
        '
        Me.Groupbox_AdditionSecondary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Groupbox_AdditionSecondary.Controls.Add(Me.TextBox_AdditionFilesSecondary)
        Me.Groupbox_AdditionSecondary.Location = New System.Drawing.Point(368, 165)
        Me.Groupbox_AdditionSecondary.Name = "Groupbox_AdditionSecondary"
        Me.Groupbox_AdditionSecondary.Size = New System.Drawing.Size(350, 378)
        Me.Groupbox_AdditionSecondary.TabIndex = 4
        Me.Groupbox_AdditionSecondary.TabStop = False
        Me.Groupbox_AdditionSecondary.Text = "Addition Files in Secondary Path"
        '
        'TextBox_AdditionFilesSecondary
        '
        Me.TextBox_AdditionFilesSecondary.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_AdditionFilesSecondary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_AdditionFilesSecondary.Location = New System.Drawing.Point(3, 16)
        Me.TextBox_AdditionFilesSecondary.Multiline = True
        Me.TextBox_AdditionFilesSecondary.Name = "TextBox_AdditionFilesSecondary"
        Me.TextBox_AdditionFilesSecondary.ReadOnly = True
        Me.TextBox_AdditionFilesSecondary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox_AdditionFilesSecondary.Size = New System.Drawing.Size(344, 359)
        Me.TextBox_AdditionFilesSecondary.TabIndex = 0
        '
        'FolderBrowserDialog
        '
        Me.FolderBrowserDialog.Description = "Set Folder For Sample Files."
        '
        'Language_FolderCompare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 555)
        Me.Controls.Add(Me.Groupbox_AdditionSecondary)
        Me.Controls.Add(Me.GroupBox_SecondaryMissing)
        Me.Controls.Add(Me.Button_DoCompare)
        Me.Controls.Add(Me.GroupBox_SecondaryPath)
        Me.Controls.Add(Me.GroupBox_PrimaryPath)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Language_FolderCompare"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Language Folder Compare Utility"
        Me.GroupBox_PrimaryPath.ResumeLayout(False)
        Me.GroupBox_PrimaryPath.PerformLayout()
        Me.GroupBox_SecondaryPath.ResumeLayout(False)
        Me.GroupBox_SecondaryPath.PerformLayout()
        Me.GroupBox_SecondaryMissing.ResumeLayout(False)
        Me.GroupBox_SecondaryMissing.PerformLayout()
        Me.Groupbox_AdditionSecondary.ResumeLayout(False)
        Me.Groupbox_AdditionSecondary.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_PrimaryPath As GroupBox
    Friend WithEvents Button_SetPrimaryFolder As Button
    Friend WithEvents TextBox_PrimaryPath As TextBox
    Friend WithEvents GroupBox_SecondaryPath As GroupBox
    Friend WithEvents Button_SetSecondaryFolder As Button
    Friend WithEvents TextBox_SecondaryPath As TextBox
    Friend WithEvents Button_DoCompare As Button
    Friend WithEvents GroupBox_SecondaryMissing As GroupBox
    Friend WithEvents Groupbox_AdditionSecondary As GroupBox
    Friend WithEvents TextBox_MissingFilesSecondary As TextBox
    Friend WithEvents TextBox_AdditionFilesSecondary As TextBox
    Friend WithEvents FolderBrowserDialog As FolderBrowserDialog
End Class

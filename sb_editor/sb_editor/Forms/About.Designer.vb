<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Panel_ToolAbout = New System.Windows.Forms.Panel()
        Me.Label_Info = New System.Windows.Forms.Label()
        Me.Label_ToolTitle = New System.Windows.Forms.Label()
        Me.GroupBox_VersionInfo = New System.Windows.Forms.GroupBox()
        Me.Label_ToolVersion = New System.Windows.Forms.Label()
        Me.Label_EngineXVersion = New System.Windows.Forms.Label()
        Me.PictureBox_LeftIcon = New System.Windows.Forms.PictureBox()
        Me.PictureBox_RightIcon = New System.Windows.Forms.PictureBox()
        Me.Button_ReleaseInfo = New System.Windows.Forms.Button()
        Me.Panel_ToolAbout.SuspendLayout()
        Me.GroupBox_VersionInfo.SuspendLayout()
        CType(Me.PictureBox_LeftIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox_RightIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(369, 356)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 5
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Panel_ToolAbout
        '
        Me.Panel_ToolAbout.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_ToolAbout.BackColor = System.Drawing.SystemColors.Window
        Me.Panel_ToolAbout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_ToolAbout.Controls.Add(Me.Label_Info)
        Me.Panel_ToolAbout.Location = New System.Drawing.Point(12, 122)
        Me.Panel_ToolAbout.Name = "Panel_ToolAbout"
        Me.Panel_ToolAbout.Size = New System.Drawing.Size(432, 228)
        Me.Panel_ToolAbout.TabIndex = 2
        '
        'Label_Info
        '
        Me.Label_Info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Info.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Info.Location = New System.Drawing.Point(0, 0)
        Me.Label_Info.Name = "Label_Info"
        Me.Label_Info.Size = New System.Drawing.Size(428, 224)
        Me.Label_Info.TabIndex = 3
        Me.Label_Info.Text = "Tool Programmer:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Jordi Martínez (Jmarti856)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Original Tool Developers:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Euroco" &
    "m Developments 2002" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "GameCube dsp Tool:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Copyright (c) 2017 Alex Barney" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Spe" &
    "cial Thanks:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ismael Ferreras (Swyter)"
        Me.Label_Info.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label_ToolTitle
        '
        Me.Label_ToolTitle.AutoSize = True
        Me.Label_ToolTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_ToolTitle.Location = New System.Drawing.Point(72, 18)
        Me.Label_ToolTitle.Name = "Label_ToolTitle"
        Me.Label_ToolTitle.Size = New System.Drawing.Size(313, 25)
        Me.Label_ToolTitle.TabIndex = 0
        Me.Label_ToolTitle.Text = "Multi Format SFX Creation Tool"
        '
        'GroupBox_VersionInfo
        '
        Me.GroupBox_VersionInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_VersionInfo.Controls.Add(Me.Label_ToolVersion)
        Me.GroupBox_VersionInfo.Controls.Add(Me.Label_EngineXVersion)
        Me.GroupBox_VersionInfo.Location = New System.Drawing.Point(12, 59)
        Me.GroupBox_VersionInfo.Name = "GroupBox_VersionInfo"
        Me.GroupBox_VersionInfo.Size = New System.Drawing.Size(432, 57)
        Me.GroupBox_VersionInfo.TabIndex = 1
        Me.GroupBox_VersionInfo.TabStop = False
        Me.GroupBox_VersionInfo.Text = "Version:"
        '
        'Label_ToolVersion
        '
        Me.Label_ToolVersion.AutoSize = True
        Me.Label_ToolVersion.Location = New System.Drawing.Point(6, 16)
        Me.Label_ToolVersion.Name = "Label_ToolVersion"
        Me.Label_ToolVersion.Size = New System.Drawing.Size(63, 13)
        Me.Label_ToolVersion.TabIndex = 0
        Me.Label_ToolVersion.Text = "Version: 1.0"
        '
        'Label_EngineXVersion
        '
        Me.Label_EngineXVersion.AutoSize = True
        Me.Label_EngineXVersion.Location = New System.Drawing.Point(6, 36)
        Me.Label_EngineXVersion.Name = "Label_EngineXVersion"
        Me.Label_EngineXVersion.Size = New System.Drawing.Size(109, 13)
        Me.Label_EngineXVersion.TabIndex = 1
        Me.Label_EngineXVersion.Text = "EngineX Version: 182"
        '
        'PictureBox_LeftIcon
        '
        Me.PictureBox_LeftIcon.Image = CType(resources.GetObject("PictureBox_LeftIcon.Image"), System.Drawing.Image)
        Me.PictureBox_LeftIcon.Location = New System.Drawing.Point(12, 3)
        Me.PictureBox_LeftIcon.Name = "PictureBox_LeftIcon"
        Me.PictureBox_LeftIcon.Size = New System.Drawing.Size(54, 50)
        Me.PictureBox_LeftIcon.TabIndex = 10
        Me.PictureBox_LeftIcon.TabStop = False
        '
        'PictureBox_RightIcon
        '
        Me.PictureBox_RightIcon.Image = CType(resources.GetObject("PictureBox_RightIcon.Image"), System.Drawing.Image)
        Me.PictureBox_RightIcon.Location = New System.Drawing.Point(391, 3)
        Me.PictureBox_RightIcon.Name = "PictureBox_RightIcon"
        Me.PictureBox_RightIcon.Size = New System.Drawing.Size(54, 50)
        Me.PictureBox_RightIcon.TabIndex = 11
        Me.PictureBox_RightIcon.TabStop = False
        '
        'Button_ReleaseInfo
        '
        Me.Button_ReleaseInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_ReleaseInfo.Location = New System.Drawing.Point(12, 356)
        Me.Button_ReleaseInfo.Name = "Button_ReleaseInfo"
        Me.Button_ReleaseInfo.Size = New System.Drawing.Size(84, 23)
        Me.Button_ReleaseInfo.TabIndex = 4
        Me.Button_ReleaseInfo.Text = "Release Info..."
        Me.Button_ReleaseInfo.UseVisualStyleBackColor = True
        '
        'About
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 391)
        Me.Controls.Add(Me.Button_ReleaseInfo)
        Me.Controls.Add(Me.PictureBox_RightIcon)
        Me.Controls.Add(Me.PictureBox_LeftIcon)
        Me.Controls.Add(Me.GroupBox_VersionInfo)
        Me.Controls.Add(Me.Label_ToolTitle)
        Me.Controls.Add(Me.Panel_ToolAbout)
        Me.Controls.Add(Me.Button_OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "About"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About"
        Me.Panel_ToolAbout.ResumeLayout(False)
        Me.GroupBox_VersionInfo.ResumeLayout(False)
        Me.GroupBox_VersionInfo.PerformLayout()
        CType(Me.PictureBox_LeftIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox_RightIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_OK As Button
    Friend WithEvents Panel_ToolAbout As Panel
    Friend WithEvents Label_Info As Label
    Friend WithEvents Label_ToolTitle As Label
    Friend WithEvents GroupBox_VersionInfo As GroupBox
    Friend WithEvents Label_ToolVersion As Label
    Friend WithEvents Label_EngineXVersion As Label
    Friend WithEvents PictureBox_LeftIcon As PictureBox
    Friend WithEvents PictureBox_RightIcon As PictureBox
    Friend WithEvents Button_ReleaseInfo As Button
End Class

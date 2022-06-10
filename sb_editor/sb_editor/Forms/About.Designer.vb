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
        Me.Label_Title = New System.Windows.Forms.Label()
        Me.Label_CurrentVersion = New System.Windows.Forms.Label()
        Me.Label_LatestVersion = New System.Windows.Forms.Label()
        Me.Button_GetUpdate = New System.Windows.Forms.Button()
        Me.GroupBox_About = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label_Credits = New System.Windows.Forms.Label()
        Me.GroupBox_About.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(140, 309)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 6
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Label_Title
        '
        Me.Label_Title.AutoSize = True
        Me.Label_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Title.Location = New System.Drawing.Point(56, 16)
        Me.Label_Title.Name = "Label_Title"
        Me.Label_Title.Size = New System.Drawing.Size(232, 20)
        Me.Label_Title.TabIndex = 0
        Me.Label_Title.Text = "Multi Format SFX creation Tool."
        '
        'Label_CurrentVersion
        '
        Me.Label_CurrentVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_CurrentVersion.Location = New System.Drawing.Point(6, 55)
        Me.Label_CurrentVersion.Name = "Label_CurrentVersion"
        Me.Label_CurrentVersion.Size = New System.Drawing.Size(332, 20)
        Me.Label_CurrentVersion.TabIndex = 1
        Me.Label_CurrentVersion.Text = "This Version: Pending..."
        Me.Label_CurrentVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label_LatestVersion
        '
        Me.Label_LatestVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_LatestVersion.Location = New System.Drawing.Point(10, 75)
        Me.Label_LatestVersion.Name = "Label_LatestVersion"
        Me.Label_LatestVersion.Size = New System.Drawing.Size(328, 20)
        Me.Label_LatestVersion.TabIndex = 2
        Me.Label_LatestVersion.Text = "Latest Version Available:  Checking..."
        Me.Label_LatestVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Button_GetUpdate
        '
        Me.Button_GetUpdate.Enabled = False
        Me.Button_GetUpdate.Location = New System.Drawing.Point(135, 98)
        Me.Button_GetUpdate.Name = "Button_GetUpdate"
        Me.Button_GetUpdate.Size = New System.Drawing.Size(75, 23)
        Me.Button_GetUpdate.TabIndex = 3
        Me.Button_GetUpdate.Text = "Get Update"
        Me.Button_GetUpdate.UseVisualStyleBackColor = True
        '
        'GroupBox_About
        '
        Me.GroupBox_About.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_About.Controls.Add(Me.Panel1)
        Me.GroupBox_About.Controls.Add(Me.Button_OK)
        Me.GroupBox_About.Controls.Add(Me.Button_GetUpdate)
        Me.GroupBox_About.Controls.Add(Me.Label_Title)
        Me.GroupBox_About.Controls.Add(Me.Label_LatestVersion)
        Me.GroupBox_About.Controls.Add(Me.Label_CurrentVersion)
        Me.GroupBox_About.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_About.Name = "GroupBox_About"
        Me.GroupBox_About.Size = New System.Drawing.Size(355, 338)
        Me.GroupBox_About.TabIndex = 0
        Me.GroupBox_About.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label_Credits)
        Me.Panel1.Location = New System.Drawing.Point(6, 127)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(343, 176)
        Me.Panel1.TabIndex = 4
        '
        'Label_Credits
        '
        Me.Label_Credits.Location = New System.Drawing.Point(-2, 0)
        Me.Label_Credits.Name = "Label_Credits"
        Me.Label_Credits.Size = New System.Drawing.Size(343, 172)
        Me.Label_Credits.TabIndex = 5
        Me.Label_Credits.Text = "Programmer:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Jordi Martínez (Jmarti856)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Original Tool Developers:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Eurocom Dev" &
    "elopments 2002" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "GameCube dsp Tool:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Copyright (c) 2017 Alex Barney" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Special " &
    "Thanks:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ismael Ferreras (Swyter)"
        Me.Label_Credits.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'About
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(379, 362)
        Me.Controls.Add(Me.GroupBox_About)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "About"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "EuroSound Help"
        Me.GroupBox_About.ResumeLayout(False)
        Me.GroupBox_About.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_OK As Button
    Friend WithEvents Label_Title As Label
    Friend WithEvents Label_CurrentVersion As Label
    Friend WithEvents Label_LatestVersion As Label
    Friend WithEvents Button_GetUpdate As Button
    Friend WithEvents GroupBox_About As GroupBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label_Credits As Label
End Class

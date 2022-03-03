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
        Me.TextBox_Credits = New System.Windows.Forms.TextBox()
        Me.Label_Title = New System.Windows.Forms.Label()
        Me.Label_CurrentVersion = New System.Windows.Forms.Label()
        Me.Label_LatestVersion = New System.Windows.Forms.Label()
        Me.Button_GetUpdate = New System.Windows.Forms.Button()
        Me.GroupBox_About = New System.Windows.Forms.GroupBox()
        Me.GroupBox_About.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(147, 290)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 5
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'TextBox_Credits
        '
        Me.TextBox_Credits.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Credits.Location = New System.Drawing.Point(6, 140)
        Me.TextBox_Credits.Multiline = True
        Me.TextBox_Credits.Name = "TextBox_Credits"
        Me.TextBox_Credits.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox_Credits.Size = New System.Drawing.Size(332, 144)
        Me.TextBox_Credits.TabIndex = 6
        Me.TextBox_Credits.Text = resources.GetString("TextBox_Credits.Text")
        Me.TextBox_Credits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label_Title
        '
        Me.Label_Title.AutoSize = True
        Me.Label_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Title.Location = New System.Drawing.Point(68, 16)
        Me.Label_Title.Name = "Label_Title"
        Me.Label_Title.Size = New System.Drawing.Size(232, 20)
        Me.Label_Title.TabIndex = 7
        Me.Label_Title.Text = "Multi Format SFX creation Tool."
        '
        'Label_CurrentVersion
        '
        Me.Label_CurrentVersion.AutoSize = True
        Me.Label_CurrentVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_CurrentVersion.Location = New System.Drawing.Point(117, 55)
        Me.Label_CurrentVersion.Name = "Label_CurrentVersion"
        Me.Label_CurrentVersion.Size = New System.Drawing.Size(135, 20)
        Me.Label_CurrentVersion.TabIndex = 8
        Me.Label_CurrentVersion.Text = "This Version: 3.57"
        '
        'Label_LatestVersion
        '
        Me.Label_LatestVersion.AutoSize = True
        Me.Label_LatestVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_LatestVersion.Location = New System.Drawing.Point(75, 75)
        Me.Label_LatestVersion.Name = "Label_LatestVersion"
        Me.Label_LatestVersion.Size = New System.Drawing.Size(218, 20)
        Me.Label_LatestVersion.TabIndex = 9
        Me.Label_LatestVersion.Text = "Latest Version Available: 4.00"
        '
        'Button_GetUpdate
        '
        Me.Button_GetUpdate.Location = New System.Drawing.Point(147, 98)
        Me.Button_GetUpdate.Name = "Button_GetUpdate"
        Me.Button_GetUpdate.Size = New System.Drawing.Size(75, 23)
        Me.Button_GetUpdate.TabIndex = 10
        Me.Button_GetUpdate.Text = "Get Update"
        Me.Button_GetUpdate.UseVisualStyleBackColor = True
        '
        'GroupBox_About
        '
        Me.GroupBox_About.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_About.Controls.Add(Me.TextBox_Credits)
        Me.GroupBox_About.Controls.Add(Me.Button_OK)
        Me.GroupBox_About.Controls.Add(Me.Button_GetUpdate)
        Me.GroupBox_About.Controls.Add(Me.Label_Title)
        Me.GroupBox_About.Controls.Add(Me.Label_LatestVersion)
        Me.GroupBox_About.Controls.Add(Me.Label_CurrentVersion)
        Me.GroupBox_About.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_About.Name = "GroupBox_About"
        Me.GroupBox_About.Size = New System.Drawing.Size(344, 319)
        Me.GroupBox_About.TabIndex = 11
        Me.GroupBox_About.TabStop = False
        '
        'About
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 343)
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_OK As Button
    Friend WithEvents TextBox_Credits As TextBox
    Friend WithEvents Label_Title As Label
    Friend WithEvents Label_CurrentVersion As Label
    Friend WithEvents Label_LatestVersion As Label
    Friend WithEvents Button_GetUpdate As Button
    Friend WithEvents GroupBox_About As GroupBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlobalVariablesWatcher
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GlobalVariablesWatcher))
        Me.TextBox_Globals = New System.Windows.Forms.TextBox()
        Me.TextBox_ProjectSettings = New System.Windows.Forms.TextBox()
        Me.TextBox_SystemFiles = New System.Windows.Forms.TextBox()
        Me.TextBox_HashCodes = New System.Windows.Forms.TextBox()
        Me.Button_Update = New System.Windows.Forms.Button()
        Me.Button_Close = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGlobals = New System.Windows.Forms.TabPage()
        Me.TabPageSystemFiles = New System.Windows.Forms.TabPage()
        Me.TabPageProjectSettings = New System.Windows.Forms.TabPage()
        Me.TabPageHashcodes = New System.Windows.Forms.TabPage()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGlobals.SuspendLayout()
        Me.TabPageSystemFiles.SuspendLayout()
        Me.TabPageProjectSettings.SuspendLayout()
        Me.TabPageHashcodes.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox_Globals
        '
        Me.TextBox_Globals.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_Globals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Globals.Location = New System.Drawing.Point(3, 3)
        Me.TextBox_Globals.Multiline = True
        Me.TextBox_Globals.Name = "TextBox_Globals"
        Me.TextBox_Globals.ReadOnly = True
        Me.TextBox_Globals.Size = New System.Drawing.Size(582, 437)
        Me.TextBox_Globals.TabIndex = 0
        '
        'TextBox_ProjectSettings
        '
        Me.TextBox_ProjectSettings.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_ProjectSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_ProjectSettings.Location = New System.Drawing.Point(3, 3)
        Me.TextBox_ProjectSettings.Multiline = True
        Me.TextBox_ProjectSettings.Name = "TextBox_ProjectSettings"
        Me.TextBox_ProjectSettings.ReadOnly = True
        Me.TextBox_ProjectSettings.Size = New System.Drawing.Size(582, 437)
        Me.TextBox_ProjectSettings.TabIndex = 1
        '
        'TextBox_SystemFiles
        '
        Me.TextBox_SystemFiles.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_SystemFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_SystemFiles.Location = New System.Drawing.Point(3, 3)
        Me.TextBox_SystemFiles.Multiline = True
        Me.TextBox_SystemFiles.Name = "TextBox_SystemFiles"
        Me.TextBox_SystemFiles.ReadOnly = True
        Me.TextBox_SystemFiles.Size = New System.Drawing.Size(582, 437)
        Me.TextBox_SystemFiles.TabIndex = 0
        '
        'TextBox_HashCodes
        '
        Me.TextBox_HashCodes.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_HashCodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_HashCodes.Location = New System.Drawing.Point(3, 3)
        Me.TextBox_HashCodes.Multiline = True
        Me.TextBox_HashCodes.Name = "TextBox_HashCodes"
        Me.TextBox_HashCodes.ReadOnly = True
        Me.TextBox_HashCodes.Size = New System.Drawing.Size(582, 437)
        Me.TextBox_HashCodes.TabIndex = 1
        '
        'Button_Update
        '
        Me.Button_Update.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Update.Location = New System.Drawing.Point(452, 487)
        Me.Button_Update.Name = "Button_Update"
        Me.Button_Update.Size = New System.Drawing.Size(75, 23)
        Me.Button_Update.TabIndex = 4
        Me.Button_Update.Text = "Update"
        Me.Button_Update.UseVisualStyleBackColor = True
        '
        'Button_Close
        '
        Me.Button_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Close.Location = New System.Drawing.Point(533, 487)
        Me.Button_Close.Name = "Button_Close"
        Me.Button_Close.Size = New System.Drawing.Size(75, 23)
        Me.Button_Close.TabIndex = 5
        Me.Button_Close.Text = "Close"
        Me.Button_Close.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGlobals)
        Me.TabControl1.Controls.Add(Me.TabPageSystemFiles)
        Me.TabControl1.Controls.Add(Me.TabPageProjectSettings)
        Me.TabControl1.Controls.Add(Me.TabPageHashcodes)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(596, 469)
        Me.TabControl1.TabIndex = 6
        '
        'TabPageGlobals
        '
        Me.TabPageGlobals.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageGlobals.Controls.Add(Me.TextBox_Globals)
        Me.TabPageGlobals.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGlobals.Name = "TabPageGlobals"
        Me.TabPageGlobals.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGlobals.Size = New System.Drawing.Size(588, 443)
        Me.TabPageGlobals.TabIndex = 0
        Me.TabPageGlobals.Text = "Globals"
        '
        'TabPageSystemFiles
        '
        Me.TabPageSystemFiles.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageSystemFiles.Controls.Add(Me.TextBox_SystemFiles)
        Me.TabPageSystemFiles.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSystemFiles.Name = "TabPageSystemFiles"
        Me.TabPageSystemFiles.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSystemFiles.Size = New System.Drawing.Size(588, 443)
        Me.TabPageSystemFiles.TabIndex = 1
        Me.TabPageSystemFiles.Text = "System Files"
        '
        'TabPageProjectSettings
        '
        Me.TabPageProjectSettings.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageProjectSettings.Controls.Add(Me.TextBox_ProjectSettings)
        Me.TabPageProjectSettings.Location = New System.Drawing.Point(4, 22)
        Me.TabPageProjectSettings.Name = "TabPageProjectSettings"
        Me.TabPageProjectSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageProjectSettings.Size = New System.Drawing.Size(588, 443)
        Me.TabPageProjectSettings.TabIndex = 2
        Me.TabPageProjectSettings.Text = "Project Settings"
        '
        'TabPageHashcodes
        '
        Me.TabPageHashcodes.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageHashcodes.Controls.Add(Me.TextBox_HashCodes)
        Me.TabPageHashcodes.Location = New System.Drawing.Point(4, 22)
        Me.TabPageHashcodes.Name = "TabPageHashcodes"
        Me.TabPageHashcodes.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageHashcodes.Size = New System.Drawing.Size(588, 443)
        Me.TabPageHashcodes.TabIndex = 3
        Me.TabPageHashcodes.Text = "HashCodes"
        '
        'GlobalVariablesWatcher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 522)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button_Close)
        Me.Controls.Add(Me.Button_Update)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GlobalVariablesWatcher"
        Me.Text = "GlobalVariablesWatcher"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGlobals.ResumeLayout(False)
        Me.TabPageGlobals.PerformLayout()
        Me.TabPageSystemFiles.ResumeLayout(False)
        Me.TabPageSystemFiles.PerformLayout()
        Me.TabPageProjectSettings.ResumeLayout(False)
        Me.TabPageProjectSettings.PerformLayout()
        Me.TabPageHashcodes.ResumeLayout(False)
        Me.TabPageHashcodes.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextBox_Globals As TextBox
    Private WithEvents TextBox_ProjectSettings As TextBox
    Private WithEvents TextBox_SystemFiles As TextBox
    Private WithEvents TextBox_HashCodes As TextBox
    Friend WithEvents Button_Update As Button
    Friend WithEvents Button_Close As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageGlobals As TabPage
    Friend WithEvents TabPageSystemFiles As TabPage
    Friend WithEvents TabPageProjectSettings As TabPage
    Friend WithEvents TabPageHashcodes As TabPage
End Class

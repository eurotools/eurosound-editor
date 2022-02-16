<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdvancedMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdvancedMenu))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button_LanguageFolder = New System.Windows.Forms.Button()
        Me.Button_CheckForDuplicateHashCodes = New System.Windows.Forms.Button()
        Me.Button_ReAllocateHashcodes = New System.Windows.Forms.Button()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Button_LanguageFolder)
        Me.GroupBox1.Controls.Add(Me.Button_CheckForDuplicateHashCodes)
        Me.GroupBox1.Controls.Add(Me.Button_ReAllocateHashcodes)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 260)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Advanced Options"
        '
        'Button_LanguageFolder
        '
        Me.Button_LanguageFolder.Location = New System.Drawing.Point(6, 77)
        Me.Button_LanguageFolder.Name = "Button_LanguageFolder"
        Me.Button_LanguageFolder.Size = New System.Drawing.Size(220, 23)
        Me.Button_LanguageFolder.TabIndex = 2
        Me.Button_LanguageFolder.Text = "Language Folder Compare"
        Me.Button_LanguageFolder.UseVisualStyleBackColor = True
        '
        'Button_CheckForDuplicateHashCodes
        '
        Me.Button_CheckForDuplicateHashCodes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_CheckForDuplicateHashCodes.Location = New System.Drawing.Point(6, 19)
        Me.Button_CheckForDuplicateHashCodes.Name = "Button_CheckForDuplicateHashCodes"
        Me.Button_CheckForDuplicateHashCodes.Size = New System.Drawing.Size(220, 23)
        Me.Button_CheckForDuplicateHashCodes.TabIndex = 0
        Me.Button_CheckForDuplicateHashCodes.Text = "Check SFX HashCodes"
        Me.Button_CheckForDuplicateHashCodes.UseVisualStyleBackColor = True
        '
        'Button_ReAllocateHashcodes
        '
        Me.Button_ReAllocateHashcodes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ReAllocateHashcodes.Location = New System.Drawing.Point(6, 48)
        Me.Button_ReAllocateHashcodes.Name = "Button_ReAllocateHashcodes"
        Me.Button_ReAllocateHashcodes.Size = New System.Drawing.Size(220, 23)
        Me.Button_ReAllocateHashcodes.TabIndex = 1
        Me.Button_ReAllocateHashcodes.Text = "Re-Allocates HashCodes"
        Me.Button_ReAllocateHashcodes.UseVisualStyleBackColor = True
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.Location = New System.Drawing.Point(92, 278)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 1
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'AdvancedMenu
        '
        Me.AcceptButton = Me.Button_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(256, 313)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AdvancedMenu"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Advanced"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button_CheckForDuplicateHashCodes As Button
    Friend WithEvents Button_ReAllocateHashcodes As Button
    Friend WithEvents Button_Ok As Button
    Friend WithEvents Button_LanguageFolder As Button
End Class

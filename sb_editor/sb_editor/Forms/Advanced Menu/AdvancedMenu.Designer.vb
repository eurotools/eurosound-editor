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
        Me.Button_ValidateSfxLinks = New System.Windows.Forms.Button()
        Me.Button_StealOnLouder = New System.Windows.Forms.Button()
        Me.Button_ValidateInterSample = New System.Windows.Forms.Button()
        Me.Button_ValidateSfx = New System.Windows.Forms.Button()
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
        Me.GroupBox1.Controls.Add(Me.Button_ValidateSfxLinks)
        Me.GroupBox1.Controls.Add(Me.Button_StealOnLouder)
        Me.GroupBox1.Controls.Add(Me.Button_ValidateInterSample)
        Me.GroupBox1.Controls.Add(Me.Button_ValidateSfx)
        Me.GroupBox1.Controls.Add(Me.Button_LanguageFolder)
        Me.GroupBox1.Controls.Add(Me.Button_CheckForDuplicateHashCodes)
        Me.GroupBox1.Controls.Add(Me.Button_ReAllocateHashcodes)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 225)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Advanced Options"
        '
        'Button_ValidateSfxLinks
        '
        Me.Button_ValidateSfxLinks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ValidateSfxLinks.Location = New System.Drawing.Point(6, 164)
        Me.Button_ValidateSfxLinks.Name = "Button_ValidateSfxLinks"
        Me.Button_ValidateSfxLinks.Size = New System.Drawing.Size(220, 23)
        Me.Button_ValidateSfxLinks.TabIndex = 5
        Me.Button_ValidateSfxLinks.Text = "Validate Sub SFS links"
        Me.Button_ValidateSfxLinks.UseVisualStyleBackColor = True
        '
        'Button_StealOnLouder
        '
        Me.Button_StealOnLouder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_StealOnLouder.Location = New System.Drawing.Point(6, 135)
        Me.Button_StealOnLouder.Name = "Button_StealOnLouder"
        Me.Button_StealOnLouder.Size = New System.Drawing.Size(220, 23)
        Me.Button_StealOnLouder.TabIndex = 4
        Me.Button_StealOnLouder.Text = "Steal On Louder Check"
        Me.Button_StealOnLouder.UseVisualStyleBackColor = True
        '
        'Button_ValidateInterSample
        '
        Me.Button_ValidateInterSample.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ValidateInterSample.Location = New System.Drawing.Point(6, 77)
        Me.Button_ValidateInterSample.Name = "Button_ValidateInterSample"
        Me.Button_ValidateInterSample.Size = New System.Drawing.Size(220, 23)
        Me.Button_ValidateInterSample.TabIndex = 2
        Me.Button_ValidateInterSample.Text = "Validate Inter-Sample Delay"
        Me.Button_ValidateInterSample.UseVisualStyleBackColor = True
        '
        'Button_ValidateSfx
        '
        Me.Button_ValidateSfx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ValidateSfx.Location = New System.Drawing.Point(6, 193)
        Me.Button_ValidateSfx.Name = "Button_ValidateSfx"
        Me.Button_ValidateSfx.Size = New System.Drawing.Size(220, 23)
        Me.Button_ValidateSfx.TabIndex = 6
        Me.Button_ValidateSfx.Text = "Validate Platform SFX Versions"
        Me.Button_ValidateSfx.UseVisualStyleBackColor = True
        '
        'Button_LanguageFolder
        '
        Me.Button_LanguageFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_LanguageFolder.Location = New System.Drawing.Point(6, 106)
        Me.Button_LanguageFolder.Name = "Button_LanguageFolder"
        Me.Button_LanguageFolder.Size = New System.Drawing.Size(220, 23)
        Me.Button_LanguageFolder.TabIndex = 3
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
        Me.Button_Ok.Location = New System.Drawing.Point(91, 243)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 33)
        Me.Button_Ok.TabIndex = 1
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'AdvancedMenu
        '
        Me.AcceptButton = Me.Button_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(256, 288)
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
    Friend WithEvents Button_ValidateSfx As Button
    Friend WithEvents Button_ValidateSfxLinks As Button
    Friend WithEvents Button_StealOnLouder As Button
    Friend WithEvents Button_ValidateInterSample As Button
End Class

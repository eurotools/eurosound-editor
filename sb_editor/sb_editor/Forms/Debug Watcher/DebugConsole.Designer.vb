<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DebugConsole
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DebugConsole))
        Me.Textbox_DebugConsole = New System.Windows.Forms.TextBox()
        Me.Button_Close = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Textbox_DebugConsole
        '
        Me.Textbox_DebugConsole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Textbox_DebugConsole.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_DebugConsole.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Textbox_DebugConsole.Location = New System.Drawing.Point(12, 12)
        Me.Textbox_DebugConsole.Multiline = True
        Me.Textbox_DebugConsole.Name = "Textbox_DebugConsole"
        Me.Textbox_DebugConsole.ReadOnly = True
        Me.Textbox_DebugConsole.Size = New System.Drawing.Size(584, 418)
        Me.Textbox_DebugConsole.TabIndex = 0
        '
        'Button_Close
        '
        Me.Button_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Close.Location = New System.Drawing.Point(521, 436)
        Me.Button_Close.Name = "Button_Close"
        Me.Button_Close.Size = New System.Drawing.Size(75, 23)
        Me.Button_Close.TabIndex = 1
        Me.Button_Close.Text = "Close"
        Me.Button_Close.UseVisualStyleBackColor = True
        '
        'DebugConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 471)
        Me.Controls.Add(Me.Button_Close)
        Me.Controls.Add(Me.Textbox_DebugConsole)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DebugConsole"
        Me.Text = "EuroSound Debug Console"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_Close As Button
    Protected Friend WithEvents Textbox_DebugConsole As TextBox
End Class

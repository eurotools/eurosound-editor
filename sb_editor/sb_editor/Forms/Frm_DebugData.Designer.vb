<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DebugData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_DebugData))
        Me.TextBox_DebugData = New System.Windows.Forms.TextBox()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBox_DebugData
        '
        Me.TextBox_DebugData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_DebugData.Location = New System.Drawing.Point(12, 12)
        Me.TextBox_DebugData.Multiline = True
        Me.TextBox_DebugData.Name = "TextBox_DebugData"
        Me.TextBox_DebugData.Size = New System.Drawing.Size(411, 368)
        Me.TextBox_DebugData.TabIndex = 0
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.Location = New System.Drawing.Point(348, 386)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 1
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'Frm_DebugData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 421)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.TextBox_DebugData)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Frm_DebugData"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Debug Data"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox_DebugData As TextBox
    Friend WithEvents Button_Ok As Button
End Class

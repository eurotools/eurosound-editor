<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SfxDefault
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SfxDefault))
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.SfxParamsAndSamplePool1 = New sb_editor.SfxControl()
        Me.SuspendLayout()
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(743, 307)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 2
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(824, 308)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 3
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'SfxParamsAndSamplePool1
        '
        Me.SfxParamsAndSamplePool1.Location = New System.Drawing.Point(12, 12)
        Me.SfxParamsAndSamplePool1.Name = "SfxParamsAndSamplePool1"
        Me.SfxParamsAndSamplePool1.ShowSampleProperties = True
        Me.SfxParamsAndSamplePool1.Size = New System.Drawing.Size(891, 289)
        Me.SfxParamsAndSamplePool1.TabIndex = 4
        '
        'SfxDefault
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(911, 343)
        Me.Controls.Add(Me.SfxParamsAndSamplePool1)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SfxDefault"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SFX Default Setting"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents Button_OK As Button
    Friend WithEvents SfxParamsAndSamplePool1 As SfxControl
End Class

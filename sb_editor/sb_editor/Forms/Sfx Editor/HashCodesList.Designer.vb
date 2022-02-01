<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HashCodesList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HashCodesList))
        Me.UserControl_SFXs1 = New sb_editor.UserControl_SFXs()
        Me.SuspendLayout()
        '
        'UserControl_SFXs1
        '
        Me.UserControl_SFXs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_SFXs1.Location = New System.Drawing.Point(0, 0)
        Me.UserControl_SFXs1.Name = "UserControl_SFXs1"
        Me.UserControl_SFXs1.Size = New System.Drawing.Size(289, 578)
        Me.UserControl_SFXs1.TabIndex = 0
        '
        'HashCodesList
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(289, 578)
        Me.Controls.Add(Me.UserControl_SFXs1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HashCodesList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Available SFXs"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UserControl_SFXs1 As UserControl_SFXs
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MissingSamples
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MissingSamples))
        Me.GroupBox_MissingSamples = New System.Windows.Forms.GroupBox()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.ListBox_MissingSamples = New System.Windows.Forms.ListBox()
        Me.GroupBox_MissingSamples.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_MissingSamples
        '
        Me.GroupBox_MissingSamples.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_MissingSamples.Controls.Add(Me.Button_Ok)
        Me.GroupBox_MissingSamples.Controls.Add(Me.ListBox_MissingSamples)
        Me.GroupBox_MissingSamples.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_MissingSamples.Name = "GroupBox_MissingSamples"
        Me.GroupBox_MissingSamples.Size = New System.Drawing.Size(404, 495)
        Me.GroupBox_MissingSamples.TabIndex = 0
        Me.GroupBox_MissingSamples.TabStop = False
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.Location = New System.Drawing.Point(323, 466)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 1
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'ListBox_MissingSamples
        '
        Me.ListBox_MissingSamples.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_MissingSamples.FormattingEnabled = True
        Me.ListBox_MissingSamples.HorizontalScrollbar = True
        Me.ListBox_MissingSamples.Location = New System.Drawing.Point(6, 13)
        Me.ListBox_MissingSamples.Name = "ListBox_MissingSamples"
        Me.ListBox_MissingSamples.Size = New System.Drawing.Size(392, 446)
        Me.ListBox_MissingSamples.TabIndex = 0
        '
        'MissingSamples
        '
        Me.AcceptButton = Me.Button_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 519)
        Me.Controls.Add(Me.GroupBox_MissingSamples)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MissingSamples"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Missing Samples Found"
        Me.GroupBox_MissingSamples.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_MissingSamples As GroupBox
    Friend WithEvents Button_Ok As Button
    Friend WithEvents ListBox_MissingSamples As ListBox
End Class

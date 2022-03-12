<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewSamples
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewSamples))
        Me.GroupBox_MissingSamples = New System.Windows.Forms.GroupBox()
        Me.ComboBox_AvailableRates = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.ListBox_NewSamples = New System.Windows.Forms.ListBox()
        Me.GroupBox_MissingSamples.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_MissingSamples
        '
        Me.GroupBox_MissingSamples.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_MissingSamples.Controls.Add(Me.ComboBox_AvailableRates)
        Me.GroupBox_MissingSamples.Controls.Add(Me.Label1)
        Me.GroupBox_MissingSamples.Controls.Add(Me.Button_Ok)
        Me.GroupBox_MissingSamples.Controls.Add(Me.ListBox_NewSamples)
        Me.GroupBox_MissingSamples.Location = New System.Drawing.Point(0, 12)
        Me.GroupBox_MissingSamples.Name = "GroupBox_MissingSamples"
        Me.GroupBox_MissingSamples.Size = New System.Drawing.Size(484, 495)
        Me.GroupBox_MissingSamples.TabIndex = 1
        Me.GroupBox_MissingSamples.TabStop = False
        '
        'ComboBox_AvailableRates
        '
        Me.ComboBox_AvailableRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_AvailableRates.FormattingEnabled = True
        Me.ComboBox_AvailableRates.Location = New System.Drawing.Point(202, 468)
        Me.ComboBox_AvailableRates.Name = "ComboBox_AvailableRates"
        Me.ComboBox_AvailableRates.Size = New System.Drawing.Size(160, 21)
        Me.ComboBox_AvailableRates.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 471)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(190, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Set Re-Sample Rate for New Samples:"
        '
        'Button_Ok
        '
        Me.Button_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Ok.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_Ok.Location = New System.Drawing.Point(397, 466)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 1
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'ListBox_NewSamples
        '
        Me.ListBox_NewSamples.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_NewSamples.FormattingEnabled = True
        Me.ListBox_NewSamples.HorizontalScrollbar = True
        Me.ListBox_NewSamples.Location = New System.Drawing.Point(6, 13)
        Me.ListBox_NewSamples.Name = "ListBox_NewSamples"
        Me.ListBox_NewSamples.Size = New System.Drawing.Size(472, 446)
        Me.ListBox_NewSamples.Sorted = True
        Me.ListBox_NewSamples.TabIndex = 0
        '
        'NewSamples
        '
        Me.AcceptButton = Me.Button_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 519)
        Me.Controls.Add(Me.GroupBox_MissingSamples)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NewSamples"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New Samples Found"
        Me.GroupBox_MissingSamples.ResumeLayout(False)
        Me.GroupBox_MissingSamples.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_MissingSamples As GroupBox
    Friend WithEvents Button_Ok As Button
    Friend WithEvents ListBox_NewSamples As ListBox
    Friend WithEvents Label1 As Label
    Protected Friend WithEvents ComboBox_AvailableRates As ComboBox
End Class

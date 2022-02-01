<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetMaxBankSize
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetMaxBankSize))
        Me.Label_BankName = New System.Windows.Forms.Label()
        Me.TextBox_BankName = New System.Windows.Forms.TextBox()
        Me.Label_PlayStationSize = New System.Windows.Forms.Label()
        Me.Label_PcSize = New System.Windows.Forms.Label()
        Me.Numeric_PlayStationSize = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_PcSize = New System.Windows.Forms.NumericUpDown()
        Me.Label_GameCubeSize = New System.Windows.Forms.Label()
        Me.Numeric_GameCubeSize = New System.Windows.Forms.NumericUpDown()
        Me.Label_XboxSize = New System.Windows.Forms.Label()
        Me.Numeric_XboxSize = New System.Windows.Forms.NumericUpDown()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        CType(Me.Numeric_PlayStationSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_PcSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_GameCubeSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_XboxSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_BankName
        '
        Me.Label_BankName.AutoSize = True
        Me.Label_BankName.Location = New System.Drawing.Point(12, 15)
        Me.Label_BankName.Name = "Label_BankName"
        Me.Label_BankName.Size = New System.Drawing.Size(66, 13)
        Me.Label_BankName.TabIndex = 0
        Me.Label_BankName.Text = "Bank Name:"
        '
        'TextBox_BankName
        '
        Me.TextBox_BankName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_BankName.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_BankName.Location = New System.Drawing.Point(84, 12)
        Me.TextBox_BankName.Name = "TextBox_BankName"
        Me.TextBox_BankName.ReadOnly = True
        Me.TextBox_BankName.Size = New System.Drawing.Size(224, 20)
        Me.TextBox_BankName.TabIndex = 1
        '
        'Label_PlayStationSize
        '
        Me.Label_PlayStationSize.AutoSize = True
        Me.Label_PlayStationSize.Location = New System.Drawing.Point(12, 53)
        Me.Label_PlayStationSize.Name = "Label_PlayStationSize"
        Me.Label_PlayStationSize.Size = New System.Drawing.Size(160, 13)
        Me.Label_PlayStationSize.TabIndex = 2
        Me.Label_PlayStationSize.Text = "SoundBank Max on PlayStation:"
        '
        'Label_PcSize
        '
        Me.Label_PcSize.AutoSize = True
        Me.Label_PcSize.Location = New System.Drawing.Point(12, 79)
        Me.Label_PcSize.Name = "Label_PcSize"
        Me.Label_PcSize.Size = New System.Drawing.Size(121, 13)
        Me.Label_PcSize.TabIndex = 4
        Me.Label_PcSize.Text = "SoundBank Max on PC:"
        '
        'Numeric_PlayStationSize
        '
        Me.Numeric_PlayStationSize.Location = New System.Drawing.Point(188, 51)
        Me.Numeric_PlayStationSize.Maximum = New Decimal(New Integer() {1868, 0, 0, 0})
        Me.Numeric_PlayStationSize.Name = "Numeric_PlayStationSize"
        Me.Numeric_PlayStationSize.Size = New System.Drawing.Size(120, 20)
        Me.Numeric_PlayStationSize.TabIndex = 3
        '
        'Numeric_PcSize
        '
        Me.Numeric_PcSize.Location = New System.Drawing.Point(188, 77)
        Me.Numeric_PcSize.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.Numeric_PcSize.Name = "Numeric_PcSize"
        Me.Numeric_PcSize.Size = New System.Drawing.Size(120, 20)
        Me.Numeric_PcSize.TabIndex = 5
        '
        'Label_GameCubeSize
        '
        Me.Label_GameCubeSize.AutoSize = True
        Me.Label_GameCubeSize.Location = New System.Drawing.Point(12, 105)
        Me.Label_GameCubeSize.Name = "Label_GameCubeSize"
        Me.Label_GameCubeSize.Size = New System.Drawing.Size(160, 13)
        Me.Label_GameCubeSize.TabIndex = 6
        Me.Label_GameCubeSize.Text = "SoundBank Max on GameCube:"
        '
        'Numeric_GameCubeSize
        '
        Me.Numeric_GameCubeSize.Location = New System.Drawing.Point(188, 103)
        Me.Numeric_GameCubeSize.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.Numeric_GameCubeSize.Name = "Numeric_GameCubeSize"
        Me.Numeric_GameCubeSize.Size = New System.Drawing.Size(120, 20)
        Me.Numeric_GameCubeSize.TabIndex = 7
        '
        'Label_XboxSize
        '
        Me.Label_XboxSize.AutoSize = True
        Me.Label_XboxSize.Location = New System.Drawing.Point(12, 131)
        Me.Label_XboxSize.Name = "Label_XboxSize"
        Me.Label_XboxSize.Size = New System.Drawing.Size(132, 13)
        Me.Label_XboxSize.TabIndex = 8
        Me.Label_XboxSize.Text = "SoundBank Max on XBox:"
        '
        'Numeric_XboxSize
        '
        Me.Numeric_XboxSize.Location = New System.Drawing.Point(188, 129)
        Me.Numeric_XboxSize.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.Numeric_XboxSize.Name = "Numeric_XboxSize"
        Me.Numeric_XboxSize.Size = New System.Drawing.Size(120, 20)
        Me.Numeric_XboxSize.TabIndex = 9
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(152, 166)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 10
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(233, 166)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 11
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'SetMaxBankSize
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(320, 201)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Numeric_XboxSize)
        Me.Controls.Add(Me.Label_XboxSize)
        Me.Controls.Add(Me.Numeric_GameCubeSize)
        Me.Controls.Add(Me.Label_GameCubeSize)
        Me.Controls.Add(Me.Numeric_PcSize)
        Me.Controls.Add(Me.Numeric_PlayStationSize)
        Me.Controls.Add(Me.Label_PcSize)
        Me.Controls.Add(Me.Label_PlayStationSize)
        Me.Controls.Add(Me.TextBox_BankName)
        Me.Controls.Add(Me.Label_BankName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SetMaxBankSize"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Set Max Bank Size"
        CType(Me.Numeric_PlayStationSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_PcSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_GameCubeSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_XboxSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_BankName As Label
    Friend WithEvents TextBox_BankName As TextBox
    Friend WithEvents Label_PlayStationSize As Label
    Friend WithEvents Label_PcSize As Label
    Friend WithEvents Numeric_PlayStationSize As NumericUpDown
    Friend WithEvents Numeric_PcSize As NumericUpDown
    Friend WithEvents Label_GameCubeSize As Label
    Friend WithEvents Numeric_GameCubeSize As NumericUpDown
    Friend WithEvents Label_XboxSize As Label
    Friend WithEvents Numeric_XboxSize As NumericUpDown
    Friend WithEvents Button_OK As Button
    Friend WithEvents Button_Cancel As Button
End Class

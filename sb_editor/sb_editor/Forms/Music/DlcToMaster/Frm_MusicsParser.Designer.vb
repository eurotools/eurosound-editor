<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MusicsParser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_MusicsParser))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Textbox_DlcFolderPath = New System.Windows.Forms.TextBox()
        Me.Button_DlcFolder = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_DestinationFolder = New System.Windows.Forms.TextBox()
        Me.Button_Import = New System.Windows.Forms.Button()
        Me.GroupBox_Output = New System.Windows.Forms.GroupBox()
        Me.TextBox_OutputConsole = New System.Windows.Forms.TextBox()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox_Output.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "DLC Folder:"
        '
        'Textbox_DlcFolderPath
        '
        Me.Textbox_DlcFolderPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Textbox_DlcFolderPath.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_DlcFolderPath.Location = New System.Drawing.Point(81, 12)
        Me.Textbox_DlcFolderPath.Name = "Textbox_DlcFolderPath"
        Me.Textbox_DlcFolderPath.ReadOnly = True
        Me.Textbox_DlcFolderPath.Size = New System.Drawing.Size(349, 20)
        Me.Textbox_DlcFolderPath.TabIndex = 1
        '
        'Button_DlcFolder
        '
        Me.Button_DlcFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_DlcFolder.Location = New System.Drawing.Point(436, 12)
        Me.Button_DlcFolder.Name = "Button_DlcFolder"
        Me.Button_DlcFolder.Size = New System.Drawing.Size(25, 20)
        Me.Button_DlcFolder.TabIndex = 2
        Me.Button_DlcFolder.Text = "..."
        Me.Button_DlcFolder.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Destination Folder:"
        '
        'TextBox_DestinationFolder
        '
        Me.TextBox_DestinationFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_DestinationFolder.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_DestinationFolder.Location = New System.Drawing.Point(113, 38)
        Me.TextBox_DestinationFolder.Name = "TextBox_DestinationFolder"
        Me.TextBox_DestinationFolder.ReadOnly = True
        Me.TextBox_DestinationFolder.Size = New System.Drawing.Size(348, 20)
        Me.TextBox_DestinationFolder.TabIndex = 4
        '
        'Button_Import
        '
        Me.Button_Import.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Import.Location = New System.Drawing.Point(386, 64)
        Me.Button_Import.Name = "Button_Import"
        Me.Button_Import.Size = New System.Drawing.Size(75, 23)
        Me.Button_Import.TabIndex = 5
        Me.Button_Import.Text = "Import!"
        Me.Button_Import.UseVisualStyleBackColor = True
        '
        'GroupBox_Output
        '
        Me.GroupBox_Output.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Output.Controls.Add(Me.TextBox_OutputConsole)
        Me.GroupBox_Output.Location = New System.Drawing.Point(12, 93)
        Me.GroupBox_Output.Name = "GroupBox_Output"
        Me.GroupBox_Output.Size = New System.Drawing.Size(449, 306)
        Me.GroupBox_Output.TabIndex = 6
        Me.GroupBox_Output.TabStop = False
        Me.GroupBox_Output.Text = "Output:"
        '
        'TextBox_OutputConsole
        '
        Me.TextBox_OutputConsole.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_OutputConsole.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_OutputConsole.Location = New System.Drawing.Point(3, 16)
        Me.TextBox_OutputConsole.Multiline = True
        Me.TextBox_OutputConsole.Name = "TextBox_OutputConsole"
        Me.TextBox_OutputConsole.ReadOnly = True
        Me.TextBox_OutputConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox_OutputConsole.Size = New System.Drawing.Size(443, 287)
        Me.TextBox_OutputConsole.TabIndex = 0
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(386, 409)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 7
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'FolderBrowserDialog
        '
        Me.FolderBrowserDialog.Description = "Specify the Soundtrack DLC Folder"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'Frm_MusicsParser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(473, 444)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.GroupBox_Output)
        Me.Controls.Add(Me.Button_Import)
        Me.Controls.Add(Me.TextBox_DestinationFolder)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button_DlcFolder)
        Me.Controls.Add(Me.Textbox_DlcFolderPath)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_MusicsParser"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import Musics From DLC"
        Me.GroupBox_Output.ResumeLayout(False)
        Me.GroupBox_Output.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Textbox_DlcFolderPath As TextBox
    Friend WithEvents Button_DlcFolder As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox_DestinationFolder As TextBox
    Friend WithEvents Button_Import As Button
    Friend WithEvents GroupBox_Output As GroupBox
    Friend WithEvents TextBox_OutputConsole As TextBox
    Friend WithEvents Button_OK As Button
    Friend WithEvents FolderBrowserDialog As FolderBrowserDialog
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class

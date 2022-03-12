<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Project_Properties
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Project_Properties))
        Me.GroupBox_Master_Path = New System.Windows.Forms.GroupBox()
        Me.Button_Master_Path = New System.Windows.Forms.Button()
        Me.Label_Master_Path = New System.Windows.Forms.Label()
        Me.Textbox_Master_Path = New System.Windows.Forms.TextBox()
        Me.GroupBox_SonixFolder = New System.Windows.Forms.GroupBox()
        Me.Button_SonixFolder = New System.Windows.Forms.Button()
        Me.Textbox_SonixFolder = New System.Windows.Forms.TextBox()
        Me.GroupBox_EngineXFolder = New System.Windows.Forms.GroupBox()
        Me.Button_EngineXFolder = New System.Windows.Forms.Button()
        Me.Textbox_EngineXFolder = New System.Windows.Forms.TextBox()
        Me.GroupBox_EuroLandServer = New System.Windows.Forms.GroupBox()
        Me.Button_EuroLandServer = New System.Windows.Forms.Button()
        Me.Textbox_EuroLandServer = New System.Windows.Forms.TextBox()
        Me.GroupBox_FormatProps = New System.Windows.Forms.GroupBox()
        Me.Button_ResampleOff = New System.Windows.Forms.Button()
        Me.Button_Resample_On = New System.Windows.Forms.Button()
        Me.Button_BrowseOutput = New System.Windows.Forms.Button()
        Me.ComboBox_Platform = New System.Windows.Forms.ComboBox()
        Me.Button_AddFormat = New System.Windows.Forms.Button()
        Me.ListView_Formats = New System.Windows.Forms.ListView()
        Me.Col_Format = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_OutputFolder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_AutoResample = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape2 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.GroupBox_AvailableRates = New System.Windows.Forms.GroupBox()
        Me.Button_AddSampleRate = New System.Windows.Forms.Button()
        Me.ListBox_SampleRates = New System.Windows.Forms.ListBox()
        Me.GroupBox_ResamplePerFormat = New System.Windows.Forms.GroupBox()
        Me.ListView_SampleRateValues = New System.Windows.Forms.ListView()
        Me.Col_Label = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_SampleRate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ComboBox_RatesFormat = New System.Windows.Forms.ComboBox()
        Me.GroupBox_Misc = New System.Windows.Forms.GroupBox()
        Me.Label_XboxMaxSizeK = New System.Windows.Forms.Label()
        Me.Label_GameCubeMaxSizeK = New System.Windows.Forms.Label()
        Me.Label_PcMaxSizeK = New System.Windows.Forms.Label()
        Me.Label_PlayStationMaxSizeK = New System.Windows.Forms.Label()
        Me.CheckBox_PrefixHashCodes = New System.Windows.Forms.CheckBox()
        Me.Numeric_XboxMaxSize = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_GameCubeMaxSize = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_PcMaxSize = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_PlayStationMaxSize = New System.Windows.Forms.NumericUpDown()
        Me.Label_XboxMaxSize = New System.Windows.Forms.Label()
        Me.Label_GameCubeMaxSize = New System.Windows.Forms.Label()
        Me.Label_PlayStationMaxSize = New System.Windows.Forms.Label()
        Me.Label_PcMaxSize = New System.Windows.Forms.Label()
        Me.TextBox_TextEditor = New System.Windows.Forms.TextBox()
        Me.Label_TextEditor = New System.Windows.Forms.Label()
        Me.TextBox_UserName = New System.Windows.Forms.TextBox()
        Me.Label_UserName = New System.Windows.Forms.Label()
        Me.TextBox_EditWavs = New System.Windows.Forms.TextBox()
        Me.Label_EditWavs = New System.Windows.Forms.Label()
        Me.ComboBox_DefaultSampleRate = New System.Windows.Forms.ComboBox()
        Me.Label_DefaultSampleRate = New System.Windows.Forms.Label()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.ShapeContainer2 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape4 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.LineShape3 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.Checkbox_ViewPrePostOutputCommands = New System.Windows.Forms.CheckBox()
        Me.GroupBox_Master_Path.SuspendLayout()
        Me.GroupBox_SonixFolder.SuspendLayout()
        Me.GroupBox_EngineXFolder.SuspendLayout()
        Me.GroupBox_EuroLandServer.SuspendLayout()
        Me.GroupBox_FormatProps.SuspendLayout()
        Me.GroupBox_AvailableRates.SuspendLayout()
        Me.GroupBox_ResamplePerFormat.SuspendLayout()
        Me.GroupBox_Misc.SuspendLayout()
        CType(Me.Numeric_XboxMaxSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_GameCubeMaxSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_PcMaxSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_PlayStationMaxSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox_Master_Path
        '
        Me.GroupBox_Master_Path.Controls.Add(Me.Button_Master_Path)
        Me.GroupBox_Master_Path.Controls.Add(Me.Label_Master_Path)
        Me.GroupBox_Master_Path.Controls.Add(Me.Textbox_Master_Path)
        Me.GroupBox_Master_Path.Location = New System.Drawing.Point(12, 8)
        Me.GroupBox_Master_Path.Name = "GroupBox_Master_Path"
        Me.GroupBox_Master_Path.Size = New System.Drawing.Size(643, 42)
        Me.GroupBox_Master_Path.TabIndex = 0
        Me.GroupBox_Master_Path.TabStop = False
        Me.GroupBox_Master_Path.Text = "Master Directory"
        '
        'Button_Master_Path
        '
        Me.Button_Master_Path.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Master_Path.Location = New System.Drawing.Point(562, 13)
        Me.Button_Master_Path.Name = "Button_Master_Path"
        Me.Button_Master_Path.Size = New System.Drawing.Size(75, 23)
        Me.Button_Master_Path.TabIndex = 2
        Me.Button_Master_Path.Text = "Set Folder"
        Me.Button_Master_Path.UseVisualStyleBackColor = True
        '
        'Label_Master_Path
        '
        Me.Label_Master_Path.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Master_Path.AutoSize = True
        Me.Label_Master_Path.Location = New System.Drawing.Point(498, 18)
        Me.Label_Master_Path.Name = "Label_Master_Path"
        Me.Label_Master_Path.Size = New System.Drawing.Size(58, 13)
        Me.Label_Master_Path.TabIndex = 1
        Me.Label_Master_Path.Text = "+ \Master\"
        '
        'Textbox_Master_Path
        '
        Me.Textbox_Master_Path.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Textbox_Master_Path.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_Master_Path.Location = New System.Drawing.Point(6, 15)
        Me.Textbox_Master_Path.Name = "Textbox_Master_Path"
        Me.Textbox_Master_Path.ReadOnly = True
        Me.Textbox_Master_Path.Size = New System.Drawing.Size(486, 20)
        Me.Textbox_Master_Path.TabIndex = 0
        '
        'GroupBox_SonixFolder
        '
        Me.GroupBox_SonixFolder.Controls.Add(Me.Button_SonixFolder)
        Me.GroupBox_SonixFolder.Controls.Add(Me.Textbox_SonixFolder)
        Me.GroupBox_SonixFolder.Location = New System.Drawing.Point(12, 56)
        Me.GroupBox_SonixFolder.Name = "GroupBox_SonixFolder"
        Me.GroupBox_SonixFolder.Size = New System.Drawing.Size(643, 42)
        Me.GroupBox_SonixFolder.TabIndex = 3
        Me.GroupBox_SonixFolder.TabStop = False
        Me.GroupBox_SonixFolder.Text = "HashCode File Directory"
        '
        'Button_SonixFolder
        '
        Me.Button_SonixFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SonixFolder.Location = New System.Drawing.Point(562, 13)
        Me.Button_SonixFolder.Name = "Button_SonixFolder"
        Me.Button_SonixFolder.Size = New System.Drawing.Size(75, 23)
        Me.Button_SonixFolder.TabIndex = 2
        Me.Button_SonixFolder.Text = "Set Folder"
        Me.Button_SonixFolder.UseVisualStyleBackColor = True
        '
        'Textbox_SonixFolder
        '
        Me.Textbox_SonixFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Textbox_SonixFolder.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_SonixFolder.Location = New System.Drawing.Point(6, 15)
        Me.Textbox_SonixFolder.Name = "Textbox_SonixFolder"
        Me.Textbox_SonixFolder.ReadOnly = True
        Me.Textbox_SonixFolder.Size = New System.Drawing.Size(486, 20)
        Me.Textbox_SonixFolder.TabIndex = 0
        '
        'GroupBox_EngineXFolder
        '
        Me.GroupBox_EngineXFolder.Controls.Add(Me.Button_EngineXFolder)
        Me.GroupBox_EngineXFolder.Controls.Add(Me.Textbox_EngineXFolder)
        Me.GroupBox_EngineXFolder.Location = New System.Drawing.Point(12, 104)
        Me.GroupBox_EngineXFolder.Name = "GroupBox_EngineXFolder"
        Me.GroupBox_EngineXFolder.Size = New System.Drawing.Size(643, 42)
        Me.GroupBox_EngineXFolder.TabIndex = 4
        Me.GroupBox_EngineXFolder.TabStop = False
        Me.GroupBox_EngineXFolder.Text = "EngineX project path"
        '
        'Button_EngineXFolder
        '
        Me.Button_EngineXFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_EngineXFolder.Location = New System.Drawing.Point(562, 13)
        Me.Button_EngineXFolder.Name = "Button_EngineXFolder"
        Me.Button_EngineXFolder.Size = New System.Drawing.Size(75, 23)
        Me.Button_EngineXFolder.TabIndex = 2
        Me.Button_EngineXFolder.Text = "Set Folder"
        Me.Button_EngineXFolder.UseVisualStyleBackColor = True
        '
        'Textbox_EngineXFolder
        '
        Me.Textbox_EngineXFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Textbox_EngineXFolder.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_EngineXFolder.Location = New System.Drawing.Point(6, 15)
        Me.Textbox_EngineXFolder.Name = "Textbox_EngineXFolder"
        Me.Textbox_EngineXFolder.ReadOnly = True
        Me.Textbox_EngineXFolder.Size = New System.Drawing.Size(486, 20)
        Me.Textbox_EngineXFolder.TabIndex = 0
        '
        'GroupBox_EuroLandServer
        '
        Me.GroupBox_EuroLandServer.Controls.Add(Me.Button_EuroLandServer)
        Me.GroupBox_EuroLandServer.Controls.Add(Me.Textbox_EuroLandServer)
        Me.GroupBox_EuroLandServer.Location = New System.Drawing.Point(12, 152)
        Me.GroupBox_EuroLandServer.Name = "GroupBox_EuroLandServer"
        Me.GroupBox_EuroLandServer.Size = New System.Drawing.Size(643, 42)
        Me.GroupBox_EuroLandServer.TabIndex = 5
        Me.GroupBox_EuroLandServer.TabStop = False
        Me.GroupBox_EuroLandServer.Text = "EuroLand HashCode Server path"
        '
        'Button_EuroLandServer
        '
        Me.Button_EuroLandServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_EuroLandServer.Location = New System.Drawing.Point(562, 13)
        Me.Button_EuroLandServer.Name = "Button_EuroLandServer"
        Me.Button_EuroLandServer.Size = New System.Drawing.Size(75, 23)
        Me.Button_EuroLandServer.TabIndex = 2
        Me.Button_EuroLandServer.Text = "Set Folder"
        Me.Button_EuroLandServer.UseVisualStyleBackColor = True
        '
        'Textbox_EuroLandServer
        '
        Me.Textbox_EuroLandServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Textbox_EuroLandServer.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_EuroLandServer.Location = New System.Drawing.Point(6, 15)
        Me.Textbox_EuroLandServer.Name = "Textbox_EuroLandServer"
        Me.Textbox_EuroLandServer.ReadOnly = True
        Me.Textbox_EuroLandServer.Size = New System.Drawing.Size(486, 20)
        Me.Textbox_EuroLandServer.TabIndex = 0
        '
        'GroupBox_FormatProps
        '
        Me.GroupBox_FormatProps.Controls.Add(Me.Button_ResampleOff)
        Me.GroupBox_FormatProps.Controls.Add(Me.Button_Resample_On)
        Me.GroupBox_FormatProps.Controls.Add(Me.Button_BrowseOutput)
        Me.GroupBox_FormatProps.Controls.Add(Me.ComboBox_Platform)
        Me.GroupBox_FormatProps.Controls.Add(Me.Button_AddFormat)
        Me.GroupBox_FormatProps.Controls.Add(Me.ListView_Formats)
        Me.GroupBox_FormatProps.Controls.Add(Me.ShapeContainer1)
        Me.GroupBox_FormatProps.Location = New System.Drawing.Point(12, 200)
        Me.GroupBox_FormatProps.Name = "GroupBox_FormatProps"
        Me.GroupBox_FormatProps.Size = New System.Drawing.Size(643, 169)
        Me.GroupBox_FormatProps.TabIndex = 6
        Me.GroupBox_FormatProps.TabStop = False
        Me.GroupBox_FormatProps.Text = "Available Format Properties"
        '
        'Button_ResampleOff
        '
        Me.Button_ResampleOff.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ResampleOff.Location = New System.Drawing.Point(562, 140)
        Me.Button_ResampleOff.Name = "Button_ResampleOff"
        Me.Button_ResampleOff.Size = New System.Drawing.Size(75, 23)
        Me.Button_ResampleOff.TabIndex = 5
        Me.Button_ResampleOff.Text = "Off"
        Me.Button_ResampleOff.UseVisualStyleBackColor = True
        '
        'Button_Resample_On
        '
        Me.Button_Resample_On.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Resample_On.Location = New System.Drawing.Point(481, 140)
        Me.Button_Resample_On.Name = "Button_Resample_On"
        Me.Button_Resample_On.Size = New System.Drawing.Size(75, 23)
        Me.Button_Resample_On.TabIndex = 4
        Me.Button_Resample_On.Text = "On"
        Me.Button_Resample_On.UseVisualStyleBackColor = True
        '
        'Button_BrowseOutput
        '
        Me.Button_BrowseOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_BrowseOutput.Location = New System.Drawing.Point(280, 140)
        Me.Button_BrowseOutput.Name = "Button_BrowseOutput"
        Me.Button_BrowseOutput.Size = New System.Drawing.Size(127, 23)
        Me.Button_BrowseOutput.TabIndex = 3
        Me.Button_BrowseOutput.Text = "Browse for output folder"
        Me.Button_BrowseOutput.UseVisualStyleBackColor = True
        '
        'ComboBox_Platform
        '
        Me.ComboBox_Platform.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Platform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Platform.FormattingEnabled = True
        Me.ComboBox_Platform.Items.AddRange(New Object() {"PlayStation2", "X Box", "GameCube", "PC"})
        Me.ComboBox_Platform.Location = New System.Drawing.Point(69, 140)
        Me.ComboBox_Platform.Name = "ComboBox_Platform"
        Me.ComboBox_Platform.Size = New System.Drawing.Size(140, 21)
        Me.ComboBox_Platform.TabIndex = 2
        '
        'Button_AddFormat
        '
        Me.Button_AddFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_AddFormat.Location = New System.Drawing.Point(6, 140)
        Me.Button_AddFormat.Name = "Button_AddFormat"
        Me.Button_AddFormat.Size = New System.Drawing.Size(57, 23)
        Me.Button_AddFormat.TabIndex = 1
        Me.Button_AddFormat.Text = "Add"
        Me.Button_AddFormat.UseVisualStyleBackColor = True
        '
        'ListView_Formats
        '
        Me.ListView_Formats.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Formats.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Col_Format, Me.Col_OutputFolder, Me.Col_AutoResample})
        Me.ListView_Formats.FullRowSelect = True
        Me.ListView_Formats.GridLines = True
        Me.ListView_Formats.HideSelection = False
        Me.ListView_Formats.Location = New System.Drawing.Point(6, 19)
        Me.ListView_Formats.Name = "ListView_Formats"
        Me.ListView_Formats.Size = New System.Drawing.Size(631, 115)
        Me.ListView_Formats.TabIndex = 0
        Me.ListView_Formats.UseCompatibleStateImageBehavior = False
        Me.ListView_Formats.View = System.Windows.Forms.View.Details
        '
        'Col_Format
        '
        Me.Col_Format.Text = "Available Formats"
        Me.Col_Format.Width = 210
        '
        'Col_OutputFolder
        '
        Me.Col_OutputFolder.Text = "Output Folder"
        Me.Col_OutputFolder.Width = 250
        '
        'Col_AutoResample
        '
        Me.Col_AutoResample.Text = "Auto Re-Sample On/Off?"
        Me.Col_AutoResample.Width = 165
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(3, 16)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape2, Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(637, 150)
        Me.ShapeContainer1.TabIndex = 6
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape2
        '
        Me.LineShape2.AccessibleRole = System.Windows.Forms.AccessibleRole.[Default]
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.SelectionColor = System.Drawing.SystemColors.Control
        Me.LineShape2.X1 = 215
        Me.LineShape2.X2 = 215
        Me.LineShape2.Y1 = 118
        Me.LineShape2.Y2 = 150
        '
        'LineShape1
        '
        Me.LineShape1.AccessibleRole = System.Windows.Forms.AccessibleRole.[Default]
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.SelectionColor = System.Drawing.SystemColors.Control
        Me.LineShape1.X1 = 463
        Me.LineShape1.X2 = 463
        Me.LineShape1.Y1 = 118
        Me.LineShape1.Y2 = 150
        '
        'GroupBox_AvailableRates
        '
        Me.GroupBox_AvailableRates.Controls.Add(Me.Button_AddSampleRate)
        Me.GroupBox_AvailableRates.Controls.Add(Me.ListBox_SampleRates)
        Me.GroupBox_AvailableRates.Location = New System.Drawing.Point(12, 375)
        Me.GroupBox_AvailableRates.Name = "GroupBox_AvailableRates"
        Me.GroupBox_AvailableRates.Size = New System.Drawing.Size(204, 263)
        Me.GroupBox_AvailableRates.TabIndex = 7
        Me.GroupBox_AvailableRates.TabStop = False
        Me.GroupBox_AvailableRates.Text = "Available Re-Sample Rates"
        '
        'Button_AddSampleRate
        '
        Me.Button_AddSampleRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_AddSampleRate.Location = New System.Drawing.Point(6, 234)
        Me.Button_AddSampleRate.Name = "Button_AddSampleRate"
        Me.Button_AddSampleRate.Size = New System.Drawing.Size(78, 23)
        Me.Button_AddSampleRate.TabIndex = 8
        Me.Button_AddSampleRate.Text = "Add"
        Me.Button_AddSampleRate.UseVisualStyleBackColor = True
        '
        'ListBox_SampleRates
        '
        Me.ListBox_SampleRates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_SampleRates.FormattingEnabled = True
        Me.ListBox_SampleRates.Location = New System.Drawing.Point(6, 19)
        Me.ListBox_SampleRates.Name = "ListBox_SampleRates"
        Me.ListBox_SampleRates.Size = New System.Drawing.Size(191, 212)
        Me.ListBox_SampleRates.TabIndex = 0
        '
        'GroupBox_ResamplePerFormat
        '
        Me.GroupBox_ResamplePerFormat.Controls.Add(Me.ListView_SampleRateValues)
        Me.GroupBox_ResamplePerFormat.Controls.Add(Me.ComboBox_RatesFormat)
        Me.GroupBox_ResamplePerFormat.Location = New System.Drawing.Point(277, 375)
        Me.GroupBox_ResamplePerFormat.Name = "GroupBox_ResamplePerFormat"
        Me.GroupBox_ResamplePerFormat.Size = New System.Drawing.Size(378, 263)
        Me.GroupBox_ResamplePerFormat.TabIndex = 8
        Me.GroupBox_ResamplePerFormat.TabStop = False
        Me.GroupBox_ResamplePerFormat.Text = "Re-Sample Rate Values per Format"
        '
        'ListView_SampleRateValues
        '
        Me.ListView_SampleRateValues.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_SampleRateValues.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Col_Label, Me.Col_SampleRate})
        Me.ListView_SampleRateValues.FullRowSelect = True
        Me.ListView_SampleRateValues.GridLines = True
        Me.ListView_SampleRateValues.HideSelection = False
        Me.ListView_SampleRateValues.Location = New System.Drawing.Point(6, 46)
        Me.ListView_SampleRateValues.Name = "ListView_SampleRateValues"
        Me.ListView_SampleRateValues.Size = New System.Drawing.Size(366, 211)
        Me.ListView_SampleRateValues.TabIndex = 1
        Me.ListView_SampleRateValues.UseCompatibleStateImageBehavior = False
        Me.ListView_SampleRateValues.View = System.Windows.Forms.View.Details
        '
        'Col_Label
        '
        Me.Col_Label.Text = "Label"
        Me.Col_Label.Width = 170
        '
        'Col_SampleRate
        '
        Me.Col_SampleRate.Text = "Re-Sample Rate"
        Me.Col_SampleRate.Width = 170
        '
        'ComboBox_RatesFormat
        '
        Me.ComboBox_RatesFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_RatesFormat.FormattingEnabled = True
        Me.ComboBox_RatesFormat.Location = New System.Drawing.Point(6, 19)
        Me.ComboBox_RatesFormat.Name = "ComboBox_RatesFormat"
        Me.ComboBox_RatesFormat.Size = New System.Drawing.Size(235, 21)
        Me.ComboBox_RatesFormat.TabIndex = 0
        '
        'GroupBox_Misc
        '
        Me.GroupBox_Misc.Controls.Add(Me.Checkbox_ViewPrePostOutputCommands)
        Me.GroupBox_Misc.Controls.Add(Me.Label_XboxMaxSizeK)
        Me.GroupBox_Misc.Controls.Add(Me.Label_GameCubeMaxSizeK)
        Me.GroupBox_Misc.Controls.Add(Me.Label_PcMaxSizeK)
        Me.GroupBox_Misc.Controls.Add(Me.Label_PlayStationMaxSizeK)
        Me.GroupBox_Misc.Controls.Add(Me.CheckBox_PrefixHashCodes)
        Me.GroupBox_Misc.Controls.Add(Me.Numeric_XboxMaxSize)
        Me.GroupBox_Misc.Controls.Add(Me.Numeric_GameCubeMaxSize)
        Me.GroupBox_Misc.Controls.Add(Me.Numeric_PcMaxSize)
        Me.GroupBox_Misc.Controls.Add(Me.Numeric_PlayStationMaxSize)
        Me.GroupBox_Misc.Controls.Add(Me.Label_XboxMaxSize)
        Me.GroupBox_Misc.Controls.Add(Me.Label_GameCubeMaxSize)
        Me.GroupBox_Misc.Controls.Add(Me.Label_PlayStationMaxSize)
        Me.GroupBox_Misc.Controls.Add(Me.Label_PcMaxSize)
        Me.GroupBox_Misc.Controls.Add(Me.TextBox_TextEditor)
        Me.GroupBox_Misc.Controls.Add(Me.Label_TextEditor)
        Me.GroupBox_Misc.Controls.Add(Me.TextBox_UserName)
        Me.GroupBox_Misc.Controls.Add(Me.Label_UserName)
        Me.GroupBox_Misc.Controls.Add(Me.TextBox_EditWavs)
        Me.GroupBox_Misc.Controls.Add(Me.Label_EditWavs)
        Me.GroupBox_Misc.Controls.Add(Me.ComboBox_DefaultSampleRate)
        Me.GroupBox_Misc.Controls.Add(Me.Label_DefaultSampleRate)
        Me.GroupBox_Misc.Location = New System.Drawing.Point(12, 644)
        Me.GroupBox_Misc.Name = "GroupBox_Misc"
        Me.GroupBox_Misc.Size = New System.Drawing.Size(559, 184)
        Me.GroupBox_Misc.TabIndex = 9
        Me.GroupBox_Misc.TabStop = False
        Me.GroupBox_Misc.Text = "Misc"
        '
        'Label_XboxMaxSizeK
        '
        Me.Label_XboxMaxSizeK.AutoSize = True
        Me.Label_XboxMaxSizeK.Location = New System.Drawing.Point(539, 122)
        Me.Label_XboxMaxSizeK.Name = "Label_XboxMaxSizeK"
        Me.Label_XboxMaxSizeK.Size = New System.Drawing.Size(14, 13)
        Me.Label_XboxMaxSizeK.TabIndex = 19
        Me.Label_XboxMaxSizeK.Text = "K"
        '
        'Label_GameCubeMaxSizeK
        '
        Me.Label_GameCubeMaxSizeK.AutoSize = True
        Me.Label_GameCubeMaxSizeK.Location = New System.Drawing.Point(274, 162)
        Me.Label_GameCubeMaxSizeK.Name = "Label_GameCubeMaxSizeK"
        Me.Label_GameCubeMaxSizeK.Size = New System.Drawing.Size(14, 13)
        Me.Label_GameCubeMaxSizeK.TabIndex = 16
        Me.Label_GameCubeMaxSizeK.Text = "K"
        '
        'Label_PcMaxSizeK
        '
        Me.Label_PcMaxSizeK.AutoSize = True
        Me.Label_PcMaxSizeK.Location = New System.Drawing.Point(274, 141)
        Me.Label_PcMaxSizeK.Name = "Label_PcMaxSizeK"
        Me.Label_PcMaxSizeK.Size = New System.Drawing.Size(14, 13)
        Me.Label_PcMaxSizeK.TabIndex = 13
        Me.Label_PcMaxSizeK.Text = "K"
        '
        'Label_PlayStationMaxSizeK
        '
        Me.Label_PlayStationMaxSizeK.AutoSize = True
        Me.Label_PlayStationMaxSizeK.Location = New System.Drawing.Point(274, 122)
        Me.Label_PlayStationMaxSizeK.Name = "Label_PlayStationMaxSizeK"
        Me.Label_PlayStationMaxSizeK.Size = New System.Drawing.Size(14, 13)
        Me.Label_PlayStationMaxSizeK.TabIndex = 10
        Me.Label_PlayStationMaxSizeK.Text = "K"
        '
        'CheckBox_PrefixHashCodes
        '
        Me.CheckBox_PrefixHashCodes.AutoSize = True
        Me.CheckBox_PrefixHashCodes.Location = New System.Drawing.Point(302, 145)
        Me.CheckBox_PrefixHashCodes.Name = "CheckBox_PrefixHashCodes"
        Me.CheckBox_PrefixHashCodes.Size = New System.Drawing.Size(204, 17)
        Me.CheckBox_PrefixHashCodes.TabIndex = 20
        Me.CheckBox_PrefixHashCodes.Text = "Prefix All HashCodes wih HT_Sound_"
        Me.CheckBox_PrefixHashCodes.UseVisualStyleBackColor = True
        '
        'Numeric_XboxMaxSize
        '
        Me.Numeric_XboxMaxSize.Location = New System.Drawing.Point(437, 119)
        Me.Numeric_XboxMaxSize.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.Numeric_XboxMaxSize.Name = "Numeric_XboxMaxSize"
        Me.Numeric_XboxMaxSize.Size = New System.Drawing.Size(100, 20)
        Me.Numeric_XboxMaxSize.TabIndex = 18
        '
        'Numeric_GameCubeMaxSize
        '
        Me.Numeric_GameCubeMaxSize.Location = New System.Drawing.Point(172, 159)
        Me.Numeric_GameCubeMaxSize.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.Numeric_GameCubeMaxSize.Name = "Numeric_GameCubeMaxSize"
        Me.Numeric_GameCubeMaxSize.Size = New System.Drawing.Size(100, 20)
        Me.Numeric_GameCubeMaxSize.TabIndex = 15
        '
        'Numeric_PcMaxSize
        '
        Me.Numeric_PcMaxSize.Location = New System.Drawing.Point(172, 139)
        Me.Numeric_PcMaxSize.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.Numeric_PcMaxSize.Name = "Numeric_PcMaxSize"
        Me.Numeric_PcMaxSize.Size = New System.Drawing.Size(100, 20)
        Me.Numeric_PcMaxSize.TabIndex = 12
        '
        'Numeric_PlayStationMaxSize
        '
        Me.Numeric_PlayStationMaxSize.Location = New System.Drawing.Point(172, 119)
        Me.Numeric_PlayStationMaxSize.Maximum = New Decimal(New Integer() {1868, 0, 0, 0})
        Me.Numeric_PlayStationMaxSize.Name = "Numeric_PlayStationMaxSize"
        Me.Numeric_PlayStationMaxSize.Size = New System.Drawing.Size(100, 20)
        Me.Numeric_PlayStationMaxSize.TabIndex = 9
        '
        'Label_XboxMaxSize
        '
        Me.Label_XboxMaxSize.AutoSize = True
        Me.Label_XboxMaxSize.Location = New System.Drawing.Point(302, 122)
        Me.Label_XboxMaxSize.Name = "Label_XboxMaxSize"
        Me.Label_XboxMaxSize.Size = New System.Drawing.Size(129, 13)
        Me.Label_XboxMaxSize.TabIndex = 17
        Me.Label_XboxMaxSize.Text = "SoundBank Max on XBox"
        '
        'Label_GameCubeMaxSize
        '
        Me.Label_GameCubeMaxSize.AutoSize = True
        Me.Label_GameCubeMaxSize.Location = New System.Drawing.Point(9, 162)
        Me.Label_GameCubeMaxSize.Name = "Label_GameCubeMaxSize"
        Me.Label_GameCubeMaxSize.Size = New System.Drawing.Size(157, 13)
        Me.Label_GameCubeMaxSize.TabIndex = 14
        Me.Label_GameCubeMaxSize.Text = "SoundBank Max on GameCube"
        '
        'Label_PlayStationMaxSize
        '
        Me.Label_PlayStationMaxSize.AutoSize = True
        Me.Label_PlayStationMaxSize.Location = New System.Drawing.Point(6, 122)
        Me.Label_PlayStationMaxSize.Name = "Label_PlayStationMaxSize"
        Me.Label_PlayStationMaxSize.Size = New System.Drawing.Size(160, 13)
        Me.Label_PlayStationMaxSize.TabIndex = 8
        Me.Label_PlayStationMaxSize.Text = "SoundBank Max on PlayStation:"
        '
        'Label_PcMaxSize
        '
        Me.Label_PcMaxSize.AutoSize = True
        Me.Label_PcMaxSize.Location = New System.Drawing.Point(45, 141)
        Me.Label_PcMaxSize.Name = "Label_PcMaxSize"
        Me.Label_PcMaxSize.Size = New System.Drawing.Size(121, 13)
        Me.Label_PcMaxSize.TabIndex = 11
        Me.Label_PcMaxSize.Text = "SoundBank Max on PC:"
        '
        'TextBox_TextEditor
        '
        Me.TextBox_TextEditor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_TextEditor.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_TextEditor.Location = New System.Drawing.Point(93, 93)
        Me.TextBox_TextEditor.Name = "TextBox_TextEditor"
        Me.TextBox_TextEditor.ReadOnly = True
        Me.TextBox_TextEditor.Size = New System.Drawing.Size(460, 20)
        Me.TextBox_TextEditor.TabIndex = 7
        '
        'Label_TextEditor
        '
        Me.Label_TextEditor.AutoSize = True
        Me.Label_TextEditor.Location = New System.Drawing.Point(26, 96)
        Me.Label_TextEditor.Name = "Label_TextEditor"
        Me.Label_TextEditor.Size = New System.Drawing.Size(61, 13)
        Me.Label_TextEditor.TabIndex = 6
        Me.Label_TextEditor.Text = "Text Editor:"
        '
        'TextBox_UserName
        '
        Me.TextBox_UserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_UserName.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_UserName.Location = New System.Drawing.Point(93, 67)
        Me.TextBox_UserName.Name = "TextBox_UserName"
        Me.TextBox_UserName.ReadOnly = True
        Me.TextBox_UserName.Size = New System.Drawing.Size(460, 20)
        Me.TextBox_UserName.TabIndex = 5
        '
        'Label_UserName
        '
        Me.Label_UserName.AutoSize = True
        Me.Label_UserName.Location = New System.Drawing.Point(24, 70)
        Me.Label_UserName.Name = "Label_UserName"
        Me.Label_UserName.Size = New System.Drawing.Size(63, 13)
        Me.Label_UserName.TabIndex = 4
        Me.Label_UserName.Text = "User Name:"
        '
        'TextBox_EditWavs
        '
        Me.TextBox_EditWavs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_EditWavs.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_EditWavs.Location = New System.Drawing.Point(93, 41)
        Me.TextBox_EditWavs.Name = "TextBox_EditWavs"
        Me.TextBox_EditWavs.ReadOnly = True
        Me.TextBox_EditWavs.Size = New System.Drawing.Size(460, 20)
        Me.TextBox_EditWavs.TabIndex = 3
        '
        'Label_EditWavs
        '
        Me.Label_EditWavs.AutoSize = True
        Me.Label_EditWavs.Location = New System.Drawing.Point(6, 44)
        Me.Label_EditWavs.Name = "Label_EditWavs"
        Me.Label_EditWavs.Size = New System.Drawing.Size(81, 13)
        Me.Label_EditWavs.TabIndex = 2
        Me.Label_EditWavs.Text = "Edit Wavs with:"
        '
        'ComboBox_DefaultSampleRate
        '
        Me.ComboBox_DefaultSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_DefaultSampleRate.FormattingEnabled = True
        Me.ComboBox_DefaultSampleRate.Location = New System.Drawing.Point(120, 14)
        Me.ComboBox_DefaultSampleRate.Name = "ComboBox_DefaultSampleRate"
        Me.ComboBox_DefaultSampleRate.Size = New System.Drawing.Size(178, 21)
        Me.ComboBox_DefaultSampleRate.TabIndex = 1
        '
        'Label_DefaultSampleRate
        '
        Me.Label_DefaultSampleRate.AutoSize = True
        Me.Label_DefaultSampleRate.Location = New System.Drawing.Point(6, 17)
        Me.Label_DefaultSampleRate.Name = "Label_DefaultSampleRate"
        Me.Label_DefaultSampleRate.Size = New System.Drawing.Size(108, 13)
        Me.Label_DefaultSampleRate.TabIndex = 0
        Me.Label_DefaultSampleRate.Text = "Default Sample Rate:"
        '
        'Button_OK
        '
        Me.Button_OK.Location = New System.Drawing.Point(577, 805)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(78, 23)
        Me.Button_OK.TabIndex = 10
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(577, 776)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(78, 23)
        Me.Button_Cancel.TabIndex = 11
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.Filter = "EXE Files (*.exe)|*.exe"
        '
        'ShapeContainer2
        '
        Me.ShapeContainer2.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer2.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer2.Name = "ShapeContainer2"
        Me.ShapeContainer2.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape4, Me.LineShape3})
        Me.ShapeContainer2.Size = New System.Drawing.Size(667, 837)
        Me.ShapeContainer2.TabIndex = 12
        Me.ShapeContainer2.TabStop = False
        '
        'LineShape4
        '
        Me.LineShape4.AccessibleRole = System.Windows.Forms.AccessibleRole.[Default]
        Me.LineShape4.Name = "LineShape4"
        Me.LineShape4.SelectionColor = System.Drawing.SystemColors.Control
        Me.LineShape4.X1 = 13
        Me.LineShape4.X2 = 654
        Me.LineShape4.Y1 = 372
        Me.LineShape4.Y2 = 372
        '
        'LineShape3
        '
        Me.LineShape3.AccessibleRole = System.Windows.Forms.AccessibleRole.[Default]
        Me.LineShape3.Name = "LineShape3"
        Me.LineShape3.SelectionColor = System.Drawing.SystemColors.Control
        Me.LineShape3.X1 = 246
        Me.LineShape3.X2 = 246
        Me.LineShape3.Y1 = 372
        Me.LineShape3.Y2 = 636
        '
        'Checkbox_ViewPrePostOutputCommands
        '
        Me.Checkbox_ViewPrePostOutputCommands.AutoSize = True
        Me.Checkbox_ViewPrePostOutputCommands.Location = New System.Drawing.Point(302, 162)
        Me.Checkbox_ViewPrePostOutputCommands.Name = "Checkbox_ViewPrePostOutputCommands"
        Me.Checkbox_ViewPrePostOutputCommands.Size = New System.Drawing.Size(206, 17)
        Me.Checkbox_ViewPrePostOutputCommands.TabIndex = 21
        Me.Checkbox_ViewPrePostOutputCommands.Text = "View Pre/Post Output Dos Commands"
        Me.Checkbox_ViewPrePostOutputCommands.UseVisualStyleBackColor = True
        '
        'Project_Properties
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(667, 837)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.GroupBox_Misc)
        Me.Controls.Add(Me.GroupBox_ResamplePerFormat)
        Me.Controls.Add(Me.GroupBox_AvailableRates)
        Me.Controls.Add(Me.GroupBox_FormatProps)
        Me.Controls.Add(Me.GroupBox_EuroLandServer)
        Me.Controls.Add(Me.GroupBox_EngineXFolder)
        Me.Controls.Add(Me.GroupBox_SonixFolder)
        Me.Controls.Add(Me.GroupBox_Master_Path)
        Me.Controls.Add(Me.ShapeContainer2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Project_Properties"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Project Properties"
        Me.GroupBox_Master_Path.ResumeLayout(False)
        Me.GroupBox_Master_Path.PerformLayout()
        Me.GroupBox_SonixFolder.ResumeLayout(False)
        Me.GroupBox_SonixFolder.PerformLayout()
        Me.GroupBox_EngineXFolder.ResumeLayout(False)
        Me.GroupBox_EngineXFolder.PerformLayout()
        Me.GroupBox_EuroLandServer.ResumeLayout(False)
        Me.GroupBox_EuroLandServer.PerformLayout()
        Me.GroupBox_FormatProps.ResumeLayout(False)
        Me.GroupBox_AvailableRates.ResumeLayout(False)
        Me.GroupBox_ResamplePerFormat.ResumeLayout(False)
        Me.GroupBox_Misc.ResumeLayout(False)
        Me.GroupBox_Misc.PerformLayout()
        CType(Me.Numeric_XboxMaxSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_GameCubeMaxSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_PcMaxSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_PlayStationMaxSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_Master_Path As GroupBox
    Friend WithEvents Button_Master_Path As Button
    Friend WithEvents Label_Master_Path As Label
    Friend WithEvents Textbox_Master_Path As TextBox
    Friend WithEvents GroupBox_SonixFolder As GroupBox
    Friend WithEvents Button_SonixFolder As Button
    Friend WithEvents Textbox_SonixFolder As TextBox
    Friend WithEvents GroupBox_EngineXFolder As GroupBox
    Friend WithEvents Button_EngineXFolder As Button
    Friend WithEvents Textbox_EngineXFolder As TextBox
    Friend WithEvents GroupBox_EuroLandServer As GroupBox
    Friend WithEvents Button_EuroLandServer As Button
    Friend WithEvents Textbox_EuroLandServer As TextBox
    Friend WithEvents GroupBox_FormatProps As GroupBox
    Friend WithEvents Button_ResampleOff As Button
    Friend WithEvents Button_Resample_On As Button
    Friend WithEvents Button_BrowseOutput As Button
    Friend WithEvents ComboBox_Platform As ComboBox
    Friend WithEvents Button_AddFormat As Button
    Friend WithEvents ListView_Formats As ListView
    Friend WithEvents Col_Format As ColumnHeader
    Friend WithEvents Col_OutputFolder As ColumnHeader
    Friend WithEvents Col_AutoResample As ColumnHeader
    Friend WithEvents GroupBox_AvailableRates As GroupBox
    Friend WithEvents Button_AddSampleRate As Button
    Friend WithEvents ListBox_SampleRates As ListBox
    Friend WithEvents GroupBox_ResamplePerFormat As GroupBox
    Friend WithEvents ListView_SampleRateValues As ListView
    Friend WithEvents Col_Label As ColumnHeader
    Friend WithEvents Col_SampleRate As ColumnHeader
    Friend WithEvents ComboBox_RatesFormat As ComboBox
    Friend WithEvents GroupBox_Misc As GroupBox
    Friend WithEvents TextBox_TextEditor As TextBox
    Friend WithEvents Label_TextEditor As Label
    Friend WithEvents TextBox_UserName As TextBox
    Friend WithEvents Label_UserName As Label
    Friend WithEvents TextBox_EditWavs As TextBox
    Friend WithEvents Label_EditWavs As Label
    Friend WithEvents ComboBox_DefaultSampleRate As ComboBox
    Friend WithEvents Label_DefaultSampleRate As Label
    Friend WithEvents Button_OK As Button
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents Numeric_XboxMaxSize As NumericUpDown
    Friend WithEvents Numeric_GameCubeMaxSize As NumericUpDown
    Friend WithEvents Numeric_PcMaxSize As NumericUpDown
    Friend WithEvents Numeric_PlayStationMaxSize As NumericUpDown
    Friend WithEvents Label_XboxMaxSize As Label
    Friend WithEvents Label_GameCubeMaxSize As Label
    Friend WithEvents Label_PlayStationMaxSize As Label
    Friend WithEvents Label_PcMaxSize As Label
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents FolderBrowserDialog As FolderBrowserDialog
    Friend WithEvents ShapeContainer1 As PowerPacks.ShapeContainer
    Friend WithEvents LineShape2 As PowerPacks.LineShape
    Friend WithEvents LineShape1 As PowerPacks.LineShape
    Friend WithEvents ShapeContainer2 As PowerPacks.ShapeContainer
    Friend WithEvents LineShape4 As PowerPacks.LineShape
    Friend WithEvents LineShape3 As PowerPacks.LineShape
    Friend WithEvents CheckBox_PrefixHashCodes As CheckBox
    Friend WithEvents Label_XboxMaxSizeK As Label
    Friend WithEvents Label_GameCubeMaxSizeK As Label
    Friend WithEvents Label_PcMaxSizeK As Label
    Friend WithEvents Label_PlayStationMaxSizeK As Label
    Friend WithEvents Checkbox_ViewPrePostOutputCommands As CheckBox
End Class

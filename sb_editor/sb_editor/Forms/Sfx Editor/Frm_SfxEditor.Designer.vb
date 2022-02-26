<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SfxEditor
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_SfxEditor))
        Me.TabControl_Platforms = New System.Windows.Forms.TabControl()
        Me.TabPage_Common = New System.Windows.Forms.TabPage()
        Me.Panel_SfxParameters = New System.Windows.Forms.Panel()
        Me.Button_ReverbTester = New System.Windows.Forms.Button()
        Me.TextBox_DllTime = New System.Windows.Forms.TextBox()
        Me.TextBox_EuroSoundTime = New System.Windows.Forms.TextBox()
        Me.Button_StopSfx = New System.Windows.Forms.Button()
        Me.Button_TestSfx = New System.Windows.Forms.Button()
        Me.Textbox_HashCodeNumber = New System.Windows.Forms.TextBox()
        Me.Label_HashCodeNumber = New System.Windows.Forms.Label()
        Me.GroupBox_SamplePool = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox_SampleProps = New System.Windows.Forms.GroupBox()
        Me.Label_RandomPan = New System.Windows.Forms.Label()
        Me.Label_Pan = New System.Windows.Forms.Label()
        Me.Label_RandomVolume = New System.Windows.Forms.Label()
        Me.Numeric_RandomPan = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_Pan = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_RandomVolume = New System.Windows.Forms.NumericUpDown()
        Me.Numeric_BaseVolume = New System.Windows.Forms.NumericUpDown()
        Me.Label_BaseVolume = New System.Windows.Forms.Label()
        Me.Numeric_RandomPitch = New System.Windows.Forms.NumericUpDown()
        Me.Label_RandomPitch = New System.Windows.Forms.Label()
        Me.Numeric_PitchOffset = New System.Windows.Forms.NumericUpDown()
        Me.Label_PitchOffset = New System.Windows.Forms.Label()
        Me.Groupbox_SampleInfo = New System.Windows.Forms.GroupBox()
        Me.Label_SampleInfo_StreamedValue = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_LoopValue = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_LengthValue = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_SizeValue = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_FreqValue = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_Streamed = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_Loop = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_Length = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_Size = New System.Windows.Forms.Label()
        Me.Label_SampleInfo_Freq = New System.Windows.Forms.Label()
        Me.Label_Move = New System.Windows.Forms.Label()
        Me.Button_MoveDown = New System.Windows.Forms.Button()
        Me.Button_MoveUp = New System.Windows.Forms.Button()
        Me.Button_StopSample = New System.Windows.Forms.Button()
        Me.Button_PlaySample = New System.Windows.Forms.Button()
        Me.Button_EditSample = New System.Windows.Forms.Button()
        Me.Button_OpenSampleFolder = New System.Windows.Forms.Button()
        Me.Button_CopySample = New System.Windows.Forms.Button()
        Me.Button_RemoveSample = New System.Windows.Forms.Button()
        Me.Button_AddSample = New System.Windows.Forms.Button()
        Me.ListBox_SamplePool = New System.Windows.Forms.ListBox()
        Me.ContextMenu_SamplePool = New System.Windows.Forms.ContextMenu()
        Me.MenuItem_SamplePool_Add = New System.Windows.Forms.MenuItem()
        Me.MenuItem_SamplePool_Remove = New System.Windows.Forms.MenuItem()
        Me.MenuItem_SamplePool_Open = New System.Windows.Forms.MenuItem()
        Me.MenuItem_SamplePool_Copy = New System.Windows.Forms.MenuItem()
        Me.MenuItem_SamplePool_Edit = New System.Windows.Forms.MenuItem()
        Me.MenuItem_SamplePool_Play = New System.Windows.Forms.MenuItem()
        Me.MenuItem_SamplePool_Stop = New System.Windows.Forms.MenuItem()
        Me.CheckBox_EnableSubSFX = New System.Windows.Forms.CheckBox()
        Me.Panel_Options = New System.Windows.Forms.Panel()
        Me.Groupbox_RemoveFormat = New System.Windows.Forms.GroupBox()
        Me.Button_RemoveSpecificVersion = New System.Windows.Forms.Button()
        Me.Groupbox_Clipboard = New System.Windows.Forms.GroupBox()
        Me.Button_ClipboardPaste = New System.Windows.Forms.Button()
        Me.Button_Clipboard_Copy = New System.Windows.Forms.Button()
        Me.GroupBox_CreateSpecificVersion = New System.Windows.Forms.GroupBox()
        Me.Button_SpecVersion_PC = New System.Windows.Forms.Button()
        Me.Button_SpecVersion_Xbox = New System.Windows.Forms.Button()
        Me.Button_SpecVersion_GameCube = New System.Windows.Forms.Button()
        Me.Button_SpecVersion_PlayStation2 = New System.Windows.Forms.Button()
        Me.Label_SFX_Name = New System.Windows.Forms.Label()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.openFileDiag = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip_Buttons = New System.Windows.Forms.ToolTip(Me.components)
        Me.SfxParamsAndSamplePool = New sb_editor.SfxControl()
        Me.TabControl_Platforms.SuspendLayout()
        Me.Panel_SfxParameters.SuspendLayout()
        Me.GroupBox_SamplePool.SuspendLayout()
        Me.GroupBox_SampleProps.SuspendLayout()
        CType(Me.Numeric_RandomPan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_Pan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_RandomVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_BaseVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_RandomPitch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Numeric_PitchOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Groupbox_SampleInfo.SuspendLayout()
        Me.Panel_Options.SuspendLayout()
        Me.Groupbox_RemoveFormat.SuspendLayout()
        Me.Groupbox_Clipboard.SuspendLayout()
        Me.GroupBox_CreateSpecificVersion.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl_Platforms
        '
        Me.TabControl_Platforms.Controls.Add(Me.TabPage_Common)
        Me.TabControl_Platforms.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl_Platforms.Location = New System.Drawing.Point(12, 3)
        Me.TabControl_Platforms.Name = "TabControl_Platforms"
        Me.TabControl_Platforms.SelectedIndex = 0
        Me.TabControl_Platforms.Size = New System.Drawing.Size(902, 29)
        Me.TabControl_Platforms.TabIndex = 0
        '
        'TabPage_Common
        '
        Me.TabPage_Common.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage_Common.Location = New System.Drawing.Point(4, 29)
        Me.TabPage_Common.Name = "TabPage_Common"
        Me.TabPage_Common.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Common.Size = New System.Drawing.Size(894, 0)
        Me.TabPage_Common.TabIndex = 0
        Me.TabPage_Common.Text = "Common"
        Me.TabPage_Common.UseVisualStyleBackColor = True
        '
        'Panel_SfxParameters
        '
        Me.Panel_SfxParameters.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_SfxParameters.AutoScroll = True
        Me.Panel_SfxParameters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_SfxParameters.Controls.Add(Me.Button_ReverbTester)
        Me.Panel_SfxParameters.Controls.Add(Me.TextBox_DllTime)
        Me.Panel_SfxParameters.Controls.Add(Me.TextBox_EuroSoundTime)
        Me.Panel_SfxParameters.Controls.Add(Me.Button_StopSfx)
        Me.Panel_SfxParameters.Controls.Add(Me.Button_TestSfx)
        Me.Panel_SfxParameters.Controls.Add(Me.Textbox_HashCodeNumber)
        Me.Panel_SfxParameters.Controls.Add(Me.Label_HashCodeNumber)
        Me.Panel_SfxParameters.Controls.Add(Me.GroupBox_SamplePool)
        Me.Panel_SfxParameters.Controls.Add(Me.SfxParamsAndSamplePool)
        Me.Panel_SfxParameters.Location = New System.Drawing.Point(12, 32)
        Me.Panel_SfxParameters.Name = "Panel_SfxParameters"
        Me.Panel_SfxParameters.Size = New System.Drawing.Size(899, 662)
        Me.Panel_SfxParameters.TabIndex = 15
        '
        'Button_ReverbTester
        '
        Me.Button_ReverbTester.Location = New System.Drawing.Point(787, 429)
        Me.Button_ReverbTester.Name = "Button_ReverbTester"
        Me.Button_ReverbTester.Size = New System.Drawing.Size(100, 23)
        Me.Button_ReverbTester.TabIndex = 24
        Me.Button_ReverbTester.Text = "Reverb Tester"
        Me.Button_ReverbTester.UseVisualStyleBackColor = True
        '
        'TextBox_DllTime
        '
        Me.TextBox_DllTime.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_DllTime.Location = New System.Drawing.Point(787, 345)
        Me.TextBox_DllTime.Name = "TextBox_DllTime"
        Me.TextBox_DllTime.ReadOnly = True
        Me.TextBox_DllTime.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_DllTime.TabIndex = 23
        '
        'TextBox_EuroSoundTime
        '
        Me.TextBox_EuroSoundTime.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_EuroSoundTime.Location = New System.Drawing.Point(787, 319)
        Me.TextBox_EuroSoundTime.Name = "TextBox_EuroSoundTime"
        Me.TextBox_EuroSoundTime.ReadOnly = True
        Me.TextBox_EuroSoundTime.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_EuroSoundTime.TabIndex = 22
        '
        'Button_StopSfx
        '
        Me.Button_StopSfx.Location = New System.Drawing.Point(787, 400)
        Me.Button_StopSfx.Name = "Button_StopSfx"
        Me.Button_StopSfx.Size = New System.Drawing.Size(100, 23)
        Me.Button_StopSfx.TabIndex = 21
        Me.Button_StopSfx.Text = "Stop SFX"
        Me.Button_StopSfx.UseVisualStyleBackColor = True
        '
        'Button_TestSfx
        '
        Me.Button_TestSfx.Location = New System.Drawing.Point(787, 371)
        Me.Button_TestSfx.Name = "Button_TestSfx"
        Me.Button_TestSfx.Size = New System.Drawing.Size(100, 23)
        Me.Button_TestSfx.TabIndex = 20
        Me.Button_TestSfx.Text = "Test SFX"
        Me.Button_TestSfx.UseVisualStyleBackColor = True
        '
        'Textbox_HashCodeNumber
        '
        Me.Textbox_HashCodeNumber.BackColor = System.Drawing.SystemColors.Window
        Me.Textbox_HashCodeNumber.Location = New System.Drawing.Point(787, 471)
        Me.Textbox_HashCodeNumber.Name = "Textbox_HashCodeNumber"
        Me.Textbox_HashCodeNumber.ReadOnly = True
        Me.Textbox_HashCodeNumber.Size = New System.Drawing.Size(96, 20)
        Me.Textbox_HashCodeNumber.TabIndex = 19
        Me.Textbox_HashCodeNumber.Text = "0"
        '
        'Label_HashCodeNumber
        '
        Me.Label_HashCodeNumber.AutoSize = True
        Me.Label_HashCodeNumber.Location = New System.Drawing.Point(787, 455)
        Me.Label_HashCodeNumber.Name = "Label_HashCodeNumber"
        Me.Label_HashCodeNumber.Size = New System.Drawing.Size(97, 13)
        Me.Label_HashCodeNumber.TabIndex = 18
        Me.Label_HashCodeNumber.Text = "HashCode Number"
        '
        'GroupBox_SamplePool
        '
        Me.GroupBox_SamplePool.Controls.Add(Me.ComboBox1)
        Me.GroupBox_SamplePool.Controls.Add(Me.Label1)
        Me.GroupBox_SamplePool.Controls.Add(Me.GroupBox_SampleProps)
        Me.GroupBox_SamplePool.Controls.Add(Me.Groupbox_SampleInfo)
        Me.GroupBox_SamplePool.Controls.Add(Me.Label_Move)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_MoveDown)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_MoveUp)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_StopSample)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_PlaySample)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_EditSample)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_OpenSampleFolder)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_CopySample)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_RemoveSample)
        Me.GroupBox_SamplePool.Controls.Add(Me.Button_AddSample)
        Me.GroupBox_SamplePool.Controls.Add(Me.ListBox_SamplePool)
        Me.GroupBox_SamplePool.Controls.Add(Me.CheckBox_EnableSubSFX)
        Me.GroupBox_SamplePool.Location = New System.Drawing.Point(3, 287)
        Me.GroupBox_SamplePool.Name = "GroupBox_SamplePool"
        Me.GroupBox_SamplePool.Size = New System.Drawing.Size(778, 368)
        Me.GroupBox_SamplePool.TabIndex = 15
        Me.GroupBox_SamplePool.TabStop = False
        Me.GroupBox_SamplePool.Text = "Sample Pool"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Playstation2", "GameCube", "PC", "X Box"})
        Me.ComboBox1.Location = New System.Drawing.Point(622, 19)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(138, 21)
        Me.ComboBox1.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(552, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 20)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Format:"
        '
        'GroupBox_SampleProps
        '
        Me.GroupBox_SampleProps.Controls.Add(Me.Label_RandomPan)
        Me.GroupBox_SampleProps.Controls.Add(Me.Label_Pan)
        Me.GroupBox_SampleProps.Controls.Add(Me.Label_RandomVolume)
        Me.GroupBox_SampleProps.Controls.Add(Me.Numeric_RandomPan)
        Me.GroupBox_SampleProps.Controls.Add(Me.Numeric_Pan)
        Me.GroupBox_SampleProps.Controls.Add(Me.Numeric_RandomVolume)
        Me.GroupBox_SampleProps.Controls.Add(Me.Numeric_BaseVolume)
        Me.GroupBox_SampleProps.Controls.Add(Me.Label_BaseVolume)
        Me.GroupBox_SampleProps.Controls.Add(Me.Numeric_RandomPitch)
        Me.GroupBox_SampleProps.Controls.Add(Me.Label_RandomPitch)
        Me.GroupBox_SampleProps.Controls.Add(Me.Numeric_PitchOffset)
        Me.GroupBox_SampleProps.Controls.Add(Me.Label_PitchOffset)
        Me.GroupBox_SampleProps.Location = New System.Drawing.Point(533, 141)
        Me.GroupBox_SampleProps.Name = "GroupBox_SampleProps"
        Me.GroupBox_SampleProps.Size = New System.Drawing.Size(239, 178)
        Me.GroupBox_SampleProps.TabIndex = 13
        Me.GroupBox_SampleProps.TabStop = False
        Me.GroupBox_SampleProps.Text = "Sample Properties"
        '
        'Label_RandomPan
        '
        Me.Label_RandomPan.AutoSize = True
        Me.Label_RandomPan.Location = New System.Drawing.Point(9, 151)
        Me.Label_RandomPan.Name = "Label_RandomPan"
        Me.Label_RandomPan.Size = New System.Drawing.Size(100, 13)
        Me.Label_RandomPan.TabIndex = 10
        Me.Label_RandomPan.Text = "Random Pan Offset"
        '
        'Label_Pan
        '
        Me.Label_Pan.AutoSize = True
        Me.Label_Pan.Location = New System.Drawing.Point(9, 125)
        Me.Label_Pan.Name = "Label_Pan"
        Me.Label_Pan.Size = New System.Drawing.Size(89, 13)
        Me.Label_Pan.TabIndex = 8
        Me.Label_Pan.Text = "Pan (-100 to 100)"
        '
        'Label_RandomVolume
        '
        Me.Label_RandomVolume.AutoSize = True
        Me.Label_RandomVolume.Location = New System.Drawing.Point(9, 99)
        Me.Label_RandomVolume.Name = "Label_RandomVolume"
        Me.Label_RandomVolume.Size = New System.Drawing.Size(139, 13)
        Me.Label_RandomVolume.TabIndex = 6
        Me.Label_RandomVolume.Text = "Random Volume Offset (-/+)"
        '
        'Numeric_RandomPan
        '
        Me.Numeric_RandomPan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Numeric_RandomPan.Location = New System.Drawing.Point(150, 149)
        Me.Numeric_RandomPan.Name = "Numeric_RandomPan"
        Me.Numeric_RandomPan.Size = New System.Drawing.Size(83, 20)
        Me.Numeric_RandomPan.TabIndex = 11
        '
        'Numeric_Pan
        '
        Me.Numeric_Pan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Numeric_Pan.Location = New System.Drawing.Point(150, 123)
        Me.Numeric_Pan.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.Numeric_Pan.Name = "Numeric_Pan"
        Me.Numeric_Pan.Size = New System.Drawing.Size(83, 20)
        Me.Numeric_Pan.TabIndex = 9
        '
        'Numeric_RandomVolume
        '
        Me.Numeric_RandomVolume.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Numeric_RandomVolume.Location = New System.Drawing.Point(150, 97)
        Me.Numeric_RandomVolume.Minimum = New Decimal(New Integer() {9, 0, 0, -2147418112})
        Me.Numeric_RandomVolume.Name = "Numeric_RandomVolume"
        Me.Numeric_RandomVolume.Size = New System.Drawing.Size(83, 20)
        Me.Numeric_RandomVolume.TabIndex = 7
        '
        'Numeric_BaseVolume
        '
        Me.Numeric_BaseVolume.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Numeric_BaseVolume.Location = New System.Drawing.Point(150, 71)
        Me.Numeric_BaseVolume.Name = "Numeric_BaseVolume"
        Me.Numeric_BaseVolume.Size = New System.Drawing.Size(83, 20)
        Me.Numeric_BaseVolume.TabIndex = 5
        '
        'Label_BaseVolume
        '
        Me.Label_BaseVolume.AutoSize = True
        Me.Label_BaseVolume.Location = New System.Drawing.Point(9, 73)
        Me.Label_BaseVolume.Name = "Label_BaseVolume"
        Me.Label_BaseVolume.Size = New System.Drawing.Size(105, 13)
        Me.Label_BaseVolume.TabIndex = 4
        Me.Label_BaseVolume.Text = "Base Volume (0-100)"
        '
        'Numeric_RandomPitch
        '
        Me.Numeric_RandomPitch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Numeric_RandomPitch.DecimalPlaces = 1
        Me.Numeric_RandomPitch.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.Numeric_RandomPitch.Location = New System.Drawing.Point(150, 45)
        Me.Numeric_RandomPitch.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.Numeric_RandomPitch.Minimum = New Decimal(New Integer() {9, 0, 0, -2147418112})
        Me.Numeric_RandomPitch.Name = "Numeric_RandomPitch"
        Me.Numeric_RandomPitch.Size = New System.Drawing.Size(83, 20)
        Me.Numeric_RandomPitch.TabIndex = 3
        '
        'Label_RandomPitch
        '
        Me.Label_RandomPitch.AutoSize = True
        Me.Label_RandomPitch.Location = New System.Drawing.Point(9, 47)
        Me.Label_RandomPitch.Name = "Label_RandomPitch"
        Me.Label_RandomPitch.Size = New System.Drawing.Size(128, 13)
        Me.Label_RandomPitch.TabIndex = 2
        Me.Label_RandomPitch.Text = "Random Pitch Offset (-/+)"
        '
        'Numeric_PitchOffset
        '
        Me.Numeric_PitchOffset.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Numeric_PitchOffset.DecimalPlaces = 1
        Me.Numeric_PitchOffset.Location = New System.Drawing.Point(150, 19)
        Me.Numeric_PitchOffset.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.Numeric_PitchOffset.Minimum = New Decimal(New Integer() {24, 0, 0, -2147483648})
        Me.Numeric_PitchOffset.Name = "Numeric_PitchOffset"
        Me.Numeric_PitchOffset.Size = New System.Drawing.Size(83, 20)
        Me.Numeric_PitchOffset.TabIndex = 1
        '
        'Label_PitchOffset
        '
        Me.Label_PitchOffset.AutoSize = True
        Me.Label_PitchOffset.Location = New System.Drawing.Point(9, 21)
        Me.Label_PitchOffset.Name = "Label_PitchOffset"
        Me.Label_PitchOffset.Size = New System.Drawing.Size(118, 13)
        Me.Label_PitchOffset.TabIndex = 0
        Me.Label_PitchOffset.Text = "Pitch Offset (semitones)"
        '
        'Groupbox_SampleInfo
        '
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_StreamedValue)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_LoopValue)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_LengthValue)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_SizeValue)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_FreqValue)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_Streamed)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_Loop)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_Length)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_Size)
        Me.Groupbox_SampleInfo.Controls.Add(Me.Label_SampleInfo_Freq)
        Me.Groupbox_SampleInfo.Location = New System.Drawing.Point(545, 46)
        Me.Groupbox_SampleInfo.Name = "Groupbox_SampleInfo"
        Me.Groupbox_SampleInfo.Size = New System.Drawing.Size(215, 89)
        Me.Groupbox_SampleInfo.TabIndex = 12
        Me.Groupbox_SampleInfo.TabStop = False
        Me.Groupbox_SampleInfo.Text = "Sample Information"
        '
        'Label_SampleInfo_StreamedValue
        '
        Me.Label_SampleInfo_StreamedValue.AutoSize = True
        Me.Label_SampleInfo_StreamedValue.Location = New System.Drawing.Point(66, 71)
        Me.Label_SampleInfo_StreamedValue.Name = "Label_SampleInfo_StreamedValue"
        Me.Label_SampleInfo_StreamedValue.Size = New System.Drawing.Size(70, 13)
        Me.Label_SampleInfo_StreamedValue.TabIndex = 9
        Me.Label_SampleInfo_StreamedValue.Text = "                     "
        '
        'Label_SampleInfo_LoopValue
        '
        Me.Label_SampleInfo_LoopValue.AutoSize = True
        Me.Label_SampleInfo_LoopValue.Location = New System.Drawing.Point(66, 58)
        Me.Label_SampleInfo_LoopValue.Name = "Label_SampleInfo_LoopValue"
        Me.Label_SampleInfo_LoopValue.Size = New System.Drawing.Size(70, 13)
        Me.Label_SampleInfo_LoopValue.TabIndex = 8
        Me.Label_SampleInfo_LoopValue.Text = "                     "
        '
        'Label_SampleInfo_LengthValue
        '
        Me.Label_SampleInfo_LengthValue.AutoSize = True
        Me.Label_SampleInfo_LengthValue.Location = New System.Drawing.Point(66, 45)
        Me.Label_SampleInfo_LengthValue.Name = "Label_SampleInfo_LengthValue"
        Me.Label_SampleInfo_LengthValue.Size = New System.Drawing.Size(70, 13)
        Me.Label_SampleInfo_LengthValue.TabIndex = 7
        Me.Label_SampleInfo_LengthValue.Text = "                     "
        '
        'Label_SampleInfo_SizeValue
        '
        Me.Label_SampleInfo_SizeValue.AutoSize = True
        Me.Label_SampleInfo_SizeValue.Location = New System.Drawing.Point(66, 32)
        Me.Label_SampleInfo_SizeValue.Name = "Label_SampleInfo_SizeValue"
        Me.Label_SampleInfo_SizeValue.Size = New System.Drawing.Size(70, 13)
        Me.Label_SampleInfo_SizeValue.TabIndex = 6
        Me.Label_SampleInfo_SizeValue.Text = "                     "
        '
        'Label_SampleInfo_FreqValue
        '
        Me.Label_SampleInfo_FreqValue.AutoSize = True
        Me.Label_SampleInfo_FreqValue.Location = New System.Drawing.Point(66, 16)
        Me.Label_SampleInfo_FreqValue.Name = "Label_SampleInfo_FreqValue"
        Me.Label_SampleInfo_FreqValue.Size = New System.Drawing.Size(70, 13)
        Me.Label_SampleInfo_FreqValue.TabIndex = 5
        Me.Label_SampleInfo_FreqValue.Text = "                     "
        '
        'Label_SampleInfo_Streamed
        '
        Me.Label_SampleInfo_Streamed.AutoSize = True
        Me.Label_SampleInfo_Streamed.Location = New System.Drawing.Point(6, 71)
        Me.Label_SampleInfo_Streamed.Name = "Label_SampleInfo_Streamed"
        Me.Label_SampleInfo_Streamed.Size = New System.Drawing.Size(55, 13)
        Me.Label_SampleInfo_Streamed.TabIndex = 4
        Me.Label_SampleInfo_Streamed.Text = "Streamed:"
        '
        'Label_SampleInfo_Loop
        '
        Me.Label_SampleInfo_Loop.AutoSize = True
        Me.Label_SampleInfo_Loop.Location = New System.Drawing.Point(6, 58)
        Me.Label_SampleInfo_Loop.Name = "Label_SampleInfo_Loop"
        Me.Label_SampleInfo_Loop.Size = New System.Drawing.Size(34, 13)
        Me.Label_SampleInfo_Loop.TabIndex = 3
        Me.Label_SampleInfo_Loop.Text = "Loop:"
        '
        'Label_SampleInfo_Length
        '
        Me.Label_SampleInfo_Length.AutoSize = True
        Me.Label_SampleInfo_Length.Location = New System.Drawing.Point(6, 45)
        Me.Label_SampleInfo_Length.Name = "Label_SampleInfo_Length"
        Me.Label_SampleInfo_Length.Size = New System.Drawing.Size(43, 13)
        Me.Label_SampleInfo_Length.TabIndex = 2
        Me.Label_SampleInfo_Length.Text = "Length:"
        '
        'Label_SampleInfo_Size
        '
        Me.Label_SampleInfo_Size.AutoSize = True
        Me.Label_SampleInfo_Size.Location = New System.Drawing.Point(6, 32)
        Me.Label_SampleInfo_Size.Name = "Label_SampleInfo_Size"
        Me.Label_SampleInfo_Size.Size = New System.Drawing.Size(30, 13)
        Me.Label_SampleInfo_Size.TabIndex = 1
        Me.Label_SampleInfo_Size.Text = "Size:"
        '
        'Label_SampleInfo_Freq
        '
        Me.Label_SampleInfo_Freq.AutoSize = True
        Me.Label_SampleInfo_Freq.Location = New System.Drawing.Point(6, 16)
        Me.Label_SampleInfo_Freq.Name = "Label_SampleInfo_Freq"
        Me.Label_SampleInfo_Freq.Size = New System.Drawing.Size(60, 13)
        Me.Label_SampleInfo_Freq.TabIndex = 0
        Me.Label_SampleInfo_Freq.Text = "Frequency:"
        '
        'Label_Move
        '
        Me.Label_Move.AutoSize = True
        Me.Label_Move.Location = New System.Drawing.Point(485, 74)
        Me.Label_Move.Name = "Label_Move"
        Me.Label_Move.Size = New System.Drawing.Size(34, 13)
        Me.Label_Move.TabIndex = 11
        Me.Label_Move.Text = "Move"
        '
        'Button_MoveDown
        '
        Me.Button_MoveDown.Image = CType(resources.GetObject("Button_MoveDown.Image"), System.Drawing.Image)
        Me.Button_MoveDown.Location = New System.Drawing.Point(488, 119)
        Me.Button_MoveDown.Name = "Button_MoveDown"
        Me.Button_MoveDown.Size = New System.Drawing.Size(25, 23)
        Me.Button_MoveDown.TabIndex = 10
        Me.Button_MoveDown.UseVisualStyleBackColor = True
        '
        'Button_MoveUp
        '
        Me.Button_MoveUp.Image = CType(resources.GetObject("Button_MoveUp.Image"), System.Drawing.Image)
        Me.Button_MoveUp.Location = New System.Drawing.Point(488, 90)
        Me.Button_MoveUp.Name = "Button_MoveUp"
        Me.Button_MoveUp.Size = New System.Drawing.Size(25, 23)
        Me.Button_MoveUp.TabIndex = 9
        Me.Button_MoveUp.UseVisualStyleBackColor = True
        '
        'Button_StopSample
        '
        Me.Button_StopSample.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_StopSample.Location = New System.Drawing.Point(429, 331)
        Me.Button_StopSample.Name = "Button_StopSample"
        Me.Button_StopSample.Size = New System.Drawing.Size(55, 23)
        Me.Button_StopSample.TabIndex = 8
        Me.Button_StopSample.Text = "Stop"
        Me.Button_StopSample.UseVisualStyleBackColor = True
        '
        'Button_PlaySample
        '
        Me.Button_PlaySample.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PlaySample.Location = New System.Drawing.Point(375, 331)
        Me.Button_PlaySample.Name = "Button_PlaySample"
        Me.Button_PlaySample.Size = New System.Drawing.Size(55, 23)
        Me.Button_PlaySample.TabIndex = 7
        Me.Button_PlaySample.Text = "Play"
        Me.Button_PlaySample.UseVisualStyleBackColor = True
        '
        'Button_EditSample
        '
        Me.Button_EditSample.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_EditSample.Location = New System.Drawing.Point(277, 331)
        Me.Button_EditSample.Name = "Button_EditSample"
        Me.Button_EditSample.Size = New System.Drawing.Size(55, 23)
        Me.Button_EditSample.TabIndex = 6
        Me.Button_EditSample.Text = "Edit"
        Me.Button_EditSample.UseVisualStyleBackColor = True
        '
        'Button_OpenSampleFolder
        '
        Me.Button_OpenSampleFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_OpenSampleFolder.Location = New System.Drawing.Point(199, 331)
        Me.Button_OpenSampleFolder.Name = "Button_OpenSampleFolder"
        Me.Button_OpenSampleFolder.Size = New System.Drawing.Size(55, 23)
        Me.Button_OpenSampleFolder.TabIndex = 5
        Me.Button_OpenSampleFolder.Text = "Open"
        Me.Button_OpenSampleFolder.UseVisualStyleBackColor = True
        '
        'Button_CopySample
        '
        Me.Button_CopySample.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_CopySample.Location = New System.Drawing.Point(138, 331)
        Me.Button_CopySample.Name = "Button_CopySample"
        Me.Button_CopySample.Size = New System.Drawing.Size(55, 23)
        Me.Button_CopySample.TabIndex = 4
        Me.Button_CopySample.Text = "Copy"
        Me.Button_CopySample.UseVisualStyleBackColor = True
        '
        'Button_RemoveSample
        '
        Me.Button_RemoveSample.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_RemoveSample.Location = New System.Drawing.Point(77, 331)
        Me.Button_RemoveSample.Name = "Button_RemoveSample"
        Me.Button_RemoveSample.Size = New System.Drawing.Size(55, 23)
        Me.Button_RemoveSample.TabIndex = 3
        Me.Button_RemoveSample.Text = "Remove"
        Me.Button_RemoveSample.UseVisualStyleBackColor = True
        '
        'Button_AddSample
        '
        Me.Button_AddSample.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_AddSample.Location = New System.Drawing.Point(16, 331)
        Me.Button_AddSample.Name = "Button_AddSample"
        Me.Button_AddSample.Size = New System.Drawing.Size(55, 23)
        Me.Button_AddSample.TabIndex = 2
        Me.Button_AddSample.Text = "Add"
        Me.Button_AddSample.UseVisualStyleBackColor = True
        '
        'ListBox_SamplePool
        '
        Me.ListBox_SamplePool.AllowDrop = True
        Me.ListBox_SamplePool.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox_SamplePool.ContextMenu = Me.ContextMenu_SamplePool
        Me.ListBox_SamplePool.FormattingEnabled = True
        Me.ListBox_SamplePool.HorizontalScrollbar = True
        Me.ListBox_SamplePool.Location = New System.Drawing.Point(16, 74)
        Me.ListBox_SamplePool.Name = "ListBox_SamplePool"
        Me.ListBox_SamplePool.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_SamplePool.Size = New System.Drawing.Size(468, 251)
        Me.ListBox_SamplePool.TabIndex = 1
        '
        'ContextMenu_SamplePool
        '
        Me.ContextMenu_SamplePool.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem_SamplePool_Add, Me.MenuItem_SamplePool_Remove, Me.MenuItem_SamplePool_Open, Me.MenuItem_SamplePool_Copy, Me.MenuItem_SamplePool_Edit, Me.MenuItem_SamplePool_Play, Me.MenuItem_SamplePool_Stop})
        '
        'MenuItem_SamplePool_Add
        '
        Me.MenuItem_SamplePool_Add.Index = 0
        Me.MenuItem_SamplePool_Add.Text = "Add"
        '
        'MenuItem_SamplePool_Remove
        '
        Me.MenuItem_SamplePool_Remove.Index = 1
        Me.MenuItem_SamplePool_Remove.Text = "Remove"
        '
        'MenuItem_SamplePool_Open
        '
        Me.MenuItem_SamplePool_Open.Index = 2
        Me.MenuItem_SamplePool_Open.Text = "Open"
        '
        'MenuItem_SamplePool_Copy
        '
        Me.MenuItem_SamplePool_Copy.Index = 3
        Me.MenuItem_SamplePool_Copy.Text = "Copy"
        '
        'MenuItem_SamplePool_Edit
        '
        Me.MenuItem_SamplePool_Edit.Index = 4
        Me.MenuItem_SamplePool_Edit.Text = "Edit"
        '
        'MenuItem_SamplePool_Play
        '
        Me.MenuItem_SamplePool_Play.Index = 5
        Me.MenuItem_SamplePool_Play.Text = "Play"
        '
        'MenuItem_SamplePool_Stop
        '
        Me.MenuItem_SamplePool_Stop.Index = 6
        Me.MenuItem_SamplePool_Stop.Text = "Stop"
        '
        'CheckBox_EnableSubSFX
        '
        Me.CheckBox_EnableSubSFX.AutoSize = True
        Me.CheckBox_EnableSubSFX.Location = New System.Drawing.Point(16, 51)
        Me.CheckBox_EnableSubSFX.Name = "CheckBox_EnableSubSFX"
        Me.CheckBox_EnableSubSFX.Size = New System.Drawing.Size(109, 17)
        Me.CheckBox_EnableSubSFX.TabIndex = 0
        Me.CheckBox_EnableSubSFX.Text = "Enable Sub SFXs"
        Me.CheckBox_EnableSubSFX.UseVisualStyleBackColor = True
        '
        'Panel_Options
        '
        Me.Panel_Options.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Options.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_Options.Controls.Add(Me.Groupbox_RemoveFormat)
        Me.Panel_Options.Controls.Add(Me.Groupbox_Clipboard)
        Me.Panel_Options.Controls.Add(Me.GroupBox_CreateSpecificVersion)
        Me.Panel_Options.Location = New System.Drawing.Point(12, 700)
        Me.Panel_Options.Name = "Panel_Options"
        Me.Panel_Options.Size = New System.Drawing.Size(899, 60)
        Me.Panel_Options.TabIndex = 16
        '
        'Groupbox_RemoveFormat
        '
        Me.Groupbox_RemoveFormat.Controls.Add(Me.Button_RemoveSpecificVersion)
        Me.Groupbox_RemoveFormat.Location = New System.Drawing.Point(606, 4)
        Me.Groupbox_RemoveFormat.Name = "Groupbox_RemoveFormat"
        Me.Groupbox_RemoveFormat.Size = New System.Drawing.Size(281, 48)
        Me.Groupbox_RemoveFormat.TabIndex = 2
        Me.Groupbox_RemoveFormat.TabStop = False
        Me.Groupbox_RemoveFormat.Text = "Delete Specific Version"
        '
        'Button_RemoveSpecificVersion
        '
        Me.Button_RemoveSpecificVersion.Enabled = False
        Me.Button_RemoveSpecificVersion.Location = New System.Drawing.Point(6, 16)
        Me.Button_RemoveSpecificVersion.Name = "Button_RemoveSpecificVersion"
        Me.Button_RemoveSpecificVersion.Size = New System.Drawing.Size(115, 23)
        Me.Button_RemoveSpecificVersion.TabIndex = 0
        Me.Button_RemoveSpecificVersion.Text = "Remove Format"
        Me.Button_RemoveSpecificVersion.UseVisualStyleBackColor = True
        '
        'Groupbox_Clipboard
        '
        Me.Groupbox_Clipboard.Controls.Add(Me.Button_ClipboardPaste)
        Me.Groupbox_Clipboard.Controls.Add(Me.Button_Clipboard_Copy)
        Me.Groupbox_Clipboard.Location = New System.Drawing.Point(344, 4)
        Me.Groupbox_Clipboard.Name = "Groupbox_Clipboard"
        Me.Groupbox_Clipboard.Size = New System.Drawing.Size(256, 48)
        Me.Groupbox_Clipboard.TabIndex = 1
        Me.Groupbox_Clipboard.TabStop = False
        Me.Groupbox_Clipboard.Text = "Copy and Paste Version Data"
        '
        'Button_ClipboardPaste
        '
        Me.Button_ClipboardPaste.Enabled = False
        Me.Button_ClipboardPaste.Location = New System.Drawing.Point(127, 16)
        Me.Button_ClipboardPaste.Name = "Button_ClipboardPaste"
        Me.Button_ClipboardPaste.Size = New System.Drawing.Size(115, 23)
        Me.Button_ClipboardPaste.TabIndex = 1
        Me.Button_ClipboardPaste.Text = "Paste From Clipboard"
        Me.Button_ClipboardPaste.UseVisualStyleBackColor = True
        '
        'Button_Clipboard_Copy
        '
        Me.Button_Clipboard_Copy.Location = New System.Drawing.Point(6, 16)
        Me.Button_Clipboard_Copy.Name = "Button_Clipboard_Copy"
        Me.Button_Clipboard_Copy.Size = New System.Drawing.Size(115, 23)
        Me.Button_Clipboard_Copy.TabIndex = 0
        Me.Button_Clipboard_Copy.Text = "Copy To Clipboard"
        Me.Button_Clipboard_Copy.UseVisualStyleBackColor = True
        '
        'GroupBox_CreateSpecificVersion
        '
        Me.GroupBox_CreateSpecificVersion.Controls.Add(Me.Button_SpecVersion_PC)
        Me.GroupBox_CreateSpecificVersion.Controls.Add(Me.Button_SpecVersion_Xbox)
        Me.GroupBox_CreateSpecificVersion.Controls.Add(Me.Button_SpecVersion_GameCube)
        Me.GroupBox_CreateSpecificVersion.Controls.Add(Me.Button_SpecVersion_PlayStation2)
        Me.GroupBox_CreateSpecificVersion.Location = New System.Drawing.Point(8, 4)
        Me.GroupBox_CreateSpecificVersion.Name = "GroupBox_CreateSpecificVersion"
        Me.GroupBox_CreateSpecificVersion.Size = New System.Drawing.Size(330, 48)
        Me.GroupBox_CreateSpecificVersion.TabIndex = 0
        Me.GroupBox_CreateSpecificVersion.TabStop = False
        Me.GroupBox_CreateSpecificVersion.Text = "Create Format Specific Version For:"
        '
        'Button_SpecVersion_PC
        '
        Me.Button_SpecVersion_PC.Location = New System.Drawing.Point(248, 16)
        Me.Button_SpecVersion_PC.Name = "Button_SpecVersion_PC"
        Me.Button_SpecVersion_PC.Size = New System.Drawing.Size(75, 23)
        Me.Button_SpecVersion_PC.TabIndex = 3
        Me.Button_SpecVersion_PC.Text = "PC"
        Me.Button_SpecVersion_PC.UseVisualStyleBackColor = True
        '
        'Button_SpecVersion_Xbox
        '
        Me.Button_SpecVersion_Xbox.Location = New System.Drawing.Point(168, 16)
        Me.Button_SpecVersion_Xbox.Name = "Button_SpecVersion_Xbox"
        Me.Button_SpecVersion_Xbox.Size = New System.Drawing.Size(75, 23)
        Me.Button_SpecVersion_Xbox.TabIndex = 2
        Me.Button_SpecVersion_Xbox.Text = "Xbox"
        Me.Button_SpecVersion_Xbox.UseVisualStyleBackColor = True
        '
        'Button_SpecVersion_GameCube
        '
        Me.Button_SpecVersion_GameCube.Location = New System.Drawing.Point(88, 16)
        Me.Button_SpecVersion_GameCube.Name = "Button_SpecVersion_GameCube"
        Me.Button_SpecVersion_GameCube.Size = New System.Drawing.Size(75, 23)
        Me.Button_SpecVersion_GameCube.TabIndex = 1
        Me.Button_SpecVersion_GameCube.Text = "GameCube"
        Me.Button_SpecVersion_GameCube.UseVisualStyleBackColor = True
        '
        'Button_SpecVersion_PlayStation2
        '
        Me.Button_SpecVersion_PlayStation2.Location = New System.Drawing.Point(8, 16)
        Me.Button_SpecVersion_PlayStation2.Name = "Button_SpecVersion_PlayStation2"
        Me.Button_SpecVersion_PlayStation2.Size = New System.Drawing.Size(75, 23)
        Me.Button_SpecVersion_PlayStation2.TabIndex = 0
        Me.Button_SpecVersion_PlayStation2.Text = "PlayStation2"
        Me.Button_SpecVersion_PlayStation2.UseVisualStyleBackColor = True
        '
        'Label_SFX_Name
        '
        Me.Label_SFX_Name.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_SFX_Name.AutoSize = True
        Me.Label_SFX_Name.Location = New System.Drawing.Point(12, 771)
        Me.Label_SFX_Name.Name = "Label_SFX_Name"
        Me.Label_SFX_Name.Size = New System.Drawing.Size(103, 13)
        Me.Label_SFX_Name.TabIndex = 17
        Me.Label_SFX_Name.Text = ">Name: XXXXXXXX"
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(836, 766)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 19
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(755, 766)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 18
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'openFileDiag
        '
        Me.openFileDiag.Filter = "Wave File (*.wav)|*.Wav"
        Me.openFileDiag.Multiselect = True
        '
        'SfxParamsAndSamplePool
        '
        Me.SfxParamsAndSamplePool.Location = New System.Drawing.Point(3, 4)
        Me.SfxParamsAndSamplePool.Name = "SfxParamsAndSamplePool"
        Me.SfxParamsAndSamplePool.ShowSampleProperties = False
        Me.SfxParamsAndSamplePool.Size = New System.Drawing.Size(891, 277)
        Me.SfxParamsAndSamplePool.TabIndex = 14
        '
        'Frm_SfxEditor
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(921, 796)
        Me.Controls.Add(Me.Panel_Options)
        Me.Controls.Add(Me.Label_SFX_Name)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.Panel_SfxParameters)
        Me.Controls.Add(Me.TabControl_Platforms)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_SfxEditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_SfxEditor"
        Me.TabControl_Platforms.ResumeLayout(False)
        Me.Panel_SfxParameters.ResumeLayout(False)
        Me.Panel_SfxParameters.PerformLayout()
        Me.GroupBox_SamplePool.ResumeLayout(False)
        Me.GroupBox_SamplePool.PerformLayout()
        Me.GroupBox_SampleProps.ResumeLayout(False)
        Me.GroupBox_SampleProps.PerformLayout()
        CType(Me.Numeric_RandomPan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_Pan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_RandomVolume, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_BaseVolume, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_RandomPitch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Numeric_PitchOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Groupbox_SampleInfo.ResumeLayout(False)
        Me.Groupbox_SampleInfo.PerformLayout()
        Me.Panel_Options.ResumeLayout(False)
        Me.Groupbox_RemoveFormat.ResumeLayout(False)
        Me.Groupbox_Clipboard.ResumeLayout(False)
        Me.GroupBox_CreateSpecificVersion.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl_Platforms As TabControl
    Friend WithEvents TabPage_Common As TabPage
    Friend WithEvents SfxParamsAndSamplePool As SfxControl
    Friend WithEvents Panel_SfxParameters As Panel
    Private WithEvents GroupBox_SamplePool As GroupBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
    Private WithEvents GroupBox_SampleProps As GroupBox
    Private WithEvents Label_RandomPan As Label
    Private WithEvents Label_Pan As Label
    Private WithEvents Label_RandomVolume As Label
    Private WithEvents Numeric_RandomPan As NumericUpDown
    Private WithEvents Numeric_Pan As NumericUpDown
    Private WithEvents Numeric_RandomVolume As NumericUpDown
    Private WithEvents Numeric_BaseVolume As NumericUpDown
    Private WithEvents Label_BaseVolume As Label
    Private WithEvents Numeric_RandomPitch As NumericUpDown
    Private WithEvents Label_RandomPitch As Label
    Private WithEvents Numeric_PitchOffset As NumericUpDown
    Private WithEvents Label_PitchOffset As Label
    Private WithEvents Groupbox_SampleInfo As GroupBox
    Private WithEvents Label_SampleInfo_StreamedValue As Label
    Private WithEvents Label_SampleInfo_LoopValue As Label
    Private WithEvents Label_SampleInfo_LengthValue As Label
    Private WithEvents Label_SampleInfo_SizeValue As Label
    Private WithEvents Label_SampleInfo_FreqValue As Label
    Private WithEvents Label_SampleInfo_Streamed As Label
    Private WithEvents Label_SampleInfo_Loop As Label
    Private WithEvents Label_SampleInfo_Length As Label
    Private WithEvents Label_SampleInfo_Size As Label
    Private WithEvents Label_SampleInfo_Freq As Label
    Private WithEvents Label_Move As Label
    Private WithEvents Button_MoveDown As Button
    Private WithEvents Button_MoveUp As Button
    Private WithEvents Button_StopSample As Button
    Private WithEvents Button_PlaySample As Button
    Private WithEvents Button_EditSample As Button
    Private WithEvents Button_OpenSampleFolder As Button
    Private WithEvents Button_CopySample As Button
    Private WithEvents Button_RemoveSample As Button
    Private WithEvents Button_AddSample As Button
    Protected Friend WithEvents ListBox_SamplePool As ListBox
    Protected Friend WithEvents CheckBox_EnableSubSFX As CheckBox
    Friend WithEvents TextBox_DllTime As TextBox
    Friend WithEvents TextBox_EuroSoundTime As TextBox
    Friend WithEvents Button_StopSfx As Button
    Friend WithEvents Button_TestSfx As Button
    Protected Friend WithEvents Textbox_HashCodeNumber As TextBox
    Private WithEvents Label_HashCodeNumber As Label
    Private WithEvents Panel_Options As Panel
    Private WithEvents Groupbox_RemoveFormat As GroupBox
    Private WithEvents Button_RemoveSpecificVersion As Button
    Private WithEvents Groupbox_Clipboard As GroupBox
    Private WithEvents Button_ClipboardPaste As Button
    Private WithEvents Button_Clipboard_Copy As Button
    Private WithEvents GroupBox_CreateSpecificVersion As GroupBox
    Private WithEvents Button_SpecVersion_PC As Button
    Private WithEvents Button_SpecVersion_Xbox As Button
    Private WithEvents Button_SpecVersion_GameCube As Button
    Private WithEvents Button_SpecVersion_PlayStation2 As Button
    Private WithEvents Label_SFX_Name As Label
    Private WithEvents Button_OK As Button
    Private WithEvents Button_Cancel As Button
    Friend WithEvents Button_ReverbTester As Button
    Friend WithEvents ContextMenu_SamplePool As ContextMenu
    Friend WithEvents MenuItem_SamplePool_Add As MenuItem
    Friend WithEvents MenuItem_SamplePool_Remove As MenuItem
    Friend WithEvents MenuItem_SamplePool_Open As MenuItem
    Friend WithEvents MenuItem_SamplePool_Copy As MenuItem
    Friend WithEvents MenuItem_SamplePool_Edit As MenuItem
    Friend WithEvents MenuItem_SamplePool_Play As MenuItem
    Friend WithEvents MenuItem_SamplePool_Stop As MenuItem
    Friend WithEvents openFileDiag As OpenFileDialog
    Friend WithEvents ToolTip_Buttons As ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainFrame
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainFrame))
        Me.SoundbanksImages = New System.Windows.Forms.ImageList(Me.components)
        Me.Label_SoundBanksCount = New System.Windows.Forms.Label()
        Me.GroupBox_Output = New System.Windows.Forms.GroupBox()
        Me.CheckBox_OutAllLanguages = New System.Windows.Forms.CheckBox()
        Me.ComboBox_OutputLanguage = New System.Windows.Forms.ComboBox()
        Me.Label_Language = New System.Windows.Forms.Label()
        Me.ComboBox_Format = New System.Windows.Forms.ComboBox()
        Me.Label_Format = New System.Windows.Forms.Label()
        Me.CheckBox_FastReSample = New System.Windows.Forms.CheckBox()
        Me.Button_QuickOutput = New System.Windows.Forms.Button()
        Me.Button_FullOutput = New System.Windows.Forms.Button()
        Me.RadioButton_Output_AllBanksAll = New System.Windows.Forms.RadioButton()
        Me.RadioButton_AllBanksSelectedFormat = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Output_SelectedSoundBank = New System.Windows.Forms.RadioButton()
        Me.Label_SoundBank_DataBases = New System.Windows.Forms.Label()
        Me.GroupBox_Misc = New System.Windows.Forms.GroupBox()
        Me.Button_MarkersEditor = New System.Windows.Forms.Button()
        Me.TextBox_Debug = New System.Windows.Forms.TextBox()
        Me.Button_Advanced = New System.Windows.Forms.Button()
        Me.Button_SfxDefault = New System.Windows.Forms.Button()
        Me.Button_MusicMaker = New System.Windows.Forms.Button()
        Me.Button_ReSampling = New System.Windows.Forms.Button()
        Me.Button_ProjectProperties = New System.Windows.Forms.Button()
        Me.TreeView_SoundBanks = New System.Windows.Forms.TreeView()
        Me.ContextMenu_TreeView = New System.Windows.Forms.ContextMenu()
        Me.ContextMenu_TreeView_New = New System.Windows.Forms.MenuItem()
        Me.ContextMenu_TreeView_Copy = New System.Windows.Forms.MenuItem()
        Me.ContextMenu_TreeView_Delete = New System.Windows.Forms.MenuItem()
        Me.ContextMenu_TreeView_Rename = New System.Windows.Forms.MenuItem()
        Me.ContextMenu_TreeView_Properties = New System.Windows.Forms.MenuItem()
        Me.ContextMenu_TreeView_SbMaxSize = New System.Windows.Forms.MenuItem()
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox_AvailableDataBases = New System.Windows.Forms.GroupBox()
        Me.Label_DataBasesCount = New System.Windows.Forms.Label()
        Me.ListBox_DataBases = New System.Windows.Forms.ListBox()
        Me.ContextMenu_DataBases = New System.Windows.Forms.ContextMenu()
        Me.ContextMenuDataBases_AddToSoundBank = New System.Windows.Forms.MenuItem()
        Me.ContextMenuDataBases_New = New System.Windows.Forms.MenuItem()
        Me.ContextMenuDataBases_Copy = New System.Windows.Forms.MenuItem()
        Me.ContextMenuDataBases_Delete = New System.Windows.Forms.MenuItem()
        Me.ContextMenuDataBases_Rename = New System.Windows.Forms.MenuItem()
        Me.ContextMenuDataBases_Properties = New System.Windows.Forms.MenuItem()
        Me.Button_AddDataBases = New System.Windows.Forms.Button()
        Me.splitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox_SFXsInDataBase = New System.Windows.Forms.GroupBox()
        Me.ContextMenu_DataBasesSFX = New System.Windows.Forms.ContextMenu()
        Me.DataBasesSFX_Remove = New System.Windows.Forms.MenuItem()
        Me.DataBasesSFX_Properties = New System.Windows.Forms.MenuItem()
        Me.DataBasesSFX_Edit = New System.Windows.Forms.MenuItem()
        Me.DataBasesSFX_SelectSFX = New System.Windows.Forms.MenuItem()
        Me.DataBasesSFX_MultiEditor = New System.Windows.Forms.MenuItem()
        Me.Label_DataBaseSFX = New System.Windows.Forms.Label()
        Me.Button_RemoveSFXs = New System.Windows.Forms.Button()
        Me.GroupBox_AvailableSFXs = New System.Windows.Forms.GroupBox()
        Me.Button_AddSFXs = New System.Windows.Forms.Button()
        Me.GroupBox_SoundBanks = New System.Windows.Forms.GroupBox()
        Me.MainMenu_EuroSound = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItemFile = New System.Windows.Forms.MenuItem()
        Me.MenuItemFile_LoadProject = New System.Windows.Forms.MenuItem()
        Me.MenuItemFile_NewProject = New System.Windows.Forms.MenuItem()
        Me.MenuItemFile_RecentProjects = New System.Windows.Forms.MenuItem()
        Me.MenuItemFile_RecentFiles = New System.Windows.Forms.MenuItem()
        Me.MenuItemFile_Exit = New System.Windows.Forms.MenuItem()
        Me.MenuItemHelp = New System.Windows.Forms.MenuItem()
        Me.MenuItemHelp_About = New System.Windows.Forms.MenuItem()
        Me.MenuItemDebug = New System.Windows.Forms.MenuItem()
        Me.MenuItemDebug_ShowGlobalVars = New System.Windows.Forms.MenuItem()
        Me.MenuItemDebug_Console = New System.Windows.Forms.MenuItem()
        Me.ToolTip_Buttons = New System.Windows.Forms.ToolTip(Me.components)
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ListBox_DataBaseSFX = New sb_editor.MultiSelListBox()
        Me.UserControl_SFXs = New sb_editor.UserControl_SFXs()
        Me.GroupBox_Output.SuspendLayout()
        Me.GroupBox_Misc.SuspendLayout()
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.GroupBox_AvailableDataBases.SuspendLayout()
        CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer2.Panel1.SuspendLayout()
        Me.splitContainer2.Panel2.SuspendLayout()
        Me.splitContainer2.SuspendLayout()
        Me.GroupBox_SFXsInDataBase.SuspendLayout()
        Me.GroupBox_AvailableSFXs.SuspendLayout()
        Me.GroupBox_SoundBanks.SuspendLayout()
        Me.SuspendLayout()
        '
        'SoundbanksImages
        '
        Me.SoundbanksImages.ImageStream = CType(resources.GetObject("SoundbanksImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.SoundbanksImages.TransparentColor = System.Drawing.Color.Transparent
        Me.SoundbanksImages.Images.SetKeyName(0, "directory_closed-1.png")
        Me.SoundbanksImages.Images.SetKeyName(1, "directory_open_cool-1.png")
        Me.SoundbanksImages.Images.SetKeyName(2, "soundbank_Database.png")
        Me.SoundbanksImages.Images.SetKeyName(3, "Music-note-blue-icon.png")
        '
        'Label_SoundBanksCount
        '
        Me.Label_SoundBanksCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_SoundBanksCount.AutoSize = True
        Me.Label_SoundBanksCount.Location = New System.Drawing.Point(3, 414)
        Me.Label_SoundBanksCount.Name = "Label_SoundBanksCount"
        Me.Label_SoundBanksCount.Size = New System.Drawing.Size(60, 13)
        Me.Label_SoundBanksCount.TabIndex = 2
        Me.Label_SoundBanksCount.Text = "SB Total: 0"
        '
        'GroupBox_Output
        '
        Me.GroupBox_Output.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Output.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox_Output.Controls.Add(Me.CheckBox_OutAllLanguages)
        Me.GroupBox_Output.Controls.Add(Me.ComboBox_OutputLanguage)
        Me.GroupBox_Output.Controls.Add(Me.Label_Language)
        Me.GroupBox_Output.Controls.Add(Me.ComboBox_Format)
        Me.GroupBox_Output.Controls.Add(Me.Label_Format)
        Me.GroupBox_Output.Controls.Add(Me.CheckBox_FastReSample)
        Me.GroupBox_Output.Controls.Add(Me.Button_QuickOutput)
        Me.GroupBox_Output.Controls.Add(Me.Button_FullOutput)
        Me.GroupBox_Output.Controls.Add(Me.RadioButton_Output_AllBanksAll)
        Me.GroupBox_Output.Controls.Add(Me.RadioButton_AllBanksSelectedFormat)
        Me.GroupBox_Output.Controls.Add(Me.RadioButton_Output_SelectedSoundBank)
        Me.GroupBox_Output.Location = New System.Drawing.Point(12, 449)
        Me.GroupBox_Output.Name = "GroupBox_Output"
        Me.GroupBox_Output.Size = New System.Drawing.Size(261, 236)
        Me.GroupBox_Output.TabIndex = 8
        Me.GroupBox_Output.TabStop = False
        Me.GroupBox_Output.Text = "Output"
        '
        'CheckBox_OutAllLanguages
        '
        Me.CheckBox_OutAllLanguages.AutoSize = True
        Me.CheckBox_OutAllLanguages.Location = New System.Drawing.Point(11, 212)
        Me.CheckBox_OutAllLanguages.Name = "CheckBox_OutAllLanguages"
        Me.CheckBox_OutAllLanguages.Size = New System.Drawing.Size(128, 17)
        Me.CheckBox_OutAllLanguages.TabIndex = 10
        Me.CheckBox_OutAllLanguages.Text = "Output All Languages"
        Me.CheckBox_OutAllLanguages.UseVisualStyleBackColor = True
        '
        'ComboBox_OutputLanguage
        '
        Me.ComboBox_OutputLanguage.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_OutputLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_OutputLanguage.FormattingEnabled = True
        Me.ComboBox_OutputLanguage.Location = New System.Drawing.Point(72, 185)
        Me.ComboBox_OutputLanguage.Name = "ComboBox_OutputLanguage"
        Me.ComboBox_OutputLanguage.Size = New System.Drawing.Size(182, 21)
        Me.ComboBox_OutputLanguage.TabIndex = 9
        '
        'Label_Language
        '
        Me.Label_Language.AutoSize = True
        Me.Label_Language.Location = New System.Drawing.Point(8, 188)
        Me.Label_Language.Name = "Label_Language"
        Me.Label_Language.Size = New System.Drawing.Size(58, 13)
        Me.Label_Language.TabIndex = 8
        Me.Label_Language.Text = "Language:"
        '
        'ComboBox_Format
        '
        Me.ComboBox_Format.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Format.FormattingEnabled = True
        Me.ComboBox_Format.Items.AddRange(New Object() {"PlayStation2", "GameCube", "PC", "X Box"})
        Me.ComboBox_Format.Location = New System.Drawing.Point(72, 160)
        Me.ComboBox_Format.Name = "ComboBox_Format"
        Me.ComboBox_Format.Size = New System.Drawing.Size(182, 21)
        Me.ComboBox_Format.TabIndex = 7
        Me.ToolTip_Buttons.SetToolTip(Me.ComboBox_Format, "Format To Output SoundBanks In")
        '
        'Label_Format
        '
        Me.Label_Format.AutoSize = True
        Me.Label_Format.Location = New System.Drawing.Point(8, 160)
        Me.Label_Format.Name = "Label_Format"
        Me.Label_Format.Size = New System.Drawing.Size(42, 13)
        Me.Label_Format.TabIndex = 6
        Me.Label_Format.Text = "Format:"
        '
        'CheckBox_FastReSample
        '
        Me.CheckBox_FastReSample.AutoSize = True
        Me.CheckBox_FastReSample.Location = New System.Drawing.Point(8, 128)
        Me.CheckBox_FastReSample.Name = "CheckBox_FastReSample"
        Me.CheckBox_FastReSample.Size = New System.Drawing.Size(98, 17)
        Me.CheckBox_FastReSample.TabIndex = 5
        Me.CheckBox_FastReSample.Text = "Fast ReSample"
        Me.CheckBox_FastReSample.UseVisualStyleBackColor = True
        '
        'Button_QuickOutput
        '
        Me.Button_QuickOutput.Location = New System.Drawing.Point(134, 96)
        Me.Button_QuickOutput.Name = "Button_QuickOutput"
        Me.Button_QuickOutput.Size = New System.Drawing.Size(120, 23)
        Me.Button_QuickOutput.TabIndex = 4
        Me.Button_QuickOutput.Text = "Quick Output"
        Me.ToolTip_Buttons.SetToolTip(Me.Button_QuickOutput, "As Per Full Output But Excluding Re-Sampling And SFX_Define.h Build.")
        Me.Button_QuickOutput.UseVisualStyleBackColor = True
        '
        'Button_FullOutput
        '
        Me.Button_FullOutput.Location = New System.Drawing.Point(8, 96)
        Me.Button_FullOutput.Name = "Button_FullOutput"
        Me.Button_FullOutput.Size = New System.Drawing.Size(120, 23)
        Me.Button_FullOutput.TabIndex = 3
        Me.Button_FullOutput.Text = "Full Output"
        Me.ToolTip_Buttons.SetToolTip(Me.Button_FullOutput, "Output SoundBank(s) Based On Options Selected")
        Me.Button_FullOutput.UseVisualStyleBackColor = True
        '
        'RadioButton_Output_AllBanksAll
        '
        Me.RadioButton_Output_AllBanksAll.AutoSize = True
        Me.RadioButton_Output_AllBanksAll.Location = New System.Drawing.Point(8, 72)
        Me.RadioButton_Output_AllBanksAll.Name = "RadioButton_Output_AllBanksAll"
        Me.RadioButton_Output_AllBanksAll.Size = New System.Drawing.Size(141, 17)
        Me.RadioButton_Output_AllBanksAll.TabIndex = 2
        Me.RadioButton_Output_AllBanksAll.TabStop = True
        Me.RadioButton_Output_AllBanksAll.Text = "All Banks For All Formats"
        Me.RadioButton_Output_AllBanksAll.UseVisualStyleBackColor = True
        '
        'RadioButton_AllBanksSelectedFormat
        '
        Me.RadioButton_AllBanksSelectedFormat.AutoSize = True
        Me.RadioButton_AllBanksSelectedFormat.Location = New System.Drawing.Point(8, 48)
        Me.RadioButton_AllBanksSelectedFormat.Name = "RadioButton_AllBanksSelectedFormat"
        Me.RadioButton_AllBanksSelectedFormat.Size = New System.Drawing.Size(145, 17)
        Me.RadioButton_AllBanksSelectedFormat.TabIndex = 1
        Me.RadioButton_AllBanksSelectedFormat.TabStop = True
        Me.RadioButton_AllBanksSelectedFormat.Text = "All Banks For This Format"
        Me.RadioButton_AllBanksSelectedFormat.UseVisualStyleBackColor = True
        '
        'RadioButton_Output_SelectedSoundBank
        '
        Me.RadioButton_Output_SelectedSoundBank.AutoSize = True
        Me.RadioButton_Output_SelectedSoundBank.Checked = True
        Me.RadioButton_Output_SelectedSoundBank.Location = New System.Drawing.Point(8, 24)
        Me.RadioButton_Output_SelectedSoundBank.Name = "RadioButton_Output_SelectedSoundBank"
        Me.RadioButton_Output_SelectedSoundBank.Size = New System.Drawing.Size(100, 17)
        Me.RadioButton_Output_SelectedSoundBank.TabIndex = 0
        Me.RadioButton_Output_SelectedSoundBank.TabStop = True
        Me.RadioButton_Output_SelectedSoundBank.Text = "Selected Banks"
        Me.RadioButton_Output_SelectedSoundBank.UseVisualStyleBackColor = True
        '
        'Label_SoundBank_DataBases
        '
        Me.Label_SoundBank_DataBases.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_SoundBank_DataBases.AutoSize = True
        Me.Label_SoundBank_DataBases.Location = New System.Drawing.Point(244, 414)
        Me.Label_SoundBank_DataBases.Name = "Label_SoundBank_DataBases"
        Me.Label_SoundBank_DataBases.Size = New System.Drawing.Size(61, 13)
        Me.Label_SoundBank_DataBases.TabIndex = 1
        Me.Label_SoundBank_DataBases.Text = "DB Total: 0"
        '
        'GroupBox_Misc
        '
        Me.GroupBox_Misc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Misc.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox_Misc.Controls.Add(Me.Button_MarkersEditor)
        Me.GroupBox_Misc.Controls.Add(Me.TextBox_Debug)
        Me.GroupBox_Misc.Controls.Add(Me.Button_Advanced)
        Me.GroupBox_Misc.Controls.Add(Me.Button_SfxDefault)
        Me.GroupBox_Misc.Controls.Add(Me.Button_MusicMaker)
        Me.GroupBox_Misc.Controls.Add(Me.Button_ReSampling)
        Me.GroupBox_Misc.Controls.Add(Me.Button_ProjectProperties)
        Me.GroupBox_Misc.Location = New System.Drawing.Point(279, 449)
        Me.GroupBox_Misc.Name = "GroupBox_Misc"
        Me.GroupBox_Misc.Size = New System.Drawing.Size(169, 236)
        Me.GroupBox_Misc.TabIndex = 9
        Me.GroupBox_Misc.TabStop = False
        Me.GroupBox_Misc.Text = "Misc"
        '
        'Button_MarkersEditor
        '
        Me.Button_MarkersEditor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_MarkersEditor.Location = New System.Drawing.Point(6, 163)
        Me.Button_MarkersEditor.Name = "Button_MarkersEditor"
        Me.Button_MarkersEditor.Size = New System.Drawing.Size(157, 23)
        Me.Button_MarkersEditor.TabIndex = 6
        Me.Button_MarkersEditor.Text = "Markers Editor"
        Me.Button_MarkersEditor.UseVisualStyleBackColor = True
        '
        'TextBox_Debug
        '
        Me.TextBox_Debug.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_Debug.Location = New System.Drawing.Point(6, 192)
        Me.TextBox_Debug.Multiline = True
        Me.TextBox_Debug.Name = "TextBox_Debug"
        Me.TextBox_Debug.ReadOnly = True
        Me.TextBox_Debug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox_Debug.Size = New System.Drawing.Size(157, 37)
        Me.TextBox_Debug.TabIndex = 5
        '
        'Button_Advanced
        '
        Me.Button_Advanced.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Advanced.Location = New System.Drawing.Point(6, 134)
        Me.Button_Advanced.Name = "Button_Advanced"
        Me.Button_Advanced.Size = New System.Drawing.Size(157, 23)
        Me.Button_Advanced.TabIndex = 4
        Me.Button_Advanced.Text = "Advanced"
        Me.Button_Advanced.UseVisualStyleBackColor = True
        '
        'Button_SfxDefault
        '
        Me.Button_SfxDefault.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SfxDefault.Location = New System.Drawing.Point(6, 105)
        Me.Button_SfxDefault.Name = "Button_SfxDefault"
        Me.Button_SfxDefault.Size = New System.Drawing.Size(157, 23)
        Me.Button_SfxDefault.TabIndex = 3
        Me.Button_SfxDefault.Text = "SFX Default"
        Me.Button_SfxDefault.UseVisualStyleBackColor = True
        '
        'Button_MusicMaker
        '
        Me.Button_MusicMaker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_MusicMaker.Location = New System.Drawing.Point(6, 76)
        Me.Button_MusicMaker.Name = "Button_MusicMaker"
        Me.Button_MusicMaker.Size = New System.Drawing.Size(157, 23)
        Me.Button_MusicMaker.TabIndex = 2
        Me.Button_MusicMaker.Text = "Music Maker"
        Me.Button_MusicMaker.UseVisualStyleBackColor = True
        '
        'Button_ReSampling
        '
        Me.Button_ReSampling.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ReSampling.Location = New System.Drawing.Point(6, 47)
        Me.Button_ReSampling.Name = "Button_ReSampling"
        Me.Button_ReSampling.Size = New System.Drawing.Size(157, 23)
        Me.Button_ReSampling.TabIndex = 1
        Me.Button_ReSampling.Text = "Re-Sampling"
        Me.ToolTip_Buttons.SetToolTip(Me.Button_ReSampling, "Open Sample Re-Sampling Window")
        Me.Button_ReSampling.UseVisualStyleBackColor = True
        '
        'Button_ProjectProperties
        '
        Me.Button_ProjectProperties.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ProjectProperties.Location = New System.Drawing.Point(6, 18)
        Me.Button_ProjectProperties.Name = "Button_ProjectProperties"
        Me.Button_ProjectProperties.Size = New System.Drawing.Size(157, 23)
        Me.Button_ProjectProperties.TabIndex = 0
        Me.Button_ProjectProperties.Text = "Properties"
        Me.ToolTip_Buttons.SetToolTip(Me.Button_ProjectProperties, "Open Project Properties Window")
        Me.Button_ProjectProperties.UseVisualStyleBackColor = True
        '
        'TreeView_SoundBanks
        '
        Me.TreeView_SoundBanks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView_SoundBanks.ContextMenu = Me.ContextMenu_TreeView
        Me.TreeView_SoundBanks.HideSelection = False
        Me.TreeView_SoundBanks.ImageIndex = 0
        Me.TreeView_SoundBanks.ImageList = Me.SoundbanksImages
        Me.TreeView_SoundBanks.Indent = 39
        Me.TreeView_SoundBanks.Location = New System.Drawing.Point(6, 19)
        Me.TreeView_SoundBanks.Name = "TreeView_SoundBanks"
        Me.TreeView_SoundBanks.SelectedImageIndex = 0
        Me.TreeView_SoundBanks.ShowPlusMinus = False
        Me.TreeView_SoundBanks.ShowRootLines = False
        Me.TreeView_SoundBanks.Size = New System.Drawing.Size(424, 381)
        Me.TreeView_SoundBanks.TabIndex = 0
        '
        'ContextMenu_TreeView
        '
        Me.ContextMenu_TreeView.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ContextMenu_TreeView_New, Me.ContextMenu_TreeView_Copy, Me.ContextMenu_TreeView_Delete, Me.ContextMenu_TreeView_Rename, Me.ContextMenu_TreeView_Properties, Me.ContextMenu_TreeView_SbMaxSize})
        '
        'ContextMenu_TreeView_New
        '
        Me.ContextMenu_TreeView_New.Index = 0
        Me.ContextMenu_TreeView_New.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.ContextMenu_TreeView_New.Text = "New"
        '
        'ContextMenu_TreeView_Copy
        '
        Me.ContextMenu_TreeView_Copy.Index = 1
        Me.ContextMenu_TreeView_Copy.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.ContextMenu_TreeView_Copy.Text = "Copy"
        '
        'ContextMenu_TreeView_Delete
        '
        Me.ContextMenu_TreeView_Delete.Index = 2
        Me.ContextMenu_TreeView_Delete.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.ContextMenu_TreeView_Delete.Text = "Delete"
        '
        'ContextMenu_TreeView_Rename
        '
        Me.ContextMenu_TreeView_Rename.Index = 3
        Me.ContextMenu_TreeView_Rename.Shortcut = System.Windows.Forms.Shortcut.CtrlR
        Me.ContextMenu_TreeView_Rename.Text = "Rename"
        '
        'ContextMenu_TreeView_Properties
        '
        Me.ContextMenu_TreeView_Properties.Index = 4
        Me.ContextMenu_TreeView_Properties.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.ContextMenu_TreeView_Properties.Text = "Properties"
        '
        'ContextMenu_TreeView_SbMaxSize
        '
        Me.ContextMenu_TreeView_SbMaxSize.Index = 5
        Me.ContextMenu_TreeView_SbMaxSize.Text = "Max Output Size?"
        '
        'splitContainer1
        '
        Me.splitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splitContainer1.Location = New System.Drawing.Point(454, 1)
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.GroupBox_AvailableDataBases)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.splitContainer2)
        Me.splitContainer1.Size = New System.Drawing.Size(685, 684)
        Me.splitContainer1.SplitterDistance = 202
        Me.splitContainer1.TabIndex = 5
        '
        'GroupBox_AvailableDataBases
        '
        Me.GroupBox_AvailableDataBases.AutoSize = True
        Me.GroupBox_AvailableDataBases.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox_AvailableDataBases.Controls.Add(Me.Label_DataBasesCount)
        Me.GroupBox_AvailableDataBases.Controls.Add(Me.ListBox_DataBases)
        Me.GroupBox_AvailableDataBases.Controls.Add(Me.Button_AddDataBases)
        Me.GroupBox_AvailableDataBases.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_AvailableDataBases.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox_AvailableDataBases.Name = "GroupBox_AvailableDataBases"
        Me.GroupBox_AvailableDataBases.Size = New System.Drawing.Size(202, 684)
        Me.GroupBox_AvailableDataBases.TabIndex = 0
        Me.GroupBox_AvailableDataBases.TabStop = False
        Me.GroupBox_AvailableDataBases.Text = "Available DataBases"
        '
        'Label_DataBasesCount
        '
        Me.Label_DataBasesCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_DataBasesCount.AutoSize = True
        Me.Label_DataBasesCount.Location = New System.Drawing.Point(6, 661)
        Me.Label_DataBasesCount.Name = "Label_DataBasesCount"
        Me.Label_DataBasesCount.Size = New System.Drawing.Size(43, 13)
        Me.Label_DataBasesCount.TabIndex = 2
        Me.Label_DataBasesCount.Text = "Total: 0"
        '
        'ListBox_DataBases
        '
        Me.ListBox_DataBases.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_DataBases.ContextMenu = Me.ContextMenu_DataBases
        Me.ListBox_DataBases.FormattingEnabled = True
        Me.ListBox_DataBases.HorizontalScrollbar = True
        Me.ListBox_DataBases.Location = New System.Drawing.Point(6, 48)
        Me.ListBox_DataBases.Name = "ListBox_DataBases"
        Me.ListBox_DataBases.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_DataBases.Size = New System.Drawing.Size(192, 602)
        Me.ListBox_DataBases.Sorted = True
        Me.ListBox_DataBases.TabIndex = 1
        Me.ToolTip_Buttons.SetToolTip(Me.ListBox_DataBases, "All Available DataBases")
        '
        'ContextMenu_DataBases
        '
        Me.ContextMenu_DataBases.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ContextMenuDataBases_AddToSoundBank, Me.ContextMenuDataBases_New, Me.ContextMenuDataBases_Copy, Me.ContextMenuDataBases_Delete, Me.ContextMenuDataBases_Rename, Me.ContextMenuDataBases_Properties})
        '
        'ContextMenuDataBases_AddToSoundBank
        '
        Me.ContextMenuDataBases_AddToSoundBank.Index = 0
        Me.ContextMenuDataBases_AddToSoundBank.Text = "Add DB to SB"
        '
        'ContextMenuDataBases_New
        '
        Me.ContextMenuDataBases_New.Index = 1
        Me.ContextMenuDataBases_New.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.ContextMenuDataBases_New.Text = "New"
        '
        'ContextMenuDataBases_Copy
        '
        Me.ContextMenuDataBases_Copy.Index = 2
        Me.ContextMenuDataBases_Copy.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.ContextMenuDataBases_Copy.Text = "Copy"
        '
        'ContextMenuDataBases_Delete
        '
        Me.ContextMenuDataBases_Delete.Index = 3
        Me.ContextMenuDataBases_Delete.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.ContextMenuDataBases_Delete.Text = "Delete"
        '
        'ContextMenuDataBases_Rename
        '
        Me.ContextMenuDataBases_Rename.Index = 4
        Me.ContextMenuDataBases_Rename.Shortcut = System.Windows.Forms.Shortcut.F2
        Me.ContextMenuDataBases_Rename.Text = "Rename"
        '
        'ContextMenuDataBases_Properties
        '
        Me.ContextMenuDataBases_Properties.Index = 5
        Me.ContextMenuDataBases_Properties.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.ContextMenuDataBases_Properties.Text = "Properties"
        '
        'Button_AddDataBases
        '
        Me.Button_AddDataBases.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_AddDataBases.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button_AddDataBases.Location = New System.Drawing.Point(6, 16)
        Me.Button_AddDataBases.Name = "Button_AddDataBases"
        Me.Button_AddDataBases.Size = New System.Drawing.Size(192, 23)
        Me.Button_AddDataBases.TabIndex = 0
        Me.Button_AddDataBases.Text = "<<< Add DataBases"
        Me.ToolTip_Buttons.SetToolTip(Me.Button_AddDataBases, "Add Selected SFX(s) To Selected DataBase")
        Me.Button_AddDataBases.UseVisualStyleBackColor = True
        '
        'splitContainer2
        '
        Me.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer2.Name = "splitContainer2"
        '
        'splitContainer2.Panel1
        '
        Me.splitContainer2.Panel1.Controls.Add(Me.GroupBox_SFXsInDataBase)
        '
        'splitContainer2.Panel2
        '
        Me.splitContainer2.Panel2.Controls.Add(Me.GroupBox_AvailableSFXs)
        Me.splitContainer2.Size = New System.Drawing.Size(479, 684)
        Me.splitContainer2.SplitterDistance = 205
        Me.splitContainer2.TabIndex = 1
        '
        'GroupBox_SFXsInDataBase
        '
        Me.GroupBox_SFXsInDataBase.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox_SFXsInDataBase.Controls.Add(Me.ListBox_DataBaseSFX)
        Me.GroupBox_SFXsInDataBase.Controls.Add(Me.Label_DataBaseSFX)
        Me.GroupBox_SFXsInDataBase.Controls.Add(Me.Button_RemoveSFXs)
        Me.GroupBox_SFXsInDataBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_SFXsInDataBase.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox_SFXsInDataBase.Name = "GroupBox_SFXsInDataBase"
        Me.GroupBox_SFXsInDataBase.Size = New System.Drawing.Size(205, 684)
        Me.GroupBox_SFXsInDataBase.TabIndex = 0
        Me.GroupBox_SFXsInDataBase.TabStop = False
        Me.GroupBox_SFXsInDataBase.Text = "SFXs In DataBase"
        '
        'ContextMenu_DataBasesSFX
        '
        Me.ContextMenu_DataBasesSFX.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.DataBasesSFX_Remove, Me.DataBasesSFX_Properties, Me.DataBasesSFX_Edit, Me.DataBasesSFX_SelectSFX, Me.DataBasesSFX_MultiEditor})
        '
        'DataBasesSFX_Remove
        '
        Me.DataBasesSFX_Remove.Index = 0
        Me.DataBasesSFX_Remove.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.DataBasesSFX_Remove.Text = "Remove SFX"
        '
        'DataBasesSFX_Properties
        '
        Me.DataBasesSFX_Properties.Index = 1
        Me.DataBasesSFX_Properties.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.DataBasesSFX_Properties.Text = "Properties"
        '
        'DataBasesSFX_Edit
        '
        Me.DataBasesSFX_Edit.Index = 2
        Me.DataBasesSFX_Edit.Shortcut = System.Windows.Forms.Shortcut.CtrlE
        Me.DataBasesSFX_Edit.Text = "Edit"
        '
        'DataBasesSFX_SelectSFX
        '
        Me.DataBasesSFX_SelectSFX.Index = 3
        Me.DataBasesSFX_SelectSFX.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.DataBasesSFX_SelectSFX.Text = "Select SFX"
        '
        'DataBasesSFX_MultiEditor
        '
        Me.DataBasesSFX_MultiEditor.Index = 4
        Me.DataBasesSFX_MultiEditor.Text = "Multi Editor"
        '
        'Label_DataBaseSFX
        '
        Me.Label_DataBaseSFX.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_DataBaseSFX.AutoSize = True
        Me.Label_DataBaseSFX.Location = New System.Drawing.Point(6, 661)
        Me.Label_DataBaseSFX.Name = "Label_DataBaseSFX"
        Me.Label_DataBaseSFX.Size = New System.Drawing.Size(43, 13)
        Me.Label_DataBaseSFX.TabIndex = 2
        Me.Label_DataBaseSFX.Text = "Total: 0"
        '
        'Button_RemoveSFXs
        '
        Me.Button_RemoveSFXs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_RemoveSFXs.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button_RemoveSFXs.Location = New System.Drawing.Point(6, 16)
        Me.Button_RemoveSFXs.Name = "Button_RemoveSFXs"
        Me.Button_RemoveSFXs.Size = New System.Drawing.Size(193, 23)
        Me.Button_RemoveSFXs.TabIndex = 0
        Me.Button_RemoveSFXs.Text = "Remove SFXs >>>"
        Me.ToolTip_Buttons.SetToolTip(Me.Button_RemoveSFXs, "Remove Selected SFX(s) From Selected DataBase")
        Me.Button_RemoveSFXs.UseVisualStyleBackColor = True
        '
        'GroupBox_AvailableSFXs
        '
        Me.GroupBox_AvailableSFXs.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox_AvailableSFXs.Controls.Add(Me.Button_AddSFXs)
        Me.GroupBox_AvailableSFXs.Controls.Add(Me.UserControl_SFXs)
        Me.GroupBox_AvailableSFXs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_AvailableSFXs.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox_AvailableSFXs.Name = "GroupBox_AvailableSFXs"
        Me.GroupBox_AvailableSFXs.Size = New System.Drawing.Size(270, 684)
        Me.GroupBox_AvailableSFXs.TabIndex = 0
        Me.GroupBox_AvailableSFXs.TabStop = False
        Me.GroupBox_AvailableSFXs.Text = "Available SFXs"
        '
        'Button_AddSFXs
        '
        Me.Button_AddSFXs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_AddSFXs.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button_AddSFXs.Location = New System.Drawing.Point(3, 16)
        Me.Button_AddSFXs.Name = "Button_AddSFXs"
        Me.Button_AddSFXs.Size = New System.Drawing.Size(264, 23)
        Me.Button_AddSFXs.TabIndex = 0
        Me.Button_AddSFXs.Text = "<<< Add SFXs"
        Me.ToolTip_Buttons.SetToolTip(Me.Button_AddSFXs, "Add Selected SFX(s) To Selected DataBase")
        Me.Button_AddSFXs.UseVisualStyleBackColor = True
        '
        'GroupBox_SoundBanks
        '
        Me.GroupBox_SoundBanks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_SoundBanks.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox_SoundBanks.Controls.Add(Me.Label_SoundBanksCount)
        Me.GroupBox_SoundBanks.Controls.Add(Me.Label_SoundBank_DataBases)
        Me.GroupBox_SoundBanks.Controls.Add(Me.TreeView_SoundBanks)
        Me.GroupBox_SoundBanks.Location = New System.Drawing.Point(12, 1)
        Me.GroupBox_SoundBanks.Name = "GroupBox_SoundBanks"
        Me.GroupBox_SoundBanks.Size = New System.Drawing.Size(436, 442)
        Me.GroupBox_SoundBanks.TabIndex = 6
        Me.GroupBox_SoundBanks.TabStop = False
        Me.GroupBox_SoundBanks.Text = "Sound Banks"
        '
        'MainMenu_EuroSound
        '
        Me.MainMenu_EuroSound.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemFile, Me.MenuItemHelp, Me.MenuItemDebug})
        '
        'MenuItemFile
        '
        Me.MenuItemFile.Index = 0
        Me.MenuItemFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemFile_LoadProject, Me.MenuItemFile_NewProject, Me.MenuItemFile_RecentProjects, Me.MenuItemFile_Exit})
        Me.MenuItemFile.Text = "File"
        '
        'MenuItemFile_LoadProject
        '
        Me.MenuItemFile_LoadProject.Index = 0
        Me.MenuItemFile_LoadProject.Text = "Open Project"
        '
        'MenuItemFile_NewProject
        '
        Me.MenuItemFile_NewProject.Index = 1
        Me.MenuItemFile_NewProject.Text = "New Project"
        '
        'MenuItemFile_RecentProjects
        '
        Me.MenuItemFile_RecentProjects.Index = 2
        Me.MenuItemFile_RecentProjects.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemFile_RecentFiles})
        Me.MenuItemFile_RecentProjects.Text = "Recent Projects"
        '
        'MenuItemFile_RecentFiles
        '
        Me.MenuItemFile_RecentFiles.Index = 0
        Me.MenuItemFile_RecentFiles.Text = ""
        '
        'MenuItemFile_Exit
        '
        Me.MenuItemFile_Exit.Index = 3
        Me.MenuItemFile_Exit.Text = "Exit"
        '
        'MenuItemHelp
        '
        Me.MenuItemHelp.Index = 1
        Me.MenuItemHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemHelp_About})
        Me.MenuItemHelp.Text = "Help"
        '
        'MenuItemHelp_About
        '
        Me.MenuItemHelp_About.Index = 0
        Me.MenuItemHelp_About.Text = "About"
        '
        'MenuItemDebug
        '
        Me.MenuItemDebug.Index = 2
        Me.MenuItemDebug.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemDebug_ShowGlobalVars, Me.MenuItemDebug_Console})
        Me.MenuItemDebug.Text = "Debug"
        '
        'MenuItemDebug_ShowGlobalVars
        '
        Me.MenuItemDebug_ShowGlobalVars.Index = 0
        Me.MenuItemDebug_ShowGlobalVars.Text = "Global Variables Watcher"
        '
        'MenuItemDebug_Console
        '
        Me.MenuItemDebug_Console.Index = 1
        Me.MenuItemDebug_Console.Text = "Console"
        '
        'ListBox_DataBaseSFX
        '
        Me.ListBox_DataBaseSFX.AllowDrop = True
        Me.ListBox_DataBaseSFX.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_DataBaseSFX.ContextMenu = Me.ContextMenu_DataBasesSFX
        Me.ListBox_DataBaseSFX.DragDropEffectVal = System.Windows.Forms.DragDropEffects.Move
        Me.ListBox_DataBaseSFX.FormattingEnabled = True
        Me.ListBox_DataBaseSFX.HorizontalScrollbar = True
        Me.ListBox_DataBaseSFX.Location = New System.Drawing.Point(6, 48)
        Me.ListBox_DataBaseSFX.Name = "ListBox_DataBaseSFX"
        Me.ListBox_DataBaseSFX.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_DataBaseSFX.Size = New System.Drawing.Size(193, 602)
        Me.ListBox_DataBaseSFX.TabIndex = 3
        '
        'UserControl_SFXs
        '
        Me.UserControl_SFXs.AllowDoubleClick = True
        Me.UserControl_SFXs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_SFXs.Location = New System.Drawing.Point(3, 16)
        Me.UserControl_SFXs.Name = "UserControl_SFXs"
        Me.UserControl_SFXs.Size = New System.Drawing.Size(264, 665)
        Me.UserControl_SFXs.TabIndex = 2
        '
        'MainFrame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1144, 690)
        Me.Controls.Add(Me.GroupBox_Output)
        Me.Controls.Add(Me.GroupBox_Misc)
        Me.Controls.Add(Me.splitContainer1)
        Me.Controls.Add(Me.GroupBox_SoundBanks)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu_EuroSound
        Me.Name = "MainFrame"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EuroSound Editor"
        Me.GroupBox_Output.ResumeLayout(False)
        Me.GroupBox_Output.PerformLayout()
        Me.GroupBox_Misc.ResumeLayout(False)
        Me.GroupBox_Misc.PerformLayout()
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel1.PerformLayout()
        Me.splitContainer1.Panel2.ResumeLayout(False)
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer1.ResumeLayout(False)
        Me.GroupBox_AvailableDataBases.ResumeLayout(False)
        Me.GroupBox_AvailableDataBases.PerformLayout()
        Me.splitContainer2.Panel1.ResumeLayout(False)
        Me.splitContainer2.Panel2.ResumeLayout(False)
        CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer2.ResumeLayout(False)
        Me.GroupBox_SFXsInDataBase.ResumeLayout(False)
        Me.GroupBox_SFXsInDataBase.PerformLayout()
        Me.GroupBox_AvailableSFXs.ResumeLayout(False)
        Me.GroupBox_SoundBanks.ResumeLayout(False)
        Me.GroupBox_SoundBanks.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents SoundbanksImages As ImageList
    Private WithEvents GroupBox_Output As GroupBox
    Private WithEvents Label_Format As Label
    Private WithEvents Button_QuickOutput As Button
    Private WithEvents Button_FullOutput As Button
    Private WithEvents Label_SoundBank_DataBases As Label
    Private WithEvents GroupBox_Misc As GroupBox
    Private WithEvents Button_Advanced As Button
    Private WithEvents Button_SfxDefault As Button
    Private WithEvents Button_MusicMaker As Button
    Private WithEvents Button_ReSampling As Button
    Private WithEvents Button_ProjectProperties As Button
    Private WithEvents splitContainer1 As SplitContainer
    Private WithEvents GroupBox_AvailableDataBases As GroupBox
    Private WithEvents splitContainer2 As SplitContainer
    Private WithEvents GroupBox_SFXsInDataBase As GroupBox
    Private WithEvents GroupBox_AvailableSFXs As GroupBox
    Private WithEvents GroupBox_SoundBanks As GroupBox
    Friend WithEvents MainMenu_EuroSound As MainMenu
    Friend WithEvents MenuItemFile_LoadProject As MenuItem
    Friend WithEvents MenuItemHelp As MenuItem
    Friend WithEvents MenuItemHelp_About As MenuItem
    Friend WithEvents ContextMenu_DataBasesSFX As ContextMenu
    Friend WithEvents DataBasesSFX_Edit As MenuItem
    Friend WithEvents DataBasesSFX_Properties As MenuItem
    Friend WithEvents DataBasesSFX_Remove As MenuItem
    Friend WithEvents MenuItemFile_Exit As MenuItem
    Friend WithEvents ToolTip_Buttons As ToolTip
    Friend WithEvents DataBasesSFX_SelectSFX As MenuItem
    Friend WithEvents ContextMenu_TreeView As ContextMenu
    Friend WithEvents ContextMenu_TreeView_New As MenuItem
    Friend WithEvents ContextMenu_TreeView_Delete As MenuItem
    Friend WithEvents ContextMenu_TreeView_Rename As MenuItem
    Friend WithEvents ContextMenu_TreeView_Properties As MenuItem
    Friend WithEvents Label_Language As Label
    Friend WithEvents MenuItemFile_NewProject As MenuItem
    Friend WithEvents FolderBrowserDialog As FolderBrowserDialog
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents ContextMenu_DataBases As ContextMenu
    Friend WithEvents ContextMenuDataBases_AddToSoundBank As MenuItem
    Friend WithEvents ContextMenuDataBases_New As MenuItem
    Friend WithEvents ContextMenuDataBases_Copy As MenuItem
    Friend WithEvents ContextMenuDataBases_Delete As MenuItem
    Friend WithEvents ContextMenuDataBases_Rename As MenuItem
    Friend WithEvents ContextMenuDataBases_Properties As MenuItem
    Friend WithEvents ContextMenu_TreeView_Copy As MenuItem
    Friend WithEvents ContextMenu_TreeView_SbMaxSize As MenuItem
    Friend WithEvents UserControl_SFXs As UserControl_SFXs
    Protected Friend WithEvents ListBox_DataBases As ListBox
    Protected Friend WithEvents ListBox_DataBaseSFX As MultiSelListBox
    Friend WithEvents TextBox_Debug As TextBox
    Protected Friend WithEvents TreeView_SoundBanks As TreeView
    Friend WithEvents Button_MarkersEditor As Button
    Friend WithEvents MenuItemDebug As MenuItem
    Friend WithEvents MenuItemDebug_ShowGlobalVars As MenuItem
    Friend WithEvents MenuItemDebug_Console As MenuItem
    Friend WithEvents DataBasesSFX_MultiEditor As MenuItem
    Protected Friend WithEvents Label_SoundBanksCount As Label
    Protected Friend WithEvents Label_DataBasesCount As Label
    Protected Friend WithEvents Label_DataBaseSFX As Label
    Protected Friend WithEvents ComboBox_OutputLanguage As ComboBox
    Protected Friend WithEvents Button_AddDataBases As Button
    Protected Friend WithEvents Button_RemoveSFXs As Button
    Protected Friend WithEvents Button_AddSFXs As Button
    Protected Friend WithEvents MenuItemFile_RecentProjects As MenuItem
    Protected Friend WithEvents MenuItemFile_RecentFiles As MenuItem
    Protected Friend WithEvents MenuItemFile As MenuItem
    Protected Friend WithEvents ComboBox_Format As ComboBox
    Protected Friend WithEvents CheckBox_FastReSample As CheckBox
    Protected Friend WithEvents RadioButton_Output_AllBanksAll As RadioButton
    Protected Friend WithEvents RadioButton_AllBanksSelectedFormat As RadioButton
    Protected Friend WithEvents RadioButton_Output_SelectedSoundBank As RadioButton
    Protected Friend WithEvents CheckBox_OutAllLanguages As CheckBox
End Class

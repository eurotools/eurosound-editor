﻿Imports System.IO
Imports IniFileFunctions
Imports RecentFilesMenu

Partial Public NotInheritable Class SplashScreen
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private ReadOnly writers As New FileWriters
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly availableDatabasesList As List(Of String)
    Private ReadOnly availableSoundBanksList As List(Of TreeNode)
    Private ReadOnly availableSfxList As List(Of String)
    Private ReadOnly mainform As MainFrame

    Sub New(baseForm As MainFrame)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        mainform = baseForm
    End Sub

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub SplashScreen_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Load ini file data
        LoadIniData()
        'Inform user if the working directory exists
        If fso.FolderExists(WorkingDirectory) Then
            LoadProjectData()
            CheckProjectFiles()
            'Load Project
            LoadSoundbanks(fso.BuildPath(WorkingDirectory, "Soundbanks"))
            LoadDataBases(fso.BuildPath(WorkingDirectory, "DataBases"))
            mainform.UserControl_SFXs.LoadHashCodes()
            mainform.UserControl_SFXs.LoadRefineList()
            'Update Project file
            writers.CreateProjectFile(fso.BuildPath(WorkingDirectory, "Project.txt"), mainform.TreeView_SoundBanks, mainform.ListBox_DataBases, mainform.UserControl_SFXs.ListBox_SFXs)
            'Load Languages
            AddProjectLanguagesToCombo()

            'Update GUI
            mainform.Text = "EuroSound: """ & WorkingDirectory & """"
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            MsgBox("Project Not Found", vbOKOnly + vbCritical, "EuroSound Load Project Error")
            Close()
        End If

        'Custom cursors
        mainform.Button_AddDataBases.Cursor = New Cursor(New MemoryStream(My.Resources.arrow_left))
        mainform.Button_RemoveSFXs.Cursor = New Cursor(New MemoryStream(My.Resources.arrow_right))
        mainform.Button_AddSFXs.Cursor = New Cursor(New MemoryStream(My.Resources.arrow_left))
        mainform.ComboBox_OutputLanguage.Cursor = New Cursor(New MemoryStream(My.Resources.lang_english))
        'Get recent files
        mainform.RecentFilesMenu = New MruStripMenuInline(mainform.MenuItemFile_RecentProjects, mainform.MenuItemFile_RecentFiles, New MostRecentFilesMenu.ClickedHandler(AddressOf mainform.MenuItemFile_Recent_Click), EuroSoundIniFilePath, 5)
        mainform.RecentFilesMenu.LoadFromIniFile()
        'Select first item by default
        mainform.ComboBox_Format.SelectedIndex = 0
        'Read ini file
        If SysFileProjectIniPath IsNot "" Then
            Dim iniFunctions As New IniFile(SysFileProjectIniPath)
            mainform.ComboBox_Format.SelectedIndex = iniFunctions.Read("FormatCombo_ListIndex", "Form1_Misc")
            mainform.RadioButton_AllBanksSelectedFormat.Checked = iniFunctions.Read("AllBanksOption_Value", "Form1_Misc")
            mainform.RadioButton_Output_SelectedSoundBank.Checked = iniFunctions.Read("SelectedlBankOption_Value", "Form1_Misc")
            mainform.RadioButton_Output_AllBanksAll.Checked = iniFunctions.Read("AllFormatsOption_Value", "Form1_Misc")
            mainform.CheckBox_FastReSample.Checked = Convert.ToBoolean(CByte(iniFunctions.Read("Check1", "MainForm")))
            Dim languageIndex = iniFunctions.Read("LanguageCombo", "MainForm")
            If languageIndex < mainform.ComboBox_OutputLanguage.Items.Count Then
                mainform.ComboBox_OutputLanguage.SelectedIndex = languageIndex
            ElseIf mainform.ComboBox_OutputLanguage.Items.Count > 0 Then
                mainform.ComboBox_OutputLanguage.SelectedIndex = 0
            End If
            mainform.UserControl_SFXs.CheckBox_SortByDate.Checked = Convert.ToBoolean(CByte(iniFunctions.Read("Check2", "MainForm")))
            mainform.CheckBox_OutAllLanguages.Checked = Convert.ToBoolean(CByte(iniFunctions.Read("OutputAllLanguages", "MainForm")))
        End If

        'Start timer
        TimerSplash.Start()
    End Sub

    Private Sub AddProjectLanguagesToCombo()
        'Get project languages
        If Dir(fso.BuildPath(ProjMasterFolder, "Master\Speech"), FileAttribute.Directory) IsNot "" Then
            Dim languages As String() = Directory.GetDirectories(fso.BuildPath(ProjMasterFolder, "Master\Speech"))
            'Add folders to combobox
            mainform.ComboBox_OutputLanguage.BeginUpdate()
            For index As Integer = 0 To languages.Length - 1
                Dim folderName As String = Trim(StrConv(New DirectoryInfo(languages(index)).Name, vbProperCase))
                If Array.IndexOf(SfxLanguages, folderName) <> -1 Then
                    mainform.ComboBox_OutputLanguage.Items.Add(folderName)
                End If
            Next
            mainform.ComboBox_OutputLanguage.EndUpdate()
        End If
    End Sub

    Private Sub TimerSplash_Tick(sender As Object, e As EventArgs) Handles TimerSplash.Tick
        Close()
    End Sub
End Class
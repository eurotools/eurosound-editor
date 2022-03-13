Imports System.IO
Imports IniFileFunctions
Imports NAudio.Wave
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Module ProgramMainModule
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly writers As New FileWriters

    '*===============================================================================================
    '* LOAD EUROSOUND INI FILE (APPLICATION.STARTUPPATH & "EuroSound.ini")
    '*===============================================================================================
    Friend Sub LoadSystemFilesIni()
        'Load Ini file
        If fso.FileExists(EuroSoundIniFilePath) Then
            Dim baseIniFunctions As New IniFile(EuroSoundIniFilePath)
            WorkingDirectory = baseIniFunctions.Read("Last_Project_Opened", "Form1_Misc")
            SysFileSamples = fso.BuildPath(WorkingDirectory, "System\Samples.txt")
            SysFileProperties = fso.BuildPath(WorkingDirectory, "System\Properties.txt")
            ProjAudioEditor = baseIniFunctions.Read("Edit_Wavs_With", "Form7_Misc")
            SysFileSfxDefaults = fso.BuildPath(WorkingDirectory, "System\SFX Defaults.txt")
            EuroSoundUser = baseIniFunctions.Read("UserName", "Form1_Misc")
            ProjTextEditor = baseIniFunctions.Read("TextEditor", "PropertiesForm")
        End If
    End Sub

    '*===============================================================================================
    '* LOAD EUROSOUND INI FILE ("System\EuroSound.ini")
    '*===============================================================================================
    Friend Sub LoadProgramLastState(mainform As MainFrame)
        'Load program last state
        If SysFileProjectIniPath > "" Then
            Dim iniFunctions As New IniFile(SysFileProjectIniPath)
            mainform.RadioButton_AllBanksSelectedFormat.Checked = StrComp(iniFunctions.Read("AllBanksOption_Value", "Form1_Misc"), "True") = 0
            mainform.RadioButton_Output_SelectedSoundBank.Checked = StrComp(iniFunctions.Read("SelectedlBankOption_Value", "Form1_Misc"), "True") = 0
            mainform.RadioButton_Output_AllBanksAll.Checked = StrComp(iniFunctions.Read("AllFormatsOption_Value", "Form1_Misc"), "True") = 0
            Dim tempVar As String = iniFunctions.Read("Check1", "MainForm")
            If Not tempVar = "" Then
                mainform.CheckBox_FastReSample.Checked = Convert.ToBoolean(CByte(tempVar))
            End If
            tempVar = iniFunctions.Read("Check2", "MainForm")
            If Not tempVar = "" Then
                mainform.UserControl_SFXs.CheckBox_SortByDate.Checked = Convert.ToBoolean(CByte(tempVar))
            End If
            tempVar = iniFunctions.Read("OutputAllLanguages", "MainForm")
            If Not tempVar = "" Then
                mainform.CheckBox_OutAllLanguages.Checked = Convert.ToBoolean(CByte(tempVar))
            End If

            'Combobox output language
            tempVar = iniFunctions.Read("LanguageCombo", "MainForm")
            If Not tempVar = "" Then
                Dim languageIndex As Integer = tempVar
                If languageIndex <> -1 AndAlso languageIndex < mainform.ComboBox_OutputLanguage.Items.Count Then
                    mainform.ComboBox_OutputLanguage.SelectedIndex = languageIndex
                ElseIf mainform.ComboBox_OutputLanguage.Items.Count > 0 Then
                    mainform.ComboBox_OutputLanguage.SelectedIndex = 0
                End If
            End If

            'Combobox output format
            tempVar = iniFunctions.Read("FormatCombo_ListIndex", "Form1_Misc")
            If Not tempVar = "" Then
                Dim outFormatIndex As Integer = tempVar
                If outFormatIndex <> -1 AndAlso outFormatIndex < mainform.ComboBox_Format.Items.Count Then
                    mainform.ComboBox_Format.SelectedIndex = outFormatIndex
                ElseIf mainform.ComboBox_Format.Items.Count > 0 Then
                    mainform.ComboBox_Format.SelectedIndex = 0
                End If
            End If

            'SoundBank Max Sizes
            Dim playStationValue As String = iniFunctions.Read("PlayStationSize", "PropertiesForm")
            If IsNumeric(playStationValue) Then
                SoundBankMaxPlayStation = CInt(playStationValue)
            End If
            Dim pcSize As String = iniFunctions.Read("PCSize", "PropertiesForm")
            If IsNumeric(pcSize) Then
                SoundBankMaxPC = CInt(pcSize)
            End If
            Dim gameCubeSize As String = iniFunctions.Read("GameCubeSize", "PropertiesForm")
            If IsNumeric(gameCubeSize) Then
                SoundBankMaxGameCube = CInt(gameCubeSize)
            End If
            Dim xboxSize As String = iniFunctions.Read("XBoxSize", "PropertiesForm")
            If IsNumeric(xboxSize) Then
                SoundBankMaxXbox = CInt(xboxSize)
            End If

            'Other Settings
            Dim prefixHashCodes As String = iniFunctions.Read("Prefix_HT_Sound", "PropertiesForm")
            If IsNumeric(prefixHashCodes) Then
                ProjectSettingsFile.MiscProps.PrefixHtSound = CBool(prefixHashCodes)
            End If
            Dim viewOutputDos As String = iniFunctions.Read("ViewOutputDos", "PropertiesForm")
            If IsNumeric(viewOutputDos) Then
                ProjectSettingsFile.MiscProps.ViewOutputDos = CBool(viewOutputDos)
            End If
        End If
    End Sub

    '*===============================================================================================
    '* UPDATE VARIABLES, PROJECT SYSTEM FILES
    '*===============================================================================================
    Friend Sub UpdateGlobalVariables()
        'Update System Files variables
        SysFileSamples = fso.BuildPath(WorkingDirectory, "System\Samples.txt")
        SysFileProperties = fso.BuildPath(WorkingDirectory, "System\Properties.txt")
        SysFileSfxDefaults = fso.BuildPath(WorkingDirectory, "System\SFX Defaults.txt")
        SysFileProjectIniPath = fso.BuildPath(WorkingDirectory, "System\EuroSound.ini")

        'Load misc properties
        Dim miscFilePath As String = fso.BuildPath(WorkingDirectory, "System\Misc.txt")
        If fso.FileExists(miscFilePath) Then
            textFileReaders.ReadMiscFile(miscFilePath)
        End If
    End Sub

    '*===============================================================================================
    '* LOAD PROJECT STUFF - Main Method
    '*===============================================================================================
    Friend Sub LoadProjectData(mainform As MainFrame, projectFilePath As String)
        'Clear controls
        mainform.TreeView_SoundBanks.Nodes.Clear()
        mainform.ListBox_DataBases.Items.Clear()
        mainform.ListBox_DataBaseSFX.Items.Clear()
        mainform.UserControl_SFXs.ListBox_SFXs.Items.Clear()

        'Get All SoundBanks
        Dim soundBanksFilePath As String = fso.BuildPath(WorkingDirectory, "SoundBanks")
        Dim availableSoundBanks As String() = GetSoundBankFiles(soundBanksFilePath)
        LoadSoundBanks(availableSoundBanks, mainform)
        'Update counter label
        mainform.Label_SoundBanksCount.Text = "Total: " & mainform.TreeView_SoundBanks.Nodes.Count

        'Get All DataBases
        Dim databasesFilePath As String = fso.BuildPath(WorkingDirectory, "DataBases")
        Dim availableDataBases As String() = GetDataBaseFiles(databasesFilePath)
        mainform.ListBox_DataBases.BeginUpdate()
        mainform.ListBox_DataBases.Items.AddRange(availableDataBases)
        mainform.ListBox_DataBases.EndUpdate()
        mainform.Label_DataBasesCount.Text = "Total: " & mainform.ListBox_DataBases.Items.Count
        'Create project file
        Dim temporalFile As String = fso.BuildPath(WorkingDirectory, "System\TempFileName.txt")
        writers.CreateProjectFile(temporalFile, availableSoundBanks, availableDataBases, Nothing)

        'Load Hashcodes
        Dim SFXFiles As String() = mainform.UserControl_SFXs.LoadHashCodes()
        mainform.UserControl_SFXs.LoadRefineList()

        'Update Project file
        writers.CreateProjectFile(projectFilePath, availableSoundBanks, availableDataBases, SFXFiles)

        'Update EuroSound Ini
        Dim programIni As New IniFile(EuroSoundIniFilePath)
        programIni.Write("Last_Project_Opened", WorkingDirectory, "Form1_Misc")

        'Update comboboxes
        AddProjectLanguagesToCombo(mainform.ComboBox_OutputLanguage)
        AddAvailableFormatsToCombobox(mainform.ComboBox_Format)
    End Sub

    '*===============================================================================================
    '* GET A LIST OF ALL SOUNDBANKS IN THE TREE VIEW
    '*===============================================================================================
    Friend Function GetSoundBanksList(soundBanksTreeView As TreeView) As String()
        Dim soundBanksList As String() = New String(soundBanksTreeView.Nodes.Count - 1) {}
        For index As Integer = 0 To soundBanksList.Length - 1
            soundBanksList(index) = soundBanksTreeView.Nodes(index).Text
        Next
        Return soundBanksList
    End Function

    '*===============================================================================================
    '* LOAD SOUNDBANKS
    '*===============================================================================================
    Private Function GetSoundBankFiles(soundBanksFilePath As String) As String()
        Dim soundBanks As New List(Of String)

        'Get file path and ensure that exists
        If fso.FolderExists(soundBanksFilePath) Then
            'Load data and create a list
            Dim soundBankFiles As String() = Directory.GetFiles(soundBanksFilePath, "*.txt", SearchOption.TopDirectoryOnly)
            For fileIndex As Integer = 0 To soundBankFiles.Length - 1
                soundBanks.Add(GetOnlyFileName(soundBankFiles(fileIndex)))
            Next
        End If

        'Get array and sort it
        Dim soundBanksArray As String() = soundBanks.ToArray
        Array.Sort(soundBanksArray)

        Return soundBanksArray
    End Function

    Private Sub LoadSoundBanks(availableSoundBanks As String(), mainform As MainFrame)
        mainform.TreeView_SoundBanks.BeginUpdate()
        For fileIndex As Integer = 0 To availableSoundBanks.Length - 1
            Dim soundBankFileName As String = Trim(availableSoundBanks(fileIndex))
            Dim soundBankFilePath As String = fso.BuildPath(WorkingDirectory & "\SoundBanks", soundBankFileName & ".txt")
            Dim soundBankFileData As SoundbankFile = textFileReaders.ReadSoundBankFile(soundBankFilePath)

            'Create SoundBank node
            Dim parentNode As TreeNode = mainform.TreeView_SoundBanks.Nodes.Add(CStr(soundBankFileData.HashCode), soundBankFileName, 0, 0)
            For dataBaseIndex As Integer = 0 To soundBankFileData.Dependencies.Length - 1
                Dim dataBaseName As String = Trim(soundBankFileData.Dependencies(dataBaseIndex))
                parentNode.Nodes.Add(dataBaseName, dataBaseName, 2, 2)
            Next

            'Add empty node if required
            If parentNode.Nodes.Count = 0 Then
                parentNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
            End If
        Next
        mainform.TreeView_SoundBanks.EndUpdate()
    End Sub

    '*===============================================================================================
    '* LOAD DATABASES
    '*===============================================================================================
    Private Function GetDataBaseFiles(dataBasesFilePath) As String()
        Dim databases As New List(Of String)

        'Get file path and ensure that exists
        If fso.FolderExists(dataBasesFilePath) Then
            'Load data and create a list
            Dim databaseFiles As String() = Directory.GetFiles(dataBasesFilePath, "*.txt", SearchOption.TopDirectoryOnly)
            For fileIndex As Integer = 0 To databaseFiles.Length - 1
                databases.Add(GetOnlyFileName(databaseFiles(fileIndex)))
            Next
        End If

        'Get array and sort it
        Dim dataBasesArray As String() = databases.ToArray
        Array.Sort(dataBasesArray)

        Return dataBasesArray
    End Function

    '*===============================================================================================
    '* COMBOBOXES FUNCTIONS
    '*===============================================================================================
    Friend Sub AddProjectLanguagesToCombo(comboboxToModify As ComboBox)
        'Get project languages
        If Dir(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master\Speech"), FileAttribute.Directory) IsNot "" Then
            Dim languages As String() = Directory.GetDirectories(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master\Speech"))
            'Previous Selected Index
            Dim comboPrevSelection As String = comboboxToModify.SelectedItem
            'Add folders to combobox
            comboboxToModify.BeginUpdate()
            comboboxToModify.Items.Clear()
            For index As Integer = 0 To languages.Length - 1
                Dim folderName As String = Trim(StrConv(New DirectoryInfo(languages(index)).Name, vbProperCase))
                If Array.IndexOf(SfxLanguages, folderName) <> -1 Then
                    comboboxToModify.Items.Add(folderName)
                End If
            Next
            'Keep the previous selection
            If comboPrevSelection IsNot Nothing Then
                If comboboxToModify.Items.Contains(comboPrevSelection) Then
                    comboboxToModify.SelectedItem = comboPrevSelection
                End If
            End If
            comboboxToModify.EndUpdate()
        End If
    End Sub

    Friend Sub AddAvailableFormatsToCombobox(comboboxToModify As ComboBox)
        'Previous Selected Index
        Dim comboPrevSelection As String = comboboxToModify.SelectedItem
        'Add all formats to combobox
        comboboxToModify.BeginUpdate()
        comboboxToModify.Items.Clear()
        comboboxToModify.Items.AddRange(ProjectSettingsFile.sampleRateFormats.Keys.ToArray)
        'Select first item
        If comboboxToModify.Items.Count > 0 AndAlso MainFrame.ComboBox_Format.SelectedIndex = -1 Then
            comboboxToModify.SelectedIndex = 0
        End If
        'Keep the previous selection
        If comboPrevSelection IsNot Nothing Then
            If comboboxToModify.Items.Contains(comboPrevSelection) Then
                comboboxToModify.SelectedItem = comboPrevSelection
            End If
        End If
        comboboxToModify.EndUpdate()
    End Sub

    '*===============================================================================================
    '* SOX FUNCTIONS
    '*===============================================================================================
    Friend Sub ReSampleWithSox(inputFile As String, outputFile As String, currentFrequency As Integer, destinationFrequency As Integer)
        'Check if we have to resample or not
        If destinationFrequency < currentFrequency Then
            RunProcess("SystemFiles\Sox.exe", """" & inputFile & """ -r " & destinationFrequency & " """ & outputFile & """  resample -qs 0.97")
        Else
            fso.CopyFile(inputFile, outputFile)
        End If
    End Sub

    Friend Sub ReSampleAndSplitWithSox(inputFile As String, outputLFilePath As String, outputRFilePath As String, destinationFrequency As String)
        'Get Current Sample Rate
        Dim currentFrequency As Integer
        Using waveReader As New WaveFileReader(inputFile)
            currentFrequency = waveReader.WaveFormat.SampleRate
        End Using
        'Split and ReSample channels
        If destinationFrequency < currentFrequency Then
            RunProcess("SystemFiles\Sox.exe", """" & inputFile & """ -c 1 -r 32000 """ & outputLFilePath & """ resample -qs 0.97 avg -l")
            RunProcess("SystemFiles\Sox.exe", """" & inputFile & """ -c 1 -r 32000 """ & outputRFilePath & """ resample -qs 0.97 avg -r")
        Else
            RunProcess("SystemFiles\Sox.exe", """" & inputFile & """ -c 1 """ & outputLFilePath & """ avg -l")
            RunProcess("SystemFiles\Sox.exe", """" & inputFile & """ -c 1 """ & outputRFilePath & """ avg -r")
        End If
    End Sub
End Module

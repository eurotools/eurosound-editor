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
        If File.Exists(EuroSoundIniFilePath) Then
            Dim baseIniFunctions As New IniFile(EuroSoundIniFilePath)
            WorkingDirectory = baseIniFunctions.Read("Last_Project_Opened", "Form1_Misc")
            SysFileSamples = Path.Combine(WorkingDirectory, "System", "Samples.txt")
            SysFileProperties = Path.Combine(WorkingDirectory, "System", "Properties.txt")
            ProjAudioEditor = baseIniFunctions.Read("Edit_Wavs_With", "Form7_Misc")
            SysFileSfxDefaults = Path.Combine(WorkingDirectory, "System", "SFX Defaults.txt")
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
            mainform.RadioButton_AllBanksSelectedFormat.Checked = iniFunctions.Read("AllBanksOption_Value", "Form1_Misc").Equals("True", StringComparison.OrdinalIgnoreCase)
            mainform.RadioButton_Output_SelectedSoundBank.Checked = iniFunctions.Read("SelectedlBankOption_Value", "Form1_Misc").Equals("True", StringComparison.OrdinalIgnoreCase)
            mainform.RadioButton_Output_AllBanksAll.Checked = iniFunctions.Read("AllFormatsOption_Value", "Form1_Misc").Equals("True", StringComparison.OrdinalIgnoreCase)
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
            tempVar = iniFunctions.Read("PlayStationSize", "PropertiesForm")
            If IsNumeric(tempVar) Then
                SoundBankMaxPlayStation = CInt(tempVar)
            End If
            tempVar = iniFunctions.Read("PCSize", "PropertiesForm")
            If IsNumeric(tempVar) Then
                SoundBankMaxPC = CInt(tempVar)
            End If
            tempVar = iniFunctions.Read("GameCubeSize", "PropertiesForm")
            If IsNumeric(tempVar) Then
                SoundBankMaxGameCube = CInt(tempVar)
            End If
            tempVar = iniFunctions.Read("XBoxSize", "PropertiesForm")
            If IsNumeric(tempVar) Then
                SoundBankMaxXbox = CInt(tempVar)
            End If

            'Other Settings
            tempVar = iniFunctions.Read("Prefix_HT_Sound", "PropertiesForm")
            If IsNumeric(tempVar) Then
                ProjectSettingsFile.MiscProps.PrefixHtSound = CBool(tempVar)
            End If
            tempVar = iniFunctions.Read("ViewOutputDos", "PropertiesForm")
            If IsNumeric(tempVar) Then
                ProjectSettingsFile.MiscProps.ViewOutputDos = CBool(tempVar)
            End If
        End If
    End Sub

    '*===============================================================================================
    '* UPDATE VARIABLES, PROJECT SYSTEM FILES
    '*===============================================================================================
    Friend Sub UpdateGlobalVariables()
        'Update System Files variables
        SysFileSamples = Path.Combine(WorkingDirectory, "System", "Samples.txt")
        SysFileProperties = Path.Combine(WorkingDirectory, "System", "Properties.txt")
        SysFileSfxDefaults = Path.Combine(WorkingDirectory, "System", "SFX Defaults.txt")
        SysFileProjectIniPath = Path.Combine(WorkingDirectory, "System", "EuroSound.ini")
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

        'Check folders
        CheckProjectFolders(WorkingDirectory)

        'Get All SoundBanks
        Dim soundBanksFilePath As String = Path.Combine(WorkingDirectory, "SoundBanks")
        Dim availableSoundBanks As String() = GetFolderFiles(soundBanksFilePath)
        LoadSoundBanks(availableSoundBanks, mainform)

        'Update counter label
        mainform.Label_SoundBanksCount.Text = "Total: " & mainform.TreeView_SoundBanks.Nodes.Count

        'Get All DataBases
        Dim databasesFilePath As String = Path.Combine(WorkingDirectory, "DataBases")
        Dim availableDataBases As String() = GetFolderFiles(databasesFilePath)
        mainform.ListBox_DataBases.BeginUpdate()
        mainform.ListBox_DataBases.Items.AddRange(availableDataBases)
        mainform.ListBox_DataBases.EndUpdate()
        mainform.Label_DataBasesCount.Text = "Total: " & mainform.ListBox_DataBases.Items.Count

        'Create project file
        Dim temporalFile As String = Path.Combine(WorkingDirectory, "System", "TempFileName.txt")
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
    '* GENERIC FUNCTIONS
    '*===============================================================================================
    Private Function GetFolderFiles(folderToInspect As String) As String()
        Dim folderFiles As New List(Of String)

        'Get file path and ensure that exists
        If Directory.Exists(folderToInspect) Then
            'Load data and create a list
            Dim soundBankFiles As String() = Directory.GetFiles(folderToInspect, "*.txt", SearchOption.TopDirectoryOnly)
            For fileIndex As Integer = 0 To soundBankFiles.Length - 1
                folderFiles.Add(Path.GetFileNameWithoutExtension(soundBankFiles(fileIndex)))
            Next
        End If

        'Get array and sort it
        Dim soundBanksArray As String() = folderFiles.ToArray
        Array.Sort(soundBanksArray)

        Return soundBanksArray
    End Function

    '*===============================================================================================
    '* CHECK PROJECT FOLDERS
    '*===============================================================================================
    Friend Sub CheckProjectFolders(projectFolderPath As String)
        'Create folders
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "DataBases"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "Debug_Report", "ForES2", "MarkerFileData"))

        Directory.CreateDirectory(Path.Combine(projectFolderPath, "Reverbs"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "SoundBanks"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "System"))

        'SFXs Folders
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "SFXs"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "SFXs", "PC"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "SFXs", "PlayStation2"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "SFXs", "X Box"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "SFXs", "GameCube"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "SFXs", "Misc"))

        'Output folders
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "TempOutputFolder", "GameCube", "SoundBanks"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "TempOutputFolder", "PC", "SoundBanks"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "TempOutputFolder", "PlayStation2", "SoundBanks"))
        Directory.CreateDirectory(Path.Combine(projectFolderPath, "TempOutputFolder", "X Box", "SoundBanks"))
    End Sub

    '*===============================================================================================
    '* LOAD SOUNDBANKS
    '*===============================================================================================
    Private Sub LoadSoundBanks(availableSoundBanks As String(), mainform As MainFrame)
        mainform.TreeView_SoundBanks.BeginUpdate()
        For fileIndex As Integer = 0 To availableSoundBanks.Length - 1
            Dim soundBankFileName As String = availableSoundBanks(fileIndex).Trim
            Dim soundBankFilePath As String = Path.Combine(WorkingDirectory, "SoundBanks", soundBankFileName & ".txt")
            Dim soundBankFileData As SoundbankFile = textFileReaders.ReadSoundBankFile(soundBankFilePath)

            'Create SoundBank node
            Dim parentNode As TreeNode = mainform.TreeView_SoundBanks.Nodes.Add(CStr(soundBankFileData.HashCode), soundBankFileName, 0, 0)
            For dataBaseIndex As Integer = 0 To soundBankFileData.Dependencies.Length - 1
                Dim dataBaseName As String = soundBankFileData.Dependencies(dataBaseIndex).Trim
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
    '* COMBOBOXES FUNCTIONS
    '*===============================================================================================
    Friend Sub AddProjectLanguagesToCombo(comboboxToModify As ComboBox)
        'Get project languages
        Dim samplesFolder As String = ProjectSettingsFile.MiscProps.SampleFileFolder
        If samplesFolder > "" AndAlso Directory.Exists(Path.Combine(samplesFolder, "Master", "Speech")) Then
            Dim languages As String() = Directory.GetDirectories(Path.Combine(samplesFolder, "Master", "Speech"))

            'Previous Selected Index
            Dim comboPrevSelection As String = comboboxToModify.SelectedItem

            'Add folders to combobox
            comboboxToModify.BeginUpdate()
            comboboxToModify.Items.Clear()
            For index As Integer = 0 To languages.Length - 1
                Dim folderName As String = StrConv(New DirectoryInfo(languages(index)).Name, vbProperCase).Trim
                If Array.FindIndex(SfxLanguages, Function(t) t.Equals(folderName, StringComparison.OrdinalIgnoreCase)) <> -1 Then
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
    Friend Sub ReSampleWithSox(inputFile As String, outputFile As String, currentFrequency As Integer, destinationFrequency As Integer, effect As String)
        'Check if we have to resample or not
        If destinationFrequency >= currentFrequency Then
            RunConsoleProcess("SystemFiles\Sox.exe", """" & inputFile & """ """ & outputFile & """")
        Else
            RunConsoleProcess("SystemFiles\Sox.exe", """" & inputFile & """ -r " & destinationFrequency & " """ & outputFile & """ " & effect)
        End If
    End Sub

    Friend Sub ReSampleAndSplitWithSox(inputFile As String, outputLFilePath As String, outputRFilePath As String, destinationFrequency As String)
        'Get Current Sample Rate
        Dim currentFrequency As Integer
        Using waveReader As New WaveFileReader(inputFile)
            currentFrequency = waveReader.WaveFormat.SampleRate
        End Using

        'Split and ReSample channels
        If destinationFrequency >= currentFrequency Then
            RunConsoleProcess("SystemFiles\Sox.exe", """" & inputFile & """ -c 1 """ & outputLFilePath & """ avg -l")
            RunConsoleProcess("SystemFiles\Sox.exe", """" & inputFile & """ -c 1 """ & outputRFilePath & """ avg -r")
        Else
            RunConsoleProcess("SystemFiles\Sox.exe", """" & inputFile & """ -c 1 -r 32000 """ & outputLFilePath & """ resample -qs 0.97 avg -l")
            RunConsoleProcess("SystemFiles\Sox.exe", """" & inputFile & """ -c 1 -r 32000 """ & outputRFilePath & """ resample -qs 0.97 avg -r")
        End If
    End Sub
End Module

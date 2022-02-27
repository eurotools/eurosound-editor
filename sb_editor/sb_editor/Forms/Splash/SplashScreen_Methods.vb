Imports IniFileFunctions

Partial Public NotInheritable Class SplashScreen
    '*===============================================================================================
    '* INI FILE FUNCTIONS
    '*===============================================================================================
    Private Sub LoadIniData()
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
    '* PROJECT DATA FUNCTIONS
    '*===============================================================================================
    Private Sub LoadProjectData()
        'Ensure that the folder really exists
        WorkingDirectory = WorkingDirectory
        'Update project variables
        Dim propsFilePath As String = fso.BuildPath(WorkingDirectory, "System\Properties.txt")
        If fso.FileExists(propsFilePath) Then
            ProjectSettingsFile = textFileReaders.ReadPropertiesFile(propsFilePath)
        End If
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
        'Create refine search file if required
        Dim refineSearchTextFile As String = fso.BuildPath(WorkingDirectory, "System\RefineSearch.txt")
        If Not fso.FileExists(refineSearchTextFile) Then
            If fso.FolderExists(fso.BuildPath(WorkingDirectory, "System")) Then
                writers.CreateRefineList(refineSearchTextFile, Nothing)
            End If
        End If
        'Check folders
        Dim musicFolder = fso.BuildPath(WorkingDirectory, "Music")
        Dim esDataFolder = fso.BuildPath(WorkingDirectory, "Music\ESData")
        Dim EsWorkFolder = fso.BuildPath(WorkingDirectory, "Music\ESWork")
        If Not fso.FolderExists(musicFolder) Then
            fso.CreateFolder(musicFolder)
        End If
        If Not fso.FolderExists(esDataFolder) Then
            fso.CreateFolder(esDataFolder)
        End If
        If Not fso.FolderExists(EsWorkFolder) Then
            fso.CreateFolder(EsWorkFolder)
        End If
        'Ask for UserName if required
        If StrComp(EuroSoundUser, "") = 0 Then
            EuroSoundUser = AskForUserName("MyName")
        End If
        'Update EuroSound Ini
        Dim programIni As New IniFile(EuroSoundIniFilePath)
        programIni.Write("Last_Project_Opened", WorkingDirectory, "Form1_Misc")
    End Sub

    Private Sub CheckProjectFiles()
        Dim projectFilePath As String = fso.BuildPath(WorkingDirectory, "Project.txt")
        If fso.FileExists(projectFilePath) Then
            Dim projectData As ProjectFile = textFileReaders.ReadProjectFile(projectFilePath)
            'Check Soundbanks
            For index As Integer = 0 To projectData.SoundBankList.Count - 1
                Dim soundbankName As String = projectData.SoundBankList(index)
                Dim soundbankFilePath As String = fso.BuildPath(WorkingDirectory, "SoundBanks\" & soundbankName & ".txt")
                If Not fso.FileExists(soundbankFilePath) Then
                    'MsgBox("File not found: " & soundbankFilePath, vbOKOnly + vbCritical, "EuroSound Load Project Error")
                    Close()
                End If
            Next
            'Check Databases
            For index As Integer = 0 To projectData.DataBaseList.Count - 1
                Dim databaseName As String = projectData.DataBaseList(index)
                Dim databaseFilePath As String = fso.BuildPath(WorkingDirectory, "Databases\" & databaseName & ".txt")
                If Not fso.FileExists(databaseFilePath) Then
                    'MsgBox("File not found: " & databaseFilePath, vbOKOnly + vbCritical, "EuroSound Load Project Error")
                    Close()
                End If
            Next
            'Check SFXs
            For index As Integer = 0 To projectData.SFXList.Count - 1
                Dim sfxName As String = projectData.SFXList(index)
                Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory, "SFXs\" & sfxName & ".txt")
                If Not fso.FileExists(sfxFilePath) Then
                    'MsgBox("File not found: " & sfxFilePath, vbOKOnly + vbCritical, "EuroSound Load Project Error")
                    Close()
                End If
            Next
        End If
    End Sub
End Class

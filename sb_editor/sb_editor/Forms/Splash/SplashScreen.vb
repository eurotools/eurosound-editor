Imports System.IO
Imports RecentFilesMenu
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

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
    Private Sub SplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label_Version.Text = "Version: " & My.Application.Info.Version.Major & My.Application.Info.Version.Minor
    End Sub

    Private Sub SplashScreen_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Load ini file data
        LoadSystemFilesIni()

        'Inform user if the working directory exists
        If Not String.IsNullOrEmpty(WorkingDirectory) Then
            Dim projectFilePath As String = Path.Combine(WorkingDirectory, "Project.txt")
            If File.Exists(projectFilePath) Then
                'Load misc properties
                Dim miscFilePath As String = Path.Combine(WorkingDirectory, "System", "Misc.txt")
                Dim projectVersionSrting = textFileReaders.ReadMiscFile(miscFilePath)

                'Compare current version with the project version
                Dim projectVersion As New Version(projectVersionSrting)
                Dim currentVersion As New Version(My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
                If currentVersion.CompareTo(projectVersion) = 0 Or currentVersion.CompareTo(projectVersion) = 1 Then
                    'Update project variables
                    Dim propsFilePath As String = Path.Combine(WorkingDirectory, "System\Properties.txt")
                    If File.Exists(propsFilePath) Then
                        ProjectSettingsFile = textFileReaders.ReadPropertiesFile(propsFilePath)
                    End If

                    'Load Project Data
                    LoadProjectData(mainform, projectFilePath)

                    'Update all variables 
                    UpdateGlobalVariables()

                    'Update misc file
                    writers.UpdateMiscFile(miscFilePath)

                    'Ask for UserName if required
                    If String.IsNullOrEmpty(EuroSoundUser) Then
                        EuroSoundUser = AskForUserName("MyName")
                    End If

                    'Update GUI
                    mainform.Text = "EuroSound: """ & WorkingDirectory & """"
                    LoadProgramLastState(mainform)

                    'Create refine search file if required
                    Dim refineSearchTextFile As String = Path.Combine(WorkingDirectory, "System", "RefineSearch.txt")
                    If Not File.Exists(refineSearchTextFile) Then
                        If Directory.Exists(Path.Combine(WorkingDirectory, "System")) Then
                            writers.CreateRefineList(refineSearchTextFile, Nothing)
                        End If
                    End If
                Else
                    mainform.GroupBox_SoundBanks.Enabled = False
                    mainform.GroupBox_AvailableDataBases.Enabled = False
                    mainform.GroupBox_SFXsInDataBase.Enabled = False
                    mainform.GroupBox_AvailableSFXs.Enabled = False
                    mainform.GroupBox_Misc.Enabled = False
                    MsgBox("EuroSound out of Date for Project:" & vbCrLf & vbCrLf & "EuroSound Version: " & currentVersion.ToString & vbCrLf & "Project Version: " & projectVersion.ToString & vbCrLf & vbCrLf & "Must Get Latest EuroSound to Load this project!", vbOKOnly + vbCritical, "EuroSound out of Date!")
                End If
            Else
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
                MsgBox("Project Not Found " & WorkingDirectory, vbOKOnly + vbCritical, "EuroSound Load Project Error")
                Close()
            End If
        End If

        'Custom cursors
        mainform.Button_AddDataBases.Cursor = New Cursor(New MemoryStream(My.Resources.arrow_left))
        mainform.Button_RemoveSFXs.Cursor = New Cursor(New MemoryStream(My.Resources.arrow_right))
        mainform.Button_AddSFXs.Cursor = New Cursor(New MemoryStream(My.Resources.arrow_left))
        mainform.ComboBox_OutputLanguage.Cursor = New Cursor(New MemoryStream(My.Resources.lang_english))
        'Get recent files
        mainform.RecentFilesMenu = New MruStripMenuInline(mainform.MenuItemFile_RecentProjects, mainform.MenuItemFile_RecentFiles, New MostRecentFilesMenu.ClickedHandler(AddressOf mainform.MenuItemFile_Recent_Click), EuroSoundIniFilePath, 5)
        mainform.RecentFilesMenu.LoadFromIniFile()

        'Start timer
        TimerSplash.Start()
    End Sub

    Private Sub TimerSplash_Tick(sender As Object, e As EventArgs) Handles TimerSplash.Tick
        Close()
    End Sub
End Class

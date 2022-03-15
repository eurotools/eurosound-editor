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
    Private Sub SplashScreen_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Load ini file data
        LoadSystemFilesIni()
        'Inform user if the working directory exists
        Dim projectFilePath As String = fso.BuildPath(WorkingDirectory, "Project.txt")
        If fso.FileExists(projectFilePath) Then
            'Update project variables
            Dim propsFilePath As String = fso.BuildPath(WorkingDirectory, "System\Properties.txt")
            If fso.FileExists(propsFilePath) Then
                ProjectSettingsFile = textFileReaders.ReadPropertiesFile(propsFilePath)
            End If

            'Load Project Data
            LoadProjectData(mainform, projectFilePath)

            'Update all variables 
            UpdateGlobalVariables()

            'Ask for UserName if required
            If StrComp(EuroSoundUser, "") = 0 Then
                EuroSoundUser = AskForUserName("MyName")
            End If

            'Update GUI
            mainform.Text = "EuroSound: """ & WorkingDirectory & """"
            LoadProgramLastState(mainform)

            'Create refine search file if required
            Dim refineSearchTextFile As String = fso.BuildPath(WorkingDirectory, "System\RefineSearch.txt")
            If Not fso.FileExists(refineSearchTextFile) Then
                If fso.FolderExists(fso.BuildPath(WorkingDirectory, "System")) Then
                    writers.CreateRefineList(refineSearchTextFile, Nothing)
                End If
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            MsgBox("Project Not Found " & WorkingDirectory, vbOKOnly + vbCritical, "EuroSound Load Project Error")
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

        'Start timer
        TimerSplash.Start()
    End Sub

    Private Sub TimerSplash_Tick(sender As Object, e As EventArgs) Handles TimerSplash.Tick
        Close()
    End Sub
End Class

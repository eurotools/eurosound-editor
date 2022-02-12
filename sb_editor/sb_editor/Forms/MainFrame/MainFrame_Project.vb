Imports System.IO

Partial Public Class MainFrame
    '*===============================================================================================
    '* LOAD PROJECT AND UPDATE GUI CONTROLS
    '*===============================================================================================
    Private Sub LoadProject(projectFilePath As String)
        If fso.FileExists(projectFilePath) Then
            'Update GUI
            RecentFilesMenu.AddFile(WorkingDirectory)
            RecentFilesMenu.SaveToIniFile()
        Else
            MsgBox("Project Not Found" & WorkingDirectory, vbOKOnly + vbCritical, "EuroSound Load Project Error")
        End If
    End Sub

    '*===============================================================================================
    '* CREATE NEW PROJECT
    '*===============================================================================================
    Private Sub CreateNewProject(selectedProjectPath As String)
        'Get folder paths
        Dim DataBasesFolder As String = fso.BuildPath(selectedProjectPath, "DataBases")
        Dim SoundbanksFolder As String = fso.BuildPath(selectedProjectPath, "SoundBanks")
        Dim SystemFolder As String = fso.BuildPath(selectedProjectPath, "System")
        'SFXs Folders
        Dim SfxFolder As String = fso.BuildPath(selectedProjectPath, "SFXs")
        'Inform user
        Dim foldersString As String = DataBasesFolder & vbNewLine & SfxFolder & vbNewLine & SoundbanksFolder & vbNewLine & SystemFolder & vbNewLine & vbNewLine
        Dim createProject As MsgBoxResult = MsgBox("This will create the following folders:" & vbNewLine & foldersString & "Proceed?", vbOKCancel + vbQuestion, "Create New Project?")
        'Continue with the project creation
        If createProject = MsgBoxResult.Ok Then
            'Update global variables
            WorkingDirectory = selectedProjectPath
            SysFileSamples = fso.BuildPath(WorkingDirectory, "System\Samples.txt")
            SysFileProperties = fso.BuildPath(WorkingDirectory, "System\Properties.txt")
            SysFileProjectIniPath = fso.BuildPath(WorkingDirectory, "System\EuroSound.ini")
            SysFileSfxDefaults = fso.BuildPath(WorkingDirectory, "System\SFX Defaults.txt")
            'Create folders
            CreateFolderIfNotExists(DataBasesFolder)
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "Debug_Report\ForES2\MarkerFileData"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "Music\ESData"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "Music\ESWork"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "Reverbs"))
            CreateFolderIfNotExists(SoundbanksFolder)
            CreateFolderIfNotExists(SystemFolder)
            'SFXs Folders
            CreateFolderIfNotExists(SfxFolder)
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "SFXs\PC"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "SFXs\PlayStation2"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "SFXs\X Box"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "SFXs\GameCube"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "SFXs\Misc"))
            'Output folders
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "TempOutputFolder\GameCube\SoundBanks"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "TempOutputFolder\PC\SoundBanks"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "TempOutputFolder\PlayStation2\SoundBanks"))
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "TempOutputFolder\X Box\SoundBanks"))
            'Master folder
            CreateFolderIfNotExists(fso.BuildPath(WorkingDirectory, "Master\Speech\English"))
            'Create Project files
            writers.CreateEmptyProjectFile(fso.BuildPath(WorkingDirectory, "Project.txt"))
            'Create System Files
            writers.CreateEmptyMiscFile(fso.BuildPath(WorkingDirectory, "System\Misc.txt"))
            writers.CreateEmptyPropertiesFile(SysFileProperties)
            writers.CreateEmptySfxDefaults(fso.BuildPath(WorkingDirectory, "System\SFX Defaults.txt"))
            'Ask for UserName
            If StrComp(EuroSoundUser, "") = 0 Then
                Dim projectPropsForm As New Project_Properties
                projectPropsForm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub CreateFolderIfNotExists(folderPath As String)
        If Not fso.FolderExists(folderPath) Then
            MkDir(folderPath)
        End If
    End Sub
End Class

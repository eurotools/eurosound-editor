Imports IniFileFunctions

Partial Public Class MainFrame
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
            'Create folders
            CreateFolderIfNotExists(DataBasesFolder)
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "Debug_Report\ForES2\MarkerFileData"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "Music\ESData"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "Music\ESWork"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "Reverbs"))
            CreateFolderIfNotExists(SoundbanksFolder)
            CreateFolderIfNotExists(SystemFolder)
            'SFXs Folders
            CreateFolderIfNotExists(SfxFolder)
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "SFXs\PC"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "SFXs\PlayStation2"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "SFXs\X Box"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "SFXs\GameCube"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "SFXs\Misc"))
            'Output folders
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "TempOutputFolder\GameCube\SoundBanks"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "TempOutputFolder\PC\SoundBanks"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "TempOutputFolder\PlayStation2\SoundBanks"))
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "TempOutputFolder\X Box\SoundBanks"))
            'Master folder
            CreateFolderIfNotExists(fso.BuildPath(selectedProjectPath, "Master\Speech\English"))
            'Create Project files
            writers.CreateEmptyProjectFile(fso.BuildPath(selectedProjectPath, "Project.txt"))
            'Create System Files
            writers.CreateEmptyMiscFile(fso.BuildPath(selectedProjectPath, "System\Misc.txt"))
            writers.CreateEmptyPropertiesFile(fso.BuildPath(selectedProjectPath, "System\Properties.txt"))
            writers.CreateEmptySfxDefaults(fso.BuildPath(selectedProjectPath, "System\SFX Defaults.txt"))
            'Ask for UserName
            If StrComp(EuroSoundUser, "") = 0 Then
                Dim projectPropsForm As New Project_Properties
                projectPropsForm.ShowDialog()
            End If
            'Update ini file
            Dim baseIniFunctions As New IniFile(EuroSoundIniFilePath)
            baseIniFunctions.Write("Last_Project_Opened", selectedProjectPath, "Form1_Misc")
        End If
    End Sub

    Private Sub CreateFolderIfNotExists(folderPath As String)
        If Not fso.FolderExists(folderPath) Then
            MkDir(folderPath)
        End If
    End Sub
End Class

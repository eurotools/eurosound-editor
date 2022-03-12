Imports IniFileFunctions
Imports sb_editor.ParsersObjects

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
            CreateFolderIfRequired(DataBasesFolder)
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "Debug_Report\ForES2\MarkerFileData"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "Music\ESData"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "Music\ESWork"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "Reverbs"))
            CreateFolderIfRequired(SoundbanksFolder)
            CreateFolderIfRequired(SystemFolder)
            'SFXs Folders
            CreateFolderIfRequired(SfxFolder)
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "SFXs\PC"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "SFXs\PlayStation2"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "SFXs\X Box"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "SFXs\GameCube"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "SFXs\Misc"))
            'Output folders
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "TempOutputFolder\GameCube\SoundBanks"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "TempOutputFolder\PC\SoundBanks"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "TempOutputFolder\PlayStation2\SoundBanks"))
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "TempOutputFolder\X Box\SoundBanks"))
            'Master folder
            CreateFolderIfRequired(fso.BuildPath(selectedProjectPath, "Master\Speech\English"))
            'Create Project files
            writers.CreateProjectFile(fso.BuildPath(selectedProjectPath, "Project.txt"), Nothing, Nothing, Nothing)
            'Create System Files
            writers.UpdateMiscFile(fso.BuildPath(selectedProjectPath, "System\Misc.txt"), True)
            writers.CreateEmptySfxDefaults(fso.BuildPath(selectedProjectPath, "System\SFX Defaults.txt"))
            'Project file
            Dim newProjectFile As New PropertiesFile With {
                .AvailableReSampleRates = New List(Of String)({"Default"}),
                .AvailableFormats = New String(,) {}
            }
            newProjectFile.MiscProps.DefaultRate = 0
            newProjectFile.MiscProps.SampleFileFolder = Application.StartupPath
            writers.SavePropertiesFile(newProjectFile, fso.BuildPath(selectedProjectPath, "System\Properties.txt"))
            'Ask for UserName
            If StrComp(EuroSoundUser, "") = 0 Then
                Dim projectPropsForm As New Project_Properties(Me)
                projectPropsForm.ShowDialog()
            End If
            'Update ini file
            Dim baseIniFunctions As New IniFile(EuroSoundIniFilePath)
            baseIniFunctions.Write("Last_Project_Opened", selectedProjectPath, "Form1_Misc")
        End If
    End Sub
End Class

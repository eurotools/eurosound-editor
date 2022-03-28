Imports System.IO
Imports IniFileFunctions
Imports sb_editor.ParsersObjects

Partial Public Class MainFrame
    '*===============================================================================================
    '* CREATE NEW PROJECT
    '*===============================================================================================
    Private Sub CreateNewProject(selectedProjectPath As String)
        'Get folder paths
        Dim DataBasesFolder As String = Path.Combine(selectedProjectPath, "DataBases")
        Dim SoundbanksFolder As String = Path.Combine(selectedProjectPath, "SoundBanks")
        Dim SystemFolder As String = Path.Combine(selectedProjectPath, "System")
        'SFXs Folders
        Dim SfxFolder As String = Path.Combine(selectedProjectPath, "SFXs")
        'Inform user
        Dim foldersString As String = DataBasesFolder & vbNewLine & SfxFolder & vbNewLine & SoundbanksFolder & vbNewLine & SystemFolder & vbNewLine & vbNewLine
        Dim createProject As MsgBoxResult = MsgBox("This will create the following folders:" & vbNewLine & foldersString & "Proceed?", vbOKCancel + vbQuestion, "Create New Project?")
        If createProject = MsgBoxResult.Ok Then
            'Master folder
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "Master"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "Music", "ESData"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "Music", "ESWork"))

            'Create folders
            CheckProjectFolders(selectedProjectPath)

            'Create Project files
            writers.CreateProjectFile(Path.Combine(selectedProjectPath, "Project.txt"), Nothing, Nothing, Nothing)

            'Create System Files
            writers.UpdateMiscFile(Path.Combine(selectedProjectPath, "System", "Misc.txt"), True)
            writers.CreateEmptySfxDefaults(Path.Combine(selectedProjectPath, "System", "SFX Defaults.txt"))

            'Project file
            Dim newProjectFile As New PropertiesFile With {
                .AvailableReSampleRates = New List(Of String)({"Default"}),
                .AvailableFormats = New String(,) {}
            }
            newProjectFile.MiscProps.DefaultRate = 0
            newProjectFile.MiscProps.SampleFileFolder = Application.StartupPath
            writers.SavePropertiesFile(newProjectFile, Path.Combine(selectedProjectPath, "System", "Properties.txt"))

            'Ask for UserName
            If StrComp(EuroSoundUser, "") = 0 Then
                AskForUserName("MyName")
                RestartEuroSound()
            End If

            'Update ini file
            Dim baseIniFunctions As New IniFile(EuroSoundIniFilePath)
            baseIniFunctions.Write("Last_Project_Opened", selectedProjectPath, "Form1_Misc")
        End If
    End Sub
End Class

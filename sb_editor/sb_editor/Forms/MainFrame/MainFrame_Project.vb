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
        'Continue with the project creation
        If createProject = MsgBoxResult.Ok Then
            'Create folders
            Directory.CreateDirectory(DataBasesFolder)
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "Debug_Report", "ForES2", "MarkerFileData"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "Music", "ESData"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "Music", "ESWork"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "Reverbs"))
            Directory.CreateDirectory(SoundbanksFolder)
            Directory.CreateDirectory(SystemFolder)
            'SFXs Folders
            Directory.CreateDirectory(SfxFolder)
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "SFXs", "PC"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "SFXs", "PlayStation2"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "SFXs", "X Box"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "SFXs", "GameCube"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "SFXs", "Misc"))
            'Output folders
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "TempOutputFolder", "GameCube", "SoundBanks"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "TempOutputFolder", "PC", "SoundBanks"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "TempOutputFolder", "PlayStation2", "SoundBanks"))
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "TempOutputFolder", "X Box", "SoundBanks"))
            'Master folder
            Directory.CreateDirectory(Path.Combine(selectedProjectPath, "Master", "Speech", "English"))
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
                Dim projectPropsForm As New Project_Properties(Me)
                projectPropsForm.ShowDialog()
            End If
            'Update ini file
            Dim baseIniFunctions As New IniFile(EuroSoundIniFilePath)
            baseIniFunctions.Write("Last_Project_Opened", selectedProjectPath, "Form1_Misc")
        End If
    End Sub
End Class

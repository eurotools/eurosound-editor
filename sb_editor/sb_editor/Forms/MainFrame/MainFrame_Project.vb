Imports System.IO

Partial Public Class MainFrame
    '*===============================================================================================
    '* LOAD PROJECT AND UPDATE GUI CONTROLS
    '*===============================================================================================
    Private Sub LoadProject(projectFilePath As String)
        If fso.FileExists(projectFilePath) Then
            'Update GUI
            Text = "EuroSound: """ & WorkingDirectory & """"
            RecentFilesMenu.AddFile(WorkingDirectory)
            RecentFilesMenu.SaveToIniFile()
            LoadSoundbanks(fso.BuildPath(WorkingDirectory, "Soundbanks"))
            LoadDataBases(fso.BuildPath(WorkingDirectory, "DataBases"))
            UserControl_SFXs.LoadHashCodes()
            UserControl_SFXs.LoadRefineList()
            'Update Project file
            writers.CreateProjectFile(fso.BuildPath(WorkingDirectory, "Project.txt"), TreeView_SoundBanks, ListBox_DataBases, UserControl_SFXs.ListBox_SFXs)
            'Load Languages
            AddProjectLanguagesToCombo()
        Else
            MsgBox("Project Not Found" & WorkingDirectory, vbOKOnly + vbCritical, "EuroSound Load Project Error")
        End If
    End Sub

    Private Sub AddProjectLanguagesToCombo()
        'Get project languages
        If Dir(fso.BuildPath(ProjMasterFolder, "Master\Speech"), FileAttribute.Directory) IsNot "" Then
            Dim languages As String() = Directory.GetDirectories(fso.BuildPath(ProjMasterFolder, "Master\Speech"))
            'Add folders to combobox
            ComboBox_OutputLanguage.BeginUpdate()
            For index As Integer = 0 To languages.Length - 1
                ComboBox_OutputLanguage.Items.Add(New DirectoryInfo(languages(index)).Name)
            Next
            ComboBox_OutputLanguage.EndUpdate()
            'Select the first one by default
            If languages.Length > 0 Then
                ComboBox_OutputLanguage.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub LoadDataBases(databasesFolder As String)
        'Get txt files from the folder
        Dim databaseFiles As String() = Directory.GetFiles(databasesFolder, "*.txt", SearchOption.TopDirectoryOnly)
        'Add items to listbox
        ListBox_DataBases.BeginUpdate()
        For i = 0 To databaseFiles.Length - 1
            Dim DataBaseName = GetOnlyFileName(databaseFiles(i))
            ListBox_DataBases.Items.Add(DataBaseName)
        Next
        Label_DataBasesCount.Text = "Total: " & ListBox_DataBases.Items.Count
        'Disable listbox update mode
        ListBox_DataBases.EndUpdate()
    End Sub

    Private Sub LoadSoundbanks(projectFolder As String)
        'Get text files full paths
        Dim soundBankFiles As String() = Directory.GetFiles(projectFolder, "*.txt", SearchOption.TopDirectoryOnly)
        'Add items to tree view
        TreeView_SoundBanks.BeginUpdate()
        For i As Integer = 0 To soundBankFiles.Length - 1
            Dim soundbankData As SoundbankFile = textFileReaders.ReadSoundBankFile(soundBankFiles(i))
            Dim SoundBankNode As TreeNode = TreeView_SoundBanks.Nodes.Add(CStr(soundbankData.HashCode), GetOnlyFileName(soundBankFiles(i)), 0, 0)
            For index As Integer = 0 To soundbankData.Dependencies.Count - 1
                SoundBankNode.Nodes.Add(soundbankData.Dependencies(index), soundbankData.Dependencies(index), 2, 2)
            Next
            'Add empty node if required
            If SoundBankNode.Nodes.Count = 0 Then
                SoundBankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
            End If
        Next
        TreeView_SoundBanks.EndUpdate()
        'Update counter label
        Label_SoundBanksCount.Text = "Total: " & TreeView_SoundBanks.Nodes.Count
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
End Class

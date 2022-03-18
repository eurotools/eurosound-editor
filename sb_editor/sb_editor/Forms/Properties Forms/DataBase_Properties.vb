Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports Scripting

Public Class DataBase_Properties
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly dataBaseFilePath As String
    Private ReadOnly fileReaders As New FileParsers

    Sub New(databaseFile As String)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        dataBaseFilePath = databaseFile
    End Sub

    Private Sub DataBase_Properties_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Read database text file
        Dim dataBaseObj = fileReaders.ReadDataBaseFile(dataBaseFilePath)

        'File Info Section
        Label_Value_FirstCreated.Text = dataBaseObj.HeaderInfo.FirstCreated
        Label_CreatedBy_Value.Text = dataBaseObj.HeaderInfo.CreatedBy
        Label_Value_LastModified.Text = dataBaseObj.HeaderInfo.LastModify
        Label_ModifiedBy_Value.Text = dataBaseObj.HeaderInfo.LastModifyBy

        'Files Recount Section
        Label_DatabaseCount_Value.Text = CountFolderFiles(Path.Combine(WorkingDirectory, "Databases"), "*.txt")
        Label_SfxCount_Value.Text = CountFolderFiles(Path.Combine(WorkingDirectory, "SFXs"), "*.txt")

        'Show SFXs info
        Dim dataBaseName = Path.GetFileNameWithoutExtension(dataBaseFilePath)
        TextBox_DataBaseName.Text = dataBaseName
        ListBox_TotalSfx.Items.AddRange(dataBaseObj.Dependencies)
        Label_TotalSfx_Count.Text = "Total: " & ListBox_TotalSfx.Items.Count

        'Get sample folder
        Dim fso As New FileSystemObject
        Dim MasterFilePath = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")
        If Directory.Exists(MasterFilePath) Then
            Label_SampleCount_Value.Text = Directory.GetFiles(MasterFilePath, "*.wav", SearchOption.AllDirectories).Length
            Dim fold As Folder = fso.GetFolder(MasterFilePath)
            Label_Value_Size.Text = BytesStringFormat(fold.Size)
        End If

        'Get Soundbank dependencies
        GetSoundbankDependencies(dataBaseName)
        'Get samples
        GetSampleList()
    End Sub

    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        Close()
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Sub GetSoundbankDependencies(dataBaseName As String)
        'Get soundbank dependencies
        Dim soundbanksFolderPath As String = Path.Combine(WorkingDirectory, "SoundBanks")
        Dim soundbanksFiles As String() = Directory.GetFiles(soundbanksFolderPath, "*.txt", SearchOption.TopDirectoryOnly)

        'Check for this database in the soundbanks
        For fileIndex As Integer = 0 To soundbanksFiles.Length - 1
            Dim currentFile As String = soundbanksFiles(fileIndex)
            'Get Soundbank data
            Dim soundbankData As SoundbankFile = fileReaders.ReadSoundBankFile(currentFile)
            Dim soundbankDataBases As String() = soundbankData.Dependencies

            'Check if this database is included
            For index As Integer = 0 To soundbankDataBases.Count - 1
                Dim currentDataBase As String = soundbankDataBases(index)
                If StrComp(currentDataBase, dataBaseName) = 0 Then
                    ListBox_SoundBank_Dependencies.Items.Add(Path.GetFileNameWithoutExtension(currentFile))
                    Exit For
                End If
            Next
        Next




        'Update counter
        Label_SbDependencies_Value.Text = ListBox_SoundBank_Dependencies.Items.Count
        Label_TotalDependencies_Count.Text = "Total: " & ListBox_SoundBank_Dependencies.Items.Count
    End Sub

    Private Sub GetSampleList()
        'Get list of user Wav files
        Dim samplesList As New HashSet(Of String)
        For Each sfxName As String In ListBox_TotalSfx.Items
            Dim sfxFullFilePath As String = Path.Combine(WorkingDirectory, "SFXs", sfxName & ".txt")
            Dim sfxFileData As SfxFile = fileReaders.ReadSFXFile(sfxFullFilePath)
            For Each sampleData As Sample In sfxFileData.Samples
                'Add sample to list if not exists
                Dim sampleFullPath As String = UCase(Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master\" & sampleData.FilePath))
                samplesList.Add(sampleFullPath)
            Next
        Next

        'Update control
        ListBox_TotalSamples.BeginUpdate()
        ListBox_TotalSamples.Items.AddRange(samplesList.ToArray)
        ListBox_TotalSamples.EndUpdate()
        'Update label
        Label_TotalSamples_Count.Text = "Total: " & ListBox_TotalSamples.Items.Count
    End Sub
End Class
Imports System.IO
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
        Label_DatabaseCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "Databases"), "*.txt")
        Label_SfxCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "SFXs"), "*.txt")

        'Show SFXs info
        Dim dataBaseName = GetOnlyFileName(dataBaseFilePath)
        TextBox_DataBaseName.Text = dataBaseName
        ListBox_TotalSfx.Items.AddRange(dataBaseObj.Dependencies)
        Label_TotalSfx_Count.Text = "Total: " & ListBox_TotalSfx.Items.Count

        'Get sample folder
        Dim MasterFilePath = fso.BuildPath(ProjMasterFolder, "Master")
        If fso.FolderExists(MasterFilePath) Then
            Label_SampleCount_Value.Text = Directory.GetFiles(MasterFilePath, "*.wav", SearchOption.AllDirectories).Length
            Dim fold As Folder = fso.GetFolder(MasterFilePath)
            Label_Value_Size.Text = BytesStringFormat(fold.Size) & " (" & Format(fold.Size, "#,#") & " bytes)"
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
        Dim soundbanksFolderPath As String = fso.BuildPath(WorkingDirectory, "SoundBanks")
        Dim soundbanksFiles As Files = fso.GetFolder(soundbanksFolderPath).Files

        'Check for this database in the soundbanks
        For Each soundbankFile As Scripting.File In soundbanksFiles
            'Get Soundbank data
            Dim soundbankData As SoundbankFile = fileReaders.ReadSoundBankFile(soundbankFile.Path)
            Dim soundbankDataBases As String() = soundbankData.Dependencies

            'Check if this database is included
            For index As Integer = 0 To soundbankDataBases.Count - 1
                Dim currentDataBase As String = soundbankDataBases(index)
                If StrComp(currentDataBase, dataBaseName) = 0 Then
                    ListBox_SoundBank_Dependencies.Items.Add(GetOnlyFileName(soundbankFile.Name))
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
        Dim samplesList As New List(Of String)
        For Each sfxName As String In ListBox_TotalSfx.Items
            Dim sfxFullFilePath As String = fso.BuildPath(WorkingDirectory, "SFXs\" & sfxName & ".txt")
            Dim sfxFileData As SfxFile = fileReaders.ReadSFXFile(sfxFullFilePath)
            For Each sampleData As Sample In sfxFileData.Samples
                'Add sample to list if not exists
                Dim sampleFullPath As String = UCase(fso.BuildPath(ProjMasterFolder, "Master\" & sampleData.FilePath))
                If Not samplesList.Contains(sampleFullPath) Then
                    samplesList.Add(sampleFullPath)
                End If
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
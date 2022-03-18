Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports Scripting

Public Class SFX_Properties
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly sfxName As String
    Private ReadOnly sfxFullPath As String
    Private ReadOnly fileReaders As New FileParsers

    Public Sub New(sfxFileName As String, sfxFilePath As String)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        sfxName = sfxFileName
        sfxFullPath = sfxFilePath
    End Sub

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub SFX_Properties_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Get file info
        Textbox_SFX_Name.Text = sfxName

        'Get DataBase files
        Dim databaseFiles As String() = Directory.GetFiles(Path.Combine(WorkingDirectory, "Databases"), "*.txt", SearchOption.TopDirectoryOnly)
        For fileIndex As Integer = 0 To databaseFiles.Length - 1
            FileOpen(1, databaseFiles(fileIndex), OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                Dim currentLine As String = LineInput(1)
                If StrComp(currentLine, sfxName) = 0 Then
                    ListBox_DataBase_Dependencies.Items.Add(Path.GetFileNameWithoutExtension(databaseFiles(fileIndex)))
                    'Quit loop
                    Exit Do
                End If
            Loop
            FileClose(1)
        Next

        'Read SFX File
        Dim SfxFileData As SfxFile = fileReaders.ReadSFXFile(sfxFullPath)
        'Show header info
        Label_Value_FirstCreated.Text = SfxFileData.HeaderInfo.FirstCreated.Trim
        Label_CreatedBy_Value.Text = SfxFileData.HeaderInfo.CreatedBy.Trim
        Label_Value_LastModified.Text = SfxFileData.HeaderInfo.LastModify.Trim
        Label_ModifiedBy_Value.Text = SfxFileData.HeaderInfo.LastModifyBy.Trim
        'Add sample name
        Dim SamplesList As New List(Of String)
        For index As Integer = 0 To SfxFileData.Samples.Count - 1
            SamplesList.Add(UCase(Path.Combine(WorkingDirectory & "\Master", SfxFileData.Samples(index).FilePath.Trim)))
        Next
        ListBox_Samples.Items.AddRange(SamplesList.ToArray)

        'Update counters
        Textbox_DataBase_Deps.Text = ListBox_DataBase_Dependencies.Items.Count
        Label_DatabaseCount_Value.Text = CountFolderFiles(Path.Combine(WorkingDirectory, "Databases"), "*.txt")
        Label_SfxCount_Value.Text = CountFolderFiles(Path.Combine(WorkingDirectory, "SFXs"), "*.txt")

        'Get master path and count samples
        Dim fso As New FileSystemObject
        Dim MasterFilePath = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")
        If Directory.Exists(MasterFilePath) Then
            Label_SampleCount_Value.Text = Directory.GetFiles(MasterFilePath, "*.wav", SearchOption.AllDirectories).Length
            'Get sample folder size
            Dim fold As Folder = fso.GetFolder(MasterFilePath)
            Label_Value_Size.Text = BytesStringFormat(fold.Size)
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        Close()
    End Sub
End Class
Imports System.IO
Imports Scripting

Public Class SFX_Properties
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly sfxName As String
    Private ReadOnly sfxFullPath As String

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
        Dim databaseFiles As Files = fso.GetFolder(fso.BuildPath(WorkingDirectory, "Databases")).Files

        'Search for DataBase dependencies
        For Each dataBaseObj As Scripting.File In databaseFiles
            FileOpen(1, dataBaseObj.Path, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                Dim currentLine As String = LineInput(1)
                If StrComp(currentLine, sfxName) = 0 Then
                    ListBox_DataBase_Dependencies.Items.Add(Path.GetFileNameWithoutExtension(dataBaseObj.Name))
                    'Quit loop
                    Exit Do
                End If
            Loop
            FileClose(1)
        Next

        'Search for Samples
        Dim SamplesList As New List(Of String)
        FileOpen(1, sfxFullPath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            Dim currentLine As String = LineInput(1)

            'Header info
            If InStr(currentLine, "## ") = 1 Then
                'Split content
                Dim lineData As String() = Split(currentLine, "...")

                'Add data to controls
                If InStr(currentLine, "## First Created ...") = 1 Then
                    Label_Value_FirstCreated.Text = lineData(1).Trim
                End If
                If InStr(currentLine, "## Created By ...") = 1 Then
                    Label_CreatedBy_Value.Text = lineData(1).Trim
                End If
                If InStr(currentLine, "## Last Modified ...") = 1 Then
                    Label_Value_LastModified.Text = lineData(1).Trim
                End If
                If InStr(currentLine, "## Last Modified By ...") = 1 Then
                    Label_ModifiedBy_Value.Text = lineData(1).Trim
                End If
            End If

            'Sample pool section
            If InStr(currentLine, "#SFXSamplePoolFiles") Then
                'Read line
                currentLine = LineInput(1)
                Do
                    'Add item to list
                    SamplesList.Add(fso.BuildPath(WorkingDirectory, currentLine.Trim()))

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END") <> 0 AndAlso Not EOF(1)
            End If
        Loop
        FileClose(1)

        'Add item to listbox
        ListBox_Samples.Items.AddRange(SamplesList.ToArray)

        'Update database file dependencies counter
        Textbox_DataBase_Deps.Text = ListBox_DataBase_Dependencies.Items.Count

        'Update counters
        Label_DatabaseCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "Databases"), "*.txt")
        Label_SfxCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "SFXs"), "*.txt")

        'Get master path
        Dim MasterFilePath = fso.BuildPath(ProjMasterFolder, "Master")

        'Count samples
        If fso.FolderExists(MasterFilePath) Then
            Label_SampleCount_Value.Text = Directory.GetFiles(MasterFilePath, "*.wav", SearchOption.AllDirectories).Length
            'Get sample folder size
            Dim fold As Folder = fso.GetFolder(MasterFilePath)
            Label_Value_Size.Text = BytesStringFormat(fold.Size) & " (" & Format(fold.Size, "#,#") & " bytes)"
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        Close()
    End Sub
End Class
Imports System.IO
Imports Scripting

Public Class Soundbank_Properties
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly SoundbankFilePath As String
    Private ReadOnly SoundbankName As String
    Private ReadOnly SoundbankHashCode As Integer
    Private ReadOnly textFileReaders As New FileParsers()

    Public Sub New(sbFilePath As String, sbName As String, sbHashCode As Integer)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        SoundbankFilePath = sbFilePath
        SoundbankName = sbName
        SoundbankHashCode = sbHashCode
    End Sub

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub Soundbank_Properties_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put soundbank name
        GroupBox_SoundbankData.Text = SoundbankName

        'Calculate Output Filename
        Label_Value_OutFileName.Text = "HC" & Hex(SoundbankHashCode).PadLeft(6, "0"c) & ".SFX"

        'Read Soundbank File
        Dim SoundbankObj As SoundbankFile = textFileReaders.ReadSoundBankFile(SoundbankFilePath)

        'Add DataBases Sort and update counter
        ListBox_Databases.Items.AddRange(SoundbankObj.Dependencies)
        If ListBox_Databases.Items.Count > 0 Then
            'Update counter
            Label_DataBasesCount.Text = "DataBases: " & ListBox_Databases.Items.Count
            'Sort items
            ListBox_Databases.Sorted = True
        End If

        GetSfxList(SoundbankObj)
        GetSamples()

        'Update Controls
        Label_Value_FirstCreated.Text = SoundbankObj.HeaderInfo.FirstCreated
        Label_CreatedBy_Value.Text = SoundbankObj.HeaderInfo.CreatedBy
        Label_Value_LastModified.Text = SoundbankObj.HeaderInfo.LastModify
        Label_ModifiedBy_Value.Text = SoundbankObj.HeaderInfo.LastModifyBy

        'Update counters
        Label_DatabaseCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "Databases"), "*.txt")
        Label_SfxCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "SFXs"), "*.txt")

        'Ensure that the Master folder exists
        Dim MasterFilePath = fso.BuildPath(ProjMasterFolder, "Master")
        If fso.FolderExists(MasterFilePath) Then
            'Count folder files
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
        'Close Form
        Close()
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Sub GetSamples()
        'Get Samples
        Dim Samples As New List(Of String)
        For Each item As String In ListBox_SFXs.Items
            'Read SFX Dependencies
            Dim sfxFullPath As String = Path.Combine(WorkingDirectory, "SFXs", item & ".txt")

            'Open file
            Dim currentLine As String
            FileOpen(1, sfxFullPath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read text file
                currentLine = LineInput(1)

                'Read samples section
                If StrComp(currentLine, "#SFXSamplePoolFiles") = 0 Then
                    'Read line
                    currentLine = LineInput(1)
                    Do
                        'Get relative path
                        Dim waveRelativePath = currentLine

                        'Get Absolute Path
                        Dim waveFilePath As String = UCase(fso.BuildPath(ProjMasterFolder, "Master\" & waveRelativePath))

                        'Add item to list
                        If Not Samples.Contains(waveFilePath) Then
                            Samples.Add(waveFilePath)
                        End If

                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END") <> 0 AndAlso Not EOF(1)
                End If
            Loop
            FileClose(1)
        Next

        'Add data to listbox, sort, and update counter
        If Samples.Count > 0 Then
            'Add items
            ListBox_SamplesList.Items.AddRange(Samples.ToArray)

            'Update counter
            Label_TotalSamples.Text = "Samples: " & ListBox_SamplesList.Items.Count

            'Sort items
            ListBox_SamplesList.Sorted = True
        End If
    End Sub

    Private Sub GetSfxList(SoundbankObj As SoundbankFile)
        'Get used SFXs
        For Each item As String In SoundbankObj.Dependencies
            'Read SFX Dependencies
            Dim sfxFullPath As String = Path.Combine(WorkingDirectory, "DataBases", item & ".txt")
            Dim SFXs As String() = textFileReaders.ReadDataBaseFile(sfxFullPath).Dependencies

            'Add items to list
            For index As Integer = 0 To SFXs.Count - 1
                'Search this SFX in the SFXs list
                Dim itemIndex = ListBox_SFXs.FindString(SFXs(index))

                'Ensure that the item does not exists
                If itemIndex = ListBox.NoMatches Then
                    ListBox_SFXs.Items.Add(SFXs(index))
                End If
            Next
        Next

        'Sort and update counter
        If ListBox_SFXs.Items.Count > 0 Then
            'Update counter
            Label_SfxCount.Text = "SFXs: " & ListBox_SFXs.Items.Count
            'Sort items
            ListBox_SFXs.Sorted = True
        End If
    End Sub
End Class
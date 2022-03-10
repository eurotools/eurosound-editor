Imports System.IO
Imports NAudio.Wave
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
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
        'Set cursor as hourglass
        Cursor.Current = Cursors.WaitCursor

        'Put soundbank name
        GroupBox_SoundbankData.Text = SoundbankName

        'Calculate Output Filename
        Label_Value_OutFileName.Text = "HC" & Hex(SoundbankHashCode).PadLeft(6, "0"c) & ".SFX"

        'Read Soundbank File and put basic info
        Dim SoundbankObj As SoundbankFile = textFileReaders.ReadSoundBankFile(SoundbankFilePath)
        Label_Value_FirstCreated.Text = SoundbankObj.HeaderInfo.FirstCreated
        Label_CreatedBy_Value.Text = SoundbankObj.HeaderInfo.CreatedBy
        Label_Value_LastModified.Text = SoundbankObj.HeaderInfo.LastModify
        Label_ModifiedBy_Value.Text = SoundbankObj.HeaderInfo.LastModifyBy
        Label_DatabaseCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "Databases"), "*.txt")
        Label_SfxCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "SFXs"), "*.txt")

        'Add DataBases Sort and update counter
        If SoundbankObj.Dependencies.Count > 0 Then
            ListBox_Databases.Items.AddRange(SoundbankObj.Dependencies)
            Label_DataBasesCount.Text = "DataBases: " & ListBox_Databases.Items.Count
        End If

        'Get soundbank Sfx list
        Dim sfxList As String() = GetSfxList(SoundbankObj)
        If sfxList.Length > 0 Then
            ListBox_SFXs.Items.AddRange(sfxList)
            Label_SfxCount.Text = "SFXs: " & sfxList.Length
        End If

        'Get Samples List
        Dim samplesList As String() = GetSamples()
        Dim samplesFullPath As String() = GetSamplesFullPath(samplesList)
        If samplesFullPath.Length > 0 Then
            ListBox_SamplesList.Items.AddRange(samplesFullPath)
            Label_TotalSamples.Text = "Samples: " & samplesFullPath.Length
        End If

        'Ensure that the Master folder exists
        Dim MasterFilePath = fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")
        If fso.FolderExists(MasterFilePath) Then
            'Count folder files
            Label_SampleCount_Value.Text = Directory.GetFiles(MasterFilePath, "*.wav", SearchOption.AllDirectories).Length
            'Get sample folder size
            Dim fold As Folder = fso.GetFolder(MasterFilePath)
            Label_Value_Size.Text = BytesStringFormat(fold.Size)
        End If

        'Get streamed samples 
        Dim streamSamplesList As String() = textFileReaders.GetStreamSoundsList(SysFileSamples)
        Dim samplesInSoundbank As String() = GetSamplesFullPath(SamplesToIncludeInSoundbank(samplesList, streamSamplesList))

        'Get estimated size PC
        Dim sfxSize As Integer = 20 * sfxList.Length
        Dim samplesSize As Integer = 12 * sfxList.Length
        Label_SizeFile_PC.Text = BytesStringFormat(GetSamplesSize(samplesInSoundbank, WorkingDirectory & "\PC\", ".wav") + sfxSize + samplesSize)
        Label_SizeFile_PlayStation2.Text = BytesStringFormat(GetSamplesSize(samplesInSoundbank, WorkingDirectory & "\PlayStation2_VAG\", ".vag") + sfxSize + samplesSize)
        Label_SizeFile_GameCube.Text = BytesStringFormat(GetSamplesSize(samplesInSoundbank, WorkingDirectory & "\GameCube_dsp_adpcm\", ".dsp") + sfxSize + samplesSize)
        Label_SizeFile_Xbox.Text = BytesStringFormat(GetSamplesSize(samplesInSoundbank, WorkingDirectory & "\XBox_adpcm\", ".wav") + sfxSize + samplesSize)

        'Set cursor as default arrow
        Cursor.Current = Cursors.Default
    End Sub

    Private Function GetSamplesSize(soundbankSamples As String(), platformFolder As String, fileExtension As String) As Long
        Dim fileLength As Long = 0
        If fso.FolderExists(platformFolder) Then
            Dim startString As Integer = Len(WorkingDirectory & "\Master\")
            For index As Integer = 0 To soundbankSamples.Length - 1
                Dim platformFilePath As String = Path.ChangeExtension(fso.BuildPath(platformFolder, Mid(soundbankSamples(index), startString)), fileExtension)
                If fso.FileExists(platformFilePath) Then
                    If platformFilePath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) Then
                        Dim waveReader As New WaveFileReader(platformFilePath)
                        fileLength += waveReader.Length
                    ElseIf platformFilePath.EndsWith(".aif", StringComparison.OrdinalIgnoreCase) Then
                        Dim aiffReader As New AiffFileReader(platformFilePath)
                        fileLength += aiffReader.Length
                    Else
                        fileLength += FileLen(platformFilePath)
                    End If
                End If
            Next
        End If
        Return fileLength
    End Function

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
    Private Function GetSamples() As String()
        'Get Samples
        Dim Samples As New HashSet(Of String)
        For Each item As String In ListBox_SFXs.Items
            'Open file
            Dim filePath As String = fso.BuildPath(WorkingDirectory, "SFXs\" & item & ".txt")
            If fso.FileExists(filePath) Then
                Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(filePath)
                For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                    Samples.Add(sfxFileData.Samples(sampleIndex).FilePath)
                Next
            End If
        Next
        Return Samples.ToArray
    End Function

    Private Function GetSamplesFullPath(samplesArray As String()) As String()
        Dim samplesList As New List(Of String)
        For index As Integer = 0 To samplesArray.Length - 1
            'Get full path
            Dim waveFilePath As String = UCase(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master\" & samplesArray(index)))
            'Add path to list
            samplesList.Add(waveFilePath)
        Next
        Return samplesList.ToArray
    End Function

    Private Function GetSfxList(SoundbankObj As SoundbankFile) As String()
        Dim sfxList As New HashSet(Of String)
        'Get used SFXs
        For Each item As String In SoundbankObj.Dependencies
            'Read SFX Dependencies
            Dim sfxFullPath As String = fso.BuildPath(WorkingDirectory, "DataBases\" & item & ".txt")
            Dim SFXs As String() = textFileReaders.ReadDataBaseFile(sfxFullPath).Dependencies
            'Add items to list
            For index As Integer = 0 To SFXs.Count - 1
                sfxList.Add(SFXs(index))
            Next
        Next
        Return sfxList.ToArray
    End Function

    Private Function SamplesToIncludeInSoundbank(samplesList As String(), streamSamples As String()) As String()
        Dim samplesToInclude As New HashSet(Of String)

        For index As Integer = 0 To samplesList.Length - 1
            Dim sampleFullPath As String = samplesList(index)
            If Not sampleFullPath.StartsWith("\") Then
                sampleFullPath = "\" & samplesList(index)
            End If
            samplesToInclude.Add(sampleFullPath)
        Next

        Return samplesToInclude.Except(streamSamples).ToArray
    End Function
End Class
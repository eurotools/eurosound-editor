Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.SoundBanksExporterFunctions
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

        'Get SoundBank info
        Dim soundBankData As SoundbankFile = textFileReaders.ReadSoundBankFile(SoundbankFilePath)
        Dim soundBankSFXs As String() = GetSFXsArray(soundBankData, True)
        Dim soundBankSamples As String() = GetSamplesList(soundBankSFXs).Distinct.ToArray

        'Show SoundBank Data
        Label_Value_FirstCreated.Text = soundBankData.HeaderInfo.FirstCreated
        Label_CreatedBy_Value.Text = soundBankData.HeaderInfo.CreatedBy
        Label_Value_LastModified.Text = soundBankData.HeaderInfo.LastModify
        Label_ModifiedBy_Value.Text = soundBankData.HeaderInfo.LastModifyBy
        Label_DatabaseCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "Databases"), "*.txt")
        Label_SfxCount_Value.Text = CountFolderFiles(fso.BuildPath(WorkingDirectory, "SFXs"), "*.txt")

        'Add DataBases Sort and update counter
        If soundBankData.Dependencies.Count > 0 Then
            ListBox_Databases.Items.AddRange(soundBankData.Dependencies)
            Label_DataBasesCount.Text = "DataBases: " & ListBox_Databases.Items.Count
        End If

        'Get soundbank Sfx list
        If soundBankSFXs.Length > 0 Then
            ListBox_SFXs.Items.AddRange(soundBankSFXs)
            Label_SfxCount.Text = "SFXs: " & soundBankSFXs.Length
        End If

        'Get Samples List
        If soundBankSamples.Length > 0 Then
            ListBox_SamplesList.Items.AddRange(soundBankSamples)
            Label_TotalSamples.Text = "Samples: " & soundBankSamples.Length
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
        Dim samplesInSoundbank As String() = ExcludeStreamsFromSamplesArray(soundBankSamples, streamSamplesList)

        'Get estimated size PC
        Dim sfxSize As Integer = 20 * soundBankSFXs.Length
        Dim samplesSize As Integer = 12 * soundBankSFXs.Length
        Label_SizeFile_PC.Text = BytesStringFormat(GetSoundBankSize(samplesInSoundbank, WorkingDirectory & "\PC\", ".wav") + sfxSize + samplesSize)
        Label_SizeFile_PlayStation2.Text = BytesStringFormat(GetSoundBankSize(samplesInSoundbank, WorkingDirectory & "\PlayStation2_VAG\", ".vag") + sfxSize + samplesSize)
        Label_SizeFile_GameCube.Text = BytesStringFormat(GetSoundBankSize(samplesInSoundbank, WorkingDirectory & "\GameCube_dsp_adpcm\", ".dsp") + sfxSize + samplesSize)
        Label_SizeFile_Xbox.Text = BytesStringFormat(GetSoundBankSize(samplesInSoundbank, WorkingDirectory & "\XBox_adpcm\", ".adpcm") + sfxSize + samplesSize)

        'Set cursor as default arrow
        Cursor.Current = Cursors.Default
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
    Private Function ExcludeStreamsFromSamplesArray(samplesList As String(), streamSamples As String()) As String()
        Dim samplesToInclude As New HashSet(Of String)
        For sampleIndex As Integer = 0 To samplesList.Length - 1
            Dim currentSample As String = samplesList(sampleIndex)
            Dim IsStream As Boolean = False
            For streamSampleIndex As Integer = 0 To streamSamples.Length - 1
                Dim currentStreamSample As String = UCase(streamSamples(streamSampleIndex))
                If InStr(1, currentSample, currentStreamSample, Microsoft.VisualBasic.CompareMethod.Binary) > 0 Then
                    IsStream = True
                    Exit For
                End If
            Next
            'Add sample if is not a stream
            If Not IsStream Then
                samplesToInclude.Add(currentSample)
            End If
        Next
        Return samplesToInclude.ToArray
    End Function
End Class
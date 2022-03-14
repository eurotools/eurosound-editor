Imports NAudio.Wave
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.SoundBanksExporterFunctions

Public Class Soundbank_Properties
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly SoundbankFilePath As String
    Private ReadOnly OutputPlatform As String
    Private ReadOnly OutputLanguage As String
    Private ReadOnly textFileReaders As New FileParsers()

    Public Sub New(sbFilePath As String, outPlatform As String, outLanguage As String)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        SoundbankFilePath = sbFilePath
        OutputPlatform = outPlatform
        OutputLanguage = outLanguage
    End Sub

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub Soundbank_Properties_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set cursor as hourglass
        Cursor.Current = Cursors.WaitCursor

        'Get SoundBank info
        Dim availablePlatforms As String() = ProjectSettingsFile.sampleRateFormats.Keys.ToArray
        Dim soundBankData As SoundbankFile = textFileReaders.ReadSoundBankFile(SoundbankFilePath)
        Dim soundBankSfxList As String() = GetSoundBankSFXsList(soundBankData, OutputPlatform)
        Dim soundBankSamplesList As String() = GetSoundBankSamplesList(soundBankSfxList, OutputLanguage)

        'Put soundbank name
        GroupBox_SoundbankData.Text = GetOnlyFileName(SoundbankFilePath)

        'Calculate Output Filename
        Label_Value_OutFileName.Text = "HC" & Hex(soundBankData.HashCode).PadLeft(6, "0"c) & ".SFX"

        'Show SoundBank Data
        Label_Value_FirstCreated.Text = soundBankData.HeaderInfo.FirstCreated
        Label_CreatedBy_Value.Text = soundBankData.HeaderInfo.CreatedBy
        Label_Value_LastModified.Text = soundBankData.HeaderInfo.LastModify
        Label_ModifiedBy_Value.Text = soundBankData.HeaderInfo.LastModifyBy
        Label_DatabaseCount_Value.Text = soundBankData.Dependencies.Length
        Label_SfxCount_Value.Text = soundBankSfxList.Length
        Label_SampleCount_Value.Text = soundBankSamplesList.Length

        'Add Soundbank data to listboxes and update counters
        If soundBankData.Dependencies.Count > 0 Then
            ListBox_Databases.Items.AddRange(soundBankData.Dependencies)
            Label_DataBasesCount.Text = "DataBases: " & ListBox_Databases.Items.Count
        End If

        If soundBankSfxList.Length > 0 Then
            ListBox_SFXs.Items.AddRange(soundBankSfxList)
            Label_SfxCount.Text = "SFXs: " & soundBankSfxList.Length
        End If

        If soundBankSamplesList.Length > 0 Then
            ListBox_SamplesList.Items.AddRange(soundBankSamplesList)
            Label_TotalSamples.Text = "Samples: " & soundBankSamplesList.Length
        End If

        'Get streamed samples 
        Dim streamSamplesList As String() = textFileReaders.GetStreamSoundsList(SysFileSamples)

        'Calculate SoundBank size for each format
        Dim pcFinalList = GetFinalList(soundBankSamplesList, streamSamplesList, "PC")
        Dim psFinalList = GetFinalList(soundBankSamplesList, streamSamplesList, "PlayStation2")
        Dim gcFinalList = GetFinalList(soundBankSamplesList, streamSamplesList, "GameCube")
        Dim xbFinalList = GetFinalList(soundBankSamplesList, streamSamplesList, "X Box")
        Dim totalSampleSize As Integer = 0
        Dim samplesTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)

        'PC Size
        Dim pcSoundBankFile As String = fso.BuildPath(WorkingDirectory, "PC\SoundBanks\" & OutputLanguage & "\" & soundBankData.HashCode & ".sbf")
        If fso.FileExists(pcSoundBankFile) Then
            totalSampleSize += GetTotalSampleSize(pcFinalList)
            Label_SizeFile_PC.Text = BytesStringFormat(FileLen(pcSoundBankFile))
        Else
            If fso.FolderExists(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")) Then
                totalSampleSize += GetTotalSampleSize(pcFinalList)
                Label_SizeFile_PC.Text = BytesStringFormat(GetEstimatedPlatformSize(pcFinalList, "PC", samplesTable)) & " - ESTIMATED"
            Else
                totalSampleSize += pcFinalList.Length
                Label_SizeFile_PC.Text = BytesStringFormat(GetEstimatedPlatformSizeNoMaster(pcFinalList, "PC")) & " - ESTIMATED"
            End If
        End If

        'PlayStation2
        Dim playStationSoundBankFile As String = fso.BuildPath(WorkingDirectory, "PlayStation2\SoundBanks\" & OutputLanguage & "\" & soundBankData.HashCode & ".sbf")
        If fso.FileExists(playStationSoundBankFile) Then
            totalSampleSize += GetTotalSampleSize(psFinalList)
            Label_SizeFile_PlayStation2.Text = BytesStringFormat(FileLen(playStationSoundBankFile))
        Else
            If fso.FolderExists(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")) Then
                totalSampleSize += GetTotalSampleSize(psFinalList)
                Label_SizeFile_PlayStation2.Text = BytesStringFormat(GetEstimatedPlatformSize(psFinalList, "PlayStation2", samplesTable)) & " - ESTIMATED"
            Else
                totalSampleSize += psFinalList.Length
                Label_SizeFile_PlayStation2.Text = BytesStringFormat(GetEstimatedPlatformSizeNoMaster(psFinalList, "PlayStation2")) & " - ESTIMATED"
            End If
        End If

        'GameCube
        Dim gameCubeSoundbankFile As String = fso.BuildPath(WorkingDirectory, "GameCube\SoundBanks\" & OutputLanguage & "\" & soundBankData.HashCode & ".sbf")
        If fso.FileExists(gameCubeSoundbankFile) Then
            totalSampleSize += GetTotalSampleSize(gcFinalList)
            Label_SizeFile_GameCube.Text = BytesStringFormat(FileLen(gameCubeSoundbankFile))
        Else
            If fso.FolderExists(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")) Then
                totalSampleSize += GetTotalSampleSize(gcFinalList)
                Label_SizeFile_GameCube.Text = BytesStringFormat(GetEstimatedPlatformSize(gcFinalList, "GameCube", samplesTable)) & " - ESTIMATED"
            Else
                totalSampleSize += gcFinalList.Length
                Label_SizeFile_GameCube.Text = BytesStringFormat(GetEstimatedPlatformSizeNoMaster(gcFinalList, "GameCube")) & " - ESTIMATED"
            End If
        End If

        'X Box
        Dim xboxSoundbankFile As String = fso.BuildPath(WorkingDirectory, "X Box\SoundBanks\" & OutputLanguage & "\" & soundBankData.HashCode & ".sbf")
        If fso.FileExists(xboxSoundbankFile) Then
            totalSampleSize += GetTotalSampleSize(xbFinalList)
            Label_SizeFile_Xbox.Text = BytesStringFormat(FileLen(xboxSoundbankFile))
        Else
            If fso.FolderExists(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")) Then
                totalSampleSize += GetTotalSampleSize(xbFinalList)
                Label_SizeFile_Xbox.Text = BytesStringFormat(GetEstimatedPlatformSize(xbFinalList, "X Box", samplesTable)) & " - ESTIMATED"
            Else
                totalSampleSize += xbFinalList.Length
                Label_SizeFile_Xbox.Text = BytesStringFormat(GetEstimatedPlatformSizeNoMaster(xbFinalList, "X Box")) & " - ESTIMATED"
            End If
        End If

        'Total Sample Size
        Label_Value_Size.Text = BytesStringFormat(totalSampleSize)

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
    '* FUNCTIONS RELATIONS WITH THE FILES MANAGMENT
    '*===============================================================================================
    Private Function GetFinalList(samplesArray As String(), StreamsArray As String(), outputPlatform As String) As String()
        Dim samplesToInclude As New HashSet(Of String)
        Dim finalSampleList As String() = RemoveAllSubSFX(samplesArray)
        If StrComp(outputPlatform, "PC") = 0 Or StrComp(outputPlatform, "PlayStation2") = 0 Then
            finalSampleList = RemoveAllStreams(finalSampleList, StreamsArray)
        End If

        'Sort and return
        Array.Sort(finalSampleList)
        Return finalSampleList
    End Function

    Private Function RemoveAllSubSFX(SamplesArray As String())
        Dim samplesToInclude As New HashSet(Of String)
        For sampleIndex As Integer = 0 To SamplesArray.Length - 1
            Dim currentSample As String = SamplesArray(sampleIndex)
            If InStr(1, currentSample, ".WAV", CompareMethod.Binary) AndAlso InStr(1, currentSample, "\\", CompareMethod.Binary) = 0 Then
                samplesToInclude.Add(currentSample)
            End If
        Next
        Return samplesToInclude.ToArray
    End Function

    Private Function RemoveAllStreams(SamplesArray As String(), StreamsArray As String())
        Dim samplesToInclude As New HashSet(Of String)
        For sampleIndex As Integer = 0 To SamplesArray.Length - 1
            Dim currentSample As String = SamplesArray(sampleIndex)
            Dim sampleIsStream As Boolean = False
            For streamIndex As Integer = 0 To StreamsArray.Length - 1
                Dim currentStream As String = StreamsArray(streamIndex)
                If InStr(1, currentSample, UCase(currentStream), CompareMethod.Binary) Then
                    sampleIsStream = True
                    Exit For
                End If
            Next
            'Check if we can add this sample
            If sampleIsStream = False Then
                samplesToInclude.Add(currentSample)
            End If
        Next

        Return samplesToInclude.ToArray
    End Function

    '*===============================================================================================
    '* FUNCTIONS TO CALCULATE ESTIMATED SIZE - NO MASTER FOLDER
    '*===============================================================================================
    Friend Function GetEstimatedPlatformSizeNoMaster(samplesList As String(), outputPlatform As String) As Integer
        Dim sampleSize As Integer = 0
        For sampleIndex As Integer = 0 To samplesList.Length - 1
            Dim filePath As String = samplesList(sampleIndex)
            If InStr(1, filePath, ".WAV", CompareMethod.Binary) Then
                If StrComp(UCase(outputPlatform), "XBOX") = 0 Or StrComp(UCase(outputPlatform), "X BOX") = 0 Then
                    sampleSize += 36
                Else
                    sampleSize += 32
                End If
            End If
        Next
        Return sampleSize
    End Function

    Friend Function GetTotalSampleSizeNoMaster(samplesList As String()) As Integer
        Dim sampleSize As Integer = 0
        For sampleIndex As Integer = 0 To samplesList.Length - 1
            Dim filePath As String = samplesList(sampleIndex)
            If InStr(1, UCase(filePath), ".WAV", CompareMethod.Binary) Then
                sampleSize += 4
            End If
        Next
        Return sampleSize
    End Function

    '*===============================================================================================
    '* FUNCTIONS TO CALCULATE ESTIMATED SIZE - MASTER FOLDER
    '*===============================================================================================
    Friend Function GetEstimatedPlatformSize(samplesList As String(), outputPlatform As String, samplesTable As DataTable) As Integer
        Dim platformSize As Integer = 0
        For fileIndex As Integer = 0 To samplesList.Length - 1
            Dim sampleFilePath As String = samplesList(fileIndex)
            'Get wave length
            Dim masterWaveSize As Integer
            Dim masterWaveFrequency As Integer
            Using waveReader As New WaveFileReader(sampleFilePath)
                masterWaveSize = waveReader.Length
                masterWaveFrequency = waveReader.WaveFormat.SampleRate
            End Using
            'Get the ReSampled wave size
            For rowIndex As Integer = 0 To samplesTable.Rows.Count - 1
                Dim currentRow As String = samplesTable.Rows(rowIndex).ItemArray(0)
                If InStr(1, sampleFilePath, UCase(currentRow), CompareMethod.Binary) Then
                    Dim frequencyLabel As String = samplesTable.Rows(rowIndex).ItemArray(1)
                    Dim platformFrequency As Integer = ProjectSettingsFile.sampleRateFormats(outputPlatform)(frequencyLabel)
                    Dim compressionFactor As Decimal = Decimal.Divide(masterWaveFrequency, platformFrequency)
                    Select Case outputPlatform
                        Case "PC"
                            platformSize += Decimal.Divide(masterWaveSize, compressionFactor)
                        Case "GameCube"
                            Dim resampledWaveSize As Decimal = Decimal.Divide(masterWaveSize, compressionFactor)
                            Dim gameCubeDSPSize As Decimal = Decimal.Divide(resampledWaveSize, 3.46)
                            platformSize += gameCubeDSPSize
                        Case "PlayStation2"
                            Dim resampledWaveSize As Decimal = Decimal.Divide(masterWaveSize, compressionFactor)
                            Dim sonyVagSize As Decimal = Decimal.Divide(resampledWaveSize, 3.45)
                            platformSize += sonyVagSize
                        Case Else
                            Dim resampledWaveSize As Decimal = Decimal.Divide(masterWaveSize, compressionFactor)
                            Dim xboxAdpcm As Decimal = Decimal.Divide(resampledWaveSize, 3.54)
                            platformSize += xboxAdpcm
                    End Select
                    Exit For
                End If
            Next
        Next
        Return platformSize
    End Function

    Friend Function GetTotalSampleSize(samplesList As String()) As Integer
        Dim sampleSize As Integer = 0
        For sampleIndex As Integer = 0 To samplesList.Length - 1
            Dim filePath As String = samplesList(sampleIndex)
            'Get wave length
            Using waveReader As New WaveFileReader(filePath)
                sampleSize += waveReader.Length
            End Using
        Next
        Return sampleSize
    End Function
End Class
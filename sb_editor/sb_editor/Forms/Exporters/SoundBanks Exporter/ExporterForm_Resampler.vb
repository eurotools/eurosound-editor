Imports System.ComponentModel
Imports System.IO
Imports NAudio.Wave

Partial Public Class ExporterForm
    '*===============================================================================================
    '* MAIN METHOD
    '*===============================================================================================
    Private Sub ResampleWaves(soundsTable As DataTable, outPlatforms As String(), SoxTimer As Stopwatch, PCTimer As Stopwatch, GCTimer As Stopwatch, XBTimer As Stopwatch, PSTimer As Stopwatch)
        Dim waveFunctions As New WaveFunctions
        'Get Wave files to include
        Dim samplesCount As Integer = soundsTable.Rows.Count - 1
        If samplesCount > 0 Then
            'Create Folder structure for all platforms
            For platformIndex As Integer = 0 To outPlatforms.Length - 1
                Dim currentPlatform As String = outPlatforms(platformIndex)
                CopyDirectory(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master"), fso.BuildPath(WorkingDirectory, currentPlatform), True)
                Select Case currentPlatform
                    Case "PC"
                        CopyDirectory(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master"), fso.BuildPath(WorkingDirectory, currentPlatform & "_Software_adpcm"), True)
                    Case "GameCube"
                        CopyDirectory(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master"), fso.BuildPath(WorkingDirectory, currentPlatform & "_Software_adpcm"), True)
                        CopyDirectory(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master"), fso.BuildPath(WorkingDirectory, currentPlatform & "_dsp_adpcm"), True)
                    Case "PlayStation2"
                        CopyDirectory(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master"), fso.BuildPath(WorkingDirectory, currentPlatform & "_VAG"), True)
                    Case Else
                        CopyDirectory(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master"), fso.BuildPath(WorkingDirectory, "XBox_adpcm"), True)
                End Select
            Next
            'Reset progress bar
            Invoke(Sub() ProgressBar1.Value = 0)
            'Start inspecting each line of the datatable
            For rowIndex As Integer = 0 To samplesCount
                If soundsTable.Rows(rowIndex).Item(4) Then
                    'Calculate and report progress
                    BackgroundWorker.ReportProgress(Decimal.Divide(rowIndex, samplesCount) * 100.0)
                    'Get paths 
                    Dim sampleRelativePath As String = soundsTable.Rows(rowIndex).ItemArray(0)
                    Dim sourceFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder & "\Master", sampleRelativePath)
                    'Read WAVE Master file and get all required info
                    Dim masterWaveNumOfChannels, masterWaveBitsPerSample, masterWaveFrequency, masterWaveLength As Integer
                    Dim masterWaveLoopInfo As Integer()
                    Using reader As New WaveFileReader(sourceFilePath)
                        masterWaveNumOfChannels = reader.WaveFormat.Channels
                        masterWaveBitsPerSample = reader.WaveFormat.BitsPerSample
                        masterWaveFrequency = reader.WaveFormat.SampleRate
                        masterWaveLength = reader.Length
                        masterWaveLoopInfo = waveFunctions.ReadSampleChunk(reader)
                    End Using
                    If masterWaveNumOfChannels <> 1 Then
                        MsgBox("Sample " & sourceFilePath & " is not 1 channel.", vbOKOnly + vbCritical, "EuroSound")
                    Else
                        If masterWaveBitsPerSample <> 16 Then
                            MsgBox("Sample " & sourceFilePath & " is not 16 bit.", vbOKOnly + vbCritical, "EuroSound")
                        Else
                            'Resample for each platform 
                            For platformIndex As Integer = 0 To outPlatforms.Length - 1
                                Dim currentPlatform As String = outPlatforms(platformIndex)
                                Dim outputFilePath As String = fso.BuildPath(WorkingDirectory & "\" & currentPlatform, sampleRelativePath)
                                'Update title
                                Invoke(Sub() Text = "ReSampling: " & sampleRelativePath & "  " & currentPlatform)
                                'Resample the wav for the destination platform
                                Dim sampleRateLabel As String = soundsTable.Rows(rowIndex).ItemArray(1)
                                Dim sampleRate As Integer = ProjectSettingsFile.sampleRateFormats(currentPlatform)(sampleRateLabel)
                                If soundsTable.Rows(rowIndex).Item(5) Then
                                    sampleRate = 22050
                                End If
                                SoxTimer.Start()
                                ReSampleWithSox(sourceFilePath, outputFilePath, masterWaveFrequency, sampleRate)
                                SoxTimer.Stop()

                                'ReSample for each platform
                                Select Case currentPlatform
                                    Case "PC" 'IMA ADPCM For PC Streams
                                        If soundsTable.Rows(rowIndex).Item(5) Then 'Check if is Streamed
                                            CreateImaAdpcm(currentPlatform, sampleRelativePath, outputFilePath, PCTimer, waveFunctions)
                                        End If
                                    Case "GameCube" 'DSP for Nintendo GameCube
                                        If soundsTable.Rows(rowIndex).Item(5) Then 'Check if is Streamed
                                            CreateImaAdpcm(currentPlatform, sampleRelativePath, outputFilePath, GCTimer, waveFunctions)
                                        End If
                                        GCTimer.Start()
                                        Dim dspOutputFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\GameCube_dsp_adpcm", sampleRelativePath), ".dsp")
                                        'Default arguments
                                        Dim dspToolArgs As String = "Encode """ & outputFilePath & """ """ & dspOutputFilePath & """"
                                        'Check loop info
                                        If masterWaveLoopInfo(0) = 1 AndAlso soundsTable.Rows(rowIndex).Item(5) = False Then 'Check if is NOT Streamed
                                            'Loop offset pos in the resampled wave
                                            Using parsedWaveReader As New WaveFileReader(outputFilePath)
                                                Dim parsedLoop As UInteger = (masterWaveLoopInfo(1) / (masterWaveLength / parsedWaveReader.Length)) * 2
                                                dspToolArgs = "Encode """ & outputFilePath & """ """ & dspOutputFilePath & """ -L " & parsedLoop
                                            End Using
                                        End If
                                        'Execute Dsp Adpcm Tool
                                        RunProcess("SystemFiles\DspCodec.exe", dspToolArgs)
                                        GCTimer.Stop()
                                    Case "PlayStation2"  'Sony VAG for PlayStation 2
                                        PSTimer.Start()
                                        Dim vagOutputFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\PlayStation2_VAG", sampleRelativePath), ".vag")
                                        'Default arguments
                                        Dim vagToolArgs As String = """" & outputFilePath & """ """ & vagOutputFilePath & """"
                                        'Check loop info
                                        If masterWaveLoopInfo(0) = 1 AndAlso soundsTable.Rows(rowIndex).Item(5) = False Then 'Check if is NOT Streamed
                                            'Loop offset pos in the resampled wave
                                            Using parsedWaveReader As New WaveFileReader(outputFilePath)
                                                Dim parsedLoop As UInteger = masterWaveLoopInfo(1) / (masterWaveLength / parsedWaveReader.Length)
                                                Dim loopOffsetVag As UInteger = ESUtils.CalculusLoopOffset.GetLoopOffsetForVag(parsedLoop) - 1
                                                vagToolArgs = """" & outputFilePath & """ """ & vagOutputFilePath & """ " & loopOffsetVag
                                            End Using
                                        End If
                                        'Execute Vag Tool
                                        RunProcess("SystemFiles\VagCodec.exe", vagToolArgs)
                                        PSTimer.Stop()
                                    Case Else 'Xbox ADPCM for Xbox
                                        XBTimer.Start()
                                        Dim xboxOutputFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\XBox_adpcm", sampleRelativePath), ".adpcm")
                                        'Execute Dsp Adpcm Tool
                                        RunProcess("SystemFiles\XboxCodec.exe", "Encode """ & outputFilePath & """ """ & xboxOutputFilePath & """")
                                        XBTimer.Stop()
                                End Select
                            Next
                            'Update Property
                            soundsTable.Rows(rowIndex).Item(4) = "False"
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub CreateImaAdpcm(currentPlatform As String, sampleRelativePath As String, outputFilePath As String, platformTimer As Stopwatch, waveLib As WaveFunctions)
        platformTimer.Start()
        Dim ImaOutputFilePath As String = fso.BuildPath(WorkingDirectory & "\" & currentPlatform & "_Software_adpcm", sampleRelativePath)
        'Resampled wav
        Dim smdFilePath As String = Path.ChangeExtension(ImaOutputFilePath, ".smd")
        RunProcess("SystemFiles\Sox.exe", """" & outputFilePath & """ -t raw """ & smdFilePath & """")
        'Wave to ima
        Dim imaData As Byte() = ESUtils.ImaCodec.Encode(waveLib.ConvertByteArrayToShortArray(File.ReadAllBytes(smdFilePath)))
        File.WriteAllBytes(Path.ChangeExtension(ImaOutputFilePath, ".ssp"), imaData)
        platformTimer.Stop()
    End Sub
End Class

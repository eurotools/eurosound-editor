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
            'Reset progress bar
            Invoke(Sub() ProgressBar1.Value = 0)
            'Start inspecting each line of the datatable
            For rowIndex As Integer = 0 To samplesCount
                If StrComp(soundsTable.Rows(rowIndex).Item(4), "True") = 0 Then
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
                                If StrComp(soundsTable.Rows(rowIndex).Item(5), "True") = 0 Then
                                    sampleRate = 22050
                                End If
                                CreateFolderIfRequired(fso.GetParentFolderName(outputFilePath))
                                SoxTimer.Start()
                                ReSampleWithSox(sourceFilePath, outputFilePath, masterWaveFrequency, sampleRate)
                                SoxTimer.Stop()

                                'ReSample for each platform
                                Select Case currentPlatform
                                    Case "PC" 'IMA ADPCM For PC Streams
                                        If StrComp(soundsTable.Rows(rowIndex).Item(5), "True") = 0 Then
                                            CreateImaAdpcm(currentPlatform, sampleRelativePath, outputFilePath, PCTimer, waveFunctions)
                                        End If
                                    Case "GameCube" 'DSP for Nintendo GameCube
                                        'Create IMA ADPCM for Streams
                                        If StrComp(soundsTable.Rows(rowIndex).Item(5), "True") = 0 Then
                                            CreateImaAdpcm(currentPlatform, sampleRelativePath, outputFilePath, GCTimer, waveFunctions)
                                        End If
                                        GCTimer.Start()
                                        Dim dspOutputFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\GameCube_dsp_adpcm", sampleRelativePath), ".dsp")
                                        CreateFolderIfRequired(fso.GetParentFolderName(dspOutputFilePath))
                                        'Default arguments
                                        Dim dspToolArgs As String = "Encode """ & outputFilePath & """ """ & dspOutputFilePath & """"
                                        'Check loop info
                                        If masterWaveLoopInfo(0) = 1 And StrComp(soundsTable.Rows(rowIndex).Item(5), "True") = 0 Then
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
                                        CreateFolderIfRequired(fso.GetParentFolderName(vagOutputFilePath))
                                        'Default arguments
                                        Dim vagToolArgs As String = """" & outputFilePath & """ """ & vagOutputFilePath & """"
                                        'Check loop info
                                        If masterWaveLoopInfo(0) = 1 And StrComp(soundsTable.Rows(rowIndex).Item(5), "True") = 0 Then
                                            'Loop offset pos in the resampled wave
                                            Using parsedWaveReader As New WaveFileReader(outputFilePath)
                                                Dim parsedLoop As UInteger = (masterWaveLoopInfo(1) / (masterWaveLength / parsedWaveReader.Length)) * 2
                                                Dim loopOffsetVag As UInteger = ((parsedLoop / 28 + (If(((parsedLoop Mod 28) <> 0), 2, 1))) / 2) - 1
                                                vagToolArgs = """" & outputFilePath & """ """ & vagOutputFilePath & """ -l" & loopOffsetVag
                                            End Using
                                        End If
                                        'Execute Vag Tool
                                        RunProcess("SystemFiles\VagCodec.exe", vagToolArgs)
                                        PSTimer.Stop()
                                    Case Else 'Xbox ADPCM for Xbox
                                        XBTimer.Start()
                                        Dim xboxOutputFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\XBox_adpcm", sampleRelativePath), ".adpcm")
                                        CreateFolderIfRequired(fso.GetParentFolderName(xboxOutputFilePath))
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
        CreateFolderIfRequired(fso.GetParentFolderName(ImaOutputFilePath))
        'Resampled wav
        Dim smdFilePath As String = Path.ChangeExtension(ImaOutputFilePath, ".smd")
        RunProcess("SystemFiles\Sox.exe", """" & outputFilePath & """ -t raw """ & smdFilePath & """")
        'Wave to ima
        Dim imaData As Byte() = ESUtils.ImaCodec.Encode(waveLib.ConvertByteArrayToShortArray(File.ReadAllBytes(smdFilePath)))
        File.WriteAllBytes(Path.ChangeExtension(ImaOutputFilePath, ".ssp"), imaData)
        platformTimer.Stop()
    End Sub
End Class

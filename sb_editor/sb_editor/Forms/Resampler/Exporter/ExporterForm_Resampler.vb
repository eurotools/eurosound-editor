Imports System.ComponentModel
Imports System.IO
Imports NAudio.Wave

Partial Public Class ExporterForm
    '*===============================================================================================
    '* MAIN METHOD
    '*===============================================================================================
    Private Sub ResampleWaves(soundsTable As DataTable, outPlatforms As String())
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
                    'Check channels
                    Dim numberOfChannels As Integer = 0
                    Dim bitsPerSample As Integer = 0
                    Using reader As New WaveFileReader(sourceFilePath)
                        numberOfChannels = reader.WaveFormat.Channels
                        bitsPerSample = reader.WaveFormat.BitsPerSample
                    End Using
                    If numberOfChannels <> 1 Then
                        MsgBox("Sample " & sourceFilePath & " is not 1 channel.", vbOKOnly + vbCritical, "EuroSound")
                    Else
                        If bitsPerSample <> 16 Then
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
                                RunProcess("SystemFiles\Sox.exe", """" & sourceFilePath & """ -r " & sampleRate & " """ & outputFilePath & """  resample -qs 0.97")
                                'IMA ADPCM For PC and Nintendo GameCube Formats
                                If StrComp(currentPlatform, "PC") = 0 Or StrComp(currentPlatform, "GameCube") = 0 Then
                                    If StrComp(soundsTable.Rows(rowIndex).Item(5), "True") = 0 Then
                                        Dim ImaOutputFilePath As String = fso.BuildPath(WorkingDirectory & "\" & currentPlatform & "_Software_adpcm", sampleRelativePath)
                                        CreateFolderIfRequired(fso.GetParentFolderName(ImaOutputFilePath))
                                        'Resampled wav
                                        Dim smdFilePath As String = Path.ChangeExtension(ImaOutputFilePath, ".smd")
                                        RunProcess("SystemFiles\Sox.exe", """" & outputFilePath & """ -t raw """ & smdFilePath & """")
                                        'Wave to ima
                                        Dim imaData As Byte() = ESUtils.ImaCodec.Encode(ConvertByteArrayToShortArray(File.ReadAllBytes(smdFilePath)))
                                        File.WriteAllBytes(Path.ChangeExtension(ImaOutputFilePath, ".ssp"), imaData)
                                    End If
                                End If
                                'DSP for Nintendo GameCube
                                If StrComp(currentPlatform, "GameCube") = 0 Then
                                    Dim dspOutputFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\GameCube_dsp_adpcm", sampleRelativePath), ".dsp")
                                    CreateFolderIfRequired(fso.GetParentFolderName(dspOutputFilePath))
                                    'Default arguments
                                    Dim dspToolArgs As String = "Encode """ & outputFilePath & """ """ & dspOutputFilePath & """"
                                    'Get loop info
                                    Using waveReader As New WaveFileReader(sourceFilePath)
                                        Dim loopInfo As Integer() = waveFunctions.ReadSampleChunk(waveReader)
                                        If loopInfo(0) = 1 And StrComp(soundsTable.Rows(rowIndex).Item(5), "True") = 0 Then
                                            'Loop offset pos in the resampled wave
                                            Using parsedWaveReader As New WaveFileReader(outputFilePath)
                                                Dim parsedLoop As UInteger = (loopInfo(1) / (waveReader.Length / parsedWaveReader.Length)) * 2
                                                dspToolArgs = "Encode """ & outputFilePath & """ """ & dspOutputFilePath & """ -L " & parsedLoop
                                            End Using
                                        End If
                                    End Using
                                    'Execute Dsp Adpcm Tool
                                    RunProcess("SystemFiles\DspCodec.exe", dspToolArgs)
                                ElseIf StrComp(currentPlatform, "PlayStation2") = 0 Then 'Sony VAG for PlayStation 2
                                    Dim vagOutputFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\PlayStation2_VAG", sampleRelativePath), ".vag")
                                    CreateFolderIfRequired(fso.GetParentFolderName(vagOutputFilePath))
                                    'Default arguments
                                    Dim vagToolArgs As String = """" & outputFilePath & """ """ & vagOutputFilePath & """"
                                    'Get loop info
                                    Using waveReader As New WaveFileReader(sourceFilePath)
                                        Dim loopInfo As Integer() = waveFunctions.ReadSampleChunk(waveReader)
                                        If loopInfo(0) = 1 And StrComp(soundsTable.Rows(rowIndex).Item(5), "True") = 0 Then
                                            'Loop offset pos in the resampled wave
                                            Using parsedWaveReader As New WaveFileReader(outputFilePath)
                                                Dim parsedLoop As UInteger = (loopInfo(1) / (waveReader.Length / parsedWaveReader.Length)) * 2
                                                Dim loopOffsetVag As UInteger = ((parsedLoop / 28 + (If(((parsedLoop Mod 28) <> 0), 2, 1))) / 2) - 1
                                                vagToolArgs = """" & outputFilePath & """ """ & vagOutputFilePath & """ -l" & loopOffsetVag
                                            End Using
                                        End If
                                    End Using
                                    'Execute Vag Tool
                                    RunProcess("SystemFiles\VagCodec.exe", vagToolArgs)
                                ElseIf StrComp(currentPlatform, "X Box") = 0 Or StrComp(currentPlatform, "Xbox") = 0 Then 'Xbox ADPCM for Xbox
                                    Dim xboxOutputFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\XBox_adpcm", sampleRelativePath), ".adpcm")
                                    CreateFolderIfRequired(fso.GetParentFolderName(xboxOutputFilePath))
                                    'Execute Dsp Adpcm Tool
                                    RunProcess("SystemFiles\XboxCodec.exe", "Encode """ & outputFilePath & """ """ & xboxOutputFilePath & """")
                                End If
                            Next
                            'Update Property
                            soundsTable.Rows(rowIndex).Item(4) = "False"
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Function ConvertByteArrayToShortArray(PCMData As Byte()) As Short()
        Dim samplesShort As Short() = New Short(PCMData.Length / 2 - 1) {}
        Dim sourceWaveBuffer As New WaveBuffer(PCMData)
        For i As Integer = 0 To samplesShort.Length - 1
            samplesShort(i) = sourceWaveBuffer.ShortBuffer(i)
        Next
        Return samplesShort
    End Function
End Class

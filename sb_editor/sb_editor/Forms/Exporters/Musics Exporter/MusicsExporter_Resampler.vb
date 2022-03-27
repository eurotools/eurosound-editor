Imports System.IO
Imports NAudio.Wave

Partial Public Class MusicsExporter
    '*===============================================================================================
    '* CREATE MUSIC STREAMS - MAIN METHOD
    '*===============================================================================================
    Private Sub ResampleMusicStreams(ESMusicFolderPath As String, ESWorkFolderPath As String, outputPlatforms As String())
        If Not MarkerFileOnly Then
            Dim temporalLeft As String = Path.Combine(WorkingDirectory, "System", "TempWave.wav")
            Dim temporalRight As String = Path.Combine(WorkingDirectory, "System", "TempWave2.wav")

            'Start main loop
            For fileIndex As Integer = 0 To outputQueue.Rows.Count - 1
                Dim musicItem As DataRow = outputQueue.Rows(fileIndex)
                Dim waveFilePath As String = Path.Combine(WorkingDirectory, "Music", musicItem.ItemArray(0) & ".wav")
                Dim musicHashCode As Integer = musicItem.ItemArray(2)

                'Split Wave channels with SoX (PC & GC)
                For platformIndex As Integer = 0 To outputPlatforms.Length - 1
                    'Get the current platform
                    Dim currentPlatform As String = outputPlatforms(platformIndex)

                    'Update title bar and progress
                    BackgroundWorker.ReportProgress(Decimal.Divide(platformIndex + (fileIndex * outputPlatforms.Length), outputQueue.Rows.Count * outputPlatforms.Length) * 100.0, "ReSampling Music Stream: " & musicItem.ItemArray(0) & " for " & currentPlatform)

                    'Start ReSampling
                    Select Case currentPlatform
                        Case "PC", "GameCube"
                            ReSampleAndSplitWithSox(waveFilePath, temporalLeft, temporalRight, 32000)

                            'Get File Paths
                            Dim statesFilePath As String = Path.Combine(ESWorkFolderPath, musicItem.ItemArray(0))
                            Dim imaFilePath As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0))

                            'IMA to Wav
                            Dim ImaFileData As Byte()() = CreateImaAdpcm(temporalLeft, temporalRight, statesFilePath)

                            'Write date
                            File.WriteAllBytes(imaFilePath & "L.ssp", ImaFileData(0))
                            File.WriteAllBytes(imaFilePath & "R.ssp", ImaFileData(1))

                        Case "PlayStation2"
                            ReSampleAndSplitWithSox(waveFilePath, temporalLeft, temporalRight, 32000)

                            'Vag Tool
                            RunConsoleProcess("SystemFiles\AIFF2VAG.exe", """" & temporalLeft & """")
                            RunConsoleProcess("SystemFiles\AIFF2VAG.exe", """" & temporalRight & """")

                            'Get File Paths
                            Dim ps2VagFilePath As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0))
                            Dim ps2VagFilePathL As String = ps2VagFilePath & "L.vag"
                            Dim ps2VagFilepathR As String = ps2VagFilePath & "R.vag"
                            Dim encoderOutputFileL As String = Path.ChangeExtension(temporalLeft, ".vag")
                            Dim encoderOutputFileR As String = Path.ChangeExtension(temporalRight, ".vag")

                            'Move files
                            File.Copy(encoderOutputFileL, ps2VagFilePathL, True)
                            File.Copy(encoderOutputFileR, ps2VagFilepathR, True)
                            File.Delete(encoderOutputFileL)
                            File.Delete(encoderOutputFileR)
                        Case Else
                            'Split channels
                            ReSampleAndSplitWithSox(waveFilePath, temporalLeft, temporalRight, 44100)

                            'Xbox Tool
                            Dim xbxAdpcmFilePath As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0))
                            Dim xbxAdpcmFilePathL As String = xbxAdpcmFilePath & "L.wav"
                            Dim xbxAdpcmFilepathR As String = xbxAdpcmFilePath & "R.wav"
                            RunConsoleProcess("SystemFiles\xbadpcmencode.exe", """" & temporalLeft & """ """ & xbxAdpcmFilePathL & """")
                            RunConsoleProcess("SystemFiles\xbadpcmencode.exe", """" & temporalRight & """ """ & xbxAdpcmFilepathR & """")
                    End Select
                Next
            Next
        End If
    End Sub

    '*===============================================================================================
    '* IMA ADPCM FUNCTIONS
    '*===============================================================================================
    Private Function CreateImaAdpcm(inputLeft As String, inputRight As String, statesFilePath As String) As Byte()()
        ' Jagged Array with Two Dimensional Array
        Dim imaFile As Byte()() = New Byte(1)() {}

        'Left Channel
        Using parsedWaveReader As New WaveFileReader(inputLeft)
            'Get PCM left Data
            Dim pcmDataLeft As Byte() = New Byte(parsedWaveReader.Length - 1) {}
            parsedWaveReader.Read(pcmDataLeft, 0, pcmDataLeft.Length)

            'Convert PCM To IMA
            imaFile(0) = ESUtils.ImaCodec.Encode(ConvertByteArrayToShortArray(pcmDataLeft))
        End Using

        'Right Channel
        Using parsedWaveReader As New WaveFileReader(inputRight)
            'Get PCM left Data
            Dim pcmDataRight As Byte() = New Byte(parsedWaveReader.Length - 1) {}
            parsedWaveReader.Read(pcmDataRight, 0, pcmDataRight.Length)

            'Convert PCM To IMA
            imaFile(1) = ESUtils.ImaCodec.Encode(ConvertByteArrayToShortArray(pcmDataRight))
        End Using

        'Decode IMA
        Dim imaDecodedLeftData As Byte() = ESUtils.ImaCodec.DecodeStatesIma(imaFile(0), imaFile(0).Length * 2)
        Dim imaDecodedRightData As Byte() = ESUtils.ImaCodec.DecodeStatesIma(imaFile(1), imaFile(1).Length * 2)
        File.WriteAllBytes(statesFilePath & ".asl", imaDecodedLeftData)
        File.WriteAllBytes(statesFilePath & ".asr", imaDecodedRightData)

        Return imaFile
    End Function
End Class

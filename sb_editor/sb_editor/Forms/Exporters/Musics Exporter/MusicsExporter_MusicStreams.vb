Imports System.IO

Partial Public Class MusicsExporter
    '*===============================================================================================
    '* CREATE MUSIC STREAMS - MAIN METHOD
    '*===============================================================================================
    Private Sub CreateMusicStreams(waveOutputFolder As String, outputPlatforms As String())
        Dim waveFunctions As New WaveFunctions
        If Not MarkerFileOnly Then
            Dim temporalLeft As String = fso.BuildPath(WorkingDirectory, "System\TempWave.wav")
            Dim temporalRight As String = fso.BuildPath(WorkingDirectory, "System\TempWave2.wav")
            'Start main loop
            For fileIndex As Integer = 0 To outputQueue.Rows.Count - 1
                Dim musicItem As DataRow = outputQueue.Rows(fileIndex)
                Dim waveFilePath As String = fso.BuildPath(WorkingDirectory, "Music\" & musicItem.ItemArray(0) & ".wav")
                Dim musicHashCode As Integer = musicItem.ItemArray(2)
                Dim ResampleAndSplit As Boolean = True
                'Split Wave channels with SoX (PC & GC)
                For platformIndex As Integer = 0 To outputPlatforms.Length - 1
                    'Get the current platform
                    Dim currentPlatform As String = outputPlatforms(platformIndex)
                    Dim soundSampleData As String = fso.BuildPath(GetOutputFolder(musicHashCode, currentPlatform), "MFX_" & musicHashCode & ".ssd")
                    'Update title bar and progress
                    Invoke(Sub() Text = "Making Music Stream: " & musicItem.ItemArray(0) & " for " & currentPlatform)
                    BackgroundWorker.ReportProgress(Decimal.Divide(platformIndex + (fileIndex * outputPlatforms.Length), outputQueue.Rows.Count * outputPlatforms.Length) * 100.0)
                    'Split channels - All platforms has the same sample rate at exception of Xbox
                    If ResampleAndSplit Then
                        ReSampleAndSplitWithSox(waveFilePath, temporalLeft, temporalRight, 32000)
                        ResampleAndSplit = False
                    End If
                    'Start ReSampling
                    Select Case currentPlatform
                        Case "PC", "GameCube"
                            'Wave to IMA
                            Dim PcOutLeft As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0))
                            Dim PcOutRight As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0))
                            CreateImaAdpcm(temporalLeft, temporalRight, PcOutLeft, PcOutRight, waveFunctions)
                            'Music Stream (.ssd)
                            MergeChannels(PcOutLeft & "_L.ima", PcOutRight & "_R.ima", 1, soundSampleData)
                        Case "PlayStation2"
                            'Vag Tool
                            Dim ps2VagL As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_L.vag"
                            Dim ps2VagR As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_R.vag"
                            RunProcess("SystemFiles\VagCodec.exe", """" & temporalLeft & """ """ & ps2VagL & """")
                            RunProcess("SystemFiles\VagCodec.exe", """" & temporalRight & """ """ & ps2VagR & """")
                            'Music Stream (.ssd)
                            MergeChannels(ps2VagL, ps2VagR, 128, soundSampleData)
                        Case Else
                            'Split channels
                            ReSampleAndSplitWithSox(waveFilePath, temporalLeft, temporalRight, 44100)
                            'Xbox Tool
                            Dim xbxVagL As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_L.adpcm"
                            Dim xbxVagR As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_R.adpcm"
                            RunProcess("SystemFiles\XboxCodec.exe", "Encode """ & temporalLeft & """ """ & xbxVagL & """")
                            RunProcess("SystemFiles\XboxCodec.exe", "Encode """ & temporalRight & """ """ & xbxVagR & """")
                            MergeChannels(xbxVagL, xbxVagR, 4, soundSampleData)
                    End Select
                Next
            Next
        End If
    End Sub

    '*===============================================================================================
    '* GET OUTPUT FOLDER (DEPENDING OF THE HASHCODE)
    '*===============================================================================================
    Private Function GetOutputFolder(fileHashCode As UInteger, currentPlatform As String) As String
        Dim folderNumber = (fileHashCode And &HF0) >> 4
        Dim markersFilePath As String = fso.BuildPath(WorkingDirectory, "TempOutputFolder\" & currentPlatform & "\Music\" & "MFX_" & folderNumber)
        CreateFolderIfRequired(markersFilePath)
        Return markersFilePath
    End Function

    '*===============================================================================================
    '* MERGE CHANNELS (CREATES THE .SSD FILE)
    '*===============================================================================================
    Private Sub MergeChannels(leftChannelFile As String, rightChannelFile As String, interleave_block_size As Integer, outputFilePath As String)
        Dim IndexLC, IndexRC As Integer

        'Read data and align array size
        Dim LeftChannelData As Byte() = File.ReadAllBytes(leftChannelFile)
        Dim RightChannelData As Byte() = File.ReadAllBytes(rightChannelFile)
        If interleave_block_size > 1 Then
            Array.Resize(LeftChannelData, ((LeftChannelData.Length + (interleave_block_size - 1)) And Not (interleave_block_size - 1)))
            Array.Resize(RightChannelData, ((RightChannelData.Length + (interleave_block_size - 1)) And Not (interleave_block_size - 1)))
        End If
        'Get total length
        Dim ArrayLength As Integer = LeftChannelData.Length + RightChannelData.Length
        Dim interleavedData As Byte() = New Byte(ArrayLength - 1) {}
        'Start channels interleaving
        For i As Integer = 0 To ArrayLength - 1
            If (i Mod 2) = 0 Then
                If IndexLC < LeftChannelData.Length Then
                    Buffer.BlockCopy(LeftChannelData, IndexLC, interleavedData, i * interleave_block_size, interleave_block_size)
                End If
                IndexLC += interleave_block_size
            Else
                If IndexRC < RightChannelData.Length Then
                    Buffer.BlockCopy(RightChannelData, IndexRC, interleavedData, i * interleave_block_size, interleave_block_size)
                End If
                IndexRC += interleave_block_size
            End If
        Next
        File.WriteAllBytes(outputFilePath, interleavedData)
    End Sub

    '*===============================================================================================
    '* IMA ADPCM FUNCTIONS
    '*===============================================================================================
    Private Sub CreateImaAdpcm(inputLeft As String, inputRight As String, outputLeft As String, outputRight As String, waveLib As WaveFunctions)
        'FilePaths
        Dim wavePcmLeft As String = outputLeft & "_L.pcm"
        Dim wavePcmRight As String = outputRight & "_R.pcm"
        'Resampled wav
        RunProcess("SystemFiles\Sox.exe", """" & inputLeft & """ -t raw """ & wavePcmLeft & """")
        RunProcess("SystemFiles\Sox.exe", """" & inputRight & """ -t raw """ & wavePcmRight & """")
        'Wave to ima
        Dim imaLeftData As Byte() = ESUtils.ImaCodec.Encode(waveLib.ConvertByteArrayToShortArray(File.ReadAllBytes(wavePcmLeft)))
        File.WriteAllBytes(outputLeft & "_L.ima", imaLeftData)
        Dim imaRightData As Byte() = ESUtils.ImaCodec.Encode(waveLib.ConvertByteArrayToShortArray(File.ReadAllBytes(wavePcmRight)))
        File.WriteAllBytes(outputRight & "_R.ima", imaRightData)
    End Sub
End Class

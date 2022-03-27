Imports System.IO

Partial Public Class MusicsExporter
    '*===============================================================================================
    '* CREATE MUSIC STREAMS - MAIN METHOD
    '*===============================================================================================
    Private Sub CreateMusicStreams(ESMusicFolderPath As String, outputPlatforms As String())
        If Not MarkerFileOnly Then
            Dim temporalWaveFiles As New List(Of String)

            'Start main loop
            For fileIndex As Integer = 0 To outputQueue.Rows.Count - 1
                Dim musicItem As DataRow = outputQueue.Rows(fileIndex)
                Dim musicHashCode As Integer = musicItem.ItemArray(2)

                'Split Wave channels with SoX (PC & GC)
                For platformIndex As Integer = 0 To outputPlatforms.Length - 1
                    'Get the current platform
                    Dim currentPlatform As String = outputPlatforms(platformIndex)
                    Dim soundSampleData As String = Path.Combine(GetOutputFolder(musicHashCode, currentPlatform), "MFX_" & musicHashCode & ".ssd")

                    'Update title bar and progress
                    BackgroundWorker.ReportProgress(Decimal.Divide(platformIndex + (fileIndex * outputPlatforms.Length), outputQueue.Rows.Count * outputPlatforms.Length) * 100.0, "Making Music Stream: " & musicItem.ItemArray(0) & " for " & currentPlatform)

                    'Start ReSampling
                    Select Case currentPlatform
                        Case "PC", "GameCube"
                            'Get File Paths
                            Dim imaFileL As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0)) & "L.ssp"
                            Dim imaFileR As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0)) & "R.ssp"

                            'Music Stream (.ssd)
                            MergeChannels(File.ReadAllBytes(imaFileL), File.ReadAllBytes(imaFileR), 1, soundSampleData)

                            'Delete files
                            temporalWaveFiles.Add(imaFileL)
                            temporalWaveFiles.Add(imaFileR)
                        Case "PlayStation2"
                            'Get File Paths
                            Dim ps2VagL As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0)) & "L.vag"
                            Dim ps2VagR As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0)) & "R.vag"

                            'Music Stream (.ssd)
                            MergeChannels(GetVagFileDataChunk(ps2VagL), GetVagFileDataChunk(ps2VagR), 128, soundSampleData)

                            'Delete files
                            temporalWaveFiles.Add(ps2VagL)
                            temporalWaveFiles.Add(ps2VagR)
                        Case Else
                            'Xbox Tool
                            Dim xbxVagL As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0)) & "L.wav"
                            Dim xbxVagR As String = Path.Combine(ESMusicFolderPath, musicItem.ItemArray(0)) & "R.wav"

                            'Music Stream (.ssd)
                            MergeChannels(GetXboxAdpcmDataChunk(xbxVagL), GetXboxAdpcmDataChunk(xbxVagR), 4, soundSampleData)

                            'Delete files
                            temporalWaveFiles.Add(xbxVagL)
                            temporalWaveFiles.Add(xbxVagR)
                    End Select
                Next
            Next

            'Delete all temporal wave files to reduce the folder size
            For fileIndex As Integer = 0 To temporalWaveFiles.Count - 1
                File.Delete(temporalWaveFiles(fileIndex))
            Next
        End If
    End Sub

    '*===============================================================================================
    '* GET OUTPUT FOLDER (DEPENDING OF THE HASHCODE)
    '*===============================================================================================
    Private Function GetOutputFolder(fileHashCode As UInteger, currentPlatform As String) As String
        Dim folderNumber = (fileHashCode And &HF0) >> 4
        Dim markersFilePath As String = Path.Combine(WorkingDirectory, "TempOutputFolder", currentPlatform, "Music", "MFX_" & folderNumber)
        Directory.CreateDirectory(markersFilePath)
        Return markersFilePath
    End Function

    '*===============================================================================================
    '* MERGE CHANNELS (CREATES THE .SSD FILE)
    '*===============================================================================================
    Private Sub MergeChannels(LeftChannelData As Byte(), RightChannelData As Byte(), interleave_block_size As Integer, outputFilePath As String)
        Dim IndexLC, IndexRC As Integer

        'Read data and align array size
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
End Class

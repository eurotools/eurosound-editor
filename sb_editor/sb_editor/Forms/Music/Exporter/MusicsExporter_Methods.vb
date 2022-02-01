Imports System.IO

Partial Public Class MusicsExporter
    Private Function GetOutputFolder(fileHashCode As UInteger, currentPlatform As String) As String
        Dim folderNumber = (fileHashCode And &HF0) >> 4
        Dim markersFilePath As String = fso.BuildPath(WorkingDirectory, "TempOutputFolder\" & currentPlatform & "\Music\" & "MFX_" & folderNumber)
        CreateFolderIfNotExists(markersFilePath)
        Return markersFilePath
    End Function

    Private Sub MergeChannels(leftChannelFile As String, rightChannelFile As String, interleave_block_size As Integer, outputFilePath As String)
        Dim IndexLC As Integer = 0
        Dim IndexRC As Integer = 0
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
End Class

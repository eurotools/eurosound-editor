Imports System.IO
Imports NAudio.Wave

Partial Public Class MusicsExporter
    '*===============================================================================================
    '* GET OUTPUT FOLDER (DEPENDING OF THE HASHCODE)
    '*===============================================================================================
    Private Function GetOutputFolder(fileHashCode As UInteger, currentPlatform As String) As String
        Dim folderNumber = (fileHashCode And &HF0) >> 4
        Dim markersFilePath As String = fso.BuildPath(WorkingDirectory, "TempOutputFolder\" & currentPlatform & "\Music\" & "MFX_" & folderNumber)
        CreateFolderIfNotExists(markersFilePath)
        Return markersFilePath
    End Function

    '*===============================================================================================
    '* MERGE CHANNELS (CREATES THE .SSD FILE)
    '*===============================================================================================
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

    '*===============================================================================================
    '* GET DATA FOR MFX_DATA FILE
    '*===============================================================================================
    Private Function GetMfxDataDict() As Dictionary(Of UInteger, String())
        Dim dictionaryData As New Dictionary(Of UInteger, String())

        'Dictionary Data
        For Each rowData As KeyValuePair(Of String, UInteger) In hashCodesCollection
            Dim filePath As String = fso.BuildPath(WorkingDirectory, "Music\" & rowData.Key & ".wav")
            Using waveReader As New WaveFileReader(filePath)
                Dim duration As Single = (waveReader.Length / waveReader.WaveFormat.AverageBytesPerSecond) + 0.0
                Dim stringDuration As String
                If duration Mod 1 = 0 Then
                    stringDuration = duration.ToString("F1", numericProvider)
                Else
                    stringDuration = duration.ToString("G7", numericProvider)
                End If
                dictionaryData.Add(rowData.Value, New String() {stringDuration & "f", "FALSE"})
            End Using
        Next

        Return dictionaryData
    End Function
End Class

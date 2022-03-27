Imports System.IO
Imports EngineXMarkersTool

Partial Public Class MusicsExporter
    Private Sub CreateMarkerFiles(ESWorkFolderPath As String, outputPlatforms As String())
        Dim markersFunctions As New ExMarkersTool
        For fileIndex As Integer = 0 To outputQueue.Rows.Count - 1
            Dim musicItem As DataRow = outputQueue.Rows(fileIndex)
            'Update Title bar
            For platformIndex As Integer = 0 To outputPlatforms.Length - 1
                'Update progress bar
                BackgroundWorker.ReportProgress(Decimal.Divide(platformIndex + (fileIndex * outputPlatforms.Length), outputQueue.Rows.Count * outputPlatforms.Length) * 100.0, "Making Marker File: " & musicItem.ItemArray(0))
                'Get the current platform
                Dim currentPlatform As String = outputPlatforms(platformIndex)
                Dim musicHashCode As Integer = musicItem.ItemArray(2)
                'Get file data and output path
                Dim outputFilePath As String = GetOutputFolder(musicHashCode, currentPlatform)
                'Get Common files paths and create the jump maker file
                Dim jumpMarkersFile As String = Path.Combine(ESWorkFolderPath, musicItem.ItemArray(0) & ".jmp")
                Dim soundMarkerFile As String = Path.Combine(outputFilePath, "MFX_" & musicHashCode & ".smf")
                'Get Marker file path
                Dim mrkFilePath As String = Path.Combine(WorkingDirectory, "Music", musicItem.ItemArray(0) & ".mrk")
                If StrComp(currentPlatform, "PC") = 0 Or StrComp(currentPlatform, "GameCube") = 0 Then
                    'Get Ima files
                    Dim ImaOutLeft As String = Path.Combine(ESWorkFolderPath, musicItem.ItemArray(0)) & ".asl"
                    Dim ImaOutRight As String = Path.Combine(ESWorkFolderPath, musicItem.ItemArray(0)) & ".asr"
                    markersFunctions.CreateMusicMarkers(ImaOutLeft, ImaOutRight, mrkFilePath, jumpMarkersFile, soundMarkerFile, currentPlatform, musicItem.ItemArray(1))
                Else
                    markersFunctions.CreateMusicMarkers(Nothing, Nothing, mrkFilePath, jumpMarkersFile, soundMarkerFile, currentPlatform, musicItem.ItemArray(1))
                End If
            Next
        Next
    End Sub
End Class

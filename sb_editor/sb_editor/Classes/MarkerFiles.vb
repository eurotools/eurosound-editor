Public Class MarkerFiles
    Private Sub AddMarkerBlock(fileNumber As Integer, markerName As String, position As Integer, markerType As Integer, markerFlags As Integer, markerIndex As Integer)
        If markerIndex > 0 Then
            PrintLine(fileNumber, "    Marker" & markerIndex)
        Else
            PrintLine(fileNumber, "    Marker")
        End If
        PrintLine(fileNumber, "    {")
        PrintLine(fileNumber, "        Name=" & markerName)
        PrintLine(fileNumber, "        Pos=" + Trim(Str(position)))
        PrintLine(fileNumber, "        Type=" + Trim(Str(markerType)))
        PrintLine(fileNumber, "        Flags=" & markerFlags)
        PrintLine(fileNumber, "        Extra=0")
        PrintLine(fileNumber, "    }")
    End Sub

    Friend Sub CreateStreamMarkerFile(filepath As String, sampleChunkData As Integer(), fileTotalSamples As Integer)
        FileOpen(1, filepath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(1, "Markers")
        PrintLine(1, "{")
        AddMarkerBlock(1, "Stream Start Marker", 0, 10, 2, -1)
        If sampleChunkData(0) = 0 Then 'No loop
            AddMarkerBlock(1, "Stream End Marker", fileTotalSamples, 9, 2, -1)
        Else
            AddMarkerBlock(1, "Stream Start Loop", sampleChunkData(1), 6, 2, -1)
            AddMarkerBlock(1, "Stream End Loop", sampleChunkData(2), 10, 2, -1)
        End If
        PrintLine(1, "}")
        FileClose(1)
    End Sub

    Friend Sub CreateMusicMarkerFile(filePath As String, markerData As SortedDictionary(Of UInteger, MarkerObject))
        FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(1, "Markers")
        PrintLine(1, "{")
        Dim markerIndex As Integer = 0
        For Each markerToPrint As KeyValuePair(Of UInteger, MarkerObject) In markerData
            AddMarkerBlock(1, markerToPrint.Value.MarkerName, markerToPrint.Value.MarkerPosition, markerToPrint.Value.MarkerType, 0, markerIndex)
            markerIndex += 1
        Next
        PrintLine(1, "}")
        FileClose(1)
    End Sub

End Class

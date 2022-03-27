Imports System.IO

Namespace MarkerFunctions
    Public Class MarkerFiles
        Private Sub AddMarkerBlock(outputFile As StreamWriter, markerName As String, position As Integer, markerType As Integer, markerFlags As Integer, markerIndex As Integer)
            If markerIndex > 0 Then
                outputFile.WriteLine(vbTab & "Marker" & markerIndex)
            Else
                outputFile.WriteLine(vbTab & "Marker")
            End If
            outputFile.WriteLine(vbTab & "{")
            outputFile.WriteLine(vbTab & vbTab & "Name=" & markerName)
            outputFile.WriteLine(vbTab & vbTab & "Pos=" + Trim(Str(position)))
            outputFile.WriteLine(vbTab & vbTab & "Type=" + Trim(Str(markerType)))
            outputFile.WriteLine(vbTab & vbTab & "Flags=" & markerFlags)
            outputFile.WriteLine(vbTab & vbTab & "Extra=0")
            outputFile.WriteLine(vbTab & "}")
        End Sub

        Friend Sub CreateStreamMarkerFile(filepath As String, sampleChunkData As Integer(), fileTotalSamples As Integer)
            Using outputFile As New StreamWriter(filepath)
                outputFile.WriteLine("Markers")
                outputFile.WriteLine("{")
                AddMarkerBlock(outputFile, "Stream Start Marker", 0, 10, 2, -1)
                If sampleChunkData(0) = 0 Then 'No loop
                    AddMarkerBlock(outputFile, "Stream End Marker", fileTotalSamples, 9, 2, -1)
                Else
                    AddMarkerBlock(outputFile, "Stream Start Loop", sampleChunkData(1), 6, 2, -1)
                    AddMarkerBlock(outputFile, "Stream End Loop", sampleChunkData(2), 10, 2, -1)
                End If
                outputFile.WriteLine("}")
            End Using
        End Sub

        Friend Sub CreateMusicMarkerFile(filePath As String, markerData As SortedDictionary(Of UInteger, MarkerObject))
            Using outputFile As New StreamWriter(filePath)
                outputFile.WriteLine("Markers")
                outputFile.WriteLine("{")
                Dim markerIndex As Integer = 0
                For Each markerToPrint As KeyValuePair(Of UInteger, MarkerObject) In markerData
                    AddMarkerBlock(outputFile, markerToPrint.Value.MarkerName, markerToPrint.Value.MarkerPosition, markerToPrint.Value.MarkerType, 0, markerIndex)
                    markerIndex += 1
                Next
                outputFile.WriteLine("}")
            End Using
        End Sub

        Friend Function GetMaxFilePosition(filepath As String) As Integer
            Dim maxPosition = 0

            Using sr As New StreamReader(File.OpenRead(filepath))
                While Not sr.EndOfStream
                    'Read text file
                    Dim currentLine As String = sr.ReadLine.Trim
                    If currentLine.StartsWith("Pos=", StringComparison.OrdinalIgnoreCase) Then
                        maxPosition = Math.Max(maxPosition, CInt(currentLine.Substring(4)))
                    End If
                End While
            End Using

            Return maxPosition
        End Function
    End Class
End Namespace

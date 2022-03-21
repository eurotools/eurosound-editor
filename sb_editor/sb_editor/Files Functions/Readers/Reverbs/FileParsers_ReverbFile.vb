Imports System.IO
Imports sb_editor.ReverbObj

Namespace ReaderClasses
    Partial Public Class FileParsers
        Friend Function ReadReverbFile(textFilePath As String) As ReverbFile
            Dim rvrbObj As New ReverbFile

            'Open file and read it
            Using sr As New StreamReader(File.OpenRead(textFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Then
                        Continue While
                    Else
                        'Check for hashcodes block
                        If currentLine.Equals("#MiscData", StringComparison.OrdinalIgnoreCase) Then
                            'Read a new line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Split line and get number
                                Dim lineSplitData As String() = currentLine.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
                                rvrbObj.HashCode = lineSplitData(1)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'PC Reverb Platform
                        If currentLine.Equals("#PCReverb", StringComparison.OrdinalIgnoreCase) Then
                            rvrbObj.PCReverb.RoomSize = sr.ReadLine.Trim
                            rvrbObj.PCReverb.Width = sr.ReadLine.Trim
                            rvrbObj.PCReverb.Damp = sr.ReadLine.Trim
                            rvrbObj.PCReverb.LowPassFilter = sr.ReadLine.Trim
                            rvrbObj.PCReverb.Filter1 = sr.ReadLine.Trim
                            rvrbObj.PCReverb.Filter2 = sr.ReadLine.Trim
                        End If

                        'Xbox Reverb Platform
                        If currentLine.Equals("#XBReverb", StringComparison.OrdinalIgnoreCase) Then
                            rvrbObj.XBReverb.RoomSize = sr.ReadLine.Trim
                            rvrbObj.XBReverb.Width = sr.ReadLine.Trim
                            rvrbObj.XBReverb.Damp = sr.ReadLine.Trim
                            rvrbObj.XBReverb.LowPassFilter = sr.ReadLine.Trim
                            rvrbObj.XBReverb.Filter1 = sr.ReadLine.Trim
                            rvrbObj.XBReverb.Filter2 = sr.ReadLine.Trim
                        End If

                        'GameCube Reverb Platform
                        If currentLine.Equals("#GCReverb", StringComparison.OrdinalIgnoreCase) Then
                            rvrbObj.GCReverb.RoomSize = sr.ReadLine.Trim
                            rvrbObj.GCReverb.Width = sr.ReadLine.Trim
                            rvrbObj.GCReverb.Damp = sr.ReadLine.Trim
                            rvrbObj.GCReverb.LowPassFilter = sr.ReadLine.Trim
                            rvrbObj.GCReverb.Filter1 = sr.ReadLine.Trim
                            rvrbObj.GCReverb.Filter2 = sr.ReadLine.Trim
                        End If
                    End If
                End While
            End Using

            Return rvrbObj
        End Function
    End Class
End Namespace

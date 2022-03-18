Imports System.IO

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* REFINE LIST
        '*===============================================================================================
        Friend Function ReadRefineList(refineFilePath As String) As String()
            Dim refineKeywords As New List(Of String)
            Using sr As New StreamReader(File.OpenRead(refineFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Then
                        Continue While
                    Else
                        'Streams Block
                        If currentLine.Equals("#RefineSearch", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                refineKeywords.Add(currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If
                    End If
                End While
            End Using

            Return refineKeywords.ToArray
        End Function
    End Class
End Namespace

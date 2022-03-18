Imports System.IO

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* PURGE LIST
        '*===============================================================================================
        Public Function ReadPurgeList(textFilePath As String) As String()
            'Create an object
            Dim dependencies As New List(Of String)

            'Open file and read it
            Using sr As New StreamReader(File.OpenRead(textFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Then
                        Continue While
                    Else
                        If currentLine.Equals("#PurgedFileList", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Add item to listbox
                                dependencies.Add(currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If
                    End If
                End While
            End Using

            Return dependencies.ToArray
        End Function
    End Class
End Namespace
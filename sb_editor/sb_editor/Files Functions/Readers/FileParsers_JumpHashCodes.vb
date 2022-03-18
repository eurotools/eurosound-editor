Imports System.IO

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* JUMP FILES
        '*===============================================================================================
        Public Function ReadJumpFile(textFilePath As String) As String()
            'Create an object
            Dim jumpHashCodesList As New List(Of String)

            'Open file and read it
            Using sr As StreamReader = File.OpenText(textFilePath)
                While Not sr.EndOfStream
                    'Read text file
                    Dim currentLine As String = sr.ReadLine.Trim
                    If currentLine.Equals("#JUMPMARKERS", StringComparison.OrdinalIgnoreCase) Then
                        'Read line
                        currentLine = sr.ReadLine.Trim
                        While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                            'Add item to listbox
                            jumpHashCodesList.Add(currentLine)
                            'Continue Reading
                            currentLine = sr.ReadLine.Trim
                        End While
                    End If
                End While
            End Using

            Return jumpHashCodesList.ToArray
        End Function
    End Class
End Namespace
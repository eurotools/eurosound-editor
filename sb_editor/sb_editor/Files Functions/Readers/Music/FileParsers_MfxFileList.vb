Imports System.IO

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* MFX FILE
        '*===============================================================================================
        Public Function ReadMfxFileList(textFilePath As String) As String()
            'Create an object
            Dim mfxFilesList As New HashSet(Of String)

            'Open file and read it
            Using sr As New StreamReader(File.OpenRead(textFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Check for Hashcode block
                    If StrComp(currentLine, "#MFXFiles", CompareMethod.Text) = 0 Then
                        'Read line
                        currentLine = sr.ReadLine.Trim
                        While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                            'Split line and get number
                            mfxFilesList.Add(currentLine)
                            'Continue Reading
                            currentLine = sr.ReadLine.Trim
                        End While
                    End If
                End While
            End Using

            Return mfxFilesList.ToArray
        End Function
    End Class
End Namespace

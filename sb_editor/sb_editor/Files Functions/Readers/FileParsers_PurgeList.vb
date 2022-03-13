Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* DATABASE FILES
        '*===============================================================================================
        Public Function ReadPurgeList(textFilePath As String) As String()
            'Create an object
            Dim dependencies As New List(Of String)

            'Open file and read it
            Dim currentLine As String
            FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read text file
                currentLine = Trim(LineInput(1))

                If StrComp(currentLine, "#PurgedFileList", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                        'Add item to listbox
                        dependencies.Add(currentLine)
                        'Continue Reading
                        currentLine = Trim(LineInput(1))
                    End While
                End If
            Loop
            FileClose(1)

            Return dependencies.ToArray
        End Function
    End Class
End Namespace
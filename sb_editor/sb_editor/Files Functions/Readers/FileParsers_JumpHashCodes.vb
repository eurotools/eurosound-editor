Partial Public Class FileParsers
    '*===============================================================================================
    '* DATABASE FILES
    '*===============================================================================================
    Public Function ReadJumpFile(textFilePath As String) As String()
        'Create an object
        Dim jumpHashCodesList As New List(Of String)

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            currentLine = Trim(LineInput(1))
            If StrComp(currentLine, "#JUMPMARKERS", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = Trim(LineInput(1))
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        'Add item to listbox
                        jumpHashCodesList.Add(currentLine)
                        'Continue Reading
                        currentLine = Trim(LineInput(1))
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                End If
            End If
        Loop
        FileClose(1)

        Return jumpHashCodesList.ToArray
    End Function
End Class
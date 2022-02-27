Partial Public Class FileParsers
    Friend Function ReadRefineList(refineFilePath As String) As String()
        Dim refineKeywords As New List(Of String)

        FileOpen(1, refineFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            Dim currentLine As String = Trim(LineInput(1))
            'Streams Block
            If StrComp(currentLine, "#RefineSearch", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = Trim(LineInput(1))
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        refineKeywords.Add(currentLine)
                        'Continue Reading
                        currentLine = Trim(LineInput(1))
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                End If
            End If
        Loop
        FileClose(1)

        Return refineKeywords.ToArray
    End Function
End Class

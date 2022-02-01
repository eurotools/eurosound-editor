Partial Public Class FileParsers
    Friend Function ReadRefineList(refineFilePath As String) As String()
        Dim refineKeywords As New Collection

        FileOpen(1, refineFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            Dim currentLine As String = LineInput(1)
            'Streams Block
            If StrComp(currentLine, "#RefineSearch", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        refineKeywords.Add(currentLine)
                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                End If
            End If
        Loop
        FileClose(1)

        'Collection to Array
        Dim keywordsArray As String() = New String(refineKeywords.Count - 1) {}
        For index As Integer = 1 To refineKeywords.Count
            keywordsArray(index - 1) = refineKeywords(index)
        Next

        ReadRefineList = keywordsArray
    End Function
End Class

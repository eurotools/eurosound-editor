Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* MFX FILE
        '*===============================================================================================
        Public Function ReadMfxFileList(textFilePath As String) As String()
            'Create an object
            Dim mfxFilesList As New HashSet(Of String)

            'Open file and read it
            Dim currentLine As String
            FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read text file
                currentLine = Trim(LineInput(1))
                'Check for Hashcode block
                If StrComp(currentLine, "#MFXFiles", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Split line and get number
                            mfxFilesList.Add(currentLine)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                    End If
                End If
            Loop
            FileClose(1)

            Return mfxFilesList.ToArray
        End Function
    End Class
End Namespace

Imports sb_editor.ParsersObjects

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* DATABASE FILES
        '*===============================================================================================
        Public Function ReadDataBaseFile(textFilePath As String) As DataBaseFile
            'Create an object
            Dim DatabaseObj As New DataBaseFile
            Dim dependencies As New List(Of String)

            'Open file and read it
            Dim currentLine As String
            FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read text file
                currentLine = Trim(LineInput(1))

                'Header info
                If InStr(1, currentLine, "## ") = 1 Then
                    'Split content
                    Dim lineData As String() = Split(currentLine, "...")

                    'Get header info
                    If InStr(currentLine, "## EuroSound") = 1 Then
                        DatabaseObj.HeaderInfo.FileHeader = currentLine
                    End If
                    If InStr(currentLine, "## First Created ...") = 1 Then
                        DatabaseObj.HeaderInfo.FirstCreated = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Created By ...") = 1 Then
                        DatabaseObj.HeaderInfo.CreatedBy = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Last Modified ...") = 1 Then
                        DatabaseObj.HeaderInfo.LastModify = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Last Modified By ...") = 1 Then
                        DatabaseObj.HeaderInfo.LastModifyBy = Trim(lineData(1))
                    End If
                End If

                If StrComp(currentLine, "#DEPENDENCIES", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Add item to listbox
                            dependencies.Add(currentLine)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                    End If
                End If
            Loop
            FileClose(1)

            'Parse items to an array
            If dependencies.Count > 0 Then
                DatabaseObj.Dependencies = dependencies.ToArray
            End If

            Return DatabaseObj
        End Function
    End Class
End Namespace
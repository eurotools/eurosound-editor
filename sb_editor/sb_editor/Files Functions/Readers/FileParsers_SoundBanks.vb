Imports sb_editor.ParsersObjects

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* SOUNDBANK FILES
        '*===============================================================================================
        Public Function ReadSoundBankFile(textFilePath As String) As SoundbankFile
            'Declare a new object
            Dim objSB As New SoundbankFile

            'List to store dependencies
            Dim dependencies As New List(Of String)

            'Open file and read it
            Dim currentLine As String
            FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read a new line
                currentLine = Trim(LineInput(1))

                'Header info
                If InStr(currentLine, "## ") = 1 Then
                    'Split content
                    Dim lineData As String() = Split(currentLine, "...")
                    'Get header info
                    If InStr(currentLine, "## EuroSound") = 1 Then
                        objSB.HeaderInfo.FileHeader = currentLine
                    End If
                    If InStr(currentLine, "## First Created ...") = 1 Then
                        objSB.HeaderInfo.FirstCreated = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Created By ...") = 1 Then
                        objSB.HeaderInfo.CreatedBy = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Last Modified ...") = 1 Then
                        objSB.HeaderInfo.LastModify = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Last Modified By ...") = 1 Then
                        objSB.HeaderInfo.LastModifyBy = Trim(lineData(1))
                    End If
                End If

                'Check for Dependencies block
                If StrComp(currentLine, "#DEPENDENCIES", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Add item to listbox
                            dependencies.Add(currentLine)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                    End If
                End If

                'Check for hashcodes block
                If StrComp(currentLine, "#HASHCODE", CompareMethod.Binary) = 0 Then
                    'Read a new line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Split line and get number
                            objSB.HashCode = Split(currentLine, " ")(1)
                            objSB.HashCodeLabel = GetOnlyFileName(textFilePath)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                    End If
                End If

                'Check for max bank sizes block
                If StrComp(currentLine, "#MaxBankSizes", CompareMethod.Binary) = 0 Then
                    'Read a new line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                            Select Case UCase(lineData(0))
                                Case "PLAYSTATIONSIZE"
                                    objSB.MaxBankSizes.PlayStationSize = CInt(lineData(1))
                                Case "PCSIZE"
                                    objSB.MaxBankSizes.PCSize = CByte(lineData(1))
                                Case "XBOXSIZE"
                                    objSB.MaxBankSizes.XboxSize = CInt(lineData(1))
                                Case "GAMECUBESIZE"
                                    objSB.MaxBankSizes.GameCubeSize = CInt(lineData(1))
                            End Select
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                    End If
                End If
            Loop
            FileClose(1)

            'Add data to object
            If dependencies.Count > 0 Then
                objSB.Dependencies = dependencies.ToArray
            End If

            Return objSB
        End Function
    End Class
End Namespace

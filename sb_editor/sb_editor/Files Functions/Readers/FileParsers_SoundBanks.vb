Imports System.IO

Partial Public Class FileParsers
    '*===============================================================================================
    '* SOUNDBANK FILES
    '*===============================================================================================
    Public Function ReadSoundBankFile(textFilePath As String) As SoundbankFile
        'Declare a new object
        Dim objSB As New SoundbankFile

        'List to store dependencies
        Dim dependencies As New Collection

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read a new line
            currentLine = LineInput(1)

            'Header info
            If InStr(currentLine, "## ") = 1 Then
                'Split content
                Dim lineData As String() = Split(currentLine, "...")

                'Get header info
                If InStr(currentLine, "## EuroSound") = 1 Then
                    objSB.HeaderInfo.FileHeader = currentLine
                End If
                If InStr(currentLine, "## First Created ...") = 1 Then
                    objSB.HeaderInfo.FirstCreated = lineData(1).Trim
                End If
                If InStr(currentLine, "## Created By ...") = 1 Then
                    objSB.HeaderInfo.CreatedBy = lineData(1).Trim
                End If
                If InStr(currentLine, "## Last Modified ...") = 1 Then
                    objSB.HeaderInfo.LastModify = lineData(1).Trim
                End If
                If InStr(currentLine, "## Last Modified By ...") = 1 Then
                    objSB.HeaderInfo.LastModifyBy = lineData(1).Trim
                End If
            End If

            'Check for Dependencies block
            If StrComp(currentLine, "#DEPENDENCIES", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        'Add item to listbox
                        dependencies.Add(currentLine)

                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                End If
            End If

            'Check for hashcodes block
            If StrComp(currentLine, "#HASHCODE", CompareMethod.Binary) = 0 Then
                'Read a new line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        'Split line and get number
                        objSB.HashCode = Split(currentLine, " ")(1)
                        objSB.HashCodeLabel = Path.GetFileNameWithoutExtension(textFilePath)

                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                End If
            End If

            'Check for max bank sizes block
            If StrComp(currentLine, "#MaxBankSizes", CompareMethod.Binary) = 0 Then
                'Read a new line
                currentLine = LineInput(1)
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
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                End If
            End If
        Loop
        FileClose(1)

        'Add data to object
        objSB.Dependencies = CollectionToArray(dependencies)

        Return objSB
    End Function

    Friend Sub GetSoundbankDependencies(soundbankFilePath As String, soundbankNode As TreeNode)
        'Open file and read it
        Dim currentLine As String
        FileOpen(1, soundbankFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            currentLine = LineInput(1)

            'Dependencies block
            If StrComp(currentLine, "#DEPENDENCIES", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        'Trim string and add a child node
                        Dim databaseName As String = currentLine.Trim()
                        soundbankNode.Nodes.Add(databaseName, databaseName, 2, 2)

                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                End If
            End If

            'Read HashCode
            If StrComp(currentLine, "#HASHCODE") = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        Dim SoundbankHashcode = Convert.ToUInt32(currentLine.Split(" "c)(1))
                        soundbankNode.Name = SoundbankHashcode.ToString()

                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                End If
            End If
        Loop
        FileClose(1)

        'Add empty node if required
        If soundbankNode.Nodes.Count = 0 Then
            soundbankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
        End If
    End Sub
End Class

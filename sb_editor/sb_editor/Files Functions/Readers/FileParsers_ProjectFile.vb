﻿Imports sb_editor.ParsersObjects

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* PROJECT FILE
        '*===============================================================================================
        Public Function ReadProjectFile(projectFilePath As String) As ProjectFile
            Dim ProjFile As New ProjectFile

            'Temporal lists
            Dim SoundBankList As New List(Of String)
            Dim DataBaseList As New List(Of String)
            Dim SFXList As New List(Of String)

            'Open file and read it
            Dim currentLine As String
            FileOpen(1, projectFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read text file
                currentLine = Trim(LineInput(1))

                'Header info
                If InStr(currentLine, "## ") = 1 Then
                    'Split content
                    Dim lineData As String() = Split(currentLine, "...")

                    'Get header info
                    If InStr(currentLine, "## EuroSound") = 1 Then
                        ProjFile.HeaderInfo.FileHeader = currentLine
                    End If
                    If InStr(currentLine, "## First Created ...") = 1 Then
                        ProjFile.HeaderInfo.FirstCreated = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Created By ...") = 1 Then
                        ProjFile.HeaderInfo.CreatedBy = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Last Modified ...") = 1 Then
                        ProjFile.HeaderInfo.LastModify = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Last Modified By ...") = 1 Then
                        ProjFile.HeaderInfo.LastModifyBy = Trim(lineData(1))
                    End If
                End If

                'Soundbanks section
                If StrComp(currentLine, "#SoundBankList", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Add item to listbox
                            SoundBankList.Add(currentLine)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                    End If
                End If

                'Database Section
                If StrComp(currentLine, "#DataBaseList", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Add item to listbox
                            DataBaseList.Add(currentLine)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                    End If
                End If

                'SFXs Section
                If StrComp(currentLine, "#SFXList", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Add item to listbox
                            SFXList.Add(currentLine)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                    End If
                End If
            Loop
            FileClose(1)

            'Add data to object
            ProjFile.DataBaseList = DataBaseList
            ProjFile.SoundBankList = SoundBankList
            ProjFile.SFXList = SFXList

            Return ProjFile
        End Function
    End Class
End Namespace
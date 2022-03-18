Imports System.IO
Imports sb_editor.ParsersObjects

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
            Using sr As New StreamReader(File.OpenRead(projectFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Then
                        Continue While
                    Else
                        'Header info
                        If currentLine.Contains("## ") Then
                            'Split content
                            Dim lineData As String() = Split(currentLine, "...")

                            'Get header info
                            If currentLine.Contains("## EuroSound") Then
                                ProjFile.HeaderInfo.FileHeader = currentLine
                            End If
                            If currentLine.Contains("## First Created ...") Then
                                ProjFile.HeaderInfo.FirstCreated = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Created By ...") Then
                                ProjFile.HeaderInfo.CreatedBy = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified ...") Then
                                ProjFile.HeaderInfo.LastModify = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified By ...") Then
                                ProjFile.HeaderInfo.LastModifyBy = lineData(1).Trim
                            End If
                        End If

                        'Soundbanks section
                        If currentLine.Equals("#SoundBankList", StringComparison.OrdinalIgnoreCase) Then
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Add item to listbox
                                SoundBankList.Add(currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Database Section
                        If currentLine.Equals("#DataBaseList", StringComparison.OrdinalIgnoreCase) Then
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Add item to listbox
                                DataBaseList.Add(currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'SFXs Section
                        If currentLine.Equals("#SFXList", StringComparison.OrdinalIgnoreCase) Then
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Add item to listbox
                                SFXList.Add(currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If
                    End If
                End While
            End Using

            'Add data to object
            ProjFile.DataBaseList = DataBaseList.ToArray
            ProjFile.SoundBankList = SoundBankList.ToArray
            ProjFile.SFXList = SFXList.ToArray

            Return ProjFile
        End Function
    End Class
End Namespace
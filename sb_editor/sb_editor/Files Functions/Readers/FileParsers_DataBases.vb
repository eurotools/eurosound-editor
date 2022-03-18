Imports System.IO
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
            Using sr As New StreamReader(File.OpenRead(textFilePath))
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
                                DatabaseObj.HeaderInfo.FileHeader = currentLine
                            End If
                            If currentLine.Contains("## First Created ...") Then
                                DatabaseObj.HeaderInfo.FirstCreated = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Created By ...") Then
                                DatabaseObj.HeaderInfo.CreatedBy = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified ...") Then
                                DatabaseObj.HeaderInfo.LastModify = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified By ...") Then
                                DatabaseObj.HeaderInfo.LastModifyBy = lineData(1).Trim
                            End If
                        End If

                        'Get SFXs included in this database
                        If currentLine.Equals("#DEPENDENCIES", StringComparison.OrdinalIgnoreCase) Then
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Add item to listbox
                                dependencies.Add(currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If
                    End If
                End While
            End Using

            'Parse items to an array
            If dependencies.Count > 0 Then
                DatabaseObj.Dependencies = dependencies.ToArray
            End If

            Return DatabaseObj
        End Function
    End Class
End Namespace
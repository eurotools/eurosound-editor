Imports System.IO
Imports sb_editor.ParsersObjects

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* CLASS GLOBAL FUNCTIONS
        '*===============================================================================================
        Private Function GetStringValue(keyword As String, currentLine As String) As String
            Dim keyWordLength = keyword.Length
            Dim LineData As String = currentLine.Substring(keyWordLength).Trim
            Return LineData
        End Function

        '*===============================================================================================
        '* GET HEADER INFO
        '*===============================================================================================
        Friend Function GetFileHeaderInfo(filePath As String) As FileHeader
            'Create Variables
            Dim headerInfo As New FileHeader

            'Open file and read it
            Using sr As New StreamReader(File.OpenRead(filePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Then
                        Exit While
                    Else
                        'Header info
                        If currentLine.Contains("## ") Then
                            'Split content
                            Dim lineData As String() = Split(currentLine, "...")

                            'Get header info
                            If currentLine.Contains("## EuroSound") Then
                                headerInfo.FileHeader = currentLine
                            End If
                            If currentLine.Contains("## First Created ...") Then
                                headerInfo.FirstCreated = GetStringValue("## First Created ...", currentLine)
                            End If
                            If currentLine.Contains("## Created By ...") Then
                                headerInfo.CreatedBy = GetStringValue("## Created By ...", currentLine)
                            End If
                            If currentLine.Contains("## Last Modified ...") Then
                                headerInfo.LastModify = GetStringValue("## Last Modified ...", currentLine)
                            End If
                            If currentLine.Contains("## Last Modified By ...") Then
                                headerInfo.LastModifyBy = GetStringValue("## Last Modified By ...", currentLine)
                            End If
                        Else
                            Exit While
                        End If
                    End If
                End While
            End Using

            'Return data
            GetFileHeaderInfo = headerInfo
        End Function
    End Class
End Namespace

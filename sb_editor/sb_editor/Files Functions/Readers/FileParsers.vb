Imports sb_editor.ParsersObjects

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* CLASS GLOBAL FUNCTIONS
        '*===============================================================================================
        Private Function GetStringValue(keyword As String, currentLine As String) As String
            Dim keyWordLength = keyword.Length
            Dim LineData As String = Trim(currentLine.Substring(keyWordLength))
            Return LineData
        End Function

        '*===============================================================================================
        '* GET HEADER INFO
        '*===============================================================================================
        Friend Function GetFileHeaderInfo(filePath As String) As FileHeader
            'Create Variables
            Dim headerInfo As New FileHeader
            Dim lineCounter As Integer = 0

            'Open file and read it
            Dim currentLine As String
            FileOpen(1, filePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1) Or lineCounter > 4
                'Read text file
                currentLine = LineInput(1)

                'Header info
                If InStr(currentLine, "## ") > 0 Then
                    'Split content
                    Dim lineData As String() = Split(currentLine, "...")

                    'Get header info
                    If InStr(currentLine, "## EuroSound") = 1 Then
                        headerInfo.FileHeader = currentLine
                    End If
                    If InStr(currentLine, "## First Created ...") = 1 Then
                        headerInfo.FirstCreated = GetStringValue("## First Created ...", currentLine)
                    End If
                    If InStr(currentLine, "## Created By ...") = 1 Then
                        headerInfo.CreatedBy = GetStringValue("## Created By ...", currentLine)
                    End If
                    If InStr(currentLine, "## Last Modified ...") = 1 Then
                        headerInfo.LastModify = GetStringValue("## Last Modified ...", currentLine)
                    End If
                    If InStr(currentLine, "## Last Modified By ...") = 1 Then
                        headerInfo.LastModifyBy = GetStringValue("## Last Modified By ...", currentLine)
                    End If
                End If

                'Update counter
                lineCounter += 1
            Loop
            FileClose(1)

            'Return data
            GetFileHeaderInfo = headerInfo
        End Function
    End Class
End Namespace

Imports System.IO
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
                                objSB.HeaderInfo.FileHeader = currentLine
                            End If
                            If currentLine.Contains("## First Created ...") Then
                                objSB.HeaderInfo.FirstCreated = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Created By ...") Then
                                objSB.HeaderInfo.CreatedBy = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified ...") Then
                                objSB.HeaderInfo.LastModify = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified By ...") Then
                                objSB.HeaderInfo.LastModifyBy = lineData(1).Trim
                            End If
                        End If

                        'Check for Dependencies block
                        If currentLine.Equals("#DEPENDENCIES", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Add item to listbox
                                dependencies.Add(currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Check for hashcodes block
                        If currentLine.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase) Then
                            'Read a new line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Split line and get number
                                objSB.HashCode = Split(currentLine, " ")(1)
                                objSB.HashCodeLabel = Path.GetFileNameWithoutExtension(textFilePath)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Check for max bank sizes block
                        If currentLine.Equals("#MaxBankSizes", StringComparison.OrdinalIgnoreCase) Then
                            'Read a new line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                Select Case lineData(0).ToUpper
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
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If
                    End If
                End While
            End Using

            'Add data to object
            If dependencies.Count > 0 Then
                objSB.Dependencies = dependencies.ToArray
            End If

            Return objSB
        End Function
    End Class
End Namespace

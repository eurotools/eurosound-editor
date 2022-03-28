Imports System.IO

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* Misc File
        '*===============================================================================================
        Friend Function ReadMiscFile(miscFilePath As String) As String
            Dim projectVersion As String = ""
            Using sr As New StreamReader(File.OpenRead(miscFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Then
                        Continue While
                    Else
                        'File Version
                        If currentLine.Equals("#VERSION", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                projectVersion = GetStringValue("VersionNumber", currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Streams Block
                        If currentLine.Equals("#STREAMS", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                If lineData(0).Equals("RESAMPLESTREAMS", StringComparison.OrdinalIgnoreCase) Then
                                    ReSampleStreams = CUInt(lineData(1))
                                End If
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'HashCodes Block
                        If currentLine.Equals("#HASHCODES", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                Select Case lineData(0).ToUpper
                                    Case "SFXHASHCODENUMBER"
                                        SFXHashCodeNumber = CInt(lineData(1))
                                    Case "SOUNDBANKHASHCODENUMBER"
                                        SoundBankHashCodeNumber = CInt(lineData(1))
                                    Case "MFXHASHCODENUMBER"
                                        MFXHashCodeNumber = CInt(lineData(1))
                                    Case "REVERBHASHCODENUMBER"
                                        ReverbHashCodeNumber = CInt(lineData(1))
                                End Select
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If
                    End If
                End While
            End Using

            Return projectVersion
        End Function
    End Class
End Namespace

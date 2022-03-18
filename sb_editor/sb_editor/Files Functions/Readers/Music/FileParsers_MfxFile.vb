Imports System.IO
Imports sb_editor.ParsersObjects

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* MFX FILE
        '*===============================================================================================
        Public Function ReadMfxFile(textFilePath As String) As MfxFile
            'Create an object
            Dim mfxFilesList As New List(Of String)
            Dim mfxObj As New MfxFile

            'Open file and read it
            Using sr As New StreamReader(File.OpenRead(textFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Check for Hashcode block
                    If currentLine.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase) Then
                        'Read line
                        currentLine = sr.ReadLine.Trim
                        While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                            'Split line and get number
                            mfxObj.HashCode = Split(currentLine, " ")(1)
                            'Continue Reading
                            currentLine = sr.ReadLine.Trim
                        End While
                    End If
                    'Check for Music Data block
                    If currentLine.Equals("#MusicData", StringComparison.OrdinalIgnoreCase) Then
                        'Read line
                        currentLine = sr.ReadLine.Trim
                        While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                            Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                            Select Case lineData(0).ToUpper
                                Case "VOLUME"
                                    mfxObj.Volume = CSByte(lineData(1))
                                Case "USERVALUE"
                                    mfxObj.UserValue = CUInt(lineData(1))
                            End Select
                            'Continue Reading
                            currentLine = sr.ReadLine.Trim
                        End While
                    End If
                    'Check for Time Stamps block
                    If currentLine.Equals("#TIMESTAMPS", StringComparison.OrdinalIgnoreCase) Then
                        'Read line
                        currentLine = sr.ReadLine.Trim
                        While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                            Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                            Select Case lineData(0).ToUpper
                                Case "MIDIFILELASTOUTPUT"
                                    mfxObj.MidiFileLastOutput = GetStringValue("MIDIFILELASTOUTPUT", currentLine)
                                Case "WAVFILELASTOUTPUT"
                                    mfxObj.WavFileLastOutput = GetStringValue("WAVFILELASTOUTPUT", currentLine)
                            End Select
                            'Continue Reading
                            currentLine = sr.ReadLine.Trim
                        End While
                    End If
                End While
            End Using

            Return mfxObj
        End Function
    End Class
End Namespace

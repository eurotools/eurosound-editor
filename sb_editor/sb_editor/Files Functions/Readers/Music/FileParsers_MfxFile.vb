Partial Public Class FileParsers
    '*===============================================================================================
    '* MFX FILE
    '*===============================================================================================
    Public Function ReadMfxFile(textFilePath As String) As MfxFile
        'Create an object
        Dim mfxFilesList As New List(Of String)
        Dim mfxObj As New MfxFile

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            currentLine = LineInput(1)
            'Check for Hashcode block
            If StrComp(currentLine, "#HASHCODE", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        'Split line and get number
                        mfxObj.HashCode = Split(currentLine, " ")(1)
                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                End If
            End If
            'Check for Music Data block
            If StrComp(currentLine, "#MusicData") = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END") <> 0 Then
                    Do
                        Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                        Select Case UCase(lineData(0))
                            Case "VOLUME"
                                mfxObj.Volume = CSByte(lineData(1))
                            Case "USERVALUE"
                                mfxObj.UserValue = CUInt(lineData(1))
                        End Select
                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END") <> 0
                End If
            End If
            'Check for Time Stamps block
            If StrComp(currentLine, "#TIMESTAMPS", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                        Select Case UCase(lineData(0))
                            Case "MIDIFILELASTOUTPUT"
                                mfxObj.MidiFileLastOutput = GetStringValue("MIDIFILELASTOUTPUT", currentLine)
                            Case "WAVFILELASTOUTPUT"
                                mfxObj.WavFileLastOutput = GetStringValue("WAVFILELASTOUTPUT", currentLine)
                        End Select
                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                End If
            End If
        Loop
        FileClose(1)

        Return mfxObj
    End Function
End Class

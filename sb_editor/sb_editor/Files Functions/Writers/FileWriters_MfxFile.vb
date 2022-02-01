Partial Public Class FileWriters
    Public Sub WriteMfxFile(textFilePath As String, mfxFileData As MfxFile)
        'Update file
        Try
            FileOpen(1, textFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
            PrintLine(1, "#HASHCODE")
            PrintLine(1, "HashCodeNumber " & mfxFileData.HashCode)
            PrintLine(1, "#END")
            PrintLine(1, "")
            PrintLine(1, "#MusicData")
            If mfxFileData.Volume > 0 Then
                PrintLine(1, "Volume " & mfxFileData.Volume)
            End If
            If mfxFileData.UserValue > 0 Then
                PrintLine(1, "UserValue " & mfxFileData.UserValue)
            End If
            PrintLine(1, "#END")
            PrintLine(1, "")
            PrintLine(1, "#TIMESTAMPS")
            PrintLine(1, "MidiFileLastOutput " & mfxFileData.MidiFileLastOutput)
            PrintLine(1, "WavFileLastOutput " & mfxFileData.WavFileLastOutput)
            PrintLine(1, "#END")
            FileClose(1)
        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly + vbCritical, "Error")
        End Try
    End Sub
End Class

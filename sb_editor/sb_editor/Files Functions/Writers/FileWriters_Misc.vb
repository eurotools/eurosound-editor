Partial Public Class FileWriters
    Friend Sub UpdateMiscFile(miscFilePath As String)
        FileOpen(1, miscFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "#VERSION")
        PrintLine(1, "VersionNumber 3.57")
        PrintLine(1, "#END")
        PrintLine(1, "")
        PrintLine(1, "#STREAMS")
        PrintLine(1, "ReSampleStreams " & ReSampleStreams)
        PrintLine(1, "#END")
        PrintLine(1, "")
        PrintLine(1, "#HASHCODES")
        PrintLine(1, "SFXHashCodeNumber 0")
        PrintLine(1, "SoundBankHashCodeNumber 0")
        If MFXHashCodeNumber > 0 Then
            PrintLine(1, "MFXHashCodeNumber " & MFXHashCodeNumber)
        End If
        PrintLine(1, "#END")
        FileClose(1)
    End Sub

    Friend Sub CreateEmptyMiscFile(miscFilePath As String)
        FileOpen(1, miscFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "#HASHCODES")
        PrintLine(1, "SFXHashCodeNumber 0")
        PrintLine(1, "SoundBankHashCodeNumber 0")
        PrintLine(1, "#END")
        FileClose(1)
    End Sub
End Class

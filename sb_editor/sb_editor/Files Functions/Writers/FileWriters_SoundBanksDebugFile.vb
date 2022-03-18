Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub WriteSoundBankDebug(outputFilePath As String, streamData As List(Of KeyValuePair(Of String, Integer)), sfxFilePath As String, soundBankName As String, soundBankHashCode As UInteger)
            Dim StreamFileRefCheckSum As Integer = 0
            FileOpen(1, outputFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
            PrintLine(1, "SoundBank Output Debug Data")
            PrintLine(1, Format(Now, "dd/mm/yyy"))
            PrintLine(1, Format(Now, "hh:mm:ss"))
            PrintLine(1, "")
            PrintLine(1, "SoundBankName = " & soundBankName)
            PrintLine(1, "SoundBankSaveName = " & soundBankHashCode)
            PrintLine(1, "SoundBankFileName = " & sfxFilePath)
            PrintLine(1, "Stream PoolFiles(n).FileRef")
            For Each itemToPrint As KeyValuePair(Of String, Integer) In streamData
                PrintLine(1, itemToPrint.Value & "    \" & itemToPrint.Key)
                StreamFileRefCheckSum += Math.Abs(itemToPrint.Value)
            Next
            PrintLine(1, "StreamFileRefCheckSum = " & (StreamFileRefCheckSum * -1))
            FileClose(1)
        End Sub
    End Class
End Namespace

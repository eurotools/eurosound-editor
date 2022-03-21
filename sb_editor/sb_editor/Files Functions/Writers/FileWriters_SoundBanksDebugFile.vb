Imports System.IO

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub WriteSoundBankDebug(outputFilePath As String, streamData As List(Of KeyValuePair(Of String, Integer)), sfxFilePath As String, soundBankName As String, soundBankHashCode As UInteger)
            Dim StreamFileRefCheckSum As Integer = 0
            Using outputFile As New StreamWriter(outputFilePath)
                outputFile.WriteLine("SoundBank Output Debug Data")
                outputFile.WriteLine(Now.ToString("dd/mm/yyy"))
                outputFile.WriteLine(Now.ToString("hh:mm:ss"))
                outputFile.WriteLine("")
                outputFile.WriteLine("SoundBankName = " & soundBankName)
                outputFile.WriteLine("SoundBankSaveName = " & soundBankHashCode)
                outputFile.WriteLine("SoundBankFileName = " & sfxFilePath)
                outputFile.WriteLine("Stream PoolFiles(n).FileRef")
                For Each itemToPrint As KeyValuePair(Of String, Integer) In streamData
                    outputFile.WriteLine(itemToPrint.Value & "    \" & itemToPrint.Key)
                    StreamFileRefCheckSum += Math.Abs(itemToPrint.Value)
                Next
                outputFile.WriteLine("StreamFileRefCheckSum = " & (StreamFileRefCheckSum * -1))
            End Using
        End Sub
    End Class
End Namespace

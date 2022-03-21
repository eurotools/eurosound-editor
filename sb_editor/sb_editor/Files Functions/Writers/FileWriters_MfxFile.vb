Imports System.IO
Imports sb_editor.ParsersObjects

Namespace WritersClasses
    Partial Public Class FileWriters
        Public Sub WriteMfxFile(textFilePath As String, mfxFileData As MfxFile)
            Using outputFile As New StreamWriter(textFilePath)
                outputFile.WriteLine("#HASHCODE")
                outputFile.WriteLine("HashCodeNumber " & mfxFileData.HashCode)
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")
                outputFile.WriteLine("#MusicData")
                If mfxFileData.Volume > 0 Then
                    outputFile.WriteLine("Volume " & mfxFileData.Volume)
                End If
                If mfxFileData.UserValue > 0 Then
                    outputFile.WriteLine("UserValue " & mfxFileData.UserValue)
                End If
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")
                outputFile.WriteLine("#TIMESTAMPS")
                outputFile.WriteLine("MidiFileLastOutput " & mfxFileData.MidiFileLastOutput)
                outputFile.WriteLine("WavFileLastOutput " & mfxFileData.WavFileLastOutput)
                outputFile.WriteLine("#END")
            End Using
        End Sub
    End Class
End Namespace

Imports System.IO
Imports sb_editor.ReverbObj

Namespace WritersClasses
    Partial Public Class FileWriters
        Public Sub WriteReverbFile(filePath As String, fileToWrite As ReverbFile)
            'Update file
            Using outputFile As New StreamWriter(filePath)
                outputFile.WriteLine("#MiscData")
                outputFile.WriteLine("HashCode  " & fileToWrite.HashCode)
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")
                outputFile.WriteLine("#PCReverb")
                WriteReverbPlatform(fileToWrite.PCReverb, outputFile)
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")
                outputFile.WriteLine("#XBReverb")
                WriteReverbPlatform(fileToWrite.XBReverb, outputFile)
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")
                outputFile.WriteLine("#GCReverb")
                WriteReverbPlatform(fileToWrite.GCReverb, outputFile)
                outputFile.WriteLine("#END")
            End Using
        End Sub

        Private Sub WriteReverbPlatform(platformObj As ReverbPlatform, outputFile As StreamWriter)
            outputFile.WriteLine(platformObj.RoomSize)
            outputFile.WriteLine(platformObj.Width)
            outputFile.WriteLine(platformObj.Damp)
            outputFile.WriteLine(platformObj.LowPassFilter)
            outputFile.WriteLine(platformObj.Filter1)
            outputFile.WriteLine(platformObj.Filter2)
        End Sub
    End Class
End Namespace

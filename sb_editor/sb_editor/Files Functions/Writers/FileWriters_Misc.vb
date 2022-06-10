Imports System.IO

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub UpdateMiscFile(miscFilePath As String, Optional newProject As Boolean = False)
            Using outputFile As New StreamWriter(miscFilePath)
                outputFile.WriteLine("#VERSION")
                outputFile.WriteLine("VersionNumber " & New Version(My.Application.Info.Version.Major, My.Application.Info.Version.Minor).ToString)
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")
                If newProject Then
                    outputFile.WriteLine("#HASHCODES")
                    outputFile.WriteLine("SFXHashCodeNumber 0")
                    outputFile.WriteLine("SoundBankHashCodeNumber 0")
                Else
                    outputFile.WriteLine("#STREAMS")
                    outputFile.WriteLine("ReSampleStreams " & ReSampleStreams)
                    outputFile.WriteLine("#END")
                    outputFile.WriteLine("")
                    outputFile.WriteLine("#HASHCODES")
                    outputFile.WriteLine("SFXHashCodeNumber " & SFXHashCodeNumber)
                    outputFile.WriteLine("SoundBankHashCodeNumber " & SoundBankHashCodeNumber)
                    If MFXHashCodeNumber > 0 Then
                        outputFile.WriteLine("MFXHashCodeNumber " & MFXHashCodeNumber)
                    End If
                    If ReverbHashCodeNumber > 0 Then
                        outputFile.WriteLine("ReverbHashCodeNumber " & ReverbHashCodeNumber)
                    End If
                End If
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")
            End Using
        End Sub
    End Class
End Namespace

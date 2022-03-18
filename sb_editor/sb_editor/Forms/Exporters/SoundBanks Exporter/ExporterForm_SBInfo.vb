Imports System.IO
Imports System.Text

Partial Public Class ExporterForm
    Private Sub CreateSBInfoFile(outPlatforms As String(), soundBanksDictionary As SortedDictionary(Of String, UInteger))
        For platformIndex As Integer = 0 To outPlatforms.Length - 1
            Dim currentPlatform As String = outPlatforms(platformIndex)
            Dim outputFilePath As String = Path.Combine(WorkingDirectory, "TempOutputFolder", currentPlatform, "SBInfo.sbi")
            'Check if is big endian or not
            Dim isBigEndian As Boolean = False
            If StrComp(currentPlatform, "GameCube") = 0 Then
                isBigEndian = True
            End If
            'Create MFXInfo file
            Using binaryWriter As New BinaryWriter(File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.ASCII)
                Dim hashCodesArray As UInteger() = soundBanksDictionary.Values.ToArray
                'Print HashCodes
                For fileName As Integer = 0 To 149
                    If fileName < hashCodesArray.Length Then
                        binaryWriter.Write(ESUtils.BytesFunctions.FlipUInt32(hashCodesArray(fileName), isBigEndian))
                    Else
                        binaryWriter.Write(&HFFFFFFFF)
                    End If
                Next
            End Using
        Next
    End Sub
End Class

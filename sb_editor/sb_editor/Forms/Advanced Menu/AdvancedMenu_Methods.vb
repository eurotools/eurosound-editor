Imports System.IO

Partial Public Class AdvancedMenu
    Private Function GetMaxHashCode(folderPath As String) As Integer
        Dim hashcodeNumber As Integer = 0
        If fso.FolderExists(folderPath) Then
            Dim filesToCheck As String() = Directory.GetFiles(folderPath, "*.txt", SearchOption.TopDirectoryOnly)
            For index As Integer = 0 To filesToCheck.Length - 1
                Dim fileData As String() = File.ReadAllLines(filesToCheck(index))
                Dim hashcodeIndex As Integer = Array.IndexOf(fileData, "#HASHCODE")
                If hashcodeIndex >= 0 Then
                    Dim stringData As String() = fileData(hashcodeIndex + 1).Split(" "c)
                    If stringData.Length > 1 AndAlso IsNumeric(stringData(1)) Then
                        hashcodeNumber = Math.Max(hashcodeNumber, CInt(stringData(1)))
                    End If
                End If
            Next
        End If
        Return hashcodeNumber
    End Function

    Private Sub GetPlatformSFXs(folderToInspect As String, sfxPlatformsList As List(Of String), platform As String)
        Dim fileNameWithExtension As String = Dir(folderToInspect & "\*.txt")
        Do While fileNameWithExtension > ""
            Dim fileNameLength As Integer = Len(fileNameWithExtension)
            Dim fileName As String = Microsoft.VisualBasic.Left(fileNameWithExtension, fileNameLength - Len(".txt"))
            sfxPlatformsList.Add(fileName & "  " & platform)
            'Get new item
            fileNameWithExtension = Dir()
        Loop
    End Sub
End Class

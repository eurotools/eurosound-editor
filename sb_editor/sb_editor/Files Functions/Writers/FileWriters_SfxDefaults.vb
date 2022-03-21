Imports System.IO

Namespace WritersClasses
    Partial Public Class FileWriters
        Public Sub CreateEmptySfxDefaults(sfxDefaultsFilePath As String)
            'Replace current file   
            Dim currentDate = Date.Now.ToString(filesDateFormat)

            'Save other data in the text file
            Using outputFile As New StreamWriter(sfxDefaultsFilePath)
                outputFile.WriteLine("## EuroSound SFX Defaults File File")
                outputFile.WriteLine("## First Created ... " & currentDate)
                outputFile.WriteLine("## Created By ... " & EuroSoundUser)
                outputFile.WriteLine("## Last Modified ... " & currentDate)
                outputFile.WriteLine("## Last Modified By ... " & EuroSoundUser)
            End Using
        End Sub
    End Class
End Namespace
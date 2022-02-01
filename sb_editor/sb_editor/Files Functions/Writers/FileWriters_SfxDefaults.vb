Partial Public Class FileWriters
    Public Sub CreateEmptySfxDefaults(sfxDefaultsFilePath As String)
        'Replace current file   
        Dim currentDate = Date.Now.ToString(filesDateFormat)

        'Save other data in the text file
        FileOpen(1, sfxDefaultsFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)

        'Write file header
        PrintLine(1, "## EuroSound SFX Defaults File File")
        PrintLine(1, "## First Created ... " & currentDate)
        PrintLine(1, "## Created By ... " & EuroSoundUser)
        PrintLine(1, "## Last Modified ... " & currentDate)
        PrintLine(1, "## Last Modified By ... " & EuroSoundUser)
        FileClose(1)
    End Sub
End Class

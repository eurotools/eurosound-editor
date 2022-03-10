Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub CreateProjectFile(projectFilePath As String, soundbanks As String(), dataBases As String(), sfxList As String())
            Dim created = Date.Now.ToString(filesDateFormat)

            'Update file
            FileOpen(1, projectFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
            PrintLine(1, "## EuroSound EuroSound Project File File")
            PrintLine(1, "## First Created ... " & created)
            PrintLine(1, "## Created By ... " & EuroSoundUser)
            PrintLine(1, "## Last Modified ... " & created)
            PrintLine(1, "## Last Modified By ... " & EuroSoundUser)
            PrintLine(1, "")
            'Print Data
            WriteListOfItems(soundbanks, "#SoundBankList", 1)
            PrintLine(1, "")
            WriteListOfItems(dataBases, "#DataBaseList", 1)
            PrintLine(1, "")
            WriteListOfItems(sfxList, "#SFXList", 1)
            FileClose(1)
        End Sub
    End Class
End Namespace

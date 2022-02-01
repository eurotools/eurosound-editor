Partial Public Class FileWriters
    Friend Sub CreateProjectFile(projectFilePath As String, soundbanks As TreeView, dataBases As ListBox, sfxList As ListBox)
        Dim created = Date.Now.ToString(filesDateFormat)

        'Update file
        FileOpen(1, projectFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "## EuroSound EuroSound Project File File")
        PrintLine(1, "## First Created ... " & created)
        PrintLine(1, "## Created By ... " & EuroSoundUser)
        PrintLine(1, "## Last Modified ... " & created)
        PrintLine(1, "## Last Modified By ... " & EuroSoundUser)
        PrintLine(1, "")
        'Iterate over all soundbank nodes
        PrintLine(1, "#SoundBankList")
        For Each sbNode As TreeNode In soundbanks.Nodes
            PrintLine(1, sbNode.Text)
        Next
        PrintLine(1, "#END")
        PrintLine(1, "")
        'Iterate over all databases items
        PrintLine(1, "#DataBaseList")
        For Each sbNode As String In dataBases.Items
            PrintLine(1, sbNode)
        Next
        PrintLine(1, "#END")
        PrintLine(1, "")
        'Iterate over all sfx items
        PrintLine(1, "#SFXList")
        For Each sfxItem As String In sfxList.Items
            PrintLine(1, sfxItem)
        Next
        PrintLine(1, "#END")
        FileClose(1)
    End Sub

    Friend Sub CreateEmptyProjectFile(projectFilePath As String)
        Dim created = Date.Now.ToString(filesDateFormat)

        'Update file
        FileOpen(1, projectFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "## EuroSound EuroSound Project File File")
        PrintLine(1, "## First Created ... " & created)
        PrintLine(1, "## Created By ... " & EuroSoundUser)
        PrintLine(1, "## Last Modified ... " & created)
        PrintLine(1, "## Last Modified By ... " & EuroSoundUser)
        PrintLine(1, "")
        'Iterate over all soundbank nodes
        PrintLine(1, "#SoundBankList")
        PrintLine(1, "#END")
        PrintLine(1, "")
        'Iterate over all databases items
        PrintLine(1, "#DataBaseList")
        PrintLine(1, "#END")
        PrintLine(1, "")
        'Iterate over all sfx items
        PrintLine(1, "#SFXList")
        PrintLine(1, "#END")
        FileClose(1)
    End Sub
End Class

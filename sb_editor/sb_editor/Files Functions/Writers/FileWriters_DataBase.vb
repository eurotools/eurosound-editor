Partial Public Class FileWriters
    Friend Sub UpdateDataBaseText(databaseTxt As String, itemsListObject As String(), headerLib As FileParsers, Optional updateHeader As Boolean = True)
        'Replace current file   
        Dim headerData As New FileHeader

        'Get creation time if file exists
        Dim created = Date.Now.ToString(filesDateFormat)
        If fso.FileExists(databaseTxt) Then
            headerData = headerLib.GetFileHeaderInfo(databaseTxt)
            If updateHeader Then
                headerData.LastModify = created
                headerData.LastModifyBy = EuroSoundUser
            End If
        Else
            headerData.FirstCreated = created
            headerData.CreatedBy = EuroSoundUser
            headerData.LastModify = created
            headerData.LastModifyBy = EuroSoundUser
        End If

        'Update file
        FileOpen(1, databaseTxt, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "## EuroSound  File")
        PrintLine(1, "## First Created ... " & headerData.FirstCreated)
        PrintLine(1, "## Created By ... " & headerData.CreatedBy)
        PrintLine(1, "## Last Modified ... " & headerData.LastModify)
        PrintLine(1, "## Last Modified By ... " & headerData.LastModifyBy)
        PrintLine(1, "")
        WriteListOfItems(itemsListObject, "#DEPENDENCIES", 1)
        FileClose(1)
    End Sub
End Class

Partial Public Class FileWriters
    Friend Sub CreateSoundbankFile(nodeObj As TreeNode, headerLib As FileParsers, Optional bankMaxSize As String() = Nothing)
        'Get new file path
        Dim soundbankFilePath As String = fso.BuildPath(WorkingDirectory, "Soundbanks\" & nodeObj.Text & ".txt")

        'Replace current file   
        Dim headerData As New FileHeader

        'Get creation time if file exists
        Dim created = Date.Now.ToString(filesDateFormat)
        If fso.FileExists(soundbankFilePath) Then
            headerData = headerLib.GetFileHeaderInfo(soundbankFilePath)
            headerData.LastModify = created
            headerData.LastModifyBy = EuroSoundUser
        Else
            headerData.FirstCreated = created
            headerData.CreatedBy = EuroSoundUser
            headerData.LastModify = created
            headerData.LastModifyBy = EuroSoundUser
        End If

        'Write file
        FileOpen(1, soundbankFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(1, "## EuroSound File")
        PrintLine(1, "## First Created ... " & headerData.FirstCreated)
        PrintLine(1, "## Created By ... " & headerData.CreatedBy)
        PrintLine(1, "## Last Modified ... " & headerData.LastModify)
        PrintLine(1, "## Last Modified By ... " & headerData.LastModifyBy)
        PrintLine(1, "")
        PrintLine(1, "#DEPENDENCIES")
        For Each childNode As TreeNode In nodeObj.Nodes
            If childNode.ImageIndex <> 3 Then
                PrintLine(1, childNode.Text)
            End If
        Next
        PrintLine(1, "#END")
        PrintLine(1, "")
        PrintLine(1, "#HASHCODE")
        PrintLine(1, "HashCodeNumber " & nodeObj.Name)
        PrintLine(1, "#END")
        If bankMaxSize IsNot Nothing Then
            PrintLine(1, "")
            PrintLine(1, "#MaxBankSizes")
            PrintLine(1, "PlayStationSize " & bankMaxSize(0))
            PrintLine(1, "PCSize " & bankMaxSize(1))
            PrintLine(1, "XBoxSize " & bankMaxSize(2))
            PrintLine(1, "GameCubeSize " & bankMaxSize(3))
            PrintLine(1, "#END")
        End If
        FileClose(1)
    End Sub
End Class

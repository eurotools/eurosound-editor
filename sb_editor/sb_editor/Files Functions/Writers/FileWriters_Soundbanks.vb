Partial Public Class FileWriters
    Friend Sub UpdateSoundbankFile(fileData As SoundbankFile, filePath As String, headerLib As FileParsers, Optional updateHeader As Boolean = True)
        'Replace current file   
        Dim headerData As New FileHeader

        'Get creation time if file exists
        Dim created = Date.Now.ToString(filesDateFormat)
        If fso.FileExists(filePath) Then
            headerData = headerLib.GetFileHeaderInfo(filePath)
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
        Try
            'Write file
            FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
            PrintLine(1, "## EuroSound File")
            PrintLine(1, "## First Created ... " & headerData.FirstCreated)
            PrintLine(1, "## Created By ... " & headerData.CreatedBy)
            PrintLine(1, "## Last Modified ... " & headerData.LastModify)
            PrintLine(1, "## Last Modified By ... " & headerData.LastModifyBy)
            PrintLine(1, "")
            WriteListOfItems(fileData.Dependencies, "#DEPENDENCIES", 1)
            PrintLine(1, "")
            PrintLine(1, "#HASHCODE")
            PrintLine(1, "HashCodeNumber " & fileData.HashCode)
            PrintLine(1, "#END")
            If fileData.MaxBankSizes.GameCubeSize <> 0 Or fileData.MaxBankSizes.PCSize <> 0 Or fileData.MaxBankSizes.PlayStationSize <> 0 Or fileData.MaxBankSizes.XboxSize <> 0 Then
                PrintLine(1, "")
                PrintLine(1, "#MaxBankSizes")
                PrintLine(1, "PlayStationSize " & fileData.MaxBankSizes.PlayStationSize)
                PrintLine(1, "PCSize " & fileData.MaxBankSizes.PCSize)
                PrintLine(1, "XBoxSize " & fileData.MaxBankSizes.XboxSize)
                PrintLine(1, "GameCubeSize " & fileData.MaxBankSizes.GameCubeSize)
                PrintLine(1, "#END")
            End If
            FileClose(1)
        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly + vbCritical, "Error")
        End Try
    End Sub

End Class

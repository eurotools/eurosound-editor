Partial Public Class FileWriters
    Friend Sub SaveSamplesFile(filePath As String, samplesFileTable As DataTable)
        'Replace current file   
        Dim headerData As New FileHeader
        Dim readers As New FileParsers

        'Get creation time if file exists
        Dim created = Date.Now.ToString(filesDateFormat)
        If fso.FileExists(filePath) Then
            headerData = readers.GetFileHeaderInfo(filePath)
            headerData.LastModify = created
            headerData.LastModifyBy = EuroSoundUser
        Else
            headerData.FirstCreated = created
            headerData.CreatedBy = EuroSoundUser
            headerData.LastModify = created
            headerData.LastModifyBy = EuroSoundUser
        End If

        'Update file
        FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "## EuroSound Samples File")
        PrintLine(1, "## First Created ... " & headerData.FirstCreated)
        PrintLine(1, "## Created By ... " & headerData.CreatedBy)
        PrintLine(1, "## Last Modified ... " & headerData.LastModify)
        PrintLine(1, "## Last Modified By ... " & headerData.LastModifyBy)
        PrintLine(1, "")
        PrintLine(1, "#AvailableSamples")
        PrintLine(1, " " & samplesFileTable.Rows.Count)
        For Col As Integer = 0 To 9
            For index As Integer = 0 To samplesFileTable.Rows.Count - 1
                PrintLine(1, samplesFileTable.Rows(index).ItemArray(Col))
            Next
        Next
        PrintLine(1, "#END")
        FileClose(1)
    End Sub
End Class

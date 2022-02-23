Partial Public Class FileWriters
    Private Function GetFileHeaderData(filePath As String, readers As FileParsers) As FileHeader
        Dim headerData As New FileHeader
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
        Return headerData
    End Function
End Class

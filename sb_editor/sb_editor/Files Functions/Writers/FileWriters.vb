Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
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

        Private Sub WriteListOfItems(filesToWrite As String(), sectionName As String, fileNumber As Integer)
            PrintLine(1, sectionName)
            'Iterate over list items
            If filesToWrite IsNot Nothing AndAlso filesToWrite.Length > 0 Then
                For index As Integer = 0 To filesToWrite.Length - 1
                    PrintLine(fileNumber, filesToWrite(index))
                Next
            End If
            'End dependencies block
            PrintLine(1, "#END")
        End Sub
    End Class
End Namespace

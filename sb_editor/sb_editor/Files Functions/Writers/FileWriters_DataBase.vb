Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub UpdateDataBaseText(databaseTxt As String, itemsListObject As String(), headerLib As FileParsers)
            'Replace current file   
            Dim headerData As New FileHeader

            'Get creation time if file exists
            Dim currentDate = Date.Now.ToString(filesDateFormat)
            If File.Exists(databaseTxt) Then
                headerData = headerLib.GetFileHeaderInfo(databaseTxt)
                headerData.LastModify = currentDate
                headerData.LastModifyBy = EuroSoundUser
            Else
                headerData.FirstCreated = currentDate
                headerData.CreatedBy = EuroSoundUser
                headerData.LastModify = currentDate
                headerData.LastModifyBy = EuroSoundUser
            End If

            'Update file
            Using outputFile As New StreamWriter(databaseTxt)
                outputFile.WriteLine("## EuroSound  File")
                outputFile.WriteLine("## First Created ... " & headerData.FirstCreated)
                outputFile.WriteLine("## Created By ... " & headerData.CreatedBy)
                outputFile.WriteLine("## Last Modified ... " & headerData.LastModify)
                outputFile.WriteLine("## Last Modified By ... " & headerData.LastModifyBy)
                outputFile.WriteLine("")
                WriteListOfItems(itemsListObject, "#DEPENDENCIES", outputFile)
            End Using
        End Sub
    End Class
End Namespace

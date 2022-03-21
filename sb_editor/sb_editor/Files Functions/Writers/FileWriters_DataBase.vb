Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub UpdateDataBaseText(databaseTxt As String, itemsListObject As String(), headerLib As FileParsers, Optional updateHeader As Boolean = True)
            'Replace current file   
            Dim headerData As New FileHeader

            'Get creation time if file exists
            Dim created = Date.Now.ToString(filesDateFormat)
            If File.Exists(databaseTxt) Then
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

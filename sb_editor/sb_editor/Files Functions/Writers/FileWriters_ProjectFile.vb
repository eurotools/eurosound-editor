Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

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

        Friend Sub MergeFiles(temporalFilePath As String, projectFilePath As String, projFileData As ProjectFile, sectionToUpdateName As String)
            Dim textFileReaders As New FileParsers
            Dim temporalFileData As ProjectFile = textFileReaders.ReadProjectFile(temporalFilePath)
            'Update file
            FileOpen(1, projectFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
            PrintLine(1, "## EuroSound EuroSound Project File File")
            PrintLine(1, "## First Created ... " & temporalFileData.HeaderInfo.FirstCreated)
            PrintLine(1, "## Created By ... " & temporalFileData.HeaderInfo.CreatedBy)
            PrintLine(1, "## Last Modified ... " & temporalFileData.HeaderInfo.LastModify)
            PrintLine(1, "## Last Modified By ... " & temporalFileData.HeaderInfo.LastModifyBy)
            PrintLine(1, "")
            'Print Data
            If StrComp(sectionToUpdateName, "#SoundBankList") = 0 Then
                WriteListOfItems(temporalFileData.SoundBankList, "#SoundBankList", 1)
            Else
                WriteListOfItems(projFileData.SoundBankList, "#SoundBankList", 1)
            End If
            PrintLine(1, "")
            If StrComp(sectionToUpdateName, "#DataBaseList") = 0 Then
                WriteListOfItems(temporalFileData.DataBaseList, "#DataBaseList", 1)
            Else
                WriteListOfItems(projFileData.DataBaseList, "#DataBaseList", 1)
            End If
            PrintLine(1, "")
            If StrComp(sectionToUpdateName, "#SFXList") = 0 Then
                WriteListOfItems(temporalFileData.SFXList, "#SFXList", 1)
            Else
                WriteListOfItems(projFileData.SFXList, "#SFXList", 1)
            End If
            FileClose(1)
        End Sub
    End Class
End Namespace

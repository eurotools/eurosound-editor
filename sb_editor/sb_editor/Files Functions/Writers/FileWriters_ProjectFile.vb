Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub CreateProjectFile(projectFilePath As String, soundbanks As String(), dataBases As String(), sfxList As String())
            Dim created = Date.Now.ToString(filesDateFormat)

            Using outputFile As New StreamWriter(projectFilePath)
                outputFile.WriteLine("## EuroSound EuroSound Project File File")
                outputFile.WriteLine("## First Created ... " & created)
                outputFile.WriteLine("## Created By ... " & EuroSoundUser)
                outputFile.WriteLine("## Last Modified ... " & created)
                outputFile.WriteLine("## Last Modified By ... " & EuroSoundUser)
                outputFile.WriteLine("")
                'Print Data
                WriteListOfItems(soundbanks, "#SoundBankList", outputFile)
                outputFile.WriteLine("")
                WriteListOfItems(dataBases, "#DataBaseList", outputFile)
                outputFile.WriteLine("")
                WriteListOfItems(sfxList, "#SFXList", outputFile)
            End Using
        End Sub

        Friend Sub MergeFiles(temporalFilePath As String, projectFilePath As String, projFileData As ProjectFile, sectionToUpdateName As String)
            Dim textFileReaders As New FileParsers
            Dim temporalFileData As ProjectFile = textFileReaders.ReadProjectFile(temporalFilePath)

            'Update file
            Using outputFile As New StreamWriter(projectFilePath)
                outputFile.WriteLine("## EuroSound EuroSound Project File File")
                outputFile.WriteLine("## First Created ... " & temporalFileData.HeaderInfo.FirstCreated)
                outputFile.WriteLine("## Created By ... " & temporalFileData.HeaderInfo.CreatedBy)
                outputFile.WriteLine("## Last Modified ... " & temporalFileData.HeaderInfo.LastModify)
                outputFile.WriteLine("## Last Modified By ... " & temporalFileData.HeaderInfo.LastModifyBy)
                outputFile.WriteLine("")
                'Print Data
                If StrComp(sectionToUpdateName, "#SoundBankList") = 0 Then
                    WriteListOfItems(temporalFileData.SoundBankList, "#SoundBankList", outputFile)
                Else
                    WriteListOfItems(projFileData.SoundBankList, "#SoundBankList", outputFile)
                End If
                outputFile.WriteLine("")
                If StrComp(sectionToUpdateName, "#DataBaseList") = 0 Then
                    WriteListOfItems(temporalFileData.DataBaseList, "#DataBaseList", outputFile)
                Else
                    WriteListOfItems(projFileData.DataBaseList, "#DataBaseList", outputFile)
                End If
                outputFile.WriteLine("")
                If StrComp(sectionToUpdateName, "#SFXList") = 0 Then
                    WriteListOfItems(temporalFileData.SFXList, "#SFXList", outputFile)
                Else
                    WriteListOfItems(projFileData.SFXList, "#SFXList", outputFile)
                End If
            End Using
        End Sub
    End Class
End Namespace

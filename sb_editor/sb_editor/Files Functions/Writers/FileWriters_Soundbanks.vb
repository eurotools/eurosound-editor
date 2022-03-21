Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub UpdateSoundbankFile(fileData As SoundbankFile, filePath As String, headerLib As FileParsers, Optional updateHeader As Boolean = True)
            'Replace current file   
            Dim headerData As New FileHeader

            'Get creation time if file exists
            Dim created = Date.Now.ToString(filesDateFormat)
            If File.Exists(filePath) Then
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

            'Write file
            Using outputFile As New StreamWriter(filePath)
                outputFile.WriteLine("## EuroSound File")
                outputFile.WriteLine("## First Created ... " & headerData.FirstCreated)
                outputFile.WriteLine("## Created By ... " & headerData.CreatedBy)
                outputFile.WriteLine("## Last Modified ... " & headerData.LastModify)
                outputFile.WriteLine("## Last Modified By ... " & headerData.LastModifyBy)
                outputFile.WriteLine("")
                WriteListOfItems(fileData.Dependencies, "#DEPENDENCIES", outputFile)
                outputFile.WriteLine("")
                outputFile.WriteLine("#HASHCODE")
                outputFile.WriteLine("HashCodeNumber " & fileData.HashCode)
                outputFile.WriteLine("#END")
                If fileData.MaxBankSizes.GameCubeSize <> 0 Or fileData.MaxBankSizes.PCSize <> 0 Or fileData.MaxBankSizes.PlayStationSize <> 0 Or fileData.MaxBankSizes.XboxSize <> 0 Then
                    outputFile.WriteLine("")
                    outputFile.WriteLine("#MaxBankSizes")
                    outputFile.WriteLine("PlayStationSize " & fileData.MaxBankSizes.PlayStationSize)
                    outputFile.WriteLine("PCSize " & fileData.MaxBankSizes.PCSize)
                    outputFile.WriteLine("XBoxSize " & fileData.MaxBankSizes.XboxSize)
                    outputFile.WriteLine("GameCubeSize " & fileData.MaxBankSizes.GameCubeSize)
                    outputFile.WriteLine("#END")
                End If
            End Using
        End Sub
    End Class
End Namespace

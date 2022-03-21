Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub SaveSamplesFile(filePath As String, samplesFileTable As DataTable)
            'Replace current file   
            Dim readers As New FileParsers
            Dim headerData As FileHeader = GetFileHeaderData(filePath, readers)

            'Update file
            Using outputFile As New StreamWriter(filePath)
                outputFile.WriteLine("## EuroSound Samples File")
                outputFile.WriteLine("## First Created ... " & headerData.FirstCreated)
                outputFile.WriteLine("## Created By ... " & headerData.CreatedBy)
                outputFile.WriteLine("## Last Modified ... " & headerData.LastModify)
                outputFile.WriteLine("## Last Modified By ... " & headerData.LastModifyBy)
                outputFile.WriteLine("")
                outputFile.WriteLine("#AvailableSamples")
                outputFile.WriteLine(" " & samplesFileTable.Rows.Count)
                For Col As Integer = 0 To 9
                    For index As Integer = 0 To samplesFileTable.Rows.Count - 1
                        outputFile.WriteLine(samplesFileTable.Rows(index).ItemArray(Col))
                    Next
                Next
                outputFile.WriteLine("#END")
            End Using
        End Sub
    End Class
End Namespace

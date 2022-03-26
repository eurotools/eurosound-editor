Imports System.IO

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub CreateRefineList(refineSearchFilePath As String, keywords As String())
            Using outputFile As New StreamWriter(refineSearchFilePath)
                WriteListOfItems(keywords, "#RefineSearch", outputFile)
                outputFile.WriteLine("")
            End Using
        End Sub
    End Class
End Namespace
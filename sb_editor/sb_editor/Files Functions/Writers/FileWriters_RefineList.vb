Partial Public Class FileWriters
    Friend Sub CreateRefineList(refineSearchFilePath As String, keywords As String())
        FileOpen(1, refineSearchFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "#RefineSearch")
        'Iterate over list items
        If keywords IsNot Nothing Then
            For Each refineSeachItem As String In keywords
                PrintLine(1, refineSeachItem)
            Next
        End If
        PrintLine(1, "#END")
        FileClose(1)
    End Sub
End Class

Partial Public Class FileWriters
    Friend Sub CreateRefineList(refineSearchFilePath As String, keywords As String())
        FileOpen(1, refineSearchFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        WriteListOfItems(keywords, "#RefineSearch", 1)
        FileClose(1)
    End Sub
End Class

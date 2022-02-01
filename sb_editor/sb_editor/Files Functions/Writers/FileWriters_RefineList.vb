Partial Public Class FileWriters
    Friend Sub CreateRefineList(refineSearchFilePath As String, keywords As SortedDictionary(Of String, Integer))
        FileOpen(1, refineSearchFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "#RefineSearch")
        PrintLine(1, "All")
        PrintLine(1, "HighLighted")
        'Iterate over list items
        If keywords IsNot Nothing Then
            For Each refineSeachItem As KeyValuePair(Of String, Integer) In keywords
                If refineSeachItem.Value > 2 Then
                    PrintLine(1, refineSeachItem.Key)
                End If
            Next
        End If
        PrintLine(1, "#END")
        FileClose(1)
    End Sub
End Class

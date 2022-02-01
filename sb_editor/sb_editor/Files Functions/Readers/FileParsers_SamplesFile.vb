Partial Public Class FileParsers
    Friend Function SamplesFileToDatatable(samplesFilePath As String) As DataTable
        Dim samplesData As New DataTable
        samplesData.Columns.Add("SampleFilename")
        samplesData.Columns.Add("ReSampleRate")
        samplesData.Columns.Add("Size")
        samplesData.Columns.Add("Date")
        samplesData.Columns.Add("ReSample")
        samplesData.Columns.Add("StreamMe")
        samplesData.Columns.Add("ReSmp4")
        samplesData.Columns.Add("ReSmp2")
        samplesData.Columns.Add("ReSmp3")
        samplesData.Columns.Add("ReSmp4duplicated")

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, samplesFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            currentLine = LineInput(1)
            'Read Available Sample Rates
            If StrComp(currentLine, "#AvailableSamples") = 0 Then
                'Read line
                currentLine = LineInput(1)
                Dim SamplesCount As Integer = currentLine
                'Read samples table
                If SamplesCount > 0 Then
                    For i As Integer = 0 To 9
                        Dim itemsCount As Integer = 0
                        Do
                            'Continue Reading
                            currentLine = LineInput(1)
                            'Read content
                            If StrComp(currentLine, "#END") <> 0 Then
                                'Add item to listview
                                If i = 0 Then
                                    samplesData.Rows.Add(currentLine)
                                Else
                                    samplesData.Rows(itemsCount)(i) = currentLine
                                End If
                                'Update counter
                                itemsCount += 1
                            Else
                                'Exit loop
                                Exit Do
                            End If
                        Loop While itemsCount < SamplesCount
                    Next
                End If
            End If
        Loop

        'Read misc properties block
        FileClose(1)

        Return samplesData
    End Function

    Friend Function GetStreamSoundsList(samplesFilePath As String) As String()
        'Create a datatable with the samples info
        Dim dataTable As DataTable = SamplesFileToDatatable(samplesFilePath)

        'Select 
        Dim results As DataRow() = dataTable.Select("StreamMe = True")
        Dim totalStreamedSamples = results.Length - 1

        'Create final list
        Dim filesToInclude = New String(totalStreamedSamples) {}

        'Iterate over all select results, we don't need to sort :)
        For index As Integer = 0 To totalStreamedSamples
            'Sample Relative path
            filesToInclude(index) = results(index).ItemArray(0)
        Next

        'Clear table
        dataTable.Clear()

        'Return data
        Return filesToInclude
    End Function

End Class

Namespace ReaderClasses
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
            If fso.FileExists(samplesFilePath) Then
                Dim currentLine As String
                FileOpen(1, samplesFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
                Do Until EOF(1)
                    'Read text file
                    currentLine = Trim(LineInput(1))
                    'Read Available Sample Rates
                    If StrComp(currentLine, "#AvailableSamples") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        Dim SamplesCount As Integer = currentLine
                        'Read samples table
                        If SamplesCount > 0 Then
                            For i As Integer = 0 To 9
                                Dim itemsCount As Integer = 0
                                Do
                                    'Continue Reading
                                    currentLine = Trim(LineInput(1))
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
            End If
            Return samplesData
        End Function

        Friend Function GetStreamSoundsList(samplesFilePath As String) As String()
            'Create a datatable with the samples info
            Dim dataTable As DataTable = SamplesFileToDatatable(samplesFilePath)

            'Get the streams that has the flag StreamMe in True
            Dim streamsList As New List(Of String)
            For rowIndex As Integer = 0 To dataTable.Rows.Count - 1
                If StrComp(dataTable.Rows(rowIndex).Item(5), "True") = 0 Then
                    Dim sampleRelativePath As String = dataTable.Rows(rowIndex).Item(0)
                    'In the multilanguage, we only need to get the english folder, the other languages should have the same folder structure
                    If InStr(1, sampleRelativePath, "Speech\", CompareMethod.Binary) Then
                        If InStr(1, sampleRelativePath, "Speech\English", CompareMethod.Binary) = 0 Then
                            Continue For
                        End If
                    End If
                    streamsList.Add(sampleRelativePath)
                End If
            Next
            'Return data
            Return streamsList.ToArray
        End Function
    End Class
End Namespace

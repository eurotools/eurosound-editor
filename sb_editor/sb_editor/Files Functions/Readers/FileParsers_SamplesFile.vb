Imports System.IO

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
            Using sr As New StreamReader(File.OpenRead(samplesFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Then
                        Continue While
                    Else
                        'Read Available Sample Rates
                        If currentLine.Equals("#AvailableSamples", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            Dim SamplesCount As Integer = currentLine
                            'Read samples table
                            If SamplesCount > 0 Then
                                For i As Integer = 0 To 9
                                    Dim itemsCount As Integer = 0
                                    Do
                                        'Continue Reading
                                        currentLine = sr.ReadLine.Trim
                                        'Read content
                                        If Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase) Then
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
                    End If
                End While
            End Using

            Return samplesData
        End Function

        Friend Function GetAllStreamSamples(dataTable As DataTable) As String()
            'Get the streams that has the flag StreamMe in True
            Dim streamsList As New List(Of String)
            For rowIndex As Integer = 0 To dataTable.Rows.Count - 1
                Dim itemData As String = dataTable.Rows(rowIndex).Item(5)
                If itemData.Equals("True", StringComparison.OrdinalIgnoreCase) Then
                    Dim sampleRelativePath As String = dataTable.Rows(rowIndex).Item(0)
                    streamsList.Add(sampleRelativePath.TrimStart("\").ToUpper)
                End If
            Next

            'Return data
            Return streamsList.Distinct.ToArray
        End Function
    End Class
End Namespace

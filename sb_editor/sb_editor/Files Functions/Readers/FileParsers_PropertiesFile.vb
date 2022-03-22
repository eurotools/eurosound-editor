Imports System.IO
Imports sb_editor.ParsersObjects

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* Properties File
        '*===============================================================================================
        Public Function ReadPropertiesFile(propsFilePath As String) As PropertiesFile
            Dim propsFile As New PropertiesFile
            'List to store data
            Dim AvailableReSampleRates As New List(Of String)

            Using sr As New StreamReader(File.OpenRead(propsFilePath))
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Then
                        Continue While
                    Else
                        'Header info
                        If currentLine.Contains("## ") Then
                            'Split content
                            Dim lineData As String() = Split(currentLine, "...")

                            'Get header info
                            If currentLine.Contains("## EuroSound") Then
                                propsFile.HeaderInfo.FileHeader = currentLine
                            End If
                            If currentLine.Contains("## First Created ...") Then
                                propsFile.HeaderInfo.FirstCreated = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Created By ...") Then
                                propsFile.HeaderInfo.CreatedBy = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified ...") Then
                                propsFile.HeaderInfo.LastModify = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified By ...") Then
                                propsFile.HeaderInfo.LastModifyBy = lineData(1).Trim
                            End If
                        End If

                        'Available formats section
                        If currentLine.Equals("#AvailableFormats", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            Dim AvailableFormatsCount As Integer = sr.ReadLine.Trim
                            propsFile.AvailableFormats = New String(AvailableFormatsCount - 1, 2) {}

                            'Ensure that there are formats stored
                            If AvailableFormatsCount > 0 Then
                                'Get available formats 
                                For i As Integer = 0 To 2
                                    Dim itemsCount As Integer = 0
                                    Do
                                        'Read line
                                        currentLine = sr.ReadLine.Trim

                                        'Read content
                                        If currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase) Then
                                            Exit Do
                                        Else
                                            'Add item to listview
                                            propsFile.AvailableFormats(itemsCount, i) = currentLine
                                            'Update counter
                                            itemsCount += 1
                                        End If
                                    Loop While itemsCount < AvailableFormatsCount
                                Next
                            End If
                        End If

                        'Read Available Sample Rates
                        If currentLine.Equals("#AvailableReSampleRates", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Add item to listview
                                AvailableReSampleRates.Add(currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                            'Add items to array
                            propsFile.AvailableReSampleRates = AvailableReSampleRates
                        End If

                        'ReSample Rates for Format
                        If currentLine.Contains("#ReSampleRates") Then
                            'Get index
                            Dim index As Integer = Right(currentLine, 1)

                            'Collection to store values
                            Dim values As New Dictionary(Of String, UInteger)

                            'Read line
                            Dim sampleRateIndex As Integer = 0
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Add item to ArrayList
                                values.Add(AvailableReSampleRates(sampleRateIndex), currentLine)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                                'Update index
                                sampleRateIndex += 1
                            End While

                            'Add data to dictionary
                            If propsFile.AvailableFormats.GetLength(0) > 0 Then
                                Dim formatName As String = propsFile.AvailableFormats(index, 0)
                                If Not propsFile.sampleRateFormats.ContainsKey(formatName) Then
                                    propsFile.sampleRateFormats.Add(formatName, values)
                                End If
                            End If
                        End If

                        'Misc properties block
                        If currentLine.Equals("#MiscProperites", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Split line
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)

                                Select Case lineData(0).ToUpper
                                    Case "DEFAULTRATE"
                                        propsFile.MiscProps.DefaultRate = CInt(lineData(1))
                                    Case "SAMPLEFILEFOLDER"
                                        propsFile.MiscProps.SampleFileFolder = GetStringValue("SAMPLEFILEFOLDER", currentLine)
                                    Case "HASHCODEFILEFOLDER"
                                        propsFile.MiscProps.HashCodeFileFolder = GetStringValue("HASHCODEFILEFOLDER", currentLine)
                                    Case "ENGINEXFOLDER"
                                        propsFile.MiscProps.EngineXFolder = GetStringValue("ENGINEXFOLDER", currentLine)
                                    Case "EUROLANDHASHCODESERVERPATH"
                                        propsFile.MiscProps.EuroLandHashCodeServerPath = GetStringValue("EUROLANDHASHCODESERVERPATH", currentLine)
                                End Select

                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If
                    End If
                End While
            End Using

            'Return object
            Return propsFile
        End Function
    End Class
End Namespace

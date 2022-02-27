Partial Public Class FileParsers
    '*===============================================================================================
    '* Properties File
    '*===============================================================================================
    Public Function ReadPropertiesFile(propsFilePath As String) As PropertiesFile
        Dim propsFile As New PropertiesFile
        'List to store data
        Dim AvailableReSampleRates As New List(Of String)

        Try
            'Open file and read it
            Dim currentLine As String
            FileOpen(1, propsFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read text file
                currentLine = Trim(LineInput(1))

                'Header info
                If InStr(currentLine, "## ") = 1 Then
                    'Split content
                    Dim lineData As String() = Split(currentLine, "...")

                    'Get header info
                    If InStr(currentLine, "## EuroSound") = 1 Then
                        propsFile.HeaderInfo.FileHeader = currentLine
                    End If
                    If InStr(currentLine, "## First Created ...") = 1 Then
                        propsFile.HeaderInfo.FirstCreated = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Created By ...") = 1 Then
                        propsFile.HeaderInfo.CreatedBy = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Last Modified ...") = 1 Then
                        propsFile.HeaderInfo.LastModify = Trim(lineData(1))
                    End If
                    If InStr(currentLine, "## Last Modified By ...") = 1 Then
                        propsFile.HeaderInfo.LastModifyBy = Trim(lineData(1))
                    End If
                End If

                'Available formats section
                If StrComp(currentLine, "#AvailableFormats", CompareMethod.Text) = 0 Then
                    'Read line
                    Dim AvailableFormatsCount As Integer = Trim(LineInput(1))
                    propsFile.AvailableFormats = New String(AvailableFormatsCount - 1, 2) {}

                    'Ensure that there are formats stored
                    If AvailableFormatsCount > 0 Then
                        'Get available formats 
                        For i As Integer = 0 To 2
                            Dim itemsCount As Integer = 0
                            Do
                                'Read line
                                currentLine = Trim(LineInput(1))

                                'Read content
                                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                                    'Add item to listview
                                    propsFile.AvailableFormats(itemsCount, i) = currentLine
                                    'Update counter
                                    itemsCount += 1
                                Else
                                    'Exit loop
                                    Exit Do
                                End If
                            Loop While itemsCount < AvailableFormatsCount
                        Next
                    End If
                End If

                'Read Available Sample Rates
                If StrComp(currentLine, "#AvailableReSampleRates", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Add item to listview
                            AvailableReSampleRates.Add(currentLine)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0

                        'Add items to array
                        propsFile.AvailableReSampleRates = AvailableReSampleRates
                    End If
                End If

                'ReSample Rates for Format
                If InStr(currentLine, "#ReSampleRates") Then
                    'Get index
                    Dim index As Integer = Right(currentLine, 1)

                    'Collection to store values
                    Dim values As New Dictionary(Of String, UInteger)

                    'Read line
                    Dim sampleRateIndex As Integer = 0
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Add item to ArrayList
                            values.Add(AvailableReSampleRates(sampleRateIndex), currentLine)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                            'Update index
                            sampleRateIndex += 1
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                    End If

                    'Add data to dictionary
                    Dim formatName As String = propsFile.AvailableFormats(index, 0)
                    If Not propsFile.sampleRateFormats.ContainsKey(formatName) Then
                        propsFile.sampleRateFormats.Add(formatName, values)
                    End If
                End If

                'Misc properties block
                If StrComp(currentLine, "#MiscProperites", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = Trim(LineInput(1))
                    If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                        Do
                            'Split line
                            Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)

                            Select Case UCase(lineData(0))
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
                            currentLine = Trim(LineInput(1))
                        Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                    End If
                End If
            Loop

            'Read misc properties block
            FileClose(1)
        Catch ex As Exception
            'Inform user about this error
            MsgBox(ex.Message, vbOKOnly + vbCritical, "Error")
        End Try

        'Return object
        Return propsFile
    End Function
End Class

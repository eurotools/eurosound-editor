Imports System.Globalization
Imports System.IO

Public Class FileParsers
    '*===============================================================================================
    '* DATABASE FILES
    '*===============================================================================================
    Public Function ReadDataBaseFile(textFilePath As String) As String()
        'List to store dependencies
        Dim dependencies As New Collection

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            currentLine = LineInput(1)
            If StrComp(currentLine, "#DEPENDENCIES", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                Do
                    'Add item to listbox
                    dependencies.Add(currentLine)

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
            End If
        Loop
        FileClose(1)

        Return CollectionToArray(dependencies)
    End Function

    '*===============================================================================================
    '* SOUNDBANK FILES
    '*===============================================================================================
    Public Function ReadSoundBankFile(textFilePath As String) As SoundbankFile
        'Declare a new object
        Dim objSB As New SoundbankFile

        'List to store dependencies
        Dim dependencies As New Collection

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read a new line
            currentLine = LineInput(1)

            'Check for Dependencies block
            If StrComp(currentLine, "#DEPENDENCIES", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                Do
                    'Add item to listbox
                    dependencies.Add(currentLine)

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
            End If

            'Check for hashcodes block
            If StrComp(currentLine, "#HASHCODE", CompareMethod.Binary) = 0 Then
                'Read a new line
                currentLine = LineInput(1)

                Do
                    'Split line and get number
                    objSB.HashCode = Split(currentLine, " ")(1)
                    objSB.HashCodeLabel = Path.GetFileNameWithoutExtension(textFilePath)

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
            End If
        Loop
        FileClose(1)

        'Add data to object
        objSB.Dependencies = CollectionToArray(dependencies)

        Return objSB
    End Function

    '*===============================================================================================
    '* SFX FILES
    '*===============================================================================================
    Public Function ReadSFXFile(textFilePath As String) As SfxFile
        'Use dot instead of comma for numbers
        Dim provider As New NumberFormatInfo With {
            .NumberDecimalSeparator = "."
        }

        'Create a new object to store the data
        Dim sfxObj As New SfxFile
        'Create list of samples
        Dim samplesList As New List(Of Sample)

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read a new line
            currentLine = LineInput(1)

            'Check for Parameters block
            If StrComp(currentLine, "#SFXParameters", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                Do
                    Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)

                    Select Case UCase(lineData(0))
                        Case "REVERBSEND"
                            sfxObj.Parameters.ReverbSend = CInt(lineData(1))
                        Case "TRACKINGTYPE"
                            sfxObj.Parameters.TrackingType = CByte(lineData(1))
                        Case "INNERRADIUS"
                            sfxObj.Parameters.InnerRadius = CInt(lineData(1))
                        Case "OUTERRADIUS"
                            sfxObj.Parameters.OuterRadius = CInt(lineData(1))
                        Case "MAXVOICES"
                            sfxObj.Parameters.MaxVoices = CInt(lineData(1))
                        Case "ACTION1"
                            sfxObj.Parameters.Action1 = CByte(lineData(1))
                        Case "PRIORITY"
                            sfxObj.Parameters.Priority = CInt(lineData(1))
                        Case "GROUP"
                            sfxObj.Parameters.Group = CInt(lineData(1))
                        Case "ACTION2"
                            sfxObj.Parameters.Action2 = CByte(lineData(1))
                        Case "ALERTNESS"
                            sfxObj.Parameters.Alertness = CInt(lineData(1))
                        Case "IGNOREAGE"
                            sfxObj.Parameters.IgnoreAge = lineData(1).Equals("1")
                        Case "DUCKER"
                            sfxObj.Parameters.Ducker = CInt(lineData(1))
                        Case "DUCKERLENGTH"
                            sfxObj.Parameters.DuckerLenght = CInt(lineData(1))
                        Case "MASTERVOLUME"
                            sfxObj.Parameters.MasterVolume = CInt(lineData(1))
                        Case "OUTDOORS"
                            sfxObj.Parameters.Outdoors = lineData(1).Equals("1")
                        Case "PAUSEINNIS"
                            sfxObj.Parameters.PauseInNis = lineData(1).Equals("1")
                        Case "STEALONAGE"
                            sfxObj.Parameters.StealOnAge = lineData(1).Equals("1")
                        Case "MUSICTYPE"
                            sfxObj.Parameters.MusicType = lineData(1).Equals("1")
                        Case "DOPPLER"
                            sfxObj.Parameters.Doppler = lineData(1).Equals("1")
                    End Select

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
            End If

            'Check for Sample Pool Files block
            If StrComp(currentLine, "#SFXSamplePoolFiles", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        Dim sampleObj As New Sample With {
                            .FilePath = currentLine
                        }
                        samplesList.Add(sampleObj)

                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                End If
            End If

            'Check for Sample Pool Modes block
            If StrComp(currentLine, "#SFXSamplePoolModes", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                    Do
                        'Get properties for each sample
                        For SampleIndex As Integer = 0 To samplesList.Count - 1
                            'IterationS for each sample
                            For iteration As Integer = 0 To 5
                                'Split line
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)

                                Select Case UCase(lineData(0))
                                    Case "BASEVOLUME"
                                        samplesList(SampleIndex).BaseVolume = CSByte(lineData(1))
                                    Case "PITCHOFFSET"
                                        samplesList(SampleIndex).PitchOffset = Convert.ToDouble(lineData(1), provider)
                                    Case "RANDOMPITCHOFFSET"
                                        samplesList(SampleIndex).RandomPitchOffset = Convert.ToDouble(lineData(1), provider)
                                    Case "RANDOMVOLUMEOFFSET"
                                        samplesList(SampleIndex).RandomVolumeOffset = CSByte(lineData(1))
                                    Case "PAN"
                                        samplesList(SampleIndex).RandomVolumeOffset = CSByte(lineData(1))
                                    Case "RANDOMPAN"
                                        samplesList(SampleIndex).RandomVolumeOffset = CSByte(lineData(1))
                                End Select

                                'Continue Reading
                                currentLine = LineInput(1)
                            Next
                        Next
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
                End If
            End If

            'Check for Sample Pool Control block
            If StrComp(currentLine, "#SFXSamplePoolControl", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                Do
                    Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)

                    Select Case UCase(lineData(0))
                        Case "ACTION1"
                            sfxObj.SamplePool.Action1 = CByte(lineData(1))
                        Case "RANDOMPICK"
                            sfxObj.SamplePool.RandomPick = lineData(1).Equals("1")
                        Case "SHUFFLED"
                            sfxObj.SamplePool.Shuffled = lineData(1).Equals("1")
                        Case "LOOP"
                            sfxObj.SamplePool.isLooped = lineData(1).Equals("1")
                        Case "POLYPHOINIC"
                            sfxObj.SamplePool.Polyphonic = lineData(1).Equals("1")
                        Case "MINDELAY"
                            sfxObj.SamplePool.MinDelay = CInt(lineData(1))
                        Case "MAXDELAY"
                            sfxObj.SamplePool.MaxDelay = CInt(lineData(1))
                        Case "ENABLESUBSFX"
                            sfxObj.SamplePool.EnableSubSFX = lineData(1).Equals("1")
                    End Select

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
            End If
        Loop
        FileClose(1)

        'Add samples to the final object
        sfxObj.Samples = samplesList

        Return sfxObj
    End Function

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
                currentLine = LineInput(1)

                'Available formats section
                If StrComp(currentLine, "#AvailableFormats", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = LineInput(1)
                    Dim AvailableFormatsCount As Integer = currentLine
                    propsFile.AvailableFormats = New String(AvailableFormatsCount - 1, 2) {}

                    'Get available formats 
                    For i As Integer = 0 To 2
                        Dim itemsCount As Integer = 0
                        Do
                            'Read line
                            currentLine = LineInput(1)

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

                'Read Available Sample Rates
                If StrComp(currentLine, "#AvailableReSampleRates", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = LineInput(1)

                    'Get ReSample Rates
                    Do
                        'Add item to listview
                        AvailableReSampleRates.Add(currentLine)

                        'Continue Reading
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0

                    'Add items to array
                    propsFile.AvailableReSampleRates = AvailableReSampleRates
                End If

                'ReSample Rates for Format
                If InStr(currentLine, "#ReSampleRates") Then
                    'Get index
                    Dim index As Integer = Right(currentLine, 1)

                    'Collection to store values
                    Dim values As New Dictionary(Of String, UInteger)

                    'Read line
                    Dim sampleRateIndex As Integer = 0
                    currentLine = LineInput(1)
                    Do
                        'Add item to ArrayList
                        values.Add(AvailableReSampleRates(sampleRateIndex), currentLine)

                        'Continue Reading
                        currentLine = LineInput(1)

                        'Update index
                        sampleRateIndex += 1
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0

                    'Add data to dictionary
                    Dim formatName As String = propsFile.AvailableFormats(index, 0)
                    If Not propsFile.sampleRateFormats.ContainsKey(formatName) Then
                        propsFile.sampleRateFormats.Add(formatName, values)
                    End If
                End If

                'Misc properties block
                If StrComp(currentLine, "#MiscProperites", CompareMethod.Text) = 0 Then
                    'Read line
                    currentLine = LineInput(1)
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
                        currentLine = LineInput(1)
                    Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
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

    '*===============================================================================================
    '* PROJECT FILE
    '*===============================================================================================
    Public Function ReadProjectFile(projectFilePath As String) As ProjectFile
        Dim ProjFile As New ProjectFile

        'Temporal lists
        Dim SoundBankList As New List(Of String)
        Dim DataBaseList As New List(Of String)
        Dim SFXList As New List(Of String)

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, projectFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            currentLine = LineInput(1)

            'Soundbanks section
            If StrComp(currentLine, "#SoundBankList", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                Do
                    'Add item to listbox
                    SoundBankList.Add(currentLine)

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
            End If

            'Database Section
            If StrComp(currentLine, "#DataBaseList", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                Do
                    'Add item to listbox
                    DataBaseList.Add(currentLine)

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
            End If

            'SFXs Section
            If StrComp(currentLine, "#SFXList", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                Do
                    'Add item to listbox
                    SFXList.Add(currentLine)

                    'Continue Reading
                    currentLine = LineInput(1)
                Loop While StrComp(currentLine, "#END", CompareMethod.Text) <> 0 AndAlso Not EOF(1)
            End If
        Loop
        FileClose(1)

        'Sync project file if counts are different
        If Directory.GetFiles() Then

            'Add data to object
            ProjFile.DataBaseList = DataBaseList
        ProjFile.SoundBankList = SoundBankList
        ProjFile.SFXList = SFXList

        Return ProjFile
    End Function

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Function CollectionToArray(objCollection As Collection) As String()
        'Create array
        Dim stringArray = New String(objCollection.Count - 1) {}

        'Loop collection
        For i As Integer = 1 To objCollection.Count
            stringArray(i - 1) = objCollection(i)
        Next

        Return stringArray
    End Function

    Private Function GetStringValue(keyword As String, currentLine As String) As String
        Dim keyWordLength = keyword.Length
        Dim LineData As String = currentLine.Substring(keyWordLength).Trim
        Return LineData
    End Function
End Class

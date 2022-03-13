Imports System.ComponentModel
Imports System.IO
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.SoundBanksExporterFunctions

Namespace ReaderClasses
    Partial Public Class FileParsers
        '*===============================================================================================
        '* SFX FILES
        '*===============================================================================================
        Public Function ReadSFXFile(textFilePath As String) As SfxFile
            Dim waveReadFunctions As New WaveFunctions

            'Create a new object to store the data
            Dim sfxObj As New SfxFile
            'Create list of samples
            Dim samplesList As New BindingList(Of Sample)

            'Open file and read it
            Dim currentLine As String
            FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read a new line
                currentLine = Trim(LineInput(1))
                If currentLine <> "" Then
                    'Header info
                    If InStr(currentLine, "## ") = 1 Then
                        'Split content
                        Dim lineData As String() = Split(currentLine, "...")
                        'Get header info
                        If InStr(currentLine, "## EuroSound") = 1 Then
                            sfxObj.HeaderInfo.FileHeader = currentLine
                        End If
                        If InStr(currentLine, "## First Created ...") = 1 Then
                            sfxObj.HeaderInfo.FirstCreated = Trim(lineData(1))
                        End If
                        If InStr(currentLine, "## Created By ...") = 1 Then
                            sfxObj.HeaderInfo.CreatedBy = Trim(lineData(1))
                        End If
                        If InStr(currentLine, "## Last Modified ...") = 1 Then
                            sfxObj.HeaderInfo.LastModify = Trim(lineData(1))
                        End If
                        If InStr(currentLine, "## Last Modified By ...") = 1 Then
                            sfxObj.HeaderInfo.LastModifyBy = Trim(lineData(1))
                        End If
                    End If

                    'Check for Parameters block
                    If StrComp(currentLine, "#SFXParameters") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
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
                                Case "DUCKERLENGHT"
                                    sfxObj.Parameters.DuckerLength = CInt(lineData(1))
                                Case "DUCKERLENGTH"
                                    sfxObj.Parameters.DuckerLength = CInt(lineData(1))
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
                            currentLine = Trim(LineInput(1))
                        End While
                    End If

                    'Check for Sample Pool Files block
                    If StrComp(currentLine, "#SFXSamplePoolFiles") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                            Dim waveRelativePath As String = currentLine
                            Dim waveFullPath As String = fso.BuildPath(WorkingDirectory & "\Master", waveRelativePath)
                            'Wave File
                            Dim sampleObj As New Sample With {
                                    .FilePath = waveRelativePath
                                }
                            'Add object to list
                            samplesList.Add(sampleObj)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        End While
                    End If

                    'Check for Sample Pool Modes block
                    If StrComp(currentLine, "#SFXSamplePoolModes") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        If samplesList.Count > 0 Then
                            While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
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
                                                samplesList(SampleIndex).PitchOffset = Convert.ToDouble(lineData(1), numericProvider)
                                            Case "RANDOMPITCHOFFSET"
                                                samplesList(SampleIndex).RandomPitchOffset = Convert.ToDouble(lineData(1), numericProvider)
                                            Case "RANDOMVOLUMEOFFSET"
                                                samplesList(SampleIndex).RandomVolumeOffset = CSByte(lineData(1))
                                            Case "PAN"
                                                samplesList(SampleIndex).Pan = CSByte(lineData(1))
                                            Case "RANDOMPAN"
                                                samplesList(SampleIndex).RandomPan = CSByte(lineData(1))
                                        End Select

                                        'Continue Reading
                                        currentLine = Trim(LineInput(1))
                                    Next
                                Next
                            End While
                        End If
                    End If

                    'Check for Sample Pool Control block
                    If StrComp(currentLine, "#SFXSamplePoolControl") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
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
                                Case "POLYPHONIC"
                                    sfxObj.SamplePool.Polyphonic = lineData(1).Equals("1")
                                Case "MINDELAY"
                                    sfxObj.SamplePool.MinDelay = CInt(lineData(1))
                                Case "MAXDELAY"
                                    sfxObj.SamplePool.MaxDelay = CInt(lineData(1))
                                Case "ENABLESUBSFX"
                                    sfxObj.SamplePool.EnableSubSFX = lineData(1).Equals("1")
                            End Select
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        End While
                    End If

                    'Check for hashcodes block
                    If StrComp(currentLine, "#HASHCODE") = 0 Then
                        'Read a new line
                        currentLine = Trim(LineInput(1))
                        While StrComp(currentLine, "#END", CompareMethod.Text) <> 0
                            'Split line and get number
                            sfxObj.HashCode = Split(currentLine, " ")(1)
                            'Continue Reading
                            currentLine = Trim(LineInput(1))
                        End While
                    End If
                End If
            Loop
            FileClose(1)
            'Add samples to the final object
            sfxObj.Samples = samplesList
            sfxObj.filePath = textFilePath
            Return sfxObj
        End Function

        Public Function ReadSFXFileExport(textFilePath As String, outPlatform As String, samplesToInclude As HashSet(Of String), streamsList As String(), Optional testMode As Boolean = False) As EXSound
            Dim waveReadFunctions As New WaveFunctions

            'Create a new object to store the data
            Dim sfxObj As New EXSound
            Dim samplesList As New List(Of EXSample)
            Dim flagsArray As Boolean() = New Boolean(12) {}

            'Open file and read it
            Dim currentLine As String
            FileOpen(1, textFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
            Do Until EOF(1)
                'Read a new line
                currentLine = Trim(LineInput(1))
                If currentLine <> "" Then
                    'Check for Parameters block
                    If StrComp(currentLine, "#SFXParameters") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        If StrComp(currentLine, "#END") <> 0 Then
                            Do
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                Select Case UCase(lineData(0))
                                    Case "REVERBSEND"
                                        sfxObj.ReverbSend = CSByte(lineData(1))
                                    Case "TRACKINGTYPE"
                                        sfxObj.TrackingType = CSByte(lineData(1))
                                    Case "INNERRADIUS"
                                        sfxObj.InnerRadius = CShort(lineData(1))
                                    Case "OUTERRADIUS"
                                        sfxObj.OuterRadius = CShort(lineData(1))
                                    Case "MAXVOICES"
                                        sfxObj.MaxVoices = CSByte(lineData(1))
                                    Case "ACTION1"
                                        flagsArray(0) = Convert.ToBoolean(CByte(lineData(1)))
                                    Case "PRIORITY"
                                        sfxObj.Priority = CInt(lineData(1))
                                    Case "IGNOREAGE"
                                        flagsArray(2) = lineData(1).Equals("1")
                                    Case "DUCKER"
                                        sfxObj.Ducker = CInt(lineData(1))
                                    Case "DUCKERLENGHT"
                                        sfxObj.DuckerLength = CInt(lineData(1))
                                    Case "DUCKERLENGTH"
                                        sfxObj.DuckerLength = CInt(lineData(1))
                                    Case "MASTERVOLUME"
                                        sfxObj.MasterVolume = CInt(lineData(1))
                                    Case "OUTDOORS"
                                        flagsArray(8) = lineData(1).Equals("1")
                                    Case "PAUSEINNIS"
                                        flagsArray(9) = lineData(1).Equals("1")
                                    Case "STEALONAGE"
                                        flagsArray(11) = lineData(1).Equals("1")
                                    Case "MUSICTYPE"
                                        flagsArray(12) = lineData(1).Equals("1")
                                End Select
                                'Continue Reading
                                currentLine = Trim(LineInput(1))
                            Loop While StrComp(currentLine, "#END") <> 0
                        End If
                    End If

                    'Check for Sample Pool Files block
                    If StrComp(currentLine, "#SFXSamplePoolFiles") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        If StrComp(currentLine, "#END") <> 0 Then
                            Do
                                Dim waveRelativePath As String = currentLine
                                Dim waveFullPath As String = fso.BuildPath(WorkingDirectory & "\Master", waveRelativePath)
                                'Wave File
                                Dim sampleObj As New EXSample With {
                                    .FilePath = RelativePathToAbs(waveRelativePath)
                                }
                                'Check if this sample is streamed or not
                                If Path.HasExtension(sampleObj.FilePath) Then
                                    Dim arraySearchResult As Integer = Array.IndexOf(streamsList, sampleObj.FilePath)
                                    If arraySearchResult = -1 Then
                                        samplesToInclude.Add(waveRelativePath)
                                    End If
                                End If
                                'Add object to list
                                samplesList.Add(sampleObj)
                                'Continue Reading
                                currentLine = Trim(LineInput(1))
                            Loop While StrComp(currentLine, "#END") <> 0 AndAlso Not EOF(1)
                        End If
                    End If

                    'Check for Sample Pool Modes block
                    If StrComp(currentLine, "#SFXSamplePoolModes") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        If StrComp(currentLine, "#END") <> 0 Then
                            If samplesList.Count > 0 Then
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
                                                    samplesList(SampleIndex).PitchOffset = Convert.ToDouble(lineData(1), numericProvider) * 1024
                                                Case "RANDOMPITCHOFFSET"
                                                    samplesList(SampleIndex).RandomPitchOffset = Convert.ToDouble(lineData(1), numericProvider) * 1024
                                                Case "RANDOMVOLUMEOFFSET"
                                                    samplesList(SampleIndex).RandomVolumeOffset = CSByte(lineData(1))
                                                Case "PAN"
                                                    samplesList(SampleIndex).Pan = CSByte(lineData(1))
                                                Case "RANDOMPAN"
                                                    samplesList(SampleIndex).RandomPan = CSByte(lineData(1))
                                            End Select
                                            'Continue Reading
                                            currentLine = Trim(LineInput(1))
                                        Next
                                        'Add sample to sound
                                        sfxObj.Samples.Add(samplesList(SampleIndex))
                                    Next
                                Loop While StrComp(currentLine, "#END") <> 0
                            End If
                        End If
                    End If

                    'Check for Sample Pool Control block
                    If StrComp(currentLine, "#SFXSamplePoolControl") = 0 Then
                        'Read line
                        currentLine = Trim(LineInput(1))
                        If StrComp(currentLine, "#END") <> 0 Then
                            Do
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                Select Case UCase(lineData(0))
                                    Case "ACTION1"
                                        flagsArray(3) = Convert.ToBoolean(CByte(lineData(1)))
                                    Case "RANDOMPICK"
                                        flagsArray(4) = lineData(1).Equals("1")
                                    Case "SHUFFLED"
                                        flagsArray(5) = lineData(1).Equals("1")
                                    Case "LOOP"
                                        flagsArray(6) = lineData(1).Equals("1")
                                    Case "POLYPHONIC"
                                        flagsArray(7) = lineData(1).Equals("1")
                                    Case "MINDELAY"
                                        sfxObj.MinDelay = CInt(lineData(1))
                                    Case "MAXDELAY"
                                        sfxObj.MaxDelay = CInt(lineData(1))
                                    Case "ENABLESUBSFX"
                                        flagsArray(10) = lineData(1).Equals("1")
                                        sfxObj.HasSubSfx = flagsArray(10)
                                End Select
                                'Continue Reading
                                currentLine = Trim(LineInput(1))
                            Loop While StrComp(currentLine, "#END") <> 0
                        End If
                    End If

                    'Check for hashcodes block
                    If testMode Then
                        sfxObj.HashCode = 3
                    Else
                        If StrComp(currentLine, "#HASHCODE") = 0 Then
                            'Read a new line
                            currentLine = Trim(LineInput(1))
                            If StrComp(currentLine, "#END") <> 0 Then
                                Do
                                    'Split line and get number
                                    sfxObj.HashCode = Split(currentLine, " ")(1)
                                    'Continue Reading
                                    currentLine = Trim(LineInput(1))
                                Loop While StrComp(currentLine, "#END") <> 0
                            End If
                        End If
                    End If
                End If
            Loop
            FileClose(1)
            'Add samples to the final object
            sfxObj.Samples = samplesList
            sfxObj.FilePath = textFilePath
            sfxObj.Flags = GetUserFlags(flagsArray)

            'Calculate Ducker Length
            Dim duckerLength As Short = 0
            If sfxObj.Ducker > 0 Then
                duckerLength = GetTotalCents(sfxObj, outPlatform)
                If sfxObj.DuckerLength < 0 Then
                    duckerLength -= Math.Abs(sfxObj.DuckerLength)
                Else
                    duckerLength += Math.Abs(sfxObj.DuckerLength)
                End If
            End If
            sfxObj.DuckerLength = duckerLength

            Return sfxObj
        End Function
    End Class
End Namespace

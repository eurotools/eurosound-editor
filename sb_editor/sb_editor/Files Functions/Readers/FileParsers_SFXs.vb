Imports System.ComponentModel
Imports System.IO
Imports NAudio.Wave
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

            'Create a new object and list to store the data
            Dim sfxObj As New SfxFile
            Dim samplesList As New BindingList(Of Sample)

            'Open file and read it
            Using sr As StreamReader = File.OpenText(textFilePath)
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
                                sfxObj.HeaderInfo.FileHeader = currentLine
                            End If
                            If currentLine.Contains("## First Created ...") Then
                                sfxObj.HeaderInfo.FirstCreated = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Created By ...") Then
                                sfxObj.HeaderInfo.CreatedBy = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified ...") Then
                                sfxObj.HeaderInfo.LastModify = lineData(1).Trim
                            End If
                            If currentLine.Contains("## Last Modified By ...") Then
                                sfxObj.HeaderInfo.LastModifyBy = lineData(1).Trim
                            End If
                        End If

                        'Check for Parameters block
                        If currentLine.Equals("#SFXParameters", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                Select Case lineData(0).ToUpper
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
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Check for Sample Pool Files block
                        If currentLine.Equals("#SFXSamplePoolFiles", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                Dim waveRelativePath As String = currentLine
                                Dim waveFullPath As String = Path.Combine(WorkingDirectory, "Master", waveRelativePath)
                                'Wave File
                                Dim sampleObj As New Sample With {
                                    .FilePath = waveRelativePath
                                }
                                'Add object to list
                                samplesList.Add(sampleObj)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Check for Sample Pool Modes block
                        If currentLine.Equals("#SFXSamplePoolModes", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            If samplesList.Count > 0 Then
                                While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                    'Get properties for each sample
                                    For SampleIndex As Integer = 0 To samplesList.Count - 1
                                        'IterationS for each sample
                                        For iteration As Integer = 0 To 5
                                            'Split line
                                            Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                            Select Case lineData(0).ToUpper
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
                                            currentLine = sr.ReadLine.Trim
                                        Next
                                    Next
                                End While
                            End If
                        End If

                        'Check for Sample Pool Control block
                        If currentLine.Equals("#SFXSamplePoolControl", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                Select Case lineData(0).ToUpper
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
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Check for hashcodes block
                        If currentLine.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase) Then
                            'Read a new line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Split line and get number
                                sfxObj.HashCode = Split(currentLine, " ")(1)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If
                    End If
                End While
            End Using

            'Add samples to the final object
            sfxObj.Samples = samplesList
            sfxObj.filePath = textFilePath
            Return sfxObj
        End Function

        Public Function ReadSFXFileExport(textFilePath As String, Optional testMode As Boolean = False) As EXSound
            'Create a new object to store the data
            Dim sfxObj As New EXSound
            Dim samplesList As New List(Of EXSample)
            Dim flagsArray As Boolean() = New Boolean(12) {}

            'Open file and read it
            Using sr As StreamReader = File.OpenText(textFilePath)
                While Not sr.EndOfStream
                    Dim currentLine As String = sr.ReadLine.Trim
                    'Skip empty lines
                    If String.IsNullOrEmpty(currentLine) Or currentLine.StartsWith("//") Or currentLine.StartsWith("## ") Then
                        Continue While
                    Else
                        'Check for Parameters block
                        If currentLine.Equals("#SFXParameters", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                Select Case lineData(0).ToUpper
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
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Check for Sample Pool Files block
                        If currentLine.Equals("#SFXSamplePoolFiles", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                'Wave File
                                Dim sampleObj As New EXSample With {
                                    .FilePath = currentLine.ToUpper.TrimStart("\")
                                }
                                'Add object to list
                                samplesList.Add(sampleObj)
                                'Continue Reading
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Check for Sample Pool Modes block
                        If currentLine.Equals("#SFXSamplePoolModes", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            If samplesList.Count > 0 Then
                                While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                    'Get properties for each sample
                                    For SampleIndex As Integer = 0 To samplesList.Count - 1
                                        'IterationS for each sample
                                        For iteration As Integer = 0 To 5
                                            'Split line
                                            Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                            Select Case lineData(0).ToUpper
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
                                            currentLine = sr.ReadLine.Trim
                                        Next
                                        'Add sample to sound
                                        sfxObj.Samples.Add(samplesList(SampleIndex))
                                    Next
                                End While
                            End If
                        End If

                        'Check for Sample Pool Control block
                        If currentLine.Equals("#SFXSamplePoolControl", StringComparison.OrdinalIgnoreCase) Then
                            'Read line
                            currentLine = sr.ReadLine.Trim
                            While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                Dim lineData = currentLine.Split(New Char() {" "c, ChrW(9)}, StringSplitOptions.RemoveEmptyEntries)
                                Select Case lineData(0).ToUpper
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
                                currentLine = sr.ReadLine.Trim
                            End While
                        End If

                        'Check for hashcodes block
                        If testMode Then
                            sfxObj.HashCode = 3
                        Else
                            If currentLine.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase) Then
                                'Read a new line
                                currentLine = sr.ReadLine.Trim
                                While Not currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase)
                                    'Split line and get number
                                    sfxObj.HashCode = Split(currentLine, " ")(1)
                                    'Continue Reading
                                    currentLine = sr.ReadLine.Trim
                                End While
                            End If
                        End If
                    End If
                End While
            End Using

            'Add samples to the final object
            sfxObj.Samples = samplesList
            sfxObj.FilePath = textFilePath
            sfxObj.Flags = GetUserFlags(flagsArray)

            Return sfxObj
        End Function
    End Class
End Namespace

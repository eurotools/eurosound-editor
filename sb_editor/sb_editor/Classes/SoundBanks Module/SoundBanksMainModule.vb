Imports System.IO
Imports NAudio.Wave
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace SoundBanksExporterFunctions
    Module SoundBanksMainModule
        Private ReadOnly textFileReaders As New FileParsers

        '*===============================================================================================
        '* FUNCTIONS TO GET LISTS
        '*===============================================================================================
        Friend Function GetSoundBankSFXsList(soundBankData As SoundbankFile, outputPlatform As String) As String()
            Dim SFXsList As New HashSet(Of String)

            'Get Platform SFXs
            For dataBaseIndex As Integer = 0 To soundBankData.Dependencies.Length - 1
                Dim currentDataBase As String = soundBankData.Dependencies(dataBaseIndex)
                Dim dataBaseFilePath As String = Path.Combine(WorkingDirectory, "DataBases", currentDataBase & ".txt")

                Dim dataBaseFileData As DataBaseFile = textFileReaders.ReadDataBaseFile(dataBaseFilePath)
                For sfxIndex As Integer = 0 To dataBaseFileData.Dependencies.Length - 1
                    Dim sfxFilename As String = dataBaseFileData.Dependencies(sfxIndex)
                    Dim sfxFilePath As String = Path.Combine(WorkingDirectory, "SFXs", sfxFilename & ".txt")
                    Dim specificSFXFilePath As String = Path.Combine(WorkingDirectory, "SFXs", outputPlatform, sfxFilename & ".txt")
                    If File.Exists(specificSFXFilePath) Then
                        SFXsList.Add(outputPlatform & "/" & sfxFilename)
                    Else
                        SFXsList.Add(sfxFilename)
                    End If
                Next
            Next

            'Parse to array and sort
            Dim sfxItemsArray As String() = SFXsList.ToArray
            Array.Sort(sfxItemsArray)

            Return sfxItemsArray
        End Function

        Friend Function GetSoundBankSamplesList(SFXsArray As String(), OutputLanguage As String) As String()
            Dim samplesList As New HashSet(Of String)

            For sfxIndex As Integer = 0 To SFXsArray.Length - 1
                Dim currentSfx As String = SFXsArray(sfxIndex)
                Dim sfxFileData As String() = File.ReadAllLines(Path.Combine(WorkingDirectory, "SFXs", currentSfx & ".txt"))
                Dim startPos As Integer = Array.IndexOf(sfxFileData, "#SFXSamplePoolFiles") + 1
                While Not sfxFileData(startPos).Equals("#END")
                    Dim currentSample As String = sfxFileData(startPos).ToUpper
                    If currentSample.Contains("SPEECH\ENGLISH") AndAlso Not OutputLanguage.Equals("English", StringComparison.OrdinalIgnoreCase) Then
                        currentSample = "SPEECH\" & OutputLanguage.ToUpper & currentSample.Substring(15)
                    End If
                    samplesList.Add(Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "MASTER\" & currentSample).ToUpper)
                    startPos += 1
                End While
            Next

            'Parse to array and sort
            Dim samplesListArray As String() = samplesList.ToArray
            Array.Sort(samplesListArray)

            Return samplesListArray
        End Function

        '*===============================================================================================
        '* FUNCTIONS RELATIONS WITH THE FILES MANAGMENT
        '*===============================================================================================
        Friend Function GetFinalList(samplesArray As String(), StreamsArray As String(), outputPlatform As String, Optional previewMode As Boolean = True) As String()
            Dim samplesToInclude As New HashSet(Of String)
            Dim finalSampleList As String() = RemoveAllSubSFX(samplesArray, previewMode)
            If previewMode Then
                If outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase) Or outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase) Then
                    finalSampleList = RemoveAllStreams(finalSampleList, StreamsArray)
                End If
            Else
                finalSampleList = RemoveAllStreams(finalSampleList, StreamsArray)
            End If

            'Sort and return
            Array.Sort(finalSampleList)
            Return finalSampleList
        End Function

        Private Function RemoveAllSubSFX(SamplesArray As String(), Optional previewMode As Boolean = True)
            Dim samplesToInclude As New HashSet(Of String)
            If previewMode Then
                For sampleIndex As Integer = 0 To SamplesArray.Length - 1
                    Dim currentSample As String = SamplesArray(sampleIndex)
                    If Path.HasExtension(currentSample) AndAlso Not currentSample.Contains("\\") Then
                        samplesToInclude.Add(currentSample)
                    End If
                Next
            Else
                For sampleIndex As Integer = 0 To SamplesArray.Length - 1
                    Dim currentSample As String = SamplesArray(sampleIndex)
                    If Path.HasExtension(currentSample) Then
                        samplesToInclude.Add(currentSample)
                    End If
                Next
            End If

            Return samplesToInclude.ToArray
        End Function

        Private Function RemoveAllStreams(SamplesArray As String(), StreamsArray As String())
            Dim samplesToInclude As New HashSet(Of String)
            For sampleIndex As Integer = 0 To SamplesArray.Length - 1
                Dim currentSample As String = SamplesArray(sampleIndex)
                Dim sampleIsStream As Boolean = False
                For streamIndex As Integer = 0 To StreamsArray.Length - 1
                    Dim currentStream As String = StreamsArray(streamIndex)
                    If currentSample.Contains(currentStream) Then
                        sampleIsStream = True
                        Exit For
                    End If
                Next
                'Check if we can add this sample
                If sampleIsStream = False Then
                    samplesToInclude.Add(currentSample)
                End If
            Next

            Return samplesToInclude.ToArray
        End Function

        '*===============================================================================================
        '* FUNCTIONS TO CALCULATE ESTIMATED SIZE - NO MASTER FOLDER
        '*===============================================================================================
        Friend Function GetEstimatedPlatformSizeNoMaster(samplesList As String(), outputPlatform As String) As Integer
            Dim sampleSize As Integer = 0
            For sampleIndex As Integer = 0 To samplesList.Length - 1
                Dim filePath As String = samplesList(sampleIndex)
                If Path.HasExtension(filePath) Then
                    If outputPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) Or outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase) Then
                        sampleSize += 36
                    Else
                        sampleSize += 32
                    End If
                End If
            Next
            Return sampleSize
        End Function

        '*===============================================================================================
        '* FUNCTIONS TO EXPORT SOUNDBANKS
        '*===============================================================================================
        Friend Sub ExportSoundBank(soundbankData As SoundbankFile, streamsList As String(), outputPlatform As String, outputLanguage As String, testMode As Boolean)
            'Get as list of the data that we will export
            Dim sfxFilesToInclude As String() = GetSoundBankSFXsList(soundbankData, outputPlatform)
            Dim sampleToInclude As String() = GetFinalList(GetSoundBankSamplesList(sfxFilesToInclude, outputLanguage), streamsList, outputPlatform, False)

            'SFX Stuff
            Dim sfxDictionary As SortedDictionary(Of String, EXSound) = ReadSfxData(sfxFilesToInclude, testMode)
            ApplyDuckerLength(sfxDictionary, outputPlatform)

            'Samples Suff
            Dim samplesDictionary As Dictionary(Of String, EXAudio) = ReadSampleData(sampleToInclude, outputPlatform)

            'Get SFX Dictionary
        End Sub

        Friend Function GetUserFlags(checkedFlags As Boolean()) As Short
            'Get Flags
            Dim selectedFlags As Short = 0
            For index As Integer = 0 To checkedFlags.Length - 1
                If checkedFlags(index) Then
                    selectedFlags = selectedFlags Or (1 << index)
                End If
            Next
            Return selectedFlags
        End Function

        Friend Function ReadSfxData(sfxList As String(), testMode As Boolean) As SortedDictionary(Of String, EXSound)
            Dim sfxDictionary As New SortedDictionary(Of String, EXSound)
            For fileIndex As Integer = 0 To sfxList.Length - 1
                Dim sfxFilePath As String = Path.Combine(WorkingDirectory, "SFXs", sfxList(fileIndex).TrimStart("\") & ".txt")
                Dim soundObj As EXSound = textFileReaders.ReadSFXFileExport(sfxFilePath, testMode)
                sfxDictionary.Add(Path.GetFileNameWithoutExtension(sfxFilePath).ToUpper, soundObj)
            Next

            Return sfxDictionary
        End Function

        Friend Sub ApplyDuckerLength(sfxDictionary As SortedDictionary(Of String, EXSound), outPlatform As String)
            For Each soundToCheck As EXSound In sfxDictionary.Values
                Dim duckerLength As Integer = 0
                'Ensure if this sound uses ducker stuff
                If soundToCheck.Ducker > 0 Then
                    'Inspect Samples
                    For Each SampleToCheck As EXSample In soundToCheck.Samples
                        Dim sampleFilePath As String = Path.Combine(WorkingDirectory, outPlatform, SampleToCheck.FilePath)
                        If File.Exists(sampleFilePath) Then
                            Using reader As New WaveFileReader(sampleFilePath)
                                Dim cents = reader.TotalTime.TotalMilliseconds / 10
                                duckerLength += cents
                            End Using
                        End If
                    Next

                    'Apply value
                    If soundToCheck.DuckerLength < 0 Then
                        duckerLength -= Math.Abs(soundToCheck.DuckerLength)
                    Else
                        duckerLength += Math.Abs(soundToCheck.DuckerLength)
                    End If
                    soundToCheck.DuckerLength = duckerLength
                End If
            Next
        End Sub

        Friend Function ReadSampleData(samplesList As String(), outputPlatform As String) As Dictionary(Of String, EXAudio)
            Dim CancelSoundBankOutput As Boolean = False
            Dim samplesDictionary As New Dictionary(Of String, EXAudio)

            For sampleIndex As Integer = 0 To samplesList.Length - 1
                If CancelSoundBankOutput Then
                    Exit For
                Else
                    'Get file path
                    Dim masterWaveFile As String = samplesList(sampleIndex)
                    Dim relativeFilePath As String = masterWaveFile.Substring((ProjectSettingsFile.MiscProps.SampleFileFolder & "\Master\").Length).TrimStart("\")
                    If Not samplesDictionary.ContainsKey(relativeFilePath.ToUpper) Then
                        'Get Format sample
                        Dim platformWaveFile As String = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, outputPlatform, relativeFilePath)
                        If outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase) Then
                            platformWaveFile = Path.ChangeExtension(platformWaveFile, ".AIF")
                        End If
                        'Ensure that the two file paths exists
                        If File.Exists(masterWaveFile) AndAlso File.Exists(platformWaveFile) Then
                            Dim soundDataObj As New EXAudio

                            'Read master wave data
                            Dim loopInfo As Integer()
                            Dim masterWaveSampleRate As Integer = 0
                            Using waveReader As New WaveFileReader(masterWaveFile)
                                loopInfo = ReadWaveSampleChunk(waveReader)
                                soundDataObj.Duration = waveReader.TotalTime.TotalMilliseconds
                                soundDataObj.Flags = loopInfo(0)
                                soundDataObj.Bits = 4
                                soundDataObj.NumberOfChannels = waveReader.WaveFormat.Channels
                                masterWaveSampleRate = waveReader.WaveFormat.SampleRate
                            End Using

                            'Read platform wave
                            Select Case outputPlatform.ToUpper
                                Case "PC"
                                    LoadPcmForPC(platformWaveFile, soundDataObj, loopInfo, masterWaveSampleRate)
                                Case "PLAYSTATION2"
                                    Dim vagFilePath As String = Path.ChangeExtension(Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "PlayStation2_VAG", relativeFilePath), ".VAG")
                                    If File.Exists(platformWaveFile) Then
                                        LoadVagForPlayStation2(platformWaveFile, vagFilePath, soundDataObj, loopInfo, masterWaveSampleRate)
                                    Else
                                        MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & masterWaveFile, vbOKOnly + vbCritical, "EuroSound")
                                        CancelSoundBankOutput = True
                                    End If
                                Case "GAMECUBE"
                                    Dim dspFilePath As String = Path.ChangeExtension(Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "GameCube_dsp_adpcm", relativeFilePath), ".DSP")
                                    If File.Exists(platformWaveFile) Then
                                        LoadDspForGameCube(platformWaveFile, dspFilePath, soundDataObj, loopInfo, masterWaveSampleRate)
                                    Else
                                        MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & masterWaveFile, vbOKOnly + vbCritical, "EuroSound")
                                        CancelSoundBankOutput = True
                                    End If
                                Case Else
                                    Dim adpcmFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "XBox_adpcm", relativeFilePath)
                                    If File.Exists(platformWaveFile) Then
                                        LoadAdpcmForXbox(platformWaveFile, adpcmFilePath, soundDataObj, loopInfo)
                                    Else
                                        MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & masterWaveFile, vbOKOnly + vbCritical, "EuroSound")
                                        CancelSoundBankOutput = True
                                    End If
                            End Select

                            'Add readed data to the dictionary
                            samplesDictionary.Add(relativeFilePath.ToUpper, soundDataObj)
                        Else
                            MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & masterWaveFile, vbOKOnly + vbCritical, "EuroSound")
                            CancelSoundBankOutput = True
                        End If
                    End If
                End If
            Next
            Return samplesDictionary
        End Function

        '*===============================================================================================
        '* FUNCTIONS TO READ THE SPECIFIC PLATFORM DATA
        '*===============================================================================================
        Private Sub LoadPcmForPC(platformWave As String, newAudioObj As EXAudio, loopInfo As Integer(), masterFileSampleRate As Integer)
            Using platformWaveReader As New WaveFileReader(platformWave)
                'Common info
                newAudioObj.Frequency = platformWaveReader.WaveFormat.SampleRate
                newAudioObj.NumberOfChannels = platformWaveReader.WaveFormat.Channels
                newAudioObj.Bits = 4

                'Get PCM Data and calculate loop Offset
                newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(platformWaveReader.Length, 4) - 1) {}
                platformWaveReader.Read(newAudioObj.SampleData, 0, platformWaveReader.Length)

                'Get Real length
                newAudioObj.RealSize = platformWaveReader.Length

                'Loop offset
                If loopInfo(0) = 1 Then
                    newAudioObj.LoopOffset = ESUtils.BytesFunctions.AlignNumber(ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileSampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1) * 2), 2)
                End If
            End Using
        End Sub

        Private Sub LoadVagForPlayStation2(platformWave As String, vagFilePath As String, newAudioObj As EXAudio, loopInfo As Integer(), masterFileSampleRate As Integer)
            Using platformWaveReader As New AiffFileReader(platformWave)
                newAudioObj.Frequency = platformWaveReader.WaveFormat.SampleRate
                newAudioObj.NumberOfChannels = platformWaveReader.WaveFormat.Channels
                newAudioObj.Bits = 4

                'Get vag block data aligned
                Dim vagFile As Byte() = GetVagFileDataChunk(vagFilePath)
                newAudioObj.RealSize = vagFile.Length - 1
                newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(vagFile.Length, 64) - 1) {}
                Buffer.BlockCopy(vagFile, 0, newAudioObj.SampleData, 0, vagFile.Length)

                'Loop offset
                If loopInfo(0) = 1 Then
                    newAudioObj.LoopOffset = Math.Round(ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileSampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1) * 2))
                End If
            End Using
        End Sub

        Private Sub LoadDspForGameCube(platformWave As String, dspFilePath As String, newAudioObj As EXAudio, loopInfo As Integer(), masterFileSampleRate As Integer)
            Using platformWaveReader As New WaveFileReader(platformWave)
                newAudioObj.Frequency = platformWaveReader.WaveFormat.SampleRate
                newAudioObj.NumberOfChannels = platformWaveReader.WaveFormat.Channels
                newAudioObj.Bits = 4

                'Get wave block data aligned
                Dim dspFile As Byte() = GetDspDataChunk(dspFilePath)
                newAudioObj.RealSize = dspFile.Length - 1
                newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(dspFile.Length, 32) - 1) {}
                Buffer.BlockCopy(dspFile, 0, newAudioObj.SampleData, 0, dspFile.Length)

                'Get header data
                newAudioObj.DspHeaderData = GetDspHeaderData(dspFilePath)

                'Loop offset
                If loopInfo(0) = 1 Then
                    newAudioObj.LoopOffset = Math.Round(ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileSampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1) * 2))
                End If
            End Using
        End Sub

        Private Sub LoadAdpcmForXbox(platformWave As String, xboxFilePath As String, newAudioObj As EXAudio, loopInfo As Integer())
            Using platformWaveReader As New WaveFileReader(platformWave)
                newAudioObj.Frequency = platformWaveReader.WaveFormat.SampleRate
                newAudioObj.NumberOfChannels = platformWaveReader.WaveFormat.Channels
                newAudioObj.SampleData = GetXboxAdpcmDataChunk(xboxFilePath)
                newAudioObj.RealSize = newAudioObj.SampleData.Length - 1
                newAudioObj.Bits = 4

                'Loop offset
                If loopInfo(0) = 1 Then
                    newAudioObj.LoopOffset = ESUtils.CalculusLoopOffset.GetXboxAlignedNumber(loopInfo(1))
                End If
            End Using
        End Sub

        '*===============================================================================================
        '* FUNCTIONS RELATED WITH THE MAX SIZE
        '*===============================================================================================
        Friend Function GetSoundBankMaxSize(currentPlatform As String, soundBankData As SoundbankFile) As Long
            Dim maxSize As Long
            Select Case currentPlatform
                Case "PlayStation2"
                    If soundBankData.MaxBankSizes.PlayStationSize > 0 Then
                        maxSize = soundBankData.MaxBankSizes.PlayStationSize
                    Else
                        maxSize = SoundBankMaxPlayStation
                    End If
                Case "GameCube"
                    If soundBankData.MaxBankSizes.GameCubeSize > 0 Then
                        maxSize = soundBankData.MaxBankSizes.GameCubeSize
                    Else
                        maxSize = SoundBankMaxGameCube
                    End If
                Case "PC"
                    If soundBankData.MaxBankSizes.PCSize > 0 Then
                        maxSize = soundBankData.MaxBankSizes.PCSize
                    Else
                        maxSize = SoundBankMaxPC
                    End If
                Case Else
                    If soundBankData.MaxBankSizes.XboxSize > 0 Then
                        maxSize = soundBankData.MaxBankSizes.XboxSize
                    Else
                        maxSize = SoundBankMaxXbox
                    End If
            End Select
            Return maxSize
        End Function

        '*===============================================================================================
        '* FUNCTIONS TO WRITE FILES
        '*===============================================================================================
        Friend Function WriteSfxFile(binWriter As BinaryWriter, hashCodesList As SortedDictionary(Of String, UInteger), sfxDictionary As SortedDictionary(Of String, EXSound), samplesDictionary As Dictionary(Of String, EXAudio), streamsList As String(), isBigEndian As Boolean) As List(Of KeyValuePair(Of String, Integer))
            Dim sfxStartOffsets As New Queue(Of UInteger)
            Dim streamsReport As New List(Of KeyValuePair(Of String, Integer))

            'SFX header
            binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sfxDictionary.Count, isBigEndian))
            For Each sfxToWrite As EXSound In sfxDictionary.Values
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sfxToWrite.HashCode, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(0, isBigEndian))
            Next

            'SFX parameter entry 
            For Each sfxItem As KeyValuePair(Of String, EXSound) In sfxDictionary
                Dim sfxToWrite As EXSound = sfxItem.Value
                sfxStartOffsets.Enqueue(binWriter.BaseStream.Position)
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(sfxToWrite.DuckerLength, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(sfxToWrite.MinDelay, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(sfxToWrite.MaxDelay, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(sfxToWrite.InnerRadius, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(sfxToWrite.OuterRadius, isBigEndian))
                binWriter.Write(sfxToWrite.ReverbSend)
                binWriter.Write(sfxToWrite.TrackingType)
                binWriter.Write(sfxToWrite.MaxVoices)
                binWriter.Write(sfxToWrite.Priority)
                binWriter.Write(sfxToWrite.Ducker)
                binWriter.Write(sfxToWrite.MasterVolume)
                binWriter.Write(sfxToWrite.Flags)
                binWriter.Write(ESUtils.BytesFunctions.FlipUShort(sfxToWrite.Samples.Count, isBigEndian))
                For sampleIndex As Integer = 0 To sfxToWrite.Samples.Count - 1
                    Dim currentSample As EXSample = sfxToWrite.Samples(sampleIndex)
                    'Find File Ref
                    Dim fileRef As Short
                    If sfxToWrite.HasSubSfx Then
                        If hashCodesList.ContainsKey(currentSample.FilePath) Then
                            fileRef = hashCodesList(currentSample.FilePath)
                        Else
                            MsgBox("HashCode Not found " & currentSample.FilePath, vbOKOnly + vbCritical, "EuroSound")
                        End If
                    Else
                        If samplesDictionary.ContainsKey(currentSample.FilePath) Then
                            fileRef = samplesDictionary(currentSample.FilePath).FileRef
                        Else
                            Dim streamFileIndex As Integer = Array.IndexOf(streamsList, currentSample.FilePath)
                            If streamFileIndex <> -1 Then
                                fileRef = (streamFileIndex + 1) * -1
                                streamsReport.Add(New KeyValuePair(Of String, Integer)(currentSample.FilePath, fileRef))
                            Else
                                MsgBox("Stream Ref Not Found " & currentSample.FilePath, vbOKOnly + vbCritical, "EuroSound")
                            End If
                        End If
                    End If
                    'Write values
                    binWriter.Write(ESUtils.BytesFunctions.FlipShort(fileRef, isBigEndian))
                    binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSample.PitchOffset, isBigEndian))
                    binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSample.RandomPitchOffset, isBigEndian))
                    binWriter.Write(currentSample.BaseVolume)
                    binWriter.Write(currentSample.RandomVolumeOffset)
                    binWriter.Write(currentSample.Pan)
                    binWriter.Write(currentSample.RandomPan)
                    binWriter.Write(CByte(0))
                    binWriter.Write(CByte(0))
                Next
            Next

            'Write start offsets
            binWriter.BaseStream.Seek(8, SeekOrigin.Begin)
            For index As Integer = 0 To sfxStartOffsets.Count - 1
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sfxStartOffsets.Dequeue, isBigEndian))
                binWriter.BaseStream.Seek(4, SeekOrigin.Current)
            Next

            Return streamsReport
        End Function

        Friend Sub WriteSifFile(binWriter As BinaryWriter, SamplesDictionary As Dictionary(Of String, EXAudio), isBigEndian As Boolean)
            binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(SamplesDictionary.Count, isBigEndian))
            Dim dspHeaderIndex As Integer = 0
            For Each soundToWrite As EXAudio In SamplesDictionary.Values
                binWriter.Write(ESUtils.BytesFunctions.FlipInt32(soundToWrite.Flags, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(soundToWrite.Address, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(soundToWrite.SampleData.Length, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(soundToWrite.Frequency, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(soundToWrite.RealSize, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(soundToWrite.NumberOfChannels, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(soundToWrite.Bits, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(dspHeaderIndex * 96, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(soundToWrite.LoopOffset, isBigEndian))
                binWriter.Write(soundToWrite.Duration)
                dspHeaderIndex += 1
            Next
        End Sub

        Friend Sub WriteSsfFile(binWriter As BinaryWriter, SamplesDictionary As Dictionary(Of String, EXAudio))
            For Each soundToWrite As EXAudio In SamplesDictionary.Values
                binWriter.Write(soundToWrite.DspHeaderData)
            Next
        End Sub

        Friend Sub WriteSbfFile(binWriter As BinaryWriter, SamplesDictionary As Dictionary(Of String, EXAudio))
            Dim fileRef As Integer = 0
            For Each soundToWrite As EXAudio In SamplesDictionary.Values
                soundToWrite.Address = binWriter.BaseStream.Position
                soundToWrite.FileRef = fileRef
                binWriter.Write(soundToWrite.SampleData)
                fileRef += 1
            Next
        End Sub
    End Module
End Namespace

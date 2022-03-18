Imports System.IO
Imports NAudio.Wave
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace SoundBanksExporterFunctions
    Friend Module SBExporterModule
        Private ReadOnly textFileReaders As New FileParsers

        Friend Sub GetSFXsDictionary(sfxList As String(), outPlatform As String, SfxDictionary As SortedDictionary(Of String, EXSound), samplesToInclude As HashSet(Of String), streamsList As String(), Optional testMode As Boolean = False)
            'Read all stored SFXs in the DataBases
            For sfxIndex As Integer = 0 To sfxList.Length - 1
                Dim sfxFileName As String = Path.GetFileNameWithoutExtension(sfxList(sfxIndex))
                Dim sfxFilePath As String = Path.Combine(WorkingDirectory & "SFXs", sfxList(sfxIndex) & ".txt")
                If File.Exists(sfxFilePath) Then
                    'Read SFX
                    Dim soundToAdd As EXSound = textFileReaders.ReadSFXFileExport(sfxFilePath, outPlatform, samplesToInclude, streamsList, testMode)
                    SfxDictionary.Add(UCase(sfxFileName), soundToAdd)
                End If
            Next
        End Sub

        Friend Sub GetSamplesDictionary(samplesToInclude As HashSet(Of String), SamplesDictionary As Dictionary(Of String, EXAudio), outPlatform As String, outputLanguage As String, CancelSoundBankOutput As Boolean, Optional testMode As Boolean = False)
            Dim SamplesSortedArray As String() = samplesToInclude.ToArray
            Array.Sort(SamplesSortedArray)
            For sampleIndex As Integer = 0 To SamplesSortedArray.Length - 1
                Dim sampleRelPath As String = SamplesSortedArray(sampleIndex)
                'If starts with speech but doesn't match the current language, get the sample with the right language
                If InStr(1, sampleRelPath, "SPEECH\ENGLISH", CompareMethod.Binary) AndAlso StrComp(outputLanguage, "ENGLISH") <> 0 Then
                    sampleRelPath = "SPEECH\" & outputLanguage & Mid(sampleRelPath, 15)
                End If
                'Add sample if we have not read it previously
                Dim sampleName As String = sampleRelPath.TrimStart("\")
                If Not SamplesDictionary.ContainsKey(sampleName) Then
                    SamplesDictionary.Add(sampleName, GetEXaudio(sampleRelPath, outPlatform, CancelSoundBankOutput, testMode))
                End If
                If CancelSoundBankOutput Then
                    Exit For
                End If
            Next

            Erase SamplesSortedArray
            samplesToInclude = Nothing
        End Sub

        Friend Function GetEXaudio(relativeSampleFilePath As String, outputPlatform As String, ByRef CancelSoundBankOutput As Boolean, testMode As Boolean) As EXAudio
            Dim waveFunctions As New WaveFunctions
            Dim newAudioObj As New EXAudio
            If testMode Then
                Using masterWaveReader As New WaveFileReader(Path.Combine(WorkingDirectory, "Master", relativeSampleFilePath))
                    Dim loopInfo As Integer() = waveFunctions.ReadSampleChunk(masterWaveReader)
                    'Get address and flags
                    newAudioObj.Flags = loopInfo(0)
                    newAudioObj.Frequency = masterWaveReader.WaveFormat.SampleRate
                    newAudioObj.NumberOfChannels = masterWaveReader.WaveFormat.Channels
                    newAudioObj.Bits = 4
                    newAudioObj.FilePath = relativeSampleFilePath
                    newAudioObj.Duration = masterWaveReader.TotalTime.TotalMilliseconds
                    newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(masterWaveReader.Length, 4) - 1) {}
                    masterWaveReader.Read(newAudioObj.SampleData, 0, masterWaveReader.Length)
                    'Get Real length
                    newAudioObj.RealSize = masterWaveReader.Length
                    'Loop offset
                    If loopInfo(0) = 1 Then
                        newAudioObj.LoopOffset = ESUtils.BytesFunctions.AlignNumber(loopInfo(1) * 2, 2)
                    End If
                End Using
            Else
                'Get loop info
                Dim masterWaveFilePath As String = Path.Combine(WorkingDirectory, "Master", relativeSampleFilePath)
                If File.Exists(masterWaveFilePath) Then
                    'Read MasterFile
                    Dim loopInfo As Integer()
                    Dim masterFileSampleRate As Integer = 0
                    Using masterWaveReader As New WaveFileReader(masterWaveFilePath)
                        loopInfo = waveFunctions.ReadSampleChunk(masterWaveReader)
                        newAudioObj.Duration = masterWaveReader.TotalTime.TotalMilliseconds
                        newAudioObj.Flags = loopInfo(0)
                        masterFileSampleRate = masterWaveReader.WaveFormat.SampleRate
                    End Using

                    'Specific formats
                    Select Case outputPlatform
                        Case "PC"
                            Dim platformWave As String = Path.Combine(WorkingDirectory, outputPlatform, relativeSampleFilePath)
                            If File.Exists(platformWave) Then
                                Using platformWaveReader As New WaveFileReader(platformWave)
                                    'Common info
                                    newAudioObj.Frequency = platformWaveReader.WaveFormat.SampleRate
                                    newAudioObj.NumberOfChannels = platformWaveReader.WaveFormat.Channels
                                    newAudioObj.Bits = 4
                                    newAudioObj.FilePath = relativeSampleFilePath

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
                            Else
                                MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & platformWave, vbOKOnly + vbCritical, "EuroSound")
                                CancelSoundBankOutput = True
                            End If
                        Case "PlayStation2"
                            Dim platformWave As String = Path.Combine(WorkingDirectory, outputPlatform, Path.ChangeExtension(relativeSampleFilePath, ".aif"))
                            If File.Exists(platformWave) Then
                                Using platformWaveReader As New AiffFileReader(platformWave)
                                    Dim vagFilePath As String = Path.ChangeExtension(Path.Combine(WorkingDirectory, "PlayStation2_VAG", relativeSampleFilePath), ".vag")
                                    If File.Exists(vagFilePath) Then
                                        'Get Vag File Without Header
                                        Dim vagFileWithHeader As Byte() = File.ReadAllBytes(vagFilePath)
                                        Dim vagFile As Byte() = New Byte(vagFileWithHeader.Length - 48) {}
                                        Array.Copy(vagFileWithHeader, 48, vagFile, 0, vagFileWithHeader.Length - 48)

                                        'Get wave block data aligned
                                        newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(vagFile.Length, 64) - 1) {}
                                        Buffer.BlockCopy(vagFile, 0, newAudioObj.SampleData, 0, vagFile.Length)

                                        'Get Real length
                                        newAudioObj.RealSize = vagFile.Length

                                        'Loop offset
                                        If loopInfo(0) = 1 Then
                                            newAudioObj.LoopOffset = Math.Round(ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileSampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1) * 2))
                                        End If
                                    Else
                                        MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & vagFilePath, vbOKOnly + vbCritical, "EuroSound")
                                        CancelSoundBankOutput = True
                                    End If
                                End Using
                            Else
                                MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & platformWave, vbOKOnly + vbCritical, "EuroSound")
                                CancelSoundBankOutput = True
                            End If
                        Case "GameCube"
                            Dim platformWave As String = Path.Combine(WorkingDirectory, outputPlatform, relativeSampleFilePath)
                            If File.Exists(platformWave) Then
                                Using platformWaveReader As New WaveFileReader(platformWave)
                                    Dim dspFilePath As String = Path.ChangeExtension(Path.Combine(WorkingDirectory, "GameCube_dsp_adpcm", relativeSampleFilePath), ".dsp")
                                    If File.Exists(dspFilePath) Then
                                        Dim dspFile As Byte() = File.ReadAllBytes(dspFilePath)
                                        'Get wave block data aligned
                                        newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(dspFile.Length, 32) - 1) {}
                                        Buffer.BlockCopy(dspFile, 0, newAudioObj.SampleData, 0, dspFile.Length)
                                        newAudioObj.DspHeaderData = File.ReadAllBytes(Path.ChangeExtension(dspFilePath, ".dsph"))
                                        'Get Real length
                                        newAudioObj.RealSize = dspFile.Length
                                        'Loop offset
                                        If loopInfo(0) = 1 Then
                                            newAudioObj.LoopOffset = Math.Round(ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileSampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1) * 2))
                                        End If
                                    Else
                                        MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & dspFilePath, vbOKOnly + vbCritical, "EuroSound")
                                        CancelSoundBankOutput = True
                                    End If
                                End Using
                            Else
                                MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & platformWave, vbOKOnly + vbCritical, "EuroSound")
                                CancelSoundBankOutput = True
                            End If
                        Case Else
                            Dim xboxFilePath As String = Path.ChangeExtension(Path.Combine(WorkingDirectory, "XBox_adpcm", relativeSampleFilePath), ".adpcm")
                            If File.Exists(xboxFilePath) Then
                                'Get Adpcm File Without Header
                                Dim adpcmFileWithHeader As Byte() = File.ReadAllBytes(xboxFilePath)
                                Dim adpcmFile As Byte() = New Byte(adpcmFileWithHeader.Length - 48) {}
                                Array.Copy(adpcmFileWithHeader, 48, adpcmFile, 0, adpcmFileWithHeader.Length - 48)
                                newAudioObj.SampleData = adpcmFile

                                'Get Real length
                                newAudioObj.RealSize = newAudioObj.SampleData.Length

                                'Loop offset
                                If loopInfo(0) = 1 Then
                                    newAudioObj.LoopOffset = ESUtils.CalculusLoopOffset.GetXboxAlignedNumber(loopInfo(1))
                                End If
                            Else
                                MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & xboxFilePath, vbOKOnly + vbCritical, "EuroSound")
                                CancelSoundBankOutput = True
                            End If
                    End Select
                Else
                    MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & masterWaveFilePath, vbOKOnly + vbCritical, "EuroSound")
                    CancelSoundBankOutput = True
                End If
            End If
            Return newAudioObj
        End Function

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
        Friend Sub WriteSfxFile(binWriter As BinaryWriter, hashCodesList As SortedDictionary(Of String, UInteger), sfxDictionary As SortedDictionary(Of String, EXSound), samplesDictionary As Dictionary(Of String, EXAudio), streamsList As String(), isBigEndian As Boolean)
            binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sfxDictionary.Count, isBigEndian))
            Dim sfxStartOffsets As New Queue(Of UInteger)
            Dim samplesList As String() = samplesDictionary.Keys.ToArray
            'SFX header
            For Each sfxToWrite As EXSound In sfxDictionary.Values
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sfxToWrite.HashCode, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(0, isBigEndian))
            Next
            'SFX parameter entry 
            Dim StreamFileRefCheckSum As Integer = 0
            For Each sfxToWrite As EXSound In sfxDictionary.Values
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
                        If hashCodesList IsNot Nothing Then
                            Dim hashCodeLabel As String = Path.GetFileNameWithoutExtension(currentSample.FilePath.TrimStart("\"c))
                            If hashCodesList.ContainsKey(hashCodeLabel) Then
                                fileRef = hashCodesList(hashCodeLabel)
                            Else
                                MsgBox("HashCode Not found " & currentSample.FilePath, vbOKOnly + vbCritical, "EuroSound")
                            End If
                        Else
                            fileRef = 0
                        End If
                    Else
                        Dim streamFileIndex As Integer = Array.IndexOf(streamsList, currentSample.FilePath)
                        If streamFileIndex = -1 Then
                            fileRef = Array.IndexOf(samplesList, currentSample.FilePath)
                            'Inform User
                            If fileRef = -1 Then
                                MsgBox("Stream/Sample Ref Not Found " & currentSample.FilePath, vbOKOnly + vbCritical, "EuroSound")
                            End If
                        Else
                            fileRef = (streamFileIndex + 1) * -1
                            PrintLine(1, fileRef & "    \" & currentSample.FilePath)
                            StreamFileRefCheckSum += Math.Abs(fileRef)
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
            'Close debug file
            PrintLine(1, "StreamFileRefCheckSum = " & (StreamFileRefCheckSum * -1))
            FileClose(1)
            'Write start offsets
            binWriter.BaseStream.Seek(8, SeekOrigin.Begin)
            For index As Integer = 0 To sfxStartOffsets.Count - 1
                binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sfxStartOffsets.Dequeue, isBigEndian))
                binWriter.BaseStream.Seek(4, SeekOrigin.Current)
            Next
        End Sub

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
            For Each soundToWrite As EXAudio In SamplesDictionary.Values
                soundToWrite.Address = binWriter.BaseStream.Position
                binWriter.Write(soundToWrite.SampleData)
            Next
        End Sub
    End Module
End Namespace

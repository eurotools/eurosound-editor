Imports System.IO
Imports NAudio.Wave
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace SoundBanksExporterFunctions
    Friend Module SBExporterModule
        Private ReadOnly textFileReaders As New FileParsers

        Friend Sub GetSFXsDictionary(sfxList As String(), outPlatform As String, SfxDictionary As Dictionary(Of String, EXSound), samplesToInclude As HashSet(Of String), streamsList As String(), Optional testMode As Boolean = False)
            'Read all stored SFXs in the DataBases
            For sfxIndex As Integer = 0 To sfxList.Length - 1
                Dim sfxFileName As String = sfxList(sfxIndex)
                Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs", sfxFileName)
                If fso.FileExists(sfxFilePath) Then
                    'Check for specific formats
                    Dim sfxPlatformPath As String = fso.BuildPath(WorkingDirectory & "\SFXs\" & outPlatform, sfxFileName)
                    If fso.FileExists(sfxPlatformPath) Then
                        sfxFilePath = sfxPlatformPath
                    End If
                    'Read SFX
                    Dim soundToAdd As EXSound = textFileReaders.ReadSFXFileExport(sfxFilePath, outPlatform, samplesToInclude, streamsList, testMode)

                    'Add object to dictionary
                    SfxDictionary.Add(sfxFileName, soundToAdd)
                End If
            Next
        End Sub

        Friend Function GetSFXsArray(soundbankData As SoundbankFile, Optional removeExtension As Boolean = False) As String()
            Dim SfxList As New HashSet(Of String)
            'Iterate over all databases
            For databaseIndex As Integer = 0 To soundbankData.Dependencies.Length - 1
                Dim databaseFilePath As String = fso.BuildPath(WorkingDirectory & "\DataBases\", soundbankData.Dependencies(databaseIndex) & ".txt")
                If fso.FileExists(databaseFilePath) Then
                    Dim databaseFile As DataBaseFile = textFileReaders.ReadDataBaseFile(databaseFilePath)
                    'Read all stored SFXs in this DataBase
                    For sfxIndex As Integer = 0 To databaseFile.Dependencies.Length - 1
                        If removeExtension Then
                            SfxList.Add(databaseFile.Dependencies(sfxIndex))
                        Else
                            SfxList.Add(databaseFile.Dependencies(sfxIndex) & ".txt")
                        End If
                    Next
                End If
            Next

            'Get and Sort Array
            Dim SfxArray As String() = SfxList.ToArray
            Array.Sort(SfxArray)

            Return SfxArray
        End Function

        Friend Sub GetSamplesDictionary(samplesToInclude As HashSet(Of String), SamplesDictionary As Dictionary(Of String, EXAudio), outPlatform As String, outputLanguage As String, CancelSoundBankOutput As Boolean, Optional testMode As Boolean = False)
            Dim SamplesSortedArray As String() = samplesToInclude.ToArray
            Array.Sort(SamplesSortedArray)
            For sampleIndex As Integer = 0 To SamplesSortedArray.Length - 1
                Dim sampleRelPath As String = SamplesSortedArray(sampleIndex)
                'If starts with speech but doesn't match the current language, get the sample with the right language
                If InStr(1, sampleRelPath, "Speech\", CompareMethod.Binary) Then
                    If StrComp(outputLanguage, "English", CompareMethod.Binary) <> 0 Then
                        Dim multiSamplePath As String = Mid(sampleRelPath, Len("Speech\English\") + 1)
                        sampleRelPath = fso.BuildPath("Speech\" & outputLanguage, multiSamplePath)
                    End If
                End If
                SamplesDictionary.Add(RelativePathToAbs(sampleRelPath), GetEXaudio(sampleRelPath, outPlatform, CancelSoundBankOutput, testMode))
                If CancelSoundBankOutput Then
                    Exit For
                End If
            Next

            Erase SamplesSortedArray
            samplesToInclude = Nothing
        End Sub

        Friend Function GetSamplesList(soundBankSFXs As String()) As String()
            'Get Samples
            Dim Samples As New List(Of String)
            For sfxIndex As Integer = 0 To soundBankSFXs.Length - 1
                'Open file
                Dim filePath As String = fso.BuildPath(WorkingDirectory, "SFXs\" & soundBankSFXs(sfxIndex) & ".txt")
                If fso.FileExists(filePath) Then
                    Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(filePath)
                    For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                        Samples.Add(UCase(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master\" & sfxFileData.Samples(sampleIndex).FilePath)))
                    Next
                End If
            Next

            Return Samples.ToArray
        End Function

        Friend Function GetSoundBankSize(soundbankSamples As String(), platformFolder As String, fileExtension As String) As Long
            Dim fileLength As Long = 0
            If fso.FolderExists(platformFolder) Then
                Dim startString As Integer = Len(WorkingDirectory & "\Master\")
                For index As Integer = 0 To soundbankSamples.Length - 1
                    Dim platformFilePath As String = Path.ChangeExtension(fso.BuildPath(platformFolder, Mid(soundbankSamples(index), startString)), fileExtension)
                    If fso.FileExists(platformFilePath) Then
                        If platformFilePath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) Then
                            Dim waveReader As New WaveFileReader(platformFilePath)
                            fileLength += waveReader.Length
                        ElseIf platformFilePath.EndsWith(".aif", StringComparison.OrdinalIgnoreCase) Then
                            Dim aiffReader As New AiffFileReader(platformFilePath)
                            fileLength += aiffReader.Length
                        Else
                            fileLength += FileLen(platformFilePath)
                        End If
                    End If
                Next
            End If
            Return fileLength
        End Function

        Friend Function GetEXaudio(relativeSampleFilePath As String, outputPlatform As String, ByRef CancelSoundBankOutput As Boolean, testMode As Boolean) As EXAudio
            Dim waveFunctions As New WaveFunctions
            Dim newAudioObj As New EXAudio
            If testMode Then
                Using masterWaveReader As New WaveFileReader(fso.BuildPath(WorkingDirectory & "\Master", relativeSampleFilePath))
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
                Dim masterWaveFilePath As String = fso.BuildPath(WorkingDirectory & "\Master", relativeSampleFilePath)
                If fso.FileExists(masterWaveFilePath) Then
                    Using masterWaveReader As New WaveFileReader(masterWaveFilePath)
                        Dim loopInfo As Integer() = waveFunctions.ReadSampleChunk(masterWaveReader)
                        'Get address and flags
                        newAudioObj.Flags = loopInfo(0)
                        'Get Platform Wave Ffile
                        Dim platformWave As String = fso.BuildPath(WorkingDirectory & "\" & outputPlatform, relativeSampleFilePath)
                        If fso.FileExists(platformWave) Then
                            Using platformWaveReader As New WaveFileReader(platformWave)
                                'Common info
                                newAudioObj.Frequency = platformWaveReader.WaveFormat.SampleRate
                                newAudioObj.NumberOfChannels = platformWaveReader.WaveFormat.Channels
                                newAudioObj.Bits = 4
                                newAudioObj.FilePath = relativeSampleFilePath
                                newAudioObj.Duration = platformWaveReader.TotalTime.TotalMilliseconds
                                'Specific formats
                                If StrComp(outputPlatform, "PC") = 0 Then
                                    newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(platformWaveReader.Length, 4) - 1) {}
                                    platformWaveReader.Read(newAudioObj.SampleData, 0, platformWaveReader.Length)
                                    'Get Real length
                                    newAudioObj.RealSize = platformWaveReader.Length
                                    'Loop offset
                                    If loopInfo(0) = 1 Then
                                        newAudioObj.LoopOffset = ESUtils.BytesFunctions.AlignNumber(ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterWaveReader.WaveFormat.SampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1) * 2), 2)
                                    End If
                                ElseIf StrComp(outputPlatform, "PlayStation2") = 0 Then
                                    Dim vagFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\PlayStation2_VAG", relativeSampleFilePath), ".vag")
                                    If fso.FileExists(vagFilePath) Then
                                        Dim vagFile As Byte() = File.ReadAllBytes(vagFilePath)
                                        'Get wave block data aligned
                                        newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(vagFile.Length, 64) - 1) {}
                                        Buffer.BlockCopy(vagFile, 0, newAudioObj.SampleData, 0, vagFile.Length)
                                        'Get Real length
                                        newAudioObj.RealSize = vagFile.Length
                                        'Loop offset
                                        If loopInfo(0) = 1 Then
                                            newAudioObj.LoopOffset = Math.Round(ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterWaveReader.WaveFormat.SampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1) * 2))
                                        End If
                                    Else
                                        MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & vagFilePath, vbOKOnly + vbCritical, "EuroSound")
                                        CancelSoundBankOutput = True
                                    End If
                                ElseIf StrComp(outputPlatform, "GameCube") = 0 Then
                                    Dim dspFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\GameCube_dsp_adpcm", relativeSampleFilePath), ".dsp")
                                    If fso.FileExists(dspFilePath) Then
                                        Dim dspFile As Byte() = File.ReadAllBytes(dspFilePath)
                                        'Get wave block data aligned
                                        newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(dspFile.Length, 32) - 1) {}
                                        Buffer.BlockCopy(dspFile, 0, newAudioObj.SampleData, 0, dspFile.Length)
                                        newAudioObj.DspHeaderData = File.ReadAllBytes(Path.ChangeExtension(dspFilePath, ".dsph"))
                                        'Get Real length
                                        newAudioObj.RealSize = dspFile.Length
                                        'Loop offset
                                        If loopInfo(0) = 1 Then
                                            newAudioObj.LoopOffset = Math.Round(ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterWaveReader.WaveFormat.SampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1) * 2))
                                        End If
                                    Else
                                        MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & dspFilePath, vbOKOnly + vbCritical, "EuroSound")
                                        CancelSoundBankOutput = True
                                    End If
                                ElseIf StrComp(outputPlatform, "X Box") = 0 Or StrComp(outputPlatform, "Xbox") = 0 Then
                                    Dim xboxFilePath As String = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\XBox_adpcm", relativeSampleFilePath), ".adpcm")
                                    If fso.FileExists(xboxFilePath) Then
                                        newAudioObj.SampleData = File.ReadAllBytes(xboxFilePath)
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
                                End If
                            End Using
                        Else
                            MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK" & vbCrLf & platformWave, vbOKOnly + vbCritical, "EuroSound")
                            CancelSoundBankOutput = True
                        End If
                    End Using
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

        Friend Function RelativePathToAbs(sampleRelPath As String) As String
            'Ensure that the string starts with "\"
            Dim relativeSampleFilePath As String = sampleRelPath
            Dim absrelativeSampleFilePath As String = relativeSampleFilePath
            If Not relativeSampleFilePath.StartsWith("\") Then
                absrelativeSampleFilePath = "\" & relativeSampleFilePath
            End If
            Return absrelativeSampleFilePath
        End Function

        Public Function GetTotalCents(sfxFileToCheck As EXSound, outputPlatform As String) As Short
            Dim totalCents As Short = 0
            For sampleIndex As Integer = 0 To sfxFileToCheck.Samples.Count - 1
                Dim currentSample As EXSample = sfxFileToCheck.Samples(sampleIndex)
                Dim sampleFilePath As String = fso.BuildPath(WorkingDirectory & "\" & outputPlatform, currentSample.FilePath)
                If fso.FileExists(sampleFilePath) Then
                    Using reader As New WaveFileReader(sampleFilePath)
                        Dim cents = reader.TotalTime.TotalMilliseconds / 10
                        totalCents += cents
                    End Using
                End If
            Next
            Return totalCents
        End Function

        '*===============================================================================================
        '* FUNCTIONS TO WRITE FILES
        '*===============================================================================================
        Friend Sub WriteSfxFile(binWriter As BinaryWriter, hashCodesList As SortedDictionary(Of String, UInteger), sfxDictionary As Dictionary(Of String, EXSound), samplesDictionary As Dictionary(Of String, EXAudio), streamsList As String(), isBigEndian As Boolean)
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
                            fileRef = hashCodesList(GetOnlyFileName(currentSample.FilePath.TrimStart("\"c)))
                        Else
                            fileRef = 0
                        End If
                    Else
                        Dim streamFileIndex As Integer = Array.IndexOf(streamsList, currentSample.FilePath)
                        If streamFileIndex = -1 Then
                            fileRef = Array.IndexOf(samplesList, currentSample.FilePath)
                        Else
                            fileRef = (streamFileIndex + 1) * -1
                            PrintLine(1, fileRef & "    " & currentSample.FilePath)
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

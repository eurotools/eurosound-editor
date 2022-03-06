Imports System.IO
Imports NAudio.Wave

Partial Public Class ExporterForm
    '*===============================================================================================
    '* PRIVATE METHODS TO GET SOUNDBANK LISTS DATA
    '*===============================================================================================
    Private Sub GetSfxList(soundbankData As SoundbankFile, outPlatform As String, outLanguage As String, SfxDictionary As SortedDictionary(Of String, EXSound), samplesToInclude As HashSet(Of String), streamsList As String())
        'Iterate over all databases
        For databaseIndex As Integer = 0 To soundbankData.Dependencies.Length - 1
            Dim databaseFilePath As String = fso.BuildPath(WorkingDirectory & "\DataBases\", soundbankData.Dependencies(databaseIndex) & ".txt")
            If fso.FileExists(databaseFilePath) Then
                Dim databaseFile As DataBaseFile = textFileReaders.ReadDataBaseFile(databaseFilePath)
                'Read all stored SFXs in this DataBase
                For sfxIndex As Integer = 0 To databaseFile.Dependencies.Length - 1
                    Dim sfxFileName As String = databaseFile.Dependencies(sfxIndex) & ".txt"
                    Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs", sfxFileName)
                    If fso.FileExists(sfxFilePath) Then
                        If Not SfxDictionary.ContainsKey(sfxFileName) Then
                            'Check for specific formats
                            Dim sfxPlatformPath As String = fso.BuildPath(WorkingDirectory & "\SFXs\" & outPlatform, sfxFileName)
                            If fso.FileExists(sfxPlatformPath) Then
                                sfxFilePath = sfxPlatformPath
                            End If
                            'Read SFX
                            Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(sfxFilePath)
                            Dim soundToAdd As New EXSound With {
                                .HashCode = sfxFileData.HashCode,
                                .Ducker = sfxFileData.Parameters.Ducker,
                                .DuckerLength = sfxFileData.Parameters.DuckerLenght,
                                .Flags = GetSfxFlags(sfxFileData),
                                .InnerRadius = sfxFileData.Parameters.InnerRadius,
                                .MasterVolume = sfxFileData.Parameters.MasterVolume,
                                .MaxDelay = sfxFileData.SamplePool.MaxDelay,
                                .MaxVoices = sfxFileData.Parameters.MaxVoices,
                                .MinDelay = sfxFileData.SamplePool.MinDelay,
                                .OuterRadius = sfxFileData.Parameters.OuterRadius,
                                .Priority = sfxFileData.Parameters.Priority,
                                .ReverbSend = sfxFileData.Parameters.ReverbSend,
                                .TrackingType = sfxFileData.Parameters.TrackingType,
                                .HasSubSfx = sfxFileData.SamplePool.EnableSubSFX
                            }
                            For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                                Dim currentSample As Sample = sfxFileData.Samples(sampleIndex)
                                Dim sampleToAdd As New EXSample With {
                                    .FilePath = RelativePathToAbs(currentSample.FilePath),
                                    .PitchOffset = currentSample.PitchOffset * 1024,
                                    .RandomPitchOffset = currentSample.RandomPitchOffset * 1024,
                                    .BaseVolume = currentSample.BaseVolume,
                                    .RandomVolumeOffset = currentSample.RandomVolumeOffset,
                                    .Pan = currentSample.Pan,
                                    .RandomPan = currentSample.RandomPan
                                }
                                If Not sfxFileData.SamplePool.EnableSubSFX Then
                                    'Check if this sample is streamed or not
                                    Dim arraySearchResult As Integer = Array.IndexOf(streamsList, sampleToAdd.FilePath)
                                    If arraySearchResult = -1 Then
                                        samplesToInclude.Add(currentSample.FilePath)
                                    End If
                                End If
                                'Add new sample to the dictionary
                                soundToAdd.Samples.Add(sampleToAdd)
                            Next
                            'Calculate Ducker Length
                            Dim duckerLength As Short = 0
                            If soundToAdd.Ducker > 0 Then
                                duckerLength = GetTotalCents(soundToAdd, outPlatform)
                                If soundToAdd.DuckerLength < 0 Then
                                    duckerLength -= Math.Abs(soundToAdd.DuckerLength)
                                Else
                                    duckerLength += Math.Abs(soundToAdd.DuckerLength)
                                End If
                            End If
                            soundToAdd.DuckerLength = duckerLength
                            'Add object to dictionary
                            SfxDictionary.Add(sfxFileName, soundToAdd)
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub GetSamplesDictionary(samplesToInclude As HashSet(Of String), SamplesDictionary As Dictionary(Of String, EXAudio), outPlatform As String)
        Dim SamplesSortedArray As String() = samplesToInclude.ToArray
        Array.Sort(SamplesSortedArray)

        For sampleIndex As Integer = 0 To SamplesSortedArray.Length - 1
            Dim sampleRelPath As String = SamplesSortedArray(sampleIndex)
            SamplesDictionary.Add(RelativePathToAbs(sampleRelPath), GetEXaudio(sampleRelPath, outPlatform))
        Next

        Erase SamplesSortedArray
        samplesToInclude = Nothing
    End Sub

    Private Function GetEXaudio(relativeSampleFilePath As String, outputPlatform As String) As EXAudio
        Dim waveFunctions As New WaveFunctions
        Dim newAudioObj As New EXAudio
        'Get loop info
        Using masterWaveReader As New WaveFileReader(fso.BuildPath(WorkingDirectory & "\Master", relativeSampleFilePath))
            Dim loopInfo As Integer() = waveFunctions.ReadSampleChunk(masterWaveReader)
            'Get address and flags
            newAudioObj.Flags = loopInfo(0)
            newAudioObj.Address = 5000
            'Get Platform Wave Ffile
            Dim platformWave As String = fso.BuildPath(WorkingDirectory & "\" & outputPlatform, relativeSampleFilePath)
            Using platformWaveReader As New WaveFileReader(platformWave)
                'Common info
                newAudioObj.Frequency = platformWaveReader.WaveFormat.SampleRate
                newAudioObj.NumberOfChannels = platformWaveReader.WaveFormat.Channels
                newAudioObj.Bits = 4
                newAudioObj.FilePath = relativeSampleFilePath
                newAudioObj.Duration = Math.Floor(platformWaveReader.TotalTime.TotalMilliseconds)
                If loopInfo(0) = 1 Then
                    newAudioObj.LoopOffset = ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterWaveReader.WaveFormat.SampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1))
                End If
                'Specific formats
                If StrComp(outputPlatform, "PC") = 0 Then
                    newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(platformWaveReader.Length, 4) - 1) {}
                    platformWaveReader.Read(newAudioObj.SampleData, 0, platformWaveReader.Length)
                    'Get Real length
                    newAudioObj.RealSize = platformWaveReader.Length
                ElseIf StrComp(outputPlatform, "PlayStation2") = 0 Then
                    Dim vagFilePath As String = fso.BuildPath(WorkingDirectory & "\PlayStation2_VAG", relativeSampleFilePath)
                    Dim vagFile As Byte() = File.ReadAllBytes(Path.ChangeExtension(vagFilePath, ".vag"))
                    'Get wave block data aligned
                    newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(vagFile.Length, 64) - 1) {}
                    Buffer.BlockCopy(vagFile, 0, newAudioObj.SampleData, 0, vagFile.Length)
                    'Get Real length
                    newAudioObj.RealSize = vagFile.Length
                ElseIf StrComp(outputPlatform, "GameCube") = 0 Then
                    Dim dspFilePath As String = fso.BuildPath(WorkingDirectory & "\GameCube_dsp_adpcm", relativeSampleFilePath)
                    Dim dspFile As Byte() = File.ReadAllBytes(Path.ChangeExtension(dspFilePath, ".dsp"))
                    'Get wave block data aligned
                    newAudioObj.SampleData = New Byte(ESUtils.BytesFunctions.AlignNumber(dspFile.Length, 32) - 1) {}
                    Buffer.BlockCopy(dspFile, 0, newAudioObj.SampleData, 0, dspFile.Length)
                    newAudioObj.DspHeaderData = File.ReadAllBytes(Path.ChangeExtension(dspFilePath, ".dsph"))
                    'Get Real length
                    newAudioObj.RealSize = dspFile.Length
                ElseIf StrComp(outputPlatform, "X Box") = 0 Or StrComp(outputPlatform, "Xbox") = 0 Then
                    Dim xboxFilePath As String = fso.BuildPath(WorkingDirectory & "\XBox_adpcm", relativeSampleFilePath)
                    newAudioObj.SampleData = File.ReadAllBytes(Path.ChangeExtension(xboxFilePath, ".adpcm"))
                    'Get Real length
                    newAudioObj.RealSize = newAudioObj.SampleData.Length
                End If
            End Using
        End Using
        Return newAudioObj
    End Function

    Private Function GetSfxFlags(sfxFileToCheck As SfxFile) As Short
        'Get Flags
        Dim selectedFlags As Short = 0
        'maxReject
        If sfxFileToCheck.Parameters.Action1 = 1 Then
            selectedFlags = selectedFlags Or (1 << 0)
        End If
        'ignoreAge
        If sfxFileToCheck.Parameters.IgnoreAge Then
            selectedFlags = selectedFlags Or (1 << 2)
        End If
        'multiSample
        If sfxFileToCheck.SamplePool.Action1 = 1 Then
            selectedFlags = selectedFlags Or (1 << 3)
        End If
        'randomPick
        If sfxFileToCheck.SamplePool.RandomPick Then
            selectedFlags = selectedFlags Or (1 << 4)
        End If
        'shuffled
        If sfxFileToCheck.SamplePool.Shuffled Then
            selectedFlags = selectedFlags Or (1 << 5)
        End If
        'loop
        If sfxFileToCheck.SamplePool.isLooped Then
            selectedFlags = selectedFlags Or (1 << 6)
        End If
        'polyphonic
        If sfxFileToCheck.SamplePool.Polyphonic Then
            selectedFlags = selectedFlags Or (1 << 7)
        End If
        'underWater
        If sfxFileToCheck.Parameters.Outdoors Then
            selectedFlags = selectedFlags Or (1 << 8)
        End If
        'pauseInNis
        If sfxFileToCheck.Parameters.PauseInNis Then
            selectedFlags = selectedFlags Or (1 << 9)
        End If
        'hasSubSfx
        If sfxFileToCheck.SamplePool.EnableSubSFX Then
            selectedFlags = selectedFlags Or (1 << 10)
        End If
        'stealOnLouder
        If sfxFileToCheck.Parameters.StealOnAge Then
            selectedFlags = selectedFlags Or (1 << 11)
        End If
        'treatLikeMusic
        If sfxFileToCheck.Parameters.MusicType Then
            selectedFlags = selectedFlags Or (1 << 12)
        End If
        Return selectedFlags
    End Function

    Private Function RelativePathToAbs(sampleRelPath As String) As String
        'Ensure that the string starts with "\"
        Dim relativeSampleFilePath As String = sampleRelPath
        Dim absrelativeSampleFilePath As String = relativeSampleFilePath
        If Not relativeSampleFilePath.StartsWith("\") Then
            absrelativeSampleFilePath = "\" & relativeSampleFilePath
        End If
        Return absrelativeSampleFilePath
    End Function

    Private Function GetTotalCents(sfxFileToCheck As EXSound, outputPlatform As String) As Short
        Dim totalCents As Short = 0
        For sampleIndex As Integer = 0 To sfxFileToCheck.Samples.Count - 1
            Dim currentSample As EXSample = sfxFileToCheck.Samples(sampleIndex)
            Dim sampleFilePath As String = fso.BuildPath(WorkingDirectory & "\" & outputPlatform, currentSample.FilePath)
            Using reader As New WaveFileReader(sampleFilePath)
                Dim cents = reader.TotalTime.TotalMilliseconds / 10
                totalCents += cents
            End Using
        Next
        Return totalCents
    End Function
End Class

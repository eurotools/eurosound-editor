Imports System.IO
Imports NAudio.Wave

Partial Public Class ExporterForm
    Public Sub OutputSoundbanks(soundbanksList As String(), streamsList As String(), outLanguages As String(), outPlatforms As String())
        'Debug Folder
        Dim debugFolder As String = fso.BuildPath(WorkingDirectory, "Debug_Report")
        CreateFolderIfRequired(debugFolder)

        'Reset progress bar
        Invoke(Sub() ProgressBar1.Value = 0)
        'For each Language
        For languageIndex As Integer = 0 To outLanguages.Length - 1
            Dim currentLanguage As String = outLanguages(languageIndex)
            'For each Platform
            For platformIndex As Integer = 0 To outPlatforms.Length - 1
                Dim currentPlatform As String = outPlatforms(platformIndex)
                'For each Soundbank
                For soundBankIndex As Integer = 0 To soundbanksList.Length - 1
                    Dim currentSoundBank As String = soundbanksList(soundBankIndex)
                    'Calculate and report progress
                    BackgroundWorker.ReportProgress(Decimal.Divide(soundBankIndex, soundbanksList.Length) * 100.0)
                    'Soundbank file path
                    Invoke(Sub() Text = "Outputting " & currentLanguage & " SoundBank " & currentSoundBank & " for " & currentPlatform)
                    Dim soundbankFilePath As String = fso.BuildPath(WorkingDirectory & "\SoundBanks", currentSoundBank & ".txt")
                    If fso.FileExists(soundbankFilePath) Then
                        Dim soundBankInfo As SoundbankFile = textFileReaders.ReadSoundBankFile(soundbankFilePath)
                        Dim soundBankSFXs As String() = GetSfxList(soundBankInfo, currentPlatform)
                        Dim soundBankSamples As String() = GetSamplesList(soundBankSFXs, streamsList)
                        'Get a collection of all stored SFXs
                        Dim sfxToOutput As SfxFile() = New SfxFile(soundBankSFXs.Length - 1) {}
                        For sfxIndex As Integer = 0 To soundBankSFXs.Length - 1
                            Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(soundBankSFXs(sfxIndex))
                            sfxToOutput(sfxIndex) = sfxFileData
                        Next
                        'Get output directory
                        Dim outputFolder As String = fso.BuildPath(WorkingDirectory, "TempOutputFolder\" & currentPlatform & "\SoundBanks\" & currentLanguage)
                        CreateFolderIfRequired(outputFolder)
                        'Check if the destination platfom uses big endian
                        Dim bigEndianPlatform = False
                        If StrComp(currentPlatform, "GameCube") = 0 Then
                            bigEndianPlatform = True
                        End If
                        'Sfx File
                        Using sampleInfoWriter As New MemoryStream()
                            Using sifWriter As New BinaryWriter(sampleInfoWriter)
                                Using sbfWriter As New BinaryWriter(File.Open(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sbf"), FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.ASCII)
                                    Using sfxBinWriter As New BinaryWriter(File.Open(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sfx"), FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.ASCII)
                                        CreateSbfFile(sbfWriter, sifWriter, soundBankSamples, currentPlatform, bigEndianPlatform)
                                        File.WriteAllBytes(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sif"), sampleInfoWriter.ToArray)
                                        CreateSfxFile(sfxBinWriter, sfxToOutput, bigEndianPlatform)
                                    End Using
                                End Using
                            End Using
                        End Using
                    End If
                Next
            Next
        Next
    End Sub

    '*===============================================================================================
    '* PRIVATE METHODS TO GET SOUNDBANK LISTS DATA
    '*===============================================================================================
    Private Function GetSfxList(soundbankData As SoundbankFile, outputPlatform As String) As String()
        Dim sfxHashSet As New HashSet(Of String)
        'Iterate over all databases
        For databaseIndex As Integer = 0 To soundbankData.Dependencies.Length - 1
            Dim databaseFilePath As String = fso.BuildPath(WorkingDirectory & "\DataBases\", soundbankData.Dependencies(databaseIndex) & ".txt")
            If fso.FileExists(databaseFilePath) Then
                Dim databaseFile As DataBaseFile = textFileReaders.ReadDataBaseFile(databaseFilePath)
                For sfxIndex As Integer = 0 To databaseFile.Dependencies.Length - 1
                    Dim sfxFileName As String = databaseFile.Dependencies(sfxIndex) & ".txt"
                    Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs", sfxFileName)
                    If fso.FileExists(sfxFilePath) Then
                        'Check for specific formats
                        Dim sfxPlatformPath As String = fso.BuildPath(WorkingDirectory & "\SFXs\" & outputPlatform, sfxFileName)
                        If fso.FileExists(sfxPlatformPath) Then
                            sfxFilePath = sfxPlatformPath
                        End If
                        sfxHashSet.Add(sfxFilePath)
                    End If
                Next
            End If
        Next

        'Create an array with the included SFXs
        Dim soundbankSfx As String() = sfxHashSet.ToArray
        Array.Sort(soundbankSfx)

        Return soundbankSfx
    End Function

    Private Function GetSamplesList(sfxList As String(), streamsList As String()) As String()
        Dim samplesHashSet As New HashSet(Of String)
        For sfxIndex As Integer = 0 To sfxList.Length - 1
            If fso.FileExists(sfxList(sfxIndex)) Then
                'Read file data and get a list with the included samples
                Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(sfxList(sfxIndex))
                For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                    Dim relativeSampleFilePath As String = sfxFileData.Samples(sampleIndex).FilePath
                    'Check if this sample is streamed
                    Dim absrelativeSampleFilePath As String = relativeSampleFilePath
                    If Not relativeSampleFilePath.StartsWith("\") Then
                        absrelativeSampleFilePath = "\" & relativeSampleFilePath
                    End If
                    'Add sample to the output list if is not streamed
                    If Array.IndexOf(streamsList, absrelativeSampleFilePath) = -1 Then
                        Dim sampleFilePath As String = WorkingDirectory & "\Master\" & relativeSampleFilePath
                        If fso.FileExists(sampleFilePath) Then
                            samplesHashSet.Add(sampleFilePath)
                        End If
                    End If
                Next
            End If
        Next

        'Create an array with the included SFXs
        Dim soundbankSfx As String() = samplesHashSet.ToArray
        Array.Sort(soundbankSfx)

        Return soundbankSfx
    End Function

    '*===============================================================================================
    '* CREATE TEMPORAL FILES
    '*===============================================================================================
    Private Sub CreateSfxFile(binWriter As BinaryWriter, sfxToOutput As SfxFile(), isBigEndian As Boolean)
        'Create binary file

        Dim sfxOffsets As Integer() = New Integer(sfxToOutput.Length - 1) {}
        'SFXs Count
        binWriter.Write(CUInt(sfxToOutput.Length))
        'SFX header
        For sfxIndex As Integer = 0 To sfxToOutput.Length - 1
            Dim currentSfx As SfxFile = sfxToOutput(sfxIndex)
            binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(currentSfx.HashCode, isBigEndian))
            binWriter.Write(0)
        Next
        'SFX parameter entry
        For sfxIndex As Integer = 0 To sfxToOutput.Length - 1
            sfxOffsets(sfxIndex) = binWriter.BaseStream.Position
            'Print parameters and sample pool info
            Dim currentSfx As SfxFile = sfxToOutput(sfxIndex)
            binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSfx.Parameters.DuckerLenght, isBigEndian))
            binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSfx.SamplePool.MinDelay, isBigEndian))
            binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSfx.SamplePool.MaxDelay, isBigEndian))
            binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSfx.Parameters.InnerRadius, isBigEndian))
            binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSfx.Parameters.OuterRadius, isBigEndian))
            binWriter.Write(CSByte(currentSfx.Parameters.ReverbSend))
            binWriter.Write(CSByte(currentSfx.Parameters.TrackingType))
            binWriter.Write(CSByte(currentSfx.Parameters.MaxVoices))
            binWriter.Write(CSByte(currentSfx.Parameters.Priority))
            binWriter.Write(CSByte(currentSfx.Parameters.Ducker))
            binWriter.Write(CSByte(currentSfx.Parameters.MasterVolume))
            binWriter.Write(GetSfxFlags(currentSfx))

            'Write samples
            binWriter.Write(ESUtils.BytesFunctions.FlipUShort(currentSfx.Samples.Count, isBigEndian))
            For sampleIndex As Integer = 0 To currentSfx.Samples.Count - 1
                Dim currentSample As Sample = currentSfx.Samples(sampleIndex)
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(-1, isBigEndian)) 'File Ref
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSample.PitchOffset * 1024, isBigEndian))
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(currentSample.RandomPitchOffset * 1024, isBigEndian))
                binWriter.Write(currentSample.BaseVolume)
                binWriter.Write(currentSample.RandomVolumeOffset)
                binWriter.Write(currentSample.Pan)
                binWriter.Write(currentSample.RandomPan)
                binWriter.Write(CByte(0)) 'Padding
                binWriter.Write(CByte(0)) 'Padding
            Next
        Next

        'Print offsets
        binWriter.BaseStream.Seek(8, SeekOrigin.Begin)
        For offsetIndex As Integer = 0 To sfxOffsets.Count - 1
            binWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sfxOffsets(offsetIndex), isBigEndian))
            binWriter.BaseStream.Seek(4, SeekOrigin.Current)
        Next
        Erase sfxOffsets
    End Sub

    Private Function GetSfxFlags(sfxFileToCheck As SfxFile) As Short
        'Get Flags
        Dim selectedFlags As Short = 0
        'maxReject
        If sfxFileToCheck.Parameters.Action1 = 1 Then
            selectedFlags = selectedFlags Or 1 << 0
        End If
        'nextFreeOneToUse
        selectedFlags = selectedFlags Or 0 << 1
        'ignoreAge
        If sfxFileToCheck.Parameters.IgnoreAge Then
            selectedFlags = selectedFlags Or 1 << 2
        End If
        'multiSample
        If sfxFileToCheck.SamplePool.Action1 = 1 Then
            selectedFlags = selectedFlags Or 1 << 3
        End If
        'randomPick
        If sfxFileToCheck.SamplePool.RandomPick Then
            selectedFlags = selectedFlags Or 1 << 4
        End If
        'shuffled
        If sfxFileToCheck.SamplePool.Shuffled Then
            selectedFlags = selectedFlags Or 1 << 5
        End If
        'shuffled
        If sfxFileToCheck.SamplePool.isLooped Then
            selectedFlags = selectedFlags Or 1 << 6
        End If
        'shuffled
        If sfxFileToCheck.SamplePool.Polyphonic Then
            selectedFlags = selectedFlags Or 1 << 7
        End If
        'underWater
        If sfxFileToCheck.Parameters.Outdoors Then
            selectedFlags = selectedFlags Or 1 << 8
        End If
        'pauseInNis
        If sfxFileToCheck.Parameters.PauseInNis Then
            selectedFlags = selectedFlags Or 1 << 9
        End If
        'hasSubSfx
        If sfxFileToCheck.SamplePool.EnableSubSFX Then
            selectedFlags = selectedFlags Or 1 << 10
        End If
        'stealOnLouder
        If sfxFileToCheck.Parameters.StealOnAge Then
            selectedFlags = selectedFlags Or 1 << 11
        End If
        'treatLikeMusic
        If sfxFileToCheck.Parameters.MusicType Then
            selectedFlags = selectedFlags Or 1 << 12
        End If
        Return selectedFlags
    End Function

    Private Sub CreateSbfFile(sbfWriter As BinaryWriter, sifWriter As BinaryWriter, samplesList As String(), outputPlatform As String, isBigEndian As Boolean)
        Dim waveFunctions As New WaveFunctions
        'Create binary file
        sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(samplesList.Length - 1, isBigEndian))
        For sampleIndex As Integer = 0 To samplesList.Length - 1
            Dim masterSamplePath As String = samplesList(sampleIndex)
            'Get loop info
            Dim loopInfo As Integer()
            Using masterWaveReader As New WaveFileReader(masterSamplePath)
                loopInfo = waveFunctions.ReadSampleChunk(masterWaveReader)
                'SIF FILE - Sample header flags and Address
                sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(loopInfo(0), isBigEndian))
                sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sbfWriter.BaseStream.Position, isBigEndian))
                'Get Platform Wave Ffile
                Dim loopOffset As UInteger = 0
                Dim platformWave As String = fso.BuildPath(WorkingDirectory & "\" & outputPlatform, Mid(masterSamplePath, Len(WorkingDirectory & "\Master\") + 1))
                Using platformWaveReader As New WaveFileReader(platformWave)
                    Dim sampleDataDma As Byte() = New Byte() {}
                    If StrComp(outputPlatform, "PC") = 0 Then
                        'Get wave block data aligned
                        Dim sampleSizeDma As Integer = ESUtils.BytesFunctions.AlignNumber(platformWaveReader.Length, 4)
                        sampleDataDma = New Byte(sampleSizeDma - 1) {}
                        platformWaveReader.Read(sampleDataDma, 0, platformWaveReader.Length)
                        'Write block
                        sbfWriter.Write(sampleDataDma)
                        'Calculate Loop Offset 
                        If loopInfo(0) = 1 Then
                            loopOffset = ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterWaveReader.WaveFormat.SampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1))
                        End If
                    End If
                    'SIF FILE - Size of the raw PCM, Frequency, Real size
                    sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sampleDataDma.Length, isBigEndian))
                    sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(platformWaveReader.WaveFormat.SampleRate, isBigEndian))
                    sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(platformWaveReader.Length, isBigEndian))
                End Using
                'SIF FILE - Number of channels, Bits per sample, PSI sample header, Loop offset, Duration
                sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(masterWaveReader.WaveFormat.Channels, isBigEndian))
                sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(4, isBigEndian))
                sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sampleIndex * 96, isBigEndian))
                sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(loopOffset, isBigEndian))
                sifWriter.Write(CUInt(masterWaveReader.TotalTime.Milliseconds))
            End Using
        Next
    End Sub
End Class

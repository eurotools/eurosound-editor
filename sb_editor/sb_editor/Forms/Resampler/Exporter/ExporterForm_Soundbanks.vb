Imports System.IO
Imports NAudio.Wave

Partial Public Class ExporterForm
    Public Sub OutputSoundbanks(soundbanksList As String(), streamsList As String(), outLanguages As String(), outPlatforms As String())
        'Debug Folder
        Dim debugFolder As String = fso.BuildPath(WorkingDirectory, "Debug_Report")
        CreateFolderIfRequired(debugFolder)

        'Reset progress bar
        Invoke(Sub() ProgressBar1.Value = 0)

        'For each Soundbank
        For soundBankIndex As Integer = 0 To soundbanksList.Length - 1
            'Calculate and report progress
            BackgroundWorker.ReportProgress(Decimal.Divide(soundBankIndex, soundbanksList.Length) * 100.0)
            Dim currentSoundBank As String = soundbanksList(soundBankIndex)
            Dim soundbankFilePath As String = fso.BuildPath(WorkingDirectory & "\SoundBanks", currentSoundBank & ".txt")
            'Read soundbank file
            If fso.FileExists(soundbankFilePath) Then
                Dim soundBankInfo As SoundbankFile = textFileReaders.ReadSoundBankFile(soundbankFilePath)
                'For each Language
                For languageIndex As Integer = 0 To outLanguages.Length - 1
                    Dim currentLanguage As String = outLanguages(languageIndex)
                    'For each Platform
                    For platformIndex As Integer = 0 To outPlatforms.Length - 1
                        'Get current platform and update title bar
                        Dim currentPlatform As String = outPlatforms(platformIndex)
                        Invoke(Sub() Text = "Outputting " & currentLanguage & " SoundBank " & currentSoundBank & " for " & currentPlatform)

                        'Get SFX and samples list
                        Dim soundBankSFXs As String() = GetSfxList(soundBankInfo, currentPlatform)
                        Dim samplesHashSet As New HashSet(Of String)
                        'Get a collection of all stored SFXs and samples
                        Dim sfxToOutput As SfxFile() = New SfxFile(soundBankSFXs.Length - 1) {}
                        For sfxIndex As Integer = 0 To soundBankSFXs.Length - 1
                            Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(soundBankSFXs(sfxIndex))
                            sfxToOutput(sfxIndex) = sfxFileData
                            GetSamplesList(sfxFileData, streamsList, samplesHashSet)
                        Next
                        'Create an array with the included SFXs
                        Dim soundBankSamples As String() = samplesHashSet.ToArray
                        Array.Sort(soundBankSamples)

                        'Get output directory
                        Dim outputFolder As String = fso.BuildPath(WorkingDirectory, "TempOutputFolder\" & currentPlatform & "\SoundBanks\" & currentLanguage)
                        CreateFolderIfRequired(outputFolder)

                        'Dictionary to store samples
                        Dim samplesIndexes As New Dictionary(Of String, UShort)

                        'Check if the destination platfom uses big endian
                        If StrComp(currentPlatform, "GameCube") = 0 Then
                            Using sampleInfoWriter As New MemoryStream()
                                Using sifWriter As New BinaryWriter(sampleInfoWriter)
                                    Using sbfWriter As New BinaryWriter(File.Open(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sbf"), FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.ASCII)
                                        Using sfxBinWriter As New BinaryWriter(File.Open(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sfx"), FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.ASCII)
                                            Using ssfBinWriter As New BinaryWriter(File.Open(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".ssf"), FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.ASCII)
                                                CreateSbfFile(sbfWriter, sifWriter, ssfBinWriter, samplesIndexes, soundBankSamples, currentPlatform, True)
                                                File.WriteAllBytes(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sif"), sampleInfoWriter.ToArray)
                                                CreateSfxFile(sfxBinWriter, sfxToOutput, samplesIndexes, streamsList, True)
                                            End Using
                                        End Using
                                    End Using
                                End Using
                            End Using
                        Else
                            Using sampleInfoWriter As New MemoryStream()
                                Using sifWriter As New BinaryWriter(sampleInfoWriter)
                                    Using sbfWriter As New BinaryWriter(File.Open(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sbf"), FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.ASCII)
                                        Using sfxBinWriter As New BinaryWriter(File.Open(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sfx"), FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.ASCII)
                                            CreateSbfFile(sbfWriter, sifWriter, Nothing, samplesIndexes, soundBankSamples, currentPlatform, False)
                                            File.WriteAllBytes(fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sif"), sampleInfoWriter.ToArray)
                                            CreateSfxFile(sfxBinWriter, sfxToOutput, samplesIndexes, streamsList, False)
                                        End Using
                                    End Using
                                End Using
                            End Using
                        End If
                    Next
                Next
            End If
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

    Private Sub GetSamplesList(sfxFileData As SfxFile, streamsList As String(), samplesHashSet As HashSet(Of String))
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
                Else
                    If sampleFilePath.EndsWith(".wav") Or sampleFilePath.EndsWith(".dsp") Or sampleFilePath.EndsWith(".vag") Or sampleFilePath.EndsWith(".adpcm") Then
                        Invoke(Sub() MsgBox("Output Error: Sample File Missing: UNKNOWN SFX & BANK " & sampleFilePath, vbOKOnly + vbCritical, "EuroSound"))
                    End If
                End If
            End If
        Next
    End Sub

    '*===============================================================================================
    '* CREATE TEMPORAL FILES
    '*===============================================================================================
    Private Sub CreateSfxFile(binWriter As BinaryWriter, sfxToOutput As SfxFile(), samplesIndexes As Dictionary(Of String, UShort), streamsList As String(), isBigEndian As Boolean)
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
                Dim sampleName As String = Path.GetFileName(currentSample.FilePath)
                Dim sampleFileRef As Integer = -1
                If currentSfx.SamplePool.EnableSubSFX Then
                Else
                    If samplesIndexes.ContainsKey(sampleName) Then
                        sampleFileRef = samplesIndexes(sampleName)
                    Else
                        Dim absSampleFilePath As String = currentSample.FilePath
                        If Not absSampleFilePath.StartsWith("\") Then
                            absSampleFilePath = "\" & currentSample.FilePath
                        End If
                        sampleFileRef = (Array.IndexOf(streamsList, absSampleFilePath) + 1) * -1
                    End If
                End If
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(sampleFileRef, isBigEndian)) 'File Ref
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

    Private Sub CreateSbfFile(sbfWriter As BinaryWriter, sifWriter As BinaryWriter, ssfWriter As BinaryWriter, samplesIndexes As Dictionary(Of String, UShort), samplesList As String(), outputPlatform As String, isBigEndian As Boolean)
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
                Dim startSubstring As Integer = Len(WorkingDirectory & "\Master\") + 1
                Dim platformWave As String = fso.BuildPath(WorkingDirectory & "\" & outputPlatform, Mid(masterSamplePath, startSubstring))
                Using platformWaveReader As New WaveFileReader(platformWave)
                    Dim sampleDataDma As Byte() = New Byte() {}
                    Dim realLength As Integer = 0
                    If StrComp(outputPlatform, "PC") = 0 Then
                        Dim sampleSizeDma As Integer = ESUtils.BytesFunctions.AlignNumber(platformWaveReader.Length, 4)
                        sampleDataDma = New Byte(sampleSizeDma - 1) {}
                        platformWaveReader.Read(sampleDataDma, 0, platformWaveReader.Length)
                        'Write block
                        sbfWriter.Write(sampleDataDma)
                        'Get Real length
                        realLength = platformWaveReader.Length
                    ElseIf StrComp(outputPlatform, "PlayStation2") = 0 Then
                        Dim vagFilePath As String = fso.BuildPath(WorkingDirectory & "\PlayStation2_VAG", Mid(masterSamplePath, startSubstring))
                        Dim vagFile As Byte() = File.ReadAllBytes(Path.ChangeExtension(vagFilePath, ".vag"))
                        'Get wave block data aligned
                        Dim sampleSizeDma As Integer = ESUtils.BytesFunctions.AlignNumber(vagFile.Length, 64)
                        sampleDataDma = New Byte(sampleSizeDma - 1) {}
                        'Write block
                        sbfWriter.Write(sampleDataDma)
                        'Get Real length
                        realLength = vagFile.Length
                    ElseIf StrComp(outputPlatform, "GameCube") = 0 Then
                        Dim dspFilePath As String = fso.BuildPath(WorkingDirectory & "\GameCube_dsp_adpcm", Mid(masterSamplePath, startSubstring))
                        Dim dspFile As Byte() = File.ReadAllBytes(Path.ChangeExtension(dspFilePath, ".dsp"))
                        'Get wave block data aligned
                        Dim sampleSizeDsp As Integer = ESUtils.BytesFunctions.AlignNumber(dspFile.Length, 32)
                        sampleDataDma = New Byte(sampleSizeDsp - 1) {}
                        'Write block
                        sbfWriter.Write(sampleDataDma)
                        'Header data
                        If ssfWriter IsNot Nothing Then
                            Dim dspHeader As Byte() = File.ReadAllBytes(Path.ChangeExtension(dspFilePath, ".dsph"))
                            ssfWriter.Write(dspHeader)
                        End If
                        'Get Real length
                        realLength = dspFile.Length
                    ElseIf StrComp(outputPlatform, "X Box") = 0 Or StrComp(outputPlatform, "Xbox") = 0 Then
                        Dim xboxFilePath As String = fso.BuildPath(WorkingDirectory & "\XBox_adpcm", Mid(masterSamplePath, startSubstring))
                        Dim xboxFile As Byte() = File.ReadAllBytes(Path.ChangeExtension(xboxFilePath, ".adpcm"))
                        'Write block
                        sbfWriter.Write(xboxFile)
                        'Get Real length
                        realLength = xboxFile.Length
                    End If
                    'Calculate Loop Offset 
                    If loopInfo(0) = 1 Then
                        loopOffset = ESUtils.CalculusLoopOffset.RuleOfThreeLoopOffset(masterWaveReader.WaveFormat.SampleRate, platformWaveReader.WaveFormat.SampleRate, loopInfo(1))
                    End If
                    'SIF FILE - Size of the raw PCM, Frequency, Real size
                    sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(sampleDataDma.Length, isBigEndian))
                    sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(platformWaveReader.WaveFormat.SampleRate, isBigEndian))
                    sifWriter.Write(ESUtils.BytesFunctions.FlipUInt32(realLength, isBigEndian))
                    'Store sample and index
                    samplesIndexes.Add(Path.GetFileName(masterSamplePath), sampleIndex)
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

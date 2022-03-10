Imports System.IO
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects

Partial Public Class ExporterForm
    Public Sub OutputSoundbanks(hashCodesList As SortedDictionary(Of String, UInteger), soundbanksList As String(), streamsList As String(), outLanguages As String(), outPlatforms As String())
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

                        'Get output folder
                        Dim outputFolder As String = fso.BuildPath(WorkingDirectory, "TempOutputFolder\" & currentPlatform & "\SoundBanks\" & currentLanguage)
                        CreateFolderIfRequired(outputFolder)

                        'Get SFX and samples list
                        Dim samplesToInclude As New HashSet(Of String)
                        Dim SfxDictionary As New SortedDictionary(Of String, EXSound)
                        Dim SamplesDictionary As New Dictionary(Of String, EXAudio)
                        Dim SfxList As String() = GetSFXsList(soundBankInfo)
                        GetSFXsDictionary(SfxList, currentPlatform, SfxDictionary, samplesToInclude, streamsList)
                        GetSamplesDictionary(samplesToInclude, SamplesDictionary, currentPlatform, currentLanguage)
                        'Skip if there are samples missing
                        If Not CancelSoundBankOutput Then
                            'Get file paths
                            Dim sfxFilePath As String = fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sfx")
                            Dim sifFilePath As String = fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sif")
                            Dim ssfFilePath As String = fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".ssf")
                            Dim sbfFilePath As String = fso.BuildPath(outputFolder, soundBankInfo.HashCode & ".sbf")

                            'Get output file paths
                            Dim sfxFileName As String = "HC" & Hex(GetSfxFileName(Array.IndexOf(SfxLanguages, currentLanguage), soundBankInfo.HashCode)).PadLeft(6, "0"c)
                            Dim outputFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary\" & GetEngineXFolder(currentPlatform) & "\" & GetEngineXLangFolder(currentLanguage))
                            CreateFolderIfRequired(outputFilePath)

                            'Debug file
                            Dim debugFile As String = fso.BuildPath(WorkingDirectory, "Debug_Report")
                            CreateFolderIfRequired(debugFile)
                            FileOpen(1, fso.BuildPath(debugFile, "StreamDebugSoundBank_" & currentSoundBank & "_" & currentPlatform & "_" & currentLanguage & ".txt"), OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                            PrintLine(1, "SoundBank Output Debug Data")
                            PrintLine(1, Format(Now, "dd/mm/yyy"))
                            PrintLine(1, Format(Now, "hh:mm:ss"))
                            PrintLine(1, "")
                            PrintLine(1, "SoundBankName = " & currentSoundBank)
                            PrintLine(1, "SoundBankSaveName = " & soundBankInfo.HashCode)
                            PrintLine(1, "SoundBankFileName = " & sfxFilePath)
                            PrintLine(1, "Stream PoolFiles(n).FileRef")

                            'Create output files
                            If StrComp(currentPlatform, "GameCube") = 0 Then
                                Using sbfWriter As New BinaryWriter(File.Open(sbfFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                    Using ssfWriter As New BinaryWriter(File.Open(ssfFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                        Using sifWriter As New BinaryWriter(File.Open(sifFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                            Using sfxWriter As New BinaryWriter(File.Open(sfxFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                                WriteSbfFile(sbfWriter, SamplesDictionary)
                                                WriteSsfFile(ssfWriter, SamplesDictionary)
                                                WriteSifFile(sifWriter, SamplesDictionary, True)
                                                WriteSfxFile(sfxWriter, hashCodesList, SfxDictionary, SamplesDictionary, streamsList, True)
                                            End Using
                                        End Using
                                    End Using
                                End Using
                                ESUtils.MusXBuild_Soundbank.BuildSoundbankFile(sfxFilePath, sifFilePath, sbfFilePath, ssfFilePath, fso.BuildPath(outputFilePath, sfxFileName & ".SFX"), soundBankInfo.HashCode, True)
                            Else
                                Using sbfWriter As New BinaryWriter(File.Open(sbfFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                    Using sifWriter As New BinaryWriter(File.Open(sifFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                        Using sfxWriter As New BinaryWriter(File.Open(sfxFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                            WriteSbfFile(sbfWriter, SamplesDictionary)
                                            WriteSifFile(sifWriter, SamplesDictionary, False)
                                            WriteSfxFile(sfxWriter, hashCodesList, SfxDictionary, SamplesDictionary, streamsList, False)
                                        End Using
                                    End Using
                                End Using
                                ESUtils.MusXBuild_Soundbank.BuildSoundbankFile(sfxFilePath, sifFilePath, sbfFilePath, Nothing, fso.BuildPath(outputFilePath, sfxFileName & ".SFX"), soundBankInfo.HashCode, False)
                            End If
                        End If
                    Next
                Next
            End If
        Next
    End Sub

    Private Sub WriteSfxFile(binWriter As BinaryWriter, hashCodesList As SortedDictionary(Of String, UInteger), sfxDictionary As SortedDictionary(Of String, EXSound), samplesDictionary As Dictionary(Of String, EXAudio), streamsList As String(), isBigEndian As Boolean)
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
                    fileRef = hashCodesList(GetOnlyFileName(currentSample.FilePath.TrimStart("\"c)))
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

    Private Sub WriteSifFile(binWriter As BinaryWriter, SamplesDictionary As Dictionary(Of String, EXAudio), isBigEndian As Boolean)
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

    Private Sub WriteSsfFile(binWriter As BinaryWriter, SamplesDictionary As Dictionary(Of String, EXAudio))
        For Each soundToWrite As EXAudio In SamplesDictionary.Values
            binWriter.Write(soundToWrite.DspHeaderData)
        Next
    End Sub

    Private Sub WriteSbfFile(binWriter As BinaryWriter, SamplesDictionary As Dictionary(Of String, EXAudio))
        For Each soundToWrite As EXAudio In SamplesDictionary.Values
            soundToWrite.Address = binWriter.BaseStream.Position
            binWriter.Write(soundToWrite.SampleData)
        Next
    End Sub
End Class

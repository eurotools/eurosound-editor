﻿Imports System.IO
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.SoundBanksExporterFunctions

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
                        Dim timerTotalTime As New Stopwatch
                        Dim timerQuery As New Stopwatch
                        Dim timerSfxData As New Stopwatch
                        Dim timerSamples As New Stopwatch
                        'Debug info
                        mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Timings For " & currentSoundBank & vbCrLf)

                        Dim CancelSoundBankOutput As Boolean = False

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
                        timerTotalTime.Start()
                        timerQuery.Start()
                        Dim SfxList As String() = GetSFXsList(soundBankInfo)
                        timerQuery.Stop()
                        timerSfxData.Start()
                        GetSFXsDictionary(SfxList, currentPlatform, SfxDictionary, samplesToInclude, streamsList)
                        timerSfxData.Stop()
                        timerSamples.Start()
                        GetSamplesDictionary(samplesToInclude, SamplesDictionary, currentPlatform, currentLanguage, CancelSoundBankOutput)
                        timerSamples.Stop()
                        timerTotalTime.Stop()
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

                        'Show debug info
                        mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Total   = " & timerTotalTime.ElapsedMilliseconds & vbCrLf)
                        mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Query   = " & timerQuery.ElapsedMilliseconds & vbCrLf)
                        mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "SFXDate = " & timerSfxData.ElapsedMilliseconds & vbCrLf)
                        mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Samples = " & timerSamples.ElapsedMilliseconds & vbCrLf)
                        mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += vbCrLf)
                    Next
                Next
            End If
        Next
    End Sub
End Class

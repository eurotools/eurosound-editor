﻿Imports System.IO
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.SoundBanksExporterFunctions

Partial Public Class ExporterForm
    Public Sub OutputSoundbanks(hashCodesList As SortedDictionary(Of String, UInteger), soundbanksList As String(), streamsList As String(), outLanguages As String(), outPlatforms As String(), ByRef AbortOutput As Boolean)
        'Debug Folder
        Dim debugFolder As String = Path.Combine(WorkingDirectory, "Debug_Report")
        Directory.CreateDirectory(debugFolder)

        'Reset progress bar
        Invoke(Sub() ProgressBar1.Value = 0)

        'For each Soundbank
        For soundBankIndex As Integer = 0 To soundbanksList.Length - 1
            If Not AbortOutput Then
                'Calculate and report progress
                BackgroundWorker.ReportProgress(Decimal.Divide(soundBankIndex, soundbanksList.Length) * 100.0)
                Dim currentSoundBank As String = soundbanksList(soundBankIndex)
                Dim soundbankFilePath As String = Path.Combine(WorkingDirectory, "SoundBanks", currentSoundBank & ".txt")
                'Read soundbank file
                If File.Exists(soundbankFilePath) Then
                    Dim soundBankInfo As SoundbankFile = textFileReaders.ReadSoundBankFile(soundbankFilePath)
                    'For each Language
                    For languageIndex As Integer = 0 To outLanguages.Length - 1
                        If Not AbortOutput Then
                            Dim currentLanguage As String = outLanguages(languageIndex)
                            'For each Platform
                            For platformIndex As Integer = 0 To outPlatforms.Length - 1
                                Dim timerTotalTime, timerQuery, timerSfxData, timerSamples As New Stopwatch

                                'Debug info
                                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Timings For " & currentSoundBank & vbCrLf)

                                Dim CancelSoundBankOutput As Boolean = False

                                'Get current platform and update title bar
                                Dim currentPlatform As String = outPlatforms(platformIndex)
                                Invoke(Sub() Text = "Outputting " & currentLanguage & " SoundBank " & currentSoundBank & " for " & currentPlatform)

                                'Get output folder
                                Dim outputFolder As String = Path.Combine(WorkingDirectory, "TempOutputFolder", currentPlatform, "SoundBanks", currentLanguage)
                                Directory.CreateDirectory(outputFolder)

                                'Get SFX and samples list
                                timerTotalTime.Start()

                                timerQuery.Start()
                                Dim SfxList As String() = GetSoundBankSFXsList(soundBankInfo, currentPlatform)
                                timerQuery.Stop()

                                timerSfxData.Start()
                                Dim sfxDictionary As Dictionary(Of String, EXSound) = ReadSfxData(SfxList, False)
                                ApplyDuckerLength(SfxDictionary, currentPlatform)
                                timerSfxData.Stop()

                                timerSamples.Start()
                                Dim sampleToInclude As String() = GetFinalList(GetSoundBankSamplesList(SfxList, currentLanguage), streamsList, currentPlatform, False)
                                Dim samplesDictionary As Dictionary(Of String, EXAudio) = ReadSampleData(sampleToInclude, currentPlatform)
                                timerSamples.Stop()

                                timerTotalTime.Stop()
                                'Skip if there are samples missing
                                If Not CancelSoundBankOutput Then
                                    Dim mainFilePath As String = Path.Combine(outputFolder, soundBankInfo.HashCode)
                                    'Get file paths
                                    Dim sfxFilePath As String = mainFilePath & ".sfx"
                                    Dim sifFilePath As String = mainFilePath & ".sif"
                                    Dim ssfFilePath As String = mainFilePath & ".ssf"
                                    Dim sbfFilePath As String = mainFilePath & ".sbf"

                                    'Get output file paths
                                    Dim sfxFileName As String = "HC" & Hex(GetSfxFileName(Array.IndexOf(SfxLanguages, currentLanguage), soundBankInfo.HashCode)).PadLeft(6, "0"c)
                                    Dim outputFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary", GetEngineXFolder(currentPlatform), GetEngineXLangFolder(currentLanguage))
                                    Directory.CreateDirectory(outputFilePath)

                                    'Debug file
                                    Dim debugFile As String = Path.Combine(WorkingDirectory, "Debug_Report")
                                    Directory.CreateDirectory(debugFile)

                                    'Create output files
                                    Dim outputIsBigEndian As Boolean = False
                                    If StrComp(currentPlatform, "GameCube") = 0 Then
                                        outputIsBigEndian = True
                                    End If

                                    'Create Soundbank files
                                    Dim streamFileReport As New List(Of KeyValuePair(Of String, Integer))
                                    Using sbfWriter As New BinaryWriter(File.Open(sbfFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                        Using sifWriter As New BinaryWriter(File.Open(sifFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                            Using sfxWriter As New BinaryWriter(File.Open(sfxFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                                WriteSbfFile(sbfWriter, samplesDictionary)
                                                WriteSifFile(sifWriter, samplesDictionary, outputIsBigEndian)
                                                streamFileReport = WriteSfxFile(sfxWriter, hashCodesList, sfxDictionary, samplesDictionary, streamsList, outputIsBigEndian)
                                            End Using
                                        End Using
                                    End Using

                                    'Write Debug File
                                    Dim soundBanksDebugFilePath As String = Path.Combine(debugFile, "StreamDebugSoundBank_" & currentSoundBank & "_" & currentPlatform & "_" & currentLanguage & ".txt")
                                    textFileWritters.WriteSoundBankDebug(soundBanksDebugFilePath, streamFileReport, sfxFilePath, currentSoundBank, soundBankInfo.HashCode)

                                    'Create GameCube Special Section
                                    If outputIsBigEndian Then
                                        Using ssfWriter As New BinaryWriter(File.Open(ssfFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                            WriteSsfFile(ssfWriter, samplesDictionary)
                                        End Using
                                    End If

                                    'Check min and max sizes
                                    Dim fileSize As Long = FileLen(sbfFilePath) - 4024
                                    Dim bankSize As Long = GetSoundBankMaxSize(currentPlatform, soundBankInfo) * 1024
                                    If fileSize > bankSize Then
                                        'Delete files
                                        File.Delete(sbfFilePath)
                                        File.Delete(sifFilePath)
                                        File.Delete(sfxFilePath)
                                        'Inform user
                                        Dim stringHeader As String = "Sample Bank Limit Exceeded With:" & vbCrLf & vbCrLf & "SoundBank: " & currentSoundBank & vbCrLf & "Format: " & currentPlatform
                                        MsgBox(stringHeader & vbCrLf & "My Size: " & BytesStringFormat(fileSize) & vbCrLf & "My Max Size: " & BytesStringFormat(bankSize) & vbCrLf & vbCrLf & "Output Aborted and Files Deleted.", vbOKOnly + vbCritical, "Fatal Output Error!")
                                        AbortOutput = True
                                    Else
                                        Dim sampleBankFilePath As String = Path.Combine(outputFilePath, sfxFileName & ".SFX")
                                        If outputIsBigEndian Then
                                            ESUtils.MusXBuild_Soundbank.BuildSoundbankFile(sfxFilePath, sifFilePath, sbfFilePath, ssfFilePath, sampleBankFilePath, soundBankInfo.HashCode, False)
                                        Else
                                            ESUtils.MusXBuild_Soundbank.BuildSoundbankFile(sfxFilePath, sifFilePath, sbfFilePath, Nothing, sampleBankFilePath, soundBankInfo.HashCode, False)
                                        End If
                                    End If
                                End If

                                'Show debug info
                                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Total   = " & timerTotalTime.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Query   = " & timerQuery.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "SFXDate = " & timerSfxData.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Samples = " & timerSamples.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += vbCrLf)

                                'Check if we have to quit
                                If AbortOutput Then
                                    Exit For
                                End If
                            Next
                        Else
                            Exit For
                        End If
                    Next
                End If
            Else
                Exit For
            End If
        Next
    End Sub
End Class

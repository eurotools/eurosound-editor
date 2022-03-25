Imports System.IO
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.SoundBanksExporterFunctions

Partial Public Class ExporterForm
    Public Sub OutputSoundbanks(hashCodesList As SortedDictionary(Of String, UInteger), soundbanksList As String(), outLanguages As String(), outPlatforms As String(), ByRef AbortOutput As Boolean, soundsTable As DataTable)
        'Debug file
        Dim debugFolderPath As String = Path.Combine(WorkingDirectory, "Debug_Report")
        Directory.CreateDirectory(debugFolderPath)

        'Reset progress bar
        Invoke(Sub() ProgressBar1.Value = 0)

        'Create Folder structure for all platforms and languages
        For platformIndex As Integer = 0 To outPlatforms.Length - 1
            Dim currentPlatform As String = outPlatforms(platformIndex)
            'For each Language
            For languageIndex As Integer = 0 To outLanguages.Length - 1
                Dim currentLanguage As String = outLanguages(languageIndex)
                'Get output folder
                Dim outputFolder As String = Path.Combine(WorkingDirectory, "TempOutputFolder", currentPlatform, "SoundBanks", currentLanguage)
                Directory.CreateDirectory(outputFolder)
            Next
        Next

        'For each Soundbank
        For soundBankIndex As Integer = 0 To soundbanksList.Length - 1
            If Not AbortOutput Then
                'Calculate and report progress

                Dim currentSoundBank As String = soundbanksList(soundBankIndex)
                Dim soundbankFilePath As String = Path.Combine(WorkingDirectory, "SoundBanks", currentSoundBank & ".txt")
                'Read soundbank file
                If File.Exists(soundbankFilePath) Then
                    Dim soundBankInfo As SoundbankFile = textFileReaders.ReadSoundBankFile(soundbankFilePath)
                    'For each Language
                    For languageIndex As Integer = 0 To outLanguages.Length - 1
                        If Not AbortOutput Then
                            Dim currentLanguage As String = outLanguages(languageIndex)
                            Dim streamsList As String() = GetStreamsCurrentLanguage(textFileReaders.GetAllStreamSamples(soundsTable), currentLanguage)

                            'For each Platform
                            For platformIndex As Integer = 0 To outPlatforms.Length - 1
                                Dim currentPlatform As String = outPlatforms(platformIndex)
                                Dim CancelSoundBankOutput As Boolean = False

                                'Report progress
                                BackgroundWorker.ReportProgress(Decimal.Divide(soundBankIndex, soundbanksList.Length) * 100.0, "Outputting " & currentLanguage & " SoundBank " & currentSoundBank & " for " & currentPlatform)

                                'Debug info
                                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Timings For " & currentSoundBank & vbCrLf)

                                'Get output folder
                                Dim outputFolder As String = Path.Combine(WorkingDirectory, "TempOutputFolder", currentPlatform, "SoundBanks", currentLanguage)

                                'Start timer
                                Dim timerTotalTime, timerQuery, timerSfxData, timerSamples As New Stopwatch
                                timerTotalTime.Start()

                                'Get SFXs List
                                timerQuery.Start()
                                Dim SfxList As String() = GetSoundBankSFXsList(soundBankInfo, currentPlatform)
                                timerQuery.Stop()

                                'Read SFXs Data
                                timerSfxData.Start()
                                Dim sfxDictionary As SortedDictionary(Of String, EXSound) = ReadSfxData(SfxList, False)
                                ApplyDuckerLength(sfxDictionary, currentPlatform)
                                timerSfxData.Stop()

                                'Read Sample Data
                                timerSamples.Start()
                                Dim sampleToInclude As String() = GetFinalList(GetSoundBankSamplesList(SfxList, currentLanguage), streamsList, currentPlatform, False)
                                Dim samplesDictionary As Dictionary(Of String, EXAudio) = ReadSampleData(sampleToInclude, currentPlatform, CancelSoundBankOutput)
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
                                    Dim sfxFileName As String = "HC" & GetSfxFileName(Array.IndexOf(SfxLanguages, currentLanguage), soundBankInfo.HashCode).ToString("X6")
                                    Dim outputFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary", GetEngineXFolder(currentPlatform), GetEngineXLangFolder(currentLanguage))
                                    Directory.CreateDirectory(outputFilePath)

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
                                                streamFileReport = WriteSfxFile(sfxWriter, hashCodesList, sfxDictionary, samplesDictionary, streamsList, currentLanguage, outputIsBigEndian)
                                            End Using
                                        End Using
                                    End Using

                                    'Create GameCube Special Section
                                    If outputIsBigEndian Then
                                        Using ssfWriter As New BinaryWriter(File.Open(ssfFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                            WriteSsfFile(ssfWriter, samplesDictionary)
                                        End Using
                                    End If

                                    'Write Debug File
                                    Dim soundBanksDebugFilePath As String = Path.Combine(debugFolderPath, "StreamDebugSoundBank_" & currentSoundBank & "_" & currentPlatform & "_" & currentLanguage & ".txt")
                                    textFileWritters.WriteSoundBankDebug(soundBanksDebugFilePath, streamFileReport, sfxFilePath, currentSoundBank, soundBankInfo.HashCode)

                                    'Check min and max sizes
                                    Dim fileSize As Long = New FileInfo(sbfFilePath).Length
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
                                        ESUtils.MusXBuild_Soundbank.BuildSoundbankFile(sfxFilePath, sifFilePath, sbfFilePath, ssfFilePath, sampleBankFilePath, soundBankInfo.HashCode, outputIsBigEndian)
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

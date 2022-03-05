Imports System.IO
Imports EngineXMarkersTool
Imports NAudio.Wave

Partial Public Class ExporterForm
    Private Sub GenerateStreamFolder(streamSamplesList As String(), outLanguages As String(), outPlatforms As String())
        Dim markersFunctions As New ExMarkersTool
        Dim markerFileFunctions As New MarkerFiles
        Dim waveFunctions As New WaveFunctions

        'Debug Folder
        Dim debugFolder As String = fso.BuildPath(WorkingDirectory, "Debug_Report")
        CreateFolderIfRequired(debugFolder)

        'Get Wave files to include
        Dim streamSamplesCount As Integer = streamSamplesList.Length - 1
        If streamSamplesCount > 0 Then
            'Reset progress bar
            Invoke(Sub() ProgressBar1.Value = 0)
            'For each Language
            For languageIndex As Integer = 0 To outLanguages.Length - 1
                Dim currentLanguage As String = outLanguages(languageIndex)
                'For each Platform
                For platformIndex As Integer = 0 To outPlatforms.Length - 1
                    Dim filesToBind As New List(Of String)
                    Dim currentPlatform As String = outPlatforms(platformIndex)
                    'Create Debug file
                    FileOpen(1, fso.BuildPath(debugFolder, "StreamsConverted_" & currentLanguage & "_" & currentPlatform & ".txt"), OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                    'For each Sample
                    For sampleIndex As Integer = 0 To streamSamplesCount
                        'Calculate and report progress
                        BackgroundWorker.ReportProgress(Decimal.Divide(sampleIndex, streamSamplesCount) * 100.0)
                        'Get sample file path
                        Dim sampleFilePath As String = streamSamplesList(sampleIndex)
                        'If starts with speech but doesn't match the current language, skip
                        If InStr(1, sampleFilePath, "Speech\", CompareMethod.Binary) Then
                            If StrComp(currentLanguage, "English", CompareMethod.Binary) <> 0 Then
                                Dim multiSamplePath As String = Mid(sampleFilePath, Len("Speech\English\") + 1)
                                sampleFilePath = fso.BuildPath("Speech\" & currentLanguage, multiSamplePath)
                            End If
                        End If
                        'Get platform samples folder path
                        Dim sampleFullPath As String = ""
                        If StrComp(currentPlatform, "PC") = 0 Or StrComp(currentPlatform, "GameCube") = 0 Then
                            sampleFullPath = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\" & currentPlatform & "_Software_adpcm", sampleFilePath), ".ssp")
                        End If
                        If StrComp(currentPlatform, "PlayStation2") = 0 Then
                            sampleFullPath = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\PlayStation2_VAG", sampleFilePath), ".vag")
                        End If
                        If StrComp(currentPlatform, "X Box") = 0 Or StrComp(currentPlatform, "Xbox") = 0 Then
                            sampleFullPath = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\XBox_adpcm", sampleFilePath), ".adpcm")
                        End If
                        'Ensure that the file exists
                        If fso.FileExists(sampleFullPath) Then
                            'Update title bar
                            Invoke(Sub() Text = currentLanguage & " Stream " & sampleFullPath & " For " & currentPlatform)
                            'Calculate destination folder
                            Dim destinationFolder As String = WorkingDirectory & "\" & currentPlatform & "_Streams\" & currentLanguage
                            CreateFolderIfRequired(destinationFolder)
                            'Move Sample to destination folder
                            Dim destinationFilePath As String = fso.BuildPath(destinationFolder, "STR_" & sampleIndex & ".ssd")
                            fso.CopyFile(sampleFullPath, destinationFilePath)
                            filesToBind.Add(destinationFilePath)
                            'Write to debug file
                            PrintLine(1, "InputFile = " & sampleFullPath)
                            PrintLine(1, "OutputFileName = " & destinationFilePath)
                            'Create marker file
                            Dim masterWaveFilePath As String = fso.BuildPath(WorkingDirectory & "\Master", sampleFilePath)
                            Dim masterMarkerFilePath As String = Path.ChangeExtension(masterWaveFilePath, ".mrk")
                            If Not fso.FileExists(masterMarkerFilePath) Then
                                Using waveReader As New WaveFileReader(masterWaveFilePath)
                                    Dim sampleChunkData As Integer() = waveFunctions.ReadSampleChunk(waveReader)
                                    markerFileFunctions.CreateStreamMarkerFile(masterMarkerFilePath, sampleChunkData, waveReader.Length / 2)
                                End Using
                            End If
                            markersFunctions.CreateStreamMarkers(destinationFilePath, masterMarkerFilePath, Path.ChangeExtension(destinationFilePath, ".smf"), currentPlatform, 100)
                        Else
                            Invoke(Sub() MsgBox("ReSampleStreams. File Not Here: " & sampleFullPath, vbOKOnly + vbCritical, "EuroSound"))
                        End If
                    Next
                    'Close file
                    FileClose(1)
                    'Get paths
                    Dim temporalOutputFile As String = fso.BuildPath(WorkingDirectory & "\TempOutputFolder\" & currentPlatform & "\" & currentLanguage, "Streams")
                    CreateFolderIfRequired(temporalOutputFile)
                    'Build Temporal Stream File
                    BuildTemporalFile(filesToBind, currentPlatform, currentLanguage, temporalOutputFile)
                    'Get final Name
                    Dim sfxFileName As String = "HC" & Hex(GetSfxFileName(Array.IndexOf(SfxLanguages, currentLanguage), &HFFFF)).PadLeft(6, "0"c)
                    Dim outputFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary\" & GetEngineXFolder(currentPlatform) & "\" & GetEngineXLangFolder(currentLanguage))
                    CreateFolderIfRequired(outputFilePath)
                    If StrComp(currentPlatform, "GameCube") = 0 Then
                        ESUtils.MusXBuild_StreamFile.BuildStreamFile(fso.BuildPath(temporalOutputFile, "STREAMS.bin"), fso.BuildPath(temporalOutputFile, "STREAMS.lut"), fso.BuildPath(outputFilePath, sfxFileName & ".SFX"), True)
                    Else
                        ESUtils.MusXBuild_StreamFile.BuildStreamFile(fso.BuildPath(temporalOutputFile, "STREAMS.bin"), fso.BuildPath(temporalOutputFile, "STREAMS.lut"), fso.BuildPath(outputFilePath, sfxFileName & ".SFX"), False)
                    End If
                Next
            Next
        End If
    End Sub
End Class

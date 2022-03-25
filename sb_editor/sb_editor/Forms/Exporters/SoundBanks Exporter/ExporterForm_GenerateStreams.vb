Imports System.IO
Imports EngineXMarkersTool
Imports NAudio.Wave
Imports sb_editor.MarkerFunctions
Imports sb_editor.SoundBanksExporterFunctions

Partial Public Class ExporterForm
    Private ReadOnly markersFunctions As New ExMarkersTool
    Private ReadOnly markerFileFunctions As New MarkerFiles

    Private Sub GenerateStreamFolder(outLanguages As String(), outPlatforms As String(,), soundsTable As DataTable)
        'Debug Folder
        Dim debugFolder As String = Path.Combine(WorkingDirectory, "Debug_Report")
        Directory.CreateDirectory(debugFolder)

        'Create Folder structure
        For languageIndex As Integer = 0 To outLanguages.Length - 1
            Dim currentLanguage As String = outLanguages(languageIndex)
            For platformIndex As Integer = 0 To outPlatforms.GetLength(0) - 1
                Dim currentPlatform As String = outPlatforms(platformIndex, 0)
                Directory.CreateDirectory(Path.Combine(WorkingDirectory, currentPlatform & "_Streams", currentLanguage))
            Next
        Next

        'Reset progress bar
        Invoke(Sub() ProgressBar1.Value = 0)

        'For each Language
        For languageIndex As Integer = 0 To outLanguages.Length - 1
            Dim currentLanguage As String = outLanguages(languageIndex)
            Dim finalPlatformStreams As String() = GetStreamsCurrentLanguage(textFileReaders.GetAllStreamSamples(soundsTable), currentLanguage)

            'For each Platform
            For platformIndex As Integer = 0 To outPlatforms.GetLength(0) - 1
                If outPlatforms(platformIndex, 2).Equals("On", StringComparison.OrdinalIgnoreCase) Then
                    Dim currentPlatform As String = outPlatforms(platformIndex, 0)
                    Dim filesToBind As New List(Of String)
                    Dim destinationFolder As String = Path.Combine(WorkingDirectory, currentPlatform & "_Streams", currentLanguage)

                    'Create Debug file
                    Using outputFile As New StreamWriter(Path.Combine(debugFolder, "StreamsConverted_" & currentLanguage & "_" & currentPlatform & ".txt"))
                        'For each Sample
                        Dim streamSamplesCount As Integer = finalPlatformStreams.Length
                        For sampleIndex As Integer = 0 To streamSamplesCount - 1
                            'If starts with speech but doesn't match the current language, skip
                            Dim sampleFilePath As String = finalPlatformStreams(sampleIndex)
                            Dim destinationFilePath As String = Path.Combine(destinationFolder, "STR_" & sampleIndex & ".ssd")

                            'Get Sample Full Path
                            Dim sampleFullPath As String
                            Select Case currentPlatform
                                Case "PC"
                                    sampleFullPath = Path.ChangeExtension(Path.Combine(WorkingDirectory, currentPlatform & "_Software_adpcm", sampleFilePath), ".ssp")
                                Case "GameCube"
                                    sampleFullPath = Path.ChangeExtension(Path.Combine(WorkingDirectory, currentPlatform & "_Software_adpcm", sampleFilePath), ".ssp")
                                Case "PlayStation2"
                                    sampleFullPath = Path.ChangeExtension(Path.Combine(WorkingDirectory, "PlayStation2_VAG", sampleFilePath), ".vag")
                                Case Else
                                    sampleFullPath = Path.Combine(WorkingDirectory, "XBox_adpcm", sampleFilePath)
                            End Select

                            'Update progress
                            BackgroundWorker.ReportProgress(Decimal.Divide(sampleIndex, streamSamplesCount) * 100.0, currentLanguage & " Stream " & sampleFullPath & " For " & currentPlatform)

                            'Create stream files
                            If File.Exists(sampleFullPath) Then
                                Select Case currentPlatform
                                    Case "PC"
                                        File.Copy(sampleFullPath, destinationFilePath, True)
                                    Case "GameCube"
                                        File.Copy(sampleFullPath, destinationFilePath, True)
                                    Case "PlayStation2"
                                        File.WriteAllBytes(destinationFilePath, GetVagFileDataChunk(sampleFullPath))
                                    Case Else
                                        File.WriteAllBytes(destinationFilePath, GetXboxAdpcmDataChunk(sampleFullPath))
                                End Select

                                'Add file to list
                                filesToBind.Add(destinationFilePath)

                                'Create marker file
                                CreateMarkerFileIfRequired(sampleFilePath, destinationFilePath, currentPlatform)

                                'Write to debug file
                                outputFile.WriteLine("InputFile = " & sampleFullPath)
                                outputFile.WriteLine("OutputFileName = " & destinationFilePath)
                            Else
                                Invoke(Sub() MsgBox("ReSampleStreams. File Not Here: " & sampleFullPath, vbOKOnly + vbCritical, "EuroSound"))
                            End If
                        Next
                    End Using

                    'Build Temporal Stream File
                    If filesToBind.Count > 0 Then
                        Dim temporalOutputFile As String = Path.Combine(WorkingDirectory, "TempOutputFolder", currentPlatform, currentLanguage, "Streams")
                        Directory.CreateDirectory(temporalOutputFile)
                        BuildTemporalFile(filesToBind, currentPlatform, currentLanguage, temporalOutputFile)

                        'Get final Name
                        Dim sfxFileName As String = "HC" & GetSfxFileName(Array.IndexOf(SfxLanguages, currentLanguage), &HFFFF).ToString("X6")
                        Dim outputFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary", GetEngineXFolder(currentPlatform), GetEngineXLangFolder(currentLanguage))
                        Directory.CreateDirectory(outputFilePath)

                        'Build SFX file
                        If StrComp(currentPlatform, "GameCube") = 0 Then
                            ESUtils.MusXBuild_StreamFile.BuildStreamFile(Path.Combine(temporalOutputFile, "STREAMS.bin"), Path.Combine(temporalOutputFile, "STREAMS.lut"), Path.Combine(outputFilePath, sfxFileName & ".SFX"), True)
                        Else
                            ESUtils.MusXBuild_StreamFile.BuildStreamFile(Path.Combine(temporalOutputFile, "STREAMS.bin"), Path.Combine(temporalOutputFile, "STREAMS.lut"), Path.Combine(outputFilePath, sfxFileName & ".SFX"), False)
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub CreateMarkerFileIfRequired(sampleFilePath As String, destFilePath As String, currentPlatform As String)
        Dim masterWaveFilePath As String = Path.Combine(WorkingDirectory, "Master", sampleFilePath)
        Dim masterMarkerFilePath As String = Path.ChangeExtension(masterWaveFilePath, ".mrk")
        If Not File.Exists(masterMarkerFilePath) Then
            Using waveReader As New WaveFileReader(masterWaveFilePath)
                Dim sampleChunkData As Integer() = ReadWaveSampleChunk(waveReader)
                markerFileFunctions.CreateStreamMarkerFile(masterMarkerFilePath, sampleChunkData, waveReader.Length / 2)
            End Using
        End If
        markersFunctions.CreateStreamMarkers(destFilePath, masterMarkerFilePath, Path.ChangeExtension(destFilePath, ".smf"), currentPlatform, 100)
    End Sub
End Class

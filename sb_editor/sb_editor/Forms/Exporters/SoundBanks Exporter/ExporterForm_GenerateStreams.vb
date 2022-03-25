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

                    'Create Debug file
                    Using outputFile As New StreamWriter(Path.Combine(debugFolder, "StreamsConverted_" & currentLanguage & "_" & currentPlatform & ".txt"))
                        'For each Sample
                        Dim streamSamplesCount As Integer = finalPlatformStreams.Length
                        For sampleIndex As Integer = 0 To streamSamplesCount - 1
                            'If starts with speech but doesn't match the current language, skip
                            Dim sampleFilePath As String = finalPlatformStreams(sampleIndex)

                            'Get destination folder
                            Dim destinationFolder As String = Path.Combine(WorkingDirectory, currentPlatform & "_Streams", currentLanguage)
                            Dim destinationFilePath As String = Path.Combine(destinationFolder, "STR_" & sampleIndex & ".ssd")

                            Select Case currentPlatform
                                Case "PC"
                                    Dim sampleFullPath As String = Path.ChangeExtension(Path.Combine(WorkingDirectory, currentPlatform & "_Software_adpcm", sampleFilePath), ".ssp")
                                    BackgroundWorker.ReportProgress(Decimal.Divide(sampleIndex, streamSamplesCount) * 100.0, currentLanguage & " Stream " & sampleFullPath & " For " & currentPlatform)
                                    If File.Exists(sampleFullPath) Then
                                        CreateStreamsForPcAndGC(outputFile, sampleFullPath, destinationFilePath, filesToBind)
                                        CreateMarkerFileIfRequired(sampleFilePath, destinationFilePath, currentPlatform)
                                    Else
                                        Invoke(Sub() MsgBox("ReSampleStreams. File Not Here: " & sampleFullPath, vbOKOnly + vbCritical, "EuroSound"))
                                    End If
                                Case "GameCube"
                                    Dim sampleFullPath As String = Path.ChangeExtension(Path.Combine(WorkingDirectory, currentPlatform & "_Software_adpcm", sampleFilePath), ".ssp")
                                    BackgroundWorker.ReportProgress(Decimal.Divide(sampleIndex, streamSamplesCount) * 100.0, currentLanguage & " Stream " & sampleFullPath & " For " & currentPlatform)

                                    If File.Exists(sampleFullPath) Then
                                        CreateStreamsForPcAndGC(outputFile, sampleFullPath, destinationFilePath, filesToBind)
                                        CreateMarkerFileIfRequired(sampleFilePath, destinationFilePath, currentPlatform)
                                    Else
                                        Invoke(Sub() MsgBox("ReSampleStreams. File Not Here: " & sampleFullPath, vbOKOnly + vbCritical, "EuroSound"))
                                    End If
                                Case "PlayStation2"
                                    Dim sampleFullPath As String = Path.ChangeExtension(Path.Combine(WorkingDirectory, "PlayStation2_VAG", sampleFilePath), ".vag")
                                    BackgroundWorker.ReportProgress(Decimal.Divide(sampleIndex, streamSamplesCount) * 100.0, currentLanguage & " Stream " & sampleFullPath & " For " & currentPlatform)

                                    If File.Exists(sampleFullPath) Then
                                        CreateStreamsForPlayStation2(outputFile, sampleFullPath, destinationFilePath, filesToBind)
                                        CreateMarkerFileIfRequired(sampleFilePath, destinationFilePath, currentPlatform)
                                    Else
                                        Invoke(Sub() MsgBox("ReSampleStreams. File Not Here: " & sampleFullPath, vbOKOnly + vbCritical, "EuroSound"))
                                    End If
                                Case Else
                                    Dim sampleFullPath As String = Path.Combine(WorkingDirectory, "XBox_adpcm", sampleFilePath)
                                    BackgroundWorker.ReportProgress(Decimal.Divide(sampleIndex, streamSamplesCount) * 100.0, currentLanguage & " Stream " & sampleFullPath & " For " & currentPlatform)

                                    If File.Exists(sampleFullPath) Then
                                        CreateStreamsForXbox(outputFile, sampleFullPath, destinationFilePath, filesToBind)
                                        CreateMarkerFileIfRequired(sampleFilePath, destinationFilePath, currentPlatform)
                                    Else
                                        Invoke(Sub() MsgBox("ReSampleStreams. File Not Here: " & sampleFullPath, vbOKOnly + vbCritical, "EuroSound"))
                                    End If
                            End Select
                        Next
                    End Using

                    'Build Temporal Stream File
                    If filesToBind.Count > 0 Then
                        Dim temporalOutputFile As String = Path.Combine(WorkingDirectory, "TempOutputFolder", currentPlatform, currentLanguage, "Streams")
                        Directory.CreateDirectory(temporalOutputFile)
                        BuildTemporalFile(filesToBind, currentPlatform, currentLanguage, temporalOutputFile)

                        'Get final Name
                        Dim sfxFileName As String = "HC" & Hex(GetSfxFileName(Array.IndexOf(SfxLanguages, currentLanguage), &HFFFF)).PadLeft(6, "0"c)
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

    Private Sub CreateStreamsForPcAndGC(outputFile As StreamWriter, sampleFullPath As String, destinationFilePath As String, filesToBind As List(Of String))
        File.Copy(sampleFullPath, destinationFilePath, True)
        filesToBind.Add(destinationFilePath)

        'Write to debug file
        outputFile.WriteLine("InputFile = " & sampleFullPath)
        outputFile.WriteLine("OutputFileName = " & destinationFilePath)
    End Sub

    Private Sub CreateStreamsForPlayStation2(outputFile As StreamWriter, sampleFullPath As String, destinationFilePath As String, filesToBind As List(Of String))
        Dim vagFileData As Byte() = GetVagFileDataChunk(sampleFullPath)
        File.WriteAllBytes(destinationFilePath, vagFileData)
        filesToBind.Add(destinationFilePath)

        'Write to debug file
        outputFile.WriteLine("InputFile = " & sampleFullPath)
        outputFile.WriteLine("OutputFileName = " & destinationFilePath)
    End Sub

    Private Sub CreateStreamsForXbox(outputFile As StreamWriter, sampleFullPath As String, destinationFilePath As String, filesToBind As List(Of String))
        Dim adpcmFileData As Byte() = GetXboxAdpcmDataChunk(sampleFullPath)
        File.WriteAllBytes(destinationFilePath, adpcmFileData)
        filesToBind.Add(destinationFilePath)

        'Write to debug file
        outputFile.WriteLine("InputFile = " & sampleFullPath)
        outputFile.WriteLine("OutputFileName = " & destinationFilePath)
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

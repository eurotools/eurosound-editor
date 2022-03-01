Imports System.IO
Imports EngineXMarkersTool
Imports NAudio.Wave

Partial Public Class ExporterForm
    Private Sub GenerateStreamFolder(streamSamplesList As String(), outputPlatforms As String(), outputLanguages As String())
        Dim markersFunctions As New ExMarkersTool
        Dim markerFileFunctions As New MarkerFiles
        Dim waveFunctions As New WaveFunctions

        'Get Wave files to include
        Dim streamSamplesCount As Integer = streamSamplesList.Length - 1
        If streamSamplesCount > 0 Then
            'Reset progress bar
            Invoke(Sub() ProgressBar1.Value = 0)
            'For each Language
            For languageIndex As Integer = 0 To outputLanguages.Length - 1
                Dim currentLanguage As String = outputLanguages(languageIndex)
                'For each Sample
                For sampleIndex As Integer = 0 To streamSamplesCount
                    'Calculate and report progress
                    Dim totalProgress As Double = Decimal.Divide(sampleIndex, streamSamplesCount) * 100.0
                    BackgroundWorker.ReportProgress(totalProgress)
                    'Get sample file path
                    Dim sampleFilePath As String = streamSamplesList(sampleIndex)
                    'If starts with speech but doesn't match the current language, skip
                    If InStr(1, sampleFilePath, "Speech\", CompareMethod.Binary) = 1 Then
                        If InStr(1, sampleFilePath, "Speech\" & currentLanguage & "\", CompareMethod.Binary) = 0 Then
                            Continue For
                        End If
                    Else
                        'For each Platform
                        For platformIndex As Integer = 0 To outputPlatforms.Length - 1
                            Dim currentPlatform As String = outputPlatforms(platformIndex)
                            'Get platform samples folder path
                            Dim sampleFullPath As String = ""
                            If StrComp(currentPlatform, "PC") = 0 Or StrComp(currentPlatform, "GameCube") = 0 Then
                                sampleFullPath = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\" & currentPlatform & "_Software_adpcm", sampleFilePath), ".ssp")
                            End If
                            If StrComp(currentPlatform, "PlayStation2") = 0 Then
                                sampleFullPath = Path.ChangeExtension(fso.BuildPath(WorkingDirectory & "\PlayStation2_VAG", sampleFilePath), ".vag")
                            End If
                            If StrComp(currentPlatform, "X Box") = 0 Then
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
                                'Create marker file
                                Dim masterWaveFilePath As String = fso.BuildPath(WorkingDirectory & "\Master", sampleFilePath)
                                Dim masterMarkerFilePath As String = Path.ChangeExtension(masterWaveFilePath, ".mkr")
                                If Not fso.FileExists(masterMarkerFilePath) Then
                                    Using waveReader As New WaveFileReader(masterWaveFilePath)
                                        Dim sampleChunkData As Integer() = waveFunctions.ReadSampleChunk(waveReader)
                                        markerFileFunctions.CreateStreamMarkerFile(masterMarkerFilePath, sampleChunkData, waveReader.Length / 2)
                                    End Using
                                End If
                                markersFunctions.CreateStreamMarkers(destinationFilePath, masterMarkerFilePath, Path.ChangeExtension(destinationFilePath, ".smf"), currentPlatform, 100)
                                'Next
                            Else
                                Invoke(Sub() MsgBox("ReSampleStreams. File Not Here: " & sampleFullPath, vbOKOnly + vbCritical, "EuroSound"))
                            End If
                        Next
                    End If
                Next

                BuildTemporalFile(streamSamplesList.Length, "PlayStation2", "English")
            Next
        End If
    End Sub
End Class

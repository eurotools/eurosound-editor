Imports System.ComponentModel
Imports System.IO
Imports NAudio.Wave
Imports sb_editor.ParsersObjects

Partial Public Class ExporterForm
    Private Function CreateSfxDataTempFolder(samplesDt As DataTable) As Integer
        Dim waveFunctions As New WaveFunctions
        Dim maxHashCode As Integer = 0
        'Ensure that we have files to resample
        If samplesDt.Rows.Count > 0 Then
            'Reset progress bar
            Invoke(Sub() ProgressBar1.Value = 0)
            'Get output folder
            Dim tempSfxDataFolder As String = fso.BuildPath(WorkingDirectory, "TempSfxData")
            CreateFolderIfRequired(tempSfxDataFolder)
            Dim StreamSamplesList As String() = textFileReaders.GetStreamSoundsList(SysFileSamples)
            'Start resample
            Dim sfxFiles As String() = Directory.GetFiles(fso.BuildPath(WorkingDirectory, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly)
            Dim samplesCount As Integer = sfxFiles.Length - 1
            For index As Integer = 0 To samplesCount
                'Update title bar
                Dim currentSampleName As String = GetOnlyFileName(sfxFiles(index))
                'Calculate progress and update title bar
                Invoke(Sub() Text = "Creating SFX_Data.h " & currentSampleName)
                BackgroundWorker.ReportProgress(Decimal.Divide(index, samplesCount) * 100.0)
                'Read file data
                If fso.FileExists(sfxFiles(index)) Then
                    Dim sfxFile As SfxFile = textFileReaders.ReadSFXFile(sfxFiles(index))
                    If sfxFile.Samples.Count > 0 Then
                        Dim waveFilePath As String = fso.BuildPath(WorkingDirectory & "\Master", sfxFile.Samples(0).FilePath)
                        Dim waveLooping As Integer() = New Integer() {0, 0, 0}
                        'Get duration
                        Dim waveDuration As Single = 0.00002267574F
                        If fso.FileExists(waveFilePath) Then
                            Using waveReader As New WaveFileReader(waveFilePath)
                                waveDuration = (waveReader.Length / waveReader.WaveFormat.AverageBytesPerSecond) + 0.0
                                waveLooping = waveFunctions.ReadSampleChunk(waveReader)
                            End Using
                        Else
                            sfxFile.Parameters.TrackingType = 4
                        End If
                        'Format String
                        Dim waveDurationFormat As String = waveDuration.ToString(".#######", numericProvider).TrimStart("0"c)
                        If waveDuration < 0.1 Then
                            waveDurationFormat = waveDuration.ToString("0.######E+00", numericProvider)
                        End If
                        'Check if is streamed or not
                        Dim fileNameToCheck As String = sfxFile.Samples(0).FilePath.TrimStart("\")
                        Dim sampleIsStreamed = 0
                        If Array.IndexOf(StreamSamplesList, UCase(fileNameToCheck)) <> -1 Then
                            sampleIsStreamed = 1
                        End If
                        'Create File
                        FileOpen(1, fso.BuildPath(tempSfxDataFolder, currentSampleName & ".txt"), OpenMode.Output, OpenAccess.Write)
                        PrintLine(1, UCase(waveFilePath))
                        PrintLine(1, "{ " & sfxFile.HashCode & " ,  " & sfxFile.Parameters.InnerRadius & ".0f ,  " & sfxFile.Parameters.OuterRadius & ".0f ,  " & sfxFile.Parameters.Alertness & ".0f ,  " & waveDurationFormat & "f , " & waveLooping(0) & ", " & sfxFile.Parameters.TrackingType & ", " & sampleIsStreamed & " } ,  // " & currentSampleName)
                        FileClose(1)
                        'Get Max HashCode
                        maxHashCode = Math.Max(maxHashCode, sfxFile.HashCode)
                    End If
                End If
            Next

            'Liberate Memmory
            Erase sfxFiles
        End If
        Return maxHashCode
    End Function

    Friend Sub CreateSFXDataBinaryFiles(outPlatforms As String(), outputLanguages As String())
        'Single Language
        If outputLanguages.Length = 1 AndAlso StrComp(UCase(outputLanguages(0)), "ENGLISH") = 0 Then
            Dim sfxDataFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Data.h")
            For platformIndex As Integer = 0 To outPlatforms.Length - 1
                Dim currentPlatform As String = outPlatforms(platformIndex)
                'Output File Path
                Dim outputFolder As String = fso.BuildPath(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary\" & GetEngineXFolder(currentPlatform) & "\_Eng")
                CreateFolderIfRequired(outputFolder)
                'Create bin file
                RunProcess("SystemFiles\SFXStructToBin.exe", """" & sfxDataFilePath & """ """ & fso.BuildPath(outputFolder, "SFX_Data.bin") & """")
            Next
        Else 'Multiples Languages
            For languageIndex As Integer = 0 To outputLanguages.Length - 1
                Dim currentLanguage As String = outputLanguages(languageIndex)
                Dim sfxDataFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, currentLanguage & "_SFX_Data.txt")
                For platformIndex As Integer = 0 To outPlatforms.Length - 1
                    Dim currentPlatform As String = outPlatforms(platformIndex)
                    'Output File Path
                    Dim outputFolder As String = fso.BuildPath(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary\" & GetEngineXFolder(currentPlatform) & "\" & GetEngineXLangFolder(currentLanguage))
                    CreateFolderIfRequired(outputFolder)
                    'Create bin file
                    RunProcess("SystemFiles\SFXStructToBin.exe", """" & sfxDataFilePath & """ """ & fso.BuildPath(outputFolder, "SFX_Data.bin") & """")
                Next
            Next
        End If
    End Sub
End Class

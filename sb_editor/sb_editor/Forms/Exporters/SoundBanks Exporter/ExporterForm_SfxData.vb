Imports System.ComponentModel
Imports System.IO
Imports NAudio.Wave
Imports sb_editor.ParsersObjects

Partial Public Class ExporterForm
    Private Function CreateSfxDataTempFolder(samplesDt As DataTable) As Integer
        Dim maxHashCode As Integer = 0
        'Ensure that we have files to resample
        If samplesDt.Rows.Count > 0 Then
            'Reset progress bar
            Invoke(Sub() ProgressBar1.Value = 0)
            'Get output folder
            Dim tempSfxDataFolder As String = Path.Combine(WorkingDirectory, "TempSfxData")
            Directory.CreateDirectory(tempSfxDataFolder)
            Dim StreamSamplesList As String() = textFileReaders.GetStreamSoundsList(SysFileSamples)
            'Start resample
            Dim sfxFiles As String() = Directory.GetFiles(Path.Combine(WorkingDirectory, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly)
            Dim samplesCount As Integer = sfxFiles.Length - 1
            For index As Integer = 0 To samplesCount
                'Update title bar
                Dim currentSampleName As String = Path.GetFileNameWithoutExtension(sfxFiles(index))
                'Calculate progress and update title bar
                Invoke(Sub() Text = "Creating SFX_Data.h " & currentSampleName)
                BackgroundWorker.ReportProgress(Decimal.Divide(index, samplesCount) * 100.0)
                'Read file data
                If File.Exists(sfxFiles(index)) Then
                    Dim sfxFile As SfxFile = textFileReaders.ReadSFXFile(sfxFiles(index))
                    If sfxFile.Samples.Count > 0 Then
                        Dim waveFilePath As String = Path.Combine(WorkingDirectory, "Master", sfxFile.Samples(0).FilePath)
                        Dim waveLooping As Integer() = New Integer() {0, 0, 0}
                        'Get duration
                        Dim waveDuration As Single = 0.00002267574F
                        If File.Exists(waveFilePath) Then
                            Using waveReader As New WaveFileReader(waveFilePath)
                                waveDuration = waveReader.Length / waveReader.WaveFormat.AverageBytesPerSecond
                                waveLooping = waveFunctions.ReadWaveSampleChunk(waveReader)
                            End Using
                        Else
                            sfxFile.Parameters.TrackingType = 4
                        End If

                        'Get the number of decimal places
                        Dim stringNum As String = waveDuration.ToString(numericProvider)
                        Dim decimalPlaces As Integer = 0
                        If InStr(1, stringNum, ".", CompareMethod.Binary) Then
                            decimalPlaces = stringNum.Substring(stringNum.IndexOf(".")).Length
                        End If

                        'Format String
                        Dim waveDurationFormat As String
                        If decimalPlaces > 8 Then
                            waveDurationFormat = waveDuration.ToString("0.######E+00", numericProvider)
                        Else
                            waveDurationFormat = waveDuration.ToString(".#######", numericProvider)
                        End If

                        'Check if is streamed or not
                        Dim fileNameToCheck As String = sfxFile.Samples(0).FilePath.TrimStart("\")
                        Dim sampleIsStreamed = 0
                        If Array.IndexOf(StreamSamplesList, UCase(fileNameToCheck)) <> -1 Then
                            sampleIsStreamed = 1
                        End If
                        'Create File
                        FileOpen(1, Path.Combine(tempSfxDataFolder, currentSampleName & ".txt"), OpenMode.Output, OpenAccess.Write)
                        PrintLine(1, UCase(waveFilePath))
                        PrintLine(1, "{ " & sfxFile.HashCode & " ,  " & sfxFile.Parameters.InnerRadius & ".0f ,  " & sfxFile.Parameters.OuterRadius & ".0f ,  " & sfxFile.Parameters.Alertness & ".0f ,  " & waveDurationFormat & "f , " & Math.Max(waveLooping(0), Convert.ToByte(sfxFile.SamplePool.isLooped)) & ", " & sfxFile.Parameters.TrackingType & ", " & sampleIsStreamed & " } ,  // " & currentSampleName)
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
            Dim sfxDataFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Data.h")
            For platformIndex As Integer = 0 To outPlatforms.Length - 1
                Dim currentPlatform As String = outPlatforms(platformIndex)
                'Output File Path
                Dim outputFolder As String = Path.Combine(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary", GetEngineXFolder(currentPlatform), "\_Eng")
                Directory.CreateDirectory(outputFolder)
                'Create bin file
                RunProcess("SystemFiles\CreateSFXDataBin.exe", """" & sfxDataFilePath & """ """ & Path.Combine(outputFolder, "SFX_Data.bin") & """")
            Next
        Else 'Multiples Languages
            For languageIndex As Integer = 0 To outputLanguages.Length - 1
                Dim currentLanguage As String = outputLanguages(languageIndex)
                Dim sfxDataFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, currentLanguage & "_SFX_Data.txt")
                For platformIndex As Integer = 0 To outPlatforms.Length - 1
                    Dim currentPlatform As String = outPlatforms(platformIndex)
                    'Output File Path
                    Dim outputFolder As String = Path.Combine(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary", GetEngineXFolder(currentPlatform), GetEngineXLangFolder(currentLanguage))
                    Directory.CreateDirectory(outputFolder)
                    'Create bin file
                    RunProcess("SystemFiles\CreateSFXDataBin.exe", """" & sfxDataFilePath & """ """ & Path.Combine(outputFolder, "SFX_Data.bin") & """")
                Next
            Next
        End If
    End Sub
End Class

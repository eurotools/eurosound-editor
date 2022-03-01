Imports System.ComponentModel
Imports System.IO
Imports HashTablesBuilder
Imports NAudio.Wave

Partial Public Class ExporterForm
    Private Sub CreateSfxDataFolder(samplesDt As DataTable, e As DoWorkEventArgs)
        Dim waveFunctions As New WaveFunctions
        'Ensure that we have files to resample
        If samplesDt.Rows.Count > 0 Then
            'Update progress bar
            Invoke(Sub() ProgressBar1.Maximum = samplesDt.Rows.Count + 1)
            Invoke(Sub() ProgressBar1.Value = 0)
            'Get output folder
            Dim tempSfxDataFolder As String = fso.BuildPath(WorkingDirectory, "TempSfxData")
            If Not fso.FolderExists(tempSfxDataFolder) Then
                fso.CreateFolder(tempSfxDataFolder)
            End If
            Dim StreamSamplesList As String() = textFileReaders.GetStreamSoundsList(SysFileSamples)
            'Start resample
            Dim sfxFilesDirectory As String = fso.BuildPath(WorkingDirectory, "SFXs")
            Dim sfxFiles As String() = Directory.GetFiles(sfxFilesDirectory, "*.txt", SearchOption.TopDirectoryOnly)
            Dim maxHashCode As Integer = 0
            For index As Integer = 0 To sfxFiles.Length - 1
                'Check for cancellation
                If BackgroundWorker.CancellationPending Then
                    e.Cancel = True
                    Exit For
                Else
                    'Update title bar
                    Dim currentSampleName As String = GetOnlyFileName(sfxFiles(index))
                    Invoke(Sub() Text = "Creating SFX_Data.h " & currentSampleName)
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
                                    waveLooping = WaveFunctions.ReadSampleChunk(waveReader)
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
                            Dim sampleIsStreamed = 0
                            Dim absFilePath As String = sfxFile.Samples(0).FilePath
                            If Not absFilePath.StartsWith("\") Then
                                absFilePath = "\" + sfxFile.Samples(0).FilePath
                            End If
                            If Array.IndexOf(StreamSamplesList, absFilePath) <> -1 Then
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
                    'Update progress bar
                    BackgroundWorker.ReportProgress(index + 1)
                End If
            Next

            'Liberate Memmory
            Erase sfxFiles

            'Merge all files
            Dim hastableBuilder As New SfxDefines
            hastableBuilder.CreateSfxData(fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Data.h"), tempSfxDataFolder, maxHashCode)
        End If
    End Sub
End Class

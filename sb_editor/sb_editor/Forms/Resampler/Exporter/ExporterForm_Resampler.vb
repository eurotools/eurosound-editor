Imports System.ComponentModel
Imports System.IO
Imports EngineXMarkersTool
Imports ESUtils.MusXBuild_StreamFile
Imports NAudio.Wave

Partial Public Class ExporterForm
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    ReadOnly waveFunctions As New WaveFunctions
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly textFileWritters As New FileWriters

    '*===============================================================================================
    '* MAIN METHOD
    '*===============================================================================================
    Private Sub ResampleWaves(soundsTable As DataTable, e As DoWorkEventArgs)
        'Get Wave files to include
        Dim samplesCount As Integer = soundsTable.Rows.Count - 1

        'Ensure that we have files to resample
        If samplesCount > 0 Then
            'Update progress bar
            Invoke(Sub() ProgressBar1.Maximum = samplesCount + 1)
            Invoke(Sub() ProgressBar1.Value = 0)

            'Start resample
            For index As Integer = 0 To samplesCount
                'Check for cancellation
                If BackgroundWorker.CancellationPending Then
                    e.Cancel = True
                    Exit For
                Else
                    'Check if this sample needs to be resampled
                    If StrComp(soundsTable.Rows(index).Item(4), "True") = 0 Then
                        'Get waveName
                        Dim waveRelFilePath As String = soundsTable.Rows(index).Item(0)
                        Dim waveRelDirectoryPath As String = fso.GetParentFolderName(waveRelFilePath)
                        'Get Wave full path
                        Dim waveMasterPath = fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master" & waveRelFilePath)
                        Dim WaveName As String = GetOnlyFileName(waveMasterPath)
                        'Resample for each platform
                        For Each outPlatform As String In outPlatforms
                            'Check for cancellation
                            If BackgroundWorker.CancellationPending Then
                                e.Cancel = True
                                Exit For
                            Else
                                'Output folder Path
                                Dim outputFolder As String = fso.BuildPath(WorkingDirectory, outPlatform)
                                If Not fso.FolderExists(outputFolder) Then
                                    MkDir(outputFolder)
                                End If
                                'Get sample rate
                                Dim waveRate As Integer = ProjectSettingsFile.sampleRateFormats(outPlatform)(soundsTable.Rows(index).Item(1))
                                'Get final output path and resample with SoX
                                Dim outputFilePath As String = fso.BuildPath(outputFolder, waveRelFilePath)
                                RunProcess("SystemFiles\Sox.exe", """" & waveMasterPath & """ -r " & waveRate & " """ & outputFilePath & """")
                                'Specific platform formats
                                If StrComp(outPlatform, "PC") = 0 Then
                                    'IMA ADPCM
                                    If StrComp(soundsTable.Rows(index).Item(5), "True") = 0 Then
                                        CreateSoftwareAdpcm(outPlatform, waveMasterPath, waveRelDirectoryPath, waveRate)
                                    End If
                                End If
                                If StrComp(outPlatform, "PlayStation2") = 0 Then
                                    ResampleForPlayStation2(soundsTable.Rows(index), waveMasterPath, waveRelDirectoryPath, outputFilePath, WaveName)
                                End If
                                If StrComp(outPlatform, "GameCube") = 0 Then
                                    ResampleForGameCube(soundsTable.Rows(index), waveMasterPath, waveRelDirectoryPath, outputFilePath, WaveName)
                                    'IMA ADPCM
                                    If StrComp(soundsTable.Rows(index).Item(5), "True") = 0 Then
                                        CreateSoftwareAdpcm(outPlatform, waveMasterPath, waveRelDirectoryPath, waveRate)
                                    End If
                                End If
                                If StrComp(outPlatform, "X Box") = 0 Then
                                    ResampleForXbox(waveRelDirectoryPath, outputFilePath, WaveName)
                                End If

                                'Update form title
                                Try
                                    Invoke(Sub() Text = "ReSampling: " & waveRelFilePath & " for: " & outPlatform)
                                Catch ex As Exception
                                    e.Cancel = True
                                    Exit For
                                End Try

                                'Update progress bar
                                BackgroundWorker.ReportProgress(index + 1)
                            End If
                        Next
                        'Update Property
                        soundsTable.Rows(index).Item(4) = "False"
                    End If
                End If
            Next

            'Update text file
            textFileWritters.SaveSamplesFile(SysFileSamples, soundsTable)
        End If
    End Sub

    '*===============================================================================================
    '* PLAYSTATION RESAMPLE FUNCTIONS
    '*===============================================================================================
    Private Sub ResampleForPlayStation2(streamSamplesList As DataRow, waveMasterPath As String, waveRelDirectoryPath As String, outputFilePath As String, WaveName As String)
        'Get Wave output folder
        Dim playStationfullOutputFolder = fso.BuildPath(WorkingDirectory & "\PlayStation2_VAG\", waveRelDirectoryPath)
        If Not fso.FolderExists(playStationfullOutputFolder) Then
            MkDir(playStationfullOutputFolder)
        End If

        Dim playStationOutputFilePath As String = fso.BuildPath(playStationfullOutputFolder, WaveName & ".vag")

        'Read Sample Loop Info
        Dim waveReader As New WaveFileReader(waveMasterPath)
        Dim loopInfo As Integer() = waveFunctions.ReadSampleChunk(waveReader)

        'Calculate Loop offset 
        Dim vagToolArgs As String
        If loopInfo(0) = 1 And StrComp(streamSamplesList.Item(5), "False") = 0 Then
            'Loop offset pos in the resampled wave
            Dim parsedWaveReader As New WaveFileReader(outputFilePath)
            Dim parsedLoop As UInteger = (loopInfo(1) / (waveReader.Length / parsedWaveReader.Length)) * 2
            parsedWaveReader.Close()
            'Vag pos
            Dim loopOffsetVag As UInteger = ((parsedLoop / 28 + (If(((parsedLoop Mod 28) <> 0), 2, 1))) / 2) - 1
            vagToolArgs = """" & outputFilePath & """ """ & playStationOutputFilePath & """ -l" & loopOffsetVag
        Else
            vagToolArgs = """" & outputFilePath & """ """ & playStationOutputFilePath & """"
        End If

        'Close reader
        waveReader.Close()

        'Execute Vag Tool
        RunProcess("SystemFiles\VagCodec.exe", vagToolArgs)
    End Sub

    '*===============================================================================================
    '* GAMECUBE RESAMPLE FUNCTIONS
    '*===============================================================================================
    Private Sub ResampleForGameCube(streamSamplesList As DataRow, waveMasterPath As String, waveRelDirectoryPath As String, outputFilePath As String, WaveName As String)
        'Get Wave output folder
        Dim gameCubefullOutputFolder = fso.BuildPath(WorkingDirectory & "\GameCube_dsp_adpcm\", waveRelDirectoryPath)
        If Not fso.FolderExists(gameCubefullOutputFolder) Then
            MkDir(gameCubefullOutputFolder)
        End If

        Dim gameCubeOutputFilePath As String = fso.BuildPath(gameCubefullOutputFolder, WaveName & ".dsp")

        'Get loop info
        Dim waveReader As New WaveFileReader(waveMasterPath)
        Dim loopInfo As Integer() = waveFunctions.ReadSampleChunk(waveReader)

        'Calculate Loop offset 
        Dim dspToolArgs As String
        If loopInfo(0) = 1 And StrComp(streamSamplesList.Item(5), "True") = 0 Then
            'Loop offset pos in the resampled wave
            Dim parsedWaveReader As New WaveFileReader(outputFilePath)
            Dim parsedLoop As UInteger = (loopInfo(1) / (waveReader.Length / parsedWaveReader.Length)) * 2
            parsedWaveReader.Close()

            dspToolArgs = "Encode """ & outputFilePath & """ """ & gameCubeOutputFilePath & """ -L " & parsedLoop
        Else
            dspToolArgs = "Encode """ & outputFilePath & """ """ & gameCubeOutputFilePath & """"
        End If

        'Close reader
        waveReader.Close()

        'Execute Dsp Adpcm Tool
        RunProcess("SystemFiles\DspCodec.exe", dspToolArgs)

    End Sub

    '*===============================================================================================
    '* XBOX RESAMPLE FUNCTIONS
    '*===============================================================================================
    Private Sub ResampleForXbox(waveRelDirectoryPath As String, outputFilePath As String, WaveName As String)
        'Get Wave output folder
        Dim XboxOutputfullOutputFolder = fso.BuildPath(WorkingDirectory & "\XBox_adpcm\", waveRelDirectoryPath)
        If Not fso.FolderExists(XboxOutputfullOutputFolder) Then
            MkDir(XboxOutputfullOutputFolder)
        End If

        Dim xboxOutputFilePath As String = fso.BuildPath(XboxOutputfullOutputFolder, WaveName & ".adpcm")

        'Execute Dsp Adpcm Tool
        RunProcess("SystemFiles\XboxCodec.exe", "Encode """ & outputFilePath & """ """ & xboxOutputFilePath & """")
    End Sub

    '*===============================================================================================
    '* SOFTWARE ADPCM FUNCTIONS
    '*===============================================================================================
    Private Sub CreateSoftwareAdpcm(outPlatform As String, waveMasterPath As String, waveRelDirectoryPath As String, sampleRate As Integer)
        'Output to save converted data
        Dim BaseOutputFolder = fso.BuildPath(WorkingDirectory, outPlatform & "_Software_adpcm")
        Dim fullOutputFolder = BaseOutputFolder & waveRelDirectoryPath
        Dim outFilepath As String = fso.BuildPath(fullOutputFolder, GetOnlyFileName(waveMasterPath))
        Dim outFilePathWav As String = outFilepath & ".smd"
        Dim outFilePathIma As String = outFilepath & ".ssp"

        'Create directory if not exists
        If Not fso.FolderExists(fullOutputFolder) Then
            MkDir(fullOutputFolder)
        End If

        'Resampled wav
        RunProcess("SystemFiles\Sox.exe", """" & waveMasterPath & """ -t raw -r " & sampleRate & " -c 1 -s """ & outFilePathWav & """")
        'Wave to ima
        RunProcess("SystemFiles\Sox.exe", "-t raw -w -s -r " & sampleRate & " -c 1 """ & outFilePathWav & """ -t ima """ & outFilePathIma & """")
    End Sub

    '*===============================================================================================
    '* GENERATE STREAMS FOLDERS
    '*===============================================================================================
    Private Sub GenerateStreamFolder(resampleForPlaforms As String(), fileList As String(), e As DoWorkEventArgs)
        'Call markers lib
        Dim markersTool As New ExMarkersTool

        'Create a folder for each platform
        For Each outPlatform As String In resampleForPlaforms
            'Check destination platform
            If StrComp(outPlatform, "PC") = 0 Or StrComp(outPlatform, "GameCube") = 0 Then
                'Get samples directory
                Dim streamsBaseDir As String = fso.BuildPath(WorkingDirectory, outPlatform & "_Software_adpcm")
                CopyStreamSamples(streamsBaseDir, outPlatform, fileList, ".ssp", e, markersTool)
            End If
            If StrComp(outPlatform, "PlayStation2") = 0 Then
                'Get samples directory
                Dim streamsBaseDir As String = fso.BuildPath(WorkingDirectory, "PlayStation2_VAG")
                CopyStreamSamples(streamsBaseDir, outPlatform, fileList, ".vag", e, markersTool)
            End If
            If StrComp(outPlatform, "X Box") = 0 Then
                'Get Samples directory
                Dim streamsBaseDir As String = fso.BuildPath(WorkingDirectory, "XBox_adpcm")
                CopyStreamSamples(streamsBaseDir, outPlatform, fileList, ".adpcm", e, markersTool)
            End If
        Next
    End Sub

    Private Sub CopyStreamSamples(streamsBaseDir As String, outPlatform As String, fileList As String(), fileExtension As String, e As DoWorkEventArgs, markersFunctions As ExMarkersTool)
        'Get sample count
        Dim samplesCount As Integer = fileList.GetLength(0) - 1
        Dim waveFunctions As New WaveFunctions
        Dim markerFileFunctions As New MarkerFiles

        'Update progress bar
        Invoke(Sub() ProgressBar1.Value = 0)
        Invoke(Sub() ProgressBar1.Maximum = samplesCount + 1)

        'Get directory
        Dim streamsFolder As String = fso.BuildPath(WorkingDirectory, outPlatform & "_Streams\English")
        If Not fso.FolderExists(streamsFolder) Then
            MkDir(streamsFolder)
        End If

        'Move files to stream folder
        For index As Integer = 0 To samplesCount
            'Check for cancellation
            If BackgroundWorker.CancellationPending Then
                e.Cancel = True
                Exit For
            Else
                Dim waveName As String = fileList(index)
                Dim platformWaveFile = fso.BuildPath(WorkingDirectory, outPlatform & waveName)

                'Update form title
                Invoke(Sub() Text = "Binding English Audio Stream Data: " & waveName & " for: " & outPlatform)

                'Update progress bar
                BackgroundWorker.ReportProgress(index + 1)

                'Get IMA ADPCM file path 
                Dim waveRelDirectoryPath As String = fso.GetParentFolderName(waveName)
                Dim streamFilePath As String = fso.BuildPath(streamsBaseDir & waveRelDirectoryPath, GetOnlyFileName(waveName) & fileExtension)

                'Ensure that the file exists
                If fso.FileExists(streamFilePath) Then
                    'ADPCM File
                    Dim adpcmFile = fso.BuildPath(streamsFolder, "STR_" & index & ".ssd")
                    FileCopy(streamFilePath, adpcmFile)
                    'Marker File
                    Dim MasterWaveFilePath = fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master" & waveName)
                    Dim MasterMarkerFilePath = fso.BuildPath(fso.GetParentFolderName(MasterWaveFilePath), GetOnlyFileName(MasterWaveFilePath) & ".mrk")
                    'Ensure that the marker file exists
                    If Not fso.FileExists(MasterMarkerFilePath) Then
                        Using waveReader As New WaveFileReader(MasterWaveFilePath)
                            Dim sampleChunkData As Integer() = waveFunctions.ReadSampleChunk(waveReader)
                            markerFileFunctions.CreateStreamMarkerFile(MasterMarkerFilePath, sampleChunkData, waveReader.Length / 2)
                        End Using
                    End If
                    markersFunctions.CreateStreamMarkers(adpcmFile, MasterMarkerFilePath, streamsFolder, platformWaveFile, 100)
                End If
            End If
        Next
    End Sub

    Private Sub GenerateStreamFile(resampleForPlaforms As String(), streamslist As String(), e As DoWorkEventArgs)
        'Get items count
        Dim streamsCount = streamslist.Length - 1

        'Resample for each platform
        For Each outPlatform As String In resampleForPlaforms
            'Check for cancellation
            If BackgroundWorker.CancellationPending Then
                e.Cancel = True
                Exit For
            Else
                'Update form title
                Invoke(Sub() Text = "Outputting: STREAMS.bin for: " & outPlatform)

                'Get platform 
                Dim streamsFolder As String = fso.BuildPath(WorkingDirectory, outPlatform & "_Streams\English")

                'Check that all required files exits
                Dim AllFilesExist As Boolean = True
                For index As Integer = 0 To streamsCount
                    'Get files Path
                    Dim adpcmFile As String = fso.BuildPath(streamsFolder, "STR_" & index & ".ssd")
                    Dim markerFile As String = fso.BuildPath(streamsFolder, "STR_" & index & ".smf")
                    'Check if ADPCM File exists
                    If Not fso.FileExists(adpcmFile) Then
                        'Update boolan
                        AllFilesExist = False
                        'Inform User
                        Invoke(Sub() MsgBox("Error, file not found: " & adpcmFile, vbCritical + vbOKOnly, "Error"))
                        'Exit loop
                        Exit For
                    End If
                    'Check if Markers file exists
                    If Not fso.FileExists(markerFile) Then
                        'Update boolan
                        AllFilesExist = False
                        'Inform User
                        Invoke(Sub() MsgBox("Error, file not found: " & markerFile, vbCritical + vbOKOnly, "Error"))
                        'Exit loop
                        Exit For
                    End If
                Next

                'Check if we have all files ready
                If AllFilesExist Then
                    'Stream paths and filenames
                    Dim outputFolder = Path.Combine(WorkingDirectory, "TempOutputFolder", outPlatform, "English", "Streams")
                    Dim fullDirPath = Path.Combine(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary", GetEngineXFolder(outPlatform), GetEngineXLangFolder(DefaultLanguage))
                    'Create Stream File
                    BuildTemporalFile(streamsCount, streamsFolder, outputFolder)
                    Dim fileName As String = "HC" & Hex(GetSfxFileName(0, &HFFFF)).PadLeft(6, "0"c)
                    If StrComp(outPlatform, "GameCube") = 0 Then
                        BuildStreamFile(fso.BuildPath(outputFolder, "STREAMS.bin"), fso.BuildPath(outputFolder, "STREAMS.lut"), fso.BuildPath(outputFolder, fileName & ".SFX"), True)
                    Else
                        BuildStreamFile(fso.BuildPath(outputFolder, "STREAMS.bin"), fso.BuildPath(outputFolder, "STREAMS.lut"), fso.BuildPath(outputFolder, fileName & ".SFX"), False)
                    End If
                End If
            End If
        Next
    End Sub
End Class

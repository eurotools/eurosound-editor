Imports System.IO
Imports EngineXMarkersTool
Imports ESUtils.MusXBuild_MusicFile
Imports sb_editor.HashTablesBuilder
Imports sb_editor.ReaderClasses

Partial Public Class MusicsExporter
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly outputQueue As DataTable
    Private ReadOnly outputPlatforms As String()
    Private ReadOnly MarkerFileOnly As Boolean
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly parentMusicForm As MusicMaker
    Private ReadOnly hashTablesFunctions As New MfxDefines
    Private ReadOnly hashCodesCollection As SortedDictionary(Of String, UInteger)
    Private canCloseForm As Boolean = False

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(outputFileList As DataTable, outputFormats As String(), onlyMarkerFile As Boolean, parentForm As MusicMaker, hashCodesDict As SortedDictionary(Of String, UInteger))
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        outputQueue = outputFileList
        outputPlatforms = outputFormats
        MarkerFileOnly = onlyMarkerFile
        parentMusicForm = parentForm
        hashCodesCollection = hashCodesDict

        'Custom cursors
        Cursor = New Cursor(New MemoryStream(My.Resources.ChristmasTree))
    End Sub

    Private Sub MusicsExporter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Hide parent form
        parentMusicForm.Hide()

        'Start process
        If Not BackgroundWorker.IsBusy Then
            BackgroundWorker.RunWorkerAsync()
        End If
    End Sub

    '*===============================================================================================
    '* BACKGROUND WORKER EVENTS
    '*===============================================================================================
    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
        'Update form title
        Invoke(Sub() Text = "Waiting")

        'Call libs
        Dim markersFunctions As New ExMarkersTool

        'Create ESWork folder if required
        Dim waveOutputFolder As String = fso.BuildPath(WorkingDirectory, "Music\ESWork")
        If Not fso.FolderExists(waveOutputFolder) Then
            MkDir(waveOutputFolder)
        End If

        'Calculate execution time
        Dim watch = Stopwatch.StartNew

        'Create Music Stream (.ssd)
        Invoke(Sub() ProgressBar1.Maximum = outputQueue.Rows.Count * outputPlatforms.Length)
        Invoke(Sub() ProgressBar1.Value = 0)
        Dim counter = 0
        If Not MarkerFileOnly Then
            For Each musicItem As DataRow In outputQueue.Rows
                'Get music files path
                Dim waveFilePath As String = fso.BuildPath(WorkingDirectory, "Music\" & musicItem.ItemArray(0) & ".wav")
                'Split Wave channels with SoX (PC & GC)
                For index As Integer = 0 To outputPlatforms.Length - 1
                    'Get the current platform
                    Dim musicHashCode As Integer = musicItem.ItemArray(2)
                    Dim currentPlatform As String = outputPlatforms(index)
                    Dim soundSampleData As String = fso.BuildPath(GetOutputFolder(musicHashCode, currentPlatform), "MFX_" & musicHashCode & ".ssd")
                    Invoke(Sub() Text = "Making Music Stream: " & musicItem.ItemArray(0) & " for " & currentPlatform)
                    If StrComp(currentPlatform, "PC") = 0 Or StrComp(currentPlatform, "GameCube") = 0 Then
                        Dim PcOutLeft As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_L.ima"
                        Dim PcOutRight As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_R.ima"
                        'Split channels
                        RunProcess("SystemFiles\Sox.exe", """" & waveFilePath & """ -t ima -r 32000 """ & PcOutLeft & """ remix 1")
                        RunProcess("SystemFiles\Sox.exe", """" & waveFilePath & """ -t ima -r 32000 """ & PcOutRight & """ remix 2")
                        'Music Stream (.ssd)
                        MergeChannels(PcOutLeft, PcOutRight, 1, soundSampleData)
                    End If
                    If StrComp(currentPlatform, "PlayStation2") = 0 Then
                        Dim Ps2OutLeft As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_L.wav"
                        Dim Ps2OutRight As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_R.wav"
                        'Split channels
                        RunProcess("SystemFiles\Sox.exe", """" & waveFilePath & """ -t wav -r 32000 """ & Ps2OutLeft & """ remix 1")
                        RunProcess("SystemFiles\Sox.exe", """" & waveFilePath & """ -t wav -r 32000 """ & Ps2OutRight & """ remix 2")
                        'Vag Tool
                        Dim ps2VagL As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_L.vag"
                        Dim ps2VagR As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_R.vag"
                        RunProcess("SystemFiles\VagCodec.exe", """" & Ps2OutLeft & """ """ & ps2VagL & """")
                        RunProcess("SystemFiles\VagCodec.exe", """" & Ps2OutRight & """ """ & ps2VagR & """")
                        'Music Stream (.ssd)
                        MergeChannels(ps2VagL, ps2VagR, 128, soundSampleData)
                    End If
                    If StrComp(currentPlatform, "X Box") = 0 Or StrComp(currentPlatform, "Xbox") = 0 Then
                        Dim XboxOutLeft As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_XbL.wav"
                        Dim XboxOutRight As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_XbR.wav"
                        'Split Channels
                        RunProcess("SystemFiles\Sox.exe", """" & waveFilePath & """ -t wav -r 44100 """ & XboxOutLeft & """ remix 1")
                        RunProcess("SystemFiles\Sox.exe", """" & waveFilePath & """ -t wav -r 44100 """ & XboxOutRight & """ remix 2")
                        'Xbox Tool
                        Dim xbxVagL As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_L.adpcm"
                        Dim xbxVagR As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_R.adpcm"
                        RunProcess("SystemFiles\XboxCodec.exe", "Encode """ & XboxOutLeft & """ """ & xbxVagL & """")
                        RunProcess("SystemFiles\XboxCodec.exe", "Encode """ & XboxOutRight & """ """ & xbxVagR & """")
                        MergeChannels(xbxVagL, xbxVagR, 4, soundSampleData)
                    End If
                    'Update progress bar
                    counter += 1
                    BackgroundWorker.ReportProgress(counter)
                Next
            Next
        End If

        'Create Marker Files (.smf)
        Invoke(Sub() ProgressBar1.Value = 0)
        counter = 0
        For Each musicItem As DataRow In outputQueue.Rows
            Invoke(Sub() Text = "Making Marker File: " & musicItem.ItemArray(0))
            For index As Integer = 0 To outputPlatforms.Length - 1
                'Get the current platform
                Dim currentPlatform As String = outputPlatforms(index)
                Dim musicHashCode As Integer = musicItem.ItemArray(2)
                'Get file data and output path
                Dim outputFilePath As String = GetOutputFolder(musicHashCode, currentPlatform)
                'Get Common files paths and create the jump maker file
                Dim jumpMarkersFile As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0) & ".jmp")
                Dim soundMarkerFile As String = fso.BuildPath(outputFilePath, "MFX_" & musicHashCode & ".smf")
                'Get Marker file path
                Dim mrkFilePath As String = fso.BuildPath(WorkingDirectory, "Music\" & musicItem.ItemArray(0) & ".mrk")
                If StrComp(currentPlatform, "PC") = 0 Or StrComp(currentPlatform, "GameCube") = 0 Then
                    'Get Ima files
                    Dim PcOutLeft As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_L.ima"
                    Dim PcOutRight As String = fso.BuildPath(waveOutputFolder, musicItem.ItemArray(0)) & "_R.ima"
                    markersFunctions.CreateMusicMarkers(PcOutLeft, PcOutRight, mrkFilePath, jumpMarkersFile, soundMarkerFile, currentPlatform, musicItem.ItemArray(1))
                Else
                    markersFunctions.CreateMusicMarkers(Nothing, Nothing, mrkFilePath, jumpMarkersFile, soundMarkerFile, currentPlatform, musicItem.ItemArray(1))
                End If
                'Update progress bar
                counter += 1
                BackgroundWorker.ReportProgress(counter)
            Next
        Next

        'Create MusX Files
        Invoke(Sub() ProgressBar1.Value = 0)
        counter = 0
        'Read properties file
        Dim propsFile = textFileReaders.ReadPropertiesFile(SysFileProperties)
        'Update listview
        For Each musicItem As DataRow In outputQueue.Rows
            Invoke(Sub() Text = "Binding Files: " & musicItem.ItemArray(0))
            For index As Integer = 0 To outputPlatforms.Length - 1
                Dim musicHashCode As Integer = musicItem.ItemArray(2)
                'Get the current platform
                Dim currentPlatform As String = outputPlatforms(index)
                'Get file data and output path
                Dim outputFilePath As String = GetOutputFolder(musicHashCode, currentPlatform)
                'Get output file paths
                Dim soundMarkerFile As String = fso.BuildPath(outputFilePath, "MFX_" & musicHashCode & ".smf")
                Dim soundSampleData As String = fso.BuildPath(outputFilePath, "MFX_" & musicHashCode & ".ssd")
                Dim musxFilename As String = "HCE" & Hex(musicHashCode).PadLeft(5, "0"c) & ".SFX"
                'Create final file
                Dim folderPath = fso.BuildPath(propsFile.MiscProps.EngineXFolder, "Binary\" & GetEngineXFolder(currentPlatform) & "\music")
                If Not fso.FolderExists(folderPath) Then
                    MkDir(folderPath)
                End If
                Dim fullDirPath = fso.BuildPath(folderPath, musxFilename)
                If StrComp(outputPlatforms(index), "GameCube") = 0 Then
                    BuildMusicFile(soundMarkerFile, soundSampleData, fullDirPath, musicHashCode, True)
                Else
                    BuildMusicFile(soundMarkerFile, soundSampleData, fullDirPath, musicHashCode, False)
                End If
                'Update progress bar
                counter += 1
                BackgroundWorker.ReportProgress(counter)
            Next
        Next

        'Create Music HashCodes
        Invoke(Sub() ProgressBar1.Maximum = hashCodesCollection.Count)
        Invoke(Sub() ProgressBar1.Value = 0)
        counter = 0
        Dim musicDefinesFilePath As String = fso.BuildPath(propsFile.MiscProps.HashCodeFileFolder, "MFX_Defines.h")
        hashTablesFunctions.CreateMfxHashTable(musicDefinesFilePath, hashCodesCollection)
        For Each musicItem As KeyValuePair(Of String, UInteger) In hashCodesCollection
            Invoke(Sub() Text = "Appending Jump HashCodes: " & musicItem.Key)
            Dim jumpMarkersFilePath As String = fso.BuildPath(WorkingDirectory, "Music\ESWork\" & musicItem.Key & ".jmp")
            If fso.FileExists(jumpMarkersFilePath) Then
                Dim jumpHashCodes As String() = textFileReaders.ReadJumpFile(jumpMarkersFilePath)
                'Append data
                FileOpen(1, musicDefinesFilePath, OpenMode.Append)
                PrintLine(1, "")
                PrintLine(1, "// Music Jump Codes For Level MFX_" & musicItem.Key)
                For jumpHashCode As Integer = 0 To jumpHashCodes.Length - 1
                    Dim mfxHashCode As Short = musicItem.Value
                    Dim hashCode As UInteger = ((&H1BE And &HFFF) << 20) Or ((jumpHashCode And &HFF) << 8) Or ((mfxHashCode And &HFF) << 0)
                    PrintLine(1, "#define " & "JMP_" & jumpHashCodes(jumpHashCode) & " 0x" & Hex(hashCode))
                Next
                FileClose(1)
            End If

            'Update progress bar
            counter += 1
            BackgroundWorker.ReportProgress(counter)
        Next

        'Create MFX Data
        Dim dataDictionary As Dictionary(Of UInteger, String()) = GetMfxDataDict()
        Dim musicDataFilePath As String = fso.BuildPath(propsFile.MiscProps.HashCodeFileFolder, "MFX_Data.h")
        hashTablesFunctions.CreateMfxData(musicDataFilePath, dataDictionary)

        'Create Valid list
        Invoke(Sub() ProgressBar1.Maximum = hashCodesCollection.Count)
        Invoke(Sub() ProgressBar1.Value = 0)
        counter = 0
        Dim musicValidListFilePath As String = fso.BuildPath(propsFile.MiscProps.HashCodeFileFolder, "MFX_ValidList.h")
        Dim jumpHashCodesDictionary As New Dictionary(Of UInteger, String)
        For Each musicItem As KeyValuePair(Of String, UInteger) In hashCodesCollection
            Invoke(Sub() Text = "Creating MFX Valid List: " & musicItem.Key)
            Dim jumpMarkersFilePath As String = fso.BuildPath(WorkingDirectory, "Music\ESWork\" & musicItem.Key & ".jmp")
            If fso.FileExists(jumpMarkersFilePath) Then
                Dim jumpHashCodes As String() = textFileReaders.ReadJumpFile(jumpMarkersFilePath)
                For jumpHashCode As Integer = 0 To jumpHashCodes.Length - 1
                    Dim mfxHashCode As Short = musicItem.Value
                    Dim hashCode As UInteger = ((jumpHashCode And &HFF) << 8) Or ((mfxHashCode And &HFF) << 0)
                    jumpHashCodesDictionary.Add(hashCode, "JMP_" & jumpHashCodes(jumpHashCode))
                Next
            End If
        Next
        hashTablesFunctions.CreateMfxValidList(musicValidListFilePath, jumpHashCodesDictionary)

        'Get Output time
        watch.Stop()
        Invoke(Sub() parentMusicForm.TextBox_OutputTime.Text = "Output Time = " & watch.ElapsedTicks)
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
        'Show parent form
        parentMusicForm.Show()
        'Close task form
        canCloseForm = True
        Close()
    End Sub

    Private Sub MusicsExporter_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not canCloseForm Then
            e.Cancel = True
        End If
    End Sub
End Class
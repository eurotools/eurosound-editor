Imports System.IO
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

        'Create ESWork folder if required
        Dim waveOutputFolder As String = fso.BuildPath(WorkingDirectory, "Music\ESWork")
        CreateFolderIfRequired(waveOutputFolder)

        'Calculate execution time
        Dim watch = Stopwatch.StartNew

        'Create Music Stream (.ssd)
        Invoke(Sub() ProgressBar1.Value = 0)
        CreateMusicStreams(waveOutputFolder)

        'Create Marker Files (.smf)
        Invoke(Sub() ProgressBar1.Value = 0)
        CreateMarkerFiles(waveOutputFolder)

        'Create MusX Files (.SFX)
        Invoke(Sub() ProgressBar1.Value = 0)
        CreateMusXFiles()

        'Create Music HashTables (.h)
        Invoke(Sub() ProgressBar1.Value = 0)
        BuildMusicHashTables()

        'Get Output time
        watch.Stop()
        Invoke(Sub() parentMusicForm.TextBox_OutputTime.Text = "Output Time = " & watch.ElapsedTicks)
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
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
Partial Public Class ExporterForm
    Inherits Frm_TimerForm
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly outPlatforms As String()
    Private ReadOnly outLanguages As String()
    Private ReadOnly quickResample As Boolean
    Private ReadOnly mainFrame As Form
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly textFileWritters As New FileWriters

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(destPlatforms As String(), outputLanguages As String(), fastResample As Boolean)
        'Esta llamada es exigida por el diseñador.
        InitializeComponent()

        'Agregue cualquier inicialización después de la llamada a InitializeComponent().
        outPlatforms = destPlatforms
        quickResample = fastResample
        outLanguages = outputLanguages

        'Get mainframe
        mainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
    End Sub

    Private Sub ExporterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Hide mainform
        mainFrame.Hide()

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

        'Get sound table from the Samples.txt and start resample
        Dim soundsTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)
        ResampleWaves(soundsTable, outPlatforms)

        'Check if we need to resample and rebuild the stream file
        If ReSampleStreams = 1 Then
            Dim streamSamplesList As String() = textFileReaders.GetStreamSoundsList(SysFileSamples)
            GenerateStreamFolder(streamSamplesList, outPlatforms, outLanguages)
        End If

        'Create SFX Data
        CreateSfxDataFolder(soundsTable, e)
    End Sub

    Private Sub CreateFolderIfRequired(destinationFilePath As String)
        If Not fso.FolderExists(destinationFilePath) Then
            MkDir(destinationFilePath)
        End If
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
        'Show mainform
        mainFrame.Show()
        'Close task form
        Close()
    End Sub

    Private Sub ExporterForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            'Check if the background worker is running
            If BackgroundWorker.IsBusy Then
                'Cancel form closing
                e.Cancel = True
                'Ask user what he wants to do
                Dim diagRes As MsgBoxResult = MsgBox("Are you sure you want to cancel this process?", vbQuestion + vbYesNo, "Question")
                If diagRes = MsgBoxResult.Yes Then
                    'Cancell task
                    BackgroundWorker.CancelAsync()
                End If
            End If
        End If
    End Sub
End Class
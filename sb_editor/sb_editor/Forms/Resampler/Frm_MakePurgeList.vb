Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Public Class Frm_MakePurgeList
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private canCloseForm As Boolean = False
    Private filesCount As Integer = 0
    Private ReadOnly mainFrame As ResampleForm

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New()
        'Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        mainFrame = CType(Application.OpenForms("ResampleForm"), ResampleForm)
    End Sub

    Private Sub Frm_MakePurgeList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mainFrame.Hide()
        'Start process
        If Not BackgroundWorker.IsBusy Then
            BackgroundWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub Frm_MakePurgeList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not canCloseForm Then
            e.Cancel = True
        End If
    End Sub

    '*===============================================================================================
    '* BACKGROUND WORKER EVENTS
    '*===============================================================================================
    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
        Dim textFileReaders As New FileParsers
        Dim samplesToPrint As New HashSet(Of String)

        'Get all SFXs
        Dim sfxFilesToInspect As String() = Directory.GetFiles(Path.Combine(WorkingDirectory, "SFXs"), "*.txt")
        Dim outputFilePath As String = Path.Combine(WorkingDirectory, "Report")
        Directory.CreateDirectory(outputFilePath)

        'Get a list of all available formats
        Dim availablePlatforms As New List(Of String)({"Common"})
        availablePlatforms.AddRange(ProjectSettingsFile.sampleRateFormats.Keys.ToArray)

        'Create a list with the used SFXs
        Dim formatsToInspect As String() = availablePlatforms.ToArray
        Dim samplesCount As Integer = sfxFilesToInspect.Length - 1
        For platformIndex As Integer = 0 To formatsToInspect.Length - 1
            Dim currentPlatform As String = availablePlatforms(platformIndex)
            For fileIndex As Integer = 0 To samplesCount
                'Read samples
                Dim sfxFileName As String = Path.GetFileNameWithoutExtension(sfxFilesToInspect(fileIndex))
                'Calculate and report progress
                Dim previousCounts = samplesCount * platformIndex
                BackgroundWorker.ReportProgress(Decimal.Divide(fileIndex + previousCounts, samplesCount * formatsToInspect.Length) * 100.0, "Creating Sample List " & currentPlatform & " " & sfxFileName)
                'Get File Path
                Dim sfxFilePath As String
                If StrComp(currentPlatform, "Common", CompareMethod.Binary) = 0 Then
                    sfxFilePath = Path.Combine(WorkingDirectory, "SFXs", sfxFileName & ".txt")
                Else
                    sfxFilePath = Path.Combine(WorkingDirectory, "SFXs", currentPlatform, sfxFileName & ".txt")
                End If
                'Get samples
                If File.Exists(sfxFilePath) Then
                    Dim sfxData As SfxFile = textFileReaders.ReadSFXFile(sfxFilesToInspect(fileIndex))
                    For sampleIndex As Integer = 0 To sfxData.Samples.Count - 1
                        Dim currentSample = sfxData.Samples(sampleIndex)
                        samplesToPrint.Add("\" & currentSample.FilePath.TrimStart("\"))
                    Next
                End If
            Next
        Next

        'Print items to file
        Using outputFile As New StreamWriter(outputFilePath)
            outputFile.WriteLine("Purged File List Created: 	" & Date.Now.ToString("MM/dd/yyyy") & "	" & Date.Now.ToString("HH:mm:ss"))
            outputFile.WriteLine("")
            outputFile.WriteLine("#PurgedFileList")

            Dim arrayToPrint As String() = samplesToPrint.ToArray
            Array.Sort(arrayToPrint)
            For itemIndex As Integer = 0 To arrayToPrint.Length - 1
                outputFile.WriteLine(arrayToPrint(itemIndex))
            Next
            outputFile.WriteLine("#END")

            filesCount = arrayToPrint.Length - 1
        End Using
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Text = e.UserState
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
        canCloseForm = True
        Hide()
        MsgBox("Found  " & filesCount & " Files", vbOKOnly + vbInformation, "EuroSound")
        Close()
    End Sub
End Class
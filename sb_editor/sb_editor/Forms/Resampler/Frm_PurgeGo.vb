Imports System.IO
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Public Class Frm_PurgeGo
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private canCloseForm As Boolean = False
    Private filesCount As Integer = 0
    Private ReadOnly mainFrame As ResampleForm
    Private ReadOnly writers As New FileWriters

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New()
        'Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        mainFrame = CType(Application.OpenForms("ResampleForm"), ResampleForm)
    End Sub

    Private Sub Frm_PurgeGo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mainFrame.Hide()
        'Start process
        If Not BackgroundWorker.IsBusy Then
            BackgroundWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub Frm_PurgeGo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not canCloseForm Then
            e.Cancel = True
        End If
    End Sub

    '*===============================================================================================
    '* BACKGROUND WORKER EVENTS
    '*===============================================================================================
    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
        Dim textFileReaders As New FileParsers

        'Get output folder
        Dim masterFolderName As String = "Master_Trash_ " & Date.Now.Day & "_" & Date.Now.Month & "_" & Date.Now.Year
        Dim destinationFolder As String = Path.Combine(WorkingDirectory, masterFolderName)
        Directory.CreateDirectory(destinationFolder)

        'Get original sample list
        Dim samplesDataTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)
        Dim samplesArray As String() = New String(samplesDataTable.Rows.Count - 1) {}
        For rowIndex As Integer = 0 To samplesDataTable.Rows.Count - 1
            samplesArray(rowIndex) = samplesDataTable.Rows(rowIndex).ItemArray(0)
        Next

        'Get used samples list
        Dim usedSamples As String() = textFileReaders.ReadPurgeList(Path.Combine(WorkingDirectory, "Report", "Last_Purge.txt"))

        'Check if we need to remove samples
        Dim samplesToRemove As String() = samplesArray.Except(usedSamples).ToArray
        If samplesToRemove.Length > 0 Then
            For sampleIndex As Integer = 0 To samplesToRemove.Length - 1
                Dim relativeFilePath As String = samplesToRemove(sampleIndex)
                If relativeFilePath > "" Then
                    Dim masterFilePath As String = Path.Combine(WorkingDirectory, "Master", relativeFilePath)
                    Dim destinationFilePath As String = Path.Combine(WorkingDirectory, masterFolderName, relativeFilePath)
                    'Calculate and report progress
                    BackgroundWorker.ReportProgress(Decimal.Divide(sampleIndex, samplesToRemove.Length - 1) * 100.0, "Moving Sample: " & masterFilePath & " to " & destinationFilePath)
                    'Move file
                    If File.Exists(masterFilePath) Then
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationFilePath))
                        File.Copy(masterFilePath, destinationFilePath, True)
                        File.Delete(masterFilePath)
                        filesCount += 1
                        For rowIndex As Integer = samplesDataTable.Rows.Count - 1 To 0 Step -1
                            Dim currentRow As DataRow = samplesDataTable.Rows(rowIndex)
                            If StrComp(currentRow.ItemArray(0), relativeFilePath, CompareMethod.Binary) = 0 Then
                                samplesDataTable.Rows(rowIndex).Delete()
                            End If
                        Next
                    End If
                End If
            Next
            samplesDataTable.AcceptChanges()
            'Save changes
            writers.SaveSamplesFile(SysFileSamples, samplesDataTable)
        End If
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Text = e.UserState
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
        canCloseForm = True
        Hide()
        MsgBox("Purged  " & filesCount & " Files.", vbOKOnly + vbInformation, "EuroSound")
        Close()
    End Sub
End Class
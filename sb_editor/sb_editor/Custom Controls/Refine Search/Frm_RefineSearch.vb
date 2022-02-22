Public Class Frm_RefineSearch
    Private Sub Frm_RefineSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Start process
        If Not BackgroundWorker.IsBusy Then
            BackgroundWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork

    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged

    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted

    End Sub
End Class
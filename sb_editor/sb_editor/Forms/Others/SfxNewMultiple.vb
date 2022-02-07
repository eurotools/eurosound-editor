Public Class SfxNewMultiple
    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub SfxNewMultiple_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ListBox_SampleFiles.Items.Count = 0 Then
            Dim diagResult = OpenFile_WaveFiles.ShowDialog
            If diagResult = DialogResult.OK Then
                AddFilesToListbox(OpenFile_WaveFiles.FileNames)
            End If
        End If
    End Sub

    '*===============================================================================================
    '* BUTTON EVENTS
    '*===============================================================================================
    Private Sub Button_Add_Click(sender As Object, e As EventArgs) Handles Button_Add.Click
        Dim diagResult = OpenFile_WaveFiles.ShowDialog
        If diagResult = DialogResult.OK Then
            AddFilesToListbox(OpenFile_WaveFiles.FileNames)
        End If
    End Sub

    Private Sub Button_Remove_Click(sender As Object, e As EventArgs) Handles Button_Remove.Click
        If ListBox_SampleFiles.SelectedIndex <> -1 Then
            Dim selectedItems As ListBox.SelectedObjectCollection = ListBox_SampleFiles.SelectedItems
            For index As Integer = selectedItems.Count - 1 To 0 Step -1
                ListBox_SampleFiles.Items.Remove(selectedItems(index))
            Next
        End If
    End Sub


    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Close()
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Sub AddFilesToListbox(selectedFiles As String())
        'Add items to listbox
        For fileIndex As Integer = 0 To selectedFiles.Length - 1
            Dim currentFilePath As String = selectedFiles(fileIndex)
            'Avoid duplicates
            If Not ListBox_SampleFiles.Items.Contains(currentFilePath) Then
                ListBox_SampleFiles.Items.Add(currentFilePath)
            End If
        Next
        Erase selectedFiles
    End Sub
End Class
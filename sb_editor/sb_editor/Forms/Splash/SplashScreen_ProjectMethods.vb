Imports System.IO

Partial Public NotInheritable Class SplashScreen
    Private Sub LoadDataBases(databasesFolder As String)
        'Get txt files from the folder
        Dim databaseFiles As String() = Directory.GetFiles(databasesFolder, "*.txt", SearchOption.TopDirectoryOnly)
        'Add items to listbox
        mainform.ListBox_DataBases.BeginUpdate()
        For i = 0 To databaseFiles.Length - 1
            Dim DataBaseName = GetOnlyFileName(databaseFiles(i))
            mainform.ListBox_DataBases.Items.Add(DataBaseName)
        Next
        mainform.Label_DataBasesCount.Text = "Total: " & mainform.ListBox_DataBases.Items.Count
        'Disable listbox update mode
        mainform.ListBox_DataBases.EndUpdate()
    End Sub

    Private Sub LoadSoundbanks(projectFolder As String)
        'Get text files full paths
        Dim soundBankFiles As String() = Directory.GetFiles(projectFolder, "*.txt", SearchOption.TopDirectoryOnly)
        'Add items to tree view
        mainform.TreeView_SoundBanks.BeginUpdate()
        For i As Integer = 0 To soundBankFiles.Length - 1
            Dim soundbankData As SoundbankFile = textFileReaders.ReadSoundBankFile(soundBankFiles(i))
            Dim SoundBankNode As TreeNode = mainform.TreeView_SoundBanks.Nodes.Add(CStr(soundbankData.HashCode), GetOnlyFileName(soundBankFiles(i)), 0, 0)
            For index As Integer = 0 To soundbankData.Dependencies.Count - 1
                SoundBankNode.Nodes.Add(soundbankData.Dependencies(index), soundbankData.Dependencies(index), 2, 2)
            Next
            'Add empty node if required
            If SoundBankNode.Nodes.Count = 0 Then
                SoundBankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
            End If
        Next
        mainform.TreeView_SoundBanks.EndUpdate()
        'Update counter label
        mainform.Label_SoundBanksCount.Text = "Total: " & mainform.TreeView_SoundBanks.Nodes.Count
    End Sub
End Class

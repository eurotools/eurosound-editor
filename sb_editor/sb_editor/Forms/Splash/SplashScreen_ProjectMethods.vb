Partial Public NotInheritable Class SplashScreen
    Private Sub LoadDataBases(databasesFolder As String)
        'Add items to listbox
        mainform.ListBox_DataBases.BeginUpdate()
        Dim databaseFile As String = Dir(databasesFolder & "\*.txt")
        Do While databaseFile > ""
            Dim fileNameLength As Integer = Len(databaseFile)
            Dim fileName As String = Microsoft.VisualBasic.Left(databaseFile, fileNameLength - Len(".txt"))
            mainform.ListBox_DataBases.Items.Add(fileName)
            'Get a new item
            databaseFile = Dir()
        Loop
        mainform.Label_DataBasesCount.Text = "Total: " & mainform.ListBox_DataBases.Items.Count
        'Disable listbox update mode
        mainform.ListBox_DataBases.EndUpdate()
    End Sub

    Private Sub LoadSoundbanks(soundBanksFolder As String)
        'Add items to tree view
        mainform.TreeView_SoundBanks.BeginUpdate()
        Dim soundbankFile As String = Dir(soundBanksFolder & "\*.txt")
        Do While soundbankFile > ""
            Dim soundbankData As SoundbankFile = textFileReaders.ReadSoundBankFile(fso.BuildPath(soundBanksFolder, soundbankFile))
            Dim SoundBankNode As TreeNode = mainform.TreeView_SoundBanks.Nodes.Add(CStr(soundbankData.HashCode), GetOnlyFileName(soundbankFile), 0, 0)
            For index As Integer = 0 To soundbankData.Dependencies.Count - 1
                SoundBankNode.Nodes.Add(soundbankData.Dependencies(index), soundbankData.Dependencies(index), 2, 2)
            Next
            'Add empty node if required
            If SoundBankNode.Nodes.Count = 0 Then
                SoundBankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
            End If
            'Get a new file
            soundbankFile = Dir()
        Loop
        mainform.TreeView_SoundBanks.EndUpdate()
        'Update counter label
        mainform.Label_SoundBanksCount.Text = "Total: " & mainform.TreeView_SoundBanks.Nodes.Count
    End Sub
End Class

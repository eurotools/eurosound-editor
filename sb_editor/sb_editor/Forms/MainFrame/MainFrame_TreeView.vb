Partial Public Class MainFrame
    Private Sub DeleteDatabaseFromSoundbank(soundbankNode As TreeNode, fileName As String)
        Dim UpdateSoundbank As Boolean = False
        'Remove node
        For Each sb_database As TreeNode In soundbankNode.Nodes
            If sb_database IsNot Nothing Then
                If StrComp(sb_database.Text, fileName) = 0 Then
                    sb_database.Remove()
                    UpdateSoundbank = True
                End If
            End If
        Next
        'Add Empty Node
        If soundbankNode.Nodes.Count = 0 Then
            soundbankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
        End If
        'Check if we need to update the text file
        If UpdateSoundbank Then
            writers.CreateSoundbankFile(soundbankNode, textFileReaders)
        End If
    End Sub

    Private Sub CreateNewSoundbank(soundbankName As String)
        'Create node
        Dim soundbankNode = TreeView_SoundBanks.Nodes.Add(CStr(SoundBankHashCodeNumber), soundbankName, 0, 0)
        soundbankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
        'Create text file
        writers.CreateSoundbankFile(soundbankNode, textFileReaders)
        'Update global var
        SoundBankHashCodeNumber += 1
        'Sort control
        TreeView_SoundBanks.Sort()
        'Expand node
        soundbankNode.Expand()
        'Seect node
        TreeView_SoundBanks.SelectedNode = soundbankNode
    End Sub

    Private Sub CopySoundbank(soundbankName As String, sourceSoundbankNode As TreeNode)
        'Node Clonation 
        Dim nodeToAdd As TreeNode = sourceSoundbankNode.Clone
        nodeToAdd.Text = soundbankName
        nodeToAdd.Name = SoundBankHashCodeNumber
        TreeView_SoundBanks.Nodes.Add(nodeToAdd)
        'Create text file
        writers.CreateSoundbankFile(nodeToAdd, textFileReaders)
        'Update global var
        SoundBankHashCodeNumber += 1
        'Sort control
        TreeView_SoundBanks.Sort()
        'Expand node
        nodeToAdd.Expand()
        'Seect node
        TreeView_SoundBanks.SelectedNode = nodeToAdd
    End Sub

    Private Sub AddDatabaseToSoundbank(dataBasesToAdd As ListBox.SelectedObjectCollection, soundBank As TreeNode)
        'Variables
        Dim sortTreeView As Boolean = False

        'Delete empty node if exists
        If soundBank.Nodes.Count = 1 Then
            If StrComp(soundBank.Nodes(0).Name, "Empty") = 0 Then
                soundBank.Nodes(0).Remove()
            End If
        End If

        'Add new nodes
        For Each database In dataBasesToAdd
            'Add databases
            soundBank.Nodes.Add(database, database, 2, 2)
            'Update boolean
            sortTreeView = True
        Next

        'Sort nodes
        If sortTreeView = True Then
            'Sort control
            TreeView_SoundBanks.Sort()
            'Select soundbank again
            TreeView_SoundBanks.SelectedNode = soundBank
        End If

        'Update text file
        writers.CreateSoundbankFile(soundBank, textFileReaders)
    End Sub
End Class

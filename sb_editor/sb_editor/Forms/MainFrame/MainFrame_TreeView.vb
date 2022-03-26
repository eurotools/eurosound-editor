Imports System.IO
Imports sb_editor.ParsersObjects

Partial Public Class MainFrame
    Private Sub DeleteDatabaseFromSoundbank(soundbankNode As TreeNode, fileName As String)
        'Remove node
        For Each sb_database As TreeNode In soundbankNode.Nodes
            If sb_database IsNot Nothing Then
                If sb_database.Text.Equals(fileName, StringComparison.OrdinalIgnoreCase) Then
                    sb_database.Remove()
                End If
            End If
        Next
        'Add Empty Node
        If soundbankNode.Nodes.Count = 0 Then
            soundbankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
        End If
    End Sub

    Private Sub CreateNewSoundbank(soundbankName As String)
        'Create node
        Dim soundbankNode = TreeView_SoundBanks.Nodes.Add(CStr(SoundBankHashCodeNumber), soundbankName, 0, 0)
        soundbankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
        'Create SoundbankObject
        Dim soundbankObj As New SoundbankFile With {
            .HashCode = SoundBankHashCodeNumber
        }
        writers.UpdateSoundbankFile(soundbankObj, Path.Combine(WorkingDirectory, "Soundbanks", soundbankName & ".txt"), textFileReaders)
        'Update global var
        SoundBankHashCodeNumber += 1
        writers.UpdateMiscFile(Path.Combine(WorkingDirectory, "System", "Misc.txt"))
        'Sort control
        TreeView_SoundBanks.Sort()
        'Expand node
        soundbankNode.Expand()
        'Seect node
        TreeView_SoundBanks.SelectedNode = soundbankNode
        'Update label
        Label_SoundBanksCount.Text = "Total: " & TreeView_SoundBanks.Nodes.Count
    End Sub

    Private Sub CopySoundbank(soundbankName As String, sourceSoundbankNode As TreeNode)
        'Node Clonation 
        Dim nodeToAdd As TreeNode = sourceSoundbankNode.Clone
        nodeToAdd.Text = soundbankName
        nodeToAdd.Name = SoundBankHashCodeNumber
        TreeView_SoundBanks.Nodes.Add(nodeToAdd)

        'Create text file
        Dim soundbankFileData As SoundbankFile = textFileReaders.ReadSoundBankFile(Path.Combine(WorkingDirectory, "Soundbanks", sourceSoundbankNode.Text & ".txt"))
        soundbankFileData.HashCode = SoundBankHashCodeNumber
        writers.UpdateSoundbankFile(soundbankFileData, Path.Combine(WorkingDirectory, "Soundbanks", soundbankName & ".txt"), textFileReaders)

        'Update global var
        SoundBankHashCodeNumber += 1
        writers.UpdateMiscFile(Path.Combine(WorkingDirectory, "System", "Misc.txt"))

        'Sort control
        TreeView_SoundBanks.Sort()

        'Expand node
        nodeToAdd.Expand()

        'Select node and update label
        TreeView_SoundBanks.SelectedNode = nodeToAdd
        Label_SoundBanksCount.Text = "Total: " & TreeView_SoundBanks.Nodes.Count
    End Sub

    Private Sub AddDatabaseToSoundbank(dataBasesToAdd As String(), soundBank As TreeNode)
        Dim soundbankFilePath As String = Path.Combine(WorkingDirectory, "Soundbanks", soundBank.Text & ".txt")
        If File.Exists(soundbankFilePath) Then
            'Read file data 
            Dim soundbankData As SoundbankFile = textFileReaders.ReadSoundBankFile(soundbankFilePath)
            If soundbankData IsNot Nothing Then
                'Delete empty node if exists
                If soundBank.Nodes.Count = 1 Then
                    If soundBank.Nodes(0).Name.Equals("Empty", StringComparison.OrdinalIgnoreCase) Then
                        soundBank.Nodes(0).Remove()
                    End If
                End If

                'Get new items to add
                Dim itemsToAdd As List(Of String) = dataBasesToAdd.Except(soundbankData.Dependencies).ToList
                If itemsToAdd.Count > 0 Then
                    'Add new nodes
                    For Each database In itemsToAdd
                        'Add databases
                        soundBank.Nodes.Add(database, database, 2, 2)
                    Next

                    'Sort control and select soundbank
                    TreeView_SoundBanks.Sort()
                    TreeView_SoundBanks.SelectedNode = soundBank

                    'Add items to file data
                    itemsToAdd.AddRange(soundbankData.Dependencies)
                    itemsToAdd.Sort()
                    soundbankData.Dependencies = itemsToAdd.ToArray

                    'Update text file
                    writers.UpdateSoundbankFile(soundbankData, soundbankFilePath, textFileReaders)
                End If
            End If
        End If
    End Sub

    Private Function GetSoundbankNode(currentSelectedNode As TreeNode) As TreeNode
        'Get parent if the user has selected a child node
        Dim soundbankNode As TreeNode = currentSelectedNode
        If currentSelectedNode.Level > 0 Then
            soundbankNode = currentSelectedNode.Parent
        End If
        Return soundbankNode
    End Function
End Class

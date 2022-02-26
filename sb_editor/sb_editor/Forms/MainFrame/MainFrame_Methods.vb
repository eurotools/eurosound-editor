Partial Public Class MainFrame
    '*===============================================================================================
    '* ACTIONS
    '*===============================================================================================
    Private Sub AddDataBaseToSoundBank()
        'Check that there are databases selected
        If ListBox_DataBases.SelectedItems.Count > 0 AndAlso TreeView_SoundBanks.Nodes.Count > 0 Then
            'Get Soundbank node
            Dim selectedSoundBank As TreeNode = TreeView_SoundBanks.SelectedNode
            'Ensure that is not null
            If selectedSoundBank IsNot Nothing Then
                'If we have selected a child node, select his parent node
                If selectedSoundBank.Level > 0 Then
                    selectedSoundBank = TreeView_SoundBanks.SelectedNode.Parent
                End If
                'Add databases to soundbank
                Dim selectedDatabases As New List(Of String)
                For Each database As String In ListBox_DataBases.SelectedItems
                    selectedDatabases.Add(database)
                Next
                AddDataBaseToSoundBank(selectedDatabases, selectedSoundBank)
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub

    Private Sub RemoveSfxFromDatabase()
        'Remove Dependency
        If ListBox_DataBaseSFX.SelectedItems.Count > 0 AndAlso ListBox_DataBases.SelectedItems.Count = 1 Then
            'Get selected items
            Dim itemsToRemove As New Collection
            For Each itemIndex As Integer In ListBox_DataBaseSFX.SelectedIndices
                itemsToRemove.Add(ListBox_DataBaseSFX.Items(itemIndex))
            Next
            'Remove items
            For index As Integer = 1 To itemsToRemove.Count
                'Remove item to the listbox
                ListBox_DataBaseSFX.Items.Remove(itemsToRemove(index))
            Next
            'Update text file
            Dim databaseTxt As String = fso.BuildPath(WorkingDirectory, "DataBases\" & ListBox_DataBases.SelectedItem & ".txt")
            Dim databaseDependencies As String() = ListBox_DataBaseSFX.Items.Cast(Of String).ToArray
            writers.UpdateDataBaseText(databaseTxt, databaseDependencies, textFileReaders)

            'Update label counter
            Label_DataBaseSFX.Text = "Total: " & ListBox_DataBaseSFX.Items.Count
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub

    Private Sub OpenDataBaseProperties()
        'Ensure that we have an item selected
        If ListBox_DataBases.SelectedItems.Count = 1 Then
            'Build path
            Dim databaseFullPath = fso.BuildPath(WorkingDirectory, "Databases\" & ListBox_DataBases.SelectedItem & ".txt")
            'Show properties form
            If fso.FileExists(databaseFullPath) Then
                Dim propertiesForm As New DataBase_Properties(databaseFullPath)
                propertiesForm.ShowDialog()
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub
End Class

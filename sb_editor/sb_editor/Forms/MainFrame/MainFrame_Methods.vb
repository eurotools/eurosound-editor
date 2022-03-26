Imports System.IO

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

                AddDatabaseToSoundbank(ListBox_DataBases.SelectedItems.Cast(Of String).ToArray, selectedSoundBank)
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub

    Private Sub RemoveSfxFromDatabase()
        'Remove Dependency
        If ListBox_DataBaseSFX.SelectedItems.Count > 0 AndAlso ListBox_DataBases.SelectedItems.Count = 1 Then
            'Remove selected items
            For itemIndex As Integer = ListBox_DataBaseSFX.SelectedItems.Count - 1 To 0 Step -1
                ListBox_DataBaseSFX.Items.Remove(ListBox_DataBaseSFX.SelectedItems(itemIndex))
            Next

            'Update text file
            Dim databaseTxt As String = Path.Combine(WorkingDirectory, "DataBases", ListBox_DataBases.SelectedItem & ".txt")
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
            Dim databaseFullPath = Path.Combine(WorkingDirectory, "Databases", ListBox_DataBases.SelectedItem & ".txt")

            'Show properties form
            If File.Exists(databaseFullPath) Then
                Dim propertiesForm As New DataBase_Properties(databaseFullPath)
                propertiesForm.ShowDialog()
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub

    '*===============================================================================================
    '* SAMPLES FILE
    '*===============================================================================================
    Friend Sub CheckForMissingSamples()
        Dim missingSamples As New List(Of String)

        'Check that all samples exists
        Dim samplesDataTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)
        For index As Integer = samplesDataTable.Rows.Count - 1 To 0 Step -1
            Dim currentRow As DataRow = samplesDataTable.Rows(index)
            Dim sampleFullPath As String = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master" & currentRow("SampleFilename"))

            'Add item to missing samples list and remove item form data table
            If Not File.Exists(sampleFullPath) Then
                missingSamples.Add(currentRow("SampleFilename"))
                currentRow.Delete()
            End If
        Next

        'Show missing samples form and update text file
        If missingSamples.Count > 0 Then
            'Update Text file
            samplesDataTable.AcceptChanges()
            writers.SaveSamplesFile(SysFileSamples, samplesDataTable)

            'Inform user about the missing samples
            Dim missingSamplesForm As New MissingSamples(missingSamples.ToArray)
            missingSamplesForm.ShowDialog()
        End If
    End Sub

    Friend Sub CheckForNewSamples()
        Dim samplesInFile As New List(Of String)
        Dim samplesInFolder As New List(Of String)

        'Get used samples
        Dim samplesTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)
        For rowIndex As Integer = 0 To samplesTable.Rows.Count - 1
            Dim currentRow As DataRow = samplesTable.Rows(rowIndex)
            samplesInFile.Add(currentRow("SampleFilename"))
        Next

        'Get files in the directory
        Dim filesList As String() = Directory.GetFiles(Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master"), "*.wav", SearchOption.AllDirectories)
        Dim substrStart = Len(ProjectSettingsFile.MiscProps.SampleFileFolder & "\Master\")
        For fileIndex As Integer = 0 To filesList.Length - 1
            Dim relPath As String = Mid(filesList(fileIndex), substrStart)
            samplesInFolder.Add(relPath)
        Next

        'Check if there are new samples
        Dim samplesToAdd As String() = samplesInFolder.Except(samplesInFile).ToArray
        If samplesToAdd.Length > 0 Then
            'Inform user about the missing samples
            Dim newSamplesForm As New NewSamples(samplesToAdd.ToArray)
            newSamplesForm.ShowDialog()
            Dim selectedSampleRate = newSamplesForm.ComboBox_AvailableRates.SelectedItem

            'Add new samples
            For sampleIndex As Integer = 0 To samplesToAdd.Length - 1
                Dim currentFilePath As String = samplesToAdd(sampleIndex)
                Dim sampleFullPath As String = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master", currentFilePath.TrimStart("\"))
                samplesTable.Rows.Add(currentFilePath, selectedSampleRate, FileLen(sampleFullPath), FileDateTime(sampleFullPath).ToString(dateFormat), "True", "False", "", "", "", "")
            Next

            'Sort table
            samplesTable.AcceptChanges()
            Dim view As DataView = samplesTable.DefaultView
            view.Sort = "SampleFilename asc"
            Dim sortedTable As DataTable = view.ToTable

            'Update file
            writers.SaveSamplesFile(SysFileSamples, sortedTable)
        End If
    End Sub
End Class

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
                AddDatabaseToSoundbank(ListBox_DataBases.SelectedItems, selectedSoundBank)
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
            Dim databaseDependencies As List(Of String) = ListBox_DataBaseSFX.Items.Cast(Of String).ToList
            writers.UpdateDataBaseText(databaseTxt, databaseDependencies, textFileReaders)
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

    '*===============================================================================================
    '* FILES FUNCTIONS
    '*===============================================================================================
    Private Function RenameFile(objectName As String, objectType As String, objectFolder As String) As String
        'Ask name for first time
        Dim inputName As String = InputBox("Enter New Name For " & objectName, "Rename " & objectType, objectName)
        Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
        Dim finalName As String = inputName

        'Ask continously if the file exists
        If StrComp(objectName, inputName) <> 0 AndAlso fso.FileExists(inputFilePath) Then
            Do
                'Inform user and ask again
                MsgBox(objectType & " Label '" & inputName & "' already exists, please use another name!", vbOKOnly + vbCritical, "Duplicate " & objectType & " Name")
                inputName = InputBox("Enter New Name For " & objectName, "Rename " & objectType, objectName)

                'If the input name is the same as the object name or is null, exit loop!
                If StrComp(objectName, inputName) = 0 Or inputName = "" Then
                    finalName = ""
                    Exit Do
                Else
                    finalName = inputName
                End If
            Loop While fso.FileExists(fso.BuildPath(objectFolder, inputName & ".txt"))
        End If

        RenameFile = finalName
    End Function

    Private Function CopyFile(objectName As String, objectType As String, objectFolder As String) As String
        'Ask name for first time
        Dim inputName As String = InputBox("Enter Copy Name For " & objectType & " " & objectName, "Copy " & objectType, objectName)
        Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
        Dim finalName As String = inputName

        'Ask continously if the file exists
        If StrComp(objectName, inputName) <> 0 AndAlso fso.FileExists(inputFilePath) Then
            Do
                'Inform user and ask again
                MsgBox(objectType & " Label '" & inputName & "' already exists, please use another name!", vbOKOnly + vbCritical, "Duplicate " & objectType & " Name")
                inputName = InputBox("Enter Copy Name For " & objectName, "Copy " & objectType, objectName)

                'If the input name is the same as the object name or is null, exit loop!
                If StrComp(objectName, inputName) = 0 Or inputName = "" Then
                    finalName = ""
                    Exit Do
                Else
                    finalName = inputName
                End If
            Loop While fso.FileExists(fso.BuildPath(objectFolder, inputName & ".txt"))
        End If

        CopyFile = finalName
    End Function

    Private Function NewFile(objectName As String, objectFolder As String) As String
        'Ask name for first time
        Dim inputName As String = InputBox("Enter Name", "Create New", objectName)
        Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
        Dim finalName As String = inputName

        'Ask continously if the file exists
        If StrComp(objectName, inputName) <> 0 AndAlso fso.FileExists(inputFilePath) Then
            Do
                'Inform user and ask again
                MsgBox("Label '" & inputName & "' already exists, please use another name!", vbOKOnly + vbCritical, "Duplicate Name")
                inputName = InputBox("Enter Name", "Create New", objectName)

                'If the input name is the same as the object name or is null, exit loop!
                If StrComp(objectName, inputName) = 0 Or inputName = "" Then
                    finalName = ""
                    Exit Do
                Else
                    finalName = inputName
                End If
            Loop While fso.FileExists(fso.BuildPath(objectFolder, inputName & ".txt"))
        End If

        NewFile = finalName
    End Function
End Class

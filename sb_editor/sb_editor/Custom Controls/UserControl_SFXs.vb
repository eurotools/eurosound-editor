Imports System.IO
Imports System.Text.RegularExpressions

Public Class UserControl_SFXs
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly writers As New FileWriters
    Public Property AllowDoubleClick As Boolean = True

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Public Sub LoadHashCodes()
        'Get files from directory and add it to list
        If fso.FolderExists(fso.BuildPath(WorkingDirectory, "SFXs")) Then
            Dim sfxFiles As String() = Directory.GetFiles(fso.BuildPath(WorkingDirectory, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly)
            AddItemsToList(sfxFiles)
        End If
    End Sub

    Public Sub LoadRefineList()
        'Get arrays item
        Dim refineFilePath As String = fso.BuildPath(WorkingDirectory, "System\RefineSearch.txt")
        If fso.FileExists(refineFilePath) Then
            Dim keywords As String() = textFileReaders.ReadRefineList(refineFilePath)
            ComboBox_SFX_Section.Items.AddRange(keywords)
            'Select first item
            If ComboBox_SFX_Section.Items.Count > 0 Then
                ComboBox_SFX_Section.SelectedIndex = 0
            End If
            'Erase Array
            Erase keywords
        End If
    End Sub

    '*===============================================================================================
    '* BUTTON EVENTS
    '*===============================================================================================
    Private Sub Button_UpdateList_Click(sender As Object, e As EventArgs) Handles Button_UpdateList.Click
        Dim keywordsDict As New Dictionary(Of String, Integer)()
        Dim refineList As New List(Of String)()
        Dim inputLines As String() = GetSfxNamesArray()

        'Get keywords count
        For i As Integer = 0 To inputLines.Length - 1
            Dim lineData As String() = inputLines(i).Split("_"c)
            For j As Integer = 0 To lineData.Length - 1
                Dim currentKeyword As String = lineData(j)
                If currentKeyword.Length < 3 OrElse currentKeyword.Equals("SFX") Then
                    Continue For
                Else
                    If keywordsDict.ContainsKey(currentKeyword) Then
                        keywordsDict(currentKeyword) += 1
                    Else
                        keywordsDict.Add(currentKeyword, 1)
                    End If
                End If
            Next
        Next
        Erase inputLines

        'Get Refine List
        For Each dictItem As KeyValuePair(Of String, Integer) In keywordsDict
            If dictItem.Value > 2 Then
                refineList.Add(dictItem.Key)
            End If
        Next

        Dim rgx As New Regex("\d+$")
        Dim keywordsWithNumber As New Dictionary(Of String, Integer)()
        For Each keyWithNum As KeyValuePair(Of String, Integer) In keywordsDict
            If keyWithNum.Key.Length > 2 Then
                If Char.IsDigit(keyWithNum.Key(keyWithNum.Key.Length - 1)) Then
                    Dim keywordWithoutNum As String = rgx.Replace(keyWithNum.Key, "")
                    If keywordsDict.ContainsKey(keywordWithoutNum) Then
                        If keywordsWithNumber.ContainsKey(keywordWithoutNum) Then
                            keywordsWithNumber(keywordWithoutNum) += 1
                        Else
                            keywordsWithNumber.Add(keywordWithoutNum, 1)
                        End If
                    End If
                End If
            End If
        Next

        'Get Refine List
        For Each dictItem As KeyValuePair(Of String, Integer) In keywordsWithNumber
            If dictItem.Value > 1 AndAlso Not refineList.Contains(dictItem.Key) Then
                refineList.Add(dictItem.Key)
            End If
        Next
        refineList.Sort()

        'Update file
        If fso.FolderExists(fso.BuildPath(WorkingDirectory, "System")) Then
            writers.CreateRefineList(fso.BuildPath(WorkingDirectory, "System\RefineSearch.txt"), refineList)
        End If

        'Update combobox
        ComboBox_SFX_Section.Items.Clear()
        LoadRefineList()
    End Sub

    Private Sub Button_ShowAll_Click(sender As Object, e As EventArgs) Handles Button_ShowAll.Click
        LoadHashCodes()
    End Sub

    Private Sub CheckBox_SortByDate_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox_SortByDate.CheckStateChanged
        If CheckBox_SortByDate.Checked Then
            Dim di As New DirectoryInfo(fso.BuildPath(WorkingDirectory, "SFXs"))
            Dim fiArray As String() = di.GetFiles().OrderByDescending(Function(p) p.LastWriteTime).Select(Function(f) f.Name).ToArray()
            AddItemsToList(fiArray)
        Else
            LoadHashCodes()
        End If
    End Sub

    Private Sub Button_UnUsedHashCodes_Click(sender As Object, e As EventArgs) Handles Button_UnUsedHashCodes.Click

    End Sub

    '*===============================================================================================
    '* COMBOBOX EVENTS
    '*===============================================================================================
    Private Sub ComboBox_SFX_Section_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox_SFX_Section.SelectionChangeCommitted
        'Clear items
        If ComboBox_SFX_Section.SelectedIndex > 1 Then
            ListBox_SFXs.Items.Clear()
            'Get keyword
            Dim keywordSelected As String = ComboBox_SFX_Section.SelectedItem
            'Get files from directory and add it to list
            Dim sfxFiles As String() = Directory.GetFiles(fso.BuildPath(WorkingDirectory, "SFXs"), "*" & keywordSelected & "*.txt", SearchOption.TopDirectoryOnly)
            AddItemsToList(sfxFiles)
        ElseIf ComboBox_SFX_Section.SelectedIndex = 1 AndAlso ListBox_SFXs.SelectedItems.Count > 0 Then 'Highlighted
            Dim selectedIndices As String() = ListBox_SFXs.SelectedItems.Cast(Of String).ToArray
            AddItemsToList(selectedIndices)
        Else 'All
            LoadHashCodes()
        End If

    End Sub

    '*===============================================================================================
    '* CONTEXT MENU
    '*===============================================================================================
    Private Sub ContextMenuSfx_AddToDb_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_AddToDb.Click
        'Get mainframe and clear selection
        Dim mainForm As MainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
        If mainForm IsNot Nothing Then
            'Ensure that we have selected an item
            If ListBox_SFXs.SelectedItems.Count > 0 AndAlso mainForm.ListBox_DataBases.SelectedItems.Count = 1 Then
                'Get selected items
                Dim selectedItems As ListBox.SelectedObjectCollection = ListBox_SFXs.SelectedItems
                For Each sfxItem As String In selectedItems
                    'Ensure that the item does not exist in this database
                    If Not mainForm.ListBox_DataBaseSFX.Items.Contains(sfxItem) Then
                        'Add item to the listbox
                        mainForm.ListBox_DataBaseSFX.Items.Add(sfxItem)
                    End If
                Next
                'Get list of items
                Dim databaseDependencies As List(Of String) = mainForm.ListBox_DataBaseSFX.Items.Cast(Of String).ToList
                'Update text file
                Dim databaseTxt As String = fso.BuildPath(WorkingDirectory, "DataBases\" & mainForm.ListBox_DataBases.SelectedItem & ".txt")
                writers.UpdateDataBaseText(databaseTxt, databaseDependencies, textFileReaders)
            Else
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
            End If
        End If
    End Sub

    Private Sub ContextMenuSfx_Properties_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_Properties.Click
        'Ensure that we have selected an item
        If ListBox_SFXs.SelectedItems.Count = 1 Then
            'Get item name and full path
            Dim selectedSFX As String = ListBox_SFXs.SelectedItem
            Dim sfxFullPath As String = fso.BuildPath(WorkingDirectory, "SFXs\" & selectedSFX & ".txt")
            'Ensure that the file exists 
            If fso.FileExists(sfxFullPath) Then
                'Show form
                Dim sfxProps As New SFX_Properties(selectedSFX, sfxFullPath)
                sfxProps.ShowDialog()
            Else
                'Inform user about this error
                MsgBox("The specified file has not been found: " & sfxFullPath, vbOKOnly + vbCritical, "Error")
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub

    Private Sub ContextMenuSfx_EditSfx_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_EditSfx.Click
        'Get selected item
        If ListBox_SFXs.SelectedItems.Count = 1 AndAlso AllowDoubleClick Then
            'Get item and file path
            Dim selectedSFX As String = ListBox_SFXs.SelectedItem
            Dim SelectedSfxPath = fso.BuildPath(WorkingDirectory, "SFXs\" & selectedSFX & ".txt")
            'Ensure that the file exists
            If fso.FileExists(SelectedSfxPath) Then
                'Open editor
                Dim sfxEditor As New Frm_SfxEditor(selectedSFX)
                sfxEditor.ShowDialog()
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub

    Private Sub ContextMenuSfx_AddNewSfx_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_AddNewSfx.Click
        If ListBox_SFXs.Items.Count > 0 AndAlso SFXHashCodeNumber = 0 Then
            MsgBox("Please Re-Alloc Hashcodes under Advanced Menu", vbOKOnly + vbExclamation, "EuroSound")
        Else
            'Ensure that the default file exists
            If fso.FileExists(SysFileSfxDefaults) Then

            Else
                'Inform user about this
                MsgBox("Must Setup Default SFX file first!", vbOKOnly + vbCritical, "Setup SFX Defaults.")
                'Open SFX Default Form
                Using defaultSettingsForm As New SfxDefault
                    defaultSettingsForm.ShowDialog()
                End Using
            End If
        End If
    End Sub

    Private Sub ContextMenuSfx_Copy_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_Copy.Click
        If ListBox_SFXs.SelectedItems.Count = 1 Then
            'Ask user
            Dim sfxCopyName As String = CopyFile(ListBox_SFXs.SelectedItem, "SFX", fso.BuildPath(WorkingDirectory, "SFXs\"))
            If sfxCopyName IsNot "" Then
                'Read original file content
                Dim originalFilePath As String = fso.BuildPath(WorkingDirectory, "SFXs\" & ListBox_SFXs.SelectedItem & ".txt")
                Dim fileContent As String() = File.ReadAllLines(originalFilePath)
                'Update HashCode
                Dim hashCodePosition As Integer = Array.IndexOf(fileContent, "#HASHCODE") + 1
                If (hashCodePosition < fileContent.Length) Then
                    fileContent(hashCodePosition) = "HashCodeNumber " & SFXHashCodeNumber
                    SFXHashCodeNumber += 1
                    'Write new file
                    File.WriteAllLines(fso.BuildPath(WorkingDirectory, "SFXs\" & sfxCopyName & ".txt"), fileContent)
                    ListBox_SFXs.Items.Add(sfxCopyName)
                End If
                Erase fileContent
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If

    End Sub

    Private Sub ContextMenuSfx_Delete_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_Delete.Click
        'Create a list with the items that we have to remove
        Dim itemsToDelete As New List(Of String)
        For Each itemToRemove As String In ListBox_SFXs.SelectedItems
            itemsToDelete.Add(itemToRemove)
        Next

        'Ask user what he wants to do
        Dim answerQuestion As MsgBoxResult = MsgBox(MultipleDeletionMessage("Are you sure you want to delete SFX(s)", itemsToDelete), vbInformation + vbYesNo, "Confirm SFX Deletion")
        If answerQuestion = MsgBoxResult.Yes Then
            'Get mainframe and clear selection
            Dim mainForm As MainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
            mainForm.ListBox_DataBases.SelectedItems.Clear()
            mainForm.ListBox_DataBaseSFX.Items.Clear()
            'Ensure that the Trash folder exists
            Dim sfxsTrash As String = fso.BuildPath(WorkingDirectory, "SFXs_Trash")
            If Not fso.FolderExists(sfxsTrash) Then
                fso.CreateFolder(sfxsTrash)
            End If
            'Get database files
            Dim databaseFiles As String() = Directory.GetFiles(fso.BuildPath(WorkingDirectory, "DataBases"), "*.txt", SearchOption.TopDirectoryOnly)
            For i As Integer = 0 To databaseFiles.Length - 1
                'Update database files
                Dim databaseFile As DataBaseFile = textFileReaders.ReadDataBaseFile(databaseFiles(i))
                Dim resultList As List(Of String) = databaseFile.Dependencies.Except(itemsToDelete).ToList
                If resultList.Count < databaseFile.Dependencies.Length Then
                    writers.UpdateDataBaseText(databaseFiles(i), resultList, textFileReaders, False)
                End If
            Next
            Erase databaseFiles

            'Update UI
            ListBox_SFXs.BeginUpdate()
            For i As Integer = 0 To itemsToDelete.Count - 1
                ListBox_SFXs.Items.Remove(itemsToDelete(i))
                'Remove text file
                Dim filesToDelete As IEnumerable(Of String) = GetFileList(itemsToDelete(i) & ".txt", fso.BuildPath(WorkingDirectory, "SFXs"))
                Using enumerator As IEnumerator(Of String) = filesToDelete.GetEnumerator
                    While enumerator.MoveNext
                        fso.CopyFile(enumerator.Current, fso.BuildPath(sfxsTrash, itemsToDelete(i) & ".txt"))
                        fso.DeleteFile(enumerator.Current)
                    End While
                End Using
            Next
            ListBox_SFXs.EndUpdate()

            'Update counter
            Label_TotalSfx.Text = "Total: " & ListBox_SFXs.Items.Count

            'Update Project file
            writers.CreateProjectFile(fso.BuildPath(WorkingDirectory, "Project.txt"), mainForm.TreeView_SoundBanks, mainForm.ListBox_DataBases, mainForm.UserControl_SFXs.ListBox_SFXs)
        End If
    End Sub

    Private Sub ContextMenuSfx_Rename_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_Rename.Click
        If ListBox_SFXs.SelectedItems.Count = 1 Then
            'Get current fileName
            Dim selectedName As String = ListBox_SFXs.SelectedItem
            Dim currentFileName As String = fso.BuildPath(WorkingDirectory, "SFXs\" & selectedName & ".txt")
            'Ask for a new name
            Dim diagResult As String = RenameFile(selectedName, "SFX", fso.BuildPath(WorkingDirectory, "SFXs\"))
            If diagResult IsNot "" Then
                Dim mainForm As MainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
                'Update UI and text file
                fso.MoveFile(currentFileName, fso.BuildPath(WorkingDirectory, "SFXs\" & diagResult & ".txt"))
                ListBox_SFXs.Items(ListBox_SFXs.SelectedIndex) = diagResult
                'Clear Selection
                mainForm.TreeView_SoundBanks.SelectedNode = Nothing
                mainForm.ListBox_DataBases.SelectedItems.Clear()
                mainForm.ListBox_DataBaseSFX.Items.Clear()
                'Update project file
                writers.CreateProjectFile(fso.BuildPath(WorkingDirectory, "Project.txt"), mainForm.TreeView_SoundBanks, mainForm.ListBox_DataBases, ListBox_SFXs)
                'Update databases
                Dim databaseFiles As String() = Directory.GetFiles(fso.BuildPath(WorkingDirectory, "Databases"), "*.txt", SearchOption.TopDirectoryOnly)
                For index As Integer = 0 To databaseFiles.Length - 1
                    'Read file
                    Dim fileLines As String() = File.ReadAllLines(databaseFiles(index))
                    'Update file and save changes
                    Dim sfxItemIndex = Array.IndexOf(fileLines, selectedName)
                    If sfxItemIndex >= 0 Then
                        fileLines(sfxItemIndex) = diagResult
                        File.WriteAllLines(databaseFiles(index), fileLines)
                    End If
                Next
                'Liberate Memmory
                Erase databaseFiles
            End If
        End If
    End Sub

    Private Sub RemoveSfxAllFolders(filename As String, trashFolder As String)
        Dim fileList As String() = Directory.GetFiles(fso.BuildPath(WorkingDirectory, "SFXs"), filename & ".txt", SearchOption.AllDirectories)
        For index As Integer = 0 To fileList.Length - 1
            If fso.FileExists(fileList(index)) Then
                fso.CopyFile(fileList(index), trashFolder)
                fso.DeleteFile(fileList(index))
            End If
        Next
        Erase fileList
    End Sub

    Public Shared Iterator Function GetFileList(fileSearchPattern As String, rootFolderPath As String) As IEnumerable(Of String)
        Dim pending As New Queue(Of String)()
        pending.Enqueue(rootFolderPath)
        Dim tmp As String()

        While pending.Count > 0
            rootFolderPath = pending.Dequeue()

            Try
                tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern)
            Catch __unusedUnauthorizedAccessException1__ As UnauthorizedAccessException
                Continue While
            End Try

            For i As Integer = 0 To tmp.Length - 1
                Yield tmp(i)
            Next

            tmp = Directory.GetDirectories(rootFolderPath)
            For i As Integer = 0 To tmp.Length - 1
                pending.Enqueue(tmp(i))
            Next
        End While
    End Function


    Private Sub ContextMenuSfx_NewMultiple_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_NewMultiple.Click
        Dim newMultiple As New SfxNewMultiple
        newMultiple.ShowDialog()
    End Sub

    Private Sub ContextMenuSfx_MultiEditor_Click(sender As Object, e As EventArgs) Handles ContextMenuSfx_MultiEditor.Click
        Dim multiEditor As New SfxMultiEditor
        multiEditor.ShowDialog()
    End Sub

    '*===============================================================================================
    '* LISTBOX EVENTS
    '*===============================================================================================
    Private Sub ListBox_SFXs_DragOver(sender As Object, e As DragEventArgs) Handles ListBox_SFXs.DragOver
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub ListBox_SFXs_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox_SFXs.DragDrop
        If e.Effect = DragDropEffects.Move Then
            If e.Data.GetDataPresent(GetType(ListBox.SelectedObjectCollection)) Then
                Dim itemsData As ListBox.SelectedObjectCollection = e.Data.GetData(GetType(ListBox.SelectedObjectCollection))
                If itemsData.Count > 0 Then
                    'Get mainframe
                    Dim mainForm As MainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
                    'Remove items
                    While mainForm.ListBox_DataBaseSFX.SelectedItems.Count > 0
                        mainForm.ListBox_DataBaseSFX.Items.Remove(mainForm.ListBox_DataBaseSFX.SelectedItems(0))
                    End While
                    'Update text
                    Dim databaseTxt As String = fso.BuildPath(WorkingDirectory, "DataBases\" & mainForm.ListBox_DataBases.SelectedItem & ".txt")
                    Dim databaseDependencies As List(Of String) = mainForm.ListBox_DataBaseSFX.Items.Cast(Of String).ToList
                    writers.UpdateDataBaseText(databaseTxt, databaseDependencies, textFileReaders)
                End If
            End If
        End If
    End Sub

    Private Sub ListBox_SFXs_DoubleClick(sender As Object, e As EventArgs) Handles ListBox_SFXs.DoubleClick
        'Ensure that we have selected an item
        If ListBox_SFXs.SelectedItems.Count > 0 AndAlso AllowDoubleClick Then
            'Get item and file path
            Dim selectedSFX As String = ListBox_SFXs.SelectedItem
            Dim SelectedSfxPath = fso.BuildPath(WorkingDirectory, "SFXs\" & selectedSFX & ".txt")
            'Ensure that the file exists
            If fso.FileExists(SelectedSfxPath) Then
                'Open editor
                Dim sfxEditor As New Frm_SfxEditor(selectedSFX)
                sfxEditor.ShowDialog()
            End If
        End If
    End Sub

    '*===============================================================================================
    '* LISTBOX EVENTS
    '*===============================================================================================
    Private Sub AddItemsToList(itemsToAdd As String())
        'Enable listbox update mode
        ListBox_SFXs.BeginUpdate()

        'Remove existing items
        If ListBox_SFXs.Items.Count > 0 Then
            ListBox_SFXs.Items.Clear()
        End If

        'Read text files
        For i = 0 To itemsToAdd.Length - 1
            Dim sfxName = GetOnlyFileName(itemsToAdd(i))
            ListBox_SFXs.Items.Add(sfxName)
        Next

        'Update counter
        Label_TotalSfx.Text = "Total: " & itemsToAdd.Count

        'Disable listbox update mode
        ListBox_SFXs.EndUpdate()
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Function GetSfxNamesArray() As String()
        Dim listOfNames As New List(Of String)
        For index As Integer = 0 To ListBox_SFXs.Items.Count - 1
            listOfNames.Add(ListBox_SFXs.Items(index))
        Next

        Return listOfNames.ToArray
    End Function
End Class

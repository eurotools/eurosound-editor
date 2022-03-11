Imports System.IO
Imports IniFileFunctions
Imports RecentFilesMenu
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Partial Public Class MainFrame
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly writers As New FileWriters
    Protected Friend RecentFilesMenu As MostRecentFilesMenu

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub MainFrame_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If fso.FolderExists(WorkingDirectory) Then
            If fso.FileExists(fso.BuildPath(WorkingDirectory, "Project.txt")) Then
                'Update GUI
                RecentFilesMenu.AddFile(WorkingDirectory)
                RecentFilesMenu.SaveToIniFile()
            End If
        End If
    End Sub

    Private Sub MainFrame_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Update text file
        If fso.FolderExists(fso.BuildPath(WorkingDirectory, "System\")) Then
            writers.UpdateMiscFile(fso.BuildPath(WorkingDirectory, "System\Misc.txt"))

            'Save data in the Ini File
            If SysFileProjectIniPath > "" Then
                Dim iniFunctions As New IniFile(SysFileProjectIniPath)
                iniFunctions.Write("Last_Project_Opened", WorkingDirectory, "Form1_Misc")
                iniFunctions.Write("FormatCombo_ListIndex", ComboBox_Format.SelectedIndex, "Form1_Misc")
                iniFunctions.Write("AllBanksOption_Value", RadioButton_AllBanksSelectedFormat.Checked, "Form1_Misc")
                iniFunctions.Write("SelectedlBankOption_Value", RadioButton_Output_SelectedSoundBank.Checked, "Form1_Misc")
                iniFunctions.Write("AllFormatsOption_Value", RadioButton_Output_AllBanksAll.Checked, "Form1_Misc")
                iniFunctions.Write("Check1", Convert.ToByte(CheckBox_FastReSample.Checked), "MainForm")
                iniFunctions.Write("LanguageCombo", ComboBox_OutputLanguage.SelectedIndex, "MainForm")
                iniFunctions.Write("Check2", Convert.ToByte(UserControl_SFXs.CheckBox_SortByDate.Checked), "MainForm")
                iniFunctions.Write("OutputAllLanguages", Convert.ToByte(CheckBox_OutAllLanguages.Checked), "MainForm")
            End If
        End If
    End Sub

    '*===============================================================================================
    '* MENU ITEM FILE
    '*===============================================================================================
    Private Sub MenuItemFile_LoadProject_Click(sender As Object, e As EventArgs) Handles MenuItemFile_LoadProject.Click
        OpenFileDialog.Filter = "Text File (*.txt)|*.txt"
        Dim selectedProjectFile As DialogResult = OpenFileDialog.ShowDialog
        If selectedProjectFile = DialogResult.OK Then
            OpenNewProject(fso.GetParentFolderName(OpenFileDialog.FileName))
        End If
    End Sub

    Private Sub MenuItemFile_NewProject_Click(sender As Object, e As EventArgs) Handles MenuItemFile_NewProject.Click
        'Ask user for a folder
        Dim projectFolder As DialogResult = FolderBrowserDialog.ShowDialog
        If projectFolder = DialogResult.OK Then
            CreateNewProject(FolderBrowserDialog.SelectedPath)
            RestartEuroSound()
        End If
    End Sub

    Friend Sub MenuItemFile_Recent_Click(number As Integer, filename As String)
        If fso.FolderExists(filename) Then
            OpenNewProject(filename)
        Else
            MsgBox("Project Directory Not Found" & filename, vbOKOnly + vbCritical, "EuroSound Load Project Error")
            RecentFilesMenu.RemoveFile(number)
        End If
    End Sub

    Private Sub OpenNewProject(projectFilePath As String)
        Dim programIni As New IniFile(EuroSoundIniFilePath)
        programIni.Write("Last_Project_Opened", projectFilePath, "Form1_Misc")
        'Application.Restart() <- Sometimes not works
        RestartEuroSound()
    End Sub

    Private Sub MenuItemFile_Exit_Click(sender As Object, e As EventArgs) Handles MenuItemFile_Exit.Click
        'Quit application
        Application.Exit()
    End Sub

    '*===============================================================================================
    '* MENU ITEM HELP
    '*===============================================================================================
    Private Sub MenuItemHelp_About_Click(sender As Object, e As EventArgs) Handles MenuItemHelp_About.Click
        Dim aboutForm As New About
        aboutForm.ShowDialog()
    End Sub

    '*===============================================================================================
    '* TREE VIEW EVENTS
    '*===============================================================================================
    Private Sub TreeView_SoundBanks_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles TreeView_SoundBanks.BeforeExpand
        'Open folder icon
        If e.Node.Level = 0 Then
            'Update icon
            e.Node.SelectedImageIndex = 1
            e.Node.ImageIndex = 1
            'Collapse other nodes that can be expanded
            For Each node As TreeNode In TreeView_SoundBanks.Nodes
                If node IsNot e.Node Then
                    If node.IsExpanded Then
                        node.Collapse()
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub TreeView_SoundBanks_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles TreeView_SoundBanks.BeforeCollapse
        'Closed folder icon
        If e.Node.Level = 0 Then
            e.Node.SelectedImageIndex = 0
            e.Node.ImageIndex = 0
        End If
    End Sub

    Private Sub TreeView_SoundBanks_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView_SoundBanks.AfterSelect
        'Dim counter
        Dim nodeCount As Integer = 0
        Dim SoundbankItem As TreeNode
        'Collapse items and get soundbank node
        If e.Node.Level = 0 Then
            SoundbankItem = e.Node
        Else
            SoundbankItem = e.Node.Parent
        End If
        'Count databases
        For Each database As TreeNode In SoundbankItem.Nodes
            If database.SelectedImageIndex <> 3 Then
                nodeCount += 1
            End If
        Next
        'Update counter label
        Label_SoundBank_DataBases.Text = "DB Total: " & nodeCount
    End Sub

    Private Sub TreeView_SoundBanks_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView_SoundBanks.NodeMouseClick
        'Ensure that is the right button
        If e.Button = MouseButtons.Right Then
            'Select node
            TreeView_SoundBanks.SelectedNode = e.Node
        End If
    End Sub

    Private Sub TreeView_SoundBanks_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView_SoundBanks.NodeMouseDoubleClick
        'Ensure that the selected node is not null and is a database
        If e.Node IsNot Nothing AndAlso e.Node.Level > 0 Then
            'Ensure that the item is selected, to avoid the bug that selects a database at the same time that the parent node is not expanded yet
            If e.Node.IsSelected Then
                'Find database item
                Dim DatabaseIndex As Integer = ListBox_DataBases.FindString(e.Node.Text)
                'Check if we have results
                If DatabaseIndex <> ListBox.NoMatches Then
                    'Clear selected items and select the new one
                    ListBox_DataBases.SelectedIndices.Clear()
                    ListBox_DataBases.SelectedIndex = DatabaseIndex
                End If
            End If
        End If
    End Sub

    '*===============================================================================================
    '* CONTEXT MENU TREEVIEW
    '*===============================================================================================
    Private Sub ContextMenu_TreeView_New_Click(sender As Object, e As EventArgs) Handles ContextMenu_TreeView_New.Click
        Dim folderToCheck As String = fso.BuildPath(WorkingDirectory, "Soundbanks")
        Dim soundbankName As String = NewFile(GetFileName(folderToCheck, "SB_Label"), folderToCheck)
        If soundbankName IsNot "" Then
            CreateNewSoundbank(soundbankName)
        End If
    End Sub

    Private Sub ContextMenu_TreeView_Copy_Click(sender As Object, e As EventArgs) Handles ContextMenu_TreeView_Copy.Click
        'Ensure that we have selected a node
        If TreeView_SoundBanks.SelectedNode IsNot Nothing Then
            'Get parent if the user has selected a child node
            Dim soundbankNode As TreeNode = GetSoundbankNode(TreeView_SoundBanks.SelectedNode)
            'Ask user
            Dim soundbankCopyName As String = CopyFile(soundbankNode.Text, "Sound Bank", fso.BuildPath(WorkingDirectory, "Soundbanks\"))
            If soundbankCopyName IsNot "" Then
                CopySoundbank(soundbankCopyName, soundbankNode)
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
        End If
    End Sub

    Private Sub ContextMenu_TreeView_Delete_Click(sender As Object, e As EventArgs) Handles ContextMenu_TreeView_Delete.Click
        'Ensure that we have selected a node
        If TreeView_SoundBanks.SelectedNode IsNot Nothing Then
            'Soundbank
            If TreeView_SoundBanks.SelectedNode.Level = 0 Then
                'Get file path
                Dim soundBankName = TreeView_SoundBanks.SelectedNode.Text
                Dim soundbankPath As String = fso.BuildPath(WorkingDirectory, "SoundBanks\" & soundBankName & ".txt")
                'Ask user
                Dim userAnswer As MsgBoxResult = MsgBox("Are you sure you want to delete SoundBank(s)" & vbNewLine & "'" & soundBankName & "'" & vbNewLine & "TotalFiles: 1", vbYesNo + vbQuestion, "Confirm Sound Bank Deletion")
                If userAnswer = MsgBoxResult.Yes Then
                    TreeView_SoundBanks.SelectedNode.Remove()
                    'Create Trash Folder if required
                    Dim soundbanksTrash As String = fso.BuildPath(WorkingDirectory, "SoundBanks_Trash")
                    If Not fso.FolderExists(soundbanksTrash) Then
                        fso.CreateFolder(soundbanksTrash)
                    End If
                    'Move file to trash
                    fso.MoveFile(soundbankPath, fso.BuildPath(soundbanksTrash, soundBankName & ".txt"))
                End If
            Else 'Database
                Dim soundbankNode As TreeNode = TreeView_SoundBanks.SelectedNode.Parent
                If TreeView_SoundBanks.SelectedNode.ImageIndex <> 3 Then
                    'Delete node
                    TreeView_SoundBanks.SelectedNode.Remove()
                    'Add empty node if required
                    If soundbankNode.Nodes.Count = 0 Then
                        soundbankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3)
                    End If
                    'Get databases
                    Dim databases As New List(Of String)
                    For Each childNode As TreeNode In soundbankNode.Nodes
                        If StrComp(childNode.Name, "Empty") = -1 Then
                            databases.Add(childNode.Text)
                        End If
                    Next
                    'Update text file
                    Dim soundbankFilePath As String = fso.BuildPath(WorkingDirectory, "Soundbanks\" & soundbankNode.Text & ".txt")
                    Dim soundbankData As New SoundbankFile With {
                        .HashCode = soundbankNode.Name,
                        .Dependencies = databases.ToArray
                    }
                    writers.UpdateSoundbankFile(soundbankData, soundbankFilePath, textFileReaders)
                End If
            End If

            'Update label
            Label_SoundBanksCount.Text = "Total: " & TreeView_SoundBanks.Nodes.Count
        End If
    End Sub

    Private Sub ContextMenu_TreeView_Rename_Click(sender As Object, e As EventArgs) Handles ContextMenu_TreeView_Rename.Click
        If TreeView_SoundBanks.SelectedNode IsNot Nothing Then
            'Get parent if the user has selected a child node
            Dim soundbankNode As TreeNode = GetSoundbankNode(TreeView_SoundBanks.SelectedNode)
            'Ask for a new name
            Dim diagResult As String = RenameFile(soundbankNode.Text, "Sound Bank", fso.BuildPath(WorkingDirectory, "SoundBanks\"))
            If diagResult IsNot "" Then
                'Move file
                Dim currentFileName As String = fso.BuildPath(WorkingDirectory, "SoundBanks\" & soundbankNode.Text & ".txt")
                fso.MoveFile(currentFileName, fso.BuildPath(WorkingDirectory, "SoundBanks\" & diagResult & ".txt"))
                'Build new file path
                soundbankNode.Text = diagResult
            End If
        End If
    End Sub

    Private Sub ContextMenu_TreeView_Properties_Click(sender As Object, e As EventArgs) Handles ContextMenu_TreeView_Properties.Click
        'Ensure that we have selected a node
        If TreeView_SoundBanks.SelectedNode IsNot Nothing Then
            'Get parent if the user has selected a child node
            Dim soundbankNode As TreeNode = GetSoundbankNode(TreeView_SoundBanks.SelectedNode)
            'Get Soundbank name and file path
            Dim soundbankFullPath As String = fso.BuildPath(WorkingDirectory, "SoundBanks\" & soundbankNode.Text & ".txt")
            'Ensure that the soundbank txt still exists
            If fso.FileExists(soundbankFullPath) Then
                'Get Soundbank HashCode
                Dim soundbankHashCode As Integer = soundbankNode.Name
                'Show form
                Dim sbProperties As New Soundbank_Properties(soundbankFullPath, soundbankNode.Text, soundbankHashCode)
                sbProperties.ShowDialog()
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
        End If
    End Sub

    Private Sub ContextMenu_TreeView_SbMaxSize_Click(sender As Object, e As EventArgs) Handles ContextMenu_TreeView_SbMaxSize.Click
        'Ensure that we have selected a node
        If TreeView_SoundBanks.SelectedNode IsNot Nothing Then
            'Get parent if the user has selected a child node
            Dim soundbankNode As TreeNode = GetSoundbankNode(TreeView_SoundBanks.SelectedNode)
            'Get Soundbank name and file path
            Dim soundbankFullPath As String = fso.BuildPath(WorkingDirectory, "SoundBanks\" & soundbankNode.Text & ".txt")
            'Ensure that the soundbank txt still exists
            If fso.FileExists(soundbankFullPath) Then
                Dim maxSoundbankSize As New SetMaxBankSize(soundbankFullPath)
                maxSoundbankSize.ShowDialog()
            End If
        End If
    End Sub

    '*===============================================================================================
    '* AVAILABLE DATABASES
    '*===============================================================================================
    Private Sub Button_AddDataBases_Click(sender As Object, e As EventArgs) Handles Button_AddDataBases.Click
        AddDataBaseToSoundBank()
    End Sub

    Private Sub ListBox_DataBases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox_DataBases.SelectedIndexChanged
        'Show Database SFXs
        If ListBox_DataBases.SelectedItems.Count = 1 Then
            'Get Database filepath
            Dim selectedDataBase As String = ListBox_DataBases.SelectedItem
            Dim DataBasePath As String = fso.BuildPath(WorkingDirectory, "DataBases\" & selectedDataBase & ".txt")
            'Ensure that the database still exists
            If fso.FileExists(DataBasePath) Then
                'Clear listbox
                ListBox_DataBaseSFX.Items.Clear()
                'Add new items
                Dim dependencies As String() = textFileReaders.ReadDataBaseFile(DataBasePath).Dependencies
                ListBox_DataBaseSFX.Items.AddRange(dependencies)
                'Update counter
                Label_DataBaseSFX.Text = "Total: " & ListBox_DataBaseSFX.Items.Count
            End If
        Else
            ListBox_DataBaseSFX.Items.Clear()
        End If
    End Sub

    Private Sub ListBox_DataBases_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox_DataBases.MouseDoubleClick
        OpenDataBaseProperties()
    End Sub

    '*===============================================================================================
    '* Context Menu Databases
    '*===============================================================================================
    Private Sub ContextMenuDataBases_AddToSoundBank_Click(sender As Object, e As EventArgs) Handles ContextMenuDataBases_AddToSoundBank.Click
        AddDataBaseToSoundBank()
    End Sub

    Private Sub ContextMenuDataBases_New_Click(sender As Object, e As EventArgs) Handles ContextMenuDataBases_New.Click
        Dim folderToCheck As String = fso.BuildPath(WorkingDirectory, "Databases")
        Dim databaseName As String = NewFile(GetFileName(folderToCheck, "DB_Label"), folderToCheck)
        If databaseName IsNot "" Then
            'Create txt
            Dim databaseTxt As String = fso.BuildPath(folderToCheck, databaseName & ".txt")
            writers.UpdateDataBaseText(databaseTxt, Nothing, textFileReaders)
            'Add item to list
            Dim itemIndex As Integer = ListBox_DataBases.Items.Add(databaseName)
            ListBox_DataBases.SelectedIndices.Clear()
            ListBox_DataBases.SelectedIndex = itemIndex
            'Update label
            Label_DataBasesCount.Text = "Total: " & ListBox_DataBases.Items.Count
        End If
    End Sub

    Private Sub ContextMenuDataBases_Copy_Click(sender As Object, e As EventArgs) Handles ContextMenuDataBases_Copy.Click
        If ListBox_DataBases.SelectedItems.Count = 1 Then
            'Ask user
            Dim sfxCopyName As String = CopyFile(vbCrLf & ListBox_DataBases.SelectedItem, "Database", fso.BuildPath(WorkingDirectory, "DataBases\"))
            If sfxCopyName IsNot "" Then
                'Read original file content
                Dim originalFilePath As String = fso.BuildPath(WorkingDirectory, "DataBases\" & ListBox_DataBases.SelectedItem & ".txt")
                fso.CopyFile(originalFilePath, fso.BuildPath(WorkingDirectory, "DataBases\" & sfxCopyName & ".txt"))
            End If
        End If
    End Sub

    Private Sub ContextMenuDataBases_Delete_Click(sender As Object, e As EventArgs) Handles ContextMenuDataBases_Delete.Click
        'Ensure that there is an item selected
        If ListBox_DataBases.SelectedItems.Count > 0 Then
            'Create a list with the items that we have to remove
            Dim itemsToDelete As New List(Of String)
            For Each itemToRemove As String In ListBox_DataBases.SelectedItems
                itemsToDelete.Add(itemToRemove)
            Next

            'Ask user what he wants to do
            Dim answerQuestion As MsgBoxResult = MsgBox(MultipleDeletionMessage("Are you sure you want to delete Database(s)", itemsToDelete), vbInformation + vbYesNo, "Confirm Database Deletion")
            If answerQuestion = MsgBoxResult.Yes Then
                'Create Trash Folder if required
                Dim databaseTrash As String = fso.BuildPath(WorkingDirectory, "Databases_Trash")
                If Not fso.FolderExists(databaseTrash) Then
                    fso.CreateFolder(databaseTrash)
                End If
                'Get Soundbanks files
                Dim soundbankFiles As String() = Directory.GetFiles(fso.BuildPath(WorkingDirectory, "Soundbanks"), "*.txt", SearchOption.TopDirectoryOnly)
                For i As Integer = 0 To soundbankFiles.Length - 1
                    'Update soundbank file
                    Dim soundbankFile As SoundbankFile = textFileReaders.ReadSoundBankFile(soundbankFiles(i))
                    Dim resultList As List(Of String) = soundbankFile.Dependencies.Except(itemsToDelete).ToList
                    If resultList.Count < soundbankFile.Dependencies.Length Then
                        soundbankFile.Dependencies = resultList.ToArray
                        writers.UpdateSoundbankFile(soundbankFile, soundbankFiles(i), textFileReaders, False)
                    End If
                Next
                'Update UI
                ListBox_DataBases.BeginUpdate()
                For i As Integer = 0 To itemsToDelete.Count - 1
                    'Delete file 
                    Dim fileFullPath As String = fso.BuildPath(WorkingDirectory, "Databases\" & itemsToDelete(i) & ".txt")
                    fso.CopyFile(fileFullPath, fso.BuildPath(databaseTrash, itemsToDelete(i) & ".txt"))
                    fso.DeleteFile(fileFullPath)
                    'Delete item from the list 
                    ListBox_DataBases.Items.Remove(itemsToDelete(i))
                    'Delete from the soundbank
                    For Each soundbank As TreeNode In TreeView_SoundBanks.Nodes
                        DeleteDatabaseFromSoundbank(soundbank, itemsToDelete(i))
                    Next
                Next
                ListBox_DataBases.EndUpdate()
                'Update counter
                Label_DataBasesCount.Text = "Total: " & ListBox_DataBases.Items.Count
                'Update Project file
                Dim databasesToWrite As String() = ListBox_DataBases.Items.Cast(Of String).ToArray
                Dim sfxsToWriter As String() = UserControl_SFXs.ListBox_SFXs.Items.Cast(Of String).ToArray
                Dim soundbanksList As String() = GetListOfSoundbanks(TreeView_SoundBanks)
                writers.CreateProjectFile(fso.BuildPath(WorkingDirectory, "Project.txt"), soundbanksList, databasesToWrite, sfxsToWriter)
            End If
        End If
    End Sub

    Private Sub ContextMenuDataBases_Rename_Click(sender As Object, e As EventArgs) Handles ContextMenuDataBases_Rename.Click
        If ListBox_DataBases.SelectedItems.Count = 1 Then
            'Get current fileName
            Dim selectedName As String = ListBox_DataBases.SelectedItem
            Dim currentFileName As String = fso.BuildPath(WorkingDirectory, "Databases\" & selectedName & ".txt")
            'Ask for a new name
            Dim diagResult As String = RenameFile(selectedName, "Database", fso.BuildPath(WorkingDirectory, "Databases\"))
            If diagResult IsNot "" Then
                'Build new file path
                Dim newFileName = fso.BuildPath(WorkingDirectory, "Databases\" & diagResult & ".txt")
                If Not fso.FileExists(newFileName) Then
                    'Rename file and update list item
                    fso.MoveFile(currentFileName, newFileName)
                    'Update Listbox
                    Dim itemPos As Integer = ListBox_DataBases.Items.IndexOf(selectedName)
                    If itemPos <> ListBox.NoMatches Then
                        ListBox_DataBases.Items(itemPos) = diagResult
                    End If
                    'Update databases in the soundbanks
                    For Each node As TreeNode In TreeView_SoundBanks.Nodes
                        'Boolean
                        Dim updateTextFile As Boolean = False
                        Dim dependenciesList As New List(Of String)
                        'Update nodes
                        If node.Nodes.Count > 0 Then
                            For Each childNode As TreeNode In node.Nodes
                                'Update GUI
                                If StrComp(childNode.Text, selectedName) = 0 Then
                                    childNode.Name = diagResult
                                    childNode.Text = diagResult
                                    'Update boolean
                                    updateTextFile = True
                                End If
                                'Add item to list
                                dependenciesList.Add(childNode.Text)
                            Next
                        End If
                        'Update text file if required
                        If updateTextFile Then
                            Dim soundbankFilePath As String = fso.BuildPath(WorkingDirectory, "Soundbanks\" & node.Text & ".txt")
                            Dim soundbankFile As SoundbankFile = textFileReaders.ReadSoundBankFile(soundbankFilePath)
                            soundbankFile.Dependencies = dependenciesList.ToArray
                            writers.UpdateSoundbankFile(soundbankFile, soundbankFilePath, textFileReaders)
                        End If
                    Next
                End If
            End If
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub

    Private Sub ContextMenuDataBases_Properties_Click(sender As Object, e As EventArgs) Handles ContextMenuDataBases_Properties.Click
        OpenDataBaseProperties()
    End Sub

    '*===============================================================================================
    '* SFXs IN DATABASE SECTION
    '*===============================================================================================
    Private Sub ListBox_DataBaseSFX_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox_DataBaseSFX.MouseDoubleClick
        'Ensure that we have selected an item
        Dim itemIndex = ListBox_DataBaseSFX.IndexFromPoint(e.Location)
        If itemIndex <> ListBox.NoMatches Then
            'Get item and file path
            Dim selectedSFX As String = ListBox_DataBaseSFX.Items(itemIndex)
            Dim SelectedSfxPath = fso.BuildPath(WorkingDirectory, "SFXs\" & selectedSFX & ".txt")
            'Ensure that the file exists
            If fso.FileExists(SelectedSfxPath) Then
                'Open editor
                Dim sfxEditor As New Frm_SfxEditor(selectedSFX)
                sfxEditor.ShowDialog()
            End If
        End If
    End Sub

    Private Sub ListBox_DataBaseSFX_DragOver(sender As Object, e As DragEventArgs) Handles ListBox_DataBaseSFX.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub ListBox_DataBaseSFX_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox_DataBaseSFX.DragDrop
        If e.Effect = DragDropEffects.Copy AndAlso ListBox_DataBases.SelectedItems.Count = 1 Then
            If e.Data.GetDataPresent(GetType(ListBox.SelectedObjectCollection)) Then
                Dim itemsData As ListBox.SelectedObjectCollection = e.Data.GetData(GetType(ListBox.SelectedObjectCollection))
                For Each sfxItem As String In itemsData
                    'Ensure that the item does not exist in this database
                    If Not ListBox_DataBaseSFX.Items.Contains(sfxItem) Then
                        'Add item to the listbox
                        ListBox_DataBaseSFX.Items.Add(sfxItem)
                    End If
                Next
                'Update text file
                If itemsData.Count > 0 Then
                    Dim databaseTxt As String = fso.BuildPath(WorkingDirectory, "DataBases\" & ListBox_DataBases.SelectedItem & ".txt")
                    Dim databaseDependencies As String() = ListBox_DataBaseSFX.Items.Cast(Of String).ToArray
                    writers.UpdateDataBaseText(databaseTxt, databaseDependencies, textFileReaders)
                End If
            End If
        End If
    End Sub

    Private Sub Button_RemoveSFXs_Click(sender As Object, e As EventArgs) Handles Button_RemoveSFXs.Click
        RemoveSfxFromDatabase()
    End Sub

    '*===============================================================================================
    '* DATABASES SFX CONTEXT MENU
    '*===============================================================================================
    Private Sub DataBasesSFX_Remove_Click(sender As Object, e As EventArgs) Handles DataBasesSFX_Remove.Click
        RemoveSfxFromDatabase()
    End Sub

    Private Sub DataBasesSFX_Properties_Click(sender As Object, e As EventArgs) Handles DataBasesSFX_Properties.Click
        'Ensure that we have selected an item
        If ListBox_DataBaseSFX.SelectedItems.Count = 1 Then
            'Get item name and full path
            Dim selectedSFX As String = ListBox_DataBaseSFX.SelectedItem
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

    Private Sub DataBasesSFX_Edit_Click(sender As Object, e As EventArgs) Handles DataBasesSFX_Edit.Click
        'Get selected item
        If ListBox_DataBaseSFX.SelectedItems.Count = 1 Then
            'Get item and file path
            Dim selectedSFX As String = ListBox_DataBaseSFX.SelectedItem
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

    Private Sub DataBasesSFX_SelectSFX_Click(sender As Object, e As EventArgs) Handles DataBasesSFX_SelectSFX.Click
        'Get selected item
        If ListBox_DataBaseSFX.SelectedItems.Count > 0 Then
            Dim selectedSFX As String = ListBox_DataBaseSFX.SelectedItem
            'Search this SFX in the SFXs list
            Dim itemIndex = UserControl_SFXs.ListBox_SFXs.FindString(selectedSFX)
            'If we have results, select this item
            If itemIndex <> ListBox.NoMatches Then
                UserControl_SFXs.ListBox_SFXs.SelectedIndex = itemIndex
            End If
        End If
    End Sub

    Private Sub DataBasesSFX_MultiEditor_Click(sender As Object, e As EventArgs) Handles DataBasesSFX_MultiEditor.Click
        Dim listOfSFXs As New List(Of String)
        For itemIndex As Integer = 0 To ListBox_DataBaseSFX.SelectedItems.Count - 1
            listOfSFXs.Add(fso.BuildPath(WorkingDirectory & "\SFXs", ListBox_DataBaseSFX.SelectedItems(itemIndex) & ".txt"))
        Next

        Dim multiEditor As New SfxMultiEditor(listOfSFXs.ToArray)
        multiEditor.ShowDialog()
    End Sub

    '*===============================================================================================
    '* AVAILABLE SFXs SECTION
    '*===============================================================================================
    Private Sub Button_AddSFXs_Click(sender As Object, e As EventArgs) Handles Button_AddSFXs.Click
        'Ensure that we have selected an item
        If UserControl_SFXs.ListBox_SFXs.SelectedItems.Count > 0 AndAlso ListBox_DataBases.SelectedItems.Count = 1 Then
            'Get selected items
            Dim selectedItems As ListBox.SelectedObjectCollection = UserControl_SFXs.ListBox_SFXs.SelectedItems
            For Each sfxItem As String In selectedItems
                'Ensure that the item does not exist in this database
                If Not ListBox_DataBaseSFX.Items.Contains(sfxItem) Then
                    'Add item to the listbox
                    ListBox_DataBaseSFX.Items.Add(sfxItem)
                End If
            Next
            'Get list of items
            Dim databaseDependencies As String() = ListBox_DataBaseSFX.Items.Cast(Of String).ToArray
            'Update text file
            Dim databaseTxt As String = fso.BuildPath(WorkingDirectory, "DataBases\" & ListBox_DataBases.SelectedItem & ".txt")
            writers.UpdateDataBaseText(databaseTxt, databaseDependencies, textFileReaders)
            'Update label
            Label_DataBaseSFX.Text = "Total: " & ListBox_DataBaseSFX.Items.Count
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        End If
    End Sub

    '*===============================================================================================
    '* MISC BUTTONS
    '*===============================================================================================
    Private Sub Button_ProjectProperties_Click(sender As Object, e As EventArgs) Handles Button_ProjectProperties.Click
        'Show form
        Dim projectProps As New Project_Properties
        projectProps.ShowDialog()
    End Sub

    Private Sub Button_ReSampling_Click(sender As Object, e As EventArgs) Handles Button_ReSampling.Click
        If fso.FolderExists(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")) Then
            If fso.FileExists(SysFileSamples) Then
                'Set cursor as hourglass
                Cursor.Current = Cursors.WaitCursor

                'Parte text file to a datatable
                Dim missingSamples As New List(Of String)
                Dim newSamples As New List(Of String)
                Dim samplesDataTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)

                '============================================================Check missing samples======================================================
                'Check that all samples exists
                For index As Integer = samplesDataTable.Rows.Count - 1 To 0 Step -1
                    Dim dr As DataRow = samplesDataTable.Rows(index)
                    Dim sampleFullPath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder & "\Master", dr("SampleFilename"))
                    'Add item to missing samples list and remove item form data table
                    If Not fso.FileExists(sampleFullPath) Then
                        missingSamples.Add(dr("SampleFilename"))
                        dr.Delete()
                    End If
                Next
                samplesDataTable.AcceptChanges()

                '============================================================Check New samples======================================================
                'Get a list of current stored samples
                Dim sampleFiles As New HashSet(Of String)
                Dim rowsEnum As IEnumerator = samplesDataTable.Rows.GetEnumerator
                While rowsEnum.MoveNext
                    sampleFiles.Add(rowsEnum.Current("SampleFilename"))
                End While

                'Check for new samples
                If fso.FolderExists(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")) Then
                    Dim filesList As String() = Directory.GetFiles(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master"), "*.wav", SearchOption.AllDirectories)
                    Dim substrStart = Len(ProjectSettingsFile.MiscProps.SampleFileFolder & "\Master\")
                    For index As Integer = 0 To filesList.Count - 1
                        Dim relPath As String = Mid(filesList(index), substrStart)
                        If Not sampleFiles.Contains(relPath) Then
                            newSamples.Add(relPath)
                            samplesDataTable.Rows.Add(relPath, "Default", FileLen(filesList(index)), FileDateTime(filesList(index)).ToString(dateFormat), "False", "False", "", "", "", "")
                        End If
                    Next
                    samplesDataTable.AcceptChanges()
                End If

                'Sort Table
                Dim finalSamplesDataTable As DataTable = samplesDataTable

                '============================================================Save Changes======================================================
                'Show missing samples form and update text file
                If missingSamples.Count > 0 Then
                    'Sort list A->Z
                    missingSamples.Sort()
                    'Update Text file
                    writers.SaveSamplesFile(SysFileSamples, finalSamplesDataTable)
                    'Inform user about the missing samples
                    Dim missingSamplesForm As New MissingSamples(missingSamples.ToArray)
                    missingSamplesForm.ShowDialog()
                End If

                'Show new samples form and update text file
                If newSamples.Count > 0 Then
                    'Sort list A->Z
                    newSamples.Sort()
                    'Inform user about the missing samples
                    Dim newSamplesForm As New NewSamples(newSamples.ToArray)
                    newSamplesForm.ShowDialog()
                    'Update Resample Rate for the new samples
                    If newSamplesForm.DialogResult = DialogResult.OK Then
                        Dim listEnum As List(Of String).Enumerator = newSamples.GetEnumerator
                        While listEnum.MoveNext
                            For Each row As DataRow In finalSamplesDataTable.Rows
                                If StrComp(row("SampleFilename"), listEnum.Current) = 0 Then
                                    row("ReSampleRate") = newSamplesForm.ComboBox_AvailableRates.SelectedItem
                                    row("ReSample") = "True"
                                    Exit For
                                End If
                            Next
                        End While
                    End If
                    'Sort table
                    Dim dv As DataView = samplesDataTable.DefaultView
                    dv.Sort = "SampleFilename asc"
                    finalSamplesDataTable = dv.ToTable
                    'Update Text file
                    writers.SaveSamplesFile(SysFileSamples, finalSamplesDataTable)
                End If

                '============================================================Show form======================================================
                Dim resampleForm As New ResampleForm(finalSamplesDataTable)
                resampleForm.ShowDialog()
            Else
                MsgBox("File not found", vbOKOnly + vbCritical, "EuroSound")
            End If
        Else
            MsgBox("Master folder could not be located. Please set the folder location under Properties menu.", vbOKOnly + vbCritical, "EuroSound")
            Dim projectPropsform As New Project_Properties
            projectPropsform.ShowDialog()
        End If
    End Sub

    Private Sub Button_Advanced_Click(sender As Object, e As EventArgs) Handles Button_Advanced.Click
        Dim advancedMenuForm As New AdvancedMenu
        advancedMenuForm.ShowDialog()
    End Sub

    Private Sub Button_MusicMaker_Click(sender As Object, e As EventArgs) Handles Button_MusicMaker.Click
        Dim musicTool As New MusicMaker
        musicTool.ShowDialog()
    End Sub

    Private Sub Button_SfxDefault_Click(sender As Object, e As EventArgs) Handles Button_SfxDefault.Click
        Dim sfxDefaultForm As New SfxDefault
        sfxDefaultForm.ShowDialog()
    End Sub

    Private Sub Button_MarkersEditor_Click(sender As Object, e As EventArgs) Handles Button_MarkersEditor.Click
        Dim markersEditor As New MusicMarkersEditor
        markersEditor.ShowDialog()
    End Sub

    Private Sub Textbox_DebugInfo_Click(sender As Object, e As EventArgs) Handles Textbox_DebugInfo.Click
        Dim debugForm As New Frm_DebugData(Textbox_DebugInfo.Text.Split(vbCrLf))
        debugForm.ShowDialog()
    End Sub

    '*===============================================================================================
    '* OUTPUT BUTTONS
    '*===============================================================================================
    Private Sub Button_FullOutput_Click(sender As Object, e As EventArgs) Handles Button_FullOutput.Click
        'Clear textbox
        Textbox_DebugInfo.Clear()
        'Open Resample Form
        Hide()
        Dim taskForm As New ExporterForm(False)
        taskForm.ShowDialog()
        Show()
    End Sub

    Private Sub Button_QuickOutput_Click(sender As Object, e As EventArgs) Handles Button_QuickOutput.Click
        'Clear textbox
        Textbox_DebugInfo.Clear()
        'Open Resample Form
        Hide()
        Dim taskForm As New ExporterForm(True)
        taskForm.ShowDialog()
        Show()
    End Sub
End Class

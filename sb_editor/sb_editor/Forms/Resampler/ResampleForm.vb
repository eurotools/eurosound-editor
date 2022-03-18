Imports System.IO
Imports IniFileFunctions
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Partial Public Class ResampleForm
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private promptSave As Boolean = True
    Private dataTableInfo As DataTable
    Private ReadOnly writers As New FileWriters
    Private ReadOnly textFileReaders As New FileParsers

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(dataTableToPrint As DataTable)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        dataTableInfo = dataTableToPrint
    End Sub

    Private Sub ResampleForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Measure execution time
        Dim stopwatch As New Stopwatch
        stopwatch.Start()

        'Get Master folder file path and select the first preview option
        TextBox_ProjectPath.Text = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")
        ComboBox_PreviewOptions.SelectedIndex = 0

        'Get available sample rates
        ComboBox_AvailableRates.Items.AddRange(ProjectSettingsFile.AvailableReSampleRates.ToArray)
        ComboBox_AvailableRates.SelectedIndex = 0

        'Get samples listview
        DataTableToListView()
        Label_SampleCount.Text = "Sample Count: " & ListView_Samples.Items.Count

        'Read move samples to
        Dim iniFunctions As New IniFile(SysFileProjectIniPath)
        Dim moveSamplesFolder = iniFunctions.Read("Text3", "ReSampleForm")
        If StrComp(moveSamplesFolder, "") = 0 Then
            TextBox_MoveSamplesTo.Text = "Set Folder"
        Else
            TextBox_MoveSamplesTo.Text = moveSamplesFolder
        End If

        'Add formats to combobox
        ComboBox_PreviewOptions.Items.AddRange(ProjectSettingsFile.sampleRateFormats.Keys.ToArray)

        'Show elapsed time
        stopwatch.Stop()
        TextBox_BootupTime.Text = "Bootup Time =  " & stopwatch.Elapsed.TotalMilliseconds

        'Set cursor as default arrow
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub ResampleForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Stop sound
        My.Computer.Audio.Stop()
        'Check closing reason
        If e.CloseReason = CloseReason.UserClosing Then
            'Check if we have to show the message
            If promptSave Then
                'Ask user what wants to do
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
                Dim save As MsgBoxResult = MsgBox("Are you sure you wish to quit without saving?", vbOKCancel + vbQuestion, "Confirm Quit")
                'Cancel close if user not want to quit
                If save = MsgBoxResult.Cancel Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        'Disable save file message
        promptSave = False
        'Save file
        writers.SaveSamplesFile(SysFileSamples, ListViewToDataTable())
        'Close form
        Close()
    End Sub

    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        'Close form
        Close()
    End Sub

    Private Sub TextBox_MoveSamplesTo_DoubleClick(sender As Object, e As EventArgs) Handles TextBox_MoveSamplesTo.DoubleClick
        Dim diagResult = FolderBrowser.ShowDialog()
        If diagResult = DialogResult.OK Then
            TextBox_MoveSamplesTo.Text = FolderBrowser.SelectedPath
            'Save data in the Ini File
            Dim iniFunctions As New IniFile(SysFileProjectIniPath)
            iniFunctions.Write("Text3", FolderBrowser.SelectedPath, "ReSampleForm")
        End If
    End Sub

    '*===============================================================================================
    '* MAIN MENU SAMPLE
    '*===============================================================================================
    Private Sub MainMenu_SamplePlay_Click(sender As Object, e As EventArgs) Handles MainMenu_SamplePlay.Click
        PlaySelectedSample()
    End Sub

    Private Sub MainMenu_SampleStop_Click(sender As Object, e As EventArgs) Handles MainMenu_SampleStop.Click
        'Stop audio
        My.Computer.Audio.Stop()
    End Sub

    Private Sub MainMenu_SampleEdit_Click(sender As Object, e As EventArgs) Handles MainMenu_SampleEdit.Click
        EditAudioFile()
    End Sub

    '*===============================================================================================
    '* LISTVIEW CONTEXT MENU
    '*===============================================================================================
    Private Sub MenuContext_Play_Click(sender As Object, e As EventArgs) Handles MenuContext_Play.Click
        PlaySelectedSample()
    End Sub

    Private Sub MenuContext_Stop_Click(sender As Object, e As EventArgs) Handles MenuContext_Stop.Click
        'Stop audio
        My.Computer.Audio.Stop()
    End Sub

    Private Sub MenuContext_Edit_Click(sender As Object, e As EventArgs) Handles MenuContext_Edit.Click
        EditAudioFile()
    End Sub

    '*===============================================================================================
    '* PREVIEW SAMPLE EVENTS
    '*===============================================================================================
    Private Sub Button_Preview_Click(sender As Object, e As EventArgs) Handles Button_Preview.Click
        PlaySelectedSample()
    End Sub

    Private Sub Button_Stop_Click(sender As Object, e As EventArgs) Handles Button_Stop.Click
        'Stop audio
        My.Computer.Audio.Stop()
    End Sub

    '*===============================================================================================
    '* LISTVIEW EVENTS
    '*===============================================================================================
    Private Sub ListView_Samples_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_Samples.SelectedIndexChanged
        'Ensure that the selection is not null
        If ListView_Samples.SelectedItems.Count > 0 Then
            'Get item associated sample rate
            Dim selectedRate = ListView_Samples.SelectedItems(0).SubItems(1).Text
            'Ensure that this sample rate is available
            If ComboBox_AvailableRates.Items.Contains(selectedRate) Then
                'Change combo item
                ComboBox_AvailableRates.SelectedItem = selectedRate
            End If
        End If
    End Sub

    Private Sub ListView_Samples_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView_Samples.MouseDoubleClick
        EditAudioFile()
    End Sub

    '*===============================================================================================
    '* COMBOBOX EVENTS
    '*===============================================================================================
    Private Sub ComboBox_AvailableRates_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox_AvailableRates.SelectionChangeCommitted
        'Ensure that the selection is not null
        If ListView_Samples.SelectedItems.Count > 0 AndAlso ComboBox_AvailableRates.SelectedItem IsNot Nothing Then
            'Iterate over all selected items
            For Each item As ListViewItem In ListView_Samples.SelectedItems
                'Apply new sample rate
                item.SubItems(1).Text = ComboBox_AvailableRates.SelectedItem
                'Update Resample booelan
                item.SubItems(4).Text = "True"
                'Update global var
                If StrComp(item.SubItems(5).Text, "True") = 0 Then
                    ReSampleStreams = 1
                End If
            Next
        End If
    End Sub

    '*===============================================================================================
    '* MISC BUTTONS
    '*===============================================================================================
    Private Sub Button_EditSample_Click(sender As Object, e As EventArgs) Handles Button_EditSample.Click
        EditAudioFile()
    End Sub

    Private Sub Button_StreamSel_Click(sender As Object, e As EventArgs) Handles Button_StreamSel.Click
        'Ensure that the selection is not null
        If ListView_Samples.SelectedItems.Count > 0 Then
            'Iterate over all selected items
            For Each item As ListViewItem In ListView_Samples.SelectedItems
                item.SubItems(4).Text = "True"
                item.SubItems(5).Text = "True"
            Next
            'Update global varibale
            ReSampleStreams = 1
        End If
    End Sub

    Private Sub Button_UnStreamSel_Click(sender As Object, e As EventArgs) Handles Button_UnStreamSel.Click
        'Ensure that the selection is not null
        If ListView_Samples.SelectedItems.Count > 0 Then
            'Iterate over all selected items
            For Each item As ListViewItem In ListView_Samples.SelectedItems
                item.SubItems(5).Text = "False"
            Next
        End If
    End Sub

    Private Sub Button_ReSampleAll_Click(sender As Object, e As EventArgs) Handles Button_ReSampleAll.Click
        'Start list update
        ListView_Samples.BeginUpdate()
        'Update booleans
        For Each item As ListViewItem In ListView_Samples.Items
            item.SubItems(4).Text = "True"
        Next
        'Update global varibale
        ReSampleStreams = 1
        'End list update
        ListView_Samples.EndUpdate()
    End Sub

    Private Sub Button_DeReSampleAll_Click(sender As Object, e As EventArgs) Handles Button_DeReSampleAll.Click
        'Start list update
        ListView_Samples.BeginUpdate()
        'Update booleans
        For Each item As ListViewItem In ListView_Samples.Items
            item.SubItems(4).Text = "False"
        Next
        'Update global varibale
        ReSampleStreams = 0
        'End list update
        ListView_Samples.EndUpdate()
    End Sub

    Private Sub Button_MoveSelection_Click(sender As Object, e As EventArgs) Handles Button_MoveSelection.Click
        If StrComp(TextBox_MoveSamplesTo.Text, "Set Folder") = 0 Then
            MsgBox("Please Set Folder by Clicking TextBox.", vbOKOnly + vbCritical, "Move File Error")
        Else
            If Directory.Exists(TextBox_MoveSamplesTo.Text) Then
                'Build master path
                Dim masterFolder As String = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master")
                'Ensure that we are not moving outside master folder
                If InStr(TextBox_MoveSamplesTo.Text, masterFolder) > 0 Then
                    'Ask user what he wants to do
                    Dim userAnswer As MsgBoxResult = MsgBox("Are You Sure You Want To Move Selection?", vbYesNo + vbQuestion, "Confirm File Move")
                    If userAnswer = MsgBoxResult.Yes Then
                        Dim substrStartIndex = Len(masterFolder) + 2
                        'Set cursor as hourglass
                        Cursor.Current = Cursors.WaitCursor
                        'Update UI
                        Dim filePathsDict As New Dictionary(Of String, String)
                        ListView_Samples.BeginUpdate()
                        For Each listItem As ListViewItem In ListView_Samples.SelectedItems
                            'Get file paths
                            Dim sourceFilePath As String = Path.Combine(masterFolder, listItem.Text)
                            Dim relativeSourceFilePath As String = LCase(listItem.Text.Trim.TrimStart("\"))
                            Dim destFilePath As String = Path.Combine(TextBox_MoveSamplesTo.Text, Path.GetFileName(listItem.Text))
                            Dim relativeDestFilePath As String = Mid(destFilePath, substrStartIndex)
                            'Move file
                            If File.Exists(destFilePath) Then
                                MsgBox("Cannot Move File: '" & sourceFilePath & "' because of duplicate Name: " & Path.Combine(destFilePath), vbOKOnly + vbCritical, "Move File Error")
                            Else
                                File.Move(sourceFilePath, destFilePath)
                                'Update listview control
                                listItem.Text = "\" & relativeDestFilePath
                                'Add item to dictionary
                                filePathsDict.Add(relativeSourceFilePath, relativeDestFilePath)
                            End If
                        Next
                        ListView_Samples.EndUpdate()
                        'Update Text Files
                        If filePathsDict.Count > 0 Then
                            Dim fileList As IEnumerable(Of String) = Directory.GetFiles(Path.Combine(WorkingDirectory, "SFXs"), "*.txt", SearchOption.AllDirectories)
                            Dim fileListEnum As IEnumerator(Of String) = fileList.GetEnumerator
                            While fileListEnum.MoveNext
                                Dim fileModified As Boolean = False
                                Dim sfxTextFileData As String() = IO.File.ReadAllLines(fileListEnum.Current)
                                Dim startPos As Integer = Array.IndexOf(sfxTextFileData, "#SFXSamplePoolFiles") + 1
                                Do While StrComp(sfxTextFileData(startPos), "#END") <> 0
                                    Dim keyToSearch As String = LCase(sfxTextFileData(startPos).Trim.TrimStart("\"))
                                    If filePathsDict.ContainsKey(keyToSearch) Then
                                        sfxTextFileData(startPos) = filePathsDict(keyToSearch)
                                        fileModified = True
                                    End If
                                    startPos += 1
                                Loop
                                'Write file again  and quit
                                If fileModified Then
                                    IO.File.WriteAllLines(fileListEnum.Current, sfxTextFileData)
                                End If
                            End While
                            'Save changes
                            writers.SaveSamplesFile(SysFileSamples, ListViewToDataTable)
                        End If
                        'Set cursor as default arrow
                        Cursor.Current = Cursors.Default
                    End If
                Else
                    MsgBox("Cannot Move Outside Master Folder: " & masterFolder, vbOKOnly + vbCritical, "Move File Error")
                End If
            Else
                MsgBox("The Specified Folder Does not Exist", vbOKOnly + vbCritical, "Move File Error")
            End If
        End If
    End Sub

    '*===============================================================================================
    '* TEMPORAL BUTTONS
    '*===============================================================================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Update list
        ListView_Samples.BeginUpdate()
        'Iterate over all items
        For Each item As ListViewItem In ListView_Samples.Items
            'Trim Size Field
            item.SubItems(2).Text = item.SubItems(2).Text.Trim()
            'Get New Date
            Dim modifiedDate As String = FileDateTime(TextBox_ProjectPath.Text & item.Text).ToString(dateFormat)
            item.SubItems(3).Text = modifiedDate
        Next
        'end
        ListView_Samples.EndUpdate()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each itemToDelete As ListViewItem In ListView_Samples.SelectedItems
            itemToDelete.Remove()
        Next
    End Sub

    '*===============================================================================================
    '* PURGE BUTTONS
    '*===============================================================================================
    Private Sub Button_MakePurgeList_Click(sender As Object, e As EventArgs) Handles Button_MakePurgeList.Click
        Dim purgeListMaker As New Frm_MakePurgeList
        purgeListMaker.ShowDialog()
        Show()
    End Sub

    Private Sub Button_ViewPurgedList_Click(sender As Object, e As EventArgs) Handles Button_ViewPurgedList.Click
        Dim purgeListFilePath As String = Path.Combine(WorkingDirectory, "Report", "Last_Purge.txt")
        If File.Exists(purgeListFilePath) Then
            If File.Exists(ProjTextEditor) Then
                RunProcess(ProjTextEditor, purgeListFilePath)
            Else
                MsgBox("No Text Editor setup." & vbCrLf & "Use Properties form to setup one.", vbOKOnly + vbExclamation, "EuroSound")
            End If
        Else
            MsgBox("No Purge file found", vbOKOnly + vbExclamation, "EuroSound")
        End If
    End Sub

    Private Sub Button_PurgeGo_Click(sender As Object, e As EventArgs) Handles Button_PurgeGo.Click
        Dim diagResult As MsgBoxResult = MsgBox("Are you Sure You want to Purge UnUsed Files?", vbYesNo + vbQuestion, "Confirm Purge")
        If diagResult = MsgBoxResult.Yes Then
            Dim purgeGoForm As New Frm_PurgeGo()
            purgeGoForm.ShowDialog()
            Show()
            'Update Listview 
            ListView_Samples.BeginUpdate()
            ListView_Samples.Items.Clear()
            dataTableInfo = textFileReaders.SamplesFileToDatatable(SysFileSamples)
            DataTableToListView()
            ListView_Samples.EndUpdate()
            'Update Counter
            Label_SampleCount.Text = "Sample Count: " & ListView_Samples.Items.Count
        End If
    End Sub
End Class
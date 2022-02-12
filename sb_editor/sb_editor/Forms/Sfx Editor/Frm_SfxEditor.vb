Imports System.Media

Partial Public Class Frm_SfxEditor
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly sfxFileName As String
    Private ReadOnly writers As New FileWriters
    Private ReadOnly reader As New FileParsers
    Private ReadOnly sfxFilesData As New Dictionary(Of String, SfxFile)
    Private ReadOnly waveReadFunctions As New WaveFunctions
    Private StreamSamplesList As String()
    Private promptSave As Boolean = True

    Sub New(fileName As String)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        sfxFileName = fileName
    End Sub

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub Frm_SfxEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Hide mainframe
        Dim MainFrame As Form = CType(Application.OpenForms("MainFrame"), MainFrame)
        MainFrame.Hide()

        Text = sfxFileName
        Label_SFX_Name.Text = ">Name: " & sfxFileName
        SfxParamsAndSamplePool.Textbox_SfxName.Text = sfxFileName
        ComboBox1.SelectedIndex = 0

        'Get stream sounds list
        StreamSamplesList = reader.GetStreamSoundsList(SysFileSamples)

        'Check if the misc folder exists
        Dim miscFolder As String = fso.BuildPath(WorkingDirectory, "SFXs\Misc")
        If Not fso.FolderExists(miscFolder) Then
            MkDir(miscFolder)
        End If

        'Add Common Tab Page
        Dim baseFilePath = fso.BuildPath(WorkingDirectory, "SFXs\" & sfxFileName & ".txt")
        If fso.FileExists(baseFilePath) Then
            Dim commonTextFile As String = fso.BuildPath(WorkingDirectory, "SFXs\Misc\Common.txt")
            fso.CopyFile(baseFilePath, commonTextFile, True)
            sfxFilesData.Add("Common", reader.ReadSFXFile(commonTextFile))
        End If

        'Add Specific Formats Tab Pages
        Dim availablePlatforms As String() = New String() {"PlayStation2", "GameCube", "X Box", "PC"}
        For index As Integer = 0 To availablePlatforms.Length - 1
            'Create folder if not exists
            Dim folderPath As String = fso.BuildPath(WorkingDirectory, "SFXs\" & availablePlatforms(index))
            If Not fso.FolderExists(folderPath) Then
                MkDir(folderPath)
            End If
            'Check if the request file exists
            baseFilePath = fso.BuildPath(folderPath, sfxFileName & ".txt")
            If fso.FileExists(baseFilePath) Then
                Dim platformTextFile As String = fso.BuildPath(WorkingDirectory, "SFXs\Misc\" & availablePlatforms(index) & ".txt")
                fso.CopyFile(baseFilePath, platformTextFile, True)
                sfxFilesData.Add(CreateTab(availablePlatforms(index)).Text, reader.ReadSFXFile(platformTextFile))
                'Disable Button
                Select Case index
                    Case 0
                        Button_SpecVersion_PlayStation2.Enabled = False
                    Case 1
                        Button_SpecVersion_GameCube.Enabled = False
                    Case 2
                        Button_SpecVersion_Xbox.Enabled = False
                    Case 3
                        Button_SpecVersion_PC.Enabled = False
                End Select
            End If
        Next

        'Inherited from common file and applied to all other formats
        ShowSfxParameters(TabControl_Platforms.SelectedTab.Text)
        'Show common file
        ShowSfxSamplePoolControl(TabControl_Platforms.SelectedTab.Text)
        ShowSfxSamplePool(TabControl_Platforms.SelectedTab.Text)
        'Enable clipboard
        If fso.FileExists(fso.BuildPath(WorkingDirectory, "SFXs\Misc\ClipBoard.txt")) Then
            Button_ClipboardPaste.Enabled = True
        End If
    End Sub

    Private Sub Frm_SfxEditor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Check closing reason
        If e.CloseReason = CloseReason.UserClosing Then
            'Check if we have to show the message
            If promptSave Then
                'Ask user what wants to do
                My.Computer.Audio.PlaySystemSound(SystemSounds.Exclamation)
                Dim save As MsgBoxResult = MsgBox("Are you sure you wish to quit without saving?", vbOKCancel + vbQuestion, "Confirm Quit")
                If save = MsgBoxResult.Cancel Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub Frm_SfxEditor_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Close hashcodes list form
        Dim subSfxListForm = CType(Application.OpenForms("HashCodesList"), HashCodesList)
        If subSfxListForm IsNot Nothing Then
            subSfxListForm.Close()
        End If
        'Stop audio playing
        My.Computer.Audio.Stop()
        'Show mainframe again
        Dim MainFrame As Form = CType(Application.OpenForms("MainFrame"), MainFrame)
        MainFrame.Show()
    End Sub

    '*===============================================================================================
    '* TAB CONTROL EVENTS
    '*===============================================================================================
    Private Sub TabControl_Platforms_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl_Platforms.Selected
        'Show common file
        ShowSfxSamplePoolControl(TabControl_Platforms.SelectedTab.Text)
        ShowSfxSamplePool(TabControl_Platforms.SelectedTab.Text)
        'Disable remove specific format if the current tab is "Common"
        If StrComp(e.TabPage.Text, "Common") = 0 Then
            Button_RemoveSpecificVersion.Enabled = False
        Else
            Button_RemoveSpecificVersion.Enabled = True
        End If
    End Sub

    '*===============================================================================================
    '* SAMPLE POOL CONTROL EVENTS
    '*===============================================================================================
    Private Sub SfxParamsAndSamplePool_MaxDelayChanged(sender As Object, e As EventArgs) Handles SfxParamsAndSamplePool.SfxControl_MaxDelayChanged
        Dim selectedFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        selectedFileData.SamplePool.MaxDelay = SfxParamsAndSamplePool.Numeric_MaxDelay.Value
    End Sub

    Private Sub SfxParamsAndSamplePool_MinDelayChanged(sender As Object, e As EventArgs) Handles SfxParamsAndSamplePool.SfxControl_MinDelayChanged
        Dim selectedFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        selectedFileData.SamplePool.MinDelay = SfxParamsAndSamplePool.Numeric_MinDelay.Value
    End Sub

    Private Sub SfxParamsAndSamplePool_LoopChecked(sender As Object, e As EventArgs) Handles SfxParamsAndSamplePool.SfxControl_LoopChecked
        Dim selectedFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        selectedFileData.SamplePool.isLooped = SfxParamsAndSamplePool.CheckBox_SamplePoolLoop.Checked
    End Sub

    Private Sub SfxParamsAndSamplePool_SingleChecked(sender As Object, e As EventArgs) Handles SfxParamsAndSamplePool.SfxControl_SingleChecked
        Dim selectedFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        If SfxParamsAndSamplePool.RadioButton_Single.Checked Then
            selectedFileData.SamplePool.Action1 = 0
        End If
    End Sub

    Private Sub SfxParamsAndSamplePool_MultiSampleChecked(sender As Object, e As EventArgs) Handles SfxParamsAndSamplePool.SfxControl_MultiSampleChecked
        Dim selectedFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        If SfxParamsAndSamplePool.RadioButton_MultiSample.Checked Then
            selectedFileData.SamplePool.Action1 = 1
        End If
    End Sub

    Private Sub SfxParamsAndSamplePool_ShuffledChecked(sender As Object, e As EventArgs) Handles SfxParamsAndSamplePool.SfxControl_ShuffledChecked
        Dim selectedFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        selectedFileData.SamplePool.Shuffled = SfxParamsAndSamplePool.CheckBox_Shuffled.Checked
    End Sub

    Private Sub SfxParamsAndSamplePool_PolyphonicChecked(sender As Object, e As EventArgs) Handles SfxParamsAndSamplePool.SfxControl_PolyphonicChecked
        Dim selectedFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        selectedFileData.SamplePool.Polyphonic = SfxParamsAndSamplePool.CheckBox_Polyphonic.Checked
    End Sub

    '*===============================================================================================
    '* SAMPLE POOL EVENTS
    '*===============================================================================================
    Private Sub CheckBox_EnableSubSFX_Click(sender As Object, e As EventArgs) Handles CheckBox_EnableSubSFX.Click
        'Ensure that the sample pool list is empty
        If ListBox_SamplePool.Items.Count > 0 Then
            'Cancel check
            Dim r As CheckBox = sender
            r.Checked = Not r.Checked
            'Inform user
            MsgBox("Sample Pool File List Must be empty!", vbOKOnly + vbCritical, "Error")
        Else
            If CheckBox_EnableSubSFX.Checked Then
                EnableSubSfxSection()
            Else
                DisableSubSfxSection()
            End If
            Dim sfxData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
            sfxData.SamplePool.EnableSubSFX = CheckBox_EnableSubSFX.Checked
        End If
    End Sub

    Private Sub ListBox_SamplePool_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox_SamplePool.SelectedIndexChanged
        If CheckBox_EnableSubSFX.Checked = False Then
            Dim lbls As List(Of Label) = GroupBox_SampleProps.Controls.OfType(Of Label)().ToList()
            'Show Samples info
            If ListBox_SamplePool.SelectedItems.Count > 1 Then
                Label_SampleInfo_FreqValue.Text = ".."
                Label_SampleInfo_SizeValue.Text = ".."
                Label_SampleInfo_LengthValue.Text = ".."
                Label_SampleInfo_LoopValue.Text = ".."
                Label_SampleInfo_StreamedValue.Text = ".."
                ShowSampleData(ListBox_SamplePool.SelectedItem)
                'Change label colors
                For Each lbl In lbls
                    lbl.BackColor = Color.Black
                    lbl.ForeColor = Color.White
                Next
            Else
                ShowSampleInfo(ListBox_SamplePool.SelectedItem)
                ShowSampleData(ListBox_SamplePool.SelectedItem)
                'Change label colors
                For Each lbl In lbls
                    lbl.BackColor = SystemColors.Control
                    lbl.ForeColor = Color.Black
                Next
            End If
        End If
    End Sub

    '*===============================================================================================
    '* SAMPLE POOL LISTBOX EVENTS
    '*===============================================================================================
    Private Sub ListBox_SamplePool_DragOver(sender As Object, e As DragEventArgs) Handles ListBox_SamplePool.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub ListBox_SamplePool_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox_SamplePool.DragDrop
        'Ensure that the drag drop effect is correct
        If e.Effect = DragDropEffects.Copy Then
            'Ensure that the data type is correct
            If e.Data.GetDataPresent(GetType(ListBox.SelectedObjectCollection)) AndAlso CheckBox_EnableSubSFX.Checked = True Then
                'Add items
                Dim sfxFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
                Dim itemsData As ListBox.SelectedObjectCollection = e.Data.GetData(GetType(ListBox.SelectedObjectCollection))
                For Each data As String In itemsData
                    'Create a new object with default settings
                    Dim sampleObj As New Sample With {
                        .FilePath = data,
                        .RandomPitchOffset = 0,
                        .BaseVolume = 0
                    }
                    'Add object to list (binding list automatically updates the listbox control)
                    sfxFileData.Samples.Add(sampleObj)
                Next
            Else
                My.Computer.Audio.PlaySystemSound(SystemSounds.Beep)
            End If
        End If
    End Sub

    Private Sub Button_MoveUp_Click(sender As Object, e As EventArgs) Handles Button_MoveUp.Click
        ListBox_SamplePool.BeginUpdate()
        Dim indexes As Integer() = ListBox_SamplePool.SelectedIndices.Cast(Of Integer)().ToArray()
        If indexes.Length > 0 AndAlso indexes(0) > 0 Then
            Dim itemsToSelect As New Collection
            'Move items
            Dim sfxFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
            For i As Integer = 0 To sfxFileData.Samples.Count - 1
                If indexes.Contains(i) Then
                    Dim moveItem As Sample = sfxFileData.Samples(i)
                    sfxFileData.Samples.Remove(moveItem)
                    sfxFileData.Samples.Insert(i - 1, moveItem)
                    itemsToSelect.Add(i - 1)
                End If
            Next
            'Update selection
            ListBox_SamplePool.SelectedIndices.Clear()
            For index As Integer = 1 To itemsToSelect.Count
                ListBox_SamplePool.SelectedIndex = itemsToSelect(index)
            Next
        End If
        ListBox_SamplePool.EndUpdate()
    End Sub

    Private Sub Button_MoveDown_Click(sender As Object, e As EventArgs) Handles Button_MoveDown.Click
        ListBox_SamplePool.BeginUpdate()
        Dim indexes As Integer() = ListBox_SamplePool.SelectedIndices.Cast(Of Integer)().ToArray()
        Dim sfxFileData As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        If indexes.Length > 0 AndAlso indexes(indexes.Length - 1) < sfxFileData.Samples.Count - 1 Then
            Dim itemsToSelect As New Collection
            'Move items
            For i As Integer = sfxFileData.Samples.Count - 1 To 0 Step -1
                If indexes.Contains(i) Then
                    Dim moveItem As Sample = sfxFileData.Samples(i)
                    sfxFileData.Samples.Remove(moveItem)
                    sfxFileData.Samples.Insert(i + 1, moveItem)
                    itemsToSelect.Add(i + 1)
                End If
            Next
            'Update selection
            ListBox_SamplePool.SelectedIndices.Clear()
            For index As Integer = 1 To itemsToSelect.Count
                ListBox_SamplePool.SelectedIndex = itemsToSelect(index)
            Next
        End If
        ListBox_SamplePool.EndUpdate()
    End Sub

    Private Sub Button_AddSample_Click(sender As Object, e As EventArgs) Handles Button_AddSample.Click
        AddNewSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub MenuItem_SamplePool_Add_Click(sender As Object, e As EventArgs) Handles MenuItem_SamplePool_Add.Click
        AddNewSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub Button_RemoveSample_Click(sender As Object, e As EventArgs) Handles Button_RemoveSample.Click
        RemoveSelectedSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub MenuItem_SamplePool_Remove_Click(sender As Object, e As EventArgs) Handles MenuItem_SamplePool_Remove.Click
        RemoveSelectedSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub Button_CopySample_Click(sender As Object, e As EventArgs) Handles Button_CopySample.Click
        CopySelectedSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub MenuItem_SamplePool_Copy_Click(sender As Object, e As EventArgs) Handles MenuItem_SamplePool_Copy.Click
        CopySelectedSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub Button_OpenSampleFolder_Click(sender As Object, e As EventArgs) Handles Button_OpenSampleFolder.Click
        OpenSelectedSampleFolder(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub MenuItem_SamplePool_Open_Click(sender As Object, e As EventArgs) Handles MenuItem_SamplePool_Open.Click
        OpenSelectedSampleFolder(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub Button_EditSample_Click(sender As Object, e As EventArgs) Handles Button_EditSample.Click
        EditSelectedSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub MenuItem_SamplePool_Edit_Click(sender As Object, e As EventArgs) Handles MenuItem_SamplePool_Edit.Click
        EditSelectedSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub Button_PlaySample_Click(sender As Object, e As EventArgs) Handles Button_PlaySample.Click
        PlaySelectedSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub MenuItem_SamplePool_Play_Click(sender As Object, e As EventArgs) Handles MenuItem_SamplePool_Play.Click
        PlaySelectedSample(TabControl_Platforms.SelectedTab.Text)
    End Sub

    Private Sub Button_StopSample_Click(sender As Object, e As EventArgs) Handles Button_StopSample.Click
        'Stop audio player
        My.Computer.Audio.Stop()
    End Sub

    Private Sub MenuItem_SamplePool_Stop_Click(sender As Object, e As EventArgs) Handles MenuItem_SamplePool_Stop.Click
        'Stop audio player
        My.Computer.Audio.Stop()
    End Sub

    '*===============================================================================================
    '* SAMPLE PROPERTIES EVENTS
    '*===============================================================================================
    Private Sub Numeric_PitchOffset_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_PitchOffset.ValueChanged
        Dim sfxElement As SfxFile = sfxFilesData(TabControl_Platforms.SelectedTab.Text)
        'Ensure that we have selected an item
        If ListBox_SamplePool.SelectedItems.Count > 0 Then
            'Update property
            For Each sampleObject As Sample In ListBox_SamplePool.SelectedItems
                sampleObject.PitchOffset = Numeric_PitchOffset.Value
            Next
        End If
    End Sub

    Private Sub Numeric_RandomPitch_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_RandomPitch.ValueChanged
        'Ensure that we have selected an item
        If ListBox_SamplePool.SelectedItems.Count > 0 Then
            'Update property
            For Each sampleObject As Sample In ListBox_SamplePool.SelectedItems
                sampleObject.RandomPitchOffset = Numeric_RandomPitch.Value
            Next
        End If
    End Sub

    Private Sub Numeric_BaseVolume_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_BaseVolume.ValueChanged
        'Ensure that we have selected an item
        If ListBox_SamplePool.SelectedItems.Count > 0 Then
            'Update property
            For Each sampleObject As Sample In ListBox_SamplePool.SelectedItems
                sampleObject.BaseVolume = Numeric_BaseVolume.Value
            Next
        End If
    End Sub

    Private Sub Numeric_RandomVolume_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_RandomVolume.ValueChanged
        'Ensure that we have selected an item
        If ListBox_SamplePool.SelectedItems.Count > 0 Then
            'Update property
            For Each sampleObject As Sample In ListBox_SamplePool.SelectedItems
                sampleObject.RandomVolumeOffset = Numeric_RandomVolume.Value
            Next
        End If
    End Sub

    Private Sub Numeric_Pan_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_Pan.ValueChanged
        'Ensure that we have selected an item
        If ListBox_SamplePool.SelectedItems.Count > 0 Then
            'Update property
            For Each sampleObject As Sample In ListBox_SamplePool.SelectedItems
                sampleObject.Pan = Numeric_Pan.Value
            Next
        End If
    End Sub

    Private Sub Numeric_RandomPan_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_RandomPan.ValueChanged
        'Ensure that we have selected an item
        If ListBox_SamplePool.SelectedItems.Count > 0 Then
            'Update property
            For Each sampleObject As Sample In ListBox_SamplePool.SelectedItems
                sampleObject.RandomPan = Numeric_RandomPan.Value
            Next
        End If
    End Sub

    '*===============================================================================================
    '* BUTTON EVENTS
    '*===============================================================================================
    Private Sub Button_SpecVersion_PlayStation2_Click(sender As Object, e As EventArgs) Handles Button_SpecVersion_PlayStation2.Click
        'Create format
        CreateSpecificFormat("PlayStation2")
        'Disable Button
        Button_SpecVersion_PlayStation2.Enabled = False
    End Sub

    Private Sub Button_SpecVersion_GameCube_Click(sender As Object, e As EventArgs) Handles Button_SpecVersion_GameCube.Click
        'Create format
        CreateSpecificFormat("GameCube")
        'Disable Button
        Button_SpecVersion_GameCube.Enabled = False
    End Sub

    Private Sub Button_SpecVersion_Xbox_Click(sender As Object, e As EventArgs) Handles Button_SpecVersion_Xbox.Click
        'Create format
        CreateSpecificFormat("X Box")
        'Disable Button
        Button_SpecVersion_Xbox.Enabled = False
    End Sub

    Private Sub Button_SpecVersion_PC_Click(sender As Object, e As EventArgs) Handles Button_SpecVersion_PC.Click
        'Create format
        CreateSpecificFormat("PC")
        'Disable Button
        Button_SpecVersion_PC.Enabled = False
    End Sub

    Private Sub Button_Clipboard_Copy_Click(sender As Object, e As EventArgs) Handles Button_Clipboard_Copy.Click
        If sfxFilesData.ContainsKey(TabControl_Platforms.SelectedTab.Text) Then
            writers.WriteSfxFile(sfxFilesData(TabControl_Platforms.SelectedTab.Text), fso.BuildPath(WorkingDirectory, "SFXs\Misc\ClipBoard.txt"))
            Button_ClipboardPaste.Enabled = True
        End If
    End Sub

    Private Sub Button_ClipboardPaste_Click(sender As Object, e As EventArgs) Handles Button_ClipboardPaste.Click
        Dim clipboardFilePath As String = fso.BuildPath(WorkingDirectory, "SFXs\Misc\ClipBoard.txt")
        If fso.FileExists(clipboardFilePath) Then
            Dim sfxFile As SfxFile = reader.ReadSFXFile(clipboardFilePath)
            sfxFilesData(TabControl_Platforms.SelectedTab.Text) = sfxFile
            'Inherited from common file and applied to all other formats
            ShowSfxParameters(TabControl_Platforms.SelectedTab.Text)
            'Show common file
            ShowSfxSamplePoolControl(TabControl_Platforms.SelectedTab.Text)
            ShowSfxSamplePool(TabControl_Platforms.SelectedTab.Text)
        End If
    End Sub

    Private Sub Button_RemoveSpecificVersion_Click(sender As Object, e As EventArgs) Handles Button_RemoveSpecificVersion.Click
        'Enable Button
        Select Case TabControl_Platforms.SelectedTab.Text
            Case "PC"
                Button_SpecVersion_PC.Enabled = True
            Case "X Box"
                Button_SpecVersion_Xbox.Enabled = True
            Case "GameCube"
                Button_SpecVersion_GameCube.Enabled = True
            Case "PlayStation2"
                Button_SpecVersion_PlayStation2.Enabled = True
        End Select
        'Delete tab
        sfxFilesData.Remove(TabControl_Platforms.SelectedTab.Text)
        TabControl_Platforms.TabPages.Remove(TabControl_Platforms.SelectedTab)
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        'Close form
        Close()
    End Sub

    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        'Disable save file message
        promptSave = False
        'Iterate over all available tabs
        For Each sfxFile As KeyValuePair(Of String, SfxFile) In sfxFilesData
            'Update properties
            UpdateFilesParameters()
            'Write file
            Dim tempFilePath As String = fso.BuildPath(WorkingDirectory, "SFXs\Misc\" & sfxFile.Key & ".txt")
            writers.WriteSfxFile(sfxFile.Value, tempFilePath)
            'Move file to the final folder
            If StrComp(sfxFile.Key, "Common") = 0 Then
                fso.CopyFile(tempFilePath, fso.BuildPath(WorkingDirectory, "SFXs\" & sfxFileName & ".txt"))
            Else
                fso.CopyFile(tempFilePath, fso.BuildPath(WorkingDirectory, "SFXs\" & sfxFile.Key & "\" & sfxFileName & ".txt"))
            End If
        Next
        'Close form
        Close()
    End Sub
End Class
Imports System.IO
Imports System.Media
Imports NAudio.Wave
Imports sb_editor.ParsersObjects

Partial Public Class Frm_SfxEditor
    '*===============================================================================================
    '* FORM GENERIC FUNCTIONS
    '*===============================================================================================
    Private Function CreateTab(platformName As String) As TabPage
        'Create tab page
        Dim platformTab As New TabPage With {
            .Text = platformName,
            .Name = platformName
        }
        TabControl_Platforms.TabPages.Add(platformTab)
        Return platformTab
    End Function

    '*===============================================================================================
    '* DATA DISPLAY FUNCTIONS
    '*===============================================================================================
    Private Sub ShowSfxParameters(platformName As String)
        Dim sfxData As SfxFile = sfxFilesData(platformName)
        If sfxData IsNot Nothing Then
            SfxParamsAndSamplePool.TrackBar_Reverb.Value = sfxData.Parameters.ReverbSend
            SfxParamsAndSamplePool.Numeric_MasterVolume.Value = sfxData.Parameters.MasterVolume
            Select Case sfxData.Parameters.TrackingType
                Case 0
                    SfxParamsAndSamplePool.RadioButton_Tracking_2D.Checked = True
                Case 1
                    SfxParamsAndSamplePool.RadioButton_TrackingType_Amb.Checked = True
                Case 2
                    SfxParamsAndSamplePool.RadioButton_TrackingType_3D.Checked = True
                Case 3
                    SfxParamsAndSamplePool.RadioButton_TrackingType_3DRandom.Checked = True
                Case 4
                    SfxParamsAndSamplePool.RadioButton_Tracking_2DPL2.Checked = True
            End Select
            SfxParamsAndSamplePool.TrackBar_InnerRadius.Value = sfxData.Parameters.InnerRadius
            SfxParamsAndSamplePool.TrackBar_OuterRadius.Value = sfxData.Parameters.OuterRadius
            SfxParamsAndSamplePool.Numeric_MaxVoices.Value = sfxData.Parameters.MaxVoices
            If sfxData.Parameters.Action1 = 0 Then
                SfxParamsAndSamplePool.RadioButton_ActionSteal.Checked = True
            Else
                SfxParamsAndSamplePool.RadioButton_ActionReject.Checked = True
            End If
            SfxParamsAndSamplePool.Numeric_Priority.Value = sfxData.Parameters.Priority
            SfxParamsAndSamplePool.Numeric_Alertness.Value = sfxData.Parameters.Alertness
            SfxParamsAndSamplePool.CheckBox_StealOnLouder.Checked = sfxData.Parameters.StealOnAge
            SfxParamsAndSamplePool.Numeric_Ducker.Value = sfxData.Parameters.Ducker
            SfxParamsAndSamplePool.Numeric_DuckerLength.Value = sfxData.Parameters.DuckerLength
            SfxParamsAndSamplePool.CheckBox_UnderWater.Checked = sfxData.Parameters.Outdoors
            SfxParamsAndSamplePool.Checkbox_PauseInNis.Checked = sfxData.Parameters.PauseInNis
            SfxParamsAndSamplePool.CheckBox_IgnoreAge.Checked = sfxData.Parameters.IgnoreAge
            SfxParamsAndSamplePool.Checkbox_MusicType.Checked = sfxData.Parameters.MusicType
            SfxParamsAndSamplePool.Checkbox_Doppler.Checked = sfxData.Parameters.Doppler
            Textbox_HashCodeNumber.Text = sfxData.HashCode
        End If
    End Sub

    Private Sub UpdateFilesParameters()
        For Each sfxFile As KeyValuePair(Of String, SfxFile) In sfxFilesData
            sfxFile.Value.Parameters.ReverbSend = SfxParamsAndSamplePool.TrackBar_Reverb.Value
            sfxFile.Value.Parameters.MasterVolume = SfxParamsAndSamplePool.Numeric_MasterVolume.Value
            Dim trackingType As Byte = 0
            If SfxParamsAndSamplePool.RadioButton_TrackingType_Amb.Checked Then
                trackingType = 1
            End If
            If SfxParamsAndSamplePool.RadioButton_TrackingType_3D.Checked Then
                trackingType = 2
            End If
            If SfxParamsAndSamplePool.RadioButton_TrackingType_3DRandom.Checked Then
                trackingType = 3
            End If
            If SfxParamsAndSamplePool.RadioButton_Tracking_2DPL2.Checked Then
                trackingType = 4
            End If
            sfxFile.Value.Parameters.TrackingType = trackingType
            sfxFile.Value.Parameters.InnerRadius = SfxParamsAndSamplePool.TrackBar_InnerRadius.Value
            sfxFile.Value.Parameters.OuterRadius = SfxParamsAndSamplePool.TrackBar_OuterRadius.Value
            sfxFile.Value.Parameters.MaxVoices = SfxParamsAndSamplePool.Numeric_MaxVoices.Value
            Dim actionType As Byte = 1
            If SfxParamsAndSamplePool.RadioButton_ActionSteal.Checked Then
                actionType = 0
            End If
            sfxFile.Value.Parameters.Action1 = actionType
            sfxFile.Value.Parameters.Priority = SfxParamsAndSamplePool.Numeric_Priority.Value
            sfxFile.Value.Parameters.Alertness = SfxParamsAndSamplePool.Numeric_Alertness.Value
            sfxFile.Value.Parameters.StealOnAge = SfxParamsAndSamplePool.CheckBox_StealOnLouder.Checked
            sfxFile.Value.Parameters.Ducker = SfxParamsAndSamplePool.Numeric_Ducker.Value
            sfxFile.Value.Parameters.DuckerLength = SfxParamsAndSamplePool.Numeric_DuckerLength.Value
            sfxFile.Value.Parameters.Outdoors = SfxParamsAndSamplePool.CheckBox_UnderWater.Checked
            sfxFile.Value.Parameters.PauseInNis = SfxParamsAndSamplePool.Checkbox_PauseInNis.Checked
            sfxFile.Value.Parameters.IgnoreAge = SfxParamsAndSamplePool.CheckBox_IgnoreAge.Checked
            sfxFile.Value.Parameters.MusicType = SfxParamsAndSamplePool.Checkbox_MusicType.Checked
            sfxFile.Value.Parameters.Doppler = SfxParamsAndSamplePool.Checkbox_Doppler.Checked
        Next
    End Sub

    Private Sub ShowSfxSamplePoolControl(platformName As String)
        Dim sfxData As SfxFile = sfxFilesData(platformName)
        If sfxData IsNot Nothing Then
            If sfxData.SamplePool.Action1 = 0 Then
                SfxParamsAndSamplePool.RadioButton_Single.Checked = True
            Else
                SfxParamsAndSamplePool.RadioButton_MultiSample.Checked = True
            End If
            SfxParamsAndSamplePool.CheckBox_SamplePoolLoop.Checked = sfxData.SamplePool.isLooped
            SfxParamsAndSamplePool.Numeric_MinDelay.Value = sfxData.SamplePool.MinDelay
            SfxParamsAndSamplePool.Numeric_MaxDelay.Value = sfxData.SamplePool.MaxDelay
            SfxParamsAndSamplePool.CheckBox_RandomPick.Checked = sfxData.SamplePool.RandomPick
            SfxParamsAndSamplePool.CheckBox_Shuffled.Checked = sfxData.SamplePool.Shuffled
            SfxParamsAndSamplePool.CheckBox_Polyphonic.Checked = sfxData.SamplePool.Polyphonic
            'Check Sub SFX Stuff
            If sfxData.SamplePool.EnableSubSFX Then
                EnableSubSfxSection()
            Else
                DisableSubSfxSection()
            End If
        End If
    End Sub

    '*===============================================================================================
    '* SAMPLE POOL SECTION
    '*===============================================================================================
    Private Sub ShowSfxSamplePool(platformName As String)
        Dim sfxData As SfxFile = sfxFilesData(platformName)
        If sfxData IsNot Nothing Then
            CheckBox_EnableSubSFX.Checked = sfxData.SamplePool.EnableSubSFX
            'Update listbox
            ListBox_SamplePool.DataSource = sfxData.Samples
            ListBox_SamplePool.DisplayMember = "FilePath"
        End If
    End Sub

    Private Sub ShowSampleData(selectedSample As Sample)
        If selectedSample IsNot Nothing Then
            Numeric_PitchOffset.Value = CDec(selectedSample.PitchOffset)
            Numeric_RandomPitch.Value = CDec(selectedSample.RandomPitchOffset)
            Numeric_BaseVolume.Value = selectedSample.BaseVolume
            Numeric_RandomVolume.Value = selectedSample.RandomVolumeOffset
            Numeric_Pan.Value = selectedSample.Pan
            Numeric_RandomPan.Value = selectedSample.RandomPan
        End If
    End Sub

    Private Sub ShowSampleInfo(selectedSample As Sample)
        If selectedSample IsNot Nothing Then
            Dim waveFullPath As String = Path.Combine(WorkingDirectory, "Master", selectedSample.FilePath)
            If File.Exists(waveFullPath) Then
                Using reader As New WaveFileReader(waveFullPath)
                    Label_SampleInfo_FreqValue.Text = reader.WaveFormat.SampleRate
                    Label_SampleInfo_SizeValue.Text = BytesStringFormat(reader.Length)
                    Label_SampleInfo_LengthValue.Text = Math.Round(reader.TotalTime.TotalSeconds, 1)
                    'Check if is looped
                    Label_SampleInfo_LoopValue.Text = "False"
                    If ReadWaveSampleChunk(reader)(0) = 1 Then
                        Label_SampleInfo_LoopValue.Text = "True"
                    End If
                    'Check if is streamed
                    Label_SampleInfo_StreamedValue.Text = "??"
                    If StreamSamplesList IsNot Nothing Then
                        Label_SampleInfo_StreamedValue.Text = Array.IndexOf(StreamSamplesList, UCase(selectedSample.FilePath.TrimStart("\"))) <> -1
                    End If
                End Using
            End If
        End If
    End Sub

    '*===============================================================================================
    '* SUB SFX SECTION
    '*===============================================================================================
    Private Sub EnableSubSfxSection()
        'Enabling Sub SFX - Disable panels
        GroupBox_SampleProps.Enabled = False
        SfxParamsAndSamplePool.GroupBox_GlobalSettings.Enabled = False
        GroupBox_SampleProps.Visible = False
        SfxParamsAndSamplePool.GroupBox_GlobalSettings.Visible = False
        'Hide labels
        Label_SampleInfo_FreqValue.Visible = False
        Label_SampleInfo_SizeValue.Visible = False
        Label_SampleInfo_LengthValue.Visible = False
        Label_SampleInfo_LoopValue.Visible = False
        Label_SampleInfo_StreamedValue.Visible = False
        'Show HashCodes List for Drag & Drop
        Dim hashcodesList As New HashCodesList With {
            .StartPosition = FormStartPosition.Manual
        }
        hashcodesList.Left = Left - hashcodesList.Width
        hashcodesList.Show()
    End Sub

    Private Sub DisableSubSfxSection()
        'Disabling Sub SFX - Enable panels
        GroupBox_SampleProps.Enabled = True
        SfxParamsAndSamplePool.GroupBox_GlobalSettings.Enabled = True
        GroupBox_SampleProps.Visible = True
        SfxParamsAndSamplePool.GroupBox_GlobalSettings.Visible = True
        'Show labels
        Label_SampleInfo_FreqValue.Visible = True
        Label_SampleInfo_SizeValue.Visible = True
        Label_SampleInfo_LengthValue.Visible = True
        Label_SampleInfo_LoopValue.Visible = True
        Label_SampleInfo_StreamedValue.Visible = True
        'Close form
        Dim subSfxListForm = CType(Application.OpenForms("HashCodesList"), HashCodesList)
        If subSfxListForm IsNot Nothing Then
            subSfxListForm.Close()
        End If
    End Sub

    '*===============================================================================================
    '* SAMPLE POOL METHODS
    '*===============================================================================================
    Private Sub AddNewSample(currentPlatform As String)
        'Check if we are working with Sub SFX or not
        If CheckBox_EnableSubSFX.Checked Then
            'Check if form is opened
            Dim subSfxListForm = CType(Application.OpenForms("HashCodesList"), HashCodesList)
            If subSfxListForm Is Nothing Then
                'Show HashCodes List for Drag & Drop
                Dim hashcodesList As New HashCodesList With {
                    .StartPosition = FormStartPosition.Manual
                }
                hashcodesList.Left = Left - hashcodesList.Width
                hashcodesList.Show()
            End If
        Else
            'Open file dialog explorer
            If openFileDiag.ShowDialog = DialogResult.OK Then
                Dim sfxFileData As SfxFile = sfxFilesData(currentPlatform)
                Dim sampleDefaultValues As Double() = GetDefaultSampleValues()
                'Get file selected path and read
                For Each waveFullPath As String In openFileDiag.FileNames
                    Using reader As New WaveFileReader(waveFullPath)
                        'Create a new object with default settings
                        Dim sampleObj As New Sample With {
                            .PitchOffset = sampleDefaultValues(0),
                            .RandomPitchOffset = sampleDefaultValues(1),
                            .BaseVolume = sampleDefaultValues(2),
                            .RandomVolumeOffset = sampleDefaultValues(3),
                            .Pan = sampleDefaultValues(4),
                            .RandomPan = sampleDefaultValues(5)
                        }
                        'Calculate Relative Path
                        Dim MasterFolderPath As String = ProjectSettingsFile.MiscProps.SampleFileFolder & "\Master\"
                        If InStr(waveFullPath, MasterFolderPath) = 1 Then
                            Dim relativePath As String = waveFullPath.Substring(MasterFolderPath.Length)
                            sampleObj.FilePath = relativePath
                            'Add object to list
                            sfxFileData.Samples.Add(sampleObj)
                        Else
                            MsgBox("The selected item path is not correct." & vbNewLine & "All samples mult be located in the master folder: " & MasterFolderPath, vbOKOnly + vbExclamation, "EuroSound")
                        End If
                    End Using
                Next
            End If
        End If
    End Sub

    Private Sub CopySelectedSample(currentPlatform As String)
        'Ensure that we have selected a valid item
        If ListBox_SamplePool.SelectedItems.Count > 0 Then
            If ListBox_SamplePool.Items.Count < 100 Then
                Dim sfxFileData As SfxFile = sfxFilesData(currentPlatform)
                'Get items to clone
                Dim objectsToClone As New Collection
                For Each sampleToClone As Sample In ListBox_SamplePool.SelectedItems
                    objectsToClone.Add(sampleToClone)
                Next
                'Add items
                For index As Integer = 1 To objectsToClone.Count
                    'Clone item
                    Dim newSample As New Sample With {
                        .FilePath = objectsToClone(index).FilePath,
                        .PitchOffset = objectsToClone(index).PitchOffset,
                        .RandomPitchOffset = objectsToClone(index).RandomPitchOffset,
                        .BaseVolume = objectsToClone(index).BaseVolume,
                        .RandomVolumeOffset = objectsToClone(index).RandomVolumeOffset,
                        .Pan = objectsToClone(index).Pan,
                        .RandomPan = objectsToClone(index).RandomPan
                    }
                    'Add item to list
                    sfxFileData.Samples.Add(newSample)
                Next
                'Clear selection
                ListBox_SamplePool.SelectedItems.Clear()
                For index As Integer = 1 To objectsToClone.Count
                    ListBox_SamplePool.SelectedItem = objectsToClone(index)
                Next
            Else
                MsgBox("Reached the limit of 100 samples per sound.", vbOKOnly + vbCritical, "EuroSound")
            End If
        Else
            SystemSounds.Exclamation.Play()
        End If
    End Sub

    Private Sub RemoveSelectedSample(currentPlatform As String)
        If ListBox_SamplePool.SelectedItems.Count > 0 Then
            Dim sfxFileData As SfxFile = sfxFilesData(currentPlatform)
            'Get items to remove
            Dim objectsToRemove As New Collection
            For Each sampleToRemove As Sample In ListBox_SamplePool.SelectedItems
                objectsToRemove.Add(sampleToRemove)
            Next
            'Remove items
            For index As Integer = 1 To objectsToRemove.Count
                sfxFileData.Samples.Remove(objectsToRemove(index))
            Next
            'Clear selection
            ListBox_SamplePool.SelectedItems.Clear()
        Else
            SystemSounds.Exclamation.Play()
        End If
    End Sub

    Private Sub PlaySelectedSample(currentPlatform As String)
        'Ensure that is not a subsfx
        If CheckBox_EnableSubSFX.Checked = False Then
            'Ensure that we have an output device
            If WaveOut.DeviceCount > 0 Then
                'Get index from the listbox object and ensure that the index is not out of range
                Dim selectedItemName As Integer = ListBox_SamplePool.SelectedIndex
                If selectedItemName = -1 Then
                    SystemSounds.Exclamation.Play()
                Else
                    Dim sfxFileData As SfxFile = sfxFilesData(currentPlatform)
                    If selectedItemName < sfxFileData.Samples.Count Then
                        'Get relative path
                        Dim waveRelativePath = sfxFileData.Samples(selectedItemName).FilePath
                        Dim waveFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master", waveRelativePath)
                        'Play audio
                        If File.Exists(waveFilePath) Then
                            My.Computer.Audio.Play(waveFilePath)
                        End If
                    End If
                End If
            Else
                'Inform user about this error
                MsgBox("No audio devices has been found", vbOKOnly + vbExclamation, "EuroSound")
            End If
        Else
            'Play warning sound
            SystemSounds.Exclamation.Play()
        End If
    End Sub

    Private Sub EditSelectedSample(currentPlatform As String)
        'Ensure that is not a subsfx
        If CheckBox_EnableSubSFX.Checked = False Then
            If ListBox_SamplePool.SelectedIndex = -1 Then
                SystemSounds.Exclamation.Play()
            Else
                If StrComp(ProjAudioEditor, "") = 0 Then
                    MsgBox("No editor setup." & vbNewLine & "Use Properties form to setup.", vbOKOnly + vbExclamation, "EuroSound")
                Else
                    'Get index from the listbox object and Ensure that the index is not out of range
                    Dim selectedItemName As Integer = ListBox_SamplePool.SelectedIndex
                    Dim sfxFileData As SfxFile = sfxFilesData(currentPlatform)
                    If selectedItemName < sfxFileData.Samples.Count Then
                        'Get absolute path
                        Dim waveRelativePath As String = sfxFileData.Samples(selectedItemName).FilePath
                        Dim waveFullPath As String = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master", waveRelativePath)
                        'Open Audio Editor tool
                        EditWaveFile(waveFullPath)
                    End If
                End If
            End If
        Else
            'Play warning sound
            SystemSounds.Exclamation.Play()
        End If
    End Sub

    Private Sub OpenSelectedSampleFolder(currentPlatform As String)
        'Ensure that is not a SubSFX
        If CheckBox_EnableSubSFX.Checked Then
            SystemSounds.Exclamation.Play()
        Else
            'Get index from the listbox object
            Dim selectedItemName As Integer = ListBox_SamplePool.SelectedIndex
            If selectedItemName = -1 Then
                SystemSounds.Exclamation.Play()
            Else
                Dim sfxFileData As SfxFile = sfxFilesData(currentPlatform)
                If selectedItemName < sfxFileData.Samples.Count Then
                    'Get wave folder path
                    Dim waveRelativePath = sfxFileData.Samples(selectedItemName).FilePath
                    Dim folderPath = Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master", Path.GetDirectoryName(waveRelativePath))
                    If Directory.Exists(folderPath) Then
                        'Open folder
                        Process.Start("Explorer.exe", folderPath)
                    End If
                End If
            End If
        End If
    End Sub

    '*===============================================================================================
    '* FORMATS FUNCTIONS
    '*===============================================================================================
    Private Sub CreateSpecificFormat(formatName As String)
        'Create format
        Dim formatTextFile As String = Path.Combine(WorkingDirectory, "SFXs", "Misc", formatName & ".txt")
        File.Copy(Path.Combine(WorkingDirectory, "SFXs", "Misc", "Common.txt"), formatTextFile, True)
        sfxFilesData.Add(CreateTab(formatName).Text, reader.ReadSFXFile(formatTextFile))
    End Sub
End Class

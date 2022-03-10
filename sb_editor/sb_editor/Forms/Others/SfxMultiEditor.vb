Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Public Class SfxMultiEditor
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly sfxFiles As String()
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly textFileWritters As New FileWriters

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(filesToModify As String())
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        sfxFiles = filesToModify
    End Sub

    Private Sub SfxMultiEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For fileIndex As Integer = 0 To sfxFiles.Length - 1
            Dim currentSfx As String = sfxFiles(fileIndex)
            Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(currentSfx)
            Dim propertiesToShow As String() = New String(14) {}
            propertiesToShow(0) = GetOnlyFileName(currentSfx)
            propertiesToShow(1) = sfxFileData.Parameters.ReverbSend
            propertiesToShow(2) = ComboBox_TrackingType.Items(sfxFileData.Parameters.TrackingType)
            propertiesToShow(3) = sfxFileData.Parameters.InnerRadius
            propertiesToShow(4) = sfxFileData.Parameters.OuterRadius
            propertiesToShow(5) = sfxFileData.Parameters.MaxVoices
            propertiesToShow(6) = (sfxFileData.Parameters.Action1 = 0)
            propertiesToShow(7) = sfxFileData.Parameters.Priority
            propertiesToShow(8) = sfxFileData.Parameters.Alertness
            propertiesToShow(9) = sfxFileData.Parameters.Ducker
            propertiesToShow(10) = sfxFileData.Parameters.DuckerLength
            propertiesToShow(11) = sfxFileData.Parameters.MasterVolume
            propertiesToShow(12) = sfxFileData.Parameters.Outdoors
            propertiesToShow(13) = sfxFileData.Parameters.StealOnAge
            ListView_SfxFiles.Items.Add(New ListViewItem(propertiesToShow))
        Next
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        Close()
    End Sub

    '*===============================================================================================
    '* CONTROLS EVENTS
    '*===============================================================================================
    Private Sub ComboBox_TrackingType_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox_TrackingType.SelectionChangeCommitted
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(2).Text = ComboBox_TrackingType.SelectedItem
            UpdateSFXFile()
        Next
    End Sub

    Private Sub ComboBox_StealLouder_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox_StealLouder.SelectionChangeCommitted
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(13).Text = ComboBox_StealLouder.SelectedItem
            UpdateSFXFile()
        Next
    End Sub

    Private Sub ComboBox_UnderWater_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox_UnderWater.SelectionChangeCommitted
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(12).Text = ComboBox_UnderWater.SelectedItem
            UpdateSFXFile()
        Next
    End Sub

    Private Sub ComboBox_Steal_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox_Steal.SelectionChangeCommitted
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(6).Text = ComboBox_Steal.SelectedItem
            UpdateSFXFile()
        Next
    End Sub

    Private Sub Numeric_Reverb_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_Reverb.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(1).Text = Numeric_Reverb.Value
            UpdateSFXFile()
        Next
    End Sub

    Private Sub Numeric_InnerRadius_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_InnerRadius.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            Dim outerRad As Integer = ListView_SfxFiles.SelectedItems(itemIndex).SubItems(4).Text
            If Numeric_InnerRadius.Value <= outerRad Then
                ListView_SfxFiles.SelectedItems(itemIndex).SubItems(3).Text = Numeric_InnerRadius.Value
                UpdateSFXFile()
            Else
                ListView_SfxFiles.SelectedItems(itemIndex).SubItems(3).Text = outerRad
                Numeric_InnerRadius.Value = outerRad
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
            End If
        Next
    End Sub

    Private Sub Numeric_OuterRadius_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_OuterRadius.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            Dim innerRad As Integer = ListView_SfxFiles.SelectedItems(itemIndex).SubItems(3).Text
            If Numeric_OuterRadius.Value >= innerRad Then
                ListView_SfxFiles.SelectedItems(itemIndex).SubItems(4).Text = Numeric_OuterRadius.Value
                UpdateSFXFile()
            Else
                ListView_SfxFiles.SelectedItems(itemIndex).SubItems(4).Text = innerRad
                Numeric_OuterRadius.Value = innerRad
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
            End If
        Next
    End Sub

    Private Sub Numeric_MaxVoices_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_MaxVoices.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(5).Text = Numeric_MaxVoices.Value
        Next
    End Sub

    Private Sub Numeric_Priority_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_Priority.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(7).Text = Numeric_Priority.Value
            UpdateSFXFile()
        Next
    End Sub

    Private Sub Numeric_Alertness_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_Alertness.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(8).Text = Numeric_Alertness.Value
            UpdateSFXFile()
        Next
    End Sub

    Private Sub Numeric_Ducker_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_Ducker.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(9).Text = Numeric_Ducker.Value
            UpdateSFXFile()
        Next
    End Sub

    Private Sub Numeric_DuckerLength_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_DuckerLength.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(10).Text = Numeric_DuckerLength.Value
            UpdateSFXFile()
        Next
    End Sub

    Private Sub Numeric_MasterVolume_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_MasterVolume.ValueChanged
        For itemIndex As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            ListView_SfxFiles.SelectedItems(itemIndex).SubItems(11).Text = Numeric_MasterVolume.Value
            UpdateSFXFile()
        Next
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Sub UpdateSFXFile()
        For index As Integer = 0 To ListView_SfxFiles.SelectedItems.Count - 1
            Dim currentItem As ListViewItem = ListView_SfxFiles.SelectedItems(index)
            'Get formats
            Dim listOfFormats As New List(Of String)({"Common"})
            listOfFormats.AddRange(ProjectSettingsFile.sampleRateFormats.Keys.ToArray)
            'Update Text Files
            For platformIndex As Integer = 0 To listOfFormats.Count - 1
                'Get SFX Text file path
                Dim currentFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs", currentItem.SubItems(0).Text & ".txt")
                If platformIndex > 0 Then
                    currentFilePath = fso.BuildPath(WorkingDirectory & "\SFXs\" & listOfFormats(platformIndex), currentItem.SubItems(0).Text & ".txt")
                End If
                'Read and update SFX File
                If fso.FileExists(currentFilePath) Then
                    Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(currentFilePath)
                    sfxFileData.Parameters.ReverbSend = currentItem.SubItems(1).Text
                    sfxFileData.Parameters.TrackingType = ComboBox_TrackingType.Items.IndexOf(currentItem.SubItems(2).Text)
                    sfxFileData.Parameters.InnerRadius = currentItem.SubItems(3).Text
                    sfxFileData.Parameters.OuterRadius = currentItem.SubItems(4).Text
                    sfxFileData.Parameters.MaxVoices = currentItem.SubItems(5).Text
                    sfxFileData.Parameters.Action1 = currentItem.SubItems(6).Text.Equals("True")
                    sfxFileData.Parameters.Priority = currentItem.SubItems(7).Text
                    sfxFileData.Parameters.Alertness = currentItem.SubItems(8).Text
                    sfxFileData.Parameters.Ducker = currentItem.SubItems(9).Text
                    sfxFileData.Parameters.DuckerLength = currentItem.SubItems(10).Text
                    sfxFileData.Parameters.MasterVolume = currentItem.SubItems(11).Text
                    sfxFileData.Parameters.Outdoors = currentItem.SubItems(12).Text
                    sfxFileData.Parameters.StealOnAge = currentItem.SubItems(13).Text
                    textFileWritters.WriteSfxFile(sfxFileData, currentFilePath)
                End If
            Next
        Next
    End Sub
End Class
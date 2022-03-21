Imports System.IO
Imports System.Media
Imports sb_editor.ReaderClasses
Imports sb_editor.ReverbObj
Imports sb_editor.WritersClasses

Public Class Frm_ReverbTester
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private ReadOnly writers As New FileWriters
    Private ReadOnly readers As New FileParsers
    Private currentLoadedFile As ReverbFile
    Private fileHasBeenModified As Boolean = False
    Private currentSelectedItem As String

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub Frm_ReverbTester_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Create folder if not exists
        Dim reverbsFolder As String = Path.Combine(WorkingDirectory, "Reverbs")
        Directory.CreateDirectory(reverbsFolder)

        'Get current items
        Dim filesToLoad As String() = Directory.GetFiles(reverbsFolder, "*.txt", SearchOption.TopDirectoryOnly)
        For index As Integer = 0 To filesToLoad.Length - 1
            ListBox_HashCodes.Items.Add(Path.GetFileNameWithoutExtension(filesToLoad(index)))
        Next

        'Select the first item
        If ListBox_HashCodes.Items.Count > 0 Then
            ListBox_HashCodes.SelectedIndex = 0
        End If
    End Sub

    Private Sub Frm_ReverbTester_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Ask for save
        If fileHasBeenModified Then
            My.Computer.Audio.PlaySystemSound(SystemSounds.Exclamation)
            Dim answer As DialogResult = MsgBox("Save Changes to : '" & currentSelectedItem & "'?", vbYesNo + vbQuestion, "Save Reverb?")
            If answer = DialogResult.Yes Then
                Dim fileToSavePath As String = Path.Combine(WorkingDirectory, "Reverbs", currentSelectedItem & ".txt")
                writers.WriteReverbFile(fileToSavePath, currentLoadedFile)
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim updateBoolean As Boolean = fileHasBeenModified
        ShowReverbFile(TabControl1.SelectedTab.Text)

        'Check if we have to update the boolean
        If Not updateBoolean Then
            fileHasBeenModified = False
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_RenameSelected_Click(sender As Object, e As EventArgs) Handles Button_RenameSelected.Click
        If ListBox_HashCodes.SelectedItem IsNot Nothing Then
            Dim folderToCheck As String = Path.Combine(WorkingDirectory, "Reverbs")
            Dim currentFileName As String = ListBox_HashCodes.SelectedItem
            Dim newFileName As String = RenameFile(currentFileName, "Reverb", folderToCheck)
            If newFileName IsNot "" Then
                'Move file
                Dim currentFilePath As String = Path.Combine(WorkingDirectory, "Reverbs", currentFileName & ".txt")
                File.Move(currentFileName, Path.Combine(WorkingDirectory, "Reverbs", newFileName & ".txt"))

                'Update Listbox
                Dim itemPos As Integer = ListBox_HashCodes.Items.IndexOf(currentFileName)
                If itemPos <> ListBox.NoMatches Then
                    ListBox_HashCodes.Items(itemPos) = newFileName
                End If
            End If
        End If
    End Sub

    Private Sub Button_RemapHashCodes_Click(sender As Object, e As EventArgs) Handles Button_RemapHashCodes.Click
        If ListBox_HashCodes.Items.Count > 0 Then
            My.Computer.Audio.PlaySystemSound(SystemSounds.Exclamation)
            Dim answer As DialogResult = MsgBox("Are you sure you wish to ReMap the Reverb HashCodes?", vbYesNo + vbQuestion, "Confirm HashCode Remap.")
            If answer = DialogResult.Yes Then
                'Reset variables
                ReverbHashCodeNumber = 1

                'Update file
                For index As Integer = 0 To ListBox_HashCodes.Items.Count - 1
                    'Get file path
                    Dim currentItem As String = ListBox_HashCodes.Items(index)
                    Dim filePath As String = Path.Combine(WorkingDirectory, "Reverbs", currentItem & ".txt")

                    'Read data and update hashcode
                    Dim fileData As String() = File.ReadAllLines(filePath)
                    fileData(1) = "HashCode  " & ReverbHashCodeNumber
                    File.WriteAllLines(filePath, fileData)
                    ReverbHashCodeNumber += 1
                Next

                'Update misc file
                writers.UpdateMiscFile(Path.Combine(WorkingDirectory, "System", "Misc.txt"))

                'Reset selection
                ListBox_HashCodes.SelectedIndex = 0
                Label_HashCode.Text = "0x00000001"
            End If
        End If
    End Sub

    Private Sub Button_DeleteSelection_Click(sender As Object, e As EventArgs) Handles Button_DeleteSelection.Click
        If ListBox_HashCodes.SelectedItem IsNot Nothing Then
            My.Computer.Audio.PlaySystemSound(SystemSounds.Exclamation)
            Dim answer As DialogResult = MsgBox("Delete Reverb '" & ListBox_HashCodes.SelectedItem & "'?", vbYesNo + vbQuestion, "Confirm Reverb Delete.")
            If answer = DialogResult.Yes Then
                Dim fileToDeletePath As String = Path.Combine(WorkingDirectory, "Reverbs", ListBox_HashCodes.SelectedItem & ".txt")
                File.Delete(fileToDeletePath)
                ListBox_HashCodes.Items.Remove(ListBox_HashCodes.SelectedItem)
            End If
        End If
    End Sub

    Private Sub Button_CopySelection_Click(sender As Object, e As EventArgs) Handles Button_CopySelection.Click
        If ListBox_HashCodes.SelectedItem IsNot Nothing Then
            Dim folderToCheck As String = Path.Combine(WorkingDirectory, "Reverbs")
            Dim reverbName As String = CopyFile(GetNextAvailableFileName(folderToCheck, "RVB_ROOM_"), "Reverb", folderToCheck)
            If reverbName IsNot "" Then
                Dim sourcePath As String = Path.Combine(folderToCheck, ListBox_HashCodes.SelectedItem & ".txt")
                Dim destinationPath As String = Path.Combine(folderToCheck, reverbName & ".txt")
                File.Copy(sourcePath, destinationPath)
                ListBox_HashCodes.Items.Add(reverbName)
            End If
        End If
    End Sub

    Private Sub Button_AddNewHashCode_Click(sender As Object, e As EventArgs) Handles Button_AddNewHashCode.Click
        'Create a new file
        Dim folderToCheck As String = Path.Combine(WorkingDirectory, "Reverbs")
        Dim reverbName As String = NewFile(GetNextAvailableFileName(folderToCheck, "RVB_ROOM_"), folderToCheck)
        If reverbName IsNot "" Then
            'Add item to listbox
            ListBox_HashCodes.Items.Add(reverbName)

            'Create a new file
            Dim newReverbFile As New ReverbFile With {
                .HashCode = ReverbHashCodeNumber
            }
            ReverbHashCodeNumber += 1
            writers.UpdateMiscFile(Path.Combine(WorkingDirectory, "System", "Misc.txt"))

            'Create a new file
            Dim filePath As String = Path.Combine(WorkingDirectory, "Reverbs", reverbName & ".txt")
            writers.WriteReverbFile(filePath, newReverbFile)
        End If
    End Sub

    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        'Close file
        Close()
    End Sub

    '*===============================================================================================
    '* LISTBOX EVENTS
    '*===============================================================================================
    Private Sub ListBox_HashCodes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox_HashCodes.SelectedIndexChanged
        If ListBox_HashCodes.SelectedItem IsNot Nothing Then
            Dim itemFileName As String = ListBox_HashCodes.SelectedItem
            Dim filePath As String = Path.Combine(WorkingDirectory, "Reverbs", itemFileName & ".txt")
            If File.Exists(filePath) Then
                'Ask for save
                If fileHasBeenModified Then
                    My.Computer.Audio.PlaySystemSound(SystemSounds.Exclamation)
                    Dim answer As DialogResult = MsgBox("Save Changes to : '" & currentSelectedItem & "'?", vbYesNo + vbQuestion, "Save Reverb?")
                    If answer = DialogResult.Yes Then
                        Dim fileToSavePath As String = Path.Combine(WorkingDirectory, "Reverbs", currentSelectedItem & ".txt")
                        writers.WriteReverbFile(fileToSavePath, currentLoadedFile)
                    End If
                End If
                'Load file
                currentSelectedItem = itemFileName
                currentLoadedFile = readers.ReadReverbFile(filePath)
                Label_HashCode.Text = "0x" & currentLoadedFile.HashCode.ToString("X8")
                ShowReverbFile(TabControl1.SelectedTab.Text)
                fileHasBeenModified = False
            End If
        End If
    End Sub

    '*===============================================================================================
    '* METHODS
    '*===============================================================================================
    Private Sub ShowReverbFile(selectedTab)
        If currentLoadedFile IsNot Nothing Then
            Select Case selectedTab
                Case "PC"
                    TrackBar_RoomSize.Value = currentLoadedFile.PCReverb.RoomSize
                    TrackBar_Width.Value = currentLoadedFile.PCReverb.Width
                    TrackBar_Damp.Value = currentLoadedFile.PCReverb.Damp
                    TrackBar_LowPassFilter.Value = currentLoadedFile.PCReverb.LowPassFilter
                    TrackBar_Filter1.Value = currentLoadedFile.PCReverb.Filter1
                    TrackBar_Filter2.Value = currentLoadedFile.PCReverb.Filter2
                Case "XBox"
                    TrackBar_RoomSize.Value = currentLoadedFile.XBReverb.RoomSize
                    TrackBar_Width.Value = currentLoadedFile.XBReverb.Width
                    TrackBar_Damp.Value = currentLoadedFile.XBReverb.Damp
                    TrackBar_LowPassFilter.Value = currentLoadedFile.XBReverb.LowPassFilter
                    TrackBar_Filter1.Value = currentLoadedFile.XBReverb.Filter1
                    TrackBar_Filter2.Value = currentLoadedFile.XBReverb.Filter2
                Case "GameCube"
                    TrackBar_RoomSize.Value = currentLoadedFile.GCReverb.RoomSize
                    TrackBar_Width.Value = currentLoadedFile.GCReverb.Width
                    TrackBar_Damp.Value = currentLoadedFile.GCReverb.Damp
                    TrackBar_LowPassFilter.Value = currentLoadedFile.GCReverb.LowPassFilter
                    TrackBar_Filter1.Value = currentLoadedFile.GCReverb.Filter1
                    TrackBar_Filter2.Value = currentLoadedFile.GCReverb.Filter2
            End Select
        End If
    End Sub

    '*===============================================================================================
    '* TRACKBAR
    '*===============================================================================================
    Private Sub TrackBar_RoomSize_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_RoomSize.ValueChanged
        If currentLoadedFile IsNot Nothing Then
            fileHasBeenModified = True
            Select Case TabControl1.SelectedTab.Text
                Case "PC"
                    currentLoadedFile.PCReverb.RoomSize = TrackBar_RoomSize.Value
                Case "XBox"
                    currentLoadedFile.XBReverb.RoomSize = TrackBar_RoomSize.Value
                Case "GameCube"
                    currentLoadedFile.GCReverb.RoomSize = TrackBar_RoomSize.Value
            End Select
        End If
    End Sub

    Private Sub TrackBar_Width_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_Width.ValueChanged
        If currentLoadedFile IsNot Nothing Then
            fileHasBeenModified = True
            Select Case TabControl1.SelectedTab.Text
                Case "PC"
                    currentLoadedFile.PCReverb.Width = TrackBar_Width.Value
                Case "XBox"
                    currentLoadedFile.XBReverb.Width = TrackBar_Width.Value
                Case "GameCube"
                    currentLoadedFile.GCReverb.Width = TrackBar_Width.Value
            End Select
        End If
    End Sub

    Private Sub TrackBar_Damp_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_Damp.ValueChanged
        If currentLoadedFile IsNot Nothing Then
            fileHasBeenModified = True
            Select Case TabControl1.SelectedTab.Text
                Case "PC"
                    currentLoadedFile.PCReverb.Damp = TrackBar_Damp.Value
                Case "XBox"
                    currentLoadedFile.XBReverb.Damp = TrackBar_Damp.Value
                Case "GameCube"
                    currentLoadedFile.GCReverb.Damp = TrackBar_Damp.Value
            End Select
        End If
    End Sub

    Private Sub TrackBar_LowPassFilter_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_LowPassFilter.ValueChanged
        If currentLoadedFile IsNot Nothing Then
            fileHasBeenModified = True
            Select Case TabControl1.SelectedTab.Text
                Case "PC"
                    currentLoadedFile.PCReverb.LowPassFilter = TrackBar_LowPassFilter.Value
                Case "XBox"
                    currentLoadedFile.XBReverb.LowPassFilter = TrackBar_LowPassFilter.Value
                Case "GameCube"
                    currentLoadedFile.GCReverb.LowPassFilter = TrackBar_LowPassFilter.Value
            End Select
        End If
    End Sub

    Private Sub TrackBar_Filter1_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_Filter1.ValueChanged
        If currentLoadedFile IsNot Nothing Then
            fileHasBeenModified = True
            Select Case TabControl1.SelectedTab.Text
                Case "PC"
                    currentLoadedFile.PCReverb.Filter1 = TrackBar_Filter1.Value
                Case "XBox"
                    currentLoadedFile.XBReverb.Filter1 = TrackBar_Filter1.Value
                Case "GameCube"
                    currentLoadedFile.GCReverb.Filter1 = TrackBar_Filter1.Value
            End Select
        End If
    End Sub

    Private Sub TrackBar_Filter2_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_Filter2.ValueChanged
        If currentLoadedFile IsNot Nothing Then
            fileHasBeenModified = True
            Select Case TabControl1.SelectedTab.Text
                Case "PC"
                    currentLoadedFile.PCReverb.Filter2 = TrackBar_Filter2.Value
                Case "XBox"
                    currentLoadedFile.XBReverb.Filter2 = TrackBar_Filter2.Value
                Case "GameCube"
                    currentLoadedFile.GCReverb.Filter2 = TrackBar_Filter2.Value
            End Select
        End If
    End Sub
End Class
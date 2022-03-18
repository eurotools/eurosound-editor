Imports System.IO
Imports System.Text.RegularExpressions
Imports IniFileFunctions
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Public Class SfxNewMultiple
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly rgx As New Regex("\d+$")
    Private samplesDictionary As New Dictionary(Of String, List(Of String))
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly writers As New FileWriters

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub SfxNewMultiple_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ListBox_SampleFiles.Items.Count = 0 Then
            Dim diagResult = OpenFile_WaveFiles.ShowDialog
            If diagResult = DialogResult.OK Then
                AddFilesToListbox(OpenFile_WaveFiles.FileNames)
            End If
        End If

        'Load config
        If Directory.Exists(Path.Combine(WorkingDirectory, "System")) Then
            Dim iniFunctions As New IniFile(SysFileProjectIniPath)
            Dim tempvar As String = iniFunctions.Read("Check1_Value", "Form11_Misc")
            If IsNumeric(tempvar) Then
                CheckBox_ForceUpperCase.Checked = CBool(tempvar)
            End If
            tempvar = iniFunctions.Read("Check2_Value", "Form11_Misc")
            If IsNumeric(tempvar) Then
                CheckBox_RandomSequence.Checked = CBool(tempvar)
            End If
            TextBox_SfxPrefix.Text = iniFunctions.Read("Text1_Text", "Form11_Misc")
        End If
    End Sub

    Private Sub SfxNewMultiple_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Save data in the Ini File
        If Directory.Exists(Path.Combine(WorkingDirectory, "System")) Then
            Dim iniFunctions As New IniFile(SysFileProjectIniPath)
            iniFunctions.Write("Check1_Value", Convert.ToByte(CheckBox_ForceUpperCase.Checked), "Form11_Misc")
            iniFunctions.Write("Check2_Value", Convert.ToByte(CheckBox_RandomSequence.Checked), "Form11_Misc")
            iniFunctions.Write("Text1_Text", TextBox_SfxPrefix.Text, "Form11_Misc")
        End If
    End Sub

    '*===============================================================================================
    '* BUTTON EVENTS
    '*===============================================================================================
    Private Sub Button_Add_Click(sender As Object, e As EventArgs) Handles Button_Add.Click
        Dim diagResult = OpenFile_WaveFiles.ShowDialog
        If diagResult = DialogResult.OK Then
            AddFilesToListbox(OpenFile_WaveFiles.FileNames)
        End If
    End Sub

    Private Sub Button_Remove_Click(sender As Object, e As EventArgs) Handles Button_Remove.Click
        If ListBox_SampleFiles.SelectedIndex <> -1 Then
            Dim selectedItems As ListBox.SelectedObjectCollection = ListBox_SampleFiles.SelectedItems
            For index As Integer = selectedItems.Count - 1 To 0 Step -1
                ListBox_SampleFiles.Items.Remove(selectedItems(index))
            Next
            GetSfxNames()
        End If
    End Sub

    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Close()
    End Sub

    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        If Directory.Exists(Path.Combine(WorkingDirectory, "System")) Then
            Dim mainFrame As MainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
            If File.Exists(SysFileSfxDefaults) Then
                Dim sfxDefaults As SfxFile = textFileReaders.ReadSFXFile(SysFileSfxDefaults)
                Dim sampleDefaultValues As Double() = GetDefaultSampleValues()
                mainFrame.UserControl_SFXs.ListBox_SFXs.BeginUpdate()

                For Each newSfx As KeyValuePair(Of String, List(Of String)) In samplesDictionary
                    Dim sfxFilePath As String = Path.Combine(WorkingDirectory, "SFXs", newSfx.Key & ".txt")
                    Dim sfxFileData As New SfxFile
                    '#SFXParameters
                    sfxFileData.Parameters.ReverbSend = sfxDefaults.Parameters.ReverbSend
                    sfxFileData.Parameters.TrackingType = sfxDefaults.Parameters.TrackingType
                    sfxFileData.Parameters.InnerRadius = sfxDefaults.Parameters.InnerRadius
                    sfxFileData.Parameters.OuterRadius = sfxDefaults.Parameters.OuterRadius
                    sfxFileData.Parameters.MaxVoices = sfxDefaults.Parameters.MaxVoices
                    sfxFileData.Parameters.Action1 = sfxDefaults.Parameters.Action1
                    sfxFileData.Parameters.Priority = sfxDefaults.Parameters.Priority
                    sfxFileData.Parameters.Group = sfxDefaults.Parameters.Group
                    sfxFileData.Parameters.Action2 = sfxDefaults.Parameters.Action2
                    sfxFileData.Parameters.Alertness = sfxDefaults.Parameters.Alertness
                    sfxFileData.Parameters.IgnoreAge = sfxDefaults.Parameters.IgnoreAge
                    sfxFileData.Parameters.Ducker = sfxDefaults.Parameters.Ducker
                    sfxFileData.Parameters.DuckerLength = sfxDefaults.Parameters.DuckerLength
                    sfxFileData.Parameters.MasterVolume = sfxDefaults.Parameters.MasterVolume
                    sfxFileData.Parameters.Outdoors = sfxDefaults.Parameters.Outdoors
                    sfxFileData.Parameters.PauseInNis = sfxDefaults.Parameters.PauseInNis
                    sfxFileData.Parameters.StealOnAge = sfxDefaults.Parameters.StealOnAge
                    sfxFileData.Parameters.Doppler = sfxDefaults.Parameters.Doppler
                    '#SFXSamplePoolFiles & #SFXSamplePoolModes
                    For Each waveFilePath As String In newSfx.Value
                        'Create new sample
                        Dim sampleObj As New Sample With {
                            .FilePath = waveFilePath.Substring(Len(ProjectSettingsFile.MiscProps.SampleFileFolder & "\Master\")),
                            .PitchOffset = sampleDefaultValues(0),
                            .RandomPitchOffset = sampleDefaultValues(1),
                            .BaseVolume = sampleDefaultValues(2),
                            .RandomVolumeOffset = sampleDefaultValues(3),
                            .Pan = sampleDefaultValues(4),
                            .RandomPan = sampleDefaultValues(5)
                        }
                        'Add object to list
                        sfxFileData.Samples.Add(sampleObj)
                    Next
                    '#SFXSamplePoolControl
                    sfxFileData.SamplePool.Action1 = sfxDefaults.SamplePool.Action1
                    sfxFileData.SamplePool.RandomPick = sfxDefaults.SamplePool.RandomPick
                    sfxFileData.SamplePool.Shuffled = sfxDefaults.SamplePool.Shuffled
                    sfxFileData.SamplePool.isLooped = sfxDefaults.SamplePool.isLooped
                    sfxFileData.SamplePool.Polyphonic = sfxDefaults.SamplePool.Polyphonic
                    sfxFileData.SamplePool.MinDelay = sfxDefaults.SamplePool.MinDelay
                    sfxFileData.SamplePool.MaxDelay = sfxDefaults.SamplePool.MaxDelay
                    sfxFileData.SamplePool.EnableSubSFX = sfxDefaults.SamplePool.EnableSubSFX
                    '#HASHCODE
                    sfxFileData.HashCode = SFXHashCodeNumber
                    SFXHashCodeNumber += 1
                    'Write files
                    writers.WriteSfxFile(sfxFileData, sfxFilePath)
                    'Add item to mainframe listbox
                    mainFrame.UserControl_SFXs.ListBox_SFXs.Items.Add(newSfx.Key)
                Next
                mainFrame.UserControl_SFXs.ListBox_SFXs.EndUpdate()
            Else
                MsgBox("Error, SFX Defaults file not found.", vbOKOnly + MsgBoxStyle.Critical, "EuroSound")
            End If
        End If
        'Close form
        Close()
    End Sub

    '*===============================================================================================
    '* TEXTBOX EVENTS
    '*===============================================================================================
    Private Sub TextBox_SfxPrefix_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SfxPrefix.TextChanged
        GetSfxNames()
    End Sub

    Private Sub CheckBox_ForceUpperCase_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox_ForceUpperCase.CheckStateChanged
        GetSfxNames()
    End Sub

    Private Sub CheckBox_RandomSequence_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox_RandomSequence.CheckStateChanged
        GetSfxNames()
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Sub AddFilesToListbox(selectedFiles As String())
        'Add items to listbox
        For fileIndex As Integer = 0 To selectedFiles.Length - 1
            Dim currentFilePath As String = selectedFiles(fileIndex)
            'Avoid duplicates
            If Not ListBox_SampleFiles.Items.Contains(currentFilePath) Then
                ListBox_SampleFiles.Items.Add(currentFilePath)
            End If
        Next
        GetSfxNames()
        Erase selectedFiles
    End Sub

    Private Sub GetSfxNames()
        ListBox_SfxNames.Items.Clear()
        ListBox_SfxNames.BeginUpdate()
        samplesDictionary = New Dictionary(Of String, List(Of String))
        'Add items
        For Each filePath As String In ListBox_SampleFiles.Items
            Dim fileName As String = Path.GetFileNameWithoutExtension(filePath)
            'Remove numbers at the start if required
            If CheckBox_RandomSequence.Checked Then
                fileName = rgx.Replace(fileName, "")
            End If
            'Get Hashcode name with the prefix, uppercase if required
            Dim hashCodeName As String = TextBox_SfxPrefix.Text & fileName
            If CheckBox_ForceUpperCase.Checked Then
                hashCodeName = TextBox_SfxPrefix.Text & UCase(fileName)
            End If
            hashCodeName = RemoveSpecialCharacters(hashCodeName)
            'Ensure that this item does not exists
            If ListBox_SfxNames.FindString(hashCodeName) = ListBox.NoMatches Then
                'Add item to listbox
                ListBox_SfxNames.Items.Add(hashCodeName)
            End If
            'Add item to dictionary
            If samplesDictionary.ContainsKey(hashCodeName) Then
                samplesDictionary(hashCodeName).Add(filePath)
            Else
                samplesDictionary.Add(hashCodeName, New List(Of String)(New String() {filePath}))
            End If
        Next
        ListBox_SfxNames.EndUpdate()
    End Sub

    Private Function RemoveSpecialCharacters(str As String) As String
        Dim sb As String = ""
        For Each c As Char In str
            If (c >= "0"c AndAlso c <= "9"c) OrElse (c >= "A"c AndAlso c <= "Z"c) OrElse (c >= "a"c AndAlso c <= "z"c) OrElse c = "_"c Then
                sb &= c
            End If
        Next
        Return sb
    End Function
End Class
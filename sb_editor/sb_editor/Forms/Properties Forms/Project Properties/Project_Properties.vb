Imports IniFileFunctions

Partial Public Class Project_Properties
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly textFileReaders As New FileParsers()
    Private ReadOnly ratesNames As String() = New String() {"Low", "Medium", "High", "Maximum"}
    Private propsFileData As PropertiesFile
    Private promptSave As Boolean = True
    Private ratesNamesIndex As Byte = 0

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub Project_Properties_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Read data if is an existing project
        If fso.FileExists(SysFileProperties) Then
            propsFileData = textFileReaders.ReadPropertiesFile(SysFileProperties)
            'Put available formats and select the first one
            If propsFileData.AvailableFormats.Length > 0 Then
                'Print available formats
                CopyArrayToListView(ListView_Formats, propsFileData.AvailableFormats)

                'Select first item
                ListView_Formats.Items(0).Selected = True
                ListView_Formats.Items(0).Focused = True
            End If
            'Select the first item by default - Formats
            If ComboBox_Platform.Items.Count > 0 Then
                ComboBox_Platform.SelectedIndex = 0
            End If
            'Print available formats in the combo
            If propsFileData.AvailableReSampleRates.Count > 0 Then
                'Add available rates format to the combobox and select the first one
                If propsFileData.AvailableFormats.Length > 0 Then
                    ComboBox_RatesFormat.Items.AddRange(GetColumn(propsFileData.AvailableFormats, 0))
                    ComboBox_RatesFormat.SelectedIndex = 0
                End If
                Dim sampleRatesArray As String() = propsFileData.AvailableReSampleRates.ToArray
                'Add available rates to the listbox
                ListBox_SampleRates.Items.AddRange(sampleRatesArray)
                'Add available rates to the combobox
                ComboBox_DefaultSampleRate.Items.AddRange(sampleRatesArray)
                ComboBox_DefaultSampleRate.SelectedIndex = propsFileData.MiscProps.DefaultRate
            End If

            'Misc properties
            Textbox_Master_Path.Text = propsFileData.MiscProps.SampleFileFolder
            Textbox_SonixFolder.Text = propsFileData.MiscProps.HashCodeFileFolder
            Textbox_EngineXFolder.Text = propsFileData.MiscProps.EngineXFolder
            Textbox_EuroLandServer.Text = propsFileData.MiscProps.EuroLandHashCodeServerPath
            TextBox_UserName.Text = EuroSoundUser
            TextBox_EditWavs.Text = ProjAudioEditor
            TextBox_TextEditor.Text = ProjTextEditor

            'Try to read the ini file
            If fso.FileExists(SysFileProjectIniPath) Then
                Dim iniFunctions As New IniFile(SysFileProjectIniPath)

                'Soundbanks Max Size
                Dim playStationValue As String = iniFunctions.Read("PlayStationSize", "PropertiesForm")
                If IsNumeric(playStationValue) Then
                    Numeric_PlayStationMaxSize.Value = CInt(playStationValue)
                End If
                Dim pcSize As String = iniFunctions.Read("PCSize", "PropertiesForm")
                If IsNumeric(pcSize) Then
                    Numeric_PcMaxSize.Value = CInt(pcSize)
                End If
                Dim gameCubeSize As String = iniFunctions.Read("GameCubeSize", "PropertiesForm")
                If IsNumeric(gameCubeSize) Then
                    Numeric_GameCubeMaxSize.Value = CInt(gameCubeSize)
                End If
                Dim xboxSize As String = iniFunctions.Read("XBoxSize", "PropertiesForm")
                If IsNumeric(xboxSize) Then
                    Numeric_XboxMaxSize.Value = xboxSize
                End If
            End If
        End If
    End Sub

    Private Sub Project_Properties_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Ensure that we have an username
        If StrComp(EuroSoundUser, "") = 0 Then
            TextBox_UserName.Text = AskForUserName("MyName")
            RestartEuroSound()
        End If
    End Sub

    Private Sub Project_Properties_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
        SavePropertiesFile()
        SaveIniFile()
        'Close form
        Close()
    End Sub

    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        'Close form
        Close()
    End Sub

    '*===============================================================================================
    '* FOLDER PATHS
    '*===============================================================================================
    Private Sub Button_Master_Path_Click(sender As Object, e As EventArgs) Handles Button_Master_Path.Click
        'Update desc
        FolderBrowserDialog.Description = "Set Folder for Sample Files."
        'Show dialog
        Dim diagRes = FolderBrowserDialog.ShowDialog
        'Set results
        If diagRes = DialogResult.OK Then
            'Ensure that the master folder exists
            If fso.FolderExists(fso.BuildPath(FolderBrowserDialog.SelectedPath, "Master")) Then
                Textbox_Master_Path.Text = FolderBrowserDialog.SelectedPath
                ProjMasterFolder = FolderBrowserDialog.SelectedPath
            Else
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
                MsgBox("Master folder not found, please choose another path.", vbOKOnly + vbCritical, "EuroSound")
            End If
        End If
    End Sub

    Private Sub Button_SonixFolder_Click(sender As Object, e As EventArgs) Handles Button_SonixFolder.Click
        'Update desc
        FolderBrowserDialog.Description = "Set Folder for Hashcodes Files."
        'Show dialog
        Dim diagRes = FolderBrowserDialog.ShowDialog
        'Set results
        If diagRes = DialogResult.OK Then
            Textbox_SonixFolder.Text = FolderBrowserDialog.SelectedPath
            ProjOutHashCodesFolder = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub Button_EngineXFolder_Click(sender As Object, e As EventArgs) Handles Button_EngineXFolder.Click
        'Update desc
        FolderBrowserDialog.Description = "Set Folder for Hashcodes Files."
        'Show dialog
        Dim diagRes = FolderBrowserDialog.ShowDialog
        'Set results
        If diagRes = DialogResult.OK Then
            Textbox_EngineXFolder.Text = FolderBrowserDialog.SelectedPath
            ProjOutEngineXFolder = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub Button_EuroLandServer_Click(sender As Object, e As EventArgs) Handles Button_EuroLandServer.Click
        'Update desc
        FolderBrowserDialog.Description = "Set Folder to the EuroLand HashCodes folder."
        'Show dialog
        Dim diagRes = FolderBrowserDialog.ShowDialog
        'Set results
        If diagRes = DialogResult.OK Then
            Textbox_EuroLandServer.Text = FolderBrowserDialog.SelectedPath
            ProjOutEuroLandServer = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub Button_BrowseOutput_Click(sender As Object, e As EventArgs) Handles Button_BrowseOutput.Click
        'Ensure that we have an item selected
        If ListView_Formats.SelectedItems.Count > 0 Then
            'Update desc
            FolderBrowserDialog.Description = "Set Folder for the Output Files."
            'Show dialog
            Dim diagRes = FolderBrowserDialog.ShowDialog
            'Set results
            If diagRes = DialogResult.OK Then
                ListView_Formats.SelectedItems(0).SubItems(1).Text = FolderBrowserDialog.SelectedPath
            End If
        End If
    End Sub

    '*===============================================================================================
    '* AVAILABLE FORMATS SECTION
    '*===============================================================================================
    Private Sub Button_AddFormat_Click(sender As Object, e As EventArgs) Handles Button_AddFormat.Click
        'Ensure that there is an item selected in the combobox
        If ComboBox_Platform.SelectedItem IsNot Nothing Then
            Dim selectedPlatform = ComboBox_Platform.SelectedItem
            'Check if the platform has been added to the dictionary
            If Not propsFileData.sampleRateFormats.ContainsKey(selectedPlatform) Then
                'Add platform to dictionary
                propsFileData.sampleRateFormats.Add(selectedPlatform, New Dictionary(Of String, UInteger))
                'Add item to list
                Dim formatitem As New ListViewItem(New String() {selectedPlatform, "Set Output Folder.", "On"})
                ListView_Formats.Items.Add(formatitem)
                'Add item to combobox
                If Not ComboBox_RatesFormat.Items.Contains(selectedPlatform) Then
                    ComboBox_RatesFormat.Items.Add(selectedPlatform)
                End If
            End If
        End If
    End Sub

    '*===============================================================================================
    '* SAMPLE RATES SECTION
    '*===============================================================================================
    Private Sub Button_AddSampleRate_Click(sender As Object, e As EventArgs) Handles Button_AddSampleRate.Click
        'Get name
        Dim defaultName
        If ratesNamesIndex < ratesNames.Length Then
            defaultName = ratesNames(ratesNamesIndex)
        Else
            defaultName = ratesNames(3) & " " & (ratesNamesIndex - 3)
        End If

        'Ask user for a new resample rate name
        Dim resampleName = InputBox("Enter New Re-sample Rate Name", "New Re-sample Name", defaultName)
        If resampleName IsNot "" Then
            'Ensure that does not exists
            If Not propsFileData.AvailableReSampleRates.Contains(resampleName) Then
                'Add item to list
                ListBox_SampleRates.Items.Add(resampleName)
                'Add item to combobox
                ComboBox_DefaultSampleRate.Items.Add(resampleName)
                'Update platform rates
                For Each platform In propsFileData.sampleRateFormats
                    Dim formatRatesList As Dictionary(Of String, UInteger) = platform.Value
                    For index As Integer = 0 To ListBox_SampleRates.Items.Count - 1
                        If Not formatRatesList.ContainsKey(ListBox_SampleRates.Items(index)) Then
                            formatRatesList.Add(ListBox_SampleRates.Items(index), 22050)
                        End If
                    Next
                Next
                'Print rates
                If ComboBox_RatesFormat.SelectedItem IsNot Nothing Then
                    'Get selected platform
                    Dim selectedPlatform As String = ComboBox_RatesFormat.SelectedItem
                    PrintFormatRates(selectedPlatform)
                End If
            End If
            'Update counter
            ratesNamesIndex += 1
        End If
    End Sub

    Private Sub ComboBox_RatesFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_RatesFormat.SelectedIndexChanged
        'Ensure that the combobox has a valid item selected
        If ComboBox_RatesFormat.SelectedItem IsNot Nothing AndAlso propsFileData.sampleRateFormats.Count > 0 Then
            PrintFormatRates(ComboBox_RatesFormat.SelectedItem)
        End If
    End Sub

    Private Sub ListView_SampleRateValues_DoubleClick(sender As Object, e As EventArgs) Handles ListView_SampleRateValues.DoubleClick
        Dim selectedLabel As String = ListView_SampleRateValues.SelectedItems(0).Text

        'Ensure that we have selected an item
        If ListView_SampleRateValues.SelectedItems.Count > 0 AndAlso propsFileData.sampleRateFormats(ComboBox_RatesFormat.SelectedItem).ContainsKey(selectedLabel) Then
            'Ask user for a value
            Dim currentSampleRate = ListView_SampleRateValues.SelectedItems(0).SubItems(1).Text
            Dim InputValue As String = InputBox("Enter New Re-sample Rate", "New Sample Rate", currentSampleRate)
            'Ensure that is valid
            If IsNumeric(InputValue) Then
                'Update subitem
                ListView_SampleRateValues.SelectedItems(0).SubItems(1).Text = InputValue
                'Update dictionary
                propsFileData.sampleRateFormats(ComboBox_RatesFormat.SelectedItem)(selectedLabel) = CInt(InputValue)
            Else
                'Inform user
                MsgBox("Invalid sample rate value", vbOKOnly + vbExclamation, "Error")
            End If
        End If
    End Sub

    '*===============================================================================================
    '* MISC SECTION
    '*===============================================================================================
    Private Sub TextBox_EditWavs_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox_EditWavs.MouseDoubleClick
        'Show dialog
        Dim diagResult As DialogResult = OpenFileDialog.ShowDialog
        'Put selected path to the textbox
        If (diagResult = DialogResult.OK) Then
            'Update textbox and variables
            TextBox_EditWavs.Text = OpenFileDialog.FileName
            ProjAudioEditor = OpenFileDialog.FileName
            'Update ini file
            Dim iniFunctions As New IniFile(EuroSoundIniFilePath)
            iniFunctions.Write("Edit_Wavs_With", OpenFileDialog.FileName, "Form7_Misc")
        End If
    End Sub

    Private Sub TextBox_UserName_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox_UserName.MouseDoubleClick
        'Ask user for a new username
        TextBox_UserName.Text = AskForUserName(TextBox_UserName.Text)
        EuroSoundUser = TextBox_UserName.Text
    End Sub

    Private Sub TextBox_TextEditor_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox_TextEditor.MouseDoubleClick
        'Show dialog
        Dim diagResult As DialogResult = OpenFileDialog.ShowDialog
        'Put selected path to the textbox
        If diagResult = DialogResult.OK Then
            'Update textbox and variables
            TextBox_TextEditor.Text = OpenFileDialog.FileName
            ProjTextEditor = OpenFileDialog.FileName
            'Update ini file
            Dim iniFunctions As New IniFile(EuroSoundIniFilePath)
            iniFunctions.Write("TextEditor", OpenFileDialog.FileName, "PropertiesForm")
        End If
    End Sub
End Class
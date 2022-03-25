Imports System.IO
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Partial Public Class ExporterForm
    Inherits Frm_TimerForm
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly quickOutput As Boolean
    Private ReadOnly mainFrame As MainFrame
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly textFileWritters As New FileWriters
    Private canCloseForm As Boolean = False

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(quickOptionClicked As Boolean)
        'Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        quickOutput = quickOptionClicked
        mainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
    End Sub

    Private Sub ExporterForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Start process
        If Not BackgroundWorker.IsBusy Then
            BackgroundWorker.RunWorkerAsync()
        End If
    End Sub

    '*===============================================================================================
    '* BACKGROUND WORKER EVENTS
    '*===============================================================================================
    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
        '----------------------------------------------Get all required data----------------------------------------------
        Invoke(Sub() Text = "Waiting")

        'Create bat if required
        Dim preOutbatFilePath As String = Path.Combine(WorkingDirectory, "System", "PreOutput.bat")
        CreatePrePostOutputBatIfRequired(preOutbatFilePath, "rem Add your pre-output stuff here")
        RunConsoleProcess(preOutbatFilePath, "", ProjectSettingsFile.MiscProps.ViewOutputDos)

        'Get output platforms
        Dim outPlaforms As String() = New String() {mainFrame.ComboBox_Format.Invoke(Function() mainFrame.ComboBox_Format.SelectedItem)}
        If mainFrame.RadioButton_Output_AllBanksAll.Checked Then
            outPlaforms = ProjectSettingsFile.sampleRateFormats.Keys.ToArray
        End If

        'Get output languages
        Dim outputLanguage As String() = New String() {SfxLanguages(0)}
        If mainFrame.ComboBox_OutputLanguage.Items.Count > 0 Then
            'Get selected language
            Dim selectedLanguage As String = mainFrame.ComboBox_OutputLanguage.Invoke(Function() mainFrame.ComboBox_OutputLanguage.SelectedItem)
            If Not String.IsNullOrEmpty(selectedLanguage) Then
                outputLanguage = New String() {selectedLanguage}
            End If

            'Get all languages
            If mainFrame.CheckBox_OutAllLanguages.Checked Then
                outputLanguage = New String(mainFrame.ComboBox_OutputLanguage.Items.Count - 1) {}
                For langIndex As Integer = 0 To outputLanguage.Length - 1
                    outputLanguage(langIndex) = mainFrame.ComboBox_OutputLanguage.Items(langIndex)
                Next
            End If
        End If

        'Get output soundbanks
        Dim outSoundBanks As String() = New String() {}
        If mainFrame.RadioButton_Output_SelectedSoundBank.Checked Then
            If mainFrame.TreeView_SoundBanks.Invoke(Function() mainFrame.TreeView_SoundBanks.SelectedNode) IsNot Nothing Then
                outSoundBanks = New String() {mainFrame.TreeView_SoundBanks.Invoke(Function() mainFrame.TreeView_SoundBanks.SelectedNode.Text)}
            End If
        Else
            outSoundBanks = New String(mainFrame.TreeView_SoundBanks.Nodes.Count) {}
            For nodeIndex As Integer = 0 To mainFrame.TreeView_SoundBanks.Nodes.Count - 1
                outSoundBanks(nodeIndex) = mainFrame.TreeView_SoundBanks.Nodes(nodeIndex).Text
            Next
        End If

        'Get SFXs
        Dim hashCodesDictionary As SortedDictionary(Of String, UInteger) = GetHashCodesDict(Path.Combine(WorkingDirectory, "SFXs"))
        Dim soundBanksDictionary As SortedDictionary(Of String, UInteger) = GetSoundBanksDict(Path.Combine(WorkingDirectory, "SoundBanks"))
        Dim reverbHashCodesDictionary As SortedDictionary(Of String, UInteger) = GetReverbsDict(Path.Combine(WorkingDirectory, "Reverbs"))

        'Check for new and missing samples
        If Not quickOutput Then
            Invoke(Sub() mainFrame.CheckForMissingSamples())
            Invoke(Sub() mainFrame.CheckForNewSamples())
        End If

        'Get data
        Dim soundsTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)

        '----------------------------------------------Resample samples and streams----------------------------------------------
        If Not quickOutput Then
            Dim SoxTimer As New Stopwatch()
            Dim PCTimer As New Stopwatch()
            Dim GCTimer As New Stopwatch()
            Dim XboxTimer As New Stopwatch()
            Dim PlayStationTimer As New Stopwatch()

            'Get all available formats
            ResampleWaves(soundsTable, ProjectSettingsFile.AvailableFormats, SoxTimer, PCTimer, GCTimer, XboxTimer, PlayStationTimer)
            If ReSampleStreams = 1 Then
                GenerateStreamFolder(outputLanguage, ProjectSettingsFile.AvailableFormats, soundsTable)
                ReSampleStreams = 0
                textFileWritters.UpdateMiscFile(Path.Combine(WorkingDirectory, "System", "Misc.txt"))
            End If

            'Show timers
            If SoxTimer.ElapsedMilliseconds > 0 Then
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "Re-Sample Times" & vbCrLf)
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "---------------" & vbCrLf)
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "" & vbCrLf)
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "SoxTime " & SoxTimer.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "PCTime " & PCTimer.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "GCTime " & GCTimer.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "XBTime " & XboxTimer.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += "PSTime " & PlayStationTimer.Elapsed.TotalMilliseconds.ToString.TrimStart("0"c) & vbCrLf)
                mainFrame.Textbox_DebugInfo.Invoke(Sub() mainFrame.Textbox_DebugInfo.Text += vbCrLf)
            End If
        End If

        '----------------------------------------------Output user selected Soundbanks----------------------------------------------
        Dim OutputAborted As Boolean = False
        If outSoundBanks.Length > 0 Then
            OutputSoundbanks(hashCodesDictionary, outSoundBanks, outputLanguage, outPlaforms, OutputAborted, soundsTable)
        End If

        'Continue if the output has not been aborted
        If Not OutputAborted Then
            If Not quickOutput Then
                '----------------------------------------------Create SFX Data----------------------------------------------
                Dim maxHashCode = CreateSfxDataTempFolder(soundsTable)

                '----------------------------------------------Create Hashtables----------------------------------------------
                Dim sfxDefinesFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Defines.h")
                Dim sfxDataFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Data.h")
                Dim reverbsFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Reverbs.h")
                Dim soundhFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.EuroLandHashCodeServerPath, "Sound.h")

                CreateSfxDebug(hashCodesDictionary, Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Debug.h"))
                CreateSfxDefines(hashCodesDictionary, soundBanksDictionary, SfxLanguages, ProjectSettingsFile.MiscProps.PrefixHtSound, sfxDefinesFilePath)
                CreateSfxData(sfxDataFilePath, Path.Combine(WorkingDirectory, "TempSfxData"), maxHashCode)
                CreateSfxReverbs(reverbsFilePath, reverbHashCodesDictionary)
                CreateSoundhFile(soundhFilePath, sfxDefinesFilePath, Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "MFX_Defines.h"), reverbsFilePath)

                '----------------------------------------------Create SFX DATA BIN----------------------------------------------
                CreateSFXDataBinaryFiles(outPlaforms, outputLanguage)
            End If

            'Create SoundBanks Info file
            CreateSBInfoFile(outPlaforms, soundBanksDictionary)

            'Create bat if required
            Invoke(Sub() Text = "End")
            Dim postOutBatFilepath As String = Path.Combine(WorkingDirectory, "System", "PostOutput.bat")
            CreatePrePostOutputBatIfRequired(postOutBatFilepath, "rem Add your post-output stuff here")
            RunConsoleProcess(postOutBatFilepath, "", ProjectSettingsFile.MiscProps.ViewOutputDos)
        End If
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Text = e.UserState
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
        'Close task form
        canCloseForm = True
        Close()
    End Sub

    Private Sub ExporterForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not canCloseForm Then
            e.Cancel = True
        End If
    End Sub

    '*===============================================================================================
    '* BAT FILES FUNCTIONS
    '*===============================================================================================
    Private Sub CreatePrePostOutputBatIfRequired(batFilePath As String, comment As String)
        If Not File.Exists(batFilePath) Then
            Using outputFile As New StreamWriter(batFilePath)
                outputFile.WriteLine(comment)
            End Using
        End If
    End Sub
End Class
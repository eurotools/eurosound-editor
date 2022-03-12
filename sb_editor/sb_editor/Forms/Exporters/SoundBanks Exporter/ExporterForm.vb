Imports System.IO
Imports sb_editor.HashTablesBuilder
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

    Private Sub ExporterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Dim preOutbatFilePath As String = fso.BuildPath(WorkingDirectory & "\System", "PreOutput.bat")
        CreatePrePostOutputBatIfRequired(preOutbatFilePath, "rem Add your pre-output stuff here")
        RunProcess(preOutbatFilePath, "", ProjectSettingsFile.MiscProps.ViewOutputDos)

        'Get output platforms
        Dim outPlaforms As String() = New String() {mainFrame.ComboBox_Format.Invoke(Function() mainFrame.ComboBox_Format.SelectedItem)}
        If mainFrame.RadioButton_Output_AllBanksAll.Checked Then
            outPlaforms = ProjectSettingsFile.sampleRateFormats.Keys.ToArray
        End If

        'Get output languages
        Dim outputLanguage As String() = New String() {SfxLanguages(0)}
        If mainFrame.ComboBox_OutputLanguage.Items.Count > 0 Then
            'Get selected language
            outputLanguage = New String() {mainFrame.ComboBox_OutputLanguage.Invoke(Function() mainFrame.ComboBox_OutputLanguage.SelectedItem)}
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
        Dim hashCodesDictionary As New SortedDictionary(Of String, UInteger)
        Dim sfxFiles As String() = Directory.GetFiles(WorkingDirectory & "\SFXs", "*.txt", SearchOption.TopDirectoryOnly)
        For fileIndex As Integer = 0 To sfxFiles.Length - 1
            Dim currentFilePath As String = sfxFiles(fileIndex)
            Dim sfxLabel As String = GetOnlyFileName(currentFilePath)
            If Not hashCodesDictionary.ContainsKey(sfxLabel) Then
                'Get HashCode
                Dim sfxFileData As String() = File.ReadAllLines(currentFilePath)
                Dim hashcodeIndex As Integer = Array.IndexOf(sfxFileData, "#HASHCODE")
                Dim stringData As String() = sfxFileData(hashcodeIndex + 1).Split(" "c)
                If stringData.Length > 1 AndAlso IsNumeric(stringData(1)) Then
                    hashCodesDictionary.Add(sfxLabel, stringData(1))
                End If
            End If
        Next

        'Get all Soundbanks
        Dim soundBanksDictionary As New SortedDictionary(Of String, UInteger)
        Dim soundbankFiles As String() = Directory.GetFiles(WorkingDirectory & "\SoundBanks", "*.txt", SearchOption.TopDirectoryOnly)
        For fileIndex As Integer = 0 To soundbankFiles.Length - 1
            Dim currentFilePath As String = soundbankFiles(fileIndex)
            Dim soundBankLabel As String = GetOnlyFileName(currentFilePath)
            If Not soundBanksDictionary.ContainsKey(soundBankLabel) Then
                'Get HashCode
                Dim soundBankFileData As String() = File.ReadAllLines(currentFilePath)
                Dim hashcodeIndex As Integer = Array.IndexOf(soundBankFileData, "#HASHCODE")
                Dim stringData As String() = soundBankFileData(hashcodeIndex + 1).Split(" "c)
                If stringData.Length > 1 AndAlso IsNumeric(stringData(1)) Then
                    soundBanksDictionary.Add(soundBankLabel, stringData(1))
                End If
            End If
        Next

        'Check for new and missing samples
        If Not quickOutput Then
            Invoke(Sub() mainFrame.CheckForMissingSamples())
            Invoke(Sub() mainFrame.CheckForNewSamples())
        End If

        'Get data
        Dim soundsTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)
        Dim streamSamplesList As String() = textFileReaders.GetStreamSoundsList(SysFileSamples)

        '----------------------------------------------Resample samples and streams----------------------------------------------
        If Not quickOutput Then
            Dim SoxTimer As New Stopwatch()
            Dim PCTimer As New Stopwatch()
            Dim GCTimer As New Stopwatch()
            Dim XboxTimer As New Stopwatch()
            Dim PlayStationTimer As New Stopwatch()
            'Get all available formats
            Dim availableFormats As String() = ProjectSettingsFile.sampleRateFormats.Keys.ToArray
            ResampleWaves(soundsTable, availableFormats, SoxTimer, PCTimer, GCTimer, XboxTimer, PlayStationTimer)
            If ReSampleStreams = 1 Then
                GenerateStreamFolder(streamSamplesList, outputLanguage, availableFormats)
                ReSampleStreams = 0
                textFileWritters.UpdateMiscFile(fso.BuildPath(WorkingDirectory, "System\Misc.txt"))
            End If
            'Save Samples File
            textFileWritters.SaveSamplesFile(SysFileSamples, soundsTable)
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
            OutputSoundbanks(hashCodesDictionary, outSoundBanks, streamSamplesList, outputLanguage, outPlaforms, OutputAborted)
        End If

        'Continue if the output has not been aborted
        If Not OutputAborted Then
            If Not quickOutput Then
                Dim hashTablesBuilder As New SfxDefines
                '----------------------------------------------Create SFX Data----------------------------------------------
                Dim maxHashCode = CreateSfxDataFolder(soundsTable)

                '----------------------------------------------Create Hashtables----------------------------------------------
                hashTablesBuilder.CreateSfxDebug(Me, hashCodesDictionary, fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Debug.h"))
                hashTablesBuilder.CreateSfxDefines(Me, hashCodesDictionary, soundBanksDictionary, SfxLanguages, ProjectSettingsFile.MiscProps.PrefixHtSound, fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Defines.h"))
                hashTablesBuilder.CreateSfxData(Me, fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Data.h"), fso.BuildPath(WorkingDirectory, "TempSfxData"), maxHashCode)
            End If

            'Create bat if required
            Invoke(Sub() Text = "End")
            Dim postOutBatFilepath As String = fso.BuildPath(WorkingDirectory & "\System", "PostOutput.bat")
            CreatePrePostOutputBatIfRequired(postOutBatFilepath, "rem Add your post-output stuff here")
            RunProcess(postOutBatFilepath, "", ProjectSettingsFile.MiscProps.ViewOutputDos)
        End If
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
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
        If Not fso.FileExists(batFilePath) Then
            FileOpen(1, batFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
            PrintLine(1, comment)
            FileClose(1)
        End If
    End Sub
End Class
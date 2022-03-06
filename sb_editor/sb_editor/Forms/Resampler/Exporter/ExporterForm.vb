Imports System.IO
Imports HashTablesBuilder

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

        quickOutput = quickOptionClicked

        'Get mainframe
        mainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
    End Sub

    Private Sub ExporterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Hide mainform
        mainFrame.Hide()
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
            Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(currentFilePath)
            Dim sfxLabel As String = GetOnlyFileName(currentFilePath)
            If Not hashCodesDictionary.ContainsKey(sfxLabel) Then
                hashCodesDictionary.Add(sfxLabel, sfxFileData.HashCode)
            End If
        Next
        'Get all Soundbanks
        Dim soundBanksDictionary As New SortedDictionary(Of String, UInteger)
        Dim soundbankFiles As String() = Directory.GetFiles(WorkingDirectory & "\SoundBanks", "*.txt", SearchOption.TopDirectoryOnly)
        For fileIndex As Integer = 0 To soundbankFiles.Length - 1
            Dim currentFilePath As String = soundbankFiles(fileIndex)
            Dim soundBankFileData As SoundbankFile = textFileReaders.ReadSoundBankFile(currentFilePath)
            Dim soundBankLabel As String = GetOnlyFileName(currentFilePath)
            If Not soundBanksDictionary.ContainsKey(soundBankLabel) Then
                soundBanksDictionary.Add(soundBankLabel, soundBankFileData.HashCode)
            End If
        Next

        'Get data
        Dim soundsTable As DataTable = textFileReaders.SamplesFileToDatatable(SysFileSamples)
        Dim streamSamplesList As String() = textFileReaders.GetStreamSoundsList(SysFileSamples)

        '----------------------------------------------Resample samples and streams----------------------------------------------
        If Not quickOutput Then
            ResampleWaves(soundsTable, outPlaforms)
            If ReSampleStreams = 1 Then
                GenerateStreamFolder(streamSamplesList, outputLanguage, outPlaforms)
                ReSampleStreams = 0
                textFileWritters.UpdateMiscFile(fso.BuildPath(WorkingDirectory, "System\Misc.txt"))
            End If
            'Save Samples File
            textFileWritters.SaveSamplesFile(SysFileSamples, soundsTable)
        End If

        '----------------------------------------------Output user selected Soundbanks----------------------------------------------
        If outSoundBanks.Length > 0 Then
            OutputSoundbanks(hashCodesDictionary, outSoundBanks, streamSamplesList, outputLanguage, outPlaforms)
        End If

        If Not quickOutput Then
            Dim hashTablesBuilder As New SfxDefines
            '----------------------------------------------Create SFX Data----------------------------------------------
            Dim maxHashCode = CreateSfxDataFolder(soundsTable)

            '----------------------------------------------Create Hashtables----------------------------------------------
            hashTablesBuilder.CreateSfxDebug(hashCodesDictionary, fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Debug.h"))
            hashTablesBuilder.CreateSfxDefines(hashCodesDictionary, soundBanksDictionary, SfxLanguages, ProjectSettingsFile.MiscProps.PrefixHtSound, fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Defines.h"))
            hashTablesBuilder.CreateSfxData(fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "SFX_Data.h"), fso.BuildPath(WorkingDirectory, "TempSfxData"), maxHashCode)
        End If
    End Sub

    Private Sub CreateFolderIfRequired(destinationFilePath As String)
        If Not fso.FolderExists(destinationFilePath) Then
            MkDir(destinationFilePath)
        End If
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
        'Show mainform
        mainFrame.Show()
        'Close task form
        canCloseForm = True
        Close()
    End Sub

    Private Sub ExporterForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not canCloseForm Then
            e.Cancel = True
        End If
    End Sub
End Class
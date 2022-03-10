Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Public Class SetMaxBankSize
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly soundBankFilePath As String
    Private ReadOnly writers As New FileWriters
    Private ReadOnly textFileReaders As New FileParsers
    Private soundbankFile As SoundbankFile

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(SoundBankFile As String)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        soundBankFilePath = SoundBankFile
    End Sub

    Private Sub SetMaxBankSize_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Get file data
        soundbankFile = textFileReaders.ReadSoundBankFile(soundBankFilePath)
        'Get filename
        TextBox_BankName.Text = GetOnlyFileName(soundBankFilePath)
        'Put info in the numerics
        Numeric_PlayStationSize.Value = soundbankFile.MaxBankSizes.PlayStationSize
        Numeric_PcSize.Value = soundbankFile.MaxBankSizes.PCSize
        Numeric_GameCubeSize.Value = soundbankFile.MaxBankSizes.GameCubeSize
        Numeric_XboxSize.Value = soundbankFile.MaxBankSizes.XboxSize
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        'Update text file
        soundbankFile.MaxBankSizes.PlayStationSize = Numeric_PlayStationSize.Value
        soundbankFile.MaxBankSizes.PCSize = Numeric_PcSize.Value
        soundbankFile.MaxBankSizes.XboxSize = Numeric_XboxSize.Value
        soundbankFile.MaxBankSizes.GameCubeSize = Numeric_GameCubeSize.Value
        writers.UpdateSoundbankFile(soundbankFile, soundBankFilePath, textFileReaders)
        'Close file
        Close()
    End Sub
End Class
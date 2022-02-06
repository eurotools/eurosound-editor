Public Class SetMaxBankSize
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly soundBankFilePath As String
    Private ReadOnly writers As New FileWriters
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly soundbankNode As TreeNode

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(SoundBankFile As String, soundbankData As TreeNode)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        soundBankFilePath = SoundBankFile
        soundbankNode = soundbankData
    End Sub

    Private Sub SetMaxBankSize_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Ensure that the file exists
        If fso.FileExists(soundBankFilePath) Then
            'Get file data
            Dim soundbankFile As SoundbankFile = textFileReaders.ReadSoundBankFile(soundBankFilePath)
            'Get filename
            TextBox_BankName.Text = GetOnlyFileName(soundBankFilePath)
            'Put info in the numerics
            Numeric_PlayStationSize.Value = soundbankFile.MaxBankSizes.PlayStationSize
            Numeric_PcSize.Value = soundbankFile.MaxBankSizes.PCSize
            Numeric_GameCubeSize.Value = soundbankFile.MaxBankSizes.GameCubeSize
            Numeric_XboxSize.Value = soundbankFile.MaxBankSizes.XboxSize
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        Dim maxSizes As String() = New String() {Numeric_PlayStationSize.Value, Numeric_PcSize.Value, Numeric_XboxSize.Value, Numeric_GameCubeSize.Value}
        writers.CreateSoundbankFile(soundbankNode, textFileReaders, maxSizes)
        Close()
    End Sub
End Class
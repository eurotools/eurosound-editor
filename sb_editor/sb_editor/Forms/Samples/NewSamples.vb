Imports sb_editor.ReaderClasses

Public Class NewSamples
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private ReadOnly textFileReaders As New FileParsers()
    Private ReadOnly samplesItemsToAdd As String()

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(missingSamplesList As String())

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        samplesItemsToAdd = missingSamplesList
    End Sub

    Private Sub MissingSamples_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox_NewSamples.Items.AddRange(samplesItemsToAdd)
        'Read properties file
        Dim propsFileData = textFileReaders.ReadPropertiesFile(SysFileProperties)
        Dim sampleRatesArray As String() = propsFileData.AvailableReSampleRates.ToArray
        'Add available rates to the combobox
        If sampleRatesArray.Length > 0 Then
            ComboBox_AvailableRates.Items.AddRange(sampleRatesArray)
            ComboBox_AvailableRates.SelectedIndex = 0
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        Close()
    End Sub
End Class
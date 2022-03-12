Public Class MissingSamples
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
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
        ListBox_MissingSamples.Items.AddRange(samplesItemsToAdd)
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        Close()
    End Sub
End Class
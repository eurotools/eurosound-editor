Public Class Frm_DebugData
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly linesToPrint As String()

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(dataToPrint As String())
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        linesToPrint = dataToPrint
    End Sub

    Private Sub Frm_DebugData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If linesToPrint IsNot Nothing Then
            TextBox_DebugData.Text = String.Join(vbCrLf, linesToPrint)
        End If
    End Sub

    Public Sub AddDataToConsole(linetoAdd As String)
        If TextBox_DebugData.InvokeRequired Then
            TextBox_DebugData.Invoke(Sub() TextBox_DebugData.Text += linetoAdd & vbCrLf)
        Else
            TextBox_DebugData.Text += linetoAdd & vbCrLf
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        Close()
    End Sub
End Class
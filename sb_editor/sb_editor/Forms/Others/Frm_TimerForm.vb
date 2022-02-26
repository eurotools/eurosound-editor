Imports System.IO

Public Class Frm_TimerForm
    Private Sub Frm_TimerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Custom cursors
        Cursor = New Cursor(New MemoryStream(My.Resources.ChristmasTree))
    End Sub
End Class
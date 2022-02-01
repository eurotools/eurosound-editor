Public Class MultiSelListBox
    Inherits ListBox

    Private MouseDownOnIndex As Integer
    Private bMouseDownOnSelection As Boolean
    Private bMouseDownOutsideSelection As Boolean
    Private Const WM_LBUTTONDOWN As Integer = &H201
    Private Const WM_LBUTTONUP As Integer = &H202
    Private Const WM_MOUSEMOVE As Integer = &H200
    Private Const MK_LBUTTON As Integer = &H1&
    Private lastClickedPosition As Point
    Public Property DragDropEffectVal As DragDropEffects

    Protected Overrides Sub OnCreateControl()
        lastClickedPosition = Point.Empty
        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_LBUTTONDOWN
                Dim pt As New Point(m.LParam.ToInt32)
                MouseDownOnIndex = Me.IndexFromPoint(pt)
                If Me.SelectedItems.Count >= 1 _
                    And Me.SelectedIndices.Contains(MouseDownOnIndex) _
                        And m.WParam = MK_LBUTTON Then
                    bMouseDownOnSelection = True
                    lastClickedPosition = pt
                    Return
                Else
                    bMouseDownOutsideSelection = True
                    lastClickedPosition = Point.Empty
                    MyBase.WndProc(m)
                End If
            Case WM_MOUSEMOVE
                If bMouseDownOnSelection Then
                    Dim pt As New Point(m.LParam.ToInt32)
                    If Math.Abs(pt.X - lastClickedPosition.X) < 3 OrElse
                        Math.Abs(pt.Y - lastClickedPosition.Y) < 3 Then
                        Me.DoDragDrop(Me.SelectedItems, DragDropEffectVal)
                    End If
                End If
                bMouseDownOnSelection = False
                MyBase.WndProc(m)
            Case WM_LBUTTONUP
                Dim pt As New Point(m.LParam.ToInt32)
                If MouseDownOnIndex = Me.IndexFromPoint(pt) _
                    And m.WParam = 0 And Not bMouseDownOutsideSelection Then
                    Dim down As New Message With {
                        .HWnd = m.HWnd,
                        .Msg = WM_LBUTTONDOWN,
                        .WParam = m.WParam,
                        .LParam = m.LParam,
                        .Result = IntPtr.Zero
                    }
                    MyBase.WndProc(down)
                    lastClickedPosition = Point.Empty
                End If
                bMouseDownOutsideSelection = False
                MyBase.WndProc(m)
            Case Else
                MyBase.WndProc(m)
        End Select
    End Sub

End Class
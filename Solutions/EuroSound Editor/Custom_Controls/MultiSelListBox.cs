using System;
using System.Drawing;
using System.Windows.Forms;

public partial class MultiSelListBox : ListBox
{
    private int MouseDownOnIndex;
    private bool bMouseDownOnSelection;
    private bool bMouseDownOutsideSelection;
    private const int WM_LBUTTONDOWN = 0x201;
    private const int WM_LBUTTONUP = 0x202;
    private const int WM_MOUSEMOVE = 0x200;
    private const int MK_LBUTTON = 0x1;
    private Point lastClickedPosition;
    public DragDropEffects DragDropEffectVal { get; set; }

    protected override void OnCreateControl()
    {
        lastClickedPosition = Point.Empty;
        base.OnCreateControl();
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case WM_LBUTTONDOWN:
                Point pt = new Point(m.LParam.ToInt32());
                MouseDownOnIndex = IndexFromPoint(pt);
                if (SelectedItems.Count >= 1 & SelectedIndices.Contains(MouseDownOnIndex) & (int)m.WParam == MK_LBUTTON)
                {
                    bMouseDownOnSelection = true;
                    lastClickedPosition = pt;
                    return;
                }
                else
                {
                    bMouseDownOutsideSelection = true;
                    lastClickedPosition = Point.Empty;
                    base.WndProc(ref m);
                }

                break;
            case WM_MOUSEMOVE:
                if (bMouseDownOnSelection)
                {
                    pt = new Point(m.LParam.ToInt32());
                    if (Math.Abs(pt.X - lastClickedPosition.X) < 3 || Math.Abs(pt.Y - lastClickedPosition.Y) < 3)
                    {
                        DoDragDrop(SelectedItems, DragDropEffectVal);
                    }
                }
                bMouseDownOnSelection = false;
                base.WndProc(ref m);
                break;
            case WM_LBUTTONUP:
                pt = new Point(m.LParam.ToInt32());
                if (MouseDownOnIndex == IndexFromPoint(pt) & (int)m.WParam == 0 & !bMouseDownOutsideSelection)
                {
                    Message down = new Message()
                    {
                        HWnd = m.HWnd,
                        Msg = WM_LBUTTONDOWN,
                        WParam = m.WParam,
                        LParam = m.LParam,
                        Result = IntPtr.Zero
                    };
                    base.WndProc(ref down);
                    lastClickedPosition = Point.Empty;
                }
                bMouseDownOutsideSelection = false;
                base.WndProc(ref m);
                break;
            default:
                base.WndProc(ref m);
                break;
        }
    }

}
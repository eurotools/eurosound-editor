Imports System.Runtime.InteropServices

Public Module FlashWindow
    <StructLayout(LayoutKind.Sequential)>
    Public Structure FLASHWINFO
        Public cbSize As UInteger
        Public hwnd As IntPtr
        Public dwFlags As UInteger
        Public uCount As UInteger
        Public dwTimeout As UInteger
    End Structure

    Public Function FlashWindowAPI(ByVal handleToWindow As IntPtr) As Boolean
        Dim flashwinfo1 As New FLASHWINFO()
        flashwinfo1.cbSize = CUInt(Marshal.SizeOf(flashwinfo1))
        flashwinfo1.hwnd = handleToWindow
        flashwinfo1.dwFlags = 15
        flashwinfo1.uCount = 7
        flashwinfo1.dwTimeout = 0
        Return FlashWindowEx(flashwinfo1) = 0
    End Function

    <DllImport("user32.dll")>
    Private Function FlashWindowEx(ByRef pwfi As FLASHWINFO) As Short
    End Function

    'Fields
    Public Const FLASHW_ALL As UInteger = 3
    Public Const FLASHW_CAPTION As UInteger = 1
    Public Const FLASHW_STOP As UInteger = 0
    Public Const FLASHW_TIMER As UInteger = 4
    Public Const FLASHW_TIMERNOFG As UInteger = 12
    Public Const FLASHW_TRAY As UInteger = 2
End Module
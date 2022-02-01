Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class IniFile
    Private ReadOnly Path As String
    Private ReadOnly EXE As String = Assembly.GetExecutingAssembly().GetName().Name

    <DllImport("kernel32", CharSet:=CharSet.Unicode)>
    Private Shared Function WritePrivateProfileString(Section As String, Key As String, Value As String, FilePath As String) As Long
    End Function

    <DllImport("kernel32", CharSet:=CharSet.Unicode)>
    Private Shared Function GetPrivateProfileString(Section As String, Key As String, [Default] As String, RetVal As StringBuilder, Size As Integer, FilePath As String) As Integer
    End Function

    Public Sub New(Optional IniPath As String = Nothing)
        Path = New FileInfo(If(IniPath, EXE & ".ini")).FullName
    End Sub

    Public Function Read(Key As String, Optional Section As String = Nothing) As String
        Dim RetVal = New StringBuilder(255)
        GetPrivateProfileString(If(Section, EXE), Key, "", RetVal, 255, Path)
        Return RetVal.ToString()
    End Function

    Public Sub Write(Key As String, Value As String, Optional Section As String = Nothing)
        WritePrivateProfileString(If(Section, EXE), Key, Value, Path)
    End Sub

    Public Sub DeleteKey(Key As String, Optional Section As String = Nothing)
        Write(Key, Nothing, If(Section, EXE))
    End Sub

    Public Sub DeleteSection(Optional Section As String = Nothing)
        Write(Nothing, Nothing, If(Section, EXE))
    End Sub

    Public Function KeyExists(Key As String, Optional Section As String = Nothing) As Boolean
        Return Read(Key, Section).Length > 0
    End Function
End Class

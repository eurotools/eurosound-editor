Public Class SoundbankFile
    Public HeaderInfo As New FileHeader
    Public Dependencies As String()
    Public HashCodeLabel As String
    Public HashCode As UInteger
    Public MaxBankSizes As New MaxBankSizes
End Class

Public Class MaxBankSizes
    Friend PlayStationSize As Integer = 0
    Friend PCSize As Integer = 0
    Friend XboxSize As Integer = 0
    Friend GameCubeSize As Integer = 0
End Class

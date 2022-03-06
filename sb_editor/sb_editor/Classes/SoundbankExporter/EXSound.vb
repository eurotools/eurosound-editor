Public Class EXSound
    Public HashCode As UInteger
    Public Ducker As SByte
    Public DuckerLength As Short
    Public Flags As UShort
    Public InnerRadius As Short
    Public MasterVolume As SByte
    Public MaxDelay As Short
    Public MaxVoices As SByte
    Public MinDelay As Short
    Public OuterRadius As Short
    Public Priority As SByte
    Public ReverbSend As SByte
    Public TrackingType As SByte
    Public HasSubSfx As Boolean
    Public Samples As New List(Of EXSample)()
End Class

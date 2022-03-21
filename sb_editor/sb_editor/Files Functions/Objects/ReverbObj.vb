Public Class ReverbObj
    Public Class ReverbFile
        Public HashCode As UInteger
        Public PCReverb As New ReverbPlatform
        Public XBReverb As New ReverbPlatform
        Public GCReverb As New ReverbPlatform
    End Class

    Public Class ReverbPlatform
        Public RoomSize As Short
        Public Width As Short
        Public Damp As Short
        Public LowPassFilter As Short
        Public Filter1 As Short
        Public Filter2 As Short
    End Class
End Class

Public Class EXAudio
    Public Flags As UInteger
    Public LoopOffset As UInteger
    Public Frequency As Integer
    Public Channels As Integer
    Public sizeAligned As UInteger
    Public ParsedAudioData As Byte()
    Public Duration As UInteger
    Public DurationFloat As Single
    Public GameCubeProps As New EXADPCMInfo()
End Class

Public Class EXADPCMInfo
    Public sampleCount As UInteger
    Public nibbleCount As UInteger
    Public coefs As Short()
    Public predScale As UShort
    Public loopPredScale As UShort
    Public adpcmData As Byte()
End Class
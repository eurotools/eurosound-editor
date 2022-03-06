Public Class EXAudio
    Public Flags As UInteger
    Public Address As UInteger
    Public FilePath As String
    Public Frequency As UInteger
    Public RealSize As UInteger
    Public NumberOfChannels As UInteger
    Public Bits As UInteger
    Public LoopOffset As UInteger
    Public Duration As UInteger
    Public DspHeaderData As Byte() = New Byte() {}
    Public SampleData As Byte() = New Byte() {}
End Class

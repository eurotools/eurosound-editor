Public Class EXAudio
    Public Flags As UInteger
    Public Address As UInteger
    Public DmaBlockSize As UInteger
    Public Frequency As UInteger
    Public RealSize As UInteger
    Public NumberOfChannels As UInteger
    Public Bits As UInteger
    Public DspHeaderOffset As UInteger
    Public LoopOffset As UInteger
    Public Duration As UInteger
    Public DspHeaderData As Byte() = New Byte() {}
    Public SampleData As Byte() = New Byte() {}
End Class

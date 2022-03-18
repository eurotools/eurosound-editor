Namespace ExporterObjects
    Public Class EXAudio
        Public Flags As UInteger
        Public Address As UInteger
        Public Frequency As UInteger
        Public RealSize As UInteger
        Public NumberOfChannels As UInteger
        Public Bits As UInteger
        Public LoopOffset As UInteger
        Public Duration As UInteger
        Public DspHeaderData As Byte() = New Byte() {}
        Public SampleData As Byte() = New Byte() {}
        Public FileRef As Integer
    End Class
End Namespace
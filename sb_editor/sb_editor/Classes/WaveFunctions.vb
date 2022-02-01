Imports NAudio.Wave

Friend Class WaveFunctions
    Friend Function ReadSampleChunk(waveReader As WaveFileReader) As Integer()
        Dim loopInfo = New Integer(2) {} '0 = Loop Yes/No, 1 = Start Position, 2 = End Position

        'Read Sample Chunck
        Dim smp As RiffChunk = waveReader.ExtraChunks.FirstOrDefault(Function(ec) StrComp(ec.IdentifierAsString, "smpl") = 0)
        If smp IsNot Nothing Then
            Dim chunkData As Byte() = waveReader.GetChunkData(smp)
            Dim midiNote = BitConverter.ToInt32(chunkData, 12)
            Dim numberOfSamples = BitConverter.ToInt32(chunkData, 28)
            Dim offset = 36

            For n = 0 To numberOfSamples - 1
                'Read Chunck info
                Dim cuePointId = BitConverter.ToInt32(chunkData, offset)
                Dim type = BitConverter.ToInt32(chunkData, offset + 4) ' 0 = loop forward, 1 = alternating loop, 2 = reverse
                Dim start = BitConverter.ToInt32(chunkData, offset + 8)
                Dim [end] = BitConverter.ToInt32(chunkData, offset + 12)
                Dim fraction = BitConverter.ToInt32(chunkData, offset + 16)
                Dim playCount = BitConverter.ToInt32(chunkData, offset + 20)
                offset += 24

                'Save Data
                loopInfo(0) = 1
                loopInfo(1) = start
                loopInfo(2) = [end]
            Next
        End If
        Return loopInfo
    End Function
End Class


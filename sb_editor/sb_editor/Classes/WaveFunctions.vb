Imports System.IO
Imports System.Text
Imports NAudio.Wave

Friend Class WaveFunctions
    Friend Function ReadSampleChunk(waveReader As WaveFileReader) As Integer()
        Dim loopInfo = New Integer(3) {} '0 = Loop Yes/No, 1 = Start Position, 2 = End Position 3 = MidiNote

        'Read Sample Chunck
        Dim smp As RiffChunk = waveReader.ExtraChunks.FirstOrDefault(Function(ec) ec.IdentifierAsString.Equals("smpl", StringComparison.OrdinalIgnoreCase))
        If smp IsNot Nothing Then
            Dim chunkData As Byte() = waveReader.GetChunkData(smp)
            Dim midiNote = BitConverter.ToInt32(chunkData, 12)
            Dim numberOfSamples = BitConverter.ToInt32(chunkData, 28)
            Dim offset = 36

            For n = 0 To numberOfSamples - 1
                'Read Chunck info
                Dim cuePointId = BitConverter.ToInt32(chunkData, offset)
                Dim type = BitConverter.ToInt32(chunkData, offset + 4) '0 = loop forward, 1 = alternating loop, 2 = reverse
                Dim loopStart = BitConverter.ToInt32(chunkData, offset + 8)
                Dim loopEnd = BitConverter.ToInt32(chunkData, offset + 12)
                Dim fraction = BitConverter.ToInt32(chunkData, offset + 16)
                Dim playCount = BitConverter.ToInt32(chunkData, offset + 20)
                offset += 24

                'Save Data
                loopInfo(0) = 1
                loopInfo(1) = loopStart
                loopInfo(2) = loopEnd
                loopInfo(3) = midiNote
            Next
        End If
        Return loopInfo
    End Function

    Friend Sub AddChunksToAif(aifFilePath As String, startPosition As Integer, endPosition As Integer, midiNote As Integer)
        Using binWriter As New BinaryWriter(File.Open(aifFilePath, FileMode.Append, FileAccess.Write))
            'Add Instrument chunk
            binWriter.Write(Encoding.ASCII.GetBytes("INST"))
            binWriter.Write(ESUtils.BytesFunctions.FlipInt32(20, True))
            binWriter.Write(CByte(midiNote))
            binWriter.Write(CByte(0))
            binWriter.Write(CByte(0))
            binWriter.Write(CByte(127))
            binWriter.Write(CByte(0))
            binWriter.Write(CByte(127))
            binWriter.Write(CShort(0))
            binWriter.Write(ESUtils.BytesFunctions.FlipShort(1, True))
            binWriter.Write(CShort(0))
            binWriter.Write(ESUtils.BytesFunctions.FlipShort(1, True))
            binWriter.Write(CShort(0))
            binWriter.Write(CShort(0))
            binWriter.Write(CShort(0))
            'Add Markers
            binWriter.Write(Encoding.ASCII.GetBytes("MARK"))
            binWriter.Write(ESUtils.BytesFunctions.FlipInt32(34, True))
            binWriter.Write(ESUtils.BytesFunctions.FlipShort(2, True))
            For markerIndex As Integer = 0 To 1
                binWriter.Write(ESUtils.BytesFunctions.FlipShort(markerIndex, True))
                If markerIndex = 0 Then
                    binWriter.Write(ESUtils.BytesFunctions.FlipInt32(startPosition, True))
                    binWriter.Write(CByte(8))
                    binWriter.Write(Encoding.ASCII.GetBytes("beg loop"))
                    binWriter.Write(CByte(0))
                Else
                    binWriter.Write(ESUtils.BytesFunctions.FlipInt32(endPosition, True))
                    binWriter.Write(CByte(8))
                    binWriter.Write(Encoding.ASCII.GetBytes("end loop"))
                    binWriter.Write(CByte(0))
                End If
            Next
            'Add empty
            For index As Integer = 0 To 17
                binWriter.Write(CByte(0))
            Next
        End Using
    End Sub

    Friend Function ConvertByteArrayToShortArray(PCMData As Byte()) As Short()
        Dim samplesShort As Short() = New Short(PCMData.Length / 2 - 1) {}
        Dim sourceWaveBuffer As New WaveBuffer(PCMData)
        For i As Integer = 0 To samplesShort.Length - 1
            samplesShort(i) = sourceWaveBuffer.ShortBuffer(i)
        Next
        Return samplesShort
    End Function
End Class


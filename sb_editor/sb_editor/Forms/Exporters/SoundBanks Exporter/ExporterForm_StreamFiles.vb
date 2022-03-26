Imports System.IO
Imports System.Text

Partial Public Class ExporterForm
    '*===============================================================================================
    '* BINARY FILE FUNCTIONS
    '*===============================================================================================
    Public Sub BuildTemporalFile(filesToEncode As List(Of String), outputPlatform As String, outputLanguage As String, binaryFilePath As String, lutFilePath As String, debugFilePath As String, isBigEndian As Boolean)
        'Reset progress bar
        Invoke(Sub() ProgressBar1.Value = 0)
        'Create a new binary writer for the binary file
        Dim StartOffsets As New Queue(Of UInteger)
        Using binaryWriter As New BinaryWriter(File.Open(binaryFilePath, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII)
            'Debug File
            Using outputFile As New StreamWriter(debugFilePath)
                Dim fileIndex As Integer = 0
                For Each filePath As String In filesToEncode
                    'Get files path
                    Dim adpcmFile As String = Path.ChangeExtension(filePath, ".ssd")
                    Dim markerFile As String = Path.ChangeExtension(filePath, ".smf")

                    'Report progress and update title bar
                    BackgroundWorker.ReportProgress(Decimal.Divide(fileIndex, filesToEncode.Count) * 100.0, "Binding " & outputLanguage & " Audio Stream Data " & adpcmFile & " For " & outputPlatform)

                    'Ensure that the adpcm file exists
                    If File.Exists(adpcmFile) AndAlso File.Exists(markerFile) Then
                        'Offset to write in look-up table
                        Dim headerStart As Long = binaryWriter.BaseStream.Position
                        StartOffsets.Enqueue(headerStart)
                        'Read files binary data
                        Dim markersFileData As Byte() = File.ReadAllBytes(markerFile)
                        Dim adpcmData As Byte() = File.ReadAllBytes(adpcmFile)
                        'Marker size
                        binaryWriter.Write(ESUtils.BytesFunctions.FlipInt32(markersFileData.Length, isBigEndian))
                        'Save position for the audio offset
                        Dim prevPosition As UInteger = binaryWriter.BaseStream.Position
                        'Audio Offset
                        binaryWriter.Write(0)
                        'Audio Size
                        binaryWriter.Write(ESUtils.BytesFunctions.FlipInt32(adpcmData.Length, isBigEndian))
                        'Marker Data
                        binaryWriter.Write(markersFileData)
                        'Alignment
                        Dim block As Byte() = New Byte(&H800 - 5) {}
                        binaryWriter.Write(block)
                        BinAlign(binaryWriter, &H800)
                        'Write adpcm data
                        Dim audioStartOffset As UInteger = binaryWriter.BaseStream.Position
                        binaryWriter.Write(adpcmData)
                        'Alignment
                        If fileIndex < filesToEncode.Count - 1 Then
                            binaryWriter.Write(block)
                            BinAlign(binaryWriter, &H800)
                        End If
                        'Save current pos
                        Dim lastPosition As UInteger = binaryWriter.BaseStream.Position
                        'Go Back to write audio start pos
                        binaryWriter.Seek(prevPosition, SeekOrigin.Begin)
                        binaryWriter.Write(ESUtils.BytesFunctions.FlipInt32(audioStartOffset, isBigEndian))
                        'Return to current pos
                        binaryWriter.Seek(lastPosition, SeekOrigin.Begin)

                        'Print Debug Data 
                        outputFile.WriteLine("------------------Stream " & fileIndex & "------------------")
                        outputFile.WriteLine("HeaderStart = " & headerStart)
                        outputFile.WriteLine("DataStart = " & audioStartOffset)
                        outputFile.WriteLine("")
                        outputFile.WriteLine("MarkerSize = " & markersFileData.Length)
                        outputFile.WriteLine("SampleDataStart = " & audioStartOffset)
                        outputFile.WriteLine("SampleSize = " & adpcmData.Length)
                        outputFile.WriteLine("")
                    End If

                    'Update counter 
                    fileIndex += 1
                Next
            End Using

            'Close file
            binaryWriter.Close()
        End Using

        'Ensure that we have items stored in the queue
        If StartOffsets.Count > 0 Then
            'Create a new binary writer for the lut file
            Using binaryWriter As New BinaryWriter(File.Open(lutFilePath, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII)
                'Wirte all start offsets
                Do
                    binaryWriter.Write(ESUtils.BytesFunctions.FlipUInt32(StartOffsets.Dequeue, isBigEndian))
                Loop While StartOffsets.Count > 0

                'Close file
                binaryWriter.Close()
            End Using
        End If
    End Sub

    Private Sub BinAlign(BWriter As BinaryWriter, alignment As Integer)
        BWriter.Seek((-BWriter.BaseStream.Position Mod alignment + alignment) Mod alignment, SeekOrigin.Current)
    End Sub
End Class

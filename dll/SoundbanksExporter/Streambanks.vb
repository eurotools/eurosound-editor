Imports System.IO
Imports System.Text

Public Class Streambanks
    Public Sub BuildStreamFile(workingDirectory As String, destinationPlatform As String, sampleRates As Dictionary(Of String, UInteger))
        'Get Wave files to include
        Dim filesList As String(,) = GetFileList(Path.Combine(workingDirectory, "System", "Samples.txt"))

        'Export files
        'ResampleFiles(filesList, workingDirectory, destinationPlatform, sampleRates)

        'Move files
        'BuildStreamsFolder(workingDirectory, destinationPlatform, filesList)

        'Build temporal file
        BuildTemporalFile(filesList, Path.Combine(workingDirectory, destinationPlatform & "_Streams", "English"), workingDirectory, destinationPlatform)
    End Sub

    Private Function GetFileList(samplesFilePath As String) As String(,)
        'Create a datatable with the samples info
        Dim dataTable As DataTable = GetFilesList(samplesFilePath)

        'Select 
        Dim results As DataRow() = dataTable.Select("Streamed = True")
        Dim totalStreamedSamples = results.Length - 1

        'Create final list
        Dim filesToInclude = New String(totalStreamedSamples, 1) {}

        'Iterate over all select results, we don't need to sort :)
        For index As Integer = 0 To totalStreamedSamples
            'Wave full path
            filesToInclude(index, 0) = results(index).ItemArray(0)
            'Sample rate
            filesToInclude(index, 1) = results(index).ItemArray(2)
        Next

        'Clear table
        dataTable.Clear()

        'Return data
        Return filesToInclude
    End Function

    Private Sub BuildStreamsFolder(workingDirectory As String, destPlatform As String, fileList As String(,))
        'Output to save converted data
        Dim BaseOutputFolder = Path.Combine(workingDirectory, destPlatform & "_Streams", "English")

        'Output to save converted data
        Dim StreamsFolder = Path.Combine(workingDirectory, destPlatform & "_Software_adpcm")

        'Master folder
        Dim MasterFolder = Path.Combine(workingDirectory, "Master")

        'Markers tool path
        Dim markersTool = Path.Combine("SystemFiles", "ExMrkTool.exe")

        'Create directory if not exists
        If Dir$(BaseOutputFolder, FileAttribute.Directory) = "" Then
            MkDir(BaseOutputFolder)
        End If

        'Move files to stream folder
        For index As Integer = 0 To fileList.GetLength(0) - 1
            Dim filePath As String = StreamsFolder & fileList(index, 0)
            Dim imaFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) & ".ssp")
            If Dir$(imaFilePath) IsNot "" Then
                'ADPCM File
                Dim destinationFolder = Path.Combine(BaseOutputFolder, "STR_" & index & ".ssd")
                FileCopy(imaFilePath, destinationFolder)

                'Marker File
                Dim MasterWaveFilePath = MasterFolder & fileList(index, 0)
                Dim MasterMarkerFilePath = Path.Combine(Path.GetDirectoryName(MasterWaveFilePath), Path.GetFileNameWithoutExtension(MasterWaveFilePath) & ".mrk")
                If Dir$(MasterMarkerFilePath) IsNot "" Then
                    Shell(markersTool & " STREAMS """ & destinationFolder & """ """ & MasterMarkerFilePath & """ """ & BaseOutputFolder & """ PC " & 100, AppWinStyle.Hide, True)
                End If
            End If
        Next
    End Sub

    Private Sub BuildTemporalFile(fileList As String(,), StreamsFolder As String, workingDirectory As String, destinationPlatform As String)
        Dim tempOutputPath As String = Path.Combine(workingDirectory, "TempOutputFolder", destinationPlatform, "English", "Streams")
        Dim tempStreamDataBin As String = Path.Combine(tempOutputPath, "STREAMS.bin")
        Dim tempStreamDataLut As String = Path.Combine(tempOutputPath, "STREAMS.lut")

        'Create directory if not exists
        If Dir$(tempOutputPath, FileAttribute.Directory) = "" Then
            MkDir(tempOutputPath)
        End If

        'Start offsets list
        Dim StartOffsets As New Queue(Of UInteger)

        'Get files count
        Dim filesCount = fileList.GetLength(0) - 1

        'Create a new binary writer for the binary file
        Using binaryWriter As New BinaryWriter(File.Open(tempStreamDataBin, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII)
            'Convert audio to the destination platform rate
            For index As Integer = 0 To filesCount
                Dim adpcmFile = Path.Combine(StreamsFolder, "STR_" & index & ".ssd")
                Dim markerFile = Path.Combine(StreamsFolder, "STR_" & index & ".smf")

                'Offset to write in look-up table
                StartOffsets.Enqueue(binaryWriter.BaseStream.Position)

                'Read files binary data
                Dim markersFileData As Byte() = File.ReadAllBytes(markerFile)
                Dim adpcmData As Byte() = File.ReadAllBytes(adpcmFile)

                'Marker size
                binaryWriter.Write(markersFileData.Length)
                'Save position for the audio offset
                Dim prevPosition As UInteger = binaryWriter.BaseStream.Position
                'Audio Offset
                binaryWriter.Write(0)
                'Audio Size
                binaryWriter.Write(adpcmData.Length)
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
                If index < filesCount Then
                    binaryWriter.Write(block)
                    BinAlign(binaryWriter, &H800)
                End If

                'Save current pos
                Dim lastPosition As UInteger = binaryWriter.BaseStream.Position

                'Go Back to write audio start pos
                binaryWriter.Seek(prevPosition, SeekOrigin.Begin)

                'Write offset
                binaryWriter.Write(audioStartOffset)

                'Return to current pos
                binaryWriter.Seek(lastPosition, SeekOrigin.Begin)
            Next
            'Close file
            binaryWriter.Close()
        End Using

        'Create a new binary writer for the lut file
        Using binaryWriter As New BinaryWriter(File.Open(tempStreamDataLut, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII)
            Do
                binaryWriter.Write(StartOffsets.Dequeue)
            Loop While StartOffsets.Count > 0
            binaryWriter.Close()
        End Using
    End Sub

    Private Sub BinAlign(BWriter As BinaryWriter, alignment As Integer)
        BWriter.Seek((-BWriter.BaseStream.Position Mod alignment + alignment) Mod alignment, SeekOrigin.Current)
    End Sub

    '*===============================================================================================
    '* RESAMPLE FUNCTIONS
    '*===============================================================================================
    Private Sub ResampleFiles(fileList As String(,), workingDirectory As String, destPlatform As String, sampleRates As Dictionary(Of String, UInteger))
        'Get Sox File path
        Dim soxPath = Path.Combine("SystemFiles", "Sox", "Sox.exe")

        'Ensure that the file exists
        If Dir$(soxPath) IsNot "" Then
            'Output to save converted data
            Dim BaseOutputFolder = Path.Combine(workingDirectory, destPlatform & "_Software_adpcm")

            'Convert audio to the destination platform rate
            For index As Integer = 0 To fileList.GetLength(0) - 1
                Dim waveFilePath As String = workingDirectory & "\Master\" & fileList(index, 0)
                Dim fullOutputFolder = BaseOutputFolder & Path.GetDirectoryName(fileList(index, 0))
                Dim outFilepath As String = Path.Combine(fullOutputFolder, Path.GetFileNameWithoutExtension(waveFilePath))
                Dim outFilePathWav As String = outFilepath & ".smd"
                Dim outFilePathIma As String = outFilepath & ".ssp"

                'Create directory if not exists
                If Dir$(fullOutputFolder, FileAttribute.Directory) = "" Then
                    MkDir(fullOutputFolder)
                End If

                'Resample Wave file
                Dim sampleRate As Integer = sampleRates(fileList(index, 1))
                Shell(soxPath & " """ & waveFilePath & """ -t raw -r " & sampleRate & " -b 16 -c 1 -e signed """ & outFilePathWav & """", AppWinStyle.Hide, True)
                'Wave to ima
                Shell(soxPath & " -t raw -r " & sampleRate & " -b 16 -c 1 -e signed """ & outFilePathWav & """ -t ima """ & outFilePathIma & """", AppWinStyle.Hide, True)
            Next
        End If
    End Sub

    '*===============================================================================================
    '* DATASET FUNCTIONS
    '*===============================================================================================
    Private Function GetFilesList(samplesFilePath As String) As DataTable
        'Create a new datatable
        Dim dataTable As New DataTable

        'Add columns
        dataTable.Columns.Add("FileName")
        dataTable.Columns.Add("Streamed")
        dataTable.Columns.Add("Rate")

        'Open file and read it
        Dim currentLine As String
        FileOpen(1, samplesFilePath, OpenMode.Input, OpenAccess.Read, OpenShare.LockWrite)
        Do Until EOF(1)
            'Read text file
            currentLine = LineInput(1)
            'Read Available Sample Rates
            If StrComp(currentLine, "#AvailableSamples", CompareMethod.Text) = 0 Then
                'Read line
                currentLine = LineInput(1)
                Dim SamplesCount As Integer = currentLine

                'Read samples table
                For i As Integer = 0 To 5
                    Dim itemsCount As Integer = 0
                    Do
                        'Continue Reading
                        currentLine = LineInput(1)

                        'Read content
                        If StrComp(currentLine, "#END", CompareMethod.Text) <> 0 Then
                            'Add item to listview
                            If i = 0 Then
                                dataTable.Rows.Add(currentLine, "")
                            ElseIf i = 1 Then
                                dataTable.Rows(itemsCount).Item(2) = currentLine
                            ElseIf i = 5 Then
                                dataTable.Rows(itemsCount).Item(1) = currentLine
                            End If

                            'Update counter
                            itemsCount += 1
                        Else
                            'Exit loop
                            Exit Do
                        End If
                    Loop While itemsCount < SamplesCount
                Next
            End If
        Loop

        'Read misc properties block
        FileClose(1)

        'Return table
        Return dataTable
    End Function
End Class

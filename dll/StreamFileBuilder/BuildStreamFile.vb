Imports System.IO
Imports System.Text

Module BuildStreamFile
    '*===============================================================================================
    '* GLOBAL VARIABLES
    '*===============================================================================================
    Private ReadOnly prefix As String = "--->"

    '*===============================================================================================
    '* MAIN METHOD
    '*===============================================================================================
    Sub BuildFinalFile(streamFolder As String, outputFolder As String, platform As Integer, numOfElements As Integer)
        'Check that the specified folder exists
        If Dir$(streamFolder) IsNot "" Then
            'Inform User
            Console.WriteLine(prefix & "Directory: """ & streamFolder & """ OK")

            'Check output platform
            Select Case platform
                Case Enumerables.Platform.PC
                    'Inform User
                    Console.WriteLine(prefix & "Output Platform: PC")
                    'Create MusX File
                    MergeFiles(streamFolder, outputFolder, Enumerables.Platform.PC, numOfElements)
                Case Enumerables.Platform.PS2
                    'Inform User
                    Console.WriteLine(prefix & "Output Platform: PS2")
                    'Create MusX File
                    MergeFiles(streamFolder, outputFolder, Enumerables.Platform.PS2, numOfElements)
                Case Enumerables.Platform.GC
                    'Inform User
                    Console.WriteLine(prefix & "Output Platform: GC")
                    'Create MusX File
                    MergeFiles(streamFolder, outputFolder, Enumerables.Platform.GC, numOfElements)
                Case Enumerables.Platform.XB
                    'Inform User
                    Console.WriteLine(prefix & "Output Platform: XB")
                    'Create MusX File
                    MergeFiles(streamFolder, outputFolder, Enumerables.Platform.XB, numOfElements)
                Case Else
                    'Inform User
                    Console.WriteLine(prefix & "Unknown Output Platform")
            End Select
        Else
            'Inform User
            Console.WriteLine(prefix & "Directory: """ & streamFolder & """ Not Found")
        End If
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Sub MergeFiles(StreamsFolder As String, OutFolder As String, OutPlatform As Platform, numberOfStreams As Integer)
        'Inform User
        Console.WriteLine(prefix & "-------------Output Started-------------")
        Console.WriteLine(prefix & "Getting SMF files...")
        Console.WriteLine(prefix & "Getting SSF files...")

        'List
        Dim smfFilesList As New List(Of String)
        Dim ssdFilesList As New List(Of String)

        'Get files
        For index As Integer = 0 To numberOfStreams - 1
            'Get full path
            Dim smfFilePath = Path.Combine(StreamsFolder, "STR_" & index & ".SMF")
            Dim ssdFilePath = Path.Combine(StreamsFolder, "STR_" & index & ".SSD")

            'Ensure that the SMF file exists
            If Dir$(smfFilePath) IsNot "" Then
                smfFilesList.Add(smfFilePath)
            End If

            'Ensure that the SSD file exists
            If Dir$(ssdFilePath) IsNot "" Then
                ssdFilesList.Add(ssdFilePath)
            End If
        Next

        'Parse list to array
        Dim smfFiles As String() = smfFilesList.ToArray
        Dim ssdFiles As String() = ssdFilesList.ToArray

        'Ensure that we have all required files
        If smfFiles.Length = ssdFiles.Length Then
            'Get Full path
            Dim fullDirPath = Path.Combine(OutFolder, "Binary", "_bin_" & OutPlatform.ToString(), "_Eng")
            Dim FileFullPath = Path.Combine(fullDirPath, "HC00FFFF.SFX")

            'Create Dir if not exists        
            If Dir$(fullDirPath) IsNot "" Then
                Console.WriteLine(prefix & "The output directory does not exists: " & fullDirPath)
                Console.WriteLine(prefix & "Creating output directory...")
                MkDir(fullDirPath)
            End If

            'Start output
            BuildMusX(FileFullPath, smfFiles, ssdFiles)
        Else
            'Inform User
            Console.WriteLine(prefix & "-------------Output Aborted-----------")
            Console.WriteLine(prefix & "Reason: Missing Files")
            Console.WriteLine(prefix & "SMF Files Count: " & smfFiles.Length)
            Console.WriteLine(prefix & "SSD Files Count: " & ssdFiles.Length)
        End If
    End Sub

    '*===============================================================================================
    '* SFX FILE FUNCTIONS
    '*===============================================================================================
    Private Sub BuildMusX(filePath As String, SmfFiles As String(), SsdFiles As String())
        'Section start offsets
        Dim FileStart1 As UInteger = &H800
        Dim FileStart2 As UInteger = &H1000

        'File section variables
        Dim FileLength1, FileLength2, FileFullLength As UInteger

        'Start offsets list
        Dim StartOffsets As New Queue(Of UInteger)

        'Create a new binary writer
        Using binaryWriter As New BinaryWriter(File.Open(filePath, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII)
            '--------------------------------------------------[Header]--------------------------------------------------
            '--magic[magic value]--
            binaryWriter.Write(Encoding.ASCII.GetBytes("MUSX"))

            '--hashc[Hashcode for the current soundbank without the section prefix]--
            binaryWriter.Write(&HFFFF)

            '--offst[Constant offset to the next section,]--
            binaryWriter.Write(&HC9)

            '--fulls[Size of the whole file, in bytes. Unused. ]--
            binaryWriter.Write(0)

            '--------------------------------------------------[SECTIONS]--------------------------------------------------
            '--File start 1[an offset that points to the stream look-up file details, always 0x800]--
            binaryWriter.Write(FileStart1)

            '--File length 1[size of the first section, in bytes]--
            binaryWriter.Write(0)

            '--File start 2[offset to the second section with the sample data]--
            binaryWriter.Write(FileStart2)

            '--File length 2[size of the second section, in bytes]--
            binaryWriter.Write(0)

            '--File start 3[unused And uses the same sample data offset as dummy for some reason]--
            binaryWriter.Write(0)

            '--File length 3[unused And set to zero]--
            binaryWriter.Write(0)

            '--------------------------------------------------[LOOK UP TABLE]--------------------------------------------------
            'Go to start pos
            binaryWriter.Seek(FileStart1, SeekOrigin.Begin)

            'Write "placeholder" offsets
            For file = 0 To SsdFiles.Length - 1
                binaryWriter.Write(0)
            Next

            'Get file section 1 length
            FileLength1 = binaryWriter.BaseStream.Position - FileStart1

            '--------------------------------------------------[Write Section 2]--------------------------------------------------
            'Go to start pos
            binaryWriter.Seek(FileStart2, SeekOrigin.Begin)

            For index = 0 To SsdFiles.Length - 1

                'Offset to write in look-up table
                StartOffsets.Enqueue(binaryWriter.BaseStream.Position - FileStart2)

                'Read files binary data
                Dim markersFileData As Byte() = File.ReadAllBytes(SmfFiles(index))
                Dim adpcmData As Byte() = File.ReadAllBytes(SsdFiles(index))

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
                Dim block As Byte() = New Byte(FileStart1 - 5) {}
                binaryWriter.Write(block)
                BinAlign(binaryWriter, FileStart1)

                'Write adpcm data
                Dim audioStartOffset As UInteger = binaryWriter.BaseStream.Position - FileStart2
                binaryWriter.Write(adpcmData)

                'Alignment
                If index < SsdFiles.Length - 1 Then
                    binaryWriter.Write(block)
                    BinAlign(binaryWriter, FileStart1)
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
            'Get section length
            FileLength2 = binaryWriter.BaseStream.Position - FileStart2

            'Get file length
            FileFullLength = binaryWriter.BaseStream.Position

            '--------------------------------------------------[Write Final Offsets]--------------------------------------------------
            'File Full Size
            binaryWriter.BaseStream.Seek(&HC, SeekOrigin.Begin)
            binaryWriter.Write(FileFullLength)

            'File length 1
            binaryWriter.BaseStream.Seek(&H14, SeekOrigin.Begin)
            binaryWriter.Write(FileLength1)

            'File length 2
            binaryWriter.BaseStream.Seek(&H1C, SeekOrigin.Begin)
            binaryWriter.Write(FileLength2)

            'Write start offsets for the stored streams
            binaryWriter.Seek(FileStart1, SeekOrigin.Begin)
            Do
                binaryWriter.Write(StartOffsets.Dequeue)
            Loop While StartOffsets.Count > 0

            'Close writer
            binaryWriter.Close()
        End Using
    End Sub

    Private Sub BinAlign(BWriter As BinaryWriter, alignment As Integer)
        BWriter.Seek((-BWriter.BaseStream.Position Mod alignment + alignment) Mod alignment, SeekOrigin.Current)
    End Sub
End Module

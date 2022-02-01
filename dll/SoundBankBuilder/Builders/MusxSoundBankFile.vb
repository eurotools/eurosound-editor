Imports System.IO
Imports System.Text

Public Class MusxSoundBankFile
    '*===============================================================================================
    '* MAIN METHOD
    '*===============================================================================================
    Public Sub BuildFinalFile(soundbanksFolder As String, outputFolder As String, platform As Integer, soundbanksFile As Integer)
        'Check that the specified folder exists
        If Dir$(soundbanksFolder) IsNot "" Then
            'Check output platform
            Select Case platform
                Case Enumerables.Platform.PC
                    'Create MusX File
                    MergeFiles(soundbanksFolder, outputFolder, Enumerables.Platform.PC, soundbanksFile, False)
                Case Enumerables.Platform.PS2
                    'Create MusX File
                    MergeFiles(soundbanksFolder, outputFolder, Enumerables.Platform.PS2, soundbanksFile, False)
                Case Enumerables.Platform.GC
                    'Create MusX File
                    MergeFiles(soundbanksFolder, outputFolder, Enumerables.Platform.GC, soundbanksFile, True)
                Case Enumerables.Platform.XB
                    'Create MusX File
                    MergeFiles(soundbanksFolder, outputFolder, Enumerables.Platform.XB, soundbanksFile, False)
            End Select
        End If
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Sub MergeFiles(SoundbanksFolder As String, OutFolder As String, OutPlatform As Platform, fileName As Integer, bigEndian As Boolean)
        'Get full paths
        Dim sfxHashCodesFile As String = Path.Combine(SoundbanksFolder, fileName & ".sfx")
        Dim SampleInfoFile As String = Path.Combine(SoundbanksFolder, fileName & ".sif")
        Dim SoundbankDataFile As String = Path.Combine(SoundbanksFolder, fileName & ".sbf")
        Dim GamecumeDataFile As String = Path.Combine(SoundbanksFolder, fileName & ".ssf")

        'Ensure that the SFX File exists
        If Dir$(sfxHashCodesFile) IsNot "" Then
            'Ensure that the SIF File exists
            If Dir$(SampleInfoFile) IsNot "" Then
                'Ensure that the SBF File exists
                If Dir$(SoundbankDataFile) IsNot "" Then
                    'Get Full path
                    Dim fullDirPath = Path.Combine(OutFolder, "Binary", "_bin_" & OutPlatform.ToString(), "_Eng")
                    Dim FileFullPath = Path.Combine(fullDirPath, "HC" & Hex(fileName).PadLeft(6, "0"c) & ".SFX")

                    'Create Dir if not exists 
                    If Dir$(fullDirPath) IsNot "" Then
                        MkDir(fullDirPath)
                    End If

                    'Start output
                    BuildMusX(FileFullPath, fileName, sfxHashCodesFile, SampleInfoFile, GamecumeDataFile, SoundbankDataFile, bigEndian)
                End If
            End If
        End If
    End Sub

    '*===============================================================================================
    '* SFX FILE FUNCTIONS
    '*===============================================================================================
    Private Sub BuildMusX(filePath As String, fileHashCode As UInteger, HashCodesFile As String, SamplePoolFile As String, SpecialSectionFile As String, SoundBankData As String, bigEndian As Boolean)
        'Align offsets
        Dim sectionAlign = &H800

        'File section variables
        Dim SfxStart, SampleInfoStart, SpecialSectionStart, SampleDataStart, fileFullSize As UInteger

        'Create a new binary writer
        Using binaryWriter As New BinaryWriter(File.Open(filePath, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII)
            '--------------------------------------------------[Header]--------------------------------------------------
            '--magic[magic value]--
            binaryWriter.Write(Encoding.ASCII.GetBytes("MUSX"))

            '--hashc[Hashcode for the current soundbank without the section prefix]--
            binaryWriter.Write(fileHashCode)

            '--offst[Constant offset to the next section,]--
            binaryWriter.Write(&HC9)

            '--fulls[Size of the whole file, in bytes. Unused. ]--
            binaryWriter.Write(0)

            '--------------------------------------------------[File Sections]--------------------------------------------------
            '--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--
            binaryWriter.Write(0)

            '--sfxlength[size of the first section, in bytes]--
            binaryWriter.Write(0)

            '--sampleinfostart[offset to the second section where the sample properties are stored]--
            binaryWriter.Write(0)

            '--sampleinfolen[size of the second section, in bytes]--
            binaryWriter.Write(0)

            '--specialsampleinfostart[used for gamecube adpcm struct info]--
            binaryWriter.Write(0)

            '--specialsampleinfolen[Size of the block, in bytes]--
            binaryWriter.Write(0)

            '--sampledatastart[Offset that points to the beginning of the PCM data, where sound Is actually stored]--
            binaryWriter.Write(0)

            '--sampledatalen[Size of the block, in bytes]--
            binaryWriter.Write(0)

            '--------------------------------------------------[Files Content]--------------------------------------------------
            Dim sfxFile As Byte() = File.ReadAllBytes(HashCodesFile)
            Dim sifFile As Byte() = File.ReadAllBytes(SamplePoolFile)
            Dim sbFile As Byte() = File.ReadAllBytes(SoundBankData)
            Dim ssFile As Byte() = New Byte() {}
            If Dir$(SpecialSectionFile) IsNot "" Then
                ssFile = File.ReadAllBytes(SpecialSectionFile)
            End If

            'Write Hashcodes SFX Section
            BinAlign(binaryWriter, sectionAlign)
            SfxStart = binaryWriter.BaseStream.Position
            binaryWriter.Write(sfxFile)

            'Write SampleInfo SFX Section
            BinAlign(binaryWriter, sectionAlign)
            SampleInfoStart = binaryWriter.BaseStream.Position
            binaryWriter.Write(sifFile)

            'Write special section
            BinAlign(binaryWriter, sectionAlign)
            SpecialSectionStart = binaryWriter.BaseStream.Position
            binaryWriter.Write(ssFile)

            'Write Sample data SFX Section
            If ssFile.Length > 0 Then
                BinAlign(binaryWriter, sectionAlign)
            End If
            SampleDataStart = binaryWriter.BaseStream.Position
            binaryWriter.Write(sbFile)

            'Get file size
            fileFullSize = binaryWriter.BaseStream.Position

            '--------------------------------------------------[Final offsets]--------------------------------------------------
            '--Size of the whole file--
            binaryWriter.BaseStream.Seek(&HC, SeekOrigin.Begin)
            binaryWriter.Write(fileFullSize)

            '--SFX Length--
            binaryWriter.BaseStream.Seek(&H10, SeekOrigin.Begin)
            binaryWriter.Write(FlipUInt32(SfxStart, bigEndian))
            binaryWriter.Write(FlipUInt32(sfxFile.Length, bigEndian))

            '--Sample info start--
            binaryWriter.BaseStream.Seek(&H18, SeekOrigin.Begin)
            binaryWriter.Write(FlipUInt32(SampleInfoStart, bigEndian))
            binaryWriter.Write(FlipUInt32(sifFile.Length, bigEndian))

            '--Special sample info start--
            binaryWriter.BaseStream.Seek(&H20, SeekOrigin.Begin)
            binaryWriter.Write(FlipUInt32(SpecialSectionStart, bigEndian))
            binaryWriter.Write(FlipUInt32(ssFile.Length, bigEndian))

            '--Sample Data Start--
            binaryWriter.BaseStream.Seek(&H28, SeekOrigin.Begin)
            binaryWriter.Write(FlipUInt32(SampleDataStart, bigEndian))
            binaryWriter.Write(FlipUInt32(sbFile.Length, bigEndian))

            'Close writer
            binaryWriter.Close()
        End Using
    End Sub
End Class

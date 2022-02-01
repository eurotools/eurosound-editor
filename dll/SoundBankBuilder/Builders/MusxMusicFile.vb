Imports System.IO
Imports System.Text
Imports Scripting

Public Class MusxMusicFile
    Private ReadOnly fso As New FileSystemObject
    '*===============================================================================================
    '* MAIN METHOD
    '*===============================================================================================
    Public Sub BuildFinalFile(soundMarkerFile As String, soundSampleData As String, filePath As String, hashcode As UInteger, bigEndian As Boolean)
        'Create folder if not exists
        Dim sfxOutputFolder As String = fso.GetParentFolderName(filePath)
        If Not fso.FolderExists(sfxOutputFolder) Then
            Directory.CreateDirectory(sfxOutputFolder)
        End If

        'Ensure that all files exists
        If fso.FileExists(soundMarkerFile) AndAlso fso.FileExists(soundSampleData) Then
            'Align offsets
            Dim sectionAlign = &H800

            'File section variables
            Dim fileStart1, fileStart2, fileFullSize As UInteger

            'Create a new binary writer
            Using binaryWriter As New BinaryWriter(IO.File.Open(filePath, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII)
                '--------------------------------------------------[Header]--------------------------------------------------
                '--magic[magic value]--
                binaryWriter.Write(Encoding.ASCII.GetBytes("MUSX"))

                '--hashc[Hashcode for the current soundbank without the section prefix]--
                binaryWriter.Write(CUInt(((&HE And &HF) << 20) Or hashcode))

                '--offst[Constant offset to the next section,]--
                binaryWriter.Write(&HC9)

                '--fulls[Size of the whole file, in bytes. Unused. ]--
                binaryWriter.Write(0)

                '--------------------------------------------------[File Sections]--------------------------------------------------
                '--File start 1; an offset that points to the stream look-up file details. Set to 0x800 in the original software. --
                binaryWriter.Write(0)
                '--File length 1; size of the first section, in bytes. --
                binaryWriter.Write(0)

                '--File start 2; offset to the second section with the sample data. Set to 0x1000 in the original software. --
                binaryWriter.Write(0)
                '--File length 2; size of the second section, in bytes. --
                binaryWriter.Write(0)

                '--File start 3; unused offset. Set to zero.--
                binaryWriter.Write(0)
                '--File length 3; unused. Set to zero.--
                binaryWriter.Write(0)

                '--------------------------------------------------[Files Content]--------------------------------------------------
                'Write MarkerFile
                BinAlign(binaryWriter, sectionAlign)
                fileStart1 = binaryWriter.BaseStream.Position
                Dim markersFileData As Byte() = IO.File.ReadAllBytes(soundMarkerFile)
                binaryWriter.Write(markersFileData)

                'Write Sample Data
                BinAlign(binaryWriter, sectionAlign)
                fileStart2 = binaryWriter.BaseStream.Position
                Dim soundSampleFileData As Byte() = IO.File.ReadAllBytes(soundSampleData)
                binaryWriter.Write(soundSampleFileData)

                'Get file size
                fileFullSize = binaryWriter.BaseStream.Position

                '--------------------------------------------------[Final offsets]--------------------------------------------------
                '--Size of the whole file--
                binaryWriter.BaseStream.Seek(&HC, SeekOrigin.Begin)
                binaryWriter.Write(fileFullSize)

                '--File Start 1--
                binaryWriter.BaseStream.Seek(&H10, SeekOrigin.Begin)
                binaryWriter.Write(FlipUInt32(fileStart1, bigEndian))
                binaryWriter.Write(FlipUInt32(markersFileData.Length, bigEndian))

                '--File Start 2--
                binaryWriter.BaseStream.Seek(&H18, SeekOrigin.Begin)
                binaryWriter.Write(FlipUInt32(fileStart2, bigEndian))
                binaryWriter.Write(FlipUInt32(soundSampleFileData.Length, bigEndian))

                'Close writer
                binaryWriter.Close()
            End Using
        End If
    End Sub
End Class

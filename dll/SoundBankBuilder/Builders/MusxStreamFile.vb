Imports System.IO
Imports System.Text

Public Class MusxStreamFile
    '*===============================================================================================
    '* MAIN METHOD
    '*===============================================================================================
    Public Sub BuildFinalFile(outputFolder As String, binaryFile As String, lutFile As String)
        'Ensure that the two files exists
        If Dir$(binaryFile) IsNot "" AndAlso Dir$(lutFile) IsNot "" Then
            'Create Dir if not exists
            If Dir$(outputFolder, FileAttribute.Directory) Is "" Then
                MkDir(outputFolder)
            End If

            'Calculate filepath
            Dim filePath As String = Path.Combine(outputFolder, "HC" & Hex(&HFFFF).PadLeft(6, "0"c) & ".SFX")

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
                binaryWriter.Write(0)

                '--File length 1[size of the first section, in bytes]--
                binaryWriter.Write(0)

                '--File start 2[offset to the second section with the sample data]--
                binaryWriter.Write(0)

                '--File length 2[size of the second section, in bytes]--
                binaryWriter.Write(0)

                '--File start 3[unused And uses the same sample data offset as dummy for some reason]--
                binaryWriter.Write(0)

                '--File length 3[unused And set to zero]--
                binaryWriter.Write(0)

                '--------------------------------------------------[Files Content]--------------------------------------------------
                Dim lutFileData As Byte() = File.ReadAllBytes(lutFile)
                Dim binFileData As Byte() = File.ReadAllBytes(binaryFile)

                'Write lookup table
                BinAlign(binaryWriter, &H800)
                Dim FileStart1 As Integer = binaryWriter.BaseStream.Position
                binaryWriter.Write(lutFileData)

                'Write stream data
                BinAlign(binaryWriter, &H800)
                Dim FileStart2 As Integer = binaryWriter.BaseStream.Position
                binaryWriter.Write(binFileData)

                '--------------------------------------------------[Write Final Offsets]--------------------------------------------------
                'File Full Size
                binaryWriter.BaseStream.Seek(&HC, SeekOrigin.Begin)
                binaryWriter.Write(CUInt(binaryWriter.BaseStream.Length))

                'File length 1
                binaryWriter.BaseStream.Seek(&H10, SeekOrigin.Begin)
                binaryWriter.Write(FileStart1)
                binaryWriter.Write(lutFileData.Length)

                'File length 2
                binaryWriter.BaseStream.Seek(&H18, SeekOrigin.Begin)
                binaryWriter.Write(FileStart2)
                binaryWriter.Write(binFileData.Length)

                'Close writer
                binaryWriter.Close()
            End Using
        End If
    End Sub
End Class

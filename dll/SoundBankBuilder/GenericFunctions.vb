Imports System.IO

Module GenericFunctions
    Friend Sub BinAlign(BWriter As BinaryWriter, alignment As Integer)
        BWriter.Seek((-BWriter.BaseStream.Position Mod alignment + alignment) Mod alignment, SeekOrigin.Current)
    End Sub

    Friend Function FlipUInt32(valueToFlip As UInteger, isBigEndian As Boolean) As UInteger
        Dim finalData As UInteger
        If isBigEndian Then
            finalData = (valueToFlip And &HFF000000) >> (8 * 3) Or
                        (valueToFlip And &HFF0000) >> (8 * 1) Or
                        (valueToFlip And &HFF00) << (8 * 1) Or
                        (valueToFlip And &HFF) << (8 * 3)
        Else
            finalData = valueToFlip
        End If

        Return finalData
    End Function
End Module

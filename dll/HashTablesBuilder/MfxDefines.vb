Imports System.IO

Public Class MfxDefines
    Public Sub CreateMfxHashTable(filePath As String, mfxDict As SortedDictionary(Of String, UInteger))
        'Add new data to the file
        Using fs = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None)
            'Create a new writer
            Using sw As New StreamWriter(fs)
                'Misc defines section
                sw.WriteLine("// Music HashCodes")
                Dim maxSfxHashcodeDefined = 0
                For Each mfxItem In mfxDict
                    Dim hashCodeWithSectin As UInteger = ((&H1BE And &HFFF) << 20) Or mfxItem.Value
                    WriteHashCode(sw, "MFX_" & mfxItem.Key, "0x" & Hex(hashCodeWithSectin).PadLeft(8, "0"c))
                    maxSfxHashcodeDefined = Math.Max(maxSfxHashcodeDefined, mfxItem.Value)
                Next
                WriteNoAlign(sw, "MFX_MaximumDefined", maxSfxHashcodeDefined)
            End Using
        End Using
    End Sub
End Class

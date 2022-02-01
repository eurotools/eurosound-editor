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
                    Dim hashCodeWithSectin As UInteger = mfxItem.Value + &H1BE00000
                    WriteHashCode(sw, "MFX_" & mfxItem.Key, "0x" & Hex(hashCodeWithSectin).PadLeft(8, "0"c))
                    maxSfxHashcodeDefined = Math.Max(maxSfxHashcodeDefined, mfxItem.Value)
                Next
                WriteNoAlign(sw, "MFX_MaximumDefined", maxSfxHashcodeDefined)
            End Using
        End Using
    End Sub

    Public Sub CreateMfxData(filePath As String, mfxDict As Dictionary(Of UInteger, String()))
        'Add new data to the file
        Using fs = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None)
            'Create a new writer
            Using sw As New StreamWriter(fs)
                'Misc defines section
                sw.WriteLine("// Music Data table from EuroSound 1")
                sw.WriteLine("// " & Date.Now.ToString("dddd, dd MMMM yyyy"))
                sw.WriteLine("")
                sw.WriteLine("typedef struct{")
                sw.WriteLine("	u32      MusicHashCode;")
                sw.WriteLine("	float    DurationInSeconds;")
                sw.WriteLine("	bool     Looping;")
                sw.WriteLine("} MusicDetails;")
                sw.WriteLine("")
                sw.WriteLine("MusicDetails MusicData[]={")
                Dim maxSfxHashcodeDefined = 0
                For Each mfxItem In mfxDict
                    Dim musicDataValues As String() = mfxItem.Value
                    Dim hashCode As String = "0x" & Hex(mfxItem.Key + &H1BE00000).PadLeft(8, "0"c)
                    sw.WriteLine("	{" & hashCode & "," & musicDataValues(0) & "," & musicDataValues(1) & "},")
                Next
                sw.WriteLine("};")
            End Using
        End Using
    End Sub
End Class

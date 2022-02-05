Public Class MfxDefines
    '*===============================================================================================
    '* MFX_Defines.h
    '*===============================================================================================
    Public Sub CreateMfxHashTable(filePath As String, mfxDict As SortedDictionary(Of String, UInteger))
        FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(1, "// Music HashCodes")
        Dim maxSfxHashcodeDefined = 0
        For Each mfxItem In mfxDict
            PrintLine(1, WriteHashCode("MFX_" & mfxItem.Key, NumberToHex(mfxItem.Value + &H1BE00000)))
            maxSfxHashcodeDefined = Math.Max(maxSfxHashcodeDefined, mfxItem.Value)
        Next
        PrintLine(1, "#define MFX_MaximumDefined " & maxSfxHashcodeDefined)
        FileClose(1)
    End Sub

    '*===============================================================================================
    '* MFX_Data.h
    '*===============================================================================================
    Public Sub CreateMfxData(filePath As String, mfxDict As Dictionary(Of UInteger, String()))
        FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(1, "// Music Data table from EuroSound 1")
        PrintLine(1, "// " & Date.Now.ToString("dddd, dd MMMM yyyy"))
        PrintLine(1, "")
        PrintLine(1, "typedef struct{")
        PrintLine(1, "	u32      MusicHashCode;")
        PrintLine(1, "	float    DurationInSeconds;")
        PrintLine(1, "	bool     Looping;")
        PrintLine(1, "} MusicDetails;")
        PrintLine(1, "")
        PrintLine(1, "MusicDetails MusicData[]={")
        For Each mfxItem In mfxDict
            Dim musicDataValues As String() = mfxItem.Value
            Dim hashCode As String = NumberToHex(mfxItem.Key + &H1BE00000)
            PrintLine(1, "	{" & hashCode & "," & musicDataValues(0) & "," & musicDataValues(1) & "},")
        Next
        PrintLine(1, "};")
        FileClose(1)
    End Sub

    '*===============================================================================================
    '* MFX_ValidList.h
    '*===============================================================================================
    Public Sub CreateMfxValidList(filePath As String, mfxDict As Dictionary(Of UInteger, String))
        FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(1, "s32 MFX_ValidList[]={")
        For Each mfxItem In mfxDict
            Dim hashCode As String = NumberToHex(mfxItem.Key + &H1BE00000)
            PrintLine(1, hashCode & ",// " & mfxItem.Value)
        Next
        PrintLine(1, "-1};")
        FileClose(1)
    End Sub
End Class

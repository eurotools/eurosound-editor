Imports System.IO

Public Class SfxDefines
    '*===============================================================================================
    '* SFX_Defines.h
    '*===============================================================================================
    Public Sub CreateSfxDefines(sfxDict As SortedDictionary(Of String, UInteger), soundbanksDict As SortedDictionary(Of String, UInteger), filePath As String)
        Const EX_SFX_PREFIX As UInteger = &H1A000000
        Const EX_SONG_PREFIX As UInteger = &H1B000000
        Const EX_SOUNDBANK_PREFIX As UInteger = &H1C000000
        Dim Languages As String() = New String() {"English", "American", "Japanese", "Danish", "Dutch", "Finnish", "French", "German", "Italian", "Norwegian", "Portuguese", "Spanish", "Swedish"}

        FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        'Misc defines section
        PrintLine(1, "// SFX Misc defines")
        PrintLine(1, WriteHashCode("SBIHashCode", NumberToHex(&HFFFFFF)))
        PrintLine(1, WriteHashCode("EX_SFX_PREFIX", NumberToHex(EX_SFX_PREFIX)))
        PrintLine(1, WriteHashCode("EX_SONG_PREFIX", NumberToHex(EX_SONG_PREFIX)))
        PrintLine(1, WriteHashCodeComment("EX_SOUNDBANK_PREFIX", NumberToHex(EX_SOUNDBANK_PREFIX)))
        PrintLine(1, vbNullString)

        'LANGUAGE DEFINES
        PrintLine(1, "// SFX Language defines")
        PrintLine(1, WriteHashCode("LanguageHashCodeOffset", NumberToHex(&H10000)))
        PrintLine(1, vbNullString)
        For index As Integer = 0 To Languages.Length - 1
            PrintLine(1, WriteNumber("SFXLanguage_" & UCase(Languages(index)), index))
        Next
        PrintLine(1, WriteNoAlign("StreamFileHashCode", NumberToHex(&HFFFF)))
        PrintLine(1, vbNullString)

        'HASHCODES
        PrintLine(1, "// SFX HashCodes")
        PrintLine(1, "#define SFX_NIS_MUSIC_TRIGGER 0")
        PrintLine(1, vbNullString)
        Dim maxSfxHashcodeDefined = 0
        For Each sfxItem In sfxDict
            PrintLine(1, WriteHashCode(sfxItem.Key, NumberToHex(sfxItem.Value + EX_SFX_PREFIX)))
            maxSfxHashcodeDefined = Math.Max(maxSfxHashcodeDefined, sfxItem.Value)
        Next
        PrintLine(1, WriteHashCode("SFX_MaximumDefined", NumberToHex(sfxDict.Count)))
        PrintLine(1, WriteHashCode("SFX_HashCodeHighest", NumberToHex(maxSfxHashcodeDefined)))
        PrintLine(1, "")

        'SOUNDBANK HASHCODES
        PrintLine(1, "// SoundBank HashCodes")
        For Each soundbankItem In soundbanksDict
            PrintLine(1, WriteHashCode(soundbankItem.Key, NumberToHex(soundbankItem.Value)))
        Next
        PrintLine(1, WriteHashCode("SB_MaximumDefined", NumberToHex(soundbanksDict.Count)))

        'Close reader
        FileClose(1)
    End Sub

    '*===============================================================================================
    '* SFX_Debug.h
    '*===============================================================================================
    Public Sub CreateSfxDebug(sfxDict As SortedDictionary(Of String, UInteger), filePath As String)
        FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        'Write first part
        PrintLine(1, "#ifdef SFX_BUILD_DEBUG_TABLES")
        PrintLine(1, "long NumberToHashCode[] = {")
        For Each item As KeyValuePair(Of String, UInteger) In sfxDict
            PrintLine(1, String.Format("{0} , ", item.Value))
        Next
        PrintLine(1, "};")
        PrintLine(1, "#endif")
        PrintLine(1, vbNullString)
        PrintLine(1, vbNullString)
        PrintLine(1, vbNullString)
        'Write second part
        PrintLine(1, "#ifdef SFX_BUILD_DEBUG_TABLES")
        PrintLine(1, "typedef struct HashCodeAndString {long HashCode;char* String;} HashCodeAndString;")
        PrintLine(1, vbNullString)
        PrintLine(1, "struct HashCodeAndString HashCodeToString[]={")
        For Each item As KeyValuePair(Of String, UInteger) In sfxDict
            PrintLine(1, String.Format("{0}{1} , ""{2}""{3} , ", "{"c, item.Value, item.Key, "}"c))
        Next
        PrintLine(1, "};")
        PrintLine(1, "#endif")
        FileClose(1)
    End Sub

    '*===============================================================================================
    '* SFX_Data.h
    '*===============================================================================================
    Public Sub CreateSfxData(filePath As String, folderPath As String, maxHashCode As Integer)
        'Declare an array of the required size
        Dim contentArray As String() = New String(maxHashCode) {}

        'Read content
        Dim filesArray As String() = Directory.GetFiles(folderPath, "*.txt", SearchOption.TopDirectoryOnly)
        For index As Integer = 0 To filesArray.Length - 1
            Dim fileContent As String() = File.ReadAllLines(filesArray(index))
            If fileContent.Length = 2 Then
                Dim stringArrayPosition = fileContent(1).Split(" "c)(1)
                If IsNumeric(stringArrayPosition) Then
                    Dim arrayPosition As Integer = stringArrayPosition
                    If arrayPosition < contentArray.Length Then
                        contentArray(arrayPosition) = fileContent(1)
                    End If
                End If
            End If
        Next

        'Create file
        FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(1, "// typedef struct SFXOutputDetails {s32 HashCode;f32 InnerRadius;f32 OuterRadius;f32 Altertness;f32 Duration;s8 Looping;s8 Tracking3d;s8 SampleStreamed;} SFXOutputDetails;")
        PrintLine(1, "SFXOutputDetails SFXOutputData[] = {")
        For index As Integer = 0 To contentArray.Length - 1
            If contentArray(index) Is Nothing Then
                PrintLine(1, "{ 0 , 0 ,  0 , 0 ,  0 , 0 , 0 } ,  // Blank")
            Else
                PrintLine(1, contentArray(index))
            End If
        Next
        PrintLine(1, "};")
        FileClose(1)

        'Liberate memmory
        Erase contentArray
        Erase filesArray
    End Sub
End Class

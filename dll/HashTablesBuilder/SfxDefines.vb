Imports System.IO

Public Class SfxDefines
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Friend ReadOnly AvailableSfx As New SortedDictionary(Of UInteger, String)()
    Private Const EX_SFX_PREFIX As UInteger = &H1A000000
    Private Const EX_SONG_PREFIX As UInteger = &H1B000000
    Private Const EX_SOUNDBANK_PREFIX As UInteger = &H1C000000
    Private ReadOnly Languages As String() = New String() {"ENGLISH", "AMERICAN", "JAPANESE", "DANISH", "DUTCH", "FINNISH", "FRENCH", "GERMAN", "ITALIAN", "NORWEGIAN", "PORTUGUESE", "SPANISH", "SWEDISH"}

    Public Sub CreateSfxDefines(sfxDict As SortedDictionary(Of String, UInteger), soundbanksDict As SortedDictionary(Of String, UInteger), filePath As String)
        'Add new data to the file
        Using fs = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None)
            'Create a new writer
            Using sw As New StreamWriter(fs)
                'Misc defines section
                sw.WriteLine("// SFX Misc defines")
                WriteHashCode(sw, "SBIHashCode", "0x" & Hex(&HFFFFFF).PadLeft(8, "0"c))
                WriteHashCode(sw, "EX_SFX_PREFIX", "0x" & Hex(EX_SFX_PREFIX).PadLeft(8, "0"c))
                WriteHashCode(sw, "EX_SONG_PREFIX", "0x" & Hex(EX_SONG_PREFIX).PadLeft(8, "0"c))
                WriteHashCodeComment(sw, "EX_SOUNDBANK_PREFIX", "0x" & Hex(EX_SOUNDBANK_PREFIX).PadLeft(8, "0"c))
                sw.WriteLine("")

                'LANGUAGE DEFINES
                sw.WriteLine("// SFX Language defines")
                WriteHashCode(sw, "LanguageHashCodeOffset", "0x" & Hex(&H10000).PadLeft(8, "0"c))
                sw.WriteLine("")
                For index As Integer = 0 To Languages.Length - 1
                    WriteNumber(sw, "SFXLanguage_" & Languages(index), index)
                Next
                WriteNoAlign(sw, "StreamFileHashCode", "0x" & Hex(&HFFFF).PadLeft(8, "0"c))
                sw.WriteLine("")

                'HASHCODES
                sw.WriteLine("// SFX HashCodes")
                WriteNoAlign(sw, "SFX_NIS_MUSIC_TRIGGER", "0")
                sw.WriteLine("")
                Dim maxSfxHashcodeDefined = 0
                For Each sfxItem In sfxDict
                    WriteHashCode(sw, sfxItem.Key, "0x" & Hex(sfxItem.Value Or EX_SFX_PREFIX).PadLeft(8, "0"c))
                    maxSfxHashcodeDefined = Math.Max(maxSfxHashcodeDefined, sfxItem.Value)
                Next
                WriteHashCode(sw, "SFX_MaximumDefined", "0x" & Hex(sfxDict.Count).PadLeft(8, "0"c))
                WriteHashCode(sw, "SFX_HashCodeHighest", "0x" & Hex(maxSfxHashcodeDefined).PadLeft(8, "0"c))
                sw.WriteLine("")

                'SOUNDBANK HASHCODES
                sw.WriteLine("// SoundBank HashCodes")
                For Each soundbankItem In soundbanksDict
                    WriteHashCode(sw, soundbankItem.Key, "0x" & Hex(soundbankItem.Value).PadLeft(8, "0"c))
                Next
                WriteHashCode(sw, "SB_MaximumDefined", "0x" & Hex(soundbanksDict.Count).PadLeft(8, "0"c))

                'Close reader
                sw.Close()
            End Using
        End Using
    End Sub
End Class

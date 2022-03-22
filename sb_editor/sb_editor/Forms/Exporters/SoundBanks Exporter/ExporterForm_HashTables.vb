Imports System.IO
Imports sb_editor.HashTablesBuilder.HashTables

Partial Public Class ExporterForm
    '*===============================================================================================
    '* SFX_Defines.h
    '*===============================================================================================
    Private Sub CreateSfxDefines(sfxDict As SortedDictionary(Of String, UInteger), soundbanksDict As SortedDictionary(Of String, UInteger), Languages As String(), prefixHashCodes As Boolean, filePath As String)
        Const EX_SFX_PREFIX As UInteger = &H1A000000
        Const EX_SONG_PREFIX As UInteger = &H1B000000
        Const EX_SOUNDBANK_PREFIX As UInteger = &H1C000000

        Using outputFile As New StreamWriter(filePath)
            'Misc defines section
            BackgroundWorker.ReportProgress(Nothing, "Writing SFX_Defines.h Stage 0")
            outputFile.WriteLine("// SFX Misc defines")
            outputFile.WriteLine(WriteHashCode("SBIHashCode", NumberToHex(&HFFFFFF)))
            outputFile.WriteLine(WriteHashCode("EX_SFX_PREFIX", NumberToHex(EX_SFX_PREFIX)))
            outputFile.WriteLine(WriteHashCode("EX_SONG_PREFIX", NumberToHex(EX_SONG_PREFIX)))
            outputFile.WriteLine(WriteHashCodeComment("EX_SOUNDBANK_PREFIX", NumberToHex(EX_SOUNDBANK_PREFIX)))
            outputFile.WriteLine(vbNullString)

            'LANGUAGE DEFINES
            outputFile.WriteLine("// SFX Language defines")
            outputFile.WriteLine(WriteHashCode("LanguageHashCodeOffset", NumberToHex(&H10000)))
            outputFile.WriteLine(vbNullString)
            For index As Integer = 0 To Languages.Length - 1
                outputFile.WriteLine(WriteNumber("SFXLanguage_" & UCase(Languages(index)), index))
            Next
            outputFile.WriteLine(WriteNoAlign("StreamFileHashCode", NumberToHex(&HFFFF)))
            outputFile.WriteLine(vbNullString)

            'HASHCODES
            BackgroundWorker.ReportProgress(Nothing, "Writing SFX_Defines.h Stage 1")
            outputFile.WriteLine("// SFX HashCodes")
            outputFile.WriteLine("#define SFX_NIS_MUSIC_TRIGGER 0")
            outputFile.WriteLine(vbNullString)
            Dim maxSfxHashcodeDefined = 0
            For Each sfxItem As KeyValuePair(Of String, UInteger) In sfxDict
                If prefixHashCodes Then
                    outputFile.WriteLine(WriteHashCode("HT_Sound_" & sfxItem.Key, NumberToHex(sfxItem.Value + EX_SFX_PREFIX)))
                Else
                    outputFile.WriteLine(WriteHashCode(sfxItem.Key, NumberToHex(sfxItem.Value + EX_SFX_PREFIX)))
                End If

                maxSfxHashcodeDefined = Math.Max(maxSfxHashcodeDefined, sfxItem.Value)
            Next
            BackgroundWorker.ReportProgress(Nothing, "Writing SFX_Defines.h Stage 2")
            outputFile.WriteLine(WriteHashCode("SFX_MaximumDefined", NumberToHex(sfxDict.Count)))
            outputFile.WriteLine(WriteHashCode("SFX_HashCodeHighest", NumberToHex(maxSfxHashcodeDefined)))
            outputFile.WriteLine("")

            'SOUNDBANK HASHCODES
            outputFile.WriteLine("// SoundBank HashCodes")
            For Each soundbankItem In soundbanksDict
                If prefixHashCodes Then
                    outputFile.WriteLine(WriteHashCode("HT_Sound_" & soundbankItem.Key, NumberToHex(soundbankItem.Value)))
                Else
                    outputFile.WriteLine(WriteHashCode(soundbankItem.Key, NumberToHex(soundbankItem.Value)))
                End If
            Next
            outputFile.WriteLine(WriteHashCode("SB_MaximumDefined", NumberToHex(soundbanksDict.Count)))
            outputFile.WriteLine(vbNullString)
        End Using
    End Sub

    '*===============================================================================================
    '* SFX_Debug.h
    '*===============================================================================================
    Private Sub CreateSfxDebug(sfxDict As SortedDictionary(Of String, UInteger), filePath As String)
        Using outputFile As New StreamWriter(filePath)
            'Write first part
            BackgroundWorker.ReportProgress(Nothing, "Writing SFX_Defines.h Stage 3")
            outputFile.WriteLine("#ifdef SFX_BUILD_DEBUG_TABLES")
            outputFile.WriteLine("long NumberToHashCode[] = {")
            For Each item As KeyValuePair(Of String, UInteger) In sfxDict
                outputFile.WriteLine(String.Format("{0} , ", item.Value))
            Next
            outputFile.WriteLine("};")
            outputFile.WriteLine("#endif")
            outputFile.WriteLine(vbNullString)
            outputFile.WriteLine(vbNullString)
            'Write second part
            outputFile.WriteLine("#ifdef SFX_BUILD_DEBUG_TABLES")
            outputFile.WriteLine("typedef struct HashCodeAndString {long HashCode;char* String;} HashCodeAndString;")
            outputFile.WriteLine(vbNullString)
            outputFile.WriteLine("struct HashCodeAndString HashCodeToString[]={")
            For Each item As KeyValuePair(Of String, UInteger) In sfxDict
                outputFile.WriteLine(String.Format("{0}{1} , ""{2}""{3} , ", "{"c, item.Value, item.Key, "}"c))
            Next
            outputFile.WriteLine("};")
            outputFile.WriteLine("#endif")
            outputFile.WriteLine(vbNullString)
            BackgroundWorker.ReportProgress(Nothing, "Writing SFX_Defines.h Stage Pre Close")
        End Using
    End Sub

    '*===============================================================================================
    '* SFX_Data.h
    '*===============================================================================================
    Private Sub CreateSfxData(filePath As String, folderPath As String, maxHashCode As Integer)
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
        BackgroundWorker.ReportProgress(Nothing, "Writing SFX_Data.h")
        Using outputFile As New StreamWriter(filePath)
            outputFile.WriteLine("// typedef struct SFXOutputDetails {s32 HashCode;f32 InnerRadius;f32 OuterRadius;f32 Altertness;f32 Duration;s8 Looping;s8 Tracking3d;s8 SampleStreamed;} SFXOutputDetails;")
            outputFile.WriteLine("SFXOutputDetails SFXOutputData[] = {")
            For index As Integer = 0 To contentArray.Length - 1
                If contentArray(index) Is Nothing Then
                    outputFile.WriteLine("{ 0 , 0 ,  0 , 0 ,  0 , 0 , 0 } ,  // Blank")
                Else
                    outputFile.WriteLine(contentArray(index))
                End If
            Next
            outputFile.WriteLine("};")
            outputFile.WriteLine(vbNullString)
        End Using

        'Liberate memmory
        Erase contentArray
        Erase filesArray
    End Sub

    '*===============================================================================================
    '* SFX_Reverbs.h
    '*===============================================================================================
    Private Sub CreateSfxReverbs(filePath As String, reverbsDict As SortedDictionary(Of String, UInteger))
        If reverbsDict.Count > 0 Then
            Using outputFile As New StreamWriter(filePath)
                outputFile.WriteLine("// Reverb Hashcodes")
                For Each sfxItem As KeyValuePair(Of String, UInteger) In reverbsDict
                    outputFile.WriteLine(WriteHashCode(sfxItem.Key, NumberToHex(sfxItem.Value)))
                Next
            End Using
        End If
    End Sub

    '*===============================================================================================
    '* Sound.h
    '*===============================================================================================
    Private Sub CreateSoundhFile(outputFilePath As String, sfxDefines As String, mfxDefines As String, sfxReverbDefines As String)
        Using outputFile As New StreamWriter(outputFilePath)
            outputFile.WriteLine("/* HT_Sound */")
            'SFX_Defines.h
            If File.Exists(sfxDefines) Then
                Dim sfxDefinesData As String() = File.ReadAllLines(sfxDefines)
                For index As Integer = 0 To sfxDefinesData.Length - 1
                    outputFile.WriteLine(sfxDefinesData(index))
                Next
            End If
            'MFX_Defines.h
            If File.Exists(mfxDefines) Then
                Dim mfxDefinesData As String() = File.ReadAllLines(mfxDefines)
                For index As Integer = 0 To mfxDefinesData.Length - 1
                    outputFile.WriteLine(mfxDefinesData(index))
                Next
            End If
            'SFX_Reverbs.h
            If File.Exists(sfxReverbDefines) Then
                Dim reverbsData As String() = File.ReadAllLines(sfxReverbDefines)
                For index As Integer = 0 To reverbsData.Length - 1
                    outputFile.WriteLine(reverbsData(index))
                Next
            End If
        End Using
    End Sub
End Class

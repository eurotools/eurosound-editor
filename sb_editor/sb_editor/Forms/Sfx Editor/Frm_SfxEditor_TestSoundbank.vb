Imports System.IO
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.SoundBanksExporterFunctions

Partial Public Class Frm_SfxEditor
    Private Sub CreateTestSFX(sfxTextFilePath As String)
        Dim sfxFileData As SfxFile = reader.ReadSFXFile(sfxTextFilePath)

        'Get output folder
        Dim outputFolder As String = Path.Combine(WorkingDirectory, "TempOutputFolder", "PC", "SoundBanks", "English")
        Directory.CreateDirectory(outputFolder)

        Dim debugFolderPath As String = Path.Combine(WorkingDirectory, "Debug_Report")
        Directory.CreateDirectory(debugFolderPath)

        'Get file paths
        Dim mainFilePath As String = Path.Combine(outputFolder, &HFFFE)
        Dim sfxFilePath As String = mainFilePath & ".sfx"
        Dim sifFilePath As String = mainFilePath & ".sif"
        Dim sbfFilePath As String = mainFilePath & ".sbf"

        'Get hashcodes
        Dim hashCodesDictionary As SortedDictionary(Of String, UInteger) = GetHashCodesDict(Path.Combine(WorkingDirectory, "SFXs"))

        'Get SFX and samples list
        Dim CancelSoundBankOutput As Boolean = False
        Dim SfxList As String() = New String() {Path.Combine(WorkingDirectory, "SFXs", "Misc", "Common")}
        Dim sfxDictionary As SortedDictionary(Of String, EXSound) = ReadSfxData(SfxList, True)
        ApplyDuckerLength(sfxDictionary, "PC")
        Dim sampleToInclude As String() = GetFinalList(GetSoundBankSamplesList(SfxList, "English"), StreamSamplesList, "PC", False)
        Dim samplesDictionary As Dictionary(Of String, EXAudio) = ReadSampleData(sampleToInclude, "Master", CancelSoundBankOutput)

        'Get output file paths
        Dim sfxFileName As String = "HC" & GetSfxFileName(Array.FindIndex(SfxLanguages, Function(t) t.Equals("English", StringComparison.OrdinalIgnoreCase)), &HFFFE).ToString("X6")
        Dim outputFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary", GetEngineXFolder("PC"), GetEngineXLangFolder("English"))
        Directory.CreateDirectory(outputFilePath)

        'Start Writting file
        Dim streamFileReport As New List(Of KeyValuePair(Of String, Integer))
        Using sbfWriter As New BinaryWriter(File.Open(sbfFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            Using sifWriter As New BinaryWriter(File.Open(sifFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                Using sfxWriter As New BinaryWriter(File.Open(sfxFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    WriteSbfFile(sbfWriter, samplesDictionary)
                    WriteSifFile(sifWriter, samplesDictionary, False)
                    streamFileReport = WriteSfxFile(sfxWriter, hashCodesDictionary, sfxDictionary, samplesDictionary, StreamSamplesList, "Englisn", False)
                End Using
            End Using
        End Using

        'Write Debug File
        Dim soundBanksDebugFilePath As String = Path.Combine(debugFolderPath, "StreamDebugSoundBank____SB_TEST_SFX____PC_English.txt")
        writers.WriteSoundBankDebug(soundBanksDebugFilePath, streamFileReport, sfxFilePath, "___SB_TEST_SFX___", 65534)

        'Merge files
        Dim fileOutputPath As String = Path.Combine(outputFilePath, sfxFileName & ".SFX")
        ESUtils.MusXBuild_Soundbank.BuildSoundbankFile(sfxFilePath, sifFilePath, sbfFilePath, Nothing, fileOutputPath, &HFFFE, False)

        'Copy Soundbank to destination folder
        File.Copy(fileOutputPath, Path.Combine(Application.StartupPath, "SystemFiles", "testAudioMod", "_Eng", sfxFileName & ".SFX"), True)
    End Sub
End Class

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

        'Get file paths
        Dim mainFilePath As String = Path.Combine(outputFolder, &HFFFE)
        Dim sfxFilePath As String = mainFilePath & ".sfx"
        Dim sifFilePath As String = mainFilePath & ".sif"
        Dim sbfFilePath As String = mainFilePath & ".sbf"

        'Get SFX and samples list
        Dim samplesToInclude As New HashSet(Of String)
        Dim SfxDictionary As New SortedDictionary(Of String, EXSound)
        Dim SamplesDictionary As New Dictionary(Of String, EXAudio)
        'GetSFXsDictionary(New String() {"Misc\" & Path.GetFileNameWithoutExtension(sfxTextFilePath)}, "PC", SfxDictionary, samplesToInclude, New String() {}, True)
        'GetSamplesDictionary(samplesToInclude, SamplesDictionary, "PC", "English", False, True)

        'Get output file paths
        Dim sfxFileName As String = "HC" & Hex(GetSfxFileName(Array.IndexOf(SfxLanguages, "English"), &HFFFE)).PadLeft(6, "0"c)
        Dim outputFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary", GetEngineXFolder("PC"), GetEngineXLangFolder("English"))
        Directory.CreateDirectory(outputFilePath)

        'Debug file
        Dim debugFile As String = Path.Combine(WorkingDirectory, "Debug_Report")
        Directory.CreateDirectory(debugFile)
        FileOpen(1, Path.Combine(debugFile, "StreamDebugSoundBank____SB_TEST_SFX____PC_English.txt"), OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(1, "SoundBank Output Debug Data")
        PrintLine(1, Format(Now, "dd/mm/yyy"))
        PrintLine(1, Format(Now, "hh:mm:ss"))
        PrintLine(1, "")
        PrintLine(1, "SoundBankName = ___SB_TEST_SFX___")
        PrintLine(1, "SoundBankSaveName = 65534")
        PrintLine(1, "SoundBankFileName = " & sfxFilePath)
        PrintLine(1, "Stream PoolFiles(n).FileRef")

        'Start Writting file
        Using sbfWriter As New BinaryWriter(File.Open(sbfFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            Using sifWriter As New BinaryWriter(File.Open(sifFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                Using sfxWriter As New BinaryWriter(File.Open(sfxFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    WriteSbfFile(sbfWriter, SamplesDictionary)
                    WriteSifFile(sifWriter, SamplesDictionary, False)
                    'WriteSfxFile(sfxWriter, Nothing, SfxDictionary, SamplesDictionary, New String() {}, False)
                End Using
            End Using
        End Using

        'Merge files
        Dim fileOutputPath As String = Path.Combine(outputFilePath, sfxFileName & ".SFX")
        ESUtils.MusXBuild_Soundbank.BuildSoundbankFile(sfxFilePath, sifFilePath, sbfFilePath, Nothing, fileOutputPath, &HFFFE, False)

        'Copy Soundbank to destination folder
        File.Copy(fileOutputPath, Path.Combine(Application.StartupPath, "SystemFiles", "testAudioMod", "_Eng", sfxFileName & ".SFX"), True)
    End Sub
End Class

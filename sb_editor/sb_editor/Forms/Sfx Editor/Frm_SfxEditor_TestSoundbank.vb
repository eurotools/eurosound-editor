Imports System.IO
Imports sb_editor.ExporterObjects
Imports sb_editor.ParsersObjects
Imports sb_editor.SoundBanksExporterFunctions

Partial Public Class Frm_SfxEditor
    Private Sub CreateTestSFX(sfxTextFilePath As String)
        Dim sfxFileData As SfxFile = reader.ReadSFXFile(sfxTextFilePath)

        'Get output folder
        Dim outputFolder As String = fso.BuildPath(WorkingDirectory, "TempOutputFolder\PC\SoundBanks\English")
        CreateFolderIfRequired(outputFolder)

        'Get file paths
        Dim sfxFilePath As String = fso.BuildPath(outputFolder, &HFFFE & ".sfx")
        Dim sifFilePath As String = fso.BuildPath(outputFolder, &HFFFE & ".sif")
        Dim sbfFilePath As String = fso.BuildPath(outputFolder, &HFFFE & ".sbf")

        'Get SFX and samples list
        Dim samplesToInclude As New HashSet(Of String)
        Dim SfxDictionary As New SortedDictionary(Of String, EXSound)
        Dim SamplesDictionary As New Dictionary(Of String, EXAudio)
        GetSFXsDictionary(New String() {"Misc\" & fso.GetFileName(sfxTextFilePath)}, "PC", SfxDictionary, samplesToInclude, New String() {}, True)
        GetSamplesDictionary(samplesToInclude, SamplesDictionary, "PC", "English", False, True)

        'Get output file paths
        Dim sfxFileName As String = "HC" & Hex(GetSfxFileName(Array.IndexOf(SfxLanguages, "English"), &HFFFE)).PadLeft(6, "0"c)
        Dim outputFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.EngineXFolder, "Binary\" & GetEngineXFolder("PC") & "\" & GetEngineXLangFolder("English"))
        CreateFolderIfRequired(outputFilePath)

        'Debug file
        Dim debugFile As String = fso.BuildPath(WorkingDirectory, "Debug_Report")
        CreateFolderIfRequired(debugFile)
        FileOpen(1, fso.BuildPath(debugFile, "StreamDebugSoundBank____SB_TEST_SFX____PC_English.txt"), OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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
                    WriteSfxFile(sfxWriter, Nothing, SfxDictionary, SamplesDictionary, New String() {}, False)
                End Using
            End Using
        End Using

        'Merge files
        Dim fileOutputPath As String = fso.BuildPath(outputFilePath, sfxFileName & ".SFX")
        ESUtils.MusXBuild_Soundbank.BuildSoundbankFile(sfxFilePath, sifFilePath, sbfFilePath, Nothing, fileOutputPath, &HFFFE, False)

        'Copy Soundbank to destination folder
        File.Copy(fileOutputPath, fso.BuildPath(Application.StartupPath, "SystemFiles\testAudioMod\_Eng\" & sfxFileName & ".SFX"), True)
    End Sub
End Class

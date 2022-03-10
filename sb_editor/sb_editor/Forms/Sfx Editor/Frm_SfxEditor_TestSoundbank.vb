Imports sb_editor.ParsersObjects

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
    End Sub
End Class

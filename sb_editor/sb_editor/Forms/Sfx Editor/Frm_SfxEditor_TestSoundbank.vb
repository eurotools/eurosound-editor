Partial Public Class Frm_SfxEditor
    Private Sub CreateTestSFX(sfxFilePath As String)
        Dim sfxFileData As SfxFile = reader.ReadSFXFile(sfxFilePath)
    End Sub
End Class

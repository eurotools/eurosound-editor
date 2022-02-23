Partial Public Class AdvancedMenu
    Private ReadOnly writers As New FileWriters

    Private Sub Button_ReAllocateHashcodes_Click(sender As Object, e As EventArgs) Handles Button_ReAllocateHashcodes.Click
        If fso.FolderExists(fso.BuildPath(WorkingDirectory, "System")) Then
            'Check SFX Hashcode
            SFXHashCodeNumber = GetMaxHashCode(fso.BuildPath(WorkingDirectory, "SFXs"))
            SoundBankHashCodeNumber = GetMaxHashCode(fso.BuildPath(WorkingDirectory, "SoundBanks"))
            MFXHashCodeNumber = GetMaxHashCode(fso.BuildPath(WorkingDirectory, "ES_Music\Music\ESData"))
            'Update file
            writers.UpdateMiscFile(fso.BuildPath(WorkingDirectory, "System\Misc.txt"))
        End If
    End Sub

    Private Sub Button_CheckForDuplicateHashCodes_Click(sender As Object, e As EventArgs) Handles Button_CheckForDuplicateHashCodes.Click

    End Sub

    Private Sub Button_LanguageFolder_Click(sender As Object, e As EventArgs) Handles Button_LanguageFolder.Click
        Dim languageTool As New Language_FolderCompare
        languageTool.ShowDialog()
    End Sub

    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        Close()
    End Sub

    Private Sub Button_ValidateSfx_Click(sender As Object, e As EventArgs) Handles Button_ValidateSfx.Click
        Dim sfxPlatformsList As New List(Of String)
        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")

        'Get GameCube SFXs
        Dim gameCubeDir As String = fso.BuildPath(baseDir, "GameCube")
        If fso.FolderExists(gameCubeDir) Then
            GetPlatformSFXs(gameCubeDir, sfxPlatformsList)
        End If
        'Get PC SFXs
        Dim pcDir As String = fso.BuildPath(baseDir, "PC")
        If fso.FolderExists(pcDir) Then
            GetPlatformSFXs(pcDir, sfxPlatformsList)
        End If
        'Get PC SFXs
        Dim playStation2 As String = fso.BuildPath(baseDir, "PlayStation2")
        If fso.FolderExists(playStation2) Then
            GetPlatformSFXs(playStation2, sfxPlatformsList)
        End If
        'Get X Box SFXs
        Dim Xbox As String = fso.BuildPath(baseDir, "X Box")
        If fso.FolderExists(Xbox) Then
            GetPlatformSFXs(Xbox, sfxPlatformsList)
        End If


    End Sub

    Private Sub GetPlatformSFXs(folderToInspect As String, sfxPlatformsList As List(Of String))
        Dim fileNameWithExtension As String = Dir(folderToInspect, "*.txt")
        Do While fileNameWithExtension > ""
            Dim fileNameLength As Integer = Len(fileNameWithExtension)
            Dim fileName As String = Microsoft.VisualBasic.Left(fileNameWithExtension, fileNameLength - Len(".txt"))
            sfxPlatformsList.Add(fileName)
            'Get new item
            fileNameWithExtension = Dir()
        Loop
    End Sub
End Class
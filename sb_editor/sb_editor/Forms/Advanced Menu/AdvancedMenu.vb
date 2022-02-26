Partial Public Class AdvancedMenu
    Private ReadOnly writers As New FileWriters
    Private ReadOnly readers As New FileParsers

    Private Sub Button_CheckForDuplicateHashCodes_Click(sender As Object, e As EventArgs) Handles Button_CheckForDuplicateHashCodes.Click
        Dim availableHashcode As New Dictionary(Of UInteger, String)
        Dim duplicatedHashcodes As New List(Of String)

        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        If fso.FolderExists(baseDir) Then
            Dim fileNameWithExtension As String = Dir(baseDir & "\*.txt", FileAttribute.Archive)
            Do While fileNameWithExtension > ""
                Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs\", fileNameWithExtension)
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(sfxFilePath)
                If availableHashcode.ContainsKey(sfxFileData.HashCode) Then
                    If duplicatedHashcodes.Count = 0 Then
                        'Add file to list
                        duplicatedHashcodes.Add("SFXs Found With Duplicate HashCodes")
                        duplicatedHashcodes.Add("")
                        'Update hashcode and write file again
                        sfxFileData.HashCode = SFXHashCodeNumber
                        SFXHashCodeNumber += 1
                        writers.WriteSfxFile(sfxFileData, sfxFilePath)
                    End If
                    duplicatedHashcodes.Add(GetOnlyFileName(fileNameWithExtension))
                Else
                    availableHashcode.Add(sfxFileData.HashCode, fileNameWithExtension)
                End If
                fileNameWithExtension = Dir()
            Loop
            availableHashcode.Clear()
            'Add message if empty
            If duplicatedHashcodes.Count = 0 Then
                duplicatedHashcodes.Add("No Duplicate HashCodes Found")
            End If
            'Show info to the user
            Dim debugInfo As New Frm_DebugData(duplicatedHashcodes.ToArray)
            debugInfo.ShowDialog()
        End If
    End Sub

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

    Private Sub Button_ValidateInterSample_Click(sender As Object, e As EventArgs) Handles Button_ValidateInterSample.Click
        'Get all SFXs that has sub SFXs
        Dim errorsToShow As New List(Of String)

        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        If fso.FolderExists(baseDir) Then
            Dim fileNameWithExtension As String = Dir(baseDir & "\*.txt", FileAttribute.Archive)
            Do While fileNameWithExtension > ""
                Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs\", fileNameWithExtension)
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(sfxFilePath)
                'Check for negative multi samples and loops
                If sfxFileData.SamplePool.Action1 = 1 AndAlso sfxFileData.SamplePool.EnableSubSFX Then
                    If sfxFileData.SamplePool.MinDelay < 0 Or sfxFileData.SamplePool.MaxDelay < 0 Then
                        errorsToShow.Add(GetOnlyFileName(fileNameWithExtension) & "   -ve Multi")
                    End If
                ElseIf sfxFileData.SamplePool.isLooped AndAlso sfxFileData.SamplePool.Action1 = 0 Then
                    If sfxFileData.SamplePool.MinDelay < 0 Or sfxFileData.SamplePool.MaxDelay < 0 Then
                        errorsToShow.Add(GetOnlyFileName(fileNameWithExtension) & "   -ve Loop")
                    End If
                End If
                fileNameWithExtension = Dir()
            Loop
            'Check what we need to show to the user
            If errorsToShow.Count > 0 Then
                'Show info to the user
                Dim debugInfo As New Frm_DebugData(errorsToShow.ToArray)
                debugInfo.ShowDialog()
            Else
                MsgBox("All OK", vbOKOnly + vbInformation, "EuroSound")
            End If
        End If
    End Sub

    Private Sub Button_LanguageFolder_Click(sender As Object, e As EventArgs) Handles Button_LanguageFolder.Click
        Dim languageTool As New Language_FolderCompare
        languageTool.ShowDialog()
    End Sub

    Private Sub Button_StealOnLouder_Click(sender As Object, e As EventArgs) Handles Button_StealOnLouder.Click
        'Get all SFXs that has sub SFXs
        Dim errorsToShow As New List(Of String)
        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        If fso.FolderExists(baseDir) Then
            Dim fileNameWithExtension As String = Dir(baseDir & "\*.txt", FileAttribute.Archive)
            Do While fileNameWithExtension > ""
                Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs\", fileNameWithExtension)
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(sfxFilePath)
                'Seems that if the flag Steal On Age is on and the random volume is different than zero is interpreted as an error
                If sfxFileData.Parameters.StealOnAge Then
                    For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                        Dim currentSample As Sample = sfxFileData.Samples(sampleIndex)
                        If currentSample.RandomVolumeOffset <> 0 Then
                            errorsToShow.Add(GetOnlyFileName(fileNameWithExtension) & " -->> " & currentSample.FilePath)
                        End If
                    Next
                End If
                fileNameWithExtension = Dir()
            Loop
            'Check what we need to show to the user
            If errorsToShow.Count > 0 Then
                'Show info to the user
                Dim debugInfo As New Frm_DebugData(errorsToShow.ToArray)
                debugInfo.ShowDialog()
            Else
                MsgBox("All OK", vbOKOnly + vbInformation, "EuroSound")
            End If
        End If
    End Sub

    Private Sub Button_ValidateSfxLinks_Click(sender As Object, e As EventArgs) Handles Button_ValidateSfxLinks.Click
        'Get all SFXs that has sub SFXs
        Dim missingLinks As New List(Of String)

        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        If fso.FolderExists(baseDir) Then
            Dim fileNameWithExtension As String = Dir(baseDir & "\*.txt", FileAttribute.Archive)
            Do While fileNameWithExtension > ""
                Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs\", fileNameWithExtension)
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(sfxFilePath)
                If sfxFileData.SamplePool.EnableSubSFX Then
                    'Add links to dictionary
                    For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                        Dim linkHashCode As String = sfxFileData.Samples(sampleIndex).FilePath
                        Dim subSfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs\", linkHashCode & ".txt")
                        'Add missing link to list
                        If Not fso.FileExists(subSfxFilePath) Then
                            missingLinks.Add(GetOnlyFileName(fileNameWithExtension) & " #=# " & linkHashCode)
                        End If
                    Next
                End If
                fileNameWithExtension = Dir()
            Loop
            'Check what we need to show to the user
            If missingLinks.Count > 0 Then
                missingLinks.Add("------------------------------------------")
                'Show info to the user
                Dim debugInfo As New Frm_DebugData(missingLinks.ToArray)
                debugInfo.ShowDialog()
            Else
                MsgBox("All OK", vbOKOnly + vbInformation, "EuroSound")
            End If
        End If
    End Sub

    Private Sub Button_ValidateSfx_Click(sender As Object, e As EventArgs) Handles Button_ValidateSfx.Click
        Dim sfxPlatformsList As New List(Of String)
        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")

        'Get GameCube SFXs
        Dim gameCubeDir As String = fso.BuildPath(baseDir, "GameCube")
        If fso.FolderExists(gameCubeDir) Then
            GetPlatformSFXs(gameCubeDir, sfxPlatformsList, "GameCube")
        End If
        'Get PC SFXs
        Dim pcDir As String = fso.BuildPath(baseDir, "PC")
        If fso.FolderExists(pcDir) Then
            GetPlatformSFXs(pcDir, sfxPlatformsList, "PC")
        End If
        'Get PC SFXs
        Dim playStation2 As String = fso.BuildPath(baseDir, "PlayStation2")
        If fso.FolderExists(playStation2) Then
            GetPlatformSFXs(playStation2, sfxPlatformsList, "PlayStation2")
        End If
        'Get X Box SFXs
        Dim Xbox As String = fso.BuildPath(baseDir, "X Box")
        If fso.FolderExists(Xbox) Then
            GetPlatformSFXs(Xbox, sfxPlatformsList, "X Box")
        End If

        'Show info to user
        sfxPlatformsList.Sort()
        Dim debugInfo As New Frm_DebugData(sfxPlatformsList.ToArray)
        debugInfo.ShowDialog()
    End Sub

    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        Close()
    End Sub
End Class
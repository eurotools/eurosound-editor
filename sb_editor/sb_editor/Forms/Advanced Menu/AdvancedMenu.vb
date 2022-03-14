﻿Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses

Partial Public Class AdvancedMenu
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly writers As New FileWriters
    Private ReadOnly readers As New FileParsers

    '*===============================================================================================
    '* MAKE REPORT
    '*===============================================================================================
    Private Sub Button_MakeReport_Click(sender As Object, e As EventArgs) Handles Button_MakeReport.Click
        Dim mainFrame As MainFrame = CType(Application.OpenForms("MainFrame"), MainFrame)
        If mainFrame IsNot Nothing Then
            If mainFrame.TreeView_SoundBanks.Nodes.Count > 0 Then
                'Get soundbank name
                Dim selectedSoundBank As String = mainFrame.TreeView_SoundBanks.Nodes(0).Text
                If mainFrame.TreeView_SoundBanks.SelectedNode IsNot Nothing Then
                    selectedSoundBank = mainFrame.TreeView_SoundBanks.SelectedNode.Text
                End If
                'Output folder
                Dim outputFolder As String = fso.BuildPath(WorkingDirectory, "Report")
                CreateFolderIfRequired(outputFolder)
                'Ensure that the SoundBank Exists
                Dim soundBankFilePath As String = fso.BuildPath(WorkingDirectory & "\SoundBanks", selectedSoundBank & ".txt")
                If fso.FileExists(soundBankFilePath) Then
                    Cursor.Current = Cursors.WaitCursor
                    'Output Language
                    Dim outLanguage As String = "English"
                    If mainFrame.ComboBox_OutputLanguage.SelectedItem IsNot Nothing Then
                        outLanguage = mainFrame.ComboBox_OutputLanguage.SelectedItem
                    End If
                    'Output Format
                    Dim outFormat As String = "PC"
                    If mainFrame.ComboBox_Format.SelectedItem IsNot Nothing Then
                        outLanguage = mainFrame.ComboBox_Format.SelectedItem
                    End If
                    CreateSFXReportFile(fso.BuildPath(outputFolder, selectedSoundBank & ".txt"), selectedSoundBank, soundBankFilePath, outFormat, outLanguage)
                    Cursor.Current = Cursors.Default
                End If
            End If
        End If
    End Sub

    '*===============================================================================================
    '* CHECK SFX HASHCODES
    '*===============================================================================================
    Private Sub Button_CheckForDuplicateHashCodes_Click(sender As Object, e As EventArgs) Handles Button_CheckForDuplicateHashCodes.Click
        Dim availableHashcode As New Dictionary(Of UInteger, String)
        Dim duplicatedHashcodes As New List(Of String)

        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        If fso.FolderExists(baseDir) Then
            Dim fileNameWithExtension As String = Dir(baseDir & "\*.txt", FileAttribute.Archive)
            Do While fileNameWithExtension > ""
                Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs\", fileNameWithExtension)
                'Read SFX file as a string array
                Dim fileData As String() = File.ReadAllLines(sfxFilePath)
                Dim hashcodeIndex As Integer = Array.IndexOf(fileData, "#HASHCODE")
                If hashcodeIndex >= 0 Then
                    'Get HashCode
                    Dim stringData As String() = fileData(hashcodeIndex + 1).Split(" "c)
                    If stringData.Length > 1 AndAlso IsNumeric(stringData(1)) Then
                        Dim hashcodeNumber As UInteger = stringData(1)
                        'Check if we have read this hashcode before
                        If availableHashcode.ContainsKey(hashcodeNumber) Then
                            If duplicatedHashcodes.Count = 0 Then
                                'Add file to list
                                duplicatedHashcodes.Add("SFXs Found With Duplicate HashCodes")
                                duplicatedHashcodes.Add("")
                                'Update hashcode and write file again
                                hashcodeNumber = SFXHashCodeNumber
                                SFXHashCodeNumber += 1
                                'Update hashcode and write the updated data
                                fileData(hashcodeIndex + 1) = "HashCodeNumber " & hashcodeNumber
                                File.WriteAllLines(sfxFilePath, fileData)
                            End If
                            duplicatedHashcodes.Add(GetOnlyFileName(fileNameWithExtension))
                        Else
                            availableHashcode.Add(hashcodeNumber, fileNameWithExtension)
                        End If
                        fileNameWithExtension = Dir()
                    End If
                End If
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

    '*===============================================================================================
    '* RE-ALLOCATE HASHCODES
    '*===============================================================================================
    Private Sub Button_ReAllocateHashcodes_Click(sender As Object, e As EventArgs) Handles Button_ReAllocateHashcodes.Click
        If fso.FolderExists(fso.BuildPath(WorkingDirectory, "System")) Then
            'Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor
            '-----------------------------------------Reallocate SFX Files-----------------------------------------
            Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory, "SFXs")
            If fso.FolderExists(sfxFilePath) Then
                'Reset variable
                SFXHashCodeNumber = 1
                'Get and modify files
                Dim sfxFilesToCheck As String() = Directory.GetFiles(sfxFilePath, "*.txt", SearchOption.TopDirectoryOnly)
                For fileIndex As Integer = 0 To sfxFilesToCheck.Length - 1
                    Dim currentFilePath As String = sfxFilesToCheck(fileIndex)
                    Dim sfxFileName As String = GetOnlyFileName(currentFilePath)
                    '---------------------------Common
                    WriteSfxFile(currentFilePath)
                    '---------------------------GameCube
                    Dim gameCubeFilePath As String = fso.BuildPath(sfxFilePath, "GameCube\" & sfxFileName & ".txt")
                    If fso.FileExists(gameCubeFilePath) Then
                        WriteSfxFile(gameCubeFilePath)
                    End If
                    '---------------------------PC
                    Dim PCFilePath As String = fso.BuildPath(sfxFilePath, "PC\" & sfxFileName & ".txt")
                    If fso.FileExists(PCFilePath) Then
                        WriteSfxFile(PCFilePath)
                    End If
                    '---------------------------PlayStation2
                    Dim PlayStation2FilePath As String = fso.BuildPath(sfxFilePath, "PlayStation2\" & sfxFileName & ".txt")
                    If fso.FileExists(PlayStation2FilePath) Then
                        WriteSfxFile(PlayStation2FilePath)
                    End If
                    '---------------------------X Box
                    Dim xboxFilePath As String = fso.BuildPath(sfxFilePath, "X Box\" & sfxFileName & ".txt")
                    If fso.FileExists(xboxFilePath) Then
                        WriteSfxFile(xboxFilePath)
                    End If
                    'Update variable
                    SFXHashCodeNumber += 1
                Next
            End If

            '-----------------------------------------Reallocate Soundbank Files-----------------------------------------
            Dim soundbankFilePath As String = fso.BuildPath(WorkingDirectory, "SoundBanks")
            If fso.FolderExists(soundbankFilePath) Then
                'Reset variable
                SoundBankHashCodeNumber = 1
                'Get and modify files
                Dim soundbankFilesToCheck As String() = Directory.GetFiles(soundbankFilePath, "*.txt", SearchOption.TopDirectoryOnly)
                For fileIndex As Integer = 0 To soundbankFilesToCheck.Length - 1
                    Dim currentFilePath As String = soundbankFilesToCheck(fileIndex)
                    'Read files
                    Dim fileLines As String() = File.ReadAllLines(currentFilePath)
                    'Update HashCode
                    Dim hashcodeLineIndex As Integer = Array.IndexOf(fileLines, "#HASHCODE") + 1
                    fileLines(hashcodeLineIndex) = "HashCodeNumber " & SoundBankHashCodeNumber
                    SoundBankHashCodeNumber += 1
                    'Write file again
                    File.WriteAllLines(currentFilePath, fileLines)
                Next
            End If

            '-----------------------------------------Reallocate MFX Files-----------------------------------------
            Dim musicsFilePath As String = fso.BuildPath(WorkingDirectory, "Music\ESData")
            If fso.FolderExists(musicsFilePath) Then
                'Reset variable
                MFXHashCodeNumber = 1
                'Get and modify files
                Dim musicFilesToCheck As String() = Directory.GetFiles(musicsFilePath, "*.txt", SearchOption.TopDirectoryOnly)
                For fileIndex As Integer = 0 To musicFilesToCheck.Length - 1
                    Dim currentFilePath As String = musicFilesToCheck(fileIndex)
                    'Read files
                    Dim fileLines As String() = File.ReadAllLines(currentFilePath)
                    'Update HashCode
                    Dim hashcodeLineIndex As Integer = Array.IndexOf(fileLines, "#HASHCODE") + 1
                    fileLines(hashcodeLineIndex) = "HashCodeNumber " & MFXHashCodeNumber
                    MFXHashCodeNumber += 1
                    'Write file again
                    File.WriteAllLines(currentFilePath, fileLines)
                Next
            End If
            'Update file
            writers.UpdateMiscFile(fso.BuildPath(WorkingDirectory, "System\Misc.txt"))
            'Set cursor as default arrow
            Cursor.Current = Cursors.Default
        End If
    End Sub

    '*===============================================================================================
    '* VALIDATE SUB SFS LINKS
    '*===============================================================================================
    Private Sub Button_ValidateInterSample_Click(sender As Object, e As EventArgs) Handles Button_ValidateInterSample.Click
        'Get all SFXs that has sub SFXs
        Dim errorsToShow As New List(Of String)

        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        If fso.FolderExists(baseDir) Then
            'Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor

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
            'Set cursor as default arrow
            Cursor.Current = Cursors.Default
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

    '*===============================================================================================
    '* LANGUAGE FOLDER COMPARE
    '*===============================================================================================
    Private Sub Button_LanguageFolder_Click(sender As Object, e As EventArgs) Handles Button_LanguageFolder.Click
        Dim languageTool As New Language_FolderCompare
        languageTool.ShowDialog()
    End Sub

    '*===============================================================================================
    '* STEAL ON LOUDER CHECK
    '*===============================================================================================
    Private Sub Button_StealOnLouder_Click(sender As Object, e As EventArgs) Handles Button_StealOnLouder.Click
        'Get all SFXs that has sub SFXs
        Dim errorsToShow As New List(Of String)
        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        If fso.FolderExists(baseDir) Then
            'Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor
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
            'Set cursor as default arrow
            Cursor.Current = Cursors.Default
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

    '*===============================================================================================
    '* VALIDATE SUB SFS LINKS
    '*===============================================================================================
    Private Sub Button_ValidateSfxLinks_Click(sender As Object, e As EventArgs) Handles Button_ValidateSfxLinks.Click
        'Get all SFXs that has sub SFXs
        Dim missingLinks As New List(Of String)

        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        If fso.FolderExists(baseDir) Then
            'Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor
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
            'Set cursor as default arrow
            Cursor.Current = Cursors.Default
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

    '*===============================================================================================
    '* VALIDATE PLATFORM SFX VERSIONS
    '*===============================================================================================
    Private Sub Button_ValidateSfx_Click(sender As Object, e As EventArgs) Handles Button_ValidateSfx.Click
        Dim sfxPlatformsList As New List(Of String)
        Dim baseDir As String = fso.BuildPath(WorkingDirectory, "SFXs")
        'Set cursor as hourglass
        Cursor.Current = Cursors.WaitCursor
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
        'Set cursor as default arrow
        Cursor.Current = Cursors.Default
        'Show info to user
        sfxPlatformsList.Sort()
        Dim debugInfo As New Frm_DebugData(sfxPlatformsList.ToArray)
        debugInfo.ShowDialog()
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        Close()
    End Sub
End Class
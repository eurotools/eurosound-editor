Imports System.IO
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
                Dim outputFolder As String = Path.Combine(WorkingDirectory, "Report")
                Directory.CreateDirectory(outputFolder)

                'Ensure that the SoundBank Exists
                Dim soundBankFilePath As String = Path.Combine(WorkingDirectory, "SoundBanks", selectedSoundBank & ".txt")
                If File.Exists(soundBankFilePath) Then
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
                    CreateSFXReportFile(Path.Combine(outputFolder, selectedSoundBank & ".txt"), selectedSoundBank, soundBankFilePath, outFormat, outLanguage)
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
        Dim outputMessages As New List(Of String)

        Dim baseDir As String = Path.Combine(WorkingDirectory, "SFXs")
        If Directory.Exists(baseDir) Then
            Dim filesToInspect As String() = Directory.GetFiles(baseDir, "*.txt", SearchOption.TopDirectoryOnly)
            For fileIndex As Integer = 0 To filesToInspect.Length - 1
                'Read SFX file as a string array
                Dim fileData As String() = File.ReadAllLines(filesToInspect(fileIndex))
                Dim hashcodeIndex As Integer = Array.FindIndex(fileData, Function(t) t.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase))
                If hashcodeIndex >= 0 Then
                    'Get HashCode
                    Dim stringData As String() = fileData(hashcodeIndex + 1).Split(" "c)
                    If stringData.Length > 1 AndAlso IsNumeric(stringData(1)) Then
                        Dim hashcodeNumber As UInteger = stringData(1)

                        'Check if we have read this hashcode before
                        If availableHashcode.ContainsKey(hashcodeNumber) Then
                            'Text to output in the debug form                           
                            If outputMessages.Count = 0 Then
                                outputMessages.Add("SFXs Found With Duplicate HashCodes")
                                outputMessages.Add("")
                            End If

                            'Update hashcode and write the updated data
                            fileData(hashcodeIndex + 1) = "HashCodeNumber " & SFXHashCodeNumber
                            SFXHashCodeNumber += 1
                            File.WriteAllLines(filesToInspect(fileIndex), fileData)

                            'Text to output in the debug form
                            outputMessages.Add(Path.GetFileNameWithoutExtension(filesToInspect(fileIndex)))
                        Else
                            availableHashcode.Add(hashcodeNumber, filesToInspect(fileIndex))
                        End If
                    End If
                End If
            Next
            availableHashcode.Clear()

            'Add message if empty
            If outputMessages.Count = 0 Then
                outputMessages.Add("No Duplicate HashCodes Found")
            Else
                writers.UpdateMiscFile(Path.Combine(WorkingDirectory, "System", "Misc.txt"))
            End If

            'Show info to the user
            Dim debugInfo As New Frm_DebugData(outputMessages.ToArray)
            debugInfo.ShowDialog()
        End If
    End Sub

    '*===============================================================================================
    '* RE-ALLOCATE HASHCODES
    '*===============================================================================================
    Private Sub Button_ReAllocateHashcodes_Click(sender As Object, e As EventArgs) Handles Button_ReAllocateHashcodes.Click
        If Directory.Exists(Path.Combine(WorkingDirectory, "System")) Then
            'Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor

            '-----------------------------------------Reallocate SFX Files-----------------------------------------
            Dim sfxFilePath As String = Path.Combine(WorkingDirectory, "SFXs")
            If Directory.Exists(sfxFilePath) Then
                'Reset variable
                SFXHashCodeNumber = 1

                'Get and modify files
                Dim sfxFilesToCheck As String() = Directory.GetFiles(sfxFilePath, "*.txt", SearchOption.TopDirectoryOnly)
                For fileIndex As Integer = 0 To sfxFilesToCheck.Length - 1
                    Dim currentFilePath As String = sfxFilesToCheck(fileIndex)
                    Dim sfxFileName As String = Path.GetFileNameWithoutExtension(currentFilePath)

                    '---------------------------Common
                    WriteSfxFile(currentFilePath)

                    '---------------------------GameCube
                    Dim gameCubeFilePath As String = Path.Combine(sfxFilePath, "GameCube", sfxFileName & ".txt")
                    If File.Exists(gameCubeFilePath) Then
                        WriteSfxFile(gameCubeFilePath)
                    End If

                    '---------------------------PC
                    Dim PCFilePath As String = Path.Combine(sfxFilePath, "PC", sfxFileName & ".txt")
                    If File.Exists(PCFilePath) Then
                        WriteSfxFile(PCFilePath)
                    End If

                    '---------------------------PlayStation2
                    Dim PlayStation2FilePath As String = Path.Combine(sfxFilePath, "PlayStation2", sfxFileName & ".txt")
                    If File.Exists(PlayStation2FilePath) Then
                        WriteSfxFile(PlayStation2FilePath)
                    End If

                    '---------------------------X Box
                    Dim xboxFilePath As String = Path.Combine(sfxFilePath, "X Box", sfxFileName & ".txt")
                    If File.Exists(xboxFilePath) Then
                        WriteSfxFile(xboxFilePath)
                    End If
                    'Update variable
                    SFXHashCodeNumber += 1
                Next
            End If

            '-----------------------------------------Reallocate Soundbank Files-----------------------------------------
            Dim soundbankFilePath As String = Path.Combine(WorkingDirectory, "SoundBanks")
            If Directory.Exists(soundbankFilePath) Then
                'Reset variable
                SoundBankHashCodeNumber = 1

                'Get and modify files
                Dim soundbankFilesToCheck As String() = Directory.GetFiles(soundbankFilePath, "*.txt", SearchOption.TopDirectoryOnly)
                For fileIndex As Integer = 0 To soundbankFilesToCheck.Length - 1
                    Dim currentFilePath As String = soundbankFilesToCheck(fileIndex)

                    'Read files
                    Dim fileLines As String() = File.ReadAllLines(currentFilePath)

                    'Update HashCode
                    Dim hashcodeLineIndex As Integer = Array.FindIndex(fileLines, Function(t) t.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase)) + 1
                    fileLines(hashcodeLineIndex) = "HashCodeNumber " & SoundBankHashCodeNumber
                    SoundBankHashCodeNumber += 1

                    'Write file again
                    File.WriteAllLines(currentFilePath, fileLines)
                Next
            End If

            '-----------------------------------------Reallocate MFX Files-----------------------------------------
            Dim musicsFilePath As String = Path.Combine(WorkingDirectory, "Music", "ESData")
            If Directory.Exists(musicsFilePath) Then
                'Reset variable
                MFXHashCodeNumber = 1

                'Get and modify files
                Dim musicFilesToCheck As String() = Directory.GetFiles(musicsFilePath, "*.txt", SearchOption.TopDirectoryOnly)
                For fileIndex As Integer = 0 To musicFilesToCheck.Length - 1
                    Dim currentFilePath As String = musicFilesToCheck(fileIndex)

                    'Read files
                    Dim fileLines As String() = File.ReadAllLines(currentFilePath)

                    'Update HashCode
                    Dim hashcodeLineIndex As Integer = Array.FindIndex(fileLines, Function(t) t.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase)) + 1
                    fileLines(hashcodeLineIndex) = "HashCodeNumber " & MFXHashCodeNumber
                    MFXHashCodeNumber += 1

                    'Write file again
                    File.WriteAllLines(currentFilePath, fileLines)
                Next
            End If

            'Update file
            writers.UpdateMiscFile(Path.Combine(WorkingDirectory, "System", "Misc.txt"))

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

        Dim baseDir As String = Path.Combine(WorkingDirectory, "SFXs")
        If Directory.Exists(baseDir) Then
            'Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor

            'Inspect files
            Dim filesToInspect As String() = Directory.GetFiles(baseDir, "*.txt", SearchOption.TopDirectoryOnly)
            For fileIndex As Integer = 0 To filesToInspect.Length - 1
                Dim sfxFilePath As String = filesToInspect(fileIndex)
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(sfxFilePath)

                'Check for negative multi samples and loops
                If sfxFileData.SamplePool.Action1 = 1 AndAlso sfxFileData.SamplePool.EnableSubSFX Then
                    If sfxFileData.SamplePool.MinDelay < 0 Or sfxFileData.SamplePool.MaxDelay < 0 Then
                        errorsToShow.Add(Path.GetFileNameWithoutExtension(sfxFilePath) & "   -ve Multi")
                    End If
                ElseIf sfxFileData.SamplePool.isLooped AndAlso sfxFileData.SamplePool.Action1 = 0 Then
                    If sfxFileData.SamplePool.MinDelay < 0 Or sfxFileData.SamplePool.MaxDelay < 0 Then
                        errorsToShow.Add(Path.GetFileNameWithoutExtension(sfxFilePath) & "   -ve Loop")
                    End If
                End If
            Next

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
        Dim baseDir As String = Path.Combine(WorkingDirectory, "SFXs")
        If Directory.Exists(baseDir) Then
            'Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor

            'Inspect files
            Dim filesToInspect As String() = Directory.GetFiles(baseDir, "*.txt", SearchOption.TopDirectoryOnly)
            For fileIndex As Integer = 0 To filesToInspect.Length - 1
                Dim sfxFilePath As String = filesToInspect(fileIndex)
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(sfxFilePath)

                'Seems that if the flag Steal On Age is on and the random volume is different than zero is interpreted as an error
                If sfxFileData.Parameters.StealOnAge Then
                    For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                        Dim currentSample As Sample = sfxFileData.Samples(sampleIndex)
                        If currentSample.RandomVolumeOffset <> 0 Then
                            errorsToShow.Add(Path.GetFileNameWithoutExtension(sfxFilePath) & " -->> " & currentSample.FilePath)
                        End If
                    Next
                End If
            Next

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

        Dim baseDir As String = Path.Combine(WorkingDirectory, "SFXs")
        If Directory.Exists(baseDir) Then
            'Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor

            'Inspect files
            Dim filesToInspect As String() = Directory.GetFiles(baseDir, "*.txt", SearchOption.TopDirectoryOnly)
            For fileIndex As Integer = 0 To filesToInspect.Length - 1
                Dim sfxFilePath As String = filesToInspect(fileIndex)
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(sfxFilePath)

                If sfxFileData.SamplePool.EnableSubSFX Then
                    'Add links to dictionary
                    For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                        Dim linkHashCode As String = sfxFileData.Samples(sampleIndex).FilePath
                        Dim subSfxFilePath As String = Path.Combine(WorkingDirectory, "SFXs", linkHashCode & ".txt")

                        'Add missing link to list
                        If Not File.Exists(subSfxFilePath) Then
                            missingLinks.Add(Path.GetFileNameWithoutExtension(sfxFilePath) & " #=# " & linkHashCode)
                        End If
                    Next
                End If
            Next

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
        Dim baseDir As String = Path.Combine(WorkingDirectory, "SFXs")

        'Set cursor as hourglass
        Cursor.Current = Cursors.WaitCursor

        'Get GameCube SFXs
        Dim gameCubeDir As String = Path.Combine(baseDir, "GameCube")
        If Directory.Exists(gameCubeDir) Then
            GetPlatformSFXs(gameCubeDir, sfxPlatformsList, "GameCube")
        End If

        'Get PC SFXs
        Dim pcDir As String = Path.Combine(baseDir, "PC")
        If Directory.Exists(pcDir) Then
            GetPlatformSFXs(pcDir, sfxPlatformsList, "PC")
        End If

        'Get PC SFXs
        Dim playStation2 As String = Path.Combine(baseDir, "PlayStation2")
        If Directory.Exists(playStation2) Then
            GetPlatformSFXs(playStation2, sfxPlatformsList, "PlayStation2")
        End If

        'Get X Box SFXs
        Dim Xbox As String = Path.Combine(baseDir, "X Box")
        If Directory.Exists(Xbox) Then
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
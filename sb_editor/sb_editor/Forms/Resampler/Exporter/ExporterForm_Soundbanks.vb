Partial Public Class ExporterForm
    Public Sub OutputSoundbanks(soundbanksList As String(), streamsList As String(), outLanguages As String(), outPlatforms As String())
        'Debug Folder
        Dim debugFolder As String = fso.BuildPath(WorkingDirectory, "Debug_Report")
        CreateFolderIfRequired(debugFolder)

        'Reset progress bar
        Invoke(Sub() ProgressBar1.Value = 0)
        'For each Language
        For languageIndex As Integer = 0 To outLanguages.Length - 1
            Dim currentLanguage As String = outLanguages(languageIndex)
            'For each Platform
            For platformIndex As Integer = 0 To outPlatforms.Length - 1
                Dim currentPlatform As String = outPlatforms(platformIndex)
                'For each Soundbank
                Dim soundbanksCount As Integer = soundbanksList.Length - 1
                For soundBankIndex As Integer = 0 To soundbanksCount
                    Dim currentSoundBank As String = soundbanksList(soundBankIndex)
                    'Calculate and report progress
                    BackgroundWorker.ReportProgress(Decimal.Divide(soundBankIndex, soundbanksCount) * 100.0)
                    'Soundbank file path
                    Invoke(Sub() Text = "Outputting " & currentLanguage & " SoundBank " & currentSoundBank & " for " & currentPlatform)
                    Dim soundbankFilePath As String = fso.BuildPath(WorkingDirectory & "\SoundBanks", currentSoundBank & ".txt")
                    If fso.FileExists(soundbankFilePath) Then
                        Dim soundBankInfo As SoundbankFile = textFileReaders.ReadSoundBankFile(soundbankFilePath)
                        Dim soundBankSFXs As String() = GetSfxList(soundBankInfo)
                        Dim soundBankSamples As String() = GetSamplesList(soundBankSFXs, streamsList, currentPlatform)
                    End If
                Next
            Next
        Next
    End Sub

    '*===============================================================================================
    '* PRIVATE METHODS TO GET SOUNDBANK LISTS DATA
    '*===============================================================================================
    Private Function GetSfxList(soundbankData As SoundbankFile) As String()
        Dim sfxHashSet As New HashSet(Of String)

        'Iterate over all databases
        For databaseIndex As Integer = 0 To soundbankData.Dependencies.Length - 1
            Dim databaseFilePath As String = fso.BuildPath(WorkingDirectory & "\DataBases\", soundbankData.Dependencies(databaseIndex) & ".txt")
            If fso.FileExists(databaseFilePath) Then
                Dim databaseFile As DataBaseFile = textFileReaders.ReadDataBaseFile(databaseFilePath)
                For sfxIndex As Integer = 0 To databaseFile.Dependencies.Length - 1
                    sfxHashSet.Add(databaseFile.Dependencies(sfxIndex))
                Next
            End If
        Next

        'Create an array with the included SFXs
        Dim soundbankSfx As String() = sfxHashSet.ToArray
        Array.Sort(soundbankSfx)

        Return soundbankSfx
    End Function

    Private Function GetSamplesList(sfxList As String(), streamsList As String(), outputPlatform As String) As String()
        Dim samplesHashSet As New HashSet(Of String)

        For sfxIndex As Integer = 0 To sfxList.Length - 1
            'Get sfx file path and ensure that exists
            Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs", sfxList(sfxIndex) & ".txt")
            If fso.FileExists(sfxFilePath) Then
                'Check for specific formats
                Dim sfxPlatformPath As String = fso.BuildPath(WorkingDirectory & "\SFXs\" & outputPlatform, sfxList(sfxIndex) & ".txt")
                If fso.FileExists(sfxPlatformPath) Then
                    sfxFilePath = sfxPlatformPath
                End If
                'Read file data and get a list with the included samples
                Dim sfxFileData As SfxFile = textFileReaders.ReadSFXFile(sfxFilePath)
                For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                    Dim relativeSampleFilePath As String = sfxFileData.Samples(sampleIndex).FilePath
                    'Check if this sample is streamed
                    Dim absrelativeSampleFilePath As String = relativeSampleFilePath
                    If Not relativeSampleFilePath.StartsWith("\") Then
                        absrelativeSampleFilePath = "\" & relativeSampleFilePath
                    End If
                    'Add sample to the output list
                    If Array.IndexOf(streamsList, fso.GetAbsolutePathName(absrelativeSampleFilePath)) = -1 Then
                        samplesHashSet.Add(WorkingDirectory & "\Master\" & relativeSampleFilePath)
                    End If
                Next
            End If
        Next

        'Create an array with the included SFXs
        Dim soundbankSfx As String() = samplesHashSet.ToArray
        Array.Sort(soundbankSfx)

        Return soundbankSfx
    End Function
End Class

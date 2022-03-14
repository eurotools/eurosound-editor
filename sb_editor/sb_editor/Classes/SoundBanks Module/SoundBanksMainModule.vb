Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace SoundBanksExporterFunctions
    Module SoundBanksMainModule
        Private ReadOnly textFileReaders As New FileParsers

        '*===============================================================================================
        '* FUNCTIONS TO GET LISTS
        '*===============================================================================================
        Friend Function GetSoundBankSFXsList(soundBankData As SoundbankFile, outputPlatform As String) As String()
            Dim SFXsList As New HashSet(Of String)

            'Get Platform SFXs
            For dataBaseIndex As Integer = 0 To soundBankData.Dependencies.Length - 1
                Dim currentDataBase As String = soundBankData.Dependencies(dataBaseIndex)
                Dim dataBaseFilePath As String = fso.BuildPath(WorkingDirectory & "\DataBases", currentDataBase & ".txt")
                Dim dataBaseFileData As DataBaseFile = textFileReaders.ReadDataBaseFile(dataBaseFilePath)
                For sfxIndex As Integer = 0 To dataBaseFileData.Dependencies.Length - 1
                    Dim sfxFilename As String = dataBaseFileData.Dependencies(sfxIndex)
                    Dim sfxFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs", sfxFilename & ".txt")
                    Dim specificSFXFilePath As String = fso.BuildPath(WorkingDirectory & "\SFXs\" & outputPlatform, sfxFilename & ".txt")
                    If fso.FileExists(specificSFXFilePath) Then
                        SFXsList.Add(outputPlatform & "/" & sfxFilename)
                    Else
                        SFXsList.Add(sfxFilename)
                    End If
                Next
            Next

            'Parse to array and sort
            Dim sfxItemsArray As String() = SFXsList.ToArray
            Array.Sort(sfxItemsArray)

            Return sfxItemsArray
        End Function

        Friend Function GetSoundBankSamplesList(SFXsArray As String(), OutputLanguage As String) As String()
            Dim samplesList As New HashSet(Of String)

            For sfxIndex As Integer = 0 To SFXsArray.Length - 1
                Dim currentSfx As String = SFXsArray(sfxIndex)
                Dim sfxFileData As String() = File.ReadAllLines(fso.BuildPath(WorkingDirectory & "\SFXs", currentSfx & ".txt"))
                Dim startPos As Integer = Array.IndexOf(sfxFileData, "#SFXSamplePoolFiles") + 1
                While StrComp(sfxFileData(startPos), "#END")
                    Dim currentLanguage As String = UCase(OutputLanguage)
                    Dim currentSample As String = UCase(sfxFileData(startPos))
                    If InStr(1, currentSample, "SPEECH\ENGLISH", CompareMethod.Binary) AndAlso StrComp(currentLanguage, "ENGLISH") <> 0 Then
                        currentSample = "SPEECH\" & currentLanguage & Mid(currentSample, 15)
                    End If
                    samplesList.Add(fso.BuildPath(UCase(ProjectSettingsFile.MiscProps.SampleFileFolder), "MASTER\" & currentSample))
                    startPos += 1
                End While
            Next

            'Parse to array and sort
            Dim samplesListArray As String() = samplesList.ToArray
            Array.Sort(samplesListArray)

            Return samplesListArray
        End Function

        '*===============================================================================================
        '* FUNCTIONS RELATIONS WITH THE FILES MANAGMENT
        '*===============================================================================================
        Friend Function GetFinalList(samplesArray As String(), StreamsArray As String(), outputPlatform As String) As String()
            Dim samplesToInclude As New HashSet(Of String)
            Dim finalSampleList As String() = RemoveAllSubSFX(samplesArray)
            If StrComp(outputPlatform, "PC") = 0 Or StrComp(outputPlatform, "PlayStation2") = 0 Then
                finalSampleList = RemoveAllStreams(finalSampleList, StreamsArray)
            End If
            'Sort and return
            Array.Sort(finalSampleList)
            Return finalSampleList
        End Function

        Private Function RemoveAllSubSFX(SamplesArray As String())
            Dim samplesToInclude As New HashSet(Of String)
            For sampleIndex As Integer = 0 To SamplesArray.Length - 1
                Dim currentSample As String = SamplesArray(sampleIndex)
                If InStr(1, currentSample, ".WAV", CompareMethod.Binary) AndAlso InStr(1, currentSample, "\\", CompareMethod.Binary) = 0 Then
                    samplesToInclude.Add(currentSample)
                End If
            Next
            Return samplesToInclude.ToArray
        End Function

        Private Function RemoveAllStreams(SamplesArray As String(), StreamsArray As String())
            Dim samplesToInclude As New HashSet(Of String)
            For sampleIndex As Integer = 0 To SamplesArray.Length - 1
                Dim currentSample As String = SamplesArray(sampleIndex)
                Dim sampleIsStream As Boolean = False
                For streamIndex As Integer = 0 To StreamsArray.Length - 1
                    Dim currentStream As String = StreamsArray(streamIndex)
                    If InStr(1, currentSample, currentStream, CompareMethod.Binary) Then
                        sampleIsStream = True
                        Exit For
                    End If
                Next
                'Check if we can add this sample
                If sampleIsStream = False Then
                    samplesToInclude.Add(currentSample)
                End If
            Next

            Return samplesToInclude.ToArray
        End Function

        '*===============================================================================================
        '* FUNCTIONS TO CALCULATE ESTIMATED SIZE - NO MASTER FOLDER
        '*===============================================================================================
        Friend Function GetEstimatedPlatformSizeNoMaster(samplesList As String(), outputPlatform As String) As Integer
            Dim sampleSize As Integer = 0
            For sampleIndex As Integer = 0 To samplesList.Length - 1
                Dim filePath As String = samplesList(sampleIndex)
                If InStr(1, filePath, ".WAV", CompareMethod.Binary) Then
                    If StrComp(UCase(outputPlatform), "XBOX") = 0 Or StrComp(UCase(outputPlatform), "X BOX") = 0 Then
                        sampleSize += 36
                    Else
                        sampleSize += 32
                    End If
                End If
            Next
            Return sampleSize
        End Function

        '*===============================================================================================
        '* FUNCTIONS TO CALCULATE ESTIMATED SIZE - NO MASTER FOLDER
        '*===============================================================================================
    End Module
End Namespace

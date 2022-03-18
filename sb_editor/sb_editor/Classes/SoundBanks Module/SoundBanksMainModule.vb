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
                Dim dataBaseFilePath As String = Path.Combine(WorkingDirectory, "DataBases", currentDataBase & ".txt")

                Dim dataBaseFileData As DataBaseFile = textFileReaders.ReadDataBaseFile(dataBaseFilePath)
                For sfxIndex As Integer = 0 To dataBaseFileData.Dependencies.Length - 1
                    Dim sfxFilename As String = dataBaseFileData.Dependencies(sfxIndex)
                    Dim sfxFilePath As String = Path.Combine(WorkingDirectory, "SFXs", sfxFilename & ".txt")
                    Dim specificSFXFilePath As String = Path.Combine(WorkingDirectory, "SFXs", outputPlatform, sfxFilename & ".txt")
                    If File.Exists(specificSFXFilePath) Then
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
                Dim sfxFileData As String() = File.ReadAllLines(Path.Combine(WorkingDirectory, "SFXs", currentSfx & ".txt"))
                Dim startPos As Integer = Array.IndexOf(sfxFileData, "#SFXSamplePoolFiles") + 1
                While Not sfxFileData(startPos).Equals("#END")
                    Dim currentSample As String = sfxFileData(startPos).ToUpper
                    If currentSample.Contains("SPEECH\ENGLISH") AndAlso Not OutputLanguage.Equals("English", StringComparison.OrdinalIgnoreCase) Then
                        currentSample = "SPEECH\" & OutputLanguage.ToUpper & currentSample.Substring(15)
                    End If
                    samplesList.Add(Path.Combine(UCase(ProjectSettingsFile.MiscProps.SampleFileFolder), "MASTER\" & currentSample))
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
            If outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase) Or outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase) Then
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
                If Path.HasExtension(currentSample) AndAlso Not currentSample.Contains("\\") Then
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
                    If currentSample.Contains(currentStream) Then
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
                If Path.HasExtension(filePath) Then
                    If outputPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) Or outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase) Then
                        sampleSize += 36
                    Else
                        sampleSize += 32
                    End If
                End If
            Next
            Return sampleSize
        End Function
    End Module
End Namespace

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


    End Module
End Namespace

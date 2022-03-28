Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.SoundBanksExporterFunctions

Partial Public Class AdvancedMenu
    Private Sub GetPlatformSFXs(folderToInspect As String, sfxPlatformsList As List(Of String), platform As String)
        Dim filesToInspect As String() = Directory.GetFiles(folderToInspect, "*.txt", SearchOption.TopDirectoryOnly)
        For fileIndex As Integer = 0 To filesToInspect.Length - 1
            Dim currentFilePath As String = filesToInspect(fileIndex)
            sfxPlatformsList.Add(Path.GetFileNameWithoutExtension(currentFilePath) & "  " & platform)
        Next
    End Sub

    Private Sub WriteSfxFile(sfxFilepath As String)
        'Read files
        Dim fileLines As String() = File.ReadAllLines(sfxFilepath)
        'Update HashCode
        Dim hashcodeLineINdex As Integer = Array.FindIndex(fileLines, Function(t) t.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase)) + 1
        fileLines(hashcodeLineINdex) = "HashCodeNumber " & SFXHashCodeNumber
        'Write file again
        File.WriteAllLines(sfxFilepath, fileLines)
    End Sub

    Private Sub CreateSFXReportFile(reportFilePath As String, selectedSoundBank As String, soundBankFilePath As String, outputFormat As String, outputLanguage As String)
        'Get SoundBank Data
        Dim totalSampleSize As Integer = 0
        Dim soundBankData As SoundbankFile = readers.ReadSoundBankFile(soundBankFilePath)
        Dim soundBankSFXs As String() = GetSoundBankSFXsList(soundBankData, outputFormat)
        Dim soundBankSamplesList As String() = GetSoundBankSamplesList(soundBankSFXs, outputLanguage)
        Dim soundBankSizes As List(Of String) = GetSoundBankFormatSizes(soundBankData, soundBankSamplesList, outputFormat, totalSampleSize)

        'Create file
        Using outputFile As New StreamWriter(reportFilePath)
            outputFile.WriteLine("SoundBank Report Created: 	" & Date.Now.ToString("MM/dd/yyyy") & "	" & Date.Now.ToString("HH:mm:ss"))
            outputFile.WriteLine("")
            outputFile.WriteLine("SoundBank Name: " & selectedSoundBank)
            outputFile.WriteLine("")
            outputFile.WriteLine("First Created :		 " & soundBankData.HeaderInfo.FirstCreated)
            outputFile.WriteLine("Created By :		 " & soundBankData.HeaderInfo.CreatedBy)
            outputFile.WriteLine("Last Modified :		 " & soundBankData.HeaderInfo.LastModify)
            outputFile.WriteLine("Last Modified By :		 " & soundBankData.HeaderInfo.LastModifyBy)
            outputFile.WriteLine("")
            outputFile.WriteLine("Database Count:		" & soundBankData.Dependencies.Length)
            outputFile.WriteLine("SFX Count:		" & soundBankSFXs.Length)
            outputFile.WriteLine("Sample Count:		" & soundBankSamplesList.Length)
            outputFile.WriteLine("")
            outputFile.WriteLine("Total Sample Size:		" & BytesStringFormat(totalSampleSize))
            outputFile.WriteLine("")
            'Print values
            For itemIndex As Integer = 0 To soundBankSizes.Count - 1
                Dim currentFormat As String = soundBankSizes(itemIndex)
                Dim dataToPrint As String() = Split(currentFormat, ";")
                Select Case dataToPrint(0)
                    Case "PlayStation2"
                        outputFile.WriteLine("PlayStation2:		" & dataToPrint(1))
                    Case "GameCube"
                        outputFile.WriteLine("GameCube:		" & dataToPrint(1))
                    Case "PC"
                        outputFile.WriteLine("PC:		" & dataToPrint(1))
                    Case Else
                        outputFile.WriteLine("X Box:		" & dataToPrint(1))
                End Select
            Next
            outputFile.WriteLine("")
            outputFile.WriteLine("")
            outputFile.WriteLine("DataBases:  " & soundBankData.Dependencies.Length)
            outputFile.WriteLine("SFXs:  " & soundBankSFXs.Length)
            outputFile.WriteLine("Samples:  " & soundBankSamplesList.Length)
            outputFile.WriteLine("")
            outputFile.WriteLine("")
            'Print SoundBank info
            Dim samplePathStartPos As Integer = Len(ProjectSettingsFile.MiscProps.SampleFileFolder)
            For databaseIndex As Integer = 0 To soundBankData.Dependencies.Length - 1
                'Get Database Data
                Dim currentDataBase As String = soundBankData.Dependencies(databaseIndex)
                Dim dataBaseFilePath As String = Path.Combine(WorkingDirectory, "DataBases", currentDataBase & ".txt")
                If File.Exists(dataBaseFilePath) Then
                    Dim dataBaseSFXs As DataBaseFile = readers.ReadDataBaseFile(dataBaseFilePath)
                    outputFile.WriteLine("DataBase: 	" & currentDataBase)
                    'Get SFXs in this DataBase
                    For sfxIndex As Integer = 0 To dataBaseSFXs.Dependencies.Length - 1
                        Dim currentSfx As String = dataBaseSFXs.Dependencies(sfxIndex)
                        soundBankSamplesList = GetSamplesList(New String() {currentSfx})
                        outputFile.WriteLine("	SFX: 	" & soundBankSFXs(sfxIndex))
                        'Print Samples in this SFX
                        For sampleIndex As Integer = 0 To soundBankSamplesList.Length - 1
                            outputFile.WriteLine("		Sample: 	" & Mid(soundBankSamplesList(sampleIndex), samplePathStartPos + 1))
                        Next
                        outputFile.WriteLine("	End SFX")
                    Next
                    outputFile.WriteLine("End DataBase")
                    outputFile.WriteLine("")
                End If
            Next
        End Using
    End Sub

    Private Function GetSoundBankFormatSizes(soundBankData As SoundbankFile, soundBankSamplesList As String(), OutputLanguage As String, ByRef totalSampleSize As Integer) As List(Of String)
        Dim soundBankFormatSizes As New List(Of String)
        Dim availablePlatforms As String() = ProjectSettingsFile.sampleRateFormats.Keys.ToArray

        'Get streamed samples 
        Dim samplesTable As DataTable = readers.SamplesFileToDatatable(SysFileSamples)
        Dim streamSamplesList As String() = readers.GetAllStreamSamples(samplesTable)
        For formatIndex As Integer = 0 To availablePlatforms.Length - 1
            Dim currentFormat As String = availablePlatforms(formatIndex)
            Dim soundBankSize As String
            Dim formatSamplesList As String() = GetFinalList(soundBankSamplesList, streamSamplesList, currentFormat)
            'Get SoundBank Size
            Dim formatSamplesFolder As String = Path.Combine(WorkingDirectory, currentFormat, "SoundBanks", OutputLanguage, soundBankData.HashCode & ".sbf")
            totalSampleSize += formatSamplesList.Length
            soundBankSize = BytesStringFormat(GetEstimatedPlatformSizeNoMaster(formatSamplesList, currentFormat)) & " - ESTIMATED"
            'Add format to dictionary
            soundBankFormatSizes.Add(currentFormat & ";" & soundBankSize)
        Next
        Return soundBankFormatSizes
    End Function

    Private Function GetSamplesList(soundBankSFXs As String()) As String()
        'Get Samples
        Dim Samples As New List(Of String)
        For sfxIndex As Integer = 0 To soundBankSFXs.Length - 1
            'Open file
            Dim filePath As String = Path.Combine(WorkingDirectory, "SFXs", soundBankSFXs(sfxIndex) & ".txt")
            If File.Exists(filePath) Then
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(filePath)
                For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                    Samples.Add(UCase(Path.Combine(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master\" & sfxFileData.Samples(sampleIndex).FilePath)))
                Next
            End If
        Next

        Return Samples.ToArray
    End Function
End Class

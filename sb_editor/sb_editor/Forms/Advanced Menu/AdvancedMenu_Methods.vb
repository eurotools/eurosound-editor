Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.SoundBanksExporterFunctions

Partial Public Class AdvancedMenu
    Private Function GetMaxHashCode(folderPath As String) As Integer
        Dim hashcodeNumber As Integer = 1
        If fso.FolderExists(folderPath) Then
            Dim filesToCheck As String() = Directory.GetFiles(folderPath, "*.txt", SearchOption.TopDirectoryOnly)
            For index As Integer = 0 To filesToCheck.Length - 1
                Dim fileData As String() = File.ReadAllLines(filesToCheck(index))
                Dim hashcodeIndex As Integer = Array.IndexOf(fileData, "#HASHCODE")
                If hashcodeIndex >= 0 Then
                    Dim stringData As String() = fileData(hashcodeIndex + 1).Split(" "c)
                    If stringData.Length > 1 AndAlso IsNumeric(stringData(1)) Then
                        hashcodeNumber = Math.Max(hashcodeNumber, CInt(stringData(1)))
                    End If
                End If
            Next
        End If
        Return hashcodeNumber
    End Function

    Private Sub GetPlatformSFXs(folderToInspect As String, sfxPlatformsList As List(Of String), platform As String)
        Dim fileNameWithExtension As String = Dir(folderToInspect & "\*.txt")
        Do While fileNameWithExtension > ""
            Dim fileNameLength As Integer = Len(fileNameWithExtension)
            Dim fileName As String = Microsoft.VisualBasic.Left(fileNameWithExtension, fileNameLength - Len(".txt"))
            sfxPlatformsList.Add(fileName & "  " & platform)
            'Get new item
            fileNameWithExtension = Dir()
        Loop
    End Sub

    Private Sub WriteSfxFile(sfxFilepath As String)
        'Read files
        Dim fileLines As String() = File.ReadAllLines(sfxFilepath)
        'Update HashCode
        Dim hashcodeLineINdex As Integer = Array.IndexOf(fileLines, "#HASHCODE") + 1
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
        FileOpen(2, reportFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        PrintLine(2, "SoundBank Report Created: 	" & Date.Now.ToString("MM/dd/yyyy") & "	" & Date.Now.ToString("HH:mm:ss"))
        PrintLine(2, "")
        PrintLine(2, "SoundBank Name: " & selectedSoundBank)
        PrintLine(2, "")
        PrintLine(2, "First Created :		 " & soundBankData.HeaderInfo.FirstCreated)
        PrintLine(2, "Created By :		 " & soundBankData.HeaderInfo.CreatedBy)
        PrintLine(2, "Last Modified :		 " & soundBankData.HeaderInfo.LastModify)
        PrintLine(2, "Last Modified By :		 " & soundBankData.HeaderInfo.LastModifyBy)
        PrintLine(2, "")
        PrintLine(2, "Database Count:		" & soundBankData.Dependencies.Length)
        PrintLine(2, "SFX Count:		" & soundBankSFXs.Length)
        PrintLine(2, "Sample Count:		" & soundBankSamplesList.Length)
        PrintLine(2, "")
        PrintLine(2, "Total Sample Size:		" & BytesStringFormat(totalSampleSize))
        PrintLine(2, "")
        'Print values
        For itemIndex As Integer = 0 To soundBankSizes.Count - 1
            Dim currentFormat As String = soundBankSizes(itemIndex)
            Dim dataToPrint As String() = Split(currentFormat, ";")
            Select Case dataToPrint(0)
                Case "PlayStation2"
                    PrintLine(2, "PlayStation2:		" & dataToPrint(1))
                Case "GameCube"
                    PrintLine(2, "GameCube:		" & dataToPrint(1))
                Case "PC"
                    PrintLine(2, "PC:		" & dataToPrint(1))
                Case Else
                    PrintLine(2, "X Box:		" & dataToPrint(1))
            End Select
        Next
        PrintLine(2, "")
        PrintLine(2, "")
        PrintLine(2, "DataBases:  " & soundBankData.Dependencies.Length)
        PrintLine(2, "SFXs:  " & soundBankSFXs.Length)
        PrintLine(2, "Samples:  " & soundBankSamplesList.Length)
        PrintLine(2, "")
        PrintLine(2, "")
        'Print SoundBank info
        Dim samplePathStartPos As Integer = Len(ProjectSettingsFile.MiscProps.SampleFileFolder)
        For databaseIndex As Integer = 0 To soundBankData.Dependencies.Length - 1
            'Get Database Data
            Dim currentDataBase As String = soundBankData.Dependencies(databaseIndex)
            Dim dataBaseFilePath As String = fso.BuildPath(WorkingDirectory & "\DataBases\", currentDataBase & ".txt")
            If fso.FileExists(dataBaseFilePath) Then
                Dim dataBaseSFXs As DataBaseFile = readers.ReadDataBaseFile(dataBaseFilePath)
                PrintLine(2, "DataBase: 	" & currentDataBase)
                'Get SFXs in this DataBase
                For sfxIndex As Integer = 0 To dataBaseSFXs.Dependencies.Length - 1
                    Dim currentSfx As String = dataBaseSFXs.Dependencies(sfxIndex)
                    soundBankSamplesList = GetSamplesList(New String() {currentSfx})
                    PrintLine(2, "	SFX: 	" & soundBankSFXs(sfxIndex))
                    'Print Samples in this SFX
                    For sampleIndex As Integer = 0 To soundBankSamplesList.Length - 1
                        PrintLine(2, "		Sample: 	" & Mid(soundBankSamplesList(sampleIndex), samplePathStartPos + 1))
                    Next
                    PrintLine(2, "	End SFX")
                Next
                PrintLine(2, "End DataBase")
                PrintLine(2, "")
            End If
        Next
        FileClose(2)
    End Sub

    Private Function GetSoundBankFormatSizes(soundBankData As SoundbankFile, soundBankSamplesList As String(), OutputLanguage As String, ByRef totalSampleSize As Integer) As List(Of String)
        Dim soundBankFormatSizes As New List(Of String)
        Dim availablePlatforms As String() = ProjectSettingsFile.sampleRateFormats.Keys.ToArray

        'Get streamed samples 
        Dim streamSamplesList As String() = readers.GetStreamSoundsList(SysFileSamples)
        Dim samplesTable As DataTable = readers.SamplesFileToDatatable(SysFileSamples)
        For formatIndex As Integer = 0 To availablePlatforms.Length - 1
            Dim currentFormat As String = availablePlatforms(formatIndex)
            Dim soundBankSize As String
            Dim formatSamplesList As String() = GetFinalList(soundBankSamplesList, streamSamplesList, currentFormat)
            'Get SoundBank Size
            Dim formatSamplesFolder As String = fso.BuildPath(WorkingDirectory, currentFormat & "\SoundBanks\" & OutputLanguage & "\" & soundBankData.HashCode & ".sbf")
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
            Dim filePath As String = fso.BuildPath(WorkingDirectory, "SFXs\" & soundBankSFXs(sfxIndex) & ".txt")
            If fso.FileExists(filePath) Then
                Dim sfxFileData As SfxFile = readers.ReadSFXFile(filePath)
                For sampleIndex As Integer = 0 To sfxFileData.Samples.Count - 1
                    Samples.Add(UCase(fso.BuildPath(ProjectSettingsFile.MiscProps.SampleFileFolder, "Master\" & sfxFileData.Samples(sampleIndex).FilePath)))
                Next
            End If
        Next

        Return Samples.ToArray
    End Function
End Class

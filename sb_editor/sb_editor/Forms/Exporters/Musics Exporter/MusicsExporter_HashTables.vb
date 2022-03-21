Imports System.IO
Imports NAudio.Wave
Imports sb_editor.HashTablesBuilder

Partial Public Class MusicsExporter
    Private Sub BuildMusicHashTables(hashCodesCollection As SortedDictionary(Of String, UInteger))
        Dim numIteration As Integer = 0
        Dim musicDefinesFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "MFX_Defines.h")
        CreateMfxHashTable(musicDefinesFilePath, hashCodesCollection)
        For Each musicItem As KeyValuePair(Of String, UInteger) In hashCodesCollection
            'Update Title and progress bar
            BackgroundWorker.ReportProgress(Decimal.Divide(numIteration, hashCodesCollection.Count) * 100.0, "Appending Jump HashCodes: " & musicItem.Key)
            'Create jump files
            Dim jumpMarkersFilePath As String = Path.Combine(WorkingDirectory, "Music", "ESWork", musicItem.Key & ".jmp")
            If File.Exists(jumpMarkersFilePath) Then
                Dim jumpHashCodes As String() = textFileReaders.ReadJumpFile(jumpMarkersFilePath)
                'Append data
                Using outputFile As New StreamWriter(musicDefinesFilePath)
                    outputFile.WriteLine("")
                    outputFile.WriteLine("// Music Jump Codes For Level MFX_" & musicItem.Key)
                    For jumpHashCode As Integer = 0 To jumpHashCodes.Length - 1
                        Dim mfxHashCode As Short = musicItem.Value
                        Dim hashCode As UInteger = ((&H1BE And &HFFF) << 20) Or ((jumpHashCode And &HFF) << 8) Or ((mfxHashCode And &HFF) << 0)
                        outputFile.WriteLine("#define " & "JMP_" & jumpHashCodes(jumpHashCode) & " 0x" & Hex(hashCode))
                    Next
                End Using
            End If
            numIteration += 1
        Next

        'Create MFX Data
        Dim dataDictionary As Dictionary(Of UInteger, String()) = GetMfxDataDict(hashCodesCollection)
        Dim musicDataFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "MFX_Data.h")
        CreateMfxData(musicDataFilePath, dataDictionary)

        'Create Valid list
        Invoke(Sub() ProgressBar1.Value = 0)
        numIteration = 0
        Dim musicValidListFilePath As String = Path.Combine(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "MFX_ValidList.h")
        Dim jumpHashCodesDictionary As New Dictionary(Of UInteger, String)
        For Each musicItem As KeyValuePair(Of String, UInteger) In hashCodesCollection
            'Update title bar and progress bar
            BackgroundWorker.ReportProgress(Decimal.Divide(numIteration, hashCodesCollection.Count) * 100.0, "Creating MFX Valid List: " & musicItem.Key)
            'Get all jump marker files and store it in the dictionary
            Dim jumpMarkersFilePath As String = Path.Combine(WorkingDirectory, "Music", "ESWork", musicItem.Key & ".jmp")
            If File.Exists(jumpMarkersFilePath) Then
                Dim jumpHashCodes As String() = textFileReaders.ReadJumpFile(jumpMarkersFilePath)
                For jumpHashCode As Integer = 0 To jumpHashCodes.Length - 1
                    Dim mfxHashCode As Short = musicItem.Value
                    Dim hashCode As UInteger = ((jumpHashCode And &HFF) << 8) Or ((mfxHashCode And &HFF) << 0)
                    jumpHashCodesDictionary.Add(hashCode, "JMP_" & jumpHashCodes(jumpHashCode))
                Next
            End If
            numIteration += 1
        Next
        CreateMfxValidList(musicValidListFilePath, jumpHashCodesDictionary)
    End Sub

    '*===============================================================================================
    '* GET DATA FOR MFX_DATA FILE
    '*===============================================================================================
    Private Function GetMfxDataDict(hashCodesCollection As SortedDictionary(Of String, UInteger)) As Dictionary(Of UInteger, String())
        Dim dictionaryData As New Dictionary(Of UInteger, String())

        'Dictionary Data
        For Each rowData As KeyValuePair(Of String, UInteger) In hashCodesCollection
            Dim filePath As String = Path.Combine(WorkingDirectory, "Music", rowData.Key & ".wav")
            Using waveReader As New WaveFileReader(filePath)
                Dim duration As Single = (waveReader.Length / waveReader.WaveFormat.AverageBytesPerSecond) + 0.0
                Dim stringDuration As String
                If duration Mod 1 = 0 Then
                    stringDuration = duration.ToString("F1", numericProvider)
                Else
                    stringDuration = duration.ToString("G7", numericProvider)
                End If
                dictionaryData.Add(rowData.Value, New String() {stringDuration & "f", "FALSE"})
            End Using
        Next

        Return dictionaryData
    End Function

    '*===============================================================================================
    '* MFX_Data.h
    '*===============================================================================================
    Private Sub CreateMfxData(filePath As String, mfxDict As Dictionary(Of UInteger, String()))
        Using outputFile As New StreamWriter(filePath)
            outputFile.WriteLine("// Music Data table from EuroSound 1")
            outputFile.WriteLine("// " & Date.Now.ToString("dddd, dd MMMM yyyy"))
            outputFile.WriteLine("")
            outputFile.WriteLine("typedef struct{")
            outputFile.WriteLine("	u32      MusicHashCode;")
            outputFile.WriteLine("	float    DurationInSeconds;")
            outputFile.WriteLine("	bool     Looping;")
            outputFile.WriteLine("} MusicDetails;")
            outputFile.WriteLine("")
            outputFile.WriteLine("MusicDetails MusicData[]={")
            For Each mfxItem In mfxDict
                Dim musicDataValues As String() = mfxItem.Value
                Dim hashCode As String = NumberToHex(mfxItem.Key + &H1BE00000)
                outputFile.WriteLine("	{" & hashCode & "," & musicDataValues(0) & "," & musicDataValues(1) & "},")
            Next
            outputFile.WriteLine("};")
        End Using
    End Sub

    '*===============================================================================================
    '* MFX_ValidList.h
    '*===============================================================================================
    Private Sub CreateMfxValidList(filePath As String, mfxDict As Dictionary(Of UInteger, String))
        Using outputFile As New StreamWriter(filePath)
            outputFile.WriteLine("s32 MFX_ValidList[]={")
            For Each mfxItem In mfxDict
                Dim hashCode As String = NumberToHex(mfxItem.Key + &H1BE00000)
                outputFile.WriteLine(hashCode & ",// " & mfxItem.Value)
            Next
            outputFile.WriteLine("-1};")
        End Using
    End Sub
End Class

Imports NAudio.Wave

Partial Public Class MusicsExporter
    Private Sub BuildMusicHashTables()
        Dim numIteration As Integer = 0
        Dim musicDefinesFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "MFX_Defines.h")
        hashTablesFunctions.CreateMfxHashTable(musicDefinesFilePath, hashCodesCollection)
        For Each musicItem As KeyValuePair(Of String, UInteger) In hashCodesCollection
            'Update Title and progress bar
            Invoke(Sub() Text = "Appending Jump HashCodes: " & musicItem.Key)
            BackgroundWorker.ReportProgress(Decimal.Divide(numIteration, hashCodesCollection.Count) * 100.0)
            'Create jump files
            Dim jumpMarkersFilePath As String = fso.BuildPath(WorkingDirectory, "Music\ESWork\" & musicItem.Key & ".jmp")
            If fso.FileExists(jumpMarkersFilePath) Then
                Dim jumpHashCodes As String() = textFileReaders.ReadJumpFile(jumpMarkersFilePath)
                'Append data
                FileOpen(1, musicDefinesFilePath, OpenMode.Append)
                PrintLine(1, "")
                PrintLine(1, "// Music Jump Codes For Level MFX_" & musicItem.Key)
                For jumpHashCode As Integer = 0 To jumpHashCodes.Length - 1
                    Dim mfxHashCode As Short = musicItem.Value
                    Dim hashCode As UInteger = ((&H1BE And &HFFF) << 20) Or ((jumpHashCode And &HFF) << 8) Or ((mfxHashCode And &HFF) << 0)
                    PrintLine(1, "#define " & "JMP_" & jumpHashCodes(jumpHashCode) & " 0x" & Hex(hashCode))
                Next
                FileClose(1)
            End If
            numIteration += 1
        Next

        'Create MFX Data
        Dim dataDictionary As Dictionary(Of UInteger, String()) = GetMfxDataDict()
        Dim musicDataFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "MFX_Data.h")
        hashTablesFunctions.CreateMfxData(musicDataFilePath, dataDictionary)

        'Create Valid list
        Invoke(Sub() ProgressBar1.Maximum = hashCodesCollection.Count)
        Invoke(Sub() ProgressBar1.Value = 0)
        Dim musicValidListFilePath As String = fso.BuildPath(ProjectSettingsFile.MiscProps.HashCodeFileFolder, "MFX_ValidList.h")
        Dim jumpHashCodesDictionary As New Dictionary(Of UInteger, String)
        For Each musicItem As KeyValuePair(Of String, UInteger) In hashCodesCollection
            Invoke(Sub() Text = "Creating MFX Valid List: " & musicItem.Key)
            Dim jumpMarkersFilePath As String = fso.BuildPath(WorkingDirectory, "Music\ESWork\" & musicItem.Key & ".jmp")
            If fso.FileExists(jumpMarkersFilePath) Then
                Dim jumpHashCodes As String() = textFileReaders.ReadJumpFile(jumpMarkersFilePath)
                For jumpHashCode As Integer = 0 To jumpHashCodes.Length - 1
                    Dim mfxHashCode As Short = musicItem.Value
                    Dim hashCode As UInteger = ((jumpHashCode And &HFF) << 8) Or ((mfxHashCode And &HFF) << 0)
                    jumpHashCodesDictionary.Add(hashCode, "JMP_" & jumpHashCodes(jumpHashCode))
                Next
            End If
        Next
        hashTablesFunctions.CreateMfxValidList(musicValidListFilePath, jumpHashCodesDictionary)
    End Sub

    '*===============================================================================================
    '* GET DATA FOR MFX_DATA FILE
    '*===============================================================================================
    Private Function GetMfxDataDict() As Dictionary(Of UInteger, String())
        Dim dictionaryData As New Dictionary(Of UInteger, String())

        'Dictionary Data
        For Each rowData As KeyValuePair(Of String, UInteger) In hashCodesCollection
            Dim filePath As String = fso.BuildPath(WorkingDirectory, "Music\" & rowData.Key & ".wav")
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
End Class

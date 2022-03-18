Imports System.IO
Imports sb_editor.ParsersObjects

Partial Public Class MusicMaker
    Private Function GetMfxFiles(musicFolder As String) As String()
        Dim filesToRead As New HashSet(Of String)

        'Check for new files
        If Directory.Exists(musicFolder) Then
            Dim availableMarkerFiles As String() = Directory.GetFiles(musicFolder, "*.mrk", SearchOption.TopDirectoryOnly)
            For itemIndex As Integer = 0 To availableMarkerFiles.Length - 1
                Dim fileName As String = Path.GetFileNameWithoutExtension(availableMarkerFiles(itemIndex))
                'Check that the markers and wave file exists, if not discard this "item"
                Dim mrkFilePath As String = Path.Combine(WorkingDirectory, "Music", fileName & ".mrk")
                Dim waveFilePath As String = Path.Combine(WorkingDirectory, "Music", fileName & ".wav")
                If File.Exists(mrkFilePath) AndAlso File.Exists(waveFilePath) Then
                    filesToRead.Add(fileName)
                End If
            Next
        End If

        'Get Array
        Dim filesArray As String() = filesToRead.ToArray
        Array.Sort(filesArray)

        Return filesArray
    End Function

    Private Sub GetMusicFilesData(filesToRead As String())
        If filesToRead.Length > 0 Then
            'Update the MFX Files File. (Used to get the hashcodes in the exporter) 
            FileOpen(2, Path.Combine(WorkingDirectory, "Music", "ESData", "MFXFiles.txt"), OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
            PrintLine(2, "#MFXFiles")
            'Read files and add items to list
            Dim itemDataGrid As String() = New String() {"", "100", "New File", "0", "Output", "Output", "", "0"}
            ListView_MusicFiles.BeginUpdate()
            ListView_MusicFiles.Items.Clear()
            For index As Integer = 0 To filesToRead.Count - 1
                'Get music files path
                Dim mrkFilePath As String = Path.Combine(WorkingDirectory, "Music", filesToRead(index) & ".mrk")
                Dim waveFilePath As String = Path.Combine(WorkingDirectory, "Music", filesToRead(index) & ".wav")
                'Add item to text file
                PrintLine(2, filesToRead(index))
                'Read properties file
                Dim mfxPropsFilePath As String = Path.Combine(WorkingDirectory, "Music", "ESData", filesToRead(index) & ".txt")
                Dim mfxFileData As MfxFile
                If File.Exists(mfxPropsFilePath) Then
                    mfxFileData = textFileReaders.ReadMfxFile(mfxPropsFilePath)
                    itemDataGrid(2) = "No Errors"
                    itemDataGrid(4) = "OK"
                    itemDataGrid(5) = "OK"
                Else
                    'Create new item
                    mfxFileData = New MfxFile With {
                        .Volume = 100,
                        .HashCode = MFXHashCodeNumber,
                        .MidiFileLastOutput = 99,
                        .WavFileLastOutput = 99
                    }
                    MFXHashCodeNumber += 1
                    'Write file 
                    writers.WriteMfxFile(mfxPropsFilePath, mfxFileData)
                End If
                itemDataGrid(0) = filesToRead(index)
                itemDataGrid(1) = mfxFileData.Volume
                itemDataGrid(3) = mfxFileData.HashCode
                itemDataGrid(6) = "HC" & Hex(mfxFileData.HashCode).PadLeft(6, "0"c) & ".SFX"
                itemDataGrid(7) = mfxFileData.UserValue
                ListView_MusicFiles.Items.Add(New ListViewItem(itemDataGrid))

            Next
            ListView_MusicFiles.EndUpdate()
            'End dependencies block
            PrintLine(2, "#END")
            FileClose(2)
        End If
    End Sub

    Private Function ListViewToDataTable() As DataTable
        Dim samplesData As New DataTable
        samplesData.Columns.Add("FileName")
        samplesData.Columns.Add("Volume")
        samplesData.Columns.Add("Hashcode")

        'Add item
        Dim listviewEnum As IEnumerator = ListView_MusicFiles.Items.GetEnumerator
        While listviewEnum.MoveNext
            Dim cells As String() = New String(2) {}
            Dim currentItem As ListViewItem = listviewEnum.Current
            If StrComp(currentItem.SubItems(4).Text, "Output") = 0 Or StrComp(currentItem.SubItems(5).Text, "Output") = 0 Then
                cells(0) = currentItem.Text
                cells(1) = currentItem.SubItems(1).Text
                cells(2) = currentItem.SubItems(3).Text
                samplesData.Rows.Add(cells)
            End If
        End While

        'Sort table
        Dim dv As DataView = samplesData.DefaultView
        dv.Sort = "FileName asc"

        Return dv.ToTable
    End Function

    Private Function AppendJumpHashCodes(hashTableFilePath As String, hashCodesDict As SortedDictionary(Of String, UInteger)) As List(Of String)
        'Declare variables
        Dim jumpDefinesList As New List(Of String)
        Dim jumpTextFilesPath As String = Path.Combine(WorkingDirectory, "Music", "ESWork")
        Dim jumpTextFiles As String() = Directory.GetFiles(jumpTextFilesPath, "*.jmp", SearchOption.TopDirectoryOnly)

        'Append text
        For index As Integer = 0 To jumpTextFiles.Length - 1
            If File.Exists(jumpTextFiles(index)) Then
                Dim jumpHashCodes As String() = textFileReaders.ReadJumpFile(jumpTextFiles(index))
                Dim mfxName As String = Path.GetFileNameWithoutExtension(jumpTextFiles(index))
                FileOpen(1, hashTableFilePath, OpenMode.Append)
                PrintLine(1, "")
                PrintLine(1, "// Music Jump Codes For Level MFX_" & mfxName)
                For jumpHashCode As Short = 0 To jumpHashCodes.Length - 1
                    Dim mfxHashCode As Short = hashCodesDict(mfxName)
                    Dim hashCode As UInteger = ((&H1BE And &HFFF) << 20) Or ((jumpHashCode And &HFF) << 8) Or ((mfxHashCode And &HFF) << 0)
                    Dim hashCodeLabel As String = "JMP_" & jumpHashCodes(jumpHashCode)
                    PrintLine(1, "#define " & hashCodeLabel & " 0x" & Hex(hashCode))
                    jumpDefinesList.Add(hashCodeLabel)
                Next
                FileClose(1)
            End If
        Next
        AppendJumpHashCodes = jumpDefinesList
    End Function
End Class

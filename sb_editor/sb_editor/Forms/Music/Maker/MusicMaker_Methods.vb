Imports System.IO

Partial Public Class MusicMaker
    Private Sub GetMusicFilesData()
        'Ensure that the Music folder exists
        If fso.FolderExists(musicStuffPath) Then
            Dim itemDataGrid As String() = New String() {"", "100", "New File", "0", "Output", "Output", "", "0"}
            'Check for new files
            Dim filesToRead As New List(Of String)
            Dim availableMarkerFiles As String() = Directory.GetFiles(musicStuffPath, "*.mrk", SearchOption.TopDirectoryOnly)
            For itemIndex As Integer = 0 To availableMarkerFiles.Length - 1
                Dim fileName As String = GetOnlyFileName(availableMarkerFiles(itemIndex))
                If Not filesToRead.Contains(fileName) Then
                    filesToRead.Add(fileName)
                End If
            Next
            filesToRead.Sort()

            'Read files and add items to list
            ListView_MusicFiles.BeginUpdate()
            ListView_MusicFiles.Items.Clear()
            For index As Integer = 0 To filesToRead.Count - 1
                'Get music files path
                Dim mrkFilePath As String = fso.BuildPath(WorkingDirectory, "Music\" & filesToRead(index) & ".mrk")
                Dim waveFilePath As String = fso.BuildPath(WorkingDirectory, "Music\" & filesToRead(index) & ".wav")
                'Add item to list
                If fso.FileExists(mrkFilePath) AndAlso fso.FileExists(waveFilePath) Then
                    'Read properties file
                    Dim mfxPropsFilePath As String = fso.BuildPath(WorkingDirectory, "Music\ESData\" & filesToRead(index) & ".txt")
                    Dim mfxFileData As MfxFile
                    If fso.FileExists(mfxPropsFilePath) Then
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
                End If
            Next
            ListView_MusicFiles.EndUpdate()
            'Create a list of the included files
            CreateMfxFilesList(fso.BuildPath(WorkingDirectory, "Music\ESData\MFXFiles.txt"))
        End If
    End Sub

    Private Sub CreateMfxFilesList(filePath As String)
        'Update file
        Try
            FileOpen(1, filePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
            PrintLine(1, "#MFXFiles")
            'Iterate over list items
            For Each musicFile As ListViewItem In ListView_MusicFiles.Items
                PrintLine(1, musicFile.Text)
            Next
            'End dependencies block
            PrintLine(1, "#END")
            FileClose(1)
        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly + vbCritical, "Error")
        End Try
    End Sub

    Private Function GetOutputPlatforms() As List(Of String)
        Dim outputFormats As New List(Of String)
        If StrComp(ComboBox_OutputFormat.SelectedItem, "All") = 0 Then
            outputFormats.Add("PlayStation2")
            outputFormats.Add("GameCube")
            outputFormats.Add("PC")
            outputFormats.Add("X Box")
        Else
            outputFormats.Add(ComboBox_OutputFormat.SelectedItem)
        End If
        GetOutputPlatforms = outputFormats
    End Function

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
        Dim jumpTextFilesPath As String = fso.BuildPath(WorkingDirectory, "Music\ESWork\")
        Dim jumpTextFiles As String() = Directory.GetFiles(jumpTextFilesPath, "*.jmp", SearchOption.TopDirectoryOnly)

        'Append text
        For index As Integer = 0 To jumpTextFiles.Length - 1
            If fso.FileExists(jumpTextFiles(index)) Then
                Dim jumpHashCodes As String() = textFileReaders.ReadJumpFile(jumpTextFiles(index))
                Dim mfxName As String = GetOnlyFileName(jumpTextFiles(index))
                FileOpen(1, hashTableFilePath, OpenMode.Append)
                PrintLine(1, "")
                PrintLine(1, "// Music Jump Codes For Level MFX_" & mfxName)
                For jumpHashCode As Integer = 0 To jumpHashCodes.Length - 1
                    Dim jumpIndex As Short = jumpHashCode
                    Dim mfxHashCode As Short = hashCodesDict(mfxName)
                    Dim hashCode As UInteger = ((&H1BE And &HFFF) << 20) Or ((jumpIndex And &HFF) << 8) Or ((mfxHashCode And &HFF) << 0)
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

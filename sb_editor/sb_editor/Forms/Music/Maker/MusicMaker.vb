Imports System.IO
Imports EngineXMarkersTool
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses
Imports sb_editor.WritersClasses
Imports sb_editor.HashTablesBuilder

Partial Public Class MusicMaker
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private ReadOnly textFileReaders As New FileParsers
    Private ReadOnly writers As New FileWriters
    Private currentReadedFiles As String()

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub MusicMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Directory.Exists(WorkingDirectory) Then
            'Ensure that the data and work directory exists
            Directory.CreateDirectory(Path.Combine(WorkingDirectory, "Music"))
            Directory.CreateDirectory(Path.Combine(WorkingDirectory, "Music", "ESData"))
            Directory.CreateDirectory(Path.Combine(WorkingDirectory, "Music", "ESWork"))
        End If

        'Add formats to combobox
        ComboBox_OutputFormat.BeginUpdate()
        ComboBox_OutputFormat.Items.AddRange(ProjectSettingsFile.sampleRateFormats.Keys.ToArray)
        ComboBox_OutputFormat.Items.Add("All")
        ComboBox_OutputFormat.SelectedIndex = ComboBox_OutputFormat.Items.Count - 1
        ComboBox_OutputFormat.EndUpdate()

        'Print available mfx files
        currentReadedFiles = GetMfxFiles(Path.Combine(WorkingDirectory, "Music"))
        GetMusicFilesData(currentReadedFiles)
    End Sub

    Private Sub ListView_MusicFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_MusicFiles.SelectedIndexChanged
        If ListView_MusicFiles.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = ListView_MusicFiles.SelectedItems(0)
            Numeric_Volume.Value = selectedItem.SubItems(1).Text
            TextBox_UserValue.Text = selectedItem.SubItems(7).Text
        End If
    End Sub

    Private Sub Numeric_Volume_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_Volume.ValueChanged
        If ListView_MusicFiles.SelectedItems.Count > 0 Then
            'Update listview
            Dim selectedItem As ListViewItem = ListView_MusicFiles.SelectedItems(0)
            selectedItem.SubItems(1).Text = Numeric_Volume.Value
            'Read and update mfx file
            Dim mfxFilePath As String = Path.Combine(WorkingDirectory, "Music", "ESData", selectedItem.Text & ".txt")
            If File.Exists(mfxFilePath) Then
                Dim mfxFileData As MfxFile = textFileReaders.ReadMfxFile(mfxFilePath)
                If mfxFileData.Volume <> Numeric_Volume.Value Then
                    mfxFileData.Volume = Numeric_Volume.Value
                    'Update Text File
                    writers.WriteMfxFile(mfxFilePath, mfxFileData)
                End If
            End If

        End If
    End Sub

    Private Sub Button_UpdateFiles_Click(sender As Object, e As EventArgs) Handles Button_UpdateFiles.Click
        'Get Files in folder
        Dim filesInFolder As String() = GetMfxFiles(Path.Combine(WorkingDirectory, "Music"))
        'Check if we really need to reload all
        Dim needToReload As Boolean = Not New HashSet(Of String)(currentReadedFiles).SetEquals(filesInFolder)
        If needToReload Then
            currentReadedFiles = filesInFolder
            GetMusicFilesData(filesInFolder)
        End If
    End Sub

    Private Sub Button_Output_Click(sender As Object, e As EventArgs) Handles Button_Output.Click
        'Get files and platforms to output
        Dim outputTable As DataTable = ListViewToDataTable()
        'Open Exporter Form
        If outputTable.Rows.Count > 0 Then
            Dim exporterTool As New MusicsExporter(outputTable, CheckBox_MarkerFileOnly.Checked, Me)
            exporterTool.ShowDialog()
            Show()
        End If
    End Sub

    Private Sub Button_ForceSelected_Click(sender As Object, e As EventArgs) Handles Button_ForceSelected.Click
        If ListView_MusicFiles.SelectedItems.Count > 0 Then
            For itemIndex As Integer = 0 To ListView_MusicFiles.SelectedItems.Count - 1
                Dim selectedItem As ListViewItem = ListView_MusicFiles.SelectedItems(itemIndex)
                selectedItem.SubItems(4).Text = "Output"
                selectedItem.SubItems(5).Text = "Output"
            Next
        End If
    End Sub

    Private Sub Button_ForceOutput_Click(sender As Object, e As EventArgs) Handles Button_ForceOutput.Click
        ListView_MusicFiles.BeginUpdate()
        For Each listviewItem As ListViewItem In ListView_MusicFiles.Items
            listviewItem.SubItems(4).Text = "Output"
            listviewItem.SubItems(5).Text = "Output"
        Next
        ListView_MusicFiles.EndUpdate()
    End Sub

    Private Sub Button_RemapHashCodes_Click(sender As Object, e As EventArgs) Handles Button_RemapHashCodes.Click
        'Reset global variable
        MFXHashCodeNumber = 1
        'Update Listview and text files
        ListView_MusicFiles.BeginUpdate()
        For Each listviewItem As ListViewItem In ListView_MusicFiles.Items
            'Read text file
            Dim mfxFilePath As String = Path.Combine(WorkingDirectory, "Music", "ESData", listviewItem.Text & ".txt")
            Dim mfxFileData As MfxFile = textFileReaders.ReadMfxFile(mfxFilePath)
            mfxFileData.HashCode = MFXHashCodeNumber
            'Update Text File
            writers.WriteMfxFile(mfxFilePath, mfxFileData)
            'Update listview
            listviewItem.SubItems(3).Text = MFXHashCodeNumber
            listviewItem.SubItems(6).Text = "HC" & Hex(MFXHashCodeNumber).PadLeft(6, "0"c) & ".SFX"
            'Update Global var
            MFXHashCodeNumber += 1
        Next
        writers.UpdateMiscFile(Path.Combine(WorkingDirectory, "System", "Misc.txt"))
        ListView_MusicFiles.EndUpdate()
    End Sub

    Private Sub Button_VeryfyMfx_Click(sender As Object, e As EventArgs) Handles Button_VeryfyMfx.Click
        'Ensure that we have musics to export
        If ListView_MusicFiles.Items.Count > 0 Then
            'Get hashcodes dictionary
            Dim hashCodesDict As New SortedDictionary(Of String, UInteger)
            For Each listItem As ListViewItem In ListView_MusicFiles.Items
                hashCodesDict.Add(listItem.Text, listItem.SubItems(3).Text)
            Next
            'Build temporal file
            Dim testHashTableFilePath As String = Path.Combine(WorkingDirectory, "System", "Temp_MFX_Defines.h")
            CreateMfxHashTable(testHashTableFilePath, hashCodesDict)
            'Merge jump hashcodes
            Dim markerFunctions As New ExMarkersTool
            Dim definesInHashTable As List(Of String) = AppendJumpHashCodes(testHashTableFilePath, hashCodesDict)
            Dim definesInFolder As List(Of String) = markerFunctions.GetJumpMakersList(Path.Combine(WorkingDirectory, "Music"))
            'Get missing defines
            Dim missingDefines As IEnumerable(Of String) = definesInFolder.Except(definesInHashTable)
            'Merge missing defines into a single string
            Dim missingFilesString As String = String.Join(vbCrLf, missingDefines.ToArray)
            'Inform user
            Dim messageLength As Integer = Len(missingFilesString)
            If messageLength > 0 Then
                MsgBox("The following defines are missing from the new MFX_Defines.h file:" & vbCrLf & vbCrLf & missingFilesString.Substring(0, Math.Min(messageLength, 953)), vbOKOnly + vbCritical, "Missing Defines in New MFX_Defines.h File!")
            Else
                MsgBox("All checks out OK wih MFX_Defines.h file", vbOKOnly + vbInformation, "MFX_Defines.h File OK.")
            End If
        End If
    End Sub

    Private Sub TextBox_UserValue_TextChanged(sender As Object, e As EventArgs) Handles TextBox_UserValue.TextChanged
        If IsNumeric(TextBox_UserValue.Text) AndAlso TextBox_UserValue.Text < Single.MaxValue Then
            TextBox_UserValue.Text = TextBox_UserValue.Text.ToString(numericProvider)
            If ListView_MusicFiles.SelectedItems.Count > 0 Then
                'Update ListView
                Dim selectedItem As ListViewItem = ListView_MusicFiles.SelectedItems(0)
                Dim userValue As UInteger = TextBox_UserValue.Text
                selectedItem.SubItems(7).Text = userValue
                'Read and update mfx file
                Dim mfxFilePath As String = Path.Combine(WorkingDirectory, "Music", "ESData", selectedItem.Text & ".txt")
                If File.Exists(mfxFilePath) Then
                    Dim mfxFileData As MfxFile = textFileReaders.ReadMfxFile(mfxFilePath)
                    If mfxFileData.UserValue <> userValue Then
                        mfxFileData.UserValue = userValue
                        'Update Text File
                        writers.WriteMfxFile(mfxFilePath, mfxFileData)
                    End If
                End If
            End If
        Else
            TextBox_UserValue.Text = "0"
        End If
    End Sub

    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        'Close Form
        Close()
    End Sub
End Class
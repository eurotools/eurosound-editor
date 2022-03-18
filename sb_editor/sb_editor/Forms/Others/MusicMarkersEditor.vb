Imports System.IO
Imports sb_editor.MarkerFunctions

Public Class MusicMarkersEditor
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    Private ReadOnly markersDictionary As New SortedDictionary(Of UInteger, MarkerObject)
    Private ReadOnly markersFunctions As New MarkerFiles
    Private waveFileName As String = ""
    Private promptToSave As Boolean = False
    Private loopMarker As String = ""

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub MusicMarkersEditor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            If promptToSave AndAlso GroupBox_MarkerTypes.Enabled Then
                Dim userAnswer As DialogResult = MsgBox("Are you sure you wish to quit without saving?", vbOKCancel + vbQuestion, "Confirm Quit")
                If userAnswer = DialogResult.Cancel Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Function MarkerExistsInDictionary(markerName As String) As Boolean
        Dim markerExists As Boolean = False
        For Each markerToCheck As KeyValuePair(Of UInteger, MarkerObject) In markersDictionary
            If StrComp(markerToCheck.Value.MarkerName, markerName) = 0 Then
                markerExists = True
                Exit For
            End If
        Next
        Return markerExists
    End Function

    '*===============================================================================================
    '* BUTTONS EVENTS
    '*===============================================================================================
    Private Sub Button_AddStartMarker_Click(sender As Object, e As EventArgs) Handles Button_AddStartMarker.Click
        If markersDictionary.ContainsKey(Numeric_MarkerPosition.Value) Then
            MsgBox("Two markers cannot contain the same position.", vbOKOnly + vbCritical, "EuroSound")
        Else
            Dim markerName As String = waveFileName & "_START"
            Dim markerIndex As Integer = 1
            If MarkerExistsInDictionary(markerName) Then
                markerName = waveFileName & "_Random_Start" & markerIndex
                While MarkerExistsInDictionary(markerName)
                    markerIndex += 1
                    markerName = waveFileName & "_Random_Start" & markerIndex
                End While
            End If
            'Create marker object
            Dim markerObject As New MarkerObject With {
                .MarkerPosition = Numeric_MarkerPosition.Value,
                .MarkerName = markerName,
                .MarkerType = 10
            }
            markersDictionary.Add(Numeric_MarkerPosition.Value, markerObject)
            PrintMarkers()
        End If
        Numeric_MarkerPosition.Value = 0
    End Sub

    Private Sub Button_AddLoopMarker_Click(sender As Object, e As EventArgs) Handles Button_AddLoopMarker.Click
        If markersDictionary.ContainsKey(Numeric_MarkerPosition.Value) Then
            MsgBox("Two markers cannot contain the same position.", vbOKOnly + vbCritical, "EuroSound")
        Else
            Dim markerName As String = waveFileName & "_LOOP"
            loopMarker = markerName
            If MarkerExistsInDictionary(markerName) Then
                MsgBox("This music already contains a LOOP marker.", vbOKOnly + vbCritical, "EuroSound")
            Else
                'Create marker object
                Dim markerObject As New MarkerObject With {
                    .MarkerPosition = Numeric_MarkerPosition.Value,
                    .MarkerName = markerName,
                    .MarkerType = 10
                }
                markersDictionary.Add(Numeric_MarkerPosition.Value, markerObject)
                PrintMarkers()
            End If
        End If
        Numeric_MarkerPosition.Value = 0
    End Sub

    Private Sub Button_AddGotoMarker_Click(sender As Object, e As EventArgs) Handles Button_AddGotoMarker.Click
        If loopMarker = "" Then
            MsgBox("The GOTO marker could not be added because a LOOP marker has not been found.", vbOKOnly + vbCritical, "EuroSound")
        Else
            If markersDictionary.ContainsKey(Numeric_MarkerPosition.Value) Then
                MsgBox("Two markers cannot contain the same position.", vbOKOnly + vbCritical, "EuroSound")
            Else
                Dim markerName As String = "GOTO_" & waveFileName & "_LOOP"
                If MarkerExistsInDictionary(markerName) Then
                    MsgBox("This music already contains a GOTO marker.", vbOKOnly + vbCritical, "EuroSound")
                Else
                    'Create marker object
                    Dim markerObject As New MarkerObject With {
                        .MarkerPosition = Numeric_MarkerPosition.Value,
                        .MarkerName = markerName,
                        .GotoMarker = loopMarker,
                        .MarkerType = 7
                    }
                    markersDictionary.Add(Numeric_MarkerPosition.Value, markerObject)
                    PrintMarkers()
                End If
            End If
        End If
        Numeric_MarkerPosition.Value = 0
    End Sub

    Private Sub Button_AddEndMarker_Click(sender As Object, e As EventArgs) Handles Button_AddEndMarker.Click
        If markersDictionary.ContainsKey(Numeric_MarkerPosition.Value) Then
            MsgBox("Two markers cannot contain the same position.", vbOKOnly + vbCritical, "EuroSound")
        Else
            If MarkerExistsInDictionary("*") Then
                MsgBox("This music already contains a END marker.", vbOKOnly + vbCritical, "EuroSound")
            Else
                'Create marker object
                Dim markerObject As New MarkerObject With {
                    .MarkerPosition = Numeric_MarkerPosition.Value,
                    .MarkerType = 9
                }
                markersDictionary.Add(Numeric_MarkerPosition.Value, markerObject)
                PrintMarkers()
            End If
        End If
        Numeric_MarkerPosition.Value = 0
    End Sub

    Private Sub Button_RemoveSelected_Click(sender As Object, e As EventArgs) Handles Button_RemoveSelected.Click
        If ListView_Markers.SelectedItems.Count > 0 Then
            Dim selectedListItem As ListViewItem = ListView_Markers.SelectedItems(0)
            'Check if we are removing a Loop marker to also remove the goto marker
            If selectedListItem.Text.Contains("_LOOP") Then
                For Each marker As KeyValuePair(Of UInteger, MarkerObject) In markersDictionary
                    If marker.Value.MarkerType = 7 Then
                        'Remove from ListView
                        For Each listItem As ListViewItem In ListView_Markers.Items
                            If StrComp(selectedListItem.Text, marker.Value.GotoMarker) = 0 Then
                                'Remove from dictionary and listview
                                markersDictionary.Remove(marker.Key)
                                'Reset string
                                loopMarker = ""
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next
            End If
            'Remove Item
            markersDictionary.Remove(selectedListItem.SubItems(1).Text)
            PrintMarkers()
        End If
    End Sub

    Private Sub Button_RenameMarkers_Click(sender As Object, e As EventArgs) Handles Button_RenameMarkers.Click
        'Get objects
        Dim iterations = 0
        For Each MarkerToCheck As KeyValuePair(Of UInteger, MarkerObject) In markersDictionary
            If MarkerToCheck.Value.MarkerType = 10 Then
                If MarkerToCheck.Value.MarkerName.EndsWith("_START") Or MarkerToCheck.Value.MarkerName.Contains("_Random_Start") Then
                    If MarkerToCheck.Value.MarkerType = 10 Then
                        If iterations < 1 Then
                            MarkerToCheck.Value.MarkerName = waveFileName & "_START"
                        Else
                            MarkerToCheck.Value.MarkerName = waveFileName & "_Random_Start" & iterations
                        End If
                        iterations += 1
                    End If
                End If
            End If
        Next
        PrintMarkers()
    End Sub

    '*===============================================================================================
    '* FILES EVENTS
    '*===============================================================================================
    Private Sub Button_SetFile_Click(sender As Object, e As EventArgs) Handles Button_SetFile.Click
        Dim waveFileBrowser As DialogResult = OpenFileDialog_WaveFile.ShowDialog
        If waveFileBrowser = DialogResult.OK Then
            TextBox_MusicFilePath.Text = OpenFileDialog_WaveFile.FileName
            waveFileName = Path.GetFileNameWithoutExtension(TextBox_MusicFilePath.Text)
            GroupBox_MarkerTypes.Enabled = True
            GroupBox_Options.Enabled = True
            ListView_Markers.Enabled = True
        End If
    End Sub

    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        promptToSave = False
        If File.Exists(TextBox_MusicFilePath.Text) Then
            markersFunctions.CreateMusicMarkerFile(Path.ChangeExtension(TextBox_MusicFilePath.Text, ".mrk"), markersDictionary)
        End If
        Close()
    End Sub

    Private Sub Button_Close_Click(sender As Object, e As EventArgs) Handles Button_Close.Click
        promptToSave = True
        Close()
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Sub PrintMarkers()
        ListView_Markers.Items.Clear()
        ListView_Markers.BeginUpdate()
        'Print markers
        For Each markerToPrint As KeyValuePair(Of UInteger, MarkerObject) In markersDictionary
            Dim markerObject As MarkerObject = markerToPrint.Value
            ListView_Markers.Items.Add(New ListViewItem(New String() {markerObject.MarkerName, markerObject.MarkerPosition, markerObject.GotoMarker}))
        Next
        ListView_Markers.EndUpdate()
    End Sub
End Class
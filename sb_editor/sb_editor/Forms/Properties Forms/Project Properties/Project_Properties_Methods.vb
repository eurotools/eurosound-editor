Imports IniFileFunctions

Partial Public Class Project_Properties
    Private Sub CopyArrayToListView(lvw As ListView, data As String(,))
        Dim max_row As Integer = data.GetUpperBound(0)
        Dim max_col As Integer = data.GetUpperBound(1)
        For row As Integer = 0 To max_row
            Dim new_item As ListViewItem = lvw.Items.Add(data(row, 0))
            For col As Integer = 1 To max_col
                new_item.SubItems.Add(data(row, col))
            Next
        Next
    End Sub

    Public Function GetColumn(matrix As String(,), columnNumber As Integer) As String()
        Dim matrixLength As Integer = matrix.GetLength(0) - 1
        Dim columnArray As String() = New String(matrixLength) {}
        For i As Integer = 0 To matrixLength
            columnArray(i) = matrix(i, columnNumber)
        Next
        Return columnArray
    End Function

    Private Sub PrintFormatRates(selectedPlatform As String)
        'Ensure that the dictionary contains this key
        If ProjectSettingsFile.sampleRateFormats.ContainsKey(selectedPlatform) Then
            'Clear listview items
            ListView_SampleRateValues.Items.Clear()
            'Get array
            Dim SampleRates As Dictionary(Of String, UInteger) = ProjectSettingsFile.sampleRateFormats(selectedPlatform)
            'Ensure that we have data stored
            If SampleRates IsNot Nothing AndAlso SampleRates.Count > 0 Then
                For Each sampleRateItem As KeyValuePair(Of String, UInteger) In SampleRates
                    'Add sample rates values
                    Dim formatitem As New ListViewItem(New String() {sampleRateItem.Key, sampleRateItem.Value})
                    ListView_SampleRateValues.Items.Add(formatitem)
                Next
            End If
        End If
    End Sub

    Private Function ParseListViewToMatrix() As String(,)
        'Parse ListView to a matrix
        Dim countItems As Integer = ListView_Formats.Items.Count - 1
        Dim columnsCount As Integer = ListView_Formats.Columns.Count - 1
        Dim availableFormats(countItems, columnsCount) As String
        For itemIndex As Integer = 0 To countItems
            Dim currentItem As ListViewItem = ListView_Formats.Items(itemIndex)
            For subItemIndex As Integer = 0 To columnsCount
                availableFormats(itemIndex, subItemIndex) = currentItem.SubItems(subItemIndex).Text
            Next
        Next
        Return availableFormats
    End Function

    Private Sub SaveIniFile()
        Dim iniFunctions As New IniFile(SysFileProjectIniPath)
        iniFunctions.Write("Edit_Wavs_With", TextBox_EditWavs.Text, "Form7_Misc")
        iniFunctions.Write("TextEditor", TextBox_TextEditor.Text, "PropertiesForm")
        iniFunctions.Write("Prefix_HT_Sound", Convert.ToByte(CheckBox_PrefixHashCodes.Checked), "PropertiesForm")
        iniFunctions.Write("PlayStationSize", Numeric_PlayStationMaxSize.Value, "PropertiesForm")
        iniFunctions.Write("PCSize", Numeric_PcMaxSize.Value, "PropertiesForm")
        iniFunctions.Write("GameCubeSize", Numeric_GameCubeMaxSize.Value, "PropertiesForm")
        iniFunctions.Write("XBoxSize", Numeric_XboxMaxSize.Value, "PropertiesForm")
    End Sub
End Class

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
        If propsFileData.sampleRateFormats.ContainsKey(selectedPlatform) Then
            'Clear listview items
            ListView_SampleRateValues.Items.Clear()
            'Get array
            Dim SampleRates As Dictionary(Of String, UInteger) = propsFileData.sampleRateFormats(selectedPlatform)
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

    '*===============================================================================================
    '* PROPERTIES FILE FUNCTION
    '*===============================================================================================
    Private Sub SavePropertiesFile()
        'Replace current file   
        Dim headerData As New FileHeader
        Dim headerLib As New FileParsers

        'Get creation time if file exists
        Dim created = Date.Now.ToString(filesDateFormat)
        If fso.FileExists(SysFileProperties) Then
            headerData = headerLib.GetFileHeaderInfo(SysFileProperties)
            headerData.LastModify = created
            headerData.LastModifyBy = EuroSoundUser
        Else
            headerData.FirstCreated = created
            headerData.CreatedBy = EuroSoundUser
            headerData.LastModify = created
            headerData.LastModifyBy = EuroSoundUser
        End If

        'Update file
        FileOpen(1, SysFileProperties, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "## EuroSound Properties File")
        PrintLine(1, String.Join(" ", "## First Created ...", headerData.FirstCreated))
        PrintLine(1, "## Created By ... " & headerData.CreatedBy)
        PrintLine(1, String.Join(" ", "## Last Modified ...", headerData.LastModify))
        PrintLine(1, "## Last Modified By ... " & headerData.LastModifyBy)
        PrintLine(1, "")

        'Print Available formats
        PrintLine(1, "#AvailableFormats")
        PrintLine(1, " " & ListView_Formats.Items.Count)
        For Col As Integer = 0 To 2
            For index As Integer = 0 To ListView_Formats.Items.Count - 1
                PrintLine(1, ListView_Formats.Items(index).SubItems(Col).Text)
            Next
        Next

        'End Available formats block
        PrintLine(1, "#END")
        PrintLine(1, "")

        'Print Available ReSample Rates
        PrintLine(1, "#AvailableReSampleRates")
        For Each listItem As String In ListBox_SampleRates.Items
            PrintLine(1, listItem)
        Next
        'End Available ReSample block
        PrintLine(1, "#END")
        PrintLine(1, "")

        'Print Formats
        For i As Integer = 0 To ComboBox_RatesFormat.Items.Count - 1
            'Get platform name
            Dim platformFormat As String = ComboBox_RatesFormat.GetItemText(ComboBox_RatesFormat.Items(i))
            'Get array
            Dim SampleRates As Dictionary(Of String, UInteger) = propsFileData.sampleRateFormats(platformFormat)
            'Print data
            PrintLine(1, "// ReSample Rates for Format " & platformFormat)
            PrintLine(1, "#ReSampleRates" & i)
            For Each sampleRate As UInteger In SampleRates.Values
                PrintLine(1, CStr(sampleRate))
            Next
            PrintLine(1, "#END")
            PrintLine(1, "")
        Next

        'Misc properties
        PrintLine(1, "#MiscProperites")
        PrintLine(1, "DefaultRate " & ComboBox_DefaultSampleRate.SelectedIndex)
        PrintLine(1, "SampleFileFolder " & ProjMasterFolder)
        PrintLine(1, "HashCodeFileFolder " & ProjOutHashCodesFolder)
        PrintLine(1, "EngineXFolder " & ProjOutEngineXFolder)
        PrintLine(1, "EuroLandHashCodeServerPath " & ProjOutEuroLandServer)
        PrintLine(1, "#END")

        'Close file
        FileClose(1)
    End Sub

    Private Sub SaveIniFile()
        Dim iniFunctions As New IniFile(SysFileProjectIniPath)
        iniFunctions.Write("Edit_Wavs_With", TextBox_EditWavs.Text, "Form7_Misc")
        iniFunctions.Write("TextEditor", TextBox_TextEditor.Text, "PropertiesForm")
        iniFunctions.Write("PlayStationSize", Numeric_PlayStationMaxSize.Value, "PropertiesForm")
        iniFunctions.Write("PCSize", Numeric_PcMaxSize.Value, "PropertiesForm")
        iniFunctions.Write("GameCubeSize", Numeric_GameCubeMaxSize.Value, "PropertiesForm")
        iniFunctions.Write("XBoxSize", Numeric_XboxMaxSize.Value, "PropertiesForm")
    End Sub
End Class

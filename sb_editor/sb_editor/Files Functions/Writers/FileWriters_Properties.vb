Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub SavePropertiesFile(propsFileData As PropertiesFile, textFilePath As String)
            'Replace current file   
            Dim headerLib As New FileParsers
            Dim headerData As FileHeader = GetFileHeaderData(textFilePath, headerLib)

            'Update file
            FileOpen(1, textFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
            PrintLine(1, "## EuroSound Properties File")
            PrintLine(1, "## First Created ... " & headerData.FirstCreated)
            PrintLine(1, "## Created By ... " & headerData.CreatedBy)
            PrintLine(1, "## Last Modified ... " & headerData.LastModify)
            PrintLine(1, "## Last Modified By ... " & headerData.LastModifyBy)
            PrintLine(1, "")

            'Print Available formats
            PrintLine(1, "#AvailableFormats")
            PrintLine(1, " " & propsFileData.AvailableFormats.GetLength(0))
            For colIndex As Integer = 0 To propsFileData.AvailableFormats.GetLength(1) - 1
                For formatIndex As Integer = 0 To propsFileData.AvailableFormats.GetLength(0) - 1
                    PrintLine(1, propsFileData.AvailableFormats(formatIndex, colIndex))
                Next
            Next
            PrintLine(1, "#END")
            PrintLine(1, "")

            'Print Available ReSample Rates
            WriteListOfItems(propsFileData.AvailableReSampleRates.ToArray, "#AvailableReSampleRates", 1)
            PrintLine(1, "")

            For i As Integer = 0 To propsFileData.sampleRateFormats.Count - 1
                'Get platform name
                Dim platformFormat As String = propsFileData.sampleRateFormats.ElementAt(i).Key
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
            PrintLine(1, "DefaultRate  " & propsFileData.MiscProps.DefaultRate)
            PrintLine(1, "SampleFileFolder " & propsFileData.MiscProps.SampleFileFolder)
            PrintLine(1, "HashCodeFileFolder " & propsFileData.MiscProps.HashCodeFileFolder)
            PrintLine(1, "EngineXFolder " & propsFileData.MiscProps.EngineXFolder)
            PrintLine(1, "EuroLandHashCodeServerPath " & propsFileData.MiscProps.EuroLandHashCodeServerPath)
            PrintLine(1, "#END")
            'Close file
            FileClose(1)
        End Sub
    End Class
End Namespace
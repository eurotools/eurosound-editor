Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub SavePropertiesFile(propsFileData As PropertiesFile, textFilePath As String)
            'Replace current file   
            Dim headerLib As New FileParsers
            Dim headerData As FileHeader = GetFileHeaderData(textFilePath, headerLib)

            'Update file
            Using outputFile As New StreamWriter(textFilePath)
                outputFile.WriteLine("## EuroSound Properties File")
                outputFile.WriteLine("## First Created ... " & headerData.FirstCreated)
                outputFile.WriteLine("## Created By ... " & headerData.CreatedBy)
                outputFile.WriteLine("## Last Modified ... " & headerData.LastModify)
                outputFile.WriteLine("## Last Modified By ... " & headerData.LastModifyBy)
                outputFile.WriteLine("")

                'Print Available formats
                outputFile.WriteLine("#AvailableFormats")
                outputFile.WriteLine(" " & propsFileData.AvailableFormats.GetLength(0))
                For colIndex As Integer = 0 To propsFileData.AvailableFormats.GetLength(1) - 1
                    For formatIndex As Integer = 0 To propsFileData.AvailableFormats.GetLength(0) - 1
                        outputFile.WriteLine(propsFileData.AvailableFormats(formatIndex, colIndex))
                    Next
                Next
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")

                'Print Available ReSample Rates
                WriteListOfItems(propsFileData.AvailableReSampleRates.ToArray, "#AvailableReSampleRates", outputFile)
                outputFile.WriteLine("")

                For i As Integer = 0 To propsFileData.sampleRateFormats.Count - 1
                    'Get platform name
                    Dim platformFormat As String = propsFileData.sampleRateFormats.ElementAt(i).Key
                    'Get array
                    Dim SampleRates As Dictionary(Of String, UInteger) = propsFileData.sampleRateFormats(platformFormat)
                    'Print data
                    outputFile.WriteLine("// ReSample Rates for Format " & platformFormat)
                    outputFile.WriteLine("#ReSampleRates" & i)
                    For Each sampleRate As UInteger In SampleRates.Values
                        outputFile.WriteLine(CStr(sampleRate))
                    Next
                    outputFile.WriteLine("#END")
                    outputFile.WriteLine("")
                Next

                'Misc properties
                outputFile.WriteLine("#MiscProperites")
                outputFile.WriteLine("DefaultRate  " & propsFileData.MiscProps.DefaultRate)
                outputFile.WriteLine("SampleFileFolder " & propsFileData.MiscProps.SampleFileFolder)
                outputFile.WriteLine("HashCodeFileFolder " & propsFileData.MiscProps.HashCodeFileFolder)
                outputFile.WriteLine("EngineXFolder " & propsFileData.MiscProps.EngineXFolder)
                outputFile.WriteLine("EuroLandHashCodeServerPath " & propsFileData.MiscProps.EuroLandHashCodeServerPath)
                outputFile.WriteLine("#END")
            End Using
        End Sub
    End Class
End Namespace
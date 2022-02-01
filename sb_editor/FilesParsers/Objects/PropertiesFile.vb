Public Class PropertiesFile
    Public AvailableFormats As String(,)
    Public AvailableReSampleRates As List(Of String)
    Public sampleRateFormats As New Dictionary(Of String, Dictionary(Of String, UInteger))
    Public MiscProps As New MiscProperties
End Class

Public Class MiscProperties
    Public DefaultRate As Integer
    Public SampleFileFolder As String
    Public HashCodeFileFolder As String
    Public EngineXFolder As String
    Public EuroLandHashCodeServerPath As String
End Class

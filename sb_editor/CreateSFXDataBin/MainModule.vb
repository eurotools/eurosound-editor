Imports System.Globalization
Imports System.IO
Imports System.Text

Module MainModule
    '*===============================================================================================
    '* GLOBAL VARIABLES 
    '*===============================================================================================
    'Use the dot instead of comma
    Private ReadOnly numericProvider As New NumberFormatInfo With {
        .NumberDecimalSeparator = "."
    }

    '*===============================================================================================
    '* MAIN METHOD
    '*===============================================================================================
    Sub Main(args As String())
        If args.Length > 1 Then
            'Get data to print
            Dim itemsList As List(Of Single()) = ReadTextFile(args(0))

            'Generate Binary File
            CreateBinaryFile(args(1), itemsList)
        End If
    End Sub

    '*===============================================================================================
    '* FILES FUNCTIONS
    '*===============================================================================================
    Private Sub CreateBinaryFile(outputFilePath As String, listOfItems As List(Of Single()))
        Using BinWriter As New BinaryWriter(File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.ASCII)
            For itemIndex As Integer = 0 To listOfItems.Count - 1
                Dim currentItem As Single() = listOfItems(itemIndex)
                'HashCode
                BinWriter.Write(CUInt(currentItem(0)))
                'Inner Radius
                BinWriter.Write(currentItem(1))
                'Outer Radius
                BinWriter.Write(currentItem(2))
                'Alertness
                BinWriter.Write(currentItem(3))
                'Duration
                BinWriter.Write(currentItem(4))
                'Looping
                BinWriter.Write(CSByte(currentItem(5)))
                'Tracking 3D
                BinWriter.Write(CSByte(currentItem(6)))
                'SampleStreamed
                BinWriter.Write(CSByte(currentItem(7)))
                'Padding
                BinWriter.Write(CSByte(0))
            Next
        End Using
    End Sub

    Private Function ReadTextFile(inputFilePath As String) As List(Of Single())
        Dim itemsList As New List(Of Single())

        'Read Text File
        Using sr As New StreamReader(File.OpenRead(inputFilePath))
            'Read each line
            Dim currentLine As String = sr.ReadLine.Trim
            While currentLine <> Nothing
                'Check if the currentLine is valid
                If currentLine.StartsWith("{") Then
                    Dim SplitedLine = currentLine.Split(New Char() {"{"c, ","c, "}"c}, StringSplitOptions.RemoveEmptyEntries)

                    'Parse text data to floats and add items to the list
                    Dim ArrayOfValues As Single() = New Single(7) {}
                    For index As Integer = 0 To ArrayOfValues.Length - 1
                        ArrayOfValues(index) = StringFloatToDouble(SplitedLine(index).Trim)
                    Next

                    itemsList.Add(ArrayOfValues)
                Else
                    'Read another line
                    currentLine = sr.ReadLine.Trim
                    Continue While
                End If

                'Read another line
                currentLine = sr.ReadLine.Trim
            End While
        End Using

        Return itemsList
    End Function

    '*===============================================================================================
    '* FORMAT NUMBERS FUNCTIONS
    '*===============================================================================================
    Private Function StringFloatToDouble(number As String)
        Dim FinalNumber As Single

        'Ensure that the string is not null
        If number > "" Then
            Dim num As String = number.Replace("f", String.Empty)
            FinalNumber = Single.Parse(num, numericProvider)
        End If

        Return FinalNumber
    End Function
End Module

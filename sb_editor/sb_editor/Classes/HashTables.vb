Imports System.IO

Namespace HashTablesBuilder
    Friend Module HashTables
        '*===============================================================================================
        '* FORMAT FUNCTIONS
        '*===============================================================================================
        Friend Function WriteHashCode(Label As String, HashCode As String) As String
            Dim formattedString As String
            Dim stringLength As Integer = 20
            If Len(Label) < stringLength Then
                formattedString = String.Format("#define {0,-19} {1,8}", Label, HashCode)
            Else
                While stringLength <= Len(Label)
                    stringLength += 14
                End While
                formattedString = String.Format("#define {0,-" & (stringLength - 1) & "} {1,8}", Label, HashCode)
            End If
            Return formattedString
        End Function

        Friend Function NumberToHex(numberToConvert As UInteger) As String
            Dim hexNum As String = Trim(Hex(numberToConvert))
            Dim hexNumLength As Integer = Len(hexNum)
            Dim finalString As String = hexNum
            If hexNumLength < 48 Then
                finalString = "0x" + StrDup(8 - hexNumLength, "0"c) + hexNum
            End If
            Return finalString
        End Function

        Friend Function WriteHashCodeComment(Label As String, HashCode As String) As String
            Dim formattedString As String
            If Len(Label) < 19 Then
                formattedString = String.Format("// #define {0,-16} {1,8}", Label, HashCode)
            Else
                formattedString = String.Format("// #define {0,-30} {1,8}", Label, HashCode)
            End If
            Return formattedString
        End Function

        Friend Function WriteNumber(Label As String, HashCode As String) As String
            Dim stringLength = 20
            Dim formattedString As String
            If Len(Label) < stringLength Then
                formattedString = String.Format("#define {0,-19} {1,1}", Label, HashCode)
            Else
                While stringLength <= Len(Label)
                    stringLength += 14
                End While
                formattedString = String.Format("#define {0,-" & (stringLength - 1) & "} {1,1}", Label, HashCode)
            End If
            Return formattedString
        End Function

        Friend Function WriteNoAlign(Label As String, HashCode As String) As String
            Dim formattedString As String
            formattedString = String.Format("#define {0} {1}", Label, HashCode)
            Return formattedString
        End Function

        '*===============================================================================================
        '* MFX_Defines.h
        '*===============================================================================================
        Friend Sub CreateMfxHashTable(filePath As String, mfxDict As SortedDictionary(Of String, UInteger))
            Using outputFile As New StreamWriter(filePath)
                outputFile.WriteLine("// Music HashCodes")
                Dim maxSfxHashcodeDefined = 0
                For Each mfxItem In mfxDict
                    outputFile.WriteLine(WriteHashCode("MFX_" & mfxItem.Key, NumberToHex(mfxItem.Value + &H1BE00000)))
                    maxSfxHashcodeDefined = Math.Max(maxSfxHashcodeDefined, mfxItem.Value)
                Next
                outputFile.WriteLine("#define MFX_MaximumDefined " & maxSfxHashcodeDefined)
            End Using
        End Sub
    End Module
End Namespace

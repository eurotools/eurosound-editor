Imports System.IO

Friend Module FormatFunctions
    '*===============================================================================================
    '* FORMAT FUNCTIONS
    '*===============================================================================================
    Friend Sub WriteHashCode(sw As StreamWriter, Label As String, HashCode As String)
        If Len(Label) < 20 Then
            sw.WriteLine(String.Format("#define {0,-19} {1,8}", Label, HashCode))
        ElseIf Len(Label) < 34 Then
            sw.WriteLine(String.Format("#define {0,-33} {1,8}", Label, HashCode))
        ElseIf Len(Label) < 48 Then
            sw.WriteLine(String.Format("#define {0,-47} {1,8}", Label, HashCode))
        ElseIf Len(Label) < 62 Then
            sw.WriteLine(String.Format("#define {0,-61} {1,8}", Label, HashCode))
        Else
            sw.WriteLine(String.Format("#define {0,-75} {1,8}", Label, HashCode))
        End If
    End Sub

    Friend Sub WriteHashCodeComment(sw As StreamWriter, Label As String, HashCode As String)
        If Len(Label) < 19 Then
            sw.WriteLine(String.Format("// #define {0,-16} {1,8}", Label, HashCode))
        Else
            sw.WriteLine(String.Format("// #define {0,-30} {1,8}", Label, HashCode))
        End If
    End Sub

    Friend Sub WriteNumber(sw As StreamWriter, Label As String, HashCode As String)
        If Len(Label) < 20 Then
            sw.WriteLine(String.Format("#define {0,-19} {1,1}", Label, HashCode))
        Else
            sw.WriteLine(String.Format("#define {0,-33} {1,1}", Label, HashCode))
        End If
    End Sub

    Friend Sub WriteNoAlign(sw As StreamWriter, Label As String, HashCode As String)
        sw.WriteLine(String.Format("#define {0} {1}", Label, HashCode))
    End Sub
End Module

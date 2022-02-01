Imports System.IO

Public Class SfxDebug
    Public Sub CreateSfxDebug(sfxDict As SortedDictionary(Of String, UInteger), filePath As String)
        'Add new data to the file
        Using fs = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None)
            'Create a new writer
            Using sw As New StreamWriter(fs)
                'Write first part
                sw.WriteLine("#ifdef SFX_BUILD_DEBUG_TABLES")
                sw.WriteLine("long NumberToHashCode[] = {")
                For Each item As KeyValuePair(Of String, UInteger) In sfxDict
                    sw.WriteLine(String.Format("{0} , ", item.Value))
                Next
                sw.WriteLine("};")
                sw.WriteLine("#endif")
                sw.WriteLine(String.Empty)
                sw.WriteLine(String.Empty)

                'Write second part
                sw.WriteLine("#ifdef SFX_BUILD_DEBUG_TABLES")
                sw.WriteLine("typedef struct HashCodeAndString {long HashCode;char* String;} HashCodeAndString;")
                sw.WriteLine(String.Empty)
                sw.WriteLine("struct HashCodeAndString HashCodeToString[]={")
                For Each item As KeyValuePair(Of String, UInteger) In sfxDict
                    sw.WriteLine(String.Format("{0}{1} , ""{2}""{3} , ", "{"c, item.Value, item.Key, "}"c))
                Next
                sw.WriteLine("};")
                sw.WriteLine("#endif")
            End Using
        End Using
    End Sub
End Class

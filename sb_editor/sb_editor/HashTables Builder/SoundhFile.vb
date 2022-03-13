Imports System.IO

Namespace HashTablesBuilder
    Friend Class SoundhFile
        '*===============================================================================================
        '* Sound.h
        '*===============================================================================================
        Public Sub CreateSoundhFile(outputFilePath As String, sfxDefines As String, mfxDefines As String)
            FileOpen(1, outputFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
            PrintLine(1, "/* HT_Sound */")
            'SFX_Defines.h
            If fso.FileExists(sfxDefines) Then
                Dim sfxDefinesData As String() = File.ReadAllLines(sfxDefines)
                For index As Integer = 0 To sfxDefinesData.Length - 1
                    PrintLine(1, sfxDefinesData(index))
                Next
            End If
            'MFX_Defines.h
            If fso.FileExists(mfxDefines) Then
                Dim mfxDefinesData As String() = File.ReadAllLines(mfxDefines)
                For index As Integer = 0 To mfxDefinesData.Length - 1
                    PrintLine(1, mfxDefinesData(index))
                Next
            End If
            FileClose(1)
        End Sub
    End Class
End Namespace

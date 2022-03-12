Imports System.IO

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub CreateProjectFile(projectFilePath As String, soundbanks As String(), dataBases As String(), sfxList As String())
            Dim created = Date.Now.ToString(filesDateFormat)

            'Update file
            FileOpen(1, projectFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
            PrintLine(1, "## EuroSound EuroSound Project File File")
            PrintLine(1, "## First Created ... " & created)
            PrintLine(1, "## Created By ... " & EuroSoundUser)
            PrintLine(1, "## Last Modified ... " & created)
            PrintLine(1, "## Last Modified By ... " & EuroSoundUser)
            PrintLine(1, "")
            'Print Data
            WriteListOfItems(soundbanks, "#SoundBankList", 1)
            PrintLine(1, "")
            WriteListOfItems(dataBases, "#DataBaseList", 1)
            PrintLine(1, "")
            WriteListOfItems(sfxList, "#SFXList", 1)
            FileClose(1)
        End Sub

        Friend Sub MergeFiles(projectFilePath As String, temporalProjectFile As String, SFXsList As String())
            Dim fileLines As String() = File.ReadAllLines(temporalProjectFile)
            Dim sfxStartPos As Integer = Array.IndexOf(fileLines, "#SFXList")
            Dim finalArray As String() = New String((fileLines.Length + SFXsList.Length) - 2) {}
            Array.Copy(fileLines, finalArray, fileLines.Length - 1)
            Array.Copy(SFXsList, 0, finalArray, fileLines.Length - 1, SFXsList.Length)

            FileOpen(1, projectFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
            For index As Integer = 0 To finalArray.Length - 1
                PrintLine(1, finalArray(index))
            Next
            PrintLine(1, "#END")
            FileClose(1)
        End Sub
    End Class
End Namespace

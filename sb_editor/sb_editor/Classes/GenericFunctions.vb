Imports System.Globalization
Imports System.Text.RegularExpressions
Imports ESUtils.BytesFunctions
Imports IniFileFunctions
Imports Scripting

Module GenericFunctions
    '*===============================================================================================
    '* FORMAT INFO
    '*===============================================================================================
    Friend ReadOnly numericProvider As New NumberFormatInfo With {
        .NumberDecimalSeparator = "."
    }

    '*===============================================================================================
    '* TOOLS FUNCTIONS
    '*===============================================================================================
    Friend Sub RunProcess(toolFileName As String, toolArguments As String)
        Dim processToExecute As New Process
        processToExecute.StartInfo.FileName = toolFileName
        processToExecute.StartInfo.Arguments = toolArguments
        processToExecute.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        processToExecute.StartInfo.UseShellExecute = True
        processToExecute.Start()
        processToExecute.WaitForExit()
    End Sub

    '*===============================================================================================
    '* INPUT DATA
    '*===============================================================================================
    Friend Function AskForUserName(defaultName As String) As String
        'Ask user for a new username
        Dim inputUserName As String = defaultName
        Do
            inputUserName = InputBox("Please Enter Your UserName", "Enter UserName.", inputUserName)
        Loop While inputUserName = ""

        'Put new username
        EuroSoundUser = inputUserName

        'Modify EuroSound Ini
        Dim programIni As New IniFile(EuroSoundIniFilePath)
        programIni.Write("UserName", EuroSoundUser, "Form1_Misc")

        Return EuroSoundUser
    End Function

    '*===============================================================================================
    '* OTHER FUNCTIONS
    '*===============================================================================================
    Friend Sub EditWaveFile(waveFilePath As String)
        'Open tool if files exists
        If fso.FileExists(waveFilePath) AndAlso fso.FileExists(ProjAudioEditor) Then
            Try
                Dim procInfo As New ProcessStartInfo With {
                    .FileName = """" & ProjAudioEditor & """",
                    .Arguments = """" & waveFilePath & """"
                }
                Process.Start(procInfo)
            Catch ex As Exception
                MsgBox(ex.Message, vbOKOnly + vbCritical, "EuroSound")
            End Try
        End If
    End Sub

    Friend Function MultipleDeletionMessage(messageToShow As String, itemsToDelete As List(Of String)) As String
        Dim maxItemsToShow As Byte = 33
        'Create message to inform user
        Dim filesListToDelete As String = messageToShow & vbNewLine & vbNewLine
        Dim numItems As Integer = Math.Min(maxItemsToShow, itemsToDelete.Count)
        For index As Integer = 0 To numItems - 1
            filesListToDelete += "'" & itemsToDelete(index) & "'" & vbNewLine
        Next
        If itemsToDelete.Count > maxItemsToShow Then
            filesListToDelete += "Plus Some More ....." & vbNewLine
            filesListToDelete += "............" & vbNewLine
        End If
        filesListToDelete += vbNewLine & "Total Files: " & itemsToDelete.Count
        Return filesListToDelete
    End Function

    Friend Sub RestartEuroSound()
        'Restart application
        Process.Start(Application.ExecutablePath)
        Application.Exit()
    End Sub

    '*===============================================================================================
    '* FILES FUNCTIONS
    '*===============================================================================================
    Friend Function GetOnlyFileName(fullFileName As String) As String
        Dim a As Integer = InStrRev(fullFileName, "\") + 1
        Dim b As Integer = InStrRev(fullFileName, ".")
        Dim fileName As String
        If b > a Then
            fileName = Mid(fullFileName, a, b - a)
        Else
            fileName = fullFileName
        End If
        Return fileName
    End Function

    Friend Function CountFolderFiles(Folder As String, Filter As String) As Integer
        Dim CountFilesDir As Integer = 0
        Dim sFile As String = Dir(Folder & "\" & Filter)
        Do While Len(sFile) > 0
            CountFilesDir += 1
            sFile = Dir()
        Loop
        Return CountFilesDir
    End Function

    Friend Function BytesStringFormat(BytesCaller As Long) As String
        Return FormatBytes(BytesCaller)
    End Function

    Friend Function RenameFile(defaultResponse As String, objectType As String, objectFolder As String) As String
        While True
            Dim inputName As String = Trim(InputBox("Enter New Name For " & objectType & " " & defaultResponse, "Rename " & objectType, defaultResponse))
            Dim match As Match = Regex.Match(inputName, namesFormat)
            If (match.Success) Then
                Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
                If StrComp(defaultResponse, inputName) = 0 Then
                    Return ""
                ElseIf fso.FileExists(inputFilePath) Then
                    MsgBox(objectType & " Label '" & inputName & "' already exists please use another name!", vbOKOnly + vbCritical, "Duplicate " & objectType & " Name")
                Else
                    Return inputName
                End If
            Else
                MsgBox(objectType & " Label '" & inputName & "' uses invalid characters, only numbers, digits and underscore characters are allowed.", vbOKOnly + vbCritical, "EuroSound")
            End If
        End While
        Return ""
    End Function

    Friend Function CopyFile(defaultResponse As String, objectType As String, objectFolder As String) As String
        While True
            Dim inputName As String = Trim(InputBox("Enter New Name For " & objectType & " " & defaultResponse, "Copy " & objectType, defaultResponse))
            Dim match As Match = Regex.Match(inputName, namesFormat)
            If (match.Success) Then
                Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
                If fso.FileExists(inputFilePath) Then
                    MsgBox(objectType & " Label '" & inputName & "' already exists please use another name!", vbOKOnly + vbCritical, "Duplicate " & objectType & " Name")
                Else
                    Return inputName
                End If
            Else
                MsgBox(objectType & " Label '" & inputName & "' uses invalid characters, only numbers, digits and underscore characters are allowed.", vbOKOnly + vbCritical, "EuroSound")
            End If
        End While
        Return ""
    End Function

    Friend Function NewFile(objectName As String, objectFolder As String) As String
        While True
            Dim inputName As String = Trim(InputBox("Enter Name", "Create New", objectName))
            Dim match As Match = Regex.Match(inputName, namesFormat)
            If (match.Success) Then
                Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
                If fso.FileExists(inputFilePath) Then
                    MsgBox("Label '" & inputName & "' already exists please use another name!", vbOKOnly + vbCritical, "Duplicate Name")
                Else
                    Return inputName
                End If
            Else
                MsgBox("Label '" & inputName & "' uses invalid characters, only numbers, digits and underscore characters are allowed.", vbOKOnly + vbCritical, "EuroSound")
            End If
        End While
        Return ""
    End Function

    '*===============================================================================================
    '* INI FILE FUNCTIONS
    '*===============================================================================================
    Friend Function GetDefaultSampleValues() As Double()
        Dim sampleInfo As Double() = New Double() {0, 0, 0, 0, 0, 0}
        Dim iniFunctions As New IniFile(SysFileProjectIniPath)
        Dim IniPitchOffset As String = iniFunctions.Read("DTextNIndex_0", "SFXForm")
        Dim IniRandomPitch As String = iniFunctions.Read("DTextNIndex_1", "SFXForm")
        Dim IniBaseVolume As String = iniFunctions.Read("DTextNIndex_2", "SFXForm")
        Dim IniRandomVol As String = iniFunctions.Read("DTextNIndex_3", "SFXForm")
        Dim IniPan As String = iniFunctions.Read("DTextNIndex_4", "SFXForm")
        Dim IniRandomPan As String = iniFunctions.Read("DTextNIndex_5", "SFXForm")

        'Pitch Offset
        If IsNumeric(IniPitchOffset) Then
            sampleInfo(0) = Convert.ToDouble(IniPitchOffset, numericProvider)
        End If
        'Random Pitch
        If IsNumeric(IniRandomPitch) Then
            sampleInfo(1) = Convert.ToDouble(IniRandomPitch, numericProvider)
        End If
        'Base Volume
        If IsNumeric(IniBaseVolume) Then
            sampleInfo(2) = CInt(IniBaseVolume)
        End If
        'Random Volume Offset
        If IsNumeric(IniRandomVol) Then
            sampleInfo(3) = CInt(IniRandomVol)
        End If
        'Pan
        If IsNumeric(IniPan) Then
            sampleInfo(4) = CInt(IniPan)
        End If
        'Random Pan
        If IsNumeric(IniRandomPan) Then
            sampleInfo(5) = CInt(IniRandomPan)
        End If

        Return sampleInfo
    End Function

    '*===============================================================================================
    '* STRINGS FUNCTIONS
    '*===============================================================================================
    Friend Function GetEngineXFolder(outputPlatform As String) As String
        Dim FolderName As String = ""
        Select Case outputPlatform
            Case "PC"
                FolderName = "_bin_PC"
            Case "PlayStation2"
                FolderName = "_bin_PS2"
            Case "GameCube"
                FolderName = "_bin_GC"
            Case "X Box"
                FolderName = "_bin_XB"
        End Select

        GetEngineXFolder = FolderName
    End Function

    Friend Function GetSfxFileName(language As Integer, fileHashCode As Integer) As Integer
        Return ((language And &HF) << 16) Or ((fileHashCode And &HFFFF) << 0)
    End Function

    Friend Function GetEngineXLangFolder(outputLanguage As String) As String
        GetEngineXLangFolder = "_" & Left(outputLanguage, 3)
    End Function
End Module

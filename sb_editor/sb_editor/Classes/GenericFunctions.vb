Imports System.Globalization
Imports System.Runtime.InteropServices
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
    '* DLL UTILS FUNCTIONS
    '*===============================================================================================
    <DllImport("SystemFiles\EuroSound_Utils.dll", CallingConvention:=CallingConvention.Cdecl)>
    Private Function FormatBytes(BytesCaller As Long) As IntPtr
    End Function

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
        programIni.Write("UserName", inputUserName, "Form1_Misc")

        Return inputUserName
    End Function

    '*===============================================================================================
    '* OTHER FUNCTIONS
    '*===============================================================================================
    Friend Sub EditWaveFile(waveFilePath As String)
        'Open tool if files exists
        If fso.FileExists(waveFilePath) AndAlso fso.FileExists(ProjAudioEditor) Then
            Dim procInfo As New ProcessStartInfo With {
                .FileName = """" & ProjAudioEditor & """",
                .Arguments = """" & waveFilePath & """"
            }
            Process.Start(procInfo)
        End If
    End Sub

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
        Return Mid(fullFileName, a, b - a)
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
        Return Marshal.PtrToStringAnsi(FormatBytes(BytesCaller))
    End Function

    Friend Function RenameFile(objectName As String, objectType As String, objectFolder As String) As String
        'Ask name for first time
        Dim inputName As String = InputBox("Enter New Name For " & objectName, "Rename " & objectType, objectName)
        Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
        Dim finalName As String = inputName

        'Ask continously if the file exists
        If StrComp(objectName, inputName) <> 0 AndAlso fso.FileExists(inputFilePath) Then
            Do
                'Inform user and ask again
                MsgBox(objectType & " Label '" & inputName & "' already exists, please use another name!", vbOKOnly + vbCritical, "Duplicate " & objectType & " Name")
                inputName = InputBox("Enter New Name For " & objectName, "Rename " & objectType, objectName)

                'If the input name is the same as the object name or is null, exit loop!
                If StrComp(objectName, inputName) = 0 Or inputName = "" Then
                    finalName = ""
                    Exit Do
                Else
                    finalName = inputName
                End If
            Loop While fso.FileExists(fso.BuildPath(objectFolder, inputName & ".txt"))
        End If

        RenameFile = finalName
    End Function

    Friend Function CopyFile(objectName As String, objectType As String, objectFolder As String) As String
        'Ask name for first time
        Dim inputName As String = InputBox("Enter Copy Name For " & objectType & " " & objectName, "Copy " & objectType, objectName).Trim
        Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
        Dim finalName As String = inputName

        'Ask continously if the file exists
        If StrComp(objectName, inputName) <> 0 AndAlso fso.FileExists(inputFilePath) Then
            Do
                'Inform user and ask again
                MsgBox(objectType & " Label '" & inputName & "' already exists, please use another name!", vbOKOnly + vbCritical, "Duplicate " & objectType & " Name")
                inputName = InputBox("Enter Copy Name For " & objectName, "Copy " & objectType, objectName).Trim

                'If the input name is the same as the object name or is null, exit loop!
                If StrComp(objectName, inputName) = 0 Or inputName = "" Then
                    finalName = ""
                    Exit Do
                Else
                    finalName = inputName
                End If
            Loop While fso.FileExists(fso.BuildPath(objectFolder, inputName & ".txt"))
        End If

        CopyFile = finalName
    End Function

    Friend Function NewFile(objectName As String, objectFolder As String) As String
        'Ask name for first time
        Dim inputName As String = InputBox("Enter Name", "Create New", objectName)
        Dim inputFilePath As String = fso.BuildPath(objectFolder, inputName & ".txt")
        Dim finalName As String = inputName

        'Ask continously if the file exists
        If StrComp(objectName, inputName) <> 0 AndAlso fso.FileExists(inputFilePath) Then
            Do
                'Inform user and ask again
                MsgBox("Label '" & inputName & "' already exists, please use another name!", vbOKOnly + vbCritical, "Duplicate Name")
                inputName = InputBox("Enter Name", "Create New", objectName)

                'If the input name is the same as the object name or is null, exit loop!
                If StrComp(objectName, inputName) = 0 Or inputName = "" Then
                    finalName = ""
                    Exit Do
                Else
                    finalName = inputName
                End If
            Loop While fso.FileExists(fso.BuildPath(objectFolder, inputName & ".txt"))
        End If

        NewFile = finalName
    End Function

    '*===============================================================================================
    '* FOLDERS FUNCTIONS
    '*===============================================================================================
    Friend Sub CreateFolderIfNotExists(folderPath)
        If Dir$(folderPath, FileAttribute.Directory) = "" Then
            MkDir(folderPath)
        End If
    End Sub

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

    Friend Function GetEngineXLangFolder(outputLanguage As String) As String
        Dim finalName As String = ""
        Select Case outputLanguage
            Case "English"
                finalName = "_Eng"
            Case "American"
                finalName = "_Usa"
            Case "Danish"
                finalName = "_Dan"
            Case "Dutch"
                finalName = "_Dut"
            Case "Finnish"
                finalName = "_Fin"
            Case "French"
                finalName = "_Fre"
            Case "German"
                finalName = "_Ger"
            Case "Italian"
                finalName = "_Ita"
            Case "Japanese"
                finalName = "_Jap"
            Case "Norwegian"
                finalName = "_Nor"
            Case "Portuguese"
                finalName = "_Por"
            Case "Spanish"
                finalName = "_Spa"
            Case "Swedish"
                finalName = "_Swe"
        End Select

        GetEngineXLangFolder = finalName
    End Function
End Module

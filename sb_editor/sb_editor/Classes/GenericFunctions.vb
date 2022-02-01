Imports System.Runtime.InteropServices
Imports IniFileFunctions
Imports Scripting

Module GenericFunctions
    '*===============================================================================================
    '* DLL UTILS FUNCTIONS
    '*===============================================================================================
    <DllImport("SystemFiles\\EuroSound_Utils.dll", CallingConvention:=CallingConvention.Cdecl)>
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
    Friend Function CountFolderFiles(Folder As String, Filter As String) As Integer
        Dim CountFilesDir As Integer = 0

        Dim sFile As String = Dir(Folder & "\" & Filter)
        Do While Len(sFile) > 0
            CountFilesDir += 1
            sFile = Dir()
        Loop

        'Return count results
        Return CountFilesDir
    End Function

    Friend Function BytesStringFormat(BytesCaller As Long) As String
        Return Marshal.PtrToStringAnsi(FormatBytes(BytesCaller))
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

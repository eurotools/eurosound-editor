Imports System.IO
Imports IniFileFunctions

Public Class Language_FolderCompare
    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Sub Language_FolderCompare_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load config
        If fso.FolderExists(fso.BuildPath(WorkingDirectory, "System")) Then
            Dim iniFunctions As New IniFile(SysFileProjectIniPath)
            TextBox_PrimaryPath.Text = iniFunctions.Read("LanguagePath1", "LanguageFolderCompare")
            TextBox_SecondaryPath.Text = iniFunctions.Read("LanguagePath2", "LanguageFolderCompare")
        End If
    End Sub

    Private Sub Language_FolderCompare_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Save data in the Ini File
        If fso.FolderExists(fso.BuildPath(WorkingDirectory, "System")) Then
            Dim iniFunctions As New IniFile(SysFileProjectIniPath)
            iniFunctions.Write("LanguagePath1", TextBox_PrimaryPath.Text, "LanguageFolderCompare")
            iniFunctions.Write("LanguagePath2", TextBox_SecondaryPath.Text, "LanguageFolderCompare")
        End If
    End Sub

    '*===============================================================================================
    '* BUTTON EVENTS
    '*===============================================================================================
    Private Sub Button_SetPrimaryFolder_Click(sender As Object, e As EventArgs) Handles Button_SetPrimaryFolder.Click
        Dim folderDiagResult As DialogResult = FolderBrowserDialog.ShowDialog
        If folderDiagResult = DialogResult.OK Then
            TextBox_PrimaryPath.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub Button_SetSecondaryFolder_Click(sender As Object, e As EventArgs) Handles Button_SetSecondaryFolder.Click
        Dim folderDiagResult As DialogResult = FolderBrowserDialog.ShowDialog
        If folderDiagResult = DialogResult.OK Then
            TextBox_SecondaryPath.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub Button_DoCompare_Click(sender As Object, e As EventArgs) Handles Button_DoCompare.Click
        'Clear textboxes
        TextBox_AdditionFilesSecondary.Clear()
        TextBox_MissingFilesSecondary.Clear()

        'Get files
        Dim primaryFolderFiles As String() = Directory.GetFiles(TextBox_PrimaryPath.Text, "*.wav", SearchOption.AllDirectories)
        Dim secondaryFolderFiles As String() = Directory.GetFiles(TextBox_SecondaryPath.Text, "*.wav", SearchOption.AllDirectories)

        'Get relative paths
        Dim primaryPathLength As Integer = Len(TextBox_PrimaryPath.Text) + 1
        For index As Integer = 0 To primaryFolderFiles.Length - 1
            primaryFolderFiles(index) = Mid(primaryFolderFiles(index), primaryPathLength)
        Next
        Dim secondaryPathLength As Integer = Len(TextBox_SecondaryPath.Text) + 1
        For index As Integer = 0 To secondaryFolderFiles.Length - 1
            secondaryFolderFiles(index) = Mid(secondaryFolderFiles(index), secondaryPathLength)
        Next

        'Missing Files in Secondary Path
        Dim missingInSecondaryPath As String() = primaryFolderFiles.Except(secondaryFolderFiles).ToArray
        For index As Integer = 0 To missingInSecondaryPath.Length - 1
            TextBox_MissingFilesSecondary.Text += missingInSecondaryPath(index) & vbNewLine
        Next

        'Addition Files in Secondary Path
        Dim additionInSecondaryPath As String() = secondaryFolderFiles.Except(primaryFolderFiles).ToArray
        For index As Integer = 0 To additionInSecondaryPath.Length - 1
            TextBox_AdditionFilesSecondary.Text += additionInSecondaryPath(index) & vbNewLine
        Next
    End Sub
End Class
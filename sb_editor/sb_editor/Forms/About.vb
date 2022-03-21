Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports Octokit

Public Class About
    '*===============================================================================================
    '* Global Variables
    '*===============================================================================================
    <DllImport("wininet.dll")>
    Private Shared Function InternetGetConnectedState(<Out> ByRef connDescription As Integer, ReservedValue As Integer) As Boolean
    End Function

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Private Async Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label_CurrentVersion.Text = "This Version: " & Assembly.GetEntryAssembly().GetName().Version.ToString
        If CheckForInternetConnection(0) Then
            Await CheckGitHubNewerVersion()
        Else
            Label_LatestVersion.Text = "Latest Version Available:  No Available"
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_GetUpdate_Click(sender As Object, e As EventArgs) Handles Button_GetUpdate.Click
        Process.Start(String.Join("", "https://github.com/eurotools/eurosound_project/releases/latest"))
    End Sub

    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        Close()
    End Sub

    '*===============================================================================================
    '* FUNCTIONS
    '*===============================================================================================
    Private Function CheckForInternetConnection(connDescription As Integer) As Boolean
        Return InternetGetConnectedState(connDescription, 0)
    End Function

    Private Async Function CheckGitHubNewerVersion() As Task
        'Get last releases from GitHub
        Dim client As New GitHubClient(New ProductHeaderValue("EuroSoundUser"))
        Try
            Dim releases As Release = Await client.Repository.Release.GetLatest("eurotools", "eurosound_project")

            'Setup the versions
            Dim localVersion As New Version(Assembly.GetEntryAssembly().GetName().Version.ToString)
            Dim latestGitHubVersion As New Version(releases.TagName)
            Label_LatestVersion.Text = "Latest Version: " & releases.TagName

            'Compare the Versions
            Dim versionComparison = localVersion.CompareTo(latestGitHubVersion)
            If versionComparison < 0 Then
                Button_GetUpdate.Enabled = True
            ElseIf versionComparison > 0 Then
                MsgBox(String.Join("", "This message should never appear to a final user." & vbLf & vbLf & "Local version: ", localVersion.ToString(), vbLf & "GitHub version: ", latestGitHubVersion.ToString()), vbOKOnly + vbExclamation, "EuroSound")
            End If
        Catch ex As Exception
            Label_LatestVersion.Text = "Latest Version Available:  No Available"
            MsgBox(ex.Message, vbOKOnly + vbCritical, "Error!")
        End Try
    End Function
End Class
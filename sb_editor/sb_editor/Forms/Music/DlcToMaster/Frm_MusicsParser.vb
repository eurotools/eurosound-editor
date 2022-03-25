Imports System.IO

Public Class Frm_MusicsParser
    '*===============================================================================================
    '* GLOBAL VARS
    '*===============================================================================================
    Private ReadOnly markerFilesToInspect As String()
    Private ReadOnly associationDictionary As New Dictionary(Of String, String) From
    {
        {"Aby_Council_Int1", "(01) Abydos Council Interior (I).flac"},
        {"Aby_Council_Int2", "(02) Abydos Council Interior (II).flac"},
        {"Aby_Juggler", "(03) Abydos Juggler.flac"},
        {"Aby_Main", "(04) Abydos Main.flac"},
        {"Aby_North", "(05) Abydos North.flac"},
        {"Action1", "(06) Action (I).flac"},
        {"Ambient1", "(07) Ambient (I).flac"},
        {"Ambient2", "(08) Ambient (II).flac"},
        {"Ambient3", "(09) Ambient (III).flac"},
        {"Ambient4", "(10) Ambient (IV).flac"},
        {"Boss1", "(11) Boss (I).flac"},
        {"Boss2", "(12) Boss (II).flac"},
        {"Boss3", "(13) Boss (III).flac"},
        {"Boss4", "(14) Boss (IV).flac"},
        {"Copy of Possess_Pokemon", "(15) Danger (I).flac"},
        {"Danger1", "(16) Danger (II).flac"},
        {"Danger2", "(17) Danger (III).flac"},
        {"Danger3", "(18) Military.flac"},
        {"Military", "(19) Minigame; Intro.flac"},
        {"MiniGame_Intro", "(20) Minigame; Pairs.flac"},
        {"MiniGame_Pairs", "(21) Minigame; Shoot.flac"},
        {"MiniGame_Shoot", "(22) Minigame; Simon.flac"},
        {"MiniGame_Simon", "(23) Minigame; Walls.flac"},
        {"MiniGame_Walls", "(24) Bat Mummy (I).flac"},
        {"Mummy_Bat1", "(25) Bat Mummy (II).flac"},
        {"Mummy_Bat2", "(26) Bat Mummy (III).flac"},
        {"Mummy_Bat3", "(27) Electric Mummy (I).flac"},
        {"Mummy_Elec1", "(28) Electric Mummy (II).flac"},
        {"Mummy_Elec2", "(29) Electric Mummy (III).flac"},
        {"Mummy_Elec3", "(30) Fire Mummy (I).flac"},
        {"Mummy_Fire1", "(31) Fire Mummy (II).flac"},
        {"Mummy_Fire2", "(32) Fire Mummy (III).flac"},
        {"Mummy_Fire3", "(33) Paper Mummy (I).flac"},
        {"Mummy_Paper1", "(34) Paper Mummy (II).flac"},
        {"Mummy_Paper2", "(35) Paper Mummy (III).flac"},
        {"Mummy_Paper3", "(36) Smoke Mummy.flac"},
        {"Mummy_Smoke", "(37) Stone Mummy.flac"},
        {"Mummy_Stone", "(38) Nomads.flac"},
        {"Nomads", "(39) Pause.flac"},
        {"Pause", "(40) Platform (I).flac"},
        {"Platform1", "(41) Possess Dino.flac"},
        {"Possess_Dino", "(42) Possess Critter.flac"},
        {"Possess_Pokemon", "(43) Puzzle (I).flac"},
        {"Puzzle1", "(44) Puzzle (II).flac"},
        {"Puzzle2", "(45) Sakkara Main.flac"},
        {"Sak_Main", "(46) Sneak (I) Jail.flac"},
        {"Silence", "(47) Sneak (I) Jail Intro.flac"},
        {"Silence_loop", "(48) Sneak (II) Puzzle.flac"},
        {"Sneak1_Jail", "(49) Sorrow.flac"},
        {"Sneak1_Jail_Intro", "(50) Swim.flac"},
        {"Sneak2_Puzzle", "(51) Swim Danger.flac"},
        {"Sorrow", "(52) Temple (I).flac"},
        {"Swim", "(53) Temple (II).flac"},
        {"Swim_Danger", "(54) Title.flac"},
        {"Temple1", "(55) Title (End Sting).flac"},
        {"Temple2", "(56) Nomad Outpost.flac"},
        {"Title", "(57) Trading Outpost.flac"},
        {"Title_EndSting", "(58) Sunshrine.flac"}
    }

    '*===============================================================================================
    '* FORM EVENTS
    '*===============================================================================================
    Sub New(filesToImport As String(), outpuFilePath As String)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        markerFilesToInspect = filesToImport
        TextBox_DestinationFolder.Text = outpuFilePath
    End Sub

    Private Sub Frm_MusicsParser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If BackgroundWorker1.IsBusy Then
            Dim answer As DialogResult = MsgBox("Are you sure you wish to exit?", vbYesNo + vbQuestion, "Cancel Musics Import")
            If answer = DialogResult.Yes Then
                BackgroundWorker1.CancelAsync()
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    '*===============================================================================================
    '* FORM BUTTONS
    '*===============================================================================================
    Private Sub Button_DlcFolder_Click(sender As Object, e As EventArgs) Handles Button_DlcFolder.Click
        Dim folderBrowser As DialogResult = FolderBrowserDialog.ShowDialog
        If folderBrowser = DialogResult.OK Then
            Textbox_DlcFolderPath.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub Button_Import_Click(sender As Object, e As EventArgs) Handles Button_Import.Click
        'Clear output textbox
        TextBox_OutputConsole.Clear()

        'Read All Marker Files
        If Directory.Exists(Textbox_DlcFolderPath.Text) Then
            If Not BackgroundWorker1.IsBusy Then
                BackgroundWorker1.RunWorkerAsync()
                Button_Import.Enabled = False
            End If
        End If
    End Sub

    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        Close()
    End Sub

    '*===============================================================================================
    '* BACKGROUND WORKER
    '*===============================================================================================
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        For index As Integer = 0 To markerFilesToInspect.Length - 1
            If BackgroundWorker1.CancellationPending Then
                Exit For
            Else
                'Get filename
                Dim fileName As String = markerFilesToInspect(index)
                Dim filePath As String = Path.Combine(WorkingDirectory, "Music", fileName & ".mrk")
                Dim fileData As String() = File.ReadAllLines(filePath)
                TextBox_OutputConsole.Invoke(Sub() TextBox_OutputConsole.Text += "----------------------------------------------" & vbCrLf)
                TextBox_OutputConsole.Invoke(Sub() TextBox_OutputConsole.Text += "Marker file: '" & filePath & "'" & vbCrLf)

                'Get end position
                Dim WaveEndPosition As UInteger = GetWaveEndPos(fileData)

                'Get input and output filepath
                If associationDictionary.ContainsKey(fileName) Then
                    Dim inputFilePath As String = Path.Combine(Textbox_DlcFolderPath.Text, associationDictionary(fileName))
                    If File.Exists(inputFilePath) Then
                        Dim outputFilePath As String = Path.Combine(TextBox_DestinationFolder.Text, fileName & ".wav")
                        TextBox_OutputConsole.Invoke(Sub() TextBox_OutputConsole.Text += "Input DLC file: " & inputFilePath & vbCrLf)
                        TextBox_OutputConsole.Invoke(Sub() TextBox_OutputConsole.Text += "Output Wave file: " & outputFilePath & vbCrLf)
                        RunConsoleProcess("SystemFiles\MusicTool\Sox.exe", """" & inputFilePath & """ """ & outputFilePath & """ trim 0s " & WaveEndPosition & "s")
                    Else
                        TextBox_OutputConsole.Invoke(Sub() TextBox_OutputConsole.Text += "Input DLC file NOT found: " & inputFilePath & vbCrLf)
                    End If
                Else
                    TextBox_OutputConsole.Invoke(Sub() TextBox_OutputConsole.Text += "This markerfile is not from the original game musics." & vbCrLf)
                End If

                TextBox_OutputConsole.Invoke(Sub() TextBox_OutputConsole.Text += "" & vbCrLf)
            End If
        Next
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Button_Import.Enabled = True
        MsgBox("Import Sucessfull!" & vbCrLf & "Click on the 'UPDATE' button to refresh the list, all musics should appear then.", vbOKOnly + vbInformation, "EuroSound")
    End Sub

    '*===============================================================================================
    '* METHODS
    '*===============================================================================================
    Private Function GetWaveEndPos(filedata As String()) As UInteger
        Dim endPos As UInteger = 0
        For lineIndex As Integer = 0 To filedata.Length - 1
            Dim currentLine = filedata(lineIndex).Trim
            If currentLine.Contains("Name=*") Then
                Dim linetoSplit As String = filedata(lineIndex + 1).Trim
                endPos = linetoSplit.Split(New Char() {"="c}, StringSplitOptions.RemoveEmptyEntries)(1)
                Exit For
            End If
        Next
        Return endPos
    End Function
End Class
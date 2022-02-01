Public Class GlobalVariablesWatcher
    Private Sub GlobalVariablesWatcher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowGlobalVarsValues()
    End Sub

    Private Sub Button_Update_Click(sender As Object, e As EventArgs) Handles Button_Update.Click
        ShowGlobalVarsValues()
    End Sub

    Private Sub Button_Close_Click(sender As Object, e As EventArgs) Handles Button_Close.Click
        Close()
    End Sub

    Private Sub ShowGlobalVarsValues()
        'Show globals
        TextBox_Globals.Clear()
        TextBox_Globals.Text = String.Format("{0} {1} = {2}{3}", DefaultLanguage.GetType().Name.ToString, NameOf(DefaultLanguage), DefaultLanguage, vbNewLine)
        TextBox_Globals.Text += String.Format("{0} {1} = {2}{3}", WorkingDirectory.GetType().Name.ToString, NameOf(WorkingDirectory), WorkingDirectory, vbNewLine)
        TextBox_Globals.Text += String.Format("{0} {1} = {2}{3}", EuroSoundIniFilePath.GetType().Name.ToString, NameOf(EuroSoundIniFilePath), EuroSoundIniFilePath, vbNewLine)
        TextBox_Globals.Text += String.Format("{0} {1} = {2}{3}", EuroSoundUser.GetType().Name.ToString, NameOf(EuroSoundUser), EuroSoundUser, vbNewLine)

        'Show Project Settings
        TextBox_ProjectSettings.Clear()
        TextBox_ProjectSettings.Text = String.Format("{0} {1} = {2}{3}", ProjMasterFolder.GetType().Name.ToString, NameOf(ProjMasterFolder), ProjMasterFolder, vbNewLine)
        TextBox_ProjectSettings.Text += String.Format("{0} {1} = {2}{3}", ProjAudioEditor.GetType().Name.ToString, NameOf(ProjAudioEditor), ProjAudioEditor, vbNewLine)
        TextBox_ProjectSettings.Text += String.Format("{0} {1} = {2}{3}", ProjOutHashCodesFolder.GetType().Name.ToString, NameOf(ProjOutHashCodesFolder), ProjOutHashCodesFolder, vbNewLine)
        TextBox_ProjectSettings.Text += String.Format("{0} {1} = {2}{3}", ProjOutEngineXFolder.GetType().Name.ToString, NameOf(ProjOutEngineXFolder), ProjOutEngineXFolder, vbNewLine)
        TextBox_ProjectSettings.Text += String.Format("{0} {1} = {2}{3}", ProjOutEuroLandServer.GetType().Name.ToString, NameOf(ProjOutEuroLandServer), ProjOutEuroLandServer, vbNewLine)
        TextBox_ProjectSettings.Text += String.Format("{0} {1} = {2}{3}", ProjTextEditor.GetType().Name.ToString, NameOf(ProjTextEditor), ProjTextEditor, vbNewLine)

        'Show System Files
        TextBox_SystemFiles.Clear()
        TextBox_SystemFiles.Text = String.Format("{0} {1} = {2}{3}", SysFileSamples.GetType().Name.ToString, NameOf(SysFileSamples), SysFileSamples, vbNewLine)
        TextBox_SystemFiles.Text += String.Format("{0} {1} = {2}{3}", SysFileProperties.GetType().Name.ToString, NameOf(SysFileProperties), SysFileProperties, vbNewLine)
        TextBox_SystemFiles.Text += String.Format("{0} {1} = {2}{3}", SysFileProjectIniPath.GetType().Name.ToString, NameOf(SysFileProjectIniPath), SysFileProjectIniPath, vbNewLine)
        TextBox_SystemFiles.Text += String.Format("{0} {1} = {2}{3}", SysFileSfxDefaults.GetType().Name.ToString, NameOf(SysFileSfxDefaults), SysFileSfxDefaults, vbNewLine)

        'Show HashCodes
        TextBox_HashCodes.Clear()
        TextBox_HashCodes.Text = String.Format("{0} {1} = {2}{3}", SFXHashCodeNumber.GetType().Name.ToString, NameOf(SFXHashCodeNumber), SFXHashCodeNumber, vbNewLine)
        TextBox_HashCodes.Text += String.Format("{0} {1} = {2}{3}", SoundBankHashCodeNumber.GetType().Name.ToString, NameOf(SoundBankHashCodeNumber), SoundBankHashCodeNumber, vbNewLine)
        TextBox_HashCodes.Text += String.Format("{0} {1} = {2}{3}", MFXHashCodeNumber.GetType().Name.ToString, NameOf(MFXHashCodeNumber), MFXHashCodeNumber, vbNewLine)
        TextBox_HashCodes.Text += String.Format("{0} {1} = {2}{3}", ReSampleStreams.GetType().Name.ToString, NameOf(ReSampleStreams), ReSampleStreams, vbNewLine)
    End Sub
End Class
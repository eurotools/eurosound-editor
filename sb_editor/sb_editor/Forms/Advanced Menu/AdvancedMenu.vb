Partial Public Class AdvancedMenu
    Private ReadOnly writers As New FileWriters

    Private Sub Button_ReAllocateHashcodes_Click(sender As Object, e As EventArgs) Handles Button_ReAllocateHashcodes.Click
        'Check SFX Hashcode
        SFXHashCodeNumber = GetMaxHashCode(fso.BuildPath(WorkingDirectory, "SFXs"))
        SoundBankHashCodeNumber = GetMaxHashCode(fso.BuildPath(WorkingDirectory, "SoundBanks"))
        MFXHashCodeNumber = GetMaxHashCode(fso.BuildPath(WorkingDirectory, "ES_Music\Music\ESData"))
        'Update file
        writers.UpdateMiscFile(fso.BuildPath(WorkingDirectory, "System\Misc.txt"))
    End Sub

    Private Sub Button_CheckForDuplicateHashCodes_Click(sender As Object, e As EventArgs) Handles Button_CheckForDuplicateHashCodes.Click

    End Sub

    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        Close()
    End Sub
End Class
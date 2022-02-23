Partial Public Class FileWriters
    Friend Sub WriteSfxFile(sfxFileObj As SfxFile, textFilePath As String)
        'Replace current file   
        Dim headerLib As New FileParsers
        Dim headerData As FileHeader = GetFileHeaderData(textFilePath, headerLib)

        'Update file
        FileOpen(1, textFilePath, OpenMode.Output, OpenAccess.Write, OpenShare.LockReadWrite)
        PrintLine(1, "## EuroSound SFX File")
        PrintLine(1, "## First Created ... " & headerData.FirstCreated)
        PrintLine(1, "## Created By ... " & headerData.CreatedBy)
        PrintLine(1, "## Last Modified ... " & headerData.LastModify)
        PrintLine(1, "## Last Modified By ... " & headerData.LastModifyBy)
        PrintLine(1, "")
        PrintLine(1, "#SFXParameters")
        PrintLine(1, "ReverbSend  " & sfxFileObj.Parameters.ReverbSend)
        PrintLine(1, "TrackingType  " & sfxFileObj.Parameters.TrackingType)
        PrintLine(1, "InnerRadius  " & sfxFileObj.Parameters.InnerRadius)
        PrintLine(1, "OuterRadius  " & sfxFileObj.Parameters.OuterRadius)
        PrintLine(1, "MaxVoices " & sfxFileObj.Parameters.MaxVoices)
        PrintLine(1, "Action1  " & sfxFileObj.Parameters.Action1)
        PrintLine(1, "Priority  " & sfxFileObj.Parameters.Priority)
        PrintLine(1, "Group  " & 0)
        PrintLine(1, "Action2  " & 0)
        PrintLine(1, "Alertness  " & sfxFileObj.Parameters.Alertness)
        PrintLine(1, "IgnoreAge  " & If(sfxFileObj.Parameters.IgnoreAge, 1, 0))
        PrintLine(1, "Ducker  " & sfxFileObj.Parameters.Ducker)
        PrintLine(1, "DuckerLenght  " & sfxFileObj.Parameters.DuckerLenght)
        PrintLine(1, "MasterVolume  " & sfxFileObj.Parameters.MasterVolume)
        PrintLine(1, "Outdoors  " & If(sfxFileObj.Parameters.Outdoors, 1, 0))
        PrintLine(1, "PauseInNis  " & If(sfxFileObj.Parameters.PauseInNis, 1, 0))
        PrintLine(1, "StealOnAge  " & If(sfxFileObj.Parameters.StealOnAge, 1, 0))
        PrintLine(1, "MusicType  " & If(sfxFileObj.Parameters.MusicType, 1, 0))
        If sfxFileObj.Parameters.Doppler Then
            PrintLine(1, "Doppler  " & If(sfxFileObj.Parameters.Doppler, 1, 0))
        End If
        PrintLine(1, "#END")
        PrintLine(1, "")

        'Write SFXSamplePoolFiles
        PrintLine(1, "#SFXSamplePoolFiles")
        For Each sampleObj As Sample In sfxFileObj.Samples
            PrintLine(1, sampleObj.FilePath)
        Next
        PrintLine(1, "#END")
        PrintLine(1, "")

        'Write SFXSamplePoolModes
        PrintLine(1, "#SFXSamplePoolModes")
        For Each sampleObj As Sample In sfxFileObj.Samples
            PrintLine(1, "BaseVolume  " & sampleObj.BaseVolume)
            PrintLine(1, "PitchOffset  " & sampleObj.PitchOffset)
            PrintLine(1, "RandomPitchOffset  " & sampleObj.RandomPitchOffset)
            PrintLine(1, "RandomVolumeOffset  " & sampleObj.RandomVolumeOffset)
            PrintLine(1, "Pan  " & sampleObj.Pan)
            PrintLine(1, "RandomPan  " & sampleObj.RandomPan)
        Next
        PrintLine(1, "#END")
        PrintLine(1, "")

        'Write SFXSamplePoolControl
        PrintLine(1, "#SFXSamplePoolControl")
        PrintLine(1, "Action1  " & sfxFileObj.SamplePool.Action1)
        PrintLine(1, "RandomPick  " & If(sfxFileObj.SamplePool.RandomPick, 1, 0))
        PrintLine(1, "Shuffled  " & If(sfxFileObj.SamplePool.Shuffled, 1, 0))
        PrintLine(1, "Loop  " & If(sfxFileObj.SamplePool.isLooped, 1, 0))
        PrintLine(1, "Polyphonic  " & If(sfxFileObj.SamplePool.Polyphonic, 1, 0))
        PrintLine(1, "MinDelay  " & sfxFileObj.SamplePool.MinDelay)
        PrintLine(1, "MaxDelay  " & sfxFileObj.SamplePool.MaxDelay)
        PrintLine(1, "EnableSubSFX  " & If(sfxFileObj.SamplePool.EnableSubSFX, 1, 0))
        PrintLine(1, "EnableStereo  " & 0)
        PrintLine(1, "#END")
        PrintLine(1, "")

        'Write HASHCODE
        PrintLine(1, "#HASHCODE")
        PrintLine(1, "HashCodeNumber " & sfxFileObj.HashCode)
        PrintLine(1, "#END")
        FileClose(1)
    End Sub
End Class

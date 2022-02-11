Public Class SfxControl
    ' Define a local variable to store the property value.
    Private samplePropsOn As Boolean = True

    ' Define the property.
    Public Property ShowSampleProperties() As Boolean
        Get
            ' The Get property procedure is called when the value
            ' of a property is retrieved.
            Return samplePropsOn
        End Get
        Set(value As Boolean)
            ' The Set property procedure is called when the value 
            ' of a property is modified.  The value to be assigned
            ' is passed in the argument to Set.
            samplePropsOn = value
            GroupBox_SampleProps.Visible = samplePropsOn
        End Set
    End Property

    'Events
    Public Event SfxControl_LoopChecked As EventHandler
    Public Event SfxControl_SingleChecked As EventHandler
    Public Event SfxControl_MultiSampleChecked As EventHandler
    Public Event SfxControl_ShuffledChecked As EventHandler
    Public Event SfxControl_PolyphonicChecked As EventHandler
    Public Event SfxControl_MinDelayChanged As EventHandler
    Public Event SfxControl_MaxDelayChanged As EventHandler

    '*===============================================================================================
    '* FROM EVENTS
    '*===============================================================================================
    Private Sub SfxParamsAndSamplePool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Reverb track bar
        Textbox_Reverb.DataBindings.Add(New Binding("Text", TrackBar_Reverb, "Value"))
        TrackBar_Reverb.DataBindings.Add(New Binding("Value", Textbox_Reverb, "Text"))

        'Inner radius track bar
        Textbox_InnerRadius.DataBindings.Add(New Binding("Text", TrackBar_InnerRadius, "Value"))
        TrackBar_InnerRadius.DataBindings.Add(New Binding("Value", Textbox_InnerRadius, "Text"))

        'Outer radius track bar
        Textbox_OuterRadius.DataBindings.Add(New Binding("Text", TrackBar_OuterRadius, "Value"))
        TrackBar_OuterRadius.DataBindings.Add(New Binding("Value", Textbox_OuterRadius, "Text"))
    End Sub

    '*===============================================================================================
    '* TRACKING BARS
    '*===============================================================================================
    Private Sub TrackBar_InnerRadius_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_InnerRadius.ValueChanged
        If TrackBar_InnerRadius.Value > TrackBar_OuterRadius.Value Then
            TrackBar_OuterRadius.Value = TrackBar_InnerRadius.Value
        End If
    End Sub

    Private Sub TrackBar_OuterRadius_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_OuterRadius.ValueChanged
        If TrackBar_OuterRadius.Value < TrackBar_InnerRadius.Value Then
            TrackBar_InnerRadius.Value = TrackBar_OuterRadius.Value
        End If
    End Sub

    '*===============================================================================================
    '* CHECKBOXES EVENTS
    '*===============================================================================================
    Private Sub CheckBox_Shuffled_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox_Shuffled.CheckStateChanged
        If CheckBox_Shuffled.Checked Then
            CheckBox_Polyphonic.Checked = False
        End If
        RaiseEvent SfxControl_SingleChecked(Me, e)
    End Sub

    Private Sub CheckBox_Polyphonic_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Polyphonic.CheckedChanged
        If CheckBox_Polyphonic.Checked Then
            CheckBox_Shuffled.Checked = False
        End If
        RaiseEvent SfxControl_PolyphonicChecked(Me, e)
    End Sub

    '*===============================================================================================
    '* TRACK BAR TEXTBOX EVENTS
    '*===============================================================================================
    Private Sub Textbox_Reverb_TextChanged(sender As Object, e As EventArgs) Handles Textbox_Reverb.TextChanged
        If Not Integer.TryParse(Textbox_Reverb.Text, Nothing) Then
            Textbox_Reverb.Text = "0"
        End If
    End Sub

    Private Sub Textbox_InnerRadius_TextChanged(sender As Object, e As EventArgs) Handles Textbox_InnerRadius.TextChanged
        If Not Integer.TryParse(Textbox_InnerRadius.Text, Nothing) Then
            Textbox_InnerRadius.Text = "0"
        End If
    End Sub

    Private Sub Textbox_OuterRadius_TextChanged(sender As Object, e As EventArgs) Handles Textbox_OuterRadius.TextChanged
        If Not Integer.TryParse(Textbox_OuterRadius.Text, Nothing) Then
            Textbox_OuterRadius.Text = "0"
        End If
    End Sub

    '*===============================================================================================
    '* RADIOBUTTONS EVENTS
    '*===============================================================================================
    Private Sub RadioButton_Single_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_Single.CheckedChanged
        If RadioButton_Single.Checked Then
            If Numeric_MinDelay.Value > -1 Then
                'CheckBox_RandomPick.Enabled = True
                CheckBox_Shuffled.Enabled = False
                CheckBox_Polyphonic.Enabled = False
                Numeric_MinDelay.Minimum = 0
                Numeric_MaxDelay.Minimum = 0
            Else
                MsgBox("Inter Sample Delay cannot be negative", vbOKOnly + vbExclamation, "EuroSound")
                RadioButton_MultiSample.Checked = True
            End If
        End If
        RaiseEvent SfxControl_SingleChecked(Me, e)
    End Sub

    Private Sub RadioButton_MultiSample_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_MultiSample.CheckedChanged
        If RadioButton_MultiSample.Checked Then
            'CheckBox_RandomPick.Enabled = False
            CheckBox_RandomPick.Checked = False
            CheckBox_Shuffled.Enabled = True
            CheckBox_Polyphonic.Enabled = True
            Numeric_MinDelay.Minimum = -32000
            Numeric_MaxDelay.Minimum = -32000
        End If
        RaiseEvent SfxControl_MultiSampleChecked(Me, e)
    End Sub

    '*===============================================================================================
    '* CUSTOM EVENTS
    '*===============================================================================================
    Private Sub CheckBox_SamplePoolLoop_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_SamplePoolLoop.CheckedChanged
        RaiseEvent SfxControl_LoopChecked(Me, e)
    End Sub

    Private Sub Numeric_MinDelay_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_MinDelay.ValueChanged
        RaiseEvent SfxControl_MinDelayChanged(Me, e)
    End Sub

    Private Sub Numeric_MaxDelay_ValueChanged(sender As Object, e As EventArgs) Handles Numeric_MaxDelay.ValueChanged
        RaiseEvent SfxControl_MaxDelayChanged(Me, e)
    End Sub
End Class
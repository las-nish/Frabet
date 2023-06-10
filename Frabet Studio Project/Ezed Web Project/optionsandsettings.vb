Public Class optionsandsettings
    Public MySettings(,) As String
    Dim intSettings1 As Integer

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            SaveSetting("ModuleProfiles", cboprof.Text, "Combobox2", Me.clr2.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "textbox5", TextBox5.Text)
            SaveSetting("ModuleProfiles", "ProfileName", cboprof.Text, cboprof.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "Combobox3", Me.clr2.Text)

            SaveSetting("ModuleProfiles", cboprof.Text, "c1", Me.c1.CheckState)
            SaveSetting("ModuleProfiles", cboprof.Text, "c2", Me.c2.CheckState)
            SaveSetting("ModuleProfiles", cboprof.Text, "c3", Me.c3.CheckState)
            SaveSetting("ModuleProfiles", cboprof.Text, "c4", Me.c4.CheckState)
            SaveSetting("ModuleProfiles", cboprof.Text, "c5", Me.c5.CheckState)
            SaveSetting("ModuleProfiles", cboprof.Text, "c6", Me.c6.CheckState)
            SaveSetting("ModuleProfiles", cboprof.Text, "c7", Me.c7.CheckState)
            SaveSetting("ModuleProfiles", cboprof.Text, "c8", Me.c8.CheckState)
            SaveSetting("ModuleProfiles", cboprof.Text, "clr2", Me.clr2.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "clr3", Me.clr3.Text)

            MsgBox("Saved Successfully", MsgBoxStyle.Information, "Saving....")


        Catch ex As Exception

        End Try
    End Sub

    Private Sub optionsandsettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboprof.PerformClick()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListBox1.Items.Add(TextBox1.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ListBox2.Items.Add(TextBox2.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox1.SelectedItems.Clear()
    End Sub


    Private Sub cboprof_SelectedIndexChanged(sender As Object, e As EventArgs)
       

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If String.IsNullOrEmpty(cboprof.Text) Or cboprof.Text = "" Then Exit Sub
        DeleteSetting("ModuleProfiles", cboprof.Text)
        DeleteSetting("ModuleProfiles", "ProfileName", cboprof.Text)
        For Each CNTL As Control In Me.Controls
            If TypeOf CNTL Is TextBox Then
                CNTL.Text = ""
            End If
        Next
        MsgBox("Settings Deleted Successfully !", MsgBoxStyle.Information, "deleting....")
    End Sub

    Private Sub cboprof_Click_1(sender As Object, e As EventArgs) Handles cboprof.Click
        c1.CheckState = GetSetting("ModuleProfiles", cboprof.Text, "c1")
        c2.CheckState = GetSetting("ModuleProfiles", cboprof.Text, "c2")
        c3.CheckState = GetSetting("ModuleProfiles", cboprof.Text, "c3")
        c4.CheckState = GetSetting("ModuleProfiles", cboprof.Text, "c4")
        c5.CheckState = GetSetting("ModuleProfiles", cboprof.Text, "c5")
        c6.CheckState = GetSetting("ModuleProfiles", cboprof.Text, "c6")
        c7.CheckState = GetSetting("ModuleProfiles", cboprof.Text, "c7")
        c8.CheckState = GetSetting("ModuleProfiles", cboprof.Text, "c8")
        clr2.Text = GetSetting("ModuleProfiles", cboprof.Text, "clr2")
        clr3.Text = GetSetting("ModuleProfiles", cboprof.Text, "clr3")

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub clr1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

 
End Class
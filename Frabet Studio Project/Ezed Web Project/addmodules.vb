Public Class addmodules
    Public MySettings(,) As String
    Dim intSettings1 As Integer
    Private Sub addmodules_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            SaveSetting("ModuleProfiles", cboprof.Text, "Combobox2", Me.ComboBox2.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "textbox5", TextBox5.Text)
            SaveSetting("ModuleProfiles", "ProfileName", cboprof.Text, cboprof.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "Combobox3", Me.ComboBox2.Text)

            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL1", md1.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL2", md2.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL3", md3.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL4", md4.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL5", md5.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL7", md7.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL6", md6.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL8", md8.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL9", md9.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagMDL10", md10.Text)


            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL1", m1.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL2", m2.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL3", m3.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL4", m4.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL5", m5.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL6", m6.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL7", m7.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL8", m8.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL9", m9.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagmL10", m10.Text)


            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL1", sd1.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL2", sd2.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL3", sd3.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL4", sd4.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL5", sd5.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL6", sd6.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL7", sd7.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL8", sd8.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL9", sd9.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsdL10", sd10.Text)


            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL1", s1.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL2", s2.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL3", s3.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL4", s4.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL5", s5.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL6", s6.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL7", s7.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL8", s8.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL9", s9.Text)
            SaveSetting("ModuleProfiles", cboprof.Text, "TagsL10", s10.Text)
            MsgBox("Saved Successfully", MsgBoxStyle.Information, "Saving....")
            Me.Visible = False
        Catch ex As Exception
            MessageBox.Show("Saved Faild ! Are you sure you add name to Module .... ", "Something's Wrong !", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cboprof_DropDown(sender As Object, e As EventArgs) Handles cboprof.DropDown
        On Error Resume Next
        MySettings = GetAllSettings("ModuleProfiles", "ProfileName")
        cboprof.Items.Clear()
        For Me.intSettings1 = LBound(MySettings, 1) To UBound(MySettings, 1)
            cboprof.Items.Add(MySettings(intSettings1, 1))
        Next intSettings1
    End Sub


    Private Sub cboprof_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboprof.SelectedIndexChanged
        md1.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL1")
        md2.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL2")
        md3.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL3")
        md4.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL4")
        md5.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL5")
        md6.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL6")
        md7.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL7")
        md8.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL8")
        md9.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL9")
        md10.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagMDL10")

        m1.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL1")
        m2.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL2")
        m3.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL3")
        m4.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL4")
        m5.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL5")
        m6.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL6")
        m7.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL7")
        m8.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL8")
        m9.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL9")
        m10.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagmL10")

        sd1.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL1")
        sd2.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL2")
        sd3.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL3")
        sd4.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL4")
        sd5.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL5")
        sd6.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL6")
        sd7.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL7")
        sd8.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL8")
        sd9.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL9")
        sd10.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsdL10")

        s1.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL1")
        s2.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL2")
        s3.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL3")
        s4.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL4")
        s5.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL5")
        s6.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL6")
        s7.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL7")
        s8.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL8")
        s9.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL9")
        s10.Text = GetSetting("ModuleProfiles", cboprof.Text, "TagsL10")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If String.IsNullOrEmpty(cboprof.Text) Or cboprof.Text = "" Then Exit Sub
        DeleteSetting("ModuleProfiles", cboprof.Text)
        DeleteSetting("ModuleProfiles", "ProfileName", cboprof.Text)
        For Each CNTL As Control In Me.Controls
            If TypeOf CNTL Is TextBox Then
                CNTL.Text = ""
            End If
        Next
        MsgBox("Deleted Successfully !", MsgBoxStyle.Information, "deleting....")
    End Sub
End Class
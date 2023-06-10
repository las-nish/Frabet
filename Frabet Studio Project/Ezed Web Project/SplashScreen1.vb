Public NotInheritable Class SplashScreen1

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Value += 5
        Label3.Text = "" & ProgressBar1.Value & "%"
        If ProgressBar1.Value <= 30 Then
            Label1.Text = "Initializing..........."
            optionsandsettings.cboprof.PerformClick()
        ElseIf ProgressBar1.Value <= 40 Then
            Label1.Text = "Loading components......"
            optionsandsettings.cboprof.PerformClick()
        ElseIf ProgressBar1.Value <= 50 Then
            Label1.Text = "Integrating Database....."
            optionsandsettings.cboprof.PerformClick()
        ElseIf ProgressBar1.Value <= 60 Then
            Label1.Text = "Initializing Languages...."
            optionsandsettings.cboprof.PerformClick()
        ElseIf ProgressBar1.Value <= 70 Then
            Label1.Text = "Loading Files............"
            optionsandsettings.cboprof.PerformClick()
        ElseIf ProgressBar1.Value <= 80 Then
            Label1.Text = "Loding DLL Data......."
            optionsandsettings.cboprof.PerformClick()
        ElseIf ProgressBar1.Value <= 100 Then
            Label1.Text = "Wait .............."
            optionsandsettings.cboprof.PerformClick()
        End If
        If ProgressBar1.Value = 100 Then
            Timer1.Dispose()
            Me.Hide()
            AAAFORM.Show()
        End If
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub SplashScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = "This software was Licened to : " + My.Computer.Info.OSFullName
        Label4.Text = "Total Physical Memory : " + My.Computer.Info.TotalPhysicalMemory.ToString
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub ITalk_ChatBubble_R1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Apex_Separator3_Click(sender As Object, e As EventArgs) Handles Apex_Separator3.Click

    End Sub

    Private Sub ITalk_Label1_Click(sender As Object, e As EventArgs) Handles ITalk_Label1.Click

    End Sub
End Class

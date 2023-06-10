Public Class abouttt

    Private Sub abouttt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start("mailto://lasan.nishshanka@hotmail.com")
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start("mailto://lasan.nishshanka@gmail.com")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start("https://www.twitter.com/@LasanNishshanka")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start("https://www.plus.google.com/LasanNishshanka")
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start("https://www.visualstudio.com")
    End Sub



    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Me.Hide()
    End Sub

    Private Sub Apex_Button1_Click(sender As Object, e As EventArgs)
        Try
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=YXBGUP7Z85GJJ")
            MessageBox.Show("Thanx for trying to Donate to me ....", "Very Very Thank You !", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label53_Click(sender As Object, e As EventArgs) Handles Label53.Click

    End Sub
End Class
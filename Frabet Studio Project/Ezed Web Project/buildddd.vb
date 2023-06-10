Public Class buildddd

    Private Sub buildddd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = optionsandsettings.clr3.SelectedItem
        TextBox4.Text += vbNewLine & "** TYPE : " + AAAFORM.ln2.SelectedItem
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        file.ToolStripButton3.PerformClick()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub
End Class
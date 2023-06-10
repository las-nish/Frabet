Public Class Tool_Add

    Private Sub Tool_Add_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pos As Integer = AAAFORM.txt1.SelectionStart
        AAAFORM.txt1.Text = AAAFORM.txt1.Text.Insert(pos, "<" + ComboBox1.Text + " " + "type=" + zz.Text + a1.Text + zz.Text + " " + "name=" + zz.Text + a2.Text + zz.Text + " " + "value=" + zz.Text + a3.Text + zz.Text + " " + "class=" + zz.Text + a4.Text + zz.Text + " " + "ID=" + zz.Text + a5.Text + zz.Text + " " + "style=" + zz.Text + a6.Text + zz.Text + " " + "title=" + zz.Text + a7.Text + zz.Text + " " + "direction=" + zz.Text + a10.Text + zz.Text + " " + "language=" + zz.Text + a11.Text + zz.Text + " " + "onBlur=" + zz.Text + b1.Text + zz.Text + "onChange=" + zz.Text + b2.Text + zz.Text + " " + "onClick=" + zz.Text + b3.Text + zz.Text + " " + "dblClick=" + zz.Text + b4.Text + zz.Text + " " + "onfocus=" + zz.Text + b5.Text + zz.Text + " " + "onKeyPress=" + zz.Text + b6.Text + zz.Text + " " + "onKeyDown=" + zz.Text + b7.Text + zz.Text + " " + "onKeyUp=" + zz.Text + b8.Text + zz.Text + " " + "onSellect=" + zz.Text + b9.Text + zz.Text + " " + "accesskey=" + zz.Text + a8.Text + zz.Text + " " + "tabindex=" + zz.Text + a9.Text + zz.Text + ">" + " " + "</" + ComboBox1.Text + ">")

    End Sub

    Private Sub a7_TextChanged(sender As Object, e As EventArgs) Handles a7.TextChanged

    End Sub

    Private Sub b7_TextChanged(sender As Object, e As EventArgs) Handles b7.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Refresh()
    End Sub
End Class
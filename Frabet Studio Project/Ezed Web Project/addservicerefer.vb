Public Class addservicerefer
    Inherits System.Windows.Forms.Form
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim txtChanged As Boolean
    Dim theStr As String
    Dim savedToFile As Boolean = False
    Dim savedFilename As String = ""
    Public searchString As String = System.DBNull.Value.ToString

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "DLL File |*.dll"
            openDLG.Title = "Add Service Reference - Browse"
            openDLG.DefaultExt = "*.dll"
            openDLG.AddExtension = False
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamReader(openDLG.FileName, True)
                txt1.Text = s.ReadToEnd
                savedToFile = True
                savedFilename = openDLG.FileName
                openDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Error Browsing File !" & vbNewLine & "DLL fie Not Support !")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        txt1.SelectAll()
        txt1.Copy()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txt1.SelectAll()
        txt1.Copy()
        AAAFORM.txt1.Paste()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            ListBox1.Items.Add("<!-- " + TextBox1.Text + " --!>")
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        AAAFORM.txt1.Text += ListBox1.SelectedItem
    End Sub
End Class
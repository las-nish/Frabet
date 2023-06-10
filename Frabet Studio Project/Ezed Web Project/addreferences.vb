Public Class addreferences
    Inherits System.Windows.Forms.Form
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim txtChanged As Boolean
    Dim theStr As String
    Dim savedToFile As Boolean = False
    Dim savedFilename As String = ""
    Public searchString As String = System.DBNull.Value.ToString

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txt1.SelectAll()
        txt1.Copy()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim saveDLG As New SaveFileDialog
        saveDLG.Filter = "DLL File|*.dll"
        saveDLG.Title = "Reference Script - Publish"
        saveDLG.DefaultExt = "*.dll"
        saveDLG.AddExtension = False
        If saveDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim s As New IO.StreamWriter(saveDLG.FileName, False)
            s.Write(txt1.Text)
            s.Flush()
            savedToFile = True
            savedFilename = saveDLG.FileName
            saveDLG.Dispose()
            s.Close()
            s.Dispose()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            txt1.SelectAll()
            txt1.Copy()
            AAAFORM.txt1.Paste()
        Catch ex As Exception

        End Try
    End Sub
End Class
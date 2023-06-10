Public Class Form2
    Dim txtChanged As Boolean
    Dim theStr As String
    Dim savedToFile As Boolean = False
    Dim savedFilename As String = ""
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        txt1.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "DLL Files|*.dll"
            openDLG.Title = "Frabet Dll file Open"
            openDLG.DefaultExt = ".dll"
            openDLG.AddExtension = False
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamReader(openDLG.FileName, True)
                txt1.Text = s.ReadToEnd
                Label1.Text = "Path :  " + "[ " + openDLG.FileName + " ]"
                Label2.Text = "Enabeld : " + openDLG.Filter + " ( FILTER ENB. )"
                savedToFile = True
                savedFilename = openDLG.FileName
                openDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Error Operning file .... ", "Oops !", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim saveDLG As New SaveFileDialog
            saveDLG.Filter = "DLL File|*.dll"
            saveDLG.Title = "Frabet Dll Publish"
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
        Catch ex As Exception
            MessageBox.Show("Error publishing dll .... ", "Oops !", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class
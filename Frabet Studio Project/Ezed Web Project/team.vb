Public Class newteam
    Inherits System.Windows.Forms.Form
    Public MySettings(,) As String
    Dim intSettings1 As Integer
    Dim ShowPrintDialog As Boolean
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim txtChanged As Boolean
    Dim theStr As String
    Dim savedToFile As Boolean = False
    Dim savedFilename As String = ""
    Public searchString As String = System.DBNull.Value.ToString
    Dim MouseState As String
    Dim pos As Integer
    Dim str As String = ""
    Dim changed As Integer = 0
    Dim zipFile As New Ionic.Zip.ZipFile
    Dim zipEntry As New Ionic.Zip.ZipEntry
    Dim fileToZipUp As String
    Dim fileToUnZip As String

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            txt1.Text += vbNewLine & "NAME: " + TextBox4.Text + " | " + "  PASSWORD: " + TextBox6.Text + " | " + "  TEAM: " + TextBox1.Text
            TextBox5.Text = txt1.Lines.Count - 1
        Catch ex As Exception
            MessageBox.Show("Something's Error ! You can't add Details Now ....", "Oops !", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
       

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim saveDLG As New SaveFileDialog
            saveDLG.Filter = "Frabet Web Team Project File|*.tm"
            saveDLG.Title = "Frabet Web Team Project Save"
            saveDLG.DefaultExt = ".tm"
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
                MessageBox.Show("Team Project will Start !" + vbNewLine & "Total Team Members = " + TextBox5.Text + vbNewLine & "Team Project Title : " + TextBox1.Text + vbNewLine & "User Name : " + TextBox2.Text + vbNewLine & "Password : " + TextBox3.Text, "Working .....", MessageBoxButtons.OK, MessageBoxIcon.Information)
                MessageBox.Show("Processing Complete !" + vbNewLine & "You can Start your Team Project Now ..." + vbNewLine & "Your Team Members .............. " + vbNewLine & txt1.Text, "Process ....", MessageBoxButtons.OK, MessageBoxIcon.Information)
                MessageBox.Show("You like send your Team Deatails to our Server ? If send data to our server's we get it, in your computer is connected to internet ..." + vbNewLine & "Thank You !" + vbNewLine & "Lasan Nishshanka (  CEO )", "Question ?", MessageBoxButtons.OK, MessageBoxIcon.Question)
                Me.Hide()
                startup.ShowDialog()
            End If

        Catch ex As Exception
            MessageBox.Show("Something's Wrong ....", "Oops !", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim saveDLG As New SaveFileDialog
            saveDLG.Filter = "Frabet Web Team Project File|*.tm"
            saveDLG.Title = "Frabet Web Team Project Save"
            saveDLG.DefaultExt = ".tm"
            saveDLG.AddExtension = False
            If saveDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamWriter(saveDLG.FileName, False)
                s.Write(txt1.text)
                s.Flush()
                savedToFile = True
                savedFilename = saveDLG.FileName
                saveDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Something's Wrong ....", "Oops !", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            TextBox5.Text = txt1.Lines.Count - 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
   
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "Frabet Web Team Project File|*.tm"
            openDLG.Title = "Frabet Web Team Project Open"
            openDLG.DefaultExt = ".tm"
            openDLG.AddExtension = False
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamReader(openDLG.FileName, True)
                txt1.text = s.ReadToEnd
                savedToFile = True
                savedFilename = openDLG.FileName
                openDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Something's Wrong ....", "Oops !", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            TextBox5.Text = txt1.Lines.Count - 1
        Catch ex As Exception

        End Try
    End Sub
End Class
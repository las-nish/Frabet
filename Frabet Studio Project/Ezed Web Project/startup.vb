Public Class startup

    Private Sub Ambiance_Button_11_Click(sender As Object, e As EventArgs)
        Me.Hide()
    End Sub

    Private Sub Ambiance_Button_13_Click(sender As Object, e As EventArgs)
        Me.Hide()
        AAAFORM.OpenProjectToolStripMenuItem.PerformClick()
        AAAFORM.Show()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Try
            r3.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Apex_RadioButton4_Click(sender As Object, e As EventArgs) Handles r3.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            If TextBox1.Text = "" Then
                MessageBox.Show("Name is UnSupported !")
            End If
            If TextBox2.Text = "" Then
                MessageBox.Show("Path is UnSupported !")
            End If
            If r1.Checked = True Then
                Me.Hide()
                AAAFORM.Show()
                AAAFORM.txt1.Text = RichTextBox1.Text
                AAAFORM.ln2.Text = r1.Text
                AAAFORM.ToolStripStatusLabel8.Visible = True
                AAAFORM.ToolStripStatusLabel8.Text = "Path : " + TextBox2.Text
            Else
                If r2.Checked = True Then
                    Me.Hide()
                    AAAFORM.Show()
                    AAAFORM.txt1.Text = RichTextBox1.Text
                    AAAFORM.ln2.Text = r2.Text
                    AAAFORM.ToolStripStatusLabel8.Visible = True
                    AAAFORM.ToolStripStatusLabel8.Text = "Path : " + TextBox2.Text
                Else
                    If r3.Checked = True Then
                        Me.Hide()
                        AAAFORM.Show()
                        AAAFORM.txt1.Text = RichTextBox1.Text
                        AAAFORM.ln2.Text = r3.Text
                        AAAFORM.ToolStripStatusLabel8.Visible = True
                        AAAFORM.ToolStripStatusLabel8.Text = "Path : " + TextBox2.Text
                    Else
                        If r4.Checked = True Then
                            Me.Hide()
                            AAAFORM.Show()
                            AAAFORM.txt1.Text = RichTextBox1.Text
                            AAAFORM.ln2.Text = r4.Text
                            AAAFORM.ToolStripStatusLabel8.Visible = True
                            AAAFORM.ToolStripStatusLabel8.Text = "Path : " + TextBox2.Text
                        Else
                            If r5.Checked = True Then
                                Me.Hide()
                                AAAFORM.Show()
                                AAAFORM.txt1.Text = RichTextBox1.Text
                                AAAFORM.ln2.Text = r5.Text
                                AAAFORM.ToolStripStatusLabel8.Visible = True
                                AAAFORM.ToolStripStatusLabel8.Text = "Path : " + TextBox2.Text
                            Else
                                If r6.Checked = True Then
                                    Me.Hide()
                                    AAAFORM.Show()
                                    AAAFORM.txt1.Text = RichTextBox1.Text
                                    AAAFORM.ln2.Text = r6.Text
                                    AAAFORM.ToolStripStatusLabel8.Visible = True
                                    AAAFORM.ToolStripStatusLabel8.Text = "Path : " + TextBox2.Text
                                Else
                                    If r7.Checked = True Then
                                        Me.Hide()
                                        AAAFORM.Show()
                                        AAAFORM.txt1.Text = ""
                                        AAAFORM.ln2.Text = r7.Text
                                        AAAFORM.ToolStripStatusLabel8.Visible = True
                                        AAAFORM.ToolStripStatusLabel8.Text = "Path : " + TextBox2.Text
                                    Else
                                        MessageBox.Show("Selection is UnSupported !")
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        AAAFORM.OpenProjectToolStripMenuItem.PerformClick()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Try
            r1.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Try
            r2.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Try
            r4.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Try
            r5.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Try
            r6.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Try
            r7.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Ambiance_Button_11_Click_1(sender As Object, e As EventArgs) Handles Ambiance_Button_11.Click
        Try
            Process.Start(Application.StartupPath & "/samples/")
        Catch ex As Exception
            MessageBox.Show("Path is Not Found ! Please ReInstall Frabet Studio.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Apex_Button1_Click(sender As Object, e As EventArgs) Handles Apex_Button1.Click
        FolderBrowserDialog1.ShowDialog()
        TextBox2.Text = FolderBrowserDialog1.SelectedPath
    End Sub
End Class
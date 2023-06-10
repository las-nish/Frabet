Public Class file
    Inherits System.Windows.Forms.Form
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim txtChanged As Boolean
    Dim theStr As String
    Dim savedToFile As Boolean = False
    Dim savedFilename As String = ""
    Public searchString As String = System.DBNull.Value.ToString
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        txt1.Clear()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Dim openDLG As New OpenFileDialog
        openDLG.Filter = "All File (*.*)|*.*"
        openDLG.Title = "Frabet - All File Open"
        openDLG.DefaultExt = "."
        openDLG.AddExtension = False
        If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim s As New IO.StreamReader(openDLG.FileName, True)
            txt1.Text = s.ReadToEnd
            Me.Text = Me.Text + "  |  PATH : " + openDLG.FileName
            savedToFile = True
            savedFilename = openDLG.FileName
            openDLG.Dispose()
            s.Close()
            s.Dispose()
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Dim saveDLG As New SaveFileDialog
        saveDLG.Filter = "Frabet File Project|*."
        saveDLG.Title = "Frabet Web - Project Save As File"
        saveDLG.DefaultExt = "."
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

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        txt1.Undo()
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        txt1.Redo()
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        txt1.Cut()
    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        txt1.Copy()
    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        txt1.Paste()
    End Sub

    Private Sub ToolStripButton12_Click(sender As Object, e As EventArgs) Handles ToolStripButton12.Click
        Try
            Dim a As Integer = txt1.SelectionLength
            If a > 0 Then
                a = txt1.SelectionStart
                txt1.Text = (txt1.Text.Remove(txt1.SelectionStart, txt1.SelectionLength))
                txt1.SelectionStart = a
            Else
                a = txt1.SelectionStart
                If a < txt1.Text.Length Then
                    txt1.Text = (txt1.Text.Remove(a, 1))
                    txt1.SelectionStart = a
                End If
            End If
            a = Nothing
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem1.Click
        txt1.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem1.Click
        txt1.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem1.Click
        txt1.Paste()
    End Sub

    Private Sub UndoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem1.Click
        txt1.Undo()
    End Sub

    Private Sub RedoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem1.Click
        txt1.Redo()
    End Sub

    Private Sub SellectAllToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SellectAllToolStripMenuItem1.Click
        txt1.SelectAll()
    End Sub

    Private Sub ClearCurrentLineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearCurrentLineToolStripMenuItem.Click
        txt1.ClearCurrentLine()
    End Sub

    Private Sub ToolStripMenuItem74_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem74.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<> </>")
    End Sub

    Private Sub w1_Click(sender As Object, e As EventArgs) Handles w1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd1.Text)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            w1.Text = addmodules.md1.Text
            w2.Text = addmodules.md2.Text
            w3.Text = addmodules.md3.Text
            w4.Text = addmodules.md4.Text
            w5.Text = addmodules.md5.Text
            w6.Text = addmodules.md6.Text
            w7.Text = addmodules.md7.Text
            w8.Text = addmodules.md8.Text
            w9.Text = addmodules.md9.Text
            w10.Text = addmodules.md10.Text


            q1.Text = addmodules.m1.Text
            q2.Text = addmodules.m2.Text
            q3.Text = addmodules.m3.Text
            q4.Text = addmodules.m4.Text
            q5.Text = addmodules.m5.Text
            q6.Text = addmodules.m6.Text
            q7.Text = addmodules.m7.Text
            q8.Text = addmodules.m8.Text
            q9.Text = addmodules.m9.Text
            q10.Text = addmodules.m10.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EDITToolStripMenuItem1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub w2_Click(sender As Object, e As EventArgs) Handles w2.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd2.Text)
    End Sub

    Private Sub w3_Click(sender As Object, e As EventArgs) Handles w3.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd3.Text)
    End Sub

    Private Sub w4_Click(sender As Object, e As EventArgs) Handles w4.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd4.Text)
    End Sub

    Private Sub w5_Click(sender As Object, e As EventArgs) Handles w5.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd5.Text)
    End Sub

    Private Sub w6_Click(sender As Object, e As EventArgs) Handles w6.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd6.Text)
    End Sub

    Private Sub w7_Click(sender As Object, e As EventArgs) Handles w7.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd7.Text)
    End Sub

    Private Sub w8_Click(sender As Object, e As EventArgs) Handles w8.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd8.Text)
    End Sub

    Private Sub w9_Click(sender As Object, e As EventArgs) Handles w9.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd9.Text)
    End Sub

    Private Sub w10_Click(sender As Object, e As EventArgs) Handles w10.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd10.Text)
    End Sub

    Private Sub q1_Click(sender As Object, e As EventArgs) Handles q1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s1.Text)
    End Sub

    Private Sub q2_Click(sender As Object, e As EventArgs) Handles q2.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s2.Text)
    End Sub

    Private Sub q3_Click(sender As Object, e As EventArgs) Handles q3.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s3.Text)
    End Sub

    Private Sub q4_Click(sender As Object, e As EventArgs) Handles q4.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s4.Text)
    End Sub

    Private Sub q5_Click(sender As Object, e As EventArgs) Handles q5.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s5.Text)
    End Sub

    Private Sub q6_Click(sender As Object, e As EventArgs) Handles q6.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s6.Text)
    End Sub

    Private Sub q7_Click(sender As Object, e As EventArgs) Handles q7.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s7.Text)
    End Sub

    Private Sub q8_Click(sender As Object, e As EventArgs) Handles q8.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s8.Text)
    End Sub

    Private Sub q9_Click(sender As Object, e As EventArgs) Handles q9.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s9.Text)
    End Sub

    Private Sub q10_Click(sender As Object, e As EventArgs) Handles q10.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.s10.Text)
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        Try
            Dim a As Integer = txt1.SelectionLength
            If a > 0 Then
                a = txt1.SelectionStart
                txt1.Text = (txt1.Text.Remove(txt1.SelectionStart, txt1.SelectionLength))
                txt1.SelectionStart = a
            Else
                a = txt1.SelectionStart
                If a < txt1.Text.Length Then
                    txt1.Text = (txt1.Text.Remove(a, 1))
                    txt1.SelectionStart = a
                End If
            End If
            a = Nothing
        Catch exc As Exception

        End Try
    End Sub
End Class
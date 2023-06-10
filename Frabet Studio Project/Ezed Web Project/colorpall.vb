Imports System.Char
Imports System.Text
Imports System.Drawing.Text
Imports System.Drawing.Printing.PrinterResolution
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class colorpall
    Dim myBrush As LinearGradientBrush
    Dim rect As Rectangle
    Dim color1 As Color
    Dim color2 As Color
    Dim gradientMode As LinearGradientMode = LinearGradientMode.BackwardDiagonal
    Private Sub cmbColor1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbColor1.SelectedIndexChanged
        color1 = Color.FromKnownColor(cmbColor1.SelectedItem)
    End Sub

    Private Sub cmbColor2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbColor2.SelectedIndexChanged
        color2 = Color.FromKnownColor(cmbColor2.SelectedItem)
    End Sub

    Private Sub rBackDiag_CheckedChanged(sender As Object, e As EventArgs) Handles rBackDiag.CheckedChanged
        gradientMode = LinearGradientMode.BackwardDiagonal
    End Sub

    Private Sub rForDiag_CheckedChanged(sender As Object, e As EventArgs) Handles rForDiag.CheckedChanged
        gradientMode = LinearGradientMode.ForwardDiagonal
    End Sub

    Private Sub rHorizonal_CheckedChanged(sender As Object, e As EventArgs) Handles rHorizonal.CheckedChanged
        gradientMode = LinearGradientMode.Horizontal
    End Sub

    Private Sub rVertical_CheckedChanged(sender As Object, e As EventArgs) Handles rVertical.CheckedChanged
        gradientMode = LinearGradientMode.Vertical
    End Sub

    Private Sub colorpall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim kColor As KnownColor
        For kColor = KnownColor.AliceBlue To KnownColor.YellowGreen
            cmbColor1.Items.Add(kColor)
            cmbColor2.Items.Add(kColor)
        Next
        cmbColor1.SelectedIndex = 85
        cmbColor2.SelectedIndex = 30
    End Sub

    Private Sub btnDo_Click(sender As Object, e As EventArgs) Handles btnDo.Click
        Try
            If cmbColor1.SelectedIndex < 0 Or cmbColor2.SelectedIndex < 0 Then
                MessageBox.Show("Please select 2 colors to create the gradient from.", " Error", MessageBoxButtons.OK, MessageBoxIcon.Information) : Exit Sub
            End If

            rect = New Rectangle(2, 2, 380, Me.Height / 2 + 15)
            myBrush = New LinearGradientBrush(rect, color1, color2, gradientMode)

            PictureBox9.CreateGraphics.FillRectangle(myBrush, rect)
            Label1.Text = color1.GetHue
            Label2.Text = color2.GetHue
            Label3.Text = color1.GetHashCode
            Label4.Text = color2.GetHashCode
            Label5.Text = color1.ToArgb
            Label6.Text = color2.ToArgb
            Label11.Text = color1.ToKnownColor
            Label12.Text = color2.ToKnownColor
        Catch ex As Exception

        End Try
    End Sub
End Class
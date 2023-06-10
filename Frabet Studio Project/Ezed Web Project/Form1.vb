Option Strict Off
Option Explicit On
Imports System.Char
Imports System.Text
Imports System.Drawing.Text
Imports System.Drawing.Printing.PrinterResolution
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports FastColoredTextBoxNS.PrintDialogSettings
Imports System.Net
Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Windows.Forms
Imports System.Windows.Forms.FormBorderStyle
Imports SpeechLib
Imports Ionic
Imports VB = Microsoft.VisualBasic

Public Class AAAFORM
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
    Dim myBrush As LinearGradientBrush
    Dim rect As Rectangle
    Dim color1 As Color
    Dim color2 As Color
    Dim gradientMode As LinearGradientMode = LinearGradientMode.BackwardDiagonal
    Dim str As String = ""
    Dim changed As Integer = 0
    Dim zipFile As New Ionic.Zip.ZipFile
    Dim zipEntry As New Ionic.Zip.ZipEntry
    Dim fileToZipUp As String
    Dim fileToUnZip As String

    Dim cmp1 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler1/cmp1.dll")
    Dim cmp2 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler2/cmp2.dll")
    Dim cmp3 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler3/cmp3.dll")
    Dim cmp4 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler4/cmp4.dll")
    Dim cmp5 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler5/cmp5.dll")
    Dim cmp6 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler6/cmp6.dll")
    Dim cmp7 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler7/cmp7.dll")
    Dim cmp8 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler8/cmp8.dll")
    Dim cmp9 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler9/cmp9.dll")
    Dim cmp10 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler10/cmp10.dll")
    Dim cmp11 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler11/cmp11.dll")
    Dim cmp12 As String = My.Computer.FileSystem.CombinePath(Application.StartupPath, "compiler/compiler12/cmp12.dll")

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick

    End Sub


    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        drag = False
    End Sub

    Private Sub PictureBox1_Move(sender As Object, e As EventArgs) Handles PictureBox1.Move
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
        End If
    End Sub

    Private Sub ITalk_StatusStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ss.ItemClicked

    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenProjectToolStripMenuItem.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "Frabet Web Project (*.frbtpr)|*.frbtpr"
            openDLG.Title = "Frabet Web Project Open"
            openDLG.DefaultExt = ".frbtpr"
            openDLG.AddExtension = False
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamReader(openDLG.FileName, True)
                txt1.Text = s.ReadToEnd
                Me.Text = "Frabet Studio  " + "[ " + openDLG.FileName + " ]" + "   |   " + openDLG.Filter
                savedToFile = True
                savedFilename = openDLG.FileName
                openDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            If saveDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamWriter(saveDLG.FileName, False)
                s.Write(txt1.Text)
                s.Flush()
                savedToFile = True
                savedFilename = saveDLG.FileName
                saveDLG.Dispose()
                s.Close()
                s.Dispose()
                changed = 0
            End If
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As New IO.StreamWriter(SaveFileDialog1.FileName, False)
                a.Write(txt1.Text)
                a.Flush()
                savedToFile = True
                savedFilename = SaveFileDialog1.FileName
                SaveFileDialog1.Dispose()
                a.Close()
                a.Dispose()
                changed = 0
            End If
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops ! Somethings Wrong ! Error Saving your Project...."
        End Try
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Try
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        txt1.Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        txt1.Redo()
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        txt1.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        txt1.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        txt1.Paste()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
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

    Private Sub SellectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SellectAllToolStripMenuItem.Click
        txt1.SelectAll()
    End Sub

    Private Sub txt1_Load(sender As Object, e As EventArgs) Handles txt1.Load

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ToolStripComboBox1.Text = "Dark ( Default )" Then
            txt1.ForeColor = Color.FromArgb(20, 202, 20)
            txt1.BackColor = Color.FromArgb(34, 34, 34)
            txt1.FoldingIndicatorColor = Color.Gray
            txt1.IndentBackColor = Color.FromArgb(30, 30, 30)
            txt1.LineNumberColor = Color.Gray
            txt1.ServiceLinesColor = Color.Chocolate
        Else
            txt1.ForeColor = Color.Black
            txt1.BackColor = Color.White
            txt1.FoldingIndicatorColor = Color.WhiteSmoke
            txt1.IndentBackColor = Color.WhiteSmoke
            txt1.LineNumberColor = Color.Gray
            txt1.ServiceLinesColor = Color.LightSkyBlue
        End If


        ss.Items.Item(0).Text = "Position: " & txt1.SelectionStart.ToString
        tme.Text = " " + My.Computer.Clock.GmtTime + " "
        cchhr.Text = " Lines: " + txt1.LinesCount.ToString + "     "
        cgh.Text = " Language: " + ln1.Text
        If ln1.Text = "HTML" Then
            txt1.Language = FastColoredTextBoxNS.Language.HTML
        Else
            If ln1.Text = "PHP" Then
                txt1.Language = FastColoredTextBoxNS.Language.PHP
            Else
                If ln1.Text = "SQL" Then
                    txt1.Language = FastColoredTextBoxNS.Language.SQL
                Else
                    If ln1.Text = "CSHARP" Then
                        txt1.Language = FastColoredTextBoxNS.Language.CSharp
                    Else
                        If ln1.Text = "VB" Then
                            txt1.Language = FastColoredTextBoxNS.Language.VB
                        Else
                            If ln1.Text = "CUSTOM" Then
                                txt1.Language = FastColoredTextBoxNS.Language.Custom
                            Else
                                If ln1.Text = "C++" Then
                                    txt1.Language = FastColoredTextBoxNS.Language.CSharp
                                Else
                                    If ln1.Text = "C" Then
                                        txt1.Language = FastColoredTextBoxNS.Language.CSharp
                                    Else
                                        If ln1.Text = "XHP" Then
                                            txt1.Language = FastColoredTextBoxNS.Language.PHP
                                        Else
                                            If ln1.Text = "YAML" Then
                                                txt1.Language = FastColoredTextBoxNS.Language.HTML
                                            Else
                                                If ln1.Text = "XML" Then
                                                    txt1.Language = FastColoredTextBoxNS.Language.HTML
                                                Else
                                                    If ln1.Text = "D" Then
                                                        txt1.Language = FastColoredTextBoxNS.Language.CSharp
                                                    Else
                                                        txt1.Language = FastColoredTextBoxNS.Language.HTML
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub NewPlanProjectToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            Process.Start(Application.StartupPath & "plan.exe")
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "File is Not Found ! Please ReInstall Software !"
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub AAAFORM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            SaveSetting("lastupdate", cboprof.Text, "textbox1", txt1.Text)
            SaveSetting("lastupdate", cboprof.Text, "ln1", ln1.Text)
            SaveSetting("lastupdate", cboprof.Text, "ln2", ln2.Text)
            SaveSetting("lastupdate", cboprof.Text, "me", Me.Text)
            SaveSetting("lastupdate", cboprof.Text, "rg1", ToolStripTextBox2.Text)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Auto Mouse Click Settings Wrong ! Last Work is not Saved !"
        End Try
    End Sub

    Private Sub AAAFORM_Load(sender As Object, e As EventArgs) Handles Me.Load
        cc1.Text = My.Computer.FileSystem.ReadAllText(cmp1)
        cc2.Text = My.Computer.FileSystem.ReadAllText(cmp2)
        cc3.Text = My.Computer.FileSystem.ReadAllText(cmp3)
        cc4.Text = My.Computer.FileSystem.ReadAllText(cmp4)
        cc5.Text = My.Computer.FileSystem.ReadAllText(cmp5)
        cc6.Text = My.Computer.FileSystem.ReadAllText(cmp6)
        cc7.Text = My.Computer.FileSystem.ReadAllText(cmp7)
        cc8.Text = My.Computer.FileSystem.ReadAllText(cmp8)
        cc9.Text = My.Computer.FileSystem.ReadAllText(cmp9)
        cc10.Text = My.Computer.FileSystem.ReadAllText(cmp10)
        cc11.Text = My.Computer.FileSystem.ReadAllText(cmp11)
        cc12.Text = My.Computer.FileSystem.ReadAllText(cmp12)
        optionsandsettings.cboprof.PerformClick()
        cboprof.PerformClick()
        Me.Refresh()
        Me.Refresh()
        Me.Refresh()

        If ToolStripTextBox2.Text = "GY4KI-5V4OK-KDH4T-5ST95" Then
            Me.Refresh()
        Else
            alert.Visible = True
            alert.Text = "Frabet Studio is Not Registered ! Please Register Now !"
        End If
    End Sub

    Private Sub RestartFrabetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartFrabetToolStripMenuItem.Click
        Application.Restart()
    End Sub

    Private Sub NewTeamProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTeamProjectToolStripMenuItem.Click
        Try
            newteam.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NewEncryptedFileToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub NewPreviewDocumentToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub NewProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewProjectToolStripMenuItem.Click
        Try
            startup.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Ambiance_ProgressBar1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub HTMLFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HTMLFileToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW HTML FILE"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub AddReferenceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddReferenceToolStripMenuItem.Click
        Try
            addreferences.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddServiceReferenceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddServiceReferenceToolStripMenuItem.Click
        Try
            addservicerefer.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddModuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddModuleToolStripMenuItem.Click
        Try
            addmodules.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OptionsAndSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsAndSettingsToolStripMenuItem.Click
        Try
            optionsandsettings.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AdvancedWEBCompilerSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedWEBCompilerSettingsToolStripMenuItem.Click
        Try
            optionsandsettings.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PHPFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PHPFileToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW PHP FILE"
            file.txt1.Language = FastColoredTextBoxNS.Language.PHP
        Catch exc As Exception

        End Try
    End Sub

    Private Sub SQLFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SQLFileToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW SQL FILE"
            file.txt1.Language = FastColoredTextBoxNS.Language.SQL
        Catch exc As Exception

        End Try
    End Sub

    Private Sub VBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VBToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW VB FILE"
            file.txt1.Language = FastColoredTextBoxNS.Language.VB
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CSHARPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CSHARPToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW CSHARP FILE"
            file.txt1.Language = FastColoredTextBoxNS.Language.CSharp
        Catch exc As Exception

        End Try
    End Sub

    Private Sub YMALToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YMALToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW YMAL FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub XMLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XMLToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW XML FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub AJAXToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AJAXToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW AJAX FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub ASPNETToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ASPNETToolStripMenuItem1.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW ASP FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW C FILE"
            file.txt1.Language = FastColoredTextBoxNS.Language.CSharp
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CFILEToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CFILEToolStripMenuItem1.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW C++ FILE"
            file.txt1.Language = FastColoredTextBoxNS.Language.CSharp
        Catch exc As Exception

        End Try
    End Sub

    Private Sub DSCRIPTToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DSCRIPTToolStripMenuItem1.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW D FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub ERLANGFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ERLANGFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW ERLANG FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub GOFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GOFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW GO FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub HACKFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HACKFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW HACK FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub PERLFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PERLFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW PERL FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub PYTHONFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PYTHONFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW PYTHON FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub RUBYFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RUBYFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW RUBY FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub SCALAFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SCALAFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW SCALA FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub XHPFILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XHPFILEToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW XHP FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CUSTOMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CUSTOMToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            file.txt1.Language = FastColoredTextBoxNS.Language.Custom
            frm.Text = "NEW CUSTOM FILE"
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CSSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CSSToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW CSS SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub JAVASCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JAVASCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW JAVA SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub VBSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VBSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW VB SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.VB
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW CSHARP SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.CSharp
        Catch exc As Exception

        End Try
    End Sub

    Private Sub AJAXToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AJAXToolStripMenuItem1.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW AJAX SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub XMLToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles XMLToolStripMenuItem1.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW XML SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub YMALToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles YMALToolStripMenuItem1.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW YMAL SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub ASPNETSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASPNETSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW ASP.NET SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CSCRIPTToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CSCRIPTToolStripMenuItem1.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW C SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.CSharp
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CSCRIPTToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles CSCRIPTToolStripMenuItem2.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW C++ SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.CSharp
        Catch exc As Exception

        End Try
    End Sub

    Private Sub DSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW D SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub ERLANGSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ERLANGSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW EARLANG SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub GOSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GOSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW GO SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub HACKSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HACKSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW HACK SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub PERLSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PERLSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW PERL SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub PYTHONSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PYTHONSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW PYTHON SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub RUBYSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RUBYSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW RUBY SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub SCALASCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SCALASCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW SCALA SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.HTML
        Catch exc As Exception

        End Try
    End Sub

    Private Sub XHPSCRIPTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XHPSCRIPTToolStripMenuItem.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW XHP SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.PHP
        Catch exc As Exception

        End Try
    End Sub

    Private Sub CUSTOMToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles CUSTOMToolStripMenuItem2.Click
        Try
            Dim frm As Form = New file
            frm.Show(Me)
            frm.Text = "NEW CUSTOM SCRIPT"
            file.txt1.Language = FastColoredTextBoxNS.Language.Custom
        Catch exc As Exception

        End Try
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
        DeleteToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs)
        CUSTOMToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripSplitButton2_Click(sender As Object, e As EventArgs) Handles ToolStripSplitButton2.Click
        CUSTOMToolStripMenuItem2.PerformClick()
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        PrintToolStripMenuItem.PerformClick()
    End Sub
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        SaveToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        OpenProjectToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        NewProjectToolStripMenuItem.PerformClick()
    End Sub

    Private Sub NewEnvironmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewEnvironmentToolStripMenuItem.Click
        Try
            Dim frm As Form = New AAAFORM
            frm.Show()
            PictureBox6.BackColor = Color.Chocolate
        Catch exc As Exception

        End Try
    End Sub

    Private Sub ToolStripButton13_Click(sender As Object, e As EventArgs) Handles ToolStripButton13.Click
        Try
            BuildProjectToolStripMenuItem.PerformClick()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ToolStripButton14_Click(sender As Object, e As EventArgs) Handles ToolStripButton14.Click
        StartDebugToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton15_Click(sender As Object, e As EventArgs) Handles ToolStripButton15.Click
        Try
            OptionsAndSettingsToolStripMenuItem.PerformClick()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub ToolStripButton16_Click(sender As Object, e As EventArgs) Handles ToolStripButton16.Click
        AdvancedWEBCompilerSettingsToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton25_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripButton17_Click(sender As Object, e As EventArgs) Handles ToolStripButton17.Click
        Try
            Tool_Add.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub EDITToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EDITToolStripMenuItem1.Click
        AddModuleToolStripMenuItem.PerformClick()
    End Sub

    Private Sub EDITToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles EDITToolStripMenuItem2.Click
        AddModuleToolStripMenuItem.PerformClick()
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

    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        DeleteToolStripMenuItem.PerformClick()
    End Sub

    Private Sub OpenEncryptedFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenEncryptedFileToolStripMenuItem.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "Frabet Web File (*.*)|*.*"
            openDLG.Title = "Frabet Web File Open"
            openDLG.DefaultExt = "."
            openDLG.AddExtension = False
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamReader(openDLG.FileName, True)
                txt1.Text = s.ReadToEnd
                Me.Text = "Frabet Studio  " + "[ " + openDLG.FileName + " ]" + "   |   " + openDLG.Filter + " // FILE //"
                savedToFile = True
                savedFilename = openDLG.FileName
                openDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        Try
            txt1.Print()
        Catch exc As Exception
            alert.Visible = True
            alert.Text = "Somethings Wrong ! You cant print Now ...."
        End Try
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try            '

        Catch exc As Exception
            alert.Visible = True
            alert.Text = "Somethings Wrong ! You cant print Now, because Printers are Not Installed ...."
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)

    End Sub

    Private Sub HELPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HELPToolStripMenuItem.Click

    End Sub

    Private Sub SaveAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAllToolStripMenuItem.Click
        SaveAsToolStripMenuItem.PerformClick()
        file.ToolStripButton3.PerformClick()
    End Sub

    Private Sub ClearCurrentLineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearCurrentLineToolStripMenuItem.Click
        txt1.ClearCurrentLine()
    End Sub

    Private Sub IDOCKTYPEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IDOCKTYPEToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<iDOCKTYPE>")
    End Sub

    Private Sub HtmlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HtmlToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<html></html>")
    End Sub

    Private Sub TitleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TitleToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<title></title>")
    End Sub

    Private Sub BodyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BodyToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<body></body>")
    End Sub

    Private Sub ToolStripMenuItem44_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem44.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<!--    --!>")
    End Sub

    Private Sub AddresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddresToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<addres></addres>")
    End Sub

    Private Sub AbbrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbbrToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<abbr></abbr>")
    End Sub

    Private Sub AcronymToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcronymToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<acronym></acronym>")
    End Sub

    Private Sub BToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<b></b>")
    End Sub

    Private Sub BdiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BdiToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<bdi></bdi>")
    End Sub

    Private Sub BdoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BdoToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<bdo></bdo>")
    End Sub

    Private Sub BigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BigToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<big></big>")
    End Sub

    Private Sub BlockquoteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BlockquoteToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<blockquote></blockquote>")
    End Sub

    Private Sub CenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CenterToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<center></center>")
    End Sub

    Private Sub CiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CiteToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<cite></cite>")
    End Sub

    Private Sub CodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CodeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<code></code>")
    End Sub

    Private Sub DelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<del></del>")
    End Sub

    Private Sub DfnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DfnToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<dfn></dfn>")
    End Sub

    Private Sub EmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<em></em>")
    End Sub

    Private Sub FontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<font></font>")
    End Sub

    Private Sub InsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles InsToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<ins></ins>")
    End Sub

    Private Sub KbdToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles KbdToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<kbd></kbd>")
    End Sub

    Private Sub MarkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarkToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<mark></mark>")
    End Sub

    Private Sub MarqueeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarqueeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<marquee></marquee>")
    End Sub

    Private Sub MeterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeterToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<meter></meter>")
    End Sub

    Private Sub ToolStripMenuItem74_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem74.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<> </>")
    End Sub

    Private Sub PreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<pre></pre>")
    End Sub

    Private Sub PrograssToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrograssToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<prograss></prograss>")
    End Sub

    Private Sub QToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<q></q>")
    End Sub

    Private Sub IToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles IToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<i></i>")
    End Sub

    Private Sub RtToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RtToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<rt></rt>")
    End Sub

    Private Sub RpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RpToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<rp></rp>")
    End Sub

    Private Sub RubyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RubyToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<ruby></ruby>")
    End Sub

    Private Sub SmallToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SmallToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<small></small>")
    End Sub

    Private Sub SampToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SampToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<samp></samp>")
    End Sub

    Private Sub SToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<s></s>")
    End Sub

    Private Sub StrikeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StrikeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<strike></strike>")
    End Sub

    Private Sub StrongToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles StrongToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<strong></strong>")
    End Sub

    Private Sub SubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<sub></sub>")
    End Sub

    Private Sub SupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<sup></sup>")
    End Sub

    Private Sub TimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<time></time>")
    End Sub

    Private Sub TtToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TtToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<tt></tt>")
    End Sub

    Private Sub UToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles UToolStripMenuItem2.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<u></u>")
    End Sub

    Private Sub VToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VToolStripMenuItem.Click

    End Sub

    Private Sub VarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VarToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<var></var>")
    End Sub

    Private Sub WbrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WbrToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<wbr></wbr>")
    End Sub

    Private Sub ButtonToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ButtonToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<button></button>")
    End Sub

    Private Sub DatalistToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatalistToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<datalist></datalist>")
    End Sub

    Private Sub FormToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FormToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<form></form>")
    End Sub

    Private Sub FieldsetToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FieldsetToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<fieldset></fieldset>")
    End Sub

    Private Sub InputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<input></input>")
    End Sub

    Private Sub LabelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LabelToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<label></label>")
    End Sub

    Private Sub LegendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LegendToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<legend></legend>")
    End Sub

    Private Sub OutputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutputToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<output></output>")
    End Sub

    Private Sub OptgroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptgroupToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<optgroup></optgroup>")
    End Sub

    Private Sub OptionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<option></option>")
    End Sub

    Private Sub KeygenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeygenToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<keygen></keygen>")
    End Sub

    Private Sub FrameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FrameToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<frame></frame>")
    End Sub

    Private Sub FramesetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FramesetToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<frameset></frameset>")
    End Sub

    Private Sub NoframeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoframeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<noframe></noframe>")
    End Sub

    Private Sub IframeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IframeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<iframe></iframe>")
    End Sub

    Private Sub ImgToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImgToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<img></img>")
    End Sub

    Private Sub MapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MapToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<map></map>")
    End Sub

    Private Sub AreaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AreaToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<area></area>")
    End Sub

    Private Sub CanvesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CanvesToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<canves></canves>")
    End Sub

    Private Sub FigcaptionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FigcaptionToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<figcaption></figcaption>")
    End Sub

    Private Sub FigureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FigureToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<figure></figure>")
    End Sub

    Private Sub AudioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AudioToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<audio></audio>")
    End Sub

    Private Sub SourceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SourceToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<source></source>")
    End Sub

    Private Sub TrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrackToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<track></track>")
    End Sub

    Private Sub VideoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VideoToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<video></video>")
    End Sub

    Private Sub AToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<a></a>")
    End Sub

    Private Sub LinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LinkToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<link></link>")
    End Sub

    Private Sub NavToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NavToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<nav></nav>")
    End Sub

    Private Sub UlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UlToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<ul></ul>")
    End Sub

    Private Sub OlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OlToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<ol></ol>")
    End Sub

    Private Sub LiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<li></li>")
    End Sub

    Private Sub DirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DirToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<dir></dir>")
    End Sub

    Private Sub DlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DlToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<dl></dl>")
    End Sub

    Private Sub DtToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DtToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<dt></dt>")
    End Sub

    Private Sub DdToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DdToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<dd></dd>")
    End Sub

    Private Sub MenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<menu></menu>")
    End Sub

    Private Sub MenuitemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuitemToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<menuitem></menuitem>")
    End Sub

    Private Sub TableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TableToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<table></table>")
    End Sub

    Private Sub CaptionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CaptionToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<caption></caption>")
    End Sub

    Private Sub ThToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<th></th>")
    End Sub

    Private Sub TrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<tr></tr>")
    End Sub

    Private Sub TdToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TdToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<td></td>")
    End Sub

    Private Sub TheadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TheadToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<thead></thead>")
    End Sub

    Private Sub TbodyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TbodyToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<tbody></tbody>")
    End Sub

    Private Sub TfootToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TfootToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<tfoot></tfoot>")
    End Sub

    Private Sub ColToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<col></col>")
    End Sub

    Private Sub ColgroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColgroupToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<colgroup></colgroup>")
    End Sub

    Private Sub StyleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StyleToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<style></style>")
    End Sub

    Private Sub DivToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DivToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<div></div>")
    End Sub

    Private Sub SpanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpanToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<span></span>")
    End Sub

    Private Sub HeaderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HeaderToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<header></header>")
    End Sub

    Private Sub FooterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FooterToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<footer></footer>")
    End Sub

    Private Sub MainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MainToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<main></main>")
    End Sub

    Private Sub SectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SectionToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<section></section>")
    End Sub

    Private Sub ArticleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArticleToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<article></article>")
    End Sub

    Private Sub AsideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsideToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<aside></aside>")
    End Sub

    Private Sub DetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailsToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<details></details>")
    End Sub

    Private Sub SummeryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SummeryToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<summery></summery>")
    End Sub

    Private Sub HeadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HeadToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<head></head>")
    End Sub

    Private Sub MetaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MetaToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<meta></meta>")
    End Sub

    Private Sub BaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BaseToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<base></base>")
    End Sub

    Private Sub BasefrontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BasefrontToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<basefront></basefront>")
    End Sub

    Private Sub SvriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SvriptToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<script></script>")
    End Sub

    Private Sub NoscriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoscriptToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<noscript></noscript>")
    End Sub

    Private Sub AppletToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AppletToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<applet></applet>")
    End Sub

    Private Sub EnbedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnbedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<enbed></enbed>")
    End Sub

    Private Sub ObjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObjectToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<object></object>")
    End Sub

    Private Sub ParamToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParamToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<param></param>")
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TopMostToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TopMostToolStripMenuItem.Click
        If TopMostToolStripMenuItem.Checked = True Then
            Me.TopMost = True
        End If
        If TopMostToolStripMenuItem.Checked = False Then
            Me.TopMost = False
        End If

    End Sub
    Private Sub WordWrapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WordWrapToolStripMenuItem.Click
        If WordWrapToolStripMenuItem.Checked = True Then
            txt1.WordWrap = True
        End If
        If WordWrapToolStripMenuItem.Checked = False Then
            txt1.WordWrap = False
        End If
    End Sub

    Private Sub AutoWordSellectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoWordSellectToolStripMenuItem.Click
        If AutoWordSellectToolStripMenuItem.Checked = True Then
            txt1.AutoIndent = True
        End If
        If AutoWordSellectToolStripMenuItem.Checked = False Then
            txt1.AutoIndent = False
        End If
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        If StatusBarToolStripMenuItem.Checked = True Then
            ss.Visible = True
        End If
        If StatusBarToolStripMenuItem.Checked = False Then
            ss.Visible = False
        End If
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        If ToolBarToolStripMenuItem.Checked = True Then
            ToolStrip1.Visible = True
        End If
        If ToolBarToolStripMenuItem.Checked = False Then
            ToolStrip1.Visible = False
        End If
    End Sub

    Private Sub ImportantToolBoxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportantToolBoxToolStripMenuItem.Click
        If ImportantToolBoxToolStripMenuItem.Checked = True Then
            ToolStrip2.Visible = True
        End If
        If ImportantToolBoxToolStripMenuItem.Checked = False Then
            ToolStrip2.Visible = False
        End If
    End Sub

    Private Sub OnafterprintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnafterprintToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onafterprint")
    End Sub

    Private Sub OnbeforeprintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnbeforeprintToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onbeforeprint")
    End Sub

    Private Sub OnbeforeunloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnbeforeunloadToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onbeforeunload")
    End Sub

    Private Sub OnerrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnerrorToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onerror")
    End Sub

    Private Sub OnhashchangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnhashchangeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onhashchange")
    End Sub

    Private Sub OnloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnloadToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onload")
    End Sub

    Private Sub OnmessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnmessageToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onmessage")
    End Sub

    Private Sub OnofflineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnofflineToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onoffline")
    End Sub

    Private Sub OnonlineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnonlineToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ononline")
    End Sub

    Private Sub OnpagehideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnpagehideToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onpagehide")
    End Sub

    Private Sub OnpageshowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnpageshowToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onpageshow")
    End Sub

    Private Sub OnpopstateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnpopstateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onpopstate")
    End Sub

    Private Sub OnresizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnresizeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onresize")
    End Sub

    Private Sub OnstorageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnstorageToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onstorage")
    End Sub

    Private Sub OnunloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnunloadToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onunload")
    End Sub

    Private Sub OnblurToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnblurToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onblur")
    End Sub

    Private Sub OnchangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnchangeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onchange")
    End Sub

    Private Sub OncontextmenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OncontextmenuToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "oncontextmenu")
    End Sub

    Private Sub OnfocusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnfocusToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onfocus")
    End Sub

    Private Sub OninputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OninputToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "oninput")
    End Sub

    Private Sub OninvalidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OninvalidToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "oninvalid")
    End Sub

    Private Sub OnresetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnresetToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onreset")
    End Sub

    Private Sub OnsearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnsearchToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onsearch")
    End Sub

    Private Sub OnselectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnselectToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onselect")
    End Sub

    Private Sub OnsubmitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnsubmitToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onsubmit")
    End Sub

    Private Sub OnkeydownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnkeydownToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onkeydown")
    End Sub

    Private Sub OnkeypressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnkeypressToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onkeypress")
    End Sub

    Private Sub OnkeypressToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OnkeypressToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onkeyup")
    End Sub

    Private Sub OnclickToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnclickToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onclick")
    End Sub

    Private Sub OndblclickToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndblclickToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondblclick")
    End Sub

    Private Sub OndragToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndragToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondrag")
    End Sub

    Private Sub OndragendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndragendToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondragend")
    End Sub

    Private Sub OndragenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndragenterToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondragenter")
    End Sub

    Private Sub OndragleaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndragleaveToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondragleave")
    End Sub

    Private Sub OndragoverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndragoverToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondragover")
    End Sub

    Private Sub OndragstartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndragstartToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondragstart")
    End Sub

    Private Sub OndropToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndropToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondrop")
    End Sub

    Private Sub OnmousedownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnmousedownToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onmousedown")
    End Sub

    Private Sub OnmousemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnmousemoveToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onmousemove")
    End Sub

    Private Sub OnmouseoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnmouseoutToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onmouseout")
    End Sub

    Private Sub OnmouseoverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnmouseoverToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onmouseover")
    End Sub

    Private Sub OnmouseupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnmouseupToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onmouseup")
    End Sub

    Private Sub OnmousewheelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnmousewheelToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onmousewheel")
    End Sub

    Private Sub OnscrollToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnscrollToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onscroll")
    End Sub

    Private Sub OnwheelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnwheelToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onwheel")
    End Sub

    Private Sub OncopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OncopyToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "oncopy")
    End Sub

    Private Sub OncutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OncutToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "oncut")
    End Sub

    Private Sub OnpasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnpasteToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onpaste")
    End Sub

    Private Sub OnerrorToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles OnerrorToolStripMenuItem2.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onerror")
    End Sub

    Private Sub OnshowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnshowToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onshow")
    End Sub

    Private Sub OntoggleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OntoggleToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ontoggle")
    End Sub

    Private Sub OnabortToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnabortToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onabort")
    End Sub

    Private Sub OncanplayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OncanplayToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "oncanplay")
    End Sub

    Private Sub OncanplaythroughToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OncanplaythroughToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "oncanplaythrough")
    End Sub

    Private Sub OncuechangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OncuechangeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "oncuechange")
    End Sub

    Private Sub OndurationchangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OndurationchangeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ondurationchange")
    End Sub

    Private Sub OnemptiedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnemptiedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onemptied")
    End Sub

    Private Sub OnendedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnendedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onended")
    End Sub

    Private Sub OnerrorToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OnerrorToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onerror")
    End Sub

    Private Sub OnloadeddataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnloadeddataToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onloadeddata")
    End Sub

    Private Sub OnloadedmetadataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnloadedmetadataToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onloadedmetadata")
    End Sub

    Private Sub OnloadstartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnloadstartToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onloadstart")
    End Sub

    Private Sub OnpauseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnpauseToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onpause")
    End Sub

    Private Sub OnplayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnplayToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onplay")
    End Sub

    Private Sub OnplayingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnplayingToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onplaying")
    End Sub

    Private Sub OnprogressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnprogressToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onprogress")
    End Sub

    Private Sub OnratechangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnratechangeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onratechange")
    End Sub

    Private Sub OnseekedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnseekedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onseeked")
    End Sub

    Private Sub OnseekingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnseekingToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onseeking")
    End Sub

    Private Sub OnstalledToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnstalledToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onstalled")
    End Sub

    Private Sub OnsuspendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnsuspendToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onsuspend")
    End Sub

    Private Sub OntimeupdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OntimeupdateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ontimeupdate")
    End Sub

    Private Sub OnvolumechangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnvolumechangeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onvolumechange")
    End Sub

    Private Sub OnwaitingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnwaitingToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "onwaiting")
    End Sub

    Private Sub AddTextTrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddTextTrackToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "addtexttrack()")
    End Sub

    Private Sub CanPlayTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CanPlayTypeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "canplaytype()")
    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "load()")
    End Sub

    Private Sub PlayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "play()")
    End Sub

    Private Sub PauseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PauseToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "pause()")
    End Sub

    Private Sub AudioTracksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AudioTracksToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "audiotrack")
    End Sub

    Private Sub AutoplayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoplayToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "autoplay")
    End Sub

    Private Sub BufferedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BufferedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "buffered")
    End Sub

    Private Sub ControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ControllerToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "controller")
    End Sub

    Private Sub ControlsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ControlsToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "controls")
    End Sub

    Private Sub CrossOriginToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrossOriginToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "crossOrigin")
    End Sub

    Private Sub CurrentSrcToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CurrentSrcToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "currentsrc")
    End Sub

    Private Sub CurrentTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CurrentTimeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "currenttime")
    End Sub

    Private Sub DefaultMutedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefaultMutedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "defaultmuted")
    End Sub

    Private Sub DefaultPlaybackRateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefaultPlaybackRateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "defaultplaybackrate")
    End Sub

    Private Sub DurationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DurationToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "duration")
    End Sub

    Private Sub EndedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EndedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ended")
    End Sub

    Private Sub ErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ErrorToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "error")
    End Sub

    Private Sub LoopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "loop")
    End Sub

    Private Sub MediaGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MediaGroupToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "mediagroup")
    End Sub

    Private Sub MutedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MutedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "muted")
    End Sub

    Private Sub NetworkStateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NetworkStateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "networkstate")
    End Sub

    Private Sub PausedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PausedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "paused")
    End Sub

    Private Sub PlaybackRateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlaybackRateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "playbackrate")
    End Sub

    Private Sub PlayedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayedToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "played")
    End Sub

    Private Sub PreloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreloadToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "preload")
    End Sub

    Private Sub ReadyStateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReadyStateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "readyState")
    End Sub

    Private Sub SeekableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeekableToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "seekable")
    End Sub

    Private Sub SeekingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeekingToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "seeking")
    End Sub

    Private Sub SrcToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SrcToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "src")
    End Sub

    Private Sub StratDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StratDateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "stratdate")
    End Sub

    Private Sub TextTracksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TextTracksToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "textTracks")
    End Sub

    Private Sub VideoTracksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VideoTracksToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "videoTracks")
    End Sub

    Private Sub VolumeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VolumeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "volume")
    End Sub

    Private Sub PixelManipulationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PixelManipulationToolStripMenuItem.Click

    End Sub

    Private Sub FillStyleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FillStyleToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "fillstyle")
    End Sub

    Private Sub StrokeStyleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StrokeStyleToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "strokestyle")
    End Sub

    Private Sub ShadowColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShadowColorToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "shadowcolor")
    End Sub

    Private Sub ShadowBlurToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShadowBlurToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "shadowblur")
    End Sub

    Private Sub ShadowOffsetXToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShadowOffsetXToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "shadowoffsetX")
    End Sub

    Private Sub ShadowOffsetYToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShadowOffsetYToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "shadowoffsetY")
    End Sub

    Private Sub CreateLinearGradientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateLinearGradientToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "createLinearGradient()")
    End Sub

    Private Sub CreatePatternToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreatePatternToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "createPattern()")
    End Sub

    Private Sub CreateRadialGradientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateRadialGradientToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "createRadialGradient()")
    End Sub

    Private Sub AddColorStopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddColorStopToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "addColorStop()")
    End Sub

    Private Sub LineCapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LineCapToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "linecap")
    End Sub

    Private Sub LineJoinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LineJoinToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "linejoin")
    End Sub

    Private Sub LineWidthToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LineWidthToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "linewidth")
    End Sub

    Private Sub MiterLimitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MiterLimitToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "miterlimit")
    End Sub

    Private Sub RectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RectToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "rect()")
    End Sub

    Private Sub FillRectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FillRectToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "fillrect()")
    End Sub

    Private Sub StrokeRectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StrokeRectToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "strokerect()")
    End Sub

    Private Sub ClearRectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearRectToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "clearrect()")
    End Sub

    Private Sub FillToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FillToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "fill()")
    End Sub

    Private Sub StrokeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StrokeToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "stroke()")
    End Sub

    Private Sub BeginPathToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BeginPathToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "beginpath()")
    End Sub

    Private Sub MoveToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoveToToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "moveto()")
    End Sub

    Private Sub ClosePathToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClosePathToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "closepath()")
    End Sub

    Private Sub LineToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LineToToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "lineto()")
    End Sub

    Private Sub ClipToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClipToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "clip()")
    End Sub

    Private Sub QuadraticCurveToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuadraticCurveToToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "quadraticCurveTo()")
    End Sub

    Private Sub BezierCurveToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BezierCurveToToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "bezierCurveTo()")
    End Sub

    Private Sub ArcToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArcToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "arc()")
    End Sub

    Private Sub ArcToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArcToToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "arcTo()")
    End Sub

    Private Sub IsPointInPathToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IsPointInPathToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "ispointinPath()")
    End Sub

    Private Sub WidthToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WidthToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "width")
    End Sub

    Private Sub HeightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HeightToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "height")
    End Sub

    Private Sub DataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "data")
    End Sub

    Private Sub CreateImageDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateImageDataToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "CreateImageData()")
    End Sub

    Private Sub GetImageDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetImageDataToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "GetImageData()")
    End Sub

    Private Sub PutImageDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PutImageDataToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "PutImageData()")
    End Sub

    Private Sub DrawImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DrawImageToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "drawimage()")
    End Sub

    Private Sub FontToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "font")
    End Sub

    Private Sub TextAlignToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TextAlignToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "textalign")
    End Sub

    Private Sub TextBaselineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TextBaselineToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "textbaseline")
    End Sub

    Private Sub FillTextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FillTextToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "filltext()")
    End Sub

    Private Sub StrokeTextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StrokeTextToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "stroketext()")
    End Sub

    Private Sub MeasureTextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeasureTextToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "measuretext()")
    End Sub

    Private Sub ScaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScaleToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "scale()")
    End Sub

    Private Sub RotateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RotateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "rotate()")
    End Sub

    Private Sub TranslateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TranslateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "translate()")
    End Sub

    Private Sub TransformToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransformToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "transform()")
    End Sub

    Private Sub SetTransformToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetTransformToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "settransform()")
    End Sub

    Private Sub SaveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "save()")
    End Sub

    Private Sub RestoreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "restore()")
    End Sub

    Private Sub CreateEventToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateEventToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "createevent()")
    End Sub

    Private Sub GetContextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetContextToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "getcontext()")
    End Sub

    Private Sub GlobalAlphaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GlobalAlphaToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "globalalpha")
    End Sub

    Private Sub GlobalCompositeOperationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GlobalCompositeOperationToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "globalcompositeoperation")
    End Sub

    Private Sub DateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DateToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, My.Computer.Clock.LocalTime)
    End Sub

    Private Sub TimeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TimeToolStripMenuItem1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, My.Computer.Clock.GmtTime)
    End Sub

    Private Sub LinesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LinesToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, txt1.LinesCount)
    End Sub

    Private Sub PositionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PositionsToolStripMenuItem.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, txt1.SelectionStart.ToString)
    End Sub

    Private Sub ToolStripButton21_Click(sender As Object, e As EventArgs) Handles ToolStripButton21.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<b></b>")
    End Sub

    Private Sub ToolStripButton20_Click(sender As Object, e As EventArgs) Handles ToolStripButton20.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<i></i>")
    End Sub

    Private Sub ToolStripButton19_Click(sender As Object, e As EventArgs) Handles ToolStripButton19.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<u></u>")
    End Sub

    Private Sub ToolStripButton18_Click(sender As Object, e As EventArgs) Handles ToolStripButton18.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<strong></strong>")
    End Sub

    Private Sub ToolStripButton22_Click(sender As Object, e As EventArgs) Handles ToolStripButton22.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<p></p>")
    End Sub

    Private Sub ToolStripButton23_Click(sender As Object, e As EventArgs) Handles ToolStripButton23.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<quote></quote>")
    End Sub

    Private Sub ToolStripButton31_Click(sender As Object, e As EventArgs) Handles ToolStripButton31.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<h1></h1>")
    End Sub

    Private Sub ToolStripButton30_Click(sender As Object, e As EventArgs) Handles ToolStripButton30.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<h2></h2>")
    End Sub

    Private Sub ToolStripButton29_Click(sender As Object, e As EventArgs) Handles ToolStripButton29.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<h3></h3>")
    End Sub

    Private Sub ToolStripButton28_Click(sender As Object, e As EventArgs) Handles ToolStripButton28.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<h4></h4>")
    End Sub

    Private Sub ToolStrip2_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip2.ItemClicked

    End Sub

    Private Sub ToolStripButton27_Click(sender As Object, e As EventArgs) Handles ToolStripButton27.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<h5></h5>")
    End Sub

    Private Sub ToolStripButton24_Click(sender As Object, e As EventArgs) Handles ToolStripButton24.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<sup></sup>")
    End Sub

    Private Sub ToolStripButton32_Click(sender As Object, e As EventArgs) Handles ToolStripButton32.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<sub></sub>")
    End Sub

    Private Sub alert_Click(sender As Object, e As EventArgs) Handles alert.Click

    End Sub

    Private Sub ToolStripMenuItem77_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem77.Click
        Try
            colorpall.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ExecuteCodeSimplePreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExecuteCodeSimplePreviewToolStripMenuItem.Click
        Try
            Timer2.Enabled = True
            WebBrowser1.DocumentText = txt1.Text

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripMenuItem78_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem78.Click
        Try
            Process.Start(Application.StartupPath & "/help/tag/html-tags-chart.pdf")
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Error ! I Cant find file, Go to 'Backup' folder create a new file."
        End Try
    End Sub

    Private Sub ColorPlettesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorPlettesToolStripMenuItem.Click
        Try
            ToolStripMenuItem77.PerformClick()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub StepDebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StepDebugToolStripMenuItem.Click
        ExecuteCodeSimplePreviewToolStripMenuItem.PerformClick()
    End Sub

    Private Sub TagsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TagsToolStripMenuItem.Click
        Try
            ToolStripMenuItem78.PerformClick()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub moduletime_Tick(sender As Object, e As EventArgs) Handles moduletime.Tick
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

    Private Sub w1_Click(sender As Object, e As EventArgs) Handles w1.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, addmodules.sd1.Text)
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

    

    Private Sub BuildProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuildProjectToolStripMenuItem.Click
        Dim saveDLG As New SaveFileDialog
        Timer2.Enabled = True
        saveDLG.Filter = "All Files|*."
        saveDLG.Title = "Frabet Web - Build Project"
        saveDLG.DefaultExt = "."
        saveDLG.CreatePrompt = True
        saveDLG.AddExtension = True
        saveDLG.Tag = "Frabet Studio Project"
        If saveDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim s As New IO.StreamWriter(saveDLG.FileName, False)
            s.Write(txt1.Text)
            s.Flush()
            savedToFile = True
            savedFilename = saveDLG.FileName
            saveDLG.Dispose()
            s.Close()
            s.Dispose()
            buildddd.TextBox2.Text = saveDLG.FileName
            buildddd.TextBox3.Text = saveDLG.Filter

            Try
                buildddd.ShowDialog()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ReBuildProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReBuildProjectToolStripMenuItem.Click
        BuildProjectToolStripMenuItem.PerformClick()
    End Sub

    Private Sub StartWithoutDebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartWithoutDebugToolStripMenuItem.Click
        alert.Visible = True
        alert.Text = "Start without Debugging is Finished !"
        StepDebugToolStripMenuItem.PerformClick()
    End Sub

    Private Sub HTTPMessagesToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub StartDebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartDebugToolStripMenuItem.Click
        Try
            Process.Start(SaveFileDialog1.FileName)
            Timer2.Enabled = True
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Somethings Wrong ! You want Save First ...."
        End Try
    End Sub

    Private Sub AddExistingItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddExistingItemToolStripMenuItem.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "All Existing File (*.*)|*.*"
            openDLG.Title = "Add Existing File"
            openDLG.DefaultExt = "."
            openDLG.AddExtension = False
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamReader(openDLG.FileName, True)
                txt1.Text += vbNewLine & s.ReadToEnd
                savedToFile = True
                savedFilename = openDLG.FileName
                openDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops ! Something's Wrong !"
        End Try
    End Sub

    Private Sub RefreshAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshAllToolStripMenuItem.Click
        WebBrowser1.Refresh()
        Me.Refresh()
    End Sub

    Private Sub ToolStripButton33_Click(sender As Object, e As EventArgs) Handles ToolStripButton33.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<em></em>")
    End Sub

    Private Sub ToolStripButton34_Click(sender As Object, e As EventArgs) Handles ToolStripButton34.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<pre></pre>")
    End Sub

    Private Sub ToolStripButton37_Click(sender As Object, e As EventArgs) Handles ToolStripButton37.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<dl></dl>")
    End Sub

    Private Sub ToolStripButton36_Click(sender As Object, e As EventArgs) Handles ToolStripButton36.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<dt></dt>")
    End Sub

    Private Sub ToolStripButton35_Click(sender As Object, e As EventArgs) Handles ToolStripButton35.Click
        Dim pos As Integer = txt1.SelectionStart
        txt1.Text = txt1.Text.Insert(pos, "<dd></dd>")
    End Sub

    Private Sub TutorialsToolStripMenuItem_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub ViewHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewHelpToolStripMenuItem.Click
        Try
            Process.Start(Application.StartupPath & "/help/help.pdf")
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Error ! I Cant find file, Go to 'Backup' folder create a new file."
        End Try
    End Sub

    Private Sub OnlineHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnlineHelpToolStripMenuItem.Click
        alert.Visible = True
        alert.Text = "Oops ! No Online Helps were Found ...."
    End Sub

    Private Sub ReportABugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportABugToolStripMenuItem.Click
        Process.Start("mailto://")
    End Sub

    Private Sub SamplesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SamplesToolStripMenuItem.Click
        Try
            Process.Start(Application.StartupPath & "/samples/")
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Error ! I Cant find file, Go to 'Backup' folder create a new file."
        End Try
    End Sub

    Private Sub ToolStripTextBox2_Enter(sender As Object, e As EventArgs) Handles ToolStripTextBox2.Enter

    End Sub

    Private Sub ToolStripMenuItem19_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem19.Click
        If ToolStripTextBox2.Text = "GY4KI-5V4OK-KDH4T-5ST95" Then
            alert.Visible = True
            alert.Text = "Frabet Studio is Now Registered ! Thank You for use Original Software."
        Else
            alert.Visible = True
            alert.Text = "Frabet Studio is Not Registered ! Please check Key."
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Try
            abouttt.ShowDialog(Me)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops ! You can't open About Now !"
        End Try
    End Sub

    Private Sub ISCEnvironmentToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub DeveloperCommandPromptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeveloperCommandPromptToolStripMenuItem.Click
        Try
            Process.Start(Application.StartupPath & "/prompt.exe")
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "File is Not Found ! Please ReInstall Software !"
        End Try
    End Sub

    Private Sub finnddd_Tick(sender As Object, e As EventArgs) Handles finnddd.Tick

    End Sub

    Private Sub ToolStripButton6_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        Try
            txt1.ShowFindDialog()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops ! Search Error !"
        End Try
    End Sub

    Private Sub ToolStripButton25_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton25.Click
        Try
            Dim aas As String
            aas = txt1.Text
            aas = Replace(aas, fnd.Text, rpl.Text)
            txt1.Text = aas
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops ! Replace Error !"
        End Try
    End Sub

    Private Sub ToolStripButton26_Click(sender As Object, e As EventArgs) Handles ToolStripButton26.Click
        Try
            txt1.ShowReplaceDialog()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops ! Find Dialog Error !"
        End Try
    End Sub

    Private Sub FindToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        Try
            ToolStripButton6.PerformClick()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops ! Find Dialog Error !"
        End Try
    End Sub

    Private Sub ReplaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReplaceToolStripMenuItem.Click
        Try
            ToolStripButton26.PerformClick()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops ! Replace Dialog Error !"
        End Try
    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage

    End Sub

    Private Sub ssLine_Click(sender As Object, e As EventArgs) Handles ssLine.Click

    End Sub

    Private Sub GoHomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoHomeToolStripMenuItem.Click
        txt1.GoHome()
    End Sub

    Private Sub GoEndToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoEndToolStripMenuItem.Click
        txt1.GoEnd()
    End Sub

    Private Sub GoHomeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GoHomeToolStripMenuItem1.Click
        txt1.GoHome()
    End Sub

    Private Sub GoEndToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GoEndToolStripMenuItem1.Click
        txt1.GoEnd()
    End Sub

 
    Private Sub CghToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CghToolStripMenuItem.Click
        If CghToolStripMenuItem.Checked = True Then
            txt1.ReadOnly = True
        Else
            txt1.ReadOnly = False
        End If
    End Sub

    Private Sub FToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FToolStripMenuItem.Click
        If FToolStripMenuItem.Checked = True Then
            txt1.ShowLineNumbers = True
        Else
            txt1.ShowLineNumbers = False
        End If
    End Sub

    Private Sub cboprof_Click(sender As Object, e As EventArgs) Handles cboprof.Click
        txt1.Text = GetSetting("lastupdate", cboprof.Text, "textbox1")
        ln1.Text = GetSetting("lastupdate", cboprof.Text, "ln1")
        ln2.Text = GetSetting("lastupdate", cboprof.Text, "ln2")
        ToolStripTextBox2.Text = GetSetting("lastupdate", cboprof.Text, "rg1")
        Me.Text = GetSetting("lastupdate", cboprof.Text, "me")
        ExecuteCodeSimplePreviewToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        ResetAllToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ResetAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetAllToolStripMenuItem.Click
        Me.Text = "Frabet Studio"
        txt1.Text = ""
        ln1.Text = "HTML"
        ln2.Text = "Web Site"
        fnd.Clear()
        rpl.Clear()
        Timer2.Enabled = True
    End Sub

    Private Sub CToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CToolStripMenuItem.Click
        If CToolStripMenuItem.Checked = True Then
            txt1.Visible = True
        Else
            txt1.Visible = False
        End If
    End Sub

    Private Sub ToolStripSplitButton1_Click(sender As Object, e As EventArgs) Handles ToolStripSplitButton1.Click
        CUSTOMToolStripMenuItem.PerformClick()
    End Sub

    Private Sub FontToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        FontDialog1.ShowDialog()
        txt1.Font = FontDialog1.Font
    End Sub

  
    Private Sub TextSizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TextSizeToolStripMenuItem.Click
        If TextSizeToolStripMenuItem.Checked = True Then
            txt1.RightToLeft = Windows.Forms.RightToLeft.Yes

        Else
            txt1.RightToLeft = Windows.Forms.RightToLeft.No
        End If

    End Sub

    Private Sub UToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles UToolStripMenuItem3.Click

    End Sub

    Private Sub DefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefaultToolStripMenuItem.Click
        txt1.Cursor = Cursors.Default
    End Sub

    Private Sub ArrowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArrowToolStripMenuItem.Click
        txt1.Cursor = Cursors.Arrow
    End Sub

    Private Sub AppStartingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AppStartingToolStripMenuItem.Click
        txt1.Cursor = Cursors.AppStarting
    End Sub

    Private Sub HandToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HandToolStripMenuItem.Click
        txt1.Cursor = Cursors.Hand
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        txt1.Cursor = Cursors.Help
    End Sub

    Private Sub IBeamToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IBeamToolStripMenuItem.Click
        txt1.Cursor = Cursors.IBeam
    End Sub

    Private Sub NoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoToolStripMenuItem.Click
        txt1.Cursor = Cursors.No
    End Sub

    Private Sub NoMove2DToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoMove2DToolStripMenuItem.Click
        txt1.Cursor = Cursors.NoMove2D
    End Sub

    Private Sub NoMoveHorisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoMoveHorisToolStripMenuItem.Click
        txt1.Cursor = Cursors.NoMoveHoriz
    End Sub

    Private Sub NoMoveVertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoMoveVertToolStripMenuItem.Click
        txt1.Cursor = Cursors.NoMoveVert
    End Sub

    Private Sub WaitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WaitToolStripMenuItem.Click
        txt1.Cursor = Cursors.WaitCursor
    End Sub

    Private Sub BToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BToolStripMenuItem1.Click
        txt1.Cursor = Cursors.VSplit
    End Sub

    Private Sub FILEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FILEToolStripMenuItem.Click

    End Sub

    Private Sub XPSOSPXSaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XPSOSPXSaveToolStripMenuItem.Click
        Try
            Dim saveDLG As New SaveFileDialog
            saveDLG.Filter = "XPS File|*.xps|OSPX File|*.ospk"
            saveDLG.Title = "Frabet Web - XPS/ OSPX file Save"
            saveDLG.DefaultExt = ".xps"
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

        End Try
    End Sub

    Private Sub SaveExecuteCodeSimplePreviewToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub SaveAsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem1.Click
        Try
            WebBrowser1.ShowSaveAsDialog()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops! Something's Wrong !"
        End Try
    End Sub

    Private Sub PrintToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem1.Click
        Try
            WebBrowser1.ShowPrintDialog()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops! Something's Wrong !"
        End Try
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        Try
            WebBrowser1.ShowPrintPreviewDialog()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops! Something's Wrong !"
        End Try
    End Sub

    Private Sub PageSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PageSetupToolStripMenuItem.Click
        Try
            WebBrowser1.ShowPageSetupDialog()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops! Something's Wrong !"
        End Try
    End Sub

    Private Sub PropertiesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PropertiesToolStripMenuItem.Click
        Try
            WebBrowser1.ShowPropertiesDialog()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops! Something's Wrong !"
        End Try
    End Sub

    Private Sub ExecutePageOptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExecutePageOptionsToolStripMenuItem.Click
        Try
            WebBrowser1.ShowPropertiesDialog()
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Oops! Something's Wrong !"
        End Try
    End Sub

    Private Sub PROJECTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PROJECTToolStripMenuItem.Click

    End Sub

    Private Sub TabPage4_Click(sender As Object, e As EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub EncryptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EncryptToolStripMenuItem.Click
        Dim encrypttt As String
        encrypttt = codetext.Text
        encrypttt = Replace(encrypttt, "A", aaa.Text)
        encrypttt = Replace(encrypttt, "B", aab.Text)
        encrypttt = Replace(encrypttt, "C", aac.Text)
        encrypttt = Replace(encrypttt, "D", aad.Text)
        encrypttt = Replace(encrypttt, "E", aae.Text)
        encrypttt = Replace(encrypttt, "F", aaf.Text)
        encrypttt = Replace(encrypttt, "G", aag.Text)
        encrypttt = Replace(encrypttt, "H", aah.Text)
        encrypttt = Replace(encrypttt, "I", aai.Text)
        encrypttt = Replace(encrypttt, "J", aaj.Text)
        encrypttt = Replace(encrypttt, "K", aak.Text)
        encrypttt = Replace(encrypttt, "L", aal.Text)
        encrypttt = Replace(encrypttt, "M", aam.Text)
        encrypttt = Replace(encrypttt, "N", aan.Text)
        encrypttt = Replace(encrypttt, "O", aao.Text)
        encrypttt = Replace(encrypttt, "P", aap.Text)
        encrypttt = Replace(encrypttt, "Q", aaq.Text)
        encrypttt = Replace(encrypttt, "R", aar.Text)
        encrypttt = Replace(encrypttt, "S", aas.Text)
        encrypttt = Replace(encrypttt, "T", aat.Text)
        encrypttt = Replace(encrypttt, "U", aau.Text)
        encrypttt = Replace(encrypttt, "V", aav.Text)
        encrypttt = Replace(encrypttt, "W", aaw.Text)
        encrypttt = Replace(encrypttt, "X", aax.Text)
        encrypttt = Replace(encrypttt, "Y", aay.Text)
        encrypttt = Replace(encrypttt, "Z", aaz.Text)
        encrypttt = Replace(encrypttt, "a", a.Text)
        encrypttt = Replace(encrypttt, "b", b.Text)
        encrypttt = Replace(encrypttt, "c", c.Text)
        encrypttt = Replace(encrypttt, "d", d.Text)
        encrypttt = Replace(encrypttt, "e", etxt.Text)
        encrypttt = Replace(encrypttt, "f", f.Text)
        encrypttt = Replace(encrypttt, "g", g.Text)
        encrypttt = Replace(encrypttt, "h", h.Text)
        encrypttt = Replace(encrypttt, "i", i.Text)
        encrypttt = Replace(encrypttt, "j", j.Text)
        encrypttt = Replace(encrypttt, "k", k.Text)
        encrypttt = Replace(encrypttt, "l", l.Text)
        encrypttt = Replace(encrypttt, "m", m.Text)
        encrypttt = Replace(encrypttt, "n", n.Text)
        encrypttt = Replace(encrypttt, "o", o.Text)
        encrypttt = Replace(encrypttt, "p", p.Text)
        encrypttt = Replace(encrypttt, "q", q.Text)
        encrypttt = Replace(encrypttt, "r", r.Text)
        encrypttt = Replace(encrypttt, "s", s.Text)
        encrypttt = Replace(encrypttt, "t", t.Text)
        encrypttt = Replace(encrypttt, "u", u.Text)
        encrypttt = Replace(encrypttt, "v", v.Text)
        encrypttt = Replace(encrypttt, "w", w.Text)
        encrypttt = Replace(encrypttt, "x", x.Text)
        encrypttt = Replace(encrypttt, "y", y.Text)
        encrypttt = Replace(encrypttt, "z", z.Text)
        encrypttt = Replace(encrypttt, "1", n1.Text)
        encrypttt = Replace(encrypttt, "2", n2.Text)
        encrypttt = Replace(encrypttt, "3", n3.Text)
        encrypttt = Replace(encrypttt, "4", n4.Text)
        encrypttt = Replace(encrypttt, "5", n5.Text)
        encrypttt = Replace(encrypttt, "6", n6.Text)
        encrypttt = Replace(encrypttt, "7", n7.Text)
        encrypttt = Replace(encrypttt, "8", n8.Text)
        encrypttt = Replace(encrypttt, "9", n9.Text)
        encrypttt = Replace(encrypttt, "0", n0.Text)
        encrypttt = Replace(encrypttt, "-", q13.Text)
        encrypttt = Replace(encrypttt, ")", q12.Text)
        encrypttt = Replace(encrypttt, "(", q11.Text)
        encrypttt = Replace(encrypttt, "*", qq10.Text)
        encrypttt = Replace(encrypttt, "&", qq9.Text)
        encrypttt = Replace(encrypttt, "^", qq8.Text)
        encrypttt = Replace(encrypttt, "%", qq7.Text)
        encrypttt = Replace(encrypttt, "$", qq6.Text)
        encrypttt = Replace(encrypttt, "#", qq5.Text)
        encrypttt = Replace(encrypttt, "@", qq4.Text)
        encrypttt = Replace(encrypttt, "!", qq3.Text)
        encrypttt = Replace(encrypttt, "`", qq2.Text)
        encrypttt = Replace(encrypttt, "~", qq1.Text)
        encrypttt = Replace(encrypttt, "\", q26.Text)
        encrypttt = Replace(encrypttt, "|", q25.Text)
        encrypttt = Replace(encrypttt, "}", q24.Text)
        encrypttt = Replace(encrypttt, "{", q23.Text)
        encrypttt = Replace(encrypttt, "]", q22.Text)
        encrypttt = Replace(encrypttt, "[", q21.Text)
        encrypttt = Replace(encrypttt, "?", q20.Text)
        encrypttt = Replace(encrypttt, "/", q19.Text)
        encrypttt = Replace(encrypttt, ">", q18.Text)
        encrypttt = Replace(encrypttt, "<", q17.Text)
        encrypttt = Replace(encrypttt, "+", q16.Text)
        encrypttt = Replace(encrypttt, "=", q15.Text)
        encrypttt = Replace(encrypttt, "_", q14.Text)
        encrypttt = Replace(encrypttt, ",", q27.Text)
        encrypttt = Replace(encrypttt, ".", q28.Text)
        encrypttt = Replace(encrypttt, ";", q29.Text)
        encrypttt = Replace(encrypttt, ":", q30.Text)
        encrypttt = Replace(encrypttt, "'", q31.Text)
        codetextencrypt.Text = encrypttt

    End Sub

    Private Sub DecryptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DecryptToolStripMenuItem.Click
        Dim decrypttt As String
        decrypttt = codetextencrypt.Text
        decrypttt = Replace(decrypttt, aaa.Text, "A")
        decrypttt = Replace(decrypttt, aab.Text, "B")
        decrypttt = Replace(decrypttt, aac.Text, "C")
        decrypttt = Replace(decrypttt, aad.Text, "D")
        decrypttt = Replace(decrypttt, aae.Text, "E")
        decrypttt = Replace(decrypttt, aaf.Text, "F")
        decrypttt = Replace(decrypttt, aag.Text, "G")
        decrypttt = Replace(decrypttt, aah.Text, "H")
        decrypttt = Replace(decrypttt, aai.Text, "I")
        decrypttt = Replace(decrypttt, aaj.Text, "J")
        decrypttt = Replace(decrypttt, aak.Text, "K")
        decrypttt = Replace(decrypttt, aal.Text, "L")
        decrypttt = Replace(decrypttt, aam.Text, "M")
        decrypttt = Replace(decrypttt, aan.Text, "N")
        decrypttt = Replace(decrypttt, aao.Text, "O")
        decrypttt = Replace(decrypttt, aap.Text, "P")
        decrypttt = Replace(decrypttt, aaq.Text, "Q")
        decrypttt = Replace(decrypttt, aar.Text, "R")
        decrypttt = Replace(decrypttt, aas.Text, "S")
        decrypttt = Replace(decrypttt, aat.Text, "T")
        decrypttt = Replace(decrypttt, aau.Text, "U")
        decrypttt = Replace(decrypttt, aav.Text, "V")
        decrypttt = Replace(decrypttt, aaw.Text, "W")
        decrypttt = Replace(decrypttt, aax.Text, "X")
        decrypttt = Replace(decrypttt, aay.Text, "Y")
        decrypttt = Replace(decrypttt, aaz.Text, "Z")
        decrypttt = Replace(decrypttt, a.Text, "a")
        decrypttt = Replace(decrypttt, b.Text, "b")
        decrypttt = Replace(decrypttt, c.Text, "c")
        decrypttt = Replace(decrypttt, d.Text, "d")
        decrypttt = Replace(decrypttt, etxt.Text, "e")
        decrypttt = Replace(decrypttt, f.Text, "f")
        decrypttt = Replace(decrypttt, g.Text, "g")
        decrypttt = Replace(decrypttt, h.Text, "h")
        decrypttt = Replace(decrypttt, i.Text, "i")
        decrypttt = Replace(decrypttt, j.Text, "j")
        decrypttt = Replace(decrypttt, k.Text, "k")
        decrypttt = Replace(decrypttt, l.Text, "l")
        decrypttt = Replace(decrypttt, m.Text, "m")
        decrypttt = Replace(decrypttt, n.Text, "n")
        decrypttt = Replace(decrypttt, o.Text, "o")
        decrypttt = Replace(decrypttt, p.Text, "p")
        decrypttt = Replace(decrypttt, q.Text, "q")
        decrypttt = Replace(decrypttt, r.Text, "r")
        decrypttt = Replace(decrypttt, s.Text, "s")
        decrypttt = Replace(decrypttt, t.Text, "t")
        decrypttt = Replace(decrypttt, u.Text, "u")
        decrypttt = Replace(decrypttt, v.Text, "v")
        decrypttt = Replace(decrypttt, w.Text, "w")
        decrypttt = Replace(decrypttt, x.Text, "x")
        decrypttt = Replace(decrypttt, y.Text, "y")
        decrypttt = Replace(decrypttt, z.Text, "z")
        decrypttt = Replace(decrypttt, n1.Text, "1")
        decrypttt = Replace(decrypttt, n2.Text, "2")
        decrypttt = Replace(decrypttt, n3.Text, "3")
        decrypttt = Replace(decrypttt, n4.Text, "4")
        decrypttt = Replace(decrypttt, n5.Text, "5")
        decrypttt = Replace(decrypttt, n6.Text, "6")
        decrypttt = Replace(decrypttt, n7.Text, "7")
        decrypttt = Replace(decrypttt, n8.Text, "8")
        decrypttt = Replace(decrypttt, n9.Text, "9")
        decrypttt = Replace(decrypttt, n0.Text, "0")
        decrypttt = Replace(decrypttt, q13.Text, "-")
        decrypttt = Replace(decrypttt, q12.Text, ")")
        decrypttt = Replace(decrypttt, q11.Text, "(")
        decrypttt = Replace(decrypttt, qq10.Text, "*")
        decrypttt = Replace(decrypttt, qq9.Text, "&")
        decrypttt = Replace(decrypttt, qq8.Text, "^")
        decrypttt = Replace(decrypttt, qq7.Text, "%")
        decrypttt = Replace(decrypttt, qq6.Text, "$")
        decrypttt = Replace(decrypttt, qq5.Text, "#")
        decrypttt = Replace(decrypttt, qq4.Text, "@")
        decrypttt = Replace(decrypttt, qq3.Text, "!")
        decrypttt = Replace(decrypttt, qq2.Text, "`")
        decrypttt = Replace(decrypttt, qq1.Text, "~")
        decrypttt = Replace(decrypttt, q26.Text, "\")
        decrypttt = Replace(decrypttt, q25.Text, "|")
        decrypttt = Replace(decrypttt, q24.Text, "{")
        decrypttt = Replace(decrypttt, q23.Text, "}")
        decrypttt = Replace(decrypttt, q22.Text, "]")
        decrypttt = Replace(decrypttt, q21.Text, "[")
        decrypttt = Replace(decrypttt, q20.Text, "?")
        decrypttt = Replace(decrypttt, q19.Text, "/")
        decrypttt = Replace(decrypttt, q18.Text, ">")
        decrypttt = Replace(decrypttt, q17.Text, "<")
        decrypttt = Replace(decrypttt, q16.Text, "+")
        decrypttt = Replace(decrypttt, q15.Text, "=")
        decrypttt = Replace(decrypttt, q14.Text, "_")
        decrypttt = Replace(decrypttt, q27.Text, ",")
        decrypttt = Replace(decrypttt, q28.Text, ".")
        decrypttt = Replace(decrypttt, q29.Text, ";")
        decrypttt = Replace(decrypttt, q30.Text, ":")
        decrypttt = Replace(decrypttt, q31.Text, "'")
        codetext.Text = decrypttt

    End Sub

    Private Sub Apex_Button1_Click(sender As Object, e As EventArgs) Handles Apex_Button1.Click
        Try
            Dim saveDLG As New SaveFileDialog
            saveDLG.Filter = "Encrypted Data File (*.encdtaflsisc)|*.encdtaflsisc"
            saveDLG.Title = "Save Encrypted Data"
            saveDLG.DefaultExt = ".encdtaflsisc"
            saveDLG.AddExtension = False
            saveDLG.AutoUpgradeEnabled = True
            If saveDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim sssssss As New IO.StreamWriter(saveDLG.FileName, False)
                sssssss.WriteLine(aaa.Text)
                sssssss.WriteLine(aab.Text)
                sssssss.WriteLine(aac.Text)
                sssssss.WriteLine(aad.Text)
                sssssss.WriteLine(aae.Text)
                sssssss.WriteLine(aaf.Text)
                sssssss.WriteLine(aag.Text)
                sssssss.WriteLine(aah.Text)
                sssssss.WriteLine(aai.Text)
                sssssss.WriteLine(aaj.Text)
                sssssss.WriteLine(aak.Text)
                sssssss.WriteLine(aal.Text)
                sssssss.WriteLine(aam.Text)
                sssssss.WriteLine(aan.Text)
                sssssss.WriteLine(aao.Text)
                sssssss.WriteLine(aap.Text)
                sssssss.WriteLine(aaq.Text)
                sssssss.WriteLine(aar.Text)
                sssssss.WriteLine(aas.Text)
                sssssss.WriteLine(aat.Text)
                sssssss.WriteLine(aau.Text)
                sssssss.WriteLine(aav.Text)
                sssssss.WriteLine(aaw.Text)
                sssssss.WriteLine(aax.Text)
                sssssss.WriteLine(aay.Text)
                sssssss.WriteLine(aaz.Text)
                sssssss.WriteLine(a.Text)
                sssssss.WriteLine(b.Text)
                sssssss.WriteLine(c.Text)
                sssssss.WriteLine(d.Text)
                sssssss.WriteLine(etxt.Text)
                sssssss.WriteLine(f.Text)
                sssssss.WriteLine(g.Text)
                sssssss.WriteLine(h.Text)
                sssssss.WriteLine(i.Text)
                sssssss.WriteLine(j.Text)
                sssssss.WriteLine(k.Text)
                sssssss.WriteLine(l.Text)
                sssssss.WriteLine(m.Text)
                sssssss.WriteLine(n.Text)
                sssssss.WriteLine(o.Text)
                sssssss.WriteLine(p.Text)
                sssssss.WriteLine(q.Text)
                sssssss.WriteLine(r.Text)
                sssssss.WriteLine(s.Text)
                sssssss.WriteLine(t.Text)
                sssssss.WriteLine(u.Text)
                sssssss.WriteLine(v.Text)
                sssssss.WriteLine(w.Text)
                sssssss.WriteLine(x.Text)
                sssssss.WriteLine(y.Text)
                sssssss.WriteLine(z.Text)
                sssssss.WriteLine(n1.Text)
                sssssss.WriteLine(n2.Text)
                sssssss.WriteLine(n3.Text)
                sssssss.WriteLine(n4.Text)
                sssssss.WriteLine(n5.Text)
                sssssss.WriteLine(n6.Text)
                sssssss.WriteLine(n7.Text)
                sssssss.WriteLine(n8.Text)
                sssssss.WriteLine(n9.Text)
                sssssss.WriteLine(n0.Text)
                sssssss.WriteLine(qq1.Text)
                sssssss.WriteLine(qq2.Text)
                sssssss.WriteLine(qq3.Text)
                sssssss.WriteLine(qq4.Text)
                sssssss.WriteLine(qq5.Text)
                sssssss.WriteLine(qq6.Text)
                sssssss.WriteLine(qq7.Text)
                sssssss.WriteLine(qq8.Text)
                sssssss.WriteLine(qq9.Text)
                sssssss.WriteLine(qq10.Text)
                sssssss.WriteLine(q11.Text)
                sssssss.WriteLine(q12.Text)
                sssssss.WriteLine(q13.Text)
                sssssss.WriteLine(q14.Text)
                sssssss.WriteLine(q15.Text)
                sssssss.WriteLine(q16.Text)
                sssssss.WriteLine(q17.Text)
                sssssss.WriteLine(q18.Text)
                sssssss.WriteLine(q19.Text)
                sssssss.WriteLine(q20.Text)
                sssssss.WriteLine(q21.Text)
                sssssss.WriteLine(q22.Text)
                sssssss.WriteLine(q23.Text)
                sssssss.WriteLine(q24.Text)
                sssssss.WriteLine(q25.Text)
                sssssss.WriteLine(q26.Text)
                sssssss.WriteLine(q27.Text)
                sssssss.WriteLine(q28.Text)
                sssssss.WriteLine(q29.Text)
                sssssss.WriteLine(q30.Text)
                sssssss.WriteLine(q31.Text)
                sssssss.Flush()
                savedToFile = True
                savedFilename = saveDLG.FileName
                saveDLG.Dispose()
                sssssss.Close()
                sssssss.Dispose()
            End If
        Catch exc As Exception
            MessageBox.Show(exc.Message, "Small Problem...")
        End Try
    End Sub

    Private Sub Apex_Button2_Click(sender As Object, e As EventArgs) Handles Apex_Button2.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "Encrypted Data Files (*.encdtaflsisc*)|*.encdtaflsisc*"
            openDLG.Title = "Open Encrypted Data"
            openDLG.DefaultExt = ".encdtaflsisc"
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim sssssss As New IO.StreamReader(openDLG.FileName, True)
                aaa.Text = sssssss.ReadLine
                aab.Text = sssssss.ReadLine
                aac.Text = sssssss.ReadLine
                aad.Text = sssssss.ReadLine
                aae.Text = sssssss.ReadLine
                aaf.Text = sssssss.ReadLine
                aag.Text = sssssss.ReadLine
                aah.Text = sssssss.ReadLine
                aai.Text = sssssss.ReadLine
                aaj.Text = sssssss.ReadLine
                aak.Text = sssssss.ReadLine
                aal.Text = sssssss.ReadLine
                aam.Text = sssssss.ReadLine
                aan.Text = sssssss.ReadLine
                aao.Text = sssssss.ReadLine
                aap.Text = sssssss.ReadLine
                aaq.Text = sssssss.ReadLine
                aar.Text = sssssss.ReadLine
                aas.Text = sssssss.ReadLine
                aat.Text = sssssss.ReadLine
                aau.Text = sssssss.ReadLine
                aav.Text = sssssss.ReadLine
                aaw.Text = sssssss.ReadLine
                aax.Text = sssssss.ReadLine
                aay.Text = sssssss.ReadLine
                aaz.Text = sssssss.ReadLine
                a.Text = sssssss.ReadLine
                b.Text = sssssss.ReadLine
                c.Text = sssssss.ReadLine
                d.Text = sssssss.ReadLine
                etxt.Text = sssssss.ReadLine
                f.Text = sssssss.ReadLine
                g.Text = sssssss.ReadLine
                h.Text = sssssss.ReadLine
                i.Text = sssssss.ReadLine
                j.Text = sssssss.ReadLine
                k.Text = sssssss.ReadLine
                l.Text = sssssss.ReadLine
                m.Text = sssssss.ReadLine
                n.Text = sssssss.ReadLine
                o.Text = sssssss.ReadLine
                p.Text = sssssss.ReadLine
                q.Text = sssssss.ReadLine
                r.Text = sssssss.ReadLine
                s.Text = sssssss.ReadLine
                t.Text = sssssss.ReadLine
                u.Text = sssssss.ReadLine
                v.Text = sssssss.ReadLine
                w.Text = sssssss.ReadLine
                x.Text = sssssss.ReadLine
                y.Text = sssssss.ReadLine
                z.Text = sssssss.ReadLine
                n1.Text = sssssss.ReadLine
                n2.Text = sssssss.ReadLine
                n3.Text = sssssss.ReadLine
                n4.Text = sssssss.ReadLine
                n5.Text = sssssss.ReadLine
                n6.Text = sssssss.ReadLine
                n7.Text = sssssss.ReadLine
                n8.Text = sssssss.ReadLine
                n9.Text = sssssss.ReadLine
                n0.Text = sssssss.ReadLine
                qq1.Text = sssssss.ReadLine
                qq2.Text = sssssss.ReadLine
                qq3.Text = sssssss.ReadLine
                qq4.Text = sssssss.ReadLine
                qq5.Text = sssssss.ReadLine
                qq6.Text = sssssss.ReadLine
                qq7.Text = sssssss.ReadLine
                qq8.Text = sssssss.ReadLine
                qq9.Text = sssssss.ReadLine
                qq10.Text = sssssss.ReadLine
                q11.Text = sssssss.ReadLine
                q12.Text = sssssss.ReadLine
                q13.Text = sssssss.ReadLine
                q14.Text = sssssss.ReadLine
                q15.Text = sssssss.ReadLine
                q16.Text = sssssss.ReadLine
                q17.Text = sssssss.ReadLine
                q18.Text = sssssss.ReadLine
                q19.Text = sssssss.ReadLine
                q20.Text = sssssss.ReadLine
                q21.Text = sssssss.ReadLine
                q22.Text = sssssss.ReadLine
                q23.Text = sssssss.ReadLine
                q24.Text = sssssss.ReadLine
                q25.Text = sssssss.ReadLine
                q26.Text = sssssss.ReadLine
                q27.Text = sssssss.ReadLine
                q28.Text = sssssss.ReadLine
                q29.Text = sssssss.ReadLine
                q30.Text = sssssss.ReadLine
                q31.Text = sssssss.ReadLine
                savedToFile = True
                savedFilename = openDLG.FileName
                openDLG.Dispose()
                sssssss.Close()
                sssssss.Dispose()
            End If
        Catch exc As Exception
            MessageBox.Show(exc.Message, "Small Problem...")
        End Try
    End Sub

    Private Sub CutToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem2.Click
        codetext.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem2.Click
        codetext.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem2.Click
        codetext.Paste()
    End Sub

    Private Sub UndoToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem2.Click
        codetext.Undo()
    End Sub

    Private Sub RedoToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem2.Click
        codetext.Redo()
    End Sub

    Private Sub ImportCodeTextFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportCodeTextFileToolStripMenuItem.Click
        txt1.SelectAll()
        txt1.Copy()
        codetext.Paste()
    End Sub

    Private Sub PublishEncryptedFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PublishEncryptedFileToolStripMenuItem.Click
        Dim saveDLG As New SaveFileDialog
        saveDLG.Filter = "Encrypt File (*.encrypt)|*.encrypt"
        saveDLG.Title = "Publish Encrypted File"
        saveDLG.DefaultExt = ".encrypt"
        saveDLG.AddExtension = False
        If saveDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim s As New IO.StreamWriter(saveDLG.FileName, False)
            s.Write(codetextencrypt.Text)
            s.Flush()
            savedToFile = True
            savedFilename = saveDLG.FileName
            saveDLG.Dispose()
            s.Close()
            s.Dispose()
        End If
    End Sub

    Private Sub ImportEncryptedFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportEncryptedFileToolStripMenuItem.Click
        Dim openDLG As New OpenFileDialog
        openDLG.Filter = "Encrypted Files (*.encrypt*)|*.encrypt*"
        openDLG.Title = "IImport Encrypted File"
        openDLG.DefaultExt = ".encrypt"
        openDLG.AddExtension = False
        If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim s As New IO.StreamReader(openDLG.FileName, True)
            codetextencrypt.Text = s.ReadToEnd
            savedToFile = True
            savedFilename = openDLG.FileName
            openDLG.Dispose()
            s.Close()
            s.Dispose()
        End If
    End Sub

    Private Sub PublishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PublishToolStripMenuItem.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "All Files|*.*"
            openDLG.Title = "Open As File"
            openDLG.DefaultExt = "*."
            openDLG.AddExtension = False
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamReader(openDLG.FileName, True)
                codetextencrypt.Text = s.ReadToEnd

                savedToFile = True
                savedFilename = openDLG.FileName
                openDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveEncryptedFileAsTXTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveEncryptedFileAsTXTToolStripMenuItem.Click
        Try
            Dim saveDLG As New SaveFileDialog
            saveDLG.Filter = "All Files|*.*"
            saveDLG.Title = "Save As File"
            saveDLG.DefaultExt = "*."
            saveDLG.AddExtension = False
            If saveDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As New IO.StreamWriter(saveDLG.FileName, False)
                s.Write(codetextencrypt.Text)
                s.Flush()
                savedToFile = True
                savedFilename = saveDLG.FileName
                saveDLG.Dispose()
                s.Close()
                s.Dispose()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TabPage5_Click(sender As Object, e As EventArgs) Handles TabPage5.Click

    End Sub

    Private Sub lstZipFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstZipFiles.SelectedIndexChanged

    End Sub

    Private Sub ITalk_Separator4_Click(sender As Object, e As EventArgs) Handles ITalk_Separator4.Click

    End Sub

    Private Sub btnSelectFiles_Click(sender As Object, e As EventArgs) Handles btnSelectFiles.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = "All Files (*.*)|*.*|Exe Files (*.exe)|*.exe"            '
            openDLG.Multiselect = True
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                lstZipFiles.Items.AddRange(openDLG.FileNames)
                lblZipUpText.Text = "Files to Zip Up: " & lstZipFiles.Items.Count.ToString
            End If
        Catch exc As Exception
            MessageBox.Show(exc.Message, "  Selecting Files Problem...")
        End Try
    End Sub

    Private Sub btnRemoveSelectedZipUpItems_Click(sender As Object, e As EventArgs) Handles btnRemoveSelectedZipUpItems.Click
        Try
            For i As Integer = 0 To lstZipFiles.SelectedIndices.Count - 1
                lstZipFiles.Items.Remove(lstZipFiles.SelectedItem)
            Next
            lblZipUpText.Text = "Files to Zip Up: " & lstZipFiles.Items.Count.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click
        Try
            Dim folderDLG As New FolderBrowserDialog
            folderDLG.Description = "  Select the directolry whos media files you want to add to the queue..."
            folderDLG.ShowNewFolderButton = False
            Dim folderPath As String
            If folderDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                folderPath = folderDLG.SelectedPath
                If chkIncludeSubDirectories.Checked Then
                    For Each fileAndPath In _
                       My.Computer.FileSystem.GetFiles(folderPath, _
                            FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                        lstZipFiles.Items.Add(fileAndPath)
                    Next
                Else
                    For Each fileAndPath In _
                        My.Computer.FileSystem.GetFiles(folderPath, _
                            FileIO.SearchOption.SearchTopLevelOnly, "*.*")
                        lstZipFiles.Items.Add(fileAndPath)
                    Next
                End If
                lblZipUpText.Text = "Files to Zip Up: " & lstZipFiles.Items.Count.ToString
            End If
        Catch exc As Exception
            MessageBox.Show(exc.Message, "  Selecting Folder Problem...")
        End Try

    End Sub

    Private Sub btnClearZipUpList_Click(sender As Object, e As EventArgs) Handles btnClearZipUpList.Click
        Try
            lstZipFiles.Items.Clear()
            lblZipUpText.Text = "Files to Zip Up: " & lstZipFiles.Items.Count.ToString
        Catch ex As Exception

        End Try
    End Sub
    Sub savingProgress(ByVal sender As Object, ByVal e As Ionic.Zip.SaveProgressEventArgs)

    End Sub
    Sub extractingProgress(ByVal sender As Object, ByVal e As Ionic.Zip.ExtractProgressEventArgs)
       
    End Sub

    Sub zipUpError(ByVal sender As Object, ByVal e As Ionic.Zip.ZipErrorEventArgs)
        MessageBox.Show("Internal Zip Problm was Fonud !")
    End Sub
    Private Sub lblSelectedZipFile_Click(sender As Object, e As EventArgs) Handles lblSelectedZipFile.Click
        Try
            Dim openDLG As New OpenFileDialog
            openDLG.Filter = tb2.Text
            If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                tttx2.Text = openDLG.CheckFileExists
                fileToUnZip = openDLG.FileName
                zipFile = New Zip.ZipFile(fileToUnZip)
                AddHandler zipFile.ExtractProgress, AddressOf extractingProgress                '
                lblUnzipFileText.Text = "Files in Selected Zip File: " & zipFile.Entries.Count.ToString
                lstUnzipFiles.Items.Clear()
                For Each entry As String In zipFile.EntryFileNames
                    lstUnzipFiles.Items.Add(entry)
                Next
            End If

        Catch exc As Exception

            MessageBox.Show(exc.Message, "  Selecting Zip File Problem...")

        End Try
    End Sub

    Private Sub Label46_Click(sender As Object, e As EventArgs) Handles Label46.Click
        Dim folderDLG As New FolderBrowserDialog

        folderDLG.Description = "  Select the directory to Unzip the file too..."
        folderDLG.ShowNewFolderButton = False

        If folderDLG.ShowDialog = Windows.Forms.DialogResult.OK Then

            txtExtractLocation.Text = folderDLG.SelectedPath

        End If
    End Sub

    Private Sub Ambiance_Button_11_Click(sender As Object, e As EventArgs) Handles Ambiance_Button_11.Click
        Try
            If lstZipFiles.Items.Count = 0 Then
                MessageBox.Show("You should add at least one file to your zip file..." _
                                & vbNewLine & vbNewLine & "I guess I will continue anyways :)", _
                               "  No File Selected", MessageBoxButtons.OK)
            End If
            For Each f As String In lstZipFiles.Items
                If Not zipFile.ContainsEntry(My.Computer.FileSystem.GetName(f)) Then
                    zipFile.AddFile(f, "\")
                End If
            Next
            Dim saveDLG As New SaveFileDialog
            saveDLG.AddExtension = True
            saveDLG.DefaultExt = tb3.Text
            saveDLG.Title = "Browse to the Folder and set the Filename to save your zip file to."
            saveDLG.Filter = tb1.Text
            If saveDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                zipFile.Save(saveDLG.FileName)
                MessageBox.Show("Zip Up Process should be finished...", _
                                "  Finished Zipping!", MessageBoxButtons.OK)
            End If
        Catch exc As Exception
            MessageBox.Show(exc.Message, "ZipUp Problem...")
        End Try

    End Sub

    Private Sub Ambiance_Button_12_Click(sender As Object, e As EventArgs) Handles Ambiance_Button_12.Click
        Try
            If My.Computer.FileSystem.FileExists(fileToUnZip) Then
                zipFile.ExtractAll(txtExtractLocation.Text, Zip.ExtractExistingFileAction.OverwriteSilently)
                MessageBox.Show("Extraction Process should be finished...", _
                               "  Finished Unzipping!", MessageBoxButtons.OK)

            Else
                MessageBox.Show("The file you want to unzip doesn't appear to exist?", _
                                "  File Exists?", MessageBoxButtons.OK)
            End If
        Catch exc As Exception
            MessageBox.Show(exc.Message, "  UnZipping File Problem...")
        End Try

    End Sub

    Private Sub r1_CheckedChanged(sender As Object, e As EventArgs) Handles r1.CheckedChanged
        tb1.Text = "Zip File|*.zip"
        tb2.Text = "Zip File|*.zip"
        tb3.Text = "*.zip"
        tb4.Text = "*.zip"
    End Sub

    Private Sub r3_CheckedChanged(sender As Object, e As EventArgs) Handles r3.CheckedChanged
        tb1.Text = "Rar File|*.rar"
        tb2.Text = "Rar File|*.rar"
        tb3.Text = "*.rar"
        tb4.Text = "*.rar"
    End Sub

    Private Sub r5_CheckedChanged(sender As Object, e As EventArgs) Handles r5.CheckedChanged
        tb1.Text = "Exe File|*.exe"
        tb2.Text = "Exe File|*.exe"
        tb3.Text = "*.exe"
        tb4.Text = "*.exe"
    End Sub

    Private Sub r4_CheckedChanged(sender As Object, e As EventArgs) Handles r4.CheckedChanged
        tb1.Text = "GZ File|*.gz"
        tb2.Text = "GZ File|*.gz"
        tb3.Text = "*.gz"
        tb4.Text = "*.gz"
    End Sub

    Private Sub r6_CheckedChanged(sender As Object, e As EventArgs) Handles r6.CheckedChanged
        tb1.Text = "..... File|*."
        tb2.Text = "..... File|*."
        tb3.Text = "*."
        tb4.Text = "*."
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        tb1.Text = "Frabet DLL PROJECT File|*.dllfrbprj"
        tb2.Text = "Frabet DLL PROJECT File|*.dllfrbprj"
        tb3.Text = "*.dllfrbprj"
        tb4.Text = "*.dllfrbprj"
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            ToolStripProgressBar1.Value = ToolStripProgressBar1.Value + 5
            If ToolStripProgressBar1.Value = 100 Then
                ToolStripProgressBar1.Value = 0
                Timer2.Dispose()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ExportCodeTextFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportCodeTextFileToolStripMenuItem.Click
        codetext.SelectAll()
        codetext.Copy()
        txt1.Paste()
    End Sub

    Private Sub CompilerFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompilerFolderToolStripMenuItem.Click
        Try
            Process.Start(Application.StartupPath & "/compiler")
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Directroy Not Found ! Plese ReInstall Software !"
        End Try
    End Sub

    Private Sub cc1_Click(sender As Object, e As EventArgs) Handles cc1.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler1/" + My.Computer.FileSystem.ReadAllText(cmp1))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler1/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc2_Click(sender As Object, e As EventArgs) Handles cc2.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler2/" + My.Computer.FileSystem.ReadAllText(cmp2))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler2/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc3_Click(sender As Object, e As EventArgs) Handles cc3.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler3/" + My.Computer.FileSystem.ReadAllText(cmp3))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler3/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc4_Click(sender As Object, e As EventArgs) Handles cc4.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler4/" + My.Computer.FileSystem.ReadAllText(cmp4))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler4/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc5_Click(sender As Object, e As EventArgs) Handles cc5.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler5/" + My.Computer.FileSystem.ReadAllText(cmp5))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler5/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc6_Click(sender As Object, e As EventArgs) Handles cc6.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler6/" + My.Computer.FileSystem.ReadAllText(cmp6))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler6/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc7_Click(sender As Object, e As EventArgs) Handles cc7.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler7/" + My.Computer.FileSystem.ReadAllText(cmp7))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler7/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc8_Click(sender As Object, e As EventArgs) Handles cc8.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler8/" + My.Computer.FileSystem.ReadAllText(cmp8))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler8/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc9_Click(sender As Object, e As EventArgs) Handles cc9.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler9/" + My.Computer.FileSystem.ReadAllText(cmp9))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler9/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc10_Click(sender As Object, e As EventArgs) Handles cc10.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler10/" + My.Computer.FileSystem.ReadAllText(cmp10))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler10/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc11_Click(sender As Object, e As EventArgs) Handles cc11.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler11/" + My.Computer.FileSystem.ReadAllText(cmp11))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler11/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub cc12_Click(sender As Object, e As EventArgs) Handles cc12.Click
        Try
            Process.Start(Application.StartupPath & "/compiler/compiler12/" + My.Computer.FileSystem.ReadAllText(cmp12))
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "'cmp.exe' file Not Found in '/compiler/compiler12/' ! Plese add compiler to Directory !"
        End Try
    End Sub

    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub PictureBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDoubleClick
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub txt1_MouseClick(sender As Object, e As MouseEventArgs) Handles txt1.MouseClick
        Try
            SaveSetting("lastupdate", cboprof.Text, "textbox1", txt1.Text)
            SaveSetting("lastupdate", cboprof.Text, "ln1", ln1.Text)
            SaveSetting("lastupdate", cboprof.Text, "ln2", ln2.Text)
            SaveSetting("lastupdate", cboprof.Text, "me", Me.Text)
            SaveSetting("lastupdate", cboprof.Text, "rg1", ToolStripTextBox2.Text)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Auto Mouse Click Settings Wrong ! Last Work is not Saved !"
        End Try
    End Sub

    Private Sub txt1_MouseHover(sender As Object, e As EventArgs) Handles txt1.MouseHover
        
    End Sub

    Private Sub EDITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EDITToolStripMenuItem.Click

    End Sub

    Private Sub FontsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontsToolStripMenuItem.Click
        Try
            Process.Start(Application.StartupPath & "/fonts/")
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Error ! I Cant find file, Go to 'Backup' folder create a new file."
        End Try
    End Sub

    Private Sub ExecuteToolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExecuteToolsToolStripMenuItem.Click

    End Sub

    Private Sub WindowsCommandPromptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WindowsCommandPromptToolStripMenuItem.Click
        Dim AppLaunch As String = "cmd.exe"
        Try
            System.Diagnostics.Process.Start(AppLaunch)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "File is Not Found ! Please ReInstall Software !"
        End Try
    End Sub

    Private Sub CopyCodeTextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyCodeTextToolStripMenuItem.Click
        Try
            txt1.SelectAll()
            txt1.Copy()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComputerNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ComputerNameToolStripMenuItem.Click
        Try
            Dim pos As Integer = txt1.SelectionStart
            txt1.Text = txt1.Text.Insert(pos, My.Computer.Info.OSFullName)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Something's Wrong ! I can't Get Informations ...."
        End Try
    End Sub

    Private Sub OSNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OSNameToolStripMenuItem.Click
        Try
            Dim pos As Integer = txt1.SelectionStart
            txt1.Text = txt1.Text.Insert(pos, My.Computer.Info.OSFullName)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Something's Wrong ! I can't Get Informations ...."
        End Try
    End Sub

    Private Sub OSPlatformToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OSPlatformToolStripMenuItem.Click
        Try
            Dim pos As Integer = txt1.SelectionStart
            txt1.Text = txt1.Text.Insert(pos, My.Computer.Info.OSPlatform)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Something's Wrong ! I can't Get Informations ...."
        End Try
    End Sub

    Private Sub OSVersionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OSVersionToolStripMenuItem.Click
        Try
            Dim pos As Integer = txt1.SelectionStart
            txt1.Text = txt1.Text.Insert(pos, My.Computer.Info.OSVersion)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Something's Wrong ! I can't Get Informations ...."
        End Try
    End Sub

    Private Sub TotalPhysicalMemoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TotalPhysicalMemoryToolStripMenuItem.Click
        Try
            Dim pos As Integer = txt1.SelectionStart
            txt1.Text = txt1.Text.Insert(pos, My.Computer.Info.TotalPhysicalMemory)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Something's Wrong ! I can't Get Informations ...."
        End Try
    End Sub

    Private Sub TotalVirtualMemoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TotalVirtualMemoryToolStripMenuItem.Click
        Try
            Dim pos As Integer = txt1.SelectionStart
            txt1.Text = txt1.Text.Insert(pos, My.Computer.Info.TotalVirtualMemory)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Something's Wrong ! I can't Get Informations ...."
        End Try
    End Sub

    Private Sub InstalledUICultureToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CalanderToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            Dim pos As Integer = txt1.SelectionStart
            txt1.Text = txt1.Text.Insert(pos, My.Computer.Info.InstalledUICulture.Calendar.MaxSupportedDateTime)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Something's Wrong ! I can't Get Informations ...."
        End Try
    End Sub

    Private Sub TickCountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TickCountToolStripMenuItem.Click
        Try
            Dim pos As Integer = txt1.SelectionStart
            txt1.Text = txt1.Text.Insert(pos, My.Computer.Clock.TickCount)
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Something's Wrong ! I can't Get Informations ...."
        End Try
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub FrabetDLLEditorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FrabetDLLEditorToolStripMenuItem.Click
        Try
            Form2.Show(Me)
        Catch ex As Exception
            MessageBox.Show("Dialog Box is already visible !" + vbNewLine & "or, Please ReInstall software ....", "Frabet_Form_View.dll", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub maincodetextmenu_Opening(sender As Object, e As CancelEventArgs) Handles maincodetextmenu.Opening

    End Sub

    Private Sub EncryptedDataFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EncryptedDataFilesToolStripMenuItem.Click
        Try
            Process.Start(Application.StartupPath & "/encrypted_data_file/")
        Catch ex As Exception
            alert.Visible = True
            alert.Text = "Error ! I Cant find file, Go to 'Backup' folder create a new file."
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox39.SelectAll()
        TextBox39.Copy()
        TextBox39.DeselectAll()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Try
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub codetext_Load(sender As Object, e As EventArgs) Handles codetext.Load

    End Sub
End Class
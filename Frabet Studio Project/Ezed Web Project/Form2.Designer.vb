<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Me.txt1 = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'txt1
        '
        Me.txt1.AutoScrollMinSize = New System.Drawing.Size(25, 15)
        Me.txt1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.txt1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.txt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt1.CommentPrefix = Nothing
        Me.txt1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt1.FoldingIndicatorColor = System.Drawing.Color.YellowGreen
        Me.txt1.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt1.ForeColor = System.Drawing.Color.White
        Me.txt1.LeftBracket = Global.Microsoft.VisualBasic.ChrW(60)
        Me.txt1.LeftBracket2 = Global.Microsoft.VisualBasic.ChrW(40)
        Me.txt1.LineNumberColor = System.Drawing.Color.DimGray
        Me.txt1.Location = New System.Drawing.Point(12, 42)
        Me.txt1.Name = "txt1"
        Me.txt1.RightBracket = Global.Microsoft.VisualBasic.ChrW(62)
        Me.txt1.RightBracket2 = Global.Microsoft.VisualBasic.ChrW(41)
        Me.txt1.ServiceLinesColor = System.Drawing.Color.Gray
        Me.txt1.Size = New System.Drawing.Size(511, 259)
        Me.txt1.TabIndex = 21
        Me.txt1.Tag = "Code Editor"
        Me.txt1.WordWrapMode = FastColoredTextBoxNS.WordWrapMode.WordWrapPreferredWidth
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(12, 307)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "Open DLL"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(641, 307)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "Publish"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Button3.Location = New System.Drawing.Point(93, 307)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 24
        Me.Button3.Text = "Clear"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Path : "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Enabled : "
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListView1.ForeColor = System.Drawing.Color.Silver
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4})
        Me.ListView1.Location = New System.Drawing.Point(529, 42)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(187, 259)
        Me.ListView1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView1.TabIndex = 27
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(728, 339)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txt1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form2"
        Me.Text = "Frabet DLL File Editor"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt1 As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
End Class

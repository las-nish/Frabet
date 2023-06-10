<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newteam
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(newteam))
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txt1 = New System.Windows.Forms.TextBox()
        Me.ITalk_Label5 = New Frabet_Studio.iTalk.iTalk_Label()
        Me.ITalk_Label4 = New Frabet_Studio.iTalk.iTalk_Label()
        Me.Apex_Separator1 = New Frabet_Studio.Apex.Apex_Separator()
        Me.ITalk_Label3 = New Frabet_Studio.iTalk.iTalk_Label()
        Me.ITalk_Label2 = New Frabet_Studio.iTalk.iTalk_Label()
        Me.ITalk_Label1 = New Frabet_Studio.iTalk.iTalk_Label()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(129, 6)
        Me.TextBox1.MaxLength = 100
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(251, 20)
        Me.TextBox1.TabIndex = 1
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(86, 31)
        Me.TextBox2.MaxLength = 10
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(110, 20)
        Me.TextBox2.TabIndex = 4
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(270, 31)
        Me.TextBox3.MaxLength = 20
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox3.Size = New System.Drawing.Size(110, 20)
        Me.TextBox3.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(305, 305)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Start >"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(86, 73)
        Me.TextBox4.MaxLength = 10
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(110, 20)
        Me.TextBox4.TabIndex = 9
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(15, 307)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(29, 20)
        Me.TextBox5.TabIndex = 10
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(270, 73)
        Me.TextBox6.MaxLength = 5
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(85, 20)
        Me.TextBox6.TabIndex = 11
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button3.Location = New System.Drawing.Point(360, 73)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(20, 20)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "+"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button4.Location = New System.Drawing.Point(147, 305)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 16
        Me.Button4.Text = "Save"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Button2
        '
        Me.Button2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(50, 305)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(91, 23)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "Open Team"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txt1
        '
        Me.txt1.Location = New System.Drawing.Point(15, 99)
        Me.txt1.Multiline = True
        Me.txt1.Name = "txt1"
        Me.txt1.ReadOnly = True
        Me.txt1.Size = New System.Drawing.Size(365, 200)
        Me.txt1.TabIndex = 19
        '
        'ITalk_Label5
        '
        Me.ITalk_Label5.AutoSize = True
        Me.ITalk_Label5.BackColor = System.Drawing.Color.Transparent
        Me.ITalk_Label5.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.ITalk_Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.ITalk_Label5.Location = New System.Drawing.Point(202, 76)
        Me.ITalk_Label5.Name = "ITalk_Label5"
        Me.ITalk_Label5.Size = New System.Drawing.Size(56, 13)
        Me.ITalk_Label5.TabIndex = 14
        Me.ITalk_Label5.Text = "ADM No :"
        '
        'ITalk_Label4
        '
        Me.ITalk_Label4.AutoSize = True
        Me.ITalk_Label4.BackColor = System.Drawing.Color.Transparent
        Me.ITalk_Label4.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.ITalk_Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.ITalk_Label4.Location = New System.Drawing.Point(12, 76)
        Me.ITalk_Label4.Name = "ITalk_Label4"
        Me.ITalk_Label4.Size = New System.Drawing.Size(42, 13)
        Me.ITalk_Label4.TabIndex = 13
        Me.ITalk_Label4.Text = "Name :"
        '
        'Apex_Separator1
        '
        Me.Apex_Separator1.BackColor = System.Drawing.Color.Transparent
        Me.Apex_Separator1.Location = New System.Drawing.Point(15, 57)
        Me.Apex_Separator1.Name = "Apex_Separator1"
        Me.Apex_Separator1.Size = New System.Drawing.Size(365, 10)
        Me.Apex_Separator1.TabIndex = 6
        Me.Apex_Separator1.Text = "Apex_Separator1"
        '
        'ITalk_Label3
        '
        Me.ITalk_Label3.AutoSize = True
        Me.ITalk_Label3.BackColor = System.Drawing.Color.Transparent
        Me.ITalk_Label3.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.ITalk_Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.ITalk_Label3.Location = New System.Drawing.Point(202, 34)
        Me.ITalk_Label3.Name = "ITalk_Label3"
        Me.ITalk_Label3.Size = New System.Drawing.Size(62, 13)
        Me.ITalk_Label3.TabIndex = 3
        Me.ITalk_Label3.Text = "Password :"
        '
        'ITalk_Label2
        '
        Me.ITalk_Label2.AutoSize = True
        Me.ITalk_Label2.BackColor = System.Drawing.Color.Transparent
        Me.ITalk_Label2.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.ITalk_Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.ITalk_Label2.Location = New System.Drawing.Point(12, 34)
        Me.ITalk_Label2.Name = "ITalk_Label2"
        Me.ITalk_Label2.Size = New System.Drawing.Size(68, 13)
        Me.ITalk_Label2.TabIndex = 2
        Me.ITalk_Label2.Text = "User Name :"
        '
        'ITalk_Label1
        '
        Me.ITalk_Label1.AutoSize = True
        Me.ITalk_Label1.BackColor = System.Drawing.Color.Transparent
        Me.ITalk_Label1.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.ITalk_Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.ITalk_Label1.Location = New System.Drawing.Point(12, 9)
        Me.ITalk_Label1.Name = "ITalk_Label1"
        Me.ITalk_Label1.Size = New System.Drawing.Size(101, 13)
        Me.ITalk_Label1.TabIndex = 0
        Me.ITalk_Label1.Text = "Team Project Title :"
        '
        'newteam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 336)
        Me.Controls.Add(Me.txt1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.ITalk_Label5)
        Me.Controls.Add(Me.ITalk_Label4)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Apex_Separator1)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.ITalk_Label3)
        Me.Controls.Add(Me.ITalk_Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ITalk_Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "newteam"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New Team Project"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ITalk_Label1 As Frabet_Studio.iTalk.iTalk_Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ITalk_Label2 As Frabet_Studio.iTalk.iTalk_Label
    Friend WithEvents ITalk_Label3 As Frabet_Studio.iTalk.iTalk_Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Apex_Separator1 As Frabet_Studio.Apex.Apex_Separator
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents ITalk_Label4 As Frabet_Studio.iTalk.iTalk_Label
    Friend WithEvents ITalk_Label5 As Frabet_Studio.iTalk.iTalk_Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txt1 As System.Windows.Forms.TextBox
End Class

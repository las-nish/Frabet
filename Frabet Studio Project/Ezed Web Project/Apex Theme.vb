#Region " Imports "

Imports System.Drawing.Drawing2D
Imports System.ComponentModel

#End Region

'|------DO-NOT-REMOVE------|
'
' Creator: HazelDev
' Site   : HazelDev.com
' Created: 20.Oct.2014
' Changed: NONE
' Version: 1.0.0
'
'|------DO-NOT-REMOVE------|

Namespace Apex

#Region " RoundRectangle "

    Module RoundRectangle
        Public Function RoundRect(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
            Dim P As GraphicsPath = New GraphicsPath()
            Dim ArcRectangleWidth As Integer = Curve * 2
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
            P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
            Return P
        End Function
    End Module

#End Region

#Region " ThemeContainer "

    Class Apex_ThemeContainer
        Inherits ContainerControl
#Region " Variables "

        Private HeaderRect As Rectangle
        Protected State As MouseState
        Private MoveHeight As Integer
        Private MouseP As Point = New Point(0, 0)
        Private Cap As Boolean = False
        Private HasShown As Boolean

#End Region
#Region " Enums "

        Enum MouseState As Byte
            None = 0
            Over = 1
            Down = 2
        End Enum

#End Region
#Region " Properties "

        Private _Sizable As Boolean = True
        Property Sizable() As Boolean
            Get
                Return _Sizable
            End Get
            Set(ByVal value As Boolean)
                _Sizable = value
            End Set
        End Property

        Private _SmartBounds As Boolean = False
        Property SmartBounds() As Boolean
            Get
                Return _SmartBounds
            End Get
            Set(ByVal value As Boolean)
                _SmartBounds = value
            End Set
        End Property

        Private _IsParentForm As Boolean
        Protected ReadOnly Property IsParentForm As Boolean
            Get
                Return _IsParentForm
            End Get
        End Property

        Protected ReadOnly Property IsParentMdi As Boolean
            Get
                If Parent Is Nothing Then Return False
                Return Parent.Parent IsNot Nothing
            End Get
        End Property

        Private _ControlMode As Boolean
        Protected Property ControlMode() As Boolean
            Get
                Return _ControlMode
            End Get
            Set(ByVal v As Boolean)
                _ControlMode = v
                Invalidate()
            End Set
        End Property

        Private _StartPosition As FormStartPosition
        Property StartPosition As FormStartPosition
            Get
                If _IsParentForm AndAlso Not _ControlMode Then Return ParentForm.StartPosition Else Return _StartPosition
            End Get
            Set(ByVal value As FormStartPosition)
                _StartPosition = value

                If _IsParentForm AndAlso Not _ControlMode Then
                    ParentForm.StartPosition = value
                End If
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected NotOverridable Overrides Sub OnParentChanged(ByVal e As EventArgs)
            MyBase.OnParentChanged(e)

            If Parent Is Nothing Then Return
            _IsParentForm = TypeOf Parent Is Form

            If Not _ControlMode Then
                InitializeMessages()

                If _IsParentForm Then
                    Me.ParentForm.FormBorderStyle = FormBorderStyle.None
                    Me.ParentForm.TransparencyKey = Color.Fuchsia

                    If Not DesignMode Then
                        AddHandler ParentForm.Shown, AddressOf FormShown
                    End If
                End If
                Parent.BackColor = BackColor
                Parent.MinimumSize = New Size(165, 33)
            End If
        End Sub

        Protected NotOverridable Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            MyBase.OnSizeChanged(e)
            If Not _ControlMode Then HeaderRect = New Rectangle(0, 0, Width - 14, MoveHeight - 7)
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseDown(e)
            If e.Button = Windows.Forms.MouseButtons.Left Then SetState(MouseState.Down)
            If Not (_IsParentForm AndAlso ParentForm.WindowState = FormWindowState.Maximized OrElse _ControlMode) Then
                If HeaderRect.Contains(e.Location) Then
                    Capture = False
                    WM_LMBUTTONDOWN = True
                    DefWndProc(Messages(0))
                ElseIf _Sizable AndAlso Not Previous = 0 Then
                    Capture = False
                    WM_LMBUTTONDOWN = True
                    DefWndProc(Messages(Previous))
                End If
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseUp(e)
            Cap = False
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseMove(e)
            If Not (_IsParentForm AndAlso ParentForm.WindowState = FormWindowState.Maximized) Then
                If _Sizable AndAlso Not _ControlMode Then InvalidateMouse()
            End If
            If Cap Then
                Parent.Location = MousePosition - MouseP
            End If
        End Sub

        Protected Overrides Sub OnInvalidated(ByVal e As System.Windows.Forms.InvalidateEventArgs)
            MyBase.OnInvalidated(e)
            ParentForm.Text = Text
        End Sub

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            MyBase.OnTextChanged(e)
            Invalidate()
        End Sub

        Private Sub FormShown(ByVal sender As Object, ByVal e As EventArgs)
            If _ControlMode OrElse HasShown Then Return

            If _StartPosition = FormStartPosition.CenterParent OrElse _StartPosition = FormStartPosition.CenterScreen Then
                Dim SB As Rectangle = Screen.PrimaryScreen.Bounds
                Dim CB As Rectangle = ParentForm.Bounds
                ParentForm.Location = New Point(SB.Width \ 2 - CB.Width \ 2, SB.Height \ 2 - CB.Width \ 2)
            End If
            HasShown = True
        End Sub

#End Region
#Region " Mouse & Size "

        Private Sub SetState(ByVal current As MouseState)
            State = current
            Invalidate()
        End Sub

        Private GetIndexPoint As Point
        Private B1x, B2x, B3, B4 As Boolean
        Private Function GetIndex() As Integer
            GetIndexPoint = PointToClient(MousePosition)
            B1x = GetIndexPoint.X < 7
            B2x = GetIndexPoint.X > Width - 7
            B3 = GetIndexPoint.Y < 7
            B4 = GetIndexPoint.Y > Height - 7

            If B1x AndAlso B3 Then Return 4
            If B1x AndAlso B4 Then Return 7
            If B2x AndAlso B3 Then Return 5
            If B2x AndAlso B4 Then Return 8
            If B1x Then Return 1
            If B2x Then Return 2
            If B3 Then Return 3
            If B4 Then Return 6
            Return 0
        End Function

        Private Current, Previous As Integer
        Private Sub InvalidateMouse()
            Current = GetIndex()
            If Current = Previous Then Return

            Previous = Current
            Select Case Previous
                Case 0
                    Cursor = Cursors.Default
                Case 6
                    Cursor = Cursors.SizeNS
                Case 8
                    Cursor = Cursors.SizeNWSE
                Case 7
                    Cursor = Cursors.SizeNESW
            End Select
        End Sub

        Private Messages(8) As Message
        Private Sub InitializeMessages()
            Messages(0) = Message.Create(Parent.Handle, 161, New IntPtr(2), IntPtr.Zero)
            For I As Integer = 1 To 8
                Messages(I) = Message.Create(Parent.Handle, 161, New IntPtr(I + 9), IntPtr.Zero)
            Next
        End Sub

        Private Sub CorrectBounds(ByVal bounds As Rectangle)
            If Parent.Width > bounds.Width Then Parent.Width = bounds.Width
            If Parent.Height > bounds.Height Then Parent.Height = bounds.Height

            Dim X As Integer = Parent.Location.X
            Dim Y As Integer = Parent.Location.Y

            If X < bounds.X Then X = bounds.X
            If Y < bounds.Y Then Y = bounds.Y

            Dim Width As Integer = bounds.X + bounds.Width
            Dim Height As Integer = bounds.Y + bounds.Height

            If X + Parent.Width > Width Then X = Width - Parent.Width
            If Y + Parent.Height > Height Then Y = Height - Parent.Height

            Parent.Location = New Point(X, Y)
        End Sub

        Private WM_LMBUTTONDOWN As Boolean
        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If WM_LMBUTTONDOWN AndAlso m.Msg = 513 Then
                WM_LMBUTTONDOWN = False

                SetState(MouseState.Over)
                If Not _SmartBounds Then Return

                If IsParentMdi Then
                    CorrectBounds(New Rectangle(Point.Empty, Parent.Parent.Size))
                Else
                    CorrectBounds(Screen.FromControl(Parent).WorkingArea)
                End If
            End If
        End Sub

#End Region

        Protected Overrides Sub CreateHandle()
            MyBase.CreateHandle()
        End Sub

        Sub New()
            SetStyle(DirectCast(139270, ControlStyles), True)
            BackColor = Color.FromArgb(244, 241, 243)
            Padding = New Padding(20, 56, 20, 16)
            DoubleBuffered = True
            Dock = DockStyle.Fill
            MoveHeight = 38
            Font = New Font("Segoe UI", 9)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            G.Clear(Color.FromArgb(245, 246, 247))

            ' Draw container rectangle
            G.DrawRectangle(New Pen(Color.FromArgb(128, 255, 255)), New Rectangle(0, 0, Width - 1, Height - 1))
            ' Color container rectangle
            G.FillRectangle(New LinearGradientBrush(New Point(0, 0), New Point(0, ClientRectangle.Height - 2), _
                                                    Color.FromArgb(246, 246, 246), _
                                                    Color.FromArgb(246, 246, 246)), _
                                                    New Rectangle(1, 1, Width - 2, ClientRectangle.Height - 2))
            ' Draw UPPER border line
            G.DrawLine(New Pen(Color.FromArgb(255, 128, 128)), 1, 1, Width - 2, 1)

            ' Color the contents body
            G.FillRectangle(New SolidBrush(Color.FromArgb(246, 246, 246)), _
                            New Rectangle(11, 33, Width - 22, Height - 46))
            ' Draw black border around the body rectangle
            G.DrawRectangle(New Pen(Color.FromArgb(20, Color.Black)), New Rectangle(11, 33, Width - 23, Height - 46))
            ' Draw shadow around the body rectangle
            G.DrawRectangle(New Pen(Color.FromArgb(20, Color.Black), 3), 10, 32, Width - 21, Height - 44)

            ' Draw LEFT, RIGHT, BOTTOM border lines
            G.DrawLine(New Pen(Color.FromArgb(255, 128, 128)), 1, 1, 1, Height - 2)
            G.DrawLine(New Pen(Color.FromArgb(255, 128, 128)), Width - 2, 1, Width - 2, Height - 2)
            G.DrawLine(New Pen(Color.FromArgb(255, 128, 128)), 1, Height - 2, Width - 2, Height - 2)

            ' Draw header text 
            G.DrawString(Text, New Font("Microsoft Sans Serif", 10, FontStyle.Bold), _
                         New SolidBrush(Color.FromArgb(64, 64, 64)), _
                         New Rectangle(9, 10, Width - 1, Height), New StringFormat() _
                         With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})


        End Sub
    End Class

#End Region
#Region " ControlBox "

    Class Apex_ControlBox
        Inherits Control

#Region " Variables "

        Private WithEvents AnimationTimer As Timer = New Timer With {.Interval = 1}
        Private MinimizedGlow, MaximizedGlow, XButtonGlow As Integer
        Private HoverMin, HoverMax, HoverExit As Boolean
        Private i As Integer

#End Region
#Region " Custom Properties "

        Private _EnableMaximize As Boolean = True
        Property EnableMaximizeButton() As Boolean
            Get
                Return _EnableMaximize
            End Get
            Set(ByVal value As Boolean)
                _EnableMaximize = value
                Invalidate()
            End Set
        End Property

        Private _EnableMinimize As Boolean = True
        Property EnableMinimizeButton() As Boolean
            Get
                Return _EnableMinimize
            End Get
            Set(ByVal value As Boolean)
                _EnableMinimize = value
                Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Size = New Size(57, 15)
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseMove(e)
            i = e.X
            If e.X > 0 AndAlso e.X < 17 Then
                HoverMin = True : HoverMax = False : HoverExit = False
            ElseIf e.X > 17 AndAlso e.X < 35 Then
                HoverMin = False : HoverMax = True : HoverExit = False
            ElseIf e.X > 35 AndAlso e.X < 52 Then
                HoverMin = False : HoverMax = False : HoverExit = True
            Else
                HoverMin = False : HoverMax = False : HoverExit = False
            End If
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
            MyBase.OnMouseLeave(e)
            HoverMin = False : HoverMax = False : HoverExit = False
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            With FindForm()
                If _EnableMaximize = True Then
                    If HoverMax Then
                        If .WindowState = FormWindowState.Normal Then
                            .WindowState = FormWindowState.Maximized
                        ElseIf .WindowState = FormWindowState.Maximized Then
                            .WindowState = FormWindowState.Normal
                        End If
                    End If
                End If
                If _EnableMinimize = True Then
                    If HoverMin Then
                        .WindowState = FormWindowState.Minimized
                    End If
                End If
                If HoverExit Then
                    If HoverExit Then
                        .Close()
                        ' Form1.Opacity2.Enabled = True  '--> FADE OUT EFFECT
                        ' Form1.Opacity1.Enabled = False '-->
                    End If
                End If
            End With
        End Sub

#End Region

        Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            BackColor = Color.Transparent
            DoubleBuffered = True
            ForeColor = Color.DimGray
            Anchor = AnchorStyles.Top Or AnchorStyles.Right
            Size = New Size(57, 15)
        End Sub

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            Location = New Point(FindForm.Width - 61, 7)
        End Sub

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            MyBase.OnHandleCreated(e)
            AnimationTimer.Start() ' activate the animation timer
        End Sub

        Private Sub Animation() Handles AnimationTimer.Tick
            If _EnableMaximize = True Then
                If HoverMax Then
                    If MaximizedGlow < 120 Then MaximizedGlow += 5
                Else
                    If MaximizedGlow >= 3 Then MaximizedGlow -= 3
                End If
            End If
            If _EnableMinimize = True Then
                If HoverMin Then
                    If MinimizedGlow < 120 Then MinimizedGlow += 5
                Else
                    If MinimizedGlow >= 3 Then MinimizedGlow -= 3
                End If
            End If
            If HoverExit Then
                If XButtonGlow < 120 Then XButtonGlow += 5
            Else
                If XButtonGlow >= 3 Then XButtonGlow -= 3
            End If
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            G.DrawString("0", New Font("Marlett", 8), _
                         New SolidBrush(Color.FromArgb(120 + MinimizedGlow, Color.Black)), _
                         New Point(2, 3))
            If FindForm.WindowState <> FormWindowState.Maximized Then
                G.DrawString("1", New Font("Marlett", 9), _
                         New SolidBrush(Color.FromArgb(120 + MaximizedGlow, Color.Black)), _
                         New Point(20, 4))
            Else
                G.DrawString("2", New Font("Marlett", 9), _
                         New SolidBrush(Color.FromArgb(120 + MaximizedGlow, Color.Black)), _
                         New Point(20, 4))
            End If
            G.DrawString("r", New Font("Marlett", 10), _
                         New SolidBrush(Color.FromArgb(120 + XButtonGlow, Color.Black)), _
                         New Point(37, 3))
        End Sub
    End Class

#End Region
#Region " Button "

    Class Apex_Button
        Inherits Control

#Region " RoundRect "

        Private CreateRoundPath As GraphicsPath

        Function CreateRound(ByVal r As Rectangle, ByVal slope As Integer) As GraphicsPath
            CreateRoundPath = New GraphicsPath(FillMode.Winding)
            CreateRoundPath.AddArc(r.X, r.Y, slope, slope, 180.0F, 90.0F)
            CreateRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270.0F, 90.0F)
            CreateRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0.0F, 90.0F)
            CreateRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90.0F, 90.0F)
            CreateRoundPath.CloseFigure()
            Return CreateRoundPath
        End Function

#End Region
#Region " Variables "

        Private MouseState As Integer
        Private InactiveGB, PressedGB As LinearGradientBrush
        Private R1 As Rectangle
        Private P1 As Pen
        Private _TextAlignment As StringAlignment = StringAlignment.Center
        Private _TextColor As Color = Color.FromArgb(150, 150, 150)

#End Region
#Region " Properties "

        Public Property TextAlignment As StringAlignment
            Get
                Return Me._TextAlignment
            End Get
            Set(ByVal value As StringAlignment)
                Me._TextAlignment = value
                Me.Invalidate()
            End Set
        End Property

        Public Overrides Property ForeColor As Color
            Get
                Return Me._TextColor
            End Get
            Set(ByVal value As Color)
                Me._TextColor = value
                Me.Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            MouseState = 0
            Invalidate()
            MyBase.OnMouseUp(e)
        End Sub
        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MouseState = 1
            Invalidate()
            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
            MouseState = 0
            Invalidate()
            MyBase.OnMouseLeave(e)
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            Invalidate()
            MyBase.OnTextChanged(e)
        End Sub

#End Region

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.OptimizedDoubleBuffer Or _
                     ControlStyles.ResizeRedraw Or _
                     ControlStyles.SupportsTransparentBackColor Or _
                     ControlStyles.UserPaint, True)

            BackColor = Color.Transparent
            DoubleBuffered = True
            Font = New Font("Segoe UI", 8)
            ForeColor = Color.FromArgb(255, 255, 255)
            Size = New Size(80, 28)
            _TextAlignment = StringAlignment.Center
            P1 = New Pen(Color.FromArgb(51, 54, 60)) ' P1 = Border color
        End Sub

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            If Width > 0 AndAlso Height > 0 Then
                R1 = New Rectangle(0, 0, Width, Height)

                InactiveGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(68, 73, 81), Color.FromArgb(55, 60, 66), 90.0F)
                PressedGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(55, 60, 66), Color.FromArgb(43, 47, 51), 90.0F)
            End If
            Invalidate()
            MyBase.OnResize(e)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            With e.Graphics
                .SmoothingMode = SmoothingMode.HighQuality
                .DrawPath(P1, CreateRound(New Rectangle(0, 0, Width - 1, Height - 1), 4))

                Select Case MouseState
                    Case 0 'Inactive
                        .FillPath(InactiveGB, CreateRound(New Rectangle(0, 0, Width - 1, Height - 1), 4))
                        .DrawString(Text, Font, New SolidBrush(ForeColor), R1, _
                                    New StringFormat() With {.Alignment = _TextAlignment, _
                                                             .LineAlignment = StringAlignment.Center})
                    Case 1 'Pressed
                        .FillPath(PressedGB, CreateRound(New Rectangle(0, 0, Width - 1, Height - 1), 4))
                        .DrawString(Text, Font, New SolidBrush(ForeColor), R1, _
                                    New StringFormat() With {.Alignment = _TextAlignment, _
                                                             .LineAlignment = StringAlignment.Center})
                End Select
            End With
        End Sub
    End Class

#End Region
#Region " CheckBox "

    <DefaultEvent("CheckedChanged")> _
    Class Apex_CheckBox
        Inherits Control

#Region " Variables "

        Private X As Integer
        Private _Checked As Boolean

#End Region
#Region " Properties "

        Property Checked() As Boolean
            Get
                Return _Checked
            End Get
            Set(ByVal value As Boolean)
                _Checked = value
                Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Event CheckedChanged(ByVal sender As Object)

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Height = 14
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseMove(e)
            X = e.Location.X
            Invalidate()
        End Sub
        Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
            _Checked = Not _Checked
            RaiseEvent CheckedChanged(Me)
            MyBase.OnMouseDown(e)
        End Sub

#End Region

        Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            BackColor = Color.Transparent
            DoubleBuffered = True
            Width = 148
            Height = 14
            Font = New Font("Microsoft Sans Serif", 9)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics

            Dim myBrush As New SolidBrush(Color.FromArgb(54, 59, 65))
            G.FillRectangle(New SolidBrush(Color.FromArgb(187, 187, 187)), New Rectangle(0, 0, 14, 14))
            G.FillRectangle(New SolidBrush(Color.FromArgb(245, 246, 247)), New Rectangle(1, 1, 14 - 2, 14 - 2))

            If _Checked Then G.DrawString("a", New Font("Marlett", 14), New SolidBrush(Color.FromArgb(55, 60, 66)), New Point(-4, -3))
            G.DrawString(Text, Font, myBrush, New Point(16, -1))
        End Sub
    End Class
#End Region
#Region " Radio Button "

    Class Apex_RadioButton
        Inherits Control

#Region " Variables "

        Private X As Integer
        Private _Checked As Boolean

#End Region
#Region " Properties "

        Property Checked() As Boolean
            Get
                Return _Checked
            End Get
            Set(ByVal value As Boolean)
                _Checked = value
                InvalidateControls()
                RaiseEvent CheckedChanged(Me)
                Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Event CheckedChanged(ByVal sender As Object)

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Height = 15
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
            If Not _Checked Then Checked = True
            MyBase.OnMouseDown(e)
        End Sub
        Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseMove(e)
            X = e.X
            Invalidate()
        End Sub
        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            MyBase.OnTextChanged(e)
            Dim textSize As Integer
            textSize = Me.CreateGraphics.MeasureString(Text, Font).Width
            Me.Width = 20 + textSize
        End Sub

#End Region

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            InvalidateControls()
        End Sub

        Public Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            BackColor = Color.Transparent
            DoubleBuffered = True
            Width = 161
            Height = 15
            Font = New Font("Microsoft Sans Serif", 9)
        End Sub

        Private Sub InvalidateControls()
            If Not IsHandleCreated OrElse Not _Checked Then Return

            For Each C As Control In Parent.Controls
                If C IsNot Me AndAlso TypeOf C Is Apex_RadioButton Then
                    DirectCast(C, Apex_RadioButton).Checked = False
                End If
            Next
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            Dim myBrush As New SolidBrush(Color.FromArgb(54, 59, 65))

            G.SmoothingMode = SmoothingMode.HighQuality

            G.DrawEllipse(New Pen(Color.FromArgb(187, 187, 187)), New Rectangle(0, 1, 12, 12))

            If _Checked Then
                G.FillEllipse(New SolidBrush(Color.FromArgb(55, 60, 66)), New Rectangle(3, 4, 6, 6))
            End If

            G.DrawString(Text, Font, myBrush, New Point(16, -1))
        End Sub
    End Class

#End Region
#Region " Label "

    Class Apex_Label
        Inherits Label

        Sub New()
            Font = New Font("Microsoft Sans Serif", 8.25)
            ForeColor = Color.FromArgb(54, 59, 65)
            BackColor = Color.Transparent
        End Sub
    End Class

#End Region
#Region " Link Label "
    Class Apex_LinkLabel
        Inherits LinkLabel

        Sub New()
            Font = New Font("Microsoft Sans Serif", 8.25)
            BackColor = Color.Transparent
            LinkColor = Color.FromArgb(46, 102, 114)
            ActiveLinkColor = Color.FromArgb(27, 61, 67)
            VisitedLinkColor = Color.FromArgb(27, 61, 67)
            LinkBehavior = Windows.Forms.LinkBehavior.NeverUnderline
        End Sub
    End Class

#End Region
#Region " Header Label "

    Class Apex_HeaderLabel
        Inherits Label

        Sub New()
            Font = New Font("Arial", 14.25, FontStyle.Bold)
            ForeColor = Color.FromArgb(54, 59, 65)
            BackColor = Color.Transparent
        End Sub
    End Class

#End Region
#Region " TextBox "

    <DefaultEvent("TextChanged")> Class Apex_TextBox
        Inherits Control

#Region " Variables "

        Public WithEvents ApexTB As New TextBox
        Private Shape As GraphicsPath
        Private _maxchars As Integer = 32767
        Private _ReadOnly As Boolean
        Private _Multiline As Boolean
        Private _Image As Image
        Private _ImageSize As Size
        Private ALNType As HorizontalAlignment
        Private isPasswordMasked As Boolean = False
        Private P1 As Pen
        Private B1 As SolidBrush

#End Region
#Region " Properties "

        Public Shadows Property TextAlignment() As HorizontalAlignment
            Get
                Return ALNType
            End Get
            Set(ByVal Val As HorizontalAlignment)
                ALNType = Val
                Invalidate()
            End Set
        End Property
        Public Shadows Property MaxLength() As Integer
            Get
                Return _maxchars
            End Get
            Set(ByVal Val As Integer)
                _maxchars = Val
                ApexTB.MaxLength = MaxLength
                Invalidate()
            End Set
        End Property

        Public Shadows Property UseSystemPasswordChar() As Boolean
            Get
                Return isPasswordMasked
            End Get
            Set(ByVal Val As Boolean)
                ApexTB.UseSystemPasswordChar = UseSystemPasswordChar
                isPasswordMasked = Val
                Invalidate()
            End Set
        End Property
        Property [ReadOnly]() As Boolean
            Get
                Return _ReadOnly
            End Get
            Set(ByVal value As Boolean)
                _ReadOnly = value
                If ApexTB IsNot Nothing Then
                    ApexTB.ReadOnly = value
                End If
            End Set
        End Property
        Property Multiline() As Boolean
            Get
                Return _Multiline
            End Get
            Set(ByVal value As Boolean)
                _Multiline = value
                If ApexTB IsNot Nothing Then
                    ApexTB.Multiline = value

                    If value Then
                        ApexTB.Height = Height - 10
                    Else
                        Height = ApexTB.Height + 10
                    End If
                End If
            End Set
        End Property

        Property Image() As Image
            Get
                Return _Image
            End Get
            Set(ByVal value As Image)
                If value Is Nothing Then
                    _ImageSize = Size.Empty
                Else
                    _ImageSize = value.Size
                End If

                _Image = value

                If Image Is Nothing Then
                    ApexTB.Location = New Point(5, 5)
                Else
                    ApexTB.Location = New Point(5, 5)
                End If
                Invalidate()
            End Set
        End Property

        Protected ReadOnly Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            MyBase.OnTextChanged(e)
            Invalidate()
        End Sub

        Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
            MyBase.OnForeColorChanged(e)
            ApexTB.ForeColor = ForeColor
            Invalidate()
        End Sub

        Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
            MyBase.OnFontChanged(e)
            ApexTB.Font = Font
        End Sub
        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub

        Private Sub _OnKeyDown(ByVal Obj As Object, ByVal e As KeyEventArgs)
            If e.Control AndAlso e.KeyCode = Keys.A Then
                ApexTB.SelectAll()
                e.SuppressKeyPress = True
            End If
            If e.Control AndAlso e.KeyCode = Keys.C Then
                ApexTB.Copy()
                e.SuppressKeyPress = True
            End If
        End Sub

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            MyBase.OnResize(e)
            If _Multiline Then
                ApexTB.Height = Height - 10
            Else
                Height = ApexTB.Height + 10
            End If

            Shape = New GraphicsPath
            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
        End Sub

        Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
            MyBase.OnGotFocus(e)
            ApexTB.Focus()
        End Sub

        Sub _TextChanged() Handles ApexTB.TextChanged
            Text = ApexTB.Text
        End Sub

        Sub _BaseTextChanged() Handles MyBase.TextChanged
            ApexTB.Text = Text
        End Sub

#End Region

        Sub AddTextBox()
            ' Initialize the TextBox
            With ApexTB
                .Size = New Size(Width - 10, 5)
                .Location = New Point(5, 5)
                .Text = String.Empty
                .BorderStyle = BorderStyle.None
                .TextAlign = HorizontalAlignment.Left
                .Font = New Font("Tahoma", 9)
                .UseSystemPasswordChar = UseSystemPasswordChar
                .Multiline = False
                .BackColor = Color.FromArgb(245, 246, 247)
                ForeColor = Color.FromArgb(64, 64, 64)
            End With
            AddHandler ApexTB.KeyDown, AddressOf _OnKeyDown
        End Sub

        Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.UserPaint, True)

            AddTextBox()
            Controls.Add(ApexTB)

            P1 = New Pen(Color.FromArgb(180, 180, 180))
            B1 = New SolidBrush(Color.White)
            BackColor = Color.Transparent

            Text = Nothing
            Font = New Font("Tahoma", 11)
            Size = New Size(135, 33)
            DoubleBuffered = True
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            Dim B As New Bitmap(Width, Height)
            Dim G As Graphics = Graphics.FromImage(B)

            G.SmoothingMode = SmoothingMode.AntiAlias

            With ApexTB
                If Image Is Nothing Then
                    .Width = Width - 10
                Else
                    .Width = Width - 35
                End If
                .TextAlign = TextAlignment
                .UseSystemPasswordChar = UseSystemPasswordChar
            End With

            G.DrawLine(New Pen(Color.FromArgb(75, 82, 90), 1), 0, Height - 2, 0, 15)
            G.DrawLine(New Pen(Color.FromArgb(75, 82, 90), 1), Width - 2, Height - 2, Width - 2, 15)
            G.DrawLine(New Pen(Color.FromArgb(75, 82, 90), 1), 1, Height - 2, Width - 2, Height - 2)

            If Image IsNot Nothing Then
                G.DrawImage(_Image, Width - 28, 0, 24, 24)
                ' 24x24 is the perfect size of the image
            End If

            e.Graphics.DrawImage(B.Clone(), 0, 0)
            G.Dispose() : B.Dispose()
        End Sub
    End Class

#End Region
#Region " Separator "

    Public Class Apex_Separator
        Inherits Control

        Sub New()
            SetStyle(ControlStyles.ResizeRedraw Or _
                     ControlStyles.SupportsTransparentBackColor, True)
            Me.Size = New Point(120, 10)
            BackColor = Color.Transparent
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            e.Graphics.DrawLine(New Pen(Color.FromArgb(184, 183, 188)), 0, 5, Width, 5)
        End Sub
    End Class

#End Region

End Namespace
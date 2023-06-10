#Region " Imports "

Imports System.Drawing.Drawing2D
Imports System.ComponentModel

#End Region

'|------DO-NOT-REMOVE------|
'
' Creator: HazelDev
' Site   : HazelDev.com
' Created: 20.Aug.2014
' Changed: 9.Sep.2014
' Version: 1.0.0
'
'|------DO-NOT-REMOVE------|

Namespace Ambiance

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
        Public Function RoundRect(ByVal X As Integer, ByVal Y As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal Curve As Integer) As GraphicsPath
            Dim Rectangle As Rectangle = New Rectangle(X, Y, Width, Height)
            Dim P As GraphicsPath = New GraphicsPath()
            Dim ArcRectangleWidth As Integer = Curve * 2
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
            P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
            Return P
        End Function
        Public Function RoundedTopRect(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
            Dim P As GraphicsPath = New GraphicsPath()
            Dim ArcRectangleWidth As Integer = Curve * 2
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
            P.AddLine(New Point(Rectangle.X + Rectangle.Width, Rectangle.Y + ArcRectangleWidth), New Point(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height - 1))
            P.AddLine(New Point(Rectangle.X, Rectangle.Height - 1 + Rectangle.Y), New Point(Rectangle.X, Rectangle.Y + Curve))
            Return P
        End Function
    End Module

#End Region

#Region " ThemeContainer "

    Class Ambiance_ThemeContainer
        Inherits ContainerControl

#Region " Enums "

        Enum MouseState As Byte
            None = 0
            Over = 1
            Down = 2
            Block = 3
        End Enum

#End Region
#Region " Variables "

        Private HeaderRect As Rectangle
        Protected State As MouseState
        Private MoveHeight As Integer
        Private MouseP As Point = New Point(0, 0)
        Private Cap As Boolean = False
        Private HasShown As Boolean

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

        Private _SmartBounds As Boolean = True
        Property SmartBounds() As Boolean
            Get
                Return _SmartBounds
            End Get
            Set(ByVal value As Boolean)
                _SmartBounds = value
            End Set
        End Property

        Private _RoundCorners As Boolean = True
        Property RoundCorners() As Boolean
            Get
                Return _RoundCorners
            End Get
            Set(ByVal value As Boolean)
                _RoundCorners = value
                Invalidate()
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
                Parent.MinimumSize = New Size(261, 65)
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
            MoveHeight = 48
            Font = New Font("Segoe UI", 9)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            G.Clear(Color.FromArgb(69, 68, 63))

            G.DrawRectangle(New Pen(Color.FromArgb(38, 38, 38)), New Rectangle(0, 0, Width - 1, Height - 1))
            ' Use [Color.FromArgb(87, 86, 81), Color.FromArgb(60, 59, 55)] for a darker taste
            ' And replace each (60, 59, 55) with (69, 68, 63)
            G.FillRectangle(New LinearGradientBrush(New Point(0, 0), New Point(0, 36), Color.FromArgb(87, 85, 77), Color.FromArgb(69, 68, 63)), New Rectangle(1, 1, Width - 2, 36))
            G.FillRectangle(New LinearGradientBrush(New Point(0, 0), New Point(0, Height), Color.FromArgb(69, 68, 63), Color.FromArgb(69, 68, 63)), New Rectangle(1, 36, Width - 2, Height - 46))

            G.DrawRectangle(New Pen(Color.FromArgb(38, 38, 38)), New Rectangle(9, 47, Width - 19, Height - 55))
            G.FillRectangle(New SolidBrush(Color.FromArgb(244, 241, 243)), New Rectangle(10, 48, Width - 20, Height - 56))

            If _RoundCorners = True Then

                ' Draw Left upper corner
                G.FillRectangle(Brushes.Fuchsia, 0, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 2, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 3, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, 2, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, 3, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, 1, 1, 1)

                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), 1, 3, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), 1, 2, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), 2, 1, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), 3, 1, 1, 1)

                ' Draw right upper corner
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 3, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 4, 0, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 2, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, 3, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, 1, 1, 1)

                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), Width - 2, 3, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), Width - 2, 2, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), Width - 3, 1, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), Width - 4, 1, 1, 1)

                ' Draw Left bottom corner
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 2, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 3, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 0, Height - 4, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 2, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 3, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, 1, Height - 2, 1, 1)

                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), 1, Height - 3, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), 1, Height - 4, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), 3, Height - 2, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), 2, Height - 2, 1, 1)

                ' Draw right bottom corner
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 3, Height, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 4, Height, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 2, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 3, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 3, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 4, Height - 1, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 4, 1, 1)
                G.FillRectangle(Brushes.Fuchsia, Width - 2, Height - 2, 1, 1)

                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), Width - 2, Height - 3, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), Width - 2, Height - 4, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), Width - 4, Height - 2, 1, 1)
                G.FillRectangle(New SolidBrush(Color.FromArgb(38, 38, 38)), Width - 3, Height - 2, 1, 1)
            End If

            G.DrawString(Text, New Font("Tahoma", 12, FontStyle.Bold), New SolidBrush(Color.FromArgb(223, 219, 210)), New Rectangle(0, 14, Width - 1, Height), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Near})
        End Sub
    End Class

#End Region
#Region " ControlBox "

    Public Class Ambiance_ControlBox
        Inherits Control

#Region " Enums "

        Enum MouseState As Byte
            None = 0
            Over = 1
            Down = 2
        End Enum

#End Region
#Region " MouseStates "
        Dim State As MouseState = MouseState.None
        Dim X As Integer
        Dim CloseBtn As New Rectangle(3, 2, 17, 17)
        Dim MinBtn As New Rectangle(23, 2, 17, 17)
        Dim MaxBtn As New Rectangle(43, 2, 17, 17)

        Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseDown(e)

            State = MouseState.Down
            Invalidate()
        End Sub
        Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseUp(e)
            If X > 3 AndAlso X < 20 Then
                FindForm.Close()
            ElseIf X > 23 AndAlso X < 40 Then
                FindForm.WindowState = FormWindowState.Minimized
            ElseIf X > 43 AndAlso X < 60 Then
                If _EnableMaximize = True Then
                    If FindForm.WindowState = FormWindowState.Maximized Then
                        FindForm.WindowState = FormWindowState.Minimized
                        FindForm.WindowState = FormWindowState.Normal
                    Else
                        FindForm.WindowState = FormWindowState.Minimized
                        FindForm.WindowState = FormWindowState.Maximized
                    End If
                End If
            End If
            State = MouseState.Over
            Invalidate()
        End Sub
        Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over
            Invalidate()
        End Sub
        Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None
            Invalidate()
        End Sub
        Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseMove(e)
            X = e.Location.X
            Invalidate()
        End Sub
#End Region
#Region " Properties "

        Dim _EnableMaximize As Boolean = True
        Property EnableMaximize() As Boolean
            Get
                Return _EnableMaximize
            End Get
            Set(ByVal value As Boolean)
                _EnableMaximize = value
                If _EnableMaximize = True Then
                    Me.Size = New Size(64, 22)
                Else
                    Me.Size = New Size(44, 22)
                End If
                Invalidate()
            End Set
        End Property

#End Region

        Sub New()
            SetStyle(ControlStyles.UserPaint Or _
              ControlStyles.SupportsTransparentBackColor Or _
              ControlStyles.ResizeRedraw Or _
              ControlStyles.DoubleBuffer, True)
            DoubleBuffered = True
            BackColor = Color.Transparent
            Font = New Font("Marlett", 7)
            Anchor = AnchorStyles.Top Or AnchorStyles.Left
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            If _EnableMaximize = True Then
                Me.Size = New Size(64, 22)
            Else
                Me.Size = New Size(44, 22)
            End If
        End Sub

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            ' Auto-decide control location on the theme container
            Location = New Point(5, 13)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            Dim B As New Bitmap(Width, Height)
            Dim G As Graphics = Graphics.FromImage(B)

            MyBase.OnPaint(e)
            G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Dim LGBClose As New LinearGradientBrush(CloseBtn, Color.FromArgb(242, 132, 99), Color.FromArgb(224, 82, 33), 90S)
            G.FillEllipse(LGBClose, CloseBtn)
            G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), CloseBtn)
            G.DrawString("r", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(6.5, 8, 0, 0))

            Dim LGBMinimize As New LinearGradientBrush(MinBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90S)
            G.FillEllipse(LGBMinimize, MinBtn)
            G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), MinBtn)
            G.DrawString("0", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(26, 4.4, 0, 0))

            If _EnableMaximize = True Then
                Dim LGBMaximize As New LinearGradientBrush(MaxBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90S)
                G.FillEllipse(LGBMaximize, MaxBtn)
                G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), MaxBtn)
                G.DrawString("1", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(46, 7, 0, 0))
            End If

            Select Case State
                Case MouseState.None
                    Dim xLGBClose As New LinearGradientBrush(CloseBtn, Color.FromArgb(242, 132, 99), Color.FromArgb(224, 82, 33), 90S)
                    G.FillEllipse(xLGBClose, CloseBtn)
                    G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), CloseBtn)
                    G.DrawString("r", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(6.5, 8, 0, 0))

                    Dim xLGBMinimize As New LinearGradientBrush(MinBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90S)
                    G.FillEllipse(xLGBMinimize, MinBtn)
                    G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), MinBtn)
                    G.DrawString("0", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(26, 4.4, 0, 0))

                    If _EnableMaximize = True Then
                        Dim xLGBMaximize As New LinearGradientBrush(MaxBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90S)
                        G.FillEllipse(xLGBMaximize, MaxBtn)
                        G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), MaxBtn)
                        G.DrawString("1", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(46, 7, 0, 0))
                    End If
                Case MouseState.Over
                    If X > 3 AndAlso X < 20 Then
                        Dim xLGBClose As New LinearGradientBrush(CloseBtn, Color.FromArgb(248, 152, 124), Color.FromArgb(231, 92, 45), 90S)
                        G.FillEllipse(xLGBClose, CloseBtn)
                        G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), CloseBtn)
                        G.DrawString("r", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(6.5, 8, 0, 0))
                    ElseIf X > 23 AndAlso X < 40 Then
                        Dim xLGBMinimize As New LinearGradientBrush(MinBtn, Color.FromArgb(196, 196, 196), Color.FromArgb(173, 173, 173), 90S)
                        G.FillEllipse(xLGBMinimize, MinBtn)
                        G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), MinBtn)
                        G.DrawString("0", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(26, 4.4, 0, 0))
                    ElseIf X > 43 AndAlso X < 60 Then
                        If _EnableMaximize = True Then
                            Dim xLGBMaximize As New LinearGradientBrush(MaxBtn, Color.FromArgb(196, 196, 196), Color.FromArgb(173, 173, 173), 90S)
                            G.FillEllipse(xLGBMaximize, MaxBtn)
                            G.DrawEllipse(New Pen(Color.FromArgb(57, 56, 53)), MaxBtn)
                            G.DrawString("1", New Font("Marlett", 7), New SolidBrush(Color.FromArgb(52, 50, 46)), New Rectangle(46, 7, 0, 0))
                        End If
                    End If
            End Select

            e.Graphics.DrawImage(B.Clone(), 0, 0)
            G.Dispose()
            B.Dispose()
        End Sub
    End Class

#End Region
#Region " Button 1 "

    Class Ambiance_Button_1
        Inherits Control

#Region " Variables "

        Private MouseState As Integer
        Private Shape As GraphicsPath
        Private InactiveGB, PressedGB, PressedContourGB As LinearGradientBrush
        Private R1 As Rectangle
        Private P1, P3 As Pen
        Private _Image As Image
        Private _ImageSize As Size
        Private _TextAlignment As StringAlignment = StringAlignment.Center
        Private _TextColor As Color = Color.FromArgb(150, 150, 150)
        Private _ImageAlign As ContentAlignment = ContentAlignment.MiddleLeft

#End Region
#Region " Image Designer "

        Private Shared Function ImageLocation(ByVal SF As StringFormat, ByVal Area As SizeF, ByVal ImageArea As SizeF) As PointF
            Dim MyPoint As PointF
            Select Case SF.Alignment
                Case StringAlignment.Center
                    MyPoint.X = CSng((Area.Width - ImageArea.Width) / 2)
                Case StringAlignment.Near
                    MyPoint.X = 2
                Case StringAlignment.Far
                    MyPoint.X = Area.Width - ImageArea.Width - 2

            End Select

            Select Case SF.LineAlignment
                Case StringAlignment.Center
                    MyPoint.Y = CSng((Area.Height - ImageArea.Height) / 2)
                Case StringAlignment.Near
                    MyPoint.Y = 2
                Case StringAlignment.Far
                    MyPoint.Y = Area.Height - ImageArea.Height - 2
            End Select
            Return MyPoint
        End Function

        Private Function GetStringFormat(ByVal _ContentAlignment As ContentAlignment) As StringFormat
            Dim SF As StringFormat = New StringFormat()
            Select Case _ContentAlignment
                Case ContentAlignment.MiddleCenter
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.MiddleLeft
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.MiddleRight
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Far
                Case ContentAlignment.TopCenter
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.TopLeft
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.TopRight
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Far
                Case ContentAlignment.BottomCenter
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.BottomLeft
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.BottomRight
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Far
            End Select
            Return SF
        End Function

#End Region
#Region " Properties "

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
                Invalidate()
            End Set
        End Property

        Protected ReadOnly Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
        End Property

        Public Property ImageAlign() As ContentAlignment
            Get
                Return _ImageAlign
            End Get
            Set(ByVal Value As ContentAlignment)
                _ImageAlign = Value
                Invalidate()
            End Set
        End Property

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
            Focus()
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
            Font = New Font("Segoe UI", 12)
            ForeColor = Color.FromArgb(76, 76, 76)
            Size = New Size(177, 30)
            _TextAlignment = StringAlignment.Center
            P1 = New Pen(Color.FromArgb(180, 180, 180)) ' P1 = Border color
        End Sub

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            If Width > 0 AndAlso Height > 0 Then

                Shape = New GraphicsPath
                R1 = New Rectangle(0, 0, Width, Height)

                InactiveGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(253, 252, 252), Color.FromArgb(239, 237, 236), 90.0F)
                PressedGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(226, 226, 226), Color.FromArgb(237, 237, 237), 90.0F)
                PressedContourGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(167, 167, 167), Color.FromArgb(167, 167, 167), 90.0F)

                P3 = New Pen(PressedContourGB)
            End If

            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
            Invalidate()
            MyBase.OnResize(e)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            With e.Graphics
                .SmoothingMode = SmoothingMode.HighQuality
                Dim ipt As PointF = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize)

                Select Case MouseState
                    Case 0 'Inactive
                        .FillPath(InactiveGB, Shape) ' Fill button body with InactiveGB color gradient
                        .DrawPath(P1, Shape) ' Draw button border [InactiveGB]
                        If IsNothing(Image) Then
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        Else
                            .DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height)
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        End If
                    Case 1 'Pressed
                        .FillPath(PressedGB, Shape) ' Fill button body with PressedGB color gradient
                        .DrawPath(P3, Shape) ' Draw button border [PressedGB]

                        If IsNothing(Image) Then
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        Else
                            .DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height)
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        End If
                End Select
            End With
            MyBase.OnPaint(e)
        End Sub
    End Class

#End Region
#Region " Button 2 "

    Class Ambiance_Button_2
        Inherits Control

#Region " Variables "

        Private MouseState As Integer
        Private Shape As GraphicsPath
        Private InactiveGB, PressedGB, PressedContourGB As LinearGradientBrush
        Private R1 As Rectangle
        Private P1, P3 As Pen
        Private _Image As Image
        Private _ImageSize As Size
        Private _TextAlignment As StringAlignment = StringAlignment.Center
        Private _TextColor As Color = Color.FromArgb(150, 150, 150)
        Private _ImageAlign As ContentAlignment = ContentAlignment.MiddleLeft

#End Region
#Region " Image Designer "

        Private Shared Function ImageLocation(ByVal SF As StringFormat, ByVal Area As SizeF, ByVal ImageArea As SizeF) As PointF
            Dim MyPoint As PointF
            Select Case SF.Alignment
                Case StringAlignment.Center
                    MyPoint.X = CSng((Area.Width - ImageArea.Width) / 2)
                Case StringAlignment.Near
                    MyPoint.X = 2
                Case StringAlignment.Far
                    MyPoint.X = Area.Width - ImageArea.Width - 2

            End Select

            Select Case SF.LineAlignment
                Case StringAlignment.Center
                    MyPoint.Y = CSng((Area.Height - ImageArea.Height) / 2)
                Case StringAlignment.Near
                    MyPoint.Y = 2
                Case StringAlignment.Far
                    MyPoint.Y = Area.Height - ImageArea.Height - 2
            End Select
            Return MyPoint
        End Function

        Private Function GetStringFormat(ByVal _ContentAlignment As ContentAlignment) As StringFormat
            Dim SF As StringFormat = New StringFormat()
            Select Case _ContentAlignment
                Case ContentAlignment.MiddleCenter
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.MiddleLeft
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.MiddleRight
                    SF.LineAlignment = StringAlignment.Center
                    SF.Alignment = StringAlignment.Far
                Case ContentAlignment.TopCenter
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.TopLeft
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.TopRight
                    SF.LineAlignment = StringAlignment.Near
                    SF.Alignment = StringAlignment.Far
                Case ContentAlignment.BottomCenter
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Center
                Case ContentAlignment.BottomLeft
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Near
                Case ContentAlignment.BottomRight
                    SF.LineAlignment = StringAlignment.Far
                    SF.Alignment = StringAlignment.Far
            End Select
            Return SF
        End Function

#End Region
#Region " Properties "

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
                Invalidate()
            End Set
        End Property

        Protected ReadOnly Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
        End Property

        Public Property ImageAlign() As ContentAlignment
            Get
                Return _ImageAlign
            End Get
            Set(ByVal Value As ContentAlignment)
                _ImageAlign = Value
                Invalidate()
            End Set
        End Property

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
            Focus()
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
            Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
            ForeColor = Color.FromArgb(76, 76, 76)
            Size = New Size(177, 30)
            _TextAlignment = StringAlignment.Center
            P1 = New Pen(Color.FromArgb(162, 120, 101)) ' P1 = Border color
        End Sub

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            If Width > 0 AndAlso Height > 0 Then

                Shape = New GraphicsPath
                R1 = New Rectangle(0, 0, Width, Height)

                InactiveGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(253, 175, 143), Color.FromArgb(244, 146, 106), 90.0F)
                PressedGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(244, 146, 106), Color.FromArgb(244, 146, 106), 90.0F)
                PressedContourGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(162, 120, 101), Color.FromArgb(162, 120, 101), 90.0F)

                P3 = New Pen(PressedContourGB)
            End If

            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
            Invalidate()
            MyBase.OnResize(e)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            With e.Graphics
                .SmoothingMode = SmoothingMode.HighQuality
                Dim ipt As PointF = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize)

                Select Case MouseState
                    Case 0 'Inactive
                        .FillPath(InactiveGB, Shape) ' Fill button body with InactiveGB color gradient
                        .DrawPath(P1, Shape) ' Draw button border [InactiveGB]
                        If IsNothing(Image) Then
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        Else
                            .DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height)
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        End If
                    Case 1 'Pressed
                        .FillPath(PressedGB, Shape) ' Fill button body with PressedGB color gradient
                        .DrawPath(P3, Shape) ' Draw button border [PressedGB]

                        If IsNothing(Image) Then
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        Else
                            .DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height)
                            .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                        End If
                End Select
            End With
            MyBase.OnPaint(e)
        End Sub
    End Class

#End Region
#Region " Label "

    Class Ambiance_Label
        Inherits Label

        Sub New()
            Font = New Font("Segoe UI", 11)
            ForeColor = Color.FromArgb(76, 76, 77)
            BackColor = Color.Transparent
        End Sub
    End Class

#End Region
#Region " Link Label "
    Class Ambiance_LinkLabel
        Inherits LinkLabel

        Sub New()
            Font = New Font("Segoe UI", 11, FontStyle.Regular)
            BackColor = Color.Transparent
            LinkColor = Color.FromArgb(240, 119, 70)
            ActiveLinkColor = Color.FromArgb(221, 72, 20)
            VisitedLinkColor = Color.FromArgb(240, 119, 70)
            LinkBehavior = Windows.Forms.LinkBehavior.AlwaysUnderline
        End Sub
    End Class

#End Region
#Region " Header Label "

    Class Ambiance_HeaderLabel
        Inherits Label

        Sub New()
            Font = New Font("Segoe UI", 11, FontStyle.Bold)
            ForeColor = Color.FromArgb(76, 76, 77)
            BackColor = Color.Transparent
        End Sub
    End Class

#End Region
#Region " Separator "

    Public Class Ambiance_Separator
        Inherits Control

        Sub New()
            SetStyle(ControlStyles.ResizeRedraw, True)
            Me.Size = New Point(120, 10)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            e.Graphics.DrawLine(New Pen(Color.FromArgb(224, 222, 220)), 0, 5, Width, 5)
            e.Graphics.DrawLine(New Pen(Color.FromArgb(250, 249, 249)), 0, 6, Width, 6)
        End Sub
    End Class

#End Region
#Region " ProgressBar "

    Public Class Ambiance_ProgressBar
        Inherits Control

#Region " Enums "

        Public Enum Alignment
            Right
            Center
        End Enum

#End Region
#Region " Variables "

        Private _Minimum As Integer
        Private _Maximum As Integer = 100
        Private _Value As Integer = 0
        Private ALN As Alignment
        Private _DrawHatch As Boolean
        Private _ShowPercentage As Boolean

        Private GP1, GP2, GP3 As GraphicsPath
        Private R1, R2 As Rectangle
        Private GB1, GB2 As LinearGradientBrush
        Private I1 As Integer

#End Region
#Region " Properties "

        Public Property Maximum() As Integer
            Get
                Return _Maximum
            End Get
            Set(ByVal V As Integer)
                If V < 1 Then V = 1
                If V < _Value Then _Value = V
                _Maximum = V
                Invalidate()
            End Set
        End Property

        Public Property Minimum() As Integer
            Get
                Return _Minimum
            End Get
            Set(ByVal value As Integer)
                _Minimum = value

                If value > _Maximum Then _Maximum = value
                If value > _Value Then _Value = value

                Invalidate()
            End Set
        End Property

        Public Property Value() As Integer
            Get
                Return _Value
            End Get
            Set(ByVal V As Integer)
                If V > _Maximum Then V = Maximum
                _Value = V
                Invalidate()
            End Set
        End Property

        Public Property ValueAlignment() As Alignment
            Get
                Return ALN
            End Get
            Set(ByVal value As Alignment)
                ALN = value
                Invalidate()
            End Set
        End Property

        Property DrawHatch() As Boolean
            Get
                Return _DrawHatch
            End Get
            Set(ByVal v As Boolean)
                _DrawHatch = v
                Invalidate()
            End Set
        End Property

        Property ShowPercentage() As Boolean
            Get
                Return _ShowPercentage
            End Get
            Set(ByVal v As Boolean)
                _ShowPercentage = v
                Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Me.Height = 20
            Dim minimumSize As New Size(58, 20)
            Me.MinimumSize = minimumSize
        End Sub

#End Region

        Sub New()
            _Maximum = 100
            _ShowPercentage = True
            _DrawHatch = True
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.UserPaint, True)
            BackColor = Color.Transparent
            DoubleBuffered = True
        End Sub

        Public Sub Increment(value As Integer)
            Me._Value += value
            Invalidate()
        End Sub

        Public Sub Deincrement(value As Integer)
            Me._Value -= value
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim B As New Bitmap(Width, Height)
            Dim G As Graphics = Graphics.FromImage(B)

            G.Clear(Color.Transparent)
            G.SmoothingMode = SmoothingMode.HighQuality

            GP1 = RoundRect(New Rectangle(0, 0, Width - 1, Height - 1), 4)
            GP2 = RoundRect(New Rectangle(1, 1, Width - 3, Height - 3), 4)

            R1 = New Rectangle(0, 2, Width - 1, Height - 1)
            GB1 = New LinearGradientBrush(R1, Color.FromArgb(255, 255, 255), Color.FromArgb(230, 230, 230), 90.0F)

            ' Draw inside background
            G.FillRectangle(New SolidBrush(Color.FromArgb(244, 241, 243)), R1)
            G.SetClip(GP1)
            G.FillPath(New SolidBrush(Color.FromArgb(244, 241, 243)), RoundRect(New Rectangle(1, 1, Width - 3, Height / 2 - 2), 4))


            I1 = CInt((_Value - _Minimum) / (_Maximum - _Minimum) * (Width - 3))
            If I1 > 1 Then
                GP3 = RoundRect(New Rectangle(1, 1, I1, Height - 3), 4)

                R2 = New Rectangle(1, 1, I1, Height - 3)
                GB2 = New LinearGradientBrush(R2, Color.FromArgb(214, 89, 37), Color.FromArgb(223, 118, 75), 90.0F)

                ' Fill the value with its gradient
                G.FillPath(GB2, GP3)

                ' Draw diagonal lines
                If _DrawHatch = True Then
                    For i = 0 To (Width - 1) * _Maximum / _Value Step 20
                        G.DrawLine(New Pen(New SolidBrush(Color.FromArgb(25, Color.White)), 10.0F), New Point(i, 0), New Point(i - 10, Height))
                    Next
                End If

                G.SetClip(GP3)
                G.SmoothingMode = SmoothingMode.None
                G.SmoothingMode = SmoothingMode.AntiAlias
                G.ResetClip()
            End If

            ' Draw value as a string
            Dim DrawString As String = CStr(CInt(Value)) & "%"
            Dim textX As Integer = Me.Width - G.MeasureString(DrawString, Font).Width - 1
            Dim textY As Integer = (Me.Height / 2) - (G.MeasureString(DrawString, Font).Height / 2 - 2)

            If _ShowPercentage = True Then
                Select Case ValueAlignment
                    Case Alignment.Right
                        G.DrawString(DrawString, New Font("Segoe UI", 8), Brushes.DimGray, New Point(textX, textY))
                    Case Alignment.Center
                        G.DrawString(DrawString, New Font("Segoe UI", 8), Brushes.DimGray, _
                                     New Rectangle(0, 0, Width, Height + 2), _
                                     New StringFormat() With {.Alignment = StringAlignment.Center, _
                                                              .LineAlignment = StringAlignment.Center})
                End Select
            End If

            ' Draw border
            G.DrawPath(New Pen(Color.FromArgb(180, 180, 180)), GP2)

            e.Graphics.DrawImage(B.Clone(), 0, 0)
            G.Dispose()
            B.Dispose()
        End Sub
    End Class

#End Region
#Region " Progress Indicator "

    Class Ambiance_ProgressIndicator
        Inherits Control

#Region " Variables "

        Private ReadOnly BaseColor As New SolidBrush(Color.FromArgb(76, 76, 76))
        Private ReadOnly AnimationColor As New SolidBrush(Color.Gray)
        Private ReadOnly AnimationSpeed As New Timer()

        Private FloatPoint As PointF()
        Private BuffGraphics As BufferedGraphics
        Private IndicatorIndex As Integer
        Private ReadOnly GraphicsContext As BufferedGraphicsContext = BufferedGraphicsManager.Current

#End Region
#Region " Properties "

        Public Property P_BaseColor() As Color
            Get
                Return BaseColor.Color
            End Get
            Set(val As Color)
                BaseColor.Color = val
            End Set
        End Property

        Public Property P_AnimationColor() As Color
            Get
                Return AnimationColor.Color
            End Get
            Set(val As Color)
                AnimationColor.Color = val
            End Set
        End Property

        Public Property P_AnimationSpeed() As Integer
            Get
                Return AnimationSpeed.Interval
            End Get
            Set(val As Integer)
                AnimationSpeed.Interval = val
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            MyBase.OnSizeChanged(e)
            SetStandardSize()
            UpdateGraphics()
            SetPoints()
        End Sub

        Protected Overrides Sub OnEnabledChanged(e As EventArgs)
            MyBase.OnEnabledChanged(e)
            AnimationSpeed.Enabled = Me.Enabled
        End Sub

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            MyBase.OnHandleCreated(e)
            AddHandler AnimationSpeed.Tick, AddressOf AnimationSpeed_Tick
            AnimationSpeed.Start()
        End Sub

        Private Sub AnimationSpeed_Tick(sender As Object, e As EventArgs)
            If IndicatorIndex.Equals(0) Then
                IndicatorIndex = FloatPoint.Length - 1
            Else
                IndicatorIndex -= 1
            End If
            Me.Invalidate(False)
        End Sub

#End Region

        Public Sub New()
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                        ControlStyles.UserPaint Or _
                        ControlStyles.ResizeRedraw Or _
                        ControlStyles.OptimizedDoubleBuffer, True)

            Size = New Size(80, 80)
            Text = String.Empty
            MinimumSize = New Size(80, 80)
            SetPoints()
            AnimationSpeed.Interval = 110
        End Sub

        Private Sub SetStandardSize()
            Dim _Size As Integer = Math.Max(Width, Height)
            Size = New Size(_Size, _Size)
        End Sub

        Private Sub SetPoints()
            Dim FPStack = New Stack(Of PointF)()
            Dim centerPoint As New PointF(Me.Width / 2.0F, Me.Height / 2.0F)
            Dim i As Single = 0

            While i < 360.0F
                SetValue(centerPoint, Me.Width / 2 - 15, i)
                Dim FP As PointF = EndPoint
                FP = New PointF(FP.X - 15 / 2.0F, FP.Y - 15 / 2.0F)
                FPStack.Push(FP)
                i += 360.0F / 8
            End While
            FloatPoint = FPStack.ToArray()
        End Sub

        Private Sub UpdateGraphics()
            If Me.Width > 0 AndAlso Me.Height > 0 Then
                GraphicsContext.MaximumBuffer = New Size(Me.Width + 1, Me.Height + 1)
                BuffGraphics = GraphicsContext.Allocate(Me.CreateGraphics(), Me.ClientRectangle)
                BuffGraphics.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            End If
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            BuffGraphics.Graphics.Clear(Me.BackColor)

            For i As Integer = 0 To FloatPoint.Length - 1
                If IndicatorIndex = i Then
                    BuffGraphics.Graphics.FillEllipse(AnimationColor, FloatPoint(i).X, FloatPoint(i).Y, 15, 15)
                Else
                    BuffGraphics.Graphics.FillEllipse(BaseColor, FloatPoint(i).X, FloatPoint(i).Y, 15, 15)
                End If
            Next
            BuffGraphics.Render(e.Graphics)
        End Sub

        Private Rise As Double, Run As Double
        Private _StartingFloatPoint As PointF

        Private Function AssignValues(Of X)(ByRef Run As X, Length As X) As X
            Run = Length
            Return Length
        End Function

        Private Sub SetValue(StartingFloatPoint As PointF, Length As Integer, Angle As Double)
            Dim CircleRadian As Double = Math.PI * Angle / 180.0

            _StartingFloatPoint = StartingFloatPoint
            Rise = AssignValues(Run, Length)
            Rise = Math.Sin(CircleRadian) * Rise
            Run = Math.Cos(CircleRadian) * Run
        End Sub

        Private ReadOnly Property EndPoint() As PointF
            Get
                Dim LocationX As Single = CSng(_StartingFloatPoint.Y + Rise)
                Dim LocationY As Single = CSng(_StartingFloatPoint.X + Run)
                Return New PointF(LocationY, LocationX)
            End Get
        End Property
    End Class

#End Region
#Region " Toggle Button "

    <DefaultEvent("ToggledChanged")> Class Ambiance_Toggle
        Inherits Control

#Region " Enums "

        Enum _Type
            OnOff
            YesNo
            IO
        End Enum

#End Region
#Region " Variables "

        Event ToggledChanged()
        Private _Toggled As Boolean
        Private ToggleType As _Type
        Private Bar As Rectangle
        Private cHandle As Size = New Size(15, 20)

#End Region
#Region " Properties "

        Public Property Toggled() As Boolean
            Get
                Return _Toggled
            End Get
            Set(ByVal value As Boolean)
                _Toggled = value
                Invalidate()
                RaiseEvent ToggledChanged()
            End Set
        End Property

        Public Property Type As _Type
            Get
                Return ToggleType
            End Get
            Set(value As _Type)
                ToggleType = value
                Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Width = 79
            Height = 27
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseUp(e)
            Toggled = Not Toggled
            Focus()
        End Sub

#End Region

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                      ControlStyles.DoubleBuffer Or _
                      ControlStyles.ResizeRedraw Or _
                      ControlStyles.UserPaint, True)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics

            G.SmoothingMode = SmoothingMode.HighQuality
            G.Clear(Parent.BackColor)

            Dim SwitchXLoc As Integer = 3
            Dim ControlRectangle As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim ControlPath As GraphicsPath = RoundRect(ControlRectangle, 4)

            Dim BackgroundLGB As LinearGradientBrush
            If _Toggled Then
                SwitchXLoc = 37
                BackgroundLGB = New LinearGradientBrush(ControlRectangle, Color.FromArgb(231, 108, 58), Color.FromArgb(236, 113, 63), 90.0F)
            Else
                SwitchXLoc = 0
                BackgroundLGB = New LinearGradientBrush(ControlRectangle, Color.FromArgb(208, 208, 208), Color.FromArgb(226, 226, 226), 90.0F)
            End If

            ' Fill inside background gradient
            G.FillPath(BackgroundLGB, ControlPath)

            ' Draw string
            Select Case ToggleType
                Case _Type.OnOff
                    If Toggled Then
                        G.DrawString("ON", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 13.5, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                    Else
                        G.DrawString("OFF", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.DimGray, Bar.X + 59, Bar.Y + 13.5, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                    End If
                Case _Type.YesNo
                    If Toggled Then
                        G.DrawString("YES", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 13.5, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                    Else
                        G.DrawString("NO", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.DimGray, Bar.X + 59, Bar.Y + 13.5, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                    End If
                Case _Type.IO
                    If Toggled Then
                        G.DrawString("I", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 13.5, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                    Else
                        G.DrawString("O", New Font("Segoe UI", 12, FontStyle.Regular), Brushes.DimGray, Bar.X + 59, Bar.Y + 13.5, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                    End If
            End Select

            Dim SwitchRectangle As New Rectangle(SwitchXLoc, 0, Width - 38, Height)
            Dim SwitchPath As GraphicsPath = RoundRect(SwitchRectangle, 4)
            Dim SwitchButtonLGB As New LinearGradientBrush(SwitchRectangle, Color.FromArgb(253, 253, 253), Color.FromArgb(240, 238, 237), LinearGradientMode.Vertical)

            ' Fill switch background gradient
            G.FillPath(SwitchButtonLGB, SwitchPath)

            ' Draw borders
            If _Toggled = True Then
                G.DrawPath(New Pen(Color.FromArgb(185, 89, 55)), SwitchPath)
                G.DrawPath(New Pen(Color.FromArgb(185, 89, 55)), ControlPath)
            Else
                G.DrawPath(New Pen(Color.FromArgb(181, 181, 181)), SwitchPath)
                G.DrawPath(New Pen(Color.FromArgb(181, 181, 181)), ControlPath)
            End If
        End Sub
    End Class

#End Region
#Region " CheckBox "

    <DefaultEvent("CheckedChanged")> Class Ambiance_CheckBox
        Inherits Control

#Region " Variables "

        Private Shape As GraphicsPath
        Private GB As LinearGradientBrush
        Private R1, R2 As Rectangle
        Private _Checked As Boolean
        Event CheckedChanged(ByVal sender As Object)

#End Region
#Region " Properties "

        Property Checked As Boolean
            Get
                Return _Checked
            End Get
            Set(ByVal value As Boolean)
                _Checked = value
                RaiseEvent CheckedChanged(Me)
                Invalidate()
            End Set
        End Property

#End Region

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.OptimizedDoubleBuffer Or _
                     ControlStyles.ResizeRedraw Or _
                     ControlStyles.SupportsTransparentBackColor Or _
                     ControlStyles.UserPaint, True)

            BackColor = Color.Transparent
            DoubleBuffered = True ' Reduce control flicker
            Font = New Font("Segoe UI", 12)
            Size = New Size(171, 26)
        End Sub

        Protected Overrides Sub OnClick(ByVal e As EventArgs)
            _Checked = Not _Checked
            RaiseEvent CheckedChanged(Me)
            Focus()
            Invalidate()
            MyBase.OnClick(e)
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            Invalidate()
            MyBase.OnTextChanged(e)
        End Sub

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            If Width > 0 AndAlso Height > 0 Then
                Shape = New GraphicsPath

                R1 = New Rectangle(17, 0, Width, Height + 1)
                R2 = New Rectangle(0, 0, Width, Height)
                GB = New LinearGradientBrush(New Rectangle(0, 0, 25, 25), Color.FromArgb(213, 85, 32), Color.FromArgb(224, 123, 82), 90)

                With Shape
                    .AddArc(0, 0, 7, 7, 180, 90)
                    .AddArc(7, 0, 7, 7, -90, 90)
                    .AddArc(7, 7, 7, 7, 0, 90)
                    .AddArc(0, 7, 7, 7, 90, 90)
                    .CloseAllFigures()
                End With
                Height = 15
            End If

            Invalidate()
            MyBase.OnResize(e)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)

            With e.Graphics
                .Clear(Parent.BackColor)
                .SmoothingMode = SmoothingMode.AntiAlias

                .FillPath(GB, Shape) ' Fill the body of the CheckBox
                .DrawPath(New Pen(Color.FromArgb(182, 88, 55)), Shape) ' Draw the border

                .DrawString(Text, Font, New SolidBrush(Color.FromArgb(76, 76, 95)), R1, New StringFormat() With {.LineAlignment = StringAlignment.Center})

                If Checked Then
                    .DrawString("ü", New Font("Wingdings", 12), New SolidBrush(Color.FromArgb(255, 255, 255)), New Rectangle(-2, 1, Width, Height + 2), New StringFormat() With {.LineAlignment = StringAlignment.Center})
                End If
            End With
            e.Dispose()
        End Sub
    End Class

#End Region
#Region " RadioButton "

    <DefaultEvent("CheckedChanged")> Class Ambiance_RadioButton
        Inherits Control

#Region " Enums "

        Enum MouseState As Byte
            None = 0
            Over = 1
            Down = 2
            Block = 3
        End Enum

#End Region
#Region " Variables "

        Private _Checked As Boolean
        Event CheckedChanged(ByVal sender As Object)

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

        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            Invalidate()
            MyBase.OnTextChanged(e)
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Height = 15
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
            If Not _Checked Then Checked = True
            MyBase.OnMouseDown(e)
            Focus()
        End Sub

#End Region

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.OptimizedDoubleBuffer Or _
                     ControlStyles.ResizeRedraw Or _
                     ControlStyles.SupportsTransparentBackColor Or _
                     ControlStyles.UserPaint, True)
            BackColor = Color.Transparent
            Font = New Font("Segoe UI", 12)
            Width = 193
        End Sub

        Private Sub InvalidateControls()
            If Not IsHandleCreated OrElse Not _Checked Then Return

            For Each _Control As Control In Parent.Controls
                If _Control IsNot Me AndAlso TypeOf _Control Is Ambiance_RadioButton Then
                    DirectCast(_Control, Ambiance_RadioButton).Checked = False
                End If
            Next
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            With e.Graphics

                .Clear(Parent.BackColor)
                .SmoothingMode = SmoothingMode.AntiAlias

                ' Fill the body of the ellipse with a gradient
                Dim LGB As New LinearGradientBrush(New Rectangle(New Point(0, 0), New Size(14, 14)), Color.FromArgb(213, 85, 32), Color.FromArgb(224, 123, 82), 90)
                .FillEllipse(LGB, New Rectangle(New Point(0, 0), New Size(14, 14)))

                Dim GP As New GraphicsPath()
                GP.AddEllipse(New Rectangle(0, 0, 14, 14))
                .SetClip(GP)
                .ResetClip()

                ' Draw ellipse border
                .DrawEllipse(New Pen(Color.FromArgb(182, 88, 55)), New Rectangle(New Point(0, 0), New Size(14, 14)))

                If _Checked Then ' Draw an ellipse inside the body
                    Dim EllipseColor As New SolidBrush(Color.FromArgb(255, 255, 255))
                    .FillEllipse(EllipseColor, New Rectangle(New Point(4, 4), New Size(6, 6)))
                End If
                .DrawString(Text, Font, New SolidBrush(Color.FromArgb(76, 76, 95)), 16, 8, New StringFormat() With {.LineAlignment = StringAlignment.Center})
            End With
            e.Dispose()
        End Sub
    End Class

#End Region
#Region " ComboBox "

    Class Ambiance_ComboBox
        Inherits ComboBox

#Region " Variables "

        Private _StartIndex As Integer = 0
        Private _HoverSelectionColor As Color = Color.FromArgb(241, 241, 241)

#End Region
#Region " Custom Properties "

        Public Property StartIndex As Integer
            Get
                Return _StartIndex
            End Get
            Set(ByVal value As Integer)
                _StartIndex = value
                Try
                    MyBase.SelectedIndex = value
                Catch
                End Try
                Invalidate()
            End Set
        End Property

        Public Property HoverSelectionColor As Color
            Get
                Return _HoverSelectionColor
            End Get
            Set(value As Color)
                _HoverSelectionColor = value
                Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
            MyBase.OnDrawItem(e)
            Dim LGB As New LinearGradientBrush(e.Bounds, Color.FromArgb(246, 132, 85), Color.FromArgb(231, 108, 57), 90.0F)

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If Not e.Index = -1 Then
                    e.Graphics.FillRectangle(LGB, e.Bounds)
                    e.Graphics.DrawString(GetItemText(Items(e.Index)), e.Font, Brushes.WhiteSmoke, e.Bounds)
                End If
            Else
                If Not e.Index = -1 Then
                    e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(242, 241, 240)), e.Bounds)
                    e.Graphics.DrawString(GetItemText(Items(e.Index)), e.Font, Brushes.DimGray, e.Bounds)
                End If
            End If
            LGB.Dispose()
        End Sub

        Protected Overrides Sub OnLostFocus(e As EventArgs)
            MyBase.OnLostFocus(e)
            SuspendLayout()
            Update()
            ResumeLayout()
        End Sub

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
        End Sub

#End Region

        Sub New()
            SetStyle(DirectCast(139286, ControlStyles), True)
            SetStyle(ControlStyles.Selectable, False)

            DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
            DropDownStyle = ComboBoxStyle.DropDownList

            BackColor = Color.FromArgb(246, 246, 246)
            ForeColor = Color.FromArgb(142, 142, 142)
            Size = New Size(135, 26)
            ItemHeight = 20
            DropDownHeight = 100
            Font = New Font("Segoe UI", 10, FontStyle.Regular)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim LGB As LinearGradientBrush
            Dim GP As GraphicsPath

            e.Graphics.Clear(Parent.BackColor)
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

            ' Create a curvy border
            GP = RoundRectangle.RoundRect(0, 0, Width - 1, Height - 1, 5)
            ' Fills the body of the rectangle with a gradient
            LGB = New LinearGradientBrush(ClientRectangle, Color.FromArgb(253, 252, 252), Color.FromArgb(239, 237, 236), 90.0F)

            e.Graphics.SetClip(GP)
            e.Graphics.FillRectangle(LGB, ClientRectangle)
            e.Graphics.ResetClip()

            ' Draw rectangle border
            e.Graphics.DrawPath(New Pen(Color.FromArgb(180, 180, 180)), GP)
            ' Draw string
            e.Graphics.DrawString(Text, Font, New SolidBrush(Color.FromArgb(76, 76, 97)), New Rectangle(3, 0, Width - 20, Height), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
            e.Graphics.DrawString("6", New Font("Marlett", 13, FontStyle.Regular), New SolidBrush(Color.FromArgb(119, 119, 118)), New Rectangle(3, 0, Width - 4, Height), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Far})
            e.Graphics.DrawLine(New Pen(Color.FromArgb(224, 222, 220)), Width - 24, 4, Width - 24, Me.Height - 5)
            e.Graphics.DrawLine(New Pen(Color.FromArgb(250, 249, 249)), Width - 25, 4, Width - 25, Me.Height - 5)

            GP.Dispose()
            LGB.Dispose()
        End Sub
    End Class

#End Region
#Region " NumericUpDown "

    Class Ambiance_NumericUpDown
        Inherits Control

#Region " Enums "

        Enum _TextAlignment
            Near
            Center
        End Enum

#End Region
#Region " Variables "

        Private Shape As GraphicsPath
        Private P1 As Pen

        Private _Value, _Minimum, _Maximum As Long
        Private Xval As Integer
        Private KeyboardNum As Boolean
        Private MyStringAlignment As _TextAlignment

        Private WithEvents LongPressTimer As New Timer

#End Region
#Region " Properties "

        Public Property Value As Long
            Get
                Return _Value
            End Get
            Set(value As Long)
                If value <= _Maximum And value >= _Minimum Then _Value = value
                Invalidate()
            End Set
        End Property

        Public Property Minimum As Long
            Get
                Return _Minimum
            End Get
            Set(value As Long)
                If value < _Maximum Then _Minimum = value
                If _Value < _Minimum Then _Value = Minimum
                Invalidate()
            End Set
        End Property

        Public Property Maximum As Long
            Get
                Return _Maximum
            End Get
            Set(value As Long)
                If value > _Minimum Then _Maximum = value
                If _Value > _Maximum Then _Value = _Maximum
                Invalidate()
            End Set
        End Property

        Public Property TextAlignment As _TextAlignment
            Get
                Return MyStringAlignment
            End Get
            Set(value As _TextAlignment)
                MyStringAlignment = value
                Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            MyBase.OnResize(e)
            Height = 28
            MinimumSize = New Size(93, 28)
            Shape = New GraphicsPath
            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseMove(e)
            Xval = e.Location.X
            Invalidate()

            If e.X < Width - 50 Then
                Cursor = Cursors.IBeam
            Else
                Cursor = Cursors.Default
            End If
            If e.X > Me.Width - 25 AndAlso e.X < Me.Width - 10 Then
                Cursor = Cursors.Hand
            End If
            If e.X > Me.Width - 44 AndAlso e.X < Me.Width - 33 Then
                Cursor = Cursors.Hand
            End If
        End Sub

        Private Sub ClickButton()
            If Xval > Me.Width - 25 AndAlso Xval < Me.Width - 10 Then
                If (Value + 1) <= _Maximum Then _Value += 1
            Else
                If Xval > Me.Width - 44 AndAlso Xval < Me.Width - 33 Then
                    If (Value - 1) >= _Minimum Then _Value -= 1
                End If
                KeyboardNum = Not KeyboardNum
            End If
            Focus()
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseClick(e)
            ClickButton()
            LongPressTimer.Start()
        End Sub
        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            LongPressTimer.Stop()
        End Sub
        Private Sub LongPressTimer_Tick(sender As Object, e As EventArgs) Handles LongPressTimer.Tick
            ClickButton()
        End Sub
        Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
            MyBase.OnKeyPress(e)
            Try
                If KeyboardNum = True Then
                    _Value = CStr(CStr(_Value) & e.KeyChar.ToString)
                End If
                If _Value > _Maximum Then
                    _Value = _Maximum
                End If
            Catch ex As Exception
            End Try
        End Sub

        Protected Overrides Sub OnKeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
            MyBase.OnKeyUp(e)
            If e.KeyCode = Keys.Back Then
                Dim TemporaryValue As String = _Value.ToString()
                TemporaryValue = TemporaryValue.Remove(Convert.ToInt32(TemporaryValue.Length - 1))
                If (TemporaryValue.Length = 0) Then TemporaryValue = "0"
                _Value = Convert.ToInt32(TemporaryValue)
            End If
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
            MyBase.OnMouseWheel(e)
            If e.Delta > 0 Then
                If (Value + 1) <= _Maximum Then _Value += 1
                Invalidate()
            Else
                If (Value - 1) >= _Minimum Then _Value -= 1
                Invalidate()
            End If
        End Sub

#End Region

        Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.UserPaint, True)

            P1 = New Pen(Color.FromArgb(180, 180, 180))
            BackColor = Color.Transparent
            ForeColor = Color.FromArgb(76, 76, 76)
            _Minimum = 0
            _Maximum = 100
            Font = New Font("Tahoma", 11)
            Size = New Size(70, 28)
            MinimumSize = New Size(62, 28)
            DoubleBuffered = True

            AddHandler LongPressTimer.Tick, AddressOf LongPressTimer_Tick
            LongPressTimer.Interval = 300
        End Sub

        Public Sub Increment(Value As Integer)
            Me._Value += Value
            Invalidate()
        End Sub

        Public Sub Decrement(Value As Integer)
            Me._Value -= Value
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            Dim B As New Bitmap(Width, Height)
            Dim G As Graphics = Graphics.FromImage(B)
            Dim BackgroundLGB As LinearGradientBrush

            BackgroundLGB = New LinearGradientBrush(ClientRectangle, Color.FromArgb(246, 246, 246), Color.FromArgb(254, 254, 254), 90.0F)

            G.SmoothingMode = SmoothingMode.AntiAlias

            G.Clear(Color.Transparent) ' Set control background color
            G.FillPath(BackgroundLGB, Shape) ' Draw background
            G.DrawPath(P1, Shape) ' Draw border

            G.DrawString("+", New Font("Tahoma", 14), New SolidBrush(Color.FromArgb(75, 75, 75)), New Rectangle(Width - 25, 1, 19, 30))
            G.DrawLine(New Pen(Color.FromArgb(229, 228, 227)), Width - 28, 1, Width - 28, Me.Height - 2)
            G.DrawString("-", New Font("Tahoma", 14), New SolidBrush(Color.FromArgb(75, 75, 75)), New Rectangle(Width - 44, 1, 19, 30))
            G.DrawLine(New Pen(Color.FromArgb(229, 228, 227)), Width - 48, 1, Width - 48, Me.Height - 2)

            Select Case MyStringAlignment
                Case _TextAlignment.Near
                    G.DrawString(Value, Font, New SolidBrush(ForeColor), New Rectangle(5, 0, Width - 1, Height - 1), New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
                Case _TextAlignment.Center
                    G.DrawString(Value, Font, New SolidBrush(ForeColor), New Rectangle(0, 0, Width - 1, Height - 1), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            End Select
            e.Graphics.DrawImage(B.Clone(), 0, 0)
            G.Dispose()
            B.Dispose()
        End Sub
    End Class

#End Region
#Region " TrackBar "

    <DefaultEvent("ValueChanged")> Class Ambiance_TrackBar
        Inherits Control

#Region " Enums "

        Enum ValueDivisor
            By1 = 1
            By10 = 10
            By100 = 100
            By1000 = 1000
        End Enum

#End Region
#Region " Variables "

        Private PipeBorder, FillValue As GraphicsPath
        Private TrackBarHandleRect As Rectangle
        Private Cap As Boolean
        Private ValueDrawer As Integer

        Private ThumbSize As Size = New Size(15, 15)
        Private TrackThumb As Rectangle

        Private _Minimum As Integer = 0
        Private _Maximum As Integer = 10
        Private _Value As Integer = 0

        Private _DrawValueString As Boolean = False
        Private _JumpToMouse As Boolean = False
        Private DividedValue As ValueDivisor = ValueDivisor.By1

#End Region
#Region " Properties "

        Public Property Minimum() As Integer
            Get
                Return _Minimum
            End Get
            Set(ByVal value As Integer)

                If value >= _Maximum Then value = _Maximum - 10
                If _Value < value Then _Value = value

                _Minimum = value
                Invalidate()
            End Set
        End Property

        Public Property Maximum() As Integer
            Get
                Return _Maximum
            End Get
            Set(ByVal value As Integer)

                If value <= _Minimum Then value = _Minimum + 10
                If _Value > value Then _Value = value

                _Maximum = value
                Invalidate()
            End Set
        End Property

        Event ValueChanged()
        Public Property Value() As Integer
            Get
                Return _Value
            End Get
            Set(ByVal value As Integer)
                If _Value <> value Then
                    If value < _Minimum Then
                        _Value = _Minimum
                    Else
                        If value > _Maximum Then
                            _Value = _Maximum
                        Else
                            _Value = value
                        End If
                    End If
                    Invalidate()
                    RaiseEvent ValueChanged()
                End If
            End Set
        End Property

        Public Property ValueDivison() As ValueDivisor
            Get
                Return DividedValue
            End Get
            Set(ByVal Value As ValueDivisor)
                DividedValue = Value
                Invalidate()
            End Set
        End Property

        <Browsable(False)> Public Property ValueToSet() As Single
            Get
                Return CSng(_Value / DividedValue)
            End Get
            Set(ByVal Val As Single)
                Value = CInt(Val * DividedValue)
            End Set
        End Property

        Public Property JumpToMouse() As Boolean
            Get
                Return _JumpToMouse
            End Get
            Set(ByVal value As Boolean)
                _JumpToMouse = value
                Invalidate()
            End Set
        End Property

        Property DrawValueString() As Boolean
            Get
                Return _DrawValueString
            End Get
            Set(ByVal value As Boolean)
                _DrawValueString = value
                If _DrawValueString = True Then
                    Height = 35
                Else
                    Height = 22
                End If
                Invalidate()
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            If Cap = True AndAlso e.X > -1 AndAlso e.X < (Width + 1) Then
                Value = _Minimum + CInt((_Maximum - _Minimum) * (e.X / Width))
            End If
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            If e.Button = Windows.Forms.MouseButtons.Left Then
                ValueDrawer = CInt((_Value - _Minimum) / (_Maximum - _Minimum) * (Width - 11))
                TrackBarHandleRect = New Rectangle(ValueDrawer, 0, 25, 25)
                Cap = TrackBarHandleRect.Contains(e.Location)
                Focus()
                If _JumpToMouse Then
                    Value = _Minimum + CInt((_Maximum - _Minimum) * (e.X / Width))
                End If
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            Cap = False
        End Sub

#End Region

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
             ControlStyles.UserPaint Or _
             ControlStyles.ResizeRedraw Or _
             ControlStyles.DoubleBuffer, True)

            Size = New Size(80, 22)
            MinimumSize = New Size(47, 22)
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            If _DrawValueString = True Then
                Height = 35
            Else
                Height = 22
            End If
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics

            G.Clear(Parent.BackColor)
            G.SmoothingMode = SmoothingMode.AntiAlias
            TrackThumb = New Rectangle(8, 10, Width - 16, 2)
            PipeBorder = RoundRectangle.RoundRect(1, 8, Width - 3, 5, 2)

            Try
                ValueDrawer = CInt((_Value - _Minimum) / (_Maximum - _Minimum) * (Width - 11))
            Catch ex As Exception
            End Try

            TrackBarHandleRect = New Rectangle(ValueDrawer, 0, 10, 20)

            G.SetClip(PipeBorder) ' Set the clipping region of this Graphics to the specified GraphicsPath
            G.FillPath(New SolidBrush(Color.FromArgb(221, 221, 221)), PipeBorder)
            FillValue = RoundRectangle.RoundRect(1, 8, TrackBarHandleRect.X + TrackBarHandleRect.Width - 4, 5, 2)

            G.ResetClip() ' Reset the clip region of this Graphics to an infinite region

            G.SmoothingMode = SmoothingMode.HighQuality
            G.DrawPath(New Pen(Color.FromArgb(200, 200, 200)), PipeBorder) ' Draw pipe border
            G.FillPath(New SolidBrush(Color.FromArgb(217, 99, 50)), FillValue)

            G.FillEllipse(New SolidBrush(Color.FromArgb(244, 244, 244)), TrackThumb.X + CInt(TrackThumb.Width * (Value / Maximum)) - CInt(ThumbSize.Width / 2), TrackThumb.Y + CInt((TrackThumb.Height / 2)) - CInt(ThumbSize.Height / 2), ThumbSize.Width, ThumbSize.Height)
            G.DrawEllipse(New Pen(Color.FromArgb(180, 180, 180)), TrackThumb.X + CInt(TrackThumb.Width * (Value / Maximum)) - CInt(ThumbSize.Width / 2), TrackThumb.Y + CInt((TrackThumb.Height / 2)) - CInt(ThumbSize.Height / 2), ThumbSize.Width, ThumbSize.Height)

            If _DrawValueString = True Then
                G.DrawString(ValueToSet, Font, Brushes.DimGray, 1, 20)
            End If
        End Sub
    End Class

#End Region
#Region " Panel "

    Class Ambiance_Panel
        Inherits ContainerControl
        Public Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.Opaque, False)
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Dim G As Graphics = e.Graphics

            Me.Font = New Font("Tahoma", 9)
            Me.BackColor = Color.White
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.FillRectangle(New SolidBrush(Color.White), New Rectangle(0, 0, Width, Height))
            G.FillRectangle(New SolidBrush(Color.White), New Rectangle(0, 0, Width - 1, Height - 1))
            G.DrawRectangle(New Pen(Color.FromArgb(211, 208, 205)), 0, 0, Width - 1, Height - 1)
        End Sub
    End Class

#End Region
#Region " TextBox "

    <DefaultEvent("TextChanged")> Class Ambiance_TextBox
        Inherits Control

#Region " Variables "

        Public WithEvents AmbianceTB As New TextBox
        Private Shape As GraphicsPath
        Private _maxchars As Integer = 32767
        Private _ReadOnly As Boolean
        Private _Multiline As Boolean
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
                AmbianceTB.MaxLength = MaxLength
                Invalidate()
            End Set
        End Property

        Public Shadows Property UseSystemPasswordChar() As Boolean
            Get
                Return isPasswordMasked
            End Get
            Set(ByVal Val As Boolean)
                AmbianceTB.UseSystemPasswordChar = UseSystemPasswordChar
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
                If AmbianceTB IsNot Nothing Then
                    AmbianceTB.ReadOnly = value
                End If
            End Set
        End Property
        Property Multiline() As Boolean
            Get
                Return _Multiline
            End Get
            Set(ByVal value As Boolean)
                _Multiline = value
                If AmbianceTB IsNot Nothing Then
                    AmbianceTB.Multiline = value

                    If value Then
                        AmbianceTB.Height = Height - 10
                    Else
                        Height = AmbianceTB.Height + 10
                    End If
                End If
            End Set
        End Property

#End Region
#Region " EventArgs "

        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            MyBase.OnTextChanged(e)
            Invalidate()
        End Sub

        Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
            MyBase.OnForeColorChanged(e)
            AmbianceTB.ForeColor = ForeColor
            Invalidate()
        End Sub

        Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
            MyBase.OnFontChanged(e)
            AmbianceTB.Font = Font
        End Sub
        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub

        Private Sub _OnKeyDown(ByVal Obj As Object, ByVal e As KeyEventArgs)
            If e.Control AndAlso e.KeyCode = Keys.A Then
                AmbianceTB.SelectAll()
                e.SuppressKeyPress = True
            End If
            If e.Control AndAlso e.KeyCode = Keys.C Then
                AmbianceTB.Copy()
                e.SuppressKeyPress = True
            End If
        End Sub

        Private Sub _Enter(ByVal Obj As Object, ByVal e As EventArgs)
            P1 = New Pen(Color.FromArgb(205, 87, 40))
            Refresh()
        End Sub

        Private Sub _Leave(ByVal Obj As Object, ByVal e As EventArgs)
            P1 = New Pen(Color.FromArgb(180, 180, 180))
            Refresh()
        End Sub

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            MyBase.OnResize(e)
            If _Multiline Then
                AmbianceTB.Height = Height - 10
            Else
                Height = AmbianceTB.Height + 10
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
            AmbianceTB.Focus()
        End Sub

        Sub _TextChanged() Handles AmbianceTB.TextChanged
            Text = AmbianceTB.Text
        End Sub

        Sub _BaseTextChanged() Handles MyBase.TextChanged
            AmbianceTB.Text = Text
        End Sub

#End Region

        Sub AddTextBox()
            ' Initialize the TextBox
            With AmbianceTB

                .Size = New Size(Width - 10, 33)
                .Location = New Point(7, 4)
                .Text = String.Empty
                .BorderStyle = BorderStyle.None
                .BackColor = Color.White
                .TextAlign = HorizontalAlignment.Left
                .Font = New Font("Tahoma", 9)
                .UseSystemPasswordChar = UseSystemPasswordChar
                .Multiline = False
            End With
            AddHandler AmbianceTB.KeyDown, AddressOf _OnKeyDown
            AddHandler AmbianceTB.Enter, AddressOf _Enter
            AddHandler AmbianceTB.Leave, AddressOf _Leave
        End Sub

        Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.UserPaint, True)

            AddTextBox()
            Controls.Add(AmbianceTB)

            P1 = New Pen(Color.FromArgb(180, 180, 180))
            B1 = New SolidBrush(Color.White)
            BackColor = Color.Transparent
            ForeColor = Color.FromArgb(76, 76, 76)

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

            With AmbianceTB
                .Width = Width - 10
                .TextAlign = TextAlignment
                .UseSystemPasswordChar = UseSystemPasswordChar
            End With

            G.Clear(Color.Transparent)
            G.FillPath(B1, Shape) ' Draw background
            G.DrawPath(P1, Shape) ' Draw border

            e.Graphics.DrawImage(B.Clone(), 0, 0)
            G.Dispose() : B.Dispose()
        End Sub
    End Class

#End Region
#Region " RichTextBox "

    <DefaultEvent("TextChanged")> Class Ambiance_RichTextBox
        Inherits Control

#Region " Variables "

        Public WithEvents AmbianceRTB As New RichTextBox
        Private _ReadOnly As Boolean
        Private _WordWrap As Boolean
        Private _AutoWordSelection As Boolean
        Private Shape As GraphicsPath
        Private P1 As Pen

#End Region
#Region " Properties "

        Overrides Property Text As String
            Get
                Return AmbianceRTB.Text
            End Get
            Set(value As String)
                AmbianceRTB.Text = value
                Invalidate()
            End Set
        End Property
        Property [ReadOnly]() As Boolean
            Get
                Return _ReadOnly
            End Get
            Set(ByVal value As Boolean)
                _ReadOnly = value
                If AmbianceRTB IsNot Nothing Then
                    AmbianceRTB.ReadOnly = value
                End If
            End Set
        End Property
        Property [WordWrap]() As Boolean
            Get
                Return _WordWrap
            End Get
            Set(ByVal value As Boolean)
                _WordWrap = value
                If AmbianceRTB IsNot Nothing Then
                    AmbianceRTB.WordWrap = value
                End If
            End Set
        End Property
        Property [AutoWordSelection]() As Boolean
            Get
                Return _AutoWordSelection
            End Get
            Set(ByVal value As Boolean)
                _AutoWordSelection = value
                If AmbianceRTB IsNot Nothing Then
                    AmbianceRTB.AutoWordSelection = value
                End If
            End Set
        End Property
#End Region
#Region " EventArgs "

        Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
            MyBase.OnForeColorChanged(e)
            AmbianceRTB.ForeColor = ForeColor
            Invalidate()
        End Sub

        Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
            MyBase.OnFontChanged(e)
            AmbianceRTB.Font = Font
        End Sub
        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub

        Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
            MyBase.OnSizeChanged(e)
            AmbianceRTB.Size = New Size(Width - 13, Height - 11)
        End Sub

        Private Sub _Enter(ByVal Obj As Object, ByVal e As EventArgs)
            P1 = New Pen(Color.FromArgb(205, 87, 40))
            Refresh()
        End Sub

        Private Sub _Leave(ByVal Obj As Object, ByVal e As EventArgs)
            P1 = New Pen(Color.FromArgb(180, 180, 180))
            Refresh()
        End Sub

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            MyBase.OnResize(e)

            Shape = New GraphicsPath
            With Shape
                .AddArc(0, 0, 10, 10, 180, 90)
                .AddArc(Width - 11, 0, 10, 10, -90, 90)
                .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
                .AddArc(0, Height - 11, 10, 10, 90, 90)
                .CloseAllFigures()
            End With
        End Sub

        Sub _TextChanged() Handles MyBase.TextChanged
            AmbianceRTB.Text = Text
        End Sub

#End Region

        Sub AddRichTextBox()
            With AmbianceRTB
                .BackColor = Color.White
                .Size = New Size(Width - 10, 100)
                .Location = New Point(7, 5)
                .Text = String.Empty
                .BorderStyle = BorderStyle.None
                .Font = New Font("Tahoma", 10)
                .Multiline = True
            End With
        End Sub

        Sub New()
            MyBase.New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.UserPaint, True)

            AddRichTextBox()
            Controls.Add(AmbianceRTB)
            BackColor = Color.Transparent
            ForeColor = Color.FromArgb(76, 76, 76)

            P1 = New Pen(Color.FromArgb(180, 180, 180))
            Text = Nothing
            Font = New Font("Tahoma", 10)
            Size = New Size(150, 100)
            WordWrap = True
            AutoWordSelection = False
            DoubleBuffered = True

            AddHandler AmbianceRTB.Enter, AddressOf _Enter
            AddHandler AmbianceRTB.Leave, AddressOf _Leave
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            Dim B As New Bitmap(Width, Height)
            Dim G = Graphics.FromImage(B)

            G.SmoothingMode = SmoothingMode.AntiAlias

            G.Clear(Color.Transparent) ' Set control background to transparent
            G.FillPath(Brushes.White, Shape) ' Draw RTB background
            G.DrawPath(P1, Shape) ' Draw border

            G.Dispose()
            e.Graphics.DrawImage(B.Clone(), 0, 0)
            B.Dispose()
        End Sub
    End Class

#End Region
#Region " ListBox "

    Class Ambiance_ListBox
        Inherits ListBox

        Public Sub New()
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
            Me.DrawMode = DrawMode.OwnerDrawFixed
            IntegralHeight = False
            ItemHeight = 18
            Font = New Font("Seoge UI", 11, FontStyle.Regular)
        End Sub

        Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
            MyBase.OnDrawItem(e)
            e.DrawBackground()
            Dim LGB As New LinearGradientBrush(e.Bounds, Color.FromArgb(246, 132, 85), Color.FromArgb(231, 108, 57), 90.0F)
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                e.Graphics.FillRectangle(LGB, e.Bounds)
            End If
            Using b As New SolidBrush(e.ForeColor)
                If MyBase.Items.Count = Nothing Then
                    Exit Sub
                Else
                    e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), e.Font, b, e.Bounds)
                End If
            End Using
            LGB.Dispose()
        End Sub
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim MyRegion As New Region(e.ClipRectangle)
            e.Graphics.FillRegion(New SolidBrush(Me.BackColor), MyRegion)

            If Me.Items.Count > 0 Then
                For i As Integer = 0 To Me.Items.Count - 1
                    Dim RegionRect As System.Drawing.Rectangle = Me.GetItemRectangle(i)
                    If e.ClipRectangle.IntersectsWith(RegionRect) Then
                        If (Me.SelectionMode = SelectionMode.One AndAlso Me.SelectedIndex = i) OrElse (Me.SelectionMode = SelectionMode.MultiSimple AndAlso Me.SelectedIndices.Contains(i)) OrElse (Me.SelectionMode = SelectionMode.MultiExtended AndAlso Me.SelectedIndices.Contains(i)) Then
                            OnDrawItem(New DrawItemEventArgs(e.Graphics, Me.Font, RegionRect, i, DrawItemState.Selected, Me.ForeColor, _
                                Me.BackColor))
                        Else
                            OnDrawItem(New DrawItemEventArgs(e.Graphics, Me.Font, RegionRect, i, DrawItemState.[Default], Color.FromArgb(60, 60, 60), _
                                Me.BackColor))
                        End If
                        MyRegion.Complement(RegionRect)
                    End If
                Next
            End If
        End Sub
    End Class

#End Region
#Region " TabControl "

    Class Ambiance_TabControl
        Inherits TabControl

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.OptimizedDoubleBuffer Or _
                     ControlStyles.ResizeRedraw Or _
                     ControlStyles.UserPaint, True)
        End Sub

        Protected Overrides Sub CreateHandle()
            MyBase.CreateHandle()

            ItemSize = New Size(80, 24)
            Alignment = TabAlignment.Top
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            Dim ItemBoundsRect As Rectangle
            G.Clear(Parent.BackColor)
            For TabIndex As Integer = 0 To TabCount - 1
                ItemBoundsRect = GetTabRect(TabIndex)
                If Not TabIndex = SelectedIndex Then
                    G.DrawString(TabPages(TabIndex).Text, New Font(Font.Name, Font.Size - 2, FontStyle.Bold), New SolidBrush(Color.FromArgb(80, 76, 76)), New Rectangle(GetTabRect(TabIndex).Location, GetTabRect(TabIndex).Size), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
                End If
            Next

            ' Draw container rectangle
            G.FillPath(New SolidBrush(Color.FromArgb(247, 246, 246)), RoundRect(0, 23, Width - 1, Height - 24, 2))
            G.DrawPath(New Pen(Color.FromArgb(201, 198, 195)), RoundRect(0, 23, Width - 1, Height - 24, 2))

            For ItemIndex As Integer = 0 To TabCount - 1
                ItemBoundsRect = GetTabRect(ItemIndex)
                If ItemIndex = SelectedIndex Then

                    ' Draw header tabs
                    G.DrawPath(New Pen(Color.FromArgb(201, 198, 195)), RoundedTopRect(New Rectangle(New Point(ItemBoundsRect.X - 2, ItemBoundsRect.Y - 2), New Size(ItemBoundsRect.Width + 3, ItemBoundsRect.Height)), 7))
                    G.FillPath(New SolidBrush(Color.FromArgb(247, 246, 246)), RoundedTopRect(New Rectangle(New Point(ItemBoundsRect.X - 1, ItemBoundsRect.Y - 1), New Size(ItemBoundsRect.Width + 2, ItemBoundsRect.Height)), 7))

                    Try
                        G.DrawString(TabPages(ItemIndex).Text, New Font(Font.Name, Font.Size - 1, FontStyle.Bold), New SolidBrush(Color.FromArgb(80, 76, 76)), New Rectangle(GetTabRect(ItemIndex).Location, GetTabRect(ItemIndex).Size), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
                        TabPages(ItemIndex).BackColor = Color.FromArgb(247, 246, 246)
                    Catch : End Try
                End If
            Next
        End Sub
    End Class

#End Region

End Namespace
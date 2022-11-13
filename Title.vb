Imports System.Runtime.Intrinsics.X86
Imports System.Windows.Forms.VisualStyles

Public Class Title
    Dim titleImage As Image
    Private Sub Title_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '1024,768 폼 사이즈 ->1365,1020
        titleImage = My.Resources.title
        startButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 50) '버튼 위치조절'


    End Sub

    Private Sub Title_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        e.Graphics.DrawImage(titleImage, 0, 0)
    End Sub
    Private Sub Start_Click()

    End Sub

    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        Me.Hide()
        Game.Show()
    End Sub
End Class
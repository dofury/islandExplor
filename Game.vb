Public Class Game
    Dim homeImage As Image
    Dim island As Image
    Dim stage As Integer
    Dim story As Integer
    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        stage = 0
        story = 0
        homeImage = My.Resources.home
        island = My.Resources.title
        'context.Location = New Point(Me.Width - 15, context.Height)
        'Me.Controls.Add(context)


    End Sub

    Private Sub Game_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Select Case stage
            Case 0
                e.Graphics.DrawImage(homeImage, 0, 0, Me.Width - 15, Me.Height)
            Case 1

        End Select

    End Sub
    Private Sub story1()
        Select Case story
            Case 0
                gameText.Text = "나는 김춘배 냥인으로 3살이다."
                story += 1
                Invalidate()
            Case 1
                gameText.Text = "무인도로 떠난 나비의 소식이 끊겨 직접 가볼려고 한다."
                story += 1
        End Select
    End Sub

    Private Sub Context_Paint(e As PaintEventArgs)
        'e.Graphics.DrawImage(context, 0, Me.Height - context.Height - 40, Me.Width - 15, context.Height)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles gameText.Click

    End Sub

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles gameContext.Click
        Select Case stage
            Case 0
                story1()
        End Select
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles gameContext.Paint

    End Sub
End Class

Imports System.Drawing.Text
Imports System.Net.Security

Public Class Game
    Dim titleImage As Image
    Dim homeImage As Image
    Dim island As Image
    Dim stage As Integer
    Dim story As Integer
    Dim autoCheck As Boolean = False
    Dim skipCheck As Boolean = False
    Private textTimer As System.Timers.Timer
    Private textTypingTimer As System.Timers.Timer
    Private textTimerInterval As Integer = 50
    Dim gText As String
    Dim gTextCount As Integer
    Dim loadfiles As String
    Dim loadfile() As String
    Dim gameSound As New GameSounds
    Public font_naver As PrivateFontCollection = New PrivateFontCollection()
    Dim tfont_24 As Font
    Dim tfont_16 As Font
    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Start_Init()
        Init()
        'context.Location = New Point(Me.Width - 15, context.Height)
        'Me.Controls.Add(context)


    End Sub
    Private Sub Start_Init()
        CheckForIllegalCrossThreadCalls = False '스레드 체크 해제'
        startButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 50) '버튼 위치조절'
        gameIcon.Location = New Point(Me.Size.Width - gameIcon.Width - 20, Me.Size.Height - gameIcon.Height - 40)
        textTimer = New Timers.Timer(textTimerInterval)
        textTimer.AutoReset = True
        AddHandler textTimer.Elapsed, AddressOf autoSkipMenu_On
        textTypingTimer = New Timers.Timer(textTimerInterval)
        textTypingTimer.AutoReset = True
        AddHandler textTypingTimer.Elapsed, AddressOf Text_Typing
        Image_Load()
        Sound_Load()
        font_naver.AddFontFile("font/MaruBuri-Regular.ttf")
        font_naver.AddFontFile("font/MaruBuri-Bold.ttf")
        tfont_16 = New Font(font_naver.Families(0), 16)
        tfont_24 = New Font(font_naver.Families(0), 24)
        gameText.Font = tfont_24
        gameName.Font = tfont_16
    End Sub
    Private Sub Init()
        BGM_Stop()
        SE_Stop()
        gameContext.Hide()
        gameIcon.Hide()
        gameName.Hide()
        startButton.Show()
        stage = 0
        story = 0
        textTimerInterval = 0
        gameSound.Play("title")
    End Sub
    Private Sub BGM_Stop()
        Select Case stage
            Case 0
                gameSound.Stop("title")
            Case 1
                gameSound.Stop("living")
        End Select
    End Sub

    Private Sub SE_Stop()
        gameSound.Stop("tick")
        gameSound.Stop("typing")
    End Sub


    Private Sub Image_Load()
        titleImage = My.Resources.title
        homeImage = My.Resources.home
        island = My.Resources.title
    End Sub
    Private Sub Sound_Load()
        gameSound.AddSound("title", "sound/Adventure Starting.mp3")
        gameSound.AddSound("tick", "sound/Tick Sound.mp3")
        gameSound.AddSound("typing", "sound/TypeWriter.mp3")
        gameSound.AddSound("living", "sound/HOW ARE YOU.mp3")
        gameSound.SetVolume("living", 70)
        gameSound.SetVolume("title", 80)
    End Sub
    Private Sub Game_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Select Case stage
            Case 0
                e.Graphics.DrawImage(titleImage, 0, 0)
            Case 1
                e.Graphics.DrawImage(homeImage, 0, 0, Me.Width - 15, Me.Height)

        End Select

    End Sub
    Private Sub Story_1()
        If gameSound.IsPlaying("living") = 0 Then
            gameSound.Play("living")
        End If
        Select Case story
            Case 0
                gamePortrait.Image = My.Resources.hero
                gameName.Text = "춘배"
                gText = "나는 김춘배 여느 때와 다르지않게 집에서 쉬고 있었다."
                textTypingTimer.Start()
            Case 1
                gText = "역시 집이 최고라니까..."
                gameSound.Play("typing")
                textTypingTimer.Start()
            Case 2
                gameSound.Stop("typing")
                gText = "어딜가야하지?"
                textTypingTimer.Start()
        End Select
    End Sub

    Private Sub Text_Typing()
        If gameText.Text.Length = gText.Length Then
            gTextCount = 0
            textTypingTimer.Enabled = False
            Exit Sub
        End If
        gameText.Text = gText.Substring(0, gTextCount)
        gTextCount += 1
    End Sub


    Private Sub gameContext_Click(sender As Object, e As EventArgs) Handles gameContext.Click
        textTypingTimer.Stop()
        gameText.Text = gText
        story += 1
        Game_Next()
    End Sub


    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        BGM_Stop()
        gameSound.Play("tick")
        stage += 1
        startButton.Hide() '게임을 시작하고 게임버튼 숨김'
        gameContext.Show()
        gameName.Show()
        Game_Next()
        Invalidate()
    End Sub

    Private Sub gameText_Click(sender As Object, e As EventArgs) Handles gameText.Click
        textTypingTimer.Stop()
        gameText.Text = gText
        story += 1
        Game_Next()
    End Sub

    Private Sub Game_Next()
        SE_Stop()
        gameSound.Play("tick")
        Select Case stage
            Case 1
                Story_1()
        End Select
    End Sub

    Private Sub titleMenu_Click(sender As Object, e As EventArgs) Handles titleMenu.Click
        Init()
        Invalidate()
    End Sub

    Private Sub titleMenu_MouseHover(sender As Object, e As EventArgs) Handles titleMenu.MouseHover
        titleMenu.ForeColor = Color.Red
    End Sub

    Private Sub titleMenu_MouseLeave(sender As Object, e As EventArgs) Handles titleMenu.MouseLeave
        titleMenu.ForeColor = Color.Black
    End Sub

    Private Sub quitMenu_Click(sender As Object, e As EventArgs) Handles quitMenu.Click
        End
    End Sub

    Private Sub quitMenu_Leave(sender As Object, e As EventArgs) Handles quitMenu.MouseLeave
        quitMenu.ForeColor = Color.Black
    End Sub

    Private Sub quitMenu_MouseHover(sender As Object, e As EventArgs) Handles quitMenu.MouseHover
        quitMenu.ForeColor = Color.Red
    End Sub

    Private Sub closeMenu_Click(sender As Object, e As EventArgs) Handles closeMenu.Click
        gameContext.Hide()
        gameIcon.Show()
    End Sub

    Private Sub gameIcon_Click(sender As Object, e As EventArgs) Handles gameIcon.Click
        gameContext.Show()
        gameIcon.Hide()
    End Sub

    Private Sub closeMenu_MouseHover(sender As Object, e As EventArgs) Handles closeMenu.MouseHover
        closeMenu.ForeColor = Color.Red
    End Sub

    Private Sub closeMenu_MouseLeave(sender As Object, e As EventArgs) Handles closeMenu.MouseLeave
        closeMenu.ForeColor = Color.Black
    End Sub

    Private Sub autoMenu_Click(sender As Object, e As EventArgs) Handles autoMenu.Click
        If skipCheck = False Then
            If autoCheck = False Then
                autoCheck = True
                textTimer.Interval = 5000
                textTimer.Enabled = True
            Else
                autoCheck = False
                textTimer.Enabled = False
            End If
        End If

    End Sub
    Private Sub autoSkipMenu_On()
        textTypingTimer.Stop()
        gameText.Text = gText
        story += 1
        Game_Next()
    End Sub

    Private Sub autoMenu_MouseHover(sender As Object, e As EventArgs) Handles autoMenu.MouseHover
        If autoCheck = True Then
            autoMenu.ForeColor = Color.Black
        Else
            autoMenu.ForeColor = Color.Red
        End If
    End Sub

    Private Sub autoMenu_MouseLeave(sender As Object, e As EventArgs) Handles autoMenu.MouseLeave
        If autoCheck = True Then
            autoMenu.ForeColor = Color.Red
        Else
            autoMenu.ForeColor = Color.Black
        End If
    End Sub

    Private Sub skipMenu_Click(sender As Object, e As EventArgs) Handles skipMenu.Click
        If autoCheck = False Then
            If skipCheck = False Then
                skipCheck = True
                textTimer.Interval = 500
                textTimer.Enabled = True
            Else
                skipCheck = False
                textTimer.Enabled = False
            End If
        End If
    End Sub

    Private Sub skipMenu_MouseHover(sender As Object, e As EventArgs) Handles skipMenu.MouseHover
        If skipCheck = True Then
            skipMenu.ForeColor = Color.Black
        Else
            skipMenu.ForeColor = Color.Red
        End If
    End Sub

    Private Sub skipMenu_MouseLeave(sender As Object, e As EventArgs) Handles skipMenu.MouseLeave
        If skipCheck = True Then
            skipMenu.ForeColor = Color.Red
        Else
            skipMenu.ForeColor = Color.Black
        End If
    End Sub

    Private Sub saveMenu_Click(sender As Object, e As EventArgs) Handles saveMenu.Click
        Dim token As String = "/".ToString
        My.Computer.FileSystem.WriteAllText("save.txt", stage, False)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", story, True)
    End Sub

    Private Sub saveMenu_MouseHover(sender As Object, e As EventArgs) Handles saveMenu.MouseHover
        saveMenu.ForeColor = Color.Red
    End Sub

    Private Sub saveMenu_MouseLeave(sender As Object, e As EventArgs) Handles saveMenu.MouseLeave
        saveMenu.ForeColor = Color.Black
    End Sub

    Private Sub loadMenu_Click(sender As Object, e As EventArgs) Handles loadMenu.Click
        loadfiles = My.Computer.FileSystem.ReadAllText("save.txt")
        loadfile = loadfiles.Split("/")
        stage = loadfile(0)
        story = loadfile(1)
        Invalidate()
        Game_Next()

    End Sub

    Private Sub loadMenu_MouseHover(sender As Object, e As EventArgs) Handles loadMenu.MouseHover
        loadMenu.ForeColor = Color.Red
    End Sub

    Private Sub loadMenu_MouseLeave(sender As Object, e As EventArgs) Handles loadMenu.MouseLeave
        loadMenu.ForeColor = Color.Black
    End Sub

End Class

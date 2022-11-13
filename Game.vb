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
        infoButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 150) '버튼 위치조절'
        endButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 250) '버튼 위치조절'
        infoContext.Height = Me.Height
        infoContext.Location = New Point(0, 0)
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
        infoDraw() '게임 정보, 저작권 표시'
    End Sub
    Private Sub infoDraw()
        infoText.Font = tfont_16
        infoText.Text = ""
        infoText.Text += "게임 소개:" + vbCrLf
        infoText.Text += "이 게임은 선택형 스토리 게임 입니다. 선택에 따라 게임의 엔딩이 정해집니다." + vbCrLf
        infoText.Text += "저작권:" + vbCrLf
        infoText.Text += "- 이미지" + vbCrLf
        infoText.Text += "게임의 모든 이미지는 novel AI를 통해 제작했습니다." + vbCrLf
        infoText.Text += "- 노래" + vbCrLf
        infoText.Text += "● 배경음악" + vbCrLf
        infoText.Text += "HOW ARE YOU, 김재영, 공유마당" + vbCrLf + "https://gongu.copyright.or.kr/gongu/wrt/wrt/view.do?wrtSn=13073758&menuNo=200020" + vbCrLf
        infoText.Text += "Adventure Starting" + vbCrLf
        infoText.Text += "● 효과음" + vbCrLf
        infoText.Text += "대한민국 대표 BGM 셀바이뮤직 https://www.sellbuymusic.com"
    End Sub
    Private Sub Init()
        BGM_Stop()
        SE_Stop()
        gameContext.Hide()
        gameIcon.Hide()
        gameName.Hide()
        startButton.Show()
        infoButton.Show()
        endButton.Show()
        stage = 0
        story = 0
        textTimerInterval = 0
        gamePortrait.Image = My.Resources.context
        gameName.Text = ""
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
        gameSound.Stop("blackCow")
        gameSound.Stop("vibrate")
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
        gameSound.AddSound("blackCow", "sound/Single Cow Sound.mp3")
        gameSound.AddSound("vibrate", "sound/Phone Vibrating Sound.mp3")
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
                Portrait(False)
                gText = "김춘배 27세 나는 여느 때와 다르지않게 집에서 쉬고 있었다."
                textTypingTimer.Start()
            Case 1
                Portrait(True)
                gameName.Text = "춘배"
                gamePortrait.Image = My.Resources.hero
                gText = "역시 집에서는 컴퓨터하면서 쉬는게 최고라니까..."
                gameSound.Play("typing")
                textTypingTimer.Start()
            Case 2
                gameSound.Stop("typing")
                gText = "어디보자 주식은 좀 올랐나 볼까...?"
                textTypingTimer.Start()
            Case 3
                gameSound.Play("blackCow")
                gText = "아니 카카오 주가 왜이래...? 도대체 무슨 일이 있었던거야???"
                textTypingTimer.Start()
            Case 4
                gameSound.Stop("blackCow")
                gText = "후... 존버하면 다시 오를거야... 따흑..."
                textTypingTimer.Start()
            Case 5
                Portrait(False)
                gameSound.Play("vibrate")
                gText = "(스마트폰 진동음...)"
                textTypingTimer.Start()
            Case 6
                Portrait(True)
                gameSound.Stop("vibrate")
                gText = "어라, 김박사님이 대체 무슨일이시지? 당분간은 쉬라고 하셨는데"
                textTypingTimer.Start()
            Case 7
                gText = "여보세요 무슨일이시죠 김박사님?"
                textTypingTimer.Start()
        End Select
    End Sub
    Private Sub Portrait(check As Boolean)
        If check = False Then
            gameName.Hide()
            gamePortrait.Hide()
        Else
            gameName.Show()
            gamePortrait.Show()
        End If
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
        gameText_Check()
        Game_Next()
    End Sub


    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        BGM_Stop()
        gameSound.Play("tick")
        stage += 1
        startButton.Hide() '게임을 시작하고 게임버튼 숨김'
        infoButton.Hide()
        endButton.Hide()
        gameContext.Show()
        gameName.Show()
        Game_Next()
        Invalidate()
    End Sub

    Private Sub gameText_Click(sender As Object, e As EventArgs) Handles gameText.Click
        textTypingTimer.Stop()
        gameText_Check()
        Game_Next()
    End Sub

    Private Sub gameText_Check()
        If Not gameText.Text = gText Then '게임 텍스트와 실제 보여지는 텍스트가 다르면 바로보여주는 함수
            gameText.Text = gText
        Else
            story += 1
        End If
    End Sub

    Private Sub Game_Next() '다음 게임 텍스트로 넘어가는 함수
        SE_Stop()
        gameSound.Play("tick")
        Select Case stage
            Case 1
                Story_1()
        End Select
    End Sub

    Private Sub titleMenu_Click(sender As Object, e As EventArgs) Handles titleMenu.Click
        gameSound.Play("tick")
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
        gameSound.Play("tick")
        End
    End Sub

    Private Sub quitMenu_Leave(sender As Object, e As EventArgs) Handles quitMenu.MouseLeave
        quitMenu.ForeColor = Color.Black
    End Sub

    Private Sub quitMenu_MouseHover(sender As Object, e As EventArgs) Handles quitMenu.MouseHover
        quitMenu.ForeColor = Color.Red
    End Sub

    Private Sub closeMenu_Click(sender As Object, e As EventArgs) Handles closeMenu.Click
        gameSound.Play("tick")
        gameContext.Hide()
        gameIcon.Show()
    End Sub

    Private Sub gameIcon_Click(sender As Object, e As EventArgs) Handles gameIcon.Click
        gameSound.Play("tick")
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
        gameSound.Play("tick")
        If skipCheck = False Then
            If autoCheck = False Then
                autoCheck = True
                textTimer.Interval = 3000
                textTimer.Start()
            Else
                autoCheck = False
                textTimer.Stop()
            End If
        End If

    End Sub
    Private Sub autoSkipMenu_On()
        gameSound.Play("tick")
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
        gameSound.Play("tick")
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
        gameSound.Play("tick")
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

    Private Sub gameText_TextChanged(sender As Object, e As EventArgs) Handles gameText.TextChanged
        Dim MeasuredSize As Size
        MeasuredSize = TextRenderer.MeasureText(CType(sender, Label).Text, CType(sender, Label).Font,
                                                CType(sender, Label).Size,
                                                TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl)
        CType(sender, Label).Height = MeasuredSize.Height


    End Sub

    Private Sub endButton_Click(sender As Object, e As EventArgs) Handles endButton.Click
        gameSound.Play("tick")
        End
    End Sub

    Private Sub infoButton_Click(sender As Object, e As EventArgs) Handles infoButton.Click
        gameSound.Play("tick")
        startButton.Hide()
        infoButton.Hide()
        endButton.Hide()
        infoContext.Show()
    End Sub

    Private Sub xButton_Click(sender As Object, e As EventArgs) Handles xButton.Click
        gameSound.Play("tick")
        startButton.Show()
        infoButton.Show()
        endButton.Show()
        infoContext.Hide()
    End Sub

    Private Sub infoText_TextChanged(sender As Object, e As EventArgs) Handles infoText.TextChanged
        Dim MeasuredSize As Size
        MeasuredSize = TextRenderer.MeasureText(CType(sender, Label).Text, CType(sender, Label).Font,
                                                CType(sender, Label).Size,
                                                TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl)
        CType(sender, Label).Height = MeasuredSize.Height
    End Sub
End Class

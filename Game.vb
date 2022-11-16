Imports System.Drawing.Text
Imports System.IO.Compression
Imports System.Net.Security

Public Class Game
    Dim titleImage As Image
    Dim homeImage As Image
    Dim village_entry As Image
    Dim quizImages As New ArrayList

    Dim stage As Integer
    Dim story As Integer
    Dim quizNumber As Integer

    Dim autoCheck As Boolean = False
    Dim skipCheck As Boolean = False

    Private textTimer As System.Timers.Timer
    Private textTypingTimer As System.Timers.Timer
    Private systemTimer As System.Timers.Timer

    Private systemTimerType As String
    Private timerInterval As Integer = 50
    Private loadingCount As Integer = 0

    Dim gText As String
    Dim gTextCount As Integer

    Dim loadfiles As String
    Dim loadfile() As String
    Dim loadTexts As String
    Dim loadText() As String
    Dim loadQuizzes As String
    Dim loadQuiz() As String

    Dim loadQuizCount As Integer
    Dim loadTextCount As Integer

    Dim gameSound As New GameSounds

    Dim pcMousePoint As New Point
    Dim hintContextCheck As Boolean

    Public font_naver As PrivateFontCollection = New PrivateFontCollection()
    Dim tfont_24 As Font
    Dim tfont_16 As Font

    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Start_Init()
        Init()
    End Sub
    Private Sub Start_Init()

        CheckForIllegalCrossThreadCalls = False '스레드 체크 해제'
        LoadStory() '대본 불러오기
        LoadPuzzle() '퀴즈 불러오기

        font_naver.AddFontFile("font/MaruBuri-Regular.ttf") '폰트 설정
        font_naver.AddFontFile("font/MaruBuri-Bold.ttf")
        tfont_16 = New Font(font_naver.Families(0), 16)
        tfont_24 = New Font(font_naver.Families(0), 24)

        startButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 50) '버튼 위치조절'
        infoButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 150) '버튼 위치조절'
        endButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 250) '버튼 위치조절'

        infoContext.Height = Me.Height
        infoContext.Location = New Point(0, 0)

        playContext.Location = New Point(0, 0)
        playContext.Width = My.Resources.playContext.Width + 200
        playContext.Height = My.Resources.playContext.Height + 200
        playContext.Location = New Point(Me.Width / 2 - playContext.Width / 2, Me.Height / 2 - playContext.Height / 2)

        playText.Font = tfont_24
        playText.Width = playContext.Width
        playText.Location = New Point(0, 350)
        playTextContent.Font = tfont_16
        playTextContent.Width = playContext.Width
        playTextContent.Height = 200
        playTextContent.Location = New Point(0, 400)

        playText.Text = loadQuiz(loadQuizCount)
        playTextContent.Text = loadQuiz(loadQuizCount + 1)

        playTextInput.Width = playContext.Width / 4
        playTextInput.Location = New Point(playContext.Width / 2 - playTextInput.Width / 2, 700)

        loadingContext.Width = Me.Width
        loadingContext.Height = Me.Height
        loadingContext.Location = New Point(0, 0)
        loadingText.Font = tfont_24
        loadingText.Text = "로딩중"
        loadingText.Location = New Point(loadingContext.Width / 2 - loadingText.Width / 2, loadingContext.Height / 2 - loadingText.Height / 2)



        hintButton.Location = New Point(playTextInput.Location.X - hintButton.Width - 10, playTextInput.Location.Y - (hintButton.Height / 2 - playTextInput.Height / 2))
        checkButton.Location = New Point(playTextInput.Location.X + playTextInput.Width + 10, hintButton.Location.Y)

        quizCheck.Location = New Point(playTextInput.Location.X + (playTextInput.Width / 2 - quizCheck.Width / 2), playTextInput.Location.Y - quizCheck.Height)
        quizCheck.Font = tfont_16
        quizCheck.Text = ""

        hintContext.Location = New Point(playContext.Width / 2 - hintContext.Width / 2, playContext.Height / 2 - hintContext.Height / 2)
        hintLabel.Text = "힌트"
        hintLabel.Font = tfont_24
        hintLabel.Location = New Point(hintContext.Width / 2 - hintLabel.Width / 2, 50)
        hintLabelContent.Text = "머리를 써라"
        hintLabelContent.Font = tfont_16
        hintLabelContent.Location = New Point(hintContext.Width / 2 - hintLabelContent.Width / 2, 100)

        gameIcon.Location = New Point(Me.Size.Width - gameIcon.Width - 20, Me.Size.Height - gameIcon.Height - 40)

        textTimer = New Timers.Timer(timerInterval)
        textTimer.AutoReset = True
        AddHandler textTimer.Elapsed, AddressOf autoSkipMenu_On

        textTypingTimer = New Timers.Timer(timerInterval)
        textTypingTimer.AutoReset = True
        AddHandler textTypingTimer.Elapsed, AddressOf Text_Typing

        systemTimer = New Timers.Timer(timerInterval)
        systemTimer.Interval = 1500
        systemTimer.AutoReset = True
        AddHandler systemTimer.Elapsed, AddressOf systemTimer_On

        Image_Load()
        Sound_Load()

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
        quizNumber = 0
        loadTextCount = 0
        loadQuizCount = 0
        timerInterval = 0
        gamePortrait.Image = My.Resources.context
        gameName.Text = ""
        playTextInput.Text = ""
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

    Private Sub LoadStory() '스토리 원고 로드 함수'
        loadTexts = My.Computer.FileSystem.ReadAllText("story.txt")
        loadText = loadTexts.Split(vbCrLf)
    End Sub

    Private Sub LoadPuzzle()
        loadQuizzes = My.Computer.FileSystem.ReadAllText("quiz.txt")
        loadQuiz = loadQuizzes.Split("/")
    End Sub

    Private Sub SE_Stop()
        gameSound.Stop("tick")
        gameSound.Stop("typing")
        gameSound.Stop("blackCow")
        gameSound.Stop("vibrate")
        gameSound.Stop("doorBell")
        gameSound.Stop("writePen")
        gameSound.Stop("phoneEnd")
        gameSound.Stop("correct")
        gameSound.Stop("incorrect")
        gameSound.Stop("pop")
    End Sub


    Private Sub Image_Load()
        titleImage = My.Resources.title
        homeImage = My.Resources.home
        village_entry = My.Resources.village_entry
        quizImages.Add(My.Resources.quiz1)
        quizImages.Add(My.Resources.quiz2)
    End Sub
    Private Sub Sound_Load()
        gameSound.AddSound("title", "sound/Adventure Starting.mp3")
        gameSound.AddSound("tick", "sound/Tick Sound.mp3")
        gameSound.AddSound("typing", "sound/TypeWriter.mp3")
        gameSound.AddSound("living", "sound/HOW ARE YOU.mp3")
        gameSound.AddSound("blackCow", "sound/Single Cow Sound.mp3")
        gameSound.AddSound("vibrate", "sound/Phone Vibrating Sound.mp3")
        gameSound.AddSound("doorBell", "sound/Door Bell Sound.mp3")
        gameSound.AddSound("writePen", "sound/Pencil Write Kor.wav")
        gameSound.AddSound("phoneEnd", "sound/Phone Dialing With Dialtone Sound.wav")
        gameSound.AddSound("correct", "sound/Correct.mp3")
        gameSound.AddSound("incorrect", "sound/Error.mp3")
        gameSound.AddSound("pop", "sound/Blop Sound.mp3")
        gameSound.SetVolume("living", 70)
        gameSound.SetVolume("title", 80)
    End Sub
    Private Sub Game_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Select Case stage
            Case 0
                e.Graphics.DrawImage(titleImage, 0, 0)
            Case 1
                e.Graphics.DrawImage(homeImage, 0, 0, Me.Width - 15, Me.Height)

            Case 2
                If story <= 2 Then
                    e.Graphics.DrawImage(village_entry, 0, 0, Me.Width - 15, Me.Height)
                End If

        End Select

    End Sub
    Private Sub Story_1()

        setPortrait(loadText(loadTextCount))
        Story_1_Event()
        gText = loadText(loadTextCount + 1)
        Invalidate()
        textTypingTimer.Start()
    End Sub
    Private Sub Story_1_Event()
        If gameSound.IsPlaying("living") = False Then
            gameSound.Play("living")
        End If
        Select Case story 'story.txt 위치 계산 공식 (story+1)*2
            Case 0
                gamePortrait.BackgroundImage = My.Resources.home
            Case 1
                If gameSound.IsPlaying("typing") = False Then
                    gameSound.Play("typing")
                End If
            Case 2
                gameSound.Stop("typing")
            Case 3
                If gameSound.IsPlaying("blackCow") = False Then
                    gameSound.Play("blackCow")
                End If
            Case 4
                gameSound.Stop("blackCow")
            Case 5
                If gameSound.IsPlaying("vibrate") = False Then
                    gameSound.Play("vibrate")
                End If
            Case 6
                gameSound.Stop("vibrate")
            Case 16
                If gameSound.IsPlaying("phoneEnd") = False Then
                    gameSound.Play("phoneEnd")
                End If
            Case 17
                gameSound.Stop("phoneEnd")
            Case 19
                If gameSound.IsPlaying("doorBell") = False Then
                    gameSound.Play("doorBell")
                End If
            Case 20
                gameSound.Stop("doorBell")
            Case 24
                If gameSound.IsPlaying("writePen") = False Then
                    gameSound.Play("writePen")
                End If
            Case 25
                gameSound.Stop("writePen")
            Case 30
                TextTimer_Stop() 'skip and auto 일시 퀴즈 전에 멈춤
            Case 31
                quiz_Show()
        End Select
    End Sub

    Private Sub Story_2()

        setPortrait(loadText(loadTextCount))
        Story_2_Event()
        gText = loadText(loadTextCount + 1)
        Invalidate()
        textTypingTimer.Start()
    End Sub
    Private Sub Story_2_Event()
        If gameSound.IsPlaying("living") = False Then
            gameSound.Play("living")
        End If
        Select Case story 'story.txt 위치 계산 공식 (story+1)*2
            Case 0
                gamePortrait.BackgroundImage = My.Resources.title
            Case 1
            Case 2
            Case 3
            Case 4
            Case 5

            Case 6
            Case 16
            Case 17
            Case 19
            Case 20
            Case 24
            Case 25
            Case 30
            Case 31
        End Select
    End Sub
    Private Sub Portrait(check As Boolean)
        If check = False Then
            gamePortrait.Hide()
            gameName.Hide()
        Else
            gamePortrait.Show()
            gameName.Show()
        End If
    End Sub
    Private Sub setPortrait(name As String)
        Select Case name
            Case "김춘배"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.hero
            Case "이박사"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.doctor_lee
            Case "집배원"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.postman
            Case Else
                Portrait(False)
        End Select
    End Sub
    Private Sub Text_Typing()
        Try
            Me.Invoke(New deleText(AddressOf allocText), gText) '크로스쓰레드 문제 해결
        Catch ex As Exception

        End Try
    End Sub

    Delegate Sub deleText(ByVal text As String) '델리게이트 선언
    Private Sub allocText(ByVal text As String) '델리게이트 함수선언
        If gameText.Text = text Then
            gTextCount = 0
            textTypingTimer.Stop()
            Exit Sub
        End If

        If gTextCount < text.Length Then
            gTextCount += 1
        End If
        gameText.Text = text.Substring(0, gTextCount)
    End Sub

    Delegate Sub deleLabel(label As Label) '델리게이트 선언
    Private Sub allocLabel(label As Label) '델리게이트 함수선언
        If systemTimerType = "quizCheck1" Or systemTimerType = "quizCheck2" Then
            label.Text = ""
            systemTimer.Stop()
        End If
        If systemTimerType = "loading" Then
            If loadingCount <= 10 Then
                If loadingCount = 3 Or loadingCount = 7 Then
                    loadingText.Text = "로딩중"
                Else
                    loadingText.Text += "."
                End If
                loadingCount += 1
            Else
                loadingText.Text = "로딩중"
                loadingCount = 0
                systemTimer.Stop()
                loadingContext.Hide()

            End If

        End If

    End Sub

    Private Sub gameContext_Click(sender As Object, e As EventArgs) Handles gameContext.Click
        gameText_Check()
        Game_Next()
    End Sub


    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        BGM_Stop()
        Object_MouseClick()
        stage += 1
        startButton.Hide() '게임을 시작하고 게임버튼 숨김'
        infoButton.Hide()
        endButton.Hide()
        gameContext.Show()
        gameName.Show()
        Game_Next()
        Invalidate()
        loading_Show()
    End Sub

    Private Sub loading_Show()
        loadingContext.Show()
        systemTimerType = "loading"
        systemTimer.Interval = 100
        systemTimer.Start()
    End Sub
    Private Sub gameText_Click(sender As Object, e As EventArgs) Handles gameText.Click
        gameText_Check()
        Game_Next()
    End Sub

    Private Sub gameText_Check()
        If Not gameText.Text = gText Then '게임 텍스트와 실제 보여지는 텍스트가 다르면 바로보여주는 함수
            gameText.Text = gText
            textTypingTimer.Stop()
        Else
            If loadTextCount < loadText.Length - 2 Then
                story += 1
                loadTextCount += 2
            End If
        End If

    End Sub

    Private Sub Game_Next() '다음 게임 텍스트로 넘어가는 함수
        Object_MouseClick()
        If story = 33 Then
            stage += 1
            story = 0
            loading_Show()
        End If
        Select Case stage
            Case 1
                Story_1()
            Case 2
                Story_2()
        End Select
    End Sub

    Private Sub titleMenu_Click(sender As Object, e As EventArgs) Handles titleMenu.Click
        Object_MouseClick()
        Init()
        Invalidate()
    End Sub

    Private Sub titleMenu_MouseHover(sender As Object, e As EventArgs) Handles titleMenu.MouseHover
        titleMenu.ForeColor = Color.Red
        Object_MouseHover()
    End Sub

    Private Sub titleMenu_MouseLeave(sender As Object, e As EventArgs) Handles titleMenu.MouseLeave
        titleMenu.ForeColor = Color.Black
    End Sub

    Private Sub quitMenu_Click(sender As Object, e As EventArgs) Handles quitMenu.Click
        Object_MouseClick()
        End
    End Sub

    Private Sub quitMenu_Leave(sender As Object, e As EventArgs) Handles quitMenu.MouseLeave
        quitMenu.ForeColor = Color.Black
    End Sub

    Private Sub quitMenu_MouseHover(sender As Object, e As EventArgs) Handles quitMenu.MouseHover
        quitMenu.ForeColor = Color.Red
        Object_MouseHover()
    End Sub

    Private Sub closeMenu_Click(sender As Object, e As EventArgs) Handles closeMenu.Click
        Object_MouseClick()
        gameContext.Hide()
        gameIcon.Show()
    End Sub

    Private Sub gameIcon_Click(sender As Object, e As EventArgs) Handles gameIcon.Click
        Object_MouseClick()
        gameContext.Show()
        gameIcon.Hide()
    End Sub

    Private Sub closeMenu_MouseHover(sender As Object, e As EventArgs) Handles closeMenu.MouseHover
        closeMenu.ForeColor = Color.Red
        Object_MouseHover()
    End Sub

    Private Sub closeMenu_MouseLeave(sender As Object, e As EventArgs) Handles closeMenu.MouseLeave
        closeMenu.ForeColor = Color.Black
    End Sub

    Private Sub autoMenu_Click(sender As Object, e As EventArgs) Handles autoMenu.Click
        SE_Stop()
        Object_MouseClick()
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
        gameText_Check()

        Game_Next()
    End Sub

    Private Sub autoMenu_MouseHover(sender As Object, e As EventArgs) Handles autoMenu.MouseHover
        Object_MouseHover()
        If autoCheck = True Then
            autoMenu.ForeColor = Color.Yellow
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
        SE_Stop()
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
        Object_MouseHover()
        If skipCheck = True Then
            skipMenu.ForeColor = Color.Yellow
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
        Object_MouseClick()
        Dim token As String = "/".ToString
        My.Computer.FileSystem.WriteAllText("save.txt", stage, False)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", story, True)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", loadTextCount, True)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", quizNumber, True)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", loadQuizCount, True)
    End Sub

    Private Sub saveMenu_MouseHover(sender As Object, e As EventArgs) Handles saveMenu.MouseHover
        saveMenu.ForeColor = Color.Red
        Object_MouseHover()
    End Sub

    Private Sub saveMenu_MouseLeave(sender As Object, e As EventArgs) Handles saveMenu.MouseLeave
        saveMenu.ForeColor = Color.Black
    End Sub

    Private Sub loadMenu_Click(sender As Object, e As EventArgs) Handles loadMenu.Click
        gameSound.Play("tick")
        SE_Stop()
        loadfiles = My.Computer.FileSystem.ReadAllText("save.txt")
        loadfile = loadfiles.Split("/")
        stage = loadfile(0)
        story = loadfile(1)
        loadTextCount = loadfile(2)
        quizNumber = loadfile(3)
        loadQuizCount = loadfile(4)
        Invalidate()
        gameText_Check()
        Game_Next()

    End Sub

    Private Sub loadMenu_MouseHover(sender As Object, e As EventArgs) Handles loadMenu.MouseHover
        loadMenu.ForeColor = Color.Red
        Object_MouseHover()
    End Sub

    Private Sub loadMenu_MouseLeave(sender As Object, e As EventArgs) Handles loadMenu.MouseLeave
        loadMenu.ForeColor = Color.Black
    End Sub

    Private Sub gameText_TextChanged(sender As Object, e As EventArgs) Handles gameText.TextChanged
        text_AutoSize(sender)
    End Sub

    Private Sub endButton_Click(sender As Object, e As EventArgs) Handles endButton.Click
        Object_MouseClick()
        End
    End Sub

    Private Sub infoButton_Click(sender As Object, e As EventArgs) Handles infoButton.Click
        Object_MouseClick()
        startButton.Hide()
        infoButton.Hide()
        endButton.Hide()
        infoContext.Show()
    End Sub

    Private Sub infoXButton_Click(sender As Object, e As EventArgs) Handles infoXButton.Click
        Object_MouseClick()
        startButton.Show()
        infoButton.Show()
        endButton.Show()
        infoContext.Hide()
    End Sub

    Private Sub infoText_TextChanged(sender As Object, e As EventArgs) Handles infoText.TextChanged
        text_AutoSize(sender)
    End Sub

    Private Sub playText_TextChanged(sender As Object, e As EventArgs) Handles playText.TextChanged
        text_AutoSize(sender)
    End Sub

    Private Sub text_AutoSize(sender As Object)
        Dim MeasuredSize As Size
        MeasuredSize = TextRenderer.MeasureText(CType(sender, Label).Text, CType(sender, Label).Font,
                                                CType(sender, Label).Size,
                                                TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl)
        CType(sender, Label).Height = MeasuredSize.Height
    End Sub

    Private Sub playContext_Paint(sender As Object, e As PaintEventArgs) Handles playContext.Paint
        Dim quizImageWidth As Integer = (playContext.Width / 2) - (quizImages(quizNumber).Width / 2) - 100
        e.Graphics.DrawImage(quizImages(quizNumber), quizImageWidth, 50)
    End Sub

    Private Sub checkButton_Click(sender As Object, e As EventArgs) Handles checkButton.Click '정답 버튼'
        Select Case quizNumber
            Case 0
                Try
                    If playTextInput.Text = 0 Then
                        quiz_Correct("quizCheck1")
                    Else
                        Throw New System.Exception("오답")
                    End If
                Catch ex As Exception
                    quiz_Incorrect("quizCheck2")
                End Try
        End Select

    End Sub
    Private Sub quiz_Correct(type As String)
        gameSound.Play("correct")
        quizCheck.ForeColor = Color.Green
        quizCheck.Text = "정답"
        loadQuizCount += 2
        quizNumber += 1
        systemTimerType = type
        systemTimer.Interval = 1500
        systemTimer.Start()
    End Sub
    Private Sub quiz_Incorrect(type As String)
        gameSound.Play("incorrect")
        quizCheck.ForeColor = Color.Red
        quizCheck.Text = "오답"
        systemTimerType = type
        systemTimer.Interval = 1500
        systemTimer.Start()
    End Sub
    Private Sub systemTimer_On()
        Select Case systemTimerType
            Case "loading"
                Me.Invoke(New deleLabel(AddressOf allocLabel), loadingText) '크로스쓰레드 문제 해결
            Case "quizCheck1"
                Me.Invoke(New deleLabel(AddressOf allocLabel), quizCheck) '크로스쓰레드 문제 해결
                playContext.Hide()
                gameContext.Show()
            Case "quizCheck2"
                Me.Invoke(New deleLabel(AddressOf allocLabel), quizCheck) '크로스쓰레드 문제 해결
        End Select
    End Sub

    Private Sub hintButton_Click(sender As Object, e As EventArgs) Handles hintButton.Click
        Object_MouseClick()
        hintContext.Show()
    End Sub

    Private Sub checkButton_MouseHover(sender As Object, e As EventArgs) Handles checkButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub hintButton_MouseHover(sender As Object, e As EventArgs) Handles hintButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub Object_MouseHover()
        gameSound.Play("pop")
    End Sub

    Private Sub Object_MouseClick()
        gameSound.Play("tick")
    End Sub

    Private Sub startButton_MouseHover(sender As Object, e As EventArgs) Handles startButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub infoButton_MouseHover(sender As Object, e As EventArgs) Handles infoButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub endButton_MouseHover(sender As Object, e As EventArgs) Handles endButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub hintXButton_Click(sender As Object, e As EventArgs) Handles hintXButton.Click
        Object_MouseClick()
        hintContext.Hide()
    End Sub

    Private Sub hintXButton_MouseHover(sender As Object, e As EventArgs) Handles hintXButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub quiz_Show()
        playText.Text = loadQuiz(loadQuizCount)
        playTextContent.Text = loadQuiz(loadQuizCount + 1)
        playContext.Show()
        gameContext.Hide()
    End Sub

    Private Sub TextTimer_Stop()
        If textTimer.Enabled = True Then
            textTimer.Stop()
            autoMenu.ForeColor = Color.Black
            skipMenu.ForeColor = Color.Black
        End If
    End Sub

End Class

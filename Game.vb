Imports System.Drawing.Text
Imports System.IO.Compression
Imports System.Net.Security

Public Class Game
    Dim titleImage As Image
    Dim homeImage As Image
    Dim village_entry As Image
    Dim village_main As Image

    Dim gameItems(10) As Image
    Dim quizImages(10) As Image
    Dim quizResultImages(10) As Image

    Dim inputKeys As New ArrayList

    Dim stage As Integer
    Dim story As Integer
    Dim quizNumber As Integer


    Dim autoCheck As Boolean = False
    Dim skipCheck As Boolean = False

    Private textTimer As System.Timers.Timer
    Private textTypingTimer As System.Timers.Timer
    Private keyboardTimer As System.Timers.Timer
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

    Dim playResultText As String
    Dim playResult As Boolean

    Dim akControl As Boolean

    Dim gameSound As New GameSounds

    Dim pcMousePoint As New Point
    Dim hintContextCheck As Boolean

    Public font_naver As PrivateFontCollection = New PrivateFontCollection()
    Dim tfont_16 As Font
    Dim tfont_24 As Font
    Dim tfont_32 As Font

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
        tfont_32 = New Font(font_naver.Families(0), 32)

        startButton.Location = New Point(Me.Size.Width / 2 - 15 - (startButton.Width / 2), Me.Size.Height / 2 - 10) '버튼 위치조절'
        infoButton.Location = New Point(Me.Size.Width / 2 - 15 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 90) '버튼 위치조절'
        endButton.Location = New Point(Me.Size.Width / 2 - 15 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 190) '버튼 위치조절'

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
        hintLabelContent.Text = loadQuiz(loadQuizCount + 2)
        playResultText = loadQuiz(loadQuizCount + 3)

        playTextInput.Width = playContext.Width / 4
        playTextInput.Location = New Point(playContext.Width / 2 - playTextInput.Width / 2, 700)

        okButton.Location = New Point(playContext.Width / 2 - okButton.Width / 2, 700)
        okButton.Hide()

        loadingContext.Width = Me.Width
        loadingContext.Height = Me.Height
        loadingContext.Location = New Point(0, 0)
        loadingText.Font = tfont_32
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
        hintLabel.Location = New Point(hintContext.Width / 2 - hintLabel.Width / 2, 30)
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

        keyboardTimer = New Timers.Timer(timerInterval)
        keyboardTimer.Interval = 100
        keyboardTimer.AutoReset = True
        AddHandler keyboardTimer.Elapsed, AddressOf keyInput

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
        infoText.Text += "이 게임은 퍼즐형 스토리 게임 입니다. 수수께끼의 해결을 통해 게임을 진행합니다." + vbCrLf
        infoText.Text += "제작자: 차도훈(도푸리)" + vbCrLf
        infoText.Text += "저작권:" + vbCrLf
        infoText.Text += "- 퀴즈" + vbCrLf
        infoText.Text += "첫번째 퀴즈를 제외한 퀴즈는 레이튼 교수 이상한 마을을 참고했습니다." + vbCrLf
        infoText.Text += "- 이미지" + vbCrLf
        infoText.Text += "게임의 모든 이미지는 novel AI를 통해 제작했습니다." + vbCrLf
        infoText.Text += "- 노래" + vbCrLf
        infoText.Text += "● 배경음악" + vbCrLf
        infoText.Text += "HOW ARE YOU, 김재영, 공유마당" + vbCrLf + "https://gongu.copyright.or.kr/gongu/wrt/wrt/view.do?wrtSn=13073758&menuNo=200020" + vbCrLf
        infoText.Text += "Tong tong (통통), 이혜린, 공유마당" + vbCrLf + "https://gongu.copyright.or.kr/gongu/wrt/wrt/view.do?wrtSn=13048800&menuNo=200020" + vbCrLf
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
        playResult = False

        playContext.Enabled = False
        gameContext.Enabled = False
        gameIcon.Enabled = False


        akControl = False
        gameName.Text = ""
        playTextInput.Text = ""
        gameSound.Play("title")
    End Sub
    Private Sub BGM_Stop()

        gameSound.Stop("title")
        gameSound.Stop("living")
        gameSound.Stop("village")

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
        village_main = My.Resources.village_main

        quizImages(0) = My.Resources.quiz1
        quizImages(1) = My.Resources.quiz2
        quizImages(2) = My.Resources.quiz2

        quizResultImages(0) = quizImages(0)
        quizResultImages(1) = My.Resources.quiz2_result
        quizResultImages(2) = quizImages(2)

        gameItems(0) = My.Resources.stock_1 '주식'
        gameItems(1) = My.Resources.map_and_letter '맵과 편지'
        gameItems(2) = My.Resources.stock_2
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
        gameSound.AddSound("village", "sound/Tongtong.mp3")
        gameSound.SetVolume("living", 70)
        gameSound.SetVolume("title", 80)
    End Sub
    Private Sub Game_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Select Case stage
            Case 0
                e.Graphics.DrawImage(titleImage, 0, 0, Me.Width - 15, Me.Height)
            Case 1
                e.Graphics.DrawImage(homeImage, 0, 0, Me.Width - 15, Me.Height)
                If story = 3 Or story = 4 Then
                    e.Graphics.DrawImage(gameItems(0), getGameItemW(0), getGameItemH(0, 0))
                    'stock
                ElseIf story = 27 Then
                    e.Graphics.DrawImage(gameItems(1), getGameItemW(1), getGameItemH(1, -50))
                End If

            Case 2
                If story <= 17 Then
                    e.Graphics.DrawImage(village_entry, 0, 0, Me.Width - 15, Me.Height)
                ElseIf story >= 18 Then
                    e.Graphics.DrawImage(village_main, 0, 0, Me.Width - 15, Me.Height)
                End If

        End Select
        formContext_Check()

    End Sub

    Private Sub formContext_Check()
        If gameContext.Enabled = False Then
            gameContext.Hide()
        Else
            gameContext.Show()
        End If

        If gameIcon.Enabled = False Then
            gameIcon.Hide()
        Else
            gameIcon.Show()
        End If


        If playContext.Enabled = False Then
            playContext.Hide()
        Else
            playContext.Show()
        End If
    End Sub


    Private Function getGameItemW(i As Integer)
        Dim gameItemW = Me.Width / 2 - gameItems(i).Width / 2
        Return CInt(gameItemW)
    End Function

    Private Function getGameItemH(i As Integer, h As Integer)
        Dim gameItemH = Me.Height / 2 - gameItems(0).Height / 2 - 100 + h
        Return CInt(gameItemH)
    End Function
    Private Sub Story_1()

        setPortrait(loadText(loadTextCount))
        Story_1_Event()
        gText = loadText(loadTextCount + 1)
        Invalidate()
        textTypingTimer.Start()
    End Sub
    Private Sub Story_1_Event()
        If gameSound.IsPlaying("living") = False Then
            BGM_Stop()
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
        If gameSound.IsPlaying("village") = False Then
            BGM_Stop()
            gameSound.Play("village")
        End If
        Select Case story 'story.txt 위치 계산 공식 x/2 - 34
            Case 0
                gamePortrait.BackgroundImage = My.Resources.village_entry
            Case 10
                quiz_Show()
            Case 31
                quiz_Show()
            Case 47
                quiz_Show()
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

    Private Sub keyInput()
        For i = 0 To inputKeys.Count - 1
            If inputKeys(i) = Keys.Space Then
                game_Progress()
            ElseIf inputKeys(i) = Keys.Enter Then
                Object_MouseClick()
                If gameContext.Enabled = False And akControl = True Then
                    gameContext.Enabled = True
                    gameIcon.Enabled = False
                    Invalidate()
                ElseIf gameContext.Enabled = True And akControl = True Then
                    gameContext.Enabled = False
                    gameIcon.Enabled = True
                    Invalidate()
                End If
            End If
        Next
    End Sub
    Private Sub setPortrait(name As String)
        Select Case name
            Case "김춘배"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.chunbae
            Case "김나비"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.nabi
            Case "???"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.nabi
            Case "김영철"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.youngchul
            Case "이박사"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.doctor_lee
            Case "집배원"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.postman
            Case "베인"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.vayne
            Case "에이빈"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.abin
            Case "빈스"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.bins
            Case "시스터"
                Portrait(True)
                gameName.Text = name
                gamePortrait.Image = My.Resources.cster
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
        If systemTimerType = "quizCheck" Then
            label.Text = ""
            If playResult = True Then
                playResult_On()
            End If
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
    Private Sub playResult_On()
        Invalidate()
        hintContext.Enabled = False
        playTextInput.Enabled = False
        checkButton.Enabled = False
        hintButton.Enabled = False

        Invalidate()

        okButton.Show()
        playTextContent.Text = playResultText
    End Sub

    Private Sub playResult_Off()
        playResult = False
        okButton.Hide()
        playTextContent.Text = ""
        checkButton.Show()
        playTextInput.Show()
        hintButton.Show()
    End Sub

    Private Sub gameContext_Click(sender As Object, e As EventArgs) Handles gameContext.Click
        TextTimer_Stop()
        gameText_Check()
        Game_Next()
    End Sub


    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        BGM_Stop()
        Object_MouseClick()
        gameContext.Enabled = True
        akControl = True
        stage += 1
        startButton.Hide() '게임을 시작하고 게임버튼 숨김'
        infoButton.Hide()
        endButton.Hide()
        gameContext.Show()
        gameName.Show()
        Game_Next()
        loading_Show()
    End Sub

    Private Sub loading_Show()
        loadingContext.Show()
        systemTimerType = "loading"
        systemTimer.Interval = 100
        systemTimer.Start()
    End Sub
    Private Sub gameText_Click(sender As Object, e As EventArgs) Handles gameText.Click
        TextTimer_Stop()
        game_Progress()
    End Sub

    Private Sub game_Progress() '대화를 빠르게 진행시 퀴즈를 풀기전에 story가 진행해버려 에러가 생김
        If akControl = True Then
            Object_MouseClick()
            gameText_Check()
            Game_Next()
        End If
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
        If stage = 1 And story = 33 Then
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
        gameContext.Enabled = False
        gameIcon.Enabled = True
        Invalidate()
    End Sub

    Private Sub gameIcon_Click(sender As Object, e As EventArgs) Handles gameIcon.Click
        gameContext.Enabled = True
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
        If skipCheck = False And akControl = True Then
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
        game_Progress()
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
        If autoCheck = False And akControl = True Then
            If skipCheck = False Then
                skipCheck = True
                textTimer.Interval = 100
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
        game_Progress()

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
        Dim quizImageX As Integer = (playContext.Width / 2) - (quizImages(quizNumber).Width / 2) - (quizImages(quizNumber).Width / 6)
        Dim quizResultImageX As Integer = (playContext.Width / 2) - (quizResultImages(quizNumber).Width / 2) - (quizResultImages(quizNumber).Width / 6)
        If playResult = False Then
            e.Graphics.DrawImage(quizImages(quizNumber), quizImageX, 50)
        Else
            e.Graphics.DrawImage(quizResultImages(quizNumber), quizResultImageX, 50)
        End If
        playContext_Check()
    End Sub

    Private Sub checkButton_Click(sender As Object, e As EventArgs) Handles checkButton.Click '정답 버튼'
        Select Case quizNumber
            Case 0
                Try
                    If playTextInput.Text = 0 Then
                        quiz_Correct("quizCheck")
                        playTextInput.Text = ""
                    Else
                        Throw New System.Exception("오답")
                    End If
                Catch ex As Exception
                    quiz_Incorrect("quizCheck")
                End Try
            Case 1
                Try
                    If playTextInput.Text = 4 Then
                        quiz_Correct("quizCheck")
                        playTextInput.Text = ""
                    Else
                        Throw New System.Exception("오답")
                    End If
                Catch ex As Exception
                    quiz_Incorrect("quizCheck")
                End Try
            Case 2
                Try
                    If playTextInput.Text = 4 Then
                        quiz_Correct("quizCheck")
                        playTextInput.Text = ""
                    Else
                        Throw New System.Exception("오답")
                    End If
                Catch ex As Exception
                    quiz_Incorrect("quizCheck")
                End Try
        End Select

    End Sub
    Private Sub quiz_Correct(type As String)
        gameSound.Play("correct")
        quizCheck.ForeColor = Color.Green
        quizCheck.Text = "정답"
        playResult = True
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
            Case "quizCheck"
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
        TextTimer_Stop() 'skip and auto 일시 퀴즈 전에 멈춤
        akControl = False 'skip auto 사용불가
        playContext.Enabled = True
        gameContext.Enabled = False
        gameIcon.Enabled = False
        Invalidate()
        playText.Text = loadQuiz(loadQuizCount)
        playTextContent.Text = loadQuiz(loadQuizCount + 1)
        hintLabelContent.Text = loadQuiz(loadQuizCount + 2)
        playResultText = loadQuiz(loadQuizCount + 3)
    End Sub

    Private Sub TextTimer_Stop()
        If textTimer.Enabled = True Then
            textTimer.Stop()
            autoMenu.ForeColor = Color.Black
            skipMenu.ForeColor = Color.Black
            skipCheck = False
            autoCheck = False
        End If
    End Sub

    Private Sub okButton_Click(sender As Object, e As EventArgs) Handles okButton.Click
        playResult_Off()
        gameContext.Enabled = True
        playContext.Enabled = False
        akControl = True
        Invalidate()
        loadQuizCount += 4
        quizNumber += 1
    End Sub

    Private Sub Game_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        keyboardTimer.Start()
        If Not inputKeys.Contains(e.KeyCode) Then
            inputKeys.Add(e.KeyCode)
        End If
    End Sub

    Private Sub Game_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        keyboardTimer.Stop()
        inputKeys.Remove(e.KeyCode)
    End Sub

    Private Sub gameContext_Paint(sender As Object, e As PaintEventArgs) Handles gameContext.Paint

        gameContext_Check()
    End Sub

    Private Sub gameContext_Check()

    End Sub

    Private Sub playContext_Check()

        If playText.Enabled = False Then
            playText.Hide()
        Else
            playText.Show()
        End If

        If playTextContent.Enabled = False Then
            playTextContent.Hide()
        Else
            playTextContent.Show()
        End If

        If hintButton.Enabled = False Then
            hintButton.Hide()
        Else
            hintButton.Show()
        End If

        If checkButton.Enabled = False Then
            checkButton.Hide()
        Else
            checkButton.Show()
        End If

        If playTextInput.Enabled = False Then
            playTextInput.Hide()
        Else
            playTextInput.Show()
        End If


    End Sub

End Class

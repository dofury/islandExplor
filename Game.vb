Imports System.Diagnostics.Eventing.Reader
Imports System.Drawing.Text
Imports System.IO.Compression
Imports System.Net.Security
Imports islandExploration.SoundSystem

Public Class Game
    Dim titleImage As Image
    Dim loadingImage As Image
    Dim homeImage As Image
    Dim village_entry As Image
    Dim village_main As Image
    Dim village_alley As Image

    Dim gameItems(10) As Image
    Dim quizImages(10) As Image
    Dim quizResultImages(10) As Image

    Dim inputKeys As New ArrayList

    Dim autoCheck As Boolean = False
    Dim skipCheck As Boolean = False
    Dim loadingImageEnabled As Boolean

    Private textTimer As System.Timers.Timer
    Private textTypingTimer As System.Timers.Timer
    Private keyboardTimer As System.Timers.Timer
    Private systemTimer As System.Timers.Timer

    Private systemTimerType As String
    Private timerInterval As Integer = 50
    Private loadingCount As Integer = 0

    Dim gText As String
    Dim gTextCount As Integer

    Dim playResultText As String
    Dim playResult As Boolean

    Dim akControl As Boolean

    Dim fileSystem As New FileSystem '파일관련처리 클래스'
    Dim soundSystem As SoundSystem = New SoundSystem(Me) ' 동적할당'


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
    Private Sub Start_Init() '최초 초기화 함수
        CheckForIllegalCrossThreadCalls = False '스레드 체크 해제'

        fileSystem.LoadStory() '스토리 불러오기
        fileSystem.LoadQuiz() '퀴즈 불러오기


        font_naver.AddFontFile("font/MaruBuri-Regular.ttf") '폰트 설정
        font_naver.AddFontFile("font/MaruBuri-Bold.ttf")
        tfont_16 = New Font(font_naver.Families(0), 16)
        tfont_24 = New Font(font_naver.Families(0), 24)
        tfont_32 = New Font(font_naver.Families(0), 32)

        startButton.Location = New Point(getFormWidth() / 2 - startButton.Width / 2, getFormHeight() / 2 - 50) '버튼 위치조절'
        loadButton.Location = New Point(getFormWidth() / 2 - startButton.Width / 2, getFormHeight() / 2 + 50)
        infoButton.Location = New Point(getFormWidth() / 2 - startButton.Width / 2, getFormHeight() / 2 + 150) '버튼 위치조절'
        endButton.Location = New Point(getFormWidth() / 2 - startButton.Width / 2, getFormHeight() / 2 + 250) '버튼 위치조절'

        infoContext.Height = Me.Height
        infoContext.Location = New Point(0, 0)

        playContext.Location = New Point(0, 0)
        playContext.Width = My.Resources.playContext.Width + 200
        playContext.Height = My.Resources.playContext.Height + 200
        playContext.Location = New Point(getFormWidth() / 2 - playContext.Width / 2, getFormHeight() / 2 - playContext.Height / 2)

        playText.Width = playContext.Width
        playText.Location = New Point(0, 350)
        playTextContent.Width = playContext.Width
        playTextContent.Height = 200
        playTextContent.Location = New Point(0, 400)

        playText.Text = fileSystem.loadQuizText(fileSystem.loadQuizTextCount)
        playTextContent.Text = fileSystem.loadQuizText(fileSystem.loadQuizTextCount + 1)
        hintLabelContent.Text = fileSystem.loadQuizText(fileSystem.loadQuizTextCount + 2)
        playResultText = fileSystem.loadQuizText(fileSystem.loadQuizTextCount + 3)

        playTextInput.Width = playContext.Width / 4
        playTextInput.Location = New Point(playContext.Width / 2 - playTextInput.Width / 2, 700)

        okButton.Location = New Point(playContext.Width / 2 - okButton.Width / 2, 700)

        loadingText.Text = "로딩중"
        loadingText.Location = New Point(getFormWidth() / 2 - loadingText.Width / 2, getFormHeight() / 2 - loadingText.Height / 2)



        hintButton.Location = New Point(playTextInput.Location.X - hintButton.Width - 10, playTextInput.Location.Y - (hintButton.Height / 2 - playTextInput.Height / 2))
        checkButton.Location = New Point(playTextInput.Location.X + playTextInput.Width + 10, hintButton.Location.Y)

        quizCheck.Location = New Point(playTextInput.Location.X + (playTextInput.Width / 2 - quizCheck.Width / 2), playTextInput.Location.Y - quizCheck.Height)
        quizCheck.Text = ""

        hintContext.Location = New Point(playContext.Width / 2 - hintContext.Width / 2, playContext.Height / 2 - hintContext.Height / 2)
        hintLabel.Text = "힌트"
        hintLabel.Location = New Point(hintContext.Width / 2 - hintLabel.Width / 2, 30)
        hintLabelContent.Location = New Point(hintContext.Width / 2 - hintLabelContent.Width / 2, 100)

        gameIcon.Location = New Point(getFormWidth() - gameIcon.Width, getFormHeight() - gameIcon.Height)

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

        'gameText.Font = tfont_24
        'gameName.Font = tfont_16


        infoDraw() '게임 정보, 저작권 표시'

        soundSystem.soundCheckStart()

    End Sub
    Private Sub infoDraw()
        infoText.Text = ""
        infoText.Text += "게임 소개:" + vbCrLf
        infoText.Text += "이 게임은 퍼즐형 스토리 게임 입니다. 수수께끼의 해결을 통해 게임을 진행합니다." + vbCrLf
        infoText.Text += "조작방법:" + vbCrLf
        infoText.Text += "마우스로 진행, 키보드로 정답 입력, SPACE: 텍스트 넘기기, ENTER: 텍스트(열기/닫기)" + vbCrLf
        infoText.Text += "제작자: 차도훈(도푸리)" + vbCrLf
        infoText.Text += "저작권:" + vbCrLf
        infoText.Text += "- 퀴즈" + vbCrLf
        infoText.Text += "대부분의 퀴즈는 레이튼 교수 이상한 마을을 참고했습니다." + vbCrLf
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
    Private Sub Init() '초기화 함수
        soundSystem.BGM_Stop()
        soundSystem.SE_Stop()

        gameContext.Enabled = False
        gameIcon.Enabled = False
        gameName.Enabled = False

        startButton.Enabled = True
        infoButton.Enabled = True
        endButton.Enabled = True
        loadButton.Enabled = True


        playContext.Enabled = False
        gameContext.Enabled = False
        gameIcon.Enabled = False

        infoContext.Enabled = False
        okButton.Enabled = False
        hintContext.Enabled = False

        loadingImageEnabled = False
        loadingText.Enabled = False

        playResult = False
        akControl = False


        gameStageReset()
        gameStepReset()


        fileSystem.quizNumber = 0

        fileSystem.loadStoryTextCount = 0
        fileSystem.loadQuizTextCount = 0

        timerInterval = 0

        gameName.Text = ""
        playTextInput.Text = ""
        Invalidate()
    End Sub

    Private Sub Image_Load()
        titleImage = My.Resources.title
        loadingImage = My.Resources.loading
        homeImage = My.Resources.home

        village_entry = My.Resources.village_entry
        village_main = My.Resources.village_main
        village_alley = My.Resources.village_alley

        quizImages(0) = My.Resources.quiz1
        quizImages(1) = My.Resources.quiz2
        quizImages(2) = My.Resources.quiz3
        quizImages(3) = My.Resources.quiz4

        quizResultImages(0) = quizImages(0)
        quizResultImages(1) = My.Resources.quiz2_result
        quizResultImages(2) = quizImages(2)
        quizResultImages(3) = quizImages(3)

        gameItems(0) = My.Resources.stock_1 '주식'
        gameItems(1) = My.Resources.map_and_letter '맵과 편지'
        gameItems(2) = My.Resources.stock_2 '주식2'
        gameItems(3) = My.Resources.nabiPhoto
    End Sub
    Private Sub Game_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Select Case fileSystem.gameStage
            Case 0
                e.Graphics.DrawImage(titleImage, 0, 0, getFormWidth(), getFormHeight)
            Case 1
                e.Graphics.DrawImage(homeImage, 0, 0, getFormWidth(), getFormHeight)
                If fileSystem.gameStep = 3 Or fileSystem.gameStep = 4 Then
                    e.Graphics.DrawImage(gameItems(0), getGameItemW(0), getGameItemH(0, 0))
                    'stock
                ElseIf fileSystem.gameStep = 27 Then
                    e.Graphics.DrawImage(gameItems(1), getGameItemW(1), getGameItemH(1, 0))
                End If

            Case 2
                If fileSystem.gameStep <= 17 Then
                    e.Graphics.DrawImage(village_entry, 0, 0, getFormWidth(), getFormHeight)
                ElseIf fileSystem.gameStep >= 18 Then
                    e.Graphics.DrawImage(village_main, 0, 0, getFormWidth(), getFormHeight)
                End If
            Case 3
                If fileSystem.gameStep <= 32 Then
                    e.Graphics.DrawImage(village_alley, 0, 0, getFormWidth(), getFormHeight)
                Else
                    e.Graphics.DrawImage(village_main, 0, 0, getFormWidth(), getFormHeight)
                End If
                If fileSystem.gameStep = 14 Then
                    e.Graphics.DrawImage(gameItems(2), getGameItemW(2), getGameItemH(2, 0))
                ElseIf fileSystem.gameStep = 28 Then
                    e.Graphics.DrawImage(gameItems(3), getGameItemW(3), getGameItemH(3, 0))
                End If

        End Select
        If loadingImageEnabled = True Then
            e.Graphics.DrawImage(loadingImage, 0, 0, getFormWidth, getFormHeight)
        End If
        formContext_Check()

    End Sub

    Private Sub formContext_Check()

        If loadingText.Enabled = False Then
            loadingText.Hide()
        Else
            loadingText.Show()
        End If

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

        If infoContext.Enabled = False Then
            infoContext.Hide()
        Else
            infoContext.Show()
        End If

        If startButton.Enabled = False Then
            startButton.Hide()
        Else
            startButton.Show()
        End If

        If infoButton.Enabled = False Then
            infoButton.Hide()
        Else
            infoButton.Show()
        End If

        If loadButton.Enabled = False Then
            loadButton.Hide()
        Else
            loadButton.Show()
        End If

        If endButton.Enabled = False Then
            endButton.Hide()
        Else
            endButton.Show()
        End If
    End Sub
    Private Function getFormWidth() '폼 가로 길이의 오차를 줄여서 반환해주는 함수
        Return Me.Width - 16
    End Function

    Private Function getFormHeight() '폼 세로 길이의 오차를 줄여서 반환해주는 함수
        Return Me.Height - 39
    End Function

    Private Function getGameItemW(i As Integer)
        Dim gameItemW = getFormWidth() / 2 - gameItems(i).Width / 2
        Return CInt(gameItemW)
    End Function

    Private Function getGameItemH(i As Integer, h As Integer)
        Dim gameItemH = (getFormHeight() - gameContext.Height) / 2 - (gameItems(i).Height / 2) + h
        Return CInt(gameItemH)
    End Function
    Private Sub Stage_1()

        setPortrait(fileSystem.loadStoryText(fileSystem.loadStoryTextCount))
        Stage_1_Event()
        gText = fileSystem.loadStoryText(fileSystem.loadStoryTextCount + 1)
        Invalidate()
        textTypingTimer.Start()
    End Sub
    Private Sub Stage_1_Event()
        Select Case fileSystem.gameStep 'story.txt 위치 계산 공식 (story+1)*2
            Case 0
                gamePortrait.BackgroundImage = My.Resources.home

            Case 31
                quiz_Show()
        End Select
    End Sub

    Private Sub Stage_2()

        setPortrait(fileSystem.loadStoryText(fileSystem.loadStoryTextCount))
        Stage_2_Event()
        gText = fileSystem.loadStoryText(fileSystem.loadStoryTextCount + 1)
        Invalidate()
        textTypingTimer.Start()
    End Sub
    Private Sub Stage_2_Event()
        Select Case fileSystem.gameStep 'story.txt 위치 계산 공식 x/2 - 34
            Case 0
                gamePortrait.BackgroundImage = My.Resources.village_entry
            Case 10
                quiz_Show()
            Case 18
                soundSystem.bgmName = "village_alley"
            Case 31
                quiz_Show()
            Case 49
                quiz_Show()
        End Select
    End Sub

    Private Sub Stage_3()

        setPortrait(fileSystem.loadStoryText(fileSystem.loadStoryTextCount))
        Stage_3_Event()
        gText = fileSystem.loadStoryText(fileSystem.loadStoryTextCount + 1)
        Invalidate()
        textTypingTimer.Start()
    End Sub

    Private Sub Stage_3_Event()
        Select Case fileSystem.gameStep 'story.txt 위치 계산 공식 x/2 - 72
            Case 0
                gamePortrait.BackgroundImage = My.Resources.village_entry
            Case 11
                quiz_Show()
        End Select
    End Sub
    Private Sub Portrait(check As Boolean)
        If check = False Then
            gamePortrait.Enabled = False
            gameName.Enabled = False
        Else
            gamePortrait.Enabled = True
            gameName.Enabled = True
        End If
    End Sub

    Private Sub keyInput()
        For i = 0 To inputKeys.Count - 1
            If inputKeys(i) = Keys.Space Then
                game_akCheck_Next()
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
                gamePortrait.Image = My.Resources.youngcheol
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
                loadingImageEnabled = False
                loadingText.Enabled = False
                gameContext.Enabled = True
                Invalidate()
                systemTimer.Stop()


            End If

        End If

    End Sub
    Private Sub playResult_On()
        hintContext.Enabled = False
        playTextInput.Enabled = False
        checkButton.Enabled = False
        hintButton.Enabled = False
        okButton.Enabled = True
        playTextContent.Text = playResultText
        Invalidate()

    End Sub

    Private Sub playResult_Off()
        playResult = False
        okButton.Enabled = False
        checkButton.Enabled = True
        playTextInput.Enabled = True
        hintButton.Enabled = True
        playTextContent.Text = ""
        Invalidate()
    End Sub

    Private Sub gameContext_Click(sender As Object, e As EventArgs) Handles gameContext.Click
        TextTimer_Stop()
        gameText_Check()
        Game_Next()
    End Sub


    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        soundSystem.BGM_Stop()
        Object_MouseClick()
        akControl = True

        '게임을 시작하고 게임버튼 숨김'
        startButton.Enabled = False
        infoButton.Enabled = False
        endButton.Enabled = False
        loadButton.Enabled = False

        gameContext.Enabled = True
        gameName.Enabled = True
        gameStageNext()
        Game_Next()
        Invalidate()

        loading_Show()
    End Sub

    Private Sub gameStageNext()
        fileSystem.gameStage += 1
        soundSystem.setStage(fileSystem.gameStage)
    End Sub
    Private Sub gameStepNext()
        fileSystem.gameStep += 1
        soundSystem.setStep(fileSystem.gameStep)
    End Sub

    Private Sub gameStepReset()
        fileSystem.gameStep = 0
        soundSystem.setStep(0)
    End Sub
    Private Sub gameStageReset()
        fileSystem.gameStage = 0
        soundSystem.setStage(0)
    End Sub
    Private Sub loading_Show()

        loadingImageEnabled = True
        loadingText.Enabled = True
        gameContext.Enabled = False
        playContext.Enabled = False

        Invalidate()
        systemTimerType = "loading"
        systemTimer.Interval = 100
        systemTimer.Start()
    End Sub
    Private Sub gameText_Click(sender As Object, e As EventArgs) Handles gameText.Click
        TextTimer_Stop()
        game_akCheck_Next()
    End Sub

    Private Sub game_akCheck_Next() '키보드 사용가능 여부를 체크하고 텍스트를 넘기는 함수
        If akControl = True Then
            gameText_Check()
            Game_Next()
        End If
    End Sub

    Private Sub gameText_Check() '게임 텍스트와 실제 보여지는 텍스트가 다르면 바로보여주는 함수
        If Not gameText.Text = gText Then
            gameText.Text = gText
            textTypingTimer.Stop()
        Else
            If fileSystem.loadStoryTextCount < fileSystem.loadStoryText.Length - 2 Then
                gameStepNext()
                fileSystem.loadStoryTextCount += 2
            End If
        End If

    End Sub

    Private Sub Game_Next() '다음 게임 텍스트로 넘어가는 함수
        Object_MouseClick()
        stageUpCheck()
        soundSystem.seCheck = False
        Select Case fileSystem.gameStage
            Case 1
                Stage_1()
            Case 2
                Stage_2()
            Case 3
                Stage_3()
        End Select

    End Sub

    Private Sub stageUpCheck() '다음 스테이지 입장 조건 확인 함수
        If fileSystem.gameStage = 1 And fileSystem.gameStep = 33 Then
            gameStageNext()
            gameStepReset()
            loading_Show()
            TextTimer_Stop()
        End If
        If fileSystem.gameStage = 2 And fileSystem.gameStep = 38 Then
            gameStageNext()
            gameStepReset()
            loading_Show()
            TextTimer_Stop()
        End If
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
        Object_MouseClick()

        gameContext.Enabled = True
        gameIcon.Enabled = False
        Invalidate()
    End Sub

    Private Sub closeMenu_MouseHover(sender As Object, e As EventArgs) Handles closeMenu.MouseHover
        closeMenu.ForeColor = Color.Red
        Object_MouseHover()
    End Sub

    Private Sub closeMenu_MouseLeave(sender As Object, e As EventArgs) Handles closeMenu.MouseLeave
        closeMenu.ForeColor = Color.Black
    End Sub

    Private Sub autoMenu_Click(sender As Object, e As EventArgs) Handles autoMenu.Click
        soundSystem.SE_Stop()
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
        game_akCheck_Next()
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
        soundSystem.SE_Stop()
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
        fileSystem.setBGMName(soundSystem.bgmName)
        fileSystem.saveMenu_Click()

    End Sub

    Private Sub saveMenu_MouseHover(sender As Object, e As EventArgs) Handles saveMenu.MouseHover
        saveMenu.ForeColor = Color.Red
        Object_MouseHover()
    End Sub

    Private Sub saveMenu_MouseLeave(sender As Object, e As EventArgs) Handles saveMenu.MouseLeave
        saveMenu.ForeColor = Color.Black
    End Sub

    Private Sub loadMenu_Click(sender As Object, e As EventArgs) Handles loadMenu.Click
        If Not fileSystem.loadMenu_Click() = 0 Then
            soundSystem.SE_Stop()
            soundSystem.setStage(fileSystem.gameStage)
            soundSystem.setStep(fileSystem.gameStep)
            soundSystem.bgmName = fileSystem.getBGMName
            Object_MouseClick()
            Game_Next()
            loading_Show()
        End If
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

        startButton.Enabled = False
        infoButton.Enabled = False
        loadButton.Enabled = False
        endButton.Enabled = False
        infoContext.Enabled = True

        Invalidate()
    End Sub

    Private Sub infoXButton_Click(sender As Object, e As EventArgs) Handles infoXButton.Click
        Object_MouseClick()

        startButton.Enabled = True
        infoButton.Enabled = True
        endButton.Enabled = True
        infoContext.Enabled = False

        Invalidate()
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
        Dim quizImageX As Integer = (playContext.Width / 2) - (quizImages(fileSystem.quizNumber).Width / 2) - (quizImages(fileSystem.quizNumber).Width / 6)
        Dim quizResultImageX As Integer = (playContext.Width / 2) - (quizResultImages(fileSystem.quizNumber).Width / 2) - (quizResultImages(fileSystem.quizNumber).Width / 6)
        If playResult = False Then
            e.Graphics.DrawImage(quizImages(fileSystem.quizNumber), quizImageX, 50)
        Else
            e.Graphics.DrawImage(quizResultImages(fileSystem.quizNumber), quizResultImageX, 50)
        End If
        playContext_Check()
    End Sub

    Private Sub checkButton_Click(sender As Object, e As EventArgs) Handles checkButton.Click '정답 버튼'
        Select Case fileSystem.quizNumber
            Case 0
                Try
                    If playTextInput.Text = 0 Then
                        quiz_Correct()
                        playTextInput.Text = ""
                    Else
                        Throw New System.Exception("오답")
                    End If
                Catch ex As Exception
                    quiz_Incorrect()
                End Try
            Case 1
                Try
                    If playTextInput.Text = 4 Then
                        quiz_Correct()
                        playTextInput.Text = ""
                    Else
                        Throw New System.Exception("오답")
                    End If
                Catch ex As Exception
                    quiz_Incorrect()
                End Try
            Case 2
                Try
                    If playTextInput.Text = 50 Then
                        quiz_Correct()
                        playTextInput.Text = ""
                    Else
                        Throw New System.Exception("오답")
                    End If
                Catch ex As Exception
                    quiz_Incorrect()
                End Try
            Case 3
                Try
                    If playTextInput.Text = 3 Then
                        quiz_Correct()
                        playTextInput.Text = ""
                    Else
                        Throw New System.Exception("오답")
                    End If
                Catch ex As Exception
                    quiz_Incorrect()
                End Try
        End Select

    End Sub
    Private Sub quiz_Correct()
        soundSystem.Play("correct")
        quizCheck.ForeColor = Color.Green
        quizCheck.Text = "정답"
        playResult = True
        systemTimerType = "quizCheck"
        systemTimer.Interval = 1500
        systemTimer.Start()
    End Sub
    Private Sub quiz_Incorrect()
        soundSystem.Play("incorrect")
        quizCheck.ForeColor = Color.Red
        quizCheck.Text = "오답"
        systemTimerType = "quizCheck"
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
        hintContext.Enabled = True
        Invalidate()
    End Sub

    Private Sub checkButton_MouseHover(sender As Object, e As EventArgs) Handles checkButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub hintButton_MouseHover(sender As Object, e As EventArgs) Handles hintButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub Object_MouseHover()
        soundSystem.Play("pop")
    End Sub

    Private Sub Object_MouseClick()
        soundSystem.Play("tick")
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
        hintContext.Enabled = False
        Invalidate()
    End Sub

    Private Sub hintXButton_MouseHover(sender As Object, e As EventArgs) Handles hintXButton.MouseHover
        Object_MouseHover()
    End Sub

    Private Sub quiz_Show()
        TextTimer_Stop() 'skip and auto 일시 퀴즈 전에 멈춤
        soundSystem.soundCheckStop() '타이머 정지'
        akControl = False 'skip auto 사용불가
        gameContext.Enabled = False
        gameIcon.Enabled = False
        Invalidate()
        playContext.Enabled = True
        Invalidate()
        playText.Text = fileSystem.loadQuizText(fileSystem.loadQuizTextCount)
        playTextContent.Text = fileSystem.loadQuizText(fileSystem.loadQuizTextCount + 1)
        hintLabelContent.Text = fileSystem.loadQuizText(fileSystem.loadQuizTextCount + 2)
        playResultText = fileSystem.loadQuizText(fileSystem.loadQuizTextCount + 3)
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
        soundSystem.soundCheckStart()
        gameContext.Enabled = True
        playContext.Enabled = False
        akControl = True
        Invalidate()
        fileSystem.loadQuizTextCount += 4
        fileSystem.quizNumber += 1
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
        If gameName.Enabled = False Then
            gameName.Hide()
        Else
            gameName.Show()
        End If

        If gamePortrait.Enabled = False Then
            gamePortrait.Hide()
        Else
            gamePortrait.Show()
        End If

        If gameText.Enabled = False Then
            gameText.Hide()
        Else
            gameText.Show()
        End If

        If saveMenu.Enabled = False Then
            saveMenu.Hide()
        Else
            saveMenu.Show()
        End If

        If loadMenu.Enabled = False Then
            loadMenu.Hide()
        Else
            loadMenu.Show()
        End If

        If skipMenu.Enabled = False Then
            skipMenu.Hide()
        Else
            skipMenu.Show()
        End If

        If autoMenu.Enabled = False Then
            autoMenu.Hide()
        Else
            autoMenu.Show()
        End If

        If closeMenu.Enabled = False Then
            closeMenu.Hide()
        Else
            closeMenu.Show()
        End If

        If titleMenu.Enabled = False Then
            titleMenu.Hide()
        Else
            titleMenu.Show()
        End If

        If quitMenu.Enabled = False Then
            quitMenu.Hide()
        Else
            quitMenu.Show()
        End If
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

        If hintContext.Enabled = False Then
            hintContext.Hide()
        Else
            hintContext.Show()
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

        If okButton.Enabled = False Then
            okButton.Hide()
        Else
            okButton.Show()
        End If

    End Sub

    Private Sub loadingContext_Paint(sender As Object, e As PaintEventArgs)
        If loadingText.Enabled = False Then
            loadingText.Hide()
        Else
            loadingText.Show()
        End If
    End Sub

    Private Sub infoContext_Paint(sender As Object, e As PaintEventArgs) Handles infoContext.Paint
        If infoText.Enabled = False Then
            infoText.Hide()
        Else
            infoText.Show()
        End If
    End Sub

    Private Sub loadButton_Click(sender As Object, e As EventArgs) Handles loadButton.Click

        If Not fileSystem.loadMenu_Click() = 0 Then
            soundSystem.BGM_Stop()
            soundSystem.SE_Stop()
            Object_MouseClick()
            soundSystem.setStage(fileSystem.gameStage)
            soundSystem.setStep(fileSystem.gameStep)
            soundSystem.bgmName = fileSystem.getBGMName
            startButton.Enabled = False
            infoButton.Enabled = False
            loadButton.Enabled = False
            endButton.Enabled = False
            gameContext.Enabled = True
            gameName.Enabled = True
            playContext.Enabled = False
            akControl = True
            Invalidate()
            Game_Next()
            loading_Show()
        End If
    End Sub

    Public Sub setBGMName(name As String)
        soundSystem.bgmName = fileSystem.getBGMName
    End Sub
End Class

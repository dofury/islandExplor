﻿Imports System.Drawing.Text
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
    Dim loadTexts As String
    Dim loadText() As String
    Dim loadTextCount As Integer
    Dim gameSound As New GameSounds
    Public font_naver As PrivateFontCollection = New PrivateFontCollection()
    Dim tfont_24 As Font
    Dim tfont_16 As Font
    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Start_Init()
        Init()


    End Sub
    Private Sub Start_Init()

        CheckForIllegalCrossThreadCalls = False '스레드 체크 해제'
        LoadStory()
        startButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 50) '버튼 위치조절'
        infoButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 150) '버튼 위치조절'
        endButton.Location = New Point(Me.Size.Width / 2 - (startButton.Size.Width / 2), Me.Size.Height / 2 + 250) '버튼 위치조절'
        infoContext.Height = Me.Height
        infoContext.Location = New Point(0, 0)
        playContext.Height = Me.Height / 5 * 4
        playContext.Width = Me.Width / 5 * 4
        playContext.Location = New Point(Me.Width / 10, Me.Height / 10)
        playText.Location += New Point(0, 100)
        playText.Width = playContext.Width
        playTextInput.Width = playContext.Width / 4
        playTextInput.Location = New Point(playContext.Width / 2 - playTextInput.Width / 2, 500)
        playText.Text = "동물원에는 여러 동물이 있다." + vbCrLf +
"그 중 뱀, 토끼, 늑대는 같은 우리에서 살고 있다.
뱀은 10마리가 있고 3일마다 한마리 씩 늘어난다, 
토끼는 5마리가 있고 늑대는 2마리가 있다.
매일 늑대가 두 마리의 토끼를 죽이고 
새로 다섯 마리의 토끼가 태어난다고 한다.
30일후에 모든 동물의 다리 갯수는 몇개일까?
"
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
        loadTextCount = 0
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

    Private Sub LoadStory() '스토리 원고 로드 함수'
        loadTexts = My.Computer.FileSystem.ReadAllText("story.txt")
        loadText = loadTexts.Split(vbCrLf)
    End Sub

    Private Sub SE_Stop()
        gameSound.Stop("tick")
        gameSound.Stop("typing")
        gameSound.Stop("blackCow")
        gameSound.Stop("vibrate")
        gameSound.Stop("doorBell")
        gameSound.Stop("writePen")
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
        gameSound.AddSound("doorBell", "sound/Door Bell Sound.mp3")
        gameSound.AddSound("writePen", "sound/Pencil Write Kor.wav")
        gameSound.AddSound("phoneEnd", "sound/Phone Dialing With Dialtone Sound.wav")
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

        setPortrait(loadText(loadTextCount))
        Story_1_Event()
        gText = loadText(loadTextCount + 1)
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
            Case 31
                playContext.Show()
                gameContext.Hide()
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
            Me.Invoke(New deleTextBox(AddressOf allocTextBox), gText) '크로스쓰레드 문제 해결
        Catch ex As Exception

        End Try
    End Sub
    Delegate Sub delePictureBox(ByVal image As PictureBox) '델리게이트 선언
    Private Sub allocPictureBox(ByVal image As PictureBox)

    End Sub

    Delegate Sub deleTextBox(ByVal text As String) '델리게이트 선언
    Private Sub allocTextBox(ByVal text As String) '델리게이트 함수선언
        If gameText.Text.Length = text.Length Then
            gTextCount = 0
            textTypingTimer.Stop()
            Exit Sub
        End If

        If gTextCount < text.Length Then
            gTextCount += 1
        End If
        gameText.Text = text.Substring(0, gTextCount)
    End Sub

    Private Sub gameContext_Click(sender As Object, e As EventArgs) Handles gameContext.Click
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
        gameText_Check()
        Game_Next()
    End Sub

    Private Sub gameText_Check()
        If Not gameText.Text = gText Then '게임 텍스트와 실제 보여지는 텍스트가 다르면 바로보여주는 함수
            gameText.Text = gText
        Else
            If loadTextCount < (loadText.Length - 2) Then
                story += 1
                loadTextCount += 2
            End If
        End If

    End Sub

    Private Sub Game_Next() '다음 게임 텍스트로 넘어가는 함수
        'SE_Stop()
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
        loadTextCount += 2
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
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", loadTextCount, True)
    End Sub

    Private Sub saveMenu_MouseHover(sender As Object, e As EventArgs) Handles saveMenu.MouseHover
        saveMenu.ForeColor = Color.Red
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
        text_AutoSize(sender)
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If playTextInput.Text = "" Then
            playContext.Hide()
            gameContext.Show()
        End If
    End Sub
End Class

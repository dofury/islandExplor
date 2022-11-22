Imports System.Reflection
Imports islandExploration.Game

Public Class SoundSystem
    Private soundCheckTimer As System.Timers.Timer
    Dim gameSound As New GameSounds
    Dim game As Game
    Private timerInterval As Integer = 500

    Public seCheck As Boolean

    Private bgmName As String
    Private seName As String
    Private gameStage As Integer = 0
    Private gameStep As Integer = 0
    Public Sub New(game As Game)
        Me.game = game
        seCheck = False
        Sound_Load()
        soundCheckTimer = New Timers.Timer(timerInterval)
        soundCheckTimer.AutoReset = True
        AddHandler soundCheckTimer.Elapsed, AddressOf soundCheck
    End Sub

    Private Sub Sound_Load()
        gameSound.AddSound("title", "sound/Adventure-Starting.wav")
        gameSound.AddSound("tick", "sound/Tick Sound.mp3")
        gameSound.AddSound("typing", "sound/TypeWriter.mp3")
        gameSound.AddSound("house", "sound/HOW ARE YOU.mp3")
        gameSound.AddSound("blackCow", "sound/Single Cow Sound.mp3")
        gameSound.AddSound("vibrate", "sound/Phone Vibrating Sound.mp3")
        gameSound.AddSound("doorBell", "sound/Door Bell Sound.mp3")
        gameSound.AddSound("writePen", "sound/Pencil Write Kor.wav")
        gameSound.AddSound("phoneEnd", "sound/Phone Dialing With Dialtone Sound.wav")
        gameSound.AddSound("correct", "sound/Correct.wav")
        gameSound.AddSound("incorrect", "sound/Error.mp3")
        gameSound.AddSound("pop", "sound/Blop Sound.mp3")
        gameSound.AddSound("village", "sound/Tongtong.mp3")
        gameSound.SetVolume("house", 80)
        gameSound.SetVolume("title", 80)
    End Sub

    Delegate Sub playSound() '델리게이트 선언
    Public Sub allocSound() '델리게이트 함수선언
        GetSoundName()
        BGM_Play()
        SE_Play()
    End Sub

    Public Sub soundCheck() '주기적으로 현재 상황에 맞는 사운드를 재생하는 함수
        game.Invoke(New playSound(AddressOf allocSound))
    End Sub
    Public Sub soundCheckStart() '주기적으로 현재 상황에 맞는 사운드를 재생하는 함수 실행
        soundCheckTimer.Start()
    End Sub
    Public Sub soundCheckStop() '주기적으로 현재 상황에 맞는 사운드를 재생하는 함수 중지
        soundCheckTimer.Stop()
    End Sub

    Public Sub setStage(gameStage As Integer)
        Me.gameStage = gameStage
    End Sub
    Public Sub setStep(gamestep As Integer)
        Me.gameStep = gamestep
    End Sub

    Public Function getStage()
        Return Me.gameStage
    End Function
    Public Function getStep()
        Return Me.gameStep
    End Function
    Private Sub GetSoundName()
        Select Case gameStage
            Case 0
                bgmName = "title"
                seName = ""
            Case 1
                bgmName = "house"
                GetStage1SoundName()
            Case 2
                bgmName = "village"
                GetStage2SoundName()
        End Select
    End Sub

    Private Sub GetStage1SoundName()
        Select Case gameStep
            Case 1
                seName = "typing"
            Case 3
                seName = "blackCow"
            Case 5
                seName = "vibrate"
            Case 16
                seName = "phoneEnd"
            Case 19
                seName = "doorBell"
            Case 24
                seName = "writePen"
            Case Else
                seName = ""
                SE_Stop()
        End Select
    End Sub

    Public Sub Play(name As String)
        If gameSound.IsPlaying(name) = False Then
            gameSound.Play(name)
        End If
    End Sub

    Private Sub BGM_Play()
        If gameSound.IsPlaying(bgmName) = False And Not bgmName = "" Then
            gameSound.Play(bgmName)
        End If
    End Sub

    Private Sub SE_Play()
        If gameSound.IsPlaying(seName) = False And Not seName = "" And seCheck = False Then
            SE_Stop()
            gameSound.Play(seName)
            seCheck = True
        End If
    End Sub
    Public Sub SE_Stop()
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

    Public Sub BGM_Stop()

        gameSound.Stop("title")
        gameSound.Stop("house")
        gameSound.Stop("village")

    End Sub

    Private Sub GetStage2SoundName()
        Select Case gameStep 'story.txt 위치 계산 공식 x/2 - 34

        End Select
    End Sub

End Class

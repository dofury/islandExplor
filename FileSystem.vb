Imports System.Windows.Forms.AxHost

Public Class FileSystem

    Public gameStage As Integer
    Public gameStep As Integer
    Public quizNumber As Integer

    Dim loadSaveTexts As String
    Dim loadSaveText() As String

    Dim loadInfoTexts As String
    Public loadInfoText() As String

    Dim bgmName As String

    Public loadStoryTexts As String
    Public loadStoryText() As String
    Public loadStoryTextCount As Integer


    Public loadQuizTexts As String
    Public loadQuizText() As String
    Public loadQuizTextCount As Integer


    Public Sub setBGMName(name As String)
        Me.bgmName = name
    End Sub

    Public Function getBGMName()
        Return Me.bgmName
    End Function

    Public Sub LoadStory() '스토리 대본을 불러오는 함수
        loadStoryTexts = My.Computer.FileSystem.ReadAllText("story.txt")
        loadStoryText = loadStoryTexts.Split(vbCrLf) '토큰기준 분리
    End Sub


    Public Sub LoadQuiz() '퀴즈 대본을 불러오는 함수
        loadQuizTexts = My.Computer.FileSystem.ReadAllText("quiz.txt")
        loadQuizText = loadQuizTexts.Split("/") '토큰기준 분리
    End Sub

    Public Sub loadInfo()
        loadInfoTexts = My.Computer.FileSystem.ReadAllText("info.txt")
        loadInfoText = loadInfoTexts.Split(vbCrLf) '토큰기준 분리
    End Sub
    Public Sub saveMenu_Click()
        Dim token As String = "/".ToString
        My.Computer.FileSystem.WriteAllText("save.txt", gameStage, False)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", gameStep, True)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", loadStoryTextCount, True)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", quizNumber, True)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", loadQuizTextCount, True)
        My.Computer.FileSystem.WriteAllText("save.txt", token, True)
        My.Computer.FileSystem.WriteAllText("save.txt", bgmName, True)
    End Sub

    Public Function loadMenu_Click()
        Try
            loadSaveTexts = My.Computer.FileSystem.ReadAllText("save.txt")
            loadSaveText = loadSaveTexts.Split("/")

            gameStage = loadSaveText(0)
            gameStep = loadSaveText(1)
            loadStoryTextCount = loadSaveText(2)
            quizNumber = loadSaveText(3)
            loadQuizTextCount = loadSaveText(4)
            bgmName = loadSaveText(5)

        Catch ex As Exception
            MsgBox("불러올 저장 파일이 없습니다", MsgBoxStyle.Critical, "경고")
            Return 0
        End Try
        Return 1

    End Function
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Game
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Game))
        Me.gameText = New System.Windows.Forms.Label()
        Me.gameContext = New System.Windows.Forms.Panel()
        Me.loadMenu = New System.Windows.Forms.Label()
        Me.saveMenu = New System.Windows.Forms.Label()
        Me.skipMenu = New System.Windows.Forms.Label()
        Me.autoMenu = New System.Windows.Forms.Label()
        Me.closeMenu = New System.Windows.Forms.Label()
        Me.quitMenu = New System.Windows.Forms.Label()
        Me.titleMenu = New System.Windows.Forms.Label()
        Me.gameName = New System.Windows.Forms.Label()
        Me.gamePortrait = New System.Windows.Forms.PictureBox()
        Me.gameIcon = New System.Windows.Forms.PictureBox()
        Me.startButton = New System.Windows.Forms.PictureBox()
        Me.gameContext.SuspendLayout()
        CType(Me.gamePortrait, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gameIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.startButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gameText
        '
        Me.gameText.AutoSize = True
        Me.gameText.BackColor = System.Drawing.Color.Transparent
        Me.gameText.Font = New System.Drawing.Font("한컴 말랑말랑 Regular", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.gameText.Location = New System.Drawing.Point(225, 89)
        Me.gameText.Margin = New System.Windows.Forms.Padding(0)
        Me.gameText.Name = "gameText"
        Me.gameText.Size = New System.Drawing.Size(102, 41)
        Me.gameText.TabIndex = 1
        Me.gameText.Text = "메세지"
        '
        'gameContext
        '
        Me.gameContext.BackColor = System.Drawing.Color.Transparent
        Me.gameContext.BackgroundImage = Global.islandExploration.My.Resources.Resources.context
        Me.gameContext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gameContext.Controls.Add(Me.loadMenu)
        Me.gameContext.Controls.Add(Me.saveMenu)
        Me.gameContext.Controls.Add(Me.skipMenu)
        Me.gameContext.Controls.Add(Me.autoMenu)
        Me.gameContext.Controls.Add(Me.closeMenu)
        Me.gameContext.Controls.Add(Me.quitMenu)
        Me.gameContext.Controls.Add(Me.titleMenu)
        Me.gameContext.Controls.Add(Me.gameName)
        Me.gameContext.Controls.Add(Me.gamePortrait)
        Me.gameContext.Controls.Add(Me.gameText)
        Me.gameContext.Location = New System.Drawing.Point(1, 758)
        Me.gameContext.Name = "gameContext"
        Me.gameContext.Size = New System.Drawing.Size(1263, 226)
        Me.gameContext.TabIndex = 2
        '
        'loadMenu
        '
        Me.loadMenu.AutoSize = True
        Me.loadMenu.Font = New System.Drawing.Font("한컴 말랑말랑 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.loadMenu.Location = New System.Drawing.Point(796, 194)
        Me.loadMenu.Name = "loadMenu"
        Me.loadMenu.Size = New System.Drawing.Size(81, 25)
        Me.loadMenu.TabIndex = 10
        Me.loadMenu.Text = "[LOAD]"
        '
        'saveMenu
        '
        Me.saveMenu.AutoSize = True
        Me.saveMenu.Font = New System.Drawing.Font("한컴 말랑말랑 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.saveMenu.Location = New System.Drawing.Point(727, 194)
        Me.saveMenu.Name = "saveMenu"
        Me.saveMenu.Size = New System.Drawing.Size(74, 25)
        Me.saveMenu.TabIndex = 9
        Me.saveMenu.Text = "[SAVE]"
        '
        'skipMenu
        '
        Me.skipMenu.AutoSize = True
        Me.skipMenu.Font = New System.Drawing.Font("한컴 말랑말랑 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.skipMenu.Location = New System.Drawing.Point(874, 194)
        Me.skipMenu.Name = "skipMenu"
        Me.skipMenu.Size = New System.Drawing.Size(68, 25)
        Me.skipMenu.TabIndex = 8
        Me.skipMenu.Text = "[SKIP]"
        '
        'autoMenu
        '
        Me.autoMenu.AutoSize = True
        Me.autoMenu.Font = New System.Drawing.Font("한컴 말랑말랑 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.autoMenu.Location = New System.Drawing.Point(936, 194)
        Me.autoMenu.Name = "autoMenu"
        Me.autoMenu.Size = New System.Drawing.Size(80, 25)
        Me.autoMenu.TabIndex = 7
        Me.autoMenu.Text = "[AUTO]"
        '
        'closeMenu
        '
        Me.closeMenu.AutoSize = True
        Me.closeMenu.Font = New System.Drawing.Font("한컴 말랑말랑 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.closeMenu.Location = New System.Drawing.Point(1012, 194)
        Me.closeMenu.Name = "closeMenu"
        Me.closeMenu.Size = New System.Drawing.Size(87, 25)
        Me.closeMenu.TabIndex = 6
        Me.closeMenu.Text = "[CLOSE]"
        '
        'quitMenu
        '
        Me.quitMenu.AutoSize = True
        Me.quitMenu.Font = New System.Drawing.Font("한컴 말랑말랑 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.quitMenu.Location = New System.Drawing.Point(1166, 194)
        Me.quitMenu.Name = "quitMenu"
        Me.quitMenu.Size = New System.Drawing.Size(74, 25)
        Me.quitMenu.TabIndex = 5
        Me.quitMenu.Text = "[QUIT]"
        '
        'titleMenu
        '
        Me.titleMenu.AutoSize = True
        Me.titleMenu.Font = New System.Drawing.Font("한컴 말랑말랑 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.titleMenu.Location = New System.Drawing.Point(1095, 194)
        Me.titleMenu.Name = "titleMenu"
        Me.titleMenu.Size = New System.Drawing.Size(75, 25)
        Me.titleMenu.TabIndex = 4
        Me.titleMenu.Text = "[TITLE]"
        '
        'gameName
        '
        Me.gameName.BackColor = System.Drawing.Color.Transparent
        Me.gameName.Font = New System.Drawing.Font("한컴 말랑말랑 Bold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.gameName.Location = New System.Drawing.Point(36, 176)
        Me.gameName.Margin = New System.Windows.Forms.Padding(0)
        Me.gameName.Name = "gameName"
        Me.gameName.Size = New System.Drawing.Size(150, 43)
        Me.gameName.TabIndex = 3
        Me.gameName.Text = "이름"
        Me.gameName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gamePortrait
        '
        Me.gamePortrait.Image = CType(resources.GetObject("gamePortrait.Image"), System.Drawing.Image)
        Me.gamePortrait.Location = New System.Drawing.Point(49, 23)
        Me.gamePortrait.Name = "gamePortrait"
        Me.gamePortrait.Size = New System.Drawing.Size(122, 150)
        Me.gamePortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.gamePortrait.TabIndex = 2
        Me.gamePortrait.TabStop = False
        '
        'gameIcon
        '
        Me.gameIcon.BackColor = System.Drawing.Color.Transparent
        Me.gameIcon.Image = Global.islandExploration.My.Resources.Resources.icon
        Me.gameIcon.Location = New System.Drawing.Point(1210, 698)
        Me.gameIcon.Name = "gameIcon"
        Me.gameIcon.Size = New System.Drawing.Size(54, 54)
        Me.gameIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.gameIcon.TabIndex = 6
        Me.gameIcon.TabStop = False
        Me.gameIcon.Visible = False
        '
        'startButton
        '
        Me.startButton.BackColor = System.Drawing.Color.Transparent
        Me.startButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.startButton.Image = Global.islandExploration.My.Resources.Resources.startButton
        Me.startButton.Location = New System.Drawing.Point(497, 458)
        Me.startButton.Name = "startButton"
        Me.startButton.Size = New System.Drawing.Size(270, 69)
        Me.startButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.startButton.TabIndex = 3
        Me.startButton.TabStop = False
        '
        'Game
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(1264, 985)
        Me.Controls.Add(Me.startButton)
        Me.Controls.Add(Me.gameContext)
        Me.Controls.Add(Me.gameIcon)
        Me.DoubleBuffered = True
        Me.Name = "Game"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game"
        Me.gameContext.ResumeLayout(False)
        Me.gameContext.PerformLayout()
        CType(Me.gamePortrait, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gameIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.startButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gameText As Label
    Friend WithEvents gameContext As Panel
    Friend WithEvents gameName As Label
    Friend WithEvents gamePortrait As PictureBox
    Friend WithEvents startButton As PictureBox
    Friend WithEvents titleMenu As Label
    Friend WithEvents quitMenu As Label
    Friend WithEvents closeMenu As Label
    Friend WithEvents gameIcon As PictureBox
    Friend WithEvents autoMenu As Label
    Friend WithEvents skipMenu As Label
    Friend WithEvents saveMenu As Label
    Friend WithEvents loadMenu As Label
End Class

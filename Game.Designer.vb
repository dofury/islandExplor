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
        Me.infoButton = New System.Windows.Forms.PictureBox()
        Me.endButton = New System.Windows.Forms.PictureBox()
        Me.infoContext = New System.Windows.Forms.Panel()
        Me.infoText = New System.Windows.Forms.Label()
        Me.xButton = New System.Windows.Forms.PictureBox()
        Me.playContext = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.playTextInput = New System.Windows.Forms.TextBox()
        Me.playText = New System.Windows.Forms.Label()
        Me.gameContext.SuspendLayout()
        CType(Me.gamePortrait, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gameIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.startButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.infoButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.endButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.infoContext.SuspendLayout()
        CType(Me.xButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.playContext.SuspendLayout()
        Me.SuspendLayout()
        '
        'gameText
        '
        Me.gameText.BackColor = System.Drawing.Color.Transparent
        Me.gameText.Font = New System.Drawing.Font("한컴 말랑말랑 Regular", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.gameText.Location = New System.Drawing.Point(223, 57)
        Me.gameText.Margin = New System.Windows.Forms.Padding(0)
        Me.gameText.Name = "gameText"
        Me.gameText.Size = New System.Drawing.Size(947, 132)
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
        Me.gameContext.Location = New System.Drawing.Point(1, 735)
        Me.gameContext.Name = "gameContext"
        Me.gameContext.Size = New System.Drawing.Size(1263, 250)
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
        Me.gameName.Location = New System.Drawing.Point(37, 187)
        Me.gameName.Margin = New System.Windows.Forms.Padding(0)
        Me.gameName.Name = "gameName"
        Me.gameName.Size = New System.Drawing.Size(122, 43)
        Me.gameName.TabIndex = 3
        Me.gameName.Text = "이름"
        Me.gameName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gamePortrait
        '
        Me.gamePortrait.BackgroundImage = Global.islandExploration.My.Resources.Resources.title
        Me.gamePortrait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gamePortrait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.gamePortrait.Image = CType(resources.GetObject("gamePortrait.Image"), System.Drawing.Image)
        Me.gamePortrait.Location = New System.Drawing.Point(37, 39)
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
        Me.gameIcon.Location = New System.Drawing.Point(1210, 677)
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
        Me.startButton.Location = New System.Drawing.Point(498, 383)
        Me.startButton.Name = "startButton"
        Me.startButton.Size = New System.Drawing.Size(270, 69)
        Me.startButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.startButton.TabIndex = 3
        Me.startButton.TabStop = False
        '
        'infoButton
        '
        Me.infoButton.BackColor = System.Drawing.Color.Transparent
        Me.infoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.infoButton.Image = Global.islandExploration.My.Resources.Resources.infoButton
        Me.infoButton.Location = New System.Drawing.Point(497, 458)
        Me.infoButton.Name = "infoButton"
        Me.infoButton.Size = New System.Drawing.Size(270, 69)
        Me.infoButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.infoButton.TabIndex = 7
        Me.infoButton.TabStop = False
        '
        'endButton
        '
        Me.endButton.BackColor = System.Drawing.Color.Transparent
        Me.endButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.endButton.Image = Global.islandExploration.My.Resources.Resources.endButton
        Me.endButton.Location = New System.Drawing.Point(498, 533)
        Me.endButton.Name = "endButton"
        Me.endButton.Size = New System.Drawing.Size(270, 69)
        Me.endButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.endButton.TabIndex = 8
        Me.endButton.TabStop = False
        '
        'infoContext
        '
        Me.infoContext.BackColor = System.Drawing.Color.Transparent
        Me.infoContext.BackgroundImage = Global.islandExploration.My.Resources.Resources.context
        Me.infoContext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.infoContext.Controls.Add(Me.infoText)
        Me.infoContext.Controls.Add(Me.xButton)
        Me.infoContext.Location = New System.Drawing.Point(1, 2)
        Me.infoContext.Name = "infoContext"
        Me.infoContext.Size = New System.Drawing.Size(1263, 131)
        Me.infoContext.TabIndex = 9
        Me.infoContext.Visible = False
        '
        'infoText
        '
        Me.infoText.AutoSize = True
        Me.infoText.Location = New System.Drawing.Point(37, 27)
        Me.infoText.Name = "infoText"
        Me.infoText.Size = New System.Drawing.Size(43, 15)
        Me.infoText.TabIndex = 1
        Me.infoText.Text = "메세지"
        '
        'xButton
        '
        Me.xButton.Image = Global.islandExploration.My.Resources.Resources.xButton
        Me.xButton.Location = New System.Drawing.Point(1202, 0)
        Me.xButton.Name = "xButton"
        Me.xButton.Size = New System.Drawing.Size(61, 61)
        Me.xButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.xButton.TabIndex = 0
        Me.xButton.TabStop = False
        '
        'playContext
        '
        Me.playContext.BackColor = System.Drawing.Color.Transparent
        Me.playContext.BackgroundImage = Global.islandExploration.My.Resources.Resources.context
        Me.playContext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.playContext.Controls.Add(Me.Button1)
        Me.playContext.Controls.Add(Me.playTextInput)
        Me.playContext.Controls.Add(Me.playText)
        Me.playContext.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.playContext.Location = New System.Drawing.Point(1, 139)
        Me.playContext.Name = "playContext"
        Me.playContext.Size = New System.Drawing.Size(1263, 147)
        Me.playContext.TabIndex = 10
        Me.playContext.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(418, 79)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'playTextInput
        '
        Me.playTextInput.Font = New System.Drawing.Font("맑은 고딕", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.playTextInput.Location = New System.Drawing.Point(157, 58)
        Me.playTextInput.Name = "playTextInput"
        Me.playTextInput.Size = New System.Drawing.Size(100, 39)
        Me.playTextInput.TabIndex = 12
        Me.playTextInput.Text = "메세지"
        Me.playTextInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'playText
        '
        Me.playText.BackColor = System.Drawing.Color.Transparent
        Me.playText.Font = New System.Drawing.Font("한컴 말랑말랑 Regular", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.playText.Location = New System.Drawing.Point(8, 54)
        Me.playText.Margin = New System.Windows.Forms.Padding(0)
        Me.playText.Name = "playText"
        Me.playText.Size = New System.Drawing.Size(102, 41)
        Me.playText.TabIndex = 11
        Me.playText.Text = "메세지"
        Me.playText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Game
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(1264, 985)
        Me.Controls.Add(Me.playContext)
        Me.Controls.Add(Me.endButton)
        Me.Controls.Add(Me.infoButton)
        Me.Controls.Add(Me.startButton)
        Me.Controls.Add(Me.gameContext)
        Me.Controls.Add(Me.gameIcon)
        Me.Controls.Add(Me.infoContext)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Game"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game"
        Me.gameContext.ResumeLayout(False)
        Me.gameContext.PerformLayout()
        CType(Me.gamePortrait, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gameIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.startButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.infoButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.endButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.infoContext.ResumeLayout(False)
        Me.infoContext.PerformLayout()
        CType(Me.xButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.playContext.ResumeLayout(False)
        Me.playContext.PerformLayout()
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
    Friend WithEvents infoButton As PictureBox
    Friend WithEvents endButton As PictureBox
    Friend WithEvents infoContext As Panel
    Friend WithEvents xButton As PictureBox
    Friend WithEvents infoText As Label
    Friend WithEvents playContext As Panel
    Friend WithEvents playText As Label
    Friend WithEvents playTextInput As TextBox
    Friend WithEvents Button1 As Button
End Class

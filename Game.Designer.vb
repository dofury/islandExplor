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
        Me.gameName = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.gameContext.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gameText
        '
        Me.gameText.AutoSize = True
        Me.gameText.BackColor = System.Drawing.Color.Transparent
        Me.gameText.Font = New System.Drawing.Font("함초롬돋움 확장", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
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
        Me.gameContext.Controls.Add(Me.gameName)
        Me.gameContext.Controls.Add(Me.PictureBox1)
        Me.gameContext.Controls.Add(Me.gameText)
        Me.gameContext.Location = New System.Drawing.Point(1, 758)
        Me.gameContext.Name = "gameContext"
        Me.gameContext.Size = New System.Drawing.Size(1263, 226)
        Me.gameContext.TabIndex = 2
        '
        'gameName
        '
        Me.gameName.BackColor = System.Drawing.Color.Transparent
        Me.gameName.Font = New System.Drawing.Font("은 돋움", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.gameName.Location = New System.Drawing.Point(36, 176)
        Me.gameName.Margin = New System.Windows.Forms.Padding(0)
        Me.gameName.Name = "gameName"
        Me.gameName.Size = New System.Drawing.Size(150, 43)
        Me.gameName.TabIndex = 3
        Me.gameName.Text = "김춘배(냥인 3세)"
        Me.gameName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(36, 23)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(150, 150)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Game
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(1264, 985)
        Me.Controls.Add(Me.gameContext)
        Me.DoubleBuffered = True
        Me.Name = "Game"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game"
        Me.gameContext.ResumeLayout(False)
        Me.gameContext.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gameText As Label
    Friend WithEvents gameContext As Panel
    Friend WithEvents gameName As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Title
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
        Me.startButton = New System.Windows.Forms.PictureBox()
        CType(Me.startButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'startButton
        '
        Me.startButton.BackColor = System.Drawing.Color.Transparent
        Me.startButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.startButton.Image = Global.islandExploration.My.Resources.Resources.startButton
        Me.startButton.Location = New System.Drawing.Point(12, 12)
        Me.startButton.Name = "startButton"
        Me.startButton.Size = New System.Drawing.Size(270, 69)
        Me.startButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.startButton.TabIndex = 1
        Me.startButton.TabStop = False
        '
        'Title
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1264, 985)
        Me.Controls.Add(Me.startButton)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Title"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Title"
        CType(Me.startButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents startButton As PictureBox
End Class

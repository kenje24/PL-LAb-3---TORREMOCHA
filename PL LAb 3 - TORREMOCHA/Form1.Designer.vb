<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Button1 = New Button()
        rtbOutput = New RichTextBox()
        dgvFirstFollow = New DataGridView()
        CType(dgvFirstFollow, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(12, 213)
        Button1.Name = "Button1"
        Button1.Size = New Size(94, 51)
        Button1.TabIndex = 0
        Button1.Text = "SIMULATE"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' rtbOutput
        ' 
        rtbOutput.Location = New Point(12, 12)
        rtbOutput.Name = "rtbOutput"
        rtbOutput.Size = New Size(241, 185)
        rtbOutput.TabIndex = 1
        rtbOutput.Text = ""
        ' 
        ' dgvFirstFollow
        ' 
        dgvFirstFollow.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFirstFollow.Location = New Point(286, 25)
        dgvFirstFollow.Name = "dgvFirstFollow"
        dgvFirstFollow.Size = New Size(461, 279)
        dgvFirstFollow.TabIndex = 2
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(824, 386)
        Controls.Add(dgvFirstFollow)
        Controls.Add(rtbOutput)
        Controls.Add(Button1)
        Name = "Form1"
        Text = "Form1"
        CType(dgvFirstFollow, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents rtbOutput As RichTextBox
    Friend WithEvents dgvFirstFollow As DataGridView

End Class

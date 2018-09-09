'    ................................................................                                                                
'   |----------------------------------------------------------------|
'   | LICENSE : MIT https://choosealicense.com/licenses/mit/         |
'   | DATE : 10.09.2018                                              |
'   | WEBSITE : https://bech0r.net                                   |
'   | GITHUB : https://github.com/clusterzx                          |
'   |----------------------------------------------------------------|
'    """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

Imports System.Drawing.Drawing2D
Imports System.Security.Cryptography
Imports System.Text
Public Class CaptchaNET
    Public Enum difficulty
        easy
        medium
        hard
    End Enum
    Private rand As New Random()
    Dim Public Shared secret As String = ""
    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Dim Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function
    Private Sub DrawRandomLines(ByVal g As Graphics) 
        Dim red As New SolidBrush(Color.Red) 
        'For Creating Lines on The Captcha 
        For i As Integer = 0 To 19 
            g.DrawLines(New Pen(red, 2), GetRandomPoints()) 
        Next 
    End Sub
    Private Function GetRandomPoints() As Point() 
        Dim points As Point() = {New Point(rand.[Next](10, 150), rand.[Next](10, 150)), New Point(rand.[Next](10, 100), rand.[Next](10, 100))} 
        Return points 
    End Function
    Private Shared Function RandomText(ByVal length As Integer)
        Dim s As String = "ABCDEFGHKLMNPQRSTUVWXYZ123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To length
            Dim idx As Integer = r.Next(0, 31)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString
    End Function
    Public Function GenerateCaptcha(ByVal difficulty As difficulty, ByVal PictureBox As PictureBox)
        Dim myFont As Font = New Font("Segoe Print", 35, FontStyle.Strikeout, GraphicsUnit.Pixel)
        Dim gr As Graphics = Graphics.FromImage(PictureBox.Image)
        Dim randText As String
        Dim b As Bitmap
        Select Case difficulty
            Case difficulty.easy
                randText = RandomText(4)
                b = New Bitmap(190,71,Imaging.PixelFormat.Format32bppArgb)
            Case difficulty.medium
                randText = RandomText(6)
                b = New Bitmap(220,71,Imaging.PixelFormat.Format32bppArgb)
            Case difficulty.hard
                randText = RandomText(8)
               b = New Bitmap(245,71,Imaging.PixelFormat.Format32bppArgb)
        End Select

        'Dim b as New Bitmap(274,71,Imaging.PixelFormat.Format32bppArgb)
        Dim g as Graphics = Graphics.FromImage(b)
        Dim hb as New HatchBrush(HatchStyle.ZigZag, Color.FromArgb(255,128,0),Color.Black)
        Dim pen as New Pen(Color.RoyalBlue,GetRandom(1,10))
        Dim pen2 as New Pen(Color.Coral,GetRandom(1,10))
        Dim pen3 as New Pen(Color.Chartreuse,GetRandom(1,10))
        
        g.DrawEllipse(pen,GetRandom(0,10),GetRandom(0,50),GetRandom(40,100),GetRandom(40,100))
        g.DrawEllipse(pen2,GetRandom(4,30),GetRandom(1,60),GetRandom(122,150),GetRandom(130,160))
        g.DrawEllipse(pen3,GetRandom(6,15),GetRandom(10,70),GetRandom(30,85),GetRandom(20,77))

        DrawRandomLines(g)
        g.DrawString(randText, myFont, Brushes.White, 6, 2)

        PictureBox.Image = b


        PictureBox.Invalidate()
        secret = randText
    End Function


End Class

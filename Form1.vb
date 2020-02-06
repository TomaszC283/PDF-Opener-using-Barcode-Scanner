Imports System.IO
Imports PDFOpener2.Form2
Imports PDFOpener2.Form3


Public Class Form1
    Private kod As String
    Private kod_poczatkowy As String
    Private pliki() As String
    Private SR As StreamReader
    Private SRczas As StreamReader
    Private check As Boolean
    Private Condition As Boolean
    Private SzukajBool As Boolean
    Public lokalizacja_pdf
    Private czas As Integer

    Public Sub SprawadzLokalizacje(sender As Object, e As EventArgs)
        SR = New StreamReader("lokalizacja.txt")
        lokalizacja_pdf = "" + SR.ReadLine
        SR.Close()
        check = True
        If (Directory.Exists(lokalizacja_pdf)) Then

        Else
            check = False
            MessageBox.Show("Nie można uzyskać dostępu do podanej lokalizacji, sprawdź poprawność pliku lokalizacja.txt!  ( Przykład 'C:/PDF/' )")
            TextBox1.Clear()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        SRczas = New StreamReader("czas_opoznienia_[ms].txt")
        czas = SRczas.ReadLine
        Timer1.Interval = czas
        Timer1.Stop()
        Timer1.Start()
        If (CheckBox1.Checked) Then
            If (Condition = False) Then
                Condition = True
            End If
        End If

    End Sub

    Private Sub czas_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (Condition = True) Then
            SzukajPDF(sender, e)
            Condition = False
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SzukajPDF(sender, e)
    End Sub
    Private Sub SzukajPDF(sender As Object, e As EventArgs)
        Condition = False
        SprawadzLokalizacje(sender, e)
        If (check = True) Then
            kod = lokalizacja_pdf + TextBox1.Text
            kod_poczatkowy = TextBox1.Text
            If (TextBox1.Text.Length > 0) Then

                If (TextBox1.Text.Length > 7) Then

                    Try
                        pliki = Directory.GetFiles(lokalizacja_pdf)
                    Catch ex As IOException

                    End Try
                    Try
                        System.Diagnostics.Process.Start(kod + ".pdf")
                    Catch ex1 As Exception
                        Try
                            kod = kod.Remove(18, 3)
                            System.Diagnostics.Process.Start(kod + ".pdf")
                        Catch ex2 As Exception
                            Try
                                kod = kod.Remove(15, 3)
                                System.Diagnostics.Process.Start(kod + ".pdf")
                            Catch ex3 As Exception
                                If (kod_poczatkowy.Contains("C")) Then
                                    kod_poczatkowy = kod_poczatkowy.Replace("C", "B")
                                    Try
                                        System.Diagnostics.Process.Start(lokalizacja_pdf + kod_poczatkowy + ".pdf")
                                    Catch ex4 As Exception
                                        kod_poczatkowy = kod_poczatkowy.Replace("B", "A")
                                        Try
                                            System.Diagnostics.Process.Start(lokalizacja_pdf + kod_poczatkowy + ".pdf")
                                        Catch ex5 As Exception
                                            kod_poczatkowy = kod_poczatkowy.Remove(8, kod_poczatkowy.Length - 8)
                                            For Each plik In pliki
                                                If (plik.Contains(kod_poczatkowy)) Then
                                                    If (plik.Contains(".pdf")) Then
                                                        plik = plik.Remove(0, 7)
                                                        plik = plik.Remove(plik.Length - 4, 4)
                                                        Form2.ListBox1.Items.Add(plik)
                                                    End If
                                                End If
                                            Next
                                            If Form2.ListBox1.Items.Count > 0 Then
                                                Form2.ShowDialog()
                                            Else
                                                MessageBox.Show("Nie odnaleziono podanego PDF")
                                            End If
                                        End Try
                                    End Try
                                ElseIf (kod_poczatkowy.Contains("B")) Then
                                    kod_poczatkowy = kod_poczatkowy.Replace("B", "A")
                                    Try
                                        System.Diagnostics.Process.Start(lokalizacja_pdf + kod_poczatkowy + ".pdf")
                                    Catch ex6 As Exception
                                        kod_poczatkowy = kod_poczatkowy.Remove(8, kod_poczatkowy.Length - 8)
                                        For Each plik In pliki
                                            If (plik.Contains(kod_poczatkowy)) Then
                                                If (plik.Contains(".pdf")) Then
                                                    plik = plik.Remove(0, 7)
                                                    plik = plik.Remove(plik.Length - 4, 4)
                                                    Form2.ListBox1.Items.Add(plik)
                                                End If
                                            End If
                                        Next
                                        If Form2.ListBox1.Items.Count > 0 Then
                                            Form2.ShowDialog()
                                        Else
                                            MessageBox.Show("Nie odnaleziono podanego PDF")
                                        End If
                                    End Try
                                Else
                                    kod_poczatkowy = kod_poczatkowy.Remove(8, kod_poczatkowy.Length - 8)
                                    For Each plik In pliki
                                        If (plik.Contains(kod_poczatkowy)) Then
                                            If (plik.Contains(".pdf")) Then
                                                plik = plik.Remove(0, 7)
                                                plik = plik.Remove(plik.Length - 4, 4)
                                                Form2.ListBox1.Items.Add(plik)
                                            End If
                                        End If
                                    Next
                                    If Form2.ListBox1.Items.Count > 0 Then
                                        Form2.ShowDialog()
                                    Else
                                        MessageBox.Show("Nie odnaleziono podanego PDF")
                                    End If

                                End If

                            End Try


                        End Try

                    End Try
                    TextBox1.Focus()
                    TextBox1.Text = ""
                Else
                    TextBox1.Focus()
                    TextBox1.Text = ""
                    MessageBox.Show("Kod jest zbyt krótki ( zawiera mniej niż 7 znaków XX-XXXXX")
                End If
            End If
        Else
            TextBox1.Clear()
        End If
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Deactivate
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If (e.KeyValue = Keys.Enter) Then
            SzukajPDF(sender, e)
        End If
    End Sub
End Class

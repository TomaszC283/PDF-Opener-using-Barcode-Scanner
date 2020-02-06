Imports System.IO
Imports PDFOpener2.Form1


Public Class Form2

    Private wybrany_detal As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Me.Close()
            ListBox1.Items.Clear()
            System.Diagnostics.Process.Start(Form1.lokalizacja_pdf + wybrany_detal + ".pdf")
        Catch ex As Exception
            MessageBox.Show("Wystąpił nieoczekiwany błąd")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        ListBox1.Items.Clear()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        wybrany_detal = ListBox1.SelectedItem
    End Sub

    Private Sub Form2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyValue = Keys.Escape) Then
            Me.Close()
            ListBox1.Items.Clear()
        End If
    End Sub

    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        If (e.KeyValue = Keys.Escape) Then
            Me.Close()
            ListBox1.Items.Clear()
        End If
    End Sub
End Class
Imports MySql.Data.MySqlClient
Public Class scan_fg
    Dim serialnumber As String
    Dim qty As Integer

    Private Sub txtqr_tape_TextChanged(sender As Object, e As EventArgs) Handles txtqr.TextChanged

    End Sub

    Private Sub txtqr_tape_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txt_qty.Text = "" Or txt_qty.Text = "0" Then
                display_error("Please enter a valid QTY!", 1)
                Exit Sub
            End If

            Dim query As String = "UPDATE `denso_line_boxes` SET `qrcode` = @QRCode,qty=" & Val(txt_qty.Text) & " ,`posted` = 1 WHERE `line` = @Line"

            Try
                con.Close()
                con.Open()

                Using cmd As New MySqlCommand(query, con)
                    ' Use parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@QRCode", txtqr.Text)
                    cmd.Parameters.AddWithValue("@Line", PCline)

                    ' Execute the query
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    ' Optional: Inform the user about the success
                    If rowsAffected > 0 Then
                        Timer1.Start()


                    Else
                        display_error("No parts scanned", 1)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
                txtqr.Clear()
                txtqr.Focus()
            End Try
        End If
    End Sub

    Private Sub txt_qty_TextChanged(sender As Object, e As EventArgs) Handles txt_qty.TextChanged
        If txt_qty.Text = "" Then
            txtqr.Enabled = False
        Else

            txtqr.Enabled = True

        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim query As String = "SELECT posted FROM denso_line_boxes WHERE line = @line "

        ' Open the connection and execute the query
        con.Close()
        con.Open()
        Using cmdselect As New MySqlCommand(query, con)
            ' Add parameter for the line value
            cmdselect.Parameters.AddWithValue("@line", PCline)

            Using dr = cmdselect.ExecuteReader()



                If dr.Read = True Then
                    If dr.GetInt32("posted") = 0 Then
                        reload("SELECT `qrcode`,`qty`,`timein` FROM `denso_line_traceability` WHERE datein =CURDATE() and line='" & PCline & "'
                                GROUP BY qrcode", datagrid1)
                        errorpanel.Hide()
                        txtqr.Enabled = True
                        txtqr.Focus()
                        Timer1.Stop()
                    Else
                        errorpanel.Show()
                        txtqr.Enabled = False
                    End If
                End If
            End Using
        End Using




    End Sub



    Private Sub cmb_line_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub scan_fg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Timer2.Start()
    End Sub

    Private Sub datagrid1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datagrid1.CellContentClick

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            con.Close()
            con.Open()
            Dim cmdselect As New MySqlCommand("
            SELECT COUNT(*) AS `type_count`
            FROM `denso_line_boxes`
            WHERE posted =0 and line='" & PCline & "'
            GROUP BY `type`;", con)

            Dim count As Integer = 0
            dr = cmdselect.ExecuteReader()

            ' Count the number of rows returned by the reader
            While dr.Read()
                count += 1
            End While

            If count >= 2 Then
                errorpanel.Hide()
                If String.IsNullOrEmpty(txt_qty.Text) Then
                    txtqr.Enabled = False
                Else
                    txtqr.Enabled = True
                End If
            Else
                errorpanel.Show()
                txtqr.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        Finally
            dr.Close()
            con.Close()
        End Try
    End Sub



End Class
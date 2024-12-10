Imports MySql.Data.MySqlClient
Public Class scan_fg
    Dim serialnumber As String
    Dim qty As Integer

    Private Sub txtqr_tape_TextChanged(sender As Object, e As EventArgs) Handles txtqr_tape.TextChanged

    End Sub

    Private Sub txtqr_tape_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr_tape.KeyDown
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
                        cmd.Parameters.AddWithValue("@QRCode", txtqr_tape.Text)
                        cmd.Parameters.AddWithValue("@Line", cmb_line.Text)

                        ' Execute the query
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                        ' Optional: Inform the user about the success
                        If rowsAffected > 0 Then



                        Else
                            MessageBox.Show("No rows were updated. Check your data.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    con.Close()
                    txtqr_tape.Clear()
                    txtqr_tape.Focus()
                End Try
            End If
    End Sub

    Private Sub txt_qty_TextChanged(sender As Object, e As EventArgs) Handles txt_qty.TextChanged

    End Sub
End Class
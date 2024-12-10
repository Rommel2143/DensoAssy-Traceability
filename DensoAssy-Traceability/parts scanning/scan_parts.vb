Imports MySql.Data.MySqlClient
Public Class scan_parts
    Private Sub txtqr_retainer_TextChanged(sender As Object, e As EventArgs) Handles txtqr_tape.TextChanged

    End Sub

    Private Sub displayinfo(textqr As Object, datagrid As Object)
        Try
            Dim query As String = "SELECT id,series,qty,status FROM denso_parts WHERE qrcode = @qrcode"
            con.Close()
            con.Open()
            Dim cmdselect As New MySqlCommand(query, con)
            cmdselect.Parameters.AddWithValue("@qrcode", textqr.Text)

            dr = cmdselect.ExecuteReader()
            If dr.Read() Then
                Dim status As Integer = dr.GetInt32("status")
                Dim dbid As Integer = dr.GetInt32("id")
                Dim series As String = dr.GetString("series")
                Dim qty As String = dr.GetInt32("qty")
                If status = 1 Then
                    display_error("Unable to Proceed, Please return to Warehouse and Scan as OUT", 1)
                    dr.Close()
                    Exit Sub
                End If

                If status = 0 Then
                    'datagrid.Rows.Add(dbid, series, qty)
                    'datagrid.refresh

                    con.Close()
                    con.Open()
                    Dim cmdinsert As New MySqlCommand("INSERT INTO `denso_line_boxes`(`qr_id`, `line`) VALUES ('" & dbid & "','" & cmb_line.Text & "')", con)
                    cmdinsert.ExecuteNonQuery()

                End If
            Else
                display_error("QR not Recorded", 1)
            End If
            dr.Close()
        Catch ex As Exception
            display_error(ex.Message, 1)
        Finally
            textqr.text = ""
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub txtqr_retainer_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr_tape.KeyDown
        If e.KeyCode = Keys.Enter Then displayinfo(txtqr_tape, datagrid_tape)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Try
            con.Close()
            con.Open()
            Using cmd As New MySqlCommand("INSERT INTO `denso_line_traceability` (`qrcode`, `parts_id`, `timein`, `datein`, `userin`)
                                            SELECT 
                                                b.qrcode, 
                                                b.qr_id, 
                                                NOW() AS `timein`, 
                                                CURDATE() AS `datein`, 
                                               '" & idno & "' AS `userin`
                                            FROM `denso_line_boxes` b
                                            WHERE b.line = 1 AND b.posted = 1", con)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            End Using

            Using cmd As New MySqlCommand("UPDATE `denso_line_boxes` SET `qrcode` ='',qty=0 ,`posted` = 0 WHERE `line` = '" & cmb_line.Text & "' and posted=1", con)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            End Using







            reload("SELECT lb.qr_id, dp.series, dp.qty " &
                      "FROM denso_line_boxes lb " &
                      "LEFT JOIN denso_parts dp ON lb.qr_id = dp.id", datagrid_tape)

        Catch ex As Exception
            display_error(ex.Message, 1)
        Finally

            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub


    Private Sub scan_parts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmb_line_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_line.SelectedIndexChanged
        Timer1.Start()
    End Sub
End Class
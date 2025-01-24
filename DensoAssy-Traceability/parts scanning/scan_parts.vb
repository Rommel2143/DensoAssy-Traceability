Imports MySql.Data.MySqlClient
Public Class scan_parts
    Private Sub txtqr_retainer_TextChanged(sender As Object, e As EventArgs) Handles txtqr_tape.TextChanged

    End Sub

    Private Sub displayinfo(textqr As Object, type As Integer)
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


                    con.Close()
                    con.Open()
                    Dim cmdinsert As New MySqlCommand("INSERT INTO `denso_line_boxes`(`qr_id`, `line`,qrcode,qty,posted,type) VALUES ('" & dbid & "','" & PCline & "','',0,0," & type & ")", con)
                    cmdinsert.ExecuteNonQuery()

                Else
                    display_error("Must be scanned as OUT first", 1)

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

    Private Sub txtqr_tape_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr_tape.KeyDown
        If e.KeyCode = Keys.Enter Then
            displayinfo(txtqr_tape, 1)

            Timer_tape.Start()
        End If
    End Sub

    Private Sub txtqr_retainer_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr_retainer.KeyDown
        If e.KeyCode = Keys.Enter Then
            displayinfo(txtqr_retainer, 3)

            Timer_retainer.Start()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer_tape.Tick

        Try
            reloadtable(datagrid_tape, 1)
            con.Close()
            con.Open()
            If datagrid_tape.Rows.Count > 0 Then ' Ensure there are rows in the DataGridView

                warning_tape.Hide()
                Dim firstQty As Integer = Convert.ToInt32(datagrid_tape.Rows(0).Cells(2).Value)
                If firstQty <= 0 Then

                    Using cmd0 As New MySqlCommand("DELETE FROM `denso_line_boxes` WHERE qr_id ='" & datagrid_tape.Rows(0).Cells(0).Value & "' and line='" & PCline & "' and type=1", con)
                        cmd0.ExecuteNonQuery()
                    End Using
                End If




                Dim query As String = "SELECT `qrcode`,`qty` FROM `denso_line_boxes` WHERE line='" & PCline & "' and posted =1 and type=1"

                ' Open the connection and execute the query
                con.Close()
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    ' Add parameter for the line value
                    cmd.Parameters.AddWithValue("@line", PCline)
                    dr = cmd.ExecuteReader()
                    If dr.Read = True Then
                        Dim qty_deduct As Integer = dr.GetInt32("qty")
                        Dim qrcode As String = dr.GetString("qrcode")


                        Dim qty1 As Integer = Convert.ToInt32(datagrid_tape.Rows(0).Cells(2).Value)

                        If qty_deduct <= qty1 Then
                            Dim qtytotal As Integer = qty1 - qty_deduct
                            con.Close()
                            con.Open()

                            Dim query2 As String = "INSERT INTO `denso_line_traceability`(`qrcode`, `parts_id`, `line`, `timein`, `datein`, `userin`,`qty`) " &
                               "VALUES (@qrcode, @parts_id, @line, NOW(), CURDATE(), @userin, @qty)"

                            Using cmdinsert As New MySqlCommand(query2, con)
                                ' Add parameters to the query
                                cmdinsert.Parameters.AddWithValue("@qrcode", qrcode)
                                cmdinsert.Parameters.AddWithValue("@parts_id", datagrid_tape.Rows(0).Cells(0).Value)
                                cmdinsert.Parameters.AddWithValue("@line", PCline)
                                cmdinsert.Parameters.AddWithValue("@userin", idno)
                                cmdinsert.Parameters.AddWithValue("@qty", qty_deduct)
                                ' Execute the query
                                cmdinsert.ExecuteNonQuery()
                            End Using


                            Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=" & qtytotal & " WHERE `id` ='" & datagrid_tape.Rows(0).Cells(0).Value & "'", con)
                                cmdupdate.ExecuteNonQuery()
                            End Using


                            Using cmdupdate As New MySqlCommand("UPDATE `denso_line_boxes` SET `qrcode` ='',qty=0 ,`posted` = 0 WHERE `line` ='" & PCline & "' and type=1", con)
                                cmdupdate.ExecuteNonQuery()
                            End Using

                        Else


                            Dim qty2 As Integer
                            If datagrid_tape.Rows.Count > 1 Then

                                warning_tape.Hide()

                                qty2 = Convert.ToInt32(datagrid_tape.Rows(1).Cells(2).Value)


                                ' Deduct the remaining amount from qty2
                                Dim remaining_deduct As Integer = qty_deduct - qty1

                                Dim qtytotal As Integer = qty2 - remaining_deduct

                                con.Close()
                                con.Open()

                                Dim query2 As String = "INSERT INTO `denso_line_traceability`(`qrcode`, `parts_id`, `line`, `timein`, `datein`, `userin`,`qty`) " &
                               "VALUES (@qrcode, @parts_id, @line, NOW(), CURDATE(), @userin, @qty)"

                                Using cmdinsert As New MySqlCommand(query2, con)
                                    ' Add parameters to the query
                                    cmdinsert.Parameters.AddWithValue("@qrcode", qrcode)
                                    cmdinsert.Parameters.AddWithValue("@parts_id", datagrid_tape.Rows(0).Cells(0).Value)
                                    cmdinsert.Parameters.AddWithValue("@line", PCline)
                                    cmdinsert.Parameters.AddWithValue("@userin", idno)
                                    cmdinsert.Parameters.AddWithValue("@qty", qty1)
                                    ' Execute the query
                                    cmdinsert.ExecuteNonQuery()
                                End Using

                                Using cmdinsert2 As New MySqlCommand(query2, con)
                                    ' Add parameters to the query
                                    cmdinsert2.Parameters.AddWithValue("@qrcode", qrcode)
                                    cmdinsert2.Parameters.AddWithValue("@parts_id", datagrid_tape.Rows(1).Cells(0).Value)
                                    cmdinsert2.Parameters.AddWithValue("@line", PCline)
                                    cmdinsert2.Parameters.AddWithValue("@userin", idno)
                                    cmdinsert2.Parameters.AddWithValue("@qty", remaining_deduct)
                                    ' Execute the query
                                    cmdinsert2.ExecuteNonQuery()
                                End Using


                                Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=0 WHERE `id` ='" & datagrid_tape.Rows(0).Cells(0).Value & "'", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using

                                Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=" & qtytotal & " WHERE `id` ='" & datagrid_tape.Rows(1).Cells(0).Value & "'", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using


                                Using cmdupdate As New MySqlCommand("UPDATE `denso_line_boxes` SET `qrcode` ='',qty=0 ,`posted` = 0 WHERE `line` ='" & PCline & "' and type=1", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using




                            Else
                                display_error("NOT ENOUGH PARTS. PLEASE SCAN ACTUAL PARTS!", 1)
                                Timer_tape.Stop()
                                warning_tape.Show()
                            End If







                        End If
                    End If



                End Using
            Else

                warning_tape.Show()

            End If
            count_qty(datagrid_tape, lbl_tape)
        Catch ex As Exception
            display_error(ex.Message, 1)
        Finally

            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub


    Private Sub count_qty(datagrid As Object, textbox As Object)
        Try
            Dim totalQty As Integer = 0

            ' Loop through each row in datagrid_tape and sum up the values in cell(2)
            For Each row As DataGridViewRow In datagrid.Rows
                If Not row.IsNewRow Then ' Ensure the row is not a new row
                    totalQty += Convert.ToInt32(row.Cells(2).Value)
                End If
            Next

            textbox.Text = "Available QTY: " & totalQty
        Catch ex As Exception
            ' Handle any potential errors, such as invalid data in cell(2)
            MessageBox.Show("An error occurred while counting quantities: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub scan_parts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reloadtable(datagrid_tape, 1)
        reloadtable(datagrid_bezel, 2)
        reloadtable(datagrid_retainer, 3)
        Timer_retainer.Start()
        Timer_tape.Start()
        ' Timer_bezel.Start()
    End Sub

    Private Sub cmb_line_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub reloadtable(datagrid As Object, type As Integer)
        Try
            datagrid.Rows.Clear()
            Dim query As String = "SELECT lb.qr_id, dp.series, dp.qty " &
                                      "FROM denso_line_boxes lb " &
                                      "LEFT JOIN denso_parts dp ON lb.qr_id = dp.id " &
                                      "WHERE lb.line = @line and type=" & type & ""

            ' Open the connection and execute the query
            con.Close()
            con.Open()
            Using cmdselect As New MySqlCommand(query, con)
                ' Add parameter for the line value
                cmdselect.Parameters.AddWithValue("@line", PCline)

                Using dr = cmdselect.ExecuteReader()
                    ' Check for existing rows in DataGridView and update the first row
                    Dim rowIndex As Integer = 0 ' Target the first row
                    While dr.Read()

                        ' Add a new row if there are no more rows to update
                        datagrid.Rows.Add(dr.GetInt32("qr_id"), dr.GetString("series"), dr.GetInt32("qty"))

                    End While
                End Using
            End Using


            ' Start the Timer for background processing


        Catch ex As Exception
            ' Display error message for debugging
            MessageBox.Show("An error occurred: " & ex.Message)
        Finally
            ' Ensure the connection is closed properly
            con.Close()
        End Try

    End Sub

    Private Sub datagrid_tape_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datagrid_tape.CellContentClick

    End Sub

    Private Sub Guna2Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel2.Paint

    End Sub

    Private Sub Timer_bezel_Tick(sender As Object, e As EventArgs) Handles Timer_bezel.Tick
        Try
            reloadtable(datagrid_bezel, 2)
            con.Close()
            con.Open()

            If datagrid_bezel.Rows.Count > 0 Then ' Ensure there are rows in the DataGridView

                warning_bezel.Hide()
                Dim firstQty As Integer = Convert.ToInt32(datagrid_bezel.Rows(0).Cells(2).Value)
                If firstQty <= 0 Then
                    Using cmd0 As New MySqlCommand("DELETE FROM `denso_line_boxes` WHERE qr_id ='" & datagrid_bezel.Rows(0).Cells(0).Value & "' and line='" & PCline & "' and type=2", con)
                        cmd0.ExecuteNonQuery()
                    End Using
                End If


                Dim query As String = "SELECT `qrcode`,`qty` FROM `denso_line_boxes` WHERE line='" & PCline & "' and posted =1 and type=2"

                ' Open the connection and execute the query
                con.Close()
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@line", PCline)
                    dr = cmd.ExecuteReader()
                    If dr.Read = True Then
                        Dim qty_deduct As Integer = dr.GetInt32("qty")
                        Dim qrcode As String = dr.GetString("qrcode")
                        Dim qty1 As Integer = Convert.ToInt32(datagrid_bezel.Rows(0).Cells(2).Value)

                        If qty_deduct <= qty1 Then
                            Dim qtytotal As Integer = qty1 - qty_deduct
                            con.Close()
                            con.Open()

                            Dim query2 As String = "INSERT INTO `denso_line_traceability`(`qrcode`, `parts_id`, `line`, `timein`, `datein`, `userin`,`qty`) " &
                               "VALUES (@qrcode, @parts_id, @line, NOW(), CURDATE(), @userin, @qty)"

                            Using cmdinsert As New MySqlCommand(query2, con)
                                cmdinsert.Parameters.AddWithValue("@qrcode", qrcode)
                                cmdinsert.Parameters.AddWithValue("@parts_id", datagrid_bezel.Rows(0).Cells(0).Value)
                                cmdinsert.Parameters.AddWithValue("@line", PCline)
                                cmdinsert.Parameters.AddWithValue("@userin", idno)
                                cmdinsert.Parameters.AddWithValue("@qty", qty_deduct)
                                cmdinsert.ExecuteNonQuery()
                            End Using

                            Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=" & qtytotal & " WHERE `id` ='" & datagrid_bezel.Rows(0).Cells(0).Value & "'", con)
                                cmdupdate.ExecuteNonQuery()
                            End Using

                            Using cmdupdate As New MySqlCommand("UPDATE `denso_line_boxes` SET `qrcode` ='',qty=0 ,`posted` = 0 WHERE `line` ='" & PCline & "' and type=2", con)
                                cmdupdate.ExecuteNonQuery()
                            End Using
                        Else
                            Dim qty2 As Integer
                            If datagrid_bezel.Rows.Count > 1 Then

                                warning_bezel.Hide()

                                qty2 = Convert.ToInt32(datagrid_bezel.Rows(1).Cells(2).Value)

                                Dim remaining_deduct As Integer = qty_deduct - qty1
                                Dim qtytotal As Integer = qty2 - remaining_deduct

                                con.Close()
                                con.Open()

                                Dim query2 As String = "INSERT INTO `denso_line_traceability`(`qrcode`, `parts_id`, `line`, `timein`, `datein`, `userin`,`qty`) " &
                                   "VALUES (@qrcode, @parts_id, @line, NOW(), CURDATE(), @userin, @qty)"

                                Using cmdinsert As New MySqlCommand(query2, con)
                                    cmdinsert.Parameters.AddWithValue("@qrcode", qrcode)
                                    cmdinsert.Parameters.AddWithValue("@parts_id", datagrid_bezel.Rows(0).Cells(0).Value)
                                    cmdinsert.Parameters.AddWithValue("@line", PCline)
                                    cmdinsert.Parameters.AddWithValue("@userin", idno)
                                    cmdinsert.Parameters.AddWithValue("@qty", qty1)
                                    cmdinsert.ExecuteNonQuery()
                                End Using

                                Using cmdinsert2 As New MySqlCommand(query2, con)
                                    cmdinsert2.Parameters.AddWithValue("@qrcode", qrcode)
                                    cmdinsert2.Parameters.AddWithValue("@parts_id", datagrid_bezel.Rows(1).Cells(0).Value)
                                    cmdinsert2.Parameters.AddWithValue("@line", PCline)
                                    cmdinsert2.Parameters.AddWithValue("@userin", idno)
                                    cmdinsert2.Parameters.AddWithValue("@qty", remaining_deduct)
                                    cmdinsert2.ExecuteNonQuery()
                                End Using

                                Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=0 WHERE `id` ='" & datagrid_bezel.Rows(0).Cells(0).Value & "'", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using

                                Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=" & qtytotal & " WHERE `id` ='" & datagrid_bezel.Rows(1).Cells(0).Value & "'", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using

                                Using cmdupdate As New MySqlCommand("UPDATE `denso_line_boxes` SET `qrcode` ='',qty=0 ,`posted` = 0 WHERE `line` ='" & PCline & "' and type=2", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using
                            Else
                                display_error("NOT ENOUGH PARTS. PLEASE SCAN ACTUAL PARTS!", 1)
                                Timer_tape.Stop()
                                warning_bezel.Show()
                            End If
                        End If
                    End If
                End Using
            Else

                warning_bezel.Show()

            End If
            count_qty(datagrid_bezel, lbl_bezel)
        Catch ex As Exception
            display_error(ex.Message, 1)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub txtqr_bezel_TextChanged(sender As Object, e As EventArgs) Handles txtqr_bezel.TextChanged

    End Sub

    Private Sub txtqr_bezel_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr_bezel.KeyDown
        If e.KeyCode = Keys.Enter Then
            displayinfo(txtqr_bezel, 2)

            Timer_bezel.Start()
        End If
    End Sub

    Private Sub txtqr_retainer_TextChanged_1(sender As Object, e As EventArgs) Handles txtqr_retainer.TextChanged

    End Sub

    Private Sub Timer_retainer_Tick(sender As Object, e As EventArgs) Handles Timer_retainer.Tick
        Try
            reloadtable(datagrid_retainer, 3)
            con.Close()
            con.Open()

            If datagrid_retainer.Rows.Count > 0 Then ' Ensure there are rows in the DataGridView

                warning_retainer.Hide()
                Dim firstQty As Integer = Convert.ToInt32(datagrid_retainer.Rows(0).Cells(2).Value)
                If firstQty <= 0 Then
                    Using cmd0 As New MySqlCommand("DELETE FROM `denso_line_boxes` WHERE qr_id ='" & datagrid_retainer.Rows(0).Cells(0).Value & "' and line='" & PCline & "' and type=3", con)
                        cmd0.ExecuteNonQuery()
                    End Using
                End If

                Dim query As String = "SELECT `qrcode`,`qty` FROM `denso_line_boxes` WHERE line='" & PCline & "' and posted =1 and type=3"

                ' Open the connection and execute the query
                con.Close()
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@line", PCline)
                    dr = cmd.ExecuteReader()
                    If dr.Read = True Then
                        Dim qty_deduct As Integer = dr.GetInt32("qty")
                        Dim qrcode As String = dr.GetString("qrcode")
                        Dim qty1 As Integer = Convert.ToInt32(datagrid_retainer.Rows(0).Cells(2).Value)

                        If qty_deduct <= qty1 Then
                            Dim qtytotal As Integer = qty1 - qty_deduct
                            con.Close()
                            con.Open()

                            Dim query2 As String = "INSERT INTO `denso_line_traceability`(`qrcode`, `parts_id`, `line`, `timein`, `datein`, `userin`,`qty`) " &
                                           "VALUES (@qrcode, @parts_id, @line, NOW(), CURDATE(), @userin, @qty)"

                            Using cmdinsert As New MySqlCommand(query2, con)
                                cmdinsert.Parameters.AddWithValue("@qrcode", qrcode)
                                cmdinsert.Parameters.AddWithValue("@parts_id", datagrid_retainer.Rows(0).Cells(0).Value)
                                cmdinsert.Parameters.AddWithValue("@line", PCline)
                                cmdinsert.Parameters.AddWithValue("@userin", idno)
                                cmdinsert.Parameters.AddWithValue("@qty", qty_deduct)
                                cmdinsert.ExecuteNonQuery()
                            End Using

                            Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=" & qtytotal & " WHERE `id` ='" & datagrid_retainer.Rows(0).Cells(0).Value & "'", con)
                                cmdupdate.ExecuteNonQuery()
                            End Using

                            Using cmdupdate As New MySqlCommand("UPDATE `denso_line_boxes` SET `qrcode` ='',qty=0 ,`posted` = 0 WHERE `line` ='" & PCline & "' and type=3", con)
                                cmdupdate.ExecuteNonQuery()
                            End Using
                        Else
                            Dim qty2 As Integer
                            If datagrid_retainer.Rows.Count > 1 Then

                                warning_retainer.Hide()

                                qty2 = Convert.ToInt32(datagrid_retainer.Rows(1).Cells(2).Value)

                                Dim remaining_deduct As Integer = qty_deduct - qty1
                                Dim qtytotal As Integer = qty2 - remaining_deduct

                                con.Close()
                                con.Open()

                                Dim query2 As String = "INSERT INTO `denso_line_traceability`(`qrcode`, `parts_id`, `line`, `timein`, `datein`, `userin`,`qty`) " &
                                               "VALUES (@qrcode, @parts_id, @line, NOW(), CURDATE(), @userin, @qty)"

                                Using cmdinsert As New MySqlCommand(query2, con)
                                    cmdinsert.Parameters.AddWithValue("@qrcode", qrcode)
                                    cmdinsert.Parameters.AddWithValue("@parts_id", datagrid_retainer.Rows(0).Cells(0).Value)
                                    cmdinsert.Parameters.AddWithValue("@line", PCline)
                                    cmdinsert.Parameters.AddWithValue("@userin", idno)
                                    cmdinsert.Parameters.AddWithValue("@qty", qty1)
                                    cmdinsert.ExecuteNonQuery()
                                End Using

                                Using cmdinsert2 As New MySqlCommand(query2, con)
                                    cmdinsert2.Parameters.AddWithValue("@qrcode", qrcode)
                                    cmdinsert2.Parameters.AddWithValue("@parts_id", datagrid_retainer.Rows(1).Cells(0).Value)
                                    cmdinsert2.Parameters.AddWithValue("@line", PCline)
                                    cmdinsert2.Parameters.AddWithValue("@userin", idno)
                                    cmdinsert2.Parameters.AddWithValue("@qty", remaining_deduct)
                                    cmdinsert2.ExecuteNonQuery()
                                End Using

                                Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=0 WHERE `id` ='" & datagrid_retainer.Rows(0).Cells(0).Value & "'", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using

                                Using cmdupdate As New MySqlCommand("UPDATE `denso_parts` SET qty=" & qtytotal & " WHERE `id` ='" & datagrid_retainer.Rows(1).Cells(0).Value & "'", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using

                                Using cmdupdate As New MySqlCommand("UPDATE `denso_line_boxes` SET `qrcode` ='',qty=0 ,`posted` = 0 WHERE `line` ='" & PCline & "' and type=3", con)
                                    cmdupdate.ExecuteNonQuery()
                                End Using
                            Else
                                display_error("NOT ENOUGH PARTS. PLEASE SCAN ACTUAL PARTS!", 1)
                                warning_retainer.Show()
                                Timer_tape.Stop()
                            End If
                        End If
                    End If
                End Using
            Else
                warning_retainer.Show()
            End If

            count_qty(datagrid_retainer, lbl_retainer)
        Catch ex As Exception
            display_error(ex.Message, 1)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

    End Sub
End Class
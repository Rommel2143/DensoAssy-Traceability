Imports MySql.Data.MySqlClient
Public Class parts_scan
    Dim boxes As New List(Of String)

    Private Sub parts_scan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtqr_retainer.TextChanged

    End Sub

    Private Sub Guna2TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr_retainer.KeyDown
        If e.KeyCode = Keys.Enter Then displayinfo(txtqr_retainer, lbl_R_pcs, lbl_R_prod, lbl_R_lotno)
    End Sub

    Private Sub Guna2TextBox3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Guna2TextBox3_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub displayinfo(textqr As Object, lblqty As Object, lblprod As Object, lbllot As Object)
        Try
            Dim query As String = "SELECT * FROM denso_parts WHERE qrcode = @qrcode"
            con.Close()
            con.Open()
            Dim cmdselect As New MySqlCommand(query, con)
            cmdselect.Parameters.AddWithValue("@qrcode", textqr.Text)

            dr = cmdselect.ExecuteReader()
            If dr.Read() Then
                Dim status As Integer = dr.GetInt32("status")
                Dim dbid As Integer = dr.GetInt32("id")
                If status = 1 Then
                    display_error("Unable to Proceed, Please return to Warehouse and Scan as OUT", 1)
                    dr.Close()
                    Exit Sub
                End If

                If status = 0 Then
                    lblqty.Text = $"{dr.GetInt32("qty")} pcs."
                    Dim proddate As DateTime = dr.GetDateTime("proddate")
                    lblprod.Text = proddate.ToString("yyyy-MM-dd") ' Format the date for display
                    lbllot.Text = dr.GetString("lotnumber")

                    ' Check production date
                    Dim monthsDifference As Integer = DateDiff(DateInterval.Month, proddate, Date.Now)
                    If monthsDifference > 10 Then
                        lblprod.ForeColor = Color.Red ' Set font color to red
                        display_error("Warning this part is already expired!", 1)
                        lblprod.Text = proddate.ToString("yyyy-MM-dd") & "(WARNING!)" ' Format the date for display
                    Else
                        lblprod.ForeColor = Color.DimGray ' Reset to default color
                    End If
                    boxes.Add(dbid)
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



    Private Sub txtqr_bezel_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr_bezel.KeyDown
        If e.KeyCode = Keys.Enter Then displayinfo(txtqr_bezel, lbl_B_pcs, lbl_B_prod, lbl_B_lot)
    End Sub

    Private Sub txtqr_tape_TextChanged(sender As Object, e As EventArgs) Handles txtqr_tape.TextChanged

    End Sub

    Private Sub txtqr_tape_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr_tape.KeyDown
        If e.KeyCode = Keys.Enter Then displayinfo(txtqr_tape, lbl_T_pcs, lbl_T_prod, lbl_T_lot)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Try
            For Each box As String In boxes
                Dim query As String = "INSERT INTO `denso_line_boxes`(`qr_id`, `line`) VALUES (" & box & ",0)"
                con.Close()
                con.Open()
                Dim cmdinsert As New MySqlCommand(query, con)
                cmdinsert.ExecuteNonQuery()
            Next

        Catch ex As Exception
            display_error(ex.Message, 1)
        Finally

            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
End Class
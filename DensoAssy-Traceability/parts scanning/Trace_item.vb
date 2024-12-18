Imports MySql.Data.MySqlClient
Public Class Trace_item
    Private Sub txtqr_TextChanged(sender As Object, e As EventArgs) Handles txtqr.TextChanged

    End Sub

    Private Sub txtqr_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr.KeyDown

        If e.KeyCode = Keys.Enter Then
            If rad_box.Checked = True Then
                reload("SELECT 
                        dp.partno,
                        dp.lotnumber,
                        dt.qty,
                        dt.line,
                        dt.timein,
                        dt.datein
                    FROM 
                        `denso_line_traceability` dt
                    LEFT JOIN 
                        `denso_parts` dp 
                    ON 
                        dt.parts_id = dp.id
                    WHERE 
                        dt.qrcode = '" & txtqr.Text & "'", datagrid1)
            Else
                reload("SELECT 
                        dt.qrcode,
                        dt.line,
                        dt.timein,
                        dt.datein
                    FROM 
                        `denso_line_traceability` dt
                    LEFT JOIN 
                        `denso_parts` dp 
                    ON 
                        dt.parts_id = dp.id
                    WHERE 
                        dt.parts_id = '" & txtqr.Text & "'", datagrid1)
            End If
        End If
    End Sub

    Private Sub rad_parts_CheckedChanged(sender As Object, e As EventArgs) Handles rad_parts.CheckedChanged

    End Sub

    Private Sub export_Click(sender As Object, e As EventArgs) Handles export.Click
        export_excel(datagrid1)
    End Sub
End Class
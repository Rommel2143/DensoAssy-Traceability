Imports MySql.Data.MySqlClient
Public Class Mainframe
    Private Sub molding_mainframe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            If con.State = ConnectionState.Closed Then
                con.Open()

            End If

            Dim cmdselect As New MySqlCommand("SELECT * FROM trc_device WHERE PCname = @PCname AND PCmac = @PCmac", con)
            cmdselect.Parameters.AddWithValue("@PCname", PCname)
            cmdselect.Parameters.AddWithValue("@PCmac", PCmac)

            dr = cmdselect.ExecuteReader()

            If dr.Read() Then

                '  PClocation = dr.GetString("location")
                PCline = dr.GetString("remarks")
                display_form(sub_mainframe)

            Else
                Dim result As DialogResult = MessageBox.Show("Machine not Verified!", "Verify first!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

                If result = DialogResult.OK Then

                    display_form(Register_PC)
                    Exit Sub
                ElseIf result = DialogResult.Cancel Then
                    con.Close()
                    Application.Exit()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            If dr IsNot Nothing Then dr.Close()
            con.Close()

        End Try

    End Sub
End Class
Imports MySql.Data.MySqlClient
Public Class Register_PC
    Private Sub Register_PC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpcname.Text = PCname
        txtpcmac.Text = PCmac
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        If txtuser.Text = "PTRCI" And txtpassword.Text = "redhorsE" And txt_line.Text IsNot String.Empty Then

            con.Close()
            con.Open()
            Dim cmdselect As New MySqlCommand("INSERT INTO `trc_device`(`PCname`, `PCmac`, `location`,remarks) VALUES ('" & PCname & "','" & PCmac & "','DENSO','" & txt_line.Text & "')", con)
            dr = cmdselect.ExecuteReader
            Dim result As DialogResult = MessageBox.Show("Machine Verified, Please restart application!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If result = DialogResult.OK Then
                Application.Exit()
            End If
        Else
            MessageBox.Show("Invalid Credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class
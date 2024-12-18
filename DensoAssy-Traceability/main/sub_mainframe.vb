Imports System
Public Class sub_mainframe




    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs)
        con.Close()
        Application.Exit()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs)
        Dim pcname As New device_info
        pcname.ShowDialog()

    End Sub

    Private Sub MasterlistToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PartsScanningToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub FGScanningToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub sub_mainframe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        display_formsub(select_item)
    End Sub
End Class
Imports System
Public Class sub_mainframe




    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        con.Close()
        Application.Exit()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        display_form(New Login)
        Me.Close()

    End Sub


    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        Dim pcname As New device_info
        pcname.ShowDialog()

    End Sub

    Private Sub MasterlistToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PartsScanningToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PartsScanningToolStripMenuItem.Click
        display_formsub(scan_parts, "Parts")
    End Sub

    Private Sub FGScanningToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FGScanningToolStripMenuItem.Click
        display_formsub(scan_fg, "FG")
    End Sub
End Class
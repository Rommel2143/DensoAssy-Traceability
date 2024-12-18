Imports MySql.Data.MySqlClient
Imports System.Net.NetworkInformation
Imports ClosedXML.Excel
Module Module1

    Public Function connection() As MySqlConnection
        'Return New MySqlConnection("server=PTI-027s;user id=Inventory;password=inventory123@;database=trcsystem")
        Return New MySqlConnection("server=localhost;user id=momel;password=Magnaye2143@#;database=trcsystem")
    End Function
    Public con As MySqlConnection = connection()
    Public result As String
    Public cmd As New MySqlCommand
    Public da As New MySqlDataAdapter
    Public dr As MySqlDataReader
    Public dt As New DataTable

    'credentials for log in
    Public fname As String
    Public idno As String = "03200728"
    Public user_level As Integer
    Public designation As String
    Public PCname As String = Environment.MachineName
    Public PCmac As String = GetMacAddress()
    ' Public PClocation As String
    Public PCline As String

    Public date1 As String = Date.Now.ToString("MMMM dd, yyyy")
    Public datedb As String = Date.Now.ToString("yyyy-MM-dd")
    Public shift1 As String

    Public report_cmlqr As String


    Public Sub export_excel(datagrid As Object)
        Try
            If datagrid.Rows.Count > 0 Then
                Dim dt As New DataTable()

                ' Adding the Columns
                For Each column As DataGridViewColumn In datagrid.Columns
                    dt.Columns.Add(column.HeaderText, column.ValueType)
                Next

                ' Adding the Rows
                For Each row As DataGridViewRow In datagrid.Rows
                    If Not row.IsNewRow Then
                        dt.Rows.Add()
                        For Each cell As DataGridViewCell In row.Cells
                            dt.Rows(dt.Rows.Count - 1)(cell.ColumnIndex) = cell.Value.ToString()
                        Next
                    End If
                Next

                ' Save the data to an Excel file
                Using sfd As New SaveFileDialog()
                    sfd.Filter = "Excel Workbook|*.xlsx"
                    sfd.Title = "Save an Excel File"
                    sfd.ShowDialog()

                    If sfd.FileName <> "" Then
                        Using wb As New XLWorkbook()
                            wb.Worksheets.Add(dt, "Sheet1")
                            wb.SaveAs(sfd.FileName)
                        End Using
                        MessageBox.Show("Data successfully exported to Excel.", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Else
                MessageBox.Show("No data available to export.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function GetMacAddress() As String
        Dim macAddress As String = ""

        ' Get all network interfaces
        Dim networkInterfaces() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()

        ' Loop through each network interface to find the MAC address
        For Each networkInterface As NetworkInterface In networkInterfaces
            ' Check if the network interface is operational and not a loopback or tunnel interface
            If networkInterface.OperationalStatus = OperationalStatus.Up AndAlso
               networkInterface.NetworkInterfaceType <> NetworkInterfaceType.Loopback AndAlso
               networkInterface.NetworkInterfaceType <> NetworkInterfaceType.Tunnel Then
                ' Get the physical address (MAC address) of the network interface
                Dim physicalAddress As PhysicalAddress = networkInterface.GetPhysicalAddress()
                macAddress = physicalAddress.ToString()
                Exit For ' Exit the loop once the MAC address is found
            End If
        Next

        Return macAddress
    End Function


    Public Sub display_formsub(form As Form)
        With form
            .Refresh()
            .TopLevel = False
            sub_mainframe.Panel1.Controls.Add(form)
            .BringToFront()
            .Show()

        End With
    End Sub




    Public Sub viewdata(ByVal sql As String)
        con.Close()
        con.Open()

        With cmd
            .Connection = con
            .CommandText = sql
        End With
        dr = cmd.ExecuteReader
    End Sub
    Public Sub display_form(form As Form)

        With form
            .Refresh()
            .TopLevel = False
            Mainframe.Panel1.Controls.Clear()
            Mainframe.Panel1.Controls.Add(form)
            .BringToFront()
            .Show()

        End With
    End Sub

    Public Sub reload(ByVal sql As String, ByVal DTG As Object)
        Try
            dt = New DataTable
            con.Close()
            con.Open()
            With cmd
                .Connection = con
                .CommandText = sql
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            DTG.DataSource = dt
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
            da.Dispose()

        End Try
    End Sub


    Public Sub display_error(text As String, sound As Integer)

        ShowSnackbar(text)
        Select Case sound
            Case 0

            Case 1
                My.Computer.Audio.Play(My.Resources.errorsound, AudioPlayMode.Background)
            Case 2
                My.Computer.Audio.Play(My.Resources.duplicate, AudioPlayMode.Background)
        End Select
    End Sub


    Public Sub ShowSnackbar(message As String)
        ' Create a new Form to act as the snackbar
        Dim snackbarForm As New Form()

        ' Set basic properties
        snackbarForm.FormBorderStyle = FormBorderStyle.None
        snackbarForm.StartPosition = FormStartPosition.Manual
        snackbarForm.BackColor = Color.FromArgb(60, 63, 65) ' Dark background
        snackbarForm.ForeColor = Color.White ' White text
        snackbarForm.Height = 40 ' Set the height of the snackbar
        snackbarForm.Width = Screen.PrimaryScreen.Bounds.Width ' Set the width to screen width
        snackbarForm.TopMost = True ' Ensure it's on top
        snackbarForm.ShowInTaskbar = False

        ' Set the position at the top of the screen (90 pixels from the top)
        snackbarForm.Location = New Point(0, 90) ' Start at the top-left corner of the screen

        ' Add a label to display the message
        Dim messageLabel As New Label()
        messageLabel.Text = message
        messageLabel.Font = New Font("Segoe UI", 10)
        messageLabel.ForeColor = Color.White ' White text for better contrast
        messageLabel.AutoSize = False
        messageLabel.TextAlign = ContentAlignment.MiddleCenter
        messageLabel.Dock = DockStyle.Fill ' Fill the entire form with the label
        snackbarForm.Controls.Add(messageLabel)



        snackbarForm.Show()


        ' Set up a timer to close the snackbar after a few seconds
        Dim closeTimer As New Timer()
        AddHandler closeTimer.Tick, Sub(sender, e)
                                        ' Fade out effect
                                        For i As Integer = 10 To 0 Step -1
                                            snackbarForm.Opacity = i / 10.0
                                            Threading.Thread.Sleep(30)
                                        Next
                                        snackbarForm.Close() ' Close the snackbar
                                        closeTimer.Stop()
                                    End Sub
        closeTimer.Interval = 3000 ' Show for specified duration
        closeTimer.Start()
    End Sub



End Module

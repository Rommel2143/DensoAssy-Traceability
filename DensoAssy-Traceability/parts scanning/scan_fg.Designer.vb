<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class scan_fg
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(scan_fg))
        Me.txtqr_tape = New Guna.UI2.WinForms.Guna2TextBox()
        Me.cmb_line = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.txt_qty = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtqr_tape
        '
        Me.txtqr_tape.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtqr_tape.DefaultText = ""
        Me.txtqr_tape.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtqr_tape.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtqr_tape.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtqr_tape.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtqr_tape.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtqr_tape.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.txtqr_tape.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtqr_tape.IconLeft = CType(resources.GetObject("txtqr_tape.IconLeft"), System.Drawing.Image)
        Me.txtqr_tape.Location = New System.Drawing.Point(272, 303)
        Me.txtqr_tape.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.txtqr_tape.Name = "txtqr_tape"
        Me.txtqr_tape.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtqr_tape.PlaceholderText = "Scan FG..."
        Me.txtqr_tape.SelectedText = ""
        Me.txtqr_tape.Size = New System.Drawing.Size(475, 44)
        Me.txtqr_tape.TabIndex = 2
        '
        'cmb_line
        '
        Me.cmb_line.BackColor = System.Drawing.Color.Transparent
        Me.cmb_line.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmb_line.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_line.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmb_line.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmb_line.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmb_line.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.cmb_line.ItemHeight = 30
        Me.cmb_line.Items.AddRange(New Object() {"1", "2", "3"})
        Me.cmb_line.Location = New System.Drawing.Point(272, 199)
        Me.cmb_line.Name = "cmb_line"
        Me.cmb_line.Size = New System.Drawing.Size(140, 36)
        Me.cmb_line.TabIndex = 3
        '
        'txt_qty
        '
        Me.txt_qty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_qty.DefaultText = ""
        Me.txt_qty.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_qty.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_qty.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_qty.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_qty.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_qty.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.txt_qty.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_qty.Location = New System.Drawing.Point(272, 245)
        Me.txt_qty.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.txt_qty.Name = "txt_qty"
        Me.txt_qty.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txt_qty.PlaceholderText = "Set quantity..."
        Me.txt_qty.SelectedText = ""
        Me.txt_qty.Size = New System.Drawing.Size(179, 44)
        Me.txt_qty.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(170, 245)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Qty per Box :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(193, 303)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Scan QR :"
        '
        'scan_fg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1080, 649)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_qty)
        Me.Controls.Add(Me.cmb_line)
        Me.Controls.Add(Me.txtqr_tape)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "scan_fg"
        Me.Text = "8"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtqr_tape As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents cmb_line As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents txt_qty As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class

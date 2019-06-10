Public Class FrmArticulo

    Private Sub Formato()
        DgvListado.Columns(0).Visible = False
        DgvListado.Columns(2).Visible = False
        DgvListado.Columns(0).Width = 100
        DgvListado.Columns(1).Width = 100
        DgvListado.Columns(3).Width = 100
        DgvListado.Columns(4).Width = 100
        DgvListado.Columns(5).Width = 150
        DgvListado.Columns(6).Width = 100
        DgvListado.Columns(7).Width = 100
        DgvListado.Columns(8).Width = 00
        DgvListado.Columns(9).Width = 100
        DgvListado.Columns(10).Width = 100


        DgvListado.Columns.Item("Seleccionar").Visible = False
        BtnEliminar.Visible = False
        BtnActivar.Visible = False
        BtnDesactivar.Visible = False
        ChkSeleccionar.CheckState = False

    End Sub

    Private Sub Listar()
        Try
            Dim Neg As New Negocio.NArticulo
            DgvListado.DataSource = Neg.Listar()
            Lbltotal.Text = "Total Registros: " & DgvListado.DataSource.Rows.Count
            Me.Formato()
            Me.Limpiar()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub
    Private Sub Buscar()
        Try
            Dim Neg As New Negocio.NArticulo
            Dim Valor As String
            Valor = Txtvalor.Text
            DgvListado.DataSource = Neg.Buscar(Valor)
            Lbltotal.Text = "Total Registros: " & DgvListado.DataSource.Rows.Count
            Me.Formato()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub Limpiar()
        BtnInsertar.Visible = True
        BtnActualizar.Visible = False
        Txtvalor.Text = ""
        TxtId.Text = ""
        Txtnombre.Text = ""
        Txtdescripcion.Text = ""
    End Sub
    Private Sub FrmArticulo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Listar()
    End Sub

    Private Sub Btnbuscar_Click(sender As Object, e As EventArgs) Handles Btnbuscar.Click
        Me.Buscar()
    End Sub
End Class
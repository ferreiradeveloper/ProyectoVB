Public Class FrmCategoria
    Private Sub Formato()
        DgvListado.Columns(0).Visible = False
        DgvListado.Columns(0).Width = 100
        DgvListado.Columns(1).Width = 100
        DgvListado.Columns(2).Width = 150
        DgvListado.Columns(3).Width = 400
        DgvListado.Columns(4).Width = 100
    End Sub

    Private Sub Listar()
        Try
            Dim Neg As New Negocio.NCategoria
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
            Dim Neg As New Negocio.NCategoria
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
    Private Sub FrmCategoria_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Listar()
    End Sub

    Private Sub Btnbuscar_Click(sender As Object, e As EventArgs) Handles Btnbuscar.Click
        Me.Buscar()
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles BtnInsertar.Click
        If Me.ValidateChildren = True And Txtnombre.Text <> "" Then
            Dim Obj As New Entidades.Categoria
            Dim Neg As New Negocio.NCategoria

            Obj.Nombre = Txtnombre.Text
            Obj.Descripcion = Txtdescripcion.Text

            If (Neg.Insertar(Obj)) Then
                MsgBox("Se ha Registrado Correctamente", vbOKOnly + vbInformation, "Registro Correcto")
                'Me.Limpiar()
                Me.Listar()
            Else
                MsgBox("No se logro Registrar", vbOKOnly + vbCritical, "Registro Incorrecto")

            End If
        Else
                MsgBox("Complete los campos obligatorios (*)", vbOKOnly + vbCritical, "Falta ingresar datos")

        End If
    End Sub

    Private Sub BtnCabcelar_Click(sender As Object, e As EventArgs) Handles BtnCabcelar.Click
        Me.Limpiar()
        TabGeneral.SelectedIndex = 0
    End Sub

    Private Sub Txtnombre_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Txtnombre.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorIcono.SetError(sender, "")
        Else
            Me.ErrorIcono.SetError(sender, "Ingrese el nombre de la catagoria")
        End If

    End Sub

    Private Sub DgvListado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellDoubleClick
        TxtId.Text = DgvListado.SelectedCells.Item(1).Value
        Txtnombre.Text = DgvListado.SelectedCells.Item(2).Value
        Txtdescripcion.Text = DgvListado.SelectedCells.Item(3).Value
        BtnInsertar.Visible = False
        BtnActualizar.Visible = True
        TabGeneral.SelectedIndex = 1
    End Sub

    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles BtnActualizar.Click
        If Me.ValidateChildren = True And Txtnombre.Text <> "" And TxtId.Text <> "" Then
            Dim Obj As New Entidades.Categoria
            Dim Neg As New Negocio.NCategoria

            Obj.IdCategoria = TxtId.Text
            Obj.Nombre = Txtnombre.Text
            Obj.Descripcion = Txtdescripcion.Text

            If (Neg.Actualizar(Obj)) Then
                MsgBox("Se ha Actualizado Correctamente", vbOKOnly + vbInformation, "Actualizacion Correcta ")
                Me.Listar()
                TabGeneral.SelectedIndex = 0

            Else
                MsgBox("No se logro Actualizar", vbOKOnly + vbCritical, "Actualizacion Incorrecta")

            End If
        Else
            MsgBox("Complete los campos obligatorios (*)", vbOKOnly + vbCritical, "Falta ingresar datos")

        End If
    End Sub
End Class
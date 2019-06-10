Imports System.IO
Public Class FrmArticulo
    Private RutaOrigen As String
    Private RutaDestino As String
    Private Directorio As String = "C:\Preparacion\ProyectoVB\imagenes\"

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
        DgvListado.Columns(8).Width = 200
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
        TxtCodigo.Text = ""
        TxtPrecioVenta.Text = ""
        Txtnombre.Text = ""
        Txtdescripcion.Text = ""
        TxtStock.Text = ""
        Txtimagen.Text = ""
        PicImagen.Image = Nothing

    End Sub
    Private Sub CargarCategoria()
        Try
            Dim Neg As New Negocio.NCategoria
            CboCategoria.DataSource = Neg.Seleccionar
            CboCategoria.ValueMember = "idcategoria"
            CboCategoria.DisplayMember = "nombre"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmArticulo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Listar()
        Me.CargarCategoria()
    End Sub

    Private Sub Btnbuscar_Click(sender As Object, e As EventArgs) Handles Btnbuscar.Click
        Me.Buscar()
    End Sub

    Private Sub Btncargarimagen_Click(sender As Object, e As EventArgs) Handles Btncargarimagen.Click
        Dim File As New OpenFileDialog()
        File.Filter = "Image Files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
        If File.ShowDialog() = DialogResult.OK Then
            PicImagen.Image = Image.FromFile(File.FileName)
            RutaOrigen = File.FileName
            Txtimagen.Text = File.FileName.Substring(File.FileName.LastIndexOf("\") + 1)

        End If

    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles BtnInsertar.Click
        Try
            If Me.ValidateChildren = True And CboCategoria.Text <> "" And Txtnombre.Text <> "" And TxtPrecioVenta.Text <> "" And TxtStock.Text <> "" Then
                Dim Obj As New Entidades.Articulo
                Dim Neg As New Negocio.NArticulo

                Obj.IdCategorias = CboCategoria.SelectedValue
                Obj.Codigo = TxtCodigo.Text
                Obj.Nombre = Txtnombre.Text
                Obj.PrecioVenta = TxtPrecioVenta.Text
                Obj.Stock = TxtStock.Text
                Obj.Imagen = Txtimagen.Text
                Obj.Descripcion = Txtdescripcion.Text

                If (Neg.Insertar(Obj)) Then
                    MsgBox("Se ha Registrado Correctamente", vbOKOnly + vbInformation, "Registro Correcto")
                    If (Txtimagen.Text <> "") Then
                        RutaDestino = Directorio & Txtimagen.Text
                        File.Copy(RutaOrigen, RutaDestino)
                    End If
                    Me.Listar()
                Else
                    MsgBox("No se logro Registrar", vbOKOnly + vbCritical, "Registro Incorrecto")
                End If
            Else
                MsgBox("Complete los campos obligatorios (*)", vbOKOnly + vbCritical, "Falta ingresar datos")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnCabcelar_Click(sender As Object, e As EventArgs) Handles BtnCabcelar.Click
        Me.Limpiar()
        TabGeneral.SelectedIndex = 0
    End Sub
End Class
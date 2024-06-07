<%@ Page Title="Registro -" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Aereolinea._Registro" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <style>
        /* Estilos generales */
        body {
            background-image: url('Images/FondoAvion.png');
            background-size: cover;
            background-position: center;
            height: 90vh;
            margin: 0;
            padding: 0;
            overflow-x: hidden;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        /* Estilos para el título */
        .titulo {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #fff;
            text-align: center;
            margin-top: 10px;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
        }
         .ridge {
             border-style: ridge;
         }


        /* Estilos para el contenedor del formulario */
        .panel {
            max-width: 400px;
            background-color: rgba(255, 255, 255, 0.8);
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
        }

        input,
        select {
            width: 100%;
            margin-bottom: 10px;
        }

        .btn-separado {
            margin-right: 10px;
        }
         .inicio-link {
             text-decoration: none;
             color: #007bff;
             font-weight: bold;
             display: block;
             margin-top: 10px;
             text-align: center;
         }
    </style>

    <h1 class="titulo ridge">Registro de usuario</h1>
    <div class="panel">
        <div class="panel-body">
            <div class="form-group">
                <label for="txtDocumento">Documento:</label>
                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtNombres">Nombres:</label>
                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtApellidos">Apellidos:</label>
                <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtUsuario">Usuario:</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtContra">Contraseña:</label>
                <asp:TextBox ID="txtContra" runat="server" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="form-group">
                <label for="txtCorreo">Correo:</label>
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtTelefono">Telefono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtDireccion">Direccion:</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtFechaNacimiento">Fecha de Nacimiento:</label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="panel-footer text-center">
            <asp:Button ID="btnRegistrar" runat="server" OnClick="Registrar_Click" CssClass="btn btn-info" Text="Registrar usuario" />
            <a href="Ingreso.aspx" class="inicio-link">¿Ya tiene cuenta? Inicie aquí</a>
        </div>
    </div>
</asp:Content>

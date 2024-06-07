<%@ Page Title="Registro -" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Aereolinea._Registro" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <style>
        /* Estilos generales */
        body {
            margin: 0;
            padding: 0;
            overflow: hidden; /* Evitar el desplazamiento */
            display: flex;
            justify-content: center; /* Centrar horizontalmente */
            align-items: center; /* Centrar verticalmente */
            height: 100vh;
            font-size: 18px;
            background: linear-gradient(270deg, #bc8be5, #5a3876, #7f7fd5, #86a8e7, #91eae4);
            background-size: 1000% 1000%;
            animation: gradientAnimation 20s ease infinite;
        }

        @keyframes gradientAnimation {
            0% {
                background-position: 0% 50%;
            }

            25% {
                background-position: 50% 50%;
            }

            50% {
                background-position: 100% 50%;
            }

            75% {
                background-position: 50% 50%;
            }

            100% {
                background-position: 0% 50%;
            }
        }

        /* Estilos para el contenido del formulario */
        .container {
            display: flex;
            flex-direction: column; /* Apilar elementos verticalmente */
            align-items: center;
            width: 80%;
            margin-top: 50px; /* Mayor espacio arriba */
        }

        .titulo {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #fff;
            text-align: center;
            margin-bottom: 20px; /* Mayor espacio abajo */
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
        }

        .panel {
            max-width: 500px;
            background-color: rgba(255, 255, 255, 0.8); /* Fondo semitransparente */
            padding: 30px; /* Mayor espacio interno */
            border-radius: 15px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.5);
            margin-bottom: 50px; /* Mayor espacio abajo */
        }

        input,
        select {
            width: 100%;
            margin-bottom: 20px; /* Mayor espacio abajo */
            font-size: 18px;
        }

        .btn-separado {
            margin-right: 10px;
        }

        .registro-link {
            text-decoration: none;
            color: #007bff;
            font-weight: bold;
            display: block;
            margin-top: 10px;
            text-align: center;
        }

        .text-switcher {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #fff;
            text-align: center;
            margin-bottom: 50px; /* Mayor espacio abajo */
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
            font-size: 24px;
            flex: 1; /* Ocupa el espacio restante */
            max-width: 500px;
        }
        /* Estilos para los iconos */
        .fa {
            margin-right: 10px; /* Espacio a la derecha del icono */
        }
        .panel-body {
    max-width: 600px; /* Ancho máximo del panel */
    min-width: 400px; /* Ancho mínimo del panel */
}
    </style>
    <div class="container">
        <h1 class="titulo ridge">Registro de usuario</h1>
        <div class="panel">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
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

                    </div>
                    <div class="col-md-6">
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

                </div>

            </div>
            <div class="row">
            </div>


            <div class="panel-footer text-center">
                <asp:Button ID="btnRegistrar" runat="server" OnClick="Registrar_Click" CssClass="btn btn-info" Text="Registrar usuario" />
                <a href="Ingreso.aspx" class="inicio-link">¿Ya tiene cuenta? Inicie aquí</a>
            </div>
        </div>
    </div>
</asp:Content>

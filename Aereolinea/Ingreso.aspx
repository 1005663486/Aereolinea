<%@ Page Title="Ingreso -" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Ingreso.aspx.cs" Inherits="Aereolinea._Ingreso" EnableEventValidation="false" %>

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

        .ridge {
            border-style: ridge;
        }

        /* Estilos para el contenido del formulario */
        .titulo {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #fff;
            text-align: center;
            margin-bottom: 10px;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
        }

        .panel {
            max-width: 400px; /* Ancho m�ximo del panel */
            background-color: rgba(255, 255, 255, 0.8); /* Fondo semitransparente */
            padding: 20px;
            border-radius: 10px; /* Bordes redondeados */
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.3); /* Sombra suave */
        }

        input,
        select {
            width: 100%;
            margin-bottom: 10px;
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

    </style>

    <h1 class="titulo ridge">Inicio de sesi�n</h1>
    <div class="panel">
        <div class="panel-body">
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario" />
            <asp:TextBox ID="txtContra" runat="server" CssClass="form-control" TextMode="Password" placeholder="Contrase�a" />
        </div>
        <div class="panel-footer text-center">
            <asp:Button ID="btnIniciar" runat="server" OnClick="Iniciar_Click" CssClass="btn btn-info" Text="Iniciar sesi�n" />
            <a href="Registro.aspx" class="registro-link">�No tiene cuenta? Reg�strese aqu�</a>
        </div>
    </div>
</asp:Content>

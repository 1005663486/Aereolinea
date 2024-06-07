<%@ Page Title="Ingreso -" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Ingreso.aspx.cs" Inherits="Aereolinea._Ingreso" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <style>
    .content {
        flex: 1;
        padding: 20px;
        background-color: #ffa0;
        margin-bottom: 10%;
    }
    body {
        background-image: url('Images/FondoAvion.png');
        background-size: cover;
        background-position: center;
        height: 100vh;
        margin: 0;
        padding: 0;
    }

    body::-webkit-scrollbar {
        display: none;
    }

    .btn-separado {
        margin-right: 10px;
    }

    .modal-footer {
        text-align: center;
    }

    .modal-open-noscroll {
        overflow: hidden;
    }
    .panel{
        margin: 5px;
    }

    .titulo {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        color: black;
        text-align: center;
        margin-top: 10px;
        text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
    }
    .panel{
        margin: 20%;

    }
    .ridge {
        border-style: ridge;
    }
    input,
    select{
        max-width: 100%;
    }
    .full-width{
        width:100%
    }
    </style>

    <div class="col-md-10 col-lg-12 col-xs-12 col-12 text-center">
        <div class="row justify-content-center align-items-center" style="height: 100vh;">
            <div class="col-md-10 col-lg-12 col-xs-12 col-12">
                <div id="panelMantenimiento col-md-6" class="panel panel-default">

                    <h1 class="titulo ridge">Inicio de sesión</h1>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-10 col-lg-12 col-xs-12 col-12">
                                <div class="form-group col-md-12 col-xs-12 text-left">
                                    <div class="form-group col-md-4 col-xs-4 col-4 col-xl-4 col-lg-4 text-center">
                                    </div>
                                    <div class="form-group col-md-4 col-xs-4 text-center">
                                        <label for="txtUsuario">Usuario:</label>
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="form-group col-md-12 col-xs-12 text-left">
                                    <div class="form-group col-md-4 col-xs-4 text-center">
                                    </div>
                                    <div class="form-group col-md-4 col-xs-4 text-center">
                                    <label for="txtContra">Contraseña:</label>
                                    <asp:TextBox ID="txtContra" runat="server" CssClass="form-control" TextMode="Password" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer text-center">
                        <asp:Button ID="btnIniciar" runat="server" OnClick="Iniciar_Click" CssClass="btn btn-info" Text="Iniciar sesión" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

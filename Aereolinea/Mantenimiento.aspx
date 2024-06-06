<%@ Page Title="Mantenimiento -" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mantenimiento.aspx.cs" Inherits="Aereolinea.Mantenimiento" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <style>
    .content {
    flex: 1;
    padding: 20px;
    background-color: #ffa0;
    margin-bottom: 10%;
    }
    .Datos .panel{
        margin: 10px;
        background-color: aliceblue;
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

    .titulo-mantenimiento {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        color: #fff;
        text-align: center;
        margin-top: 50px;
        text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
    }
    .ridge {
        border-style: ridge;
    }
</style>


    <div class="containerImage">
        <asp:Button ID="btnMostrarPanel" runat="server" OnClientClick="mostrarPanel(); return false;" Text="Agregar nuevo Mantenimiento" CssClass="btn btn-primary btn-lg" />
        <br />

        <div id="panelMantenimiento" class="panel panel-default" style="display: none;">

            <h1 class="titulo-mantenimiento ridge">Historial de Mantenimiento</h1>

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="ddlResponsable">Responsable:</label>
                            <asp:DropDownList ID="ddlResponsable" runat="server" CssClass="form-control">
                                <!-- Aquí puedes llenar la lista desplegable con los responsables -->
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlAeronave">Aeronave:</label>
                            <asp:DropDownList ID="ddlAeronave" runat="server" CssClass="form-control">
                                <!-- Aquí puedes llenar la lista desplegable con las aeronaves -->
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlTipoMantenimiento">Tipo de Mantenimiento:</label>
                            <asp:DropDownList ID="ddlTipoMantenimiento" runat="server" CssClass="form-control">
                                <!-- Aquí puedes llenar la lista desplegable con los tipos de mantenimiento -->
                            </asp:DropDownList>
                        </div>
                        <!-- Otros campos de mantenimiento según sea necesario -->
                    </div>
                </div>
            </div>
            <div class="panel-footer text-center">
                <asp:Button ID="btnGuardarMantenimiento" runat="server" OnClick="btnGuardarMantenimiento_Click" CssClass="btn btn-info" Text="Guardar" />
                <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-warning" OnClick="btnEditar_Click" />
            </div>
        </div>

        <!-- Aquí puedes agregar la lista de mantenimientos similar al GridView de tripulación -->
    </div>
</asp:Content>
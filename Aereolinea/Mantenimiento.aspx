<%@ Page Title="Mantenimiento -" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mantenimiento.aspx.cs" Inherits="Aereolinea._Mantenimiento" EnableEventValidation="false" %>

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

    .titulo-mantenimiento {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        color: black;
        text-align: center;
        margin-top: 10px;
        text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
    }
    .ridge {
        border-style: ridge;
    }
    .full-width{
        width:100%
    }
    </style>

    <div class="col-md-12 col-lg-12 col-xs-12 col-12 text-center">
        <div class="row justify-content-center align-items-center" style="height: 100vh;">
            <div class="col-md-12 col-lg-12 col-xs-12 col-12">
                <asp:Button ID="btnVerHistorial" runat="server" OnClick="VerHistorial_Click" CssClass="btn btn-info" Text="Ver historial" />
                <div id="panelMantenimiento" class="panel panel-default">

                    <h1 class="titulo-mantenimiento ridge">Agregar mantenimiento</h1>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12 col-lg-12 col-xs-12 col-12">
                                <div class="form-group col-md-4 col-xs-4 text-left">
                                    <label for="txtFechaInicio">Fecha de inicio(dd-mm-aaaa):</label>
                                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group col-md-4 col-xs-4 text-left">
                                    <label for="txtFechaFin">Fecha de fin(dd-mm-aaaa):</label>
                                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group col-md-4 col-xs-4 text-left">
                                    <label for="ddlTipoMantenimiento">Tipo de Mantenimiento:</label>
                                    <asp:DropDownList ID="ddlTipoMantenimiento" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Correctivo" Value="Correctivo"></asp:ListItem>
                                        <asp:ListItem Text="Preventivo" Value="Preventivo"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4 col-xs-4 text-left">
                                    <label for="ddlResponsable">Responsable:</label>
                                    <asp:DropDownList ID="ddlResponsable" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4 col-xs-4 text-left">
                                    <label for="ddlAeronavesActivas">Aeronave:</label>
                                    <asp:DropDownList ID="ddlAeronavesActivas" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4 col-xs-4 text-left">
                                    <label for="ddlEstado">Estado:</label>
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                                        <asp:ListItem Text="En curso" Value="En curso"></asp:ListItem>
                                        <asp:ListItem Text="Finalizado" Value="Finalizado"></asp:ListItem>
                                        <asp:ListItem Text="Pospuesto" Value="Pospuesto"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-12 col-lg-12 col-xs-12 col-12 text-left">
                                    <label for="txtObservaciones">Observaciones:</label>
                                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Rows="3" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ddlTipoMantenimiento">Tipo de Mantenimiento:</label>
                            <asp:DropDownList ID="ddlTipoMantenimiento" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="panel-footer text-center">
                        <asp:Button ID="btnGuardar" runat="server" OnClick="Guardar_Click" CssClass="btn btn-info" Text="Guardar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

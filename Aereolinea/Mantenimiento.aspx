<%@ Page Title="Historial de Mantenimiento -" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialMantenimiento.aspx.cs" Inherits="Aereolinea._HistorialMantenimiento" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/HistorialMantenimiento.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <script>
        $(document).ready(function () {
            $('#modalDetallesMantenimiento').on('show.bs.modal', function () {
                $('body').css('overflow', 'hidden');
                $('body').css('background-color', 'rgba(0, 0, 0, 0.5)');
            });

            $('#modalDetallesMantenimiento').on('hidden.bs.modal', function () {
                $('body').css('overflow', 'auto');
                $('body').css('background-color', 'transparent');
            });
        });
    </script>
    <style>
        .content {
            flex: 1;
            padding: 20px;
            background-color: #ffa0;
        }

        body {
            background-image: url('Images/FondoAvion.png');
            background-size: cover;
            background-position: center;
            height: 100vh;
            margin: 0;
            padding: 0;
            background-repeat: no-repeat;
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

        .titulo-historial {
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
        <div>
            <div class="col-md-12">
                <h1 class="titulo-historial ridge">Historial de Mantenimiento</h1>
                <br />
                 <p><strong>Aeronave:</strong> <asp:DropDownList ID="ddlAeronavesActivas" runat="server"></asp:DropDownList>
                <asp:Button ID="btnBuscarMantenimientos" runat="server" Text="Buscar Historial" OnClick="BuscarMantenimientos_Click" />
                <asp:ListView ID="LVHistorialMantenimiento" runat="server" ItemPlaceholderID="itemPlaceholder">
                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Mantenimiento <%# Eval("IdMantenimiento") %></h3>
                                </div>
                                <div class="panel-body">
                                    <p><strong>Aeronave:</strong> <%# Eval("Aeronave") %></p>
                                    <p><strong>Observaciones:</strong> <%# Eval("Observaciones") %></p>
                                    <p><strong>Fecha de Inicio:</strong> <%# Eval("FechaInicio") %></p>
                                    <p><strong>Fecha de Fin:</strong> <%# Eval("FechaFin") %></p>
                                    <p><strong>Tipo de Mantenimiento:</strong> <%# Eval("TipoManetimiento") %></p>
                                    <p><strong>Responsable:</strong> <%# Eval("Responsable") %></p>
                                    <p><strong>Estado:</strong> <%# Eval("Estado") %></p>
                                    <p><strong>Tripulante ID:</strong> <%# Eval("TripulanteId") %></p>
                                    <p><strong>Aeronave ID:</strong> <%# Eval("AeronaveId") %></p>
                                    <div class="btn-group" role="group" aria-label="Opciones">
                                        <asp:Button ID="btnVer" runat="server" Text='Ver' CommandArgument='<%# Eval("IdMantenimiento") %>' OnClick="Ver_Click" CssClass="btn btn-info btn-separado" />
                                        <asp:Button ID="btnEliminar" runat="server" Text='Eliminar' CommandArgument='<%# Eval("IdMantenimiento") %>' OnClick="Eliminar_Click" CssClass="btn btn-danger" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>

    <div id="modalDetallesMantenimiento" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title text-center">Detalles del Mantenimiento</h4>
                </div>
                <div class="modal-body" style="padding-left: 60px;">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtAeronave">Aeronave:</label>
                                    <asp:TextBox ID="txtAeronave" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtFechaInicio">Fecha de Inicio:</label>
                                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtFechaFin">Fecha de Fin:</label>
                                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtTipoMantenimiento">Tipo de Mantenimiento:</label>
                                    <asp:TextBox ID="txtTipoMantenimiento" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtResponsable">Responsable:</label>
                                    <asp:TextBox ID="txtResponsable" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtEstado">Estado:</label>
                                    <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtObservaciones">Observaciones:</label>
                                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" />
                                </div>
                                <asp:TextBox ID="txtIdMantenimiento" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnEditar" runat="server" Text='Editar' CommandArgument='<%# Eval("IdMantenimiento") %>' OnClick="Editar_Click" CssClass="btn btn-warning" Style="margin-right: 300px" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

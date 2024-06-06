<%@ Page Title="Historial de Mantenimiento -" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialMantenimiento.aspx.cs" Inherits="Aereolinea._HistorialMantenimiento" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/HistorialMantenimiento.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <script>
        $(document).ready(function () {
            $('#modalDetallesHistorialMantenimiento').on('show.bs.modal', function () {
                $('body').css('overflow', 'hidden');
                $('body').css('background-color', 'rgba(0, 0, 0, 0.5)');
            });

            $('#modalDetallesHistorialMantenimiento').on('hidden.bs.modal', function () {
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

        .titulo-historial {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #fff;
            text-align: center;
            margin-top: 50px;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
        }
        .no-historial-message {
            font-size: 18px;
            color: #dc3545; 
            font-weight: bold;
            margin-top: 100px;
            text-align: center;
            border: 2px solid #dc3545; 
            border-radius: 5px;
            padding: 10px;
            background-color: #f8d7da;
        }
        .ridge {
            border-style: ridge;
        }
    </style>

    <div class="containerImage Datos">
            <div class="col-md-12 col-lg-12 col-12 col-xs-12">
                <h1 class="titulo-historial ridge">Historial de Mantenimiento</h1>
               <div class="align-items-center text-center" style="font-size:18px; margin:20px">
                   <strong>Aeronave:</strong>
                    <asp:DropDownList ID="ddlAeronavesActivas" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnBuscarMantenimientos" runat="server" Text="Buscar Historial" OnClick="BuscarMantenimientos_Click" />
               </div> 
                <asp:ListView ID="LVHistorialMantenimiento" runat="server" ItemPlaceholderID="itemPlaceholder">
                        <ItemTemplate>
                            <div class="col-md-5 col-4 col-xs-6 col-lg-4">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Mantenimiento del <%# Eval("Codigo") %></h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-9">
                                                <p><strong>Fecha de inicio:</strong> <%# Eval("FechaInicio") %></p>
                                                <p><strong>Fecha de fin:</strong> <%# Eval("FechaFin") %></p>
                                                <p><strong>Observaciones:</strong> <%# Eval("Observaciones") %></p>
                                                <p><strong>Responsable:</strong> <%# Eval("Responsable") %></p>
                                                <p><strong>Estado:</strong> <%# Eval("Estado") %></p>
                                            </div>
                                        </div>
                                        <div class="btn-group col-md-12 col-lg-12 col-12 col-xs-12" role="group" aria-label="Opciones">
                                            <div class="col-md-6 col-6 col-xs-6 col-lg-6 text-right">
                                                <asp:Button ID="Button1" runat="server" Text='Ver' CommandArgument='<%# Eval("IdMantenimiento") %>' OnClick="Ver_Click" CssClass="btn btn-info btn-separado" />
                                            </div>
                                            <div class="col-md-6 col-6 col-xs-6 col-lg-6 text-left">
                                               <asp:Button ID="btnEliminar" runat="server" Text='Eliminar' CommandArgument='<%# Eval("IdMantenimiento") %>' OnClick="Eliminar_Click" CssClass="btn btn-danger" />
                                            </div>  
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                </asp:ListView>  
            </div>
     </div>
    <div class="text-center">
         <asp:Label ID="lblNoHistorial" runat="server" Text="No hay historial de mantenimientos." Visible="false" CssClass="no-historial-message"></asp:Label>
    </div>
   
    <div id="modalDetallesHistorialMantenimiento" class="modal fade" role="dialog">
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
                                    <label for="txtModelo">Modelo de la aeronave:</label>
                                    <asp:TextBox ID="txtModelo" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtFechaInicio">Fecha de inicio:</label>
                                    <asp:TextBox ID="txtFechaInicio" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtFechaFin">Fecha de fin:</label>
                                    <asp:TextBox ID="txtFechaFin" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtTipoMantenimiento">Tipo de Mantenimiento:</label>
                                    <asp:TextBox ID="txtTipoMantenimiento" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                <label for="txtResponsable">Responsable:</label>
                                <asp:TextBox ID="txtResponsable" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                
                                <div class="form-group">
                                    <label for="txtEstado">Estado:</label>
                                    <asp:TextBox ID="txtEstado" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtCodigo">Codigo:</label>
                                    <asp:TextBox ID="txtCodigo" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtObservaciones">Observaciones:</label>
                                    <asp:TextBox ID="txtObservaciones" ReadOnly="true" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnEdit" runat="server" Text="Editar" CommandName="EditRow" CommandArgument='<%#Eval("IdMantenimiento")%>' OnClick="Editar_Click" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vuelos.aspx.cs" Inherits="Aereolinea._Vuelos" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/Vuelos.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>


 
    <script>
            $(document).ready(function () {
                $('#modalDetallesVuelo').on('show.bs.modal', function () {
                    $('body').css('overflow', 'hidden');
                    $('body').css('background-color', 'rgba(0, 0, 0, 0.5)');
                });

                $('#modalDetallesVuelo').on('hidden.bs.modal', function () {
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
            background-position:center;
            height: 100vh;
            margin: 0;
            padding: 0;
            background-repeat: no-repeat; /* Evita que la imagen se repita */        
        }

        body::-webkit-scrollbar {
            display: none; /* Oculta la barra de desplazamiento en Chrome, Safari y otros navegadores basados en WebKit */
        }

        .btn-separado {
            margin-right: 10px; /* Ajusta este valor según el espacio deseado entre los botones */
        }

        .modal-footer {
            text-align: center;
        }

        .modal-open-noscroll {
            overflow: hidden;
        }

       /* Estilo para el título */
    .titulo-vuelos {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; /* Fuente elegante */
        color: #fff; /* Color del texto */
        text-align: center;
        margin-top: 50px; /* Ajusta este valor según sea necesario */
        text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5); /* Sombra del texto para destacarlo */
    }
    .ridge {border-style: ridge;}
    </style>
    <div class="containerImage">
        <div>
            <div class="col-md-12">
              <h1 class="titulo-vuelos ridge">Vuelos programados</h1>
                <br />

                <!-- Contenido principal -->
                <%--  <div class="jumbotron">
                    <h1>Bienvenido a nuestra página de vuelos</h1>
                    <p class="lead">Encuentra los mejores vuelos para tu próximo viaje</p>
                    <p><a href="http://www.tuwebdevuelos.com" class="btn btn-primary btn-lg">Explora ahora &raquo;</a></p>
                </div>--%>
                <asp:ListView ID="LVVuelos" runat="server" ItemPlaceholderID="itemPlaceholder">

                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Vuelo <%# Eval("NumeroVuelo") %></h3>
                                </div>
                                <div class="panel-body">
                                    <p><strong>Origen:</strong> <%# Eval("Origen") %></p>
                                    <p><strong>Destino:</strong> <%# Eval("Destino") %></p>
                                    <p><strong>Fecha de Salida:</strong> <%# Eval("Fecha") %></p>
                                    <p><strong>Fecha de Llegada:</strong> <%# Eval("FechaLlegada") %></p>
                                    <div class="btn-group" role="group" aria-label="Opciones">
                                        <!-- Tu código HTML para mostrar la información del vuelo -->

                                        <asp:Button ID="btnVer" runat="server" Text='Ver' CommandArgument='<%# Eval("NumeroVuelo") %>' OnClick="Ver_Click" CssClass="btn btn-info btn-separado" />
                                        <asp:Button ID="btnEliminar" runat="server" Text='Eliminar' CommandArgument='<%# Eval("NumeroVuelo") %>' OnClick="Eliminar_Click" CssClass="btn btn-danger" />


                                    </div>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div id="modalDetallesVuelo" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title text-center">Detalles del Vuelo</h4>
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
                                    <label for="txtFechaSalida">Fecha de Salida:</label>
                                    <asp:TextBox ID="txtFechaSalida" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtFechaLlegada">Fecha de Llegada:</label>
                                    <asp:TextBox ID="txtFechaLlegada" runat="server" CssClass="form-control" />
                                </div>

                                <div class="form-group">
                                    <label for="txtOrigen">Origen:</label>
                                    <asp:TextBox ID="txtOrigen" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtDestino">Destino:</label>
                                    <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtEstado">Estado:</label>
                                    <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" />
                                </div>


                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtPasajeros">Pasajeros:</label>
                                    <asp:TextBox ID="txtPasajeros" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtRuta">Ruta:</label>
                                    <asp:TextBox ID="txtRuta" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtSillas">Sillas:</label>
                                    <asp:TextBox ID="txtSillas" runat="server" CssClass="form-control" />
                                </div>

                                <div class="form-group">
                                    <label for="txtPuertaAbordaje">Puerta Abordaje:</label>
                                    <asp:TextBox ID="txtPuertaAbordaje" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtTripulante">Tripulante:</label>
                                    <asp:TextBox ID="txtTripulante" runat="server" CssClass="form-control" />
                                </div>
                                <asp:TextBox ID="txtIdVuelo" runat="server" Visible="false"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnEditar" runat="server" Text='Editar' CommandArgument='<%# Eval("IdVuelo") %>' OnClick="Editar_Click" CssClass="btn btn-warning" Style="margin-right: 300px" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>

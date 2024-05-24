<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tripulacion.aspx.cs" Inherits="Aereolinea.Tripulacion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script>
        function mostrarPanel() {
            var panel = document.getElementById('panelTripulante');
            panel.style.display = 'block';
        }
        function cerrarPanel() {
            var panel = document.getElementById('panelTripulante');
            panel.style.display = 'none';
        }


    </script>


    <style>
        .content {
            flex: 1;
            padding: 20px;
            background-color: #ffa0;
        }

        body {
            background-image: url('Images/AVION1.jpg');
            background-size: cover;
            background-position: center;
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

        .panel-footer {
            margin-top: 20px; /* Ajusta el valor según el espacio deseado */
        }

        #gvTripulacion {
            margin-bottom: 50px; /* Ajusta el valor según el espacio deseado entre la tabla y el pie de página */
        }

        .containerImage {
            padding-bottom: 100px; /* Ajusta el valor según el espacio deseado entre el contenido y el pie de página */
        }
    </style>
    <div class="containerImage">
        <asp:Button ID="btnMostrarPanel" runat="server" OnClientClick="mostrarPanel(); return false;" Text="Agregar nuevo Tripulante" CssClass="btn btn-primary btn-lg" />
        <br />


        <div id="panelTripulante" class="panel panel-default" style="display: none;">
            <div class="panel-heading text-center">Agregar un nuevo Tripulante

             <button type="button" class="close" onclick="cerrarPanel()">&times;</button>
            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="form-group">
                                <label for="nombre">Nombre Completo:</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-group">
                            <label for="Rol">Rol:</label>
                            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Piloto" Value="Piloto"></asp:ListItem>
                                <asp:ListItem Text="Copiloto" Value="Copiloto"></asp:ListItem>
                                <asp:ListItem Text="Auxiliar de Vuelo" Value="Auxiliar de Vuelo"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="TipoID">Tipo de identificación:</label>
                            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Cédula de Ciudadanía" Value="Cédula de Ciudadanía"></asp:ListItem>
                                <asp:ListItem Text="Tarjeta de Identidad" Value="Tarjeta de Identidad"></asp:ListItem>
                                <asp:ListItem Text="Pasaporte" Value="Pasaporte"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="form-group">
                            <label for="numero_identificacion">Número de Identificación:</label>
                            <asp:TextBox ID="txtNumIdentificacion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Horario">Horario:</label>
                            <asp:DropDownList ID="ddlHorario" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Mañana" Value="06:00:00"></asp:ListItem>
                                <asp:ListItem Text="Tarde" Value="15:00:00"></asp:ListItem>
                                <asp:ListItem Text="Noche" Value="19:00:00"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="form-group">
                            <label for="celular">Celular:</label>
                            <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="correo">Correo:</label>
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="Estado">Estado:</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                        </div>

                    </div>

                </div>
            </div>
            <div class="panel-footer text-center">
                <asp:Button ID="btnGuardarTripulante" runat="server" OnClick="btnGuardarTripulante_Click" CssClass="btn btn-info btn-separado" Text="Guardar" />
               
            </div>
        </div>

        <asp:GridView ID="gvTripulacion" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
            CellPadding="4">
            <Columns>
                <asp:BoundField DataField="IdTripulacion" HeaderText="ID" />
                <asp:BoundField DataField="Rol" HeaderText="Rol" />
                <asp:BoundField DataField="Hoario" HeaderText="Horario" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="NumeroId" HeaderText="Número de Identificación" />
                <asp:BoundField DataField="TipoId" HeaderText="Tipo de Identificación" />
                <asp:BoundField DataField="Celular" HeaderText="Celular" />
                <asp:BoundField DataField="Correo" HeaderText="Correo" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
            <HeaderStyle BackColor="#007bff" ForeColor="White" />
            <RowStyle BackColor="#f8f9fa" />
            <AlternatingRowStyle BackColor="white" />
        </asp:GridView>

    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aeronaves.aspx.cs" Inherits="Aereolinea.Aeronaves" EnableEventValidation="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script>
        function mostrarPanel() {
            var panel = document.getElementById('panelAeronaves');
            panel.style.display = 'block';
        }
        function cerrarPanel() {
            var panel = document.getElementById('panelAeronaves');
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
        .panel-footer {
            margin-top: 20px;
        }
        #gvTripulacion {
            margin-bottom: 50px;
        }
        .containerImage {
            padding-bottom: 100px;
        }
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
    </style>

    <div class="containerImage">
                       
        <asp:Button ID="btnMostrarPanel" runat="server" OnClientClick="mostrarPanel(); return false;" Text="Agregar nueva Aeronave" CssClass="btn btn-primary btn-lg" />
        <br />

        <div id="panelAeronaves" class="panel panel-default" style="display: none;">
            <div class="panel-heading text-center">Agregar una nueva Aeronave
                <button type="button" class="close" onclick="cerrarPanel()">&times;</button>
            </div>
            
            <div class="panel-body">
                <div class="row">
                    <input id="IDAeronave" runat="server" style="display:none"/>
                    <div class="col-md-6">                 
                        <div class="form-group">
                            <label for="Modelo">Modelo:</label>
                            <asp:DropDownList ID="ddlModelo" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Boeing 737" Value="Boeing 737"></asp:ListItem>
                                <asp:ListItem Text="Airbus A320" Value="Airbus A320"></asp:ListItem>
                                <asp:ListItem Text="Boeing 747" Value="Boeing 747"></asp:ListItem>
                                <asp:ListItem Text="Embraer 190" Value="Embraer 190"></asp:ListItem>
                                <asp:ListItem Text="Boeing 777" Value="Boeing 777"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label for="fecha">Fecha de Adquision:</label>
                            <asp:TextBox ID="txtfecha" runat="server" CssClass="form-control" TextMode="Date" ></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="Estado">Estado:</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Codigo">Codigo:</label>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>                       
                        <div class="form-group">
                            <label for="Fabricante">Fabricante:</label>
                            <asp:TextBox ID="txtFabricante" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="Capacidad">Capacidad:</label>
                            <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer text-center">
                <asp:Button ID="btnGuardarAeronaves" runat="server" OnClick="btnGuardarAeronaves_Click" CssClass="btn btn-info btn-separado" Text="Guardar" />
             <asp:Button ID="btnEditarAeronaves" runat="server" Text="Actualizar datos" CssClass="btn btn-warning" OnClick="btnEditarAeronaves_Click" />
            </div>
        </div>
         <h1 class="titulo ridge">Aeronaves</h1>
                <asp:GridView ID="gvAeronaves" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
            CellPadding="4" DataKeyNames="IdAeronave" OnRowCommand="gvAeronaves_RowCommand" OnRowDeleting="gvAeronaves_RowDeleting1" AutoGenerateDeleteButton="False">
            <Columns>
                <asp:BoundField DataField="IdAeronave" HeaderText="ID" />
                <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha de Adquision" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                <asp:BoundField DataField="Fabricante" HeaderText="Fabricante" />
                <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar" />
                <asp:TemplateField HeaderText="Acciones">
        <ItemTemplate>
            <asp:Button ID="btnEdit" runat="server" Text="Editar" CommandName="EditRow" CommandArgument='<%#Eval("IdAeronave")%>' OnClick="btnEdit_Click" />
        </ItemTemplate>
    </asp:TemplateField>
                </Columns>
            <HeaderStyle BackColor="#007bff" ForeColor="White" />
            <RowStyle BackColor="#f8f9fa" />
            <AlternatingRowStyle BackColor="white" />
        </asp:GridView>

    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tripulacion.aspx.cs" Inherits="Aereolinea.Tripulacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
     <style>
        .content {
            flex: 1;
            padding: 20px;
            background-color: #ffa0;
        }

        body {
            background-image: url('Images/AVION1.jpg');
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

    </style>
    <div class="containerImage">
        <div>
            <div class="col-md-12">
       
                <asp:ListView ID="LMantenimientos" runat="server" ItemPlaceholderID="itemPlaceholder">

                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                   
                                </div>
                                <div class="panel-body">
                                
                                    <div class="btn-group" role="group" aria-label="Opciones">
                                        <!-- Tu código HTML para mostrar la información del vuelo -->

                                       

                                    </div>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
</asp:Content>

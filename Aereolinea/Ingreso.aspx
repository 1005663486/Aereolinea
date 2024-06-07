<%@ Page Title="Ingreso -" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="Ingreso.aspx.cs" Inherits="Aereolinea._Ingreso" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <style>
        /* Estilos generales */
        body {
            margin: 0;
            padding: 0; 
            overflow: hidden; /* Evitar el desplazamiento */
            display: flex;
            justify-content: center; /* Centrar horizontalmente */
            align-items: center; /* Centrar verticalmente */
            height: 100vh;
            font-size: 18px; 
            background: linear-gradient(270deg, #bc8be5, #5a3876, #7f7fd5, #86a8e7, #91eae4);
            background-size: 1000% 1000%;
            animation: gradientAnimation 20s ease infinite;

        }

        @keyframes gradientAnimation {
            0% {
                background-position: 0% 50%;
            }

            25% {
                background-position: 50% 50%;
            }

            50% {
                background-position: 100% 50%;
            }

            75% {
                background-position: 50% 50%;
            }

            100% {
                background-position: 0% 50%;
            }
        }

        /* Estilos para el contenido del formulario */
        .container {
            display: flex;
            flex-direction: column; /* Apilar elementos verticalmente */
            align-items: center;
            width: 80%;
            margin-top: 50px; /* Mayor espacio arriba */
        }

        .titulo {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #fff;
            text-align: center;
            margin-bottom: 20px; /* Mayor espacio abajo */
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
        }

        .panel {
            max-width: 500px; 
            background-color: rgba(255, 255, 255, 0.8); /* Fondo semitransparente */
            padding: 30px; /* Mayor espacio interno */
            border-radius: 15px; 
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.5); 
            margin-bottom: 50px; /* Mayor espacio abajo */
        }

        input,
        select {
            width: 100%;
            margin-bottom: 20px; /* Mayor espacio abajo */
            font-size: 18px; /* Tama�o de fuente m�s grande */
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

        /* Estilos para los textos de misi�n y visi�n */
        .text-switcher {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #fff;
            text-align: center;
            margin-bottom: 50px; /* Mayor espacio abajo */
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
            font-size: 24px; /* Tama�o de fuente m�s grande */
            flex: 1; /* Ocupa el espacio restante */
            max-width: 500px; /* Ancho m�ximo del contenedor de textos */
        }
        /* Estilos para los iconos */
        .fa {
            margin-right: 10px; /* Espacio a la derecha del icono */
        }
    </style>
     <div class="container">
    <div class="row">
            <div class="col-md-2">
                <img src="Images/4071704.png" alt="Icono de avión" style="vertical-align:central; height: 84px; width: 103px;" />
            </div>
          <div class="col-md-10">
               <h1 class="titulo">AVIACOL - EMPRESA DE VUELOS</h1>
            </div>
        </div>
        <br />
        <div class="row">

            <div class="col-md-5" style="padding-top: 100px;">

                <div class="text-switcher" id="textSwitcher">
                    <span id="switchText">Nuestra Misión: Ofrecer el mejor servicio de transporte aéreo.</span>
                </div>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-6">
            <div class="panel">
            <h2 class="titulo ridge"><i class="fa fa-sign-in-alt"></i> Inicio de sesión</h2>
            <div class="panel-body">
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario" />
                <asp:TextBox ID="txtContra" runat="server" CssClass="form-control" TextMode="Password" placeholder="Contraseña" />
            </div>
            <div class="panel-footer text-center">
                <asp:Button ID="btnIniciar" runat="server" OnClick="Iniciar_Click" CssClass="btn btn-info" Text="Iniciar sesión" />
                            <a href="Registro.aspx" class="registro-link">¿No tiene cuenta? Registrese aqui</a>
            </div>
        </div>
            </div>
        </div>
    </div>

    <script>
        document.addEventListener('DOMContentLoaded', (event) => {
            const switchText = document.getElementById('switchText');

            const texts = [
                "Nuestra Misión: Ofrecer el mejor servicio de transporte aereo.",
                "Nuestra Visión: Ser la aerolínea lider en innovación y calidad."
            ];

            let index = 0;

            setInterval(() => {
                index = (index + 1) % texts.length;
                switchText.textContent = texts[index];
            }, 2000); // Cambia el texto cada 3 segundos
        });

    </script>
</asp:Content>

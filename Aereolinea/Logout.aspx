<!DOCTYPE html>
<html5>
    <head runat="server">
    <title>Cierre de Sesión Exitoso</title>
    <script type="text/javascript">
        // Función para redirigir a la página de inicio de sesión después de un breve retraso
        function redirigirAIngreso() {
            setTimeout(function () {
                window.location.href = 'Ingreso.aspx'; // Cambia 'Ingreso.aspx' por la URL correcta de tu página de inicio de sesión
            }, 100); // Redirige después de 3 segundos (3000 milisegundos)
        }

        // Llamar a la función de redirección cuando la página se cargue completamente
        window.onload = function () {
            redirigirAIngreso();
        };
    </script>
</head>
</html5>

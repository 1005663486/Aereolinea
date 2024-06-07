<!DOCTYPE html>
<html5>
    <head runat="server">
    <title>Cierre de Sesi�n Exitoso</title>
    <script type="text/javascript">
        // Funci�n para redirigir a la p�gina de inicio de sesi�n despu�s de un breve retraso
        function redirigirAIngreso() {
            setTimeout(function () {
                window.location.href = 'Ingreso.aspx'; // Cambia 'Ingreso.aspx' por la URL correcta de tu p�gina de inicio de sesi�n
            }, 100); // Redirige despu�s de 3 segundos (3000 milisegundos)
        }

        // Llamar a la funci�n de redirecci�n cuando la p�gina se cargue completamente
        window.onload = function () {
            redirigirAIngreso();
        };
    </script>
</head>
</html5>

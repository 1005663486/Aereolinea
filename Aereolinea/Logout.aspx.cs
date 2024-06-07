using System;

namespace Aereolinea
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cerrar la sesión del usuario
            Session.Clear();
            Session.Abandon();

            // Redirigir a la página de ingreso
            Response.Redirect("Ingreso.aspx");
        }
    }
}

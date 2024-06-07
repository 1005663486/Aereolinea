using System;
using System.Web;
using System.Web.UI;

namespace Aereolinea
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["NombreCompleto"] != null)
            {
                lblNombreUsuario.Text = Session["NombreCompleto"].ToString();
            }
        }

        [System.Web.Services.WebMethod]
        public static void CerrarSesion_Click()
        {
            // Cerrar la sesión del usuario
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();

            // Redirigir a la página de ingreso
            HttpContext.Current.Response.Redirect("Ingreso.aspx");
        }
    }
}

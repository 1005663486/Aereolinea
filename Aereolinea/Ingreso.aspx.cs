using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using BCrypt.Net;

namespace Aereolinea
{
    public partial class _Ingreso : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {    
        }

        protected void Iniciar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContra.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MostrarMensaje("Por favor, ingrese ambos campos.");
                return;
            }

            if (ValidarUsuario(usuario, contrasena))
            {
                // Redireccionar a la página principal o dashboard después de la autenticación exitosa
                Response.Redirect("Vuelos.aspx");
            }
            else
            {
                MostrarMensaje("Usuario o contraseña incorrectos.");
            }
        }

        private bool ValidarUsuario(string usuario, string contrasena)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString;
            bool usuarioValido = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_login", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Contraseña", contrasena); // Pasar la contraseña como parámetro

                try
                {
                    connection.Open();
                    int count = (int)cmd.ExecuteScalar(); // Obtener el resultado del procedimiento almacenado

                    // Si el resultado es mayor que cero, significa que se encontró un usuario con la contraseña dada
                    if (count > 0)
                    {
                        usuarioValido = true;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Ocurrió un error al validar el usuario: " + ex.Message);
                }
            }

            return usuarioValido;
        }


        private void MostrarMensaje(string mensaje)
        {
            string script = $"Swal.fire('{mensaje}')";
            ScriptManager.RegisterStartupScript(this, GetType(), "Mensaje", script, true);
        }
    }
}

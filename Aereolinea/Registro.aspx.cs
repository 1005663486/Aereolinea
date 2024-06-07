using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Aereolinea
{
    public partial class _Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Registrar_Click(object sender, EventArgs e)
        {
            // Capturar los valores de los campos de texto
            string documento = txtDocumento.Text.Trim();
            string nombres = txtNombres.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContra.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            int telefono = int.Parse(txtTelefono.Text.Trim());
            string direccion = txtDireccion.Text.Trim();
            string fechaNacimiento = txtFechaNacimiento.Text.Trim();

            // Llamar al método para registrar el usuario en la base de datos
            if (RegistrarUsuario(documento, nombres, apellidos, usuario, contraseña, correo, telefono, direccion, fechaNacimiento))
            {
                // Redireccionar a la página de inicio de sesión después del registro exitoso
                Response.Redirect("Ingreso.aspx");
            }
            else
            {
                MostrarMensaje("Error al registrar el usuario.");
            }
        }

        private bool RegistrarUsuario(string documento, string nombres, string apellidos, string usuario, string contraseña, string correo, int telefono, string direccion, string fechaNacimiento)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_CreateUsuario", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Documento", documento);
                cmd.Parameters.AddWithValue("@Nombres", nombres);
                cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Contraseña", contraseña);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Direccion", direccion);
                cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true; // Registro exitoso
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Ocurrió un error al registrar el usuario: " + ex.Message);
                    return false; // Error en el registro
                }
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            string script = $"alert('{mensaje}')";
            ScriptManager.RegisterStartupScript(this, GetType(), "Mensaje", script, true);
        }
    }
}

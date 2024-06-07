using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aereolinea
{
    public partial class _Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Registrar_Click(object sender, EventArgs e)
        {
            string documento = txtDocumento.Text.Trim();
            string nombres = txtNombres.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContra.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string direccion = txtDireccion.Text.Trim();

            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
            {
                MostrarMensaje("Formato de fecha de nacimiento inválido.");
                return;
            }

            Debug.WriteLine("Documento: " + documento);
            Debug.WriteLine("Nombres: " + nombres);
            Debug.WriteLine("Apellidos: " + apellidos);
            Debug.WriteLine("Usuario: " + usuario);
            Debug.WriteLine("Contraseña: " + contraseña);
            Debug.WriteLine("Correo: " + correo);
            Debug.WriteLine("Telefono: " + telefono);
            Debug.WriteLine("Direccion: " + direccion);
            Debug.WriteLine("FechaNacimiento: " + fechaNacimiento);

            if (RegistrarUsuario(documento, nombres, apellidos, usuario, contraseña, correo, telefono, direccion, fechaNacimiento))
            {
                Response.Redirect("Ingreso.aspx");
            }
            else
            {
                MostrarMensaje("Error al registrar el usuario.");
            }
        }

        private bool RegistrarUsuario(string documento, string nombres, string apellidos, string usuario, string contraseña, string correo, string telefono, string direccion, DateTime fechaNacimiento)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_CreateUsuario", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    cmd.Parameters.AddWithValue("@Nombres", nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Contraseña", contraseña);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un error al registrar el usuario: " + ex.Message);
                return false;
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            string script = $"alert('{mensaje}')";
            ScriptManager.RegisterStartupScript(this, GetType(), "Mensaje", script, true);
        }
    }
}

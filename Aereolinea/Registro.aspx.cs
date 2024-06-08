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

            try
            {
                Button btn = (Button)sender;

                string documento = txtDocumento.Text.Trim();
                string nombres = txtNombres.Text.Trim();
                string apellidos = txtApellidos.Text.Trim();
                string usuario = txtUsuario.Text.Trim();
                string contraseña = txtContra.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                DateTime fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);



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
                    cmd.Parameters.AddWithValue("@Contra", contraseña);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        // Verificar si se realizaron cambios en la base de datos
                        if (rowsAffected > 0)
                        {
                            // La actualización fue exitosa
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'La actualización fue exitosa.', 'success');", true);


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el vuelo.', 'error');", true);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el vuelo.', 'error');", true);
            }

        }

        private void MostrarMensaje(string mensaje)
        {
            string script = $"alert('{mensaje}')";
            ScriptManager.RegisterStartupScript(this, GetType(), "Mensaje", script, true);
        }
    }
}

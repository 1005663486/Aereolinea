using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aereolinea
{
    public partial class Tripulacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListarTripulacion();

        }

        protected void btnGuardarTripulante_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            if (nombre=="")
            {
                Console.WriteLine("Error al crear la tripulación: ");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al crear la tripulación.', 'error');", true);
            }
            else
            {
                string rol = ddlRol.SelectedValue;
                string numeroId = txtNumIdentificacion.Text;
                string tipoId = ddlTipoIdentificacion.SelectedValue;
                string celular = txtCelular.Text;
                string correo = txtCorreo.Text;
                int estado = Convert.ToInt32(ddlEstado.SelectedValue);
                TimeSpan horario = TimeSpan.Parse(ddlHorario.SelectedValue);

                // Llamar al método para crear la tripulación
                CrearTripulacion(rol, nombre, numeroId, tipoId, celular, correo, estado, horario);

            }

        }
        private void CrearTripulacion(string rol, string nombre, string numeroId, string tipoId, string celular, string correo, int estado, TimeSpan horario)
        {
            try
            {
                // Crear y abrir conexión
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_CreateTripulacion";
                    cmd.Parameters.AddWithValue("@Rol", rol);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@NumeroId", numeroId);
                    cmd.Parameters.AddWithValue("@TipoId", tipoId);
                    cmd.Parameters.AddWithValue("@Celular", celular);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@Horario", horario);
                    cmd.Connection = conn;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Si llega a este punto, significa que la inserción fue exitosa
                // Puedes mostrar un mensaje de éxito, recargar la página o realizar cualquier otra acción necesaria
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'Se ha creado la tripulación correctamente.', 'success');", true);
                LimpiarCampos();
                ListarTripulacion();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante el proceso de inserción
                // Aquí puedes mostrar un mensaje de error al usuario, hacer un registro de errores, etc.
                Console.WriteLine("Error al crear la tripulación: " + ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al crear la tripulación.', 'error');", true);
            }
        }
        private void LimpiarCampos()
        {
            // Limpiar los valores de los controles de entrada
            txtNombre.Text = "";
            txtNumIdentificacion.Text = "";

            txtCelular.Text="";
            txtCorreo.Text="";
            // Limpiar otros controles de entrada según sea necesario

            // Opcionalmente, también puedes restablecer los valores predeterminados de los DropDownList
            ddlRol.SelectedIndex = 0;
            ddlTipoIdentificacion.SelectedIndex = 0;
            ddlHorario.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;


        }
        private void ListarTripulacion()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetAllTripulacion"; // Cambia el nombre del procedimiento almacenado según tu implementación
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Crear una tabla de datos para almacenar los resultados del procedimiento almacenado
                    DataTable dtTripulacion = new DataTable();
                    dtTripulacion.Load(reader);

                    // Asignar los datos al GridView
                    gvTripulacion.DataSource = dtTripulacion;
                    gvTripulacion.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Manejar errores de conexión o consulta
                    // Por ejemplo, puedes mostrar un mensaje de error o registrar el error en un archivo de registro
                }
            }
        }



    }
}
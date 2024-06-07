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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "warning", "Swal.fire('Advertencia', 'Debe ingresar un nombre', 'warning');", true);
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
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(numeroId) || string.IsNullOrWhiteSpace(celular) || string.IsNullOrWhiteSpace(correo))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "warning", "Swal.fire('Advertencia', 'Por favor, complete todos los campos obligatorios.', 'warning');", true);
                    return;
                }

                // Validar formato de correo electrónico
                if (!IsValidEmail(correo))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'El formato del correo electrónico no es válido.', 'error');", true);
                    return;
                }
                if (UsuarioExiste(numeroId))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'El usuario ya existe.', 'error');", true);
                    return;
                }

                // Crear y abrir conexión
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
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
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetAllTripulacion"; 
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
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected void gvTripulacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int idTripulacion = Convert.ToInt32(gvTripulacion.DataKeys[rowIndex].Value);

                try
                {
                    // Crear y abrir conexión
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_DeleteTripulacion";
                        cmd.Parameters.AddWithValue("@IdTripulacion", idTripulacion);
                        cmd.Connection = conn;

                        try
                        {
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            // Verificar si se realizaron cambios en la base de datos
                            if (rowsAffected > 0)
                            {
                                // La eliminación fue exitosa
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'Se eliminó el tripulante correctamente.', 'success');", true);
                                ListarTripulacion();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al eliminar el tripulante.', 'error');", true);
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    // Actualizar la lista de tripulación después de eliminar
                    ListarTripulacion();
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra durante el proceso de eliminación
                    Console.WriteLine("Error al eliminar el tripulante: " + ex.Message);
                }
            }
            if (e.CommandName == "Edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int idTripulacion = Convert.ToInt32(gvTripulacion.DataKeys[rowIndex].Value);
                // Aquí puedes usar el ID del tripulante (idTripulacion) para ejecutar tu lógica de edición
                // Por ejemplo, redirigir a una página donde se pueda editar la información del tripulante.
            }
        }

        protected void gvTripulacion_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int IdTripulante = Convert.ToInt32(btn.CommandArgument);
            DataTable dtTripulante = ObtenerTripulantePorId(IdTripulante);

            // Verificamos si se encontraron datos en el DataTable
            if (dtTripulante.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarPanelScript", "mostrarPanel();", true);
                ddlRol.SelectedValue = dtTripulante.Rows[0]["Rol"].ToString();
                txtNombre.Text = dtTripulante.Rows[0]["Nombre"].ToString();
                txtNumIdentificacion.Text = dtTripulante.Rows[0]["NumeroId"].ToString();
                ddlTipoIdentificacion.Text = dtTripulante.Rows[0]["TipoId"].ToString();
                txtCelular.Text = dtTripulante.Rows[0]["Celular"].ToString();
                txtCorreo.Text = dtTripulante.Rows[0]["Correo"].ToString();
                ddlEstado.Text = dtTripulante.Rows[0]["Estado"].ToString();
                ddlHorario.Text = dtTripulante.Rows[0]["Hoario"].ToString();
            }
            else
            {
                // Manejar el caso en el que no se encuentren datos para el ID del vuelo proporcionado
            }
        }
        private DataTable ObtenerTripulantePorId(int Idtripulante)
        {
            DataTable dtTripulacion = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetAllTripulacionID";
                cmd.Parameters.AddWithValue("@IdTripulante", Idtripulante);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Cargar los resultados en un DataTable
                    dtTripulacion.Load(reader);

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return dtTripulacion;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los controles de entrada
                Button btn = (Button)sender;
                int idTripulacion = Convert.ToInt32(IDTripulante.Value);
                string rol = ddlRol.SelectedValue;
                string nombre = txtNombre.Text;
                string numeroId = txtNumIdentificacion.Text;
                string tipoId = ddlTipoIdentificacion.SelectedValue;
                int celular = Convert.ToInt32(txtCelular.Text);
                string correo = txtCorreo.Text;
                byte estado = Convert.ToByte(ddlEstado.SelectedValue);
                TimeSpan horario = TimeSpan.Parse(ddlHorario.SelectedValue);

                // Llamar al procedimiento almacenado para actualizar la información del tripulante
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateTripulacion", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdTripulacion", idTripulacion);
                    cmd.Parameters.AddWithValue("@Rol", rol);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@NumeroId", numeroId);
                    cmd.Parameters.AddWithValue("@TipoId", tipoId);
                    cmd.Parameters.AddWithValue("@Celular", celular);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@Horario", horario);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    // Verificar si se realizaron cambios en la base de datos
                    if (rowsAffected > 0)
                    {
                        // La actualización fue exitosa
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'La actualización fue exitosa.', 'success');", true);
                        LimpiarCampos(); // Función para limpiar los campos después de la actualización
                                         // Puedes agregar aquí cualquier otra lógica necesaria después de la actualización exitosa
                        ListarTripulacion();
                    }
                    else
                    {
                        // No se realizó ninguna actualización
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el tripulante.', 'error');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el tripulante.', 'error');", true);
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool UsuarioExiste(string NumeroID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tripulacion WHERE NumeroID = @NumeroID", conn);
                cmd.Parameters.AddWithValue("@NumeroID", NumeroID);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();

                return count > 0;
            }
        }
    }
}
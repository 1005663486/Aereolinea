using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aereolinea
{
    public partial class Aeronaves : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListarAeronaves();
        }

        protected void btnGuardarAeronaves_Click(object sender, EventArgs e)
        {
            string modelo = ddlModelo.SelectedValue;
            DateTime fecha = DateTime.Parse(txtfecha.Text);
            string codigo = txtCodigo.Text;
            string fabricante = txtFabricante.Text;
            int capacidad = int.Parse(txtCapacidad.Text);
            int estado = Convert.ToInt32(ddlEstado.SelectedValue);

            if (string.IsNullOrEmpty(modelo) || string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(fabricante))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Todos los campos son obligatorios.', 'error');", true);
            }
            else
            {
                CrearAeronave(modelo, fecha, codigo, fabricante, capacidad, estado);
            }
        }

        private void CrearAeronave(string modelo, DateTime fechaAdquision, string codigo, string fabricante, int capacidad, int estado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "sp_CreateAeronave",
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Fecha", fechaAdquision);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Fabricante", fabricante);
                    cmd.Parameters.AddWithValue("@Capacidad", capacidad);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'Se ha creado la aeronave correctamente.', 'success');", true);
                LimpiarCampos();
                ListarAeronaves();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", $"Swal.fire('Error', 'Hubo un error al crear la aeronave: {ex.Message}', 'error');", true);
            }
        }

        private void LimpiarCampos()
        {
            ddlModelo.SelectedIndex = 0;
            txtfecha.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtFabricante.Text = string.Empty;
            txtCapacidad.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
        }

        private void ListarAeronaves()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "sp_GetAllAeronaves",
                    Connection = conn
                };

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dtAeronaves = new DataTable();
                    dtAeronaves.Load(reader);

                    gvAeronaves.DataSource = dtAeronaves;
                    gvAeronaves.DataBind();
                    reader.Close();
                }
                catch (Exception ex)
                {

                }
            }
        }


        protected void gvAeronaves_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int idAeronave = Convert.ToInt32(gvAeronaves.DataKeys[rowIndex].Value);

                try
                {
                    // Crear y abrir conexión
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_DeleteAeronave";
                        cmd.Parameters.AddWithValue("@IdAeronave", idAeronave);
                        cmd.Connection = conn;

                        try
                        {
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            // Verificar si se realizaron cambios en la base de datos
                            if (rowsAffected > 0)
                            {
                                // La eliminación fue exitosa
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'Se eliminó la aeronave correctamente.', 'success');", true);
                                ListarAeronaves();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al eliminar la aeronave.', 'error');", true);
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    // Actualizar la lista de aeronaves después de eliminar
                    ListarAeronaves();
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra durante el proceso de eliminación
                    Console.WriteLine("Error al eliminar la aeronave: " + ex.Message);
                }
            }
            if (e.CommandName == "Edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int idAeronave = Convert.ToInt32(gvAeronaves.DataKeys[rowIndex].Value);
                // Aquí puedes usar el ID de la aeronave (idAeronave) para ejecutar tu lógica de edición
                // Por ejemplo, redirigir a una página donde se pueda editar la información de la aeronave.
            }
        }

        protected void gvAeronaves_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int IdAeronave = Convert.ToInt32(btn.CommandArgument);
            DataTable dtAeronave = ObtenerAeronavePorId(IdAeronave);

            // Verificamos si se encontraron datos en el DataTable
            if (dtAeronave.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarPanelScript", "mostrarPanel();", true);
                ddlModelo.SelectedValue = dtAeronave.Rows[0]["modelo"].ToString();
                txtfecha.Text = ((DateTime)dtAeronave.Rows[0]["Fecha"]).ToString("yyyy-MM-dd");
                txtCodigo.Text = dtAeronave.Rows[0]["Codigo"].ToString();
                txtFabricante.Text = dtAeronave.Rows[0]["Fabricante"].ToString();
                txtCapacidad.Text = dtAeronave.Rows[0]["Capacidad"].ToString();
                ddlEstado.Text = dtAeronave.Rows[0]["Estado"].ToString();
                IDAeronave.Value = dtAeronave.Rows[0]["IdAeronave"].ToString();
                // Luego, abrimos el modal
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Vuelos", "abrirModal();", true);
            }
            else
            {
                // Manejar el caso en el que no se encuentren datos para el ID del vuelo proporcionado
            }
        }

        private DataTable ObtenerAeronavePorId(int IdAeronave)
        {
            DataTable dtAeronave = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetAllAeronaveID";
                cmd.Parameters.AddWithValue("@IdAeronave", IdAeronave);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Cargar los resultados en un DataTable
                    dtAeronave.Load(reader);

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Manejar errores de conexión o consulta
                    // Por ejemplo, puedes mostrar un mensaje de error o registrar el error en un archivo de registro
                }
            }

            return dtAeronave;
        }

        protected void btnEditarAeronaves_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los controles de entrada
                Button btn = (Button)sender;
                int idAeronave = Convert.ToInt32(IDAeronave.Value);
                string modelo = ddlModelo.SelectedValue;
                string fecha = txtfecha.Text;
                string codigo = txtCodigo.Text;
                string fabricante = txtFabricante.Text;
                int capacidad = int.Parse(txtCapacidad.Text);
                int estado = Convert.ToInt32(ddlEstado.SelectedValue);

                // Llamar al procedimiento almacenado para actualizar la información del aeronave
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateAeronave", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAeronave", idAeronave);
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Fabricante", fabricante);
                    cmd.Parameters.AddWithValue("@Capacidad", capacidad);
                    cmd.Parameters.AddWithValue("@Estado", estado);

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
                        ListarAeronaves();
                    }
                    else
                    {
                        // No se realizó ninguna actualización
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el aeronave.', 'error');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el aeronave.', 'error');", true);
            }
        }
    }
}
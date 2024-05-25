using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Aereolinea
{
    public partial class _Vuelos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
            }
            ListarVuelos();
        }
        private void ListarVuelos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Listar_Vuelos";
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Crear una tabla de datos para almacenar los resultados del procedimiento almacenado
                    DataTable dtVuelos = new DataTable();
                    dtVuelos.Load(reader);

                    // Asignar los datos al ListView
                    LVVuelos.DataSource = dtVuelos;
                    LVVuelos.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Manejar errores de conexión o consulta
                    // Por ejemplo, puedes mostrar un mensaje de error o registrar el error en un archivo de registro
                }
            }
        }
        protected void Ver_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idVuelo = Convert.ToInt32(btn.CommandArgument);
            DataTable dtVuelo = ObtenerVueloPorId(idVuelo);

            // Verificamos si se encontraron datos en el DataTable
            if (dtVuelo.Rows.Count > 0)
            {
                // Obtenemos el valor de la columna 'Aeronave' del primer registro (suponiendo que solo haya un registro)
                txtAeronave.Text = dtVuelo.Rows[0]["Aeronave"].ToString();
                txtFechaSalida.Text = dtVuelo.Rows[0]["FechaSalida"].ToString();
                txtFechaLlegada.Text = dtVuelo.Rows[0]["FechaLlegada"].ToString();
                txtOrigen.Text = dtVuelo.Rows[0]["Origen"].ToString();
                txtDestino.Text = dtVuelo.Rows[0]["Destino"].ToString();
                txtPasajeros.Text = dtVuelo.Rows[0]["CantidadPasajeros"].ToString();
                txtRuta.Text = dtVuelo.Rows[0]["Ruta"].ToString();
                txtSillas.Text = dtVuelo.Rows[0]["Sillas"].ToString();
                txtPuertaAbordaje.Text = dtVuelo.Rows[0]["PuertaAbordaje"].ToString();
                txtTripulante.Text = dtVuelo.Rows[0]["Tripulante"].ToString();
                txtEstado.Text= dtVuelo.Rows[0]["Estado"].ToString();
                txtIdVuelo.Text= dtVuelo.Rows[0]["IdVuelo"].ToString();
                // Luego, abrimos el modal
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Vuelos", "abrirModal();", true);
            }
            else
            {
                // Manejar el caso en el que no se encuentren datos para el ID del vuelo proporcionado
            }
        }
        // Método para llamar al procedimiento almacenado y obtener el vuelo por su ID
        private DataTable ObtenerVueloPorId(int idVuelo)
        {
            DataTable dtVuelo = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetAllVuelosID";
                cmd.Parameters.AddWithValue("@IdVuelo", idVuelo);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Cargar los resultados en un DataTable
                    dtVuelo.Load(reader);

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Manejar errores de conexión o consulta
                    // Por ejemplo, puedes mostrar un mensaje de error o registrar el error en un archivo de registro
                }
            }

            return dtVuelo;
        }  
        protected void Editar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los controles de entrada
                int IdVuelo = Convert.ToInt32(txtIdVuelo.Text);
                string aeronave = txtAeronave.Text;
                DateTime fechaSalida = Convert.ToDateTime(txtFechaSalida.Text);
                DateTime fechaLlegada = Convert.ToDateTime(txtFechaLlegada.Text);
                int estado = Convert.ToInt32(txtEstado.Text);
                int cantidadPasajeros = Convert.ToInt32(txtPasajeros.Text);
                string destino = txtDestino.Text;
                string ruta = txtRuta.Text;
                int sillas = Convert.ToInt32(txtSillas.Text);
                string puertaAbordaje = txtPuertaAbordaje.Text;
                string origen = txtOrigen.Text;
                string tripulante = txtTripulante.Text;

                // Llamar al procedimiento almacenado para actualizar el vuelo
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateVuelo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdVuelo", IdVuelo);
                    cmd.Parameters.AddWithValue("@Aeronave", aeronave);
                    cmd.Parameters.AddWithValue("@FechaSalida", fechaSalida);
                    cmd.Parameters.AddWithValue("@FechaLlegada", fechaLlegada);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@CantidadPasajeros", cantidadPasajeros);
                    cmd.Parameters.AddWithValue("@Destino", destino);
                    cmd.Parameters.AddWithValue("@Ruta", ruta);
                    cmd.Parameters.AddWithValue("@Sillas", sillas);
                    cmd.Parameters.AddWithValue("@PuertaAbordaje", puertaAbordaje);
                    cmd.Parameters.AddWithValue("@Origen", origen);
                    cmd.Parameters.AddWithValue("@Tripulante", tripulante);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        // Verificar si se realizaron cambios en la base de datos
                        if (rowsAffected > 0)
                        {
                            // La actualización fue exitosa
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'La actualización fue exitosa.', 'success');", true);
                            ListarVuelos();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el vuelo.', 'error');", true);
                        }
                        ListarVuelos();
                    }
                    catch (Exception ex)
                    {
                        // Manejar errores de conexión o consulta
                        // Por ejemplo, puedes mostrar un mensaje de error o registrar el error en un archivo de registro
                    }
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el vuelo.', 'error');", true);
            }
          
        }
        protected void Eliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            string idVuelo = btnEliminar.CommandArgument;
            try
            {
                // Crear y abrir conexión
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_DeleteVuelo";
                    cmd.Parameters.AddWithValue("@IdVuelo", idVuelo);
                    cmd.Connection = conn;

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        // Verificar si se realizaron cambios en la base de datos
                        if (rowsAffected > 0)
                        {
                            // La actualización fue exitosa
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'Se eliminó el vuelo correctamente.', 'success');", true);
                            ListarVuelos();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al eliminar el vuelo.', 'error');", true);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                // Actualizar la lista de vuelos después de eliminar el vuelo
                ListarVuelos();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante el proceso de eliminación
                // Aquí puedes mostrar un mensaje de error al usuario, hacer un registro de errores, etc.
                Console.WriteLine("Error al eliminar el vuelo: " + ex.Message);
            }
        }

    }
}

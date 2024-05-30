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
    public partial class _HistorialMantenimiento : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarMantenimientos();
                CargarAeronavesActivas();
            }
        }

        private void ListarMantenimientos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Listar_Mantenimientos";
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dtMantenimientos = new DataTable();
                    dtMantenimientos.Load(reader);

                    LVHistorialMantenimiento.DataSource = dtMantenimientos;
                    LVHistorialMantenimiento.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Manejar errores de conexión o consulta
                }
            }
        }

        private void CargarAeronavesActivas()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Listar_AeronavesActivas";
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dtAeronaves = new DataTable();
                    dtAeronaves.Load(reader);
                    ddlAeronavesActivas.DataSource = dtAeronaves;
                    ddlAeronavesActivas.DataTextField = "NombreAeronave"; // Nombre de la columna que deseas mostrar
                    ddlAeronavesActivas.DataValueField = "AeronaveId"; // Nombre de la columna que contiene el ID
                    ddlAeronavesActivas.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Manejar errores de conexión o consulta
                }
            }
        }
        protected void BuscarMantenimientos_Click(object sender, EventArgs e)
        {
            int aeronaveId = Convert.ToInt32(ddlAeronavesActivas.SelectedValue);
            ListarMantenimientos();
        }

        protected void Ver_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int IdAeronave = Convert.ToInt32(btn.CommandArgument);
            DataTable dtMantenimiento = ObtenerMantenimientoPorIdAeronave(IdAeronave);

            if (dtMantenimiento.Rows.Count > 0)
            {
                txtAeronave.Text = dtMantenimiento.Rows[0]["Aeronave"].ToString();
                txtFechaInicio.Text = dtMantenimiento.Rows[0]["FechaInicio"].ToString();
                txtFechaFin.Text = dtMantenimiento.Rows[0]["FechaFin"].ToString();
                txtTipoMantenimiento.Text = dtMantenimiento.Rows[0]["TipoMantenimiento"].ToString();
                txtResponsable.Text = dtMantenimiento.Rows[0]["Responsable"].ToString();
                txtEstado.Text = dtMantenimiento.Rows[0]["Estado"].ToString();
                txtObservaciones.Text = dtMantenimiento.Rows[0]["Observaciones"].ToString();
                txtIdMantenimiento.Text = dtMantenimiento.Rows[0]["IdMantenimiento"].ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Mantenimiento", "abrirModal();", true);
            }
            else
            {
                // Manejar el caso en el que no se encuentren datos para el ID del mantenimiento proporcionado
            }
        }

        private DataTable ObtenerMantenimientoPorIdAeronave(int IdAeronave)
        {
            DataTable dtMantenimiento = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetMantenimientoID";
                cmd.Parameters.AddWithValue("@IdAeronave", IdAeronave);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    dtMantenimiento.Load(reader);

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Manejar errores de conexión o consulta
                }
            }

            return dtMantenimiento;
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            try
            {
                int idMantenimiento = Convert.ToInt32(txtIdMantenimiento.Text);
                string aeronave = txtAeronave.Text;
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
                DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);
                string tipoMantenimiento = txtTipoMantenimiento.Text;
                string responsable = txtResponsable.Text;
                string estado = txtEstado.Text;
                string observaciones = txtObservaciones.Text;

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateMantenimiento", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
                    cmd.Parameters.AddWithValue("@Aeronave", aeronave);
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@TipoMantenimiento", tipoMantenimiento);
                    cmd.Parameters.AddWithValue("@Responsable", responsable);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@Observaciones", observaciones);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'La actualización fue exitosa.', 'success');", true);
                            ListarMantenimientos();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el mantenimiento.', 'error');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar errores de conexión o consulta
                    }
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el mantenimiento.', 'error');", true);
            }
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            int idMantenimiento = Convert.ToInt32(btnEliminar.CommandArgument);

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_DeleteMantenimiento";
                    cmd.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
                    cmd.Connection = conn;

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'Se eliminó el mantenimiento correctamente.', 'success');", true);
                            ListarMantenimientos();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al eliminar el mantenimiento.', 'error');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar errores de conexión o consulta
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el mantenimiento: " + ex.Message);
            }
        }
    }
}

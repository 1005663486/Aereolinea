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

                    if (reader.HasRows)
                    {
                        DataTable dtAeronaves = new DataTable();
                        dtAeronaves.Load(reader);

                        ddlAeronavesActivas.DataSource = dtAeronaves;
                        ddlAeronavesActivas.DataTextField = "Codigo"; // Nombre de la columna que deseas mostrar
                        ddlAeronavesActivas.DataValueField = "IdAeronave"; // Nombre de la columna que contiene el ID
                        ddlAeronavesActivas.DataBind();

                        ddlAeronavesActivas.Items.Insert(0, new ListItem("Seleccione una aeronave", "0"));
                    }
                    else
                    {
                        ddlAeronavesActivas.Items.Insert(0, new ListItem("No hay aeronaves activas", "0"));
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void ListarMantenimientos(int? aeronaveId = null)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarHistorialMantenimientoPorAeronave";
                cmd.Connection = conn;

                if (aeronaveId.HasValue && aeronaveId.Value > 0)
                {
                    cmd.Parameters.AddWithValue("@IdAeronave", aeronaveId.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IdAeronave", DBNull.Value);
                }

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

        protected void BuscarMantenimientos_Click(object sender, EventArgs e)
        {
            int aeronaveId = Convert.ToInt32(ddlAeronavesActivas.SelectedValue);
            if (aeronaveId > 0)
            {
                ListarMantenimientos(aeronaveId);
            }
            else
            {
                ListarMantenimientos(); // Mostrar todos los mantenimientos si no se selecciona ninguna aeronave
            }

            if (LVHistorialMantenimiento.Items.Count == 0)
            {
                lblNoHistorial.Visible = true; // Mostrar el mensaje
            }
            else
            {
                lblNoHistorial.Visible = false; // Ocultar el mensaje si hay datos
            }
        }
        protected void Ver_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int IdMantenimiento = Convert.ToInt32(btn.CommandArgument);
            DataTable dtMantenimiento = ObtenerMantenimientoPorId(IdMantenimiento);

            System.Diagnostics.Debug.WriteLine("ID del Mantenimiento: " + IdMantenimiento);

            if (dtMantenimiento.Rows.Count > 0)
            {
                txtModelo.Text = dtMantenimiento.Rows[0]["Aeronave"].ToString();
                txtFechaInicio.Text = dtMantenimiento.Rows[0]["FechaInicio"].ToString();
                txtFechaFin.Text = dtMantenimiento.Rows[0]["FechaFin"].ToString();
                txtTipoMantenimiento.Text = dtMantenimiento.Rows[0]["tipoMantenimiento"].ToString();
                txtResponsable.Text = dtMantenimiento.Rows[0]["Responsable"].ToString();
                txtEstado.Text = dtMantenimiento.Rows[0]["Estado"].ToString();
                txtCodigo.Text = dtMantenimiento.Rows[0]["Codigo"].ToString();
                txtObservaciones.Text = dtMantenimiento.Rows[0]["Observaciones"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalScript", "abrirModal();", true);

            }
            else
            {
                // Manejar el caso en el que no se encuentren datos para el ID del mantenimiento proporcionado
            }
        }
        private DataTable ObtenerMantenimientoPorId(int IdMantenimiento)
        {
            DataTable dtMantenimiento = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetMantenimientoID";
                cmd.Parameters.AddWithValue("@IdMantenimiento", IdMantenimiento);
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
        private DataTable ObtenerMantenimientoPorIdAeronave(int IdAeronave)
        {
            DataTable dtMantenimiento = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarHistorialMantenimientoPorAeronave";
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
            Button btn = (Button)sender;
            int IdAeronave = Convert.ToInt32(btn.CommandArgument);
            DataTable dtMantenimiento = ObtenerMantenimientoPorIdAeronave(IdAeronave);

            // Verificamos si se encontraron datos en el DataTable
            if (dtMantenimiento.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarPanelScript", "mostrarPanel();", true);
                txtFechaInicio.Text = dtMantenimiento.Rows[0]["FechaInicio"].ToString();
                txtFechaFin.Text = dtMantenimiento.Rows[0]["FechaFin"].ToString();
                txtTipoMantenimiento.Text = dtMantenimiento.Rows[0]["tipoMantenimiento"].ToString();
                txtResponsable.Text = dtMantenimiento.Rows[0]["Responsable"].ToString();
                txtEstado.Text = dtMantenimiento.Rows[0]["Estado"].ToString();
                txtCodigo.Text = dtMantenimiento.Rows[0]["Codigo"].ToString();
                txtObservaciones.Text = dtMantenimiento.Rows[0]["Observaciones"].ToString();
            }
            else
            {
                // Manejar el caso en el que no se encuentren datos para el ID del vuelo proporcionado
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
        protected void Agregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mantenimiento.aspx");
        }
    }
    
}

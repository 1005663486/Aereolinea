using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace Aereolinea
{
    public partial class _Mantenimiento : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAeronavesActivas();
                CargarPersonalMant();
            }
        }
        private void CargarAeronavesActivas()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "Listar_AeronavesActivas",
                    Connection = conn
                };

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

                        ViewState["Aeronaves"] = dtAeronaves;
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
        private void CargarPersonalMant()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "Listar_PersonalMant",
                    Connection = conn
                };

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        DataTable dtResponsables = new DataTable();
                        dtResponsables.Load(reader);

                        ddlResponsable.DataSource = dtResponsables;
                        ddlResponsable.DataTextField = "Nombre"; // Nombre de la columna que deseas mostrar
                        ddlResponsable.DataValueField = "IdTripulacion"; // Nombre de la columna que contiene el ID
                        ddlResponsable.DataBind();

                        ddlResponsable.Items.Insert(0, new ListItem("Seleccione el responsable", "0"));

                        ViewState["Responsables"] = dtResponsables;
                    }
                    else
                    {
                        ddlResponsable.Items.Insert(0, new ListItem("No hay responsables activos", "0"));
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        protected void Guardar_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = (Button)sender;

                DateTime fechaInicio = DateTime.Parse(txtFechaInicio.Text);
                DateTime fechaFin = DateTime.Parse(txtFechaFin.Text);
                string tipoMantenimiento = ddlTipoMantenimiento.SelectedValue;
                string responsable = ddlResponsable.SelectedValue;
                string codigo = ddlAeronavesActivas.SelectedValue;
                string estado = ddlEstado.SelectedValue;
                string observaciones = txtObservaciones.Text;

                Debug.WriteLine("FechaInicio: " + fechaInicio);
                Debug.WriteLine("FechaFin: " + fechaFin);
                Debug.WriteLine("TipoMantenimiento: " + tipoMantenimiento);
                Debug.WriteLine("Responsable: " + responsable);
                Debug.WriteLine("Codigo: " + codigo);
                Debug.WriteLine("Estado: " + estado);
                Debug.WriteLine("Observaciones: " + observaciones);

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AviacolDBConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_CreateMantenimiento", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@TipoMantenimiento", tipoMantenimiento);
                    cmd.Parameters.AddWithValue("@IdTripulante", responsable);
                    cmd.Parameters.AddWithValue("@IdAeronave", codigo);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@Observaciones", observaciones);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        // Verificar si se realizaron cambios en la base de datos
                        if (rowsAffected > 0)
                        {
                            // La actualización fue exitosa
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "Swal.fire('¡Éxito!', 'La actualización fue exitosa.', 'success');", true);
                            CargarAeronavesActivas();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "Swal.fire('Error', 'Hubo un error al actualizar el vuelo.', 'error');", true);
                        }
                        CargarAeronavesActivas();
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
        protected void VerHistorial_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistorialMantenimiento.aspx");
        }

    }
}

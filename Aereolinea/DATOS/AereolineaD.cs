using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace Aereolinea.DATOS
{
    public class AereolineaD
    {
        Conexion conn = new Conexion();
        private List<SqlParameter> lista = null;
        public DataTable Listar_Vuelos(string ConnectionString)
        {
            lista = new List<SqlParameter>();
            string procedimiento = "[Listar_Vuelos]";
            lista.Clear();
           // lista.Add(new SqlParameter("@agno", año));

            return new Conexion().EjecutarProcedimientoAlmacenado(procedimiento, lista, ConnectionString);
        }
    }
   
}
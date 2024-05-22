using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Aereolinea.DATOS;
namespace Aereolinea.NEGOCIO
{
    public class AereolineaN
    {
        AereolineaN aereolinaN = new AereolineaN();
        public DataTable Listar_Vuelos(string ConnectionString)
        {
            return aereolinaN.Listar_Vuelos(ConnectionString);
        }
    }
}
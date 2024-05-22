using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Net;
using System.Configuration;

namespace Aereolinea.DATOS
{
    public class Conexion
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Conexion()
        {
        }
        /// <summary>
        /// Ejecuta un procedimiento almacenado 
        /// </summary>
        /// <param name="NombreSP">Nombre del procedimiento almacenado</param>
        /// <param name="Parametros">Lista de parámetros del tipo List SqlParamter </param>
        /// <param name="ConexionString">Cadena de conexión a la base de datos</param>
        /// <returns></returns>
        public DataTable EjecutarProcedimientoAlmacenado(string NombreSP, List<SqlParameter> Parametros, string ConnetionString)
        {
            try
            {
                ConnetionString = ConfigurationManager.ConnectionStrings[ConnetionString] != null ? ConfigurationManager.ConnectionStrings[ConnetionString].ToString() : ConnetionString;
                SqlConnection cn = new SqlConnection(ConnetionString);
                SqlDataAdapter da = null;
                SqlTransaction tr = null; //transaccion actual
                SqlCommand cmd;
                DataSet ds;
                cn.Open();//abrir conexion
                          //Ejecutar el procedimiento como una transacción
                tr = cn.BeginTransaction();
                cmd = cn.CreateCommand();
                cmd.Connection = cn;
                cmd.Transaction = tr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = NombreSP;

                ds = new DataSet();
                try
                {
                    //Determinar si hay parámetros en el procedimiento
                    if (Parametros != null)
                    {
                        //Añadir cada parámetro al procedimiento
                        foreach (SqlParameter item in Parametros)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }//if (Parametros != null)
                    //SqlAdapter utiliza el SqlCommand para llenar el Dataset
                    da = new SqlDataAdapter(cmd);
                    //ejecuta procedimiento y llena el dataset (En lugar de cmd.ExecuteNonQuery(), Este a su vez retorna la cantidad de filas afectadas.)
                    da.SelectCommand.CommandTimeout = 180;//Máximo 3 minutos
                    da.Fill(ds);
                    //Completar la transacción
                    tr.Commit();
                    tr.Dispose();
                    da.Dispose();
                }
                catch (Exception ex)
                {
                    tr.Rollback();//Devolver la ejecución de la transacción
                    throw (ex);
                }
                finally
                {
                    //Cerrar conexión con o sin error
                    if (cn.State.Equals(ConnectionState.Open))
                    {
                        da.Dispose();//destruir el DataAdapter
                        da = null;
                        tr.Dispose();//destruir la transaccion
                        tr = null;
                        cmd.Parameters.Clear();
                        cmd.Dispose(); //destruir el comando
                        cmd = null;
                        cn.Close(); //cerrar conexión
                        cn.Dispose(); //Eliminar conexión
                        cn = null;
                    }
                }
               
                Int32 LastChild = ds.Tables.Count - 1;
                if (LastChild == -1)
                {
                    return new DataTable();
                }
                return ds.Tables[LastChild];//Devolver el DataTable
            }
            catch (Exception ex)
            {
                throw (ex);

            }//try

        }//EjecutarProcedimientoAlmacenado

        public static void mDesconectar(SqlCommand command)
        {
            if (command != null && command.Connection.State == ConnectionState.Open)
            {
                command.Connection.Close();
            }
        }
    }
}
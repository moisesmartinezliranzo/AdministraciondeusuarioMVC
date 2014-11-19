using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Administracion_de_usuarios.Models
{
    public class MyConnection
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(Administracion_de_usuarios.Properties.Resources.MyConectionString);
                connection.Open();
                return connection;
            }
            catch
            {                
                return null;
            }
        }
    }
}
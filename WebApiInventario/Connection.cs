using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiInventario
{
    public class Connection
    {
        public static SqlConnection getConnection()
        {
            SqlConnection ocon = new SqlConnection("Data Source=DESKTOP-3AO6C47;Initial Catalog=BaseDatosInventario;Integrated Security=True;");
            ocon.Open();
            return ocon;
        }
    }
}
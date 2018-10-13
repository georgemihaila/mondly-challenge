using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_api.Entities
{
    public static class SqlExpressHelper
    {
        static SqlExpressHelper()
        {
            Connection = Connection ?? new SqlConnection(ConnectionString);
            Connection.Open();
        }

        private static string ConnectionString => @"Data Source=DESKTOP-CE0B3GV\SQLEXPRESS;Initial Catalog=mondly-db;Integrated Security=SSPI";

        private static SqlConnection Connection { get; set; }
    }
}

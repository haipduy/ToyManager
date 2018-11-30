using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiTTLibrary
{
    class Connection
    {
        public static SqlConnection getConnection()
        {
            SqlConnection sqlConnection;
            try
            {
                sqlConnection = new SqlConnection("server=DUYPNKSE62427\\SQLEXPRESS5;database=ToyManagement;uid=sa;pwd=1");

            }
            catch (Exception)
            {
                throw;
            }
            return sqlConnection;
        }
    }
}

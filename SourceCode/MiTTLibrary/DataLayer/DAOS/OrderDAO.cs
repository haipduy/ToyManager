using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace MiTTLibrary
{
    public class OrderDAO : ICRUD
    {
        private readonly string SELECT_ALL_SQL = "select * from tblorder";
        private readonly string SELECT_ALL_USER_ID_FROM_ACCOUNT_TABLE_SQL = "select UserId from tblaccount";
        SqlConnection sqlConnection;
        //SqlCommand sqlCommand
        
        public OrderDAO()
        {
            sqlConnection = Connection.getConnection();
        }
        public DataTable readAll()
        {
            DataTable dataTable;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SELECT_ALL_SQL, sqlConnection);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataTable;
        }
        public bool update(DataTable dataTable)
        {
            bool success = false;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SELECT_ALL_SQL, sqlConnection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            int updatedRows = sqlDataAdapter.Update(dataTable);
            success = (updatedRows > 0);
            return success;
        }
        public DataTable readAllUserIdFromAccountTable()
        {
            DataTable dataTable;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SELECT_ALL_USER_ID_FROM_ACCOUNT_TABLE_SQL, sqlConnection);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataTable;
        }
    }
}

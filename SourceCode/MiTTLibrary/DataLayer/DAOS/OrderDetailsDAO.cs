using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace MiTTLibrary
{
    public class OrderDetailsDAO : ICRUD
    {
        private readonly string SELECT1_BY_ORDER_ID_SQL = "select tblOrderDetails.productId,Product.name,tblOrderDetails.quantity,Product.price from tblOrderDetails, Product where tblOrderDetails.orderId=@orderId and tblOrderDetails.productId=Product.id ";
        private readonly string SELECT_BY_ORDER_ID_SQL = "select * from tblOrderDetails where orderId=@orderId";
        private readonly string UPDATE_BY_ORDER_ID_SQL = "update tblOrderDetails set ProductId=@ProductId,Quantity=@Quantity where OrderId=@OrderId";
        private readonly string SELECT_BY_PRODUCT_ID_SQL = "select * from tblOrderDetails where productId=@productId";
        private readonly string SELECT_ALL_SQL = "select * from tblorderdetails";
        private readonly string SELECT_ALL_PRODUCT_ID_FROM_PRODUCT_TABLE_SQL = "select ID from Product";
        private readonly string COLUMN0 = "OrderId";
        private readonly string COLUMN1 = "ProductId";
        private readonly string COLUMN2 = "Quantity";
        SqlConnection sqlConnection;
        

        public OrderDetailsDAO()
        {
            sqlConnection = Connection.getConnection();
        }
        public DataTable readAll()
        {
            DataTable dataTable;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SELECT_ALL_SQL, sqlConnection);
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
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
        public DataTable readAllProductIdFromProductTable()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SELECT_ALL_PRODUCT_ID_FROM_PRODUCT_TABLE_SQL, sqlConnection);
            DataTable dataTable;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
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
        public DataTable read1ByOrderId(string orderId)
        {
            SqlCommand sqlCommand = new SqlCommand(SELECT1_BY_ORDER_ID_SQL, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@orderId", orderId);
            DataTable dataTable = new DataTable();
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
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

        public DataTable readByOrderId(string orderId)
        {
            SqlCommand sqlCommand = new SqlCommand(SELECT_BY_ORDER_ID_SQL, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@orderId", orderId);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
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
       
        public DataTable readByProductId(string productId)
        {
            SqlCommand sqlCommand = new SqlCommand(SELECT_BY_PRODUCT_ID_SQL, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@productId", productId);
            DataTable dataTable = new DataTable();
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
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
            int updatedRowCount = -1;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SELECT_ALL_SQL, sqlConnection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                updatedRowCount = sqlDataAdapter.Update(dataTable);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            success = updatedRowCount > 0;
            return success;
        }
        public bool updateProductQuantityFromProductTable(DataTable dataTable)
        {
            bool success = true;
            try
            {
                SqlCommand sqlCommand;
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sqlCommand = new SqlCommand("update product set quantity=@quantity where id=@id", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id", dataTable.Rows[i][0]);
                    sqlCommand.Parameters.AddWithValue("@quantity", dataTable.Rows[i][1]); 
                    success = success&&sqlCommand.ExecuteNonQuery() > 0;
                    
                }
                
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return success;
        }
        public DataTable readQuantityFromProductTableById(string productId)
        {
            DataTable dataTable;
            try
            {
                SqlCommand sqlCommand = new SqlCommand("select quantity from product where id=@id", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", productId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
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
        public DataTable selectProductIdAndQuantityFromProductTable()
        {
            DataTable dataTable;
            try
            {
                SqlCommand sqlCommand = new SqlCommand("select id,quantity from product", sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
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
        public DataTable selectQuantityByOrderDetails (string orderId,string productId)
        {
            DataTable dataTable;
            int filledRowCount;
            try
            {
                SqlCommand sqlCommand = new SqlCommand("select quantity from tblOrderDetails where OrderId=@OrderId and ProductId=@ProductId", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@OrderId", orderId);
                sqlCommand.Parameters.AddWithValue("@ProductId", productId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                dataTable = new DataTable();
                filledRowCount=sqlDataAdapter.Fill(dataTable);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return filledRowCount==0?null: dataTable;
        }
    }
}

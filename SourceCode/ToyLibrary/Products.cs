using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ToyLibrary
{
    public class Product
    {
        public string ProdictID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string SupllierName { get; set; }
        public string CategoryName { get; set; }
    }
     public class ProductDAO
    {
        string strConnection;
        public ProductDAO()
        {
            strConnection = GetConnectionString();
        }
        public String GetConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["ToyManagement"].ConnectionString;
            return strConnection;
        }
        public DataTable GetAllProduct()
        {
            string SQL = "select ID, Name, Price, Quantity,CategoryName,SupplierName,Description from Product p, TblCategory c, TblSupplier s where p.SupplierID=s.SupplierID AND p.CategoryID=c.CategoryID AND p.Status=1";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtProduct = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(dtProduct);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return dtProduct;
        }
        public DataTable GetAllProductLikeName(string name)
        {
            string SQL = "select * from Product Where Status=1 And Name like @Name";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@Name", "%"+name+"%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtProduct = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(dtProduct);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return dtProduct;
        }
        public bool removeProduct(string id)
        {
            bool result = false;
            string SQL = "UPDATE Product set Status=0 where ID=@ID";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public DataTable LoadCategory()
        {
            DataTable table = new DataTable();
            string sql = "Select CategoryName from TblCategory";
            SqlConnection con = new SqlConnection(strConnection);
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                Mydata.Fill(table);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return table;
        }
        public DataTable LoadSupllier()
        {
            DataTable table = new DataTable();
            string sql = "Select SupplierName from TblSupplier";
            SqlConnection con = new SqlConnection(strConnection);
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                Mydata.Fill(table);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return table;
        }
        public string GetCategoryID(string name)
        {
            string id = null;
            string SQL = "select CategoryID from TblCategory Where CategoryName=@Name";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@Name", name);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    id = rd["CategoryID"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return id;
        }
        public string GetSupllierID(string name)
        {
            string id = null;
            string SQL = "select SupplierID from TblSupplier Where SupplierName=@Name";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@Name", name);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    id = rd["SupplierID"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return id;
        }
        public bool AddNewProduct(string id, string name, string price, string quantity, string description, string categoryID, string SupID)
        {
            string sql = "Insert Product VALUES(@ID,@Name,@Price,@SupID,@CateID,@Quantity,@Des,@Status)";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@SupID", SupID);
            cmd.Parameters.AddWithValue("@CateID", categoryID);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Des", description);
            cmd.Parameters.AddWithValue("@Status", 1);
            int count = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            return (count > 0);
        }
        public string GetProductID(string id)
        {
            string ID = null;
            string SQL = "select ID from Product Where ID=@ID";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    ID = rd["ID"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return ID;
        }
        public bool UpdateProduct(string id, string name, string price, string quantity, string description, string categoryID, string SupID)
        {
            bool result = false;
            string SQL = "UPDATE Product set Name=@Name,Price=@Price,SupplierID=@SupplierID,CategoryID=@CategoryID,Quantity=@Quantity,Description=@Description where ID=@ID";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@SupplierID", SupID);
            cmd.Parameters.AddWithValue("@CategoryID", categoryID);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Description", description);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}

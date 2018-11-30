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
    public class SupllierDAO
    {
        string strConnection;

        public SupllierDAO()
        {
            strConnection = GetConnectionString();
        }
        public String GetConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["ToyManagement"].ConnectionString;
            return strConnection;
        }
        public DataTable GetallSupllier()
        {
            string SQL = "select * from TblSupplier";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtSup = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(dtSup);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return dtSup;
        }
        public bool InsertSupllier(string id, string name, string address, string phone)
        {
            int count = 0;
            string sql = "Insert TblSupplier VALUES(@ID,@Name,@Address,@Phone)";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Phone", phone);
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
        public bool UpdateSupllier(string id, string name, string address, string phone)
        {
            bool result = false;
            string SQL = "UPDATE TblSupplier set SupplierName=@Name,AddressItem=@Address,Phone=@Phone where SupplierID=@ID";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Phone", phone);
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
        public bool DeleteCategory(string id)
        {
            SqlConnection con = new SqlConnection(strConnection);
            string sql = "DELETE FROM TblSupplier WHERE SupplierID=@ID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", id);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }

        public string GetSupllierID(string id)
        {
            string SupID = null;
            string SQL = "select SupplierID from TblSupplier Where SupplierID=@ID";
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
                    SupID = rd["SupplierID"].ToString();
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
            return SupID;
        }
    }
}

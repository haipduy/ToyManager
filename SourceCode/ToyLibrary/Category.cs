using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ToyLibrary
{
    public class CategoryDAO
    {
        string strConnection;

        public CategoryDAO()
        {
            strConnection = GetConnectionString();
        }
        public String GetConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["ToyManagement"].ConnectionString;
            return strConnection;
        }
        public DataTable GetallCategory()
        {
            string SQL = "select * from TblCategory";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtCategory = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(dtCategory);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return dtCategory;
        }
        public bool InsertCategory(string id, string name)
        {
            int count = 0;
            string sql = "Insert TblCategory VALUES(@ID,@Name)";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
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
        public bool UpdateCategory(string id, string name)
        {
            bool result = false;
            string SQL = "UPDATE TblCategory set CategoryName=@Name  where CategoryID=@ID";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
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
            string sql = "DELETE FROM TblCategory WHERE CategoryID=@ID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", id);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }
        public string GetCategoryID(string id)
        {
            string CateID = null;
            string SQL = "select CategoryID from TblCategory Where CategoryID=@ID";
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
                    CateID = rd["CategoryID"].ToString();
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
            return CateID;
        }
    }
}

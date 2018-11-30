using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ToyLibrary
{
    public class Accounts
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public bool StatusAcc { get; set; }
    }
    public class Employee
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public DateTime StartDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
    }
    public class ManagerAccount
    {
        string strConnection;

        public ManagerAccount()
        {
            strConnection = GetConnectionString();
        }


        public String GetConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["ToyManagement"].ConnectionString;
            return strConnection;
        }
        public string CheckLogin(string username, string password)
        {
            string roleName = null;
            string sql = "select rol.RoleName from TblAccount acc, TblRole rol where acc.RoleID = rol.RoleID And acc.UserID = @Username AND acc.Password = @Password And acc.StatusAcc = 'True'";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    roleName = rd["RoleName"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return roleName;
        }
        public DataTable GetAllEmployee()
        {
            string SQL = "select UserID, FullName,StartDate,Address,Email,Phone,Sex from TblAccount where StatusAcc=1 And RoleID=2";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtEmployee = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(dtEmployee);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return dtEmployee;
        }
        public DataTable GetAllEmployeeLikeName(string name)
        {
            string SQL = "select * from TblAccount Where RoleID=2 And StatusAcc=1 And FullName like @Name";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
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
        public bool RemoveEmployee(string id)
        {
            bool result = false;
            string SQL = "UPDATE TblAccount set StatusAcc=0 where UserID=@ID";
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
        public bool UpdteEmployee(string id, string name, string startdate, string address, string email, string phone, string sex)
        {
            bool result = false;
            string SQL = "UPDATE TblAccount set   FullName=@Name, StartDate=@Start, Address=@Address, Email=@Email, Phone=@Phone, Sex=@Sex where UserID=@ID";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Start", startdate);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Sex", sex);
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
        public string GetID(string id)
        {
            string userID = null;
            string SQL = "select UserID from TblAccount Where UserID=@ID";
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
                    userID = rd["UserID"].ToString();
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
            return userID;
        }
        public bool AddNewEmployee(string id, string password, string fullname, string address, string email, string startdate, string sex, string phone)
        {
            string sql = "Insert TblAccount VALUES(@ID,@Password,@Fullname,@Address,@Email,@Startdate,@Sex,@Phone,@RoleID,@Status)";
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Fullname", fullname);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Startdate", startdate);
            cmd.Parameters.AddWithValue("@Sex", sex);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@RoleID", 2);
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
        public bool setPasswordDefault(string id)
        {
            bool result = false;
            string SQL = "UPDATE TblAccount set Password='123@123' where UserID=@ID";
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
    }
}

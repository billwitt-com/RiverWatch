using RWInbound2.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string UZerName = tbUserName.Text.Trim();
            string Password = tbPassword.Text.Trim();
            string FirstName = ""; 
            string LastName = "";
            int success = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = GlobalSite.RiverWatchConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("select FirstName, LastName from tbluser where UserName like '{0}' and Password like '{1}'", UZerName, Password);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                if (sdr.HasRows)
                                {
                                    FirstName = sdr["FirstName"] as string;
                                    LastName = sdr["LastName"] as string;
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                string msg = ex.Message;    // XXXX need to build an error log file and logging code               
            }

            if (FirstName.Length < 1)   // login failed
            {
                lblFailureText.Text = "Login Failed"; 
            }     


        // update user profile 

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = GlobalSite.RiverWatchConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("update tbluser set [DateLastActivity] = '{2}' where UserName like '{0}' and Password like '{1}'", UZerName, Password, DateTime.Now.ToString());
                        cmd.Connection = conn;
                        conn.Open();

                        success = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                string msg = ex.Message;    // XXXX need to build an error log file and logging code               
            }

            FormsAuthentication.SetAuthCookie(UZerName, false); // create user name as authenticated 
            FormsAuthentication.RedirectFromLoginPage(UZerName, false);
        }

      
    }
} 
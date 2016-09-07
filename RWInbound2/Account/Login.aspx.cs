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

        // new bwitt sept 2016
        // allow user to login from index page, which will show no menu tabs until user is logged in and role is determined
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string UZerName = tbUserName.Text.Trim();
            string Password = tbPassword.Text.Trim();
            string FirstName = ""; 
            string LastName = "";
            int? role = 0; 
            int success = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  //GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("select FirstName, LastName, Role from tbluser where UserName like '{0}' and Password like '{1}'", UZerName, Password);
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
                                    role = sdr["Role"] as int?; 
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }


            if (FirstName.Length < 1)   // login failed
            {
                lblFailureText.Text = "Login Failed";
                return; 
            }     

        // update user profile 

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
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
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            FormsAuthentication.SetAuthCookie(UZerName, false); // create user name as authenticated 
           // FormsAuthentication.RedirectFromLoginPage(UZerName, false);
            if(role.Value > 0)
            {
                Session["Role"] = role.Value; 
            }
          
             Response.Redirect("~/index.aspx"); 
        }
    }
} 
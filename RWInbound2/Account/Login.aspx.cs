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

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchWaterDEV"].ConnectionString;
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

            //FormsAuthentication.SetAuthCookie(FirstName +" " + LastName, false); // create user name as authenticated  
           
            //FormsAuthentication.RedirectFromLoginPage(FirstName + " " + LastName, false);
            //FormsAuthentication.GetAuthCookie(FirstName + " " + LastName, false);


            FormsAuthentication.SetAuthCookie(UZerName, false); // create user name as authenticated  

            FormsAuthentication.RedirectFromLoginPage(UZerName, false);
        //    FormsAuthentication.GetAuthCookie(UZerName, false);
      //      HttpContext.Current.Profile.SetPropertyValue("UserName", FirstName + " " + LastName); 

            string usernm = User.Identity.Name; 
                //System.Environment.UserName;           
            string contextName = HttpContext.Current.Request.LogonUserIdentity.Name; 
            bool isAuth = HttpContext.Current.User.Identity.IsAuthenticated;
            isAuth = User.Identity.IsAuthenticated; 
        }

      
    }
} 
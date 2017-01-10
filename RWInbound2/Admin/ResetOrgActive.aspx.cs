using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace RWInbound2.Admin
{
    public partial class ResetOrgActive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int rowcount = 0;
            using(SqlCommand cmd = new SqlCommand())
            {
                using(SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;
                    cmd.Connection = conn;
                    conn.Open(); 
                    cmd.CommandText = "  UPDATE [dbo].[organization] SET ACTIVE = 0 ";
                    cmd.CommandType = System.Data.CommandType.Text;
                    rowcount = cmd.ExecuteNonQuery(); 
                }
            }

            LblResults.Text = string.Format("Set {0} Orgs to inactive", rowcount);
        }
    }
}
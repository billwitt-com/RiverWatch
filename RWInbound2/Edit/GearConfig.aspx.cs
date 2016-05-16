using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2
{
    public partial class GearConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("GearConfigNewRow.aspx");
        }

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = System.Environment.UserName;
            if (user.Length < 1)
                user = "Unknown";
            e.Command.Parameters["@CreatedBy"].Value = user;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
            e.Command.Parameters["@Valid"].Value = 1;
        }
    }
}
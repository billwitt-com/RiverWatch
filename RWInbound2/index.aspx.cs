using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = string.Format("Welcome {0}", User.Identity.Name);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/login.aspx"); 
        }
    }
}
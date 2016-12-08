using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
      //      throw new InvalidOperationException("An InvalidOperationException " +
      //"occurred in the Page_Load handler on the Default.aspx page.");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/login.aspx"); 
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        { 
            FormsAuthentication.SignOut();            
            Response.Redirect("~/index.ASPX"); 
        }      
    }
}
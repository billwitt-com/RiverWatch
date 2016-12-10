using System;
using System.Web.Security;

namespace RWInbound2
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
      //      throw new InvalidOperationException("An InvalidOperationException " +
      //"occurred in the Page_Load handler on the index.aspx page.");
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Validation
{
    public partial class Validation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnICP_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Validation/Default.aspx"); 
        }
    }
}
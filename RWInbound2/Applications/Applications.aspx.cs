using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Applications
{
    public partial class Applications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAWQMSChems_Click(object sender, EventArgs e)
        {
            Response.Redirect("AWQMSChems.aspx");
        }
    }
}
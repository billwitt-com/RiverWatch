using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlQuickview.GroupingText = "Title"; 
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
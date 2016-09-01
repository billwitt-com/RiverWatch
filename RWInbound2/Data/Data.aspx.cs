using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Data
{
    public partial class Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int role = 1;
            if (Session["Role"] != null)
            {
                role = (int)Session["Role"];
            }

            if (role < 5)
            {
                btnUploadLatchat.Visible = false; 

            }

        }

        protected void btnFieldData_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/FieldData.aspx"); 
        }

        protected void btnUploadLatchat_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Data/UploadLatchet.aspx"); // this is spelled wrong
        }
    }
}
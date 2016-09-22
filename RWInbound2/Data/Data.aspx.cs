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
                role = (int)Session["Role"];    // get users role
            }
          
            RiverWatchEntities RWE = new RiverWatchEntities();

            var R = from r in RWE.ControlPermissions
                    where r.PageName.ToUpper() == "Data"
                    select r;

            int? Q = (from r in R
                      where r.ControlID.ToUpper() == "btnUploadLatchat"
                      select r.RoleValue).FirstOrDefault();

            if (Q != null)
            {
                if (role >= Q.Value)
                    btnUploadLatchat.Visible = true;

                else
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

        protected void btnOrganization_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Data/organizations.aspx");
        }

        protected void btnStation_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Data/Stations.aspx");
        }
    }
}
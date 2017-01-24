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

            if (Session["ROLE"] != null)
            {
                role = (int)Session["ROLE"];    // get users role
            }
          
            RiverWatchEntities RWE = new RiverWatchEntities();

            var R = from r in RWE.ControlPermissions
                    where r.PageName.ToUpper() == "DATA"
                    select r;

            int? Q = (from r in R
                      where r.ControlID.ToUpper() == "BTNUPLOADLACHAT"  
                      select r.RoleValue).FirstOrDefault();

            if (Q != null)
            {
                if (role >= Q.Value)
                    btnUploadLatchat.Visible = true;

                else
                    btnUploadLatchat.Visible = false;
            }

            //if(role < Q.Value)
            //{
            //    pnlDownloadData.Visible = false;
            //}
            //else
            //{
            //    pnlDownloadData.Visible = true; 
            //}
        }

        // Field Data button is default to everyone 
        protected void btnFieldData_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Data/FieldData.aspx"); 
        }

        protected void btnUploadLatchat_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Data/UploadLatchet.aspx"); // this is spelled wrong
        }



        // below are leftovers from another time, now moved to public menu

        //protected void btnOrganization_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Data/organizations.aspx");
        //}

        //protected void btnStation_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Data/Stations.aspx");
        //}

        protected void btnUnknownSample_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Data/UnknownData.aspx");
        }
    }
}
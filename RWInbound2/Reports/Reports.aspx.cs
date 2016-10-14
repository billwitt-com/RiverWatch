using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check users permissons 
            try
            {
                int role = 1;

                if (Session["Role"] != null)
                {
                    role = (int)Session["Role"];    // get users role
                }

                string pgname = Page.ToString().Replace("ASP.", "").Replace("_", ".").ToUpper();
                int idxEnd = pgname.IndexOf(".ASPX");
                pgname = pgname.Remove(idxEnd);
                int x = pgname.Length - 1;
                int y = 0;
                while (x != 0)
                {
                    if (pgname[x--] == '.')   // find a period, if it exists
                        break;
                }
                if (x != 0) // a period, so take from this point to end of string
                {
                    x += 2; // advance beyond period
                    y = pgname.Length - x;
                    pgname = pgname.Substring(x, y);
                }

                RiverWatchEntities RWE = new RiverWatchEntities();
                var R = from r in RWE.ControlPermissions
                        where r.PageName.ToUpper() == pgname           //"VALIDATION"   // this is the page name and should appear in the table ControlPermissions
                        select r;
                if (R == null)
                    Response.Redirect("~/index.aspx");  // there is no table entry, so don't let user use this page

                int? Q = (from r in R
                          where r.ControlID.ToUpper() == "PAGE"
                          select r.RoleValue).FirstOrDefault();

                if (Q != null)
                {
                    if (role < Q.Value)
                        Response.Redirect("~/index.aspx");  // send unauthorized back to home page... 
                }
                else
                {
                    Response.Redirect("~/index.aspx");
                }
            }
            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

        }

        protected void btnErrorLogReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ViewErrorLog.aspx");
        }

        protected void btnStationsWithGauges_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ViewStationsWithGauges.aspx");
        }

        protected void btnLachatNoBC_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportLachatNOTINNutrientBarcode.aspx");
        }

        protected void btnOrgStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrgStatus.aspx");
        }

        protected void btnOrgStations_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrgStations.aspx");
        }

        protected void btnOrganizations_Click(object sender, EventArgs e)
        {
            Response.Redirect("Organizations.aspx");
        }

        protected void btnParticipants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Participants.aspx");
        }

        protected void btnStations_Click(object sender, EventArgs e)
        {
            Response.Redirect("Stations.aspx");
        }
    }
}
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
            //pnlQuickview.GroupingText = "Quick View";
            //pnlQuickview.HorizontalAlign = HorizontalAlign.Center;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearchStationName_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearchOrgName_Click(object sender, EventArgs e)
        {

        }

        protected void btnManageStations_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminStations.aspx");
        }

        protected void btnEditMetalBarcode_Click(object sender, EventArgs e)
        {
            Response.Redirect("editmetalbarcode.aspx"); 
        }

        protected void btnProjectStations_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminProjectStations.aspx");
        }

        protected void btnControlPermissions_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminControlPermissions.aspx");
        }

        protected void btnRoles_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminRoles.aspx");
        }

        protected void btnNutrientLimits_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminNutrientLimits.aspx");
        }

        protected void btnExpWaters_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditExpWater.aspx");
        }

        protected void btnAddInboundICP_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddInboundICP.aspx");
        }

        protected void btnEditIncoming_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditInboundFieldData.aspx");
        }

        protected void btnManageOrgs_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminOrgs.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditUnknowns.aspx");
        }

        // miss labled should be admin users
        protected void btnUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminUsers.aspx"); 
        }

 
    }
}
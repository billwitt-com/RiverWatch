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

        }

        protected void btnSearchOrgName_Click(object sender, EventArgs e)
        {

        }

        protected void btnManageStations_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminStations.aspx");
        }

        protected void btnManageParticipants_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditParticipants.aspx");
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

        protected void btnManagePublicUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagePublicAccess.aspx");
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

        protected void btnEditNutrientBarcodes_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditNutrientBarcode.aspx"); 
        }

        protected void btnEditWaterCodes_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditWatercodes.aspx"); 
        }

        protected void btnManageOrgStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminOrgstatus.aspx");
        }

        protected void btnEditNutrients_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditNutrients.aspx");
        }

        protected void btnPermissions_Click(object sender, EventArgs e)
        {
            // no code for this yet?
        }

        protected void btnStationImages_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditStationImages.aspx");
        }

        protected void btnBulkEditOrgActive_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrgStatusBulklUpdate.aspx");
        }

        protected void btnResetAllOrgActive_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResetOrgActive.aspx");
        }

        protected void btnBulkEditStnActive_Click(object sender, EventArgs e)
        {
            // StationStatusBulklUpdate
            Response.Redirect("StationStatusBulklUpdate.aspx");
        }

        protected void btnResetAllStnActive_Click(object sender, EventArgs e)
        {
            // ResetStationActive
            Response.Redirect("ResetStationActive.aspx");
        }

        protected void btnEnterBenthicsPhysHab_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnterBenthicsPhysHab.aspx");
        }
    }
}
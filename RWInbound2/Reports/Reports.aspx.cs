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
            bool allowed = false;
            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx"); 
        }

        protected void btnErrorLogReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ViewErrorLog.aspx");
        }

        //protected void btnStationsWithGauges_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Reports/ViewStationsWithGauges.aspx");
        //}

        protected void btnLachatNoBC_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportLachatNOTINNutrientBarcode.aspx");
        }

        //protected void btnOrgStatus_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("OrgStatus.aspx");
        //}

        //protected void btnOrgStations_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("OrgStations.aspx");
        //}

        //protected void btnOrganizations_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Organizations.aspx");
        //}

        protected void btnParticipants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Participants.aspx");
        }

        //protected void btnStations_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Stations.aspx");
        //}

        protected void btnSamples_Click(object sender, EventArgs e)
        {
            Response.Redirect("Samples.aspx");
        }

        protected void btnMetalBarCodes_Click(object sender, EventArgs e)
        {
            Response.Redirect("MetalBarCodes.aspx");
        }

        protected void btnPublicUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("PublicUsers.aspx");
        }

        protected void btnParticipantsPrimaryContacts_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParticipantsPrimaryContacts.aspx");
        }

        protected void btnMailingList_Click(object sender, EventArgs e)
        {
            Response.Redirect("MailingLabels.aspx");
        }

        protected void btnOrgUnknownResults_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrgUnknownResults.aspx");
        }

        protected void btnQAQC_Click(object sender, EventArgs e)
        {
            Response.Redirect("QAQCReport.aspx");
        }

        protected void btnOrganizations_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllOrgsReport.aspx");
        }

        protected void btnICPBlanksAndDups_Click(object sender, EventArgs e)
        {
            Response.Redirect("ICPBlanksAndDups.aspx");
        }

        protected void btnAWQMSlookups_Click(object sender, EventArgs e)
        {
            Response.Redirect("AWQMSLookups.aspx");
        }

        protected void btnNutrientBlanksDups_Click(object sender, EventArgs e)
        {
            Response.Redirect("NutrientBlankDups.aspx");
        }

        protected void btnNutrientNOT_Click(object sender, EventArgs e)
        {
            Response.Redirect("NutrientsNOTBlankorDup.aspx");
        }

        protected void btnOrgEquipment_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrgEquipment.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("StationsWithNoSamples.aspx");
        }

        protected void btnOrgsNoSamples_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrgsWithNoSamples.aspx");
        }

        protected void btnNutrientsLookup_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrgsWithNoSamples.aspx");
        }

        protected void btnMetalsLookups_Click(object sender, EventArgs e)
        {
            Response.Redirect("MetalsLimits.aspx");
        }

    }
}
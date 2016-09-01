using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// main page holding all editing options
namespace RWInbound2.Edit
{
    public partial class Edit : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string iid = User.Identity.Name;
            if (iid.Length < 3)
                iid = "Unknown";


            lblWelcome.Text = string.Format("Welcome {0}", User.Identity.Name);
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                lblWelcome.Text += " You are authenticated";
            }
            else
            {
                lblWelcome.Text += " You are NOT authenticated";
            }
        }

        // add redirects to each page 
        protected void btnOrganizations_Click(object sender, EventArgs e)
        {
           Response.Redirect("EditOrgFormView.aspx");
        }

        protected void brnLimits_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditLimits.aspx");
        }

        protected void btnGear_Click(object sender, EventArgs e)
        {
            Response.Redirect("GearConfig.aspx");
        }

        protected void btnActivityCategories_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditActivityCategory.aspx");
        }

        protected void btnActivityTypes_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditActivityType.aspx");
        }

        protected void btnBioResultsTypes_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditBioResultsType.aspx");
        }

        protected void btnCommunities_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditCommunities.aspx");
        }

        protected void btnCounties_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditCounty.aspx");
        }

        protected void btnEcoRegions_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditEcoRegion.aspx");
        }

        protected void btnEquipCategories_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditEquipCategory.aspx");
        }

        protected void btnEquipItems_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditEquipItems.aspx");
        }

        protected void btnFieldGear_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditFieldGear.aspx");
        }

        protected void btnFieldProcedure_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditFieldProcedure.aspx");
        }
        
        protected void btnGrid_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditGrid.aspx");
        }

        protected void btnHydroUnit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditHydroUnit.aspx");
        }

        protected void btnMetalBarCodeType_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditMetalBarCodeType.aspx");
        }

        protected void btnNutrientBarCodeType_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditNutrientBarCodeType.aspx");
        }

        protected void btnOrganizationType_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditOrganizationType.aspx");
        }

        protected void btnQUADI_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditQUADI.aspx");
        }

        protected void btnQuarterSection_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditQuarterSection.aspx");
        }

        protected void btnRange_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditRange.aspx");
        }

        protected void btnRegion_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditRegion.aspx");
        }
    }
}
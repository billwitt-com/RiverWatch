using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// main page holding all editing options
namespace RWInbound2
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
    }
}
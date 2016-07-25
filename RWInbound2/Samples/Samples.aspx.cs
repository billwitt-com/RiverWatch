using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Samples
{
    public partial class SamplesPage : System.Web.UI.Page
    {
        dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2(); 

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnNewSample_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Samples/newsamples1.aspx");
        }

        protected void btnAddMetalBarcode_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SamplesPage.AddMetalBarcode.aspx");
        }

        protected void btnAddNutrientBarcode_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }

       

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class StationStatusBulklUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string selectedValue;
        protected void PreventErrorsOn_DataBinding(object sender, EventArgs e)
        {
            DropDownList theDropDownList = (DropDownList)sender;
            theDropDownList.DataBinding -= new EventHandler(PreventErrorsOn_DataBinding);
            theDropDownList.Items.Clear();
            theDropDownList.AppendDataBoundItems = true;

            selectedValue = "";
            try
            {
                theDropDownList.DataBind();
            }
            catch (ArgumentOutOfRangeException)
            {
                theDropDownList.Items.Clear();
                theDropDownList.Items.Insert(0, new ListItem("Please select", ""));
                theDropDownList.SelectedValue = "";
            }
        }
        protected void DropDownList1_DataBinding(object sender, EventArgs e)
        {
            string h = "Hello";
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class EditWaterCodes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string sCommand = "Select * FROM [tblWatercodes] order by [WATERNAME]";
                SqlDataSource1.SelectCommand = sCommand;
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sCommand = ""; 
            if(RadioButtonList1.Items[0].Selected)  // sort by water name
            {
                sCommand = "Select * FROM [tblWatercodes] order by [WATERNAME]";
                SqlDataSource1.SelectCommand = sCommand;
            }
            else
            {
                sCommand = "Select * FROM [tblWatercodes] order by [WATERCODE]";
                SqlDataSource1.SelectCommand = sCommand;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditLimits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string iid = User.Identity.Name;
             if(iid.Length < 3)   
               iid =  "Unknown";

            lblWelcomeName.Text = string.Format("Welcome {0}", User.Identity.Name);            

        }

        // this is called at updating and fills in the fields we want to force into data set
        // it may be possible to move the command text code here, but for now... 
        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = User.Identity.Name;
            e.Command.Parameters["@UserCreated"].Value = user;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
            e.Command.Parameters["@Valid"].Value = 1;
        }

        protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = System.Environment.UserName;
            e.Command.Parameters["@UserCreated"].Value = user;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
            e.Command.Parameters["@Valid"].Value = 1;
        }   
       
    }
}
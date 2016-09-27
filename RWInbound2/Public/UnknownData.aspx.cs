using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Public
{
    public partial class UnknownData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool allowed = false;
            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx"); 
            pnlData.Visible = false; // hide until login
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int kitNumber = 0;
            string kn = tbKitNumber.Text.Trim();
            string passWord = ""; 
            
            if(int.TryParse(kn, out kitNumber))
            {
                passWord = tbOrgPwd.Text.Trim();
                if(passWord.Length > 4)
                {
                    // query to see if this kit and pwd exist... 

                    RiverWatchEntities RWE = new RiverWatchEntities();
                    var Q = (from q in RWE.organizations
                            where q.KitNumber == kitNumber & q.Password.ToUpper() == passWord.ToUpper()
                            select q).FirstOrDefault(); 

                    if(Q != null)
                    {
                        pnlData.Visible = true;
                        lblKitNumber.Text = kitNumber.ToString();
                        lblOrgName.Text = Q.OrganizationName; 
                        tbAlk1.Focus();
                        return;
                    }
                }
            }
            tbKitNumber.Text = "";
            tbOrgPwd.Text = "";
            return; 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            RiverWatchEntities RWE = new RiverWatchEntities();
            UnknownSample US = new UnknownSample();

            int kitNumber = 0;
            string kn = tbKitNumber.Text.Trim();
            string passWord = "";
            string nam = "";
            if (int.TryParse(kn, out kitNumber))
            {
                passWord = tbOrgPwd.Text.Trim();
                if (passWord.Length > 4)
                {
                    // query to see if this kit and pwd exist... second time as something may have changed 
                    try
                    {
                        var Q = (from q in RWE.organizations
                                 where q.KitNumber == kitNumber & q.Password.ToUpper() == passWord.ToUpper()
                                 select q).FirstOrDefault();

                        if (Q != null)
                        {
                            US.OrganizationID = Q.ID;
                            US.Comment = tbComments.Text.Trim();
                            US.DateCreated = DateTime.Now;
                            if (User.Identity.Name.Length < 3)
                                nam = "Not logged in";
                            else
                                nam = User.Identity.Name;
                            US.UserCreated = nam;                         

                        }
                    }
                    catch (Exception ex)
                    {

                        if (User.Identity.Name.Length < 3)
                            nam = "Not logged in";
                        else
                            nam = User.Identity.Name;
                        string msg = ex.Message;
                        LogError LE = new LogError();
                        LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
                    }
                }
            }
        }
    }
}
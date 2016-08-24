using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RWInbound2.App_Code;

namespace RWInbound2
{
    public partial class EditOrgFormView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SEARCHTERM"] = ""; // make sure search term does not hang around between pages

            }
            string uniqueID = FormView1.Controls[0].Parent.UniqueID;    // find the control on this page. NOTE using .Parent, which works but 
            string controlID = uniqueID + "$" + "ReturnButton";
            Button RB = this.FindControl(controlID) as Button;
            if (RB != null)
                RB.Visible = false;
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            FormView1.ChangeMode(FormViewMode.Insert);
        }

        // approved method of getting data for the autocomplete extender. Can reuse for other tables... 

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchOrgs(string prefixText, int count)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = GlobalSite.RiverWatchConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select OrganizationName from Organization where OrganizationName like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        List<string> customers = new List<string>();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                customers.Add(sdr["OrganizationName"].ToString());
                            }
                        }
                        conn.Close();
                        return customers;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;    // XXXX need to build an error log file and logging code
                return null;
            }
        }

        /// <summary>
        /// User has clicked the search button next to the auto complete text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchterm = tbSearch.Text;
            if (searchterm.Length < 3)
                return; 
            searchterm.Replace("@", " ");   // clean up some obvious sql injection chars. Perhaps move to utility 
            searchterm.Replace("#", " ");
            searchterm.Replace("!", " ");
            searchterm.Replace("'", " ");
            searchterm.Replace("%", " ");
            Session["SEARCHTERM"] = searchterm;     // save for later 

            SqlDataSource1.SelectCommand = string.Format("select * from organization where OrganizationName like '{0}'", searchterm);
            FormView1.DataBind();

            string uniqueID = FormView1.Controls[0].Parent.UniqueID; 
            string controlID = uniqueID + "$" + "ReturnButton";
            Button RB = this.FindControl(controlID) as Button;
            if(RB != null)
                RB.Visible = true;
        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            string jeb = "jeb";
        }

        // changed the method for this button in page code, added this event. 
        protected void EditButton_Click(object sender, EventArgs e)
        {
            string searchterm = ""; 
            if(Session["SEARCHTERM"] != null)
            {
                searchterm = (string)Session["SEARCHTERM"];
                if (searchterm.Length < 1)
                    return;
                // XXXX dont forget to check valid flag when table is updated to have that field

                SqlDataSource1.SelectCommand = string.Format("select * from organization where OrganizationName like '{0}'", searchterm);
                FormView1.DataBind();
                FormView1.ChangeMode(FormViewMode.Edit);    // put in edit mode
                tbSearch.Text = "";

                string uniqueID = FormView1.Controls[0].Parent.UniqueID;
                string controlID = uniqueID + "$" + "ReturnButton";
                Button RB = this.FindControl(controlID) as Button;
                    RB.Visible = false; // turn off the button as it makes no sense here
            }  
        }
        /// <summary>
        /// Used to allow user to return to normal processing without editing the searched for record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = string.Format("select * from tblOrganization ");
            FormView1.ChangeMode(FormViewMode.ReadOnly);
            tbSearch.Text = "";
            string uniqueID = FormView1.Controls[0].Parent.UniqueID;
            string controlID = uniqueID + "$" + "ReturnButton";
            Button RB = this.FindControl(controlID) as Button;
                RB.Visible = false;
        }
    }
}



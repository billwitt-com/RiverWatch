using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class EditAWQMSLookups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sCommand; 
            if(!IsPostBack)
            {
                FormView1.Visible = false;
            }
            if (Session["COMMAND"] != null) // reset the command each time
            {
                sCommand = (string)Session["COMMAND"];
                SqlDataSource1.SelectCommand = sCommand;
                //   FormView1.DataBind(); 
            }
        }


        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]
        public static List<string> SearchNames(string prefixText, int count)
        {
            List<string> Names = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; // GlobalSite.RiverWatchDev;

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //   select [LocalName] from [dbo].[tlkAQWMStranslation]  where localname is not null and localname like @SearchText + '%  order by localname
                        cmd.CommandText = "select [LocalName] from [dbo].[tlkAQWMStranslation]  where localname is not null and localname like @SearchText + '%'  order by localname";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Names.Add(sdr["LocalName"].ToString());
                            }
                        }
                        conn.Close();
                        return Names;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return Names;
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string sCommand = "";
            string commonName = tbCommonName.Text;
            if (commonName.Length < 1)
            {
                lblMsg.Text = "Please choose a valid name";
                return;
            }
            try
            {
                RiverWatchEntities RWE = new RiverWatchEntities();
                // get the org id and then set up a count of unknowns to work with
                var C = (from c in RWE.tlkAQWMStranslations
                         where c.LocalName.ToUpper() == commonName.ToUpper()
                         select c).FirstOrDefault();
                if (C == null)
                {
                    lblMsg.Text = "Please choose a valid Name";
                    return;
                }
                lblMsg.Text = "";


                sCommand = string.Format(" select *  FROM [tlkAQWMStranslation] where LocalName is not null and LocalName = '{0}' order by LocalName ", commonName);

                Session["COMMAND"] = sCommand; 
                SqlDataSource1.SelectCommand = sCommand;
                Session["COMMAND"] = sCommand;
                FormView1.Visible = true;
                FormView1.DataBind();
            }

            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
            btnAddNew.Visible = false; // turn off is org chosen

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            FormView1.ChangeMode(FormViewMode.Insert);  // force into insert mode
            FormView1.Visible = true;
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            string sCommand = ""; 
            try
            {
                sCommand = string.Format("select * FROM [tlkAQWMStranslation] where LocalName is not null order by LocalName");
                Session["COMMAND"] = sCommand; 
                SqlDataSource1.SelectCommand = sCommand;

                FormView1.Visible = true;
                FormView1.DataBind();
            }

            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
            btnAddNew.Visible = false; // turn off is org chosen
        }

        protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
                // <asp:Parameter Name="DateCreated" Type="DateTime" />
                // <asp:Parameter Name="UserCreated" Type="String" />

            string user = User.Identity.Name;
            //e.Command.Parameters["@UserCreated"].Value = user;
            //e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
        }

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = User.Identity.Name;

                // <asp:Parameter Name="DateCreated" Type="DateTime" />
                //<asp:Parameter Name="UserCreated" Type="String" />
        //    e.Command.Parameters["@UserCreated"].Value = user;
        //    e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
        }

        protected void SqlDataSource1_DataBinding(object sender, EventArgs e)
        {
            string hereweare = "here we are";
        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            string hereweare = "here we are";
        }
    }
}
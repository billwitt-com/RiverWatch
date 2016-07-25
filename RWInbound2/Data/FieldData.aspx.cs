using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using System.IO;
using System.Web.Providers.Entities;

namespace RWInbound2.Data
{
    public partial class FieldData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Panel1.Visible = false;
              Button IB =  (Button) FormView1.FindControl("InsertButton");
              IB.Enabled = false; 
                
               
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            int stationNumber = 0;
            int kitNumber = 0;
            string samplenumber = "";
            string tmpstr = "";
            DateTime currentYear;
            DateTime thisyear = DateTime.Now;
            string date2parse = "";
            string orgName = "";
            NewRiverwatchEntities NRWDE = new NewRiverwatchEntities();  // create our local EF 

            kitNumber = -1; // no real kit number yet
            bool success = int.TryParse(tbSite.Text, out stationNumber);
            bool success2 = int.TryParse(tbKitNumber.Text, out kitNumber);

            orgName = tbOrg.Text;

            if (!success)   // no site, so erase input boxes and return, could use message
            {
                tbSite.Text = "Enter Site #";
                tbKitNumber.Text = "";
                tbOrg.Text = "";
                Panel1.Visible = false;
                return;
            }
            if (!success2)  // first, no kit number if we get below this
            {
                if (orgName.Length > 2)   // there is an org name
                {
                    var KN = (from k in NRWDE.Organizations
                              where k.OrganizationName == orgName
                              select k.KitNumber).FirstOrDefault();

                    if (KN != null)
                    {
                        kitNumber = KN.Value;
                    }
                }
            }

            if (kitNumber == -1)
            {
                tbSite.Text = "";
                tbKitNumber.Text = "";
                tbOrg.Text = "";
                Panel1.Visible = false;
                return;
            }

            // we have good kit number, so fill in the text box, just for....
            tbKitNumber.Text = kitNumber.ToString();

            try
            {
                var RES = from r in NRWDE.Stations
                          join o in NRWDE.Organizations on kitNumber equals o.KitNumber // s.OrganizationID equals o.OrganizationID
                          join ts in NRWDE.OrgStatus on o.OrganizationID equals ts.OrganizationID
                          where r.StationNumber == stationNumber & o.KitNumber == kitNumber
                          select new
                          {
                              stnName = r.StationName,
                              orgName = o.OrganizationName,
                              riverName = r.River,
                              startDate = ts.ContractStartDate,
                              endDate = ts.ContractEndDate,
                              active = o.Active,
                              watershed = r.RWWaterShed,
                              stnID = r.ID,
                              orgID = o.OrganizationID
                          };
                if (RES.Count() == 0)
                {
                    Panel1.Visible = false;                   
                    tbSite.Text = "";
                    tbKitNumber.Text = "";
                    return;
                }

                // query good, fill in text boxes below select button

                tbOrg.Text = orgName;   // to make it 'nice'
                Panel1.Visible = true;
                tbOrgName.Text = string.Format("{0}", RES.FirstOrDefault().orgName);
                tbKitNum.Text = kitNumber.ToString();
                tbStationName.Text = string.Format("{0}", RES.FirstOrDefault().stnName);
                tbRiver.Text = string.Format("{0}", RES.FirstOrDefault().riverName);
                tbStationNum.Text = stationNumber.ToString();


                //lblStationNumber.Text = string.Format("Station Number: {0}", stationNumber);
                //lblEndDate.Text = string.Format("End Date: {0:M/d/yyyy}", RES.FirstOrDefault().endDate);
                //lblOrganization.Text = string.Format("Organization: {0}", RES.FirstOrDefault().orgName);
                //lblRiver.Text = string.Format("River: {0}", RES.FirstOrDefault().riverName);
                //LblStartDate.Text = string.Format("Start Date: {0:M/d/yyyy}", RES.FirstOrDefault().startDate);
                //lblBlankForNow.Text = string.Format("Active: {0}", RES.FirstOrDefault().active.ToString());
                //lblWatershed.Text = string.Format("Watershed: {0}", RES.FirstOrDefault().watershed);
                //lblStationDescription.Text = string.Format("Description: {0}", RES.FirstOrDefault().stnName);
                //TabContainer1.Visible = true;
            }
            catch (Exception ex)
            {
                Panel1.Visible = false; // clean up and then report error

                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
            // enable the save button
            Button IB = (Button)FormView1.FindControl("InsertButton");
            IB.Enabled = true; 

        }

        protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        // user has chosen to save data 
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            string msg = "we are here";
        }


        // used to search on org name 

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchOrgs(string prefixText, int count)
        {
            List<string> customers = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchDEV"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select OrganizationName from Organization where OrganizationName like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

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
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return customers;
            }
        }

       
        
    }
}
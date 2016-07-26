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

        protected void SqlDataSourceInBoundSample_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            string sampNum = (string)Session["SAMPNUM"];
            string uzr = User.Identity.Name;
            if ((uzr == null) | (uzr.Length < 3))
                uzr = "Dev User";
            e.Command.Parameters["@EntryStaff"].Value = uzr;    // not really necessary as most entries in table are null [PassValStep]
            e.Command.Parameters["@PassValStep"].Value = -1;
            e.Command.Parameters["@StationNum"].Value = (int)Session["STATIONNUMBER"];
            e.Command.Parameters["@SampleID"].Value = (string)Session["SAMPNUM"];
            e.Command.Parameters["@KitNum"].Value = (int)Session["KITNUMBER"];
            e.Command.Parameters["@Date"].Value = (DateTime)Session["DATE"];
            e.Command.Parameters["@Time"].Value = (int)Session["TIME"];
            e.Command.Parameters["@txtSampleID"].Value = ((int)Session["STATIONNUMBER"]).ToString(); // this is a string, but is same as station number ---- 
        }

        // top button on page, does not save, just creates form and fills in some stuff
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            int stationNumber = 0;
            int kitNumber = 0;
            DateTime thisyear = DateTime.Now;
            string orgName = "";

            NewRiverwatchEntities NRWDE = new NewRiverwatchEntities();  // create our local EF 

            kitNumber = -1; // no real kit number yet
            bool success = int.TryParse(tbSite.Text, out stationNumber);
            bool success2 = int.TryParse(tbKitNumber.Text, out kitNumber);

            orgName = tbOrg.Text;
            try
            {
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

                Session["STATIONNUMBER"] = stationNumber;
                Session["KITNUMBER"] = kitNumber; 
              

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
      
        // user has chosen to save data 
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            DateTime newdate;
            string timestr;
            bool success;
            string sampnum; 
            string msg = "we are here";
            int hours;
            int mins; 
            // build sample number 

            // get text box from form TimeTextBox
            string dateStr = tbDateCollected.Text;

            success = DateTime.TryParse(dateStr, out newdate);

            timestr = tbTimeCollected.Text;           

            success = int.TryParse(timestr.Substring(0, 2), out hours);          

            success = int.TryParse(timestr.Substring(3, 2), out mins);

            sampnum = string.Format("{0}{1}{2:D2}{3:D2}{4:D2}{5:D2}",
                tbSite.Text,
                newdate.Year,
                newdate.Month,
                newdate.Day,
                hours,
                mins);

            Session["SAMPNUM"] = sampnum; // save for sql update
            Session["DATE"] = newdate;
            Session["TIME"] = (hours * 100) + mins; 
                
                //timestr.Replace(":", ""); // drop colon if here as result field is only 4 chars wide --- 

            ParameterCollection ourCol = SqlDataSourceInBoundSample.InsertParameters;

            //int res = SqlDataSourceInBoundSample.Insert();

            //int smoke = res; 


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
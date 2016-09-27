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
using System.Web.Security;

namespace RWInbound2.Data
{
    public partial class FieldData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            bool allowed = false;
            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx"); 

            if (!IsPostBack)
            {
                Panel1.Visible = false;
                pnlExisting.Visible = false; 
                lblErrorMsg.Visible = false;
                Button IB = (Button)FormView1.FindControl("InsertButton");
                IB.Enabled = false;
                pnlDetail.Visible = false;  // hide until org validated
                Session["TRIES"] = 0;
            }
        }

        protected void SqlDataSourceInBoundSample_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            string sampNum = (string)Session["SAMPNUM"];
            string uzr = User.Identity.Name;
            if ((uzr == null) | (uzr.Length < 3))
                uzr = "Dev User";
            e.Command.Parameters["@EntryStaff"].Value = uzr;    
            e.Command.Parameters["@PassValStep"].Value = -1;            // not really necessary as most entries in table are null [PassValStep]
            e.Command.Parameters["@StationNum"].Value = (int)Session["STATIONNUMBER"];
            e.Command.Parameters["@SampleID"].Value = (string)Session["SAMPNUM"];
            e.Command.Parameters["@KitNum"].Value = (int)Session["KITNUMBER"];
            e.Command.Parameters["@Date"].Value = (DateTime)Session["DATE"];
            e.Command.Parameters["@Time"].Value = (int)Session["TIME"];
            e.Command.Parameters["@Valid"].Value = true; 
            e.Command.Parameters["@txtSampleID"].Value = ((int)Session["STATIONNUMBER"]).ToString(); // this is a string, but is same as station number ---- 
        }

        protected void SqlDataSourceInBoundSample_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
          //  string sampNum = (string)Session["SAMPNUM"];
            // calc date and time as we do not save parms below
            string dateStr = tbDateCollected.Text;
            bool success = false;
            DateTime newdate;
            int hours = 0;
            int mins = 0;

            success = DateTime.TryParse(dateStr, out newdate);
            string timestr = tbTimeCollected.Text;
            success = int.TryParse(timestr.Substring(0, 2), out hours);
            success = int.TryParse(timestr.Substring(3, 2), out mins);

            Session["DATE"] = newdate;
            Session["TIME"] = (hours * 100) + mins; 
             
            string uzr = User.Identity.Name;
            if ((uzr == null) | (uzr.Length < 3))
                uzr = "Dev User";
            e.Command.Parameters["@EntryStaff"].Value = uzr;
            e.Command.Parameters["@PassValStep"].Value = -1;            // not really necessary as most entries in table are null [PassValStep]
            e.Command.Parameters["@StationNum"].Value = (int)Session["STATIONNUMBER"];
            e.Command.Parameters["@SampleID"].Value = (string)Session["SAMPNUM"];
            e.Command.Parameters["@KitNum"].Value = (int)Session["KITNUMBER"];
            e.Command.Parameters["@Date"].Value = (DateTime)Session["DATE"];
            e.Command.Parameters["@Time"].Value = (int)Session["TIME"];
            e.Command.Parameters["@Valid"].Value = true; 
            e.Command.Parameters["@txtSampleID"].Value = ((int)Session["STATIONNUMBER"]).ToString(); // this is a string, but is same as station number ---- 
        }

        // we are validating the user against org and org pwd and if valid, filling in lots of stuff 
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int stationNumber = 0;
            int kitNumber = 0;
            string orgName = "";
            string pwd = tbOrgPwd.Text.Trim();

            RiverWatchEntities NRWDE = new RiverWatchEntities();  // create our local EF 

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
                    try
                    {
                        if (orgName.Length > 2)   // there is an org name
                        {
                            var KN = (from k in NRWDE.organizations
                                      where k.OrganizationName == orgName
                                      select k.KitNumber).FirstOrDefault();

                            if (KN != null)
                            {
                                kitNumber = KN.Value;
                            }
                        }
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

                Session["STATIONNUMBER"] = stationNumber;
                Session["KITNUMBER"] = kitNumber;

            // if any user is logged in, we will allow them on this page, otherwise, we will test against the org pwd

                if (!User.Identity.IsAuthenticated)    // we don't know who this is 
                {
                    // now, test the pwd against the org pwd or previous logged in user

                    var VP = (from v in NRWDE.organizations
                              where v.Password.ToUpper() == pwd.ToUpper()
                              select v.OrganizationName).FirstOrDefault();

                    if (VP != null)
                    { 
                        FormsAuthentication.SetAuthCookie(VP, false); // create user name as authenticated 
                        //    FormsAuthentication.RedirectFromLoginPage(VP, false);
                        Session["Role"] = 1;  // XXXX not sure about this yet.... 
                    }
                    else  // login failure
                    {
                        if (Session["TRIES"] != null)
                        {
                            int tries = (int)Session["TRIES"];
                            tries++;
                            Session["TRIES"] = tries;
                            if (tries > 3)
                            {
                                // for now, take user to timedout page, not sure what else to do... 
                                Response.Redirect("timedout.aspx");
                            }
                            else
                            {
                                lblErrorMsg.Visible = true;
                                lblErrorMsg.Text = "Your loging information is incorrect, please try again";
                                tbOrgPwd.Text = "";
                                return;
                            }
                        }
                    }
                }
            // we have authenticated user
                pnlDetail.Visible = true;
                lblErrorMsg.Visible = false;

            try
                {
                    var RES = from r in NRWDE.Stations
                              join o in NRWDE.organizations on kitNumber equals o.KitNumber // s.OrganizationID equals o.OrganizationID
                              join ts in NRWDE.OrgStatus on o.ID equals ts.OrganizationID
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
                                  orgID = o.ID
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
            }       
       
        
        // top button on page, does not save, just creates form and fills in some stuff
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            int stationNumber = 0;
            int kitNumber = 0;
            DateTime thisyear = DateTime.Now;
   
            DateTime dateCollected;
            string dc = "";

            RiverWatchEntities NRWDE = new RiverWatchEntities();  // create our local EF 

            dc = tbDateCollected.Text.Trim();
            bool rz = DateTime.TryParse(dc, out dateCollected);

            if (!rz)
            {
                lblErrorMsg.Text = "Please Enter Valid Date";
                lblErrorMsg.Visible = true;
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                Panel1.Visible = false;
                return;
            }
            else
            {
                lblErrorMsg.Visible = false; 
            }

            Session["COLLECTIONDATE"] = dateCollected;

            if(Session["STATIONNUMBER"] != null)
                stationNumber = (int)Session["STATIONNUMBER"]; 
            else
                Response.Redirect("Timedout.aspx");

            if (Session["KITNUMBER"] != null)
                kitNumber = (int)Session["KITNUMBER"]; 
            else
                Response.Redirect("Timedout.aspx");

            // test to see if there is existing record for this date, and if so, warn user. 
            // nasty query... 

            var Exists = from ex in NRWDE.InboundSamples
                         where ex.KitNum == kitNumber & ex.StationNum == stationNumber & ex.Date.Value.Year == dateCollected.Year
                         & ex.Date.Value.Month == dateCollected.Month & ex.Date.Value.Day == dateCollected.Day & ex.Valid == true
                         select ex; 

            if(Exists.Count() > 0)  // we have same date already
            {
                pnlExisting.Visible = true;
                lblWarnExisting.Text = string.Format("NOTICE: there is an existing data entry for this station on this date {0:MM/dd/yyyy} You can choose to update the existing record or create a new one", dateCollected);
            }
            else
            {
                pnlExisting.Visible = false; 
            }
            // enable the save button
            Button IB = (Button)FormView1.FindControl("InsertButton");
            IB.Enabled = true; 
        }
      
        // user has chosen to save data 
        // we are capturing this prior to sql insert
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
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  //GlobalSite.RiverWatchDev;
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

        protected void btnUseExisting_Click(object sender, EventArgs e)
        {
            // fill in form data with query
          //  SqlDataSourceInBoundSample
            int kitNumber;
            int stationNumber;
            DateTime colDate;
            try
            {
                pnlExisting.Visible = false;

                if (Session["STATIONNUMBER"] == null)
                {
                    Response.Redirect("TimeedOut.aspx");
                }

                if (Session["KITNUMBER"] == null)
                {
                    Response.Redirect("TimeedOut.aspx");
                }

                if (Session["COLLECTIONDATE"] == null)
                {
                    Response.Redirect("TimeedOut.aspx");
                }

                kitNumber = (int)Session["KITNUMBER"];
                stationNumber = (int)Session["STATIONNUMBER"];
                colDate = (DateTime)Session["COLLECTIONDATE"];

                Button IB = (Button)FormView1.FindControl("InsertButton");
                IB.Enabled = true; 

                FormView1.ChangeMode(FormViewMode.Edit);

                string smdStr = string.Format("SELECT * FROM [InboundSamples] where  [KitNum] = {0} AND [StationNum] = {1} and [Date] = '{2}'",
                    kitNumber, stationNumber, colDate.Date);
                SqlDataSourceInBoundSample.SelectCommand = smdStr;
                FormView1.DataBind();
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


        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            // nothing to do so just return
            pnlExisting.Visible = false;
            return; 
        }      
    }
}
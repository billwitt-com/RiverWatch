using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using RWInbound2.Validation; 
 
// XXXX modified to put all samples with codes of 15, 45, 55, 65 to final data base as blank / dup
// as per Michaela, as I understood it... 
// XXXX will change when Michaela adds date to this.
 
namespace RWInbound2.Data
{
    public partial class UploadLatchet : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            bool allowed = false;
            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx");            

            FileUpload1.AllowMultiple = false;     
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string FileName = "";
            string fName = FileUpload1.FileName;  //Server.MapPath(PATH) + FileUpload1.FileName;
            int Startindex = 0;
            int Endindex = 0;
            string tstr = "";
            string line = "";
            int lineNumber = 0;
            int linesProcessed = 0;

            // run this regardless 



            if (FileUpload1.HasFile)
            {
                try
                {
                    if ((FileUpload1.PostedFile.ContentType == "application/vnd.ms-excel") | (FileUpload1.PostedFile.ContentType == "application/x-csv"))         // "image/jpeg" 
                    {
                        if (FileUpload1.PostedFile.ContentLength < 902400)
                        {
                            FileName = Path.GetFileName(FileUpload1.FileName);
                            FileName = FileUpload1.FileName;
                            lblStatus.Text = "Upload status: File uploaded!";
                            //      lblUploadComplete.Text = string.Format("Would you like to process file {0} now?", FileName);
                        }
                        else
                        {
                            lblStatus.Text = "Upload status: The file has to be less than 900 kb!";
                            return;
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Upload status: Only Excel CVS files are accepted!";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    string nam = "";
                    if (User.Identity.Name.Length < 3)
                        nam = "Not logged in";
                    else
                        nam = User.Identity.Name;
                    string msg = ex.Message;
                    LogError LE = new LogError();
                    LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
                }
               
                RiverWatchEntities RWE = new RiverWatchEntities();
               
                using (StreamReader SR = new StreamReader(FileUpload1.FileContent))
                {
                    if (SR.Peek() > 0)  // read and drop the first line, which is headers
                    {
                        line = SR.ReadLine();
                    }
                    try
                    {
                        while (SR.Peek() > 0)
                        {
                            Lachat LACHAT = new Lachat();
                            line = SR.ReadLine();
                            lineNumber++; 
                            if(line.Length < 26)    // just a guess, but there seem to be some lines with no data, but I don't see them 
                            {
                                continue; // skip if no real data 
                            }
                            linesProcessed++; 

                            Startindex = 0; // line.IndexOf(",", Startindex) + 1; // find position of first comma and skip past it
                            Endindex = line.IndexOf(",", Startindex + 1);
                            tstr = line.Substring(Startindex, Endindex - Startindex); // ,[Batch]
                            LACHAT.Batch = tstr.Trim();
                            Startindex = Endindex + 1; // reposition 

                            Startindex = Endindex + 1;
                            Endindex = line.IndexOf(",", Startindex + 1);
                            tstr = line.Substring(Startindex, Endindex - Startindex);    //,[SampleType]
                            LACHAT.SampleType = tstr;

                            Startindex = Endindex + 1; // reposition 
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);     //,[SampleDescription]
                            LACHAT.SampleDescription = tstr;

                            Startindex = Endindex + 1; // reposition 
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex).Trim();      //,[CODE]

                            if(tstr.Length == 1) // single char
                            {
                                if(tstr == "5")
                                {
                                    tstr = "05"; 
                                }
                            }
                            LACHAT.CODE = tstr;

                            Startindex = Endindex + 1; // reposition                   
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);           //,[Parameter]
                            LACHAT.Parameter = tstr;

                            Startindex = Endindex + 1; // reposition                   
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);         //,[Result]
                            if (tstr.Length > 1)
                                LACHAT.Result = decimal.Parse(tstr);

                            Startindex = Endindex + 1; // reposition                   
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);     //,[Unit]
                            LACHAT.Unit = tstr;

                            Startindex = Endindex + 1; // reposition                   
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);       //,[CONHI]
                            bool test = tstr.Equals("1");
                            LACHAT.CONHI = test;

                            Startindex = Endindex + 1; // reposition                   
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);      //,[RPDDivPctREC]
                            if (tstr.Length > 1)
                            {
                                tstr = tstr.Replace("%", "");
                                LACHAT.RPDDivPctREC = decimal.Parse(tstr);
                            }

                            Startindex = Endindex + 1; // reposition                   
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);     //,[CHANGED]
                            test = tstr.Equals("1");
                            LACHAT.CHANGED = test;

                            Startindex = Endindex + 1; // reposition                   
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);     //,[NumChanged]
                            int num;
                            if (int.TryParse(tstr, out num))
                                LACHAT.NumChanged = num;

                            Startindex = Endindex + 1; // reposition                   
                            Endindex = line.IndexOf(",", Startindex);
                            tstr = line.Substring(Startindex, Endindex - Startindex);      //,[Comment]
                            LACHAT.Comment = tstr;

                            // no more string to process, so just fill in the right stuff

                            LACHAT.PassValStep = 0;         //,[PassValStep]
                            LACHAT.tblSampleID = null;      //,[tblSampleID]
                            LACHAT.Valid = true;            //,[Valid]
                            LACHAT.Validated = false;       //,[Validated]
                           

                            // added 08/30/2016 as the other codes do not need any validatation and thus can go right to table as blank dups 
                            if ((LACHAT.CODE.Trim() == "05") | (LACHAT.CODE.Trim() == "25") | (LACHAT.CODE.Trim() == "35"))
                            {
                                LACHAT.BlkDup = false;          //,[BlkDup]
                                LACHAT.Validated = false;       //,[Validated]
                            }
                            else
                            {
                                LACHAT.BlkDup = true;
                                LACHAT.Validated = true;       //,[Validated]   this flag will keep this sample from showing up in validaton steps
                            }

                            LACHAT.Failed = false;          //,[Failed]                          

                            RWE.Lachats.Add(LACHAT);
                        }
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                        string nam = "";
                        if (User.Identity.Name.Length < 3)
                            nam = "Not logged in";
                        else
                            nam = User.Identity.Name;
                        string msg = ex.Message;
                        LogError LE = new LogError();
                        LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
                        lblStatus.Text = "Upload failed, please check your input file";
                        lblStatus.Visible = true; 
                        return; 
                    }

                    int result = RWE.SaveChanges();

                    lblStatus.Text = string.Format("{0} lines processed from file {1}", linesProcessed, FileName);
                    lblStatus.Visible = true;

                    // put back here because it is too slow to run in Validate
                    // XXXX
                    // moved to start of method, so it will always run... 
                  //  UpdateNutrients.Update(User.Identity.Name); // static class.. process any new lachat input before we get going.. 
                    
                    // now save the data in the File table

                    try
                    {
                        FileStorage FS = new FileStorage();
                         
                        FS.Name = FileName;
                        FS.CreatedBy = User.Identity.Name;
                        FS.DateCreated = DateTime.Now;
                        FileUpload1.FileContent.Position = 0; 

                        using (StreamReader SR1 = new StreamReader(FileUpload1.FileContent))
                        {
                            string s = SR1.ReadToEnd();
                            FS.Content = Encoding.ASCII.GetBytes(s);      
                        }

                        FS.Valid = true;
                        FS.Type = "application/vnd.ms-excel"; 
                        RWE.FileStorages.Add(FS);
                        int rez = RWE.SaveChanges();
                        int j = rez; 
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
                        lblStatus.Text = "Excel File Save failed, please check your input file";
                        lblStatus.Visible = true; 
                    }
                }
            }  // end of file to upload test 
            // XXXX moved this here.. so it will always run
            UpdateNutrients.Update(User.Identity.Name); // static class.. process any new lachat input before we get going.. 
        }

        protected void FileUpload1_Load(object sender, EventArgs e)
        {
            FileUpload FU = (FileUpload)sender;
            pnlUploadComplete.Visible = true;     

        }

        // forgot to name this correctly but it is button that loads drop down list to show all used batch numbers for reference
        protected void Button1_Click(object sender, EventArgs e)
        {
            // load drop down list and sort
            int work = 0; 

            RiverWatchEntities RWE = new RiverWatchEntities();
            var B = (from b in RWE.Lachats
                     select b.Batch).Distinct();

            List<int> LI = new List<int>();

            foreach(string s in B)
            {

                //if (s.Length < 1)
                //    break; 
                if(int.TryParse(s, out work))
                    LI.Add(work);
            }
            LI.Sort();

            ddlBatches.DataSource = LI;
            ddlBatches.DataBind(); 
        }
    }
}


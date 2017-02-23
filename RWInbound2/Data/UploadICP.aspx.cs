using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Data
{
    public partial class UploadICP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //bool allowed = false;
            //allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            //if (!allowed)
            //    Response.Redirect("~/index.aspx");            

            FileUpload1.AllowMultiple = false;
            lblErrorList.Text = "";
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
            string Eventcode = ""; // these will be used to look up barcode
            string BarCode = "";
            string dup = ""; 
            bool r = false;
            decimal td ;
            List<string> errorList = new List<string>(); 
            string el = "";
            DateTime anaDate ; 
            int sampID = 0;
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
                errorList.Clear(); 

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
                            // create one of each, so we can write them both at one time
                            InboundICPOrigional ICPO = new InboundICPOrigional();
                            InboundICPFinal ICPF = new InboundICPFinal();

                            line = SR.ReadLine();
                            lineNumber++;
                            if (line.Length < 26)    // just a guess, but there seem to be some lines with no data, but I don't see them 
                            {
                                el = string.Format("Input Line {0} is too short, and has been skipped", lineNumber);
                                errorList.Add(el);
                                continue; // skip if no real data 
                            }
                            linesProcessed++;

                            string[] c = line.Split(',');

                            Eventcode = c[0] ?? "";
                            dup = c[1] ?? "";
                            if (dup.Length == 1)
                                dup = "0" + dup;    // fill in in case only one character

                            if ((Eventcode.Length < 2) | (dup.Length < 2))
                            {
                                el = string.Format("Input Line {0} is missing either code or dup values, and has been skipped", lineNumber);
                                errorList.Add(el);
                                continue; // skip as we don't have a code or dup
                            }

                            // get bar code here to make sure we know it
                            var B = (from b in RWE.MetalBarCodes
                                     where b.Code == dup & b.NumberSample == Eventcode
                                     select b.LabID).FirstOrDefault();


                            if (B == null)    // bad or no bar code
                            {
                                el = string.Format("Input Line {0} has dup: {1} and NumSample: {2} which are not in Metals Barcode table, and has been skipped", lineNumber, dup, Eventcode);
                                errorList.Add(el);
                                continue; // skip it
                            }

                            BarCode = B;
                            // now see if there is a bar code in either of the icp input files

                            var BC = from bc in RWE.InboundICPOrigionals
                                     where bc.CODE == BarCode
                                     select bc;

                            if (BC.Count() >= 1)
                            {
                                el = string.Format("Input Line {0} has barcode {1} which is already in inbound ICP table, and has been skipped", lineNumber, BarCode);
                                errorList.Add(el);
                                continue;
                            }

                            r = decimal.TryParse(c[2], out td);
                            if (r)
                            {
                                ICPO.AL_D = td;
                                ICPF.AL_D = td;
                            }

                            r = decimal.TryParse(c[3], out td);
                            if (r)
                            {
                                ICPO.AL_T = td;
                                ICPF.AL_T = td;
                            }

                            r = decimal.TryParse(c[4], out td);
                            if (r)
                            {
                                ICPO.AS_D = td;
                                ICPF.AS_D = td;
                            }
                            r = decimal.TryParse(c[5], out td);
                            if (r)
                            {
                                ICPO.AS_T = td;
                                ICPF.AS_T = td;
                            }

                            r = decimal.TryParse(c[6], out td);
                            if (r)
                            {
                                ICPO.CA_D = td;
                                ICPF.CA_D = td;
                            }
                            r = decimal.TryParse(c[7], out td);
                            if (r)
                            {
                                ICPO.CA_T = td;
                                ICPF.CA_T = td;
                            }

                            r = decimal.TryParse(c[8], out td);
                            if (r)
                            {
                                ICPO.CD_D = td;
                                ICPF.CD_D = td;
                            }
                            r = decimal.TryParse(c[9], out td);
                            if (r)
                            {
                                ICPO.CD_T = td;
                                ICPF.CD_T = td;
                            }
                            r = decimal.TryParse(c[10], out td);
                            if (r)
                            {
                                ICPO.CU_D = td;
                                ICPF.CU_D = td;
                            }
                            r = decimal.TryParse(c[11], out td);
                            if (r)
                            {
                                ICPO.CU_T = td;
                                ICPF.CU_T = td;
                            }

                            r = decimal.TryParse(c[12], out td);
                            if (r)
                            {
                                ICPO.FE_D = td;
                                ICPF.FE_D = td;
                            }
                            r = decimal.TryParse(c[13], out td);
                            if (r)
                            {
                                ICPO.FE_T = td;
                                ICPF.FE_T = td;
                            }
                            r = decimal.TryParse(c[14], out td);
                            if (r)
                            {
                                ICPO.PB_D = td;
                                ICPF.PB_D = td;
                            }
                            r = decimal.TryParse(c[15], out td);
                            if (r)
                            {
                                ICPO.PB_T = td;
                                ICPF.PB_T = td;
                            }

                            r = decimal.TryParse(c[16], out td);
                            if (r)
                            {
                                ICPO.MG_D = td;
                                ICPF.MG_D = td;
                            }
                            r = decimal.TryParse(c[17], out td);
                            if (r)
                            {
                                ICPO.MG_T = td;
                                ICPF.MG_T = td;
                            }

                            r = decimal.TryParse(c[18], out td);
                            if (r)
                            {
                                ICPO.MN_D = td;
                                ICPF.MN_D = td;
                            }
                            r = decimal.TryParse(c[19], out td);
                            if (r)
                            {
                                ICPO.MN_T = td;
                                ICPF.MN_T = td;
                            }

                            r = decimal.TryParse(c[20], out td);
                            if (r)
                            {
                                ICPO.SE_D = td;
                                ICPF.SE_D = td;
                            }
                            r = decimal.TryParse(c[21], out td);
                            if (r)
                            {
                                ICPO.SE_T = td;
                                ICPF.SE_T = td;
                            }
                            r = decimal.TryParse(c[22], out td);
                            if (r)
                            {
                                ICPO.ZN_D = td;
                                ICPF.ZN_D = td;
                            }
                            r = decimal.TryParse(c[23], out td);
                            if (r)
                            {
                                ICPO.ZN_T = td;
                                ICPF.ZN_T = td;
                            }

                            r = decimal.TryParse(c[24], out td);
                            if (r)
                            {
                                ICPO.NA_D = td;
                                ICPF.NA_D = td;
                            }
                            r = decimal.TryParse(c[25], out td);
                            if (r)
                            {
                                ICPO.NA_T = td;
                                ICPF.NA_T = td;
                            }

                            r = decimal.TryParse(c[26], out td);
                            if (r)
                            {
                                ICPO.K_D = td;
                                ICPF.K_D = td;
                            }
                            r = decimal.TryParse(c[27], out td);
                            if (r)
                            {
                                ICPO.K_T = td;
                                ICPF.K_T = td;
                            }

                            // now do ANA date
                            if (c[28].Length > 5)
                            {
                                r = DateTime.TryParse(c[28], out anaDate);
                                if (r)
                                {
                                    ICPF.ANADATE = anaDate;
                                    ICPO.ANADATE = anaDate;
                                }
                            }

                            // now do date SENT
                            if (c[30].Length > 5)
                            {
                                r = DateTime.TryParse(c[30], out anaDate);  // REUISE ANADATE   
                                if (r)
                                {
                                    ICPF.DATE_SENT = anaDate;
                                    ICPO.DATE_SENT = anaDate;
                                }
                            }

                            // COMMENTS

                            if (c[31].Length > 1)
                            {
                                ICPO.Comments = c[31];
                                ICPF.Comments = c[31];
                            }

                            ICPF.COMPLETE = true; // always
                            ICPO.COMPLETE = true;

                            ICPO.CreatedBy = User.Identity.Name;
                            ICPF.CreatedBy = User.Identity.Name;

                            ICPO.CreatedDate = DateTime.Now;
                            ICPF.CreatedDate = DateTime.Now;

                            ICPO.DUPLICATE = dup;
                            ICPF.DUPLICATE = dup;

                            ICPO.CODE = BarCode;    // don't you love all the different names for the same thing
                            ICPF.CODE = BarCode;

                            r = int.TryParse(c[35], out sampID);
                            if (r)
                            {
                                ICPO.tblSampleID = sampID;
                                ICPF.tblSampleID = sampID;
                            }

                            ICPO.Valid = true;
                            ICPF.Valid = true;

                            ICPO.Edited = false;
                            ICPF.Edited = false;

                            ICPO.Saved = false;
                            ICPF.Saved = false;

                            RWE.InboundICPOrigionals.Add(ICPO);
                            RWE.InboundICPFinals.Add(ICPF);
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

                    if (errorList.Count > 0)
                    {
                        foreach (string EL in errorList)
                        {
                            lblErrorList.Text += EL;
                            lblErrorList.Text += Environment.NewLine;
                        }
                    }

                    lblStatus.Text = string.Format("{0} lines processed from file {1}", linesProcessed, FileName);
                    lblStatus.Visible = true;
                }
            }
        
        }

        protected void FileUpload1_Load(object sender, EventArgs e)
        {
            FileUpload FU = (FileUpload)sender;
            pnlUploadComplete.Visible = true;
        }
   
    }
}
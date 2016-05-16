using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;

namespace RWInbound2
{
    public partial class _Default : Page
    {
        // this works for setting control styles !!!


        List<string> MetalsList = new List<string>();
        Dictionary<string, double> Limits = new Dictionary<string, double>(); 

       
       
        protected void Page_Load(object sender, EventArgs e)
        {

             HyperLink HL = this.FindControl("Val") as HyperLink;           
            int x = 0;
            int sampID = 0;            
            string sampleType = ""; // this is Duplicate in data base, for now
            //int pageCount = FormView1.PageCount;
         //   lblCount.Text = string.Format("There are {0} records to validate", pageCount); 
            
            Limits.Add("AL", .1);   // use these names for symbols too, when needed
            Limits.Add("AS", 5.0);  // created a table for these, but will not implement until needed bwitt 05/04/2016
            Limits.Add("CA", .1);
            Limits.Add("CD", .1);
            Limits.Add("CU", .1);
            Limits.Add("FE", .1);
            Limits.Add("PB", .1);
            Limits.Add("MG", .1);
            Limits.Add("MN", .1);
            Limits.Add("SE", .1);
            Limits.Add("ZN", .1);
            Limits.Add("NA", .1);
            Limits.Add("K", .1); 


            MetalsList.Add("AL");   // LOAD SYMBOLS FOR METALS FROM INBOUNDICP
            MetalsList.Add("AS");
            MetalsList.Add("CA"); 
            MetalsList.Add("CD");
            MetalsList.Add("CU");
            MetalsList.Add("FE");
            MetalsList.Add("PB"); 
            MetalsList.Add("MG");
            MetalsList.Add("MN");
            MetalsList.Add("SE"); 
            MetalsList.Add("ZN");
            MetalsList.Add("NA");
            MetalsList.Add("K"); 

            DataSourceSelectArguments args = new DataSourceSelectArguments(); 
            SqlDataSource1.SelectCommand = "SELECT* FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] ";
            System.Data.DataView result = (DataView)SqlDataSource1.Select(args);
            DataTable DT = result.ToTable();
            int rowCount = (int)DT.Rows.Count; // get number of rows in result   
            lblCount.Text = string.Format("There are {0} ICP records to validate", rowCount); 
           
            DataColumn DC = new DataColumn();
            DC.AllowDBNull = true;
            DC.ColumnName = "isNormalHere";
            DC.DataType = typeof(bool); 
            DT.Columns.Add(DC); 
            // now, loop through each row and if not a 'normal' sample, see if there is a normal sample. 
            // for now, use csampID but this may change

            for (x = 0; x < rowCount; x++)  // one pass for each sample in icp inbound
            {
                DT.Rows[x]["isNormalHere"] = false; // set false as default
                sampID = (int)DT.Rows[x]["tblSampleID"];
                sampleType = (string)DT.Rows[x]["DUPLICATE"];
                if (!sampleType.Substring(0, 1).Contains("0")) // this is not normal sample, so go see if there is a normal attached to this group
                {
                    string selstr = string.Format("tblSampleID = {0}", sampID); 
                    DataRow[] results = DT.Select(selstr);

                    // I think this will always fail as it will always find itself at least
                    if (results.Count() < 1) // no results, so no normals possible
                    {
                        DT.Rows[x]["isNormalHere"] = false;
                        continue;
                    }
                    else
                    {
                        foreach (DataRow row in results)    // these rows all have sampID in them
                        {
                            string dup = (string)row["DUPLICATE"];
                            if (dup.Substring(0, 1).Contains("0"))   // there is a normal related to sample
                            {
                                DT.Rows[x]["isNormalHere"] = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    DT.Rows[x]["isNormalHere"] = true;  // this is a normal, so make it true
                }
            }
                
            Session["OurTable"] = DT;   //save current copy for later

            SqlDataSource1.SelectCommand = "SELECT* FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP]";

        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            int smith = 0;
            smith += 1;
        }

        // this is swap button XXXX
        protected void Button1_Click(object sender, CommandEventArgs ea)             //EventArgs e)
        {
            string tmpStr;
            string CA = ea.CommandArgument.ToString();
            Button btn = sender as Button;
            string btnID = btn.ID;
            string bid = btnID.Substring(3, 2); // parse string to get metals type prefix

            string uniqueID = FormView1.Controls[0].UniqueID; // CC[0].UniqueID;   // get the whole name as made up by ASP web page
         
            string tbDName = uniqueID + "$" + bid + "_DTextBox";
            string tbTName = uniqueID + "$" + bid + "_TTextBox";

            TextBox tbD = this.FindControl(tbDName) as TextBox;
            TextBox tbT = this.FindControl(tbTName) as TextBox;
            tmpStr = tbD.Text; // save for swap
            tbD.Text = tbT.Text;
            tbT.Text = tmpStr;
            setControls(); // update color schemes
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            string barCode = "";
            int sampleID = 0;
            string selcmd = "";            

            FormView FV = sender as FormView;
            if (FV.Row.RowType != DataControlRowType.DataRow)    // if not data row, do nothing
                return;
            int rownum = FV.Row.ItemIndex; // this is page number and index into data table

            DataTable LDT = (DataTable)Session["OurTable"]; // get fresh copy of latest table
            barCode = (string)LDT.Rows[rownum]["CODE"];
            sampleID = (int)LDT.Rows[rownum]["tblSampleID"];

            SqlDataSource SDS = new SqlDataSource();
            SDS.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchWaterDEV"].ConnectionString; 
            DataSourceSelectArguments args = new DataSourceSelectArguments();
            // test to see these are in data base


            selcmd = string.Format("SELECT * FROM [dbRiverwatchWaterData].[dbo].[tblMetalBarCode] where [LabID] = '{0}'", barCode);
            SDS.SelectCommand = selcmd;
            try
            {
                System.Data.DataView result = (DataView)SDS.Select(args);
                if (result.Table.Rows.Count == 0)    // there is no barcode in table, so show error message
                {
                    lblNoBarCode.Text = string.Format("Bar Code {0} not found in data base", barCode);
                    lblNoBarCode.BackColor = System.Drawing.Color.MediumVioletRed;
                }
                else
                {
                    lblNoBarCode.Text = string.Format("Bar Code {0} found in data base", barCode);
                    lblNoBarCode.BackColor = System.Drawing.Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                int jones = 1;
            }
            try
            {
                selcmd = string.Format("SELECT * FROM [dbRiverwatchWaterData].[dbo].[tblMetalBarCode] where [SampleID] = {0}", sampleID);
                SDS.SelectCommand = selcmd;
                System.Data.DataView result = (DataView)SDS.Select(args);

                if (result.Table.Rows.Count == 0)    // there is no barcode in table, so show error message
                {
                    lblNoSampleID.Text = string.Format("Sample ID {0} not found in data base", sampleID);
                    lblNoSampleID.BackColor = System.Drawing.Color.MediumVioletRed;
                }
                else
                {
                    lblNoSampleID.Text = string.Format("Sample ID {0} found in data base", sampleID);
                    lblNoSampleID.BackColor = System.Drawing.Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                int jones = 1;
            }

            SDS.Dispose(); 

            //  now compare disolved and total for errors 
            // new stuff here 

     //       string uniqueID = FormView1.Controls[0].UniqueID; // get the whole name as made up by ASP web page
       //     string uniqueID = FormView1.Controls[0].NamingContainer.UniqueID;  // get the whole name as made up by ASP web page
       //     string bid = "AL";
       //     string tbDName = uniqueID + "$" + bid + "_DTextBox";
       //     string tbTName = uniqueID + "$" + bid + "_TTextBox";

       //     TextBox TD = (TextBox)FormView1.FindControl("ctl00$MainContent$FormView1$ctl00$AL_DTextBox");

       ////     int rownum = FV.Row.ItemIndex; // this is page number and index into data table

       //     TableCellCollection cells = FV.Row.Cells;
       //     int count = cells.Count;

       //     FormViewRow row = FV.Row;
       //  //   TableCellCollection cells = row.Cells; // seem to be about 80 on this page, and they are named as expected
       //     string locID = "";
       //     uniqueID = ""; 
       //     foreach(TableCell C in cells)
       //     {
       //         locID = C.ID;
       //         uniqueID = C.UniqueID;
       //         string jones = uniqueID; 


       //     }


       //     return;

            //SqlDataSource DS = (SqlDataSource)FV.DataSource;
           
            //DataTable DT = (DataTable)FV.DataSource; 
            //int sampID = (int) DT.Rows[rownum][36];
            //string dup = (string)DT.Rows[rownum][2]; 
            //if(!dup.Substring(0,1).Contains("0"))   // if this is NOT a normal sample, we must check to make sure there is a corresponding normal sample
            //{
            //    FV.Row.BackColor = System.Drawing.Color.Red; 
            //}
          
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            FormView FV = sender as FormView;
            EventArgs ARGS = e;

            if (FV.Row.RowType != DataControlRowType.DataRow)    // if not data row, do nothing
                return;
            int rownum = FV.Row.ItemIndex; // this is page number and index into data table
           
            int jones = 1;
        }

        protected void FormView1_DataBinding(object sender, EventArgs e)
        {
            FormView FV = sender as FormView;
            EventArgs ARGS = e;

            //if (FV.Row.RowType != DataControlRowType.DataRow)    // if not data row, do nothing
            //    return;
            //int rownum = FV.Row.ItemIndex; // this is page number and index into data table
           
            int jones = 1;
        }

        protected void FormView1_ItemCreated(object sender, EventArgs e)
        {
          // not currently of use
        }

        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            FormView FV = sender as FormView;
            EventArgs ARGS = e;

            if (FV.Row.RowType != DataControlRowType.DataRow)    // if not data row, do nothing
                return;
            int rownum = FV.Row.ItemIndex; // this is page number and index into data table
           
            int jones = 1;
        }

        // not in use now... using rendered event instead
        protected void FormView1_PageIndexChanged(object sender, EventArgs e)
        {
            int jones = 1;
        //    setControls(); 
           // Page_LoadComplete( sender,  e); // call to update all controls 
        }

       
        // this works for setting control styles etc. 
        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            setControls();
        }

        public void setControls()
        {
            TextBox tbD;
            TextBox tbT;
            string barCode = "";
            string dupCode = "";
            double limit = 00.0; 
            string tbDName;
            string tbTName;
            string codeTextBoxName;
            string dupTextBoxName; 
            double Total;
            double Disolved; 
            string uniqueID = FormView1.Controls[0].UniqueID; // CC[0].UniqueID;   // get the whole name as made up by ASP web page
            lblNote.Text = "";
            lblNote.ForeColor = Color.White;

            
            // get table we built earlier where we found no normal condition
            DataTable DT = (DataTable)Session["ourTable"];
          //  string selstr = string.Format("tblSampleID = {0}", sampID);

            // get barcode for this page as it is unique
            codeTextBoxName =uniqueID + "$" + "CODETextBox"; // get the text box off the page
            TextBox CTB = this.FindControl(codeTextBoxName) as TextBox;
            barCode = CTB.Text; 

            // get dup - Type - two digit number for this sample
            dupTextBoxName = uniqueID + "$" + "TextBox1"; // will have to change this name on aspx page XXXX
            TextBox DTB = this.FindControl(dupTextBoxName) as TextBox;
            dupCode = DTB.Text; 

            // search datatable for this barcode and the isNormalHere 

            int count = DT.Rows.Count;
            for (int t = 0; t < count; t++ )
            {
                if( ((bool)DT.Rows[t]["isNormalHere"] == false) & ((string)DT.Rows[t]["CODE"] == barCode))
                {
                    lblNote.Text = "No normal for this sample";
                    lblNote.ForeColor = Color.Red; 
                }
            }

                // process each metals data row to see if it falls out of 'correct' 
                foreach (string item in MetalsList)
                {
                    tbDName = uniqueID + "$" + item + "_DTextBox";
                    tbTName = uniqueID + "$" + item + "_TTextBox";
                    limit = Limits[item];

                    tbD = this.FindControl(tbDName) as TextBox;
                    tbT = this.FindControl(tbTName) as TextBox;

                    if (!double.TryParse(tbT.Text, out Total))
                    {
                        Total = 0.0;
                    }

                    if (!double.TryParse(tbD.Text, out Disolved))
                    {
                        Disolved = 0.0;
                    }

                    // truncate number of decimal places 
                    tbD.Text = string.Format("{0:0.0000}", Disolved);
                    tbT.Text = string.Format("{0:0.0000}", Total);

                    // see if the difference between the two is greater than 2 times the limit
                    if ((Disolved - Total) >= (2 * limit))
                    {
                        tbD.BackColor = Color.PowderBlue;
                        tbT.BackColor = Color.PowderBlue;
                    }
                    else
                    {
                        tbD.BackColor = Color.White;
                        tbT.BackColor = Color.White;
                    }    
               
                    // now see if this is a blank, then test for out of range

                    if(dupCode.Substring(0,1) == "1")   // we have a dupe
                    {
                        if(Total > (2 * limit))
                        {
                            tbT.ForeColor = Color.Red;
                        }
                        else
                        {
                            tbT.ForeColor = Color.Black; 
                        }
                        if (Disolved > (2 * limit))
                        {
                            tbD.ForeColor = Color.Red;
                        }
                        else
                        {
                            tbD.ForeColor = Color.Black;
                        }
                    }
                }
        }

        protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }
    }
}
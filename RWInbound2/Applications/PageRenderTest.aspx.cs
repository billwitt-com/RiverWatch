using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2
{
    public partial class PageRenderTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime startTime;
            DateTime endTime;
            double DN = 0; 


            // here we are going to build a table inside the pnlMain
            Table TA = new Table();
            TA.Width = Unit.Pixel(900); 
          //  TableRow TR = new TableRow();

            startTime = DateTime.Now;
            //for (int q = 0; q < 1; q++)
            //{
            //    for (int x = 0; x < 100; x++)
            //    {
            //        TableRow TR = new TableRow();

            //        for (int y = 0; y < 100; y++)
            //        {
            //            TextBox TB = new TextBox();
            //            TableCell TC = new TableCell();
            //            TB.ID = string.Format("tb{0}{1}", x.ToString(), y.ToString());
            //            TB.Text = string.Format("Time {0}:{1}:{2}.{3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            //            //string.Format("{0}", DateTime.Now.ToShortTimeString());                  
            //            TC.Controls.Add(TB);
            //            TR.Controls.Add(TC);
            //        }
            //        TA.Controls.Add(TR);
            //    }
            //}

            TA.Dispose();
            for (int x = 0; x < 100; x++)
            {
                TableRow TR = new TableRow();

                for (int y = 0; y < 10; y++)
                {
                    TextBox TB = new TextBox();
                    TableCell TC = new TableCell();
                    TB.ID = string.Format("tb{0}{1}", x.ToString(), y.ToString());
                    DN = x + y / 10;

                    TB.Text = string.Format("DN = {0}, SQRRT = {1}, QR = {2}", DN, Math.Round(Math.Sqrt(DN), 6), Math.Round(Math.Pow(DN, .25)));
                    TB.Text = ""; 
                    TB.Text = string.Format("Time {0}:{1}.{2}",DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                       
                        //string.Format("Time {0}:{1}:{2}.{3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
      
                    TC.Controls.Add(TB);
                    TR.Controls.Add(TC);
                }
                TA.Controls.Add(TR);
            }


            pnlMain.Controls.Add(TA);
            pnlMain.Width = Unit.Pixel(900); 
            endTime = DateTime.Now;
            // string.Format("Ended {0}:{1}:{2}:{3}", TTE.Hour, TTE.Minute, TTE.Second, TTE.Millisecond);
            lblStartTime.Text = string.Format("Started {0}:{1}:{2}.{3}", startTime.Hour, startTime.Minute, startTime.Second, startTime.Millisecond);
            lblEndTime.Text = string.Format("Started {0}:{1}:{2}.{3}", endTime.Hour, endTime.Minute, endTime.Second, endTime.Millisecond);
            TimeSpan TS;
            TS = endTime - startTime;
            lblET.Text = string.Format("Page Render took {0}.{1} seconds", TS.Seconds, TS.Milliseconds); 
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Web.Providers.Entities;

namespace RWInbound2
{
    public partial class DBSpeedTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  if(!IsPostBack)
                pnlStuff.Visible = false;
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            string AzureConnectionString = "Data Source=istrwtest.database.windows.net;Initial Catalog=RiverWatch;User ID=istonish;Password=RiverWatch!"; // providerName = "System.Data.SqlClient";
            string RemoteConnectionString = "Data Source=45.35.12.6,1090;Initial Catalog=RiverWatch_2014;Persist Security Info=False;User ID=riveruser;Password=DBM@123";
        //    string LocalConnectionString = @"Data Source=BILL2014\SQLEXPRESS2008;Initial Catalog=RiverWatch;Persist Security Info=False;User ID=Bill;Password=Password16";
            DateTime TTS;
            DateTime TTE;
            int count = 0;
            TimeSpan TS = new TimeSpan();
            int Masterloops = 0;
            int loops = 0;

            RiverWatchEntities RWE = new RiverWatchEntities();
            RWE.Database.Connection.ConnectionString = AzureConnectionString;
            DataSourceSelectArguments ARGS = new DataSourceSelectArguments();

            if (tbLoopCount.Text.Length < 0)
                return;
            if (!int.TryParse(tbLoopCount.Text.Trim(), out Masterloops))
            {
                return; 
            }

            pnlStuff.Visible = false; 
            // AZURE
            // first, do a linq query on azure
            var Q1 = from q in RWE.ViewSoloNutrientDups
                     select q;
            TTS = DateTime.Now;
            lblAzLinqStart.Text = string.Format("Starting {0}:{1}:{2}:{3}", TTS.Hour, TTS.Minute, TTS.Second, TTS.Millisecond);
            loops = Masterloops;
            while (loops > 0)
            {
                count = Q1.Count(); // count the values which actually runs the query
                loops--;
            }
            TTE = DateTime.Now;
            lblAZLinqEnd.Text = string.Format("Ended {0}:{1}:{2}:{3}", TTE.Hour, TTE.Minute, TTE.Second, TTE.Millisecond);
            TS = TTE - TTS;
            lblAzLinqTotal.Text = string.Format("Total Time {0}:{1}:{2}:{3}", TS.Hours, TS.Minutes, TS.Seconds, TS.Milliseconds);
            lblAzLinqCount.Text = string.Format("Counted {0} items", count);

            // now do a sql query

            TTS = DateTime.Now;
            lblAzSqlStart.Text = string.Format("Starting {0}:{1}:{2}:{3}", TTS.Hour, TTS.Minute, TTS.Second, TTS.Millisecond);
            count = 0;

            loops = Masterloops;
            while (loops > 0)
            {
                count = 0;
                using (SqlConnection conn = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        conn.ConnectionString = AzureConnectionString;
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandText = "select * from ViewSoloNutrientDups";

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            count++;
                        }
                    }
                }
                loops--; 
            }

            TTE = DateTime.Now;
            lblAzSqlEnd.Text = string.Format("Ended {0}:{1}:{2}:{3}", TTE.Hour, TTE.Minute, TTE.Second, TTE.Millisecond);
            TS = TTE - TTS;
            lblAzSqlTotal.Text = string.Format("Total Time {0}:{1}:{2}:{3}", TS.Hours, TS.Minutes, TS.Seconds, TS.Milliseconds);
            lblAzSqlCount.Text = string.Format("Counted {0} items", count);

            // REMOTE
            // next , do a linq query on Remote
            RWE.Database.Connection.ConnectionString = RemoteConnectionString;
            loops = Masterloops;
            TTS = DateTime.Now;
            lblReLinqStart.Text = string.Format("Starting {0}:{1}:{2}:{3}", TTS.Hour, TTS.Minute, TTS.Second, TTS.Millisecond);
            while (loops > 0)
            {
                var Q2 = from q in RWE.ViewSoloNutrientDups      //RWE.MetalBarCodes
                         select q;
                count = Q2.Count(); // count the values which actually runs the query
                loops--;
            }

            TTE = DateTime.Now;
            lblReLinqEnd.Text = string.Format("Ended {0}:{1}:{2}:{3}", TTE.Hour, TTE.Minute, TTE.Second, TTE.Millisecond);
            TS = TTE - TTS;
            lblReLinqTotal.Text = string.Format("Total Time {0}:{1}:{2}:{3}", TS.Hours, TS.Minutes, TS.Seconds, TS.Milliseconds);
            lblReLinqCount.Text = string.Format("Counted {0} items", count);

            // REMOTE
            // now do a sql query

            TTS = DateTime.Now;
            lblReSqlStart.Text = string.Format("Starting {0}:{1}:{2}:{3}", TTS.Hour, TTS.Minute, TTS.Second, TTS.Millisecond);
            count = 0;
            loops = Masterloops;
            while (loops > 0)
            {
                count = 0;
                using (SqlConnection conn = new SqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        conn.ConnectionString = RemoteConnectionString;
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandText = "select * from ViewSoloNutrientDups";

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            count++;
                        }
                    }
                }
                loops--; 
            }

            TTE = DateTime.Now;
            lblReSqlEnd.Text = string.Format("Ended {0}:{1}:{2}:{3}", TTE.Hour, TTE.Minute, TTE.Second, TTE.Millisecond);
            TS = TTE - TTS;
            lblReSqlTotal.Text = string.Format("Total Time {0}:{1}:{2}:{3}", TS.Hours, TS.Minutes, TS.Seconds, TS.Milliseconds);
            lblReSqlCount.Text = string.Format("Counted {0} items", count);

            //// Local
            //// next , do a linq query on Local

            //RWE.Database.Connection.ConnectionString = LocalConnectionString;
            //loops = Masterloops;
            //TTS = DateTime.Now;
            //lblLoLinqStart.Text = string.Format("Starting {0}:{1}:{2}:{3}", TTS.Hour, TTS.Minute, TTS.Second, TTS.Millisecond);
            //while (loops > 0)
            //{
            //    var Q2 = from q in RWE.ViewSoloNutrientDups      //RWE.MetalBarCodes
            //             select q;
            //    count = Q2.Count(); // count the values which actually runs the query
            //    loops--;
            //}

            //TTE = DateTime.Now;
            //lblLoLinqEnd.Text = string.Format("Ended {0}:{1}:{2}:{3}", TTE.Hour, TTE.Minute, TTE.Second, TTE.Millisecond);
            //TS = TTE - TTS;
            //lblLoLinqTotal.Text = string.Format("Total Time {0}:{1}:{2}:{3}", TS.Hours, TS.Minutes, TS.Seconds, TS.Milliseconds);
            //lblLoLinqCount.Text = string.Format("Counted {0} items", count);

            //// Local
            //// now do a sql query


            //TTS = DateTime.Now;
            //lblLoSqlStart.Text = string.Format("Starting {0}:{1}:{2}:{3}", TTS.Hour, TTS.Minute, TTS.Second, TTS.Millisecond);
            //count = 0;
            //loops = Masterloops;
            //while (loops > 0)
            //{
            //    count = 0;
            //    using (SqlConnection conn = new SqlConnection())
            //    {
            //        using (SqlCommand cmd = new SqlCommand())
            //        {
            //            conn.ConnectionString = LocalConnectionString;
            //            cmd.Connection = conn;
            //            conn.Open();
            //            cmd.CommandText = "select * from ViewSoloNutrientDups";

            //            SqlDataReader rdr = cmd.ExecuteReader();
            //            while (rdr.Read())
            //            {
            //                count++;
            //            }
            //        }
            //    }
            //    loops--;
            //}

            //TTE = DateTime.Now;
            //lblLoSqlEnd.Text = string.Format("Ended {0}:{1}:{2}:{3}", TTE.Hour, TTE.Minute, TTE.Second, TTE.Millisecond);
            //TS = TTE - TTS;
            //lblLoSqlTotal.Text = string.Format("Total Time {0}:{1}:{2}:{3}", TS.Hours, TS.Minutes, TS.Seconds, TS.Milliseconds);
            //lblLoSqlCount.Text = string.Format("Counted {0} items", count);

            pnlStuff.Visible = true;
        }
    }
}
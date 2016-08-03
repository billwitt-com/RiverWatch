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
using System.Data;

namespace RWInbound2.Validation
{
    public partial class ValidateField : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {

        }

        protected void btnBAD_Click(object sender, EventArgs e)
        {

        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int stationNumber = 0;
            int kitNumber = 0;
            DateTime thisyear = DateTime.Now;
            string orgName = "";

            //DataView dv = new DataView(); 
            //string cmd = e.Command.CommandText; 
            //dv = SqlDataSource1.Select(DataSourceSelectArguments.Empty) as DataView; 
           
          

            
          

         
        }

        protected void FormView1_PageIndexChanged(object sender, EventArgs e)
        {
            string hereweare = "hello";
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            int stationNumber = 0;
            int kitNumber = 0;
            int tm = 0;
            string tmStr = ""; 
            bool success = false;
            FormView fv = sender as FormView;
            TextBox snTB = fv.Controls[0].FindControl("StationNumTextBox") as TextBox;
            TextBox knTB = fv.Controls[0].FindControl("KitNumTextBox") as TextBox;
            TextBox hrsTB = fv.Controls[0].FindControl("TimeTextBox") as TextBox; 


            success = int.TryParse(snTB.Text, out stationNumber);
            if(!success)
            {
                // do something, I don't know what 
                return; 
            }

            success = int.TryParse(knTB.Text, out kitNumber);
            if (!success)
            {
                // do something, I don't know what 
                return;
            }

            tmStr = hrsTB.Text;
            if(tmStr.Length == 4)
            {
                tmStr = tmStr.Insert(2, ":");
                hrsTB.Text = tmStr; 
            }


            NewRiverwatchEntities NRWDE = new NewRiverwatchEntities();  // create our local EF 
            try
            {
                var RES = from r in NRWDE.Stations
                          join o in NRWDE.Organizations on kitNumber equals o.KitNumber // s.OrganizationID equals o.OrganizationID
                          where r.StationNumber == stationNumber & o.KitNumber == kitNumber
                          select new
                          {
                              stnName = r.StationName,
                              orgName = o.OrganizationName,
                              riverName = r.River,
                              orgID = o.OrganizationID
                          };
                if (RES.Count() == 0)
                {

                    return;
                }

                // query good, fill in text boxes below select button

                tbOrgName.Text = string.Format("{0}", RES.FirstOrDefault().orgName);
                tbStationName.Text = string.Format("{0}", RES.FirstOrDefault().stnName);
                tbRiver.Text = string.Format("{0}", RES.FirstOrDefault().riverName);


            }
            catch (Exception ex)
            {


            }

            string hereweare = "hello";
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class GearConfigNewRow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormView1.ChangeMode(FormViewMode.Insert);   // put in edit mode
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {

        }

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = System.Environment.UserName;
            if (user.Length < 1)
                user = "Unknown"; 
            e.Command.Parameters["@CreatedBy"].Value = user;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
            e.Command.Parameters["@Valid"].Value = 1;
        }

        protected void NewButton_Click(object sender, EventArgs e)
        {
           
        }

        protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = System.Environment.UserName;
            if (user.Length < 1)
                user = "Unknown";
            e.Command.Parameters["@CreatedBy"].Value = user;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
            e.Command.Parameters["@Valid"].Value = 1;
        }

        protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            Response.Redirect("GearConfig.aspx");
        } 
    }
}
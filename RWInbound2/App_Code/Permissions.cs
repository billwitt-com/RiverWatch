using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text; 

namespace RWInbound2.App_Code
{
    public static class Permissions
    {
        public static bool Test(string pageName, string controlName)
        {

            // Added code to read page name so this is now generic for a page... 
            // pass in Page.ToString() and name of control which can be 'PAGE' for the whole page
            try
            {
                int role = 1;

                if (HttpContext.Current.Session["Role"] != null)
                {
                    role = (int)HttpContext.Current.Session["Role"];    // get users role set on login page
                }

                string pgname = pageName.Replace("ASP.", "").Replace("_", ".").ToUpper(); // Page.ToString().Replace("ASP.", "").Replace("_", ".").ToUpper();
                int idxEnd = pgname.IndexOf(".ASPX");
                pgname = pgname.Remove(idxEnd);
                int x = pgname.Length - 1;
                int y = 0;
                while (x != 0)
                {
                    if (pgname[x--] == '.')   // find a period, if it exists
                        break;
                }
                if (x != 0) // a period, so take from this point to end of string
                {
                    x += 2; // advance beyond period
                    y = pgname.Length - x;
                    pgname = pgname.Substring(x, y);
                }

                RiverWatchEntities RWE = new RiverWatchEntities();
                var R = (from r in RWE.ControlPermissions
                        where r.PageName.ToUpper() == pgname & r.ControlID.ToUpper() == controlName     
                        select r).FirstOrDefault();
                
                if (R == null)
                    return false;               

                if (role < R.RoleValue)
                    return false;
                else
                    return true;   
            }
            catch (Exception ex)
            {
                string nam = "In Permissions.cs";
                //if (User.Identity.Name.Length < 3)
                //    nam = "Not logged in";
                //else
                //    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "In Permissions.cs", ex.StackTrace.ToString(), nam, "");
                return false; 
            }
        }


    }
}
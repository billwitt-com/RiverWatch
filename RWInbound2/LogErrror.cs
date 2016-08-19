using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWInbound2
{
    /// <summary>
    /// class to record errors
    /// could send emails if needed
    /// </summary>
    public class LogError
    {
        public void logError(string msg, string fromPage, string stackTrace, string loggedInUser, string comment)
        {
            
         //   RiverWatchEntities NRWE = new RiverWatchEntities();
            RiverWatchEntities NRWE = new RiverWatchEntities(); 
            ErrorLog EL = new ErrorLog();
            EL.Date = DateTime.Now;
            EL.Message = msg; 
            EL.Comment = comment;
            EL.StackTrace = stackTrace;
            EL.FromPage = fromPage;
            EL.LoggedInUser = loggedInUser;

            try
            {
                NRWE.ErrorLogs.Add(EL);
                int rows = NRWE.SaveChanges();
                int moreBetter = rows;
            }
            catch(Exception ex)
            {
                string ms = ex.Message; 
            }
        }
    }
    
    /*
     MODEL FOR USE:
            catch (Exception ex)
            {       
                string name = "";
                if (User.Identity.Name.Length < 3)
                    name = "Not logged in";
                else
                    name = User.Identity.Name;
                string msg = ex.Message; 
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), name, "");
            }     
     */
}
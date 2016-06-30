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
    public class LogErrror
    {
        public void logError(string msg, string fromPage, string stackTrace, string loggedInUser, string comment)
        {
            
            NewRiverwatchEntities NRWE = new NewRiverwatchEntities();
            ErrorLog EL = new ErrorLog();
            EL.Date = DateTime.Now;
            EL.Message = msg; 
            EL.Comment = comment;
            EL.StackTrace = stackTrace;
            EL.FromPage = fromPage;
            EL.LoggedInUser = loggedInUser;

            NRWE.ErrorLogs.Add(EL);
            NRWE.SaveChanges(); 
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
                LogErrror LE = new LogErrror();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), name, "");
            }     
     */
}
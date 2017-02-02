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
            RiverWatchEntities NRWE = new RiverWatchEntities(); 
            ErrorLog EL = new ErrorLog();

            // added 02/02 bwitt to try to get accurate times for log
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 

            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time");

            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            EL.Date = dateTime; // DateTime.Now.ToLocalTime();   // Changed 12/29 to get better time info

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
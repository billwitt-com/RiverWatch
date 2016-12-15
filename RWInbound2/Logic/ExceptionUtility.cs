using System;
using System.Web;
using System.IO;

namespace RWInbound2.Logic
{
    public class ExceptionUtility
    {
        // All methods are static, so this can be private
        private ExceptionUtility()
        { }

        // Log an Exception
        public static void LogException(Exception exc, string source)
        {

            LogError LE = new LogError();
            LE.logError(exc.Message, "ExceptionUtility", exc.StackTrace.ToString(), "", "Unhandled Exception caught.");

            // Include logic for logging exceptions
            // Get the absolute path to the log file
            string app_DataDirectory = HttpContext.Current.Server.MapPath("~/App_Data");
            if (!Directory.Exists(app_DataDirectory))
            {
                Directory.CreateDirectory(app_DataDirectory);
            }
            
            string logFile = "~/App_Data/ErrorLog." + DateTime.Now.ToString("MM.dd.yyyy") + ".txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            string oldLogFile = logFile;

            FileInfo logFileInfo = new FileInfo(logFile);
            if (logFileInfo.Exists)
            {
                //convert to megabytes
                double CONVERSION_VALUE = Math.Pow(1024, 2);
                double fileSize = (logFileInfo.Length / CONVERSION_VALUE);
                if (fileSize > 1)
                {                    
                    string newLogFile = "App_Data/ErrorLog." + DateTime.Now.ToString("MM.dd.yyyy.hh.mm.ss") + ".txt";
                    newLogFile = HttpContext.Current.Server.MapPath(newLogFile);
                    File.Move(oldLogFile, newLogFile);
                }
            }

            // Open the log file for append and write the log
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exc.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exc.GetType().ToString());
            sw.WriteLine("Exception: " + exc.Message);
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }
            sw.Close();           
        }
    }
}
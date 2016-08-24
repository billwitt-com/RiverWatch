using System.Web.Configuration;

namespace RWInbound2.App_Code
{
    /// <summary>
    /// Access cached values. Now we can simply call into the GlobalSite class 
    /// and use the properties there for access to the appSettings. 
    /// Using the cached values is 30 times faster, and will reduce memory pressure.
    /// This also allows the values for these settings to only be changed in one place
    /// vs having to search through the code.
    /// </summary>
    public static class GlobalSite
    {
        /// <summary>
        /// ConnectionString directly to SQL
        /// </summary>
        static public string RiverWatchConnectionString { get; set; }

        /// <summary>
        /// ConnectionString for Entity Framework dbcontext
        /// </summary>
        static public string RiverWatchEntitiesDbContext { get; set; }        

        static GlobalSite()
        {
            // Cache all these values in static properties.            
            RiverWatchConnectionString = WebConfigurationManager.AppSettings["RiverWatchConnectionString"];
            RiverWatchEntitiesDbContext = WebConfigurationManager.AppSettings["RiverWatchEntitiesDbContext"];
        }
    }
}
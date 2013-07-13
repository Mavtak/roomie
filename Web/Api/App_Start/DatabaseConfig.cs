using System.IO;
using System.Web;
using System.Xml.Linq;
using Roomie.Web.Persistence.Database;

namespace Roomie.Web.Api
{
    public static class DatabaseConfig
    {
        private static string key = "ConnectionString";
        public static void RegisterConnectionStrings()
        {
            var settingsPath = HttpContext.Current.Server.MapPath("~/App_Data/databaseconfig.xml");
            try
            {
                var settingsNode = XElement.Load(settingsPath);
                if (settingsNode.Attribute(key) != null)
                {
                    var connectionString = settingsNode.Attribute(key).Value;
                    RoomieDatabaseContext.ConnectionString = connectionString;
                };
            }
            catch (IOException)
            {
            }
        }
    }
}
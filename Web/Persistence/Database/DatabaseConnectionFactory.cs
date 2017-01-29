using System.Data.SqlClient;

namespace Roomie.Web.Persistence.Database
{
    public static class DatabaseConnectionFactory
    {
        public static string ConnectionString { private get; set; }

        public static SqlConnection Connect()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}

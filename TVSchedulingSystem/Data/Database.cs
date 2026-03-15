using System.Data.SqlClient;

namespace TVSchedulingSystem.Data
{
    public static class Database
    {
        private static string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=TVSchedulingDB;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
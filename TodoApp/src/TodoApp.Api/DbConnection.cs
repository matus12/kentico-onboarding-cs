using System.Configuration;
using TodoApp.Contracts;

namespace TodoApp.Api
{
    public class DbConnection : IDbConnection
    {
        public string DbConnectionString { get; }

        public DbConnection()
        {
            DbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
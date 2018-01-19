using System.Configuration;
using TodoApp.Contracts;

namespace TodoApp.Api
{
    public class DbConnection : IDbConnection
    {
        public DbConnection()
        {
            DbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public string DbConnectionString { get; }
    }
}
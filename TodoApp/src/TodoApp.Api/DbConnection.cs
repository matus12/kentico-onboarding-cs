using System.Configuration;
using TodoApp.Contracts;

namespace TodoApp.Api
{
    internal class DbConnection : IDbConnection
    {
        public string DbConnectionString { get; }

        public DbConnection()
        {
            DbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
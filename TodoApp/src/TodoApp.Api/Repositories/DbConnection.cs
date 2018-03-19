using System.Configuration;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Api.Repositories
{
    internal class DbConnection : IDbConnection
    {
        public string DbConnectionString { get; }

        public DbConnection() 
            => DbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}
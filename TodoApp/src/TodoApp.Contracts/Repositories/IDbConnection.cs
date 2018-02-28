namespace TodoApp.Contracts.Repositories
{
    public interface IDbConnection
    {
        string DbConnectionString { get; }
    }
}
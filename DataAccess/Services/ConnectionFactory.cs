using DataAccess.Services.Connections;

namespace DataAccess.Services;

public class ConnectionFactory : IConnectionFactory
{
    private readonly IPostgresConnection postgresConnection;

    public ConnectionFactory(
        IPostgresConnection postgresConnection)
    {
        this.postgresConnection = postgresConnection;
    }

    public ConnectionWrapper ProduceConnection(DbConnectionList connection)
    {
        return connection switch
        {
            DbConnectionList.Postgres => new ConnectionWrapper { ConnectionName = DbConnectionList.Postgres.ToString(), npgsqlConnection = postgresConnection.GetConnection() },
            _ => throw new InvalidOperationException($"No connection of indicated type {connection} could be returned")
        };
    }
}

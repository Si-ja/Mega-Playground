using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Services.Connections;

public class PostgresConnection : IPostgresConnection
{
    private readonly string connectionId;
    private readonly IConfiguration configuration;

    public PostgresConnection(IConfiguration configuration)
    {
        connectionId = "Postgres";
        this.configuration = configuration;
    }

    public NpgsqlConnection GetConnection()
    {
        NpgsqlConnection connection = new(this.configuration.GetConnectionString(connectionId));
        return connection;
    }
}

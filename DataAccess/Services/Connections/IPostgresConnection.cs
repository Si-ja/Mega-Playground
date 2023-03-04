using Npgsql;

namespace DataAccess.Services.Connections;

public interface IPostgresConnection
{
    NpgsqlConnection GetConnection();
}
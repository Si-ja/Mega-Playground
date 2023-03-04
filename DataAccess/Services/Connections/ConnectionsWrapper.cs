using Npgsql;

namespace DataAccess.Services.Connections;

public class ConnectionWrapper
{
    public string ConnectionName { get; set; }

    public NpgsqlConnection? npgsqlConnection { get; set; }
}

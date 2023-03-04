using DataAccess.Services.Connections;

namespace DataAccess.Services;

public interface IConnectionFactory
{
    ConnectionWrapper ProduceConnection(DbConnectionList connection);
}
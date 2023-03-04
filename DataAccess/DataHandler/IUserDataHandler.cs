using DataAccess.Models;
using DataAccess.Services.Connections;

namespace DataAccess.DataHandler;

public interface IUserDataHandler
{
    Task InsertUserData(UserDataModel user, DbConnectionList connectionName);
}
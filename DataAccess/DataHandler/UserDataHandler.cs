using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Services.Connections;

namespace DataAccess.DataHandler;

public class UserDataHandler : IUserDataHandler
{
    private readonly ISqlDataAccess db;

    public UserDataHandler(ISqlDataAccess db)
    {
        this.db = db;
    }

    public Task InsertUserData(UserDataModel user, DbConnectionList connectionName) =>
        this.db.SaveData<dynamic>(
            StoredProceduresList.LogUserDataInsert,
            new { io_browser = user.Browser, io_endpoint = user.Endpoint, io_time = user.Time },
            connectionName);
}

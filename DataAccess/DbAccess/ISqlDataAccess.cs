using DataAccess.Models;
using DataAccess.Services.Connections;

namespace DataAccess.DbAccess;

public interface ISqlDataAccess
{
    /// <summary>
    /// A call to a function that can return a list of items.
    /// </summary>
    /// <typeparam name="T">Parameters that are passed to the call.</typeparam>
    /// <param name="storedProcedure">A properly defined function name that will be executed including references to parameters used.</param>
    /// <param name="parameters">Parameters filled and used.</param>
    /// <param name="connectionId">Name of the connection that is established with a DB.</param>
    /// <returns></returns>
    Task<dynamic> LoadList<T>(string functionName, T parameters, DbConnectionList connectionName);

    /// <summary>
    /// Asynchronously save data via a stored procedure.
    /// </summary>
    /// <typeparam name="T">Model of the data that is saved.</typeparam>
    /// <param name="storedProcedure">Reference name of the stored procedure used.</param>
    /// <param name="parameters">Parameters to pass to the stored procedure.</param>
    /// <param name="connectionId">Connection reference to connect to a viable database.</param>
    /// <returns>None</returns>
    Task SaveData<T>(string storedProcedure, T parameters, DbConnectionList connectionId);

    /// <summary>
    /// A call to a function that can return a scalar value.
    /// </summary>
    /// <typeparam name="T">Parameters that are passed to the call.</typeparam>
    /// <param name="functionName">A properly defined function name that will be executed including references to parameters used.</param>
    /// <param name="parameters">Parameters filled and used.</param>
    /// <param name="connectionName">Name of the connection that is established with a DB.</param>
    /// <returns></returns>
    Task<dynamic> LoadSingleValue<T>(string functionName, T parameters, DbConnectionList connectionName);
}

using System.Data;
using Dapper;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.Services.Connections;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConnectionFactory connectionFactory;

    public SqlDataAccess(
        IConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public async Task<dynamic> LoadList<T>(
        string functionName,
        T parameters,
        DbConnectionList connectionName)
    {
        ConnectionWrapper connection = connectionFactory.ProduceConnection(connectionName);

        switch ((DbConnectionList)Enum.Parse(typeof(DbConnectionList), connection.ConnectionName))
        {
            case DbConnectionList.Postgres:
                {
                    try
                    {
                        await connection.npgsqlConnection.OpenAsync();
                        var answer = await connection.npgsqlConnection.QueryAsync<T>(
                            sql: functionName, param: parameters, commandType: CommandType.Text);

                        return answer;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Issue with {nameof(LoadSingleValue)} at {ex}.");
                    }
                    finally
                    {
                        await connection.npgsqlConnection.CloseAsync();
                    }
                }
        }

        return default;
    }

    public async Task<dynamic> LoadSingleValue<T>(
        string functionName,
        T parameters,
        DbConnectionList connectionName)
    {
        ConnectionWrapper connection = connectionFactory.ProduceConnection(connectionName);

        switch ((DbConnectionList)Enum.Parse(typeof(DbConnectionList), connection.ConnectionName))
        {
            case DbConnectionList.Postgres:
                {
                    try
                    {
                        await connection.npgsqlConnection.OpenAsync();
                        var answer = await connection.npgsqlConnection.ExecuteScalarAsync<dynamic>(
                            sql: functionName, param: parameters, commandType: CommandType.Text);
                        

                        return answer;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Issue with {nameof(LoadSingleValue)} at {ex}.");
                    }
                    finally
                    {
                        await connection.npgsqlConnection.CloseAsync();
                    }
                }
        }

        return default;
    }

    public async Task SaveData<T>(
        string storedProcedure,
        T parameters,
        DbConnectionList connectionName)
    {
        ConnectionWrapper connection = connectionFactory.ProduceConnection(connectionName);

        switch ((DbConnectionList)Enum.Parse(typeof(DbConnectionList), connection.ConnectionName))
        {
            case DbConnectionList.Postgres:
                {
                    try
                    {
                        await connection.npgsqlConnection.OpenAsync();
                        await connection.npgsqlConnection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    }
                    catch
                    {
                        throw new Exception($"Issue with {nameof(SaveData)}");
                    }
                    finally
                    {
                        await connection.npgsqlConnection.CloseAsync();
                    }
                }

                break;

            default:
                break;
        }
    }
}

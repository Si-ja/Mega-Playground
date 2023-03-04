using DataAccess.Models;

namespace StocksAPI.Services.DataCollection;

public interface IUserDataCollector
{
    /// <summary>
    /// Collect data on the user based on their request.
    /// </summary>
    /// <param name="httpRequest">Http request that comes from the user.</param>
    /// <returns></returns>
    Task<UserDataModel> GetUserDataAsync(HttpRequest httpRequest);
}

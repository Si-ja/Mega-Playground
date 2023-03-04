using DataAccess.Models;
using MonitoringTools.Prometheus;

namespace StocksAPI.Services.DataCollection;

public class UserDataCollector : IUserDataCollector
{
    private readonly ILogger<UserDataCollector> logger;
    private readonly IMonitoringMetrics monitoringMetrics;
    private readonly UserHeadersListSettings userHeadersListSettings;

    private readonly string parseAction = "Parse";

    public UserDataCollector(
        ILogger<UserDataCollector> logger,
        IMonitoringMetrics monitoringMetrics,
        IConfiguration configuration)
    {
        this.logger = logger;
        this.monitoringMetrics = monitoringMetrics;
        this.userHeadersListSettings = configuration
            .GetSection(nameof(UserHeadersListSettings))
            .Get<UserHeadersListSettings>();
    }

    public async Task<UserDataModel> GetUserDataAsync(HttpRequest httpRequest)
    {
        UserDataModel userData = new(await ReturnCurrentDateTime());

        if (this.userHeadersListSettings.Headers.Count == 0)
        {
            return userData;
        }

        foreach (var header in httpRequest.Headers)
        {
            if (this.userHeadersListSettings.Headers.Contains(header.Key))
            {
                string key = header.Key;

                // Prepare the key for identification
                key = key.Replace("-", "");
                key = key.Trim();

                // Decide which component now needs to be prepared
                switch (string.Concat(this.parseAction, key))
                {

                    case (nameof(ParseUserAgent)):
                        userData.Browser = await ParseUserAgent(header.Value);
                        break;

                    case (nameof(ParseReferer)):
                        userData.Endpoint = await ParseReferer(header.Value);
                        break;

                    default:
                        break;
                }
            }
        }

        return userData;
    }

    private static Task<string> ReturnCurrentDateTime()
    {
        return Task.FromResult(DateTime.Now.ToString("O"));
    }

    private static Task<string> ParseUserAgent(string userAgentData)
    {
        // Find the type of a browser the user is using
        // By all indications it seems to be the last components
        string[] browserComponents = userAgentData.Split(" ");
        string browser = browserComponents.Last();

        return Task.FromResult(browser);
    }

    private static Task<string> ParseReferer(string refererData)
    {
        // Referer is just an indication what page was triggered
        // No parsing at the current time is needed
        return Task.FromResult(refererData);
    }
}

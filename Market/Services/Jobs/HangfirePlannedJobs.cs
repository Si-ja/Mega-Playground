using Hangfire;
using Hangfire.PostgreSql;

namespace Market.Services.Jobs;

public static class HangfirePlannedJobs
{
    public static IServiceCollection AddHangfirePlannedJobs(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddHangfire(x =>
        {
            x.UsePostgreSqlStorage(configuration.GetConnectionString("Postgres"));
        });

        service.AddHangfireServer();

        return service;
    }
}

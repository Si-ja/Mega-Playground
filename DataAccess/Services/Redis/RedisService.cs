using DataAccess.Services.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Services.Redis;

public static class RedisService
{
    public static IServiceCollection AddRedisService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisSettings = configuration
            .GetSection(nameof(RedisSettings))
            .Get<RedisSettings>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString(nameof(DbConnectionList.Redis));
            options.InstanceName = redisSettings.InstanceName;
        });

        return services;
    }
}

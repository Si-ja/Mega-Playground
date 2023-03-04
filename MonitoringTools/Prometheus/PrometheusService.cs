using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace MonitoringTools.Prometheus;

// Good reading: https://www.c-sharpcorner.com/article/reading-values-from-appsettings-json-in-asp-net-core/

public static class PrometheusService
{
    public static IServiceCollection AddPrometheusServer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var prometheusSettings = configuration
            .GetSection(nameof(PrometheusSettings))
            .Get<PrometheusSettings>();

        services
            .AddSingleton<IMonitoringMetrics, MonitoringMetrics>();

        Run(prometheusSettings);

        return services;
    }

    private static void Run(PrometheusSettings settings)
    {
        var metricServer = new KestrelMetricServer(port: settings.Port);
        metricServer.Start();
    }
}

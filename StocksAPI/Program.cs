using Serilog;
using StocksAPI.Services.DataCollection;
using DataAccess.DataHandler;
using DataAccess.DbAccess;
using DataAccess.Services.Connections;
using DataAccess.Services;
using MonitoringTools.Prometheus;
using StocksAPI.Services.StocksRetrieval;
using DataAccess.Services.Redis;

namespace StocksAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adding Settings that we can have
        builder.Services.Configure<PrometheusSettings>(builder.Configuration.GetSection(nameof(PrometheusSettings)));

        // Add Main services to the container.
        builder.Services.AddScoped<IStocksDataRetriever, StocksDataRetriever>();
        builder.Services.AddScoped<IUserDataCollector, UserDataCollector>();

        // Add Database communication services to the container.
        builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
        builder.Services.AddScoped<IUserDataHandler, UserDataHandler>();
        builder.Services.AddScoped<IStocksDataHandler, StocksDataHandler>();
        builder.Services.AddScoped<IPostgresConnection, PostgresConnection>();
        builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();
        builder.Services.AddScoped<IStocksReferenceDataRetriever, StocksReferenceDataRetriever>();

        // Add Prometheus as a service to operate with
        builder.Services.AddPrometheusServer(builder.Configuration);

        // Add communication with Redis
        builder.Services.AddRedisService(builder.Configuration);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add a Logger
       var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.SetIsOriginAllowed(
                    origin => new Uri(origin).Host == "localhost"
                    );
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

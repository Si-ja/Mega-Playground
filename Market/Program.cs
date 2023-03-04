using DataAccess.DataHandler;
using DataAccess.DbAccess;
using DataAccess.Services;
using DataAccess.Services.Connections;
using Hangfire;
using Hangfire.Dashboard;
using Market.Services.Jobs;
using Market.Services.StocksGenerator;
using Serilog;

namespace Market
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add developed services
            builder.Services.AddScoped<IRecurringJobsService, RecurringJobsService>();
            builder.Services.AddScoped<IStocksDataGenerator, StocksDataGenerator>();

            // Add Database communication services to the container.
            builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddScoped<IStocksDataHandler, StocksDataHandler>();
            builder.Services.AddScoped<IPostgresConnection, PostgresConnection>();
            builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();

            // Add Hangfire with needed relations
            builder.Services.AddHangfirePlannedJobs(builder.Configuration);

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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var options = new DashboardOptions()
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            };

            app.UseHangfireDashboard("/hangfire", options);

            app.MapControllers();

            app.Run();
        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context) => true;
        }
    }
}

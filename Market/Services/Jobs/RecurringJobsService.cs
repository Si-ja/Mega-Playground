using Hangfire;
using Market.Services.StocksGenerator;
using Market.Settings;

namespace Market.Services.Jobs;

public class RecurringJobsService : IRecurringJobsService
{
    private readonly ILogger<RecurringJobsService> logger;
    private readonly IBackgroundJobClient backgroundJobClient;
    private readonly IRecurringJobManager recurringJobManager;
    private readonly IStocksDataGenerator stocksDataGenerator;
    private readonly IConfiguration configuration;

    public RecurringJobsService(
        ILogger<RecurringJobsService> logger,
        IBackgroundJobClient backgroundJobClient,
        IRecurringJobManager recurringJobManager,
        IStocksDataGenerator stocksDataGenerator,
        IConfiguration configuration)
    {
        this.logger = logger;
        this.backgroundJobClient = backgroundJobClient;
        this.recurringJobManager = recurringJobManager;
        this.stocksDataGenerator = stocksDataGenerator;
        this.configuration = configuration;
    }

    public void CreateRecurringJobs()
    {
        var recurringCrons = configuration
                                .GetSection(nameof(JobSettings))
                                .Get<JobSettings>();

        foreach (var recurringCron in recurringCrons.Stocks)
        {
            logger.LogInformation("Creating job for {NameId}", recurringCron.NameId);
            switch (recurringCron.StockName)
            {
                case "XYZStock":
                    recurringJobManager.AddOrUpdate(
                        recurringCron.NameId,
                        () => stocksDataGenerator.GenerateXYZStockAsync(),
                        recurringCron.Cron);
                    break;

                case "EvilCorpStock":
                    recurringJobManager.AddOrUpdate(
                        recurringCron.NameId,
                        () => stocksDataGenerator.GenerateEvilCorpStockAsync(),
                        recurringCron.Cron);
                    break;

                case "7734Stock":
                    recurringJobManager.AddOrUpdate(
                        recurringCron.NameId,
                        () => stocksDataGenerator.GenerateHellStockAsync(),
                        recurringCron.Cron);
                    break;
            }
            logger.LogInformation($"Job created for {recurringCron.NameId}");
        }
    }

}

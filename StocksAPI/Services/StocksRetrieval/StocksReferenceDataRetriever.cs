using DataAccess.DataHandler;
using DataAccess.Models;
using DataAccess.Services.Connections;
using MonitoringTools.Prometheus;
using DataAccess.Services.Redis.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using DataAccess.Services.Redis;

namespace StocksAPI.Services.StocksRetrieval
{
    public class StocksReferenceDataRetriever : IStocksReferenceDataRetriever
    {
        private readonly ILogger<StocksReferenceDataRetriever> logger;
        private readonly IStocksDataHandler stocksDataHandler;
        private readonly IMonitoringMetrics monitoringMetrics;
        private readonly IDistributedCache cache;
        private readonly RedisSettings redisSettings;

        public StocksReferenceDataRetriever(
            IConfiguration configuration,
            ILogger<StocksReferenceDataRetriever> logger,
            IStocksDataHandler stocksDataHandler,
            IMonitoringMetrics monitoringMetrics,
            IDistributedCache cache)
        {
            this.logger = logger;
            this.stocksDataHandler = stocksDataHandler;
            this.monitoringMetrics = monitoringMetrics;
            this.cache = cache;

            this.redisSettings = configuration
                .GetSection(nameof(RedisSettings))
                .Get<RedisSettings>();
        }

        public async Task<List<StockReferencesModel>> GetStocksReferenceAsync()
        {
            try
            {
                // Check the cache first
                List<StockReferencesModel> stocksReference = new();
                string recordKey = "StocksApiList_" + DateTime.Now.ToString(redisSettings.RecordKeyForDate);

                try
                {
                    stocksReference = await cache.GetRecordAsync<List<StockReferencesModel>>(recordKey);
                }
                catch (Exception e)
                {
                    this.logger.LogError("An error has been encountered attempting to load data from Redis. Error: {e}", e);
                }

                if (stocksReference == null)
                {
                    // If nothing is found in the cache proceed with calling of the DB
                    stocksReference = await stocksDataHandler.GetListOfAvailableStocks(DbConnectionList.Postgres);
                    this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Postgres)}_{nameof(stocksDataHandler.GetListOfAvailableStocks)}");

                    await cache.SetRecordAsync<List<StockReferencesModel>>(
                        recordKey,
                        stocksReference,
                        TimeSpan.FromSeconds(redisSettings.AbsoluteExpirationRelativeToNow ?? default),
                        TimeSpan.FromSeconds(redisSettings.SlidingExpiration ?? default));
                } 
                else
                {
                    // Make a log of the load from the cache
                    this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Redis)}_{nameof(stocksDataHandler.GetListOfAvailableStocks)}");
                }

                if (stocksReference == null || stocksReference == default)
                {
                    return default;
                }

                return stocksReference;
            }
            catch (Exception e)
            {
                throw new NullReferenceException("An issue ocurred collection data on stocks present. Error: {Error}", e);
            }
        }
    }
}

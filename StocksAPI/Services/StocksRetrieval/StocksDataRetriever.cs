using Data.Models.Stocks;
using DataAccess.DataHandler;
using DataAccess.Models;
using DataAccess.Services.Connections;
using DataAccess.Services.Redis;
using DataAccess.Services.Redis.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using MonitoringTools.Prometheus;

namespace StocksAPI.Services.StocksRetrieval;

public class StocksDataRetriever : IStocksDataRetriever
{
    private readonly ILogger<StocksDataRetriever> logger;
    private readonly IStocksDataHandler stocksDataHandler;
    private readonly IMonitoringMetrics monitoringMetrics;
    private readonly IDistributedCache cache;
    private readonly RedisSettings redisSettings;

    public StocksDataRetriever(
        IConfiguration configuration,
        IStocksDataHandler stocksDataHandler,
        IMonitoringMetrics monitoringMetrics,
        ILogger<StocksDataRetriever> logger,
        IDistributedCache cache)
    {
        this.stocksDataHandler = stocksDataHandler;
        this.monitoringMetrics = monitoringMetrics;
        this.logger = logger;
        this.cache = cache;

        this.redisSettings = configuration
            .GetSection(nameof(RedisSettings))
            .Get<RedisSettings>();
    }

    public async Task<StockModel> GetXYZStockAsync()
    {
        return await this.GetStockDataAsync("XYZStock");
    }

    public async Task<StockModel> GetEvilCorpStockAsync()
    {
        return await this.GetStockDataAsync("EvilCorpStock");
    }

    public async Task<StockModel> GetHellStockAsync()
    {
        return await this.GetStockDataAsync("HellStock");
    }

    private async Task<StockModel> GetStockDataAsync(string stockName)
    {
        try
        {
            StockDataModel stockDbData;

            switch (stockName)
            {
                case "XYZStock":
                    XYZStock xYZStock = new();
                    string recordKey_XYZStock = $"StocksApi_{nameof(XYZStock)}_" + DateTime.Now.ToString(redisSettings.RecordKeyForDate);
                    xYZStock = await this.cache.GetRecordAsync<XYZStock>(recordKey_XYZStock);
                    
                    if (xYZStock == null)
                    {
                        stockDbData = await stocksDataHandler.GetStockPrice(nameof(XYZStock), DbConnectionList.Postgres);
                        if (stockDbData != null || stockDbData != default)
                        {
                            xYZStock = new()
                            {
                                Date = RetrieveCurrentTime().ConventionalDateTime(),
                                Price = stockDbData.Price.ConventionalPrice()
                            };
                        }

                        this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Postgres)}_{nameof(stocksDataHandler.GetStockPrice)}_{nameof(XYZStock)}");
                        await cache.SetRecordAsync<XYZStock>(
                            recordKey_XYZStock,
                            xYZStock,
                            TimeSpan.FromSeconds(redisSettings.AbsoluteExpirationRelativeToNow ?? default),
                            TimeSpan.FromSeconds(redisSettings.SlidingExpiration ?? default));
                    }
                    else
                    {
                        this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Redis)}_{nameof(stocksDataHandler.GetStockPrice)}_{nameof(XYZStock)}");
                    }

                    return xYZStock;

                case "EvilCorpStock":
                    EvilCorpStock evilCorpStock = new();
                    string recordKey_EvilCorpStock = $"StocksApi_{nameof(EvilCorpStock)}_" + DateTime.Now.ToString(redisSettings.RecordKeyForDate);
                    evilCorpStock = await this.cache.GetRecordAsync<EvilCorpStock>(recordKey_EvilCorpStock);

                    if (evilCorpStock == null)
                    {
                        stockDbData = await stocksDataHandler.GetStockPrice(nameof(EvilCorpStock), DbConnectionList.Postgres);
                        if (stockDbData != null || stockDbData != default)
                        {
                            evilCorpStock = new()
                            {
                                Date = RetrieveCurrentTime().ConventionalDateTime(),
                                Price = stockDbData.Price.ConventionalPrice()
                            };
                        }

                        this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Postgres)}_{nameof(stocksDataHandler.GetStockPrice)}_{nameof(EvilCorpStock)}");
                        await cache.SetRecordAsync<EvilCorpStock>(
                            recordKey_EvilCorpStock,
                            evilCorpStock,
                            TimeSpan.FromSeconds(redisSettings.AbsoluteExpirationRelativeToNow ?? default),
                            TimeSpan.FromSeconds(redisSettings.SlidingExpiration ?? default));
                    }
                    else
                    {
                        this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Redis)}_{nameof(stocksDataHandler.GetStockPrice)}_{nameof(EvilCorpStock)}");
                    }

                    return evilCorpStock;

                case "HellStock":
                    HellStock hellStock = new();
                    string recordKey_HellStock = $"StocksApi_{nameof(HellStock)}_" + DateTime.Now.ToString(redisSettings.RecordKeyForDate);
                    hellStock = await this.cache.GetRecordAsync<HellStock>(recordKey_HellStock);

                    if (hellStock == null)
                    {
                        stockDbData = await stocksDataHandler.GetStockPrice(nameof(HellStock), DbConnectionList.Postgres);
                        if (stockDbData != null || stockDbData != default)
                        {
                            hellStock = new()
                            {
                                Date = RetrieveCurrentTime().ConventionalDateTime(),
                                Price = stockDbData.Price.ConventionalPrice()
                            };
                        }

                        this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Postgres)}_{nameof(stocksDataHandler.GetStockPrice)}_{nameof(HellStock)}");
                        await cache.SetRecordAsync<HellStock>(
                            recordKey_HellStock,
                            hellStock,
                            TimeSpan.FromSeconds(redisSettings.AbsoluteExpirationRelativeToNow ?? default),
                            TimeSpan.FromSeconds(redisSettings.SlidingExpiration ?? default));
                    }
                    else
                    {
                        this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Redis)}_{nameof(stocksDataHandler.GetStockPrice)}_{nameof(HellStock)}");
                    }

                    return hellStock;

                default:
                    throw new ArgumentException("No stock exists for the name provided of {stockName}", stockName);
            }
        }
        catch (Exception e)
        {
            throw new NullReferenceException($"An issue ocurred providing stock data. Error: {e}");
        }
    }

    private static DateTime RetrieveCurrentTime()
    {
        return DateTime.Now;
    }
}

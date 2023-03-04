using Data.Models.Stocks;
using DataAccess.DataHandler;
using DataAccess.Models;
using DataAccess.Services.Connections;
using MonitoringTools.Prometheus;

namespace StocksAPI.Services.StocksRetrieval;

public class StocksDataRetriever : IStocksDataRetriever
{
    private readonly ILogger<StocksDataRetriever> logger;
    private readonly IStocksDataHandler stocksDataHandler;
    private readonly IMonitoringMetrics monitoringMetrics;

    public StocksDataRetriever(
        IStocksDataHandler stocksDataHandler,
        IMonitoringMetrics monitoringMetrics,
        ILogger<StocksDataRetriever> logger)
    {
        this.stocksDataHandler = stocksDataHandler;
        this.monitoringMetrics = monitoringMetrics;
        this.logger = logger;
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
        this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Postgres)}_{nameof(stocksDataHandler.GetStockPrice)}");

        try
        {
            StockDataModel stockDbData;

            switch (stockName)
            {
                case "XYZStock":
                    stockDbData = await stocksDataHandler.GetStockPrice(nameof(XYZStock), DbConnectionList.Postgres);
                    if (stockDbData != null || stockDbData != default)
                    {
                        XYZStock xYZStock = new()
                        {
                            Date = RetrieveCurrentTime().ConventionalDateTime(),
                            Price = stockDbData.Price.ConventionalPrice()
                        };
                        return xYZStock;
                    }
                    else
                    {
                        return new XYZStock();
                    }

                case "EvilCorpStock":
                    stockDbData = await stocksDataHandler.GetStockPrice(nameof(EvilCorpStock), DbConnectionList.Postgres);
                    if (stockDbData != null || stockDbData != default)
                    {
                        EvilCorpStock evilCorpStock = new()
                        {
                            Date = RetrieveCurrentTime().ConventionalDateTime(),
                            Price = stockDbData.Price.ConventionalPrice()
                        };
                        return evilCorpStock;
                    }
                    else
                    {
                        return new EvilCorpStock();
                    }

                case "HellStock":
                    stockDbData = await stocksDataHandler.GetStockPrice(nameof(HellStock), DbConnectionList.Postgres);
                    if (stockDbData != null || stockDbData != default)
                    {
                        HellStock hellStock = new()
                        {
                            Date = RetrieveCurrentTime().ConventionalDateTime(),
                            Price = stockDbData.Price.ConventionalPrice()
                        };
                        return hellStock;
                    }
                    else
                    {
                        return new HellStock();
                    }

                default:
                    throw new ArgumentException("No stock exists for the name provided of {stockName}", stockName);
            }
        }
        catch (Exception e)
        {
            throw new NullReferenceException("An issue ocurred providing stock data. Error: {Error}", e);
        }
    }

    private static DateTime RetrieveCurrentTime()
    {
        return DateTime.Now;
    }
}

using Data.Models.Stocks;
using DataAccess.DataHandler;
using DataAccess.Services.Connections;

namespace Market.Services.StocksGenerator;

public class StocksDataGenerator : IStocksDataGenerator
{
    private readonly ILogger<StocksDataGenerator> logger;
    private readonly IStocksDataHandler stocksDataHandler;

    public StocksDataGenerator(
        ILogger<StocksDataGenerator> logger,
        IStocksDataHandler stocksDataHandler)
    {
        this.logger = logger;
        this.stocksDataHandler = stocksDataHandler;
    }

    public async Task<StockModel> GenerateXYZStockAsync()
    {
        XYZStock xYZStock = new()
        {
            Date = RetrieveCurrentTime().ConventionalDateTime(),
            Price = RetrieveCurrentStockPrice().ConventionalPrice()
        };

        await stocksDataHandler.InsertStockData(xYZStock, DbConnectionList.Postgres);
        await stocksDataHandler.InsertHistoricalDataRecord(xYZStock, DbConnectionList.Postgres);
        
        return xYZStock;
    }

    public async Task<StockModel> GenerateEvilCorpStockAsync()
    {
        EvilCorpStock evilCorpStock = new()
        {
            Date = RetrieveCurrentTime().ConventionalDateTime(),
            Price = RetrieveCurrentStockPrice().ConventionalPrice()
        };

        await stocksDataHandler.InsertStockData(evilCorpStock, DbConnectionList.Postgres);
        await stocksDataHandler.InsertHistoricalDataRecord(evilCorpStock, DbConnectionList.Postgres);

        return evilCorpStock;
    }

    public async Task<StockModel> GenerateHellStockAsync()
    {
        HellStock hellStock = new()
        {
            Date = RetrieveCurrentTime().ConventionalDateTime(),
            Price = RetrieveCurrentStockPrice().ConventionalPrice()
        };

        await stocksDataHandler.InsertStockData(hellStock, DbConnectionList.Postgres);
        await stocksDataHandler.InsertHistoricalDataRecord(hellStock, DbConnectionList.Postgres);

        return hellStock;
    }

    public StockModel GetBlankStock()
    {
        StockModel stock = new(Name: "Unknown", Currency: "Unknown")
        {
            Date = RetrieveCurrentTime().ConventionalDateTime(),
            Price = RetrieveCurrentStockPriceZeroedOut().ConventionalPrice()
        };

        return stock;
    }

    private static DateTime RetrieveCurrentTime()
    {
        return DateTime.Now;
    }

    private static float RetrieveCurrentStockPrice(int maxRange = 400)
    {
        Random random = new();
        int minRange = 0;

        float randomPrice = random.Next(minRange, maxRange) + random.NextSingle();

        return randomPrice;
    }

    private static float RetrieveCurrentStockPriceZeroedOut()
    {
        return 0;
    }
}

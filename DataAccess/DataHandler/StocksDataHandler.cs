using Data.Models.Stocks;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Services.Connections;

namespace DataAccess.DataHandler;

public class StocksDataHandler : IStocksDataHandler
{
    private readonly ISqlDataAccess db;

    public StocksDataHandler(ISqlDataAccess db)
    {
        this.db = db;
    }

    public async Task<List<StockReferencesModel>> GetListOfAvailableStocks(DbConnectionList connectionName)
    {
        var answer = await this.db.LoadList<StockReferencesModel>(
            StoredProceduresList.LogAllStocksReferenceLoad,
            null,
            connectionName);

        return answer;
    }

    public Task InsertStockData(StockModel stock, DbConnectionList connectionName) =>
        this.db.SaveData<dynamic>(
            StoredProceduresList.LogStockDataInsert,
            new { io_name = stock.Name, io_price = stock.Price },
            connectionName);

    public Task InsertHistoricalDataRecord(StockModel stock, DbConnectionList connectionName) =>
        this.db.SaveData<dynamic>(
            StoredProceduresList.LogHistoricalStockPriceInsert,
            new { io_name = stock.Name, io_price = stock.Price, io_time = stock.Date },
            connectionName);

    public async Task<StockDataModel> GetStockPrice(string stockName, DbConnectionList connectionName)
    {
        var answer = await this.db.LoadSingleValue<dynamic>(
            StoredProceduresList.LogStockDataLoadFunctionScalar,
            new { io_name = stockName },
            connectionName);

        StockDataModel stockData = new();
        if (answer != null)
        {
            try
            {
                stockData.Name = stockName;
                stockData.Price = (float)answer;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.ToString());
            }
        }

        return stockData;
    }
}

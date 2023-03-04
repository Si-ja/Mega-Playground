using Data.Models.Stocks;
using DataAccess.Models;
using DataAccess.Services.Connections;

namespace DataAccess.DataHandler
{
    public interface IStocksDataHandler
    {
        Task<List<StockReferencesModel>> GetListOfAvailableStocks(DbConnectionList connectionName);

        Task InsertStockData(StockModel stock, DbConnectionList connectionName);

        Task InsertHistoricalDataRecord(StockModel stock, DbConnectionList connectionName);

        Task<StockDataModel> GetStockPrice(string stockName, DbConnectionList connectionName);
    }
}

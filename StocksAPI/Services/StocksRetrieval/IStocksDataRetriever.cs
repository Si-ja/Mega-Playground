using Data.Models.Stocks;

namespace StocksAPI.Services.StocksRetrieval;

public interface IStocksDataRetriever
{
    /// <summary>
    /// GET XYZStock information.
    /// </summary>
    /// <returns>Date on XYZStock for the current moment.</returns>
    Task<StockModel> GetXYZStockAsync();

    /// <summary>
    /// GET EvilCorpStock information.
    /// </summary>
    /// <returns>Date on EvilCorpStock for the current moment.</returns>
    Task<StockModel> GetEvilCorpStockAsync();

    /// <summary>
    /// GET HellStock information.
    /// </summary>
    /// <returns>Date on HellStock for the current moment.</returns>
    Task<StockModel> GetHellStockAsync();
}

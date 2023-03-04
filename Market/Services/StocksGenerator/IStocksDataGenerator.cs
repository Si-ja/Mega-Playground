using Data.Models.Stocks;

namespace Market.Services.StocksGenerator
{
    public interface IStocksDataGenerator
    {
        /// <summary>
        /// Generate XYZStock information.
        /// </summary>
        /// <returns>Data on XYZStock for the current moment.</returns>
        Task<StockModel> GenerateXYZStockAsync();

        /// <summary>
        /// Generate EvilCorpStock information.
        /// </summary>
        /// <returns>Data on EvilCorpStock for the current moment.</returns>
        Task<StockModel> GenerateEvilCorpStockAsync();

        /// <summary>
        /// Generate HellStock information.
        /// </summary>
        /// <returns>Data on HellStock for the current moment.</returns>
        Task<StockModel> GenerateHellStockAsync();

        /// <summary>
        /// Get Blank stocks information. Essentially should never be triggered, and is a sanity check method.
        /// </summary>
        /// <returns>Data on a Blank stock.</returns>
        StockModel GetBlankStock();
    }
}

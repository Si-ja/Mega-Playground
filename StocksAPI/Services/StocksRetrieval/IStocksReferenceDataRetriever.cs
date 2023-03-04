using DataAccess.Models;

namespace StocksAPI.Services.StocksRetrieval
{
    public interface IStocksReferenceDataRetriever
    {
        /// <summary>
        /// Retrieve information on the stocks that are present in the api.
        /// </summary>
        /// <returns>A list of stock names.</returns>
        Task<List<StockReferencesModel>> GetStocksReferenceAsync();
    }
}

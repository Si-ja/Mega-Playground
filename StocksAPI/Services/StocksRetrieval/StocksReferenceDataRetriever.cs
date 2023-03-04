using DataAccess.DataHandler;
using DataAccess.Models;
using DataAccess.Services.Connections;
using MonitoringTools.Prometheus;

namespace StocksAPI.Services.StocksRetrieval
{
    public class StocksReferenceDataRetriever : IStocksReferenceDataRetriever
    {
        private readonly ILogger<StocksReferenceDataRetriever> logger;
        private readonly IStocksDataHandler stocksDataHandler;
        private readonly IMonitoringMetrics monitoringMetrics;

        public StocksReferenceDataRetriever(
            ILogger<StocksReferenceDataRetriever> logger,
            IStocksDataHandler stocksDataHandler,
            IMonitoringMetrics monitoringMetrics)
        {
            this.logger = logger;
            this.stocksDataHandler = stocksDataHandler;
            this.monitoringMetrics = monitoringMetrics;
        }

        public async Task<List<StockReferencesModel>> GetStocksReferenceAsync()
        {
            try
            {
                var stocksReference = await stocksDataHandler.GetListOfAvailableStocks(DbConnectionList.Postgres);
                this.monitoringMetrics.IncrementUserMadeRequest($"{nameof(DbConnectionList.Postgres)}_{nameof(stocksDataHandler.GetListOfAvailableStocks)}");

                if (stocksReference == null || stocksReference == default)
                {
                    return new List<StockReferencesModel>();
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

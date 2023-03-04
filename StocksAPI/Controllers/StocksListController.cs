using DataAccess.DataHandler;
using DataAccess.Models;
using DataAccess.Services.Connections;
using Microsoft.AspNetCore.Mvc;
using MonitoringTools.Prometheus;
using StocksAPI.Services.StocksRetrieval;

namespace StocksAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StocksListController : ControllerBase
    {
        private readonly ILogger<StocksListController> logger;
        private readonly IMonitoringMetrics monitoringMetrics;
        private readonly IStocksReferenceDataRetriever stocksReferenceDataRetriever;

        public StocksListController(
            ILogger<StocksListController> logger,
            IMonitoringMetrics monitoringMetrics,
            IStocksReferenceDataRetriever stocksReferenceDataRetriever)
        {
            this.logger = logger;
            this.monitoringMetrics = monitoringMetrics;
            this.stocksReferenceDataRetriever = stocksReferenceDataRetriever;
        }

        [HttpGet(Name = "GetStocksList")]
        public async Task<List<StockReferencesModel>> Get()
        {
            var answer = await stocksReferenceDataRetriever.GetStocksReferenceAsync();

            return answer;
        }
    }
}

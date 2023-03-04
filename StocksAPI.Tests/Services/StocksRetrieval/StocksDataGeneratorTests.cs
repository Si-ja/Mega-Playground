using DataAccess.DataHandler;
using DataAccess.Services.Connections;
using Data.Models.Stocks;
using Microsoft.Extensions.Logging;
using Moq;
using DataAccess.Models;
using MonitoringTools.Prometheus;
using StocksAPI.Services.StocksRetrieval;

namespace StocksAPI.Tests.Services.StocksRetrieval
{
    public class StocksDataGeneratorTests
    {
        private readonly Mock<IStocksDataHandler> _mockStocksDataHandler = new();
        private readonly Mock<IMonitoringMetrics> _mockMonitoringMetrics = new();
        private readonly Mock<ILogger<StocksDataRetriever>> _mockLogger = new();

        [Fact]
        public async Task When_GetXYZStockMethodIsCalledAndDbIsSuccessful_Then_RetrieveStockInformationAsync()
        {
            // Arrange
            _mockStocksDataHandler.Setup(
                s => s.GetStockPrice(It.IsAny<string>(), It.IsAny<DbConnectionList>())).Returns(Task.FromResult(new StockDataModel() { Price = 100 }));

            var sut = GetSut();

            // Act
            var stock = await sut.GetXYZStockAsync();

            // Assert
            Assert.IsType<string>(stock.Name);
            Assert.IsType<string>(stock.Date);
            Assert.IsType<string>(stock.Currency);
            Assert.IsType<decimal>(stock.Price);

            Assert.Equal(nameof(XYZStock), stock.Name); // Stock name should still be labeled correctly no matter what;
            Assert.NotEqual(0, stock.Price); // A stock not updated with data from the DB will always have a 0 value
            Assert.NotNull(stock);
        }

        [Fact]
        public async Task When_GetXYZStockMethodIsCalledAndDbDoesntReturnAnythingValid_Then_ReturnBlankStockData()
        {
            // Arrange
            _mockStocksDataHandler.Setup(
                s => s.GetStockPrice(It.IsAny<string>(), It.IsAny<DbConnectionList>())).Returns(Task.FromResult(new StockDataModel()));

            var sut = GetSut();

            // Act
            var stock = await sut.GetXYZStockAsync();

            // Assert
            Assert.IsType<string>(stock.Name);
            Assert.IsType<string>(stock.Date);
            Assert.IsType<string>(stock.Currency);
            Assert.IsType<decimal>(stock.Price);

            Assert.Equal(nameof(XYZStock), stock.Name); // Stock name should be labeled correctly no matter what;
            Assert.Equal(0, stock.Price); // A stock not updated with data from the DB will always have a 0 value
            Assert.NotNull(stock);
        }

        public StocksDataRetriever GetSut()
        {
            return new StocksDataRetriever(
                _mockStocksDataHandler.Object,
                _mockMonitoringMetrics.Object,
                _mockLogger.Object);
        }
    }
}

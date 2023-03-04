using DataAccess.DataHandler;
using DataAccess.Services.Connections;
using Data.Models.Stocks;
using Microsoft.Extensions.Logging;
using Moq;
using DataAccess.Models;
using MonitoringTools.Prometheus;
using StocksAPI.Services.StocksRetrieval;
using FluentAssertions;

namespace StocksAPI.Tests.Services.StocksRetrieval
{
    public class StocksReferenceDataRetrieverTests
    {
        private readonly Mock<IStocksDataHandler> _mockStocksDataHandler = new();
        private readonly Mock<IMonitoringMetrics> _mockMonitoringMetrics = new();
        private readonly Mock<ILogger<StocksReferenceDataRetriever>> _mockLogger = new();

        [Fact]
        public async Task When_GetStocksReferenceAsyncMethodIsCalledAndDbIsSuccessful_Then_RetrieveStockInformationAsync()
        {
            // Arrange
            List<StockReferencesModel> replyList = new();
            string stockName = "Test";
            replyList.Add(new StockReferencesModel { StockName = stockName });

            _mockStocksDataHandler.Setup(
                s => s.GetListOfAvailableStocks(It.IsAny<DbConnectionList>())).Returns(Task.FromResult(replyList));

            var sut = GetSut();

            // Act
            var stock = await sut.GetStocksReferenceAsync();

            // Assert
            Assert.IsType<List<StockReferencesModel>>(stock);
            stock.Should().NotBeNull();
            var castedStock = stock.FirstOrDefault();
            castedStock.StockName.Should().Be(stockName);
        }

        
        [Fact]
        public async Task When_GetStocksReferenceAsyncMethodIsCalledAndDbDoesntReturnAnythingValid_Then_ReturnBlankList()
        {
            // Arrange
            List<StockReferencesModel> replyList = new();
            _mockStocksDataHandler.Setup(
                s => s.GetListOfAvailableStocks(It.IsAny<DbConnectionList>())).Returns(Task.FromResult(replyList));

            var sut = GetSut();

            // Act
            var stock = await sut.GetStocksReferenceAsync();

            // Assert
            Assert.IsType<List<StockReferencesModel>>(stock);
            stock.Should().BeNullOrEmpty();
        }
        

        public StocksReferenceDataRetriever GetSut()
        {
            return new StocksReferenceDataRetriever(
                _mockLogger.Object,
                _mockStocksDataHandler.Object,
                _mockMonitoringMetrics.Object);
        }
    }
}

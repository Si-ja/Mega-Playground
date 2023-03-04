using Data.Models.Stocks;
using DataAccess.DataHandler;
using DataAccess.Models;
using DataAccess.Services.Connections;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MonitoringTools.Prometheus;
using Moq;
using StocksAPI.Controllers;
using StocksAPI.Services.DataCollection;
using StocksAPI.Services.StocksRetrieval;

namespace StocksAPI.Tests.Controllers;

public class StocksListControllerTests
{
    private readonly Mock<ILogger<StocksListController>> _mockLogger = new ();
    private readonly Mock<IMonitoringMetrics> _mockMonitoringMetrics = new ();
    private readonly Mock<IStocksReferenceDataRetriever> _mockStocksReferenceDataRetriever = new ();

    [Fact]
    public async Task When_StocksListControllerCalled_Then_RetrieveDataAboutKnownStocksOnce()
    {
        // Arrange
        _mockStocksReferenceDataRetriever.Setup(
            s => s.GetStocksReferenceAsync()).ReturnsAsync(new List<StockReferencesModel>());

        var sut = GetSut();

        // Act
        var output = await sut.Get();

        // Assert
        _mockStocksReferenceDataRetriever.Verify(
            v => v.GetStocksReferenceAsync(), Times.Once);

        output.Should().BeEmpty();
    }

    public StocksListController GetSut()
    {
        return new StocksListController(
            this._mockLogger.Object,
            this._mockMonitoringMetrics.Object,
            this._mockStocksReferenceDataRetriever.Object);
    }
}

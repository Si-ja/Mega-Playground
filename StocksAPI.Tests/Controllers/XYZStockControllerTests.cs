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

public class XYZStockControllerTests
{
    private readonly Mock<ILogger<XYZStockController>> _mockLogger = new ();
    private readonly Mock<IStocksDataRetriever> _mockStocksDataRetriever = new ();
    private readonly Mock<IMonitoringMetrics> _mockMonitoringMetrics = new ();
    private readonly Mock<IUserDataCollector> _mockUserDataCollector = new ();
    private readonly Mock<IUserDataHandler> _mockUserDataHandler = new ();

    private readonly UserDataModel _userDataModel = new (string.Empty);
    private readonly XYZStock _xYZStock = new();

    [Fact]
    public async Task When_XYZStockAPIIsCalled_Then_RetrieveDataAboutTheStock()
    {
        // Arrange
        _mockUserDataCollector.Setup(
            s => s.GetUserDataAsync(It.IsAny<HttpRequest>())).ReturnsAsync(this._userDataModel);
        _mockStocksDataRetriever.Setup(
            s => s.GetXYZStockAsync()).ReturnsAsync(this._xYZStock);

        var sut = GetSut();

        // Act
        var output = await sut.Get();

        // Assert
        _mockUserDataHandler.Verify(
            v => v.InsertUserData(It.IsAny<UserDataModel>(), It.IsAny<DbConnectionList>()), Times.Once);
        _mockMonitoringMetrics.Verify(
            v => v.IncrementUserMadeRequest(It.IsAny<string>()), Times.Once);
        
        output.Value.Should().BeOfType<XYZStock>();
        output.Value.Should().NotBeNull();
    }

    public XYZStockController GetSut()
    {
        return new XYZStockController(
            this._mockLogger.Object,
            this._mockStocksDataRetriever.Object,
            this._mockMonitoringMetrics.Object,
            this._mockUserDataCollector.Object,
            this._mockUserDataHandler.Object);
    }
}

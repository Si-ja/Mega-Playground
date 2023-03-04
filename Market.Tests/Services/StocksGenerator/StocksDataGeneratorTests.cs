using Data.Models.Stocks;
using DataAccess.DataHandler;
using DataAccess.Services.Connections;
using Market.Services.StocksGenerator;
using Microsoft.Extensions.Logging;

namespace Market.Tests.Services.StocksGenerator;

public class StocksDataGeneratorTests
{
    private readonly Mock<ILogger<StocksDataGenerator>> _mockLogger = new();
    private readonly Mock<IStocksDataHandler> _mockStocksDataHandler = new();

    [Fact]
    public async Task When_GenerateXYZStock_Then_StockDataGeneratedAndStored()
    {
        // Arrange
        _mockStocksDataHandler.Setup(
            s => s.InsertStockData(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>())).Verifiable();
        _mockStocksDataHandler.Setup(
            s => s.InsertHistoricalDataRecord(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>())).Verifiable();
        StockModel stockData = new StockModel(Name: string.Empty, Currency: string.Empty);

        var sut = GetSut();

        // Act
        var output = await sut.GenerateXYZStockAsync();

        // Assess
        output.Should().NotBeNull();
        output.Should().BeOfType<XYZStock>();

        output.Price.Should().BeGreaterThanOrEqualTo(0);

        _mockStocksDataHandler.Verify(
            v => v.InsertStockData(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>()), Times.Once);
        _mockStocksDataHandler.Verify(
            v => v.InsertHistoricalDataRecord(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>()), Times.Once);
    }

    [Fact]
    public async Task When_GenerateEvilCorpStock_Then_StockDataGeneratedAndStored()
    {
        // Arrange
        _mockStocksDataHandler.Setup(
            s => s.InsertStockData(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>())).Verifiable();
        _mockStocksDataHandler.Setup(
            s => s.InsertHistoricalDataRecord(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>())).Verifiable();
        StockModel stockData = new StockModel(Name: string.Empty, Currency: string.Empty);

        var sut = GetSut();

        // Act
        var output = await sut.GenerateEvilCorpStockAsync();

        // Assess
        output.Should().NotBeNull();
        output.Should().BeOfType<EvilCorpStock>();

        output.Price.Should().BeGreaterThanOrEqualTo(0);

        _mockStocksDataHandler.Verify(
            v => v.InsertStockData(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>()), Times.Once);
        _mockStocksDataHandler.Verify(
            v => v.InsertHistoricalDataRecord(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>()), Times.Once);
    }

    [Fact]
    public async Task When_GenerateHellStock_Then_StockDataGeneratedAndStored()
    {
        // Arrange
        _mockStocksDataHandler.Setup(
            s => s.InsertStockData(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>())).Verifiable();
        _mockStocksDataHandler.Setup(
            s => s.InsertHistoricalDataRecord(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>())).Verifiable();
        StockModel stockData = new StockModel(Name: string.Empty, Currency: string.Empty);

        var sut = GetSut();

        // Act
        var output = await sut.GenerateHellStockAsync();

        // Assess
        output.Should().NotBeNull();
        output.Should().BeOfType<HellStock>();

        output.Price.Should().BeGreaterThanOrEqualTo(0);

        _mockStocksDataHandler.Verify(
            v => v.InsertStockData(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>()), Times.Once);
        _mockStocksDataHandler.Verify(
            v => v.InsertHistoricalDataRecord(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>()), Times.Once);
    }

    [Fact]
    public void When_GetBlankStockRequested_Then_GiveBlankStockButNoOtherActions()
    {
        // Arrange
        _mockStocksDataHandler.Setup(
            s => s.InsertStockData(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>())).Verifiable();
        _mockStocksDataHandler.Setup(
            s => s.InsertHistoricalDataRecord(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>())).Verifiable();
        StockModel stockData = new StockModel(Name: string.Empty, Currency: string.Empty);

        var sut = GetSut();

        // Act
        var output = sut.GetBlankStock();

        // Assess
        output.Should().NotBeNull();
        output.Should().BeOfType<StockModel>();

        output.Price.Should().BeGreaterThanOrEqualTo(0);

        _mockStocksDataHandler.Verify(
            v => v.InsertStockData(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>()), Times.Never);
        _mockStocksDataHandler.Verify(
            v => v.InsertHistoricalDataRecord(It.IsAny<StockModel>(), It.IsAny<DbConnectionList>()), Times.Never);
    }


    private StocksDataGenerator GetSut()
    {
        return new StocksDataGenerator(
            _mockLogger.Object,
            _mockStocksDataHandler.Object);
    }
}

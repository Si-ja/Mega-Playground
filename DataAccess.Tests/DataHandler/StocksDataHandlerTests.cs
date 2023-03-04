using Data.Models.Stocks;
using DataAccess.DataHandler;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Services.Connections;

namespace DataAccess.Tests.DataHandler;

public class StocksDataHandlerTests
{
    private readonly Mock<ISqlDataAccess> _mockDb = new();

    [Fact]
    public async Task When_InsertStockDataCalled_Then_InsertIsInvokedOnce()
    {
        // Arrange
        var stockModel = new StockModel("Test", "Test");

        _mockDb.Setup(
            s => s.SaveData(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>())).Verifiable();

        var sut = GetSut();

        // Act
        await sut.InsertStockData(
            stockModel,
            It.IsAny<DbConnectionList>());

        // Assert
        _mockDb.Verify();
        _mockDb.Verify(
            v => v.SaveData(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>()), Times.Once);
    }

    [Fact]
    public async Task When_InsertHistoricalDataRecordCalled_Then_InsertIsInvokedOnce()
    {
        // Arrange
        var stockModel = new StockModel("Test", "Test");

        _mockDb.Setup(
            s => s.SaveData(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>())).Verifiable();

        var sut = GetSut();

        // Act
        await sut.InsertHistoricalDataRecord(
            stockModel,
            It.IsAny<DbConnectionList>());

        // Assert
        _mockDb.Verify();
        _mockDb.Verify(
            v => v.SaveData(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>()), Times.Once);
    }

    [Fact]
    public async Task When_GetStockPriceCalled_Then_ReturnStockPriceOnce()
    {
        // Arrange
        var stockModel = new StockModel("Test", "Test");
        var stockPriceReturned = 0;

        _mockDb.Setup(
            s => s.LoadSingleValue(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>())).ReturnsAsync(stockPriceReturned);

        var sut = GetSut();

        // Act
        var queryOutput = await sut.GetStockPrice(
            It.IsAny<string>(), 
            It.IsAny<DbConnectionList>());

        // Assert
        _mockDb.Verify();
        _mockDb.Verify(
            v => v.LoadSingleValue(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>()), Times.Once);
        
        queryOutput.Should().NotBeNull();
        queryOutput.Should().BeOfType<StockDataModel>();
        queryOutput.Price.Should().Be((float)stockPriceReturned);
    }

    [Fact]
    public async Task When_GetStockPriceCalledAndStockPriceNotFound_Then_ReturnBlankStockData()
    {
        // Arrange
        var stockModel = new StockModel("Test", "Test");

        _mockDb.Setup(
            s => s.LoadSingleValue(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>())).ReturnsAsync(null);

        var sut = GetSut();

        // Act
        var queryOutput = await sut.GetStockPrice(
            It.IsAny<string>(),
            It.IsAny<DbConnectionList>());

        // Assert
        _mockDb.Verify();
        _mockDb.Verify(
            v => v.LoadSingleValue(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>()), Times.Once);

        queryOutput.Should().NotBeNull();
        queryOutput.Should().BeOfType<StockDataModel>();
        queryOutput.Price.Should().Be(default);
        queryOutput.Name.Should().Be(default);
    }

    [Fact]
    public async Task When_GetListOfAvailableStocksCalled_Then_ReturnStocksListOnce()
    {
        // Arrange
        var listOfAvailableStocks = new List<StockReferencesModel>();
        listOfAvailableStocks.Add(new StockReferencesModel { StockName = "Test" });

        _mockDb.Setup(
            s => s.LoadList<StockReferencesModel>(
                It.IsAny<string>(),
                null,
                It.IsAny<DbConnectionList>())).ReturnsAsync(listOfAvailableStocks.AsEnumerable());

        var sut = GetSut();

        // Act
        var queryOutput = await sut.GetListOfAvailableStocks(
            It.IsAny<DbConnectionList>());

        // Assert
        _mockDb.Verify();
        _mockDb.Verify(
            v => v.LoadList<StockReferencesModel>(
                It.IsAny<string>(),
                null,
                It.IsAny<DbConnectionList>()), Times.Once);

        queryOutput.Should().NotBeNull();
        queryOutput.Should().BeOfType<List<StockReferencesModel>>();
        queryOutput.FirstOrDefault().Should().NotBeNull();
    }

    [Fact]
    public async Task When_GetListOfAvailableStocksAndStockNamesNotFound_Then_ReturnBlankList()
    {
        // Arrange
        var listOfAvailableStocks = new List<StockReferencesModel>();
        listOfAvailableStocks.Add(new StockReferencesModel { StockName = "Test" });

        _mockDb.Setup(
            s => s.LoadList<StockReferencesModel>(
                It.IsAny<string>(),
                null,
                It.IsAny<DbConnectionList>())).ReturnsAsync(null);

        var sut = GetSut();

        // Act
        var queryOutput = await sut.GetListOfAvailableStocks(
            It.IsAny<DbConnectionList>());

        // Assert
        _mockDb.Verify();
        _mockDb.Verify(
            v => v.LoadList<StockReferencesModel>(
                It.IsAny<string>(),
                null,
                It.IsAny<DbConnectionList>()), Times.Once);

        queryOutput.Should().BeNullOrEmpty();
    }

    public StocksDataHandler GetSut()
    {
        return new StocksDataHandler(
            _mockDb.Object);
    }
}

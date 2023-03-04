using DataAccess.DataHandler;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Services.Connections;

namespace DataAccess.Tests.DataHandler;

public class UserDataHandlerTests
{
    private readonly Mock<ISqlDataAccess> _mockDb = new ();

    [Fact]
    public async Task When_InsertUserDataInvoked_Then_SaveDataOnce()
    {
        // Arrange
        var mockUserDataModel = new UserDataModel("Time")
        {
            Browser = "Browser",
            Endpoint = "Endpoint"
        };

        _mockDb.Setup(
            s => s.SaveData(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>())).Verifiable();

        var sut = GetSut();

        // Act
        await sut.InsertUserData(mockUserDataModel, It.IsAny<DbConnectionList>());

        // Assert
        _mockDb.Verify();
        _mockDb.Verify(
            v => v.SaveData(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<DbConnectionList>()), Times.Once);
    }

    public UserDataHandler GetSut()
    {
        return new UserDataHandler(
            _mockDb.Object);
    }
}

using DataAccess.Services;
using DataAccess.Services.Connections;

using Npgsql;

namespace DataAccess.Tests.Services;

public class ConnectionFactoryTests
{
    private readonly Mock<IPostgresConnection> _mockPostgresConnection = new ();

    [Fact]
    public void When_WorkingWithPostgres_Then_ReturnPostgresConnectionWrapper()
    {
        // Arrange
        _mockPostgresConnection.Setup(
            s => s.GetConnection()).Returns(It.IsAny<NpgsqlConnection>());
        var sut = GetSut();

        // Act
        var connection = sut.ProduceConnection(DbConnectionList.Postgres);

        // Assert
        connection.Should().BeOfType<ConnectionWrapper>();
        connection.ConnectionName.Should().Be(DbConnectionList.Postgres.ToString());
    }

    [Fact]
    public void When_WorkingWithUnknownDb_Then_ThrowInvalidOperationException()
    {
        // Arrange
        var sut = GetSut();

        // Act
        Action action = () =>
        {
            sut.ProduceConnection((DbConnectionList)(-1));
        };

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    private ConnectionFactory GetSut()
    {
        return new ConnectionFactory(
            _mockPostgresConnection.Object);
    }
}

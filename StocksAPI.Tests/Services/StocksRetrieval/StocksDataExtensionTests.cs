using StocksAPI.Services.StocksRetrieval;

namespace StocksAPI.Tests.Services.StocksRetrieval
{
    public class StocksExtensionTests
    {
        [Theory]
        [InlineData(4.45f, 4.45)]
        [InlineData(5.5f, 5.50)]
        [InlineData(-10.4567f, -10.46)]
        public void When_ConventonalPriceIsCalled_Then_2DigitsAfterTheValueReturned(float price, decimal decimalPrice)
        {
            // Arrange

            // Act
            decimal conversion = price.ConventionalPrice();

            // Assess
            Assert.Equal(decimalPrice, conversion);
        }

        [Fact]
        public void When_ConventionalDataTimeCalled_Then_StandartizedDateIsReturned()
        {
            // Arrange
            DateTime date = new();
            string expectedDate = "0001-01-01 00:00:00";

            // Act
            string conversion = date.ConventionalDateTime();

            // Assert
            Assert.Equal(expectedDate, conversion);
        }
    }
}
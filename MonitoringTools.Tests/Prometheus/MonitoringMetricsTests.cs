using MonitoringTools.Prometheus;
using Prometheus;

namespace MonitoringTools.Tests.Prometheus;

public class MonitoringMetricsTests
{
    [Fact]
    public void When_DefaultAccessPointIncremented_Then_CounterCreatedWithOneValue()
    {
        // Arrange
        MonitoringMetrics monitoringMetrics = new();

        // Act
        monitoringMetrics.IncrementUserMadeRequest();

        // Assert
        Assert.IsType<Dictionary<string, Counter>>(monitoringMetrics.AmountOfUserRequests);
        Assert.Single(monitoringMetrics.AmountOfUserRequests);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(10)]
    public void When_DefaultAccessPointIncrementedMultipleTimes_Then_CounterCreatedWithMultipleInstances(int calls)
    {
        // Arrange
        MonitoringMetrics monitoringMetrics = new();

        // Act
        for (int i = 0; i < calls; i++)
        {
            monitoringMetrics.IncrementUserMadeRequest();
        }

        // Assert
        Assert.IsType<Dictionary<string, Counter>>(monitoringMetrics.AmountOfUserRequests);
        Assert.Single(monitoringMetrics.AmountOfUserRequests);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(10)]
    public void When_TargetedAccessPointIncrementedMultipleTimes_Then_MultipleCountersCreatedWithMultipleInstances(int calls)
    {
        // Arrange
        MonitoringMetrics monitoringMetrics = new();

        // Act
        for (int i = 0; i < calls; i++)
        {
            monitoringMetrics.IncrementUserMadeRequest($"EndPoint_{i}");
        }

        // Assert
        Assert.IsType<Dictionary<string, Counter>>(monitoringMetrics.AmountOfUserRequests);
        Assert.Equal(calls, monitoringMetrics.AmountOfUserRequests.Count);
    }
}

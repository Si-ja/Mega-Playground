using Prometheus;

namespace MonitoringTools.Prometheus;

public class MonitoringMetrics : IMonitoringMetrics
{
    private readonly string generalApiName = "general";
    public readonly Dictionary<string, Counter> AmountOfUserRequests = new();

    public void IncrementUserMadeRequest()
    {
        this.IncrementUserMadeRequest(this.generalApiName);
    }

    public void IncrementUserMadeRequest(string requestedEndpoint)
    {
        if (!this.AmountOfUserRequests.ContainsKey(requestedEndpoint))
        {
            var newCounter = Metrics.CreateCounter(
                requestedEndpoint,
                $"A user request has been made to the {requestedEndpoint} api endpoint");
            this.AmountOfUserRequests.Add(requestedEndpoint, newCounter);
        }

        this.AmountOfUserRequests[requestedEndpoint].Inc();
    }
}

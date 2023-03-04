namespace MonitoringTools.Prometheus;

public interface IMonitoringMetrics
{
    /// <summary>
    /// Increment a counter that keeps track of how many user requests to various
    /// API points come in. In case a name for the endpoint is not specified,
    /// the request will be marked as accessing some generic api.
    /// </summary>
    void IncrementUserMadeRequest();

    /// <summary>
    /// Increment a counter that keeps track of how many user requests to various
    /// API points come in.
    /// </summary>
    /// <param name="requestedEndpoint">Name of the endpoint or API that is being triggered.</param>
    void IncrementUserMadeRequest(string requestedEndpoint);
}
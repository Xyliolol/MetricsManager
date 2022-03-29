namespace MetricsManager.Response.Responses
{
    public class NetworkMetricResponse
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }

    public class NetworkGetMetricsFromAgentResponse
    {
        public List<NetworkMetricResponse> Metrics { get; set; }
    }

    public class NetworkGetMetricsFromAllClusterResponse
    {
        public List<NetworkMetricResponse> Metrics { get; set; }
    }
}
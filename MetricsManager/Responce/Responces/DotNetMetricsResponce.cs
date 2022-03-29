namespace MetricsManager.Response.Responses
{
    public class DotNetMetricResponse
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
    public class DotNetGetMetricsFromAgentResponse
    {
        public List<DotNetMetricResponse> Metrics { get; set; }
    }

    public class DotNetGetMetricsFromAllClusterResponse
    {
        public List<DotNetMetricResponse> Metrics { get; set; }
    }
}
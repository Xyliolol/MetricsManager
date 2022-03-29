namespace MetricsManager.Response.Responses
{
    public class RamMetricResponse
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }

    public class RamGetMetricsFromAgentResponse
    {
        public List<RamMetricResponse> Metrics { get; set; }
    }

    public class RamGetMetricsFromAllClusterResponse
    {
        public List<RamMetricResponse> Metrics { get; set; }
    }
}
namespace MetricsManager.Response.Responses
{
    public class HddMetricResponse
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }

    public class HddGetMetricFromAgentResponse
    {
        public List<HddMetricResponse> Metrics { get; set; }
    }

    public class HddGetMetricsFromAllClusterResponse
    {
        public List<HddMetricResponse> Metrics { get; set; }
    }
}
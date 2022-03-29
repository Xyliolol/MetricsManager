namespace MetricsManager.Response.Responses
{
    public class CpuMetricResponse
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }

    public class CpuGetMetricsFromAgentResponse
    {
        public List<CpuMetricResponse> Metrics { get; set; }
    }

    public class CpuGetMetricsFromAllClusterResponse
    {
        public List<CpuMetricResponse> Metrics { get; set; }
    }
}
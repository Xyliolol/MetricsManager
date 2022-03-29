namespace MetricsManager.Requests
{
    public class GetAllDotNetHeapMetrisApiRequest
    {
        public string AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
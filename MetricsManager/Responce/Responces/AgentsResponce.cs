namespace MetricsManager.Response.Responses
{
    public class AgentsResponse
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class GetRegisteredAgentsResponse
    {
        public IEnumerable<AgentsResponse> Agents { get; set; }
    }
}
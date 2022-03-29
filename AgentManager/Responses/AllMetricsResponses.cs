using AgentManager.Request;

namespace AgentManager.Responses
{
    public class AllMetricsResponses
    {
        public class CpuMetricResponse
        {
            public List<CpuMetricsDto> Metrics { get; set; }
        }

        public class DotNetMetricResponse
        {
            public List<DotNetMetricsDto> Metrics { get; set; }
        }

        public class HddMetricResponse
        {
            public List<HddMetricsDto> Metrics { get; set; }
        }

        public class NetworkMetricResponse
        {
            public List<NetworkMetricsDto> Metrics { get; set; }
        }

        public class RamMetricResponse
        {
            public List<RamMetricsDto> Metrics { get; set; }
        }
    }
}

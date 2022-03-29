using MetricsManager.DAL.Models;

namespace MetricsManager.Response
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetric> Metrics { get; set; }
    }
}
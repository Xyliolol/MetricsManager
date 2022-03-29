using MetricsManager.DAL.Models;

namespace MetricsManager.Response
{
    public class AllHddMetricsApiResponse
    {
        public List<HddMetric> Metrics { get; set; }
    }
}
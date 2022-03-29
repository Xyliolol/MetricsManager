using MetricsManager.DAL.Models;

namespace MetricsManager.Response
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetric> Metrics { get; set; }
    }
}